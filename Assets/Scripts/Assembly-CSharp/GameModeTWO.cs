using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameModeTWO : Singleton<GameModeTWO>
{
	[Header("GameObject")]
	public GameObject Holder;

	public GameObject newUserPopup;

	public GameObject WCHolder;

	public GameObject PLModes;

	public GameObject SixMeterToolTip;

	public GameObject arcadeMeterProgress;

	public GameObject arcadeMeterCompleted;

	public GameObject content;

	public GameObject SigninButton;

	public GameObject SpinWheelPanel;

	public GameObject betaUserReward;

	[Header("Text")]
	public Text[] timerText;

	public Text arcadeProgress;

	public Text PowerPercent;

	public Text ControlPercent;

	public Text AgilityPercent;

	public Text PowerPercent2;

	public Text ControlPercent2;

	public Text AgilityPercent2;

	public Text[] userXP;

	public Text[] userCoins;

	public Text[] userTickets;

	[Header("Image")]
	public Image[] star;

	public Image arcadeMeterFill;

	public Image PowerFillImage;

	public Image ControlFillImage;

	public Image AgilityFillImage;

	public Image PowerFillImage2;

	public Image ControlFillImage2;

	public Image AgilityFillImage2;

	public Image AchievementSpark1;

	public Image AchievementSpark2;

	public Image AchievementSpark3;

	public Image googleUserImage;

	public Image[] UpgradesImages;

	[Header("Sprite")]
	public Sprite defaultImg;

	[Header("Transform")]
	public Transform giftImg;

	public Transform SpinImg;

	[Header("Variables")]
	public static string insuffStatus;

	private Vector3 startPos;

	public bool ButtonTab1Bool;

	public bool ResetTab1;

	private int ReturnState;

	public int kitValue;

	private int starToAnimate;

	private Tweener tween;

	private bool canOpenToolTip = true;

	private void Awake()
	{
		Singleton<Google_SignIn>.instance.LoadUIComponents();
	}

	protected void Start()
	{
		StartStarAnim();
		AchievementTable.InitAchievementValues();
		KitTable.InitKitValues();
		startPos = content.transform.position;
		ResetTab1 = true;
		ReturnState = 0;
		MainPageAnimations();
		DirectLink();
	}

	private void Update()
	{
		SpinImg.DORotate(new Vector3(0f, 0f, SpinImg.eulerAngles.z + 113f), 5f, RotateMode.FastBeyond360);
		if (content.transform.localPosition.x <= -500f)
		{
			if (ReturnState == 2)
			{
				ReturnState = 1;
			}
			ButtonTab1Bool = true;
		}
		else
		{
			ButtonTab1Bool = false;
			ReturnState = 2;
			ResetTab1 = true;
		}
		if (ButtonTab1Bool && ReturnState == 1 && ResetTab1)
		{
			Singleton<InfoAnim>.instance.loop1();
			Singleton<settingsAnim>.instance.Settingsloop();
			Singleton<Help_Anim>.instance.loop1();
			Singleton<LeaderBoard_Anim>.instance.loop1();
			Singleton<Like_Anim>.instance.loop1();
			Singleton<Follow_Anim>.instance.loop1();
			Singleton<Rate_Anim>.instance.loop1();
			ResetTab1 = false;
		}
	}

	//public void OpenLeaderBoard()
	//{
	//	Singleton<LeaderBoard>.instance.ShowMe();
	//}

	public void closePopup(int index)
	{
		switch (index)
		{
		case 0:
			CONTROLLER.QuitApp();
			break;
		case 1:
			CONTROLLER.CurrentMenu = "landingpage";
			break;
		}
	}

	public void OpenWorldCupModes()
	{
		Singleton<NavigationBack>.instance.deviceBack = CloseWorldCupModes;
		Holder.SetActive(value: false);
		WCHolder.SetActive(value: true);
		if (CONTROLLER.canShowbannerMainmenu == 1)
		{
			//Singleton<AdIntegrate>.instance.ShowAd();
		}
		CONTROLLER.pageName = "WCModes";
	}

	public void CloseWorldCupModes()
	{
		WCHolder.SetActive(value: false);
		showMe();
	}

	public void WatchVideoForExtraSpin()
	{
		//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_FreeSpin" });
		//Singleton<AdIntegrate>.instance.showRewardedVideo(5);
	}

	public void WatchVideoForExtraTicket()
	{
		//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_EntryFee" });
		//Singleton<AdIntegrate>.instance.showRewardedVideo(4);
	}

	public void GetExtraSpin()
	{
		Singleton<Popups>.instance.HideMe();
		if (Singleton<GameModeTWO>.instance.Holder.activeInHierarchy)
		{
			CONTROLLER.pageName = "landingPage";
		}
		else if (Singleton<Store>.instance.Holder.activeInHierarchy)
		{
			CONTROLLER.pageName = "store";
		}
		else
		{
			CONTROLLER.pageName = "store";
		}
		SavePlayerPrefs.SaveSpins(1);
		Server_Connection.instance.Get_User_Sync();
		ResetDetails();
		Singleton<SpinWheel>.instance.UpdateRemainingSpinUI();
		getSpinWheel();
	}

	public void GetExtraTicket()
	{
		CONTROLLER.tickets = 1;
		CONTROLLER.earnedTickets = 1;
		SavePlayerPrefs.SaveUserTickets();
		Singleton<MultiplayerPage>.instance.TicketsCount.text = CONTROLLER.tickets.ToString();
	}

	public void CloseArcadeModes(int index)
	{
		CONTROLLER.pageName = "landingPage";
		if (!Holder.activeInHierarchy)
		{
			showMe();
		}
	}

	public void OpenNoInternetPopup()
	{
		CONTROLLER.PopupName = "noInternet";
		Singleton<Popups>.instance.ShowMe();
	}

	public void CloseNoInternetPopup()
	{
		Singleton<Popups>.instance.HideMe();
	}

	public void PlayGameSound(string SoundType)
	{
		if (CONTROLLER.sndController != null)
		{
			CONTROLLER.sndController.PlayGameSnd(SoundType);
		}
	}

	public void getSpinWheel()
	{
		CONTROLLER.AndroidBackButtonEnable = true;
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	Singleton<SpinWheel>.instance.ShowMe();
		//	return;
		//}
		CONTROLLER.PopupName = "noInternet";
		Singleton<Popups>.instance.ShowMe();
	}

	public void SpinWheelOKButton()
	{
		SpinWheelPanel.SetActive(value: false);
	}

	public void CloseNoSpin()
	{
		Singleton<Popups>.instance.HideMe();
		CONTROLLER.pageName = "landingPage";
	}

	public void getExhibitionState()
	{
		CONTROLLER.matchType = "oneday";
		UIDataHolder.gameMode = 0;
		CONTROLLER.PlayModeSelected = 0;
		WCHolder.SetActive(value: false);
		string empty = string.Empty;
		empty = AutoSave.ReadFile();
		Singleton<GUIRoot>.instance.GetWorldCupTeams("XML/GameMode/WorldCupSeries");
		if (empty == string.Empty)
		{
			CONTROLLER.menuTitle = "TEAM SELECTION";
			hideMe();
			Singleton<EntryFeesAndRewards>.instance.ShowMe();
			displayGameMode(_bool: false);
			updateTitle(_modeSelected: true);
		}
		else
		{
			Singleton<IncompleteMatch>.instance.showMe();
		}
		//FirebaseAnalyticsManager.instance.logEvent("MainMenu_click", "MainMenu", CONTROLLER.userID);
		//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "QP_Clicked" });
	}

	public void getTournamentState()
	{
		CONTROLLER.matchType = "oneday";
		CONTROLLER.PlayModeSelected = 1;
		WCHolder.SetActive(value: false);
		UIDataHolder.gameMode = 1;
		Singleton<GUIRoot>.instance.GetWorldCupTeams("XML/GameMode/WorldCupSeries");
		CONTROLLER.TournamentStage = 0;
		string empty = string.Empty;
		empty = AutoSave.ReadFile();
		if (ObscuredPrefs.HasKey("tour") || empty != string.Empty)
		{
			string text = string.Empty;
			if (ObscuredPrefs.HasKey("tour"))
			{
				text = ObscuredPrefs.GetString("tour");
			}
			string[] array = text.Split("&"[0]);
			CONTROLLER.TournamentStage = int.Parse(array[0]);
			CONTROLLER.myTeamIndex = int.Parse(array[1]);
			CONTROLLER.oversSelectedIndex = int.Parse(array[2]);
			CONTROLLER.matchIndex = int.Parse(array[3]);
			CONTROLLER.quaterFinalList = array[5];
			CONTROLLER.semiFinalList = array[6];
			CONTROLLER.finalList = array[7];
			CONTROLLER.tournamentStr = string.Concat(CONTROLLER.TournamentStage + "&" + CONTROLLER.myTeamIndex + "&" + CONTROLLER.oversSelectedIndex + "&" + CONTROLLER.matchIndex + "&" + array[4], "&", CONTROLLER.quaterFinalList, "&", CONTROLLER.semiFinalList, "&", CONTROLLER.finalList);
			Singleton<GameModeTWO>.instance.hideMe();
			Singleton<FixturesTWO>.instance.showMe();
		}
		else
		{
			CONTROLLER.menuTitle = "TEAM SELECTION";
			hideMe();
			if (ObscuredPrefs.GetInt("T20TeamsSelected") == 0)
			{
				Singleton<EntryFeesAndRewards>.instance.ShowMe();
			}
			else
			{
				Singleton<FixturesTWO>.instance.showMe();
			}
			Singleton<FixturesTWO>.instance.disableReset = true;
			displayGameMode(_bool: false);
			hideHomeBtn(_bool: false);
			updateTitle(_modeSelected: true);
		}
		//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "T20_Clicked" });
		//FirebaseAnalyticsManager.instance.logEvent("MainMenu_click", "MainMenu", CONTROLLER.userID);
	}

	public void getNplState()
	{
		CONTROLLER.matchType = "oneday";
		CONTROLLER.tournamentType = "NPL";
		CONTROLLER.PlayModeSelected = 2;
		UIDataHolder.gameMode = 2;
		Singleton<GUIRoot>.instance.GetNPLIndiaTeams("XML/GameMode/NplIndia");
		CONTROLLER.teamType = "County";
		string empty = string.Empty;
		empty = AutoSave.ReadFile();
		PLModes.SetActive(value: false);
		if (ObscuredPrefs.HasKey("NPLIndiaLeague") || ObscuredPrefs.HasKey("NPLIndiaPlayOff") || empty != string.Empty)
		{
			Singleton<LoadPlayerPrefs>.instance.GetNPLIndiaTournamentList();
			CONTROLLER.NPLIndiaPointsTable = Singleton<LoadPlayerPrefs>.instance.setPointsTable(CONTROLLER.TeamList.Length, CONTROLLER.NPLIndiaPointsTable);
			if (CONTROLLER.NPLIndiaTournamentStage > 0)
			{
				Singleton<NPLIndiaPlayOff>.instance.ShowMe();
			}
			else
			{
				Singleton<NplGroupMatchesTWOPanelTransistion>.instance.ResetTransistion();
				Singleton<NPLIndiaLeague>.instance.ShowMe();
			}
		}
		else
		{
			hideMe();
			CONTROLLER.NPLIndiaPointsTable = Singleton<LoadPlayerPrefs>.instance.setPointsTable(CONTROLLER.TeamList.Length, CONTROLLER.WCPointsTable);
			if (ObscuredPrefs.GetInt("NPLTeamsSelected") == 0)
			{
				Singleton<EntryFeesAndRewards>.instance.ShowMe();
			}
			else
			{
				Singleton<NPLIndiaLeague>.instance.ShowMe();
			}
			Singleton<NPLIndiaLeague>.instance.disableReset = true;
		}
		//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "NPL_Clicked" });
		//FirebaseAnalyticsManager.instance.logEvent("MainMenu_click", "MainMenu", CONTROLLER.userID);
	}

	public void getPakState()
	{
		CONTROLLER.matchType = "oneday";
		CONTROLLER.tournamentType = "PAK";
		CONTROLLER.PlayModeSelected = 2;
		UIDataHolder.gameMode = 2;
		Singleton<GUIRoot>.instance.GetNPLIndiaTeams("XML/GameMode/NplPak");
		CONTROLLER.teamType = "County";
		PLModes.SetActive(value: false);
		string empty = string.Empty;
		empty = AutoSave.ReadFile();
		if (ObscuredPrefs.HasKey("NPLPakistanLeague") || ObscuredPrefs.HasKey("NPLPakistanPlayOff") || empty != string.Empty)
		{
			Singleton<LoadPlayerPrefs>.instance.GetNPLIndiaTournamentList();
			CONTROLLER.NPLIndiaPointsTable = Singleton<LoadPlayerPrefs>.instance.setPointsTable(CONTROLLER.TeamList.Length, CONTROLLER.NPLIndiaPointsTable);
			if (CONTROLLER.NPLIndiaTournamentStage > 0)
			{
				Singleton<NPLIndiaPlayOff>.instance.ShowMe();
			}
			else
			{
				Singleton<NplGroupMatchesTWOPanelTransistion>.instance.ResetTransistion();
				Singleton<NPLIndiaLeague>.instance.ShowMe();
			}
		}
		else
		{
			hideMe();
			CONTROLLER.NPLIndiaPointsTable = Singleton<LoadPlayerPrefs>.instance.setPointsTable(CONTROLLER.TeamList.Length, CONTROLLER.NPLIndiaPointsTable);
			if (ObscuredPrefs.GetInt("PAKTeamsSelected") == 0)
			{
				Singleton<EntryFeesAndRewards>.instance.ShowMe();
			}
			else
			{
				Singleton<NPLIndiaLeague>.instance.ShowMe();
			}
			Singleton<NPLIndiaLeague>.instance.disableReset = true;
		}
		//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "PAK_Clicked" });
		//FirebaseAnalyticsManager.instance.logEvent("MainMenu_click", "MainMenu", CONTROLLER.userID);
	}

	public void getAusState()
	{
		CONTROLLER.matchType = "oneday";
		CONTROLLER.tournamentType = "AUS";
		CONTROLLER.PlayModeSelected = 2;
		UIDataHolder.gameMode = 2;
		Singleton<GUIRoot>.instance.GetNPLIndiaTeams("XML/GameMode/NplAus");
		PLModes.SetActive(value: false);
		string empty = string.Empty;
		empty = AutoSave.ReadFile();
		if (ObscuredPrefs.HasKey("NplAUS") || ObscuredPrefs.HasKey("NplAustraliaPlayoff") || empty != string.Empty)
		{
			Singleton<LoadPlayerPrefs>.instance.GetNPLIndiaTournamentList();
			CONTROLLER.NPLIndiaPointsTable = Singleton<LoadPlayerPrefs>.instance.setPointsTable(CONTROLLER.TeamList.Length, CONTROLLER.NPLIndiaPointsTable);
			if (CONTROLLER.NPLIndiaTournamentStage > 0)
			{
				Singleton<NPLIndiaPlayOff>.instance.ShowMe();
			}
			else
			{
				Singleton<NplGroupMatchesTWOPanelTransistion>.instance.ResetTransistion();
				Singleton<NPLIndiaLeague>.instance.ShowMe();
			}
		}
		else
		{
			hideMe();
			CONTROLLER.NPLIndiaPointsTable = Singleton<LoadPlayerPrefs>.instance.setPointsTable(CONTROLLER.TeamList.Length, CONTROLLER.NPLIndiaPointsTable);
			if (ObscuredPrefs.GetInt("AUSTeamsSelected") == 0)
			{
				Singleton<EntryFeesAndRewards>.instance.ShowMe();
			}
			else
			{
				Singleton<NPLIndiaLeague>.instance.ShowMe();
			}
			Singleton<NPLIndiaLeague>.instance.disableReset = true;
		}
		//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "AUZ_Clicked" });
		//FirebaseAnalyticsManager.instance.logEvent("MainMenu_click", "MainMenu", CONTROLLER.userID);
	}

	public void getWorldCupState()
	{
		CONTROLLER.PlayModeSelected = 3;
		WCHolder.SetActive(value: false);
		UIDataHolder.gameMode = 3;
		CONTROLLER.matchType = "oneday";
		CONTROLLER.teamType = "National";
		Singleton<GUIRoot>.instance.GetWorldCupTeams("XML/GameMode/WorldCupSeries");
		string empty = string.Empty;
		empty = AutoSave.ReadFile();
		if (ObscuredPrefs.HasKey("worldcup") || ObscuredPrefs.HasKey("wcplayoff") || empty != string.Empty)
		{
			Singleton<LoadPlayerPrefs>.instance.GetWCTournamentList();
			CONTROLLER.WCPointsTable = Singleton<LoadPlayerPrefs>.instance.setPointsTable(CONTROLLER.TeamList.Length, CONTROLLER.WCPointsTable);
			if (CONTROLLER.WCTournamentStage > 0)
			{
				Singleton<WorldCupPlayOff>.instance.ShowMe();
			}
			else
			{
				Singleton<WCTeamFixturesTWOPanelTransistion>.instance.ResetTransistion();
				Singleton<WorldCupLeague>.instance.ShowMe();
				Singleton<WCTeamFixturesTWOPanelTransistion>.instance.PanelTransistion();
			}
			Singleton<GameModeTWO>.instance.hideMe();
		}
		else
		{
			hideMe();
			CONTROLLER.WCPointsTable = Singleton<LoadPlayerPrefs>.instance.setPointsTable(CONTROLLER.TeamList.Length, CONTROLLER.WCPointsTable);
			if (ObscuredPrefs.GetInt("WCTeamsSelected") == 0)
			{
				Singleton<EntryFeesAndRewards>.instance.ShowMe();
			}
			else
			{
				Singleton<WorldCupLeague>.instance.ShowMe();
			}
			Singleton<WorldCupLeague>.instance.disableReset = true;
		}
		//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "WC_Clicked" });
		//FirebaseAnalyticsManager.instance.logEvent("MainMenu_click", "MainMenu", CONTROLLER.userID);
	}

	public void GetTestMatchState()
	{
		CONTROLLER.PlayModeSelected = 7;
		if (PlayerPrefs.HasKey("TestMatchReset"))
		{
			AutoSave.DeleteFile();
			PlayerPrefs.DeleteKey("TestMatchReset");
		}
		CONTROLLER.teamType = "TestMatch";
		CONTROLLER.matchType = "test";
		_resetTestCricketData();
		XMLReader.assignNow();
		string empty = string.Empty;
		empty = AutoSave.ReadFile();
		Singleton<GUIRoot>.instance.GetWorldCupTeams("XML/GameMode/WorldCupSeries");
		if (empty != string.Empty)
		{
			Singleton<IncompleteMatch>.instance.showMe();
		}
		else
		{
			CONTROLLER.GameStartsFromSave = false;
			Singleton<EntryFeesAndRewards>.instance.ShowMe();
			hideMe();
		}
		//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "TC_Clicked" });
		//FirebaseAnalyticsManager.instance.logEvent("MainMenu_click", "MainMenu", CONTROLLER.userID);
	}

	private void _resetTestCricketData()
	{
		CONTROLLER.TMcurrentInnings = 0;
		CONTROLLER.currentSession = 0;
		CONTROLLER.ballsBowledPerDay = 0;
		CONTROLLER.followOnTarget = 1;
		CONTROLLER.currentDay = 1;
		CONTROLLER.isSkipBallsForSession = false;
		CONTROLLER.isFollowOn = false;
		CONTROLLER.TestSeriesData = string.Empty;
		CONTROLLER.StoredTestSeriesResult = string.Empty;
		CONTROLLER.TestTeamWonIndexStr = string.Empty;
		CONTROLLER.CurrentTestMatch = 0;
		CONTROLLER.TotalTestMatches = 1;
		CONTROLLER.MyTestWinCount = 0;
		CONTROLLER.OppTestWinCount = 0;
	}

	//public void OpenUpgrades()
	//{
	//	if (Singleton<AdIntegrate>.instance.CheckForInternet())
	//	{
	//		//if (Singleton<Google_SignIn>.instance.googleAuthenticated == 1)
	//		//{
	//			hideMe();
	//			Singleton<AdIntegrate>.instance.HideAd();
	//			CONTROLLER.pageName = "store";
	//			Singleton<Store>.instance.ShowMe();
	//			Singleton<Store>.instance.PerformanceUpgradeButtonClicked();
	//			Singleton<NavigationBack>.instance.deviceBack = Singleton<Store>.instance.HideMe;
	//		//}
	//		//else
	//		//{
	//		//	CONTROLLER.PopupName = "googlesignin";
	//		//	Singleton<Popups>.instance.ShowMe();
	//		//}
	//	}
	//	else
	//	{
	//		OpenNoInternetPopup();
	//	}
	//}

	//public void OpenUpgradeFromMainMenu(int index)
	//{
	//	if (Singleton<AdIntegrate>.instance.CheckForInternet())
	//	{
	//		//if (Singleton<Google_SignIn>.instance.googleAuthenticated == 1)
	//		//{
	//			hideMe();
	//			Singleton<AdIntegrate>.instance.HideAd();
	//			CONTROLLER.pageName = "store";
	//			Singleton<PowerUps>.instance.packIndex = index;
	//			Invoke("HoverAnimation", 1f);
	//			Singleton<Store>.instance.ShowMe();
	//			Singleton<Store>.instance.PerformanceUpgradeButtonClicked();
	//			Singleton<NavigationBack>.instance.deviceBack = Singleton<Store>.instance.HideMe;
	//		//}
	//		//else
	//		//{
	//		//	CONTROLLER.PopupName = "googlesignin";
	//		//	Singleton<Popups>.instance.ShowMe();
	//		//}
	//	}
	//	else
	//	{
	//		OpenNoInternetPopup();
	//	}
	//}

	private void HoverAnimation()
	{
		Singleton<PowerUps>.instance.HoverAnimation();
	}

	public void getSuperOverState(string mode = "bat")
	{
		CONTROLLER.matchType = "oneday";
		CONTROLLER.SuperOverMode = mode;
		CONTROLLER.PlayModeSelected = 4;
		UIDataHolder.gameMode = 4;
		CONTROLLER.teamType = "County";
		Singleton<GUIRoot>.instance.GetWorldCupTeams("XML/GameMode/WorldCupSeries");
		string empty = string.Empty;
		empty = AutoSave.ReadFile();
		Singleton<LoadPlayerPrefs>.instance.GetSuperOverLevelDetails();
		if (empty == string.Empty)
		{
			CONTROLLER.GameStartsFromSave = false;
			Holder.SetActive(value: false);
			Singleton<TeamSelectionArcadePanelTransition>.instance.panelTransition();
			Singleton<TeamSelectionTWO>.instance.showMe();
		}
		else
		{
			Singleton<IncompleteMatch>.instance.showMe();
		}
		//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "SO_Clicked" });
		//FirebaseAnalyticsManager.instance.logEvent("MainMenu_click", "MainMenu", CONTROLLER.userID);
	}

	//public void UserInfoClicked()
	//{
	//	if (Singleton<AdIntegrate>.instance.CheckForInternet())
	//	{
	//		if (Singleton<Google_SignIn>.instance.googleAuthenticated == 0)
	//		{
	//			SigninButton.SetActive(value: true);
	//			Singleton<Google_SignIn>.instance.UI_signInWithMarshMallow();
	//			return;
	//		}
	//		SigninButton.SetActive(value: false);
	//		hideMe();
	//		CONTROLLER.pageName = "userinfo";
	//		Singleton<userInfo>.instance.ShowMe();
	//	}
	//	else
	//	{
	//		OpenNoInternetPopup();
	//	}
	//}

	public void ResumeSuperOverSavedGame()
	{
		CONTROLLER.GameStartsFromSave = true;
		AutoSave.LoadGame();
		CONTROLLER.SceneIsLoading = true;
		Singleton<GameModeTWO>.instance.hideMe();
		CONTROLLER.CurrentMenu = string.Empty;
		Singleton<NavigationBack>.instance.deviceBack = null;
		Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
	}

	public void getSuperChaseState()
	{
		CONTROLLER.matchType = "oneday";
		CONTROLLER.PlayModeSelected = 5;
		UIDataHolder.gameMode = 5;
		CONTROLLER.teamType = "County";
		Singleton<GUIRoot>.instance.GetWorldCupTeams("XML/GameMode/WorldCupSeries");
		string empty = string.Empty;
		empty = AutoSave.ReadFile();
		if (empty == string.Empty)
		{
			CONTROLLER.GameStartsFromSave = false;
			Holder.SetActive(value: false);
			Singleton<TeamSelectionArcadePanelTransition>.instance.panelTransition();
			Singleton<TeamSelectionTWO>.instance.showMe();
		}
		else
		{
			Singleton<IncompleteMatch>.instance.showMe();
		}
		//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "SC_Clicked" });
		//FirebaseAnalyticsManager.instance.logEvent("MainMenu_click", "MainMenu", CONTROLLER.userID);
	}

	//public void TopPanelClicked(int index)
	//{
	//	if (Singleton<AdIntegrate>.instance.CheckForInternet())
	//	{
	//		Singleton<NavigationBack>.instance.deviceBack = showMe;
	//		if (!Singleton<Store>.instance.Holder.activeInHierarchy)
	//		{
	//			Singleton<Store>.instance.ShowMe();
	//		}
	//		switch (index)
	//		{
	//		case 1:
	//			Singleton<Store>.instance.TicketButtonClicked();
	//			Singleton<Store>.instance.SelectedIndex = 2;
	//			Holder.SetActive(value: false);
	//			break;
	//		case 2:
	//			Singleton<Store>.instance.TokenButtonClicked();
	//			Singleton<Store>.instance.SelectedIndex = 1;
	//			Holder.SetActive(value: false);
	//			break;
	//		case 3:
	//			OpenStore();
	//			Singleton<StorePanelTransition>.instance.panelTransition();
	//			break;
	//		}
	//	}
	//	else
	//	{
	//		CONTROLLER.PopupName = "noInternet";
	//		Singleton<Popups>.instance.ShowMe();
	//	}
	//}

	public void NewUserClaimPopup()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		newUserPopup.SetActive(value: false);
		CONTROLLER.newUser = false;
		ObscuredPrefs.SetInt("newUser", 1);
		CONTROLLER.pageName = "landingPage";
	}

	public void GoToStorePage()
	{
		//Singleton<AdIntegrate>.instance.HideAd();
		Singleton<NPLIndiaLeague>.instance.holder.SetActive(value: false);
		Singleton<SOLevelSelectionPage>.instance.holder.SetActive(value: false);
		Singleton<CTLevelSelectionPage>.instance.holder.SetActive(value: false);
		Singleton<WorldCupLeague>.instance.holder.SetActive(value: false);
		Singleton<FixturesTWO>.instance.Holder.SetActive(value: false);
		Singleton<EntryFeesAndRewards>.instance.Holder.SetActive(value: false);
		Singleton<EntryFeesAndRewards>.instance.TMHolder.SetActive(value: false);
		if (!Singleton<Store>.instance.Holder.activeInHierarchy)
		{
			Singleton<Store>.instance.ShowMe();
		}
		if (insuffStatus == "tickets")
		{
			Singleton<Store>.instance.TicketButtonClicked();
			Holder.SetActive(value: false);
		}
		else
		{
			Singleton<Popups>.instance.HideMe();
			Singleton<Store>.instance.CoinButtonClicked();
		}
	}

	public void CloseInsufficientPopup()
	{
		Singleton<Popups>.instance.HideMe();
		CONTROLLER.pageName = "store";
	}

	//public void GoToTicketsStore()
	//{
	//	if (Singleton<AdIntegrate>.instance.CheckForInternet())
	//	{
	//		OpenStore();
	//		Singleton<Store>.instance.TicketButtonClicked();
	//	}
	//	else
	//	{
	//		CONTROLLER.PopupName = "noInternet";
	//		Singleton<Popups>.instance.ShowMe();
	//	}
	//}

	//public void GoToCoinsStore()
	//{
	//	if (Singleton<AdIntegrate>.instance.CheckForInternet())
	//	{
	//		OpenStore();
	//		return;
	//	}
	//	CONTROLLER.PopupName = "noInternet";
	//	Singleton<Popups>.instance.ShowMe();
	//}

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
				Server_Connection.instance.Get_User_Sync();
				ObscuredPrefs.GetInt("prevSixCount", CONTROLLER.sixMeterCount);
			}
			if (CONTROLLER.weekly_Xps > 0 || CONTROLLER.weekly_ArcadeXps > 0)
			{
				Server_Connection.instance.Get_User_Leaderboard_Sync();
			}
		}
		ResetDetails();
	}

	public void hideModes()
	{
		displayGameMode(_bool: false);
		hideHomeBtn(_bool: false);
	}

	public void OpenStore()
	{
		if (Server_Connection.instance.guestUser)
		{
			UpgradeUI_Reset();
		}
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	Singleton<NavigationBack>.instance.deviceBack = showMe;
		//	Singleton<AdIntegrate>.instance.HideAd();
		//	CONTROLLER.pageName = "store";
		//	Singleton<Store>.instance.ShowMe();
		//	Holder.SetActive(value: false);
		//	Singleton<StorePanelTransition>.instance.panelTransition();
		//}
		//else
		//{
		//	Singleton<GameModeTWO>.instance.OpenNoInternetPopup();
		//}
	}

	public void OpenAbout()
	{
		//Singleton<About>.instance.ShowMe();
		Holder.SetActive(value: false);
		if (CONTROLLER.canShowbannerMainmenu == 1)
		{
			//Singleton<AdIntegrate>.instance.ShowAd();
		}
	}

	public void ClaimTickets()
	{
		ObscuredPrefs.SetInt("ArcadeSixes", 0);
		SavePlayerPrefs.SaveSixCount();
		SavePlayerPrefs.SaveUserTickets(3, 0, 3);
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	Server_Connection.instance.SyncPoints();
		//}
		ResetDetails();
	}

	public void OpenAchievements()
	{
		int num = 1;
		if (Singleton<Google_SignIn>.instance.googleAuthenticated == num)
		{
			Singleton<AchievementsPanelTransition>.instance.PanelTransition();
			Singleton<AchievementManager>.instance.ShowMe();
			Holder.SetActive(value: false);
		}
		else
		{
			CONTROLLER.PopupName = "googlesignin";
			Singleton<Popups>.instance.ShowMe();
		}
	}

	public void ValidateArcadeMeter()
	{
		if (ObscuredPrefs.HasKey("ArcadeSixes"))
		{
			if (ObscuredPrefs.GetInt("ArcadeSixes") >= 30)
			{
				arcadeMeterCompleted.SetActive(value: true);
				arcadeMeterProgress.SetActive(value: false);
				return;
			}
			arcadeMeterCompleted.SetActive(value: false);
			arcadeMeterProgress.SetActive(value: true);
			arcadeMeterFill.fillAmount = (float)ObscuredPrefs.GetInt("ArcadeSixes") / 30f;
			arcadeProgress.text = ObscuredPrefs.GetInt("ArcadeSixes") + "/30";
		}
		else
		{
			ObscuredPrefs.SetInt("ArcadeSixes", 0);
			arcadeMeterFill.fillAmount = (float)ObscuredPrefs.GetInt("ArcadeSixes") / 30f;
			arcadeProgress.text = ObscuredPrefs.GetInt("ArcadeSixes") + "/30";
		}
	}

	public void MainmenuUpgradesUI()
	{
		int num = Mathf.RoundToInt(CONTROLLER.totalPowerSubGrade / 13);
		int num2 = Mathf.RoundToInt(CONTROLLER.totalControlSubGrade / 13);
		int num3 = Mathf.RoundToInt(CONTROLLER.totalAgilitySubGrade / 13);
		if (num > 100)
		{
			num = 100;
		}
		if (num2 > 100)
		{
			num2 = 100;
		}
		if (num3 > 100)
		{
			num3 = 100;
		}
		float num4 = (float)num / 100f;
		float num5 = (float)num2 / 100f;
		float num6 = (float)num3 / 100f;
		Sequence s = DOTween.Sequence();
		s.Append(PowerFillImage.DOFillAmount(num4, 2f));
		s.Insert(0f, ControlFillImage.DOFillAmount(num5, 2f));
		s.Insert(0f, AgilityFillImage.DOFillAmount(num6, 2f));
		PowerFillImage2.fillAmount = num4;
		ControlFillImage2.fillAmount = num5;
		AgilityFillImage2.fillAmount = num6;
		PowerPercent.text = num + "%";
		ControlPercent.text = num2 + "%";
		AgilityPercent.text = num3 + "%";
		PowerPercent2.text = num + "%";
		ControlPercent2.text = num2 + "%";
		AgilityPercent2.text = num3 + "%";
	}

	public void OpenQuitGamePopup()
	{
		CONTROLLER.PopupName = "exitPopup";
		Singleton<Popups>.instance.ShowMe();
	}

	public void ShowWithoutAnim()
	{
		Singleton<NavigationBack>.instance.deviceBack = OpenQuitGamePopup;
		//Singleton<AdIntegrate>.instance.HideAd();
		Holder.SetActive(value: true);
	}

	private void MainPageAnimations()
	{
		giftImg.DOScale(Vector3.one * 1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(AchievementSpark1.DOFade(1f, 0.25f));
		sequence.Insert(0.25f, AchievementSpark1.DOFade(0f, 1f));
		sequence.Insert(2.25f, AchievementSpark3.DOFade(1f, 1f));
		sequence.Insert(4.5f, AchievementSpark3.DOFade(0f, 1f));
		sequence.Insert(6.5f, AchievementSpark2.DOFade(1f, 1f));
		sequence.Insert(8.25f, AchievementSpark2.DOFade(0f, 1f));
		sequence.Insert(10f, AchievementSpark2.DOFade(0f, 0f));
		sequence.SetLoops(-1, LoopType.Yoyo);
		Sequence s = DOTween.Sequence();
		for (int i = 0; i < UpgradesImages.Length; i++)
		{
			s.Insert(0f, UpgradesImages[i].gameObject.transform.DOPunchScale(Vector3.zero, 0f, 0, 0f));
			s.Insert(0.5f + (float)i * 0.25f, UpgradesImages[i].gameObject.transform.DOPunchScale(Vector3.one * 0.5f, 1f, 3, 0.1f));
		}
	}

	public void showMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = OpenQuitGamePopup;
		//SignOut_Button_Set();
		//Singleton<AdIntegrate>.instance.HideAd();

		//For Try
		FindObjectOfType<TeamSelectionTWO>().Holder.SetActive(false);

		CONTROLLER.pageName = "landingPage";
		CONTROLLER.screenToDisplay = "landingPage";
		//if (ObscuredPrefs.GetInt("newUser") == 0)
		//{
		//	CONTROLLER.newUser = true;
		//}
		//else
		//{
		//	CONTROLLER.newUser = false;
		//}
		ValidateArcadeMeter();
		Singleton<FreeRewards>.instance.holder.SetActive(value: false);
		//if (Singleton<AdIntegrate>.instance.AdInt == 1)
		//{
		//	Singleton<AdIntegrate>.instance.HideAd();
		//}
		Singleton<Store>.instance.Holder.SetActive(value: false);
		//Singleton<IAP_Manager>.instance.AssignPricevalue();
		ResetScroll();
		ResetDetails();
		Holder.SetActive(value: true);
		Singleton<GameModeTWOPanelTransition>.instance.PanelTransition();
		MainmenuUpgradesUI();
		if (Singleton<CheckinRewardManager>.instance.showMe)
		{
			//Debug.LogError("From");
			Singleton<DailyRewardManager>.instance.GetDailyRewardsDay();
			Singleton<CheckinRewardManager>.instance.ShowMe();
			Singleton<CheckinRewardManager>.instance.HighlightTodaysReward(Singleton<DailyRewardManager>.instance.day);
		}
		else if (QuickPlayFreeEntry.Instance.showSpinPopup)
		{
			Singleton<DailySpin>.instance.ShowMe();
			QuickPlayFreeEntry.Instance.showSpinPopup = false;
		}
		QuickPlayFreeEntry.Instance.StartDayRewards();
	}

	public void ShowNewUserPopUp()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = NewUserClaimPopup;
		CONTROLLER.pageName = "newUser";
		CONTROLLER.newUser = false;
		newUserPopup.SetActive(value: true);
		SavePlayerPrefs.SaveUserCoins(5000, 0, 5000);
		SavePlayerPrefs.SaveUserTickets(5, 0, 5);
	}

	public void ResetDetails()
	{
		for (int i = 0; i < 2; i++)
		{
			userTickets[i].text = ObscuredPrefs.GetInt(CONTROLLER.TicketsKey).ToString();
			userXP[i].text = ObscuredPrefs.GetInt(CONTROLLER.XPKey).ToString();
			userCoins[i].text = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey).ToString();
		}
		arcadeMeterProgress.SetActive(value: true);
		arcadeMeterFill.fillAmount = (float)ObscuredPrefs.GetInt("ArcadeSixes") / 30f;
		arcadeProgress.text = ObscuredPrefs.GetInt("ArcadeSixes") + "/30";
		ValidateArcadeMeter();
	}

	public void ResetDetailstoZero()
	{
		for (int i = 0; i < 2; i++)
		{
			userTickets[i].text = "0".ToString();
			userXP[i].text = "0".ToString();
			userCoins[i].text = "0".ToString();
		}
		arcadeMeterProgress.SetActive(value: true);
		arcadeMeterFill.fillAmount = (float)ObscuredPrefs.GetInt("ArcadeSixes") / 30f;
		arcadeProgress.text = ObscuredPrefs.GetInt("ArcadeSixes") + "/30";
	}

	public void hideMe()
	{
		if (CONTROLLER.canShowbannerMainmenu == 1)
		{
			//Singleton<AdIntegrate>.instance.ShowAd();
		}
		CONTROLLER.mainMenuHidden = true;
		Holder.SetActive(value: false);
		Singleton<GameModeTWOPanelTransition>.instance.resetTransition();
	}

	public void utilSelected(int index)
	{
		switch (index)
		{
		case 0:
			CONTROLLER.menuTitle = "CONTROLS";
			Singleton<SettingsPageTWO>.instance.hideMe();
			Singleton<ControlsPageTWO>.instance.showMe();
			displayGameMode(_bool: false);
			break;
		case 1:
			CONTROLLER.menuTitle = "SETTINGS";
			Singleton<ControlsPageTWO>.instance.hideMe();
			Singleton<SettingsPageTWO>.instance.showMe();
			displayGameMode(_bool: false);
			break;
		case 2:
			CONTROLLER.menuTitle = "ABOUT";
			Singleton<SettingsPageTWO>.instance.hideMe();
			Singleton<ControlsPageTWO>.instance.hideMe();
			displayGameMode(_bool: false);
			break;
		case 3:
			Singleton<TeamSelectionTWO>.instance.hideMe();
			Singleton<TossPageTWO>.instance.hideMe();
			Singleton<SquadPageTWO>.instance.hideMe();
			Singleton<EditPlayersTWO>.instance.hideMe();
			Singleton<FixturesTWO>.instance.hideMe();
			CONTROLLER.menuTitle = string.Empty;
			Singleton<GameModeTWO>.instance.showMe();
			displayGameMode(_bool: true);
			break;
		}
		updateTitle(_modeSelected: false);
	}

	private void PushNotiCal()
	{
		if (CONTROLLER.PushScreenNumber == 0)
		{
			showMe();
		}
		else if (CONTROLLER.PushScreenNumber == 1)
		{
			getExhibitionState();
		}
		else if (CONTROLLER.PushScreenNumber == 2)
		{
			getTournamentState();
		}
		else if (CONTROLLER.PushScreenNumber == 3)
		{
			getWorldCupState();
		}
		else if (CONTROLLER.PushScreenNumber == 4)
		{
			getSuperOverState();
		}
		else if (CONTROLLER.PushScreenNumber == 5)
		{
			getSuperChaseState();
		}
		else if (CONTROLLER.PushScreenNumber == 6)
		{
			//if (Singleton<AdIntegrate>.instance.CheckForInternet())
			//{
			//	InterfaceHandler._instance.multiplayerButtonClickEvent();
			//}
			//else
			//{
			//	showMe();
			//}
		}
		else if (CONTROLLER.PushScreenNumber == 7)
		{
			OpenPremierLeagueModes();
		}
		else if (CONTROLLER.PushScreenNumber == 8)
		{
			//if (Singleton<AdIntegrate>.instance.CheckForInternet())
			//{
			//	if (!Singleton<IAP_Manager>.instance.IsInitialized())
			//	{
			//		Singleton<IAP_Manager>.instance.InitializePurchasing();
			//	}
			//	OpenStore();
			//	Singleton<Store>.instance.CoinButtonClicked();
			//}
			//else
			//{
				showMe();
			//}
		}
		else if (CONTROLLER.PushScreenNumber == 9)
		{
			//if (Singleton<AdIntegrate>.instance.CheckForInternet())
			//{
			//	OpenStore();
			//	Singleton<Store>.instance.TicketButtonClicked();
			//}
			//else
			//{
			//	showMe();
			//}
		}
		else if (CONTROLLER.PushScreenNumber == 10)
		{
			//if (Singleton<AdIntegrate>.instance.CheckForInternet())
			//{
			//	OpenStore();
			//	Singleton<Store>.instance.PerformanceUpgradeButtonClicked();
			//}
			//else
			//{
			//	showMe();
			//}
		}
		else if (CONTROLLER.PushScreenNumber == 11)
		{
			//if (Singleton<AdIntegrate>.instance.CheckForInternet())
			//{
			//	OpenStore();
			//}
			//else
			//{
			//	showMe();
			//}
		}
		else if (CONTROLLER.PushScreenNumber == 12)
		{
			//if (Singleton<AdIntegrate>.instance.CheckForInternet() && Singleton<Google_SignIn>.instance.googleAuthenticated == 1)
			//{
			//	OpenAchievements();
			//}
			//else
			//{
			//	showMe();
			//}
		}
		else if (CONTROLLER.PushScreenNumber == 13)
		{
			//if (Singleton<AdIntegrate>.instance.CheckForInternet() && Singleton<Google_SignIn>.instance.googleAuthenticated == 1)
			//{
			//	Singleton<LeaderBoard>.instance.ShowMe();
			//}
			//else
			//{
			//	showMe();
			//}
		}
		else if (CONTROLLER.PushScreenNumber == 14)
		{
			//if (Singleton<AdIntegrate>.instance.CheckForInternet())
			//{
			//	getSpinWheel();
			//}
			//else
			//{
			//	showMe();
			//}
		}
		else if (CONTROLLER.PushScreenNumber == 15)
		{
			getTournamentState();
		}
		else if (CONTROLLER.PushScreenNumber == 16)
		{
			getWorldCupState();
		}
		else if (CONTROLLER.PushScreenNumber == 17)
		{
			getWorldCupState();
		}
		else if (CONTROLLER.PushScreenNumber == 18)
		{
			GetTestMatchState();
		}
		CONTROLLER.pushNotiClicked = false;
		CONTROLLER.PushScreenNumber = 0;
	}

	public void DirectLink()
	{
		if (CONTROLLER.pushNotiClicked)
		{
			Invoke("PushNotiCal", 0.5f);
			return;
		}
		Singleton<NavigationBack>.instance.disableDeviceBack = false;
		if (CONTROLLER.screenToDisplay == "landingPage")
		{
			showMe();
		}
		else if (CONTROLLER.screenToDisplay == "fixtures")
		{
			Singleton<FixturesTWO>.instance.showMe();
		}
		else if (CONTROLLER.screenToDisplay == "NPLLeague")
		{
			Singleton<NplGroupMatchesTWOPanelTransistion>.instance.ResetTransistion();
			Singleton<NPLIndiaLeague>.instance.ShowMe();
		}
		else if (CONTROLLER.screenToDisplay == "NPLfixtures")
		{
			Singleton<NPLIndiaPlayOff>.instance.ShowMe();
		}
		else if (CONTROLLER.screenToDisplay == "WCLeague")
		{
			Singleton<WorldCupLeague>.instance.ShowMe();
			Singleton<WCTeamFixturesTWOPanelTransistion>.instance.ResetPosition();
		}
		else if (CONTROLLER.screenToDisplay == "WCfixtures")
		{
			Singleton<WorldCupPlayOff>.instance.ShowMe();
		}
		else if (CONTROLLER.screenToDisplay == "CTMainmenu")
		{
			Singleton<CTMenuScreen>.instance.ShowMe();
		}
		else if (CONTROLLER.screenToDisplay == "CTLevels")
		{
			CONTROLLER.PlayModeSelected = 5;
			Singleton<CTMenuScreen>.instance.LevelSelected(CONTROLLER.CTLevelId);
		}
		else if (CONTROLLER.screenToDisplay == "SOLevels")
		{
			CONTROLLER.PlayModeSelected = 4;
			Singleton<SOLevelSelectionPage>.instance.ShowMe();
		}
		else if (CONTROLLER.screenToDisplay == "quickPlay")
		{
			getExhibitionState();
		}
		else if (CONTROLLER.screenToDisplay == "TossPage")
		{
			Time.timeScale = 1f;
			Singleton<TossPageTWO>.instance.showMeDelay();
		}
		if (!CONTROLLER.fromPreloader)
		{
			SyncPoints();
		}
		if (CONTROLLER.screenToDisplay != "landingPage" && CONTROLLER.canShowbannerMainmenu == 1)
		{
			//Singleton<AdIntegrate>.instance.ShowAd();
		}
	}

	private void resetAllUtilBtns()
	{
		CONTROLLER.menuTitle = string.Empty;
		updateTitle(_modeSelected: false);
	}

	private void ResetScroll()
	{
		content.transform.position = startPos;
	}

	public void hideHomeBtn(bool _bool)
	{
		if (!_bool)
		{
		}
	}

	public void displayGameMode(bool _bool)
	{
		if (_bool)
		{
			CONTROLLER.CurrentMenu = "landingpage";
			resetAllUtilBtns();
		}
	}

	public void updateTitle(bool _modeSelected)
	{
		if (!_modeSelected)
		{
		}
	}

	private void StartStarAnim()
	{
		int num = (starToAnimate = Random.Range(0, 3));
		tween = star[num].DOFade(1f, 1f).OnComplete(StopStarAnim);
	}

	private void StopStarAnim()
	{
		tween = star[starToAnimate].DOFade(0f, 0.6f);
		Invoke("StartStarAnim", Random.Range(2, 6));
	}

	public void CloseExitPopup()
	{
		Singleton<Popups>.instance.HideMe();
		CONTROLLER.pageName = "landingPage";
	}

	public void GameExit()
	{
		Application.Quit();
	}

	public void OpenPremierLeagueModes()
	{
		Singleton<NavigationBack>.instance.deviceBack = ClosePremierLeagueModes;
		PLModes.SetActive(value: true);
		Holder.SetActive(value: false);
		if (CONTROLLER.canShowbannerMainmenu == 1)
		{
			//Singleton<AdIntegrate>.instance.ShowAd();
		}
		CONTROLLER.pageName = "plmodes";
	}

	public void ClosePremierLeagueModes()
	{
		PLModes.SetActive(value: false);
		showMe();
	}

	private void FreeEntry()
	{
	}

	//public void SignOut_Button_Set()
	//{
	//	if (Singleton<Google_SignIn>.instance.googleAuthenticated == 0)
	//	{
	//		SigninButton.SetActive(value: true);
	//	}
	//	else
	//	{
	//		SigninButton.SetActive(value: false);
	//	}
	//}

	public void UpgradeUI_Reset()
	{
		for (int i = 0; i <= 2; i++)
		{
			Singleton<PowerUps>.instance.MaxUpgrade[i].SetActive(value: false);
			Singleton<PowerUps>.instance.beforePayment[i].SetActive(value: true);
		}
	}

	public void OpenSixMeterToolTip()
	{
		if (canOpenToolTip)
		{
			SixMeterToolTip.SetActive(value: true);
			canOpenToolTip = false;
			Invoke("CloseSixMeterToolTip", 3f);
		}
	}

	private void CloseSixMeterToolTip()
	{
		SixMeterToolTip.SetActive(value: false);
		canOpenToolTip = true;
	}

	public void CallFreeRewards()
	{
		//Singleton<FreeRewards>.instance.ShowMe(1);
	}

	public void ClaimBetaUserReward()
	{
		SavePlayerPrefs.SaveUserTickets(100, 0, 100);
		ResetDetails();
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		betaUserReward.SetActive(value: false);
		PlayerPrefs.SetInt("ClaimedBetaReward", 1);
	}
}
