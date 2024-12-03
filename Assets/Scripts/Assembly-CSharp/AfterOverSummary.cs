using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AfterOverSummary : Singleton<AfterOverSummary>
{
	public GameObject holder;

	[Header("FirstInnings")]
	public GameObject firstInningsHolder;

	public Text firstInningsScore;

	public Text firstInningsRR;

	public Text firstInningsOvers;

	public Image Team1Flag;

	[Header("SecondInnings")]
	public GameObject secondInningsHolder;

	public Text secondInningsScore;

	public Text secondInningsRR;

	public Text secondInningsOvers;

	public Image Team2Flag;

	[Header("GameModeDetails")]
	public Image CupImage;

	public Text gameModeName;

	public Text gameModeStage;

	public Sprite[] CupSprites;

	[Header("AdTimer")]
	public GameObject adTimer;

	public Image timerFill;

	public Text timerText;

	private int stage;

	private float timer;

	private string[] gameModeNames = new string[6] { "183", "17", "625", "13", "15", "16" };

	private string[,] gameModeStageNames = new string[4, 4]
	{
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		},
		{ "610", "399", "400", "401" },
		{ "455", "460", "460", "401" },
		{ "455", "399", "400", "401" }
	};

	public GameObject tapToContinue;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && holder.activeInHierarchy && tapToContinue.activeInHierarchy)
		{
			Continue();
		}
		if (adTimer.activeInHierarchy)
		{
			timer += Time.deltaTime;
			string text = Mathf.Floor(timer / 60f).ToString("00");
			string s = (timer % 60f).ToString("00");
			timerText.text = (5 - int.Parse(s)).ToString();
		}
	}

	public void ShowMe()
	{
		if (CONTROLLER.receivedAdEvent)
		{
			//Singleton<AdIntegrate>.instance.HideAd();
		}
		if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls > 0)
		{
			
				tapToContinue.SetActive(value: true);
				adTimer.SetActive(value: false);
			
		}
		else
		{
			tapToContinue.SetActive(value: true);
			adTimer.SetActive(value: false);
		}
		holder.SetActive(value: true);
		ValidateScoreDetails();
		ValidateGameModeDetails();
		Singleton<StandbyCam>.instance.RotateStandbyCam();
	}

	private string ReplaceText(string original, string replace)
	{
		string result = string.Empty;
		if (original.Contains("#"))
		{
			result = original.Replace("#", replace);
		}
		return result;
	}

	private void ValidateScoreDetails()
	{
		if (CONTROLLER.currentInnings == 0)
		{
			secondInningsHolder.SetActive(value: false);
			firstInningsScore.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets;
			Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
			foreach (Sprite sprite in flags)
			{
				if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
				{
					Team1Flag.sprite = sprite;
				}
			}
			firstInningsOvers.text = LocalizationData.instance.getText(267) + string.Empty + LocalizationData.instance.getText(649) + " ";
			firstInningsOvers.text = ReplaceText(firstInningsOvers.text, (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6).ToString());
			firstInningsRR.text = LocalizationData.instance.getText(268) + string.Empty + LocalizationData.instance.getText(649) + " " + ((float)CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores / (float)(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6)).ToString("F2");
			return;
		}
		secondInningsHolder.SetActive(value: true);
		firstInningsScore.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].currentMatchScores + "/" + CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].currentMatchWickets;
		Sprite[] flags2 = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite2 in flags2)
		{
			if (sprite2.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
			{
				Team1Flag.sprite = sprite2;
			}
		}
		firstInningsOvers.text = LocalizationData.instance.getText(265);
		int num = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].currentMatchBalls / 6;
		int num2 = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].currentMatchBalls % 6;
		string replace = num + "." + num2;
		firstInningsRR.text = LocalizationData.instance.getText(637);
		firstInningsRR.text = ReplaceText(firstInningsRR.text, replace);
		secondInningsScore.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets;
		Sprite[] flags3 = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite3 in flags3)
		{
			if (sprite3.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
			{
				Team2Flag.sprite = sprite3;
			}
		}
		secondInningsOvers.text = LocalizationData.instance.getText(267) + string.Empty + LocalizationData.instance.getText(649) + " ";
		secondInningsOvers.text = ReplaceText(secondInningsOvers.text, (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6).ToString());
		int num3 = CONTROLLER.TargetToChase - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores;
		float num4 = ((float)CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6f - (float)CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls) / 6f;
		float num5 = (float)num3 / num4;
		secondInningsRR.text = LocalizationData.instance.getText(268) + " " + ((float)CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores / (float)(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6)).ToString("F2") + "\n " + LocalizationData.instance.getText(269) + " " + num5.ToString("F2");
	}

	private void ValidateGameModeDetails()
	{
		Debug.LogWarning(gameModeName.text);
		if (CONTROLLER.PlayModeSelected == 2 && CONTROLLER.tournamentType == "PAK")
		{
			gameModeName.text = LocalizationData.instance.getText(int.Parse(gameModeNames[4]));
			CupImage.sprite = CupSprites[4];
		}
		else if (CONTROLLER.PlayModeSelected == 2 && CONTROLLER.tournamentType == "AUS")
		{
			gameModeName.text = LocalizationData.instance.getText(int.Parse(gameModeNames[5]));
			CupImage.sprite = CupSprites[5];
		}
		else
		{
			gameModeName.text = LocalizationData.instance.getText(int.Parse(gameModeNames[CONTROLLER.PlayModeSelected]));
			CupImage.sprite = CupSprites[CONTROLLER.PlayModeSelected];
		}
		Debug.LogWarning(gameModeName.text);
		SetStage();
		if (CONTROLLER.PlayModeSelected == 0)
		{
			gameModeStage.text = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] + " " + LocalizationData.instance.getText(184) + " " + LocalizationData.instance.getText(454);
		}
		else if (CONTROLLER.PlayModeSelected == 2 && CONTROLLER.NPLIndiaTournamentStage == 1 && CONTROLLER.NPLIndiaLeagueMatchIndex == 1)
		{
			gameModeStage.text = LocalizationData.instance.getText(461);
		}
		else
		{
			gameModeStage.text = LocalizationData.instance.getText(int.Parse(gameModeStageNames[CONTROLLER.PlayModeSelected, stage]));
		}
		if (CONTROLLER.PlayModeSelected == 2)
		{
			if (stage == 1)
			{
				gameModeStage.text += " 1";
			}
			else if (stage == 2)
			{
				gameModeStage.text += " 2";
			}
		}
	}

	private void SetStage()
	{
		if (CONTROLLER.PlayModeSelected == 1)
		{
			stage = CONTROLLER.TournamentStage;
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			stage = CONTROLLER.NPLIndiaTournamentStage;
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			stage = CONTROLLER.WCTournamentStage;
		}
		else
		{
			stage = 0;
		}
	}

	private void ActivateTap()
	{
		//FirebaseAnalyticsManager.instance.logEvent("InterstitialAds", new string[2] { "Ad_Network_Interstitial_Actions", "Spot_Generated_BetweenOvers" });
		//Singleton<AdIntegrate>.instance.DisplayInterestialAd();
		adTimer.SetActive(value: false);
		Invoke("ShowTap", 0.7f);
	}

	private void ShowTap()
	{
		tapToContinue.SetActive(value: true);
	}

	public void Continue()
	{
		if (CONTROLLER.myTeamIndex == CONTROLLER.BowlingTeamIndex)
		{
			Singleton<BattingScoreCard>.instance.Continue();
		}
		else
		{
			Singleton<BowlingScoreCard>.instance.Continue();
		}
		HideMe();
	}

	private void HideMe()
	{
		holder.SetActive(value: false);
		CONTROLLER.canShowbannerGround = 1;
		Singleton<StandbyCam>.instance.PauseTween();
	}
}
