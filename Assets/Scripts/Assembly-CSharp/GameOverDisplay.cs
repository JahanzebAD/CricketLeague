using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDisplay : Singleton<GameOverDisplay>
{
	public GameObject holder;

	public GameObject screenOne;

	public GameObject screenTwo;

	public GameObject screenThree;

	public GameObject screenFour;

	public GameObject doubleRewards;

	public GameObject cupImg;

	public GameObject matchStatusImg;

	public GameObject MatchStatsSubImg;

	public GameObject PLWon;

	public GameObject achievementsClaim;

	public GameObject achievementsHolder;

	public GameObject noAchievementsHolder;

	public GameObject replayImage;

	public GameObject replayButton;

	public Transform ScoreCardContent;

	public AchievementAnimation AnimationAchieve;

	public static int pageNumber = 1;

	public Sprite[] matchStatusSprite;

	public Sprite[] cupImgSprite;

	public Sprite[] WinTextSprite;

	public Sprite[] WinningCup;

	public Sprite WinningBG;

	public Image WinningCupImg;

	public ScrollRect achievementsScroll;

	public EndAchievementDetails achievementTab;

	private EndAchievementDetails extraAchievement;

	public Text matchResult;

	public Text tourStatus;

	public Text description;

	public Text matchWonBy;

	public Text coins;

	public Text bonusCoins;

	public Text freeSpins;

	public Text matchXp;

	public Text matchXp1;

	public Text extras;

	public Text score;

	public Text spins;

	public Text extrasSubText;

	public Text scoreSubText;

	public Text achCount;

	public Text replayFees;

	private bool alreadySavedXP;

	public Image XPFill;

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

	private int totalExpInaMatch;

	private int CoinsFromFour;

	private int CoinsFromMaiden;

	private int CoinsFromDot;

	private int CoinsFromSix;

	private int CoinsFromWicketBowled;

	private int CoinsFromWicketCatch;

	private int CoinsFromWicketOthers;

	private int CoinsFromFifty;

	private int CoinsFromCentury;

	private int totalCoinsInaMatch;

	private int myTeamScore;

	private int oppTeamScore;

	private int myTeamWicket;

	private int oppTeamWicket;

	private int coinAmount;

	private int count;

	private float difficultyMultiplier;

	private float oversMultiplier;

	private int milestoneIndex;

	public string matchStatus;

	public string index;

	public string key;

	private string value;

	private string levelValue;

	private string limitValue;

	private string triggerValue;

	private string coinValue;

	private int rewardMultiplier = 1;

	private float XPGained;

	private int amountToReduce;

	private int tempXP;

	private int startingXP;

	private int endingXP;

	private int totalXP;

	[Header("Leaderboard")]
	public GameObject LeaderboardPosition;

	[Header("Leaderboard")]
	public GameObject rankIncreased1;

	[Header("Leaderboard")]
	public GameObject rankIncreased2;

	[Header("Leaderboard")]
	public GameObject achievementPresent;

	public Text positionChanged;

	public Text positionChanged2;

	public Text LBdescription;

	public Text AchievementCoins;

	public Image up;

	public Image down;

	public Image up2;

	public Image down2;

	public GameObject bottomWidget;

	public Text upgradeNameText;

	public Text upgradeAmountText;

	public Text upgradeNameText2;

	public Text upgradeAmountText2;

	public Text replayBtnText;

	private GameObject sideScreen;

	private GameObject mainScreen;

	private float sideScreenPos;

	private float mainScreenPos;

	public GameObject leftBtn;

	public GameObject rightBtn;

	private bool isAnimating;

	[Header("ScreenOneAnimation")]
	public Transform TopImage;

	[Header("ScreenOneAnimation")]
	public Transform LeaderboardTransform;

	[Header("ScreenOneAnimation")]
	public Transform UpgradeTransform;

	[Header("ScreenOneAnimation")]
	public Transform sideTransform;

	[Header("ScreenOneAnimation")]
	public Transform LBIcon;

	[Header("ScreenOneAnimation")]
	public Transform PUIcon;

	[Header("ScreenOneAnimation")]
	public Transform LBIcon2;

	[Header("ScreenOneAnimation")]
	public Transform PUIcon2;

	[Header("ScreenOneAnimation")]
	public Transform ACIcon;

	[Header("ScreenOneAnimation")]
	public Transform PUDesc;

	public Image shine;

	private bool canShowAchievement;

	private bool showUpgrade;

	private bool canShowLeaderboard;

	[Header("ScreenTwoAnimation")]
	public Transform LeaderboardTransform2;

	[Header("ScreenTwoAnimation")]
	public Transform UpgradeTransform2;

	[Header("ScreenTwoAnimation")]
	public Transform AchievementTransform;

	private int[] xpAmount = new int[34]
	{
		1000, 2500, 5000, 7500, 10000, 12500, 25000, 50000, 75000, 100000,
		125000, 150000, 175000, 200000, 250000, 300000, 350000, 400000, 450000, 500000,
		550000, 600000, 650000, 700000, 750000, 800000, 850000, 900000, 950000, 1000000,
		1500000, 2000000, 2500000, 5000000
	};

	public Image dynamicImage;

	public Image winStatusImg;

	public Sprite[] dynamicSprites;

	public Sprite[] winStatusSprite;

	private float subgrades;

	private float limit;

	private int[] XPmilestones = new int[10] { 0, 5000, 10000, 20000, 35000, 55000, 80000, 110000, 145000, 185000 };

	private void Start()
	{
		alreadySavedXP = false;
		if (CONTROLLER.PlayModeSelected == 0 && ObscuredPrefs.HasKey("doubleRewards"))
		{
			doubleRewards.SetActive(value: true);
			rewardMultiplier = 2;
		}
		else
		{
			doubleRewards.SetActive(value: false);
			rewardMultiplier = 1;
		}
		coinAmount = 0;
		GameOverDetails.InitDescriptionValues();
		CoinSystem.GetBaseEarnings();
		HideMe();
	}

	private void CalculateXPforCurrentMatch()
	{
		XPFromFour = Singleton<GameModel>.instance.CounterFour * XPTable.XPEarned["FoursHit" + CONTROLLER.difficultyMode];
		XPFromMaiden = Singleton<GameModel>.instance.CounterMaiden * XPTable.XPEarned["MaidenOver" + CONTROLLER.difficultyMode];
		XPFromDot = Singleton<GameModel>.instance.CounterDot * XPTable.XPEarned["DotBall" + CONTROLLER.difficultyMode];
		XPFromSix = Singleton<GameModel>.instance.CounterSix * XPTable.XPEarned["SixHit" + CONTROLLER.difficultyMode];
		XPFromWicketBowled = Singleton<GameModel>.instance.CounterWicketBowled * XPTable.XPEarned["Bowled" + CONTROLLER.difficultyMode];
		XPFromWicketCatch = Singleton<GameModel>.instance.CounterWicketCatch * XPTable.XPEarned["Caught" + CONTROLLER.difficultyMode];
		XPFromWicketOthers = Singleton<GameModel>.instance.CounterWicketOthers * XPTable.XPEarned["WicketOthers" + CONTROLLER.difficultyMode];
		XPFromFifty = Singleton<GameModel>.instance.CounterFifty * XPTable.XPEarned["Fifty" + CONTROLLER.difficultyMode];
		XPFromCentury = Singleton<GameModel>.instance.CounterCentury * XPTable.XPEarned["Century" + CONTROLLER.difficultyMode];
		XPFromPerOver = Singleton<GameModel>.instance.CounterPerOversPlayed * XPTable.XPEarned["PerOverPlayed" + CONTROLLER.difficultyMode];
		CoinsFromFour = Singleton<GameModel>.instance.CounterFour * CoinsTable.CoinsEarned["FoursHitCoins"];
		CoinsFromMaiden = Singleton<GameModel>.instance.CounterMaiden * CoinsTable.CoinsEarned["MaidenOverCoins"];
		CoinsFromDot = Singleton<GameModel>.instance.CounterDot * CoinsTable.CoinsEarned["DotBallCoins"];
		CoinsFromSix = Singleton<GameModel>.instance.CounterSix * CoinsTable.CoinsEarned["SixHitCoins"];
		CoinsFromWicketBowled = Singleton<GameModel>.instance.CounterWicketBowled * CoinsTable.CoinsEarned["BowledCoins"];
		CoinsFromWicketCatch = Singleton<GameModel>.instance.CounterWicketCatch * CoinsTable.CoinsEarned["CaughtCoins"];
		CoinsFromWicketOthers = Singleton<GameModel>.instance.CounterWicketOthers * CoinsTable.CoinsEarned["WicketOthersCoins"];
		CoinsFromFifty = Singleton<GameModel>.instance.CounterFifty * CoinsTable.CoinsEarned["FiftyCoins"];
		CoinsFromCentury = Singleton<GameModel>.instance.CounterCentury * CoinsTable.CoinsEarned["CenturyCoins"];
		totalExpInaMatch = XPFromFour + XPFromMaiden + XPFromDot + XPFromSix + XPFromWicketBowled + XPFromWicketCatch + XPFromWicketOthers + XPFromFifty + XPFromCentury + XPFromPerOver;
		totalCoinsInaMatch = (CoinsFromFour + CoinsFromMaiden + CoinsFromDot + CoinsFromSix + CoinsFromWicketBowled + CoinsFromWicketCatch + CoinsFromWicketOthers + CoinsFromFifty + CoinsFromCentury) * rewardMultiplier;
		SavePlayerPrefs.SaveUserCoins(totalCoinsInaMatch, 0, totalCoinsInaMatch);
		SavePlayerPrefs.SaveUserXP();
		Singleton<GameModel>.instance.DeleteKeys();
	}

	public void ScreenOneDetails()
	{
		index = CONTROLLER.PlayModeSelected.ToString() + GameOverScreen.stage;
		if (matchStatus == "Win")
		{
			MatchWonBy(0);
		}
		else if (matchStatus == "Loss")
		{
			MatchWonBy(1);
		}
		else
		{
			MatchWonBy(0);
		}
		ScreenThreeDetails();
		ScreenTwoDetails();
	}

	private void GetSpinDetails()
	{
		int num = 0;
		if (CONTROLLER.PlayModeSelected != 0)
		{
			if (CONTROLLER.PlayModeSelected == 1)
			{
				key = "1" + GameOverScreen.stage + CONTROLLER.oversSelectedIndex.ToString();
				num = 3;
			}
			else if (CONTROLLER.PlayModeSelected == 2)
			{
				key = "2" + GameOverScreen.stage + CONTROLLER.oversSelectedIndex.ToString();
				num = 3;
			}
			else if (CONTROLLER.PlayModeSelected == 3)
			{
				key = "3" + GameOverScreen.stage + CONTROLLER.oversSelectedIndex.ToString();
				num = 5;
			}
			if (GameOverDetails.SpinsWonDetails.ContainsKey(key) && matchStatus == "Win")
			{
				int amount = Random.Range(GameOverDetails.SpinsWonDetails[key], GameOverDetails.SpinsWonDetails[key] + num);
				spins.text = amount.ToString();
				SavePlayerPrefs.SaveSpins(amount);
			}
			else
			{
				spins.text = "0";
			}
		}
	}

	private void ScreenTwoDetails()
	{
		DecidePageToShow();
		GenerateMultiplierValues();
		GenerateXPGained();
		float num = 0f;
		num = (float)CoinSystem.BaseValues[GenerateBaseEarningsKey()] * difficultyMultiplier * oversMultiplier * (float)rewardMultiplier;
		SavePlayerPrefs.SaveUserCoins((int)num, 0, (int)num);
		ValidateXPMilestones();
		bonusCoins.text = ((float)totalCoinsInaMatch + num).ToString();
	}

	private string GenerateBaseEarningsKey()
	{
		string empty = string.Empty;
		empty += CONTROLLER.PlayModeSelected;
		if (CONTROLLER.PlayModeSelected != 0)
		{
			empty = empty + "|" + GameOverScreen.stage;
		}
		if (CONTROLLER.PlayModeSelected == 2 && GameOverScreen.stage == 1)
		{
			empty = empty + "|" + GameOverScreen.nplPlayoffs;
		}
		return empty + "|" + matchStatus;
	}

	private void GenerateMultiplierValues()
	{
		if (CONTROLLER.difficultyMode == "easy")
		{
			difficultyMultiplier = 1f;
		}
		else if (CONTROLLER.difficultyMode == "medium")
		{
			difficultyMultiplier = 1.2f;
		}
		else if (CONTROLLER.difficultyMode == "hard")
		{
			difficultyMultiplier = 1.6f;
		}
		if (CONTROLLER.PlayModeSelected == 0)
		{
			if (CONTROLLER.oversSelectedIndex == 0)
			{
				oversMultiplier = 1f;
			}
			else if (CONTROLLER.oversSelectedIndex == 1)
			{
				oversMultiplier = 2.5f;
			}
			else if (CONTROLLER.oversSelectedIndex == 2)
			{
				oversMultiplier = 5f;
			}
		}
		else if (CONTROLLER.PlayModeSelected == 1 || CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.oversSelectedIndex == 2)
			{
				oversMultiplier = 1f;
			}
			else if (CONTROLLER.oversSelectedIndex == 3)
			{
				oversMultiplier = 2f;
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			if (CONTROLLER.oversSelectedIndex == 3)
			{
				oversMultiplier = 1f;
			}
			else if (CONTROLLER.oversSelectedIndex == 4)
			{
				oversMultiplier = 1.5f;
			}
			else if (CONTROLLER.oversSelectedIndex == 5)
			{
				oversMultiplier = 2.5f;
			}
		}
	}

	public void ReplayMatch()
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			if (CONTROLLER.PlayModeSelected != 0)
			{
				int num = Singleton<PaymentProcess>.instance.GenerateAmount();
				SavePlayerPrefs.SaveUserTickets(-num, num, 0);
			}
			ObscuredPrefs.SetInt("QPPaid", 1);
			CONTROLLER.PlayModeSelected = 0;
			Singleton<GroundController>.instance.ResetAll();
			Singleton<GameModel>.instance.ResetVariables();
			HideMe();
			Time.timeScale = 1f;
			CONTROLLER.screenToDisplay = "TossPage";
			CONTROLLER.NewInnings = true;
			CONTROLLER.InningsCompleted = false;
			//Singleton<Firebase_Events>.instance.Firebase_QP_Mode();
			//Singleton<Firebase_Events>.instance.Firebase_QPReplay_Mode();
			Singleton<GameModel>.instance.GameQuitted();
		}
		else
		{
			Singleton<GameOverScreen>.instance.GameQuit(1);
		}
	}

	public void ValidateLeaderboardPosition()
	{
		positionChanged.text = Mathf.Abs(Server_Connection.instance.diff_Xp).ToString();
		positionChanged2.text = Mathf.Abs(Server_Connection.instance.diff_Xp).ToString();
		if (Server_Connection.instance.diff_Xp > 0)
		{
			up.gameObject.SetActive(value: true);
			LBdescription.text = LocalizationData.instance.getText(297) + "!";
			down.gameObject.SetActive(value: false);
			up2.gameObject.SetActive(value: true);
			down2.gameObject.SetActive(value: false);
			rankIncreased1.SetActive(value: true);
			rankIncreased2.SetActive(value: true);
		}
		else if (Server_Connection.instance.diff_Xp == 0)
		{
			LBdescription.text = LocalizationData.instance.getText(534);
			rankIncreased1.SetActive(value: false);
			rankIncreased2.SetActive(value: false);
		}
		else
		{
			down.gameObject.SetActive(value: true);
			LBdescription.text = LocalizationData.instance.getText(287);
			up.gameObject.SetActive(value: false);
			down2.gameObject.SetActive(value: true);
			up2.gameObject.SetActive(value: false);
			rankIncreased1.SetActive(value: true);
			rankIncreased2.SetActive(value: true);
		}
	}

	private IEnumerator SyncPoints()
	{
		if (CONTROLLER.userID != 0)
		{
			yield return Server_Connection.instance.User_Leaderboard_Sync();
			Server_Connection.instance.Get_XPRank();
			Server_Connection.instance.Get_User_Sync();
		}
	}

	public void ValidateXPMilestones()
	{
		startingXP = tempXP;
		endingXP = startingXP + totalExpInaMatch + (int)XPGained;
		if (!alreadySavedXP)
		{
			SavePlayerPrefs.SaveUserXPs(totalExpInaMatch + (int)XPGained, totalExpInaMatch + (int)XPGained);
			alreadySavedXP = true;
			StartCoroutine(SyncPoints());
		}
		if (ObscuredPrefs.HasKey("XPMilestoneIndex"))
		{
			milestoneIndex = ObscuredPrefs.GetInt("XPMilestoneIndex");
		}
		else
		{
			milestoneIndex = 0;
			amountToReduce = 0;
		}
		matchXp1.text = "/ " + xpAmount[milestoneIndex];
		coins.text = coinAmount.ToString();
		AchievementCoins.text = coinAmount.ToString();
		if (coinAmount == 0)
		{
			achievementPresent.SetActive(value: false);
		}
		else
		{
			achievementPresent.SetActive(value: true);
		}
		AnimateXPbar();
	}

	private void GenerateXPGained()
	{
		if (matchStatus == "Win")
		{
			XPGained = (float)(CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 100) * difficultyMultiplier;
		}
	}

	public void TempOpen()
	{
		leftBtn.SetActive(value: false);
		rightBtn.SetActive(value: false);
		screenTwo.SetActive(value: false);
		CONTROLLER.pageName = "screen3";
		screenThree.SetActive(value: true);
		AnimationAchieve.CoinsTopPanel.text = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey).ToString();
		AnimationAchieve.TicketTopPanel.text = ObscuredPrefs.GetInt(CONTROLLER.TicketsKey).ToString();
		AnimationAchieve.XpTopPanel.text = ObscuredPrefs.GetInt(CONTROLLER.XPKey).ToString();
	}

	public void OpenAchievements()
	{
		//Singleton<AdIntegrate>.instance.HideAd();
		if (Singleton<Google_SignIn>.instance.googleAuthenticated == 1)
		{
			leftBtn.SetActive(value: false);
			rightBtn.SetActive(value: false);
			Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
			Singleton<NavigationBack>.instance.deviceBack = CloseAchievements;
			screenTwo.SetActive(value: false);
			CONTROLLER.pageName = "screen3";
			screenThree.SetActive(value: true);
			AnimationAchieve.CoinsTopPanel.text = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey).ToString();
			AnimationAchieve.TicketTopPanel.text = ObscuredPrefs.GetInt(CONTROLLER.TicketsKey).ToString();
			AnimationAchieve.XpTopPanel.text = ObscuredPrefs.GetInt(CONTROLLER.XPKey).ToString();
		}
		else
		{
			CONTROLLER.PopupName = "googlesignin";
			Singleton<Popups>.instance.ShowMe();
		}
	}

	public void CloseAchievements()
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			Singleton<TMGameOverDisplay>.instance.CloseAchievements();
			return;
		}
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		//Singleton<AdIntegrate>.instance.ShowAd();
		leftBtn.SetActive(value: true);
		rightBtn.SetActive(value: true);
		screenTwo.SetActive(value: true);
		CONTROLLER.pageName = "screen2";
		screenThree.SetActive(value: false);
	}

	private void AnimateXPbar()
	{
		float fillAmount = (float)startingXP / (float)xpAmount[milestoneIndex];
		XPFill.fillAmount = fillAmount;
		float endValue = (float)endingXP / (float)xpAmount[milestoneIndex];
		XPFill.DOFillAmount(endValue, 2f).SetUpdate(isIndependentUpdate: true);
		matchXp.DOText(endingXP.ToString(), 2f, richTextEnabled: true, ScrambleMode.Numerals).SetUpdate(isIndependentUpdate: true);
	}

	public void ScreenThreeDetails()
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

	public void ScreenFourDetails(int teamIndex)
	{
		extras.text = CONTROLLER.TeamList[teamIndex].currentMatchExtras.ToString();
		score.text = CONTROLLER.TeamList[teamIndex].currentMatchScores.ToString();
		if (CONTROLLER.TeamList[teamIndex].currentMatchWideBall + CONTROLLER.TeamList[teamIndex].currentMatchNoball != CONTROLLER.TeamList[teamIndex].currentMatchExtras)
		{
			extrasSubText.text = "(W " + (CONTROLLER.TeamList[teamIndex].currentMatchExtras - CONTROLLER.TeamList[teamIndex].currentMatchNoball) + ", NB " + CONTROLLER.TeamList[teamIndex].currentMatchNoball + ")";
		}
		else
		{
			extrasSubText.text = "(W " + CONTROLLER.TeamList[teamIndex].currentMatchWideBall + ", NB " + CONTROLLER.TeamList[teamIndex].currentMatchNoball + ")";
		}
		int num = CONTROLLER.TeamList[teamIndex].currentMatchBalls / 6;
		int num2 = CONTROLLER.TeamList[teamIndex].currentMatchBalls % 6;
		string text = num + "." + num2;
		scoreSubText.text = "(" + CONTROLLER.TeamList[teamIndex].currentMatchWickets + " wkts, " + text + " ov)";
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

	private void DecidePageToShow()
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			ObscuredPrefs.SetInt("QPPaid", 0);
			CONTROLLER.screenToDisplay = "landingPage";
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			ObscuredPrefs.SetInt("T20Paid", 0);
			if (matchStatus == "Win")
			{
				CONTROLLER.screenToDisplay = "fixtures";
			}
			else
			{
				ObscuredPrefs.DeleteKey("tour");
				ObscuredPrefs.SetInt("T20TeamsSelected", 0);
				CONTROLLER.screenToDisplay = "landingPage";
				replayBtnText.transform.parent.gameObject.SetActive(value: false);
			}
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			ObscuredPrefs.SetInt("NPLPaid", 0);
			if (GameOverScreen.stage == 0)
			{
				if (Singleton<TournamentFailedPopUp>.instance.showMe)
				{
					CONTROLLER.screenToDisplay = "landingPage";
					ObscuredPrefs.SetInt(CONTROLLER.tournamentType + "TeamsSelected", 0);
					replayBtnText.transform.parent.gameObject.SetActive(value: false);
				}
				else
				{
					CONTROLLER.screenToDisplay = "NPLLeague";
				}
			}
			else if (matchStatus == "Win")
			{
				CONTROLLER.screenToDisplay = "NPLfixtures";
			}
			else if (GameOverScreen.stage == 1)
			{
				if (GameOverScreen.nplPlayoffs == 0)
				{
					CONTROLLER.screenToDisplay = "NPLfixtures";
				}
				else
				{
					replayBtnText.transform.parent.gameObject.SetActive(value: false);
					CONTROLLER.screenToDisplay = "landingPage";
					ObscuredPrefs.SetInt(CONTROLLER.tournamentType + "TeamsSelected", 0);
				}
			}
			else
			{
				replayBtnText.transform.parent.gameObject.SetActive(value: false);
				CONTROLLER.screenToDisplay = "landingPage";
				ObscuredPrefs.SetInt(CONTROLLER.tournamentType + "TeamsSelected", 0);
			}
			CONTROLLER.NPLIndiaTournamentStage = 0;
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			ObscuredPrefs.SetInt("WCPaid", 0);
			if (GameOverScreen.stage == 0)
			{
				if (Singleton<TournamentFailedPopUp>.instance.showMe)
				{
					replayBtnText.transform.parent.gameObject.SetActive(value: false);
					ObscuredPrefs.SetInt("WCTeamsSelected", 0);
					CONTROLLER.screenToDisplay = "landingPage";
				}
				else
				{
					CONTROLLER.screenToDisplay = "WCLeague";
				}
			}
			else if (GameOverScreen.stage == 3)
			{
				replayBtnText.transform.parent.gameObject.SetActive(value: false);
				ObscuredPrefs.SetInt("WCTeamsSelected", 0);
				CONTROLLER.screenToDisplay = "landingPage";
			}
			else if (matchStatus == "Win")
			{
				CONTROLLER.screenToDisplay = "WCfixtures";
			}
			else
			{
				replayBtnText.transform.parent.gameObject.SetActive(value: false);
				ObscuredPrefs.SetInt("WCTeamsSelected", 0);
				CONTROLLER.screenToDisplay = "landingPage";
			}
			CONTROLLER.WCTournamentStage = 0;
		}
		if (GameOverScreen.stage == 3)
		{
			if (CONTROLLER.PlayModeSelected == 1)
			{
				ObscuredPrefs.DeleteKey("tour");
				ObscuredPrefs.SetInt("T20TeamsSelected", 0);
			}
			else if (CONTROLLER.PlayModeSelected == 2)
			{
				ObscuredPrefs.SetInt(CONTROLLER.tournamentType + "TeamsSelected", 0);
			}
			else if (CONTROLLER.PlayModeSelected == 3)
			{
				ObscuredPrefs.SetInt("WCTeamsSelected", 0);
			}
			CONTROLLER.screenToDisplay = "landingPage";
		}
		if (CONTROLLER.screenToDisplay == "landingPage" && CONTROLLER.PlayModeSelected != 0 && myTeamScore < oppTeamScore)
		{
			Singleton<TournamentFailedPopUp>.instance.bottomComponent.SetActive(value: false);
			Singleton<TournamentFailedPopUp>.instance.showMe = true;
		}
	}

	private void AlignImages()
	{
		if (CONTROLLER.PlayModeSelected != 0 && GameOverScreen.stage == 3)
		{
			PLWon.SetActive(value: true);
			dynamicImage.sprite = dynamicSprites[2];
			if (CONTROLLER.PlayModeSelected != 2)
			{
				cupImg.GetComponent<Image>().sprite = cupImgSprite[CONTROLLER.PlayModeSelected];
			}
			else if (CONTROLLER.tournamentType == "NPL")
			{
				WinningCupImg.sprite = cupImgSprite[CONTROLLER.PlayModeSelected];
			}
			else if (CONTROLLER.tournamentType == "PAK")
			{
				WinningCupImg.sprite = cupImgSprite[4];
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				WinningCupImg.sprite = cupImgSprite[5];
			}
			WinningCupImg.gameObject.SetActive(value: true);
			dynamicImage.SetNativeSize();
			dynamicImage.gameObject.transform.localPosition = new Vector3(-63.5f, 0f, 0f);
			if (myTeamScore >= oppTeamScore)
			{
				PLWon.GetComponent<Text>().text = LocalizationData.instance.getText(535) + "\n" + LocalizationData.instance.getText(536) + "!";
			}
			else
			{
				PLWon.GetComponent<Text>().text = LocalizationData.instance.getText(537) + "\n" + LocalizationData.instance.getText(536) + "!";
			}
			MatchStatsSubImg.SetActive(value: false);
			bottomWidget.SetActive(value: false);
		}
		else
		{
			WinningCupImg.gameObject.SetActive(value: false);
			if (myTeamScore >= oppTeamScore)
			{
				dynamicImage.sprite = dynamicSprites[0];
				dynamicImage.SetNativeSize();
				dynamicImage.gameObject.transform.localPosition = new Vector3(-253f, 0f, 0f);
			}
			else
			{
				dynamicImage.sprite = dynamicSprites[1];
				dynamicImage.SetNativeSize();
				dynamicImage.gameObject.transform.localPosition = new Vector3(180f, 0f, 0f);
			}
		}
	}

	public void MoveToPreviousPage()
	{
		if (!isAnimating)
		{
			if (pageNumber == 2)
			{
				leftBtn.SetActive(value: false);
				mainScreen = screenTwo;
				sideScreen = screenOne;
			}
			else if (pageNumber == 3)
			{
				mainScreen = screenFour;
				sideScreen = screenTwo;
			}
			if (pageNumber > 1)
			{
				pageNumber--;
				isAnimating = true;
				sideScreenPos = -1500f;
				mainScreenPos = 1500f;
				AnimatePageMovement();
			}
		}
	}

	public void MoveToNextPage()
	{
		if (!isAnimating)
		{
			if (pageNumber == 1)
			{
				leftBtn.SetActive(value: true);
				sideScreen = screenTwo;
				mainScreen = screenOne;
			}
			else if (pageNumber == 2)
			{
				Singleton<MatchSummary>.instance.SortPlayerForSummary(0);
				sideScreen = screenFour;
				mainScreen = screenTwo;
			}
			else if (pageNumber == 3)
			{
				Singleton<GameOverScreen>.instance.GameQuit(1);
			}
			if (pageNumber < 3)
			{
				isAnimating = true;
				Singleton<NavigationBack>.instance.deviceBack = MoveToPreviousPage;
				pageNumber++;
				sideScreenPos = 1500f;
				mainScreenPos = -1500f;
				AnimatePageMovement();
			}
		}
	}

	private void ScreenCleanup()
	{
		isAnimating = false;
		mainScreen.SetActive(value: false);
		mainScreen.transform.DOLocalMoveX(0f, 0f);
	}

	private void ScreenOneAnimation()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, TopImage.DOScale(Vector3.one * 1.2f, 0.75f));
		sequence.Insert(0.75f, TopImage.DOScale(Vector3.one, 0.1f));
		sequence.OnComplete(LeaderBoardAnim);
		sequence.SetUpdate(isIndependentUpdate: true);
		ShineAnim();
		Sequence s = DOTween.Sequence();
		s.Insert(0.5f, PUIcon.DOPunchRotation(new Vector3(0f, 0f, 8f), 1f, 20, 0.8f)).SetLoops(-1).SetUpdate(isIndependentUpdate: true);
		s.Insert(0.5f, PUDesc.DOScale(Vector3.one * 1.1f, 1f)).SetLoops(-1, LoopType.Yoyo).SetUpdate(isIndependentUpdate: true);
		s.Insert(0.5f, PUIcon.GetComponent<Image>().DOColor(Color.green, 1f)).SetLoops(-1).SetUpdate(isIndependentUpdate: true);
		s.Insert(0.5f, PUIcon2.DOPunchRotation(new Vector3(0f, 0f, 8f), 1f, 20, 0.8f)).SetLoops(-1).SetUpdate(isIndependentUpdate: true);
		s.Insert(0.5f, PUIcon2.GetComponent<Image>().DOColor(Color.green, 1f)).SetLoops(-1).SetUpdate(isIndependentUpdate: true);
	}

	private void ScreenTwoAnimation()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, TopImage.DOScale(Vector3.one * 1.2f, 1f));
		sequence.Insert(1f, TopImage.DOScale(Vector3.one, 0.1f));
		sequence.OnComplete(LeaderBoardAnim);
		sequence.SetUpdate(isIndependentUpdate: true);
		ShineAnim();
	}

	private void LeaderBoardAnim()
	{
		int num = 1;
		num = 1;
		if (Singleton<Google_SignIn>.instance.googleAuthenticated == num)
		{
			canShowLeaderboard = true;
			LeaderboardPosition.SetActive(value: true);
			LeaderboardTransform2.gameObject.SetActive(value: true);
		}
		else
		{
			canShowLeaderboard = false;
			LeaderboardPosition.SetActive(value: false);
			LeaderboardTransform2.gameObject.SetActive(value: false);
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
		if (showUpgrade)
		{
			UpgradeTransform.DOScale(Vector3.one, 0.5f).SetUpdate(isIndependentUpdate: true);
		}
	}

	private void ShineAnim()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(0.75f, shine.transform.DOLocalMoveX(240f, 1.5f)).SetLoops(-1).SetUpdate(isIndependentUpdate: true);
	}

	private void AnimatePageMovement()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, sideScreen.transform.DOLocalMoveX(sideScreenPos, 0f));
		sideScreen.SetActive(value: true);
		sequence.Insert(0f, mainScreen.transform.DOLocalMoveX(mainScreenPos, 0.5f));
		sequence.Insert(0.1f, sideScreen.transform.DOLocalMoveX(0f, 0.75f));
		sequence.SetUpdate(isIndependentUpdate: true);
		sequence.OnComplete(ScreenCleanup);
	}

	public void ScreenTransition(int index)
	{
		switch (index)
		{
		case 0:
			CONTROLLER.pageName = "screen2";
			screenTwo.SetActive(value: true);
			screenOne.SetActive(value: false);
			ScreenTwoDetails();
			break;
		case 1:
			CONTROLLER.pageName = "screen4";
			Singleton<MatchSummary>.instance.SortPlayerForSummary(0);
			screenTwo.SetActive(value: false);
			screenThree.SetActive(value: false);
			screenFour.SetActive(value: true);
			break;
		case 2:
			CONTROLLER.pageName = "screen4";
			Singleton<MatchSummary>.instance.SortPlayerForSummary(0);
			screenThree.SetActive(value: false);
			screenFour.SetActive(value: true);
			break;
		case -1:
			CONTROLLER.pageName = "screen1";
			screenTwo.SetActive(value: false);
			screenOne.SetActive(value: true);
			break;
		case -2:
			CONTROLLER.pageName = "screen2";
			screenThree.SetActive(value: false);
			screenTwo.SetActive(value: true);
			break;
		case -3:
			CONTROLLER.pageName = "screen2";
			screenFour.SetActive(value: false);
			screenTwo.SetActive(value: true);
			ScreenTwoDetails();
			break;
		}
	}

	private void MatchWonBy(int index)
	{
		if (myTeamScore == oppTeamScore)
		{
			if (index == 0)
			{
				matchWonBy.text = string.Empty;
			}
			else
			{
				matchWonBy.text = string.Empty;
			}
		}
		else if (index == 0)
		{
			if (CONTROLLER.meFirstBatting == 1)
			{
				matchWonBy.text = LocalizationData.instance.getText(471);
			}
			else
			{
				matchWonBy.text = LocalizationData.instance.getText(474);
			}
		}
		else if (CONTROLLER.meFirstBatting == 0)
		{
			matchWonBy.text = LocalizationData.instance.getText(471);
		}
		else
		{
			matchWonBy.text = LocalizationData.instance.getText(474);
		}
	}

	public void ShowMe()
	{
		GroundController.QPMatchStart = false;
		LBdescription.text = LocalizationData.instance.getText(534);
		rankIncreased1.SetActive(value: false);
		rankIncreased2.SetActive(value: false);
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	bottomWidget.SetActive(value: true);
		//	if (Singleton<Google_SignIn>.instance.googleAuthenticated == 1)
		//	{
		//		canShowAchievement = true;
		//		AchievementTransform.gameObject.SetActive(value: true);
		//	}
		//	else
		//	{
		//		canShowAchievement = false;
		//		AchievementTransform.gameObject.SetActive(value: false);
		//	}
		//}
		//else
		//{
		//	LeaderboardTransform2.gameObject.SetActive(value: false);
		//	bottomWidget.SetActive(value: false);
		//}
		if (GameOverScreen.stage == 3)
		{
			bottomWidget.SetActive(value: false);
		}
		PLWon.SetActive(value: false);
		leftBtn.SetActive(value: false);
		pageNumber = 1;
		ObscuredPrefs.DeleteKey("perMatchEdgeCount" + CONTROLLER.PlayModeSelected);
		Singleton<NavigationBack>.instance.deviceBack = null;
		SavePlayerPrefs.SaveSixCount();
		tempXP = CONTROLLER.XPs;
		Singleton<PauseGameScreen>.instance.Hide(boolean: true);
		CONTROLLER.pageName = string.Empty;
		CalculateXPforCurrentMatch();
		myTeamScore = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores;
		oppTeamScore = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores;
		myTeamWicket = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets;
		oppTeamWicket = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchWickets;
		if (myTeamScore > oppTeamScore)
		{
			cupImg.SetActive(value: false);
			MatchStatsSubImg.SetActive(value: true);
			matchStatus = "Win";
			MatchStatsSubImg.GetComponent<Image>().sprite = WinTextSprite[0];
			sideTransform.localPosition = new Vector3(246f, sideTransform.localPosition.y, sideTransform.localPosition.z);
			KitTable.SetKitValues(5);
		}
		else if (myTeamScore < oppTeamScore)
		{
			cupImg.SetActive(value: false);
			MatchStatsSubImg.GetComponent<Image>().sprite = WinTextSprite[2];
			sideTransform.localPosition = new Vector3(-246f, sideTransform.localPosition.y, sideTransform.localPosition.z);
			MatchStatsSubImg.SetActive(value: true);
			matchStatus = "Loss";
		}
		else
		{
			MatchStatsSubImg.SetActive(value: true);
			cupImg.SetActive(value: false);
			MatchStatsSubImg.GetComponent<Image>().sprite = WinTextSprite[1];
			sideTransform.localPosition = new Vector3(246f, sideTransform.localPosition.y, sideTransform.localPosition.z);
			matchStatus = "Tie";
		}
		if (GameOverScreen.stage != 3 || myTeamScore <= oppTeamScore || CONTROLLER.PlayModeSelected != 0)
		{
		}
		if (Singleton<GameModel>.instance.CheckIfAlreadyAchieved(15) && matchStatus == "Win" && !CONTROLLER.isAutoPlayed)
		{
			Singleton<AchievementsSyncronizer>.instance.UpdateAchievement(15);
			AchievementTable.SetAchievementValues(15);
		}
		if (CONTROLLER.PlayModeSelected == 0)
		{
			replayBtnText.text = LocalizationData.instance.getText(276);
		}
		else
		{
			replayBtnText.text = LocalizationData.instance.getText(416);
		}
		if (GameOverScreen.stage == 3)
		{
			replayBtnText.transform.parent.gameObject.SetActive(value: false);
		}
		holder.SetActive(value: true);
		screenOne.SetActive(value: true);
		ShowAvailableUpgrade();
		XPGeneralAwards();
		ScreenOneDetails();
		ScreenOneAnimation();
		//SendToFirebase();
		AlignImages();
	}

	public void HideMe()
	{
		holder.SetActive(value: false);
		screenOne.SetActive(value: false);
		screenTwo.SetActive(value: false);
		screenThree.SetActive(value: false);
		screenFour.SetActive(value: false);
	}

	//private void SendToFirebase()
	//{
	//	if (CONTROLLER.PlayModeSelected == 0)
	//	{
	//		Singleton<Firebase_Events>.instance.Firebase_QP_GameOver();
	//	}
	//	else if (CONTROLLER.PlayModeSelected == 1)
	//	{
	//		Singleton<Firebase_Events>.instance.Firebase_T20_GameOver();
	//	}
	//	else if (CONTROLLER.PlayModeSelected == 2)
	//	{
	//		Singleton<Firebase_Events>.instance.Firebase_PRL_GameOver();
	//	}
	//	else if (CONTROLLER.PlayModeSelected == 3)
	//	{
	//		Singleton<Firebase_Events>.instance.Firebase_WC_GameOver();
	//	}
	//	if (GameModel.RunRate <= 10f && !(CONTROLLER.difficultyMode == "easy") && !(CONTROLLER.difficultyMode == "medium") && !(CONTROLLER.difficultyMode == "hard"))
	//	{
	//	}
	//}

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
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		holder.SetActive(value: true);
		if (pageNumber == 2)
		{
			screenTwo.SetActive(value: true);
		}
		else
		{
			screenOne.SetActive(value: true);
		}
	}

	public void openLeaderboard()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		//Singleton<LeaderBoard>.instance.ShowMe();
	}

	public void ContentScrollDown()
	{
		ScoreCardContent.transform.DOLocalMove(new Vector3(0f, 150f, 0f), 0.5f).SetUpdate(isIndependentUpdate: true);
	}

	public void XPGeneralAwards()
	{
		if (GameOverScreen.stage == 3)
		{
			int num = 0;
			if ((CONTROLLER.PlayModeSelected == 1 || CONTROLLER.PlayModeSelected == 2) && CONTROLLER.difficultyMode == "easy" && (matchStatus == "Win" || matchStatus == "Tie"))
			{
				num = 6000;
			}
			else if ((CONTROLLER.PlayModeSelected == 1 || CONTROLLER.PlayModeSelected == 2) && CONTROLLER.difficultyMode == "medium" && (matchStatus == "Win" || matchStatus == "Tie"))
			{
				num = 7200;
			}
			else if ((CONTROLLER.PlayModeSelected == 1 || CONTROLLER.PlayModeSelected == 2) && CONTROLLER.difficultyMode == "hard" && (matchStatus == "Win" || matchStatus == "Tie"))
			{
				num = 9600;
			}
			else if ((CONTROLLER.PlayModeSelected == 1 || CONTROLLER.PlayModeSelected == 2) && CONTROLLER.difficultyMode == "easy" && matchStatus == "Loss")
			{
				num = 2000;
			}
			else if ((CONTROLLER.PlayModeSelected == 1 || CONTROLLER.PlayModeSelected == 2) && CONTROLLER.difficultyMode == "medium" && matchStatus == "Loss")
			{
				num = 2400;
			}
			else if ((CONTROLLER.PlayModeSelected == 1 || CONTROLLER.PlayModeSelected == 2) && CONTROLLER.difficultyMode == "hard" && matchStatus == "Loss")
			{
				num = 3200;
			}
			else if (CONTROLLER.PlayModeSelected == 3 && CONTROLLER.difficultyMode == "easy" && (matchStatus == "Win" || matchStatus == "Tie"))
			{
				num = 15000;
			}
			else if (CONTROLLER.PlayModeSelected == 3 && CONTROLLER.difficultyMode == "medium" && (matchStatus == "Win" || matchStatus == "Tie"))
			{
				num = 18000;
			}
			else if (CONTROLLER.PlayModeSelected == 3 && CONTROLLER.difficultyMode == "hard" && (matchStatus == "Win" || matchStatus == "Tie"))
			{
				num = 24000;
			}
			else if (CONTROLLER.PlayModeSelected == 3 && CONTROLLER.difficultyMode == "easy" && matchStatus == "Loss")
			{
				num = 5000;
			}
			else if (CONTROLLER.PlayModeSelected == 3 && CONTROLLER.difficultyMode == "medium" && matchStatus == "Loss")
			{
				num = 6000;
			}
			else if (CONTROLLER.PlayModeSelected == 3 && CONTROLLER.difficultyMode == "hard" && matchStatus == "Loss")
			{
				num = 8000;
			}
			totalExpInaMatch += num;
		}
	}

	public void ShowAvailableUpgrade()
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string[] array = new string[3] { "298", "299", "300" };
		int num = 0;
		int num2 = -1;
		int num3 = 1;
		num3 = 1;
		if (Singleton<Google_SignIn>.instance.googleAuthenticated == num3)
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
				text = LocalizationData.instance.getText(int.Parse(array[num2]));
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
		if (showUpgrade)
		{
			UpgradeTransform.gameObject.SetActive(value: true);
			UpgradeTransform2.gameObject.SetActive(value: true);
			upgradeAmountText.text = text2;
			upgradeNameText.text = text;
			upgradeAmountText2.text = text2;
			upgradeNameText2.text = text;
		}
		else
		{
			UpgradeTransform.gameObject.SetActive(value: false);
			UpgradeTransform2.gameObject.SetActive(value: false);
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
