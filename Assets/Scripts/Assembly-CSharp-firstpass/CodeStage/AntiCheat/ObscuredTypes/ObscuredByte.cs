using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredByte : IFormattable, IEquatable<ObscuredByte>, IComparable<ObscuredByte>, IComparable<byte>, IComparable
	{
		private static byte cryptoKey = 244;

		private byte currentCryptoKey;

		private byte hiddenValue;

		private bool inited;

		private byte fakeValue;

		private bool fakeValueActive;

		private ObscuredByte(byte value)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = EncryptDecrypt(value);
			bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
			fakeValue = (byte)(existsAndIsRunning ? value : 0);
			fakeValueActive = existsAndIsRunning;
			inited = true;
		}

		public static void SetNewCryptoKey(byte newKey)
		{
			cryptoKey = newKey;
		}

		public static byte EncryptDecrypt(byte value)
		{
			return EncryptDecrypt(value, 0);
		}

		public static void EncryptDecrypt(byte[] value)
		{
			EncryptDecrypt(value, 0);
		}

		public static byte EncryptDecrypt(byte value, byte key)
		{
			if (key == 0)
			{
				return (byte)(value ^ cryptoKey);
			}
			return (byte)(value ^ key);
		}

		public static void EncryptDecrypt(byte[] value, byte key)
		{
			int num = value.Length;
			for (int i = 0; i < num; i++)
			{
				if (key == 0)
				{
					value[i] = (byte)(value[i] ^ cryptoKey);
				}
				else
				{
					value[i] = (byte)(value[i] ^ key);
				}
			}
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
			byte value = InternalDecrypt();
			currentCryptoKey = (byte)UnityEngine.Random.Range(1, 255);
			hiddenValue = EncryptDecrypt(value, currentCryptoKey);
		}

		public byte GetEncrypted()
		{
			ApplyNewCryptoKey();
			return hiddenValue;
		}

		public void SetEncrypted(byte encrypted)
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

		public byte GetDecrypted()
		{
			return InternalDecrypt();
		}

		private byte InternalDecrypt()
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
			byte b = EncryptDecrypt(hiddenValue, currentCryptoKey);
			if (ObscuredCheatingDetector.ExistsAndIsRunning && fakeValueActive && b != fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return b;
		}

		public static implicit operator ObscuredByte(byte value)
		{
			return new ObscuredByte(value);
		}

		public static implicit operator byte(ObscuredByte value)
		{
			return value.InternalDecrypt();
		}

		public static ObscuredByte operator ++(ObscuredByte input)
		{
			byte value = (byte)(input.InternalDecrypt() + 1);
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

		public static ObscuredByte operator --(ObscuredByte input)
		{
			byte value = (byte)(input.InternalDecrypt() - 1);
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
			if (!(obj is ObscuredByte))
			{
				return false;
			}
			return Equals((ObscuredByte)obj);
		}

		public bool Equals(ObscuredByte obj)
		{
			if (currentCryptoKey == obj.currentCryptoKey)
			{
				return hiddenValue == obj.hiddenValue;
			}
			return EncryptDecrypt(hiddenValue, currentCryptoKey) == EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		public int CompareTo(ObscuredByte other)
		{
			return InternalDecrypt().CompareTo(other.InternalDecrypt());
		}

		public int CompareTo(byte other)
		{
			return InternalDecrypt().CompareTo(other);
		}

		public int CompareTo(object obj)
		{
			return InternalDecrypt().CompareTo(obj);
		}
	}
}
