using System;

namespace CodeStage.AntiCheat.Common
{
	[Serializable]
	public struct ACTkByte4
	{
		public byte b1;

		public byte b2;

		public byte b3;

		public byte b4;

		public void Shuffle()
		{
			byte b = b2;
			b2 = b3;
			b3 = b;
		}

		public void UnShuffle()
		{
			byte b = b3;
			b3 = b2;
			b2 = b;
		}
	}
}
