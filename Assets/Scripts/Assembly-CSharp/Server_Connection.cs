using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using LitJson;
using UnityEngine;

public class Server_Connection : MonoBehaviour
{
	public string[] IAP_Values = new string[7];

	public int RemoveAdsPurchased;

	private int weekNumber = 1;

	private int ispushRegistered;

	private int weekNumber_Com = 1;

	private string weekNumberPref = "weekNumber";

	private string prefPushRegistered = "prefPushRegistered";

	private string updateUpgrades = "1|0|0|0*1|0|0|0*1|0|0|0*0";

	private string[] upgradesOuterSplit = new string[4];

	private string[] upgradesPowerSplit = new string[4];

	private string[] upgradesControlSplit = new string[4];

	private string[] upgradesAgilitySplit = new string[4];

	public bool guestUser;

	private bool CalledGoogle_User;

	public static string Google_Sync_status;

	public static Server_Connection instance;

	public int start_PlayerRankXP;

	public int start_playerRankAXP;

	public int end_PlayerRankXP;

	public int end_playerRankAXP;

	public int diff_Xp;

	public int diff_AXp;

	private void Awake()
	{
		Debug.unityLogger.logEnabled = false;
		if (instance == null)
		{
			instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			Object.Destroy(base.gameObject);
		}
		if (PlayerPrefs.HasKey(weekNumberPref))
		{
			weekNumber = PlayerPrefs.GetInt(weekNumberPref, weekNumber);
		}
		else
		{
			weekNumber = 1;
		}
		if (PlayerPrefs.HasKey(prefPushRegistered))
		{
			ispushRegistered = PlayerPrefs.GetInt(prefPushRegistered, ispushRegistered);
		}
		else
		{
			ispushRegistered = 0;
		}
		if (ObscuredPrefs.HasKey("GetUserId"))
		{
			CONTROLLER.userID = ObscuredPrefs.GetInt("GetUserId", CONTROLLER.userID);
		}
		else
		{
			CONTROLLER.userID = 0;
		}
		if (ObscuredPrefs.HasKey("GuestUserId"))
		{
			Google_SignIn.guestUserID = ObscuredPrefs.GetInt("GuestUserId", Google_SignIn.guestUserID);
		}
		else
		{
			Google_SignIn.guestUserID = -1;
		}
	}

	private void Start()
	{
		Encryption.Initialize();
		if (ispushRegistered == 0 || ispushRegistered != CONTROLLER.AppVersionCode)
		{
			Get_PushNotification();
		}
	}

	public void Get_User()
	{
		if (CONTROLLER.isGameDataSecure)
		{
			if (guestUser)
			{
				CalledGoogle_User = true;
				StartCoroutine(User_Sync());
			}
			else
			{
				CalledGoogle_User = false;
				StartCoroutine(User_Registration());
			}
		}
	}

	public void Get_User_Guest()
	{
		if (CONTROLLER.isGameDataSecure && !guestUser)
		{
			StartCoroutine(GuestUser_Registration());
		}
	}

	public void Get_User_Sync()
	{
		if (CONTROLLER.isGameDataSecure && (CONTROLLER.earnedCoins > 0 || CONTROLLER.spendCoins > 0 || CONTROLLER.earnedXPs > 0 || CONTROLLER.earnedTickets > 0 || CONTROLLER.spendTickets > 0 || Singleton<Google_SignIn>.instance.signOutButtonClicked) && (CONTROLLER.userID != 0 || (Google_SignIn.guestUserID != 0 && guestUser)))
		{
			StartCoroutine(User_Sync());
		}
	}

	public void Get_User_Leaderboard()
	{
		if (CONTROLLER.isGameDataSecure && CONTROLLER.userID != 0)
		{
			StartCoroutine(User_Leaderboard());
		}
	}

	public void Get_User_ArcadeLeaderboard()
	{
		if (CONTROLLER.isGameDataSecure && CONTROLLER.userID != 0)
		{
			StartCoroutine(User_ArcadeLeaderboard());
		}
	}

	public void Get_User_Leaderboard_Sync()
	{
		if (CONTROLLER.isGameDataSecure && (CONTROLLER.weekly_Xps > 0 || CONTROLLER.weekly_ArcadeXps > 0) && CONTROLLER.userID != 0)
		{
			StartCoroutine(User_Leaderboard_Sync());
		}
	}

	public void Get_PushNotification()
	{
		if (CONTROLLER.isGameDataSecure)
		{
			StartCoroutine(User_PushNotification());
		}
	}

	public void Set_StoreUpgrades()
	{
		if (CONTROLLER.isGameDataSecure)
		{
			StartCoroutine(Set_User_StoreUpgrades());
		}
	}

	public void Get_StoreUpgrades()
	{
		if (CONTROLLER.isGameDataSecure)
		{
			StartCoroutine(Get_User_StoreUpgrades());
		}
	}

	public void Get_XPRank()
	{
		if (CONTROLLER.isGameDataSecure && CONTROLLER.userID != 0)
		{
			StartCoroutine(Get_User_XPRank());
		}
	}

	public void Get_ArcadeXPRank()
	{
		if (CONTROLLER.isGameDataSecure && CONTROLLER.userID != 0)
		{
			StartCoroutine(Get_User_ArcadeXPRank());
		}
	}

	public IEnumerator GuestUser_Registration()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (Network_Connection.isNetworkConnected && CONTROLLER.isGameDataSecure)
		{
			WWWForm form = new WWWForm();
			form.AddField("action", "GuestUID");
			form.AddField("deviceid", GetAppInfo.deviceID);
			form.AddField("bv", GetAppInfo.AppVersionCode);
			form.AddField("platform", "Android");
			Singleton<LoadingPanelTransition>.instance.PanelTransition();
			WWW GetUSer = new WWW(CONTROLLER.PHP_Server_Link, form);
			yield return GetUSer;
			if (GetUSer.error == null)
			{
				JsonData jsonData = JsonMapper.ToObject(GetUSer.text);
				if (jsonData["GuestUID"] != null && (jsonData["GuestUID"]["status"].ToString() == "1" || jsonData["GuestUID"]["status"].ToString() == "2"))
				{
					CONTROLLER.GetUserStatus = int.Parse(jsonData["GuestUID"]["status"].ToString());
					weekNumber = int.Parse(jsonData["GuestUID"]["Wnum"].ToString());
					RemoveAdsPurchased = int.Parse(jsonData["GuestUID"]["na"].ToString());
					if (RemoveAdsPurchased == 1)
					{
						Singleton<IAP_Manager>.instance.RemoveAdsDone();
						PlayerPrefs.SetInt("ud_removeads", RemoveAdsPurchased);
						PlayerPrefs.Save();
					}
					else if (RemoveAdsPurchased == 0)
					{
						Singleton<IAP_Manager>.instance.Reset_PurchasedRemoveAds();
						PlayerPrefs.SetInt("ud_removeads", RemoveAdsPurchased);
						PlayerPrefs.Save();
					}
					CONTROLLER.sixMeterCount = int.Parse(jsonData["GuestUID"]["six"].ToString());
					SavePlayerPrefs.SetSixesCount();
					Singleton<GameModeTWO>.instance.ResetDetails();
					Google_SignIn.guestUserID = int.Parse(jsonData["GuestUID"]["userid"].ToString());
					ObscuredPrefs.SetInt("GuestUserId", Google_SignIn.guestUserID);
					ObscuredPrefs.SetInt("GetUserId", 0);
					ObscuredPrefs.DeleteKey("GetUserId");
					CONTROLLER.userID = 0;
					Google_SignIn.FirstTimeGuestUser = 0;
					PlayerPrefs.SetInt(Singleton<Google_SignIn>.instance.prefFirstTimeGuestUser, Google_SignIn.FirstTimeGuestUser);
					if (PlayerPrefs.HasKey(Singleton<Google_SignIn>.instance.prefFirstTimeGuestUser))
					{
						Google_SignIn.FirstTimeGuestUser = PlayerPrefs.GetInt(Singleton<Google_SignIn>.instance.prefFirstTimeGuestUser, Google_SignIn.FirstTimeGuestUser);
						if (Google_SignIn.FirstTimeGuestUser == 2)
						{
							Google_SignIn.FirstTimeGuestUser = 0;
						}
					}
					PlayerPrefs.SetInt(Singleton<Google_SignIn>.instance.prefFirstTimeGuestUser, Google_SignIn.FirstTimeGuestUser);
					PlayerPrefs.Save();
					CONTROLLER.FreeSpin = int.Parse(jsonData["GuestUID"]["SCount"].ToString());
					Singleton<SevenDaysRewards>.instance.SetRemainingDays(int.Parse(jsonData["GuestUID"]["DayBonus"].ToString()));
					QuickPlayFreeEntry.Instance.loggedIn = true;
					QuickPlayFreeEntry.Instance.StartDayRewards();
					PlayerPrefs.SetInt("userStatus", CONTROLLER.GetUserStatus);
					if (CONTROLLER.GetUserStatus == 1)
					{
						guestUser = true;
						//FirebaseAnalyticsManager.instance.setUserId(Google_SignIn.guestUserID.ToString());
					}
					else if (CONTROLLER.GetUserStatus == 2)
					{
						guestUser = true;
						CONTROLLER.tickets = int.Parse(jsonData["GuestUID"]["TotalTickets"].ToString());
						CONTROLLER.Coins = int.Parse(jsonData["GuestUID"]["TotalCoins"].ToString());
						CONTROLLER.XPs = int.Parse(jsonData["GuestUID"]["TotalXP"].ToString());
						CONTROLLER.ArcadeXPs = int.Parse(jsonData["GuestUID"]["ArcadeXp"].ToString());
						CONTROLLER.FreeSpin = int.Parse(jsonData["GuestUID"]["SCount"].ToString());
						SavePlayerPrefs.SaveUserCoins();
						SavePlayerPrefs.SaveUserTickets();
						SavePlayerPrefs.SaveUserXP();
						SavePlayerPrefs.SaveUserArcadeXP();
						SavePlayerPrefs.SaveSpins(0);
						if (Singleton<GameModeTWO>.instance != null)
						{
							Singleton<GameModeTWO>.instance.ResetDetails();
						}
						if (Google_SignIn.guestUserID != -1)
						{
							Get_User_Sync();
						}
						//FirebaseAnalyticsManager.instance.setUserId(Google_SignIn.guestUserID.ToString());
					}
					else
					{
						Singleton<LoadingPanelTransition>.instance.HideMe();
					}
				}
				else
				{
					Singleton<LoadingPanelTransition>.instance.HideMe();
				}
			}
			else
			{
				QuickPlayFreeEntry.Instance.loggedIn = false;
				if (Singleton<SevenDaysRewards>.instance.IsAvailable() == 0)
				{
					QuickPlayFreeEntry.Instance.StartDayRewards();
				}
				Singleton<LoadingPanelTransition>.instance.HideMe();
			}
		}
		else
		{
			QuickPlayFreeEntry.Instance.loggedIn = false;
			if (Singleton<SevenDaysRewards>.instance.IsAvailable() == 0)
			{
				QuickPlayFreeEntry.Instance.StartDayRewards();
			}
			Singleton<LoadingPanelTransition>.instance.HideMe();
		}
		Singleton<LoadingPanelTransition>.instance.HideMe();
	}

	public IEnumerator User_Registration()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (Network_Connection.isNetworkConnected && CONTROLLER.isGameDataSecure)
		{
			WWWForm form = new WWWForm();
			form.AddField("action", "GetUID");
			form.AddField("guid", Google_SignIn.googleUserID);
			form.AddField("type", "gp");
			form.AddField("deviceid", GetAppInfo.deviceID);
			form.AddField("bv", GetAppInfo.AppVersionCode);
			form.AddField("platform", "Android");
			form.AddField("username", Google_SignIn.googleUserName);
			form.AddField("guest_id", Google_SignIn.guestUserID);
			StartCoroutine(CheckForTimeOut());
			Singleton<LoadingPanelTransition>.instance.PanelTransition();
			WWW GetUSer = new WWW(CONTROLLER.PHP_Server_Link, form);
			yield return GetUSer;
			StopCoroutine(CheckForTimeOut());
			if (GetUSer.error == null)
			{
				JsonData jsonData = JsonMapper.ToObject(GetUSer.text);
				if (jsonData["GetUID"] != null && (jsonData["GetUID"]["status"].ToString() == "1" || jsonData["GetUID"]["status"].ToString() == "2"))
				{
					Singleton<Google_SignIn>.instance.googleAuthenticated = 1;
					PlayerPrefs.SetInt(Singleton<Google_SignIn>.instance.prefGoogleAuthenticated, Singleton<Google_SignIn>.instance.googleAuthenticated);
					PlayerPrefs.Save();
					CONTROLLER.GetUserStatus = int.Parse(jsonData["GetUID"]["status"].ToString());
					RemoveAdsPurchased = int.Parse(jsonData["GetUID"]["na"].ToString());
					if (RemoveAdsPurchased == 1)
					{
						Singleton<IAP_Manager>.instance.RemoveAdsDone();
						PlayerPrefs.SetInt("ud_removeads", RemoveAdsPurchased);
						PlayerPrefs.Save();
					}
					else if (RemoveAdsPurchased == 0)
					{
						Singleton<IAP_Manager>.instance.Reset_PurchasedRemoveAds();
						PlayerPrefs.SetInt("ud_removeads", RemoveAdsPurchased);
						PlayerPrefs.Save();
					}
					CONTROLLER.sixMeterCount = int.Parse(jsonData["GetUID"]["six"].ToString());
					SavePlayerPrefs.SetSixesCount();
					if (Singleton<GameModeTWO>.instance != null)
					{
						Singleton<GameModeTWO>.instance.ResetDetails();
					}
					if (PlayerPrefs.HasKey(Singleton<Google_SignIn>.instance.prefFirstTimeGuestUser))
					{
						Google_SignIn.FirstTimeGuestUser = PlayerPrefs.GetInt(Singleton<Google_SignIn>.instance.prefFirstTimeGuestUser, Google_SignIn.FirstTimeGuestUser);
						if (Google_SignIn.FirstTimeGuestUser == 0)
						{
							Google_SignIn.FirstTimeGuestUser = 2;
						}
					}
					PlayerPrefs.SetInt(Singleton<Google_SignIn>.instance.prefFirstTimeGuestUser, Google_SignIn.FirstTimeGuestUser);
					weekNumber_Com = int.Parse(jsonData["GetUID"]["Wnum"].ToString());
					if (weekNumber_Com != weekNumber)
					{
						Singleton<LeaderBoard>.instance.ResetWeeklyLeaderBoard();
						Singleton<LeaderBoard>.instance.ResetWeeklyArcadeLeaderBoard();
						weekNumber = int.Parse(jsonData["GetUID"]["Wnum"].ToString());
						PlayerPrefs.SetInt(weekNumberPref, weekNumber);
						StartCoroutine(User_Leaderboard_Sync());
					}
					PlayerPrefs.Save();
					CONTROLLER.userID = int.Parse(jsonData["GetUID"]["userid"].ToString());
					ObscuredPrefs.SetInt("GetUserId", CONTROLLER.userID);
					ObscuredPrefs.SetInt("GuestUserId", 0);
					ObscuredPrefs.DeleteKey("GuestUserId");
					Google_SignIn.guestUserID = -1;
					CONTROLLER.FreeSpin = int.Parse(jsonData["GetUID"]["SCount"].ToString());
					Singleton<SevenDaysRewards>.instance.SetRemainingDays(int.Parse(jsonData["GetUID"]["DayBonus"].ToString()));
					QuickPlayFreeEntry.Instance.loggedIn = true;
					QuickPlayFreeEntry.Instance.StartDayRewards();
					UIDataHolder.openingFirstTime = false;
					PlayerPrefs.SetInt("userStatus", CONTROLLER.GetUserStatus);
					if (CONTROLLER.GetUserStatus == 1)
					{
						guestUser = false;
						CalledGoogle_User = false;
						Singleton<GameModeTWO>.instance.SigninButton.SetActive(value: false);
						Singleton<Google_SignIn>.instance.Display_UserInfo();
						Google_SignIn.guestUserID = -1;
						if (Google_SignIn.guestUserID == -1 && Singleton<GameModeTWO>.instance != null)
						{
							Singleton<GameModeTWO>.instance.ShowNewUserPopUp();
						}
						if (CONTROLLER.userID != 0)
						{
							Get_User_Sync();
							Get_StoreUpgrades();
							StartCoroutine(Singleton<AchievementsSyncronizer>.instance.RetriveAchievements());
						}
						//FirebaseAnalyticsManager.instance.setUserId(CONTROLLER.userID.ToString());
					}
					else if (CONTROLLER.GetUserStatus == 2)
					{
						guestUser = false;
						CalledGoogle_User = false;
						Singleton<GameModeTWO>.instance.SigninButton.SetActive(value: false);
						Singleton<Google_SignIn>.instance.Display_UserInfo();
						CONTROLLER.tickets = int.Parse(jsonData["GetUID"]["TotalTickets"].ToString());
						CONTROLLER.Coins = int.Parse(jsonData["GetUID"]["TotalCoins"].ToString());
						CONTROLLER.XPs = int.Parse(jsonData["GetUID"]["TotalXP"].ToString());
						CONTROLLER.ArcadeXPs = int.Parse(jsonData["GetUID"]["ArcadeXp"].ToString());
						CONTROLLER.FreeSpin = int.Parse(jsonData["GetUID"]["SCount"].ToString());
						if (Singleton<MultiplayerPage>.instance != null)
						{
							Singleton<MultiplayerPage>.instance.TicketsCount.text = CONTROLLER.tickets.ToString();
						}
						SavePlayerPrefs.SaveUserCoins();
						SavePlayerPrefs.SaveUserTickets();
						SavePlayerPrefs.SaveUserArcadeXP();
						SavePlayerPrefs.SaveUserXP();
						SavePlayerPrefs.SaveSpins(0);
						if (Singleton<GameModeTWO>.instance != null)
						{
							Singleton<GameModeTWO>.instance.ResetDetails();
						}
						if (CONTROLLER.userID != 0)
						{
							Get_User_Sync();
							if (!ObscuredPrefs.HasKey("UpgradesSet"))
							{
								Get_StoreUpgrades();
							}
							StartCoroutine(Singleton<AchievementsSyncronizer>.instance.RetriveAchievements());
						}
						//FirebaseAnalyticsManager.instance.setUserId(CONTROLLER.userID.ToString());
					}
					else
					{
						Singleton<LoadingPanelTransition>.instance.HideMe();
					}
				}
				else
				{
					Singleton<LoadingPanelTransition>.instance.HideMe();
				}
			}
			else
			{
				QuickPlayFreeEntry.Instance.loggedIn = false;
				if (Singleton<SevenDaysRewards>.instance.IsAvailable() == 0)
				{
					QuickPlayFreeEntry.Instance.StartDayRewards();
				}
				Singleton<LoadingPanelTransition>.instance.HideMe();
			}
		}
		else
		{
			QuickPlayFreeEntry.Instance.loggedIn = false;
			if (Singleton<SevenDaysRewards>.instance.IsAvailable() == 0)
			{
				QuickPlayFreeEntry.Instance.StartDayRewards();
			}
			Singleton<LoadingPanelTransition>.instance.HideMe();
		}
		Singleton<LoadingPanelTransition>.instance.HideMe();
	}

	public IEnumerator User_Sync()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (!Network_Connection.isNetworkConnected || !CONTROLLER.isGameDataSecure)
		{
			yield break;
		}
		if (ObscuredPrefs.HasKey("userSpendCoins"))
		{
			CONTROLLER.spendCoins = ObscuredPrefs.GetInt("userSpendCoins");
		}
		if (ObscuredPrefs.HasKey("userEarnedCoins"))
		{
			CONTROLLER.earnedCoins = ObscuredPrefs.GetInt("userEarnedCoins");
		}
		if (ObscuredPrefs.HasKey("userEarnedTickets"))
		{
			CONTROLLER.earnedTickets = ObscuredPrefs.GetInt("userEarnedTickets");
		}
		if (ObscuredPrefs.HasKey("userSpendTickets"))
		{
			CONTROLLER.spendTickets = ObscuredPrefs.GetInt("userSpendTickets");
		}
		Google_Sync_status = "0";
		WWWForm form = new WWWForm();
		form.AddField("action", "SyncPoints");
		if (!guestUser)
		{
			form.AddField("userid", CONTROLLER.userID);
		}
		else if (guestUser)
		{
			form.AddField("userid", Google_SignIn.guestUserID);
		}
		form.AddField("deviceid", GetAppInfo.deviceID);
		StartCoroutine("CheckForTimeOut");
		int keyValue1 = Random.Range(1, 10);
		if (CONTROLLER.spendCoins <= 0)
		{
			CONTROLLER.spendCoins = 0;
		}
		form.AddField("SpendCoin", CONTROLLER.spendCoins);
		form.AddField("EncryptedSC", Encryption.encryptPointsSystem(CONTROLLER.spendCoins, keyValue1));
		form.AddField("EncryptedKeySC", keyValue1);
		int keyValue6 = Random.Range(1, 10);
		if (CONTROLLER.earnedCoins <= 0)
		{
			CONTROLLER.earnedCoins = 0;
		}
		form.AddField("EarnedCoins", CONTROLLER.earnedCoins);
		form.AddField("EncryptedEC", Encryption.encryptPointsSystem(CONTROLLER.earnedCoins, keyValue6));
		form.AddField("EncryptedKeyEC", keyValue6);
		int keyValue8 = Random.Range(1, 10);
		if (CONTROLLER.earnedXPs <= 0)
		{
			CONTROLLER.earnedXPs = 0;
		}
		form.AddField("EarnedXP", CONTROLLER.earnedXPs);
		form.AddField("EncryptedEXP", Encryption.encryptPointsSystem(CONTROLLER.earnedXPs, keyValue8));
		form.AddField("EncryptedKeyEXP", keyValue8);
		int keyValue9 = Random.Range(1, 10);
		if (CONTROLLER.earnedArcade <= 0)
		{
			CONTROLLER.earnedArcade = 0;
		}
		form.AddField("EarnedArcadeXP", CONTROLLER.earnedArcade);
		form.AddField("EncryptedArcadeEXP", Encryption.encryptPointsSystem(CONTROLLER.earnedArcade, keyValue9));
		form.AddField("EncryptedKeyArcadeEXP", keyValue9);
		int keyValue10 = Random.Range(1, 10);
		if (CONTROLLER.spendTickets <= 0)
		{
			CONTROLLER.spendTickets = 0;
		}
		form.AddField("SpendTickets", CONTROLLER.spendTickets);
		form.AddField("EncryptedSTickets", Encryption.encryptPointsSystem(CONTROLLER.spendTickets, keyValue10));
		form.AddField("EncryptedKeySTickets", keyValue10);
		int keyValue11 = Random.Range(1, 10);
		if (CONTROLLER.earnedTickets <= 0)
		{
			CONTROLLER.earnedTickets = 0;
		}
		form.AddField("EarnedTickets", CONTROLLER.earnedTickets);
		form.AddField("EncryptedETickets", Encryption.encryptPointsSystem(CONTROLLER.earnedTickets, keyValue11));
		form.AddField("EncryptedKeyETickets", keyValue11);
		int keyValue7 = Random.Range(1, 10);
		if (CONTROLLER.FreeSpin <= 0)
		{
			CONTROLLER.FreeSpin = 0;
		}
		form.AddField("scount", CONTROLLER.FreeSpin);
		form.AddField("scount_e", Encryption.encryptPointsSystem(CONTROLLER.FreeSpin, keyValue7));
		form.AddField("scount_k", keyValue7);
		int keyValue2 = Random.Range(1, 10);
		form.AddField("p_grade", CONTROLLER.powerGrade);
		form.AddField("p_grade_e", Encryption.encryptPointsSystem(CONTROLLER.powerGrade, keyValue2));
		form.AddField("p_grade_k", keyValue2);
		int keyValue3 = Random.Range(1, 10);
		form.AddField("c_grade", CONTROLLER.controlGrade);
		form.AddField("c_grade_e", Encryption.encryptPointsSystem(CONTROLLER.controlGrade, keyValue3));
		form.AddField("c_grade_k", keyValue3);
		int keyValue4 = Random.Range(1, 10);
		form.AddField("a_grade", CONTROLLER.agilityGrade);
		form.AddField("a_grade_e", Encryption.encryptPointsSystem(CONTROLLER.agilityGrade, keyValue4));
		form.AddField("a_grade_k", keyValue4);
		int keyValue5 = Random.Range(1, 10);
		if (CONTROLLER.sixMeterCount <= 0)
		{
			CONTROLLER.sixMeterCount = 0;
		}
		form.AddField("six", CONTROLLER.sixMeterCount);
		form.AddField("esix", Encryption.encryptPointsSystem(CONTROLLER.sixMeterCount, keyValue5));
		form.AddField("ksix", keyValue5);
		WWW UserSync = new WWW(CONTROLLER.PHP_Server_Link, form);
		yield return UserSync;
		StopCoroutine("CheckForTimeOut");
		if (UserSync.text == null && !(UserSync.text != string.Empty))
		{
			yield break;
		}
		JsonData getUserSyncData = JsonMapper.ToObject(UserSync.text);
		if (getUserSyncData["SyncPoints"] == null || UserSync.error != null)
		{
			yield break;
		}
		Google_Sync_status = getUserSyncData["SyncPoints"]["status"].ToString();
		if (Google_Sync_status == "1")
		{
			Singleton<IAP_Manager>.instance.showRemoveAdsText();
			CONTROLLER.tickets = int.Parse(Encryption.DecryptPointsSystem(string.Empty + getUserSyncData["SyncPoints"]["etickets"].ToString(), int.Parse(getUserSyncData["SyncPoints"]["eticketskey"].ToString())));
			CONTROLLER.Coins = int.Parse(Encryption.DecryptPointsSystem(string.Empty + getUserSyncData["SyncPoints"]["ecoins"].ToString(), int.Parse(getUserSyncData["SyncPoints"]["ecoinskey"].ToString())));
			CONTROLLER.XPs = int.Parse(Encryption.DecryptPointsSystem(string.Empty + getUserSyncData["SyncPoints"]["exp"].ToString(), int.Parse(getUserSyncData["SyncPoints"]["expkey"].ToString())));
			CONTROLLER.ArcadeXPs = int.Parse(Encryption.DecryptPointsSystem(string.Empty + getUserSyncData["SyncPoints"]["earcadexp"].ToString(), int.Parse(getUserSyncData["SyncPoints"]["earcadexpkey"].ToString())));
			CONTROLLER.FreeSpin = int.Parse(getUserSyncData["SyncPoints"]["scount"].ToString());
			CONTROLLER.sixMeterCount = int.Parse(Encryption.DecryptPointsSystem(string.Empty + getUserSyncData["SyncPoints"]["esix"].ToString(), int.Parse(getUserSyncData["SyncPoints"]["ksix"].ToString())));
			SavePlayerPrefs.SetSixesCount();
			SavePlayerPrefs.SaveUserCoins();
			SavePlayerPrefs.SaveUserTickets();
			SavePlayerPrefs.SaveUserArcadeXP();
			SavePlayerPrefs.SaveUserXP();
			SavePlayerPrefs.SaveSpins(0);
			if (Singleton<GameModeTWO>.instance != null)
			{
				Singleton<GameModeTWO>.instance.ResetDetails();
			}
			SavePlayerPrefs.ResetAllDetails();
			if (ManageScene.activeSceneName() == "MainMenu")
			{
				Singleton<AchievementManager>.instance.SetXPMilestoneIndex();
			}
			if (Singleton<Google_SignIn>.instance.signOutButtonClicked)
			{
				yield return Singleton<AchievementsSyncronizer>.instance.SendAchievementsAndKits();
				Singleton<Google_SignIn>.instance.SignoutUserSync();
				Singleton<Google_SignIn>.instance.signOutButtonClicked = false;
			}
			if (CalledGoogle_User && guestUser && CONTROLLER.isGameDataSecure)
			{
				StartCoroutine(User_Registration());
			}
		}
		else if (Google_Sync_status == "0")
		{
			Singleton<LoadingPanelTransition>.instance.HideMe();
		}
	}

	public IEnumerator User_Leaderboard_Sync()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (!Network_Connection.isNetworkConnected || !CONTROLLER.isGameDataSecure)
		{
			yield break;
		}
		WWWForm form = new WWWForm();
		form.AddField("action", "WSync");
		if (!guestUser)
		{
			form.AddField("uid", CONTROLLER.userID);
		}
		else if (guestUser)
		{
			form.AddField("uid", Google_SignIn.guestUserID);
		}
		form.AddField("bv", GetAppInfo.AppVersionCode);
		weekNumber = PlayerPrefs.GetInt(weekNumberPref, weekNumber);
		form.AddField("wnum", weekNumber);
		form.AddField("xp", CONTROLLER.weekly_Xps);
		int keyValue1 = Random.Range(1, 10);
		form.AddField("exp", Encryption.encryptPointsSystem(CONTROLLER.weekly_Xps, keyValue1));
		form.AddField("kxp", keyValue1);
		form.AddField("axp", CONTROLLER.weekly_ArcadeXps);
		int keyValue2 = Random.Range(1, 10);
		form.AddField("eaxp", Encryption.encryptPointsSystem(CONTROLLER.weekly_ArcadeXps, keyValue2));
		form.AddField("kaxp", keyValue2);
		WWW UserLeaderboardSync = new WWW(CONTROLLER.PHP_Server_Link, form);
		yield return UserLeaderboardSync;
		JsonData getLeaderBoardSync = JsonMapper.ToObject(UserLeaderboardSync.text);
		if (getLeaderBoardSync["WSync"] != null && UserLeaderboardSync.error == null)
		{
			if (getLeaderBoardSync["WSync"]["status"].ToString() == "2")
			{
				weekNumber = int.Parse(getLeaderBoardSync["WSync"]["wnum"].ToString());
				Singleton<LeaderBoard>.instance.ResetWeeklyLeaderBoard();
				Singleton<LeaderBoard>.instance.ResetWeeklyArcadeLeaderBoard();
				PlayerPrefs.SetInt(weekNumberPref, weekNumber);
				Get_User_Leaderboard_Sync();
			}
			else if (getLeaderBoardSync["WSync"]["status"].ToString() == "1")
			{
				weekNumber = int.Parse(getLeaderBoardSync["WSync"]["wnum"].ToString());
				Singleton<LeaderBoard>.instance.ResetWeeklyLeaderBoard();
				Singleton<LeaderBoard>.instance.ResetWeeklyArcadeLeaderBoard();
				PlayerPrefs.SetInt(weekNumberPref, weekNumber);
			}
		}
	}

	public IEnumerator User_Leaderboard()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (Network_Connection.isNetworkConnected && CONTROLLER.isGameDataSecure)
		{
			WWWForm form = new WWWForm();
			form.AddField("action", "LeaderBoardXP");
			if (!guestUser)
			{
				form.AddField("uid", CONTROLLER.userID);
			}
			else if (guestUser)
			{
				form.AddField("uid", Google_SignIn.guestUserID);
			}
			form.AddField("bv", GetAppInfo.AppVersionCode);
			WWW UserLeaderboard = new WWW(CONTROLLER.PHP_Server_Link, form);
			yield return UserLeaderboard;
			JsonData getUserLeaderBoard = JsonMapper.ToObject(UserLeaderboard.text);
			if (getUserLeaderBoard["LeaderBoardXP"] != null && UserLeaderboard.error == null && getUserLeaderBoard["LeaderBoardXP"]["status"].ToString() == "1")
			{
				int count = getUserLeaderBoard["LeaderBoardXP"]["XP"]["Leaderboard"].Count;
				for (int i = 0; i < count; i++)
				{
					Singleton<LeaderBoard>.instance.name[i].text = getUserLeaderBoard["LeaderBoardXP"]["XP"]["Leaderboard"][i]["uname"].ToString();
					Singleton<LeaderBoard>.instance.xp[i].text = getUserLeaderBoard["LeaderBoardXP"]["XP"]["Leaderboard"][i]["xp"].ToString();
					if (Singleton<LeaderBoard>.instance.xp[i].text != "-")
					{
						Singleton<LeaderBoard>.instance.rank[i].text = (i + 1).ToString();
					}
				}
				Singleton<LeaderBoard>.instance.myRank.text = getUserLeaderBoard["LeaderBoardXP"]["XP"]["MyDetails"]["rank"].ToString();
				Singleton<LeaderBoard>.instance.myName.text = getUserLeaderBoard["LeaderBoardXP"]["XP"]["MyDetails"]["uname"].ToString();
				Singleton<LeaderBoard>.instance.myXp.text = getUserLeaderBoard["LeaderBoardXP"]["XP"]["MyDetails"]["xp"].ToString();
			}
		}
		Singleton<LoadingPanelTransition>.instance.HideMe();
	}

	public IEnumerator User_ArcadeLeaderboard()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (Network_Connection.isNetworkConnected && CONTROLLER.isGameDataSecure)
		{
			WWWForm form = new WWWForm();
			form.AddField("action", "LeaderBoardArcadeXp");
			if (!guestUser)
			{
				form.AddField("uid", CONTROLLER.userID);
			}
			else if (guestUser)
			{
				form.AddField("uid", Google_SignIn.guestUserID);
			}
			form.AddField("bv", GetAppInfo.AppVersionCode);
			WWW UserArcadeLeaderboard = new WWW(CONTROLLER.PHP_Server_Link, form);
			yield return UserArcadeLeaderboard;
			JsonData getUserArcadeLeaderBoard = JsonMapper.ToObject(UserArcadeLeaderboard.text);
			if (getUserArcadeLeaderBoard["LeaderBoardArcadeXp"] != null && UserArcadeLeaderboard.error == null && getUserArcadeLeaderBoard["LeaderBoardArcadeXp"]["status"].ToString() == "1")
			{
				int count = getUserArcadeLeaderBoard["LeaderBoardArcadeXp"]["ArcadeXp"]["Leaderboard"].Count;
				for (int i = 0; i < count; i++)
				{
					Singleton<LeaderBoard>.instance.arcadeName[i].text = getUserArcadeLeaderBoard["LeaderBoardArcadeXp"]["ArcadeXp"]["Leaderboard"][i]["uname"].ToString();
					Singleton<LeaderBoard>.instance.arcadeXp[i].text = getUserArcadeLeaderBoard["LeaderBoardArcadeXp"]["ArcadeXp"]["Leaderboard"][i]["xp"].ToString();
					if (Singleton<LeaderBoard>.instance.arcadeXp[i].text != "-")
					{
						Singleton<LeaderBoard>.instance.arcadeRank[i].text = (i + 1).ToString();
					}
				}
				Singleton<LeaderBoard>.instance.myArcadeRank.text = getUserArcadeLeaderBoard["LeaderBoardArcadeXp"]["ArcadeXp"]["MyDetails"]["rank"].ToString();
				Singleton<LeaderBoard>.instance.myArcadeName.text = getUserArcadeLeaderBoard["LeaderBoardArcadeXp"]["ArcadeXp"]["MyDetails"]["uname"].ToString();
				Singleton<LeaderBoard>.instance.myArcadeXp.text = getUserArcadeLeaderBoard["LeaderBoardArcadeXp"]["ArcadeXp"]["MyDetails"]["xp"].ToString();
			}
		}
		Singleton<LoadingPanelTransition>.instance.HideMe();
	}

	public IEnumerator User_PushNotification()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (Network_Connection.isNetworkConnected && CONTROLLER.isGameDataSecure)
		{
			WWWForm form = new WWWForm();
			form.AddField("action", "PushNotificationRegister");
			form.AddField("deviceid", GetAppInfo.deviceID);
			form.AddField("buildnum", GetAppInfo.AppVersionCode);
			form.AddField("deviceregistrationid", CONTROLLER.deviceRegistrationID);
			form.AddField("platform", "Android");
			WWW PushNotification = new WWW(CONTROLLER.PHP_Server_Link, form);
			yield return PushNotification;
			JsonData getPushNotification = JsonMapper.ToObject(PushNotification.text);
			if (getPushNotification["PushNotificationRegister"] != null && PushNotification.error == null && getPushNotification["PushNotificationRegister"]["status"].ToString() == "1")
			{
				ispushRegistered = CONTROLLER.AppVersionCode;
				PlayerPrefs.SetInt(prefPushRegistered, ispushRegistered);
				PlayerPrefs.Save();
			}
		}
	}

	public IEnumerator Set_User_StoreUpgrades()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (Network_Connection.isNetworkConnected && CONTROLLER.isGameDataSecure)
		{
			WWWForm form = new WWWForm();
			form.AddField("action", "UpgradeStore");
			if (!guestUser)
			{
				form.AddField("uid", CONTROLLER.userID);
			}
			else if (guestUser)
			{
				form.AddField("uid", Google_SignIn.guestUserID);
			}
			updateUpgrades = "1|" + CONTROLLER.powerGrade + "|" + CONTROLLER.totalPowerSubGrade + "|" + (CONTROLLER.powerUpgradeTimerStarted ? 1 : 0) + "*2|" + CONTROLLER.controlGrade + "|" + CONTROLLER.totalControlSubGrade + "|" + (CONTROLLER.controlUpgradeTimerStarted ? 1 : 0) + "*3|" + CONTROLLER.agilityGrade + "|" + CONTROLLER.totalAgilitySubGrade + "|" + (CONTROLLER.agilityUpgradeTimerStarted ? 1 : 0);
			form.AddField("upgradedata", updateUpgrades);
			WWW _StoreUpgrades = new WWW(CONTROLLER.PHP_Server_Link, form);
			yield return _StoreUpgrades;
			JsonData getStoreUpgrades = JsonMapper.ToObject(_StoreUpgrades.text);
			if (getStoreUpgrades["UpgradeStore"] != null && _StoreUpgrades.error == null && !(getStoreUpgrades["UpgradeStore"]["status"].ToString() == "1"))
			{
			}
		}
	}

	public IEnumerator Get_User_StoreUpgrades()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (Network_Connection.isNetworkConnected && CONTROLLER.isGameDataSecure)
		{
			WWWForm form = new WWWForm();
			form.AddField("action", "GetStore");
			if (!guestUser)
			{
				form.AddField("uid", CONTROLLER.userID);
			}
			else if (guestUser)
			{
				form.AddField("uid", Google_SignIn.guestUserID);
			}
			WWW _setStoreUpgrades = new WWW(CONTROLLER.PHP_Server_Link, form);
			yield return _setStoreUpgrades;
			JsonData setStoreUpgrades = JsonMapper.ToObject(_setStoreUpgrades.text);
			if (setStoreUpgrades["GetStore"] != null && _setStoreUpgrades.error == null && setStoreUpgrades["GetStore"]["status"].ToString() == "1")
			{
				updateUpgrades = setStoreUpgrades["GetStore"]["upgradedata"].ToString();
				upgradesOuterSplit = updateUpgrades.Split('*');
				upgradesPowerSplit = upgradesOuterSplit[0].Split('|');
				upgradesControlSplit = upgradesOuterSplit[1].Split('|');
				upgradesAgilitySplit = upgradesOuterSplit[2].Split('|');
				CONTROLLER.powerGrade = int.Parse(upgradesPowerSplit[1]);
				CONTROLLER.controlGrade = int.Parse(upgradesControlSplit[1]);
				CONTROLLER.agilityGrade = int.Parse(upgradesAgilitySplit[1]);
				CONTROLLER.totalPowerSubGrade = int.Parse(upgradesPowerSplit[2]);
				CONTROLLER.totalControlSubGrade = int.Parse(upgradesControlSplit[2]);
				CONTROLLER.totalAgilitySubGrade = int.Parse(upgradesAgilitySplit[2]);
				CONTROLLER.powerUpgradeTimerStarted = TypeInttoBoolchange(upgradesPowerSplit[3]);
				CONTROLLER.controlUpgradeTimerStarted = TypeInttoBoolchange(upgradesControlSplit[3]);
				CONTROLLER.agilityUpgradeTimerStarted = TypeInttoBoolchange(upgradesAgilitySplit[3]);
				QuickPlayFreeEntry.Instance.Set_UpgradeKeys();
				SavePlayerPrefs.InitialPowerUpDetails();
				SavePlayerPrefs.SetPowerUpDetails();
				Singleton<GameModeTWO>.instance.MainmenuUpgradesUI();
			}
		}
	}

	public IEnumerator Get_User_XPRank()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (!Network_Connection.isNetworkConnected || !CONTROLLER.isGameDataSecure)
		{
			yield break;
		}
		WWWForm form = new WWWForm();
		form.AddField("action", "MyXPRank");
		if (!guestUser)
		{
			form.AddField("uid", CONTROLLER.userID);
		}
		else if (guestUser)
		{
			form.AddField("uid", Google_SignIn.guestUserID);
		}
		WWW _getxprank = new WWW(CONTROLLER.PHP_Server_Link, form);
		yield return _getxprank;
		JsonData getxprank = JsonMapper.ToObject(_getxprank.text);
		if (getxprank["MyXPRank"] == null || _getxprank.error != null)
		{
			yield break;
		}
		if (getxprank["MyXPRank"]["status"].ToString() == "1")
		{
			if (GroundController.QPMatchStart)
			{
				start_PlayerRankXP = int.Parse(getxprank["MyXPRank"]["rank"].ToString());
				yield break;
			}
			end_PlayerRankXP = int.Parse(getxprank["MyXPRank"]["rank"].ToString());
			if (end_PlayerRankXP - start_PlayerRankXP != 0)
			{
				diff_Xp = start_PlayerRankXP - end_PlayerRankXP;
			}
			else
			{
				diff_Xp = 0;
			}
			if (ManageScene.activeSceneName() == "Ground")
			{
				Singleton<GameOverDisplay>.instance.ValidateLeaderboardPosition();
			}
		}
		else
		{
			start_PlayerRankXP = 0;
			end_PlayerRankXP = 0;
		}
	}

	public IEnumerator Get_User_ArcadeXPRank()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (!Network_Connection.isNetworkConnected || !CONTROLLER.isGameDataSecure)
		{
			yield break;
		}
		WWWForm form = new WWWForm();
		form.AddField("action", "MyArcadeXPRank");
		if (!guestUser)
		{
			form.AddField("uid", CONTROLLER.userID);
		}
		else if (guestUser)
		{
			form.AddField("uid", Google_SignIn.guestUserID);
		}
		WWW _getxprank = new WWW(CONTROLLER.PHP_Server_Link, form);
		yield return _getxprank;
		JsonData getxprank = JsonMapper.ToObject(_getxprank.text);
		if (getxprank["MyArcadeXPRank"] == null || _getxprank.error != null)
		{
			yield break;
		}
		if (getxprank["MyArcadeXPRank"]["status"].ToString() == "1")
		{
			if (GroundController.SOMatchStart)
			{
				start_playerRankAXP = int.Parse(getxprank["MyArcadeXPRank"]["rank"].ToString());
				yield break;
			}
			end_playerRankAXP = int.Parse(getxprank["MyArcadeXPRank"]["rank"].ToString());
			if (end_playerRankAXP - start_playerRankAXP != 0)
			{
				diff_AXp = start_playerRankAXP - end_playerRankAXP;
			}
			else
			{
				diff_AXp = 0;
			}
			if (ManageScene.activeSceneName() == "Ground")
			{
				if (CONTROLLER.PlayModeSelected == 4)
				{
					Singleton<SuperOverResult>.instance.ValidateLeaderboardPosition();
				}
				else if (CONTROLLER.PlayModeSelected == 5)
				{
					Singleton<SuperChaseResult>.instance.ValidateLeaderboardPosition();
				}
			}
		}
		else
		{
			start_playerRankAXP = 0;
			end_playerRankAXP = 0;
		}
	}

	public IEnumerator Set_User_UpdateRewards()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (Network_Connection.isNetworkConnected && CONTROLLER.isGameDataSecure)
		{
			WWWForm form = new WWWForm();
			form.AddField("action", "UpdateRewards");
			if (!guestUser)
			{
				form.AddField("uid", CONTROLLER.userID);
			}
			else if (guestUser)
			{
				form.AddField("uid", Google_SignIn.guestUserID);
			}
			form.AddField("rewards", "1|23232323");
			WWW _setupgrades = new WWW(CONTROLLER.PHP_Server_Link, form);
			yield return _setupgrades;
			JsonData setupgrades = JsonMapper.ToObject(_setupgrades.text);
			if (setupgrades["UpdateRewards"] != null && _setupgrades.error == null && !(setupgrades["UpdateRewards"]["status"].ToString() == "1"))
			{
			}
		}
	}

	public IEnumerator Get_User_GetRewards()
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (Network_Connection.isNetworkConnected && CONTROLLER.isGameDataSecure)
		{
			WWWForm form = new WWWForm();
			form.AddField("action", "GetRewards");
			if (!guestUser)
			{
				form.AddField("uid", CONTROLLER.userID);
			}
			else if (guestUser)
			{
				form.AddField("uid", Google_SignIn.guestUserID);
			}
			WWW _getupgrades = new WWW(CONTROLLER.PHP_Server_Link, form);
			yield return _getupgrades;
			JsonData getupgrades = JsonMapper.ToObject(_getupgrades.text);
			if (getupgrades["GetRewards"] != null && _getupgrades.error == null && !(getupgrades["GetRewards"]["status"].ToString() == "1"))
			{
			}
		}
	}

	public bool TypeInttoBoolchange(string i)
	{
		if (i == "1")
		{
			return true;
		}
		return false;
	}

	public void SyncPoints()
	{
		if (CONTROLLER.GetUserStatus == 2)
		{
			if (CONTROLLER.CanRetrieveAchievements)
			{
				StartCoroutine(Singleton<AchievementsSyncronizer>.instance.RetriveAchievements());
				CONTROLLER.CanRetrieveAchievements = false;
			}
			if (CONTROLLER.earnedCoins > 0 || CONTROLLER.spendCoins > 0 || CONTROLLER.earnedXPs > 0 || CONTROLLER.earnedTickets > 0 || CONTROLLER.spendTickets > 0 || CONTROLLER.sixMeterCount > ObscuredPrefs.GetInt("prevSixCount"))
			{
				Get_User_Sync();
			}
			if (CONTROLLER.weekly_Xps > 0 || CONTROLLER.weekly_ArcadeXps > 0)
			{
				Get_User_Leaderboard_Sync();
			}
		}
	}

	public IEnumerator CheckForTimeOut()
	{
		yield return new WaitForSeconds(20f);
		Singleton<LoadingPanelTransition>.instance.HideMe();
	}
}
