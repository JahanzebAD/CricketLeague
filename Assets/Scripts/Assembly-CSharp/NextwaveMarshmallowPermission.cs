using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class NextwaveMarshmallowPermission : MonoBehaviour
{
	public static NextwaveMarshmallowPermission instance;

	private int INITIAL_PERMISSIONS_REQUEST_CODE = 100;

	private int SINGLE_PERMISSIONS_REQUEST_CODE = 200;

	public AndroidJavaObject objNextwavePermission;

	public AndroidJavaObject objNative;

	public AndroidJavaObject playerActivityContext;

	private string positiveButton = "Ok";

	private string negativeButton = "Cancel";

	private bool isInitialPermissionRequested;

	private List<string> myDangerousPermissions = new List<string>();

	private List<string> myExplanationToPermissions = new List<string>();

	private List<string> mySettingMessages = new List<string>();

	private List<string> neededExplanationPermissions = new List<string>();

	private List<string> neededSettingsScreenPermissions = new List<string>();

	private List<string> normalRequestPermissions = new List<string>();

	private int neededExplanationIndexe = -1;

	private int neededSettingScreenIndexe = -1;

	private string[] deniedInitialPermissions;

	private string singlePermission;

	private string singlePermissionPositiveButton;

	private string singlePermissionNegativeButton;

	private string singlePermissionCallBackGameobjectName;

	private string singlePermissionCallBackMethodName;

	private bool isFromSettingScreen;

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		instance = this;
	}

	private void Start()
	{
		initTheNativeAndroid();
	}

	public void initTheNativeAndroid()
	{
        //if (!isMarshMallow())
        //{
           moveToNextScene();
        //    return;
        //}
        if (objNextwavePermission == null)
        {
            AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.nextwave.unityandroidpermission.PermissionManager");
            objNextwavePermission = androidJavaClass.CallStatic<AndroidJavaObject>("instance", new object[0]);
        }
        if (objNative == null)
        {
            AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            //playerActivityContext = androidJavaClass2.GetStatic<AndroidJavaObject>("currentActivity");
            //AndroidJavaClass androidJavaClass3 = new AndroidJavaClass("com.nextwave.android.NativeAndroid");
            //if (androidJavaClass3 != null)
            //{
            //    objNative = androidJavaClass3.CallStatic<AndroidJavaObject>("instance", new object[0]);
            //    objNative.Call("setContext", playerActivityContext);
            //    objNative.Call("setActivity", playerActivityContext);
            //}
        }
        requestTheInitialPermissions();
    }

    //public bool isMarshMallow()
    //{
    //    using AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Build$VERSION");
    //    int @static = androidJavaClass.GetStatic<int>("SDK_INT");
    //    if (@static >= 23)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    public void requestTheInitialPermissions()
    {
        isInitialPermissionRequested = true;
        myDangerousPermissions.Add("android.permission.WRITE_EXTERNAL_STORAGE");
        myDangerousPermissions.Add("android.permission.ACCESS_COARSE_LOCATION");
        myExplanationToPermissions.Add("Read/Write permissions is needed to offers and caching ads.");
        myExplanationToPermissions.Add("Coarse location permission is needed to serve you location specific ads & offers.");
        mySettingMessages.Add("You have denied STORAGE permission, please enable it from settings.");
        mySettingMessages.Add("You have denied ACCESS COARSE LOCATION permission, please enable it from settings.");
        List<string> list = new List<string>();
        for (byte b = 0; b < myDangerousPermissions.Count; b = (byte)(b + 1))
        {
            if (!objNextwavePermission.Call<bool>("checkSelfPermission", new object[1] { myDangerousPermissions[b] }))
            {
                list.Add(myDangerousPermissions[b]);
            }
        }
        if (list.Count > 0)
		{
			//bool flag = false;
			for (byte b2 = 0; b2 < list.Count; b2 = (byte)(b2 + 1))
			{
				//flag = false;
				if (objNextwavePermission.Call<bool>("shouldShowRequestPermissionRationale", new object[1] { list[b2] }))
				{
					neededExplanationIndexe = 0;
					neededExplanationPermissions.Add(list[b2]);
				}
				else if (PlayerPrefs.HasKey(list[b2]))
				{
					neededSettingScreenIndexe = 0;
					neededSettingsScreenPermissions.Add(list[b2]);
				}
				else
				{
					normalRequestPermissions.Add(list[b2]);
				}
			}
			if (normalRequestPermissions.Count > 0)
			{
				requestInitialPermissions(normalRequestPermissions.ToArray());
			}
			else if (neededExplanationPermissions.Count > 0)
			{
				showTheExplanationOnebyOne();
			}
			else if (neededSettingsScreenPermissions.Count > 0)
			{
				showTheSettingScreenOnebyOne();
			}
			else
			{
				moveToNextScene();
			}
		}
		else
		{
			moveToNextScene();
		}
		list = null;
	}

	private void showTheExplanationOnebyOne()
	{
		objNative.Call("showTheAlertDialog", base.gameObject.name, "initialRequestExplanationAlertClicked", "None", myExplanationToPermissions[myDangerousPermissions.IndexOf(neededExplanationPermissions[neededExplanationIndexe])], positiveButton, "None", true, "None");
		PlayerPrefs.SetString(neededExplanationPermissions[neededExplanationIndexe], "neveraskagain");
	}

	private void showTheSettingScreenOnebyOne()
	{
		positiveButton = "Settings";
		negativeButton = "Cancel";
		objNative.Call("showTheAlertDialog", base.gameObject.name, "showTheInitialSettingScreen", "None", mySettingMessages[myDangerousPermissions.IndexOf(neededSettingsScreenPermissions[neededSettingScreenIndexe])], positiveButton, negativeButton, true, "None");
		neededSettingScreenIndexe++;
	}

	private void requestInitialPermissions(string[] initialPermissionss)
	{
		deniedInitialPermissions = initialPermissionss;
		objNextwavePermission.Call("requestPermissions", deniedInitialPermissions, INITIAL_PERMISSIONS_REQUEST_CODE);
	}

	public void requestThePermission(string permission, string explanationString, string positiveButton, string negativeButton, bool isCancelable, int requestCode, string callBackGameObjectName, string callBackMethodName)
	{
		singlePermission = permission;
		SINGLE_PERMISSIONS_REQUEST_CODE = requestCode;
		singlePermissionPositiveButton = positiveButton;
		singlePermissionNegativeButton = negativeButton;
		singlePermissionCallBackGameobjectName = callBackGameObjectName;
		singlePermissionCallBackMethodName = callBackMethodName;
		if (!objNextwavePermission.Call<bool>("checkSelfPermission", new object[1] { permission }))
		{
			if (objNextwavePermission.Call<bool>("shouldShowRequestPermissionRationale", new object[1] { permission }))
			{
				objNative.Call("showTheAlertDialog", base.gameObject.name, "singleRequestExplanationAlertClicked", "None", explanationString, positiveButton, "None", isCancelable, "None");
				PlayerPrefs.SetString(permission, "neveraskagain");
			}
			else if (PlayerPrefs.HasKey(permission))
			{
				this.positiveButton = "Settings";
				this.negativeButton = "Cancel";
				explanationString = "You have denied CONTACT, please enable it from setting!";
				objNative.Call("showTheAlertDialog", base.gameObject.name, "showTheSettingScreen", "None", explanationString, this.positiveButton, this.negativeButton, isCancelable, "None");
			}
			else
			{
				string[] array = new string[1] { permission };
				objNextwavePermission.Call("requestPermissions", array, SINGLE_PERMISSIONS_REQUEST_CODE);
			}
		}
		else
		{
			if (PlayerPrefs.HasKey(permission))
			{
				PlayerPrefs.DeleteKey(permission);
				PlayerPrefs.Save();
			}
			objNative.Call("callTheUnityMethod", callBackGameObjectName, callBackMethodName);
		}
	}

	public void onRequestPermissionsResult(string data)
	{
		string[] array = data.Split("|"[0]);
		int num = int.Parse(array[0]);
		if (num == INITIAL_PERMISSIONS_REQUEST_CODE)
		{
			if (neededExplanationIndexe == -1 && neededSettingScreenIndexe == -1)
			{
				moveToNextScene();
			}
			else if (neededExplanationPermissions.Count > 0 && neededExplanationIndexe < neededExplanationPermissions.Count)
			{
				showTheExplanationOnebyOne();
			}
			else if (neededSettingsScreenPermissions.Count > 0 && neededSettingScreenIndexe < neededSettingsScreenPermissions.Count)
			{
				showTheSettingScreenOnebyOne();
			}
			else
			{
				moveToNextScene();
			}
		}
		else
		{
			if (num != SINGLE_PERMISSIONS_REQUEST_CODE)
			{
				return;
			}
			if (int.Parse(array[2]) == 0)
			{
				objNative.Call("callTheUnityMethod", singlePermissionCallBackGameobjectName, singlePermissionCallBackMethodName);
				return;
			}
			Server_Connection.instance.Get_User_Guest();
			if (!ObscuredPrefs.HasKey("FirstDailyClaim"))
			{
				Singleton<DailyRewardManager>.instance.GetDailyRewardsDay();
				Singleton<CheckinRewardManager>.instance.ShowMe();
				Singleton<CheckinRewardManager>.instance.HighlightTodaysReward(Singleton<DailyRewardManager>.instance.day);
				ObscuredPrefs.SetInt("FirstDailyClaim", 1);
			}
		}
	}

	public void initialRequestExplanationAlertClicked(string result)
	{
		if (result.Equals(positiveButton))
		{
			requestInitialPermissions(new string[1] { neededExplanationPermissions[neededExplanationIndexe] });
			neededExplanationIndexe++;
		}
	}

	public void singleRequestExplanationAlertClicked(string result)
	{
		if (result.Equals(singlePermissionPositiveButton))
		{
			string[] array = new string[1] { singlePermission };
			objNextwavePermission.Call("requestPermissions", array, SINGLE_PERMISSIONS_REQUEST_CODE);
		}
	}

	public void showTheSettingScreen(string result)
	{
		if (result.Equals(positiveButton))
		{
			objNative.Call("showTheAppDetailSettings", "this");
		}
		else if (!result.Equals(negativeButton))
		{
		}
	}

	private void showTheInitialSettingScreen(string result)
	{
		if (result.Equals(positiveButton))
		{
			isFromSettingScreen = true;
			objNative.Call("showTheAppDetailSettings", "this");
		}
		else if (result.Equals(negativeButton))
		{
			if (neededSettingScreenIndexe < neededSettingsScreenPermissions.Count)
			{
				showTheSettingScreenOnebyOne();
			}
			else
			{
				moveToNextScene();
			}
		}
	}

	public bool isPermitted(string permission)
	{
		return true;
	}

	public void moveToNextScene()
	{
		isInitialPermissionRequested = false;
		if (Singleton<LoadingScreen>.instance != null)
		{
			Singleton<LoadingScreen>.instance.LoadingBarAnim();
		}
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			return;
		}
		if (isFromSettingScreen)
		{
			isFromSettingScreen = false;
			bool flag = true;
			while (neededSettingScreenIndexe < neededSettingsScreenPermissions.Count)
			{
				if (objNextwavePermission.Call<bool>("checkSelfPermission", new object[1] { neededSettingsScreenPermissions[neededSettingScreenIndexe] }))
				{
					neededSettingScreenIndexe++;
					continue;
				}
				flag = false;
				showTheSettingScreenOnebyOne();
				break;
			}
			if (flag)
			{
				moveToNextScene();
			}
		}
		else if (!isInitialPermissionRequested)
		{
		}
	}
}
