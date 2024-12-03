using nxtCrypto.Encryptor;
using nxtCrypto.PackData;
using nxtCrypto.SecurityState;

namespace SecurityForNextwave
{
	public class NextwaveSecurity
	{
		private Encryptor mCryptMechanism;

		private string mProductId;

		private string mCompanyId;

		public NextwaveSecurity(string ProductId, string CompanyId)
		{
			mProductId = ProductId;
			mCompanyId = CompanyId;
			mCryptMechanism = new Encryptor(mProductId, mCompanyId);
		}

		public bool Encrypt(PackData PackData)
		{
			return mCryptMechanism.CreateEncryption(PackData);
		}

		public bool Decrypt(out PackData PackData)
		{
			return mCryptMechanism.CreateDecryption(out PackData);
		}

		public bool ExportState(out SecurityState SecurityStateData)
		{
			return mCryptMechanism.ExportCryptoState(out SecurityStateData);
		}

		public bool ImportState(SecurityState SecurityStateData)
		{
			return mCryptMechanism.ImportCryptoState(SecurityStateData);
		}
	}
}
