using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : Singleton<Scoreboard>
{
	private GroundController groundControllerScript;

	public GameObject scoreBoard;

	public GameObject SOInfoTab;

	public GameObject toHide;

	public GameObject testMatchInfo;

	public Image LogoFlag;

	public Text LogoText;

	public Text SOInfoText;

	public Text PowerplayText;

	public Button pauseBtn;

	public Button SOPauseBtn;

	public Text ScoreTxt;

	public Text BowlerToBatsmanText;

	public Text OverTxt;

	public GameObject TargetBG;

	public Text TargetTxt;

	public Text StrikerName;

	private string[] LevelDescriptionArray = new string[18]
	{
		LocalizationData.instance.getText(278),
		LocalizationData.instance.getText(279),
		LocalizationData.instance.getText(281),
		LocalizationData.instance.getText(280),
		LocalizationData.instance.getText(279),
		LocalizationData.instance.getText(278),
		LocalizationData.instance.getText(282),
		LocalizationData.instance.getText(279),
		LocalizationData.instance.getText(280),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527)
	};

	public Text NonStrikerName;

	public string str = string.Empty;

	public GameObject StripBG;

	public Text StripTxt;

	public GameObject BallInfo;

	public List<Text> BallInfoList;

	public List<Text> BallExtras;

	public GameObject freeHitGO;

	public ScrollRect ballScroll;

	private Vector3 pausePos;

	private Vector2 pauseSize;

	private Transform _transform;

	private bool canShowTargetBg;

	public Transform tutorialBtn;

	public Transform[] tutorialBG;

	private bool theRingIsFired;

	protected void Awake()
	{
		if (CONTROLLER.PlayModeSelected == 4)
		{
			ShowChallengeTitle();
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			CTTargetToWin();
		}
		else
		{
			UpdateSOTabText(string.Empty);
		}
		groundControllerScript = GameObject.Find("GroundController").GetComponent<GroundController>();
		Hide(boolean: true);
		showFreeHitBg(canShow: false);
	}

	public void HideScoreBoard()
	{
		toHide.transform.DOLocalMoveY(-250f, 1f).SetUpdate(isIndependentUpdate: true);
	}

	public void ShowScoreBoard()
	{
		toHide.transform.DOLocalMoveY(0f, 0.5f).SetUpdate(isIndependentUpdate: true);
	}

	public void UpdateSOTabText(string str)
	{
		SOInfoText.text = str;
		if (str != string.Empty)
		{
			SOInfoTab.SetActive(value: true);
		}
		else
		{
			SOInfoTab.SetActive(value: false);
		}
	}

	public void ShowChallengeTitle()
	{
		if (CONTROLLER.SuperOverMode == "bat")
		{
			UpdateSOTabText(LevelDescriptionArray[(int)Mathf.Floor(CONTROLLER.LevelId / 2)]);
		}
		else
		{
			UpdateSOTabText(LevelDescriptionArray[CONTROLLER.LevelId + 9]);
		}
	}

	public void pauseGame()
	{
		if (pauseBtn.gameObject.activeInHierarchy && pauseBtn.enabled)
		{
			Singleton<PauseGameScreen>.instance.Hide(boolean: false);
			Singleton<GameModel>.instance.GamePaused(boolean: true);
		}
	}

	public void CTTargetToWin()
	{
		int num = CONTROLLER.TargetToChase - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores;
		int num2 = CONTROLLER.totalOvers * 6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls;
		int num3 = CONTROLLER.totalWickets - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets;
		string text = LocalizationData.instance.getText(528);
		UpdateSOTabText(text);
	}

	public void showFreeHitBg(bool canShow)
	{
		if (canShow)
		{
			TargetBG.SetActive(value: false);
			freeHitGO.SetActive(value: true);
			return;
		}
		if (canShowTargetBg)
		{
			TargetBG.SetActive(value: true);
		}
		else
		{
			TargetBG.SetActive(value: false);
		}
		freeHitGO.SetActive(value: false);
	}

	public string ReplaceText(string original, string replace1, string replace2)
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

	public void UpdateScoreCard()
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			testMatchInfo.SetActive(value: true);
			if (CONTROLLER.currentInnings < 2)
			{
				testMatchInfo.GetComponentInChildren<Text>().text = LocalizationData.instance.getText(465);
				testMatchInfo.GetComponentInChildren<Text>().text = ReplaceText(testMatchInfo.GetComponentInChildren<Text>().text, CONTROLLER.currentDay.ToString(), "1");
			}
			else
			{
				testMatchInfo.GetComponentInChildren<Text>().text = LocalizationData.instance.getText(465);
				testMatchInfo.GetComponentInChildren<Text>().text = ReplaceText(testMatchInfo.GetComponentInChildren<Text>().text, CONTROLLER.currentDay.ToString(), "2");
			}
			UpdateBowlerToBatsmanLabel();
			int num;
			int num2;
			if (CONTROLLER.currentInnings < 2)
			{
				ScoreTxt.text = CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchScores1 + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets1;
				num = CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls1 / 6;
				num2 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls1 % 6;
			}
			else
			{
				ScoreTxt.text = CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchScores2 + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2;
				num = CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls2 / 6;
				num2 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls2 % 6;
			}
			string text = num + "." + num2 + " (" + CONTROLLER.totalOvers.ToString() + ")";
			OverTxt.text = text;
		}
		else
		{
			testMatchInfo.SetActive(value: false);
			BowlerToBatsmanText.text = string.Empty;
			ScoreTxt.text = Singleton<GameModel>.instance.ScoreStr;
			OverTxt.text = Singleton<GameModel>.instance.OversStr;
		}
		string abbrevation = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation;
		Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite in flags)
		{
			if (sprite.name.ToUpper() == abbrevation)
			{
				LogoFlag.sprite = sprite;
			}
		}
		LogoText.text = abbrevation;
		string text2;
		int num3;
		int num4;
		if (CONTROLLER.StrikerIndex >= 0 && CONTROLLER.StrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length)
		{
			text2 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].ScoreboardName;
			if (CONTROLLER.PlayModeSelected == 7)
			{
				if (CONTROLLER.currentInnings < 2)
				{
					num3 = TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex);
					num4 = TestMatchBatsman.GetBallsPlayed(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex);
				}
				else
				{
					num3 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMRunsScored2;
					num4 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMBallsPlayed2;
				}
			}
			else
			{
				num3 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored;
				num4 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.BallsPlayed;
			}
		}
		else
		{
			text2 = string.Empty;
			num3 = 0;
			num4 = 0;
		}
		if (text2.Length > 10)
		{
			text2 = text2.Substring(0, 10);
		}
		StrikerName.text = text2 + "* " + num3 + "(" + num4 + ")";
		if (CONTROLLER.NonStrikerIndex >= 0 && CONTROLLER.NonStrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length)
		{
			text2 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].ScoreboardName;
			if (CONTROLLER.PlayModeSelected == 7)
			{
				if (CONTROLLER.currentInnings < 2)
				{
					num3 = TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.NonStrikerIndex);
					num4 = TestMatchBatsman.GetBallsPlayed(CONTROLLER.BattingTeamIndex, CONTROLLER.NonStrikerIndex);
				}
				else
				{
					num3 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.TMRunsScored2;
					num4 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.TMBallsPlayed2;
				}
			}
			else
			{
				num3 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.RunsScored;
				num4 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.BallsPlayed;
			}
		}
		else
		{
			text2 = string.Empty;
			num3 = 0;
			num4 = 0;
		}
		if (text2.Length > 10)
		{
			text2 = text2.Substring(0, 10);
		}
		Text strikerName = StrikerName;
		string text3 = strikerName.text;
		strikerName.text = text3 + "\t\t" + text2 + " " + num3 + "(" + num4 + ")";
		NonStrikerName.text = string.Empty;
		string[] array = Singleton<ScoreBoardBallList>.instance.ballList.ToArray();
		string[] array2 = Singleton<ScoreBoardBallList>.instance.extras.ToArray();
		if (array.Length > 6)
		{
			ballScroll.content.pivot = new Vector2(1f, 0.5f);
			if (array.Length > 14)
			{
				for (int j = 0; j < array.Length - BallInfoList.Count; j++)
				{
					GameObject gameObject = Object.Instantiate(BallInfo);
					gameObject.transform.parent = BallInfoList[0].transform.parent.transform.parent;
					BallInfoList.Add(gameObject.transform.GetChild(1).GetComponent<Text>());
					BallExtras.Add(gameObject.transform.GetChild(0).GetComponent<Text>());
					gameObject.transform.localScale = Vector3.one;
				}
			}
			for (int j = 0; j < BallInfoList.Count; j++)
			{
				if (j < array.Length)
				{
					BallInfoList[j].transform.parent.gameObject.SetActive(value: true);
					BallInfoList[j].gameObject.SetActive(value: true);
					BallInfoList[j].text = array[j];
				}
				else
				{
					BallInfoList[j].transform.parent.gameObject.SetActive(value: false);
					BallInfoList[j].gameObject.SetActive(value: false);
				}
			}
		}
		else
		{
			ballScroll.content.pivot = new Vector2(0f, 0.5f);
			for (int j = BallInfoList.Count - 1; j >= 0; j--)
			{
				if (j < 15)
				{
					if (j < array.Length)
					{
						BallInfoList[j].transform.parent.gameObject.SetActive(value: true);
						BallInfoList[j].gameObject.SetActive(value: true);
						BallInfoList[j].text = array[j];
					}
					else
					{
						BallInfoList[j].transform.parent.gameObject.SetActive(value: false);
					}
				}
				else
				{
					Object.Destroy(BallInfoList[j].transform.parent.gameObject);
					BallInfoList.RemoveAt(j);
				}
			}
		}
		BallInfoList.TrimExcess();
		array = null;
		for (int j = 0; j < BallInfoList.Count; j++)
		{
			BallExtras[j].text = " ";
		}
		for (int j = 0; j < array2.Length; j += 2)
		{
			BallExtras[int.Parse(array2[j])].text = array2[j + 1];
		}
		BallExtras.TrimExcess();
		array2 = null;
		if (CONTROLLER.PowerPlay)
		{
			PowerplayText.gameObject.SetActive(value: true);
		}
		else
		{
			PowerplayText.gameObject.SetActive(value: false);
		}
		if (CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5)
		{
			Singleton<SuperOverScoreboard>.instance.UpdateScoreboard();
		}
	}

	public void ShowTargetScreen(bool boolean)
	{
		if (boolean)
		{
			canShowTargetBg = true;
			TargetBG.SetActive(value: true);
			TargetTxt.text = string.Empty + CONTROLLER.TargetToChase;
		}
		else
		{
			canShowTargetBg = false;
			TargetTxt.text = string.Empty;
			TargetBG.SetActive(value: false);
		}
	}

	public void UpdateStripText(string str)
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			StripTxt.text = string.Empty;
		}
		else
		{
			StripTxt.text = str;
		}
	}

	public void HideStrip(bool boolean)
	{
		UpdateStripText(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation + " Vs " + CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation);
		if (!boolean)
		{
			StripBG.SetActive(value: true);
		}
	}

	public void BowlerToBatsman()
	{
		string playerName = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].PlayerName;
		string playerName2 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].PlayerName;
		string text = playerName + " to " + playerName2;
		UpdateStripText(text);
	}

	public void TargetToWin()
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			UpdateStripText(string.Empty);
			return;
		}
		int num = CONTROLLER.TargetToChase - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores;
		int num2 = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls;
		string text = "Need " + num + " from " + num2 + " balls";
		CONTROLLER.RequiredRun = num;
		UpdateStripText(text);
	}

	public void NewOver()
	{
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[0] = string.Empty;
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[1] = string.Empty;
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[2] = string.Empty;
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[3] = string.Empty;
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[4] = string.Empty;
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[5] = string.Empty;
		Singleton<ScoreBoardBallList>.instance.ResetBallList();
	}

	public void Hide(bool boolean)
	{
		if (boolean)
		{
			Singleton<BattingControls>.instance.battingMeter.SetActive(value: false);
			Singleton<BattingControls>.instance.GreedyAdsImage.SetActive(value: true);
			if (CONTROLLER.PlayModeSelected != 6)
			{
				Screen.sleepTimeout = -2;
			}
			if (CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5)
			{
				Singleton<SuperOverScoreboard>.instance.holder.SetActive(value: false);
			}
			scoreBoard.SetActive(value: false);
			HidePause(boolean: true);
		}
		else
		{
			Singleton<NavigationBack>.instance.deviceBack = pauseGame;
			CONTROLLER.pageName = "Ground";
			Screen.sleepTimeout = -1;
			if (CONTROLLER.receivedAdEvent)
			{
				//Singleton<AdIntegrate>.instance.HideAd();
			}
			RealignUIObjects();
			scoreBoard.SetActive(value: true);
			HidePause(boolean: false);
		}
	}

	private void RealignUIObjects()
	{
		if (CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5)
		{
			Singleton<SuperOverScoreboard>.instance.holder.SetActive(value: true);
			toHide.SetActive(value: false);
			tutorialBtn.localPosition = new Vector3(tutorialBtn.localPosition.x, -269f, tutorialBtn.localPosition.z);
		}
		else
		{
			tutorialBtn.localPosition = new Vector3(tutorialBtn.localPosition.x, -314f, tutorialBtn.localPosition.z);
		}
		if (CONTROLLER.PlayModeSelected == 7)
		{
			Transform[] array = tutorialBG;
			foreach (Transform transform in array)
			{
				transform.localPosition = new Vector3(transform.localPosition.x, -196f, transform.localPosition.z);
			}
		}
	}

	private void OpenPause()
	{
		Singleton<PauseGameScreen>.instance.Hide(boolean: false);
	}

	public void HidePause(bool boolean)
	{
		if (boolean)
		{
			pauseBtn.gameObject.SetActive(value: false);
		}
		else
		{
			pauseBtn.gameObject.SetActive(value: true);
		}
	}

	public void UpdateBowlerToBatsmanLabel()
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i <= CONTROLLER.currentInnings; i++)
		{
			if (i < 2)
			{
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					if (CONTROLLER.BattingTeam[i] == CONTROLLER.myTeamIndex)
					{
						num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores1;
					}
					else
					{
						num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores1;
					}
				}
				else if (CONTROLLER.BattingTeam[i] == CONTROLLER.myTeamIndex)
				{
					num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores1;
				}
				else
				{
					num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores1;
				}
			}
			else if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				if (CONTROLLER.BattingTeam[i] == CONTROLLER.myTeamIndex)
				{
					num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores2;
				}
				else
				{
					num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores2;
				}
			}
			else if (CONTROLLER.BattingTeam[i] == CONTROLLER.myTeamIndex)
			{
				num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores2;
			}
			else
			{
				num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores2;
			}
		}
		int num3 = num - num2;
		string abbrevation = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation;
		if (num3 > 0)
		{
			if (CONTROLLER.currentInnings != 3)
			{
				str = abbrevation + " lead by " + Mathf.Abs(num3) + " runs.";
			}
			else if (CONTROLLER.TargetToChase + 1 > 1)
			{
				str = abbrevation + " need " + (Mathf.Abs(num3) + 1) + " runs to win.";
			}
			else
			{
				str = abbrevation + " need " + (Mathf.Abs(num3) + 1) + " run to win.";
			}
		}
		else if (num3 < 0)
		{
			if (CONTROLLER.currentInnings != 3)
			{
				str = abbrevation + " trail by " + Mathf.Abs(num3) + " runs.";
			}
			else if (CONTROLLER.TargetToChase + 1 > 1)
			{
				str = abbrevation + " need " + (Mathf.Abs(num3) + 1) + " runs to win.";
			}
			else
			{
				str = abbrevation + " need " + (Mathf.Abs(num3) + 1) + " run to win.";
			}
		}
		else if (num3 == 0)
		{
			if (CONTROLLER.currentInnings != 3)
			{
				str = "SCORES LEVEL";
			}
			else
			{
				str = abbrevation + " need " + (Mathf.Abs(num3) + 1) + " runs to win.";
			}
		}
		int num4 = 0;
		int num5 = 0;
		string empty = string.Empty;
		string empty2 = string.Empty;
		int num6 = 0;
		int num7 = 0;
		if (CONTROLLER.currentInnings == 0)
		{
			string empty3 = string.Empty;
			string empty4 = string.Empty;
			num4 = Random.Range(0, 10);
			empty3 = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation;
			empty4 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation;
			if (num4 > 2 && num4 < 7)
			{
				num5 = CONTROLLER.BowlingTeamIndex;
				empty = CONTROLLER.TeamList[num5].PlayerList[CONTROLLER.CurrentBowlerIndex].PlayerName;
				empty2 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].PlayerName;
				BowlerToBatsmanText.text = empty + " to " + empty2;
			}
			else if (CONTROLLER.currentSession > 1)
			{
				if (num4 < 3)
				{
					num6 = CONTROLLER.totalOvers - 1 - CONTROLLER.ballsBowledPerDay / 6;
					num7 = ((CONTROLLER.currentInnings >= 2) ? (6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls2 % 6) : (6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 % 6));
					if (num6 > 0)
					{
						if (num7 == 6)
						{
							BowlerToBatsmanText.text = LocalizationData.instance.getText(529) + " " + (num6 + 1);
							return;
						}
						BowlerToBatsmanText.text = LocalizationData.instance.getText(529) + " " + num6 + "." + num7;
					}
					else
					{
						BowlerToBatsmanText.text = LocalizationData.instance.getText(530) + " " + num7;
					}
				}
				else
				{
					BowlerToBatsmanText.text = empty3 + " vs " + empty4;
				}
			}
			else
			{
				BowlerToBatsmanText.text = empty3 + " vs " + empty4;
			}
		}
		else if (CONTROLLER.currentInnings == 1 || CONTROLLER.currentInnings == 2)
		{
			num4 = Random.Range(0, 10);
			if (num4 < 2)
			{
				num5 = CONTROLLER.BowlingTeamIndex;
				empty = CONTROLLER.TeamList[num5].PlayerList[CONTROLLER.CurrentBowlerIndex].PlayerName;
				empty2 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].PlayerName;
				BowlerToBatsmanText.text = empty + " to " + empty2;
			}
			else if (CONTROLLER.currentSession > 1)
			{
				if (num4 > 1 && num4 < 5)
				{
					num6 = CONTROLLER.totalOvers - 1 - CONTROLLER.ballsBowledPerDay / 6;
					num7 = ((CONTROLLER.currentInnings >= 2) ? (6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls2 % 6) : (6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 % 6));
					if (num6 > 0)
					{
						if (num7 == 6)
						{
							BowlerToBatsmanText.text = LocalizationData.instance.getText(529) + " " + (num6 + 1);
							return;
						}
						BowlerToBatsmanText.text = LocalizationData.instance.getText(529) + " " + num6 + "." + num7;
					}
					else
					{
						BowlerToBatsmanText.text = LocalizationData.instance.getText(530) + " " + num7;
					}
				}
				else
				{
					BowlerToBatsmanText.text = str;
				}
			}
			else
			{
				BowlerToBatsmanText.text = str;
			}
		}
		else
		{
			if (CONTROLLER.currentInnings != 3)
			{
				return;
			}
			num4 = Random.Range(0, 10);
			if (num4 == 0)
			{
				num5 = CONTROLLER.BowlingTeamIndex;
				empty = CONTROLLER.TeamList[num5].PlayerList[CONTROLLER.CurrentBowlerIndex].PlayerName;
				empty2 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].PlayerName;
				BowlerToBatsmanText.text = empty + " to " + empty2;
			}
			else if (CONTROLLER.currentSession > 1)
			{
				if (num4 > 0 && num4 < 4)
				{
					num6 = CONTROLLER.totalOvers - 1 - CONTROLLER.ballsBowledPerDay / 6;
					num7 = ((CONTROLLER.currentInnings >= 2) ? (6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls2 % 6) : (6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 % 6));
					if (num6 > 0)
					{
						if (num7 == 6)
						{
							BowlerToBatsmanText.text = LocalizationData.instance.getText(529) + " " + (num6 + 1);
							return;
						}
						BowlerToBatsmanText.text = LocalizationData.instance.getText(529) + " " + num6 + "." + num7;
					}
					else
					{
						BowlerToBatsmanText.text = LocalizationData.instance.getText(530) + " " + num7;
					}
				}
				else
				{
					BowlerToBatsmanText.text = str;
				}
			}
			else
			{
				BowlerToBatsmanText.text = str;
			}
		}
	}

	public Vector3 GetPausePos()
	{
		return pausePos;
	}

	public Vector2 GetPauseSize()
	{
		return pauseSize;
	}
}
