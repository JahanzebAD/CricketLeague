using UnityEngine;
using UnityEngine.UI;

public class MatchSummary : Singleton<MatchSummary>
{
	public Image myTeamFlag;

	public Image myTeamFlagTwo;

	public Image oppTeamFlag;

	public Text myTeamName;

	public Text myTeamScore;

	public Text oppTeamName;

	public Text oppTeamScore;

	public Text MatchResultTxt;

	public Text manOfTheMatch;

	private float playerEcon;

	private float tempEcon;

	public GameObject BG;

	public Text MyTeamBatsmanName;

	public Text MyTeamBatsmanScore;

	public Text MyTeamBowlerName;

	public Text OppTeamBowlerScore;

	public Text OppTeamBatsmanName;

	public Text OppTeamBatsmanScore;

	public Text OppTeamBowlerName;

	public Text MyTeamBowlerScore;

	public EndScreenBatsmanDetails[] batsmen;

	public EndScreenBowlerDetails[] bowlers;

	public GameObject holder;

	private int[] MyTopThreeBatsman = new int[3];

	private int[] MyTopThreeBowler = new int[3];

	private int[] OppTopThreeBatsman = new int[3];

	private int[] OppTopThreeBowler = new int[3];

	private bool MatchTie;

	protected void Start()
	{
		addEventListener();
		hideMe();
	}

	public void addEventListener()
	{
	}

	private void quitGame()
	{
		CONTROLLER.CurrentMenu = "landingpage";
		Singleton<GameModel>.instance.GameQuitted();
	}

	public void btnSelected(int index)
	{
		Singleton<GameModel>.instance.UpdateAction(-1);
		switch (index)
		{
		case 0:
			holder.SetActive(value: false);
			quitGame();
			break;
		case 2:
			hideMe();
			Singleton<BattingScoreCard>.instance.Hide(boolean: false);
			break;
		case 3:
			hideMe();
			Singleton<BowlingScoreCard>.instance.Hide(boolean: false);
			break;
		}
	}

	private void DisPlayResult()
	{
		int currentMatchScores = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores;
		int currentMatchScores2 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores;
		int currentMatchWickets = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets;
		int currentMatchWickets2 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchWickets;
		string teamName = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].teamName;
		Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite in flags)
		{
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation)
			{
				myTeamFlag.sprite = sprite;
			}
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation)
			{
				oppTeamFlag.sprite = sprite;
			}
		}
		if (currentMatchScores > currentMatchScores2)
		{
			CONTROLLER.gameWonBy = 0;
			if (CONTROLLER.meFirstBatting == 1)
			{
				if (currentMatchScores - currentMatchScores2 <= 1)
				{
					MatchResultTxt.text = "You won the match by " + (currentMatchScores - currentMatchScores2) + " run";
				}
				else
				{
					MatchResultTxt.text = "You won the match by " + (currentMatchScores - currentMatchScores2) + " runs";
				}
			}
			else if (CONTROLLER.totalWickets - currentMatchWickets <= 1)
			{
				MatchResultTxt.text = "You won the match by " + (10 - currentMatchWickets) + " wicket";
			}
			else
			{
				MatchResultTxt.text = "You won the match by " + (10 - currentMatchWickets) + " wickets";
			}
		}
		else if (currentMatchScores < currentMatchScores2)
		{
			CONTROLLER.gameWonBy = 1;
			if (CONTROLLER.meFirstBatting == 0)
			{
				if (currentMatchScores2 - currentMatchScores <= 1)
				{
					MatchResultTxt.text = teamName + " won the match by " + (currentMatchScores2 - currentMatchScores) + " run";
				}
				else
				{
					MatchResultTxt.text = teamName + " won the match by " + (currentMatchScores2 - currentMatchScores) + " runs";
				}
			}
			else if (CONTROLLER.totalWickets - currentMatchWickets2 <= 1)
			{
				MatchResultTxt.text = teamName + " won the match by " + (10 - currentMatchWickets2) + " wicket";
			}
			else
			{
				MatchResultTxt.text = teamName + " won the match by " + (10 - currentMatchWickets2) + " wickets";
			}
		}
		else
		{
			CONTROLLER.gameWonBy = 1;
			MatchTie = true;
			MatchResultTxt.text = LocalizationData.instance.getText(524);
		}
		SetTeamInfo();
		SetPlayerDetails();
		DecideManOfTheMatch();
	}

	private void SetTeamInfo()
	{
		int num;
		int num2;
		if (CONTROLLER.meFirstBatting == 1)
		{
			num = CONTROLLER.myTeamIndex;
			num2 = CONTROLLER.opponentTeamIndex;
		}
		else
		{
			num = CONTROLLER.opponentTeamIndex;
			num2 = CONTROLLER.myTeamIndex;
		}
		myTeamName.text = CONTROLLER.TeamList[num].teamName.ToUpper();
		myTeamScore.text = GetScore(num);
		oppTeamName.text = CONTROLLER.TeamList[num2].teamName.ToUpper();
		oppTeamScore.text = GetScore(num2);
	}

	private string GetScore(int TeamID)
	{
		int currentMatchScores = CONTROLLER.TeamList[TeamID].currentMatchScores;
		int currentMatchWickets = CONTROLLER.TeamList[TeamID].currentMatchWickets;
		int num = CONTROLLER.TeamList[TeamID].currentMatchBalls / 6;
		int num2 = CONTROLLER.TeamList[TeamID].currentMatchBalls % 6;
		return currentMatchScores + "/" + currentMatchWickets + " in " + num + "." + num2 + " Overs";
	}

	private void SetPlayerDetails()
	{
		int num;
		int num2;
		if (CONTROLLER.meFirstBatting == 1)
		{
			num = CONTROLLER.myTeamIndex;
			num2 = CONTROLLER.opponentTeamIndex;
		}
		else
		{
			num = CONTROLLER.opponentTeamIndex;
			num2 = CONTROLLER.myTeamIndex;
		}
		SortPlayerForSummary(0);
		MyTeamBatsmanName.text = string.Empty;
		MyTeamBatsmanScore.text = string.Empty;
		MyTeamBowlerName.text = string.Empty;
		OppTeamBowlerScore.text = string.Empty;
		OppTeamBatsmanName.text = string.Empty;
		OppTeamBatsmanScore.text = string.Empty;
		OppTeamBowlerName.text = string.Empty;
		MyTeamBowlerScore.text = string.Empty;
		for (int i = 0; i < 3; i++)
		{
			if (MyTopThreeBatsman[i] >= 0)
			{
				MyTeamBatsmanName.text = CONTROLLER.TeamList[num].PlayerList[MyTopThreeBatsman[0]].ScoreboardName;
				MyTeamBatsmanScore.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[MyTopThreeBatsman[0]].BatsmanList.RunsScored;
				if (CONTROLLER.TeamList[num].PlayerList[MyTopThreeBatsman[0]].BatsmanList.Status == "not out")
				{
					MyTeamBatsmanScore.text += "*";
				}
				Text myTeamBatsmanScore = MyTeamBatsmanScore;
				string text = myTeamBatsmanScore.text;
				myTeamBatsmanScore.text = text + "(" + CONTROLLER.TeamList[num].PlayerList[MyTopThreeBatsman[0]].BatsmanList.BallsPlayed + ")";
			}
			if (OppTopThreeBowler[i] >= 0)
			{
				MyTeamBowlerName.text = CONTROLLER.TeamList[num2].PlayerList[OppTopThreeBowler[0]].ScoreboardName;
				MyTeamBowlerScore.text = string.Empty + CONTROLLER.TeamList[num2].PlayerList[OppTopThreeBowler[0]].BowlerList.Wicket;
				Text myTeamBowlerScore = MyTeamBowlerScore;
				myTeamBowlerScore.text = myTeamBowlerScore.text + "-" + CONTROLLER.TeamList[num2].PlayerList[OppTopThreeBowler[0]].BowlerList.RunsGiven;
			}
			if (OppTopThreeBatsman[i] >= 0)
			{
				OppTeamBatsmanName.text = CONTROLLER.TeamList[num2].PlayerList[OppTopThreeBatsman[0]].ScoreboardName;
				OppTeamBatsmanScore.text = string.Empty + CONTROLLER.TeamList[num2].PlayerList[OppTopThreeBatsman[0]].BatsmanList.RunsScored;
				if (CONTROLLER.TeamList[num2].PlayerList[OppTopThreeBatsman[0]].BatsmanList.Status == "not out")
				{
					OppTeamBatsmanScore.text += "*";
				}
				Text oppTeamBatsmanScore = OppTeamBatsmanScore;
				string text = oppTeamBatsmanScore.text;
				oppTeamBatsmanScore.text = text + "(" + CONTROLLER.TeamList[num2].PlayerList[OppTopThreeBatsman[0]].BatsmanList.BallsPlayed + ")";
			}
			if (MyTopThreeBowler[0] >= 0)
			{
				OppTeamBowlerName.text = CONTROLLER.TeamList[num].PlayerList[MyTopThreeBowler[0]].ScoreboardName;
				OppTeamBowlerScore.text = string.Empty + CONTROLLER.TeamList[num].PlayerList[MyTopThreeBowler[0]].BowlerList.Wicket;
				Text oppTeamBowlerScore = OppTeamBowlerScore;
				oppTeamBowlerScore.text = oppTeamBowlerScore.text + "-" + CONTROLLER.TeamList[num].PlayerList[MyTopThreeBowler[0]].BowlerList.RunsGiven;
			}
		}
	}

	public void SwapScorecard()
	{
		if (oppTeamFlag.sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation)
		{
			ScreenFourDetails(1);
		}
		else
		{
			ScreenFourDetails(0);
		}
	}

	public void ScreenFourDetails(int index)
	{
		if (index == 0)
		{
			Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
			foreach (Sprite sprite in flags)
			{
				if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation)
				{
					myTeamFlag.sprite = sprite;
					myTeamFlagTwo.sprite = sprite;
				}
				if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation)
				{
					oppTeamFlag.sprite = sprite;
				}
			}
			Singleton<GameOverDisplay>.instance.ScreenFourDetails(CONTROLLER.myTeamIndex);
			for (int j = 0; j < MyTopThreeBowler.Length; j++)
			{
				if (MyTopThreeBatsman[j] > -1)
				{
					batsmen[j].name.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[MyTopThreeBatsman[j]].ScoreboardName.ToString();
					batsmen[j].runs.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[MyTopThreeBatsman[j]].BatsmanList.RunsScored.ToString();
					batsmen[j].balls.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[MyTopThreeBatsman[j]].BatsmanList.BallsPlayed.ToString();
					batsmen[j].fours.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[MyTopThreeBatsman[j]].BatsmanList.Fours.ToString();
					batsmen[j].sixes.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[MyTopThreeBatsman[j]].BatsmanList.Sixes.ToString();
					batsmen[j].status.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[MyTopThreeBatsman[j]].BatsmanList.Status.ToString();
					batsmen[j].strikeRate.text = Singleton<BattingScoreCard>.instance.GetStrikeRate(CONTROLLER.myTeamIndex, MyTopThreeBatsman[j]);
				}
				else
				{
					batsmen[j].name.text = "-";
					batsmen[j].runs.text = "-";
					batsmen[j].balls.text = "-";
					batsmen[j].fours.text = "-";
					batsmen[j].status.text = string.Empty;
					batsmen[j].sixes.text = "-";
					batsmen[j].sixes.text = string.Empty;
					batsmen[j].strikeRate.text = "-";
				}
			}
			for (int j = 0; j < MyTopThreeBowler.Length; j++)
			{
				if (MyTopThreeBowler[j] > -1)
				{
					bowlers[j].name.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[MyTopThreeBowler[j]].ScoreboardName.ToString();
					bowlers[j].overs.text = Singleton<BowlingScoreCard>.instance.GetOversBowled(CONTROLLER.myTeamIndex, MyTopThreeBowler[j]);
					bowlers[j].maidens.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[MyTopThreeBowler[j]].BowlerList.Maiden.ToString();
					bowlers[j].runs.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[MyTopThreeBowler[j]].BowlerList.RunsGiven.ToString();
					bowlers[j].wickets.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[MyTopThreeBowler[j]].BowlerList.Wicket.ToString();
					bowlers[j].economy.text = Singleton<BowlingScoreCard>.instance.GetEconRate(CONTROLLER.myTeamIndex, MyTopThreeBowler[j]);
				}
				else
				{
					bowlers[j].name.text = "-";
					bowlers[j].overs.text = "-";
					bowlers[j].maidens.text = "-";
					bowlers[j].runs.text = "-";
					bowlers[j].wickets.text = "-";
					bowlers[j].economy.text = "-";
				}
			}
			return;
		}
		Sprite[] flags2 = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite2 in flags2)
		{
			if (sprite2.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation)
			{
				myTeamFlag.sprite = sprite2;
				myTeamFlagTwo.sprite = sprite2;
			}
			if (sprite2.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation)
			{
				oppTeamFlag.sprite = sprite2;
			}
		}
		Singleton<GameOverDisplay>.instance.ScreenFourDetails(CONTROLLER.opponentTeamIndex);
		for (int j = 0; j < OppTopThreeBatsman.Length; j++)
		{
			if (OppTopThreeBatsman[j] > -1)
			{
				batsmen[j].name.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[OppTopThreeBatsman[j]].ScoreboardName.ToString();
				batsmen[j].runs.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[OppTopThreeBatsman[j]].BatsmanList.RunsScored.ToString();
				batsmen[j].balls.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[OppTopThreeBatsman[j]].BatsmanList.BallsPlayed.ToString();
				batsmen[j].fours.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[OppTopThreeBatsman[j]].BatsmanList.Fours.ToString();
				batsmen[j].sixes.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[OppTopThreeBatsman[j]].BatsmanList.Sixes.ToString();
				batsmen[j].status.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[OppTopThreeBatsman[j]].BatsmanList.Status.ToString();
				batsmen[j].strikeRate.text = Singleton<BattingScoreCard>.instance.GetStrikeRate(CONTROLLER.opponentTeamIndex, OppTopThreeBatsman[j]);
			}
			else
			{
				batsmen[j].name.text = "-";
				batsmen[j].runs.text = "-";
				batsmen[j].balls.text = "-";
				batsmen[j].status.text = string.Empty;
				batsmen[j].fours.text = "-";
				batsmen[j].sixes.text = "-";
				batsmen[j].sixes.text = string.Empty;
				batsmen[j].strikeRate.text = "-";
			}
		}
		for (int j = 0; j < OppTopThreeBowler.Length; j++)
		{
			if (OppTopThreeBowler[j] > -1)
			{
				bowlers[j].name.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[OppTopThreeBowler[j]].ScoreboardName.ToString();
				bowlers[j].overs.text = Singleton<BowlingScoreCard>.instance.GetOversBowled(CONTROLLER.opponentTeamIndex, OppTopThreeBowler[j]);
				bowlers[j].maidens.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[OppTopThreeBowler[j]].BowlerList.Maiden.ToString();
				bowlers[j].runs.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[OppTopThreeBowler[j]].BowlerList.RunsGiven.ToString();
				bowlers[j].wickets.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[OppTopThreeBowler[j]].BowlerList.Wicket.ToString();
				bowlers[j].economy.text = Singleton<BowlingScoreCard>.instance.GetEconRate(CONTROLLER.opponentTeamIndex, OppTopThreeBowler[j]);
			}
			else
			{
				bowlers[j].name.text = "-";
				bowlers[j].overs.text = "-";
				bowlers[j].maidens.text = "-";
				bowlers[j].runs.text = "-";
				bowlers[j].wickets.text = "-";
				bowlers[j].economy.text = "-";
			}
		}
	}

	public void SortPlayerForSummary(int index)
	{
		for (int i = 0; i < 3; i++)
		{
			MyTopThreeBatsman[i] = -1;
			MyTopThreeBowler[i] = -1;
			OppTopThreeBatsman[i] = -1;
			OppTopThreeBowler[i] = -1;
		}
		int num;
		int num2;
		if (index == 0)
		{
			num = CONTROLLER.myTeamIndex;
			num2 = CONTROLLER.opponentTeamIndex;
		}
		else
		{
			num = CONTROLLER.opponentTeamIndex;
			num2 = CONTROLLER.myTeamIndex;
		}
		for (int i = 0; i < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length; i++)
		{
			int num3 = CONTROLLER.TeamList[num].PlayerList[i].BatsmanList.RunsScored;
			int ballsPlayed = CONTROLLER.TeamList[num].PlayerList[i].BatsmanList.BallsPlayed;
			for (int j = 0; j < MyTopThreeBatsman.Length; j++)
			{
				int num4;
				if (MyTopThreeBatsman[j] > -1)
				{
					num4 = CONTROLLER.TeamList[num].PlayerList[MyTopThreeBatsman[j]].BatsmanList.RunsScored;
					if (num3 == num4 && num3 > 0)
					{
						int ballsPlayed2 = CONTROLLER.TeamList[num].PlayerList[MyTopThreeBatsman[j]].BatsmanList.BallsPlayed;
						if (ballsPlayed < ballsPlayed2)
						{
							num3++;
						}
					}
				}
				else
				{
					num4 = 0;
				}
				if (num3 > num4 && num3 > 0)
				{
					num4 = MyTopThreeBatsman[j];
					MyTopThreeBatsman[j] = i;
					for (int k = j + 1; k < MyTopThreeBatsman.Length; k++)
					{
						int num5 = MyTopThreeBatsman[k];
						MyTopThreeBatsman[k] = num4;
						num4 = num5;
					}
					break;
				}
			}
			num3 = CONTROLLER.TeamList[num].PlayerList[i].BowlerList.Wicket;
			ballsPlayed = CONTROLLER.TeamList[num].PlayerList[i].BowlerList.BallsBowled;
			int runsGiven = CONTROLLER.TeamList[num].PlayerList[i].BowlerList.RunsGiven;
			if (ballsPlayed > 0)
			{
				playerEcon = Mathf.Round(runsGiven / ballsPlayed * 6 * 100) / 100f;
			}
			for (int j = 0; j < MyTopThreeBowler.Length; j++)
			{
				if (MyTopThreeBowler[j] > -1)
				{
					int runsGiven2 = CONTROLLER.TeamList[num].PlayerList[MyTopThreeBowler[j]].BowlerList.RunsGiven;
					int ballsPlayed2 = CONTROLLER.TeamList[num].PlayerList[MyTopThreeBowler[j]].BowlerList.BallsBowled;
					int num4 = CONTROLLER.TeamList[num].PlayerList[MyTopThreeBowler[j]].BowlerList.Wicket;
					if (ballsPlayed2 > 0)
					{
						tempEcon = Mathf.Round(runsGiven2 / ballsPlayed2 * 6 * 100) / 100f;
					}
					else
					{
						tempEcon = 100f;
					}
					if (playerEcon == tempEcon && ballsPlayed > 0)
					{
						ballsPlayed2 = CONTROLLER.TeamList[num].PlayerList[MyTopThreeBowler[j]].BowlerList.BallsBowled;
						if (ballsPlayed > ballsPlayed2 || num3 > num4)
						{
							playerEcon -= 1f;
						}
					}
				}
				else
				{
					tempEcon = 100f;
				}
				if (playerEcon < tempEcon && ballsPlayed > 0)
				{
					int num4 = MyTopThreeBowler[j];
					MyTopThreeBowler[j] = i;
					for (int k = j + 1; k < MyTopThreeBowler.Length; k++)
					{
						int num5 = MyTopThreeBowler[k];
						MyTopThreeBowler[k] = num4;
						num4 = num5;
					}
					break;
				}
			}
			num3 = CONTROLLER.TeamList[num2].PlayerList[i].BatsmanList.RunsScored;
			ballsPlayed = CONTROLLER.TeamList[num2].PlayerList[i].BatsmanList.BallsPlayed;
			for (int j = 0; j < OppTopThreeBatsman.Length; j++)
			{
				int num4;
				if (OppTopThreeBatsman[j] > -1)
				{
					num4 = CONTROLLER.TeamList[num2].PlayerList[OppTopThreeBatsman[j]].BatsmanList.RunsScored;
					if (num3 == num4 && num3 > 0)
					{
						int ballsPlayed2 = CONTROLLER.TeamList[num2].PlayerList[OppTopThreeBatsman[j]].BatsmanList.BallsPlayed;
						if (ballsPlayed < ballsPlayed2)
						{
							num3++;
						}
					}
				}
				else
				{
					num4 = 0;
				}
				if (num3 > num4 && num3 > 0)
				{
					num4 = OppTopThreeBatsman[j];
					OppTopThreeBatsman[j] = i;
					for (int k = j + 1; k < OppTopThreeBatsman.Length; k++)
					{
						int num5 = OppTopThreeBatsman[k];
						OppTopThreeBatsman[k] = num4;
						num4 = num5;
					}
					break;
				}
			}
			num3 = CONTROLLER.TeamList[num2].PlayerList[i].BowlerList.Wicket;
			ballsPlayed = CONTROLLER.TeamList[num2].PlayerList[i].BowlerList.BallsBowled;
			runsGiven = CONTROLLER.TeamList[num2].PlayerList[i].BowlerList.RunsGiven;
			if (ballsPlayed > 0)
			{
				playerEcon = Mathf.Round(runsGiven / ballsPlayed * 6 * 100) / 100f;
			}
			else
			{
				playerEcon = 100f;
			}
			for (int j = 0; j < OppTopThreeBowler.Length; j++)
			{
				if (OppTopThreeBowler[j] > -1)
				{
					int runsGiven2 = CONTROLLER.TeamList[num2].PlayerList[OppTopThreeBowler[j]].BowlerList.RunsGiven;
					int ballsPlayed2 = CONTROLLER.TeamList[num2].PlayerList[OppTopThreeBowler[j]].BowlerList.BallsBowled;
					int num4 = CONTROLLER.TeamList[num2].PlayerList[OppTopThreeBowler[j]].BowlerList.Wicket;
					if (ballsPlayed2 > 0)
					{
						tempEcon = Mathf.Round(runsGiven2 / ballsPlayed2 * 6 * 100) / 100f;
					}
					else
					{
						tempEcon = 100f;
					}
					if (playerEcon == tempEcon && ballsPlayed > 0)
					{
						ballsPlayed2 = CONTROLLER.TeamList[num2].PlayerList[OppTopThreeBowler[j]].BowlerList.BallsBowled;
						if (ballsPlayed > ballsPlayed2 || num3 > num4)
						{
							playerEcon -= 1f;
						}
					}
				}
				else
				{
					tempEcon = 100f;
				}
				if (playerEcon < tempEcon && ballsPlayed > 0)
				{
					int runsGiven2 = OppTopThreeBowler[j];
					OppTopThreeBowler[j] = i;
					for (int k = j + 1; k < OppTopThreeBowler.Length; k++)
					{
						int num5 = OppTopThreeBowler[k];
						OppTopThreeBowler[k] = runsGiven2;
						runsGiven2 = num5;
					}
					break;
				}
			}
		}
		ScreenFourDetails(0);
	}

	private void DecideManOfTheMatch()
	{
		int num;
		int num2;
		if (CONTROLLER.gameWonBy == 0)
		{
			num = CONTROLLER.myTeamIndex;
			num2 = GetManOfTheMatch(num);
		}
		else if (!MatchTie)
		{
			num = CONTROLLER.opponentTeamIndex;
			num2 = GetManOfTheMatch(num);
		}
		else
		{
			num = CONTROLLER.myTeamIndex;
			num2 = GetManOfTheMatch(num);
			int num3 = num2;
			float num4 = GetPoints(num, num2);
			num = CONTROLLER.opponentTeamIndex;
			num2 = GetManOfTheMatch(num);
			float num5 = GetPoints(num, num2);
			if (num4 >= num5)
			{
				num = CONTROLLER.myTeamIndex;
				num2 = num3;
			}
		}
		if (num2 > -1)
		{
			manOfTheMatch.text = LocalizationData.instance.getText(525) + ": " + CONTROLLER.TeamList[num].PlayerList[num2].PlayerName + " (" + CONTROLLER.TeamList[num].teamName + ")";
		}
		else
		{
			manOfTheMatch.text = string.Empty;
		}
	}

	private int GetManOfTheMatch(int TeamID)
	{
		int num = -1;
		int num2 = CONTROLLER.TeamList[TeamID].PlayerList.Length;
		float num3 = 0f;
		float num4 = 0f;
		for (int i = 0; i < num2; i++)
		{
			num4 = GetPoints(TeamID, i);
			if (num4 == num3 && num4 > 0f)
			{
				if (CompareBestPlayer(TeamID, i, num))
				{
					num = i;
					num3 = num4;
				}
			}
			else if (num4 > num3 && num4 > 0f)
			{
				num = i;
				num3 = num4;
			}
		}
		return num;
	}

	private int GetPoints(int TeamID, int PlayerID)
	{
		if (PlayerID > -1)
		{
			int runsScored = CONTROLLER.TeamList[TeamID].PlayerList[PlayerID].BatsmanList.RunsScored;
			int wicket = CONTROLLER.TeamList[TeamID].PlayerList[PlayerID].BowlerList.Wicket;
			return (int)((float)runsScored * 0.2f + (float)(wicket * 3));
		}
		return 0;
	}

	private bool CompareBestPlayer(int TeamID, int CurrPlayerID, int PrevPlayerID)
	{
		bool result = false;
		string text = string.Empty;
		string text2 = string.Empty;
		int ballsPlayed = CONTROLLER.TeamList[TeamID].PlayerList[CurrPlayerID].BatsmanList.BallsPlayed;
		int ballsPlayed2 = CONTROLLER.TeamList[TeamID].PlayerList[PrevPlayerID].BatsmanList.BallsPlayed;
		int runsGiven = CONTROLLER.TeamList[TeamID].PlayerList[CurrPlayerID].BowlerList.RunsGiven;
		int runsGiven2 = CONTROLLER.TeamList[TeamID].PlayerList[PrevPlayerID].BowlerList.RunsGiven;
		int runsScored = CONTROLLER.TeamList[TeamID].PlayerList[CurrPlayerID].BatsmanList.RunsScored;
		int wicket = CONTROLLER.TeamList[TeamID].PlayerList[CurrPlayerID].BowlerList.Wicket;
		if (runsScored > 0 && wicket > 0)
		{
			text = "AllRounder";
		}
		else if (runsScored > 0)
		{
			text = "Batsman";
		}
		else if (wicket > 0)
		{
			text = "Bowler";
		}
		runsScored = CONTROLLER.TeamList[TeamID].PlayerList[PrevPlayerID].BatsmanList.RunsScored;
		wicket = CONTROLLER.TeamList[TeamID].PlayerList[PrevPlayerID].BowlerList.Wicket;
		if (runsScored > 0 && wicket > 0)
		{
			text2 = "AllRounder";
		}
		else if (runsScored > 0)
		{
			text2 = "Batsman";
		}
		else if (wicket > 0)
		{
			text2 = "Bowler";
		}
		if (text == "AllRounder" && text2 != "AllRounder")
		{
			result = true;
		}
		else if (text2 == "AllRounder" && text != "AllRounder")
		{
			result = false;
		}
		else if (text == "Batsman" && text2 == "Batsman")
		{
			if (ballsPlayed < ballsPlayed2)
			{
				result = true;
			}
		}
		else if (text == "Bowler" && text2 == "Bowler")
		{
			if (runsGiven < runsGiven2)
			{
				result = true;
			}
		}
		else if (text == "Batsman" && text2 == "Bowler")
		{
			result = true;
		}
		else if (text == "AllRounder" && text2 == "AllRounder" && ballsPlayed < ballsPlayed2)
		{
			result = true;
		}
		return result;
	}

	public void showMe()
	{
		DisPlayResult();
		holder.SetActive(value: true);
		BG.SetActive(value: true);
		CONTROLLER.CurrentMenu = "matchsummary";
	}

	public void hideMe()
	{
		holder.SetActive(value: false);
	}
}
