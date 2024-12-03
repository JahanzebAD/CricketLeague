using System;
using System.Collections;
using UnityEngine;

public class HideObjects : MonoBehaviour
{
	private void Start()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				IEnumerator enumerator2 = transform.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						Transform transform2 = (Transform)enumerator2.Current;
						Renderer component = transform2.GetComponent<Renderer>();
						component.GetComponent<Renderer>().enabled = false;
						UnityEngine.Object.Destroy(component.gameObject);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = enumerator2 as IDisposable) != null)
					{
						disposable.Dispose();
					}
				}
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = enumerator as IDisposable) != null)
			{
				disposable2.Dispose();
			}
		}
	}
}
