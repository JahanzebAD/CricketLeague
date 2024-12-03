using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace nxtCrypto.BaseEncryption
{
	public class BaseEncryption
	{
		private static int STATIC_KEYSIZE = 16;

		private string mProductId = string.Empty;

		private string mCompanyId = string.Empty;

		private string keyString = string.Empty;

		private int keySize;

		private int IvLength;

		private readonly UTF8Encoding encoder;

		private readonly RijndaelManaged rijndael;

		public BaseEncryption(string ProductId, string CompanyId)
		{
			mProductId = ProductId;
			mCompanyId = CompanyId;
			keyString = StaticKeyStabilizer(ProductId, CompanyId);
			keySize = keyString.Length;
			IvLength = keyString.Length;
			encoder = new UTF8Encoding();
			rijndael = new RijndaelManaged
			{
				Key = encoder.GetBytes(keyString).Take(keySize).ToArray()
			};
			rijndael.BlockSize = IvLength * 8;
		}

		private string StaticKeyStabilizer(string ProductId, string CompanyId)
		{
			string text = ProductId + CompanyId;
			int length = text.Length;
			if (length == STATIC_KEYSIZE)
			{
				return text;
			}
			if (length > STATIC_KEYSIZE)
			{
				return text.Substring(0, STATIC_KEYSIZE);
			}
			string text2 = new string('x', STATIC_KEYSIZE);
			string text3 = text + text2;
			return text3.Substring(0, STATIC_KEYSIZE);
		}

		private int StaticKeySize(string ProductId, string CompanyId)
		{
			return ProductId.Length + CompanyId.Length;
		}

		public byte[] Encrypt(byte[] buffer, byte[] IV)
		{
			return EncryptKeyIV(buffer, rijndael.Key, IV);
		}

		public byte[] Decrypt(byte[] buffer, byte[] IV)
		{
			return DecryptKeyIV(buffer, rijndael.Key, IV);
		}

		private byte[] EncryptKeyIV(byte[] buffer, byte[] key, byte[] IV)
		{
			using ICryptoTransform cryptoTransform = rijndael.CreateEncryptor(key, IV);
			return cryptoTransform.TransformFinalBlock(buffer, 0, buffer.Length);
		}

		private byte[] DecryptKeyIV(byte[] buffer, byte[] key, byte[] IV)
		{
			using ICryptoTransform cryptoTransform = rijndael.CreateDecryptor(key, IV);
			return cryptoTransform.TransformFinalBlock(buffer, 0, buffer.Length);
		}
	}
}
