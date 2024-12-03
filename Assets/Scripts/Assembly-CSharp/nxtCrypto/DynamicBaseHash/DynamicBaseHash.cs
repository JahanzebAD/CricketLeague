using System.Text;

namespace nxtCrypto.DynamicBaseHash
{
	public class DynamicBaseHash
	{
		private static string mHashStaticSalt = "x2";

		private string mHash;

		public DynamicBaseHash()
		{
			mHash = null;
		}

		public string CreateHash(byte[] Bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < Bytes.Length; i++)
			{
				byte b = Bytes[i];
				string value = b.ToString();
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		public string GetCurrentHash()
		{
			return mHash;
		}

		public bool UpdateHash(string Hash)
		{
			mHash = Hash;
			return true;
		}
	}
}
