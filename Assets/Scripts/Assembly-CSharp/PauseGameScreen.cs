using UnityEngine;
using UnityEngine.UI;

public class PauseGameScreen : Singleton<PauseGameScreen>
{
	public GameObject BG;

	public GameObject skipbutton;

	public Image MyTeamFlag;

	public Image OppTeamFlag;

	public Text TeamName1Txt;

	public Text TeamName2Txt;

	public Text PauseScreenTittle1;

	public Text PauseScreenTittle2;

	public Text ScoreTxt1;

	public Text ScoreTxt2;

	public Text APScore;

	public Text NeedText1;

	public Text NeedText2;

	public Button autoplayBtn;

	public GameObject autoPlayDisableBtn;

	public GameObject bowlingScoreCard;

	public GameObject battingScoreCard;

	public GameObject declareBtn;

	public GameObject noteText;

	public GameObject midPageGO;

	public GameObject RVPanel;

	public int remainingOvers;

	protected void Awake()
	{
		Hide(boolean: true);
	}

	public void addEventListener()
	{
	}

	private string ReplaceText(string original, string replace1, string replace2)
	{
		string text = string.Empty;
		if (original.Contains("#"))
		{
			text = original.Replace("#", replace1);
		}
		if (text.Contains("$"))
		{
			text = text.Replace("$", replace2);
		}
		return text;
	}

	private void UpdateGamePause()
	{
		Singleton<GamePausePanelTransition>.instance.panelTransition();
		midPageGO.SetActive(value: true);
		Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite in flags)
		{
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation)
			{
				MyTeamFlag.sprite = sprite;
			}
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation)
			{
				OppTeamFlag.sprite = sprite;
			}
		}
		Singleton<BowlingControls>.instance.Hide(boolean: true);
		Debug.Log(TeamName1Txt.text + " " + TeamName2Txt.text);
		TeamName1Txt.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].teamName;
		int index = LocalizationData.instance.refList.IndexOf(TeamName1Txt.text.ToUpper());
		TeamName1Txt.text = LocalizationData.instance.getText(index);
		TeamName2Txt.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].teamName;
		index = LocalizationData.instance.refList.IndexOf(TeamName2Txt.text.ToUpper());
		TeamName2Txt.text = LocalizationData.instance.getText(index);
		Debug.Log(TeamName1Txt.text + " " + TeamName2Txt.text);
		ScoreTxt1.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation.ToUpper() + " " + Singleton<GameModel>.instance.ScoreStr;
		ScoreTxt2.text = " " + LocalizationData.instance.getText(253);
		ScoreTxt2.text = ReplaceText(ScoreTxt2.text, string.Empty, Singleton<GameModel>.instance.OversStr);
		Debug.Log(ScoreTxt2.text);
		if (ScoreTxt2.text.Contains("("))
		{
			Debug.Log(ScoreTxt2.text);
			ScoreTxt2.text = ScoreTxt2.text.Replace("(", LocalizationData.instance.getText(651));
		}
		Debug.Log(ScoreTxt2.text);
		if (ScoreTxt2.text.Contains(")"))
		{
			Debug.Log(ScoreTxt2.text);
			ScoreTxt2.text = ScoreTxt2.text.Replace(")", LocalizationData.instance.getText(652));
		}
		Debug.Log(ScoreTxt2.text);
		int num = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls;
		int num2 = CONTROLLER.TargetToChase - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores;
		if (CONTROLLER.PlayModeSelected != 7)
		{
			NeedText1.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation.ToUpper();
			NeedText2.text = " " + LocalizationData.instance.getText(260);
			NeedText2.text = ReplaceText(NeedText2.text, num2.ToString(), num.ToString());
		}
		else
		{
			string text = (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 / 6).ToString();
			string text2 = (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 % 6).ToString();
			if (CONTROLLER.currentInnings == 0)
			{
				ScoreTxt1.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation.ToUpper() + " " + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores1 + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1;
				ScoreTxt2.text = " " + LocalizationData.instance.getText(260);
				ScoreTxt2.text = ReplaceText(ScoreTxt2.text, string.Empty, text + "." + text2);
			}
			else
			{
				ScoreTxt1.text = string.Empty;
				ScoreTxt2.text = Singleton<Scoreboard>.instance.str;
			}
			NeedText1.text = string.Empty;
			NeedText2.text = string.Empty;
		}
		if (!Singleton<GroundController>.instance.ballReleased)
		{
			noteText.SetActive(value: false);
			autoplayBtn.interactable = true;
		}
		else
		{
			noteText.SetActive(value: true);
			autoplayBtn.interactable = false;
		}
		if (CONTROLLER.PlayModeSelected == 5 || CONTROLLER.PlayModeSelected == 4)
		{
			noteText.SetActive(value: true);
			noteText.GetComponent<Text>().text = string.Empty;
			autoplayBtn.interactable = false;
		}
		
	}

	public void menuClicked(int index)
	{
		Singleton<GameModel>.instance.GamePauseMenuSelected();
		switch (index)
		{
		case 0:
			CONTROLLER.pageName = "Ground";
			Screen.sleepTimeout = -1;
			Hide(boolean: true);
			Singleton<GameModel>.instance.groundControllerScript.CanShowCountDown = true;
			Singleton<GameModel>.instance.GamePaused(boolean: false);
			LocalizeTextUpdate();
			break;
		case 1:
			Hide(boolean: true);
			Singleton<BattingScoreCard>.instance.Hide(boolean: false);
			if (CONTROLLER.PlayModeSelected != 7)
			{
				Singleton<BattingScoreCard>.instance.UpdateScoreCard();
			}
			Singleton<NavigationBack>.instance.deviceBack = Singleton<BattingScoreCard>.instance.Continue;
			break;
		case 2:
			Hide(boolean: true);
			Singleton<GameModel>.instance.ShowBowlingScoreCard();
			Singleton<NavigationBack>.instance.deviceBack = Singleton<BowlingScoreCard>.instance.Continue;
			break;
		case 3:
			midPageGO.SetActive(value: false);
			Singleton<SettingsPageTWO>.instance.hideMe();
			break;
		case 4:
			midPageGO.SetActive(value: false);
			Singleton<SettingsPageTWO>.instance.showMe();
			break;
		case 5:
			showQuitPopup();
			break;
		case 6:
			autoPlaySelected();
			break;
		}
		//GoogleAllAds.Instance.InterstitialAdPlz();
	}

	public void LocalizeTextUpdate()
	{
		Singleton<PreviewScreen>.instance.SetFieldPreview();
		if (CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5)
		{
			Singleton<SuperOverScoreboard>.instance.UpdateScoreboard();
		}
		if (CONTROLLER.PlayModeSelected == 7)
		{
			if (CONTROLLER.currentInnings < 2)
			{
				Singleton<Scoreboard>.instance.testMatchInfo.GetComponentInChildren<Text>().text = LocalizationData.instance.getText(465);
				Singleton<Scoreboard>.instance.testMatchInfo.GetComponentInChildren<Text>().text = Singleton<Scoreboard>.instance.ReplaceText(Singleton<Scoreboard>.instance.testMatchInfo.GetComponentInChildren<Text>().text, CONTROLLER.currentDay.ToString(), "1");
			}
			else
			{
				Singleton<Scoreboard>.instance.testMatchInfo.GetComponentInChildren<Text>().text = LocalizationData.instance.getText(465);
				Singleton<Scoreboard>.instance.testMatchInfo.GetComponentInChildren<Text>().text = Singleton<Scoreboard>.instance.ReplaceText(Singleton<Scoreboard>.instance.testMatchInfo.GetComponentInChildren<Text>().text, CONTROLLER.currentDay.ToString(), "2");
			}
		}
	}

	public void declare()
	{
		if (CONTROLLER.currentInnings < 2)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].isDeclared1 = true;
		}
		else
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].isDeclared2 = true;
		}
		Singleton<GameModel>.instance.checkForDeclaration();
		if (CONTROLLER.RunRate >= 4f && CONTROLLER.currentInnings != 2)
		{
			CONTROLLER.declareMode = true;
		}
		midPageGO.SetActive(value: false);
	}

	private void autoPlaySelected()
	{
		CONTROLLER.pageName = "autoplay";
		midPageGO.SetActive(value: false);
		Singleton<RewardedVideoScript>.instance.isForRV = false;
		Singleton<NavigationBack>.instance.deviceBack = Singleton<RewardedVideoScript>.instance.AutoPlayNoButton;
		//if (Singleton<AdIntegrate>.instance.CheckForInternet() )
		//{
		//	Singleton<RewardedVideoScript>.instance.isForRV = true;
		//	Singleton<RewardedVideoScript>.instance.RVText.text = LocalizationData.instance.getText(254);
		//	RVPanel.SetActive(value: true);
		//	Singleton<RewardedVideoScript>.instance.RewardPanelButton[0].gameObject.SetActive(value: true);
		//	Singleton<RewardedVideoScript>.instance.RewardPanelButton[1].gameObject.SetActive(value: true);
		//	Singleton<RewardedVideoScript>.instance.YesButtonText.text = LocalizationData.instance.getText(164);
		//	return;
		//}
		int num = Singleton<PaymentProcess>.instance.GenerateAmount() / 2;
		if (num == 0)
		{
			num = 1;
		}
		Singleton<RewardedVideoScript>.instance.RVText.text = LocalizationData.instance.getText(270);
		Singleton<RewardedVideoScript>.instance.RVText.text = ReplaceText(Singleton<RewardedVideoScript>.instance.RVText.text, num + " ", string.Empty);
		RVPanel.SetActive(value: true);
		Singleton<RewardedVideoScript>.instance.RewardPanelButton[0].gameObject.SetActive(value: true);
		Singleton<RewardedVideoScript>.instance.RewardPanelButton[1].gameObject.SetActive(value: true);
		Singleton<RewardedVideoScript>.instance.YesButtonText.text = LocalizationData.instance.getText(164);
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	Singleton<AdIntegrate>.instance.requestRewardedVideo();
		//}
	}

	public void RVAutoPlaySelected()
	{
		//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_AutoPlay" });
		//Singleton<AdIntegrate>.instance.showRewardedVideo(3);
	}

	public void autoPlayOperations()
	{
		//if (CONTROLLER.PlayModeSelected == 0)
		//{
		//	Singleton<Firebase_Events>.instance.Firebase_QP_AutoPlay();
		//}
		//else if (CONTROLLER.PlayModeSelected == 1)
		//{
		//	Singleton<Firebase_Events>.instance.Firebase_T20_AutoPlay();
		//}
		//else if (CONTROLLER.PlayModeSelected == 2)
		//{
		//	Singleton<Firebase_Events>.instance.Firebase_PRL_AutoPlay();
		//}
		//else if (CONTROLLER.PlayModeSelected == 3)
		//{
		//	Singleton<Firebase_Events>.instance.Firebase_WC_AutoPlay();
		//}
		//else if (CONTROLLER.PlayModeSelected == 7)
		//{
		//	Singleton<Firebase_Events>.instance.Firebase_TM_AutoPlay();
		//}
		RVPanel.SetActive(value: false);
		midPageGO.SetActive(value: false);
		iTween.Stop(Singleton<BowlingControls>.instance.SpeedArrow);
		Singleton<BowlingControls>.instance.stopSwingAngle();
		generateScore();
	}

	private void generateScore()
	{
		Singleton<NavigationBack>.instance.disableDeviceBack = true;
		if (CONTROLLER.PlayModeSelected != 7)
		{
			remainingOvers = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6;
			if (CONTROLLER.myTeamIndex == CONTROLLER.BowlingTeamIndex)
			{
				Singleton<AutoPlay>.instance.AutoplayOvers(int.Parse(CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].rank), int.Parse(CONTROLLER.TeamList[CONTROLLER.myTeamIndex].rank), remainingOvers);
			}
			else
			{
				Singleton<AutoPlay>.instance.AutoplayOvers(int.Parse(CONTROLLER.TeamList[CONTROLLER.myTeamIndex].rank), int.Parse(CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].rank), remainingOvers);
			}
			return;
		}
		remainingOvers = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] - CONTROLLER.ballsBowledPerDay / 6;
		if (CONTROLLER.myTeamIndex == CONTROLLER.BowlingTeamIndex)
		{
			if (CONTROLLER.currentInnings < 2)
			{
				Singleton<AutoPlay>.instance.AutoplayCalculation(int.Parse(CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].rank), int.Parse(CONTROLLER.TeamList[CONTROLLER.myTeamIndex].rank));
			}
			else
			{
				Singleton<AutoPlay>.instance.AutoplayCalculationSecondInnings(int.Parse(CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].rank), int.Parse(CONTROLLER.TeamList[CONTROLLER.myTeamIndex].rank));
			}
		}
		else if (CONTROLLER.currentInnings < 2)
		{
			Singleton<AutoPlay>.instance.AutoplayCalculation(int.Parse(CONTROLLER.TeamList[CONTROLLER.myTeamIndex].rank), int.Parse(CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].rank));
		}
		else
		{
			Singleton<AutoPlay>.instance.AutoplayCalculationSecondInnings(int.Parse(CONTROLLER.TeamList[CONTROLLER.myTeamIndex].rank), int.Parse(CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].rank));
		}
	}

	public void showQuitPopup()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<QuitConfirm>.instance.hideMe;
		Singleton<SettingsPageTWO>.instance.Holder.SetActive(value: false);
		midPageGO.SetActive(value: false);
		Singleton<QuitConfirm>.instance.showMe();
	}

	public void hideAll()
	{
		midPageGO.SetActive(value: false);
		Singleton<SettingsPageTWO>.instance.hideMe();
	}

	private void OpenPause()
	{
		Hide(boolean: false);
	}

	private void ClosePause()
	{
		menuClicked(0);
	}

	public void Hide(bool boolean)
	{
		if (boolean)
		{
			if (!CONTROLLER.gameCompleted)
			{
				BG.SetActive(value: false);
			}
			else
			{
				BG.SetActive(value: true);
			}
			midPageGO.SetActive(value: false);
			return;
		}
		if (CONTROLLER.canShowbannerGround == 1)
		{
			//Singleton<AdIntegrate>.instance.ShowAd();
		}
		if (CONTROLLER.PlayModeSelected == 7)
		{
			CONTROLLER.isFromAutoPlay = false;
		}
		if (CONTROLLER.PlayModeSelected == 7 && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex && CONTROLLER.currentInnings != 3)
		{
			declareBtn.SetActive(value: true);
		}
		else
		{
			declareBtn.SetActive(value: false);
		}
		Singleton<NavigationBack>.instance.deviceBack = ClosePause;
		CONTROLLER.pageName = "GamePause";
		CONTROLLER.CurrentMenu = "pauseScreen";
		DisplayTittle();
		if (CONTROLLER.PlayModeSelected == 5 || (CONTROLLER.PlayModeSelected == 4 && CONTROLLER.SuperOverMode == "bat"))
		{
			bowlingScoreCard.SetActive(value: false);
		}
		if (CONTROLLER.PlayModeSelected == 4 && CONTROLLER.SuperOverMode == "bowl")
		{
			battingScoreCard.SetActive(value: false);
		}
		Singleton<GroundController>.instance.showPreviewCamera(status: false);
		if (!BG.activeSelf)
		{
			BG.SetActive(value: true);
		}
		UpdateGamePause();
		midPageGO.SetActive(value: true);
		if (CONTROLLER.PlayModeSelected > 3 && CONTROLLER.PlayModeSelected != 7)
		{
			autoplayBtn.gameObject.SetActive(value: false);
		}
		else
		{
			autoplayBtn.gameObject.SetActive(value: true);
		}
		Singleton<Tutorial>.instance.getBoolean();
	}

	public void DisplayTittle()
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			PauseScreenTittle1.text = LocalizationData.instance.getText(247) + " " + LocalizationData.instance.getText(650) + " " + LocalizationData.instance.getText(183);
			PauseScreenTittle2.text = string.Empty;
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			PauseScreenTittle1.text = LocalizationData.instance.getText(247) + " " + LocalizationData.instance.getText(650) + " " + LocalizationData.instance.getText(396);
			if (CONTROLLER.TournamentStage == 0)
			{
				PauseScreenTittle2.text = " " + LocalizationData.instance.getText(526);
			}
			else if (CONTROLLER.TournamentStage == 1)
			{
				PauseScreenTittle2.text = LocalizationData.instance.getText(399);
			}
			else if (CONTROLLER.TournamentStage == 2)
			{
				PauseScreenTittle2.text = LocalizationData.instance.getText(400);
			}
			else if (CONTROLLER.TournamentStage == 3)
			{
				PauseScreenTittle2.text = LocalizationData.instance.getText(401);
			}
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "NPL")
			{
				PauseScreenTittle1.text = LocalizationData.instance.getText(247) + " " + LocalizationData.instance.getText(650) + " " + LocalizationData.instance.getText(14);
			}
			else if (CONTROLLER.tournamentType == "PAK")
			{
				PauseScreenTittle1.text = LocalizationData.instance.getText(247) + " " + LocalizationData.instance.getText(650) + " " + LocalizationData.instance.getText(15);
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				PauseScreenTittle1.text = LocalizationData.instance.getText(247) + " " + LocalizationData.instance.getText(650) + " " + LocalizationData.instance.getText(16);
			}
			if (CONTROLLER.NPLIndiaTournamentStage == 0)
			{
				PauseScreenTittle2.text = LocalizationData.instance.getText(454) + " " + (CONTROLLER.NPLIndiaMyCurrentMatchIndex + 1);
			}
			else if (CONTROLLER.NPLIndiaTournamentStage == 1)
			{
				if (CONTROLLER.NPLIndiaLeagueMatchIndex == 0)
				{
					PauseScreenTittle2.text = LocalizationData.instance.getText(460) + " 1";
				}
				else
				{
					PauseScreenTittle2.text = LocalizationData.instance.getText(461);
				}
			}
			else if (CONTROLLER.NPLIndiaTournamentStage == 2)
			{
				PauseScreenTittle2.text = LocalizationData.instance.getText(460) + " 2";
			}
			else if (CONTROLLER.NPLIndiaTournamentStage == 3)
			{
				PauseScreenTittle2.text = "FINALS";
				PauseScreenTittle2.text = LocalizationData.instance.getText(401);
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			PauseScreenTittle1.text = LocalizationData.instance.getText(247) + " " + LocalizationData.instance.getText(650) + " " + LocalizationData.instance.getText(52) + " " + LocalizationData.instance.getText(4);
			if (CONTROLLER.WCTournamentStage == 0)
			{
				PauseScreenTittle2.text = LocalizationData.instance.getText(454) + " " + (CONTROLLER.WCMyCurrentMatchIndex + 1);
			}
			else if (CONTROLLER.WCTournamentStage == 1)
			{
				PauseScreenTittle2.text = LocalizationData.instance.getText(399);
			}
			else if (CONTROLLER.WCTournamentStage == 2)
			{
				PauseScreenTittle2.text = LocalizationData.instance.getText(400);
			}
			else if (CONTROLLER.WCTournamentStage == 3)
			{
				PauseScreenTittle2.text = LocalizationData.instance.getText(401);
			}
		}
		else if (CONTROLLER.PlayModeSelected == 4)
		{
			PauseScreenTittle1.text = LocalizationData.instance.getText(247) + " " + LocalizationData.instance.getText(650) + " " + LocalizationData.instance.getText(11);
			PauseScreenTittle2.text = string.Empty;
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			PauseScreenTittle1.text = LocalizationData.instance.getText(247) + " " + LocalizationData.instance.getText(650) + " " + LocalizationData.instance.getText(12);
			PauseScreenTittle2.text = string.Empty;
		}
		else if (CONTROLLER.PlayModeSelected == 6)
		{
			PauseScreenTittle1.text = LocalizationData.instance.getText(247) + " " + LocalizationData.instance.getText(650) + " " + LocalizationData.instance.getText(353);
			PauseScreenTittle2.text = string.Empty;
		}
	}
}
