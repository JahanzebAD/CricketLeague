using System;
using System.Runtime.InteropServices;
using CodeStage.AntiCheat.Common;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredDecimal : IFormattable, IEquatable<ObscuredDecimal>, IComparable<ObscuredDecimal>, IComparable<decimal>, IComparable
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct DecimalLongBytesUnion
		{
			[FieldOffset(0)]
			public decimal d;

			[FieldOffset(0)]
			public long l1;

			[FieldOffset(8)]
			public long l2;

			[FieldOffset(0)]
			public ACTkByte16 b16;
		}

		private static long cryptoKey = 209208L;

		[SerializeField]
		private long currentCryptoKey;

		[SerializeField]
		private ACTkByte16 hiddenValue;

		[SerializeField]
		private bool inited;

		private decimal fakeValue;

		[SerializeField]
		private bool fakeValueActive;

		private ObscuredDecimal(decimal value)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = InternalEncrypt(value);
			bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
			fakeValue = ((!existsAndIsRunning) ? 0m : value);
			fakeValueActive = existsAndIsRunning;
			inited = true;
		}

		public static void SetNewCryptoKey(long newKey)
		{
			cryptoKey = newKey;
		}

		public static decimal Encrypt(decimal value)
		{
			return Encrypt(value, cryptoKey);
		}

		public static decimal Encrypt(decimal value, long key)
		{
			DecimalLongBytesUnion decimalLongBytesUnion = default(DecimalLongBytesUnion);
			decimalLongBytesUnion.d = value;
			decimalLongBytesUnion.l1 ^= key;
			decimalLongBytesUnion.l2 ^= key;
			return decimalLongBytesUnion.d;
		}

		private static ACTkByte16 InternalEncrypt(decimal value)
		{
			return InternalEncrypt(value, 0L);
		}

		private static ACTkByte16 InternalEncrypt(decimal value, long key)
		{
			long num = key;
			if (num == 0)
			{
				num = cryptoKey;
			}
			DecimalLongBytesUnion decimalLongBytesUnion = default(DecimalLongBytesUnion);
			decimalLongBytesUnion.d = value;
			decimalLongBytesUnion.l1 ^= num;
			decimalLongBytesUnion.l2 ^= num;
			return decimalLongBytesUnion.b16;
		}

		public static decimal Decrypt(decimal value)
		{
			return Decrypt(value, cryptoKey);
		}

		public static decimal Decrypt(decimal value, long key)
		{
			DecimalLongBytesUnion decimalLongBytesUnion = default(DecimalLongBytesUnion);
			decimalLongBytesUnion.d = value;
			decimalLongBytesUnion.l1 ^= key;
			decimalLongBytesUnion.l2 ^= key;
			return decimalLongBytesUnion.d;
		}

		public void ApplyNewCryptoKey()
		{
			if (currentCryptoKey != cryptoKey)
			{
				hiddenValue = InternalEncrypt(InternalDecrypt(), cryptoKey);
				currentCryptoKey = cryptoKey;
			}
		}

		public void RandomizeCryptoKey()
		{
			decimal value = InternalDecrypt();
			do
			{
				currentCryptoKey = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			while (currentCryptoKey == 0);
			hiddenValue = InternalEncrypt(value, currentCryptoKey);
		}

		public decimal GetEncrypted()
		{
			ApplyNewCryptoKey();
			DecimalLongBytesUnion decimalLongBytesUnion = default(DecimalLongBytesUnion);
			decimalLongBytesUnion.b16 = hiddenValue;
			return decimalLongBytesUnion.d;
		}

		public void SetEncrypted(decimal encrypted)
		{
			inited = true;
			DecimalLongBytesUnion decimalLongBytesUnion = default(DecimalLongBytesUnion);
			decimalLongBytesUnion.d = encrypted;
			hiddenValue = decimalLongBytesUnion.b16;
			if (currentCryptoKey == 0)
			{
				currentCryptoKey = cryptoKey;
			}
			if (ObscuredCheatingDetector.ExistsAndIsRunning)
			{
				fakeValueActive = false;
				fakeValue = InternalDecrypt();
				fakeValueActive = true;
			}
			else
			{
				fakeValueActive = false;
			}
		}

		public decimal GetDecrypted()
		{
			return InternalDecrypt();
		}

		private decimal InternalDecrypt()
		{
			if (!inited)
			{
				currentCryptoKey = cryptoKey;
				hiddenValue = InternalEncrypt(0m);
				fakeValue = 0m;
				fakeValueActive = false;
				inited = true;
				return 0m;
			}
			DecimalLongBytesUnion decimalLongBytesUnion = default(DecimalLongBytesUnion);
			decimalLongBytesUnion.b16 = hiddenValue;
			decimalLongBytesUnion.l1 ^= currentCryptoKey;
			decimalLongBytesUnion.l2 ^= currentCryptoKey;
			decimal d = decimalLongBytesUnion.d;
			if (ObscuredCheatingDetector.ExistsAndIsRunning && fakeValueActive && d != fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return d;
		}

		public static implicit operator ObscuredDecimal(decimal value)
		{
			return new ObscuredDecimal(value);
		}

		public static implicit operator decimal(ObscuredDecimal value)
		{
			return value.InternalDecrypt();
		}

		public static explicit operator ObscuredDecimal(ObscuredFloat f)
		{
			return (decimal)(float)f;
		}

		public static ObscuredDecimal operator ++(ObscuredDecimal input)
		{
			decimal value = input.InternalDecrypt() + 1m;
			input.hiddenValue = InternalEncrypt(value, input.currentCryptoKey);
			if (ObscuredCheatingDetector.ExistsAndIsRunning)
			{
				input.fakeValue = value;
				input.fakeValueActive = true;
			}
			else
			{
				input.fakeValueActive = false;
			}
			return input;
		}

		public static ObscuredDecimal operator --(ObscuredDecimal input)
		{
			decimal value = input.InternalDecrypt() - 1m;
			input.hiddenValue = InternalEncrypt(value, input.currentCryptoKey);
			if (ObscuredCheatingDetector.ExistsAndIsRunning)
			{
				input.fakeValue = value;
				input.fakeValueActive = true;
			}
			else
			{
				input.fakeValueActive = false;
			}
			return input;
		}

		public override int GetHashCode()
		{
			return InternalDecrypt().GetHashCode();
		}

		public override string ToString()
		{
			return InternalDecrypt().ToString();
		}

		public string ToString(string format)
		{
			return InternalDecrypt().ToString(format);
		}

		public string ToString(IFormatProvider provider)
		{
			return InternalDecrypt().ToString(provider);
		}

		public string ToString(string format, IFormatProvider provider)
		{
			return InternalDecrypt().ToString(format, provider);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredDecimal))
			{
				return false;
			}
			return Equals((ObscuredDecimal)obj);
		}

		public bool Equals(ObscuredDecimal obj)
		{
			return obj.InternalDecrypt().Equals(InternalDecrypt());
		}

		public int CompareTo(ObscuredDecimal other)
		{
			return InternalDecrypt().CompareTo(other.InternalDecrypt());
		}

		public int CompareTo(decimal other)
		{
			return InternalDecrypt().CompareTo(other);
		}

		public int CompareTo(object obj)
		{
			return InternalDecrypt().CompareTo(obj);
		}
	}
}
