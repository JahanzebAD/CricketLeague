using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowlingScoreCard : Singleton<BowlingScoreCard>
{
	private GroundController groundControllerScript;

	public Font bold;

	public Font normal;

	public GameObject scoreCard;

	public GameObject BG;

	public Button ContinueBtn;

	public Button BackBtn;

	public Sprite[] bowlerStates;

	public Text ScoreText;

	public Text ExtrasText;

	public Text OversText;

	public Image SCTeamFlag;

	public Image BtnTeamFlag;

	public Text SCTeamName;

	public Text BtnTeamName;

	public BowlerDetails[] bowler;

	private bool firstTimeHide;

	private List<int> BowlersIndexArray = new List<int>();

	private int firstSpellOver;

	private int currentBowler = -1;

	private int lastBowlerIndex = -1;

	private int TMLastbowler = -1;

	private GameObject introCamGO;

	private GameObject introCameraPivot;

	private Camera introCamera;

	private int previousBowler = -1;

	private int SelectedIndex = 1;

	public Text SelectedInningsText;

	public static bool fisrtTimeShow = true;

	private Transform _transform;

	private bool fromDisplayList;

	public static bool newOverCalled;

	protected void Awake()
	{
		firstTimeHide = false;
		groundControllerScript = GameObject.Find("GroundController").GetComponent<GroundController>();
		Hide(boolean: true);
	}

	public void ResetBowlingCard()
	{
		if (CONTROLLER.PlayModeSelected != 7)
		{
			currentBowler = -1;
			lastBowlerIndex = -1;
			int bowlingTeamIndex = CONTROLLER.BowlingTeamIndex;
			SetTeamInfo();
			GetBowlersInTeam(bowlingTeamIndex);
			for (int i = 0; i < bowler.Length; i++)
			{
				if (CONTROLLER.BowlingTeamIndex == CONTROLLER.opponentTeamIndex)
				{
					bowler[i].Name.font = normal;
					bowler[i].Highlight.sprite = bowlerStates[0];
					bowler[i].ClickBtn.enabled = false;
				}
				else
				{
					bowler[i].ClickBtn.enabled = true;
				}
				if (i < BowlersIndexArray.Count)
				{
					int num = BowlersIndexArray[i];
					bowler[i].Name.text = CONTROLLER.TeamList[bowlingTeamIndex].PlayerList[num].ScoreboardName.ToUpper();
					bowler[i].Type.text = GetBowlerType(bowlingTeamIndex, num).ToUpper();
					bowler[i].Overs.text = "0.0";
					bowler[i].Maiden.text = "0";
					bowler[i].Runs.text = "0";
					bowler[i].Wickets.text = "0";
					bowler[i].ERate.text = GetEconRate(bowlingTeamIndex, num);
					if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
					{
						bowler[i].ClickBtn.enabled = true;
					}
				}
				else
				{
					bowler[i].Name.text = string.Empty.ToUpper();
					bowler[i].Type.text = string.Empty.ToUpper();
					bowler[i].Overs.text = string.Empty;
					bowler[i].Maiden.text = string.Empty;
					bowler[i].Runs.text = string.Empty;
					bowler[i].Wickets.text = string.Empty;
					bowler[i].ERate.text = string.Empty;
					bowler[i].ClickBtn.enabled = false;
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
		else
		{
			TMResetBowlingCard();
		}
	}

	private void SetTeamInfo()
	{
		//if (CONTROLLER.PlayModeSelected >= 8)
		//{
		//	return;
		//}
		//if (CONTROLLER.currentInnings == 0)
		//{
		//	Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		//	foreach (Sprite sprite in flags)
		//	{
		//		if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
		//		{
		//			SCTeamFlag.sprite = sprite;
		//		}
		//		if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
		//		{
		//			BtnTeamFlag.sprite = sprite;
		//		}
		//	}
		//	SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper();
		//	BtnTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper();
		//	return;
		//}
		//Sprite[] flags2 = Singleton<FlagHolderGround>.instance.flags;
		//foreach (Sprite sprite2 in flags2)
		//{
		//	if (sprite2.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
		//	{
		//		SCTeamFlag.sprite = sprite2;
		//	}
		//	if (sprite2.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
		//	{
		//		BtnTeamFlag.sprite = sprite2;
		//	}
		//}
		//SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper();
		//BtnTeamName.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper();
	}

	public void UpdateScoreCard()
	{
		if (CONTROLLER.PlayModeSelected != 7)
		{
			Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
			foreach (Sprite sprite in flags)
			{
				if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
				{
					SCTeamFlag.sprite = sprite;
				}
				if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
				{
					BtnTeamFlag.sprite = sprite;
				}
			}
			SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper();
			BtnTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper();
			int bowlingTeamIndex = CONTROLLER.BowlingTeamIndex;
			int currentBowlerIndex = CONTROLLER.CurrentBowlerIndex;
			if (!CONTROLLER.isFromAutoPlay)
			{
				bowler[currentBowler].Name.font = bold;
				bowler[currentBowler].Highlight.sprite = bowlerStates[1];
				bowler[currentBowler].Overs.text = GetOversBowled(bowlingTeamIndex, currentBowlerIndex);
				bowler[currentBowler].Maiden.text = string.Empty + CONTROLLER.TeamList[bowlingTeamIndex].PlayerList[currentBowlerIndex].BowlerList.Maiden;
				bowler[currentBowler].Runs.text = string.Empty + CONTROLLER.TeamList[bowlingTeamIndex].PlayerList[currentBowlerIndex].BowlerList.RunsGiven;
				bowler[currentBowler].Wickets.text = string.Empty + CONTROLLER.TeamList[bowlingTeamIndex].PlayerList[currentBowlerIndex].BowlerList.Wicket;
				bowler[currentBowler].ERate.text = GetEconRate(bowlingTeamIndex, currentBowlerIndex);
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
		else
		{
			TMUpdateScoreCard();
		}
	}

	public string GetOversBowled(int teamID, int playerID)
	{
		if (CONTROLLER.PlayModeSelected != 7)
		{
			int num = CONTROLLER.TeamList[teamID].PlayerList[playerID].BowlerList.BallsBowled / 6;
			int num2 = CONTROLLER.TeamList[teamID].PlayerList[playerID].BowlerList.BallsBowled % 6;
			return num + "." + num2;
		}
		return TMGetOversBowled(teamID, playerID);
	}

	public void BowlerSelected(int index)
	{
		if (CONTROLLER.PlayModeSelected < 8 && CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex && SCTeamName.text == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper())
		{
			if (currentBowler >= 0 && currentBowler < BowlersIndexArray.Count)
			{
				bowler[currentBowler].Name.font = normal;
				bowler[currentBowler].Highlight.sprite = bowlerStates[0];
			}
			currentBowler = index;
			EliminateBowlerKeeper();
			bowler[currentBowler].Name.font = bold;
			bowler[currentBowler].Highlight.sprite = bowlerStates[1];
		}
	}

	private void GetBowlersInTeam(int TeamID)
	{
		//if (CONTROLLER.PlayModeSelected >= 8)
		//{
		//	return;
		//}
		//BowlersIndexArray = new List<int>();
		//int num = CONTROLLER.TeamList[TeamID].PlayerList.Length;
		//for (int i = 0; i < num; i++)
		//{
		//	string style = CONTROLLER.TeamList[TeamID].PlayerList[i].BowlerList.Style;
		//	if (style != string.Empty && style != null)
		//	{
		//		BowlersIndexArray.Add(i);
		//	}
		//}
		//firstSpellOver = (int)((float)CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 0.2f);
	}

	private string GetBowlerType(int teamID, int bowlerIndex)
	{
		if (CONTROLLER.PlayModeSelected < 8)
		{
			string result = string.Empty;
			if (CONTROLLER.TeamList[teamID].PlayerList[bowlerIndex].BowlerList.bowlingRank == "0")
			{
				result = ((!(CONTROLLER.TeamList[teamID].PlayerList[bowlerIndex].BowlerList.BowlingHand == "L")) ? "RIGHT ARM FAST" : "LEFT ARM FAST");
			}
			else if (CONTROLLER.TeamList[teamID].PlayerList[bowlerIndex].BowlerList.bowlingRank == "1")
			{
				result = ((!(CONTROLLER.TeamList[teamID].PlayerList[bowlerIndex].BowlerList.BowlingHand == "L")) ? "RIGHT OFF SPIN" : "LEFT OFF SPIN");
			}
			else if (CONTROLLER.TeamList[teamID].PlayerList[bowlerIndex].BowlerList.bowlingRank == "2")
			{
				result = ((!(CONTROLLER.TeamList[teamID].PlayerList[bowlerIndex].BowlerList.BowlingHand == "L")) ? "RIGHT LEG SPIN" : "LEFT LEG SPIN");
			}
			else if (CONTROLLER.TeamList[teamID].PlayerList[bowlerIndex].BowlerList.bowlingRank == "3")
			{
				result = ((!(CONTROLLER.TeamList[teamID].PlayerList[bowlerIndex].BowlerList.BowlingHand == "L")) ? "RIGHT ARM MEDIUM" : "LEFT ARM MEDIUM");
			}
			return result;
		}
		return "-";
	}

	public string GetEconRate(int TeamID, int playerID)
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			float num = 0f;
			float num2 = CONTROLLER.TeamList[TeamID].PlayerList[playerID].BowlerList.RunsGiven;
			float num3 = CONTROLLER.TeamList[TeamID].PlayerList[playerID].BowlerList.BallsBowled;
			if (num3 > 0f)
			{
				num = num2 / num3 * 6f;
				num = Mathf.Round(num * 100f) / 100f;
			}
			return num.ToString();
		}
		return TMGetEconRate(TeamID, playerID);
	}

	public void GetRandomBowler()
	{
		if (CONTROLLER.PlayModeSelected >= 8)
		{
			return;
		}
		int currentSpell = GetCurrentSpell();
		if (currentSpell == 1)
		{
			int currentOver = GetCurrentOver();
			if (currentOver % 2 == 0)
			{
				if (lastBowlerIndex != 1)
				{
					currentBowler = 1;
				}
				else
				{
					currentBowler = 0;
				}
			}
			else if (lastBowlerIndex != 0)
			{
				currentBowler = 0;
			}
			else
			{
				currentBowler = 1;
			}
		}
		else
		{
			currentBowler = Random.Range(0, BowlersIndexArray.Count);
			if (currentBowler == TMLastbowler)
			{
				if (currentBowler != 0)
				{
					currentBowler--;
				}
				else
				{
					currentBowler++;
				}
			}
			if (!CanBowlCurrentOver(currentBowler))
			{
				selectRandomBowler();
			}
		}
		if (BowlersIndexArray.Count <= 5)
		{
			fromDisplayList = false;
			checkBowlerSpell();
		}
		EliminateBowlerKeeper();
		if (CONTROLLER.isAutoplay)
		{
			CONTROLLER.CurrentBowlerIndex = BowlersIndexArray[currentBowler];
			lastBowlerIndex = currentBowler;
		}
		bowler[currentBowler].Name.font = bold;
		bowler[currentBowler].Highlight.sprite = bowlerStates[1];
		//CONTROLLER.CurrentBowlerIndex = BowlersIndexArray[currentBowler];
		lastBowlerIndex = currentBowler;
	}

	private void selectRandomBowler()
	{
		if (CONTROLLER.PlayModeSelected != 7)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < BowlersIndexArray.Count; i++)
			{
				int ballsBowled = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[BowlersIndexArray[i]].BowlerList.BallsBowled;
				int num = (int)((float)(CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6) * 0.2f);
				int num2 = num - ballsBowled;
				if (num2 > 0 && i != lastBowlerIndex)
				{
					list.Add(i);
				}
			}
			if (list.Count > 0)
			{
				int index = Random.Range(0, list.Count);
				currentBowler = list[index];
			}
		}
		else
		{
			TMselectRandomBowler();
		}
	}

	private void EliminateBowlerKeeper()
	{
		if (CONTROLLER.PlayModeSelected >= 8)
		{
			return;
		}
		int num = 0;
		for (int i = 0; i <= CONTROLLER.totalWickets; i++)
		{
			//if (i != BowlersIndexArray[currentBowler] && i != CONTROLLER.wickerKeeperIndex && i < CONTROLLER.FielderArray.Length)
			//{
			//	CONTROLLER.FielderArray[num] = i;
			//	num++;
			//}
		}
	}

	private bool CheckRemainingOvers(int bowlerindex)
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			return false;
		}
		if (CONTROLLER.PlayModeSelected < 8)
		{
			//int ballsBowled = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[BowlersIndexArray[bowlerindex]].BowlerList.BallsBowled;
			//int num = (int)((float)(CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6) * 0.2f);
			//if (num > ballsBowled)
			//{
			//	return false;
			//}
			return true;
		}
		return false;
	}

	private int GetCurrentSpell()
	{
		//if (CONTROLLER.PlayModeSelected != 7)
		//{
		//	int num = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6;
		//	if (num < firstSpellOver)
		//	{
		//		return 1;
		//	}
		//	return 2;
		//}
		return TMGetCurrentSpell();
	}

	private int GetCurrentOver()
	{
		if (CONTROLLER.PlayModeSelected != 7)
		{
			int num = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6;
			return num + 1;
		}
		return TMGetCurrentOver();
	}

	private void checkBowlerSpell()
	{
		if (CONTROLLER.PlayModeSelected != 7)
		{
			//int num = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6;
			bool flag = false;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			List<int> list = new List<int>();
			for (int i = 0; i < BowlersIndexArray.Count; i++)
			{
				num2 = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[BowlersIndexArray[i]].BowlerList.BallsBowled;
				num3 = (int)((float)(CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6) * 0.2f);
				num5 = (num3 - num2) / 6;
				if (num6 < num5)
				{
					list = new List<int>();
					num6 = num5;
					num7 = i;
					list.Add(i);
				}
				else if (num6 == num5)
				{
					list.Add(i);
				}
			}
			num5 = 0;
			for (int j = 0; j < BowlersIndexArray.Count; j++)
			{
				if (j != num7)
				{
					num2 = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[BowlersIndexArray[j]].BowlerList.BallsBowled;
					num3 = (int)((float)(CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6) * 0.2f);
					num5 += (num3 - num2) / 6;
				}
			}
			for (int k = 0; k < BowlersIndexArray.Count; k++)
			{
				num2 = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[BowlersIndexArray[k]].BowlerList.BallsBowled;
				num3 = (int)((float)(CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6) * 0.2f);
				num4 = num3 - num2;
				if (num2 <= CONTROLLER.oversSelectedIndex * 6 - 6 && num6 > num5 && list.Count <= 1 && k != lastBowlerIndex)
				{
					flag = true;
					currentBowler = num7;
				}
			}
			for (int l = 0; l < BowlersIndexArray.Count; l++)
			{
				if (flag && l != currentBowler)
				{
					bowler[l].ClickBtn.enabled = false;
					bowler[currentBowler].Name.font = normal;
					bowler[l].Highlight.sprite = bowlerStates[2];
				}
			}
		}
		else
		{
			TMcheckBowlerSpell();
		}
	}

	private bool CanBowlCurrentOver(int bowlerID)
	{
		if (CONTROLLER.PlayModeSelected != 7)
		{
			if (bowlerID == lastBowlerIndex)
			{
				return false;
			}
			if (!CheckRemainingOvers(bowlerID))
			{
				return true;
			}
			return false;
		}
		if (bowlerID == TMLastbowler)
		{
			return false;
		}
		return true;
	}

	public void OverCompleted()
	{
		newOverCalled = true;
		if (CONTROLLER.PlayModeSelected >= 8)
		{
			return;
		}
		if (CONTROLLER.BowlingTeamIndex == CONTROLLER.opponentTeamIndex)
		{
			for (int i = 0; i < BowlersIndexArray.Count; i++)
			{
				if (CheckRemainingOvers(i))
				{
					bowler[i].ClickBtn.enabled = false;
					bowler[i].Name.font = normal;
					bowler[i].Highlight.sprite = bowlerStates[2];
				}
			}
		}
		else
		{
			for (int i = 0; i < BowlersIndexArray.Count; i++)
			{
				if (!CheckRemainingOvers(i))
				{
					bowler[i].ClickBtn.enabled = true;
					bowler[i].Name.font = normal;
					bowler[i].Highlight.sprite = bowlerStates[0];
				}
				else
				{
					bowler[i].ClickBtn.enabled = false;
					bowler[i].Name.font = normal;
					bowler[i].Highlight.sprite = bowlerStates[2];
				}
			}
		}
		if (currentBowler >= 0 && currentBowler < bowler.Length)
		{
			bowler[currentBowler].ClickBtn.enabled = false;
			bowler[currentBowler].Name.font = normal;
			bowler[currentBowler].Highlight.sprite = bowlerStates[2];
			if (CONTROLLER.PlayModeSelected == 7)
			{
				TMLastbowler = currentBowler;
			}
		}
		GetRandomBowler();
	}

	private void DisableBowler()
	{
		if (CONTROLLER.PlayModeSelected < 8)
		{
			for (int i = 0; i < BowlersIndexArray.Count; i++)
			{
				bowler[i].ClickBtn.enabled = false;
			}
		}
	}

	public void InningsCompleted()
	{
		if (CONTROLLER.PlayModeSelected < 8)
		{
			for (int i = 0; i < BowlersIndexArray.Count; i++)
			{
				bowler[i].ClickBtn.enabled = false;
				bowler[currentBowler].Name.font = normal;
				bowler[i].Highlight.sprite = bowlerStates[0];
			}
		}
	}

	public void RestartGame()
	{
		if (CONTROLLER.PlayModeSelected >= 8)
		{
			return;
		}
		for (int i = 0; i < BowlersIndexArray.Count; i++)
		{
			if (BowlersIndexArray[i] == CONTROLLER.CurrentBowlerIndex)
			{
				currentBowler = i;
			}
		}
		SetTeamInfo();
		UpdatePreviousBowlers();
		UpdateScoreCard();
		ContinueSelected();
	}

	private void UpdatePreviousBowlers()
	{
		if (CONTROLLER.PlayModeSelected != 7)
		{
			for (int i = 0; i < BowlersIndexArray.Count; i++)
			{
				int num = BowlersIndexArray[i];
				bowler[i].Overs.text = GetOversBowled(CONTROLLER.BowlingTeamIndex, num);
				bowler[i].Maiden.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[num].BowlerList.Maiden;
				bowler[i].Runs.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[num].BowlerList.RunsGiven;
				bowler[i].Wickets.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[num].BowlerList.Wicket;
				bowler[i].ERate.text = GetEconRate(CONTROLLER.BowlingTeamIndex, num);
				if (CheckRemainingOvers(i))
				{
					bowler[i].ClickBtn.enabled = false;
					bowler[currentBowler].Name.font = normal;
					bowler[i].Highlight.sprite = bowlerStates[2];
				}
			}
		}
		else
		{
			TMUpdatePreviousBowlers();
		}
	}

	private void CheckIfNewInnings()
	{
		if (!CONTROLLER.NewInnings)
		{
			return;
		}
		GetRandomBowler();
		for (int i = 0; i < bowler.Length; i++)
		{
			bowler[i].Name.font = normal;
			bowler[i].Highlight.sprite = bowlerStates[0];
		}
		bowler[currentBowler].Highlight.sprite = bowlerStates[1];
		if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
		{
			for (int j = 0; j < bowler.Length; j++)
			{
				bowler[j].ClickBtn.enabled = true;
			}
		}
		else
		{
			for (int k = 0; k < bowler.Length; k++)
			{
				bowler[k].ClickBtn.enabled = false;
			}
		}
		CONTROLLER.NewInnings = false;
	}

	public void SwapScorecard()
	{
		if (CONTROLLER.PlayModeSelected < 8)
		{
			if (BtnTeamName.text == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper())
			{
				DisplayList(CONTROLLER.BattingTeamIndex);
				BtnTeamName.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper();
				SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper();
				bowler[currentBowler].Name.font = normal;
				bowler[currentBowler].Highlight.sprite = bowlerStates[0];
				Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
				foreach (Sprite sprite in flags)
				{
					if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
					{
						BtnTeamFlag.sprite = sprite;
					}
					if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
					{
						SCTeamFlag.sprite = sprite;
					}
				}
				if (CONTROLLER.PlayModeSelected == 7)
				{
					ScoreText.text = GetScore(CONTROLLER.BowlingTeamIndex);
					OversText.text = GetOvers(CONTROLLER.BowlingTeamIndex);
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
			else
			{
				DisplayList(CONTROLLER.BowlingTeamIndex);
				BtnTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper();
				SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper();
				if (!CONTROLLER.isFromAutoPlay)
				{
					bowler[currentBowler].Name.font = bold;
					bowler[currentBowler].Highlight.sprite = bowlerStates[1];
				}
				Sprite[] flags2 = Singleton<FlagHolderGround>.instance.flags;
				foreach (Sprite sprite2 in flags2)
				{
					if (sprite2.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
					{
						BtnTeamFlag.sprite = sprite2;
					}
					if (sprite2.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
					{
						SCTeamFlag.sprite = sprite2;
					}
				}
				if (CONTROLLER.PlayModeSelected == 7)
				{
					ScoreText.text = GetScore(CONTROLLER.BattingTeamIndex);
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
			if (Singleton<GameModel>.instance.inningsCompleted)
			{
				for (int k = 0; k < bowler.Length; k++)
				{
					bowler[k].Name.font = normal;
					bowler[k].Highlight.sprite = bowlerStates[0];
				}
			}
			Singleton<BowlingScoreBoardPanelTransition>.instance.PanelTransition();
		}
		DisableBowlerSelection();
	}

	private void DisplayList(int TeamID)
	{
		if (CONTROLLER.PlayModeSelected != 7)
		{
			for (int i = 0; i < bowler.Length; i++)
			{
				if (CONTROLLER.BowlingTeamIndex == CONTROLLER.opponentTeamIndex)
				{
					if (firstTimeHide)
					{
						if (CONTROLLER.BowlingTeamIndex == TeamID)
						{
							if (!CONTROLLER.isFromAutoPlay)
							{
								bowler[currentBowler].Name.font = bold;
								bowler[currentBowler].Highlight.sprite = bowlerStates[1];
								bowler[i].ClickBtn.enabled = false;
							}
						}
						else
						{
							bowler[currentBowler].Name.font = normal;
							bowler[i].Highlight.sprite = bowlerStates[0];
							bowler[i].ClickBtn.enabled = false;
						}
					}
				}
				else if (CONTROLLER.BowlingTeamIndex != TeamID)
				{
					bowler[i].Highlight.sprite = bowlerStates[0];
				}
				else
				{
					checkBowlerSpell();
				}
				bowler[i].Name.text = string.Empty.ToUpper();
				bowler[i].Type.text = string.Empty.ToUpper();
				bowler[i].Overs.text = string.Empty;
				bowler[i].Maiden.text = string.Empty;
				bowler[i].Runs.text = string.Empty;
				bowler[i].Wickets.text = string.Empty;
				bowler[i].ERate.text = string.Empty;
			}
			int num = CONTROLLER.TeamList[TeamID].PlayerList.Length;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				string style = CONTROLLER.TeamList[TeamID].PlayerList[i].BowlerList.Style;
				if (style != string.Empty && style != null)
				{
					bowler[num2].Name.text = CONTROLLER.TeamList[TeamID].PlayerList[i].ScoreboardName.ToUpper();
					bowler[num2].Type.text = GetBowlerType(TeamID, i).ToUpper();
					bowler[num2].Overs.text = GetOversBowled(TeamID, i);
					bowler[num2].Maiden.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BowlerList.Maiden;
					bowler[num2].Runs.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BowlerList.RunsGiven;
					bowler[num2].Wickets.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BowlerList.Wicket;
					bowler[num2].ERate.text = GetEconRate(TeamID, i);
					num2++;
				}
			}
			int num3 = ((TeamID != CONTROLLER.BowlingTeamIndex) ? CONTROLLER.BowlingTeamIndex : CONTROLLER.BattingTeamIndex);
			string text = CONTROLLER.TeamList[num3].currentMatchScores + "/" + CONTROLLER.TeamList[num3].currentMatchWickets;
			string text2 = LocalizationData.instance.getText(235) + " " + CONTROLLER.TeamList[num3].currentMatchExtras;
			int num4 = CONTROLLER.TeamList[num3].currentMatchBalls / 6;
			int num5 = CONTROLLER.TeamList[num3].currentMatchBalls % 6;
			string text3 = LocalizationData.instance.getText(184) + " " + num4 + "." + num5 + "(" + CONTROLLER.totalOvers + ")";
			ScoreText.text = text;
			ExtrasText.text = text2;
			OversText.text = text3;
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
			if (Singleton<GameModel>.instance.inningsCompleted)
			{
				for (int i = 0; i < bowler.Length; i++)
				{
					bowler[i].Name.font = normal;
					bowler[i].Highlight.sprite = bowlerStates[0];
				}
			}
		}
		else
		{
			TMDisplayList(TeamID);
		}
	}

	public void ContinueSelected()
	{
		DisableBowler();
		lastBowlerIndex = currentBowler;
	}

	public void Continue()
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			if (ContinueBtn.gameObject.activeInHierarchy && !Singleton<GameModel>.instance.CheckForMatchCompletion() && Time.timeScale == 0f)
			{
				Time.timeScale = 1f;
			}
			if ((Singleton<GameModel>.instance.checkforSessionComplete() && fisrtTimeShow) || (Singleton<GameModel>.instance.checkforSessionComplete() && CONTROLLER.TMisFromAutoPlay) || CONTROLLER.ShowTMGameOver)
			{
				Singleton<TMSessionScreen>.instance.ShowMe();
				Hide(boolean: true);
				fisrtTimeShow = false;
				CONTROLLER.TMisFromAutoPlay = false;
				BG.SetActive(value: true);
				return;
			}
			firstTimeHide = true;
			Hide(boolean: true);
			if (CONTROLLER.gameCompleted)
			{
				Singleton<MatchSummary>.instance.showMe();
			}
			else if (!Singleton<GameModel>.instance.isGamePaused)
			{
				CONTROLLER.CurrentBowlerIndex = BowlersIndexArray[currentBowler];
				BallSimulationClicked();
			}
			else
			{
				Singleton<GameModel>.instance.AgainToGamePauseScreen();
				Singleton<PauseGameScreen>.instance.Hide(boolean: false);
			}
		}
		else
		{
			firstTimeHide = true;
			Hide(boolean: true);
			if (CONTROLLER.gameCompleted)
			{
				Singleton<MatchSummary>.instance.showMe();
			}
			else if (!Singleton<GameModel>.instance.isGamePaused)
			{
				CONTROLLER.CurrentBowlerIndex = BowlersIndexArray[currentBowler];
				BallSimulationClicked();
			}
			else
			{
				Singleton<GameModel>.instance.AgainToGamePauseScreen();
				Singleton<PauseGameScreen>.instance.Hide(boolean: false);
			}
		}
	}

	private void BallSimulationClicked()
	{
		Debug.LogError("ball simulation clicked:::::");
		if (Singleton<BallSimulationManager>.instance.CanShowBallSimulation() && Singleton<BallSimulationManager>.instance.settedSimulationData && !Singleton<BallSimulationManager>.instance.showingBallSimulation && !CONTROLLER.isAutoPlayed)
		{
			groundControllerScript.hideAllCamera();
			groundControllerScript.EnableAllSkinRenderers(state: false);
			groundControllerScript.ShowBall(status: false);
			Singleton<BallSimulationManager>.instance.StartBallSimulation();
		}
		else
		{
			groundControllerScript.EnableAllSkinRenderers(state: true);
			Singleton<BallSimulationManager>.instance.BallSimulationSkipped();
		}
	}

	private void TMSetTeamInfo()
	{
		Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite in flags)
		{
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
			{
				SCTeamFlag.sprite = sprite;
			}
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
			{
				BtnTeamFlag.sprite = sprite;
			}
		}
		SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper();
		BtnTeamName.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName.ToUpper();
	}

	private void DisableBowlerSelection()
	{
		if (!ContinueBtn.gameObject.activeSelf)
		{
			for (int i = 0; i < bowler.Length; i++)
			{
				bowler[i].ClickBtn.enabled = false;
			}
		}
	}

	public void Hide(bool boolean)
	{
		if (boolean)
		{
			if (CONTROLLER.PlayModeSelected != 7 || !Singleton<GameModel>.instance.sessionComplete)
			{
				BG.SetActive(value: false);
			}
			scoreCard.SetActive(value: false);
			if (firstTimeHide)
			{
				DisplayList(CONTROLLER.BowlingTeamIndex);
				SetTeamInfo();
			}
			return;
		}
		//if (CONTROLLER.canShowbannerGround == 1)
		//{
		//	Singleton<AdIntegrate>.instance.ShowAd();
		//}
		if (CONTROLLER.PlayModeSelected > 3 || CONTROLLER.PlayModeSelected == 7)
		{
			BtnTeamName.transform.parent.gameObject.SetActive(value: false);
		}
		if (CONTROLLER.PlayModeSelected == 7)
		{
			if (CONTROLLER.currentInnings < 2)
			{
				SelectedIndex = 1;
				SelectedInningsText.text = LocalizationData.instance.getText(470);
			}
			else
			{
				SelectedIndex = 2;
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
		}
		else
		{
			SelectedInningsText.transform.parent.gameObject.SetActive(value: false);
		}
		Singleton<NavigationBack>.instance.deviceBack = null;
		CONTROLLER.pageName = "bowlingSC";
		if (!BG.activeSelf)
		{
			BG.SetActive(value: true);
		}
		if (!Singleton<GameModel>.instance.isGamePaused && !CONTROLLER.gameCompleted)
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
			ContinueBtn.gameObject.SetActive(value: false);
			BackBtn.gameObject.SetActive(value: true);
		}
		Singleton<GameOverScreen>.instance.Hide(boolean: true);
		scoreCard.SetActive(value: true);
		DisplayList(CONTROLLER.BowlingTeamIndex);
		UpdateScoreCard();
		Singleton<BowlingScoreBoardPanelTransition>.instance.PanelTransition();
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TMSetTeamInfo();
			CheckIfNewInnings();
		}
		DisableBowlerSelection();
	}

	public void BackKeySelected()
	{
		Continue();
	}

	public void TMResetBowlingCard()
	{
		currentBowler = -1;
		lastBowlerIndex = -1;
		int bowlingTeamIndex = CONTROLLER.BowlingTeamIndex;
		SetTeamInfo();
		GetBowlersInTeam(bowlingTeamIndex);
		for (int i = 0; i < bowler.Length; i++)
		{
			if (CONTROLLER.BowlingTeamIndex == CONTROLLER.opponentTeamIndex)
			{
				bowler[i].Name.font = normal;
				bowler[i].Highlight.sprite = bowlerStates[0];
				bowler[i].ClickBtn.enabled = false;
			}
			else
			{
				bowler[i].ClickBtn.enabled = true;
			}
			if (i < BowlersIndexArray.Count)
			{
				int num = BowlersIndexArray[i];
				bowler[i].Name.text = CONTROLLER.TeamList[bowlingTeamIndex].PlayerList[num].ScoreboardName.ToUpper();
				bowler[i].Type.text = GetBowlerType(bowlingTeamIndex, num).ToUpper();
				bowler[i].Overs.text = "0.0";
				bowler[i].Maiden.text = "0";
				bowler[i].Runs.text = "0";
				bowler[i].Wickets.text = "0";
				bowler[i].ERate.text = TMGetEconRate(bowlingTeamIndex, num);
				if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
				{
					bowler[i].ClickBtn.enabled = true;
				}
			}
			else
			{
				bowler[i].Name.text = string.Empty.ToUpper();
				bowler[i].Type.text = string.Empty.ToUpper();
				bowler[i].Overs.text = string.Empty;
				bowler[i].Maiden.text = string.Empty;
				bowler[i].Runs.text = string.Empty;
				bowler[i].Wickets.text = string.Empty;
				bowler[i].ERate.text = string.Empty;
				bowler[i].ClickBtn.enabled = false;
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

	public void TMUpdateScoreCard()
	{
		Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite in flags)
		{
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation)
			{
				SCTeamFlag.sprite = sprite;
			}
		}
		SCTeamName.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName.ToUpper();
		int bowlingTeamIndex = CONTROLLER.BowlingTeamIndex;
		int currentBowlerIndex = CONTROLLER.CurrentBowlerIndex;
		bowler[currentBowler].Name.font = bold;
		bowler[currentBowler].Highlight.sprite = bowlerStates[1];
		bowler[currentBowler].Overs.text = TMGetOversBowled(bowlingTeamIndex, currentBowlerIndex);
		if (SelectedIndex == 1)
		{
			bowler[currentBowler].Maiden.text = string.Empty + CONTROLLER.TeamList[bowlingTeamIndex].PlayerList[currentBowlerIndex].BowlerList.TMMaiden1;
			bowler[currentBowler].Runs.text = string.Empty + CONTROLLER.TeamList[bowlingTeamIndex].PlayerList[currentBowlerIndex].BowlerList.TMRunsGiven1;
			bowler[currentBowler].Wickets.text = string.Empty + CONTROLLER.TeamList[bowlingTeamIndex].PlayerList[currentBowlerIndex].BowlerList.TMWicket1;
			bowler[currentBowler].ERate.text = TMGetEconRate(bowlingTeamIndex, currentBowlerIndex);
		}
		else
		{
			bowler[currentBowler].Maiden.text = string.Empty + CONTROLLER.TeamList[bowlingTeamIndex].PlayerList[currentBowlerIndex].BowlerList.TMMaiden2;
			bowler[currentBowler].Runs.text = string.Empty + CONTROLLER.TeamList[bowlingTeamIndex].PlayerList[currentBowlerIndex].BowlerList.TMRunsGiven2;
			bowler[currentBowler].Wickets.text = string.Empty + CONTROLLER.TeamList[bowlingTeamIndex].PlayerList[currentBowlerIndex].BowlerList.TMWicket2;
			bowler[currentBowler].ERate.text = TMGetEconRate(bowlingTeamIndex, currentBowlerIndex);
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

	private int TMGetCurrentSpell()
	{
		//int num = ((CONTROLLER.currentInnings >= 2) ? (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls2 / 6) : (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 / 6));
		//if (num < firstSpellOver)
		//{
		//	return 1;
		//}
		return 2;
	}

	private int TMGetCurrentOver()
	{
		int num = ((CONTROLLER.currentInnings >= 2) ? (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls2 / 6) : (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 / 6));
		return num + 1;
	}

	private void TMcheckBowlerSpell()
	{
		int num = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6;
		bool flag = false;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		List<int> list = new List<int>();
		if ((fromDisplayList && SelectedIndex == 1) || (!fromDisplayList && CONTROLLER.currentInnings < 2))
		{
			num = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls1 / 6;
		}
		else
		{
			num = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchBalls2 / 6;
		}
		for (int i = 0; i < BowlersIndexArray.Count; i++)
		{
			num2 = (((!fromDisplayList || SelectedIndex != 1) && (fromDisplayList || CONTROLLER.currentInnings >= 2)) ? CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[BowlersIndexArray[i]].BowlerList.TMBallsBowled2 : CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[BowlersIndexArray[i]].BowlerList.TMBallsBowled1);
			num3 = (int)((float)(CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6) * 0.2f);
			num5 = (num3 - num2) / 6;
			if (num6 < num5)
			{
				list = new List<int>();
				num6 = num5;
				num7 = i;
				list.Add(i);
			}
			else if (num6 == num5)
			{
				list.Add(i);
			}
		}
		num5 = 0;
		for (int j = 0; j < BowlersIndexArray.Count; j++)
		{
			if (j != num7)
			{
				num2 = (((!fromDisplayList || SelectedIndex != 1) && (fromDisplayList || CONTROLLER.currentInnings >= 2)) ? CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[BowlersIndexArray[j]].BowlerList.TMBallsBowled2 : CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[BowlersIndexArray[j]].BowlerList.TMBallsBowled1);
				num3 = (int)((float)(CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6) * 0.2f);
				num5 += (num3 - num2) / 6;
			}
		}
		for (int k = 0; k < BowlersIndexArray.Count; k++)
		{
			num2 = (((!fromDisplayList || SelectedIndex != 1) && (fromDisplayList || CONTROLLER.currentInnings >= 2)) ? CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[BowlersIndexArray[k]].BowlerList.TMBallsBowled2 : CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[BowlersIndexArray[k]].BowlerList.TMBallsBowled1);
			num3 = (int)((float)(CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6) * 0.2f);
			num4 = num3 - num2;
			if (num2 <= CONTROLLER.oversSelectedIndex * 6 - 6 && num6 > num5 && list.Count <= 1 && k != lastBowlerIndex)
			{
				flag = true;
				currentBowler = num7;
			}
		}
		for (int l = 0; l < BowlersIndexArray.Count; l++)
		{
			if (flag && l != currentBowler)
			{
				bowler[l].ClickBtn.enabled = false;
				bowler[currentBowler].Name.font = normal;
				bowler[l].Highlight.sprite = bowlerStates[2];
			}
		}
	}

	private void TMUpdatePreviousBowlers()
	{
		if (CONTROLLER.PlayModeSelected >= 8)
		{
			return;
		}
		for (int i = 0; i < BowlersIndexArray.Count; i++)
		{
			int num = BowlersIndexArray[i];
			if (CONTROLLER.currentInnings < 2)
			{
				bowler[i].Overs.text = GetOversBowled(CONTROLLER.BowlingTeamIndex, num);
				bowler[i].Maiden.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[num].BowlerList.TMMaiden1;
				bowler[i].Runs.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[num].BowlerList.TMRunsGiven1;
				bowler[i].Wickets.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[num].BowlerList.TMWicket1;
				bowler[i].ERate.text = TMGetEconRate(CONTROLLER.BowlingTeamIndex, num);
			}
			else
			{
				bowler[i].Overs.text = GetOversBowled(CONTROLLER.BowlingTeamIndex, num);
				bowler[i].Maiden.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[num].BowlerList.TMMaiden2;
				bowler[i].Runs.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[num].BowlerList.TMRunsGiven2;
				bowler[i].Wickets.text = string.Empty + CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[num].BowlerList.TMWicket2;
				bowler[i].ERate.text = TMGetEconRate(CONTROLLER.BowlingTeamIndex, num);
			}
			if (CheckRemainingOvers(i))
			{
				bowler[i].ClickBtn.enabled = false;
				bowler[currentBowler].Name.font = normal;
				bowler[i].Highlight.sprite = bowlerStates[2];
			}
		}
	}

	public void SwapInnings()
	{
		if (SelectedIndex == 2)
		{
			SelectedIndex = 1;
			SelectedInningsText.text = LocalizationData.instance.getText(470);
		}
		else
		{
			SelectedIndex = 2;
			SelectedInningsText.text = "FIRST INNINGS";
			SelectedInningsText.text = LocalizationData.instance.getText(469);
		}
		TMDisplayList(CONTROLLER.BowlingTeamIndex);
		if (!CONTROLLER.isFromAutoPlay)
		{
			bowler[currentBowler].Name.font = bold;
			bowler[currentBowler].Highlight.sprite = bowlerStates[1];
			if (TMLastbowler != -1)
			{
				bowler[TMLastbowler].Highlight.sprite = bowlerStates[2];
			}
		}
		if ((CONTROLLER.currentInnings < 2 && SelectedIndex == 2) || (CONTROLLER.currentInnings >= 2 && SelectedIndex == 1))
		{
			for (int i = 0; i < bowler.Length; i++)
			{
				bowler[i].Name.font = normal;
				bowler[i].Highlight.sprite = bowlerStates[0];
			}
		}
		DisableBowlerSelection();
		Singleton<BowlingScoreBoardPanelTransition>.instance.PanelTransition();
	}

	private void TMDisplayList(int TeamID)
	{
		for (int i = 0; i < bowler.Length; i++)
		{
			if (CONTROLLER.BowlingTeamIndex == CONTROLLER.opponentTeamIndex)
			{
				if (firstTimeHide)
				{
					if (CONTROLLER.BowlingTeamIndex == TeamID)
					{
						bowler[currentBowler].Name.font = bold;
						bowler[currentBowler].Highlight.sprite = bowlerStates[1];
						bowler[i].ClickBtn.enabled = false;
					}
					else
					{
						bowler[currentBowler].Name.font = normal;
						bowler[i].Highlight.sprite = bowlerStates[0];
						bowler[i].ClickBtn.enabled = false;
					}
				}
			}
			else if (CONTROLLER.BowlingTeamIndex != TeamID)
			{
				bowler[i].Highlight.sprite = bowlerStates[0];
			}
			else
			{
				fromDisplayList = true;
				checkBowlerSpell();
			}
			bowler[i].Name.text = string.Empty.ToUpper();
			bowler[i].Type.text = string.Empty.ToUpper();
			bowler[i].Overs.text = string.Empty;
			bowler[i].Maiden.text = string.Empty;
			bowler[i].Runs.text = string.Empty;
			bowler[i].Wickets.text = string.Empty;
			bowler[i].ERate.text = string.Empty;
		}
		int num = CONTROLLER.TeamList[TeamID].PlayerList.Length;
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			string style = CONTROLLER.TeamList[TeamID].PlayerList[i].BowlerList.Style;
			if (style != string.Empty && style != null)
			{
				if (SelectedIndex == 1)
				{
					bowler[num2].Name.text = CONTROLLER.TeamList[TeamID].PlayerList[i].ScoreboardName.ToUpper();
					bowler[num2].Type.text = GetBowlerType(TeamID, i).ToUpper();
					bowler[num2].Overs.text = TMGetOversBowled(TeamID, i);
					bowler[num2].Maiden.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BowlerList.TMMaiden1;
					bowler[num2].Runs.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BowlerList.TMRunsGiven1;
					bowler[num2].Wickets.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BowlerList.TMWicket1;
					bowler[num2].ERate.text = TMGetEconRate(TeamID, i);
					num2++;
				}
				else
				{
					bowler[num2].Name.text = CONTROLLER.TeamList[TeamID].PlayerList[i].ScoreboardName.ToUpper();
					bowler[num2].Type.text = GetBowlerType(TeamID, i).ToUpper();
					bowler[num2].Overs.text = TMGetOversBowled(TeamID, i);
					bowler[num2].Maiden.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BowlerList.TMMaiden2;
					bowler[num2].Runs.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BowlerList.TMRunsGiven2;
					bowler[num2].Wickets.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[i].BowlerList.TMWicket2;
					bowler[num2].ERate.text = TMGetEconRate(TeamID, i);
					num2++;
				}
			}
		}
		int num3 = ((TeamID != CONTROLLER.BowlingTeamIndex) ? CONTROLLER.BowlingTeamIndex : CONTROLLER.BattingTeamIndex);
		string text = CONTROLLER.TeamList[num3].currentMatchScores + "/" + CONTROLLER.TeamList[num3].currentMatchWickets;
		string text2 = LocalizationData.instance.getText(235) + " " + CONTROLLER.TeamList[num3].currentMatchExtras;
		int num4 = CONTROLLER.TeamList[num3].currentMatchBalls / 6;
		int num5 = CONTROLLER.TeamList[num3].currentMatchBalls % 6;
		string text3 = LocalizationData.instance.getText(184) + " " + num4 + "." + num5 + "(" + CONTROLLER.totalOvers + ")";
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
		if (Singleton<GameModel>.instance.inningsCompleted)
		{
			for (int i = 0; i < bowler.Length; i++)
			{
				bowler[i].Name.font = normal;
				bowler[i].Highlight.sprite = bowlerStates[0];
			}
		}
		for (int i = 0; i < bowler.Length; i++)
		{
			bowler[i].Name.font = normal;
			bowler[i].Highlight.sprite = bowlerStates[0];
			if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
			{
				bowler[i].ClickBtn.enabled = true;
			}
			else
			{
				bowler[i].ClickBtn.enabled = false;
			}
		}
		bowler[currentBowler].Name.font = bold;
		bowler[currentBowler].Highlight.sprite = bowlerStates[1];
		if (TMLastbowler != -1)
		{
			bowler[TMLastbowler].ClickBtn.enabled = false;
			bowler[TMLastbowler].Highlight.sprite = bowlerStates[2];
		}
	}

	public string TMGetEconRate(int TeamID, int playerID)
	{
		float num = 0f;
		float num2;
		float num3;
		if (SelectedIndex == 1)
		{
			num2 = CONTROLLER.TeamList[TeamID].PlayerList[playerID].BowlerList.TMRunsGiven1;
			num3 = CONTROLLER.TeamList[TeamID].PlayerList[playerID].BowlerList.TMBallsBowled1;
		}
		else
		{
			num2 = CONTROLLER.TeamList[TeamID].PlayerList[playerID].BowlerList.TMRunsGiven2;
			num3 = CONTROLLER.TeamList[TeamID].PlayerList[playerID].BowlerList.TMBallsBowled2;
		}
		if (num3 > 0f)
		{
			num = num2 / num3 * 6f;
			num = Mathf.Round(num * 100f) / 100f;
		}
		return num.ToString();
	}

	private void TMselectRandomBowler()
	{
		if (CONTROLLER.PlayModeSelected >= 8)
		{
			return;
		}
		List<int> list = new List<int>();
		for (int i = 0; i < BowlersIndexArray.Count; i++)
		{
			int num = 0;
			int num2 = 1200;
			num = ((CONTROLLER.currentInnings >= 2) ? CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[BowlersIndexArray[i]].BowlerList.TMBallsBowled2 : CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[BowlersIndexArray[i]].BowlerList.TMBallsBowled1);
			int num3 = num2 - num;
			if (num3 > 0 && i != lastBowlerIndex)
			{
				list.Add(i);
			}
		}
		if (list.Count > 0)
		{
			int index = Random.Range(0, list.Count);
			currentBowler = list[index];
		}
	}

	private string GetScore(int TeamID)
	{
		int num;
		int num2;
		if (SelectedIndex < 2)
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
		if (SelectedIndex == 1)
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

	public string TMGetOversBowled(int teamID, int playerID)
	{
		int num;
		int num2;
		if (SelectedIndex == 1)
		{
			num = CONTROLLER.TeamList[teamID].PlayerList[playerID].BowlerList.TMBallsBowled1 / 6;
			num2 = CONTROLLER.TeamList[teamID].PlayerList[playerID].BowlerList.TMBallsBowled1 % 6;
		}
		else
		{
			num = CONTROLLER.TeamList[teamID].PlayerList[playerID].BowlerList.TMBallsBowled2 / 6;
			num2 = CONTROLLER.TeamList[teamID].PlayerList[playerID].BowlerList.TMBallsBowled2 % 6;
		}
		return num + "." + num2;
	}

	private string GetOvers(int TeamID)
	{
		int num;
		int num2;
		if (SelectedIndex < 2)
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
}
