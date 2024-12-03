using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	public class DerSequence : Asn1Sequence
	{
		public static readonly DerSequence Empty = new DerSequence();

		public DerSequence()
			: base(0)
		{
		}

		public DerSequence(Asn1Encodable obj)
			: base(1)
		{
			AddObject(obj);
		}

		public DerSequence(params Asn1Encodable[] v)
			: base(v.Length)
		{
			foreach (Asn1Encodable obj in v)
			{
				AddObject(obj);
			}
		}

		public DerSequence(Asn1EncodableVector v)
			: base(v.Count)
		{
			IEnumerator enumerator = v.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Asn1Encodable obj = (Asn1Encodable)enumerator.Current;
					AddObject(obj);
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

		public static DerSequence FromVector(Asn1EncodableVector v)
		{
			return (v.Count >= 1) ? new DerSequence(v) : Empty;
		}

		internal override void Encode(DerOutputStream derOut)
		{
			MemoryStream memoryStream = new MemoryStream();
			DerOutputStream derOutputStream = new DerOutputStream(memoryStream);
			IEnumerator enumerator = GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Asn1Encodable obj = (Asn1Encodable)enumerator.Current;
					derOutputStream.WriteObject(obj);
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
			Platform.Dispose(derOutputStream);
			byte[] bytes = memoryStream.ToArray();
			derOut.WriteEncoded(48, bytes);
		}
	}
}