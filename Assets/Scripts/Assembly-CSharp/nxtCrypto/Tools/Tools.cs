using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using nxtCrypto.PackData;
using nxtCrypto.SecurityState;

namespace nxtCrypto.Tools
{
	public static class Tools
	{
		public static byte[] ConvertStringToByteArray(string KeyString)
		{
			return Encoding.UTF8.GetBytes(KeyString);
		}

		public static string ConvertByteArrayToString(byte[] KeyBytes)
		{
			return Encoding.UTF8.GetString(KeyBytes);
		}

		public static byte[] PackdataToByteArray(nxtCrypto.PackData.PackData Data)
		{
			if (Data == null)
			{
				return null;
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			binaryFormatter.Serialize(memoryStream, Data);
			return memoryStream.ToArray();
		}

		public static nxtCrypto.PackData.PackData ByteArrayToPackdata(byte[] Bytes)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			memoryStream.Write(Bytes, 0, Bytes.Length);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return (nxtCrypto.PackData.PackData)binaryFormatter.Deserialize(memoryStream);
		}

		public static byte[] SecurityStateToByteArray(nxtCrypto.SecurityState.SecurityState Data)
		{
			if (Data == null)
			{
				return null;
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			binaryFormatter.Serialize(memoryStream, Data);
			return memoryStream.ToArray();
		}

		public static nxtCrypto.SecurityState.SecurityState ByteArrayToSecurityState(byte[] Bytes)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			memoryStream.Write(Bytes, 0, Bytes.Length);
			memoryStream.Seek(0L, SeekOrigin.Begin);
			return (nxtCrypto.SecurityState.SecurityState)binaryFormatter.Deserialize(memoryStream);
		}
	}
}
