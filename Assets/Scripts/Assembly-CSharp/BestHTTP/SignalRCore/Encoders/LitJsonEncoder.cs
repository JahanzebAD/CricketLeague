using System;
using LitJson;

namespace BestHTTP.SignalRCore.Encoders
{
	public sealed class LitJsonEncoder : IEncoder
	{
		public string Name => "json";

		public LitJsonEncoder()
		{
			JsonMapper.RegisterImporter((ImporterFunc<int, long>)((int input) => input));
			JsonMapper.RegisterImporter((long input) => (int)input);
			JsonMapper.RegisterImporter((double input) => (int)(input + 0.5));
			JsonMapper.RegisterImporter((string input) => Convert.ToDateTime(input).ToUniversalTime());
			JsonMapper.RegisterImporter((double input) => (float)input);
		}

		public T DecodeAs<T>(string text)
		{
			return JsonMapper.ToObject<T>(text);
		}

		public T DecodeAs<T>(byte[] data)
		{
			throw new NotImplementedException();
		}

		public byte[] EncodeAsBinary<T>(T value)
		{
			throw new NotImplementedException();
		}

		public string EncodeAsText<T>(T value)
		{
			return JsonMapper.ToJson(value);
		}

		public object ConvertTo(Type toType, object obj)
		{
			string json = JsonMapper.ToJson(obj);
			return JsonMapper.ToObject(toType, json);
		}
	}
}
