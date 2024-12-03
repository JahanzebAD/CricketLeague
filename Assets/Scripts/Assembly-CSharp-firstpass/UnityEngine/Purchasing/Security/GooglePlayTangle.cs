using System;

namespace UnityEngine.Purchasing.Security
{
	public class GooglePlayTangle
	{
		private static byte[] data = Convert.FromBase64String("AWvKHlEFkQSRfdbTPt5g95ZSMZXKT9rXaKkh86G5SIFgGDSxp2ZlVRakJwQWKyAvDKBuoNErJycnIyYl2j+3vWDsfEN2WtqO/ZE2nl/AOh2EdpeQcViQL4TnPLrmqJb6lItveLZMeMXs6+w8xsGVCOrZYbwl7HTDDPfDYJn+x18GwtPS0igbketX1/9sUTqaHtQDo1WaN9oj7kkon5NoR+N/luKnalTZM91gGZdM3DDyIWP3pCcpJhakJywkpCcnJqX0NjdTxjize3PFodaNoxCGNh00B2JmlV2E0uhsN+GJ14iG8Hg3/GKP81Y59+vg3SAcfpuwQu1oK6CapWuEcEVTQsg9u71chYGCdJ99X9cvD7t4+k+52thy02wXt/4bYyQlJyYn");

		private static int[] order = new int[15]
		{
			11, 12, 11, 5, 8, 5, 9, 9, 9, 12,
			11, 11, 12, 13, 14
		};

		private static int key = 38;

		public static readonly bool IsPopulated = true;

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
