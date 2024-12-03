using System;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class GameOverScreen : Singleton<GameOverScreen>
{
	public GameObject previewPanel;

	public GameObject multiplayerPanel;

	public static int stage;

	public static int nplPlayoffs;

	public Sprite[] gamemodesprites;

	public GameObject BG;

	public LoadingPanelTransition loadingScreen;

	private string dictKey = string.Empty;

	private string[] WCTournament;

	private string result;

	private int teamWon;

	private string TournamentStageStr = string.Empty;

	public bool MatchTie;

	private string[] getQuaterFinalData;

	private string[] getSemiFinalData;

	private string[] getFinalData;

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

	public float PresentTime_2X;

	public int rewardMultiplier;

	public bool MatchWon;

	public static int LeagueStage;

	protected void Awake()
	{
		if (CONTROLLER.PlayModeSelected == 0 && CONTROLLER.QPDoubleRewards)
		{
			rewardMultiplier = 2;
		}
		else
		{
			rewardMultiplier = 1;
		}
		Hide(boolean: true);
	}

	public void CalculateXPforCurrentMatch()
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
		totalExpInaMatch = XPFromFour + XPFromMaiden + XPFromDot + XPFromDot + XPFromWicketBowled + XPFromWicketCatch + XPFromWicketOthers + XPFromFifty + XPFromCentury + XPFromPerOver;
		totalCoinsInaMatch = CoinsFromFour + CoinsFromMaiden + CoinsFromDot + CoinsFromSix + CoinsFromWicketBowled + CoinsFromWicketCatch + CoinsFromWicketOthers + CoinsFromFifty + CoinsFromCentury;
		SavePlayerPrefs.SaveUserCoins(totalCoinsInaMatch * rewardMultiplier, 0, totalCoinsInaMatch * rewardMultiplier);
		XPFromFour = (XPFromMaiden = (XPFromDot = (XPFromSix = (XPFromWicketBowled = (XPFromWicketCatch = (XPFromWicketOthers = (XPFromFifty = (XPFromCentury = (XPFromPerOver = 0)))))))));
		totalExpInaMatch = 0;
		CoinsFromFour = (CoinsFromMaiden = (CoinsFromDot = (CoinsFromSix = (CoinsFromWicketBowled = (CoinsFromWicketCatch = (CoinsFromWicketOthers = (CoinsFromFifty = (CoinsFromCentury = 0))))))));
		totalCoinsInaMatch = 0;
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
			nplPlayoffs = CONTROLLER.NPLIndiaLeagueMatchIndex;
			LeagueStage = CONTROLLER.NPLIndiaMyCurrentMatchIndex;
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			stage = CONTROLLER.WCTournamentStage;
			LeagueStage = CONTROLLER.WCMyCurrentMatchIndex;
		}
	}

	private void DisPlayResult()
	{
		SetStage();
		int currentMatchScores = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores;
		int currentMatchScores2 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores;
		int currentMatchWickets = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets;
		int currentMatchWickets2 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchWickets;
		if (CONTROLLER.PlayModeSelected == 0)
		{
			CONTROLLER.screenToDisplay = "landingPage";
			if (currentMatchScores > currentMatchScores2)
			{
				UIDataHolder.wonMatch = true;
				string text = "YOU WON";
				Singleton<GameModel>.instance.PlayGameSound("Won");
				
			}
			else if (currentMatchScores < currentMatchScores2)
			{
				UIDataHolder.wonMatch = false;
				string text = "YOU LOST";
				Singleton<GameModel>.instance.PlayGameSound("Lost");
				
			}
			else
			{
				UIDataHolder.wonMatch = false;
				string text = "MATCH TIED";
				Singleton<GameModel>.instance.PlayGameSound("Lost");
				
			}
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			dictKey = "T20";
			if (CONTROLLER.TournamentStage == 0)
			{
				if (currentMatchScores >= currentMatchScores2)
				{
					stage = 0;
					UIDataHolder.wonMatch = true;
					tournamentUpdate(CONTROLLER.myTeamIndex, isTie: false);
				}
				else if (currentMatchScores < currentMatchScores2)
				{
					ObscuredPrefs.SetInt("T20Paid", 0);
					UIDataHolder.wonMatch = false;
					tournamentUpdate(CONTROLLER.opponentTeamIndex, isTie: false);
					
				}
				else
				{
					ObscuredPrefs.SetInt("T20Paid", 0);
					UIDataHolder.wonMatch = false;
					decideMatchTieResult();
				}
			}
			else if (CONTROLLER.TournamentStage == 1)
			{
				stage = 1;
				if (currentMatchScores >= currentMatchScores2)
				{
					UIDataHolder.wonMatch = true;
					tournamentUpdate(CONTROLLER.myTeamIndex, isTie: false);
				}
				else if (currentMatchScores < currentMatchScores2)
				{
					ObscuredPrefs.SetInt("T20Paid", 0);
					UIDataHolder.wonMatch = false;
					tournamentUpdate(CONTROLLER.opponentTeamIndex, isTie: false);
					
				}
				else
				{
					ObscuredPrefs.SetInt("T20Paid", 0);
					UIDataHolder.wonMatch = false;
					decideMatchTieResult();
				}
			}
			else if (CONTROLLER.TournamentStage == 2)
			{
				stage = 2;
				if (currentMatchScores >= currentMatchScores2)
				{
					UIDataHolder.wonMatch = true;
					tournamentUpdate(CONTROLLER.myTeamIndex, isTie: false);
				}
				else if (currentMatchScores < currentMatchScores2)
				{
					ObscuredPrefs.SetInt("T20Paid", 0);
					UIDataHolder.wonMatch = false;
					tournamentUpdate(CONTROLLER.opponentTeamIndex, isTie: false);
					
				}
				else
				{
					ObscuredPrefs.SetInt("T20Paid", 0);
					UIDataHolder.wonMatch = false;
					decideMatchTieResult();
				}
			}
			else
			{
				if (CONTROLLER.TournamentStage != 3)
				{
					return;
				}
				stage = 3;
				if (currentMatchScores >= currentMatchScores2)
				{
					UIDataHolder.wonMatch = true;
					tournamentUpdate(CONTROLLER.myTeamIndex, isTie: false);
				}
				else if (currentMatchScores < currentMatchScores2)
				{
					ObscuredPrefs.SetInt("T20Paid", 0);
					UIDataHolder.wonMatch = false;
					tournamentUpdate(CONTROLLER.opponentTeamIndex, isTie: false);
					
				}
				else
				{
					ObscuredPrefs.SetInt("T20Paid", 0);
					UIDataHolder.wonMatch = false;
					decideMatchTieResult();
				}
			}
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			dictKey = "NPL";
			if (currentMatchScores >= currentMatchScores2)
			{
				UIDataHolder.wonMatch = true;
				if (CONTROLLER.NPLIndiaTournamentStage == 0)
				{
					stage = 0;
				}
				else if (CONTROLLER.NPLIndiaTournamentStage == 1)
				{
					stage = 1;
				}
				if (CONTROLLER.NPLIndiaTournamentStage == 2)
				{
					stage = 2;
				}
				if (CONTROLLER.NPLIndiaTournamentStage == 3)
				{
					stage = 3;
				}
				MatchWon = true;
			}
			else if (currentMatchScores < currentMatchScores2)
			{
				if (CONTROLLER.NPLIndiaTournamentStage == 1)
				{
				}
				if (CONTROLLER.NPLIndiaTournamentStage == 1)
				{
					if (CONTROLLER.NPLIndiaLeagueMatchIndex != 0)
					{
					}
				}
				else if (CONTROLLER.NPLIndiaTournamentStage != 2 && CONTROLLER.NPLIndiaTournamentStage != 3)
				{
				}
				MatchWon = false;
			}
			else
			{
				decideMatchTieResult();
			}
			if (MatchWon)
			{
				teamWon = CONTROLLER.myTeamIndex;
				if (CONTROLLER.meFirstBatting == 1)
				{
					if (currentMatchScores - currentMatchScores2 == 0)
					{
						result = "You WON by Net Run Rate";
					}
					else
					{
						result = "You WON by " + (currentMatchScores - currentMatchScores2) + " runs.";
					}
					if (CONTROLLER.NPLIndiaTournamentStage == 0)
					{
					}
				}
				else
				{
					if (10 - currentMatchWickets == 0)
					{
						result = "You WON by Net Run Rate";
					}
					else
					{
						result = "You WON by " + (10 - currentMatchWickets) + " wickets.";
					}
					if (CONTROLLER.NPLIndiaTournamentStage == 0)
					{
					}
				}
			}
			else if (!MatchWon)
			{
				teamWon = CONTROLLER.opponentTeamIndex;
				if (CONTROLLER.meFirstBatting == 0)
				{
					result = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation + " WON by " + (currentMatchScores2 - currentMatchScores) + " runs.";
					if (CONTROLLER.NPLIndiaTournamentStage == 0)
					{
					}
				}
				else
				{
					result = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation + " WON by " + (10 - currentMatchWickets2) + " wickets.";
					if (CONTROLLER.NPLIndiaTournamentStage == 0)
					{
					}
				}
			}
			if (CONTROLLER.NPLIndiaTournamentStage > 0)
			{
				if (CONTROLLER.NPLIndiaTournamentStage == 1)
				{
					if (CONTROLLER.NPLIndiaLeagueMatchIndex == 0)
					{
						SavePlayerPrefs.SaveUserCoins();
					}
				}
				else
				{
					SavePlayerPrefs.SaveUserCoins();
				}
			}
			UpdateNPLIndiaTournamentData(result, teamWon);
		}
		else
		{
			if (CONTROLLER.PlayModeSelected != 3)
			{
				return;
			}
			dictKey = "WC";
			if (currentMatchScores >= currentMatchScores2)
			{
				UIDataHolder.wonMatch = true;
				if (CONTROLLER.WCTournamentStage == 1)
				{
					stage = 1;
				}
				else if (CONTROLLER.WCTournamentStage == 2)
				{
					stage = 2;
				}
				else if (CONTROLLER.WCTournamentStage == 3)
				{
					stage = 3;
				}
				MatchWon = true;
			}
			else if (currentMatchScores < currentMatchScores2)
			{
				UIDataHolder.wonMatch = false;
				MatchWon = false;
			}
			else
			{
				decideMatchTieResult();
			}
			if (MatchWon)
			{
				teamWon = CONTROLLER.myTeamIndex;
				if (CONTROLLER.meFirstBatting == 1)
				{
					if (currentMatchScores - currentMatchScores2 == 0)
					{
						result = "You WON by Net Run Rate";
					}
					else
					{
						result = "You WON by " + (currentMatchScores - currentMatchScores2) + " runs.";
					}
					if (CONTROLLER.NPLIndiaTournamentStage == 0)
					{
					}
				}
				else
				{
					if (10 - currentMatchWickets == 0)
					{
						result = "You WON by Net Run Rate";
					}
					else
					{
						result = "You WON by " + (10 - currentMatchWickets) + " wickets.";
					}
					if (CONTROLLER.NPLIndiaTournamentStage == 0)
					{
					}
				}
			}
			else
			{
				teamWon = CONTROLLER.opponentTeamIndex;
				if (CONTROLLER.meFirstBatting == 0)
				{
					result = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation + " WON by " + (currentMatchScores2 - currentMatchScores) + " runs.";
				}
				else
				{
					result = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation + " WON by " + (10 - currentMatchWickets2) + " wickets.";
				}
			}
			if (CONTROLLER.WCTournamentStage > 0)
			{
				SavePlayerPrefs.SaveUserCoins();
			}
			UpdateWorldCupTournamentData(result, teamWon);
		}
	}

	private void GetMatchResultCoin(int stage, string key)
	{
	}

	private void tournamentUpdate(int winnerTeam, bool isTie)
	{
		SavePlayerPrefs.SaveUserCoins();
		setTeamFlags();
		if (winnerTeam == CONTROLLER.opponentTeamIndex)
		{
			knockOutTournamentCompleted();
			Singleton<GameModel>.instance.PlayGameSound("Lost");
			CONTROLLER.CurrentMenu = "MainMenu";
			return;
		}
		CONTROLLER.CurrentMenu = "Fixtures";
		string empty = string.Empty;
		string empty2 = string.Empty;
		CONTROLLER.matchIndex++;
		string[] array;
		string quaterFinalList;
		if (CONTROLLER.TournamentStage == 0)
		{
			array = CONTROLLER.quaterFinalList.Split("|"[0]);
			empty2 = array[array.Length - 1];
			if (array.Length < 4)
			{
				if (empty2.Contains("-"))
				{
					quaterFinalList = CONTROLLER.quaterFinalList;
					CONTROLLER.quaterFinalList = quaterFinalList + string.Empty + winnerTeam + "|";
				}
				else
				{
					quaterFinalList = CONTROLLER.quaterFinalList;
					CONTROLLER.quaterFinalList = quaterFinalList + string.Empty + winnerTeam + "-";
				}
			}
			else if (empty2.Contains("-"))
			{
				CONTROLLER.quaterFinalList = CONTROLLER.quaterFinalList + string.Empty + winnerTeam;
			}
			else
			{
				quaterFinalList = CONTROLLER.quaterFinalList;
				CONTROLLER.quaterFinalList = quaterFinalList + string.Empty + winnerTeam + "-";
			}
			setQuaterFinalMatches(isTie, winnerTeam);
		}
		else if (CONTROLLER.TournamentStage == 1)
		{
			array = CONTROLLER.semiFinalList.Split("|"[0]);
			empty2 = array[array.Length - 1];
			if (array.Length < 2)
			{
				if (empty2.Contains("-"))
				{
					quaterFinalList = CONTROLLER.semiFinalList;
					CONTROLLER.semiFinalList = quaterFinalList + string.Empty + winnerTeam + "|";
				}
				else
				{
					quaterFinalList = CONTROLLER.semiFinalList;
					CONTROLLER.semiFinalList = quaterFinalList + string.Empty + winnerTeam + "-";
				}
			}
			else if (empty2.Contains("-"))
			{
				CONTROLLER.semiFinalList = CONTROLLER.semiFinalList + string.Empty + winnerTeam;
			}
			else
			{
				quaterFinalList = CONTROLLER.semiFinalList;
				CONTROLLER.semiFinalList = quaterFinalList + string.Empty + winnerTeam + "-";
			}
			setSemiFinalMatches(isTie, winnerTeam);
		}
		else if (CONTROLLER.TournamentStage == 2)
		{
			array = CONTROLLER.finalList.Split("|"[0]);
			empty2 = array[array.Length - 1];
			if (array.Length < 2)
			{
				if (empty2.Contains("-"))
				{
					quaterFinalList = CONTROLLER.finalList;
					CONTROLLER.finalList = quaterFinalList + string.Empty + winnerTeam + "|";
				}
				else
				{
					quaterFinalList = CONTROLLER.finalList;
					CONTROLLER.finalList = quaterFinalList + string.Empty + winnerTeam + "-";
				}
			}
			else if (empty2.Contains("-"))
			{
				CONTROLLER.finalList = CONTROLLER.finalList + string.Empty + winnerTeam;
			}
			else
			{
				quaterFinalList = CONTROLLER.finalList;
				CONTROLLER.finalList = quaterFinalList + string.Empty + winnerTeam + "-";
			}
			setFinalMatches(isTie, winnerTeam);
		}
		array = CONTROLLER.tournamentStr.Split("&"[0]);
		CONTROLLER.tournamentStr = string.Empty;
		CONTROLLER.tournamentStr = CONTROLLER.TournamentStage + "&" + CONTROLLER.myTeamIndex + "&" + CONTROLLER.oversSelectedIndex + "&" + CONTROLLER.matchIndex + "&" + array[4];
		empty = CONTROLLER.tournamentStr;
		quaterFinalList = empty;
		empty = quaterFinalList + "&" + CONTROLLER.quaterFinalList + "&" + CONTROLLER.semiFinalList + "&" + CONTROLLER.finalList;
		SavePlayerPrefs.SetTournamentStage(empty);
		SavePlayerPrefs.SetTournamentStatus(CONTROLLER.tournamentStatus);
		Singleton<GameModel>.instance.PlayGameSound("Won");
	}

	private void UpdateNPLIndiaTournamentData(string result, int teamWonIndex)
	{
		int currentMatchScores = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores;
		int currentMatchScores2 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores;
		if (currentMatchScores < currentMatchScores2 && (CONTROLLER.NPLIndiaTournamentStage != 0 || (CONTROLLER.NPLIndiaTournamentStage == 1 && CONTROLLER.NPLIndiaLeagueMatchIndex == 0)))
		{
			ObscuredPrefs.SetInt("NPLPaid", 0);
		}
		int num = 0;
		int num2 = 1;
		if (CONTROLLER.tournamentType == "PAK")
		{
			num = 8;
		}
		else if (CONTROLLER.tournamentType == "AUS")
		{
			num = 7;
		}
		else if (CONTROLLER.tournamentType == "NPL")
		{
			num = 9;
		}
		if (CONTROLLER.NPLIndiaTournamentStage == 0)
		{
			if (CONTROLLER.NPLIndiaMyCurrentMatchIndex < num)
			{
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				float num7 = 0f;
				float num8 = 0f;
				num3 = CONTROLLER.myTeamIndex;
				num4 = CONTROLLER.opponentTeamIndex;
				num5 = CONTROLLER.TeamList[num3].currentMatchScores;
				num6 = CONTROLLER.TeamList[num4].currentMatchScores;
				num7 = ((CONTROLLER.TeamList[num3].currentMatchWickets >= 10) ? ((float)CONTROLLER.Overs[CONTROLLER.oversSelectedIndex]) : ((CONTROLLER.TeamList[num3].currentMatchBalls != 0) ? ((float)CONTROLLER.TeamList[num3].currentMatchBalls / 6f) : (355f / (678f * (float)Math.PI))));
				num8 = ((CONTROLLER.TeamList[num4].currentMatchWickets >= 10) ? ((float)CONTROLLER.Overs[CONTROLLER.oversSelectedIndex]) : ((CONTROLLER.TeamList[num4].currentMatchBalls != 0) ? ((float)CONTROLLER.TeamList[num4].currentMatchBalls / 6f) : (355f / (678f * (float)Math.PI))));
				CONTROLLER.NPLIndiaMyCurrentMatchIndex++;
				Singleton<LoadPlayerPrefs>.instance.savePlayerDetails(num3, num5, num7, num4, num6, num8, CONTROLLER.NPLIndiaPointsTable);
				CONTROLLER.NPLIndiaTeamWonIndexStr = CONTROLLER.NPLIndiaTeamWonIndexStr + teamWonIndex + "$";
				CONTROLLER.StoredNPLIndiaSeriesResult = CONTROLLER.StoredNPLIndiaSeriesResult + result + "&";
				CONTROLLER.NplIndiaData = string.Empty + CONTROLLER.NPLIndiaTournamentStage + "|" + CONTROLLER.myTeamIndex + "|" + CONTROLLER.NPLIndiaMyCurrentMatchIndex + "|" + CONTROLLER.NPLIndiaTeamWonIndexStr + "|" + CONTROLLER.StoredNPLIndiaSeriesResult;
				if (CONTROLLER.tournamentType == "NPL")
				{
					ObscuredPrefs.SetString("NPLIndiaLeague", CONTROLLER.NplIndiaData);
				}
				else if (CONTROLLER.tournamentType == "PAK")
				{
					ObscuredPrefs.SetString("NPLPakistanLeague", CONTROLLER.NplIndiaData);
				}
				else if (CONTROLLER.tournamentType == "AUS")
				{
					ObscuredPrefs.SetString("NplAUS", CONTROLLER.NplIndiaData);
				}
				if (CONTROLLER.NPLIndiaMyCurrentMatchIndex >= num)
				{
					completeNPLIndiaLeague();
				}
			}
			else
			{
				getQualifierTeams();
			}
		}
		else if (CONTROLLER.NPLIndiaTournamentStage == 1)
		{
			if (CONTROLLER.NPLIndiaLeagueMatchIndex == 0)
			{
				if (teamWonIndex == CONTROLLER.myTeamIndex || teamWonIndex == -1)
				{
					teamWonIndex = CONTROLLER.myTeamIndex;
					CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
					SetNPLFinalTeams(teamWonIndex);
				}
				else if (teamWonIndex == CONTROLLER.opponentTeamIndex)
				{
					SetNPLQualifier2(CONTROLLER.myTeamIndex);
				}
			}
			else if (teamWonIndex == CONTROLLER.myTeamIndex || teamWonIndex == -1)
			{
				teamWonIndex = CONTROLLER.myTeamIndex;
				CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
				SetNPLQualifier2(teamWonIndex);
			}
			else if (teamWonIndex == CONTROLLER.opponentTeamIndex)
			{
				CONTROLLER.CurrentMenu = "mainmenu";
				ClearNPLIndiaTournamentInfo();
			}
		}
		else if (CONTROLLER.NPLIndiaTournamentStage == 2)
		{
			if (teamWonIndex == CONTROLLER.myTeamIndex || teamWonIndex == -1)
			{
				teamWonIndex = CONTROLLER.myTeamIndex;
				CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
				SetNPLFinalTeams(teamWonIndex);
			}
			else if (teamWonIndex == CONTROLLER.opponentTeamIndex)
			{
				CONTROLLER.CurrentMenu = "mainmenu";
				ClearNPLIndiaTournamentInfo();
			}
		}
		else if (CONTROLLER.NPLIndiaTournamentStage == 3)
		{
			if (teamWonIndex != CONTROLLER.myTeamIndex && teamWonIndex != -1 && teamWonIndex == CONTROLLER.opponentTeamIndex)
			{
				CONTROLLER.CurrentMenu = "mainmenu";
			}
			ClearNPLIndiaTournamentInfo();
		}
	}

	private void SetNPLFinalTeams(int winnerTeam)
	{
		string[] array = CONTROLLER.NplIndiaData.Split("&"[0]);
		CONTROLLER.NPLIndiaTournamentStage = int.Parse(array[0]);
		CONTROLLER.myTeamIndex = int.Parse(array[1]);
		CONTROLLER.NPLIndiaLeagueMatchIndex = int.Parse(array[2]);
		string text = string.Empty + array[3];
		string[] array2 = text.Split("#"[0]);
		string empty = string.Empty;
		empty = string.Empty + array2[2];
		string[] array3 = empty.Split("|"[0]);
		empty = string.Empty + array3[0];
		string text2;
		if (empty.Contains("-"))
		{
			empty = empty + string.Empty + winnerTeam;
		}
		else
		{
			text2 = empty;
			empty = text2 + string.Empty + winnerTeam + "-";
		}
		if (CONTROLLER.NPLIndiaTournamentStage == 2)
		{
			CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
			CONTROLLER.NPLIndiaTournamentStage = 3;
		}
		else
		{
			CONTROLLER.NPLIndiaLeagueMatchIndex++;
		}
		string text3 = CONTROLLER.NPLIndiaTournamentStage + "&" + CONTROLLER.myTeamIndex + "&" + CONTROLLER.NPLIndiaLeagueMatchIndex;
		text2 = text3;
		text3 = (CONTROLLER.NplIndiaData = text2 + "&" + array2[0] + "#" + array2[1] + "#" + empty);
		CONTROLLER.CurrentMenu = "nplindiaplayoff";
		if (CONTROLLER.tournamentType == "NPL")
		{
			ObscuredPrefs.SetString("NPLIndiaPlayOff", CONTROLLER.NplIndiaData);
		}
		else if (CONTROLLER.tournamentType == "PAK")
		{
			ObscuredPrefs.SetString("NPLPakistanPlayOff", CONTROLLER.NplIndiaData);
		}
		else if (CONTROLLER.tournamentType == "AUS")
		{
			ObscuredPrefs.SetString("NplAustraliaPlayoff", CONTROLLER.NplIndiaData);
		}
	}

	private void SetNPLQualifier2(int Team)
	{
		string[] array = CONTROLLER.NplIndiaData.Split("&"[0]);
		CONTROLLER.NPLIndiaTournamentStage = int.Parse(array[0]);
		CONTROLLER.myTeamIndex = int.Parse(array[1]);
		CONTROLLER.NPLIndiaLeagueMatchIndex = int.Parse(array[2]);
		string text = string.Empty + array[3];
		string[] array2 = text.Split("#"[0]);
		string text2 = string.Empty + array2[1];
		string[] array3 = text2.Split("|"[0]);
		text2 = string.Empty + array3[0];
		string text3;
		if (text2.Contains("-"))
		{
			text2 = text2 + string.Empty + Team;
		}
		else
		{
			text3 = text2;
			text2 = text3 + string.Empty + Team + "-";
		}
		string text4 = string.Empty;
		if (CONTROLLER.NPLIndiaLeagueMatchIndex == 0)
		{
			string text5 = string.Empty + array2[0];
			string[] array4 = text5.Split("|"[0]);
			string text6 = string.Empty + array4[0];
			array4 = text6.Split("-"[0]);
			text4 = ((int.Parse(string.Empty + array4[0]) != Team) ? (text4 + string.Empty + array4[0] + "-") : (text4 + string.Empty + array4[1] + "-"));
		}
		if (CONTROLLER.NPLIndiaLeagueMatchIndex == 1)
		{
			text4 = string.Empty + array2[2];
			CONTROLLER.NPLIndiaTournamentStage = 2;
			CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
		}
		else
		{
			CONTROLLER.NPLIndiaLeagueMatchIndex++;
		}
		string text7 = CONTROLLER.NPLIndiaTournamentStage + "&" + CONTROLLER.myTeamIndex + "&" + CONTROLLER.NPLIndiaLeagueMatchIndex;
		text3 = text7;
		text7 = (CONTROLLER.NplIndiaData = text3 + "&" + array2[0] + "#" + text2 + "#" + text4);
		CONTROLLER.CurrentMenu = "nplindiaplayoff";
		if (CONTROLLER.tournamentType == "NPL")
		{
			ObscuredPrefs.SetString("NPLIndiaPlayOff", CONTROLLER.NplIndiaData);
		}
		else if (CONTROLLER.tournamentType == "PAK")
		{
			ObscuredPrefs.SetString("NPLPakistanPlayOff", CONTROLLER.NplIndiaData);
		}
		else if (CONTROLLER.tournamentType == "AUS")
		{
			ObscuredPrefs.SetString("NplAustraliaPlayoff", CONTROLLER.NplIndiaData);
		}
	}

	private void completeNPLIndiaLeague()
	{
		if (CONTROLLER.NPLIndiaLeagueMatchIndex < CONTROLLER.NPLIndiaMatchDetails.Length)
		{
			if (Singleton<LoadPlayerPrefs>.instance.getNPLMatches(CONTROLLER.NPLIndiaMatchDetails, CONTROLLER.NPLIndiaLeagueMatchIndex))
			{
				completeNPLIndiaLeague();
			}
			else
			{
				getQualifierTeams();
			}
		}
		else
		{
			getQualifierTeams();
		}
	}

	private void getQualifierTeams()
	{
		CONTROLLER.NPLIndiaSortedPointsTable = Singleton<LoadPlayerPrefs>.instance.SortWCPointsTable(CONTROLLER.NPLIndiaPointsTable, CONTROLLER.NPLIndiaSortedPointsTable);
		for (int i = 0; i < CONTROLLER.NPLIndiaSortedPointsTable.Length; i++)
		{
		}
		setQualifierTeams();
	}

	private void setQualifierTeams()
	{
		int num = 4;
		bool flag = false;
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			if (CONTROLLER.myTeamIndex == CONTROLLER.NPLIndiaSortedPointsTable[i])
			{
				flag = true;
				num2 = i;
			}
		}
		if (flag)
		{
			CONTROLLER.NplIndiaData = string.Empty + CONTROLLER.NPLIndiaTournamentStage + "|" + CONTROLLER.myTeamIndex + "|" + CONTROLLER.NPLIndiaMyCurrentMatchIndex + "|" + CONTROLLER.NPLIndiaTeamWonIndexStr + "|" + CONTROLLER.StoredNPLIndiaSeriesResult;
			if (CONTROLLER.tournamentType == "NPL")
			{
				ObscuredPrefs.SetString("NPLIndiaPlayOff", CONTROLLER.NplIndiaData);
			}
			else if (CONTROLLER.tournamentType == "PAK")
			{
				ObscuredPrefs.SetString("NPLPakistanPlayOff", CONTROLLER.NplIndiaData);
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				ObscuredPrefs.SetString("NplAustraliaPlayoff", CONTROLLER.NplIndiaData);
			}
			CONTROLLER.CurrentMenu = "nplindialeague";
		}
		else
		{
			CONTROLLER.CurrentMenu = "mainmenu";
			CONTROLLER.screenToDisplay = "landingPage";
			Singleton<TournamentFailedPopUp>.instance.showMe = true;
			ClearNPLIndiaTournamentInfo();
		}
	}

	private void ClearNPLIndiaTournamentInfo()
	{
		if (CONTROLLER.tournamentType == "NPL")
		{
			if (ObscuredPrefs.HasKey("NPLIndiaLeague"))
			{
				ObscuredPrefs.DeleteKey("NPLIndiaLeague");
			}
			if (ObscuredPrefs.HasKey("NPLIndiaPlayOff"))
			{
				ObscuredPrefs.DeleteKey("NPLIndiaPlayOff");
			}
			if (ObscuredPrefs.HasKey("NPLIndiaPointsTable"))
			{
				ObscuredPrefs.DeleteKey("NPLIndiaPointsTable");
			}
			if (ObscuredPrefs.HasKey("NPLIndiaLeagueMatchIndex"))
			{
				ObscuredPrefs.DeleteKey("NPLIndiaLeagueMatchIndex");
			}
		}
		else if (CONTROLLER.tournamentType == "PAK")
		{
			if (ObscuredPrefs.HasKey("NPLPakistanLeague"))
			{
				ObscuredPrefs.DeleteKey("NPLPakistanLeague");
			}
			if (ObscuredPrefs.HasKey("NPLPakistanPlayOff"))
			{
				ObscuredPrefs.DeleteKey("NPLPakistanPlayOff");
			}
			if (ObscuredPrefs.HasKey("NPLPakistanPointsTable"))
			{
				ObscuredPrefs.DeleteKey("NPLPakistanPointsTable");
			}
			if (ObscuredPrefs.HasKey("NPLPakistanLeagueMatchIndex"))
			{
				ObscuredPrefs.DeleteKey("NPLPakistanLeagueMatchIndex");
			}
		}
		else if (CONTROLLER.tournamentType == "AUS")
		{
			if (ObscuredPrefs.HasKey("NplAUS"))
			{
				ObscuredPrefs.DeleteKey("NplAUS");
			}
			if (ObscuredPrefs.HasKey("NplAustraliaPlayoff"))
			{
				ObscuredPrefs.DeleteKey("NplAustraliaPlayoff");
			}
			if (ObscuredPrefs.HasKey("NPLAustraliaPointsTable"))
			{
				ObscuredPrefs.DeleteKey("NPLAustraliaPointsTable");
			}
			if (ObscuredPrefs.HasKey("NPLAustraliaLeagueMatchIndex"))
			{
				ObscuredPrefs.DeleteKey("NPLAustraliaLeagueMatchIndex");
			}
		}
		CONTROLLER.CurrentMenu = "mainmenu";
		CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
		CONTROLLER.NPLIndiaMyCurrentMatchIndex = 0;
		CONTROLLER.NplIndiaData = string.Empty;
		CONTROLLER.StoredNPLIndiaSeriesResult = string.Empty;
		CONTROLLER.NPLIndiaTeamWonIndexStr = string.Empty;
	}

	private void UpdateWorldCupTournamentData(string result, int teamWonIndex)
	{
		int currentMatchScores = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores;
		int currentMatchScores2 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores;
		if (currentMatchScores < currentMatchScores2 && CONTROLLER.WCTournamentStage != 0)
		{
			ObscuredPrefs.SetInt("WCPaid", 0);
		}
		WCTournament = CONTROLLER.WCSchedule.Split("&"[0]);
		CONTROLLER.WCMatchDetails = WCTournament[1].Split("|"[0]);
		if (CONTROLLER.WCTournamentStage == 0)
		{
			if (CONTROLLER.WCMyCurrentMatchIndex < 7)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				float num5 = 0f;
				float num6 = 0f;
				num = CONTROLLER.myTeamIndex;
				num2 = CONTROLLER.opponentTeamIndex;
				num3 = CONTROLLER.TeamList[num].currentMatchScores;
				num4 = CONTROLLER.TeamList[num2].currentMatchScores;
				num5 = ((CONTROLLER.TeamList[num].currentMatchWickets >= 10) ? ((float)CONTROLLER.Overs[CONTROLLER.oversSelectedIndex]) : ((CONTROLLER.TeamList[num].currentMatchBalls != 0) ? ((float)CONTROLLER.TeamList[num].currentMatchBalls / 6f) : (355f / (678f * (float)Math.PI))));
				num6 = ((CONTROLLER.TeamList[num2].currentMatchWickets >= 10) ? ((float)CONTROLLER.Overs[CONTROLLER.oversSelectedIndex]) : ((CONTROLLER.TeamList[num2].currentMatchBalls != 0) ? ((float)CONTROLLER.TeamList[num2].currentMatchBalls / 6f) : (355f / (678f * (float)Math.PI))));
				CONTROLLER.WCMyCurrentMatchIndex++;
				Singleton<LoadPlayerPrefs>.instance.savePlayerDetails(num, num3, num5, num2, num4, num6, CONTROLLER.WCPointsTable);
				if (currentMatchScores == currentMatchScores2)
				{
					result = "MATCH  TIED";
				}
				CONTROLLER.WCTeamWonIndexStr = CONTROLLER.WCTeamWonIndexStr + teamWonIndex + "$";
				CONTROLLER.StoredWCTournamentResult = CONTROLLER.StoredWCTournamentResult + result + "&";
				CONTROLLER.WCLeagueData = string.Empty + CONTROLLER.WCTournamentStage + "|" + CONTROLLER.myTeamIndex + "|" + CONTROLLER.WCMyCurrentMatchIndex + "|" + CONTROLLER.WCTeamWonIndexStr + "|" + CONTROLLER.StoredWCTournamentResult;
				ObscuredPrefs.SetString("worldcup", CONTROLLER.WCLeagueData);
				if (CONTROLLER.WCMyCurrentMatchIndex >= 7)
				{
					completeWCTournament();
				}
			}
			else
			{
				getQuarterFinalTeams();
			}
		}
		else if (CONTROLLER.WCTournamentStage == 1)
		{
			if (teamWonIndex == CONTROLLER.myTeamIndex || teamWonIndex == -1)
			{
				teamWonIndex = CONTROLLER.myTeamIndex;
				CONTROLLER.WCLeagueMatchIndex = 0;
				SetSemiFinalTeams(teamWonIndex);
			}
			else if (teamWonIndex == CONTROLLER.opponentTeamIndex)
			{
				CONTROLLER.CurrentMenu = "mainmenu";
				ClearWCTournamentInfo();
			}
		}
		else if (CONTROLLER.WCTournamentStage == 2)
		{
			if (teamWonIndex == CONTROLLER.myTeamIndex || teamWonIndex == -1)
			{
				teamWonIndex = CONTROLLER.myTeamIndex;
				CONTROLLER.WCLeagueMatchIndex = 0;
				SetFinalTeams(teamWonIndex);
			}
			else if (teamWonIndex == CONTROLLER.opponentTeamIndex)
			{
				CONTROLLER.CurrentMenu = "mainmenu";
				ClearWCTournamentInfo();
			}
		}
		else if (CONTROLLER.WCTournamentStage == 3)
		{
			if (teamWonIndex != CONTROLLER.myTeamIndex && teamWonIndex != -1 && teamWonIndex == CONTROLLER.opponentTeamIndex)
			{
				CONTROLLER.CurrentMenu = "mainmenu";
			}
			ClearWCTournamentInfo();
		}
	}

	private void completeWCTournament()
	{
		if (CONTROLLER.WCLeagueMatchIndex < CONTROLLER.WCMatchDetails.Length)
		{
			if (Singleton<LoadPlayerPrefs>.instance.getWCMatch())
			{
				completeWCTournament();
			}
			else
			{
				getQuarterFinalTeams();
			}
		}
		else
		{
			getQuarterFinalTeams();
		}
	}

	private void getQuarterFinalTeams()
	{
		CONTROLLER.WCSortedPointsTable = Singleton<LoadPlayerPrefs>.instance.SortWCPointsTable(CONTROLLER.WCPointsTable, CONTROLLER.WCSortedPointsTable);
		CheckQualifiedForQuarterFinals();
	}

	private void CheckQualifiedForQuarterFinals()
	{
		int num = 0;
		int num2 = 0;
		bool flag = false;
		for (int i = 0; i < CONTROLLER.WCSortedPointsTable.Length; i++)
		{
			if (CONTROLLER.WCGroupDetails[CONTROLLER.WCSortedPointsTable[i]] == "A")
			{
				if (num <= 3)
				{
					if (CONTROLLER.myTeamIndex == CONTROLLER.WCSortedPointsTable[i])
					{
						flag = true;
					}
					num++;
				}
			}
			else if (CONTROLLER.WCGroupDetails[CONTROLLER.WCSortedPointsTable[i]] == "B" && num2 <= 3)
			{
				if (CONTROLLER.myTeamIndex == CONTROLLER.WCSortedPointsTable[i])
				{
					flag = true;
				}
				num2++;
			}
		}
		if (flag)
		{
			CONTROLLER.WCLeagueData = string.Empty + CONTROLLER.WCTournamentStage + "|" + CONTROLLER.myTeamIndex + "|" + 8 + "|" + CONTROLLER.WCTeamWonIndexStr + "|" + CONTROLLER.StoredWCTournamentResult;
			ObscuredPrefs.SetString("worldcup", CONTROLLER.WCLeagueData);
		}
		else
		{
			CONTROLLER.CurrentMenu = "mainmenu";
			CONTROLLER.screenToDisplay = "landingPage";
			Singleton<TournamentFailedPopUp>.instance.showMe = true;
			ClearWCTournamentInfo();
		}
	}

	private void SetSemiFinalTeams(int winnerTeam)
	{
		string[] array = new string[4];
		string[] array2 = CONTROLLER.WCLeagueData.Split("&"[0]);
		CONTROLLER.WCTournamentStage = int.Parse(array2[0]);
		CONTROLLER.myTeamIndex = int.Parse(array2[1]);
		CONTROLLER.WCMyCurrentMatchIndex = int.Parse(array2[2]);
		string text = string.Empty + array2[3];
		string[] array3 = text.Split("#"[0]);
		string[] array4 = array3[1].Split("|"[0]);
		string text2 = string.Empty;
		int num = 0;
		for (num = 0; num < array4.Length; num++)
		{
			text2 = ((array4.Length - 1 <= num) ? (text2 + array4[num]) : (text2 + array4[num] + "|"));
		}
		string text3;
		if (array4.Length < array.Length / 2)
		{
			if (text2.Contains("-"))
			{
				text3 = text2;
				text2 = text3 + string.Empty + winnerTeam + "|";
			}
			else
			{
				text3 = text2;
				text2 = text3 + string.Empty + winnerTeam + "-";
			}
		}
		else if (text2.Contains("-"))
		{
			text2 = text2 + string.Empty + winnerTeam;
		}
		else
		{
			text3 = text2;
			text2 = text3 + string.Empty + winnerTeam + "-";
		}
		array4 = text2.Split("|"[0]);
		CONTROLLER.WCMyCurrentMatchIndex++;
		if (CONTROLLER.WCMyCurrentMatchIndex >= array.Length)
		{
			CONTROLLER.WCTournamentStage = 2;
			CONTROLLER.WCMyCurrentMatchIndex = 0;
		}
		string text4 = CONTROLLER.WCTournamentStage + "&";
		text3 = text4;
		text4 = text3 + CONTROLLER.myTeamIndex + "&" + CONTROLLER.WCMyCurrentMatchIndex + "&";
		text4 = text4 + array3[0] + "#";
		for (num = 0; num < array4.Length; num++)
		{
			if (!(array4[num] != string.Empty))
			{
				continue;
			}
			array = array4[num].Split("-"[0]);
			if (array.Length > 1)
			{
				if (num < array4.Length - 1)
				{
					text3 = text4;
					text4 = text3 + string.Empty + array[array.Length - 2] + "-" + array[array.Length - 1] + "|";
				}
				else
				{
					text3 = text4;
					text4 = text3 + string.Empty + array[array.Length - 2] + "-" + array[array.Length - 1] + string.Empty;
				}
			}
			else
			{
				text4 = text4 + string.Empty + array[array.Length - 1] + "-";
			}
		}
		text4 = (CONTROLLER.WCLeagueData = text4 + "#");
		ObscuredPrefs.SetString("wcplayoff", CONTROLLER.WCLeagueData);
		CONTROLLER.CurrentMenu = "WCPlayOffs";
	}

	private void SetFinalTeams(int winnerTeam)
	{
		string[] array = new string[2];
		string[] array2 = CONTROLLER.WCLeagueData.Split("&"[0]);
		CONTROLLER.WCTournamentStage = int.Parse(array2[0]);
		CONTROLLER.myTeamIndex = int.Parse(array2[1]);
		CONTROLLER.WCMyCurrentMatchIndex = int.Parse(array2[2]);
		string text = string.Empty + array2[3];
		string[] array3 = text.Split("#"[0]);
		string[] array4 = array3[2].Split("|"[0]);
		string text2 = string.Empty;
		int num = 0;
		for (num = 0; num < array4.Length; num++)
		{
			text2 = ((array4.Length - 1 <= num) ? (text2 + array4[num]) : (text2 + array4[num] + "|"));
		}
		string text3;
		if (array4.Length < array.Length / 2)
		{
			if (text2.Contains("-"))
			{
				text3 = text2;
				text2 = text3 + string.Empty + winnerTeam + "|";
			}
			else
			{
				text3 = text2;
				text2 = text3 + string.Empty + winnerTeam + "-";
			}
		}
		else if (text2.Contains("-"))
		{
			text2 = text2 + string.Empty + winnerTeam;
		}
		else
		{
			text3 = text2;
			text2 = text3 + string.Empty + winnerTeam + "-";
		}
		array4 = text2.Split("|"[0]);
		CONTROLLER.WCMyCurrentMatchIndex++;
		if (CONTROLLER.WCMyCurrentMatchIndex >= array.Length)
		{
			CONTROLLER.WCTournamentStage = 3;
			CONTROLLER.WCMyCurrentMatchIndex = 0;
		}
		string text4 = CONTROLLER.WCTournamentStage + "&";
		text3 = text4;
		text4 = text3 + CONTROLLER.myTeamIndex + "&" + CONTROLLER.WCMyCurrentMatchIndex + "&";
		text3 = text4;
		text4 = text3 + array3[0] + "#" + array3[1] + "#";
		for (num = 0; num < array4.Length; num++)
		{
			if (!(array4[num] != string.Empty))
			{
				continue;
			}
			array = array4[num].Split("-"[0]);
			if (array.Length > 1)
			{
				if (num < array4.Length - 1)
				{
					text3 = text4;
					text4 = text3 + string.Empty + array[array.Length - 2] + "-" + array[array.Length - 1] + "|";
				}
				else
				{
					text3 = text4;
					text4 = text3 + string.Empty + array[array.Length - 2] + "-" + array[array.Length - 1] + string.Empty;
				}
			}
			else
			{
				text4 = text4 + string.Empty + array[array.Length - 1] + "-";
			}
		}
		CONTROLLER.WCLeagueData = text4;
		ObscuredPrefs.SetString("wcplayoff", CONTROLLER.WCLeagueData);
		CONTROLLER.CurrentMenu = "WCPlayOffs";
	}

	private void ClearWCTournamentInfo()
	{
		if (ObscuredPrefs.HasKey("worldcup"))
		{
			ObscuredPrefs.DeleteKey("worldcup");
		}
		if (ObscuredPrefs.HasKey("wcplayoff"))
		{
			ObscuredPrefs.DeleteKey("wcplayoff");
		}
		if (ObscuredPrefs.HasKey("WCPointsTable"))
		{
			ObscuredPrefs.DeleteKey("WCPointsTable");
		}
		if (ObscuredPrefs.HasKey("WCLeagueMatchIndex"))
		{
			ObscuredPrefs.DeleteKey("WCLeagueMatchIndex");
		}
		CONTROLLER.CurrentMenu = "MainMenu";
		CONTROLLER.WCTournamentStage = 0;
		CONTROLLER.WCMyCurrentMatchIndex = 0;
		CONTROLLER.WCLeagueData = string.Empty;
		CONTROLLER.StoredWCTournamentResult = string.Empty;
		CONTROLLER.WCTeamWonIndexStr = string.Empty;
	}

	private void setTeamFlags()
	{
	}

	public void decideMatchTieResult()
	{
		int num = calculateNRR();
		if (num != CONTROLLER.myTeamIndex)
		{
			setTeamFlags();
			knockOutTournamentCompleted();
			Singleton<GameModel>.instance.PlayGameSound("Lost");
			
			MatchWon = false;
		}
		else
		{
			MatchWon = true;
			if (CONTROLLER.PlayModeSelected != 0)
			{
				tournamentUpdate(num, isTie: true);
			}
		}
	}

	private void setQuaterFinalMatches(bool isTie, int winnerTeam)
	{
		getQuaterFinalData = CONTROLLER.quaterFinalList.Split("|"[0]);
		int num = -1;
		int num2 = -1;
		string[] array = getQuaterFinalData[0].Split("-"[0]);
		if (array.Length != 0 && array[0] != null && !(array[0] == string.Empty) && !(array[0] == " "))
		{
			if (winnerTeam == CONTROLLER.myTeamIndex && CONTROLLER.matchIndex >= 8)
			{
				CONTROLLER.TournamentStage = 1;
				CONTROLLER.matchIndex = 0;
			}
			
			CONTROLLER.tournamentStatus = "QUARTER FINAL";
		}
	}

	private void setSemiFinalMatches(bool isTie, int winnerTeam)
	{
		getSemiFinalData = CONTROLLER.semiFinalList.Split("|"[0]);
		int num = -1;
		int num2 = -1;
		string[] array = getSemiFinalData[0].Split("-"[0]);
		if (array.Length != 0 && array[0] != null && !(array[0] == string.Empty) && !(array[0] == " "))
		{
			if (isTie)
			{
			}
			if (winnerTeam == CONTROLLER.myTeamIndex && CONTROLLER.matchIndex >= 4)
			{
				CONTROLLER.TournamentStage = 2;
				CONTROLLER.matchIndex = 0;
			}
			
			CONTROLLER.tournamentStatus = "SEMI FINAL";
		}
	}

	private void setFinalMatches(bool isTie, int winnerTeam)
	{
		getFinalData = CONTROLLER.finalList.Split("|"[0]);
		int num = -1;
		int num2 = -1;
		string[] array = getFinalData[0].Split("-"[0]);
		if (array.Length != 0 && array[0] != null && !(array[0] == string.Empty) && !(array[0] == " "))
		{
			if (isTie)
			{
			}
			if (winnerTeam == CONTROLLER.myTeamIndex && CONTROLLER.matchIndex >= 2 && (array[1] != null || array[1] == string.Empty || array[1] == " "))
			{
				CONTROLLER.TournamentStage = 3;
				CONTROLLER.matchIndex = 0;
			}
			
			CONTROLLER.tournamentStatus = "FINAL";
		}
	}

	private int calculateNRR()
	{
		int currentMatchScores = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores;
		int currentMatchScores2 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores;
		int num = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchBalls;
		int num2 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchBalls;
		int currentMatchWickets = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets;
		int currentMatchWickets2 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchWickets;
		if (currentMatchWickets >= 10)
		{
			num = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6;
		}
		if (currentMatchWickets2 >= 10)
		{
			num2 = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6;
		}
		float num3 = currentMatchScores / num - currentMatchScores2 / num2 + currentMatchWickets;
		float num4 = currentMatchScores2 / num2 - currentMatchScores / num + currentMatchWickets2;
		if (num3 < num4)
		{
			return CONTROLLER.myTeamIndex;
		}
		if (num3 > num4)
		{
			return CONTROLLER.opponentTeamIndex;
		}
		return CONTROLLER.myTeamIndex;
	}

	private void knockOutTournamentCompleted()
	{
		if (CONTROLLER.TournamentStage != 3)
		{
			setTeamFlags();
		}
		if (ObscuredPrefs.HasKey("tour"))
		{
			ObscuredPrefs.DeleteKey("tour");
		}
		if (ObscuredPrefs.HasKey("tstatus"))
		{
			ObscuredPrefs.DeleteKey("tstatus");
		}
		CONTROLLER.CurrentMenu = "MainMenu";
		CONTROLLER.tournamentStr = string.Empty;
		CONTROLLER.quaterFinalList = string.Empty;
		CONTROLLER.semiFinalList = string.Empty;
		CONTROLLER.finalList = string.Empty;
		CONTROLLER.TournamentStage = 0;
		CONTROLLER.matchIndex = 0;
		CONTROLLER.tournamentStatus = "LEAGUE MATCH";
	}

	public void openMatchSummary()
	{
		Hide(boolean: true);
		Singleton<MatchSummary>.instance.showMe();
	}

	public void GameQuit(int index)
	{
		CONTROLLER.isAutoPlayed = false;
		Singleton<GameModel>.instance.UpdateAction(-1);
		Singleton<GameModel>.instance.GameQuitted();
		if (Singleton<GameOverDisplay>.instance.holder.activeSelf)
		{
			Singleton<GameOverDisplay>.instance.holder.SetActive(value: false);
		}
		if (Singleton<SuperOverResult>.instance.Holder.activeSelf)
		{
			Singleton<SuperOverResult>.instance.Holder.SetActive(value: false);
		}
		if (Singleton<SuperChaseResult>.instance.holder.activeSelf)
		{
			Singleton<SuperChaseResult>.instance.holder.SetActive(value: false);
		}
		if (Singleton<TMGameOverDisplay>.instance.holder.activeSelf)
		{
			Singleton<TMGameOverDisplay>.instance.holder.SetActive(value: false);
		}
		Time.timeScale = 1f;
	}

	public void Hide(bool boolean)
	{
		if (!boolean)
		{
			BG.SetActive(value: true);
			Singleton<GameModel>.instance.UpdateAction(8);
			CONTROLLER.gameCompleted = true;
			stage = 0;
			DisPlayResult();
			Singleton<GameOverDisplay>.instance.ShowMe();
			previewPanel.SetActive(value: false);
		}
	}

	public void showMe()
	{
		Singleton<Scoreboard>.instance.pauseBtn.gameObject.SetActive(value: false);
		Singleton<BattingControls>.instance.LoftBtn.gameObject.SetActive(value: false);
		Singleton<GameModel>.instance.rvPopup.SetActive(value: false);
		Singleton<GameOverDisplay>.instance.ShowMe();
		previewPanel.SetActive(value: false);
		StartCoroutine(Singleton<AchievementsSyncronizer>.instance.SendAchievementsAndKits());
	}

	public bool CheckRebowlAvailability()
	{
		if (CONTROLLER.meFirstBatting == 0 && CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchBalls >= CONTROLLER.totalOvers * 6 && CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores <= CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores && CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores - CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores < 12)
		{
			return true;
		}
		return false;
	}
}
