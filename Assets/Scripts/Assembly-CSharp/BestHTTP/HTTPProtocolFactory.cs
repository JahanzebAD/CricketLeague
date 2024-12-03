using System;
using System.IO;
using BestHTTP.ServerSentEvents;
using BestHTTP.WebSocket;

namespace BestHTTP
{
	internal static class HTTPProtocolFactory
	{
		public static HTTPResponse Get(SupportedProtocols protocol, HTTPRequest request, Stream stream, bool isStreamed, bool isFromCache)
		{
			return protocol switch
			{
				SupportedProtocols.WebSocket => new WebSocketResponse(request, stream, isStreamed, isFromCache), 
				SupportedProtocols.ServerSentEvents => new EventSourceResponse(request, stream, isStreamed, isFromCache), 
				_ => new HTTPResponse(request, stream, isStreamed, isFromCache), 
			};
		}

		public static SupportedProtocols GetProtocolFromUri(Uri uri)
		{
			if (uri == null || uri.Scheme == null)
			{
				throw new Exception("Malformed URI in GetProtocolFromUri");
			}
			switch (uri.Scheme.ToLowerInvariant())
			{
			case "ws":
			case "wss":
				return SupportedProtocols.WebSocket;
			default:
				return SupportedProtocols.HTTP;
			}
		}

		public static bool IsSecureProtocol(Uri uri)
		{
			if (uri == null || uri.Scheme == null)
			{
				throw new Exception("Malformed URI in IsSecureProtocol");
			}
			switch (uri.Scheme.ToLowerInvariant())
			{
			case "https":
			case "wss":
				return true;
			default:
				return false;
			}
		}
	}
}
