namespace BestHTTP.SignalRCore
{
	public sealed class HubOptions
	{
		public bool SkipNegotiation { get; set; }

		public TransportTypes PreferedTransport { get; set; }
	}
}
