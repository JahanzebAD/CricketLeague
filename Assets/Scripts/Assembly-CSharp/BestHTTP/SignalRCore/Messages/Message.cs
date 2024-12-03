namespace BestHTTP.SignalRCore.Messages
{
	public class Message
	{
		public MessageTypes type;

		public string invocationId;

		public bool nonblocking;

		public string target;

		public object[] arguments;

		public object item;

		public object result;

		public string error;

		public override string ToString()
		{
			return type switch
			{
				MessageTypes.Invocation => $"[Invocation Id: {invocationId}, Target: '{target}', Argument count: {((arguments != null) ? arguments.Length : 0)}]", 
				MessageTypes.StreamItem => $"[StreamItem Id: {invocationId}, Item: {item.ToString()}]", 
				MessageTypes.Completion => $"[Completion Id: {invocationId}, Result: {result}, Error: '{error}']", 
				MessageTypes.StreamInvocation => $"[StreamInvocation Id: {invocationId}, Target: '{target}', Argument count: {((arguments != null) ? arguments.Length : 0)}]", 
				MessageTypes.CancelInvocation => $"[CancelInvocation Id: {invocationId}]", 
				MessageTypes.Ping => "[Ping]", 
				MessageTypes.Close => (!string.IsNullOrEmpty(error)) ? $"[Close {error}]" : "[Close]", 
				_ => "Unknown message! Type: " + type, 
			};
		}
	}
}
