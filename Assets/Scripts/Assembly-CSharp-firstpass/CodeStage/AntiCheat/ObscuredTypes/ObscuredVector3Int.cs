using System;
using CodeStage.AntiCheat.Detectors;
using UnityEngine;

namespace CodeStage.AntiCheat.ObscuredTypes
{
	[Serializable]
	public struct ObscuredVector3Int
	{
		[Serializable]
		public struct RawEncryptedVector3Int
		{
			public int x;

			public int y;

			public int z;
		}

		private static int cryptoKey = 120207;

		private static readonly Vector3Int zero = Vector3Int.zero;

		[SerializeField]
		private int currentCryptoKey;

		[SerializeField]
		private RawEncryptedVector3Int hiddenValue;

		[SerializeField]
		private bool inited;

		[SerializeField]
		private Vector3Int fakeValue;

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
					fakeValue.z = InternalDecryptField(hiddenValue.z);
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
					fakeValue.z = InternalDecryptField(hiddenValue.z);
					fakeValueActive = true;
				}
				else
				{
					fakeValueActive = false;
				}
			}
		}

		public int z
		{
			get
			{
				int num = InternalDecryptField(hiddenValue.z);
				if (ObscuredCheatingDetector.ExistsAndIsRunning && fakeValueActive && Math.Abs(num - fakeValue.z) > 0)
				{
					ObscuredCheatingDetector.Instance.OnCheatingDetected();
				}
				return num;
			}
			set
			{
				hiddenValue.z = InternalEncryptField(value);
				if (ObscuredCheatingDetector.ExistsAndIsRunning)
				{
					fakeValue.x = InternalDecryptField(hiddenValue.x);
					fakeValue.y = InternalDecryptField(hiddenValue.y);
					fakeValue.z = value;
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
					2 => z, 
					_ => throw new IndexOutOfRangeException("Invalid ObscuredVector3Int index!"), 
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
				case 2:
					z = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid ObscuredVector3Int index!");
				}
			}
		}

		private ObscuredVector3Int(Vector3Int value)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = Encrypt(value);
			bool existsAndIsRunning = ObscuredCheatingDetector.ExistsAndIsRunning;
			fakeValue = ((!existsAndIsRunning) ? zero : value);
			fakeValueActive = existsAndIsRunning;
			inited = true;
		}

		public ObscuredVector3Int(int x, int y, int z)
		{
			currentCryptoKey = cryptoKey;
			hiddenValue = Encrypt(x, y, z, currentCryptoKey);
			if (ObscuredCheatingDetector.ExistsAndIsRunning)
			{
				fakeValue = new Vector3Int
				{
					x = x,
					y = y,
					z = z
				};
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

		public static RawEncryptedVector3Int Encrypt(Vector3Int value)
		{
			return Encrypt(value, 0);
		}

		public static RawEncryptedVector3Int Encrypt(Vector3Int value, int key)
		{
			return Encrypt(value.x, value.y, value.z, key);
		}

		public static RawEncryptedVector3Int Encrypt(int x, int y, int z, int key)
		{
			if (key == 0)
			{
				key = cryptoKey;
			}
			RawEncryptedVector3Int result = default(RawEncryptedVector3Int);
			result.x = ObscuredInt.Encrypt(x, key);
			result.y = ObscuredInt.Encrypt(y, key);
			result.z = ObscuredInt.Encrypt(z, key);
			return result;
		}

		public static Vector3Int Decrypt(RawEncryptedVector3Int value)
		{
			return Decrypt(value, 0);
		}

		public static Vector3Int Decrypt(RawEncryptedVector3Int value, int key)
		{
			if (key == 0)
			{
				key = cryptoKey;
			}
			Vector3Int result = default(Vector3Int);
			result.x = ObscuredInt.Decrypt(value.x, key);
			result.y = ObscuredInt.Decrypt(value.y, key);
			result.z = ObscuredInt.Decrypt(value.z, key);
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
			Vector3Int value = InternalDecrypt();
			do
			{
				currentCryptoKey = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
			}
			while (currentCryptoKey == 0);
			hiddenValue = Encrypt(value, currentCryptoKey);
		}

		public RawEncryptedVector3Int GetEncrypted()
		{
			ApplyNewCryptoKey();
			return hiddenValue;
		}

		public void SetEncrypted(RawEncryptedVector3Int encrypted)
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

		public Vector3Int GetDecrypted()
		{
			return InternalDecrypt();
		}

		private Vector3Int InternalDecrypt()
		{
			if (!inited)
			{
				currentCryptoKey = cryptoKey;
				hiddenValue = Encrypt(zero, cryptoKey);
				fakeValue = zero;
				fakeValueActive = false;
				inited = true;
				return zero;
			}
			Vector3Int vector3Int = default(Vector3Int);
			vector3Int.x = ObscuredInt.Decrypt(hiddenValue.x, currentCryptoKey);
			vector3Int.y = ObscuredInt.Decrypt(hiddenValue.y, currentCryptoKey);
			vector3Int.z = ObscuredInt.Decrypt(hiddenValue.z, currentCryptoKey);
			Vector3Int vector3Int2 = vector3Int;
			if (ObscuredCheatingDetector.ExistsAndIsRunning && fakeValueActive && vector3Int2 != fakeValue)
			{
				ObscuredCheatingDetector.Instance.OnCheatingDetected();
			}
			return vector3Int2;
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

		public static implicit operator ObscuredVector3Int(Vector3Int value)
		{
			return new ObscuredVector3Int(value);
		}

		public static implicit operator Vector3Int(ObscuredVector3Int value)
		{
			return value.InternalDecrypt();
		}

		public static implicit operator Vector3(ObscuredVector3Int value)
		{
			return value.InternalDecrypt();
		}

		public static ObscuredVector3Int operator +(ObscuredVector3Int a, ObscuredVector3Int b)
		{
			return a.InternalDecrypt() + b.InternalDecrypt();
		}

		public static ObscuredVector3Int operator +(Vector3Int a, ObscuredVector3Int b)
		{
			return a + b.InternalDecrypt();
		}

		public static ObscuredVector3Int operator +(ObscuredVector3Int a, Vector3Int b)
		{
			return a.InternalDecrypt() + b;
		}

		public static ObscuredVector3Int operator -(ObscuredVector3Int a, ObscuredVector3Int b)
		{
			return a.InternalDecrypt() - b.InternalDecrypt();
		}

		public static ObscuredVector3Int operator -(Vector3Int a, ObscuredVector3Int b)
		{
			return a - b.InternalDecrypt();
		}

		public static ObscuredVector3Int operator -(ObscuredVector3Int a, Vector3Int b)
		{
			return a.InternalDecrypt() - b;
		}

		public static ObscuredVector3Int operator *(ObscuredVector3Int a, int d)
		{
			return a.InternalDecrypt() * d;
		}

		public static bool operator ==(ObscuredVector3Int lhs, ObscuredVector3Int rhs)
		{
			return lhs.InternalDecrypt() == rhs.InternalDecrypt();
		}

		public static bool operator ==(Vector3Int lhs, ObscuredVector3Int rhs)
		{
			return lhs == rhs.InternalDecrypt();
		}

		public static bool operator ==(ObscuredVector3Int lhs, Vector3Int rhs)
		{
			return lhs.InternalDecrypt() == rhs;
		}

		public static bool operator !=(ObscuredVector3Int lhs, ObscuredVector3Int rhs)
		{
			return lhs.InternalDecrypt() != rhs.InternalDecrypt();
		}

		public static bool operator !=(Vector3Int lhs, ObscuredVector3Int rhs)
		{
			return lhs != rhs.InternalDecrypt();
		}

		public static bool operator !=(ObscuredVector3Int lhs, Vector3Int rhs)
		{
			return lhs.InternalDecrypt() != rhs;
		}

		public override bool Equals(object other)
		{
			return InternalDecrypt().Equals(other);
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
	}
}
