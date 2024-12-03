using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeStage.AntiCheat.Detectors
{
	[AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Time Cheating Detector")]
	[HelpURL("http://codestage.net/uas_files/actk/api/class_code_stage_1_1_anti_cheat_1_1_detectors_1_1_time_cheating_detector.html")]
	public class TimeCheatingDetector : ActDetectorBase
	{
		public enum TimeCheatingDetectorResult
		{
			Unknown = 0,
			CheckPassed = 5,
			CheatDetected = 10,
			Error = 0xF
		}

		public enum ErrorKind
		{
			NoError = 0,
			CantResolveHost = 5,
			Unknown = 10
		}

		internal const string ComponentName = "Time Cheating Detector";

		private const string LogPrefix = "[ACTk] Time Cheating Detector: ";

		private static int instancesInScene;

		private const int NtpDataBufferLength = 48;

		[Tooltip("Time (in minutes) between detector checks.")]
		[Range(0f, 60f)]
		public float interval = 1f;

		[Tooltip("Maximum allowed difference between online and offline time, in minutes.")]
		public int threshold = 65;

		public string timeServer = "pool.ntp.org";

		private readonly DateTime date1900 = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private readonly WaitForEndOfFrame cachedEndOfFrame = new WaitForEndOfFrame();

		private Socket asyncSocket;

		private byte[] ntpData = new byte[48];

		private byte[] targetIP;

		private IPEndPoint targetEndpoint;

		private SocketAsyncEventArgs connectArgs;

		private SocketAsyncEventArgs sendArgs;

		private SocketAsyncEventArgs receiveArgs;

		private float timeElapsed;

		private double lastOnlineTime;

		private bool gettingOnlineTimeAsync;

		private ErrorKind asyncError;

		public bool IsCheckingForCheat { get; private set; }

		public ErrorKind LastError { get; private set; }

		public TimeCheatingDetectorResult LastResult { get; private set; }

		public static TimeCheatingDetector Instance { get; private set; }

		private static TimeCheatingDetector GetOrCreateInstance
		{
			get
			{
				if (Instance != null)
				{
					return Instance;
				}
				if (ActDetectorBase.detectorsContainer == null)
				{
					ActDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
				}
				Instance = ActDetectorBase.detectorsContainer.AddComponent<TimeCheatingDetector>();
				return Instance;
			}
		}

		public event Action<ErrorKind> Error;

		public event Action CheckPassed;

		[Obsolete("Please use StartDetection(int, ...) instead.")]
		public static void StartDetection(Action detectionCallback, int interval)
		{
			StartDetection(interval, detectionCallback);
		}

		[Obsolete("Please use StartDetection(int, ...) instead.")]
		public static void StartDetection(Action detectionCallback, Action<ErrorKind> errorCallback, int interval)
		{
			StartDetection(interval, detectionCallback, errorCallback);
		}

		public static TimeCheatingDetector AddToSceneOrGetExisting()
		{
			return GetOrCreateInstance;
		}

		public static void StartDetection(Action detectionCallback = null, Action<ErrorKind> errorCallback = null, Action checkPassedCallback = null)
		{
			if (detectionCallback == null)
			{
				if (Instance != null)
				{
					Instance.StartDetectionInternal(Instance.interval, null, checkPassedCallback, errorCallback);
				}
			}
			else
			{
				StartDetection(GetOrCreateInstance.interval, detectionCallback, errorCallback, checkPassedCallback);
			}
		}

		public static void StartDetection(float intervalMinutes, Action detectionCallback = null, Action<ErrorKind> errorCallback = null, Action checkPassedCallback = null)
		{
			GetOrCreateInstance.StartDetectionInternal(intervalMinutes, detectionCallback, checkPassedCallback, errorCallback);
		}

		public static void StopDetection()
		{
			if (Instance != null)
			{
				Instance.StopDetectionInternal();
			}
		}

		[Obsolete("Please use Instance.Error event instead.", true)]
		public static void SetErrorCallback(Action<ErrorKind> errorCallback)
		{
		}

		public static void Dispose()
		{
			if (Instance != null)
			{
				Instance.DisposeInternal();
			}
		}

		private void Awake()
		{
			instancesInScene++;
			if (Init(Instance, "Time Cheating Detector"))
			{
				Instance = this;
			}
			SceneManager.sceneLoaded += OnLevelWasLoadedNew;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			instancesInScene--;
		}

		private void OnLevelWasLoadedNew(Scene scene, LoadSceneMode mode)
		{
			OnLevelLoadedCallback();
		}

		private void OnLevelLoadedCallback()
		{
			if (instancesInScene < 2)
			{
				if (!keepAlive)
				{
					DisposeInternal();
				}
			}
			else if (!keepAlive && Instance != this)
			{
				DisposeInternal();
			}
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			if (started)
			{
				if (pauseStatus)
				{
					PauseDetector();
				}
				else
				{
					ResumeDetector();
				}
			}
		}

		private void Update()
		{
			if (started && isRunning && interval > 0f)
			{
				timeElapsed += Time.unscaledDeltaTime;
				if (timeElapsed >= interval * 60f)
				{
					timeElapsed = 0f;
					StartCoroutine(CheckForCheat());
				}
			}
		}

		public bool ForceCheck()
		{
			if (!started || !isRunning)
			{
				LastError = ErrorKind.Unknown;
				LastResult = TimeCheatingDetectorResult.Error;
				return false;
			}
			if (IsCheckingForCheat)
			{
				LastError = ErrorKind.Unknown;
				LastResult = TimeCheatingDetectorResult.Error;
				return false;
			}
			if (!DetectorHasCallbacks())
			{
				LastError = ErrorKind.Unknown;
				LastResult = TimeCheatingDetectorResult.Error;
				return false;
			}
			timeElapsed = 0f;
			StartCoroutine(CheckForCheat());
			return true;
		}

		public IEnumerator ForceCheckEnumerator()
		{
			if (!started || !isRunning)
			{
				LastError = ErrorKind.Unknown;
				LastResult = TimeCheatingDetectorResult.Error;
				yield break;
			}
			if (IsCheckingForCheat)
			{
				while (IsCheckingForCheat)
				{
					yield return cachedEndOfFrame;
				}
			}
			if (!DetectorHasCallbacks())
			{
				LastError = ErrorKind.Unknown;
				LastResult = TimeCheatingDetectorResult.Error;
				yield break;
			}
			timeElapsed = 0f;
			StartCoroutine(CheckForCheat());
			yield return null;
			if (IsCheckingForCheat)
			{
				while (IsCheckingForCheat)
				{
					yield return cachedEndOfFrame;
				}
			}
		}

		private void StartDetectionInternal(float checkInterval, Action detectionCallback, Action checkPassedCallback, Action<ErrorKind> errorCallback)
		{
			if (isRunning || !base.enabled)
			{
				return;
			}
			if (detectionCallback == null || detectionEventHasListener)
			{
			}
			if (detectionCallback == null && !detectionEventHasListener)
			{
				base.enabled = false;
				return;
			}
			timeElapsed = 0f;
			base.CheatDetected += detectionCallback;
			if (errorCallback != null)
			{
				Error += errorCallback;
			}
			if (checkPassedCallback != null)
			{
				CheckPassed += checkPassedCallback;
			}
			interval = checkInterval;
			started = true;
			isRunning = true;
		}

		protected override void StartDetectionAutomatically()
		{
			StartDetectionInternal(interval, null, null, null);
		}

		protected override bool DetectorHasCallbacks()
		{
			return base.DetectorHasCallbacks() || this.Error != null || this.CheckPassed != null;
		}

		protected override void PauseDetector()
		{
			base.PauseDetector();
			timeElapsed = 0f;
		}

		protected override void StopDetectionInternal()
		{
			base.StopDetectionInternal();
			this.Error = null;
			this.CheckPassed = null;
			CloseSocket();
		}

		protected override void DisposeInternal()
		{
			if (Instance == this)
			{
				Instance = null;
			}
			base.DisposeInternal();
		}

		private IEnumerator CheckForCheat()
		{
			if (!isRunning || IsCheckingForCheat)
			{
				yield break;
			}
			IsCheckingForCheat = true;
			LastError = ErrorKind.NoError;
			LastResult = TimeCheatingDetectorResult.Unknown;
			yield return StartCoroutine(GetOnlineTimeInternal());
			if (!started || !isRunning)
			{
				LastError = ErrorKind.Unknown;
			}
			if (lastOnlineTime <= 0.0 && LastError == ErrorKind.NoError)
			{
				LastError = ErrorKind.Unknown;
			}
			if (LastError != 0)
			{
				LastResult = TimeCheatingDetectorResult.Error;
				if (this.Error != null)
				{
					this.Error(LastError);
				}
				IsCheckingForCheat = false;
				yield break;
			}
			double offlineTime = GetLocalTime();
			TimeSpan onlineTimeSpan = new TimeSpan((long)lastOnlineTime * 10000);
			TimeSpan offlineTimeSpan = new TimeSpan((long)offlineTime * 10000);
			double minutesDifference = onlineTimeSpan.TotalMinutes - offlineTimeSpan.TotalMinutes;
			if (Math.Abs(minutesDifference) > (double)threshold)
			{
				LastResult = TimeCheatingDetectorResult.CheatDetected;
				OnCheatingDetected();
			}
			else
			{
				LastResult = TimeCheatingDetectorResult.CheckPassed;
				if (this.CheckPassed != null)
				{
					this.CheckPassed();
				}
			}
			IsCheckingForCheat = false;
		}

		private IEnumerator GetOnlineTimeInternal()
		{
			gettingOnlineTimeAsync = false;
			lastOnlineTime = 0.0;
			asyncError = ErrorKind.NoError;
			IPAddress[] addresses = null;
			try
			{
				addresses = Dns.GetHostEntry(timeServer).AddressList;
			}
			catch (Exception)
			{
				LastError = ErrorKind.CantResolveHost;
			}
			if (addresses == null || addresses.Length == 0)
			{
				LastError = ErrorKind.CantResolveHost;
			}
			if (LastError != 0)
			{
				yield break;
			}
			float timeBeforeAsyncCall3 = Time.unscaledTime;
			try
			{
				if (asyncSocket == null)
				{
					asyncSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
					asyncSocket.SendTimeout = 3000;
					asyncSocket.ReceiveTimeout = 3000;
				}
				IPAddress iPAddress = addresses[0];
				byte[] addressBytes = iPAddress.GetAddressBytes();
				if (addressBytes != targetIP)
				{
					targetEndpoint = new IPEndPoint(iPAddress, 123);
					targetIP = addressBytes;
				}
				if (connectArgs == null)
				{
					connectArgs = new SocketAsyncEventArgs();
					connectArgs.Completed += OnSocketConnectedOrSent;
				}
				connectArgs.RemoteEndPoint = targetEndpoint;
				gettingOnlineTimeAsync = true;
				if (!asyncSocket.ConnectAsync(connectArgs))
				{
					OnSocketConnectedOrSent(asyncSocket, connectArgs);
				}
			}
			catch (Exception exception)
			{
				HandleSocketException(exception);
			}
			while (gettingOnlineTimeAsync)
			{
				if (Time.unscaledTime - timeBeforeAsyncCall3 > 4f)
				{
					LastError = ErrorKind.Unknown;
					CloseSocket();
					yield break;
				}
				yield return cachedEndOfFrame;
			}
			if (asyncError != 0)
			{
				LastError = asyncError;
			}
			if (LastError != 0)
			{
				CloseSocket();
				yield break;
			}
			ntpData[0] = 27;
			if (sendArgs == null)
			{
				sendArgs = new SocketAsyncEventArgs();
				sendArgs.Completed += OnSocketConnectedOrSent;
				sendArgs.UserToken = asyncSocket;
				sendArgs.SetBuffer(ntpData, 0, 48);
			}
			sendArgs.RemoteEndPoint = targetEndpoint;
			timeBeforeAsyncCall3 = Time.unscaledTime;
			try
			{
				gettingOnlineTimeAsync = true;
				if (!asyncSocket.SendAsync(sendArgs))
				{
					OnSocketConnectedOrSent(asyncSocket, sendArgs);
				}
			}
			catch (Exception exception2)
			{
				HandleSocketException(exception2);
			}
			while (gettingOnlineTimeAsync)
			{
				if (Time.unscaledTime - timeBeforeAsyncCall3 > 4f)
				{
					LastError = ErrorKind.Unknown;
					CloseSocket();
					yield break;
				}
				yield return cachedEndOfFrame;
			}
			if (asyncError != 0)
			{
				LastError = asyncError;
			}
			if (LastError != 0)
			{
				CloseSocket();
				yield break;
			}
			if (receiveArgs == null)
			{
				receiveArgs = new SocketAsyncEventArgs();
				receiveArgs.Completed += OnSocketReceive;
				receiveArgs.UserToken = asyncSocket;
				receiveArgs.SetBuffer(ntpData, 0, 48);
			}
			receiveArgs.RemoteEndPoint = targetEndpoint;
			timeBeforeAsyncCall3 = Time.unscaledTime;
			try
			{
				gettingOnlineTimeAsync = true;
				if (!asyncSocket.ReceiveAsync(receiveArgs))
				{
					OnSocketReceive(asyncSocket, receiveArgs);
				}
			}
			catch (Exception exception3)
			{
				HandleSocketException(exception3);
			}
			while (gettingOnlineTimeAsync)
			{
				if (Time.unscaledTime - timeBeforeAsyncCall3 > 4f)
				{
					LastError = ErrorKind.Unknown;
					CloseSocket();
					yield break;
				}
				yield return cachedEndOfFrame;
			}
			if (asyncError != 0)
			{
				LastError = asyncError;
			}
			if (LastError != 0)
			{
				CloseSocket();
				yield break;
			}
			ulong intc = ((ulong)ntpData[40] << 24) | ((ulong)ntpData[41] << 16) | ((ulong)ntpData[42] << 8) | ntpData[43];
			ulong frac = ((ulong)ntpData[44] << 24) | ((ulong)ntpData[45] << 16) | ((ulong)ntpData[46] << 8) | ntpData[47];
			lastOnlineTime = (double)intc * 1000.0 + (double)frac * 1000.0 / 4294967296.0;
		}

		private void OnSocketConnectedOrSent(object sender, SocketAsyncEventArgs e)
		{
			try
			{
				if (e.SocketError != 0)
				{
					asyncError = ErrorKind.Unknown;
				}
			}
			catch (Exception)
			{
				asyncError = ErrorKind.Unknown;
			}
			finally
			{
				gettingOnlineTimeAsync = false;
			}
		}

		private void OnSocketReceive(object sender, SocketAsyncEventArgs e)
		{
			try
			{
				if (e.SocketError == SocketError.Success)
				{
					ntpData = e.Buffer;
				}
				else
				{
					asyncError = ErrorKind.Unknown;
				}
			}
			catch (Exception)
			{
				asyncError = ErrorKind.Unknown;
			}
			finally
			{
				gettingOnlineTimeAsync = false;
			}
		}

		private void CloseSocket()
		{
			if (asyncSocket != null)
			{
				asyncSocket.Shutdown(SocketShutdown.Both);
				asyncSocket.Close();
				asyncSocket = null;
			}
			gettingOnlineTimeAsync = false;
		}

		private void HandleSocketException(Exception exception)
		{
			LastError = ErrorKind.Unknown;
			SocketException ex = exception as SocketException;
			if (ex != null && ex.SocketErrorCode == SocketError.HostNotFound)
			{
				LastError = ErrorKind.CantResolveHost;
			}
			gettingOnlineTimeAsync = false;
		}

		private double GetLocalTime()
		{
			return DateTime.UtcNow.Subtract(date1900).TotalMilliseconds;
		}

		public static double GetOnlineTime(string server)
		{
			try
			{
				byte[] array = new byte[48];
				array[0] = 27;
				IPAddress[] addressList = Dns.GetHostEntry(server).AddressList;
				Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				socket.Connect(new IPEndPoint(addressList[0], 123));
				socket.ReceiveTimeout = 3000;
				socket.Send(array);
				socket.Receive(array);
				socket.Close();
				ulong num = ((ulong)array[40] << 24) | ((ulong)array[41] << 16) | ((ulong)array[42] << 8) | array[43];
				ulong num2 = ((ulong)array[44] << 24) | ((ulong)array[45] << 16) | ((ulong)array[46] << 8) | array[47];
				return (double)num * 1000.0 + (double)num2 * 1000.0 / 4294967296.0;
			}
			catch (Exception)
			{
				return -1.0;
			}
		}
	}
}
