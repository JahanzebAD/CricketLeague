using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredChar : IEquatable<ObscuredChar>, IComparable<ObscuredChar>, IComparable<char>, IComparable
	{
		private static char cryptoKey = '—';

		private char currentCryptoKey;

		private char hiddenValue;

		private bool inited;

		private char fakeValue;

		private bool fakeValueActive;

		private ObscuredChar(char value)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = EncryptDecrypt(value);
			bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
			fakeValue = (existsAndIsRunning ? value : '\0');
			fakeValueActive = existsAndIsRunning;
			inited = true;
		}

		public static void SetNewCryptoKey(char newKey)
		{
			cryptoKey = newKey;
		}

		public static char EncryptDecrypt(char value)
		{
			return EncryptDecrypt(value, '\0');
		}

		public static char EncryptDecrypt(char value, char key)
		{
			if (key == '\0')
			{
				return (char)(value ^ cryptoKey);
			}
			return (char)(value ^ key);
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
			char value = InternalDecrypt();
			currentCryptoKey = (char)UnityEngine.Random.Range(1, 65535);
			hiddenValue = EncryptDecrypt(value, currentCryptoKey);
		}

		public char GetEncrypted()
		{
			ApplyNewCryptoKey();
			return hiddenValue;
		}

		public void SetEncrypted(char encrypted)
		{
			inited = true;
			hiddenValue = encrypted;
			if (currentCryptoKey == '\0')
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

		public char GetDecrypted()
		{
			return InternalDecrypt();
		}

		private char InternalDecrypt()
		{
			if (!inited)
			{
				currentCryptoKey = cryptoKey;
				hiddenValue = EncryptDecrypt('\0');
				fakeValue = '\0';
				fakeValueActive = false;
				inited = true;
				return '\0';
			}
			char c = EncryptDecrypt(hiddenValue, currentCryptoKey);
			if (ObscuredCheatingDetector.ExistsAndIsRunning && fakeValueActive && c != fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return c;
		}

		public static implicit operator ObscuredChar(char value)
		{
			return new ObscuredChar(value);
		}

		public static implicit operator char(ObscuredChar value)
		{
			return value.InternalDecrypt();
		}

		public static ObscuredChar operator ++(ObscuredChar input)
		{
			char value = (char)(input.InternalDecrypt() + 1);
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

		public static ObscuredChar operator --(ObscuredChar input)
		{
			char value = (char)(input.InternalDecrypt() - 1);
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

		public string ToString(IFormatProvider provider)
		{
			return InternalDecrypt().ToString(provider);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ObscuredChar))
			{
				return false;
			}
			return Equals((ObscuredChar)obj);
		}

		public bool Equals(ObscuredChar obj)
		{
			if (currentCryptoKey == obj.currentCryptoKey)
			{
				return hiddenValue == obj.hiddenValue;
			}
			return EncryptDecrypt(hiddenValue, currentCryptoKey) == EncryptDecrypt(obj.hiddenValue, obj.currentCryptoKey);
		}

		public int CompareTo(ObscuredChar other)
		{
			return InternalDecrypt().CompareTo(other.InternalDecrypt());
		}

		public int CompareTo(char other)
		{
			return InternalDecrypt().CompareTo(other);
		}

		public int CompareTo(object obj)
		{
			return InternalDecrypt().CompareTo(obj);
		}
	}
}
