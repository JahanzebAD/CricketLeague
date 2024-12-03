using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TMGameOverDisplay : Singleton<TMGameOverDisplay>
{
	public GameObject holder;

	public GameObject screenOne;

	public GameObject screenTwo;

	public GameObject leftBtn;

	public GameObject rightBtn;

	private bool isAnimating;

	private GameObject sideScreen;

	private GameObject mainScreen;

	private float sideScreenPos;

	private float mainScreenPos;

	private int pageNumber;

	[Header("FirstPage")]
	public Text BeatByText;

	[Header("FirstPage")]
	public Text MatchWonTeamText;

	[Header("FirstPage")]
	public Text TeamName;

	public Image TeamFlag;

	public Image CupImg;

	public Image bg;

	public Image matchStatus;

	public Sprite[] CupSprites;

	public Sprite[] matchStatusSprite;

	public Sprite[] bgSprite;

	public string winStatus;

	[Header("SecondPage")]
	private int XPFromFour;

	[Header("SecondPage")]
	private int XPFromMaiden;

	[Header("SecondPage")]
	private int XPFromDot;

	[Header("SecondPage")]
	private int XPFromSix;

	[Header("SecondPage")]
	private int XPFromWicketBowled;

	[Header("SecondPage")]
	private int XPFromWicketCatch;

	[Header("SecondPage")]
	private int XPFromWicketOthers;

	[Header("SecondPage")]
	private int XPFromFifty;

	[Header("SecondPage")]
	private int XPFromCentury;

	[Header("SecondPage")]
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

	public Image XpFill;

	public Text TotalXp;

	public Text CoinsEarned;

	public Text Coin1;

	public Text Coin2;

	public Text Coin3;

	private float oversMultiplier = 1f;

	private float difficultyMultiplier = 1f;

	private float diffMultiplierCoins = 1f;

	private int CoinsEarnedTotal;

	private int CoinsEarnedPerDay;

	private int CoinsEarnedForWinning;

	private int[] baseValues = new int[4] { 750, 1500, 3000, 3750 };

	private int[] xpAmount = new int[34]
	{
		1000, 2500, 5000, 7500, 10000, 12500, 25000, 50000, 75000, 100000,
		125000, 150000, 175000, 200000, 250000, 300000, 350000, 400000, 450000, 500000,
		550000, 600000, 650000, 700000, 750000, 800000, 850000, 900000, 950000, 1000000,
		1500000, 2000000, 2500000, 5000000
	};

	private int milestoneIndex;

	private void SendToFirebase()
	{
		//Singleton<Firebase_Events>.instance.Firebase_TC_GameOver();
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
		totalCoinsInaMatch = CoinsFromFour + CoinsFromMaiden + CoinsFromDot + CoinsFromSix + CoinsFromWicketBowled + CoinsFromWicketCatch + CoinsFromWicketOthers + CoinsFromFifty + CoinsFromCentury;
		SavePlayerPrefs.SaveUserCoins(totalCoinsInaMatch, 0, totalCoinsInaMatch);
		SavePlayerPrefs.SaveUserXP();
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

	private string ReplaceText(string original, string replace1, string replace2)
	{
		string text = string.Empty;
		Debug.Log(original + " " + replace1 + " " + replace2 + " " + text);
		if (original.Contains("#"))
		{
			text = original.Replace("#", replace1);
		}
		Debug.Log(original + " " + replace1 + " " + replace2 + " " + text);
		if (text.Contains("$"))
		{
			text = text.Replace("$", replace2);
		}
		Debug.Log(original + " " + replace1 + " " + replace2 + " " + text);
		return text;
	}

	private void DisPlayResult()
	{
		int num = -1;
		string text = string.Empty;
		int num2 = 0;
		int num3 = 0;
		string teamName = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].teamName;
		string teamName2 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].teamName;
		int num4 = 0;
		int num5 = 0;
		bool flag = false;
		if (CONTROLLER.currentDay == CONTROLLER.MaxDays)
		{
			if (CONTROLLER.currentInnings == 3)
			{
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= CONTROLLER.totalWickets)
				{
					flag = true;
				}
				else
				{
					for (int i = 0; i < CONTROLLER.currentTestInnings.Length; i++)
					{
						if (i < 2)
						{
							if (CONTROLLER.BattingTeam[i] == CONTROLLER.myTeamIndex)
							{
								num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores1;
								num4 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchWickets1;
							}
							else
							{
								num3 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores1;
								num5 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchWickets1;
							}
						}
						else if (CONTROLLER.BattingTeam[i] == CONTROLLER.myTeamIndex)
						{
							num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores2;
							num4 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchWickets2;
						}
						else
						{
							num3 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores2;
							num5 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchWickets2;
						}
					}
					if (CONTROLLER.BattingTeam[3] == CONTROLLER.myTeamIndex)
					{
						if (num2 < num3)
						{
							num2 = (num3 = 0);
						}
					}
					else if (num2 > num3)
					{
						num2 = (num3 = 0);
					}
				}
			}
			else if (CONTROLLER.currentInnings == 2)
			{
				int num6 = CONTROLLER.totalOvers * 6 - CONTROLLER.ballsBowledPerDay;
				int num7 = 0;
				for (int j = 0; j <= CONTROLLER.currentInnings; j++)
				{
					num7++;
				}
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= CONTROLLER.totalWickets)
				{
					flag = true;
					if (num7 < 4 && num6 >= 0)
					{
						int num8 = 0;
						int num9 = 0;
						for (int k = 0; k <= CONTROLLER.currentInnings; k++)
						{
							if (k < 2)
							{
								if (CONTROLLER.BattingTeam[k] == CONTROLLER.myTeamIndex)
								{
									num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchScores1;
									num4 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchWickets1;
								}
								else
								{
									num3 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchScores1;
									num5 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchWickets1;
								}
							}
							else if (CONTROLLER.BattingTeam[k] == CONTROLLER.myTeamIndex)
							{
								num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchScores2;
								num4 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchWickets2;
							}
							else
							{
								num3 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchScores2;
								num5 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchWickets2;
							}
						}
						flag = (num9 == 1 && num2 < num3) || (num9 == 2 && num2 > num3) || (num8 == 1 && num3 < num2) || ((num8 == 2 && num3 > num2) ? true : false);
					}
					num2 = 0;
					num3 = 0;
				}
			}
		}
		else if (CONTROLLER.currentInnings == 2 || CONTROLLER.currentInnings == 3)
		{
			flag = true;
		}
		else if (CONTROLLER.isFollowOn)
		{
			if (CONTROLLER.currentInnings < 2)
			{
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets1 >= CONTROLLER.totalWickets)
				{
					flag = true;
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= CONTROLLER.totalWickets)
			{
				flag = true;
			}
		}
		if (flag)
		{
			for (int l = 0; l < CONTROLLER.currentTestInnings.Length; l++)
			{
				if (l < 2)
				{
					if (CONTROLLER.BattingTeam[l] == CONTROLLER.myTeamIndex)
					{
						num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchScores1;
						num4 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchWickets1;
					}
					else
					{
						num3 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchScores1;
						num5 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchWickets1;
					}
				}
				else if (CONTROLLER.BattingTeam[l] == CONTROLLER.myTeamIndex)
				{
					num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchScores2;
					num4 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchWickets2;
				}
				else
				{
					num3 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchScores2;
					num5 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchWickets2;
				}
			}
		}
		if (num2 > num3)
		{
			KitTable.SetKitValues(5);
			winStatus = "Win";
			matchStatus.gameObject.SetActive(value: true);
			matchStatus.sprite = matchStatusSprite[0];
			bg.sprite = bgSprite[0];
			CupImg.sprite = CupSprites[0];
			BeatByText.transform.localPosition = new Vector3(170f, BeatByText.transform.localPosition.y, BeatByText.transform.localPosition.z);
			if (CONTROLLER.BattingTeam[CONTROLLER.currentInnings] == CONTROLLER.myTeamIndex)
			{
				if (CONTROLLER.totalWickets - num4 <= 1)
				{
					BeatByText.text = "YOU WON BY " + (CONTROLLER.totalWickets - num4) + " WICKET.";
					text = " WON by " + (CONTROLLER.totalWickets - num4) + " wicket.";
				}
				else
				{
					BeatByText.text = "YOU WON BY " + (CONTROLLER.totalWickets - num4) + " WICKETS.";
					text = " WON by " + (CONTROLLER.totalWickets - num4) + " wickets.";
				}
			}
			else if (num2 - num3 <= 1)
			{
				BeatByText.text = "YOU WON BY " + (num2 - num3) + " RUN";
				text = "YOU WON BY " + (num2 - num3) + " RUN";
				if (CONTROLLER.currentInnings == 2 && CONTROLLER.tournamentType != "WorldTour")
				{
					BeatByText.text += " & AN INNINGS.";
					text += " & an inngs.";
				}
				else if (CONTROLLER.currentInnings == 2 && CONTROLLER.tournamentType == "WorldTour")
				{
					BeatByText.text += " AND AN INNINGS.";
					text += " and an inngs.";
				}
				else
				{
					BeatByText.text += ".";
					text += ".";
				}
			}
			else
			{
				BeatByText.text = "YOU WON BY " + (num2 - num3) + " RUNS";
				text = "WON by " + (num2 - num3) + " runs";
				if (CONTROLLER.currentInnings == 2 && CONTROLLER.tournamentType != "WorldTour")
				{
					BeatByText.text += " & AN INNINGS.";
					text += " & an inngs.";
				}
				else if (CONTROLLER.currentInnings == 2 && CONTROLLER.tournamentType == "WorldTour")
				{
					BeatByText.text += " AND AN INNINGS.";
					text += " and an inngs.";
				}
				else
				{
					BeatByText.text += ".";
					text += ".";
				}
			}
			MatchWonTeamText.text = teamName;
			num = CONTROLLER.myTeamIndex;
		}
		else if (num2 < num3)
		{
			winStatus = "Loss";
			matchStatus.gameObject.SetActive(value: false);
			bg.sprite = bgSprite[1];
			CupImg.sprite = CupSprites[1];
			BeatByText.transform.localPosition = new Vector3(-170f, BeatByText.transform.localPosition.y, BeatByText.transform.localPosition.z);
			if (CONTROLLER.BattingTeam[CONTROLLER.currentInnings] == CONTROLLER.opponentTeamIndex)
			{
				if (CONTROLLER.totalWickets - num5 <= 1)
				{
					BeatByText.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].teamName.ToUpper() + " WON BY " + (CONTROLLER.totalWickets - num5) + " WICKET.";
					text = " WON by " + (CONTROLLER.totalWickets - num5) + " wicket.";
				}
				else
				{
					BeatByText.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].teamName.ToUpper() + " WON BY " + (CONTROLLER.totalWickets - num5) + " WICKETS.";
					text = " WON by " + (CONTROLLER.totalWickets - num5) + " wickets.";
				}
			}
			else if (num3 - num2 <= 1)
			{
				BeatByText.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].teamName.ToUpper() + " WON BY " + (num3 - num2) + " RUN";
				text = " WON by " + (num3 - num2) + " run";
				if (CONTROLLER.currentInnings == 2 && CONTROLLER.tournamentType != "WorldTour")
				{
					BeatByText.text += " & AN INNINGS.";
					text += " & an inngs.";
				}
				else if (CONTROLLER.currentInnings == 2 && CONTROLLER.tournamentType == "WorldTour")
				{
					BeatByText.text += " AND AN INNINGS.";
					text += " and an inngs.";
				}
				else
				{
					BeatByText.text += ".";
					text += ".";
				}
			}
			else
			{
				BeatByText.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].teamName.ToUpper() + " WON BY " + (num3 - num2) + " RUNS";
				if (CONTROLLER.currentInnings == 2 && CONTROLLER.tournamentType != "WorldTour")
				{
					BeatByText.text += " & AN INNINGS.";
					text += " & an inngs.";
				}
				else if (CONTROLLER.currentInnings == 2 && CONTROLLER.tournamentType == "WorldTour")
				{
					BeatByText.text += " AND AN INNINGS.";
					text += " and an inngs.";
				}
				else
				{
					BeatByText.text += ".";
					text += ".";
				}
			}
			MatchWonTeamText.text = teamName2;
			num = CONTROLLER.opponentTeamIndex;
		}
		else
		{
			winStatus = "Tie";
			matchStatus.gameObject.SetActive(value: true);
			BeatByText.transform.localPosition = new Vector3(170f, BeatByText.transform.localPosition.y, BeatByText.transform.localPosition.z);
			bg.sprite = bgSprite[0];
			CupImg.sprite = CupSprites[2];
			for (int m = 0; m < CONTROLLER.currentTestInnings.Length; m++)
			{
				if (m < 2)
				{
					if (CONTROLLER.BattingTeam[m] == CONTROLLER.myTeamIndex)
					{
						num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[m]].TMcurrentMatchScores1;
					}
					else
					{
						num3 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[m]].TMcurrentMatchScores1;
					}
				}
				else if (CONTROLLER.BattingTeam[m] == CONTROLLER.myTeamIndex)
				{
					num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[m]].TMcurrentMatchScores2;
				}
				else
				{
					num3 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[m]].TMcurrentMatchScores2;
				}
			}
			if (num2 == num3)
			{
				matchStatus.sprite = matchStatusSprite[1];
				BeatByText.text = string.Empty;
			}
			else
			{
				matchStatus.sprite = matchStatusSprite[2];
				BeatByText.text = string.Empty;
				num = -2;
			}
			TeamFlag.gameObject.SetActive(value: false);
			MatchWonTeamText.gameObject.SetActive(value: false);
		}
		if (num != -1 && num != -2)
		{
			Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
			foreach (Sprite sprite in flags)
			{
				if (sprite.name.ToUpper() == CONTROLLER.TeamList[num].abbrevation)
				{
					TeamFlag.sprite = sprite;
				}
			}
		}
		UpdateTestMatchData(text, num);
		//SendToFirebase();
		AutoSave.DeleteFile();
	}

	private void UpdateTestMatchData(string result, int teamWonIndex)
	{
		string empty = string.Empty;
		if (CONTROLLER.tournamentType == "Ashes")
		{
			empty = "Ashes";
		}
		else
		{
			empty = string.Empty;
		}
		if (CONTROLLER.CurrentTestMatch < CONTROLLER.TotalTestMatches)
		{
			if (teamWonIndex == CONTROLLER.myTeamIndex)
			{
				CONTROLLER.MyTestWinCount++;
			}
			else if (teamWonIndex == CONTROLLER.opponentTeamIndex)
			{
				CONTROLLER.OppTestWinCount++;
			}
			CONTROLLER.CurrentTestMatch++;
			CONTROLLER.TestTeamWonIndexStr = CONTROLLER.TestTeamWonIndexStr + teamWonIndex + "$";
			CONTROLLER.StoredTestSeriesResult = CONTROLLER.StoredTestSeriesResult + result + "!";
			CONTROLLER.TestSeriesData = string.Empty + CONTROLLER.difficultyMode + "|" + CONTROLLER.myTeamIndex + "|" + CONTROLLER.opponentTeamIndex + "|" + CONTROLLER.TotalTestMatches + "|" + CONTROLLER.totalOvers + "|" + CONTROLLER.CurrentTestMatch + "|" + CONTROLLER.MyTestWinCount + "|" + CONTROLLER.OppTestWinCount + "|" + CONTROLLER.TestTeamWonIndexStr + "|" + CONTROLLER.StoredTestSeriesResult;
			if (CONTROLLER.tournamentType == "TestSeries")
			{
				PlayerPrefs.SetString("testinfo", CONTROLLER.TestSeriesData);
			}
			else if (CONTROLLER.tournamentType == "Ashes")
			{
				PlayerPrefs.SetString("AshesInfo", CONTROLLER.TestSeriesData);
			}
			CONTROLLER.CurrentPage = "testfixtures";
		}
		if (CONTROLLER.CurrentTestMatch >= CONTROLLER.TotalTestMatches)
		{
			if (PlayerPrefs.HasKey("testinfo") && CONTROLLER.tournamentType == "TestSeries")
			{
				PlayerPrefs.DeleteKey("testinfo");
				CONTROLLER.CurrentTestMatch = 0;
				CONTROLLER.TestTeamWonIndexStr = string.Empty;
				CONTROLLER.StoredTestSeriesResult = string.Empty;
				CONTROLLER.TestSeriesData = string.Empty;
				CONTROLLER.MyTestWinCount = 0;
				CONTROLLER.OppTestWinCount = 0;
				CONTROLLER.CurrentPage = "mainmenu";
			}
			if (PlayerPrefs.HasKey("AshesInfo") && CONTROLLER.tournamentType == "Ashes")
			{
				PlayerPrefs.DeleteKey("AshesInfo");
				CONTROLLER.CurrentTestMatch = 0;
				CONTROLLER.TestTeamWonIndexStr = string.Empty;
				CONTROLLER.StoredTestSeriesResult = string.Empty;
				CONTROLLER.TestSeriesData = string.Empty;
				CONTROLLER.MyTestWinCount = 0;
				CONTROLLER.OppTestWinCount = 0;
				CONTROLLER.CurrentPage = "mainmenu";
			}
		}
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = null;
		Singleton<PauseGameScreen>.instance.BG.SetActive(value: true);
		CONTROLLER.ShowTMGameOver = false;
		if (ObscuredPrefs.HasKey("ShowTMGO"))
		{
			ObscuredPrefs.DeleteKey("ShowTMGO");
		}
		CalculateXPforCurrentMatch();
		holder.SetActive(value: true);
		DisPlayResult();
		pageNumber = 1;
		leftBtn.SetActive(value: false);
		screenOne.SetActive(value: true);
		screenTwo.SetActive(value: false);
		CalculateCoinsEarned();
		ValidateXPMilestones();
		Singleton<GameOverDisplay>.instance.ScreenThreeDetails();
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
				Singleton<GameOverScreen>.instance.GameQuit(1);
			}
			if (pageNumber < 2)
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

	private void CalculateCoinsEarned()
	{
		GetDifficultyMultiplier();
		GetOversMultiplier();
		CoinSystem.GetBaseEarnings();
		if (ObscuredPrefs.GetInt("TMOvers") == 0)
		{
			CoinsEarnedPerDay = (int)((float)baseValues[ObscuredPrefs.GetInt("TMOvers")] * (difficultyMultiplier * (float)CONTROLLER.currentDay + GetFactorial(CONTROLLER.currentDay)));
		}
		else
		{
			CoinsEarnedPerDay = (int)((float)baseValues[ObscuredPrefs.GetInt("TMOvers") - 6] * (difficultyMultiplier * (float)CONTROLLER.currentDay + GetFactorial(CONTROLLER.currentDay)));
		}
		Coin1.text = CoinsEarnedPerDay.ToString();
		CoinsEarnedForWinning = (int)((float)CoinSystem.BaseValues[GenerateBaseEarningsKey()] * oversMultiplier * diffMultiplierCoins);
		Coin2.text = CoinsEarnedForWinning.ToString();
		Coin3.text = totalCoinsInaMatch.ToString();
		CoinsEarnedTotal = totalCoinsInaMatch + CoinsEarnedForWinning + CoinsEarnedPerDay;
		CoinsEarned.text = CoinsEarnedTotal.ToString();
		SavePlayerPrefs.SaveUserCoins(CoinsEarnedTotal, 0, CoinsEarnedTotal);
	}

	private void GetDifficultyMultiplier()
	{
		if (CONTROLLER.difficultyMode == "easy")
		{
			difficultyMultiplier = 0.3f;
			diffMultiplierCoins = 1f;
		}
		else if (CONTROLLER.difficultyMode == "medium")
		{
			difficultyMultiplier = 0.4f;
			diffMultiplierCoins = 1.2f;
		}
		else if (CONTROLLER.difficultyMode == "hard")
		{
			difficultyMultiplier = 0.5f;
			diffMultiplierCoins = 1.4f;
		}
	}

	private string GenerateBaseEarningsKey()
	{
		string empty = string.Empty;
		empty += CONTROLLER.PlayModeSelected;
		return empty + "|" + winStatus;
	}

	private void GetOversMultiplier()
	{
		if (ObscuredPrefs.GetInt("TMOvers") == 0 || ObscuredPrefs.GetInt("TMOvers") == 6)
		{
			oversMultiplier = 1f;
		}
		else if (ObscuredPrefs.GetInt("TMOvers") == 7)
		{
			oversMultiplier = 2f;
		}
		else if (ObscuredPrefs.GetInt("TMOvers") == 8)
		{
			oversMultiplier = 4f;
		}
		else if (ObscuredPrefs.GetInt("TMOvers") == 9)
		{
			oversMultiplier = 5f;
		}
	}

	private float GetFactorial(int index)
	{
		float num = 0f;
		float num2 = 0.1f;
		for (int i = 0; i < index; i++)
		{
			if (i != 0)
			{
				num += num2;
				num2 += 0.1f;
			}
		}
		return num;
	}

	public void ValidateXPMilestones()
	{
		SavePlayerPrefs.SaveUserXPs(totalExpInaMatch, totalExpInaMatch);
		StartCoroutine(SyncPoints());
		if (ObscuredPrefs.HasKey("XPMilestoneIndex"))
		{
			milestoneIndex = ObscuredPrefs.GetInt("XPMilestoneIndex");
		}
		else
		{
			milestoneIndex = 0;
		}
		TotalXp.text = CONTROLLER.XPs + " / " + xpAmount[milestoneIndex].ToString();
		XpFill.fillAmount = (float)CONTROLLER.XPs / (float)xpAmount[milestoneIndex];
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

	public void HideMe()
	{
		holder.SetActive(value: false);
		screenOne.SetActive(value: false);
		screenTwo.SetActive(value: false);
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
			Singleton<GameOverDisplay>.instance.screenThree.SetActive(value: true);
			Singleton<GameOverDisplay>.instance.AnimationAchieve.CoinsTopPanel.text = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey).ToString();
			Singleton<GameOverDisplay>.instance.AnimationAchieve.TicketTopPanel.text = ObscuredPrefs.GetInt(CONTROLLER.TicketsKey).ToString();
			Singleton<GameOverDisplay>.instance.AnimationAchieve.XpTopPanel.text = ObscuredPrefs.GetInt(CONTROLLER.XPKey).ToString();
		}
		else
		{
			CONTROLLER.PopupName = "googlesignin";
			Singleton<Popups>.instance.ShowMe();
		}
	}

	public void CloseAchievements()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		//Singleton<AdIntegrate>.instance.ShowAd();
		leftBtn.SetActive(value: true);
		rightBtn.SetActive(value: true);
		screenTwo.SetActive(value: true);
		Singleton<GameOverDisplay>.instance.screenThree.SetActive(value: false);
	}

	public void openStore()
	{
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
			if (Singleton<Google_SignIn>.instance.googleAuthenticated == 1)
			{
				Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
				Singleton<NavigationBack>.instance.deviceBack = FromStoreBack;
				HideMe();
				Singleton<Store>.instance.ShowMe();
				CONTROLLER.pageName = "store";
				Singleton<Store>.instance.PerformanceUpgradeButtonClicked();
			}
			else
			{
				CONTROLLER.PopupName = "googlesignin";
				Singleton<Popups>.instance.ShowMe();
			}
		//}
		//else
		//{
		//	CONTROLLER.PopupName = "noInternet";
		//	Singleton<Popups>.instance.ShowMe();
		//}
	}

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
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
			if (Singleton<Google_SignIn>.instance.googleAuthenticated == 1)
			{
				HideMe();
				Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
				//Singleton<LeaderBoard>.instance.ShowMe();
			}
			else
			{
				CONTROLLER.PopupName = "googlesignin";
				Singleton<Popups>.instance.ShowMe();
			}
		//}
		//else
		//{
		//	CONTROLLER.PopupName = "noInternet";
		//	Singleton<Popups>.instance.ShowMe();
		//}
	}
}
