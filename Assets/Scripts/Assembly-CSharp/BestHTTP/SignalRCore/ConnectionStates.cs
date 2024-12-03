namespace BestHTTP.SignalRCore
{
	public enum ConnectionStates
	{
		Initial,
		Authenticating,
		Negotiating,
		Connected,
		CloseInitiated,
		Closed
	}
}
