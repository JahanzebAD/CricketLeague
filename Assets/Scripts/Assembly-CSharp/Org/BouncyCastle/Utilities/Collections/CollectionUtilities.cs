using System;
using System.Collections;
using System.Text;

namespace Org.BouncyCastle.Utilities.Collections
{
	public abstract class CollectionUtilities
	{
		public static void AddRange(IList to, IEnumerable range)
		{
			IEnumerator enumerator = range.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object current = enumerator.Current;
					to.Add(current);
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

		public static bool CheckElementsAreOfType(IEnumerable e, Type t)
		{
			IEnumerator enumerator = e.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object current = enumerator.Current;
					if (!t.IsInstanceOfType(current))
					{
						return false;
					}
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
			return true;
		}

		public static IDictionary ReadOnly(IDictionary d)
		{
			return d;
		}

		public static IList ReadOnly(IList l)
		{
			return l;
		}

		public static ISet ReadOnly(ISet s)
		{
			return s;
		}

		public static string ToString(IEnumerable c)
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			IEnumerator enumerator = c.GetEnumerator();
			if (enumerator.MoveNext())
			{
				stringBuilder.Append(enumerator.Current.ToString());
				while (enumerator.MoveNext())
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(enumerator.Current.ToString());
				}
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}
	}
}
