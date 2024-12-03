using System;
using System.Collections;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Asn1
{
	public class Asn1EncodableVector : IEnumerable
	{
		private IList v = Platform.CreateArrayList();

		public Asn1Encodable this[int index] => (Asn1Encodable)v[index];

		[Obsolete("Use 'Count' property instead")]
		public int Size => v.Count;

		public int Count => v.Count;

		public Asn1EncodableVector(params Asn1Encodable[] v)
		{
			Add(v);
		}

		public static Asn1EncodableVector FromEnumerable(IEnumerable e)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector();
			IEnumerator enumerator = e.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Asn1Encodable asn1Encodable = (Asn1Encodable)enumerator.Current;
					asn1EncodableVector.Add(asn1Encodable);
				}
				return asn1EncodableVector;
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

		public void Add(params Asn1Encodable[] objs)
		{
			foreach (Asn1Encodable value in objs)
			{
				v.Add(value);
			}
		}

		public void AddOptional(params Asn1Encodable[] objs)
		{
			if (objs == null)
			{
				return;
			}
			foreach (Asn1Encodable asn1Encodable in objs)
			{
				if (asn1Encodable != null)
				{
					v.Add(asn1Encodable);
				}
			}
		}

		[Obsolete("Use 'object[index]' syntax instead")]
		public Asn1Encodable Get(int index)
		{
			return this[index];
		}

		public IEnumerator GetEnumerator()
		{
			return v.GetEnumerator();
		}
	}
}
