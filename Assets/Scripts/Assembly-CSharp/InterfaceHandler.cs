using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceHandler : MonoBehaviour
{
	public static InterfaceHandler _instance;

	public MultiplayerPage multiplayerMode;

	public LoadingPanelTransition loading;

	public GameObject GameModeSelGO;

	public GameObject MultiplayerUIGO;

	public GameObject LoadingScreenGO;

	public Text LoadingScreenCountdown;

	public GameObject noInternetPopup;

	public GameObject noVideoPopup;

	public GameObject noPlayersFoundPopup;

	public GameObject gameAbandonedPopup;

	public GameObject noCoinsPopup;

	public GameObject duplicatePlayerPopup;

	public GameObject serverDisconnectedPopup;

	public GameObject disableOnlineSyncFailed;

	public GameObject serverMaintenancePopup;

	public GameObject gameUpdatePopup;

	public GameObject gameExitPopup;

	public GameObject roomNotFound;

	public GameObject signinfailed;

	public GameObject gameupdatePopupNoButton;

	public GameObject gameupdatePopupYesButton;

	public Text roomNotFoundErrorText;

	public Text gameAbandonedPopupLabel;

	public Text SignInFailedText;

	public Text gameupdatePopupYesButtonText;

	public Text gameupdatePopupNoButtonText;

	public GameObject popupCanvas;

	public Text gameUpdateTitle;

	public Text gameUpdateContent;

	public bool bForceConnectMultiplayer;

	private float fLoadingCountDownStrtTime;

	private float fLoadingCountDownTotTime = 30f;

	private void Start()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		Screen.sleepTimeout = -2;
		bForceConnectMultiplayer = false;
	}

	private void Awake()
	{
		MultiplayerUIGO.SetActive(value: false);
		GameModeSelGO.SetActive(value: true);
		LoadingScreenGO.SetActive(value: false);
	}

	public void Update()
	{
		if (LoadingScreenCountdown.enabled)
		{
			int num = (int)(fLoadingCountDownTotTime - (Time.time - fLoadingCountDownStrtTime));
			if (num <= 0)
			{
				HideLoadingScreen();
			}
			if (num < 0)
			{
				num = 0;
			}
			if ((float)num < fLoadingCountDownTotTime - 4f)
			{
				LoadingScreenCountdown.text = num.ToString();
			}
		}
	}

	public void multiplayerButtonClickEvent()
	{
		Debug.Log(string.Concat(Application.internetReachability, " ", NetworkReachability.NotReachable, " "));
		if (Application.internetReachability != 0 )
		{
			//FirebaseAnalyticsManager.instance.logEvent("MainMenu_click", "MainMenu", CONTROLLER.userID);
			//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "MP_Clicked" });
			if (UpdateSettings.MultiplayerUpdate == 1)
			{
				ShowGameUpdatePopup();
				return;
			}
			if (UpdateSettings.MultiplayerMaintain == 1)
			{
				ShowServerMaintenancePopup();
				return;
			}
			if (CONTROLLER.username == string.Empty)
			{
				ShowSigninfailedPopup();
				return;
			}
			OpenMultiplayer();
			CONTROLLER.GameStartsFromSave = false;
		}
		else
		{
			_instance.ShowNoInternetPopup();
		}
	}

	public void OpenMultiplayer()
	{
		bForceConnectMultiplayer = false;
		CONTROLLER.PlayModeSelected = 6;
		CONTROLLER.gameMode = string.Empty;
		StartCoroutine(ConnectToMultiplayer());
	}

	private IEnumerator ConnectToMultiplayer()
	{
		ShowLoadingScreen(bCanShowCountdown: true);
		yield return StartCoroutine(NetworkManager.Instance.CheckInternetConnection());
		if (NetworkManager.Instance.IsNetworkConnected)
		{
			ServerManager.Instance.Connect();
			yield break;
		}
		HideLoadingScreen();
		ShowNoInternetPopup();
	}

	public IEnumerator LoadGroundScene()
	{
		//Singleton<AdIntegrate>.instance.HideAd();
		if (Multiplayer.roomType == 2)
		{
			Multiplayer.roomType = -1;
		}
		Singleton<MultiplayerPage>.instance.backButton.SetActive(value: false);
		Singleton<MultiplayerPage>.instance.lobbyPage.SetActive(value: false);
		Singleton<NavigationBack>.instance.deviceBack = null;
		loading.PanelTransition1("Ground");
		yield return 0;
	}

	public void ShowMultiplayerMode()
	{
		if (CONTROLLER.canShowbannerMainmenu == 1)
		{
			//Singleton<AdIntegrate>.instance.ShowAd();
		}
		HideLoadingScreen();
		CONTROLLER.canPressBackBtn = true;
		CONTROLLER.CurrentPage = "multiplayerpage";
		multiplayerMode.ShowMe(isTrue: true);
		GameModeSelGO.SetActive(value: false);
		MultiplayerUIGO.SetActive(value: true);
		Singleton<NavigationBack>.instance.deviceBack = Singleton<MultiplayerPage>.instance.ClickedBack;
		if (PlayerPrefs.HasKey("multiplayerteamlist"))
		{
			string @string = PlayerPrefs.GetString("multiplayerteamlist");
			XMLReader.ParseXML(@string);
		}
		else
		{
			XMLReader.ParseXML(Singleton<LoadPlayerPrefs>.instance.xmlAsset.text);
		}
	}

	public void HideMultiplayerMode()
	{
		CONTROLLER.canPressBackBtn = true;
		CONTROLLER.CurrentPage = "splashpage";
		CONTROLLER.pageName = "landingPage";
		multiplayerMode.ShowMe(isTrue: false);
		MultiplayerUIGO.SetActive(value: false);
		GameModeSelGO.SetActive(value: true);
		Singleton<GameModeTWO>.instance.showMe();
		if (ServerManager.Instance != null)
		{
			ServerManager.Instance.Disconnect();
			ServerManager.Instance.ExitRoom();
		}
		Screen.sleepTimeout = -2;
		CONTROLLER.gameMode = string.Empty;
		CONTROLLER.PlayModeSelected = -1;
	}

	private void Activate_multiplayerUIGO(bool flag)
	{
		MultiplayerUIGO.SetActive(flag);
	}

	private void ShowLoadingGO(bool flag)
	{
		LoadingScreenGO.SetActive(flag);
	}

	public void ShowLoadingScreen(bool bCanShowCountdown = false)
	{
		CONTROLLER.tempCanPressBackBtn = CONTROLLER.canPressBackBtn;
		CONTROLLER.canPressBackBtn = false;
		ShowLoadingGO(flag: true);
		Singleton<NavigationBack>.instance.disableDeviceBack = true;
		CONTROLLER.pageName = "LoadingMultiplayer";
		fLoadingCountDownStrtTime = Time.time;
		if (bCanShowCountdown)
		{
			LoadingScreenCountdown.enabled = true;
			LoadingScreenCountdown.text = string.Empty;
		}
		else
		{
			LoadingScreenCountdown.enabled = false;
		}
	}

	public void HideLoadingScreen()
	{
		Singleton<NavigationBack>.instance.disableDeviceBack = false;
		CONTROLLER.canPressBackBtn = CONTROLLER.tempCanPressBackBtn;
		ShowLoadingGO(flag: false);
		CONTROLLER.pageName = "landingPage";
		LoadingScreenCountdown.enabled = false;
	}

	public void ShowNoInternetPopup()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = HideNoInternetPopup;
		noInternetPopup.SetActive(value: true);
	}

	public void HideNoInternetPopup()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		noInternetPopup.SetActive(value: false);
		if (CONTROLLER.PlayModeSelected == 6)
		{
			HideMultiplayerMode();
			HideNoPlayersFoundPopup();
		}
	}

	public void showNoVideoPopup()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = HideNoVideoPopup;
		_instance.noVideoPopup.SetActive(value: true);
		CONTROLLER.pageName = "noVideoMP";
	}

	public void HideNoVideoPopup()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		_instance.noVideoPopup.SetActive(value: false);
		CONTROLLER.pageName = "mpPage1";
	}

	public void ShowNoPlayersFoundPopup()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = HideNoPlayersFoundPopup;
		if (CONTROLLER.CurrentPage == "InfoPopUp")
		{
			HideInfoPopUp();
		}
		CONTROLLER.pageName = "noplayers";
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		CONTROLLER.canPressBackBtn = true;
		noPlayersFoundPopup.SetActive(value: true);
	}

	public void HideNoPlayersFoundPopup()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		CONTROLLER.CurrentPage = CONTROLLER.tempCurrentPage;
		noPlayersFoundPopup.SetActive(value: false);
		HideMultiplayerMode();
		CONTROLLER.CurrentPage = "splashpage";
	}

	public void ShowRoomNotFoundPopup(string errorText)
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = HideRoomNotFoundPopup;
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		roomNotFound.SetActive(value: true);
		roomNotFoundErrorText.text = errorText;
	}

	public void HideRoomNotFoundPopup()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		CONTROLLER.CurrentPage = CONTROLLER.tempCurrentPage;
		roomNotFound.SetActive(value: false);
	}

	public void ShowNoCoinsPopup()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = HideNoCoinsPopup;
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		CONTROLLER.CurrentPage = " PopupPage";
		noCoinsPopup.SetActive(value: true);
	}

	public void HideNoCoinsPopup()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		CONTROLLER.CurrentPage = CONTROLLER.tempCurrentPage;
		noCoinsPopup.SetActive(value: false);
	}

	public void ShowSigninfailedPopup()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = HideSignInfailedPopup;
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		signinfailed.SetActive(value: true);
		SignInFailedText.text = LocalizationData.instance.getText(656);
	}

	public void GooglSignIn()
	{
		bForceConnectMultiplayer = true;
		Singleton<Google_SignIn>.instance.UI_signInWithMarshMallow();
	}

	public void HideSignInfailedPopup()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		CONTROLLER.CurrentPage = CONTROLLER.tempCurrentPage;
		signinfailed.SetActive(value: false);
		CONTROLLER.CurrentPage = "splashpage";
	}

	public void ShowGameAbandonedPopup(string hostName)
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = HideGameAbandonedPopup;
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		if (ManageScenes._instance.getCurrentLoadedSceneName() == "MainMenu")
		{
			HideLoadingScreen();
		}
		else if (ManageScenes._instance.getCurrentLoadedSceneName() == "Ground")
		{
			HideLoadingScreen();
		}
		gameAbandonedPopupLabel.text = LocalizationData.instance.getText(373);
		gameAbandonedPopupLabel.text = ReplaceText(gameAbandonedPopupLabel.text, hostName);
		gameAbandonedPopup.SetActive(value: true);
	}

	private string ReplaceText(string original, string replace)
	{
		string result = string.Empty;
		Debug.Log(original + " " + replace + " ");
		if (original.Contains("#"))
		{
			result = original.Replace("#", replace);
		}
		return result;
	}

	public void HideGameAbandonedPopup()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		CONTROLLER.CurrentPage = CONTROLLER.tempCurrentPage;
		gameAbandonedPopup.SetActive(value: false);
		HideMultiplayerMode();
		CONTROLLER.CurrentPage = "splashpage";
	}

	public void ShowDuplicatePlayerPopup()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = HideDuplicatePlayerPopup;
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		duplicatePlayerPopup.SetActive(value: true);
	}

	public void HideDuplicatePlayerPopup()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		CONTROLLER.CurrentPage = CONTROLLER.tempCurrentPage;
		duplicatePlayerPopup.SetActive(value: false);
		HideMultiplayerMode();
	}

	public void ShowServerDisconnectedPopup()
	{
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		if (serverDisconnectedPopup != null)
		{
			serverDisconnectedPopup.SetActive(value: true);
		}
	}

	public void HideServerDisconnectedPopup()
	{
		CONTROLLER.CurrentPage = CONTROLLER.tempCurrentPage;
		serverDisconnectedPopup.SetActive(value: false);
		HideNoPlayersFoundPopup();
	}

	public void ShowDisableOnlineSyncPopup()
	{
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
	}

	public void HideDisableOnlineSyncPopup()
	{
	}

	public void ShowDisableOnlineSyncFailedPopup()
	{
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		disableOnlineSyncFailed.SetActive(value: true);
	}

	public void HideDisableOnlineSyncFailedPopup()
	{
		disableOnlineSyncFailed.SetActive(value: false);
	}

	public void ShowInfoPopUp()
	{
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
	}

	public void HideInfoPopUp()
	{
		CONTROLLER.CurrentPage = CONTROLLER.tempCurrentPage;
	}

	public void ShowServerMaintenancePopup()
	{
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		serverMaintenancePopup.SetActive(value: true);
	}

	public void HideServerMaintenancePopup()
	{
		CONTROLLER.CurrentPage = CONTROLLER.tempCurrentPage;
		serverMaintenancePopup.SetActive(value: false);
	}

	public void ShowGameUpdatePopup()
	{
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		gameUpdatePopup.SetActive(value: true);
	}

	public void ShowStoreGameUpdate()
	{
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		Application.OpenURL(AppUpdate.AppUpdateUri);
	}

	public void HideGameUpdatePopup()
	{
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		gameUpdatePopup.SetActive(value: false);
	}

	public void ShowGameExitPopup()
	{
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		gameExitPopup.SetActive(value: true);
	}

	public void HideGameExitPopup()
	{
		CONTROLLER.CurrentPage = CONTROLLER.tempCurrentPage;
		gameExitPopup.SetActive(value: false);
	}

	public void GameExit()
	{
		Application.Quit();
	}

	public void HideAllPopups()
	{
		noInternetPopup.SetActive(value: false);
		noPlayersFoundPopup.SetActive(value: false);
		gameAbandonedPopup.SetActive(value: false);
		noCoinsPopup.SetActive(value: false);
		duplicatePlayerPopup.SetActive(value: false);
		disableOnlineSyncFailed.SetActive(value: false);
		serverMaintenancePopup.SetActive(value: false);
		gameUpdatePopup.SetActive(value: false);
		gameExitPopup.SetActive(value: false);
		roomNotFound.SetActive(value: false);
		signinfailed.SetActive(value: false);
		if (serverDisconnectedPopup.activeSelf)
		{
			serverDisconnectedPopup.SetActive(value: false);
			HideMultiplayerMode();
		}
		CONTROLLER.CurrentPage = CONTROLLER.tempCurrentPage;
	}
}
