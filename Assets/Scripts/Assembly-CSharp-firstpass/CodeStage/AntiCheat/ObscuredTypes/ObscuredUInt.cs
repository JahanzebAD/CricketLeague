using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredUInt : IFormattable, IEquatable<ObscuredUInt>, IComparable<ObscuredUInt>, IComparable<uint>, IComparable
	{
		private static uint cryptoKey = 240513u;

		[SerializeField]
		private uint currentCryptoKey;

		[SerializeField]
		private uint hiddenValue;

		[SerializeField]
		private bool inited;

		[SerializeField]
		private uint fakeValue;

		[SerializeField]
		private bool fakeValueActive;

		private ObscuredUInt(uint value)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = Encrypt(value);
			bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
			fakeValue = (existsAndIsRunning ? value : 0u);
			fakeValueActive = existsAndIsRunning;
			inited = true;
		}

		public static void SetNewCryptoKey(uint newKey)
		{
			cryptoKey = newKey;
		}

		public static uint Encrypt(uint value)
		{
			return Encrypt(value, 0u);
		}

		public static uint Decrypt(uint value)
		{
			return Decrypt(value, 0u);
		}

		public static uint Encrypt(uint value, uint key)
		{
			if (key == 0)
			{
				return value ^ cryptoKey;
			}
			return value ^ key;
		}

		public static uint Decrypt(uint value, uint key)
		{
			if (key == 0)
			{
				return value ^ cryptoKey;
			}
			return value ^ key;
		}

		public void ApplyNewCryptoKey()
		{
			if (currentCryptoKey != cryptoKey)
			{
				hiddenValue = Encrypt(InternalDecrypt(), cryptoKey);
				currentCryptoKey = cryptoKey;
			}
		}

		public void RandomizeCryptoKey()
		{
			uint value = InternalDecrypt();
			currentCryptoKey = (uint)UnityEngine.Random.Range(1, int.MaxValue);
			hiddenValue = Encrypt(value, currentCryptoKey);
		}

		public uint GetEncrypted()
		{
			ApplyNewCryptoKey();
			return hiddenValue;
		}

		public void SetEncrypted(uint encrypted)
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

		public uint GetDecrypted()
		{
			return InternalDecrypt();
		}

		private uint InternalDecrypt()
		{
			if (!inited)
			{
				currentCryptoKey = cryptoKey;
				hiddenValue = Encrypt(0u);
				fakeValue = 0u;
				fakeValueActive = false;
				inited = true;
				return 0u;
			}
			uint num = Decrypt(hiddenValue, currentCryptoKey);
			if (ObscuredCheatingDetector.ExistsAndIsRunning && fakeValueActive && num != fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return num;
		}

		public static implicit operator ObscuredUInt(uint value)
		{
			return new ObscuredUInt(value);
		}

		public static implicit operator uint(ObscuredUInt value)
		{
			return value.InternalDecrypt();
		}

		public static explicit operator ObscuredInt(ObscuredUInt value)
		{
			return (int)value.InternalDecrypt();
		}

		public static ObscuredUInt operator ++(ObscuredUInt input)
		{
			uint value = input.InternalDecrypt() + 1;
			input.hiddenValue = Encrypt(value, input.currentCryptoKey);
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

		public static ObscuredUInt operator --(ObscuredUInt input)
		{
			uint value = input.InternalDecrypt() - 1;
			input.hiddenValue = Encrypt(value, input.currentCryptoKey);
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
			if (!(obj is ObscuredUInt))
			{
				return false;
			}
			return Equals((ObscuredUInt)obj);
		}

		public bool Equals(ObscuredUInt obj)
		{
			if (currentCryptoKey == obj.currentCryptoKey)
			{
				return hiddenValue == obj.hiddenValue;
			}
			return Decrypt(hiddenValue, currentCryptoKey) == Decrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		public int CompareTo(ObscuredUInt other)
		{
			return InternalDecrypt().CompareTo(other.InternalDecrypt());
		}

		public int CompareTo(uint other)
		{
			return InternalDecrypt().CompareTo(other);
		}

		public int CompareTo(object obj)
		{
			return InternalDecrypt().CompareTo(obj);
		}
	}
}
