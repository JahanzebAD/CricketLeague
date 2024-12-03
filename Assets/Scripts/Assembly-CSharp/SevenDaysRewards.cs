using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class SevenDaysRewards : Singleton<SevenDaysRewards>
{
	public bool enable;

	private int remainingDays;

	public bool claimed;

	public bool showMe;

	public GameObject holder;

	public GameObject packageHolder;

	public Button[] packages;

	public GameObject[] selectedPanelRed;

	public GameObject[] selectedPanelYellow;

	public Text[] claimedText;

	public Color blue;

	public Color white;

	public GameObject rewardVideoBtn;

	public Image[] coinImage;

	public Sprite[] coinSprites;

	private bool doubleRewards;

	private int isSavedToServer;

	private Dictionary<string, int> SevenDayRewardCoin;

	public Image[] AllPackageBGS;

	public Image[] AllBlueBGS;

	public GameObject[] AllDeactivePackagePanels;

	private void Start()
	{
		if (ObscuredPrefs.HasKey("remainingDays"))
		{
			if (ObscuredPrefs.HasKey("Bonus7daysynced"))
			{
				isSavedToServer = ObscuredPrefs.GetInt("Bonus7daysynced");
			}
			else
			{
				ObscuredPrefs.SetInt("Bonus7daysynced", 0);
				isSavedToServer = 0;
			}
		}
		if (isSavedToServer == 0)
		{
			SendToServer();
		}
		coinImage = new Image[7];
		AllPackageBGS = new Image[7];
		AllBlueBGS = new Image[7];
		AllDeactivePackagePanels = new GameObject[7];
	}

	public int IsAvailable()
	{
		if (ObscuredPrefs.HasKey("remainingDays"))
		{
			if (ObscuredPrefs.GetInt("remainingDays") > 0)
			{
				return 1;
			}
			return 0;
		}
		return 1;
	}

	public void LoadPanels()
	{
		SevenDayRewardCoin = new Dictionary<string, int>();
		for (int i = 0; i < 7; i++)
		{
			SevenDayRewardCoin.Add("day" + i, 500 * (i + 2));
		}
		packages = new Button[7];
		selectedPanelRed = new GameObject[packages.Length];
		selectedPanelYellow = new GameObject[packages.Length];
		claimedText = new Text[packages.Length];
		for (int j = 0; j < packages.Length; j++)
		{
			packages[j] = packageHolder.transform.GetChild(j).GetComponent<Button>();
			selectedPanelRed[j] = packages[j].transform.GetChild(1).gameObject;
			selectedPanelYellow[j] = packages[j].transform.Find("BlueBg/SelectedImage").gameObject;
			AllDeactivePackagePanels[j] = packages[j].transform.Find("DeactivatePanel").gameObject;
			AllPackageBGS[j] = packages[j].GetComponent<Image>();
			AllBlueBGS[j] = packages[j].transform.Find("BlueBg").GetComponent<Image>();
			claimedText[j] = packages[j].transform.GetChild(2).GetComponent<Text>();
			coinImage[j] = packages[j].transform.Find("BlueBg/CoinIcon").GetComponent<Image>();
			coinImage[j].sprite = coinSprites[0];
			claimedText[j].color = blue;
			claimedText[j].lineSpacing = 1f;
			if (j < 7 - remainingDays)
			{
				claimedText[j].text = "<size=60>" + SevenDayRewardCoin["day" + j] + "</size>\nCOINS\n<size=50>CLAIMED</size>";
				DeactivateClaimedPackage(j);
			}
			else
			{
				claimedText[j].text = "<size=40>" + SevenDayRewardCoin["day" + j] + "</size>\n<size=25>COINS</size>";
				coinImage[j].sprite = coinSprites[1];
				claimedText[j].lineSpacing = 0.7f;
			}
		}
	}

	private void DeactivateClaimedPackage(int position)
	{
		AllDeactivePackagePanels[position].SetActive(value: true);
		claimedText[position].color = new Vector4(1f, 1f, 1f, 1f);
		AllBlueBGS[position].color = new Vector4(188f / 255f, 191f / 255f, 0.7686275f, 1f);
		AllPackageBGS[position].color = new Vector4(0.7058824f, 0.7215686f, 0.7333333f, 1f);
	}

	public void deactivateAllPanels()
	{
		GameObject[] array = selectedPanelRed;
		foreach (GameObject gameObject in array)
		{
			gameObject.SetActive(value: false);
		}
		GameObject[] array2 = selectedPanelYellow;
		foreach (GameObject gameObject2 in array2)
		{
			gameObject2.SetActive(value: false);
		}
		Button[] array3 = packages;
		foreach (Button button in array3)
		{
			button.interactable = false;
		}
	}

	public int GetRemaingingDays()
	{
		if (ObscuredPrefs.HasKey("remainingDays"))
		{
			remainingDays = ObscuredPrefs.GetInt("remainingDays");
		}
		else
		{
			ObscuredPrefs.SetInt("remainingDays", remainingDays);
		}
		return remainingDays;
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = CloseButtonClicked;
		LoadPanels();
		deactivateAllPanels();
		selectedPanelRed[7 - remainingDays].SetActive(value: true);
		selectedPanelYellow[7 - remainingDays].SetActive(value: true);
		claimedText[7 - remainingDays].text = "<size=60>" + SevenDayRewardCoin["day" + (7 - remainingDays)] + "</size><size=40>\nCOINS</size>";
		coinImage[7 - remainingDays].sprite = coinSprites[0];
		claimedText[7 - remainingDays].lineSpacing = 1f;
		claimedText[7 - remainingDays].color = white;
		packages[7 - remainingDays].interactable = true;
		claimed = false;
		CONTROLLER.pageName = "7days";
		holder.SetActive(value: true);
		rewardVideoBtn.SetActive(value: true);
		showMe = false;
	}

	public void HideMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		if (Singleton<GameModeTWO>.instance.newUserPopup.activeInHierarchy)
		{
			CONTROLLER.pageName = "newUser";
		}
		else
		{
			CONTROLLER.pageName = "landingPage";
		}
		holder.SetActive(value: false);
		showMe = false;
	}

	public void Claim7DayReward()
	{
		CONTROLLER.pageName = "landingPage";
		if (doubleRewards)
		{
			SavePlayerPrefs.SaveUserCoins(SevenDayRewardCoin["day" + (7 - remainingDays)] * 2, 0, SevenDayRewardCoin["day" + (7 - remainingDays)] * 2);
		}
		else
		{
			SavePlayerPrefs.SaveUserCoins(SevenDayRewardCoin["day" + (7 - remainingDays)], 0, SevenDayRewardCoin["day" + (7 - remainingDays)]);
		}
		remainingDays--;
		SendToServer();
		ObscuredPrefs.SetInt("remainingDays", remainingDays);
		if (remainingDays > 0)
		{
			setTimer();
		}
		else
		{
			QuickPlayFreeEntry.Instance.firstTime = true;
			QuickPlayFreeEntry.Instance.StartDayRewards();
		}
		Singleton<GameModeTWO>.instance.ResetDetails();
		Server_Connection.instance.Get_User_Sync();
		HideMe();
	}

	public void CloseButtonClicked()
	{
		Claim7DayReward();
	}

	public void RVSelected()
	{
		//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_DailyClaim" });
		//Singleton<AdIntegrate>.instance.showRewardedVideo(7);
	}

	public void DoubleMyRewards()
	{
		doubleRewards = true;
		Claim7DayReward();
	}

	public void setTimer()
	{
		QuickPlayFreeEntry.Instance.SetSevenDayTimer();
	}

	public void SetRemainingDays(int value)
	{
		remainingDays = value;
	}

	public void SendToServer()
	{
		if ( CONTROLLER.isGameDataSecure)
		{
			if (CONTROLLER.userID != 0)
			{
				StartCoroutine(IServerProcess());
			}
		}
		else
		{
			ObscuredPrefs.SetInt("Bonus7daysynced", 0);
		}
	}

	private IEnumerator IServerProcess()
	{
		WWWForm form = new WWWForm();
		form.AddField("action", "SDBU");
		form.AddField("bonus", remainingDays);
		if (!Server_Connection.instance.guestUser)
		{
			form.AddField("uid", CONTROLLER.userID);
		}
		else if (Server_Connection.instance.guestUser)
		{
			form.AddField("uid", Google_SignIn.guestUserID);
		}
		WWW www = new WWW(CONTROLLER.PHP_Server_Link, form);
		yield return www;
		JsonData json = JsonMapper.ToObject(www.text);
		if (json["SDBU"] != null)
		{
			if (json["SDBU"]["status"].ToString() == "1")
			{
				ObscuredPrefs.SetInt("Bonus7daysynced", 1);
			}
			else
			{
				ObscuredPrefs.SetInt("Bonus7daysynced", 0);
			}
		}
	}
}
