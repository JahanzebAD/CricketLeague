using UnityEngine;
using UnityEngine.UI;

public class MultiplayerPage : Singleton<MultiplayerPage>
{
	public GameObject oversPage;

	public GameObject ticketsPage;

	public GameObject roomPage;

	public GameObject createPage;

	public GameObject joinPage;

	public GameObject lobbyPage;

	public GameObject roomIDGO;

	public GameObject backButton;

	public Image backBtn;

	public GameObject continueButton;

	public Toggle[] roomToggle;

	public Toggle[] createToggle;

	public Toggle[] oversToggle;

	public InputField roomIDInput;

	public Text roomIDVal;

	public Text timeToWait;

	public GameObject statusTxtGO;

	public Text statusTxt;

	public Text[] playerNameLabel;

	public Image[] playerNameSprite;

	public int multiplayerPageNumber;

	public GameObject waitingForOtherPlayers;

	public Text TicketsCount;

	public Sprite BackButtonEnable;

	public Sprite BackButtonDiable;

	public Sprite PlayerNameSprite_User;

	public Sprite PlayerNameSprite_other;

	public Button watchVideoButton;

	public Sprite[] btnTexture;

	public void ShareRoom()
	{
		string text = "WCC Lite";
		string text2 = CONTROLLER.username + " wants you to joins his Multiplayer Match. Room ID:" + roomIDVal.text + ". You can download Wcc Lite from " + CONTROLLER.BOC_2_Link;
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent");
		androidJavaObject.Call<AndroidJavaObject>("setAction", new object[1] { androidJavaClass.GetStatic<string>("ACTION_SEND") });
		androidJavaObject.Call<AndroidJavaObject>("setType", new object[1] { "text/plain" });
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
		{
			androidJavaClass.GetStatic<string>("EXTRA_SUBJECT"),
			text
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
		{
			androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
			text2
		});
		AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass2.GetStatic<AndroidJavaObject>("currentActivity");
		@static.Call("startActivity", androidJavaObject);
	}

	public void ShowContinueButton()
	{
		continueButton.SetActive(value: true);
	}

	public void HideContinueButton()
	{
		continueButton.SetActive(value: false);
	}

	public void ShowLobbyForPrivateJoin()
	{
		multiplayerPageNumber += 2;
		ClickedContinue();
	}

	public void ClickedContinue()
	{
		if (Application.internetReachability != 0)
		{
			Singleton<NavigationBack>.instance.deviceBack = ClickedBack;
			CheckToggles();
			CheckPage();
		}
		else
		{
			InterfaceHandler._instance.ShowNoInternetPopup();
		}
	}

	public void ClickedBack()
	{
		if (!CONTROLLER.canPressBackBtn)
		{
			return;
		}
		if (Multiplayer.roomType == 2)
		{
			multiplayerPageNumber = -1;
			ServerManager.Instance.ExitRoom();
			Multiplayer.roomType = -1;
		}
		else if (multiplayerPageNumber == 5)
		{
			multiplayerPageNumber = 0;
			ServerManager.Instance.ExitRoom();
			roomIDInput.text = string.Empty;
		}
		else if (Multiplayer.roomType == 0 && multiplayerPageNumber == 3)
		{
			multiplayerPageNumber -= 3;
		}
		else if (Multiplayer.isHost == 1 && multiplayerPageNumber == 3)
		{
			multiplayerPageNumber -= 2;
		}
		else if (multiplayerPageNumber == 50)
		{
			multiplayerPageNumber = -1;
		}
		else if (multiplayerPageNumber == 0)
		{
			multiplayerPageNumber = 0;
			ServerManager.Instance.ExitRoom();
			InterfaceHandler._instance.HideMultiplayerMode();
		}
		else
		{
			if (multiplayerPageNumber == 2)
			{
				roomIDInput.text = string.Empty;
				InterfaceHandler._instance.HideRoomNotFoundPopup();
				CONTROLLER.CurrentPage = "multiplayerpage";
			}
			multiplayerPageNumber--;
		}
		CheckPage();
	}

	//public void watchVideo()
	//{
	//	if (Singleton<AdIntegrate>.instance.CheckForInternet())
	//	{
	//		//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_FreeTicket" });
	//		Singleton<AdIntegrate>.instance.showRewardedVideo(10);
	//	}
	//	else
	//	{
	//		InterfaceHandler._instance.ShowNoInternetPopup();
	//	}
	//}

	private void ShowToast()
	{
		GameObject original = Resources.Load("Prefabs/Toast") as GameObject;
		GameObject gameObject = Object.Instantiate(original);
		gameObject.name = "Toast";
		gameObject.GetComponent<Toast>().setMessge("No video Available");
	}

	public void ShowWaitingForOtherPlayers()
	{
		waitingForOtherPlayers.SetActive(value: true);
	}

	public void HideWaitingForOtherPlayers()
	{
		waitingForOtherPlayers.SetActive(value: false);
	}

	public void SetRoomIDValue(string roomVal)
	{
		roomIDVal.text = roomVal;
	}

	public void ShowMe(bool isTrue)
	{
		base.gameObject.SetActive(isTrue);
		if (isTrue)
		{
			multiplayerPageNumber = 50;
			TicketsCount.text = CONTROLLER.tickets.ToString();
			CheckPage();
			Screen.sleepTimeout = -1;
		}
	}

	public void UpdatePlayerList()
	{
		for (int i = 0; i < 5; i++)
		{
			playerNameLabel[i].text = Multiplayer.playerList[i].PlayerName;
			if (Multiplayer.playerList[i].PlayerName == CONTROLLER.username)
			{
				playerNameLabel[i].color = Color.black;
				playerNameSprite[i].sprite = PlayerNameSprite_User;
			}
			else
			{
				playerNameLabel[i].color = Color.white;
				playerNameSprite[i].sprite = PlayerNameSprite_other;
			}
		}
		if (multiplayerPageNumber == 4 || multiplayerPageNumber == 5)
		{
			if (Multiplayer.playerCount > 1 && Multiplayer.isHost == 1 && Multiplayer.roomType == 1)
			{
				ShowContinueButton();
			}
			else
			{
				HideContinueButton();
			}
		}
		statusTxt.text = string.Empty + Multiplayer.overs + " " + LocalizationData.instance.getText(532) + ".";
		if (Multiplayer.playerCount == 5)
		{
			BlockBackBtn();
		}
	}

	public void UpdateCountdown(int time)
	{
		timeToWait.text = LocalizationData.instance.getText(365);
		timeToWait.text = ReplaceText(timeToWait.text, time.ToString());
		if (time == 2)
		{
			BlockBackBtn();
		}
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

	private void BlockBackBtn()
	{
		backBtn.sprite = BackButtonDiable;
		backButton.SetActive(value: false);
		CONTROLLER.canPressBackBtn = false;
	}

	public void CheckPage()
	{
		if (multiplayerPageNumber == 50)
		{
			ShowContinueButton();
			ticketsPage.SetActive(value: true);
			CONTROLLER.pageName = "mpPage1";
			roomPage.SetActive(value: false);
			createPage.SetActive(value: false);
			joinPage.SetActive(value: false);
			oversPage.SetActive(value: false);
			lobbyPage.SetActive(value: false);
			CONTROLLER.canPressBackBtn = true;
			backBtn.sprite = BackButtonEnable;
			backButton.SetActive(value: true);
			TicketsCount.text = CONTROLLER.tickets.ToString();
			if (InterfaceHandler._instance.noCoinsPopup.activeSelf)
			{
				CONTROLLER.pageName = "PopupPage";
			}
		}
		else if (multiplayerPageNumber == 0)
		{
			ShowContinueButton();
			ticketsPage.SetActive(value: false);
			roomPage.SetActive(value: true);
			createPage.SetActive(value: false);
			joinPage.SetActive(value: false);
			oversPage.SetActive(value: false);
			lobbyPage.SetActive(value: false);
			CONTROLLER.canPressBackBtn = true;
			backBtn.sprite = BackButtonEnable;
			backButton.SetActive(value: true);
			if (!InterfaceHandler._instance.GameModeSelGO.activeInHierarchy)
			{
				CONTROLLER.CurrentPage = "multiplayerpage";
			}
		}
		else if (multiplayerPageNumber == 1)
		{
			ShowContinueButton();
			ticketsPage.SetActive(value: false);
			roomPage.SetActive(value: false);
			createPage.SetActive(value: true);
			joinPage.SetActive(value: false);
			oversPage.SetActive(value: false);
			lobbyPage.SetActive(value: false);
			CONTROLLER.CurrentPage = "multiplayerpage";
		}
		else if (multiplayerPageNumber == 2)
		{
			ShowContinueButton();
			ticketsPage.SetActive(value: false);
			roomPage.SetActive(value: false);
			createPage.SetActive(value: false);
			joinPage.SetActive(value: true);
			oversPage.SetActive(value: false);
			lobbyPage.SetActive(value: false);
			CONTROLLER.CurrentPage = "multiplayerpage";
		}
		else if (multiplayerPageNumber == 3)
		{
			ShowContinueButton();
			ticketsPage.SetActive(value: false);
			roomPage.SetActive(value: false);
			createPage.SetActive(value: false);
			joinPage.SetActive(value: false);
			oversPage.SetActive(value: true);
			lobbyPage.SetActive(value: false);
			CONTROLLER.CurrentPage = "multiplayerpage";
		}
		else if (multiplayerPageNumber == 4)
		{
			CONTROLLER.CurrentPage = "multiplayerpage";
			roomPage.SetActive(value: false);
			ticketsPage.SetActive(value: false);
			createPage.SetActive(value: false);
			joinPage.SetActive(value: false);
			oversPage.SetActive(value: false);
			lobbyPage.SetActive(value: true);
			multiplayerPageNumber++;
			if (Multiplayer.isHost == 0)
			{
				HideContinueButton();
				roomIDGO.SetActive(value: false);
				statusTxtGO.SetActive(value: true);
			}
			else
			{
				ServerManager.Instance.CreateRoom();
				roomIDGO.SetActive(value: true);
				statusTxtGO.SetActive(value: true);
			}
			if (Multiplayer.roomType == 0)
			{
				ServerManager.Instance.FindRoom();
				HideContinueButton();
			}
			else if (Multiplayer.roomType == 2)
			{
				HideContinueButton();
			}
		}
		else if (multiplayerPageNumber == -1)
		{
			InterfaceHandler._instance.HideMultiplayerMode();
		}
	}

	private void CheckToggles()
	{
		if (multiplayerPageNumber == 50)
		{
			if (CONTROLLER.tickets > 0)
			{
				multiplayerPageNumber = 0;
				return;
			}
			CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
			InterfaceHandler._instance.ShowNoCoinsPopup();
		}
		else if (multiplayerPageNumber == 0)
		{
			for (int i = 0; i < roomToggle.Length; i++)
			{
				if (roomToggle[i].isOn)
				{
					Multiplayer.roomType = i;
					if (i == 0)
					{
						Multiplayer.isHost = 0;
						multiplayerPageNumber += 3;
					}
					else
					{
						multiplayerPageNumber++;
					}
				}
			}
		}
		else if (multiplayerPageNumber == 1)
		{
			for (int j = 0; j < createToggle.Length; j++)
			{
				if (createToggle[j].isOn)
				{
					if (j == 0)
					{
						Multiplayer.isHost = 1;
						multiplayerPageNumber += 2;
					}
					else
					{
						Multiplayer.isHost = 0;
						multiplayerPageNumber++;
					}
				}
			}
		}
		else if (multiplayerPageNumber == 2)
		{
			VerifyID();
		}
		else if (multiplayerPageNumber == 3)
		{
			for (int k = 0; k < oversToggle.Length; k++)
			{
				if (oversToggle[k].isOn)
				{
					Multiplayer.oversCount = k;
					if (k == 0)
					{
						Multiplayer.overs = 2;
					}
					else
					{
						Multiplayer.overs = 5;
					}
				}
			}
			multiplayerPageNumber++;
		}
		else if (multiplayerPageNumber != 4 && multiplayerPageNumber == 5 && Multiplayer.isHost == 1)
		{
			GameObject original = Resources.Load("Prefabs/Preloader") as GameObject;
			GameObject gameObject = Object.Instantiate(original);
			gameObject.name = "Preloader";
			gameObject.transform.localPosition = new Vector3(0f, 0f, 0.5f);
			ServerManager.Instance.StartPrivateMatch();
		}
	}

	private void VerifyID()
	{
		if (roomIDInput.text == string.Empty || roomIDInput.text == "Enter room ID.")
		{
			InterfaceHandler._instance.ShowRoomNotFoundPopup(LocalizationData.instance.getText(653));
			return;
		}
		Multiplayer.roomID = roomIDInput.text;
		ServerManager.Instance.JoinRoom();
		roomIDInput.text = string.Empty;
	}
}
