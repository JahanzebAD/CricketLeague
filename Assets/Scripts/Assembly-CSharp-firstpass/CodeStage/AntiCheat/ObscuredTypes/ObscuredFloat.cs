using System;
using System.Runtime.InteropServices;
using CodeStage.AntiCheat.Common;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredFloat : IFormattable, IEquatable<ObscuredFloat>, IComparable<ObscuredFloat>, IComparable<float>, IComparable
	{
		[StructLayout(LayoutKind.Explicit)]
		internal struct FloatIntBytesUnion
		{
			[FieldOffset(0)]
			public float f;

			[FieldOffset(0)]
			public int i;

			[FieldOffset(0)]
			public ACTkByte4 b4;
		}

		private static int cryptoKey = 230887;

		[SerializeField]
		private int currentCryptoKey;

		[SerializeField]
		private int hiddenValue;

		[SerializeField]
		[FormerlySerializedAs("hiddenValue")]
		private ACTkByte4 hiddenValueOldByte4;

		[SerializeField]
		private bool inited;

		[SerializeField]
		private float fakeValue;

		[SerializeField]
		private bool fakeValueActive;

		private ObscuredFloat(float value)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = InternalEncrypt(value);
			hiddenValueOldByte4 = default(ACTkByte4);
			bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
			fakeValue = ((!existsAndIsRunning) ? 0f : value);
			fakeValueActive = existsAndIsRunning;
			inited = true;
		}

		public static void SetNewCryptoKey(int newKey)
		{
			cryptoKey = newKey;
		}

		public static int Encrypt(float value)
		{
			return Encrypt(value, cryptoKey);
		}

		public static int Encrypt(float value, int key)
		{
			FloatIntBytesUnion floatIntBytesUnion = default(FloatIntBytesUnion);
			floatIntBytesUnion.f = value;
			FloatIntBytesUnion floatIntBytesUnion2 = floatIntBytesUnion;
			floatIntBytesUnion2.i ^= key;
			floatIntBytesUnion2.b4.Shuffle();
			return floatIntBytesUnion2.i;
		}

		private static int InternalEncrypt(float value, int key = 0)
		{
			int num = key;
			if (num == 0)
			{
				num = cryptoKey;
			}
			return Encrypt(value, num);
		}

		public static float Decrypt(int value)
		{
			return Decrypt(value, cryptoKey);
		}

		public static float Decrypt(int value, int key)
		{
			FloatIntBytesUnion floatIntBytesUnion = default(FloatIntBytesUnion);
			floatIntBytesUnion.i = value;
			FloatIntBytesUnion floatIntBytesUnion2 = floatIntBytesUnion;
			floatIntBytesUnion2.b4.UnShuffle();
			floatIntBytesUnion2.i ^= key;
			return floatIntBytesUnion2.f;
		}

		public static int MigrateEncrypted(int encrypted, byte fromVersion = 0, byte toVersion = 2)
		{
			FloatIntBytesUnion floatIntBytesUnion = default(FloatIntBytesUnion);
			floatIntBytesUnion.i = encrypted;
			FloatIntBytesUnion floatIntBytesUnion2 = floatIntBytesUnion;
			if (fromVersion < 2 && toVersion == 2)
			{
				floatIntBytesUnion2.b4.Shuffle();
			}
			return floatIntBytesUnion2.i;
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
			float value = InternalDecrypt();
			currentCryptoKey = UnityEngine.Random.Range(100000, 999999);
			hiddenValue = InternalEncrypt(value, currentCryptoKey);
		}

		public int GetEncrypted()
		{
			ApplyNewCryptoKey();
			return hiddenValue;
		}

		public void SetEncrypted(int encrypted)
		{
			inited = true;
			hiddenValue = encrypted;
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

		public float GetDecrypted()
		{
			return InternalDecrypt();
		}

		private float InternalDecrypt()
		{
			if (!inited)
			{
				currentCryptoKey = cryptoKey;
				hiddenValue = InternalEncrypt(0f);
				fakeValue = 0f;
				fakeValueActive = false;
				inited = true;
				return 0f;
			}
			float num = Decrypt(hiddenValue, currentCryptoKey);
			if (ObscuredCheatingDetector.ExistsAndIsRunning && fakeValueActive && Math.Abs(num - fakeValue) > ObscuredCheatingDetector.Instance.floatEpsilon)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return num;
		}

		public static implicit operator ObscuredFloat(float value)
		{
			return new ObscuredFloat(value);
		}

		public static implicit operator float(ObscuredFloat value)
		{
			return value.InternalDecrypt();
		}

		public static ObscuredFloat operator ++(ObscuredFloat input)
		{
			float value = input.InternalDecrypt() + 1f;
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

		public static ObscuredFloat operator --(ObscuredFloat input)
		{
			float value = input.InternalDecrypt() - 1f;
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
			if (!(obj is ObscuredFloat))
			{
				return false;
			}
			return Equals((ObscuredFloat)obj);
		}

		public bool Equals(ObscuredFloat obj)
		{
			return obj.InternalDecrypt().Equals(InternalDecrypt());
		}

		public int CompareTo(ObscuredFloat other)
		{
			return InternalDecrypt().CompareTo(other.InternalDecrypt());
		}

		public int CompareTo(float other)
		{
			return InternalDecrypt().CompareTo(other);
		}

		public int CompareTo(object obj)
		{
			return InternalDecrypt().CompareTo(obj);
		}
	}
}
