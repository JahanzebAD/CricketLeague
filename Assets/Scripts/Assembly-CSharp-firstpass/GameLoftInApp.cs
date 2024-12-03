using UnityEngine;

public class GameLoftInApp
{
	private static AndroidJavaObject _plugin;

	private static AndroidJavaClass pluginClass;

	static GameLoftInApp()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			pluginClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			_plugin = pluginClass.GetStatic<AndroidJavaObject>("currentActivity");
		}
	}

	public static void showToast(string myMsg)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("showTheToast", myMsg);
		}
	}

	public static void requestForBuyFullGame()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("preLoaderScreenFoxXml");
		}
	}

	public static void getTheGame(string str)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("getTheGame", str);
		}
	}

	public static void restoreTheGame(string str)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("restoreTheGame", str);
		}
	}

	public static void termsAndConditions()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("termsAndConditions");
		}
	}

	public static void checkTheKeycodeFromDevice(string str)
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("checkTheKeycodeFromDevice", str);
		}
	}

	public static void goToHomeScreen()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			_plugin.Call("goToHomeScreen");
		}
	}
}
