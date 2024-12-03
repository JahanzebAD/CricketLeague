using System;
using UnityEngine;

public class Unbiased_Time : MonoBehaviour
{
	private static Unbiased_Time instance;

	[HideInInspector]
	public long timeOffset;

	public static Unbiased_Time Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject gameObject = new GameObject("Unbiased_TimeSingleton");
				instance = gameObject.AddComponent<Unbiased_Time>();
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			return instance;
		}
	}

	private void Awake()
	{
		SessionStart();
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			SessionEnd();
		}
		else
		{
			SessionStart();
		}
	}

	private void OnApplicationQuit()
	{
		SessionEnd();
	}

	public DateTime Now()
	{
		return DateTime.Now.AddSeconds(-1f * (float)timeOffset);
	}

	public void UpdateTimeOffset()
	{
		UpdateTimeOffsetAndroid();
	}

	public bool IsUsingSystemTime()
	{
		return UsingSystemTimeAndroid();
	}

	private void SessionStart()
	{
		StartAndroid();
	}

	private void SessionEnd()
	{
		EndAndroid();
	}

	private void UpdateTimeOffsetAndroid()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		using AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		using AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.vasilij.Unbiased_Time.Unbiased_Time");
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		if (@static != null && androidJavaClass2 != null)
		{
			timeOffset = androidJavaClass2.CallStatic<long>("vtcTimestampOffset", new object[1] { @static });
		}
	}

	private void StartAndroid()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		using AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		using AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.vasilij.Unbiased_Time.Unbiased_Time");
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		if (@static != null && androidJavaClass2 != null)
		{
			androidJavaClass2.CallStatic("vtcOnSessionStart", @static);
			timeOffset = androidJavaClass2.CallStatic<long>("vtcTimestampOffset", new object[0]);
		}
	}

	private void EndAndroid()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		using AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		using AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.vasilij.Unbiased_Time.Unbiased_Time");
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		if (@static != null)
		{
			androidJavaClass2?.CallStatic("vtcOnSessionEnd", @static);
		}
	}

	private bool UsingSystemTimeAndroid()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return true;
		}
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.vasilij.Unbiased_Time.Unbiased_Time");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			if (@static != null && androidJavaClass2 != null)
			{
				return androidJavaClass2.CallStatic<bool>("vtcUsingDeviceTime", new object[0]);
			}
		}
		return true;
	}
}
