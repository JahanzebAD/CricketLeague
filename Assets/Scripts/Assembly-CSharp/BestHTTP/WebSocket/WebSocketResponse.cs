using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using BestHTTP.Extensions;
using BestHTTP.WebSocket.Frames;

namespace BestHTTP.WebSocket
{
	public sealed class WebSocketResponse : HTTPResponse, IHeartbeat, IProtocol
	{
		public static int RTTBufferCapacity = 5;

		public Action<WebSocketResponse, string> OnText;

		public Action<WebSocketResponse, byte[]> OnBinary;

		public Action<WebSocketResponse, WebSocketFrameReader> OnIncompleteFrame;

		public Action<WebSocketResponse, ushort, string> OnClosed;

		private int _bufferedAmount;

		private List<WebSocketFrameReader> IncompleteFrames = new List<WebSocketFrameReader>();

		private List<WebSocketFrameReader> CompletedFrames = new List<WebSocketFrameReader>();

		private List<WebSocketFrameReader> frameCache = new List<WebSocketFrameReader>();

		private WebSocketFrameReader CloseFrame;

		private object FrameLock = new object();

		private object SendLock = new object();

		private List<WebSocketFrame> unsentFrames = new List<WebSocketFrame>();

		private AutoResetEvent newFrameSignal = new AutoResetEvent(initialState: false);

		private volatile bool sendThreadCreated;

		private volatile bool closeSent;

		private volatile bool closed;

		private DateTime lastPing = DateTime.MinValue;

		private DateTime lastMessage = DateTime.MinValue;

		private CircularBuffer<int> rtts = new CircularBuffer<int>(RTTBufferCapacity);

		public WebSocket WebSocket { get; internal set; }

		public bool IsClosed => closed;

		public TimeSpan PingFrequnecy { get; private set; }

		public ushort MaxFragmentSize { get; private set; }

		public int BufferedAmount => _bufferedAmount;

		public int Latency { get; private set; }

		internal WebSocketResponse(HTTPRequest request, Stream stream, bool isStreamed, bool isFromCache)
			: base(request, stream, isStreamed, isFromCache)
		{
			base.IsClosedManually = true;
			closed = false;
			MaxFragmentSize = 32767;
		}

		internal void StartReceive()
		{
			if (base.IsUpgraded)
			{
				ThreadPool.QueueUserWorkItem(ReceiveThreadFunc);
			}
		}

		internal void CloseStream()
		{
			HTTPManager.GetConnectionWith(baseRequest)?.Abort(HTTPConnectionStates.Closed);
		}

		public void Send(string message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message must not be null!");
			}
			byte[] bytes = Encoding.UTF8.GetBytes(message);
			Send(new WebSocketFrame(WebSocket, WebSocketFrameTypes.Text, bytes));
		}

		public void Send(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data must not be null!");
			}
			WebSocketFrame webSocketFrame = new WebSocketFrame(WebSocket, WebSocketFrameTypes.Binary, data);
			if (webSocketFrame.Data != null && webSocketFrame.Data.Length > MaxFragmentSize)
			{
				WebSocketFrame[] array = webSocketFrame.Fragment(MaxFragmentSize);
				lock (SendLock)
				{
					Send(webSocketFrame);
					if (array != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							Send(array[i]);
						}
					}
				}
			}
			else
			{
				Send(webSocketFrame);
			}
		}

		public void Send(byte[] data, ulong offset, ulong count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data must not be null!");
			}
			if (offset + count > (ulong)data.Length)
			{
				throw new ArgumentOutOfRangeException("offset + count >= data.Length");
			}
			WebSocketFrame webSocketFrame = new WebSocketFrame(WebSocket, WebSocketFrameTypes.Binary, data, offset, count, isFinal: true, useExtensions: true);
			if (webSocketFrame.Data != null && webSocketFrame.Data.Length > MaxFragmentSize)
			{
				WebSocketFrame[] array = webSocketFrame.Fragment(MaxFragmentSize);
				lock (SendLock)
				{
					Send(webSocketFrame);
					if (array != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							Send(array[i]);
						}
					}
				}
			}
			else
			{
				Send(webSocketFrame);
			}
		}

		public void Send(WebSocketFrame frame)
		{
			if (frame == null)
			{
				throw new ArgumentNullException("frame is null!");
			}
			if (closed || closeSent)
			{
				return;
			}
			lock (SendLock)
			{
				unsentFrames.Add(frame);
				if (!sendThreadCreated)
				{
					HTTPManager.Logger.Information("WebSocketResponse", "Send - Creating thread");
					ThreadPool.QueueUserWorkItem(SendThreadFunc);
					sendThreadCreated = true;
				}
			}
			Interlocked.Add(ref _bufferedAmount, (frame.Data != null) ? frame.Data.Length : 0);
			newFrameSignal.Set();
		}

		public void Insert(WebSocketFrame frame)
		{
			if (frame == null)
			{
				throw new ArgumentNullException("frame is null!");
			}
			if (closed || closeSent)
			{
				return;
			}
			lock (SendLock)
			{
				unsentFrames.Insert(0, frame);
				if (!sendThreadCreated)
				{
					HTTPManager.Logger.Information("WebSocketResponse", "Insert - Creating thread");
					ThreadPool.QueueUserWorkItem(SendThreadFunc);
					sendThreadCreated = true;
				}
			}
			Interlocked.Add(ref _bufferedAmount, (frame.Data != null) ? frame.Data.Length : 0);
			newFrameSignal.Set();
		}

		public void SendNow(WebSocketFrame frame)
		{
			if (frame == null)
			{
				throw new ArgumentNullException("frame is null!");
			}
			if (!closed && !closeSent)
			{
				byte[] array = frame.Get();
				Stream.Write(array, 0, array.Length);
				Stream.Flush();
			}
		}

		public void Close()
		{
			Close(1000, "Bye!");
		}

		public void Close(ushort code, string msg)
		{
			if (!closed)
			{
				lock (SendLock)
				{
					unsentFrames.Clear();
				}
				Interlocked.Exchange(ref _bufferedAmount, 0);
				Send(new WebSocketFrame(WebSocket, WebSocketFrameTypes.ConnectionClose, WebSocket.EncodeCloseData(code, msg)));
			}
		}

		public void StartPinging(int frequency)
		{
			if (frequency < 100)
			{
				throw new ArgumentException("frequency must be at least 100 milliseconds!");
			}
			PingFrequnecy = TimeSpan.FromMilliseconds(frequency);
			lastMessage = DateTime.UtcNow;
			SendPing();
			HTTPManager.Heartbeats.Subscribe(this);
			HTTPUpdateDelegator.OnApplicationForegroundStateChanged = (Action<bool>)Delegate.Combine(HTTPUpdateDelegator.OnApplicationForegroundStateChanged, new Action<bool>(OnApplicationForegroundStateChanged));
		}

		private void SendThreadFunc(object param)
		{
			List<WebSocketFrame> list = new List<WebSocketFrame>();
			try
			{
				while (!closed && !closeSent)
				{
					newFrameSignal.WaitOne();
					try
					{
						lock (SendLock)
						{
							for (int num = unsentFrames.Count - 1; num >= 0; num--)
							{
								list.Add(unsentFrames[num]);
							}
							unsentFrames.Clear();
						}
						while (list.Count > 0)
						{
							WebSocketFrame webSocketFrame = list[list.Count - 1];
							list.RemoveAt(list.Count - 1);
							if (!closeSent)
							{
								byte[] array = webSocketFrame.Get();
								Stream.Write(array, 0, array.Length);
								if (webSocketFrame.Type == WebSocketFrameTypes.ConnectionClose)
								{
									closeSent = true;
								}
							}
							Interlocked.Add(ref _bufferedAmount, -webSocketFrame.Data.Length);
						}
						Stream.Flush();
					}
					catch (Exception exception)
					{
						if (HTTPUpdateDelegator.IsCreated)
						{
							baseRequest.Exception = exception;
							baseRequest.State = HTTPRequestStates.Error;
						}
						else
						{
							baseRequest.State = HTTPRequestStates.Aborted;
						}
						closed = true;
					}
				}
			}
			finally
			{
				sendThreadCreated = false;
				HTTPManager.Logger.Information("WebSocketResponse", "SendThread - Closed!");
			}
		}

		private void ReceiveThreadFunc(object param)
		{
			try
			{
				while (!closed)
				{
					try
					{
						WebSocketFrameReader webSocketFrameReader = new WebSocketFrameReader();
						webSocketFrameReader.Read(Stream);
						lastMessage = DateTime.UtcNow;
						if (webSocketFrameReader.HasMask)
						{
							Close(1002, "Protocol Error: masked frame received from server!");
							continue;
						}
						if (!webSocketFrameReader.IsFinal)
						{
							if (OnIncompleteFrame == null)
							{
								IncompleteFrames.Add(webSocketFrameReader);
								continue;
							}
							lock (FrameLock)
							{
								CompletedFrames.Add(webSocketFrameReader);
							}
							continue;
						}
						switch (webSocketFrameReader.Type)
						{
						case WebSocketFrameTypes.Continuation:
							if (OnIncompleteFrame == null)
							{
								webSocketFrameReader.Assemble(IncompleteFrames);
								IncompleteFrames.Clear();
								goto case WebSocketFrameTypes.Text;
							}
							lock (FrameLock)
							{
								CompletedFrames.Add(webSocketFrameReader);
							}
							break;
						case WebSocketFrameTypes.Text:
						case WebSocketFrameTypes.Binary:
							webSocketFrameReader.DecodeWithExtensions(WebSocket);
							lock (FrameLock)
							{
								CompletedFrames.Add(webSocketFrameReader);
							}
							break;
						case WebSocketFrameTypes.Ping:
							if (!closeSent && !closed)
							{
								Send(new WebSocketFrame(WebSocket, WebSocketFrameTypes.Pong, webSocketFrameReader.Data));
							}
							break;
						case WebSocketFrameTypes.Pong:
							try
							{
								long num = BitConverter.ToInt64(webSocketFrameReader.Data, 0);
								TimeSpan timeSpan = TimeSpan.FromTicks(lastMessage.Ticks - num);
								rtts.Add((int)timeSpan.TotalMilliseconds);
								Latency = CalculateLatency();
							}
							catch
							{
							}
							break;
						case WebSocketFrameTypes.ConnectionClose:
							CloseFrame = webSocketFrameReader;
							if (!closeSent)
							{
								Send(new WebSocketFrame(WebSocket, WebSocketFrameTypes.ConnectionClose, null));
							}
							closed = true;
							break;
						}
					}
					catch (ThreadAbortException)
					{
						IncompleteFrames.Clear();
						baseRequest.State = HTTPRequestStates.Aborted;
						closed = true;
						newFrameSignal.Set();
					}
					catch (Exception exception)
					{
						if (HTTPUpdateDelegator.IsCreated)
						{
							baseRequest.Exception = exception;
							baseRequest.State = HTTPRequestStates.Error;
						}
						else
						{
							baseRequest.State = HTTPRequestStates.Aborted;
						}
						closed = true;
						newFrameSignal.Set();
					}
				}
			}
			finally
			{
				HTTPManager.Heartbeats.Unsubscribe(this);
				HTTPUpdateDelegator.OnApplicationForegroundStateChanged = (Action<bool>)Delegate.Remove(HTTPUpdateDelegator.OnApplicationForegroundStateChanged, new Action<bool>(OnApplicationForegroundStateChanged));
				HTTPManager.Logger.Information("WebSocketResponse", "ReceiveThread - Closed!");
			}
		}

		void IProtocol.HandleEvents()
		{
			frameCache.Clear();
			lock (FrameLock)
			{
				frameCache.AddRange(CompletedFrames);
				CompletedFrames.Clear();
			}
			for (int i = 0; i < frameCache.Count; i++)
			{
				WebSocketFrameReader webSocketFrameReader = frameCache[i];
				try
				{
					WebSocketFrameTypes type = webSocketFrameReader.Type;
					if (type == WebSocketFrameTypes.Continuation)
					{
						goto IL_0074;
					}
					if (type != WebSocketFrameTypes.Text)
					{
						if (type == WebSocketFrameTypes.Binary)
						{
							if (!webSocketFrameReader.IsFinal)
							{
								goto IL_0074;
							}
							if (OnBinary != null)
							{
								OnBinary(this, webSocketFrameReader.Data);
							}
						}
					}
					else
					{
						if (!webSocketFrameReader.IsFinal)
						{
							goto IL_0074;
						}
						if (OnText != null)
						{
							OnText(this, webSocketFrameReader.DataAsText);
						}
					}
					goto end_IL_0054;
					IL_0074:
					if (OnIncompleteFrame != null)
					{
						OnIncompleteFrame(this, webSocketFrameReader);
					}
					end_IL_0054:;
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("WebSocketResponse", "HandleEvents", ex);
				}
			}
			frameCache.Clear();
			if (!IsClosed || OnClosed == null || baseRequest.State != HTTPRequestStates.Processing)
			{
				return;
			}
			try
			{
				ushort arg = 0;
				string arg2 = string.Empty;
				if (CloseFrame != null && CloseFrame.Data != null && CloseFrame.Data.Length >= 2)
				{
					if (BitConverter.IsLittleEndian)
					{
						Array.Reverse(CloseFrame.Data, 0, 2);
					}
					arg = BitConverter.ToUInt16(CloseFrame.Data, 0);
					if (CloseFrame.Data.Length > 2)
					{
						arg2 = Encoding.UTF8.GetString(CloseFrame.Data, 2, CloseFrame.Data.Length - 2);
					}
				}
				OnClosed(this, arg, arg2);
			}
			catch (Exception ex2)
			{
				HTTPManager.Logger.Exception("WebSocketResponse", "HandleEvents - OnClosed", ex2);
			}
		}

		void IHeartbeat.OnHeartbeatUpdate(TimeSpan dif)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow - lastPing >= PingFrequnecy)
			{
				SendPing();
			}
			if (utcNow - (lastMessage + PingFrequnecy) > WebSocket.CloseAfterNoMesssage)
			{
				HTTPManager.Logger.Warning("WebSocketResponse", $"No message received in the given time! Closing WebSocket. LastMessage: {lastMessage}, PingFrequency: {PingFrequnecy}, Close After: {WebSocket.CloseAfterNoMesssage}, Now: {utcNow}");
				CloseWithError("No message received in the given time!");
			}
		}

		private void OnApplicationForegroundStateChanged(bool isPaused)
		{
			if (!isPaused)
			{
				lastMessage = DateTime.UtcNow;
			}
		}

		private void SendPing()
		{
			lastPing = DateTime.UtcNow;
			try
			{
				long ticks = DateTime.UtcNow.Ticks;
				byte[] bytes = BitConverter.GetBytes(ticks);
				WebSocketFrame frame = new WebSocketFrame(WebSocket, WebSocketFrameTypes.Ping, bytes);
				Insert(frame);
			}
			catch
			{
				HTTPManager.Logger.Information("WebSocketResponse", "Error while sending PING message! Closing WebSocket.");
				CloseWithError("Error while sending PING message!");
			}
		}

		private void CloseWithError(string message)
		{
			baseRequest.Exception = new Exception(message);
			baseRequest.State = HTTPRequestStates.Error;
			closed = true;
			HTTPManager.Heartbeats.Unsubscribe(this);
			HTTPUpdateDelegator.OnApplicationForegroundStateChanged = (Action<bool>)Delegate.Remove(HTTPUpdateDelegator.OnApplicationForegroundStateChanged, new Action<bool>(OnApplicationForegroundStateChanged));
			newFrameSignal.Set();
			CloseStream();
		}

		private int CalculateLatency()
		{
			if (rtts.Count == 0)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < rtts.Count; i++)
			{
				num += rtts[i];
			}
			return num / rtts.Count;
		}
	}
}
