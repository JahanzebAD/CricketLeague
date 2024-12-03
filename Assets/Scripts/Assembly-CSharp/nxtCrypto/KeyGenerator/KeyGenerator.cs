using nxtCrypto.Tools;
using UnityEngine;

namespace nxtCrypto.KeyGenerator
{
	public class KeyGenerator
	{
		private static int HEX_LOOKUP_TABLE_SIZE = 16;

		private static int SIZE_LOOKUP_TABLE_SIZE = 1;

		private static int OUTPUT_BYTE_SIZE = 16;

		private static string[] HexLookupTable = new string[16]
		{
			"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
			"A", "B", "C", "D", "E", "F"
		};

		private static int[] SizeLookupTable = new int[5] { 19, 23, 29, 31, 37 };

		private string mHex;

		private byte[] mHexKey;

		public KeyGenerator()
		{
			mHex = CreateDynamicKey();
			mHexKey = CreateDynamicKeyBytes(mHex);
		}

		public byte[] GetDynamicKey()
		{
			return mHexKey;
		}

		private int createKeySize()
		{
			return SizeLookupTable[Random.Range(0, SIZE_LOOKUP_TABLE_SIZE)];
		}

		private string CreateByteKey()
		{
			return HexLookupTable[Random.Range(0, HEX_LOOKUP_TABLE_SIZE)] + HexLookupTable[Random.Range(0, HEX_LOOKUP_TABLE_SIZE)];
		}

		private string CretateWholeStringKey(int Size)
		{
			string text = string.Empty;
			for (int i = 0; i < Size; i++)
			{
				text += CreateByteKey();
			}
			return "0x" + text;
		}

		private int GetHexVal(char hex)
		{
			return hex - ((hex >= ':') ? 55 : 48);
		}

		private string CreateDynamicKey()
		{
			int size = createKeySize();
			return CretateWholeStringKey(size);
		}

		private byte[] CreateDynamicKeyBytes(string HexKeyString)
		{
			byte[] array = new byte[OUTPUT_BYTE_SIZE];
			byte[] array2 = nxtCrypto.Tools.Tools.ConvertStringToByteArray(HexKeyString);
			for (int i = 0; i < OUTPUT_BYTE_SIZE; i++)
			{
				array[i] = array2[i];
			}
			return array;
		}
	}
}
