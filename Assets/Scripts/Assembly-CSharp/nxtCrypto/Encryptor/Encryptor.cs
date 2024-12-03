using System.Linq;
using nxtCrypto.BaseEncryption;
using nxtCrypto.DynamicBaseHash;
using nxtCrypto.KeyGenerator;
using nxtCrypto.PackData;
using nxtCrypto.SecurityState;
using nxtCrypto.Tools;

namespace nxtCrypto.Encryptor
{
	public class Encryptor
	{
		private nxtCrypto.BaseEncryption.BaseEncryption mBaseEncryptor;

		private nxtCrypto.DynamicBaseHash.DynamicBaseHash mHash;

		private nxtCrypto.KeyGenerator.KeyGenerator mKeyGenerator;

		private byte[] mEncryptedPackdata;

		private bool mActivated;

		public Encryptor(string ProductId, string CompanyId)
		{
			mBaseEncryptor = new nxtCrypto.BaseEncryption.BaseEncryption(ProductId, CompanyId);
			mHash = new nxtCrypto.DynamicBaseHash.DynamicBaseHash();
			mKeyGenerator = new nxtCrypto.KeyGenerator.KeyGenerator();
			mActivated = false;
		}

		private bool IsActivated()
		{
			return mActivated;
		}

		private byte[] GetKey()
		{
			return mKeyGenerator.GetDynamicKey();
		}

		private string GetHash()
		{
			return mHash.GetCurrentHash();
		}

		private byte[] PackToBytes(nxtCrypto.PackData.PackData RawData)
		{
			return nxtCrypto.Tools.Tools.PackdataToByteArray(RawData);
		}

		private nxtCrypto.PackData.PackData BytesToPack(byte[] PackedDataBytes)
		{
			return nxtCrypto.Tools.Tools.ByteArrayToPackdata(PackedDataBytes);
		}

		private byte[] EncryptionAtDefault(byte[] PackedData, byte[] Key)
		{
			return mBaseEncryptor.Encrypt(PackedData, Key);
		}

		private byte[] DecryptionAtDefault(byte[] EncryptedPackedData, byte[] Key)
		{
			return mBaseEncryptor.Decrypt(EncryptedPackedData, Key);
		}

		private string CreateHash(byte[] EncryptedData)
		{
			return mHash.CreateHash(EncryptedData);
		}

		private bool IsCurrentStateAuthentic()
		{
			string text = CreateHash(mEncryptedPackdata);
			return text == GetHash();
		}

		public bool CreateEncryption(nxtCrypto.PackData.PackData RawPackedData)
		{
			byte[] array = null;
			byte[] array2 = null;
			byte[] array3 = null;
			string hash = null;
			int num = 0;
			if (RawPackedData == null)
			{
				num++;
			}
			if (num == 0)
			{
				array = PackToBytes(RawPackedData);
				array2 = GetKey();
				array3 = EncryptionAtDefault(array, array2);
				hash = CreateHash(array3);
				if (IsActivated() && !IsCurrentStateAuthentic())
				{
					num++;
				}
			}
			if (num > 0)
			{
				return false;
			}
			if (num == 0)
			{
				mEncryptedPackdata = array3.ToArray();
				mHash.UpdateHash(hash);
				return true;
			}
			return false;
		}

		public bool CreateDecryption(out nxtCrypto.PackData.PackData DecryptedPackData)
		{
			DecryptedPackData = new nxtCrypto.PackData.PackData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
			int num = 0;
			if (DecryptedPackData == null)
			{
				num++;
			}
			if (num == 0 && !IsActivated() && !IsCurrentStateAuthentic())
			{
				num++;
			}
			if (num > 0)
			{
				return false;
			}
			if (num == 0)
			{
				byte[] key = GetKey();
				byte[] packedDataBytes = DecryptionAtDefault(mEncryptedPackdata, key);
				nxtCrypto.PackData.PackData src = BytesToPack(packedDataBytes);
				DecryptedPackData.CopyFrom(src);
				return true;
			}
			return false;
		}

		public bool ExportCryptoState(out nxtCrypto.SecurityState.SecurityState SecurityStateData)
		{
			SecurityStateData = new nxtCrypto.SecurityState.SecurityState();
			int num = 0;
			if (SecurityStateData == null)
			{
				num++;
			}
			if (num == 0 && !IsActivated() && !IsCurrentStateAuthentic())
			{
				num++;
			}
			if (num > 0)
			{
				return false;
			}
			if (num == 0)
			{
				byte[] encryptedData = mEncryptedPackdata.ToArray();
				byte[] key = GetKey();
				string hash = GetHash();
				nxtCrypto.SecurityState.SecurityState src = new nxtCrypto.SecurityState.SecurityState(encryptedData, key, hash);
				SecurityStateData.CopyFrom(src);
				return true;
			}
			return false;
		}

		public bool ImportCryptoState(nxtCrypto.SecurityState.SecurityState SecurityStateData)
		{
			int num = 0;
			if (SecurityStateData == null)
			{
				num++;
			}
			byte[] encryption = SecurityStateData.GetEncryption();
			byte[] key = SecurityStateData.GetKey();
			string hash = SecurityStateData.GetHash();
			byte[] array = null;
			nxtCrypto.PackData.PackData packData = null;
			if (num == 0)
			{
				string text = CreateHash(encryption);
				if (text != hash)
				{
					num++;
				}
			}
			if (num == 0)
			{
				array = DecryptionAtDefault(encryption, key);
			}
			if (array == null)
			{
				num++;
			}
			if (num == 0)
			{
				packData = BytesToPack(array);
				if (packData == null)
				{
					num++;
				}
			}
			if (num == 0 && !CreateEncryption(packData))
			{
				num++;
			}
			if (num == 0)
			{
				return true;
			}
			return false;
		}
	}
}
