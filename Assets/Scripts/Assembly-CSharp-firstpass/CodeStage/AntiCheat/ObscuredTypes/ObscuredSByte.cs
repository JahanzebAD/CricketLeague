using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredSByte : IFormattable, IEquatable<ObscuredSByte>, IComparable<ObscuredSByte>, IComparable<sbyte>, IComparable
	{
		private static sbyte cryptoKey = 112;

		private sbyte currentCryptoKey;

		private sbyte hiddenValue;

		private bool inited;

		private sbyte fakeValue;

		private bool fakeValueActive;

		private ObscuredSByte(sbyte value)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = EncryptDecrypt(value);
			bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
			fakeValue = (sbyte)(existsAndIsRunning ? value : 0);
			fakeValueActive = existsAndIsRunning;
			inited = true;
		}

		public static void SetNewCryptoKey(sbyte newKey)
		{
			cryptoKey = newKey;
		}

		public static sbyte EncryptDecrypt(sbyte value)
		{
			return EncryptDecrypt(value, 0);
		}

		public static sbyte EncryptDecrypt(sbyte value, sbyte key)
		{
			if (key == 0)
			{
				return (sbyte)(value ^ cryptoKey);
			}
			return (sbyte)(value ^ key);
		}

		public void ApplyNewCryptoKey()
		{
			if (currentCryptoKey != cryptoKey)
			{
				hiddenValue = EncryptDecrypt(InternalDecrypt(), cryptoKey);
				currentCryptoKey = cryptoKey;
			}
		}

		public void RandomizeCryptoKey()
		{
			sbyte value = InternalDecrypt();
			do
			{
				currentCryptoKey = (sbyte)UnityEngine.Random.Range(-128, 127);
			}
			while (currentCryptoKey == 0);
			hiddenValue = EncryptDecrypt(value, currentCryptoKey);
		}

		public sbyte GetEncrypted()
		{
			ApplyNewCryptoKey();
			return hiddenValue;
		}

		public void SetEncrypted(sbyte encrypted)
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

		public sbyte GetDecrypted()
		{
			return InternalDecrypt();
		}

		private sbyte InternalDecrypt()
		{
			if (!inited)
			{
				currentCryptoKey = cryptoKey;
				hiddenValue = EncryptDecrypt(0);
				fakeValue = 0;
				fakeValueActive = false;
				inited = true;
				return 0;
			}
			sbyte b = EncryptDecrypt(hiddenValue, currentCryptoKey);
			if (ObscuredCheatingDetector.ExistsAndIsRunning && fakeValueActive && b != fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return b;
		}

		public static implicit operator ObscuredSByte(sbyte value)
		{
			return new ObscuredSByte(value);
		}

		public static implicit operator sbyte(ObscuredSByte value)
		{
			return value.InternalDecrypt();
		}

		public static ObscuredSByte operator ++(ObscuredSByte input)
		{
			sbyte value = (sbyte)(input.InternalDecrypt() + 1);
			input.hiddenValue = EncryptDecrypt(value, input.currentCryptoKey);
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

		public static ObscuredSByte operator --(ObscuredSByte input)
		{
			sbyte value = (sbyte)(input.InternalDecrypt() - 1);
			input.hiddenValue = EncryptDecrypt(value, input.currentCryptoKey);
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
			if (!(obj is ObscuredSByte))
			{
				return false;
			}
			return Equals((ObscuredSByte)obj);
		}

		public bool Equals(ObscuredSByte obj)
		{
			if (currentCryptoKey == obj.currentCryptoKey)
			{
				return hiddenValue == obj.hiddenValue;
			}
			return EncryptDecrypt(hiddenValue, currentCryptoKey) == EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		public int CompareTo(ObscuredSByte other)
		{
			return InternalDecrypt().CompareTo(other.InternalDecrypt());
		}

		public int CompareTo(sbyte other)
		{
			return InternalDecrypt().CompareTo(other);
		}

		public int CompareTo(object obj)
		{
			return InternalDecrypt().CompareTo(obj);
		}
	}
}
