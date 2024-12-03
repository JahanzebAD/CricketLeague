using System;
using System.Linq;
using UnityEngine;

namespace nxtCrypto.SecurityState
{
	[Serializable]
	public class SecurityState
	{
		[SerializeField]
		private byte[] mEncryptedData;

		[SerializeField]
		private byte[] mKey;

		[SerializeField]
		private string mHash;

		public SecurityState()
		{
			mEncryptedData = null;
			mKey = null;
			mHash = null;
		}

		public SecurityState(byte[] EncryptedData, byte[] Key, string Hash)
		{
			mEncryptedData = EncryptedData.ToArray();
			mKey = Key.ToArray();
			mHash = Hash;
		}

		public void CopyFrom(SecurityState Src)
		{
			mEncryptedData = Src.mEncryptedData.ToArray();
			mKey = Src.mKey.ToArray();
			mHash = Src.mHash;
		}

		public void CopyTo(out SecurityState Dst)
		{
			Dst = new SecurityState();
			Dst.mEncryptedData = mEncryptedData.ToArray();
			Dst.mKey = mKey.ToArray();
			Dst.mHash = mHash;
		}

		public byte[] GetEncryption()
		{
			return mEncryptedData;
		}

		public byte[] GetKey()
		{
			return mKey;
		}

		public string GetHash()
		{
			return mHash;
		}
	}
}
