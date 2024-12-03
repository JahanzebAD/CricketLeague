using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
	public Text toastMsg;

	private void Awake()
	{
		base.gameObject.SetActive(value: true);
	}

	public void setMessge(string msg)
	{
		toastMsg.text = msg;
		if (Time.timeScale < 1f)
		{
			StartCoroutine(DestroyToast(Time.realtimeSinceStartup + 2f));
		}
		else
		{
			Invoke("disableToast", 2f);
		}
	}

	private void disableToast()
	{
		Object.Destroy(base.gameObject);
	}

	private IEnumerator DestroyToast(float time)
	{
		while (Time.realtimeSinceStartup < time)
		{
			yield return 0;
		}
		Object.Destroy(base.gameObject);
	}
}
