using System;
using UnityEngine;

namespace nxtCrypto.SecurityState
{
	[Serializable]
	public class JunkData
	{
		[SerializeField]
		private byte[] AEBO15;

		[SerializeField]
		private byte[] DFA128;

		[SerializeField]
		private string FBC890;

		public JunkData()
		{
			AEBO15 = null;
			DFA128 = null;
			FBC890 = null;
		}

		public JunkData(SecurityState Src)
		{
			AEBO15 = Src.GetEncryption();
			DFA128 = Src.GetKey();
			FBC890 = Src.GetHash();
		}

		public void CopyFrom(SecurityState Src)
		{
			AEBO15 = Src.GetEncryption();
			DFA128 = Src.GetKey();
			FBC890 = Src.GetHash();
		}

		public void CopyTo(out SecurityState Dst)
		{
			Dst = new SecurityState(AEBO15, DFA128, FBC890);
		}
	}
}
