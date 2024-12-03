using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GreedyGame.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Google_SignIn : Singleton<Google_SignIn>
{
	private string googleEmailID;

	public static string googleUserName;

	public static string googleUserID;

	private string prefGoogleName = "prefGoogleName";

	public string firstTimeSignIn = "firstTimeSignIn";

	public string prefGoogleAuthenticated = "prefGoogleAuthenticated";

	private string prefGoogleUserID = "prefGoogleUserID";

	public string prefFirstTimeGuestUser = "prefFirstTimeGuestUser";

	public Image googleUserImage;

	public Image userImg;

	public Image googlePlayIcon;

	public Text uigoogleUserName;

	public Text userName;

	public int googleAuthenticated;

	public int firstTimeGoogleSignIn;

	public static int guestUserID = -1;

	public static int FirstTimeGuestUser = 1;

	private string profilePicURL = string.Empty;

	public bool signOutButtonClicked;

	public bool callonlineevent;

	public void LoadUIComponents()
	{
		googleUserImage = GameObject.FindGameObjectWithTag("userimage").GetComponent<Image>();
		googlePlayIcon = GameObject.FindGameObjectWithTag("googleplay").GetComponent<Image>();
		userImg = GameObject.FindGameObjectWithTag("profilepic").GetComponent<Image>();
		uigoogleUserName = GameObject.FindGameObjectWithTag("googleusername").GetComponent<Text>();
		userName = GameObject.FindGameObjectWithTag("username").GetComponent<Text>();
		Singleton<userInfo>.instance.Holder.SetActive(value: false);
		Singleton<GameModeTWO>.instance.Holder.SetActive(value: false);
		GetOfflineGoogleData();
	}

	private void Awake()
	{
		CONTROLLER.username = string.Empty;
		if (PlayerPrefs.HasKey("ud_removeads"))
		{
			Server_Connection.instance.RemoveAdsPurchased = PlayerPrefs.GetInt("ud_removeads", Server_Connection.instance.RemoveAdsPurchased);
			CONTROLLER.AdsPurchased = Server_Connection.instance.RemoveAdsPurchased;
		}
		if (Server_Connection.instance.RemoveAdsPurchased == 1)
		{
			Singleton<IAP_Manager>.instance.RemoveAdsDone();
		}
		if (PlayerPrefs.HasKey(prefFirstTimeGuestUser))
		{
			FirstTimeGuestUser = PlayerPrefs.GetInt(prefFirstTimeGuestUser, FirstTimeGuestUser);
		}
		else
		{
			FirstTimeGuestUser = 1;
		}
		if (PlayerPrefs.HasKey(prefGoogleName))
		{
			CONTROLLER.username = PlayerPrefs.GetString(prefGoogleName, googleUserName);
			googleUserName = PlayerPrefs.GetString(prefGoogleName, googleUserName);
		}
		if (PlayerPrefs.HasKey("userStatus"))
		{
			CONTROLLER.GetUserStatus = PlayerPrefs.GetInt("userStatus");
		}
		if (PlayerPrefs.HasKey(prefGoogleAuthenticated))
		{
			googleAuthenticated = PlayerPrefs.GetInt(prefGoogleAuthenticated);
		}
		else
		{
			googleAuthenticated = 0;
		}
		//Singleton<GameModeTWO>.instance.SignOut_Button_Set();
		if (googleAuthenticated == 0)
		{
			googleUserImage.sprite = Singleton<GameModeTWO>.instance.defaultImg;
			googleUserImage.gameObject.SetActive(value: false);
			googlePlayIcon.gameObject.SetActive(value: true);
		}
		if (PlayerPrefs.HasKey(prefGoogleUserID))
		{
			googleUserID = PlayerPrefs.GetString(prefGoogleUserID);
		}
		else
		{
			googleUserID = string.Empty;
		}
		if (PlayerPrefs.HasKey(firstTimeSignIn))
		{
			firstTimeGoogleSignIn = PlayerPrefs.GetInt(firstTimeSignIn, firstTimeGoogleSignIn);
		}
		if (firstTimeGoogleSignIn == 0)
		{
			PlayGamesPlatform.DebugLogEnabled = true;
			PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().RequestEmail().Build();
			PlayGamesPlatform.InitializeInstance(configuration);
			PlayGamesPlatform.DebugLogEnabled = true;
			PlayGamesPlatform.Activate();
			if (!ObscuredPrefs.HasKey("FirstTimeSpin"))
			{
				ObscuredPrefs.SetInt("freeSpinTimer", 1);
				ObscuredPrefs.SetInt("FirstTimeSpin", 1);
			}
		}
	}

	private void Start()
	{
		if (!CONTROLLER.isGameDataSecure)
		{
		}
		if (firstTimeGoogleSignIn == 0 && FirstTimeGuestUser == 1)
		{
			Invoke("InitialGoogleSignIn", 0.5f);
		}
		else if (firstTimeGoogleSignIn == 1 && googleAuthenticated == 0 && CONTROLLER.isGameDataSecure)
		{
			Server_Connection.instance.Get_User_Guest();
		}
		else if (googleAuthenticated == 1)
		{
			Server_Connection.instance.Get_User();
		}
		if (Server_Connection.instance.RemoveAdsPurchased == 0 && CONTROLLER.isGreedyGameEnabled == 1)
		{
			SingletoneBase<GreedyCampaignLoader>.Instance.Call_GreedyAdInit();
		}
		StartCoroutine(OnlineFirebaeuser());
	}

	private void InitialGoogleSignIn()
	{
		//if (NextwaveMarshmallowPermission.instance.isMarshMallow())
		//{
			NextwaveMarshmallowPermission.instance.requestThePermission("android.permission.GET_ACCOUNTS", "Google Play Games Services requires access to your contacts to sign-in to the game.", "ok", "cancel", isCancelable: true, 200, base.gameObject.name, "signInWithMarshMallow");
		//}
		//else
		//{
			signInWithMarshMallow();
		//}
	}

	public void UI_signInWithMarshMallow()
	{
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().RequestEmail().Build();
		PlayGamesPlatform.InitializeInstance(configuration);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
		//if (NextwaveMarshmallowPermission.instance.isMarshMallow())
		//{
			NextwaveMarshmallowPermission.instance.requestThePermission("android.permission.GET_ACCOUNTS", "Google Play Games Services requires access to your contacts to sign-in to the game.", "ok", "cancel", isCancelable: true, 200, base.gameObject.name, "signInWithMarshMallow");
		//}
		//else
		//{
			signInWithMarshMallow();
		//}
	}

	public void signInWithMarshMallow()
	{
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	StartCoroutine(GoogleSignIn());
		//}
		 if (!ObscuredPrefs.HasKey("FirstDailyClaim"))
		{
			Singleton<DailyRewardManager>.instance.GetDailyRewardsDay();
			Singleton<CheckinRewardManager>.instance.ShowMe();
			Singleton<CheckinRewardManager>.instance.HighlightTodaysReward(Singleton<DailyRewardManager>.instance.day);
			ObscuredPrefs.SetInt("FirstDailyClaim", 1);
		}
	}

	public void signInWithMarshMallow2()
	{
		StartCoroutine(GoogleSignIn());
	}

	public void Display_UserInfo()
	{
		uigoogleUserName.text = googleUserName;
		userName.text = googleUserName;
		if (profilePicURL != string.Empty)
		{
			StartCoroutine(GetProfilePicture(profilePicURL));
		}
		Singleton<LoadingPanelTransition>.instance.HideMe();
		if (!ObscuredPrefs.HasKey("FirstDailyClaim"))
		{
			Singleton<DailyRewardManager>.instance.GetDailyRewardsDay();
			Singleton<CheckinRewardManager>.instance.ShowMe();
			Singleton<CheckinRewardManager>.instance.HighlightTodaysReward(Singleton<DailyRewardManager>.instance.day);
			ObscuredPrefs.SetInt("FirstDailyClaim", 1);
		}
	}

	private IEnumerator GoogleSignIn()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (googleAuthenticated == 0 && Network_Connection.isNetworkConnected && CONTROLLER.isGameDataSecure)
		{
			Singleton<LoadingPanelTransition>.instance.PanelTransition();
			if (!Social.localUser.authenticated)
			{
				Social.localUser.Authenticate(delegate(bool success)
				{
					if (success)
					{
						googleUserID = Social.localUser.id;
						PlayerPrefs.SetString(prefGoogleUserID, googleUserID);
						googleUserName = Social.localUser.userName;
						CONTROLLER.username = googleUserName;
						profilePicURL = PlayGamesPlatform.Instance.GetUserImageUrl();
						googleEmailID = ((PlayGamesLocalUser)Social.localUser).Email;
						firstTimeGoogleSignIn = 1;
						PlayerPrefs.SetInt(firstTimeSignIn, firstTimeGoogleSignIn);
						PlayerPrefs.SetString(prefGoogleName, googleUserName);
						PlayerPrefs.Save();
						Server_Connection.instance.Get_User();
						if (InterfaceHandler._instance.bForceConnectMultiplayer)
						{
							InterfaceHandler._instance.HideSignInfailedPopup();
							InterfaceHandler._instance.OpenMultiplayer();
						}
					}
					else
					{
						googleAuthenticated = 0;
						PlayerPrefs.SetInt(prefGoogleAuthenticated, googleAuthenticated);
						PlayerPrefs.Save();
						Singleton<LoadingPanelTransition>.instance.HideMe();
						if (CONTROLLER.isGameDataSecure)
						{
							Server_Connection.instance.Get_User_Guest();
						}
					}
				});
			}
			else
			{
				Singleton<LoadingPanelTransition>.instance.HideMe();
			}
		}
		else
		{
			if (googleAuthenticated == 1 && Network_Connection.isNetworkConnected && CONTROLLER.isGameDataSecure)
			{
				Server_Connection.instance.Get_User();
				GetOfflineGoogleData();
			}
			else
			{
				Server_Connection.instance.Get_User();
			}
			Singleton<LoadingPanelTransition>.instance.HideMe();
		}
		Singleton<LoadingPanelTransition>.instance.HideMe();
	}

	public void GetOfflineGoogleData()
	{
		googleUserName = PlayerPrefs.GetString(prefGoogleName, googleUserName);
		if (googleUserName != string.Empty.ToString())
		{
			uigoogleUserName.text = googleUserName;
			userName.text = googleUserName;
			Retrieveimage();
		}
		else
		{
			googleUserName = LocalizationData.instance.getText(511);
			uigoogleUserName.text = googleUserName;
		}
	}

	private IEnumerator OnlineFirebaeuser()
	{
		if (!callonlineevent)
		{
			yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
			if (Network_Connection.isNetworkConnected)
			{
				//FirebaseAnalyticsManager.instance.logEvent("OnlineUser", "OnlineUserAction", CONTROLLER.userID);
			}
			else
			{
				//FirebaseAnalyticsManager.instance.logEvent("OfflineUser", "OnlineUserAction", CONTROLLER.userID);
			}
			callonlineevent = true;
		}
	}

	public IEnumerator GetProfilePicture(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		Texture2D tex = www.texture;
		googleUserImage.sprite = Sprite.Create(www.texture, new Rect(0f, 0f, www.texture.width, www.texture.height), new Vector2(0f, 0f));
		googleUserImage.gameObject.SetActive(value: true);
		googlePlayIcon.gameObject.SetActive(value: false);
		userImg.sprite = Sprite.Create(www.texture, new Rect(0f, 0f, www.texture.width, www.texture.height), new Vector2(0f, 0f));
		ImageSavers.SaveTexture(tex, "Googleplayprofpic");
	}

	public void Retrieveimage()
	{
		googleUserImage.sprite = Sprite.Create(ImageSavers.RetriveTexture("Googleplayprofpic"), new Rect(0f, 0f, PlayerPrefs.GetInt("Googleplayprofpic_w"), PlayerPrefs.GetInt("Googleplayprofpic_h")), new Vector2(0f, 0f));
		googleUserImage.gameObject.SetActive(value: true);
		googlePlayIcon.gameObject.SetActive(value: false);
		userImg.sprite = Sprite.Create(ImageSavers.RetriveTexture("Googleplayprofpic"), new Rect(0f, 0f, PlayerPrefs.GetInt("Googleplayprofpic_w"), PlayerPrefs.GetInt("Googleplayprofpic_h")), new Vector2(0f, 0f));
	}

	public void SignoutUserSync()
	{
		Server_Connection.instance.Get_User_Leaderboard_Sync();
		Singleton<AchievementsSyncronizer>.instance.OnSignOut();
		bool flag = false;
		string value = "0";
		if (ObscuredPrefs.HasKey("freeSpinTimer"))
		{
			if (ObscuredPrefs.HasKey("ubFreeSpin"))
			{
				value = ObscuredPrefs.GetString("ubFreeSpin", "0");
			}
			flag = true;
		}
		bool flag2 = false;
		if (ObscuredPrefs.HasKey("FirstDailyClaim"))
		{
			flag2 = true;
		}
		int value2 = 0;
		bool flag3 = false;
		if (ObscuredPrefs.HasKey("XPMilestoneIndex"))
		{
			value2 = ObscuredPrefs.GetInt("XPMilestoneIndex");
			flag3 = true;
		}
		bool flag4 = false;
		if (PlayerPrefs.HasKey("GDPRClicked"))
		{
			flag4 = true;
		}
		int value3 = -1;
		if (ObscuredPrefs.HasKey("MilestoneClaimedIndex"))
		{
			value3 = ObscuredPrefs.GetInt("MilestoneClaimedIndex");
		}
		PlayerPrefs.DeleteAll();
		ObscuredPrefs.DeleteAll();
		QuickPlayFreeEntry.Instance.SetDailyRewardsTimer();
		if (flag)
		{
			ObscuredPrefs.SetInt("freeSpinTimer", 1);
			ObscuredPrefs.SetString("ubFreeSpin", value);
			flag = false;
		}
		if (flag2)
		{
			ObscuredPrefs.SetInt("FirstDailyClaim", 1);
		}
		if (flag3)
		{
			ObscuredPrefs.SetInt("XPMilestoneIndex", value2);
		}
		if (flag4)
		{
			PlayerPrefs.SetInt("GDPRClicked", 1);
			PlayerPrefs.SetInt("InstallationPopup", 1);
		}
		ObscuredPrefs.SetInt("MilestoneClaimedIndex", value3);
		Singleton<LoadPlayerPrefs>.instance.GetPowerUpDetails();
		SavePlayerPrefs.OnSignOut();
		Singleton<GameModeTWO>.instance.MainmenuUpgradesUI();
		Singleton<GameModeTWO>.instance.UpgradeUI_Reset();
		Singleton<GameModeTWO>.instance.ResetDetails();
		CONTROLLER.username = string.Empty;
		uigoogleUserName.text = LocalizationData.instance.getText(511);
		Singleton<GameModeTWO>.instance.SigninButton.SetActive(value: true);
		googleUserImage.sprite = Singleton<GameModeTWO>.instance.defaultImg;
		googleUserImage.gameObject.SetActive(value: false);
		googlePlayIcon.gameObject.SetActive(value: true);
		googleAuthenticated = 0;
		PlayerPrefs.SetInt(prefGoogleAuthenticated, googleAuthenticated);
		firstTimeGoogleSignIn = 1;
		PlayerPrefs.SetInt(firstTimeSignIn, firstTimeGoogleSignIn);
		ObscuredPrefs.SetInt("newUser", 1);
		googleUserName = string.Empty;
		PlayerPrefs.SetString(prefGoogleName, googleUserName);
		PlayerPrefs.Save();
		ObscuredPrefs.SetInt("GetUserId", 0);
		ObscuredPrefs.SetInt("GuestUserId", -1);
		ObscuredPrefs.DeleteKey("GetUserId");
		ObscuredPrefs.DeleteKey("GuestUserId");
		PlayGamesPlatform.Instance.SignOut();
		CONTROLLER.userID = 0;
		guestUserID = -1;
		if (Server_Connection.Google_Sync_status == "1" && CONTROLLER.isGameDataSecure)
		{
			Server_Connection.instance.Get_User_Guest();
		}
		Singleton<LoadingPanelTransition>.instance.HideMe();
	}

	public void GoogleSignOut()
	{
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	Server_Connection.Google_Sync_status = "0";
		//	Singleton<LoadingPanelTransition>.instance.PanelTransition();
		//	signOutButtonClicked = true;
		//	Server_Connection.instance.Get_User_Sync();
		//}
		//else
		//{
		//	CONTROLLER.PopupName = "noInternet";
		//	Singleton<Popups>.instance.ShowMe();
		//}
	}

	public void Force_SignOut()
	{
		Server_Connection.Google_Sync_status = "0";
		signOutButtonClicked = true;
		PlayerPrefs.DeleteAll();
		ObscuredPrefs.DeleteAll();
		Singleton<LoadPlayerPrefs>.instance.GetPowerUpDetails();
		SavePlayerPrefs.OnSignOut();
		Singleton<GameModeTWO>.instance.MainmenuUpgradesUI();
		Singleton<GameModeTWO>.instance.UpgradeUI_Reset();
		Singleton<GameModeTWO>.instance.ResetDetails();
		CONTROLLER.username = string.Empty;
		uigoogleUserName.text = LocalizationData.instance.getText(511);
		Singleton<GameModeTWO>.instance.SigninButton.SetActive(value: true);
		googleUserImage.sprite = Singleton<GameModeTWO>.instance.defaultImg;
		googleUserImage.gameObject.SetActive(value: false);
		googlePlayIcon.gameObject.SetActive(value: true);
	}

	public void Post_GoogleAchievements()
	{
	}

	public void Show_GoogleAchievments()
	{
		Social.ShowAchievementsUI();
	}

	public void Post_GoogleLeaderboard()
	{
	}

	public void Show_GoogleLeaderboard()
	{
	}
}
