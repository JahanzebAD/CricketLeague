using System.Collections;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
	public AndroidJavaObject objNative;

	private static NetworkManager _Instance;

	private bool _IsNetworkConnected;

	private float timeOut = 10f;

	public static NetworkManager Instance => _Instance;

	public bool IsNetworkConnected => _IsNetworkConnected;

	private void Awake()
	{
		if (_Instance == null)
		{
			_Instance = this;
			initTheNativeAndroid();
		}
	}

	public IEnumerator CheckInternetConnection()
	{
		//_IsNetworkConnected = Singleton<AdIntegrate>.instance.CheckForInternet();
		yield return 0;
	}

	public void initTheNativeAndroid()
	{
		if (objNative == null)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			//AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			//AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.nextwave.android.NativeAndroid");
			//if (androidJavaClass2 != null)
			//{
				//objNative = androidJavaClass2.CallStatic<AndroidJavaObject>("instance", new object[0]);
				//objNative.Call("setContext", @static);
				//objNative.Call("setActivity", @static);
			//}
		}
	}

	public bool CheckInternetUsingNative()
	{
		if (objNative != null)
		{
			bool flag = objNative.Call<bool>("isInternetOn", new object[0]);
			if (!flag)
			{
			}
			return flag;
		}
		return false;
	}
}
