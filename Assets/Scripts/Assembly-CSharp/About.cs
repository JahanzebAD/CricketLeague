//using Prime31;
//using UnityEngine;
//using UnityEngine.UI;

//public class About : Singleton<About>
//{
//	public Text versionNumber;

//	public GameObject textView;

//	public GameObject holder;

//	public ScrollRect creditsScrollView;

//	private void Awake()
//	{
//		versionNumber.text = AppInfo.Version;
//	}

//	public void ButtonEvent(int value)
//	{
//		switch (value)
//		{
//		case 1:
//			//Application.OpenURL("https://www.cricketbuddies.com/privacy-policy");
//			break;
//		case 2:
//			//Application.OpenURL("https://www.cricketbuddies.com/terms-of-use");
//			break;
//		case 3:
//		{
//			string toAddress = "cricket@nextwavemultimedia.com";
//			string text = "Version:" + AppInfo.Version;
//			string text2 = "WCC Lite-" + text + "-Feedback-" + AppInfo.Platform;
//			string empty = string.Empty;
//			if (Application.isEditor)
//			{
//				//Application.OpenURL("mailto:cricket@nextwavemultimedia.com?subject=" + text2 + "&body=Hi");
//			}
//			else
//			{
//				EtceteraAndroid.showEmailComposer(toAddress, text2, empty, isHTML: false);
//			}
//			break;
//		}
//		case 4:
//			//Application.OpenURL("https://www.cricketbuddies.com/");
//			break;
//		}
//	}

//	public void ShareGame()
//	{
//		string text = "WCC Lite";
//		//string text2 = "Check out the new game, WCC Lite on playstore https://play.google.com/store/apps/details?id=com.nextwave.wcclite";
//		AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
//		AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent");
//		androidJavaObject.Call<AndroidJavaObject>("setAction", new object[1] { androidJavaClass.GetStatic<string>("ACTION_SEND") });
//		androidJavaObject.Call<AndroidJavaObject>("setType", new object[1] { "text/plain" });
//		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
//		{
//			androidJavaClass.GetStatic<string>("EXTRA_SUBJECT"),
//			text
//		});
//		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
//		{
//			androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
//			text2
//		});
//		AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//		AndroidJavaObject @static = androidJavaClass2.GetStatic<AndroidJavaObject>("currentActivity");
//		@static.Call("startActivity", androidJavaObject);
//	}

//	public void ShowMe()
//	{
//		Singleton<NavigationBack>.instance.deviceBack = HideMe;
//		holder.SetActive(value: true);
//		Singleton<AboutPageTWOPanelTransistion>.instance.CreditsContentText.transform.localPosition = new Vector3(Singleton<AboutPageTWOPanelTransistion>.instance.CreditsContentText.transform.localPosition.x, -1190f, Singleton<AboutPageTWOPanelTransistion>.instance.CreditsContentText.transform.localPosition.z);
//		CONTROLLER.pageName = "about";
//	}

//	public void HideMe()
//	{
//		holder.SetActive(value: false);
//		Singleton<GameModeTWO>.instance.ShowWithoutAnim();
//	}

//	public void Facebook_Like()
//	{
//		if (Singleton<AdIntegrate>.instance.CheckForInternet())
//		{
//			Application.OpenURL("https://www.facebook.com/LiteWcc/");
//			return;
//		}
//		CONTROLLER.PopupName = "noInternet";
//		Singleton<Popups>.instance.ShowMe();
//	}

//	public void Twitter_Follow()
//	{
//		if (Singleton<AdIntegrate>.instance.CheckForInternet())
//		{
//			Application.OpenURL("https://twitter.com/LiteWcc");
//			return;
//		}
//		CONTROLLER.PopupName = "noInternet";
//		Singleton<Popups>.instance.ShowMe();
//	}

//	public void Rate_us()
//	{
//		if (Singleton<AdIntegrate>.instance.CheckForInternet())
//		{
//			Application.OpenURL("https://play.google.com/store/apps/details?id=com.nextwave.wcclite");
//			return;
//		}
//		CONTROLLER.PopupName = "noInternet";
//		Singleton<Popups>.instance.ShowMe();
//	}
//}
