using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SuperOverResult : Singleton<SuperOverResult>
{
	public GameObject background;

	public GameObject playButton;

	public GameObject Holder;

	public GameObject BG;

	public GameObject MatchStatsScreen;

	public GameObject EarningsIntheMatch;

	public GameObject Achievements;

	public GameObject AchievementClaim;

	public GameObject SpinEarned;

	public GameObject replayButton;

	public GameObject replayImage;

	public GameObject playImage;

	public GameObject[] passContents;

	public GameObject[] failContents;

	public GameObject achievementsHolder;

	public GameObject noAchievementsHolder;

	public GameObject achievementsClaim;

	public ScrollRect achievementsScroll;

	public AchievementAnimation AnimationAchieve;

	private string value;

	private string levelValue;

	private string limitValue;

	private string triggerValue;

	private string coinValue;

	public EndAchievementDetails achievementTab;

	private int count;

	public GameObject settings;

	public GameObject instructionsPage;

	private string str;

	private int milestoneIndex;

	public Text content;

	public Text coins;

	public Text achCount;

	public Text XPWon;

	public Text NewXpWon;

	public Text NewXpWon1;

	private int tempxp;

	private int StartingXp;

	private int EndingXp;

	private bool alreadySavedXP;

	public Image flag;

	public Image Xpfill;

	public Image WinImage;

	public Sprite[] WinAndLoseImage;

	private int amountToReduce;

	private int totalExpInaMatch;

	private int coinAmount;

	private int XPFromFour;

	private int XPFromMaiden;

	private int XPFromDot;

	private int XPFromSix;

	private int XPFromWicketBowled;

	private int XPFromWicketCatch;

	private int XPFromWicketOthers;

	private int XPFromFifty;

	private int XPFromCentury;

	private int XPFromPerOver;

	private int[] xpAmount = new int[34]
	{
		1000, 2500, 5000, 7500, 10000, 12500, 25000, 50000, 75000, 100000,
		125000, 150000, 175000, 200000, 250000, 300000, 350000, 400000, 450000, 500000,
		550000, 600000, 650000, 700000, 750000, 800000, 850000, 900000, 950000, 1000000,
		1500000, 2000000, 2500000, 5000000
	};

	private bool canShowUpgrade;

	private bool showUpgrade;

	private bool canShowLeaderboard;

	public Transform spinWheelBox;

	public Transform textBox;

	public Transform fillBar;

	public Transform statusText;

	public int pageNumber = 1;

	public Text positionChanged;

	public Text LBdescription;

	public GameObject up;

	public GameObject down;

	public GameObject rankIncreased;

	public Text upgradeNameText;

	public Text upgradeAmountText;

	private float subgrades;

	private float limit;

	private int[] XPmilestones = new int[10] { 0, 5000, 10000, 20000, 35000, 55000, 80000, 110000, 145000, 185000 };

	public Transform UpgradeTransform;

	public Transform LeaderboardTransform;

	public GameObject bottomComponent;

	private void CalculateXPforCurrentMatch()
	{
		XPFromFour = Singleton<GameModel>.instance.CounterFour * 48;
		XPFromSix = Singleton<GameModel>.instance.CounterSix * 72;
		XPFromFifty = Singleton<GameModel>.instance.CounterFifty * 120;
		XPFromCentury = Singleton<GameModel>.instance.CounterCentury * 240;
		XPFromPerOver = Singleton<GameModel>.instance.CounterPerOversPlayed * 12;
		totalExpInaMatch = XPFromFour + XPFromSix + XPFromFifty + XPFromCentury + XPFromPerOver;
		Singleton<GameModel>.instance.DeleteKeys();
	}

	protected void Start()
	{
		alreadySavedXP = false;
	}

	public void ValidateLeaderboardPosition()
	{
		positionChanged.DOText(Mathf.Abs(Server_Connection.instance.diff_AXp).ToString(), 0.5f, richTextEnabled: true, ScrambleMode.Numerals);
		if (Server_Connection.instance.diff_AXp > 0)
		{
			up.gameObject.SetActive(value: true);
			up.transform.DOScale(Vector3.one, 0.3f).SetUpdate(isIndependentUpdate: true);
			LBdescription.text = LocalizationData.instance.getText(297) + "!";
			down.gameObject.SetActive(value: false);
			rankIncreased.SetActive(value: true);
		}
		else if (Server_Connection.instance.diff_AXp == 0)
		{
			LBdescription.text = LocalizationData.instance.getText(534);
			rankIncreased.SetActive(value: false);
		}
		else
		{
			down.gameObject.SetActive(value: true);
			down.transform.DOScale(Vector3.one, 0.3f).SetUpdate(isIndependentUpdate: true);
			LBdescription.text = LocalizationData.instance.getText(287);
			up.gameObject.SetActive(value: false);
			rankIncreased.SetActive(value: true);
		}
	}

	private IEnumerator SyncPoints()
	{
		if (CONTROLLER.userID != 0)
		{
			yield return Server_Connection.instance.User_Leaderboard_Sync();
			Server_Connection.instance.Get_ArcadeXPRank();
			Server_Connection.instance.Get_User_Sync();
		}
	}

	private void ValidateThisLevel()
	{
		ObscuredPrefs.SetInt("SOPaid", 0);
		if ((CONTROLLER.LevelId >= 17 && CONTROLLER.SuperOverMode == "bat") || (CONTROLLER.LevelId >= 8 && CONTROLLER.SuperOverMode == "bowl"))
		{
			playButton.SetActive(value: false);
			playImage.SetActive(value: false);
		}
		if (CONTROLLER.CurrentLevelCompleted >= CONTROLLER.LevelId && CONTROLLER.LevelFailed == 1 && CONTROLLER.LevelCompletedArray[CONTROLLER.CurrentLevelCompleted] == 0)
		{
			SentToFirebase(1);
		}
		else
		{
			SentToFirebase(0);
		}
		if ((Singleton<GameModel>.instance.levelCompleted && CONTROLLER.SuperOverMode == "bat") || (!Singleton<GameModel>.instance.levelCompleted && CONTROLLER.SuperOverMode == "bowl"))
		{
			playButton.SetActive(value: true);
			playImage.SetActive(value: true);
			WinImage.sprite = WinAndLoseImage[1];
			if (PlayerPrefs.HasKey("SO" + CONTROLLER.LevelId + CONTROLLER.SuperOverMode))
			{
				SpinEarned.SetActive(value: false);
			}
			else
			{
				PlayerPrefs.SetInt("SO" + CONTROLLER.LevelId + CONTROLLER.SuperOverMode, 1);
				SpinEarned.SetActive(value: true);
				SavePlayerPrefs.SaveSpins(1);
			}
		}
		else
		{
			SpinEarned.SetActive(value: false);
			playButton.SetActive(value: false);
			playImage.SetActive(value: false);
			WinImage.sprite = WinAndLoseImage[0];
		}
		CalculateXPforCurrentMatch();
	}

	private void AnimateXPbar()
	{
		float fillAmount = (float)StartingXp / (float)xpAmount[milestoneIndex];
		Xpfill.fillAmount = fillAmount;
		float endValue = (float)EndingXp / (float)xpAmount[milestoneIndex];
		Xpfill.DOFillAmount(endValue, 2f).SetUpdate(isIndependentUpdate: true);
		NewXpWon.DOText(EndingXp.ToString(), 2f, richTextEnabled: true, ScrambleMode.Numerals).SetUpdate(isIndependentUpdate: true);
	}

	public void Claim()
	{
		for (int i = 1; i < 16; i++)
		{
			value = AchievementTable.AchievementName[i];
			triggerValue = value + " Trigger" + CONTROLLER.PlayModeSelected;
			if (ObscuredPrefs.HasKey(triggerValue) && ObscuredPrefs.GetInt(triggerValue) == 1)
			{
				if (ObscuredPrefs.HasKey(value + " ReachedMax"))
				{
					AchievementTable.GoNextLevel(i);
				}
				else
				{
					AchievementTable.ResetAmount(i);
				}
			}
		}
		achievementsClaim.SetActive(value: false);
		coins.text = "0";
		SavePlayerPrefs.SaveUserCoins(coinAmount, 0, coinAmount);
		coinAmount = 0;
		AnimationAchieve.CoinAnim(0);
		AnimationAchieve.CoinsTopPanel.text = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey).ToString();
	}

	private void SentToFirebase(int index)
	{
		int num = CONTROLLER.LevelId % 2;
		if (CONTROLLER.LevelId >= 18)
		{
			return;
		}
		if (num == 0)
		{
			if (index == 0)
			{
				FBAppEvents.instance.LogSuperOverEvent("SO_Win", "Win");
			}
			else
			{
				FBAppEvents.instance.LogSuperOverEvent("SO_Loss", "Loss");
			}
		}
		else if (index == 0)
		{
			FBAppEvents.instance.LogSuperOverEvent("SO_Win", "Win");
		}
		else
		{
			FBAppEvents.instance.LogSuperOverEvent("SO_Loss", "Loss");
		}
	}

	public void GoToHome()
	{
		Singleton<GameModel>.instance.levelCompleted = false;
		CONTROLLER.sndController.PlayButtonSnd();
		Holder.SetActive(value: false);
		Singleton<PauseGameScreen>.instance.showQuitPopup();
		//GoogleAllAds.Instance.InterstitialAdPlz();
	}

	public void ReplayThisLevel()
	{
		//Singleton<AdIntegrate>.instance.HideAd();
		Singleton<PauseGameScreen>.instance.BG.SetActive(value: false);
		CONTROLLER.PlayModeSelected = 4;
		AutoSave.DeleteFile();
		CONTROLLER.NewInnings = true;
		Singleton<Scoreboard>.instance.NewOver();
		Singleton<Scoreboard>.instance.UpdateScoreCard();
		Singleton<GroundController>.instance.ResetAll();
		Singleton<GameModel>.instance.ResetVariables();
		CONTROLLER.GameStartsFromSave = false;
		Singleton<GameModel>.instance.levelCompleted = false;
		CONTROLLER.NewInnings = true;
		CONTROLLER.InningsCompleted = false;
		HideMe();
		//GoogleAllAds.Instance.InterstitialAdPlz();
		Singleton<NavigationBack>.instance.deviceBack = null;
		ManageScene.loadScene("Ground");
	}

	public void OpenAchievements()
	{
		//Singleton<AdIntegrate>.instance.HideAd();
		if (Singleton<Google_SignIn>.instance.googleAuthenticated == 1)
		{
			Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
			Singleton<NavigationBack>.instance.deviceBack = CloseAchievements;
			EarningsIntheMatch.SetActive(value: false);
			CONTROLLER.pageName = "AchievementsSuperOver";
			Achievements.SetActive(value: true);
			AnimationAchieve.CoinsTopPanel.text = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey).ToString();
			AnimationAchieve.TicketTopPanel.text = ObscuredPrefs.GetInt(CONTROLLER.TicketsKey).ToString();
			AnimationAchieve.XpTopPanel.text = ObscuredPrefs.GetInt(CONTROLLER.XPKey).ToString();
		}
		else
		{
			CONTROLLER.PopupName = "googlesignin";
			Singleton<Popups>.instance.ShowMe();
		}
		//GoogleAllAds.Instance.InterstitialAdPlz();
	}

	private void ValidateAchievements()
	{
		count = 1;
		int num = 0;
		for (int i = 1; i < 16; i++)
		{
			value = AchievementTable.AchievementName[i];
			triggerValue = value + " Trigger" + CONTROLLER.PlayModeSelected;
			levelValue = value + " Level";
			limitValue = value + " Limit";
			coinValue = value + " Coins";
			if (i == 15)
			{
				if (!AchievementTable.achie15)
				{
					ObscuredPrefs.SetInt(triggerValue, 0);
				}
				AchievementTable.achie15 = false;
			}
			if (ObscuredPrefs.HasKey(triggerValue) && ObscuredPrefs.GetInt(triggerValue) == 1)
			{
				if (count > 1)
				{
					Object.Instantiate(achievementTab, achievementTab.transform.parent);
				}
				num = 1;
				achievementTab.title.text = AchievementTable.AchievementName[i];
				achievementTab.subtittle.text = AchievementTable.AchievementGoal[i];
				achievementTab.progress.text = ObscuredPrefs.GetInt(value) + "/" + ObscuredPrefs.GetInt(limitValue);
				achievementTab.progressFill.fillAmount = (float)ObscuredPrefs.GetInt(value) / (float)ObscuredPrefs.GetInt(limitValue);
				achievementTab.level.text = LocalizationData.instance.getText(312) + " 0" + ObscuredPrefs.GetInt(levelValue);
				coinAmount += ObscuredPrefs.GetInt(coinValue);
				count++;
			}
		}
		if (num == 0 || Singleton<Google_SignIn>.instance.googleAuthenticated == 0)
		{
			achievementsHolder.SetActive(value: false);
			noAchievementsHolder.SetActive(value: true);
			achievementsClaim.SetActive(value: false);
			achCount.gameObject.SetActive(value: false);
		}
		else
		{
			achCount.gameObject.SetActive(value: true);
			achCount.text = "+" + (count - 1);
		}
		if (count < 3)
		{
			achievementsScroll.enabled = false;
		}
		coins.text = coinAmount.ToString();
		AnimationAchieve.RewardCoinsText.text = coinAmount.ToString();
	}

	public void CloseAchievements()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		MatchstatsScreenOKButton();
		EarningsIntheMatch.SetActive(value: true);
		CONTROLLER.pageName = "SuperOverEarningsPage";
		Achievements.SetActive(value: false);
		//Singleton<AdIntegrate>.instance.ShowAd();
	}

	public void openLeaderboard()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		//Singleton<LeaderBoard>.instance.ShowMe();
	}

	//public void openStore()
	//{
	//	if (Singleton<AdIntegrate>.instance.CheckForInternet())
	//	{
	//		if (Singleton<Google_SignIn>.instance.googleAuthenticated == 1)
	//		{
	//			Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
	//			Singleton<NavigationBack>.instance.deviceBack = FromStoreBack;
	//			HideMe();
	//			Singleton<Store>.instance.ShowMe();
	//			CONTROLLER.pageName = "store";
	//			Singleton<Store>.instance.PerformanceUpgradeButtonClicked();
	//		}
	//		else
	//		{
	//			CONTROLLER.PopupName = "googlesignin";
	//			Singleton<Popups>.instance.ShowMe();
	//		}
	//	}
	//	else
	//	{
	//		CONTROLLER.PopupName = "noInternet";
	//		Singleton<Popups>.instance.ShowMe();
	//	}
	//}

	private void FromStoreBack()
	{
		if (pageNumber == 1)
		{
			MatchStatsScreen.SetActive(value: true);
		}
		else
		{
			MatchstatsScreenOKButton();
			EarningsIntheMatch.SetActive(value: true);
		}
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		Holder.SetActive(value: true);
	}

	public void GoNextLevel()
	{
		//Singleton<AdIntegrate>.instance.HideAd();
		CONTROLLER.GameStartsFromSave = false;
		if (CONTROLLER.LevelId < 17)
		{
			CONTROLLER.LevelId++;
		}
		HideMe();
		Singleton<NavigationBack>.instance.deviceBack = null;
		Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
		//GoogleAllAds.Instance.InterstitialAdPlz();
	}

	private void SetBowler()
	{
		if (CONTROLLER.LevelId % 2 == 0)
		{
			Singleton<GroundController>.instance.currentBowlerType = "fast";
		}
		else
		{
			Singleton<GroundController>.instance.currentBowlerType = "spin";
		}
		ShowSuperOverLevelInfo();
	}

	private void ShowSuperOverLevelInfo()
	{
		Singleton<NavigationBack>.instance.deviceBack = null;
		Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
		Holder.SetActive(value: false);
	}

	private void HideMe()
	{
		Holder.SetActive(value: false);
	}

	private void LeaderBoardAnim()
	{
		int num = 1;
		num = 1;
		if (Singleton<Google_SignIn>.instance.googleAuthenticated == num )
		{
			canShowLeaderboard = true;
			LeaderboardTransform.gameObject.SetActive(value: true);
		}
		else
		{
			canShowLeaderboard = false;
			LeaderboardTransform.gameObject.SetActive(value: false);
		}
		if (canShowLeaderboard)
		{
			LeaderboardTransform.DOScale(Vector3.one, 0.5f).SetUpdate(isIndependentUpdate: true).OnComplete(UpgradeAnim);
		}
		else
		{
			UpgradeAnim();
		}
	}

	private void UpgradeAnim()
	{
		UpgradeTransform.DOScale(Vector3.one, 0.5f).SetUpdate(isIndependentUpdate: true);
	}

	private void ResetAnimations()
	{
		UpgradeTransform.DOScale(Vector3.zero, 0f).SetUpdate(isIndependentUpdate: true);
		LeaderboardTransform.DOScale(Vector3.zero, 0f).SetUpdate(isIndependentUpdate: true);
		up.transform.DOScale(Vector3.zero, 0f).SetUpdate(isIndependentUpdate: true);
		down.transform.DOScale(Vector3.zero, 0f).SetUpdate(isIndependentUpdate: true);
		up.transform.DOScale(Vector3.zero, 0.5f).SetUpdate(isIndependentUpdate: true).OnComplete(LeaderBoardAnim);
		LBdescription.text = string.Empty;
		positionChanged.text = string.Empty;
		LBdescription.text = "Your position is unchanged";
		rankIncreased.SetActive(value: false);
	}

	public void ShowMe()
	{
		//Singleton<AdIntegrate>.instance.DisplayInterestialAd();
		GroundController.SOMatchStart = false;
		pageNumber = 1;
		ResetAnimations();
		CONTROLLER.pageName = "SuperOverResult";
		tempxp = CONTROLLER.ArcadeXPs;
		ObscuredPrefs.SetInt("SOPaid", 0);
		Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite in flags)
		{
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation)
			{
			}
		}
		//if (CONTROLLER.canShowbannerMainmenu == 1)
		//{
		//	//Singleton<AdIntegrate>.instance.ShowAd();
		//}
		CONTROLLER.CurrentMenu = "SuperOverResult";
		ValidateThisLevel();
		ValidateAchievements();
		BG.SetActive(value: true);
		MatchStatsScreen.SetActive(value: true);
		Holder.SetActive(value: true);
		StartCoroutine(Singleton<AchievementsSyncronizer>.instance.SendAchievementsAndKits());
		//if (!Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	bottomComponent.SetActive(value: false);
		//}
		//else
		//{
		//	bottomComponent.SetActive(value: true);
		//}
		ScreenOneDetails();
		ShowAvailableUpgrade();
	}

	public void MatchstatsScreenOKButton()
	{
		pageNumber = 2;
		Singleton<NavigationBack>.instance.deviceBack = EarningsBackButton;
		CONTROLLER.pageName = "SuperOverEarningsPage";
		MatchStatsScreen.SetActive(value: false);
		EarningsIntheMatch.SetActive(value: true);
		ScreenOneDetails();
		NewXpWon1.text = "/ " + xpAmount[milestoneIndex];
		AnimateXPbar();
	}

	private void ScreenOneDetails()
	{
		StartingXp = tempxp;
		EndingXp = StartingXp + totalExpInaMatch;
		if (!alreadySavedXP)
		{
			SavePlayerPrefs.SaveUserArcadeXPs(totalExpInaMatch, totalExpInaMatch);
			alreadySavedXP = true;
			StartCoroutine(SyncPoints());
		}
		if (ObscuredPrefs.HasKey("ArcadeXPMilestoneIndex"))
		{
			milestoneIndex = ObscuredPrefs.GetInt("ArcadeXPMilestoneIndex");
		}
		else
		{
			milestoneIndex = 0;
		}
	}

	public void EarningsBackButton()
	{
		pageNumber = 1;
		CONTROLLER.pageName = "SuperOverResult";
		EarningsIntheMatch.SetActive(value: false);
		MatchStatsScreen.SetActive(value: true);
	}

	public void showSettings()
	{
		Singleton<SettingsPageTWO>.instance.showMe();
		HideMe();
	}

	public void hideInstructions()
	{
		background.SetActive(value: true);
		instructionsPage.SetActive(value: false);
	}

	private void ResetScreen1Components()
	{
		statusText.DOScale(Vector3.zero, 0f).SetUpdate(isIndependentUpdate: true);
	}

	private void ResetScreen2Components()
	{
	}

	public void ShowAvailableUpgrade()
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string[] array = new string[3] { "POWER UPGRADE", "CONTROL UPGRADE", "AGILITY UPGRADE" };
		int num = 0;
		int num2 = -1;
		int num3 = 1;
		num3 = 1;
		if ( Singleton<Google_SignIn>.instance.googleAuthenticated == num3)
		{
			for (int i = 0; i < 3; i++)
			{
				if (Singleton<PowerUps>.instance.timerStarted[i])
				{
					switch (i)
					{
					case 0:
						text2 = (QuickPlayFreeEntry.Instance.powerUBTime.TotalSeconds / double.Parse(Singleton<PaymentProcess>.instance.GenerateTimerValue(i).ToString())).ToString();
						break;
					case 1:
						text2 = (QuickPlayFreeEntry.Instance.ControlUBTime.TotalSeconds / double.Parse(Singleton<PaymentProcess>.instance.GenerateTimerValue(i).ToString())).ToString();
						break;
					case 2:
						text2 = (QuickPlayFreeEntry.Instance.AgilityUBTime.TotalSeconds / double.Parse(Singleton<PaymentProcess>.instance.GenerateTimerValue(i).ToString())).ToString();
						break;
					}
					float num4 = float.Parse(text2);
					int num5 = Mathf.CeilToInt(num4 / 0.05f);
					int num6 = Singleton<PaymentProcess>.instance.GenerateFinishTimeCoinValue(i);
					text2 = (num6 * num5).ToString();
				}
				else
				{
					text2 = Singleton<PaymentProcess>.instance.GenerateCoinValue(i).ToString();
				}
				if (CONTROLLER.Coins >= int.Parse(text2))
				{
					switch (i)
					{
					case 0:
						showUpgrade = IsPowerUpgradeAvailable();
						break;
					case 1:
						showUpgrade = IsControlUpgradeAvailable();
						break;
					case 2:
						showUpgrade = IsAgilityUpgradeAvailable();
						break;
					}
				}
				if (showUpgrade)
				{
					num2 = i;
					num = int.Parse(text2);
					if (Random.Range(0, 10) < 5)
					{
						break;
					}
				}
			}
			if (num2 != -1 && num != 0)
			{
				showUpgrade = true;
				text = array[num2];
				text2 = num.ToString();
			}
			else
			{
				showUpgrade = false;
			}
		}
		else
		{
			showUpgrade = false;
		}
		upgradeAmountText.text = text2;
		upgradeNameText.text = text;
		if (showUpgrade)
		{
			UpgradeTransform.gameObject.SetActive(value: true);
		}
		else
		{
			UpgradeTransform.gameObject.SetActive(value: false);
		}
	}

	private bool IsPowerUpgradeAvailable()
	{
		subgrades = 0f;
		limit = 0f;
		if (CONTROLLER.powerGrade <= 5)
		{
			subgrades = CONTROLLER.totalPowerSubGrade - CONTROLLER.powerGrade * 50;
			limit = 50f;
		}
		else if (CONTROLLER.powerGrade >= 6 && CONTROLLER.powerGrade <= 9)
		{
			subgrades = CONTROLLER.totalPowerSubGrade - 300 - (CONTROLLER.powerGrade - 6) * 250;
			limit = 250f;
		}
		if (subgrades + 10f < limit || CONTROLLER.XPs >= XPmilestones[CONTROLLER.powerGrade] || CONTROLLER.powerGrade == 10)
		{
			return true;
		}
		return false;
	}

	private bool IsAgilityUpgradeAvailable()
	{
		subgrades = 0f;
		limit = 0f;
		if (CONTROLLER.agilityGrade <= 5)
		{
			subgrades = CONTROLLER.totalAgilitySubGrade - CONTROLLER.agilityGrade * 50;
			limit = 50f;
		}
		else if (CONTROLLER.agilityGrade >= 6 && CONTROLLER.agilityGrade <= 9)
		{
			subgrades = CONTROLLER.totalAgilitySubGrade - 300 - (CONTROLLER.agilityGrade - 6) * 250;
			limit = 250f;
		}
		if (subgrades + 10f < limit || CONTROLLER.XPs >= XPmilestones[CONTROLLER.agilityGrade] || CONTROLLER.agilityGrade == 10)
		{
			return true;
		}
		return false;
	}

	private bool IsControlUpgradeAvailable()
	{
		subgrades = 0f;
		limit = 0f;
		if (CONTROLLER.controlGrade <= 5)
		{
			subgrades = CONTROLLER.totalControlSubGrade - CONTROLLER.controlGrade * 50;
			limit = 50f;
		}
		else if (CONTROLLER.controlGrade >= 6 && CONTROLLER.controlGrade <= 9)
		{
			subgrades = CONTROLLER.totalControlSubGrade - 300 - (CONTROLLER.controlGrade - 6) * 250;
			limit = 250f;
		}
		if (subgrades + 10f < limit || CONTROLLER.XPs >= XPmilestones[CONTROLLER.controlGrade] || CONTROLLER.controlGrade == 10)
		{
			return true;
		}
		return false;
	}
}
