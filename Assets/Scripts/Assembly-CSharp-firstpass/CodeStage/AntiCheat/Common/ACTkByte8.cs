using System;

namespace CodeStage.AntiCheat.Common
{
	[Serializable]
	public struct ACTkByte8
	{
		public byte b1;

		public byte b2;

		public byte b3;

		public byte b4;

		public byte b5;

		public byte b6;

		public byte b7;

		public byte b8;

		public void Shuffle()
		{
			byte b = b1;
			b1 = this.b2;
			this.b2 = b;
			b = b5;
			b5 = b6;
			byte b2 = b8;
			b8 = b;
			b6 = b2;
		}

		public void UnShuffle()
		{
			byte b = b1;
			b1 = this.b2;
			this.b2 = b;
			b = b5;
			b5 = b8;
			byte b2 = b6;
			b6 = b;
			b8 = b2;
		}
	}
}
