using System;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class TMSessionScreen : Singleton<TMSessionScreen>
{
	public GameObject holder;

	public Text sessionType;

	[Header("Team Name")]
	public Text team1_FirstInningsTeamName;

	public Text team2_FirstInningsTeamName;

	public Text team1_SecondInningsTeamName;

	public Text team2_SecondInningsTeamName;

	[Header("Innings GameObject")]
	public GameObject team1FirstInnings;

	public GameObject team2FirstInnings;

	public GameObject team1SecondInnings;

	public GameObject team2SecondInnings;

	[Header("Team Flag")]
	public Image team1_FirstInningsTeamFlag;

	public Image team2_FirstInningsTeamFlag;

	public Image team1_SecondInningsTeamFlag;

	public Image team2_SecondInningsTeamFlag;

	[Header("Innings Score,Target,PageTitle,RunRate")]
	public Text[] InningsTeamScore;

	public Text pageTitleText;

	public Text MatchStatusText;

	[Header("Each Innings Match Data")]
	public Text[] InningsBatsmanName;

	public Text[] InningsBatsmanScore;

	public Text[] InningsBowlerName;

	public Text[] InningsBowlerRunsGiven;

	[Header("First Innings Team 1 Top 3")]
	private int[] InningsTopBatsmen = new int[3];

	private int[] InningsTopBowler = new int[3];

	[Header("GameObject Need to Hide as Game Progress")]
	public GameObject team1_FirstInnings;

	public GameObject team2_FirstInnings;

	public GameObject team1_SecondInnings;

	public GameObject team2_SecondInnings;

	public Sprite[] sessionSprites;

	private int maxDisplayPlayer = 2;

	public GameObject sessionBreakMsgPopup;

	public Text topText;

	public Text bottomText;

	public Image sessionIcon;

	private string popupName = string.Empty;

	private bool canShowSessionBreakMessage;

	private int skipSessionCoinsValue;

	public bool isAfterSession;

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = null;
		Time.timeScale = 0f;
		AutoSave.SaveInGameMatch();
		CONTROLLER.TMisFromAutoPlay = false;
		BowlingScoreCard.fisrtTimeShow = false;
		holder.SetActive(value: true);
		SetTeamInfo();
		Singleton<PauseGameScreen>.instance.BG.SetActive(value: true);
		for (int i = 0; i <= CONTROLLER.currentInnings; i++)
		{
			SetPlayerDetails(CONTROLLER.currentTMPlayingInnings[i], i);
		}
		DisableEmptyBoxes();
	}

	private string GetScore(int TeamID)
	{
		int num;
		int num2;
		int num3;
		int num4;
		if (TeamID < 2)
		{
			num = CONTROLLER.TeamList[CONTROLLER.BattingTeam[TeamID]].TMcurrentMatchWickets1;
			num2 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[TeamID]].TMcurrentMatchScores1;
			num3 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[TeamID]].TMcurrentMatchBalls1 / 6;
			num4 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[TeamID]].TMcurrentMatchBalls1 % 6;
		}
		else
		{
			num = CONTROLLER.TeamList[CONTROLLER.BattingTeam[TeamID]].TMcurrentMatchWickets2;
			num2 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[TeamID]].TMcurrentMatchScores2;
			num3 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[TeamID]].TMcurrentMatchBalls2 / 6;
			num4 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[TeamID]].TMcurrentMatchBalls2 % 6;
		}
		string text = string.Empty;
		if (CONTROLLER.currentInnings < 2)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[TeamID]].isDeclared1 && CONTROLLER.TeamList[CONTROLLER.BattingTeam[TeamID]].TMcurrentMatchWickets1 != 10)
			{
				text = " DEC ";
			}
		}
		else if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[TeamID]].isDeclared2 && CONTROLLER.TeamList[CONTROLLER.BattingTeam[TeamID]].TMcurrentMatchWickets2 != 10)
		{
			text = " DEC ";
		}
		return num2 + "/" + num + text + " in " + num3 + "." + num4 + " Overs";
	}

	private void SetTeamInfo()
	{
		team1FirstInnings.SetActive(value: false);
		team2FirstInnings.SetActive(value: false);
		team1SecondInnings.SetActive(value: false);
		team2SecondInnings.SetActive(value: false);
		sessionBreakMsgPopup.SetActive(value: false);
		team1FirstInnings.SetActive(value: true);
		Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite in flags)
		{
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeam[0]].abbrevation)
			{
				team1_FirstInningsTeamFlag.sprite = sprite;
			}
		}
		team1_FirstInningsTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeam[0]].teamName + " - 1st Innings";
		int num = 0;
		InningsTeamScore[0].text = GetScore(0);
		if (CONTROLLER.currentInnings >= 1)
		{
			team2FirstInnings.SetActive(value: true);
			Sprite[] flags2 = Singleton<FlagHolderGround>.instance.flags;
			foreach (Sprite sprite2 in flags2)
			{
				if (sprite2.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeam[1]].abbrevation)
				{
					team2_FirstInningsTeamFlag.sprite = sprite2;
				}
			}
			team2_FirstInningsTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeam[1]].teamName + " - 1st Innings";
			InningsTeamScore[1].text = GetScore(1);
			if (CONTROLLER.currentInnings >= 2)
			{
				team1SecondInnings.SetActive(value: true);
				Sprite[] flags3 = Singleton<FlagHolderGround>.instance.flags;
				foreach (Sprite sprite3 in flags3)
				{
					if (sprite3.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeam[2]].abbrevation)
					{
						team1_SecondInningsTeamFlag.sprite = sprite3;
					}
				}
				team1_SecondInningsTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeam[2]].teamName + " - 2nd Innings";
				InningsTeamScore[2].text = GetScore(2);
				if (CONTROLLER.currentInnings >= 3)
				{
					team2SecondInnings.SetActive(value: true);
					Sprite[] flags4 = Singleton<FlagHolderGround>.instance.flags;
					foreach (Sprite sprite4 in flags4)
					{
						if (sprite4.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeam[3]].abbrevation)
						{
							team2_SecondInningsTeamFlag.sprite = sprite4;
						}
					}
					team2_SecondInningsTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeam[3]].teamName + " - 2nd Innings";
					InningsTeamScore[3].text = GetScore(3);
				}
			}
		}
		string text = string.Empty;
		if (!Singleton<GameModel>.instance.CheckForInningsComplete() || CONTROLLER.isSkipBallsForSession)
		{
			switch ((CONTROLLER.currentSession + 1) % 3)
			{
			case 1:
				sessionBreakMsgPopup.SetActive(value: true);
				text = " - " + LocalizationData.instance.getText(557) + " " + LocalizationData.instance.getText(556);
				CONTROLLER.sessionName = "lunch";
				break;
			case 2:
				sessionBreakMsgPopup.SetActive(value: true);
				text = " - " + LocalizationData.instance.getText(555) + " " + LocalizationData.instance.getText(556);
				CONTROLLER.sessionName = "tea";
				break;
			default:
				if (CONTROLLER.ballsBowledPerDay >= CONTROLLER.totalOvers * 6)
				{
					sessionBreakMsgPopup.SetActive(value: true);
					text = " - " + LocalizationData.instance.getText(467);
					CONTROLLER.sessionName = "stumps";
				}
				break;
			}
		}
		string empty = string.Empty;
		string empty2 = string.Empty;
		empty = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation;
		empty2 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation;
		pageTitleText.text = "INNINGS SUMMARY - " + empty + " vs " + empty2;
		MatchStatusText.text = " " + LocalizationData.instance.getText(466) + " " + CONTROLLER.currentDay + text;
		int num2 = (CONTROLLER.currentSession + 1) % 3;
		int num3 = CONTROLLER.Overs[ObscuredPrefs.GetInt("TMOvers")] * 6;
		if (CONTROLLER.ballsBowledPerDay >= num3)
		{
			topText.text = LocalizationData.instance.getText(467);
			bottomText.text = LocalizationData.instance.getText(468) + " " + CONTROLLER.currentDay;
			sessionIcon.sprite = sessionSprites[2];
		}
		else if (CONTROLLER.ballsBowledPerDay >= num3 * 2 / 3)
		{
			topText.text = LocalizationData.instance.getText(555);
			bottomText.text = LocalizationData.instance.getText(556);
			sessionIcon.sprite = sessionSprites[1];
		}
		else if (CONTROLLER.ballsBowledPerDay >= num3 / 3)
		{
			topText.text = LocalizationData.instance.getText(557);
			bottomText.text = LocalizationData.instance.getText(556);
			sessionIcon.sprite = sessionSprites[0];
		}
	}

	private void SortPlayerForSummary(int _index)
	{
		bool flag = true;
		bool flag2 = true;
		List<float> list = new List<float>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		for (int i = 0; i < 3; i++)
		{
			InningsTopBatsmen[i] = -1;
			InningsTopBowler[i] = -1;
		}
		int num = CONTROLLER.BattingTeam[_index];
		int num2 = CONTROLLER.BowlingTeam[_index];
		for (int i = 0; i < CONTROLLER.TeamList[num].PlayerList.Length; i++)
		{
			int num3;
			int num4;
			if (_index < 2)
			{
				num3 = TestMatchBatsman.GetRunsScored(num, i);
				num4 = TestMatchBatsman.GetBallsPlayed(num, i);
			}
			else
			{
				num3 = CONTROLLER.TeamList[num].PlayerList[i].BatsmanList.TMRunsScored2;
				num4 = CONTROLLER.TeamList[num].PlayerList[i].BatsmanList.TMBallsPlayed2;
			}
			for (int j = 0; j < InningsTopBatsmen.Length; j++)
			{
				int num5;
				if (InningsTopBatsmen[j] > -1)
				{
					num5 = ((_index >= 2) ? CONTROLLER.TeamList[num].PlayerList[j].BatsmanList.TMRunsScored2 : TestMatchBatsman.GetRunsScored(num, j));
					if (num3 == num5 && num3 > 0)
					{
						int ballsPlayed = TestMatchBatsman.GetBallsPlayed(num, InningsTopBatsmen[j]);
						if (num4 < ballsPlayed)
						{
							num3++;
						}
					}
				}
				else
				{
					num5 = 0;
				}
				if (num3 > num5 && num3 > 0)
				{
					flag2 = false;
					num5 = InningsTopBatsmen[j];
					InningsTopBatsmen[j] = i;
					for (int k = j + 1; k < InningsTopBatsmen.Length; k++)
					{
						int num6 = InningsTopBatsmen[k];
						InningsTopBatsmen[k] = num5;
						num5 = num6;
					}
					break;
				}
				if (!flag2)
				{
					continue;
				}
				if (_index < 2)
				{
					if (TestMatchBatsman.GetStatus(num, i) == "not out")
					{
						list3.Add(i);
						break;
					}
				}
				else if (CONTROLLER.TeamList[num].PlayerList[i].BatsmanList.TMStatus2 == "not out")
				{
					list3.Add(i);
					break;
				}
			}
			int num7;
			if (_index < 2)
			{
				num3 = CONTROLLER.TeamList[num2].PlayerList[i].BowlerList.TMWicket1;
				num4 = CONTROLLER.TeamList[num2].PlayerList[i].BowlerList.TMRunsGiven1;
				num7 = CONTROLLER.TeamList[num2].PlayerList[i].BowlerList.TMBallsBowled1;
			}
			else
			{
				num3 = CONTROLLER.TeamList[num2].PlayerList[i].BowlerList.TMWicket2;
				num4 = CONTROLLER.TeamList[num2].PlayerList[i].BowlerList.TMRunsGiven2;
				num7 = CONTROLLER.TeamList[num2].PlayerList[i].BowlerList.TMBallsBowled2;
			}
			for (int j = 0; j < InningsTopBowler.Length; j++)
			{
				int num5;
				if (_index < 2)
				{
					if (InningsTopBowler[j] > -1)
					{
						num5 = CONTROLLER.TeamList[num2].PlayerList[InningsTopBowler[j]].BowlerList.TMWicket1;
						if (num3 == num5 && num3 > 0)
						{
							int ballsPlayed = CONTROLLER.TeamList[num2].PlayerList[InningsTopBowler[j]].BowlerList.TMRunsGiven1;
							if (num4 < ballsPlayed)
							{
								num3++;
							}
						}
					}
					else
					{
						num5 = 0;
					}
				}
				else if (InningsTopBowler[j] > -1)
				{
					num5 = CONTROLLER.TeamList[num2].PlayerList[InningsTopBowler[j]].BowlerList.TMWicket2;
					if (num3 == num5 && num3 > 0)
					{
						int ballsPlayed = CONTROLLER.TeamList[num2].PlayerList[InningsTopBowler[j]].BowlerList.TMRunsGiven2;
						if (num4 < ballsPlayed)
						{
							num3++;
						}
					}
				}
				else
				{
					num5 = 0;
				}
				if (num3 > num5 && num3 > 0)
				{
					flag = false;
					num5 = InningsTopBowler[j];
					InningsTopBowler[j] = i;
					for (int k = j + 1; k < InningsTopBowler.Length; k++)
					{
						int num6 = InningsTopBowler[k];
						InningsTopBowler[k] = num5;
						num5 = num6;
					}
					break;
				}
				if (flag && num7 > 0)
				{
					float econRate = GetEconRate(num2, i);
					list.Add(econRate);
					list2.Add(i);
					break;
				}
			}
		}
		if (flag)
		{
			float[] array = new float[list.Count];
			int[] array2 = new int[list2.Count];
			for (int k = 0; k < array.Length; k++)
			{
				array[k] = list[k];
				array2[k] = list2[k];
			}
			Array.Sort(array, array2);
			if (list.Count == 1)
			{
				InningsTopBowler[0] = array2[0];
			}
			else if (list.Count == 2)
			{
				InningsTopBowler[0] = array2[0];
				InningsTopBowler[1] = array2[1];
			}
			else if (list.Count == 3)
			{
				InningsTopBowler[0] = array2[0];
				InningsTopBowler[1] = array2[1];
				InningsTopBowler[2] = array2[2];
			}
			if (list.Count > 3)
			{
				for (int k = 0; k < InningsTopBowler.Length; k++)
				{
					InningsTopBowler[k] = array2[k];
				}
			}
		}
		if (flag2)
		{
			if (list3.Count == 1)
			{
				InningsTopBatsmen[0] = list3[0];
			}
			else if (list3.Count == 2)
			{
				InningsTopBatsmen[0] = list3[0];
				InningsTopBatsmen[1] = list3[1];
			}
		}
	}

	private void DisableEmptyBoxes()
	{
		if (team1_FirstInnings.activeInHierarchy)
		{
			if (team1_FirstInnings.transform.GetChild(0).GetChild(0).GetComponent<Text>()
				.text == string.Empty)
			{
				team1_FirstInnings.transform.GetChild(0).gameObject.SetActive(value: false);
			}
			if (team1_FirstInnings.transform.GetChild(1).GetChild(0).GetComponent<Text>()
				.text == string.Empty)
			{
				team1_FirstInnings.transform.GetChild(1).gameObject.SetActive(value: false);
			}
		}
		if (team2_FirstInnings.activeInHierarchy)
		{
			if (team2_FirstInnings.transform.GetChild(0).GetChild(0).GetComponent<Text>()
				.text == string.Empty)
			{
				team2_FirstInnings.transform.GetChild(0).gameObject.SetActive(value: false);
			}
			if (team2_FirstInnings.transform.GetChild(1).GetChild(0).GetComponent<Text>()
				.text == string.Empty)
			{
				team2_FirstInnings.transform.GetChild(1).gameObject.SetActive(value: false);
			}
		}
		if (team1_SecondInnings.activeInHierarchy)
		{
			if (team1_SecondInnings.transform.GetChild(0).GetChild(0).GetComponent<Text>()
				.text == string.Empty)
			{
				team1_SecondInnings.transform.GetChild(0).gameObject.SetActive(value: false);
			}
			if (team1_SecondInnings.transform.GetChild(1).GetChild(0).GetComponent<Text>()
				.text == string.Empty)
			{
				team1_SecondInnings.transform.GetChild(1).gameObject.SetActive(value: false);
			}
		}
		if (team2_SecondInnings.activeInHierarchy)
		{
			if (team2_SecondInnings.transform.GetChild(0).GetChild(0).GetComponent<Text>()
				.text == string.Empty)
			{
				team2_SecondInnings.transform.GetChild(0).gameObject.SetActive(value: false);
			}
			if (team2_SecondInnings.transform.GetChild(1).GetChild(0).GetComponent<Text>()
				.text == string.Empty)
			{
				team2_SecondInnings.transform.GetChild(1).gameObject.SetActive(value: false);
			}
		}
	}

	private float GetEconRate(int TeamID, int playerID)
	{
		float result = 0f;
		float num;
		float num2;
		if (CONTROLLER.currentInnings < 2)
		{
			num = CONTROLLER.TeamList[TeamID].PlayerList[playerID].BowlerList.TMRunsGiven1;
			num2 = CONTROLLER.TeamList[TeamID].PlayerList[playerID].BowlerList.TMBallsBowled1;
		}
		else
		{
			num = CONTROLLER.TeamList[TeamID].PlayerList[playerID].BowlerList.TMRunsGiven2;
			num2 = CONTROLLER.TeamList[TeamID].PlayerList[playerID].BowlerList.TMBallsBowled2;
		}
		if (num2 > 0f)
		{
			result = num / num2 * 6f;
			result = Mathf.Round(result * 100f) / 100f;
		}
		return result;
	}

	private void SetPlayerDetails(int _index, int count)
	{
		int num = CONTROLLER.BattingTeam[count];
		int num2 = CONTROLLER.BowlingTeam[count];
		SortPlayerForSummary(count);
		for (int i = 0; i < maxDisplayPlayer; i++)
		{
			InningsBatsmanName[maxDisplayPlayer * count + i].text = string.Empty;
			InningsBatsmanScore[maxDisplayPlayer * count + i].text = string.Empty;
			InningsBowlerName[maxDisplayPlayer * count + i].text = string.Empty;
			InningsBowlerRunsGiven[maxDisplayPlayer * count + i].text = string.Empty;
			if (count < 2)
			{
				if (InningsTopBatsmen[i] >= 0)
				{
					InningsBatsmanName[maxDisplayPlayer * count + i].text = CONTROLLER.TeamList[num].PlayerList[InningsTopBatsmen[i]].PlayerName;
					InningsBatsmanScore[maxDisplayPlayer * count + i].text = string.Empty + TestMatchBatsman.GetRunsScored(num, InningsTopBatsmen[i]);
					if (TestMatchBatsman.GetStatus(num, InningsTopBatsmen[i]) == "not out")
					{
						InningsBatsmanScore[maxDisplayPlayer * count + i].text += "*";
					}
					Text obj = InningsBatsmanScore[maxDisplayPlayer * count + i];
					string text = obj.text;
					obj.text = text + "(" + TestMatchBatsman.GetBallsPlayed(num, InningsTopBatsmen[i]) + ")";
				}
				if (InningsTopBowler[i] >= 0)
				{
					InningsBowlerName[maxDisplayPlayer * count + i].text = CONTROLLER.TeamList[num2].PlayerList[InningsTopBowler[i]].PlayerName;
					InningsBowlerRunsGiven[maxDisplayPlayer * count + i].text = string.Empty + CONTROLLER.TeamList[num2].PlayerList[InningsTopBowler[i]].BowlerList.TMWicket1;
					Text obj2 = InningsBowlerRunsGiven[maxDisplayPlayer * count + i];
					obj2.text = obj2.text + "-" + CONTROLLER.TeamList[num2].PlayerList[InningsTopBowler[i]].BowlerList.TMRunsGiven1;
				}
				continue;
			}
			if (InningsTopBatsmen[i] >= 0)
			{
				InningsBatsmanName[maxDisplayPlayer * count + i].text = CONTROLLER.TeamList[num].PlayerList[InningsTopBatsmen[i]].PlayerName;
				InningsBatsmanScore[maxDisplayPlayer * count + i].text = string.Empty + CONTROLLER.TeamList[num].PlayerList[InningsTopBatsmen[i]].BatsmanList.TMRunsScored2;
				if (CONTROLLER.TeamList[num].PlayerList[InningsTopBatsmen[i]].BatsmanList.TMStatus2 == "not out")
				{
					InningsBatsmanScore[maxDisplayPlayer * count + i].text += "*";
				}
				Text obj3 = InningsBatsmanScore[maxDisplayPlayer * count + i];
				string text = obj3.text;
				obj3.text = text + "(" + CONTROLLER.TeamList[num].PlayerList[InningsTopBatsmen[i]].BatsmanList.TMBallsPlayed2 + ")";
			}
			if (InningsTopBowler[i] >= 0)
			{
				InningsBowlerName[maxDisplayPlayer * count + i].text = CONTROLLER.TeamList[num2].PlayerList[InningsTopBowler[i]].PlayerName;
				InningsBowlerRunsGiven[maxDisplayPlayer * count + i].text = string.Empty + CONTROLLER.TeamList[num2].PlayerList[InningsTopBowler[i]].BowlerList.TMWicket2;
				Text obj4 = InningsBowlerRunsGiven[maxDisplayPlayer * count + i];
				obj4.text = obj4.text + "-" + CONTROLLER.TeamList[num2].PlayerList[InningsTopBowler[i]].BowlerList.TMRunsGiven2;
			}
		}
	}

	public void Continue()
	{
		HideMe();
		if (CONTROLLER.ShowTMGameOver)
		{
			Singleton<TMGameOverDisplay>.instance.ShowMe();
			return;
		}
		if (CONTROLLER.currentInnings == 0 && Singleton<GameModel>.instance.CheckForInningsComplete())
		{
			CONTROLLER.declareMode = false;
		}
		if (CONTROLLER.currentInnings == 1 && Singleton<GameModel>.instance.CheckForInningsComplete())
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
		}
		if (CONTROLLER.currentInnings == 2 && Singleton<GameModel>.instance.CheckForInningsComplete())
		{
			float num3 = CONTROLLER.TeamList[CONTROLLER.currentTMPlayingInnings[CONTROLLER.currentInnings]].currentMatchBalls;
			float num4 = CONTROLLER.totalOvers * 6;
		}
		if (!Singleton<GameModel>.instance.CheckForInningsComplete() && CONTROLLER.currentInnings < 4)
		{
			Singleton<GameModel>.instance.sessionComplete = false;
			Singleton<GameModel>.instance.setOversAfterSession();
		}
		else if (CONTROLLER.currentInnings < 4)
		{
			Singleton<GameModel>.instance.setSession();
			Singleton<GameModel>.instance.clearData();
			CONTROLLER.currentInnings++;
			Singleton<GameModel>.instance.ResetVariables();
			Time.timeScale = 1f;
		}
		else
		{
			Singleton<GameModel>.instance.inningsCompleted = true;
		}
	}

	public void ClosePopup()
	{
		sessionBreakMsgPopup.SetActive(value: false);
	}

	private void HideMe()
	{
		holder.SetActive(value: false);
		Singleton<PauseGameScreen>.instance.BG.SetActive(value: false);
	}
}
