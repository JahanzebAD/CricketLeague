using System;

namespace UnityEngine.Purchasing.Security
{
	public class UnityChannelTangle
	{
		private static byte[] data = Convert.FromBase64String(string.Empty);

		private static int[] order = new int[0];

		private static int key = 0;

		public static readonly bool IsPopulated = false;

		//public static byte[] Data()
		//{
		//	if (!IsPopulated)
		//	{
		//		return null;
		//	}
		//	return Obfuscator.DeObfuscate(data, order, key);
		//}
	}
}
