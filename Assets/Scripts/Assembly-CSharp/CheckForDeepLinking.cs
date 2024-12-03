using UnityEngine;

public class CheckForDeepLinking : MonoBehaviour
{
	public AndroidJavaObject objNative1;

	private AndroidJavaObject playerActivityContext1;

	public static CheckForDeepLinking instance;

	public int Goto_GameScreen;

	private void Awake()
	{
		instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
		CheckForAnyNewIntent();
	}

	public void OnApplicationPause(bool isPaused)
	{
		if (!isPaused && ManageScene.activeSceneName() == "MainMenu")
		{
			CheckForAnyNewIntent();
		}
	}

	public void CheckForAnyNewIntent()
	{
		if (objNative1 == null)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			//playerActivityContext1 = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			//AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.nextwave.android.NativeAndroid");
			//if (androidJavaClass2 != null)
			//{
				//objNative1 = androidJavaClass2.CallStatic<AndroidJavaObject>("instance", new object[0]);
				//objNative1.Call("setContext", playerActivityContext1);
				//objNative1.Call("setActivity", playerActivityContext1);
			//}
		}
		//objNative1.Call("checkAnyNewIntent", "INTERNAL_DEEPLINKING", "DeepLinkingReceiver", "onNewIntent");
		//objNative1.Call("checkAnyNewNwQueryInIntent", "DeepLinkingReceiver", "onNewIntentReferrer");
	}

	public void onNewIntentReferrer(string data)
	{
		if (!(data == string.Empty) && data != string.Empty && data != null)
		{
			//FirebaseAnalyticsManager.instance.logEvent("InstallReferral", "Referral_" + data, string.Empty);
		}
	}

	public void onNewIntent(string data)
	{
		if (!(data == string.Empty))
		{
			string[] array = data.Split("|"[0]);
			Goto_GameScreen = int.Parse(array[1]);
			CONTROLLER.pushNotiClicked = true;
			if (Goto_GameScreen == 1)
			{
				Goto_LastPlayedGamemode();
			}
			else if (Goto_GameScreen == 8)
			{
				CONTROLLER.PushScreenNumber = 8;
			}
			else
			{
				CONTROLLER.PushScreenNumber = Goto_GameScreen;
			}
			Singleton<GameModeTWO>.instance.DirectLink();
		}
	}

	public void Goto_LastPlayedGamemode()
	{
		if (CONTROLLER.lastPlayedMode == 1)
		{
			CONTROLLER.PushScreenNumber = 1;
		}
		else if (CONTROLLER.lastPlayedMode == 2)
		{
			CONTROLLER.PushScreenNumber = 2;
		}
		else if (CONTROLLER.lastPlayedMode == 3)
		{
			CONTROLLER.PushScreenNumber = 3;
		}
		else if (CONTROLLER.lastPlayedMode == 4)
		{
			CONTROLLER.PushScreenNumber = 4;
		}
		else if (CONTROLLER.lastPlayedMode == 5)
		{
			CONTROLLER.PushScreenNumber = 5;
		}
		else if (CONTROLLER.lastPlayedMode == 6)
		{
			CONTROLLER.PushScreenNumber = 6;
		}
		else
		{
			CONTROLLER.PushScreenNumber = 0;
		}
	}
}
