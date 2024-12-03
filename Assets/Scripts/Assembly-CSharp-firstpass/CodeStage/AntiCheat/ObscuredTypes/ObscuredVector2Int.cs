using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredVector2Int
	{
		[Serializable]
		public struct RawEncryptedVector2Int
		{
			public int x;

			public int y;
		}

		private static int cryptoKey = 160122;

		private static readonly Vector2Int zero = Vector2Int.zero;

		[SerializeField]
		private int currentCryptoKey;

		[SerializeField]
		private RawEncryptedVector2Int hiddenValue;

		[SerializeField]
		private bool inited;

		[SerializeField]
		private Vector2Int fakeValue;

		[SerializeField]
		private bool fakeValueActive;

		public int x
		{
			get
			{
				int num = InternalDecryptField(hiddenValue.x);
				if (ObscuredCheatingDetector.ExistsAndIsRunning && fakeValueActive && Math.Abs(num - fakeValue.x) > 0)
				{
					ObscuredCheatingDetector.Instance.OnCheatingDetected();
				}
				return num;
			}
			set
			{
				hiddenValue.x = InternalEncryptField(value);
				if (ObscuredCheatingDetector.ExistsAndIsRunning)
				{
					fakeValue.x = value;
					fakeValue.y = InternalDecryptField(hiddenValue.y);
					fakeValueActive = true;
				}
				else
				{
					fakeValueActive = false;
				}
			}
		}

		public int y
		{
			get
			{
				int num = InternalDecryptField(hiddenValue.y);
				if (ObscuredCheatingDetector.ExistsAndIsRunning && fakeValueActive && Math.Abs(num - fakeValue.y) > 0)
				{
					ObscuredCheatingDetector.Instance.OnCheatingDetected();
				}
				return num;
			}
			set
			{
				hiddenValue.y = InternalEncryptField(value);
				if (ObscuredCheatingDetector.ExistsAndIsRunning)
				{
					fakeValue.x = InternalDecryptField(hiddenValue.x);
					fakeValue.y = value;
					fakeValueActive = true;
				}
				else
				{
					fakeValueActive = false;
				}
			}
		}

		public int this[int index]
		{
			get
			{
				return index switch
				{
					0 => x, 
					1 => y, 
					_ => throw new IndexOutOfRangeException("Invalid ObscuredVector2Int index!"), 
				};
			}
			set
			{
				switch (index)
				{
				case 0:
					x = value;
					break;
				case 1:
					y = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid ObscuredVector2Int index!");
				}
			}
		}

		private ObscuredVector2Int(Vector2Int value)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = Encrypt(value);
			bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
			fakeValue = ((!existsAndIsRunning) ? zero : value);
			fakeValueActive = existsAndIsRunning;
			inited = true;
		}

		public ObscuredVector2Int(int x, int y)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = Encrypt(x, y, currentCryptoKey);
			if (ObscuredCheatingDetector.ExistsAndIsRunning)
			{
				fakeValue = new Vector2Int(x, y);
				fakeValueActive = true;
			}
			else
			{
				fakeValue = zero;
				fakeValueActive = false;
			}
			inited = true;
		}

		public static void SetNewCryptoKey(int newKey)
		{
			cryptoKey = newKey;
		}

		public static RawEncryptedVector2Int Encrypt(Vector2Int value)
		{
			return Encrypt(value, 0);
		}

		public static RawEncryptedVector2Int Encrypt(Vector2Int value, int key)
		{
			return Encrypt(value.x, value.y, key);
		}

		public static RawEncryptedVector2Int Encrypt(int x, int y, int key)
		{
			if (key == 0)
			{
				key = cryptoKey;
			}
			RawEncryptedVector2Int result = default(RawEncryptedVector2Int);
			result.x = ObscuredInt.Encrypt(x, key);
			result.y = ObscuredInt.Encrypt(y, key);
			return result;
		}

		public static Vector2Int Decrypt(RawEncryptedVector2Int value)
		{
			return Decrypt(value, 0);
		}

		public static Vector2Int Decrypt(RawEncryptedVector2Int value, int key)
		{
			if (key == 0)
			{
				key = cryptoKey;
			}
			Vector2Int result = default(Vector2Int);
			result.x = ObscuredInt.Decrypt(value.x, key);
			result.y = ObscuredInt.Decrypt(value.y, key);
			return result;
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
			Vector2Int value = InternalDecrypt();
			do
			{
				currentCryptoKey = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			while (currentCryptoKey == 0);
			hiddenValue = Encrypt(value, currentCryptoKey);
		}

		public RawEncryptedVector2Int GetEncrypted()
		{
			ApplyNewCryptoKey();
			return hiddenValue;
		}

		public void SetEncrypted(RawEncryptedVector2Int encrypted)
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

		public Vector2Int GetDecrypted()
		{
			return InternalDecrypt();
		}

		private Vector2Int InternalDecrypt()
		{
			if (!inited)
			{
				currentCryptoKey = cryptoKey;
				hiddenValue = Encrypt(zero);
				fakeValue = zero;
				fakeValueActive = false;
				inited = true;
				return zero;
			}
			Vector2Int vector2Int = default(Vector2Int);
			vector2Int.x = ObscuredInt.Decrypt(hiddenValue.x, currentCryptoKey);
			vector2Int.y = ObscuredInt.Decrypt(hiddenValue.y, currentCryptoKey);
			Vector2Int vector2Int2 = vector2Int;
			if (ObscuredCheatingDetector.ExistsAndIsRunning && fakeValueActive && vector2Int2 != fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return vector2Int2;
		}

		private int InternalDecryptField(int encrypted)
		{
			int key = cryptoKey;
			if (currentCryptoKey != cryptoKey)
			{
				key = currentCryptoKey;
			}
			return ObscuredInt.Decrypt(encrypted, key);
		}

		private int InternalEncryptField(int encrypted)
		{
			return ObscuredInt.Encrypt(encrypted, cryptoKey);
		}

		public static implicit operator ObscuredVector2Int(Vector2Int value)
		{
			return new ObscuredVector2Int(value);
		}

		public static implicit operator Vector2Int(ObscuredVector2Int value)
		{
			return value.InternalDecrypt();
		}

		public static implicit operator Vector2(ObscuredVector2Int value)
		{
			return value.InternalDecrypt();
		}

		public override int GetHashCode()
		{
			return InternalDecrypt().GetHashCode();
		}

		public override string ToString()
		{
			return InternalDecrypt().ToString();
		}
	}
}
