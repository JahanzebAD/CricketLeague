using System;
using System.Collections;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Crypto.Tls
{
	internal class DeferredHash : TlsHandshakeHash, IDigest
	{
		protected const int BUFFERING_HASH_LIMIT = 4;

		protected TlsContext mContext;

		private DigestInputBuffer mBuf;

		private IDictionary mHashes;

		private int mPrfHashAlgorithm;

		public virtual string AlgorithmName
		{
			get
			{
				throw new InvalidOperationException("Use Fork() to get a definite IDigest");
			}
		}

		internal DeferredHash()
		{
			mBuf = new DigestInputBuffer();
			mHashes = Platform.CreateHashtable();
			mPrfHashAlgorithm = -1;
		}

		private DeferredHash(byte prfHashAlgorithm, IDigest prfHash)
		{
			mBuf = null;
			mHashes = Platform.CreateHashtable();
			mPrfHashAlgorithm = prfHashAlgorithm;
			mHashes[prfHashAlgorithm] = prfHash;
		}

		public virtual void Init(TlsContext context)
		{
			mContext = context;
		}

		public virtual TlsHandshakeHash NotifyPrfDetermined()
		{
			int prfAlgorithm = mContext.SecurityParameters.PrfAlgorithm;
			if (prfAlgorithm == 0)
			{
				CombinedHash combinedHash = new CombinedHash();
				combinedHash.Init(mContext);
				mBuf.UpdateDigest(combinedHash);
				return combinedHash.NotifyPrfDetermined();
			}
			mPrfHashAlgorithm = TlsUtilities.GetHashAlgorithmForPrfAlgorithm(prfAlgorithm);
			CheckTrackingHash((byte)mPrfHashAlgorithm);
			return this;
		}

		public virtual void TrackHashAlgorithm(byte hashAlgorithm)
		{
			if (mBuf == null)
			{
				throw new InvalidOperationException("Too late to track more hash algorithms");
			}
			CheckTrackingHash(hashAlgorithm);
		}

		public virtual void SealHashAlgorithms()
		{
			CheckStopBuffering();
		}

		public virtual TlsHandshakeHash StopTracking()
		{
			byte b = (byte)mPrfHashAlgorithm;
			IDigest digest = TlsUtilities.CloneHash(b, (IDigest)mHashes[b]);
			if (mBuf != null)
			{
				mBuf.UpdateDigest(digest);
			}
			DeferredHash deferredHash = new DeferredHash(b, digest);
			deferredHash.Init(mContext);
			return deferredHash;
		}

		public virtual IDigest ForkPrfHash()
		{
			CheckStopBuffering();
			byte b = (byte)mPrfHashAlgorithm;
			if (mBuf != null)
			{
				IDigest digest = TlsUtilities.CreateHash(b);
				mBuf.UpdateDigest(digest);
				return digest;
			}
			return TlsUtilities.CloneHash(b, (IDigest)mHashes[b]);
		}

		public virtual byte[] GetFinalHash(byte hashAlgorithm)
		{
			IDigest digest = (IDigest)mHashes[hashAlgorithm];
			if (digest == null)
			{
				throw new InvalidOperationException("HashAlgorithm." + HashAlgorithm.GetText(hashAlgorithm) + " is not being tracked");
			}
			digest = TlsUtilities.CloneHash(hashAlgorithm, digest);
			if (mBuf != null)
			{
				mBuf.UpdateDigest(digest);
			}
			return DigestUtilities.DoFinal(digest);
		}

		public virtual int GetByteLength()
		{
			throw new InvalidOperationException("Use Fork() to get a definite IDigest");
		}

		public virtual int GetDigestSize()
		{
			throw new InvalidOperationException("Use Fork() to get a definite IDigest");
		}

		public virtual void Update(byte input)
		{
			if (mBuf != null)
			{
				mBuf.WriteByte(input);
				return;
			}
			IEnumerator enumerator = mHashes.Values.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					IDigest digest = (IDigest)enumerator.Current;
					digest.Update(input);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = enumerator as IDisposable) != null)
				{
					disposable.Dispose();
				}
			}
		}

		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (mBuf != null)
			{
				mBuf.Write(input, inOff, len);
				return;
			}
			IEnumerator enumerator = mHashes.Values.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					IDigest digest = (IDigest)enumerator.Current;
					digest.BlockUpdate(input, inOff, len);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = enumerator as IDisposable) != null)
				{
					disposable.Dispose();
				}
			}
		}

		public virtual int DoFinal(byte[] output, int outOff)
		{
			throw new InvalidOperationException("Use Fork() to get a definite IDigest");
		}

		public virtual void Reset()
		{
			if (mBuf != null)
			{
				mBuf.SetLength(0L);
				return;
			}
			IEnumerator enumerator = mHashes.Values.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					IDigest digest = (IDigest)enumerator.Current;
					digest.Reset();
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = enumerator as IDisposable) != null)
				{
					disposable.Dispose();
				}
			}
		}

		protected virtual void CheckStopBuffering()
		{
			if (mBuf == null || mHashes.Count > 4)
			{
				return;
			}
			IEnumerator enumerator = mHashes.Values.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					IDigest d = (IDigest)enumerator.Current;
					mBuf.UpdateDigest(d);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = enumerator as IDisposable) != null)
				{
					disposable.Dispose();
				}
			}
			mBuf = null;
		}

		protected virtual void CheckTrackingHash(byte hashAlgorithm)
		{
			if (!mHashes.Contains(hashAlgorithm))
			{
				IDigest value = TlsUtilities.CreateHash(hashAlgorithm);
				mHashes[hashAlgorithm] = value;
			}
		}
	}
}
