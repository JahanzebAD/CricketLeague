using UnityEngine;
using UnityEngine.UI;

public class BattingScoreCard : Singleton<BattingScoreCard>
{
	public Font bold;

	public Font normal;

	public GameObject BG;

	public GameObject scoreCard;

	private Transform _transform;

	public Sprite[] batsmanStates;

	public GameObject scrollList;

	private Vector3 startPos;

	public Button ContinueBtn;

	public Button BackBtn;

	public Image SCTeamFlag;

	public Image BtnTeamFlag;

	public Text SCTeamName;

	public Text BtnTeamName;

	public Text OversText;

	public Text ScoreText;

	public Text ExtrasText;

	public bool BackKeyEnable;

	public BatsmanDetails[] batsman;

	public int SelectedInnings = 1;

	public Text SelectedInningsText;

	public Scrollbar scrollView;

	private int battingIndex;

	protected void Awake()
	{
		Hide(boolean: true);
	}

	protected void Start()
	{
		startPos = scrollList.transform.position;
	}

	public void addEventListener()
	{
	}

	public void ResetBattingCard()
	{
		//if (CONTROLLER.PlayModeSelected == 7)
		//{
		//	TMResetBattingCard();
		//	return;
		//}
		//int battingTeamIndex = CONTROLLER.BattingTeamIndex;
		//SetTeamInfo();
		//for (int i = 0; i < batsman.Length; i++)
		//{
		//	batsman[i].Highlight.sprite = batsmanStates[0];
		//	batsman[i].Name.font = normal;
		//	batsman[i].Name.text = CONTROLLER.TeamList[battingTeamIndex].PlayerList[i].ScoreboardName.ToUpper();
		//	batsman[i].Status.text = string.Empty.ToUpper();
		//	batsman[i].Runs.text = string.Empty;
		//	batsman[i].Balls.text = string.Empty;
		//	batsman[i].SR.text = string.Empty;
		//	batsman[i].Fours.text = string.Empty;
		//	batsman[i].Sixes.text = string.Empty;
		//	batsman[i].FOW.text = string.Empty;
		//}
		//ScoreText.text = Singleton<GameModel>.instance.ScoreStr;
		//ExtrasText.text = Singleton<GameModel>.instance.ExtraStr;
		//OversText.text = Singleton<GameModel>.instance.OversStr;
		//if (OversText.text.Contains("("))
		//{
		//	Debug.Log(OversText.text);
		//	OversText.text = OversText.text.Replace("(", LocalizationData.instance.getText(651));
		//}
		//if (OversText.text.Contains(")"))
		//{
		//	Debug.Log(OversText.text);
		//	OversText.text = OversText.text.Replace(")", LocalizationData.instance.getText(652));
		//}
	}

	public void RestartGame()
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TMRestartGame();
			return;
		}
		int battingTeamIndex = CONTROLLER.BattingTeamIndex;
		SetTeamInfo();
		for (int i = 0; i < batsman.Length; i++)
		{
			if (CONTROLLER.TeamList[battingTeamIndex].PlayerList[i].BatsmanList.Status == string.Empty)
			{
				batsman[i].Highlight.sprite = batsmanStates[0];
				batsman[i].Name.font = normal;
				batsman[i].Name.text = CONTROLLER.TeamList[battingTeamIndex].PlayerList[i].ScoreboardName.ToUpper();
				batsman[i].Status.text = string.Empty.ToUpper();
				batsman[i].Runs.text = string.Empty;
				batsman[i].Balls.text = string.Empty;
				batsman[i].SR.text = string.Empty;
				batsman[i].Fours.text = string.Empty;
				batsman[i].Sixes.text = string.Empty;
				batsman[i].FOW.text = string.Empty;
			}
			else
			{
				UpdateWicket(i);
			}
		}
		ScoreText.text = Singleton<GameModel>.instance.ScoreStr;
		ExtrasText.text = Singleton<GameModel>.instance.ExtraStr;
		OversText.text = Singleton<GameModel>.instance.OversStr;
		if (OversText.text.Contains("("))
		{
			Debug.Log(OversText.text);
			OversText.text = OversText.text.Replace("(", LocalizationData.instance.getText(651));
		}
		if (OversText.text.Contains(")"))
		{
			Debug.Log(OversText.text);
			OversText.text = OversText.text.Replace(")", LocalizationData.instance.getText(652));
		}
	}

	private void SetTeamInfo()
	{
		//if (CONTROLLER.currentInnings == 0)
		//{
		//	Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		//	foreach (Sprite sprite in flags)
		//	{
		//		if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
		//		{
		//			SCTeamFlag.sprite = sprite;
		//		}
		//		if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
		//		{
		//			BtnTeamFlag.sprite = sprite;
		//		}
		//	}
		//	SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper();
		//	BtnTeamName.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper();
		//	return;
		//}
		//Sprite[] flags2 = Singleton<FlagHolderGround>.instance.flags;
		//foreach (Sprite sprite2 in flags2)
		//{
		//	if (sprite2.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
		//	{
		//		SCTeamFlag.sprite = sprite2;
		//	}
		//	if (sprite2.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
		//	{
		//		BtnTeamFlag.sprite = sprite2;
		//	}
		//}
		//SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper();
		//BtnTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper();
	}

	private void TMSetTeamInfo()
	{
		Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite in flags)
		{
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
			{
				SCTeamFlag.sprite = sprite;
			}
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
			{
				BtnTeamFlag.sprite = sprite;
			}
		}
		SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper();
		BtnTeamName.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper();
	}

	public void UpdateScoreCard()
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TMUpdateScoreCard();
			return;
		}
		Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite in flags)
		{
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
			{
				SCTeamFlag.sprite = sprite;
			}
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
			{
				BtnTeamFlag.sprite = sprite;
			}
		}
		SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper();
		BtnTeamName.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper();
		int battingTeamIndex = CONTROLLER.BattingTeamIndex;
		int strikerIndex = CONTROLLER.StrikerIndex;
		if (strikerIndex >= 0 && strikerIndex < CONTROLLER.TeamList[battingTeamIndex].PlayerList.Length)
		{
			batsman[strikerIndex].Status.text = CONTROLLER.TeamList[battingTeamIndex].PlayerList[strikerIndex].BatsmanList.Status.ToUpper();
			batsman[strikerIndex].Runs.text = string.Empty + CONTROLLER.TeamList[battingTeamIndex].PlayerList[strikerIndex].BatsmanList.RunsScored;
			batsman[strikerIndex].Balls.text = string.Empty + CONTROLLER.TeamList[battingTeamIndex].PlayerList[strikerIndex].BatsmanList.BallsPlayed;
			batsman[strikerIndex].SR.text = GetStrikeRate(battingTeamIndex, strikerIndex);
			batsman[strikerIndex].Fours.text = string.Empty + CONTROLLER.TeamList[battingTeamIndex].PlayerList[strikerIndex].BatsmanList.Fours;
			batsman[strikerIndex].Sixes.text = string.Empty + CONTROLLER.TeamList[battingTeamIndex].PlayerList[strikerIndex].BatsmanList.Sixes;
			if (batsman[strikerIndex].Status.text == "not out".ToUpper())
			{
				batsman[strikerIndex].Highlight.sprite = batsmanStates[1];
				batsman[strikerIndex].Name.font = bold;
				batsman[strikerIndex].FOW.text = string.Empty;
			}
			else if (batsman[strikerIndex].Status.text == string.Empty)
			{
				batsman[strikerIndex].Name.font = normal;
				batsman[strikerIndex].Highlight.sprite = batsmanStates[0];
			}
			else
			{
				batsman[strikerIndex].Name.font = normal;
				batsman[strikerIndex].Highlight.sprite = batsmanStates[2];
			}
		}
		strikerIndex = CONTROLLER.NonStrikerIndex;
		if (strikerIndex >= 0 && strikerIndex < CONTROLLER.TeamList[battingTeamIndex].PlayerList.Length)
		{
			batsman[strikerIndex].Status.text = CONTROLLER.TeamList[battingTeamIndex].PlayerList[strikerIndex].BatsmanList.Status.ToUpper();
			batsman[strikerIndex].Runs.text = string.Empty + CONTROLLER.TeamList[battingTeamIndex].PlayerList[strikerIndex].BatsmanList.RunsScored;
			batsman[strikerIndex].Balls.text = string.Empty + CONTROLLER.TeamList[battingTeamIndex].PlayerList[strikerIndex].BatsmanList.BallsPlayed;
			batsman[strikerIndex].SR.text = GetStrikeRate(battingTeamIndex, strikerIndex);
			batsman[strikerIndex].Fours.text = string.Empty + CONTROLLER.TeamList[battingTeamIndex].PlayerList[strikerIndex].BatsmanList.Fours;
			batsman[strikerIndex].Sixes.text = string.Empty + CONTROLLER.TeamList[battingTeamIndex].PlayerList[strikerIndex].BatsmanList.Sixes;
			if (batsman[strikerIndex].Status.text == "not out".ToUpper())
			{
				batsman[strikerIndex].Name.font = bold;
				batsman[strikerIndex].Highlight.sprite = batsmanStates[1];
				batsman[strikerIndex].FOW.text = string.Empty;
			}
			else if (batsman[strikerIndex].Status.text == string.Empty)
			{
				batsman[strikerIndex].Name.font = normal;
				batsman[strikerIndex].Highlight.sprite = batsmanStates[0];
			}
			else
			{
				batsman[strikerIndex].Name.font = normal;
				batsman[strikerIndex].Highlight.sprite = batsmanStates[2];
			}
		}
		ScoreText.text = Singleton<GameModel>.instance.ScoreStr;
		ExtrasText.text = Singleton<GameModel>.instance.ExtraStr;
		OversText.text = Singleton<GameModel>.instance.OversStr;
		if (OversText.text.Contains("("))
		{
			Debug.Log(OversText.text);
			OversText.text = OversText.text.Replace("(", LocalizationData.instance.getText(651));
		}
		if (OversText.text.Contains(")"))
		{
			Debug.Log(OversText.text);
			OversText.text = OversText.text.Replace(")", LocalizationData.instance.getText(652));
		}
		CheckBatsmanStatus();
		Singleton<BattingScoreBoardPanelTransition>.instance.PanelTransition();
	}

	public string GetStrikeRate(int teamID, int playerID)
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			return TMGetStrikeRate(teamID, playerID);
		}
		float num = CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.RunsScored;
		float num2 = CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.BallsPlayed;
		return string.Concat(arg1: (num2 > 0f) ? ((int)(num / num2 * 100f)) : 0, arg0: string.Empty);
	}

	public void UpdateWicket(int playerID)
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TMUpdateWicket(playerID);
			return;
		}
		batsman[playerID].Name.font = normal;
		batsman[playerID].Highlight.sprite = batsmanStates[2];
		batsman[playerID].Name.text = GetBatsmanShortName(CONTROLLER.BattingTeamIndex, playerID).ToUpper();
		batsman[playerID].Runs.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.RunsScored;
		batsman[playerID].Balls.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.BallsPlayed;
		batsman[playerID].SR.text = GetStrikeRate(CONTROLLER.BattingTeamIndex, playerID);
		batsman[playerID].Fours.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.Fours;
		batsman[playerID].Sixes.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.Sixes;
		batsman[playerID].Status.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.Status.ToUpper();
		batsman[playerID].FOW.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.FOW;
	}

	public string GetBatsmanShortName(int TeamID, int playerID)
	{
		return CONTROLLER.TeamList[TeamID].PlayerList[playerID].ScoreboardName;
	}

	private void DisplayList(int TeamID)
	{
		//if (CONTROLLER.PlayModeSelected == 7)
		//{
		//	TMDisplayList(TeamID);
		//	return;
		//}
		//for (int i = 0; i < batsman.Length; i++)
		//{
		//	if (CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.Status == string.Empty || CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.Status == null)
		//	{
		//		batsman[i].Name.font = normal;
		//		batsman[i].Highlight.sprite = batsmanStates[0];
		//		batsman[i].Name.text = CONTROLLER.TeamList[TeamID].PlayerList[i].ScoreboardName.ToUpper();
		//		batsman[i].Status.text = string.Empty.ToUpper();
		//		batsman[i].Runs.text = string.Empty;
		//		batsman[i].Balls.text = string.Empty;
		//		batsman[i].SR.text = string.Empty;
		//		batsman[i].Fours.text = string.Empty;
		//		batsman[i].Sixes.text = string.Empty;
		//		batsman[i].FOW.text = string.Empty;
		//		continue;
		//	}
		//	if (CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.Status == "not out".ToUpper())
		//	{
		//		batsman[i].Name.font = bold;
		//		batsman[i].Highlight.sprite = batsmanStates[1];
		//		batsman[i].FOW.text = string.Empty;
		//	}
		//	else
		//	{
		//		batsman[i].Name.font = normal;
		//		batsman[i].Highlight.sprite = batsmanStates[2];
		//		batsman[i].FOW.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.FOW;
		//	}
		//	batsman[i].Name.text = GetBatsmanShortName(TeamID, i).ToUpper();
		//	batsman[i].Runs.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.RunsScored;
		//	batsman[i].Balls.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.BallsPlayed;
		//	batsman[i].SR.text = GetStrikeRate(TeamID, i);
		//	batsman[i].Fours.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.Fours;
		//	batsman[i].Sixes.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.Sixes;
		//	batsman[i].Status.text = CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.Status.ToUpper();
		//}
		//string text = CONTROLLER.TeamList[TeamID].currentMatchScores + "/" + CONTROLLER.TeamList[TeamID].currentMatchWickets;
		//string text2 = LocalizationData.instance.getText(235) + " " + CONTROLLER.TeamList[TeamID].currentMatchExtras;
		//int num = CONTROLLER.TeamList[TeamID].currentMatchBalls / 6;
		//int num2 = CONTROLLER.TeamList[TeamID].currentMatchBalls % 6;
		//string text3 = LocalizationData.instance.getText(184) + " " + num + "." + num2 + "(" + CONTROLLER.totalOvers + ")";
		//ScoreText.text = text;
		//ExtrasText.text = text2;
		//OversText.text = text3;
		//if (OversText.text.Contains("("))
		//{
		//	Debug.Log(OversText.text);
		//	OversText.text = OversText.text.Replace("(", LocalizationData.instance.getText(651));
		//}
		//if (OversText.text.Contains(")"))
		//{
		//	Debug.Log(OversText.text);
		//	OversText.text = OversText.text.Replace(")", LocalizationData.instance.getText(652));
		//}
	}

	public void SwapScorecard()
	{
		//scrollList.transform.position = startPos;
		//if (BtnTeamName.text.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper())
		//{
		//	DisplayList(CONTROLLER.BattingTeamIndex);
		//	BtnTeamName.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper();
		//	SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper();
		//	Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		//	foreach (Sprite sprite in flags)
		//	{
		//		if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
		//		{
		//			BtnTeamFlag.sprite = sprite;
		//		}
		//		if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
		//		{
		//			SCTeamFlag.sprite = sprite;
		//		}
		//	}
		//	if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets > 7)
		//	{
		//		scrollView.value = 0f;
		//	}
		//	else
		//	{
		//		scrollView.value = 1f;
		//	}
		//	if (CONTROLLER.PlayModeSelected == 7)
		//	{
		//		ScoreText.text = GetScore(CONTROLLER.BattingTeamIndex);
		//		OversText.text = GetOvers(CONTROLLER.BattingTeamIndex);
		//		if (OversText.text.Contains("("))
		//		{
		//			Debug.Log(OversText.text);
		//			OversText.text = OversText.text.Replace("(", LocalizationData.instance.getText(651));
		//		}
		//		if (OversText.text.Contains(")"))
		//		{
		//			Debug.Log(OversText.text);
		//			OversText.text = OversText.text.Replace(")", LocalizationData.instance.getText(652));
		//		}
		//	}
		//}
		//else
		//{
		//	DisplayList(CONTROLLER.BowlingTeamIndex);
		//	BtnTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper();
		//	SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper();
		//	Sprite[] flags2 = Singleton<FlagHolderGround>.instance.flags;
		//	foreach (Sprite sprite2 in flags2)
		//	{
		//		if (sprite2.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
		//		{
		//			BtnTeamFlag.sprite = sprite2;
		//		}
		//		if (sprite2.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
		//		{
		//			SCTeamFlag.sprite = sprite2;
		//		}
		//	}
		//	if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].currentMatchWickets > 7)
		//	{
		//		scrollView.value = 0f;
		//	}
		//	else
		//	{
		//		scrollView.value = 1f;
		//	}
		//	if (CONTROLLER.PlayModeSelected == 7)
		//	{
		//		ScoreText.text = GetScore(CONTROLLER.BowlingTeamIndex);
		//		OversText.text = GetOvers(CONTROLLER.BowlingTeamIndex);
		//		if (OversText.text.Contains("("))
		//		{
		//			Debug.Log(OversText.text);
		//			OversText.text = OversText.text.Replace("(", LocalizationData.instance.getText(651));
		//		}
		//		if (OversText.text.Contains(")"))
		//		{
		//			Debug.Log(OversText.text);
		//			OversText.text = OversText.text.Replace(")", LocalizationData.instance.getText(652));
		//		}
		//	}
		//}
		//CheckBatsmanStatus();
		//Singleton<BattingScoreBoardPanelTransition>.instance.PanelTransition();
	}

	private void CheckBatsmanStatus()
	{
		BatsmanDetails[] array = batsman;
		foreach (BatsmanDetails batsmanDetails in array)
		{
			if (batsmanDetails.Status.text.ToUpper() == "not out".ToUpper())
			{
				batsmanDetails.Highlight.sprite = batsmanStates[1];
			}
		}
	}

	public void Continue()
	{
		Hide(boolean: true);
		if (CONTROLLER.gameCompleted)
		{
			Singleton<MatchSummary>.instance.showMe();
		}
		else if (!Singleton<GameModel>.instance.isGamePaused)
		{
			if (CONTROLLER.PlayModeSelected < 4 || CONTROLLER.PlayModeSelected == 7)
			{
				if (CONTROLLER.isFromAutoPlay)
				{
					Singleton<GameModel>.instance.isGamePaused = true;
				}
				if (CONTROLLER.PlayModeSelected == 7)
				{
					if (CONTROLLER.currentInnings < 2)
					{
						if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 % CONTROLLER.interstitialshowDuration == 0 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 > 0)
						{
							//FirebaseAnalyticsManager.instance.logEvent("InterstitialAds", new string[2] { "Ad_Network_Interstitial_Actions", "Spot_Generated_BetweenOvers" });
							//Singleton<AdIntegrate>.instance.DisplayInterestialAd();
						}
					}
					else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls2 % CONTROLLER.interstitialshowDuration == 0 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls2 > 0)
					{
						//FirebaseAnalyticsManager.instance.logEvent("InterstitialAds", new string[2] { "Ad_Network_Interstitial_Actions", "Spot_Generated_BetweenOvers" });
						//Singleton<AdIntegrate>.instance.DisplayInterestialAd();
					}
				}
				Singleton<GameModel>.instance.ShowBowlingScoreCard();
			}
			else
			{
				Singleton<BowlingScoreCard>.instance.Continue();
			}
		}
		else
		{
			Singleton<GameModel>.instance.AgainToGamePauseScreen();
			Singleton<PauseGameScreen>.instance.Hide(boolean: false);
		}
	}

	public void SwapInnings()
	{
		if (SelectedInnings == 2)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 > 7)
			{
				scrollView.value = 0f;
			}
			else
			{
				scrollView.value = 1f;
			}
			SelectedInnings = 1;
			SelectedInningsText.text = LocalizationData.instance.getText(470);
		}
		else
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets2 > 7)
			{
				scrollView.value = 0f;
			}
			else
			{
				scrollView.value = 1f;
			}
			SelectedInnings = 2;
			SelectedInningsText.text = LocalizationData.instance.getText(469);
		}
		TMDisplayList(CONTROLLER.BattingTeamIndex);
		CheckBatsmanStatus();
	}

	public void Hide(bool boolean)
	{
		if (boolean)
		{
			BG.SetActive(value: false);
			BackKeyEnable = false;
			scoreCard.SetActive(value: false);
			DisplayList(CONTROLLER.BattingTeamIndex);
			return;
		}
		//if (CONTROLLER.canShowbannerGround == 1)
		//{
		//	Singleton<AdIntegrate>.instance.ShowAd();
		//}
		Singleton<NavigationBack>.instance.deviceBack = null;
		if (CONTROLLER.PlayModeSelected == 6)
		{
			return;
		}
		CONTROLLER.pageName = "battingSC";
		if (CONTROLLER.PlayModeSelected > 3 || CONTROLLER.PlayModeSelected == 7)
		{
			BtnTeamName.transform.parent.gameObject.SetActive(value: false);
		}
		if (CONTROLLER.PlayModeSelected == 7)
		{
			if (CONTROLLER.currentInnings < 2)
			{
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 > 7)
				{
					scrollView.value = 0f;
				}
				else
				{
					scrollView.value = 1f;
				}
				SelectedInnings = 1;
				SelectedInningsText.text = LocalizationData.instance.getText(470);
			}
			else
			{
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets2 > 7)
				{
					scrollView.value = 0f;
				}
				else
				{
					scrollView.value = 1f;
				}
				SelectedInnings = 2;
				SelectedInningsText.text = LocalizationData.instance.getText(469);
			}
			if (CONTROLLER.currentInnings == 1)
			{
				BtnTeamName.transform.parent.gameObject.SetActive(value: true);
			}
			if (CONTROLLER.currentInnings > 1)
			{
				SelectedInningsText.transform.parent.gameObject.SetActive(value: true);
			}
			else
			{
				SelectedInningsText.transform.parent.gameObject.SetActive(value: false);
			}
			DisplayList(CONTROLLER.BattingTeamIndex);
		}
		else
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets > 7)
			{
			}
			SelectedInningsText.transform.parent.gameObject.SetActive(value: false);
		}
		if (!BG.activeSelf)
		{
			BG.SetActive(value: true);
		}
		if ((!Singleton<GameModel>.instance.isGamePaused && !CONTROLLER.gameCompleted) || CONTROLLER.ShowTMGameOver)
		{
			ContinueBtn.gameObject.SetActive(value: true);
			BackBtn.gameObject.SetActive(value: false);
		}
		else if (CONTROLLER.isFromAutoPlay)
		{
			Singleton<GameModel>.instance.isGamePaused = false;
			ContinueBtn.gameObject.SetActive(value: true);
			BackBtn.gameObject.SetActive(value: false);
		}
		else
		{
			BackKeyEnable = true;
			ContinueBtn.gameObject.SetActive(value: false);
			BackBtn.gameObject.SetActive(value: true);
		}
		scrollList.transform.position = startPos;
		scoreCard.SetActive(value: true);
		Singleton<BattingControls>.instance.EnableRun(boolean: false);
		Singleton<BattingControls>.instance.EnableCancelRun(boolean: false);
		CheckBatsmanStatus();
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TMSetTeamInfo();
		}
	}

	public void TMResetBattingCard()
	{
		int battingTeamIndex = CONTROLLER.BattingTeamIndex;
		SetTeamInfo();
		for (int i = 0; i < batsman.Length; i++)
		{
			batsman[i].Highlight.sprite = batsmanStates[0];
			batsman[i].Name.font = normal;
			batsman[i].Name.text = CONTROLLER.TeamList[battingTeamIndex].PlayerList[i].ScoreboardName.ToUpper();
			batsman[i].Status.text = string.Empty.ToUpper();
			batsman[i].Runs.text = string.Empty;
			batsman[i].Balls.text = string.Empty;
			batsman[i].SR.text = string.Empty;
			batsman[i].Fours.text = string.Empty;
			batsman[i].Sixes.text = string.Empty;
			batsman[i].FOW.text = string.Empty;
		}
		ScoreText.text = GetScore(battingTeamIndex);
		ExtrasText.text = string.Empty;
		OversText.text = GetOvers(battingTeamIndex);
		if (OversText.text.Contains("("))
		{
			Debug.Log(OversText.text);
			OversText.text = OversText.text.Replace("(", LocalizationData.instance.getText(651));
		}
		if (OversText.text.Contains(")"))
		{
			Debug.Log(OversText.text);
			OversText.text = OversText.text.Replace(")", LocalizationData.instance.getText(652));
		}
	}

	private string GetScore(int TeamID)
	{
		int num;
		int num2;
		if (SelectedInnings == 1)
		{
			num = CONTROLLER.TeamList[TeamID].TMcurrentMatchWickets1;
			num2 = CONTROLLER.TeamList[TeamID].TMcurrentMatchScores1;
			int num3 = CONTROLLER.TeamList[TeamID].TMcurrentMatchBalls1 / 6;
			int num4 = CONTROLLER.TeamList[TeamID].TMcurrentMatchBalls1 % 6;
		}
		else
		{
			num = CONTROLLER.TeamList[TeamID].TMcurrentMatchWickets2;
			num2 = CONTROLLER.TeamList[TeamID].TMcurrentMatchScores2;
			int num3 = CONTROLLER.TeamList[TeamID].TMcurrentMatchBalls2 / 6;
			int num4 = CONTROLLER.TeamList[TeamID].TMcurrentMatchBalls2 % 6;
		}
		string text = string.Empty;
		if (SelectedInnings == 1)
		{
			if (CONTROLLER.TeamList[TeamID].isDeclared1)
			{
				text = " DEC";
			}
		}
		else if (CONTROLLER.TeamList[TeamID].isDeclared2)
		{
			text = " DEC";
		}
		return num2 + "/" + num + text;
	}

	private string GetOvers(int TeamID)
	{
		int num;
		int num2;
		if (SelectedInnings == 1)
		{
			num = CONTROLLER.TeamList[TeamID].TMcurrentMatchBalls1 / 6;
			num2 = CONTROLLER.TeamList[TeamID].TMcurrentMatchBalls1 % 6;
		}
		else
		{
			num = CONTROLLER.TeamList[TeamID].TMcurrentMatchBalls2 / 6;
			num2 = CONTROLLER.TeamList[TeamID].TMcurrentMatchBalls2 % 6;
		}
		return LocalizationData.instance.getText(184) + string.Empty + LocalizationData.instance.getText(650) + " " + num + "." + num2 + "(" + CONTROLLER.totalOvers + ")";
	}

	public void TMRestartGame()
	{
		int battingTeamIndex = CONTROLLER.BattingTeamIndex;
		SetTeamInfo();
		for (int i = 0; i < batsman.Length; i++)
		{
			if (CONTROLLER.currentInnings < 2)
			{
				if (CONTROLLER.TeamList[battingTeamIndex].PlayerList[i].BatsmanList.TMStatus1 == string.Empty)
				{
					batsman[i].Highlight.sprite = batsmanStates[0];
					batsman[i].Name.font = normal;
					batsman[i].Name.text = CONTROLLER.TeamList[battingTeamIndex].PlayerList[i].ScoreboardName.ToUpper();
					batsman[i].Status.text = string.Empty.ToUpper();
					batsman[i].Runs.text = string.Empty;
					batsman[i].Balls.text = string.Empty;
					batsman[i].SR.text = string.Empty;
					batsman[i].Fours.text = string.Empty;
					batsman[i].Sixes.text = string.Empty;
					batsman[i].FOW.text = string.Empty;
				}
				else
				{
					TMUpdateWicket(i);
				}
			}
			else if (CONTROLLER.TeamList[battingTeamIndex].PlayerList[i].BatsmanList.TMStatus2 == string.Empty)
			{
				batsman[i].Highlight.sprite = batsmanStates[0];
				batsman[i].Name.font = normal;
				batsman[i].Name.text = CONTROLLER.TeamList[battingTeamIndex].PlayerList[i].ScoreboardName.ToUpper();
				batsman[i].Status.text = string.Empty.ToUpper();
				batsman[i].Runs.text = string.Empty;
				batsman[i].Balls.text = string.Empty;
				batsman[i].SR.text = string.Empty;
				batsman[i].Fours.text = string.Empty;
				batsman[i].Sixes.text = string.Empty;
				batsman[i].FOW.text = string.Empty;
			}
			else
			{
				TMUpdateWicket(i);
			}
		}
		ScoreText.text = GetScore(battingTeamIndex);
		ExtrasText.text = string.Empty;
		OversText.text = GetOvers(battingTeamIndex);
		if (OversText.text.Contains("("))
		{
			Debug.Log(OversText.text);
			OversText.text = OversText.text.Replace("(", LocalizationData.instance.getText(651));
		}
		if (OversText.text.Contains(")"))
		{
			Debug.Log(OversText.text);
			OversText.text = OversText.text.Replace(")", LocalizationData.instance.getText(652));
		}
	}

	public void TMUpdateScoreCard()
	{
		Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite in flags)
		{
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
			{
				SCTeamFlag.sprite = sprite;
			}
		}
		SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper();
		int num = ((battingIndex != 0) ? CONTROLLER.BowlingTeamIndex : CONTROLLER.BattingTeamIndex);
		int strikerIndex = CONTROLLER.StrikerIndex;
		if (strikerIndex >= 0 && strikerIndex < CONTROLLER.TeamList[num].PlayerList.Length)
		{
			if (SelectedInnings == 1)
			{
				batsman[strikerIndex].Status.text = CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMStatus1.ToUpper();
				batsman[strikerIndex].Runs.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMRunsScored1;
				batsman[strikerIndex].Balls.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMBallsPlayed1;
				batsman[strikerIndex].SR.text = TMGetStrikeRate(num, strikerIndex);
				batsman[strikerIndex].Fours.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMFours1;
				batsman[strikerIndex].Sixes.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMSixes1;
			}
			else
			{
				batsman[strikerIndex].Status.text = CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMStatus2.ToUpper();
				batsman[strikerIndex].Runs.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMRunsScored2;
				batsman[strikerIndex].Balls.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMBallsPlayed2;
				batsman[strikerIndex].SR.text = TMGetStrikeRate(num, strikerIndex);
				batsman[strikerIndex].Fours.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMFours2;
				batsman[strikerIndex].Sixes.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMSixes2;
			}
			if (batsman[strikerIndex].Status.text == "not out".ToUpper())
			{
				batsman[strikerIndex].Highlight.sprite = batsmanStates[1];
				batsman[strikerIndex].Name.font = bold;
				batsman[strikerIndex].FOW.text = string.Empty;
			}
			else if (batsman[strikerIndex].Status.text == string.Empty)
			{
				batsman[strikerIndex].Name.font = normal;
				batsman[strikerIndex].Highlight.sprite = batsmanStates[0];
			}
			else
			{
				batsman[strikerIndex].Name.font = normal;
				batsman[strikerIndex].Highlight.sprite = batsmanStates[2];
			}
		}
		strikerIndex = CONTROLLER.NonStrikerIndex;
		if (strikerIndex >= 0 && strikerIndex < CONTROLLER.TeamList[num].PlayerList.Length)
		{
			if (SelectedInnings == 1)
			{
				batsman[strikerIndex].Status.text = CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMStatus1.ToUpper();
				batsman[strikerIndex].Runs.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMRunsScored1;
				batsman[strikerIndex].Balls.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMBallsPlayed1;
				batsman[strikerIndex].SR.text = TMGetStrikeRate(num, strikerIndex);
				batsman[strikerIndex].Fours.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMFours1;
				batsman[strikerIndex].Sixes.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMSixes1;
			}
			else
			{
				batsman[strikerIndex].Status.text = CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMStatus2.ToUpper();
				batsman[strikerIndex].Runs.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMRunsScored2;
				batsman[strikerIndex].Balls.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMBallsPlayed2;
				batsman[strikerIndex].SR.text = TMGetStrikeRate(num, strikerIndex);
				batsman[strikerIndex].Fours.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMFours2;
				batsman[strikerIndex].Sixes.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[strikerIndex].BatsmanList.TMSixes2;
			}
			if (batsman[strikerIndex].Status.text == "not out".ToUpper())
			{
				batsman[strikerIndex].Name.font = bold;
				batsman[strikerIndex].Highlight.sprite = batsmanStates[1];
				batsman[strikerIndex].FOW.text = string.Empty;
			}
			else if (batsman[strikerIndex].Status.text == string.Empty)
			{
				batsman[strikerIndex].Name.font = normal;
				batsman[strikerIndex].Highlight.sprite = batsmanStates[0];
			}
			else
			{
				batsman[strikerIndex].Name.font = normal;
				batsman[strikerIndex].Highlight.sprite = batsmanStates[2];
			}
		}
		ScoreText.text = GetScore(CONTROLLER.BattingTeamIndex);
		ExtrasText.text = string.Empty;
		OversText.text = GetOvers(CONTROLLER.BattingTeamIndex);
		if (OversText.text.Contains("("))
		{
			Debug.Log(OversText.text);
			OversText.text = OversText.text.Replace("(", LocalizationData.instance.getText(651));
		}
		if (OversText.text.Contains(")"))
		{
			Debug.Log(OversText.text);
			OversText.text = OversText.text.Replace(")", LocalizationData.instance.getText(652));
		}
		CheckBatsmanStatus();
		Singleton<BattingScoreBoardPanelTransition>.instance.PanelTransition();
	}

	public string TMGetStrikeRate(int teamID, int playerID)
	{
		float num;
		float num2;
		if (SelectedInnings == 1)
		{
			num = CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMRunsScored1;
			num2 = CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMBallsPlayed1;
		}
		else
		{
			num = CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMRunsScored2;
			num2 = CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMBallsPlayed2;
		}
		return string.Concat(arg1: (num2 > 0f) ? ((int)(num / num2 * 100f)) : 0, arg0: string.Empty);
	}

	public void TMUpdateWicket(int playerID)
	{
		batsman[playerID].Name.font = normal;
		batsman[playerID].Highlight.sprite = batsmanStates[2];
		batsman[playerID].Name.text = GetBatsmanShortName(CONTROLLER.BattingTeamIndex, playerID).ToUpper();
		if (SelectedInnings == 1)
		{
			batsman[playerID].Runs.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMRunsScored1;
			batsman[playerID].Balls.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMBallsPlayed1;
			batsman[playerID].SR.text = TMGetStrikeRate(CONTROLLER.BattingTeamIndex, playerID);
			batsman[playerID].Fours.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMFours1;
			batsman[playerID].Sixes.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMSixes1;
			batsman[playerID].Status.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMStatus1.ToUpper();
			batsman[playerID].FOW.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMFOW1;
		}
		else
		{
			batsman[playerID].Runs.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMRunsScored2;
			batsman[playerID].Balls.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMBallsPlayed2;
			batsman[playerID].SR.text = TMGetStrikeRate(CONTROLLER.BattingTeamIndex, playerID);
			batsman[playerID].Fours.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMFours2;
			batsman[playerID].Sixes.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMSixes2;
			batsman[playerID].Status.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMStatus2.ToUpper();
			batsman[playerID].FOW.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMFOW2;
		}
	}

	private void TMDisplayList(int TeamID)
	{
		for (int i = 0; i < batsman.Length; i++)
		{
			if (SelectedInnings == 1)
			{
				if (CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMStatus1 == string.Empty || CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMStatus1 == null)
				{
					batsman[i].Name.font = normal;
					batsman[i].Highlight.sprite = batsmanStates[0];
					batsman[i].Name.text = CONTROLLER.TeamList[TeamID].PlayerList[i].ScoreboardName.ToUpper();
					batsman[i].Status.text = string.Empty.ToUpper();
					batsman[i].Runs.text = string.Empty;
					batsman[i].Balls.text = string.Empty;
					batsman[i].SR.text = string.Empty;
					batsman[i].Fours.text = string.Empty;
					batsman[i].Sixes.text = string.Empty;
					batsman[i].FOW.text = string.Empty;
					continue;
				}
				if (CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMStatus1 == "not out".ToUpper())
				{
					batsman[i].Name.font = bold;
					batsman[i].Highlight.sprite = batsmanStates[1];
					batsman[i].FOW.text = string.Empty;
				}
				else
				{
					batsman[i].Name.font = normal;
					batsman[i].Highlight.sprite = batsmanStates[2];
					batsman[i].FOW.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMFOW1;
				}
				batsman[i].Name.text = GetBatsmanShortName(TeamID, i).ToUpper();
				batsman[i].Runs.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMRunsScored1;
				batsman[i].Balls.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMBallsPlayed1;
				batsman[i].SR.text = TMGetStrikeRate(TeamID, i);
				batsman[i].Fours.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMFours1;
				batsman[i].Sixes.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMSixes1;
				batsman[i].Status.text = CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMStatus1.ToUpper();
			}
			else if (CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMStatus2 == string.Empty || CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMStatus2 == null)
			{
				batsman[i].Name.font = normal;
				batsman[i].Highlight.sprite = batsmanStates[0];
				batsman[i].Name.text = CONTROLLER.TeamList[TeamID].PlayerList[i].ScoreboardName.ToUpper();
				batsman[i].Status.text = string.Empty.ToUpper();
				batsman[i].Runs.text = string.Empty;
				batsman[i].Balls.text = string.Empty;
				batsman[i].SR.text = string.Empty;
				batsman[i].Fours.text = string.Empty;
				batsman[i].Sixes.text = string.Empty;
				batsman[i].FOW.text = string.Empty;
			}
			else
			{
				if (CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMStatus2 == "not out".ToUpper())
				{
					batsman[i].Name.font = bold;
					batsman[i].Highlight.sprite = batsmanStates[1];
					batsman[i].FOW.text = string.Empty;
				}
				else
				{
					batsman[i].Name.font = normal;
					batsman[i].Highlight.sprite = batsmanStates[2];
					batsman[i].FOW.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMFOW2;
				}
				batsman[i].Name.text = GetBatsmanShortName(TeamID, i).ToUpper();
				batsman[i].Runs.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMRunsScored2;
				batsman[i].Balls.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMBallsPlayed2;
				batsman[i].SR.text = TMGetStrikeRate(TeamID, i);
				batsman[i].Fours.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMFours2;
				batsman[i].Sixes.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMSixes2;
				batsman[i].Status.text = CONTROLLER.TeamList[TeamID].PlayerList[i].BatsmanList.TMStatus2.ToUpper();
			}
		}
		ScoreText.text = GetScore(CONTROLLER.BattingTeamIndex);
		ExtrasText.text = string.Empty;
		OversText.text = GetOvers(CONTROLLER.BattingTeamIndex);
		if (OversText.text.Contains("("))
		{
			Debug.Log(OversText.text);
			OversText.text = OversText.text.Replace("(", LocalizationData.instance.getText(651));
		}
		if (OversText.text.Contains(")"))
		{
			Debug.Log(OversText.text);
			OversText.text = OversText.text.Replace(")", LocalizationData.instance.getText(652));
		}
	}
}
