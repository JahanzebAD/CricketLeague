using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
	private static T Instance;

	public static T instance
	{
		get
		{
			if ((Object)Instance == (Object)null)
			{
				Instance = (T)Object.FindObjectOfType(typeof(T));
			}
			return Instance;
		}
	}
}
