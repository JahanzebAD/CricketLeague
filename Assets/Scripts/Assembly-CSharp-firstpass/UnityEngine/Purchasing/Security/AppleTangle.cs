using System;

namespace UnityEngine.Purchasing.Security
{
	public class AppleTangle
	{
		private static byte[] data = Convert.FromBase64String("CikEAwcHBQADFBxqdnZycTgtLXVQZ25rY2xhZyJtbCJ2amtxImFncCQyJgQBVwYJER9DcnJuZyJBZ3B2KIRKhPUPAwMHBwIyYDMJMgsEAVduZyJLbGEsMyQyJgQBVwYJER9DcixCpPVFT30KXDIdBAFXHyEGGjIUvPZxmezQZg3Je0022qA8+3r9acqNcYNixBlZCy2QsPpGSvJiOpwX93BjYXZrYWcicXZjdmdvZ2x2cSwyHZPZHEVS6QfvXHuGL+k0oFVOV+6XnHgOpkWJWdYUNTHJxg1PzBZr0wYEEQBXUTMRMhMEAVcGCBEIQ3JyBDINBAFXHxEDA/0GBzIBAwP9Mh8Nnz/xKUsqGMr8zLe7DNtcHtTJP3ZrZGthY3ZnImB7ImNseyJyY3B2tziv9g0MApAJsyMULHbXPg/ZYBRrZGthY3ZrbWwiQ3d2am1wa3Z7M6mhc5BFUVfDrS1Dsfr54XLP5KFOcm5nIlBtbXYiQUMyHBUPMjQyNjAxNFgyYDMJMgsEAVcGBBEAV1EzEWY3IRdJF1sfsZb19J6czVK4w1pSdmptcGt2ezMUMhYEAVcGAREPQ3IKXDKAAxMEAVcfIgaAAwoygAMGMngygAN0MgwEAVcfDQMD/QYGAQADdXUsY3JybmcsYW1vLWNycm5nYWN9Q6qa+9PIZJ4maRPSobnmGSjBHYADAgQLKIRKhPVhZgcDMoPwMigEBwIBgAMNAjKAAwgAgAMDAuaTqwsdh4GHGZs/RTXwq5lCjC7Ws5IQ2kvadJ0xFmejdZbLLwABAwIDoYADIm1kInZqZyJ2amdsImNycm5rYWNbpQcLfhVCVBMcdtG1iSE5RaHXbUd8HU5pUpRDi8Z2YAkSgUOFMYiDszJa7lgGMI5qsY0f3Gdx/WVcZ74uImFncHZrZGthY3ZnInJtbmthew8ECyiESoT1DwMDBwcCAYADAwJeLTKDwQQKKQQDBwcFAAAyg7QYg7EEAVcfDAYUBhYp0mtFlnQL/PZpj8JhMXX1OAUuVOnYDSMM2LhxG023ghYp0mtFlnQL/PZpjyxCpPVFT32JG4vc+0lu9wWpIDIA6ho8+lIL0TSbTi96te+Omd7xdZnwdNB1Mk3DPyRlIogxaPUPgM3c6aEt+1FoWWa1Gb+RQCYQKMUNH7RPnlxhykmCFXsiY3Fxd29ncSJjYWFncnZjbGFnMhMEAVcGCBEIQ3JybmciS2xhLDMiQUMygAMgMg8ECyiESoT1DwMDA2BuZyJxdmNsZmNwZiJ2Z3BvcSJjMoAGuTKAAaGiAQADAAADADIPBAvLG3D3XwzXfV2Z8CcBuFeNT18P8yJjbGYiYWdwdmtka2FjdmttbCJyZY0KtiL1ya4uIm1ytD0DMo61Qc1ybmciQWdwdmtka2FjdmttbCJDdwXufzuBiVEi0TrGs72YTQhp/Sn+JuDp07Vy3Q1H4yXI82967+W3FRVsZiJhbWxma3ZrbWxxIm1kIndxZ6refCA3yCfX2w3UadagJiET9aOuFDIWBAFXBgERD0Nycm5nIlBtbXbbNH3DhVfbpZu7MED52tdznHyjUDcwMzYyMTRYFQ8xNzIwMjswMzYyUqiI19jm/tILBTWyd3cj");

		private static int[] order = new int[61]
		{
			35, 38, 10, 38, 38, 31, 50, 55, 46, 15,
			18, 18, 29, 22, 39, 23, 18, 24, 50, 46,
			20, 31, 33, 28, 36, 36, 34, 39, 49, 39,
			41, 52, 36, 34, 44, 47, 37, 41, 57, 41,
			50, 48, 56, 59, 50, 49, 50, 56, 56, 50,
			50, 52, 57, 57, 57, 59, 57, 57, 58, 59,
			60
		};

		private static int key = 2;

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
