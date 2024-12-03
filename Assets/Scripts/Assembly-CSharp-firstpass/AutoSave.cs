using CodeStage.AntiCheat.ObscuredTypes;

public class AutoSave
{
	public static int currentBall;

	public static int runsScoredInOver;

	public static int WicketsInOver;

	public static bool maxRunsReached;

	public static bool maxWidesReached;

	public static int currentPartnership;

	public static bool fourInOver;

	public static bool sixInOver;

	public static bool maxSixes;

	public static string ballUpdate;

	public static string ballListInfo;

	public static string ballextras;

	private string temp;

	public static string simulatedBallAngle;

	public static string simulatedBallHorizontalSpeed;

	public static string simulatedBallProjectileHeight;

	public static string simulatedBallSpotLength;

	public static string simulatedBallSwingValue;

	public static string simulatedBallSpinValue;

	public static string currentBowlerType;

	public static string currentBowlerHand;

	public static string bowlerSide;

	public static string ballScoreData;

	public static string noOfBallDataSetted;

	public static void WriteFile(string output)
	{
		string key = GenerateFileName();
		ObscuredPrefs.SetString(key, output);
	}

	public static string ReadFile()
	{
		string key = GenerateFileName();
		string result = string.Empty;
		if (ObscuredPrefs.HasKey(key))
		{
			result = ObscuredPrefs.GetString(key);
		}
		return result;
	}

	public static void DeleteFile()
	{
		string key = GenerateFileName();
		if (ObscuredPrefs.HasKey(key))
		{
			ObscuredPrefs.DeleteKey(key);
		}
	}

	public static void LoadGame()
	{
		string empty = string.Empty;
		empty = ReadFile();
		ParseSavedMatch(empty);
	}

	private static string GenerateFileName()
	{
		string result = string.Empty;
		if (CONTROLLER.PlayModeSelected == 0)
		{
			result = "Exhibition";
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			result = "Tournament";
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "NPL")
			{
				result = "Tour-NPLINDIA";
			}
			else if (CONTROLLER.tournamentType == "PAK")
			{
				result = "Tour-NPLPAK";
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				result = "Tour-NPLAUS";
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			result = "Tour-WC";
		}
		else if (CONTROLLER.PlayModeSelected == 4)
		{
			result = "SuperOver" + CONTROLLER.SuperOverMode;
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			result = "SuperChase";
		}
		else if (CONTROLLER.PlayModeSelected == 7)
		{
			result = "TestMatch";
		}
		return result;
	}

	public static void SaveInGameMatch()
	{
		if (CONTROLLER.PlayModeSelected == 6)
		{
			return;
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "Refund"))
		{
			ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "Refund");
		}
		string empty = string.Empty;
		empty += "<SavedDetail>";
		empty += "<controller>";
		string text = empty;
		empty = text + "<currentPageIndex>" + CONTROLLER.currentPageIndex + "</currentPageIndex>";
		text = empty;
		empty = text + "<myTeamIndex>" + CONTROLLER.myTeamIndex + "</myTeamIndex>";
		text = empty;
		empty = text + "<opponentTeamIndex>" + CONTROLLER.opponentTeamIndex + "</opponentTeamIndex>";
		text = empty;
		empty = text + "<oversSelectedIndex>" + CONTROLLER.oversSelectedIndex + "</oversSelectedIndex>";
		text = empty;
		empty = text + "<MyTeamStateIndex>" + CONTROLLER.MyTeamStateIndex + "</MyTeamStateIndex>";
		text = empty;
		empty = text + "<OppTeamStateIndex>" + CONTROLLER.OppTeamStateIndex + "</OppTeamStateIndex>";
		text = empty;
		empty = text + "<meFirstBatting>" + CONTROLLER.meFirstBatting + "</meFirstBatting>";
		text = empty;
		empty = text + "<BattingTeamIndex>" + CONTROLLER.BattingTeamIndex + "</BattingTeamIndex>";
		text = empty;
		empty = text + "<BowlingTeamIndex>" + CONTROLLER.BowlingTeamIndex + "</BowlingTeamIndex>";
		text = empty;
		empty = text + "<StrikerIndex>" + CONTROLLER.StrikerIndex + "</StrikerIndex>";
		text = empty;
		empty = text + "<NonStrikerIndex>" + CONTROLLER.NonStrikerIndex + "</NonStrikerIndex>";
		text = empty;
		empty = text + "<CurrentBowlerIndex>" + CONTROLLER.CurrentBowlerIndex + "</CurrentBowlerIndex>";
		text = empty;
		empty = text + "<wickerKeeperIndex>" + CONTROLLER.wickerKeeperIndex + "</wickerKeeperIndex>";
		empty = empty + "<difficultyMode>" + CONTROLLER.difficultyMode + "</difficultyMode>";
		text = empty;
		empty = text + "<ConfidenceVal>" + CONTROLLER.ConfidenceVal + "</ConfidenceVal>";
		text = empty;
		empty = text + "<fielderChangeIndex>" + CONTROLLER.fielderChangeIndex + "</fielderChangeIndex>";
		text = empty;
		empty = text + "<computerFielderChangeIndex>" + CONTROLLER.computerFielderChangeIndex + "</computerFielderChangeIndex>";
		text = empty;
		empty = text + "<HattrickBall>" + CONTROLLER.HattrickBall + "</HattrickBall>";
		text = empty;
		empty = text + "<currentInnings>" + CONTROLLER.currentInnings + "</currentInnings>";
		text = empty;
		empty = text + "<RunsInAOver>" + CONTROLLER.RunsInAOver + "</RunsInAOver>";
		text = empty;
		empty = text + "<WideInAOver>" + CONTROLLER.WideInAOver + "</WideInAOver>";
		text = empty;
		empty = text + "<DotInAOver>" + CONTROLLER.DotInAOver + "</DotInAOver>";
		text = empty;
		empty = text + "<NewOver>" + CONTROLLER.NewOver + "</NewOver>";
		text = empty;
		empty = text + "<InBetweenOvers>" + CONTROLLER.InBetweenOvers + "</InBetweenOvers>";
		text = empty;
		empty = text + "<TournamentSixes>" + CONTROLLER.TournamentSixes + "</TournamentSixes>";
		text = empty;
		empty = text + "<TournamentStage>" + CONTROLLER.TournamentStage + "</TournamentStage>";
		empty = empty + "<BowlerSide>" + CONTROLLER.BowlerSide + "</BowlerSide>";
		empty = empty + "<ballUpdate>" + ballUpdate + "</ballUpdate>";
		empty = empty + "<ballListInfo>" + ballListInfo + "</ballListInfo>";
		empty = empty + "<ballExtras>" + ballextras + "</ballExtras>";
		empty = empty + "<simulatedBallAngle>" + simulatedBallAngle + "</simulatedBallAngle>";
		empty = empty + "<simulatedBallHorizontalSpeed>" + simulatedBallHorizontalSpeed + "</simulatedBallHorizontalSpeed>";
		empty = empty + "<simulatedBallProjectileHeight>" + simulatedBallProjectileHeight + "</simulatedBallProjectileHeight>";
		empty = empty + "<simulatedBallSpotLength>" + simulatedBallSpotLength + "</simulatedBallSpotLength>";
		empty = empty + "<simulatedBallSwingValue>" + simulatedBallSwingValue + "</simulatedBallSwingValue>";
		empty = empty + "<simulatedBallSpinValue>" + simulatedBallSpinValue + "</simulatedBallSpinValue>";
		empty = empty + "<currentBowlerType>" + currentBowlerType + "</currentBowlerType>";
		empty = empty + "<currentBowlerHand>" + currentBowlerHand + "</currentBowlerHand>";
		empty = empty + "<bowlerSide>" + bowlerSide + "</bowlerSide>";
		empty = empty + "<ballScoreData>" + ballScoreData + "</ballScoreData>";
		empty = empty + "<noOfBallDataSetted>" + noOfBallDataSetted + "</noOfBallDataSetted>";
		if (CONTROLLER.canShowPartnerShip)
		{
			text = empty;
			empty = text + "<strikerScore>" + CONTROLLER.strikerPartnershipRuns + "</strikerScore>";
			text = empty;
			empty = text + "<strikerBallPlayed>" + CONTROLLER.strikerPartnershipBall + "</strikerBallPlayed>";
			text = empty;
			empty = text + "<runnerScore>" + CONTROLLER.NonstrikerPartnershipRuns + "</runnerScore>";
			text = empty;
			empty = text + "<runnerBallPlayed>" + CONTROLLER.NonstrikerPartnershipBall + "</runnerBallPlayed>";
		}
		empty += "</controller>";
		empty += "<gamemodel>";
		text = empty;
		empty = text + "<currentBall>" + currentBall + "</currentBall>";
		text = empty;
		empty = text + "<runsScoredInOver>" + runsScoredInOver + "</runsScoredInOver>";
		text = empty;
		empty = text + "<WicketsInOver>" + WicketsInOver + "</WicketsInOver>";
		text = empty;
		empty = text + "<maxRunsReached>" + maxRunsReached + "</maxRunsReached>";
		text = empty;
		empty = text + "<maxWidesReached>" + maxWidesReached + "</maxWidesReached>";
		text = empty;
		empty = text + "<currentPartnership>" + currentPartnership + "</currentPartnership>";
		text = empty;
		empty = text + "<fourInOver>" + fourInOver + "</fourInOver>";
		text = empty;
		empty = text + "<sixInOver>" + sixInOver + "</sixInOver>";
		text = empty;
		empty = text + "<maxSixes>" + maxSixes + "</maxSixes>";
		empty += "</gamemodel>";
		empty += "<teams>";
		empty += "<myteam>";
		text = empty;
		empty = text + "<currentMatchScores>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores + "</currentMatchScores>";
		text = empty;
		empty = text + "<currentMatchBalls>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchBalls + "</currentMatchBalls>";
		text = empty;
		empty = text + "<currentMatchWickets>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets + "</currentMatchWickets>";
		text = empty;
		empty = text + "<currentMatchExtras>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchExtras + "</currentMatchExtras>";
		text = empty;
		empty = text + "<currentMatchLbs>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchLbs + "</currentMatchLbs>";
		text = empty;
		empty = text + "<currentMatchByes>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchbyes + "</currentMatchByes>";
		text = empty;
		empty = text + "<currentMatchNoBall>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchNoball + "</currentMatchNoBall>";
		text = empty;
		empty = text + "<currentMatchWide>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWideBall + "</currentMatchWide>";
		text = empty;
		empty = text + "<myteamDRS>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].noofDRSLeft + "</myteamDRS>";
		if (CONTROLLER.PlayModeSelected == 7)
		{
			text = empty;
			empty = text + "<currentMatchWickets1>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].TMcurrentMatchWickets1 + "</currentMatchWickets1>";
			text = empty;
			empty = text + "<currentMatchWickets2>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].TMcurrentMatchWickets2 + "</currentMatchWickets2>";
			text = empty;
			empty = text + "<currentMatchScores1>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].TMcurrentMatchScores1 + "</currentMatchScores1>";
			text = empty;
			empty = text + "<currentMatchScores2>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].TMcurrentMatchScores2 + "</currentMatchScores2>";
			text = empty;
			empty = text + "<currentMatchBalls1>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].TMcurrentMatchBalls1 + "</currentMatchBalls1>";
			text = empty;
			empty = text + "<currentMatchBalls2>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].TMcurrentMatchBalls2 + "</currentMatchBalls2>";
			text = empty;
			empty = text + "<isDeclared1>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].isDeclared1 + "</isDeclared1>";
			text = empty;
			empty = text + "<isDeclared2>" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].isDeclared2 + "</isDeclared2>";
		}
		empty += "</myteam>";
		empty += "<oppteam>";
		text = empty;
		empty = text + "<currentMatchScores>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores + "</currentMatchScores>";
		text = empty;
		empty = text + "<currentMatchBalls>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchBalls + "</currentMatchBalls>";
		text = empty;
		empty = text + "<currentMatchWickets>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchWickets + "</currentMatchWickets>";
		text = empty;
		empty = text + "<currentMatchExtras>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchExtras + "</currentMatchExtras>";
		text = empty;
		empty = text + "<currentMatchLbs>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchLbs + "</currentMatchLbs>";
		text = empty;
		empty = text + "<currentMatchByes>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchbyes + "</currentMatchByes>";
		text = empty;
		empty = text + "<currentMatchNoBall>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchNoball + "</currentMatchNoBall>";
		text = empty;
		empty = text + "<currentMatchWide>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchWideBall + "</currentMatchWide>";
		text = empty;
		empty = text + "<oppteamDRS>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].noofDRSLeft + "</oppteamDRS>";
		if (CONTROLLER.PlayModeSelected == 7)
		{
			text = empty;
			empty = text + "<currentMatchWickets1>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].TMcurrentMatchWickets1 + "</currentMatchWickets1>";
			text = empty;
			empty = text + "<currentMatchWickets2>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].TMcurrentMatchWickets2 + "</currentMatchWickets2>";
			text = empty;
			empty = text + "<currentMatchScores1>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].TMcurrentMatchScores1 + "</currentMatchScores1>";
			text = empty;
			empty = text + "<currentMatchScores2>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].TMcurrentMatchScores2 + "</currentMatchScores2>";
			text = empty;
			empty = text + "<currentMatchBalls1>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].TMcurrentMatchBalls1 + "</currentMatchBalls1>";
			text = empty;
			empty = text + "<currentMatchBalls2>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].TMcurrentMatchBalls2 + "</currentMatchBalls2>";
			text = empty;
			empty = text + "<isDeclared1>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].isDeclared1 + "</isDeclared1>";
			text = empty;
			empty = text + "<isDeclared2>" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].isDeclared2 + "</isDeclared2>";
		}
		empty += "</oppteam>";
		empty += "</teams>";
		empty += "<players>";
		empty += "<myteam>";
		for (int i = 0; i < CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList.Length; i++)
		{
			empty += "<player";
			empty = empty + " name=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].PlayerName + "\"";
			empty = empty + " battingHand=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.BattingHand + "\"";
			empty = empty + " bowlingHand=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.BowlingHand + "\"";
			empty = empty + " bowlingStyle=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.Style + "\"";
			empty = empty + " bowlingRank=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.bowlingRank + "\"";
			if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].KeeperIndex == i)
			{
				text = empty;
				empty = text + " isKeeper=\"" + 1 + "\"";
			}
			if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].CaptainIndex == i)
			{
				text = empty;
				empty = text + " isCaptain=\"" + 1 + "\"";
			}
			text = empty;
			empty = text + " ConfidenceVal=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].ConfidenceVal + "\"";
			text = empty;
			empty = text + " LoftMeterFill=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].LoftMeterFillVal + "\"";
			text = empty;
			empty = text + " reachedHalfCentury=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].reachedHalfCentury + "\"";
			text = empty;
			empty = text + " reachedCentury=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].reachedCentury + "\"";
			empty = empty + " Status=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Status + "\"";
			text = empty;
			empty = text + " RunsScored=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.RunsScored + "\"";
			text = empty;
			empty = text + " BallsPlayed=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.BallsPlayed + "\"";
			text = empty;
			empty = text + " Fours=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Fours + "\"";
			text = empty;
			empty = text + " Sixes=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Sixes + "\"";
			text = empty;
			empty = text + " FOW=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.FOW + "\"";
			text = empty;
			empty = text + " RunsGiven=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.RunsGiven + "\"";
			text = empty;
			empty = text + " BallsBowled=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.BallsBowled + "\"";
			text = empty;
			empty = text + " Maiden=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.Maiden + "\"";
			text = empty;
			empty = text + " Wicket=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.Wicket + "\"";
			text = empty;
			empty = text + " maxBallInMatch=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.maxBallInMatch + "\"";
			text = empty;
			empty = text + " WicketsInBallCount=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.WicketsInBallCount + "\"";
			if (CONTROLLER.PlayModeSelected == 7)
			{
				empty = empty + " Status1=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMStatus1 + "\"";
				empty = empty + " Status2=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMStatus2 + "\"";
				text = empty;
				empty = text + " RunsScored1=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMRunsScored1 + "\"";
				text = empty;
				empty = text + " RunsScored2=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMRunsScored2 + "\"";
				text = empty;
				empty = text + " BallsPlayed1=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMBallsPlayed1 + "\"";
				text = empty;
				empty = text + " BallsPlayed2=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMBallsPlayed2 + "\"";
				text = empty;
				empty = text + " Fours1=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMFours1 + "\"";
				text = empty;
				empty = text + " Fours2=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMFours2 + "\"";
				text = empty;
				empty = text + " Sixes1=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMSixes1 + "\"";
				text = empty;
				empty = text + " Sixes2=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMSixes2 + "\"";
				text = empty;
				empty = text + " FOW1=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMFOW1 + "\"";
				text = empty;
				empty = text + " FOW2=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMFOW2 + "\"";
				text = empty;
				empty = text + " TMreachedHalfCentury1=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].TMreachedHalfCentury1 + "\"";
				text = empty;
				empty = text + " TMreachedCentury1=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].TMreachedCentury1 + "\"";
				text = empty;
				empty = text + " TMreachedHalfCentury2=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].TMreachedHalfCentury2 + "\"";
				text = empty;
				empty = text + " TMreachedCentury2=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].TMreachedCentury2 + "\"";
				text = empty;
				empty = text + " RunsGiven1=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMRunsGiven1 + "\"";
				text = empty;
				empty = text + " BallsBowled1=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMBallsBowled1 + "\"";
				text = empty;
				empty = text + " Wicket1=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMWicket1 + "\"";
				text = empty;
				empty = text + " RunsGiven2=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMRunsGiven2 + "\"";
				text = empty;
				empty = text + " BallsBowled2=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMBallsBowled2 + "\"";
				text = empty;
				empty = text + " Wicket2=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMWicket2 + "\"";
				text = empty;
				empty = text + " Maiden1=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMMaiden1 + "\"";
				text = empty;
				empty = text + " Maiden2=\"" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMMaiden2 + "\"";
			}
			empty += " />";
		}
		empty += "</myteam>";
		empty += "<oppteam>";
		for (int j = 0; j < CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList.Length; j++)
		{
			empty += "<player";
			empty = empty + " name=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].PlayerName + "\"";
			empty = empty + " battingHand=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.BattingHand + "\"";
			empty = empty + " bowlingHand=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.BowlingHand + "\"";
			empty = empty + " bowlingStyle=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.Style + "\"";
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].KeeperIndex == j)
			{
				text = empty;
				empty = text + " isKeeper=\"" + 1 + "\"";
			}
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].CaptainIndex == j)
			{
				text = empty;
				empty = text + " isCaptain=\"" + 1 + "\"";
			}
			text = empty;
			empty = text + " ConfidenceVal=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].ConfidenceVal + "\"";
			text = empty;
			empty = text + " LoftMeterFill=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].LoftMeterFillVal + "\"";
			text = empty;
			empty = text + " reachedHalfCentury=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].reachedHalfCentury + "\"";
			text = empty;
			empty = text + " reachedCentury=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].reachedCentury + "\"";
			empty = empty + " Status=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.Status + "\"";
			text = empty;
			empty = text + " RunsScored=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.RunsScored + "\"";
			text = empty;
			empty = text + " BallsPlayed=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.BallsPlayed + "\"";
			text = empty;
			empty = text + " Fours=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.Fours + "\"";
			text = empty;
			empty = text + " Sixes=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.Sixes + "\"";
			text = empty;
			empty = text + " FOW=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.FOW + "\"";
			text = empty;
			empty = text + " RunsGiven=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.RunsGiven + "\"";
			text = empty;
			empty = text + " BallsBowled=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.BallsBowled + "\"";
			text = empty;
			empty = text + " Maiden=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.Maiden + "\"";
			text = empty;
			empty = text + " Wicket=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.Wicket + "\"";
			text = empty;
			empty = text + " maxBallInMatch=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.maxBallInMatch + "\"";
			text = empty;
			empty = text + " WicketsInBallCount=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.WicketsInBallCount + "\"";
			if (CONTROLLER.PlayModeSelected == 7)
			{
				empty = empty + " Status1=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMStatus1 + "\"";
				empty = empty + " Status2=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMStatus2 + "\"";
				text = empty;
				empty = text + " RunsScored1=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMRunsScored1 + "\"";
				text = empty;
				empty = text + " RunsScored2=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMRunsScored2 + "\"";
				text = empty;
				empty = text + " BallsPlayed1=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMBallsPlayed1 + "\"";
				text = empty;
				empty = text + " BallsPlayed2=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMBallsPlayed2 + "\"";
				text = empty;
				empty = text + " Fours1=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMFours1 + "\"";
				text = empty;
				empty = text + " Fours2=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMFours2 + "\"";
				text = empty;
				empty = text + " Sixes1=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMSixes1 + "\"";
				text = empty;
				empty = text + " Sixes2=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMSixes2 + "\"";
				text = empty;
				empty = text + " FOW1=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMFOW1 + "\"";
				text = empty;
				empty = text + " FOW2=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMFOW2 + "\"";
				text = empty;
				empty = text + " TMreachedHalfCentury1=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].TMreachedHalfCentury1 + "\"";
				text = empty;
				empty = text + " TMreachedCentury1=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].TMreachedCentury1 + "\"";
				text = empty;
				empty = text + " TMreachedHalfCentury2=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].TMreachedHalfCentury2 + "\"";
				text = empty;
				empty = text + " TMreachedCentury2=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].TMreachedCentury2 + "\"";
				text = empty;
				empty = text + " RunsGiven1=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMRunsGiven1 + "\"";
				text = empty;
				empty = text + " BallsBowled1=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMBallsBowled1 + "\"";
				text = empty;
				empty = text + " Wicket1=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMWicket1 + "\"";
				text = empty;
				empty = text + " RunsGiven2=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMRunsGiven2 + "\"";
				text = empty;
				empty = text + " BallsBowled2=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMBallsBowled2 + "\"";
				text = empty;
				empty = text + " Wicket2=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMWicket2 + "\"";
				text = empty;
				empty = text + " Maiden1=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMMaiden1 + "\"";
				text = empty;
				empty = text + " Maiden2=\"" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMMaiden2 + "\"";
			}
			empty += " />";
		}
		empty += "</oppteam>";
		empty += "</players>";
		empty += "<Game>";
		text = empty;
		empty = text + "<FreeHit>" + CONTROLLER.isFreeHitBall + "</FreeHit>";
		text = empty;
		empty = text + "<BIndex>" + CONTROLLER.noBallFacedBatsmanId + "</BIndex>";
		empty += "</Game>";
		if (CONTROLLER.PlayModeSelected == 4 && CONTROLLER.SuperOverMode == "bat")
		{
			empty += "<SuperOverBatting>";
			text = empty;
			empty = text + "<SOtotFour>" + CONTROLLER.totalFours + "</SOtotFour>";
			text = empty;
			empty = text + "<SOtotSix>" + CONTROLLER.totalSixes + "</SOtotSix>";
			text = empty;
			empty = text + "<SOtotContFour>" + CONTROLLER.continousBoundaries + "</SOtotContFour>";
			text = empty;
			empty = text + "<SOtotContSix>" + CONTROLLER.continousSixes + "</SOtotContSix>";
			text = empty;
			empty = text + "<SOLevID>" + CONTROLLER.LevelId + "</SOLevID>";
			empty += "</SuperOverBatting>";
		}
		if (CONTROLLER.PlayModeSelected == 5)
		{
			empty += "<SuperChase>";
			text = empty;
			empty = text + "<SCTotalOvers>" + CONTROLLER.totalOvers + "</SCTotalOvers>";
			text = empty;
			empty = text + "<SCTarget>" + CONTROLLER.TargetToChase + "</SCTarget>";
			text = empty;
			empty = text + "<SCLevelID>" + CONTROLLER.CTLevelId + "</SCLevelID>";
			text = empty;
			empty = text + "<SCSubLevelID>" + CONTROLLER.CTSubLevelCompleted + "</SCSubLevelID>";
			empty += "</SuperChase>";
		}
		if (CONTROLLER.PlayModeSelected == 7)
		{
			empty += "<TestMatch>";
			text = empty;
			empty = text + "<ballsBowledPerDay>" + CONTROLLER.ballsBowledPerDay + "</ballsBowledPerDay>";
			for (int k = 0; k <= CONTROLLER.currentInnings; k++)
			{
				text = empty;
				empty = text + "<TMBatting" + k + ">" + CONTROLLER.BattingTeam[k] + "</TMBatting" + k + ">";
				text = empty;
				empty = text + "<TMBowling" + k + ">" + CONTROLLER.BowlingTeam[k] + "</TMBowling" + k + ">";
			}
			text = empty;
			empty = text + "<currentSession>" + CONTROLLER.currentSession + "</currentSession>";
			text = empty;
			empty = text + "<currentDay>" + CONTROLLER.currentDay + "</currentDay>";
			text = empty;
			empty = text + "<AIFirstInningsDeclareRuns>" + CONTROLLER.AIFirstInningsDeclareRuns + "</AIFirstInningsDeclareRuns>";
			text = empty;
			empty = text + "<AIFirstInningsDeclareBalls>" + CONTROLLER.AIFirstInningsDeclareBalls + "</AIFirstInningsDeclareBalls>";
			text = empty;
			empty = text + "<AiFirstInngsLeadDeclareRuns>" + CONTROLLER.AiFirstInngsLeadDeclareRuns + "</AiFirstInngsLeadDeclareRuns>";
			text = empty;
			empty = text + "<AiTargetDeclareRuns>" + CONTROLLER.AiTargetDeclareRuns + "</AiTargetDeclareRuns>";
			text = empty;
			empty = text + "<totalWickets>" + CONTROLLER.totalWickets + "</totalWickets>";
			text = empty;
			empty = text + "<TMTotalOvers>" + CONTROLLER.totalOvers + "</TMTotalOvers>";
			text = empty;
			empty = text + "<followOnTarget>" + CONTROLLER.followOnTarget + "</followOnTarget>";
			empty += "</TestMatch>";
		}
		empty += "</SavedDetail>";
		WriteFile(empty);
	}

	private static void ParseSavedMatch(string result)
	{
		if (CONTROLLER.PlayModeSelected == 6)
		{
			return;
		}
		result = ObscuredPrefs.GetString(GenerateFileName());
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(result);
		XMLNodeList xMLNodeList = (XMLNodeList)xMLNode["SavedDetail"];
		XMLNode xMLNode2 = (XMLNode)xMLNodeList[0];
		XMLNodeList xMLNodeList2 = (XMLNodeList)xMLNode2["controller"];
		XMLNode xMLNode3 = (XMLNode)xMLNodeList2[0];
		XMLNodeList xMLNodeList3 = (XMLNodeList)xMLNode3["currentPageIndex"];
		XMLNode xMLNode4 = (XMLNode)xMLNodeList3[0];
		string s = xMLNode4["_text"] as string;
		CONTROLLER.currentPageIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["myTeamIndex"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.myTeamIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["opponentTeamIndex"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.opponentTeamIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["oversSelectedIndex"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.oversSelectedIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["MyTeamStateIndex"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.MyTeamStateIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["OppTeamStateIndex"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.OppTeamStateIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["meFirstBatting"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.meFirstBatting = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["BattingTeamIndex"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.BattingTeamIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["BowlingTeamIndex"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.BowlingTeamIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["StrikerIndex"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.StrikerIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["NonStrikerIndex"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.NonStrikerIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["CurrentBowlerIndex"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.CurrentBowlerIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["wickerKeeperIndex"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.wickerKeeperIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["difficultyMode"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (CONTROLLER.difficultyMode = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["ConfidenceVal"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.ConfidenceVal = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["fielderChangeIndex"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.fielderChangeIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["computerFielderChangeIndex"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.computerFielderChangeIndex = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["HattrickBall"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.HattrickBall = bool.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["currentInnings"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.currentInnings = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["RunsInAOver"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.RunsInAOver = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["WideInAOver"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.WideInAOver = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["DotInAOver"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.DotInAOver = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["NewOver"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.NewOver = bool.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["InBetweenOvers"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.InBetweenOvers = bool.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["TournamentSixes"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.TournamentSixes = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["TournamentStage"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		CONTROLLER.TournamentStage = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["BowlerSide"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (CONTROLLER.BowlerSide = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["ballUpdate"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (ballUpdate = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["ballListInfo"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (ballListInfo = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["ballExtras"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (ballextras = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["simulatedBallAngle"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (simulatedBallAngle = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["simulatedBallHorizontalSpeed"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (simulatedBallHorizontalSpeed = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["simulatedBallSpotLength"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (simulatedBallSpotLength = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["simulatedBallProjectileHeight"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (simulatedBallProjectileHeight = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["simulatedBallSpinValue"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (simulatedBallSpinValue = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["simulatedBallSwingValue"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (simulatedBallSwingValue = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["currentBowlerType"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (currentBowlerType = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["ballScoreData"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (ballScoreData = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["currentBowlerHand"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (currentBowlerHand = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["bowlerSide"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (bowlerSide = xMLNode4["_text"] as string);
		xMLNodeList3 = (XMLNodeList)xMLNode3["noOfBallDataSetted"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = (noOfBallDataSetted = xMLNode4["_text"] as string);
		if (xMLNode3.ContainsKey("strikerScore"))
		{
			CONTROLLER.canShowPartnerShip = true;
			xMLNodeList3 = (XMLNodeList)xMLNode3["strikerScore"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.strikerPartnershipRuns = int.Parse(s);
		}
		else
		{
			CONTROLLER.canShowPartnerShip = false;
		}
		if (xMLNode3.ContainsKey("strikerBallPlayed"))
		{
			CONTROLLER.canShowPartnerShip = true;
			xMLNodeList3 = (XMLNodeList)xMLNode3["strikerBallPlayed"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.strikerPartnershipBall = int.Parse(s);
		}
		else
		{
			CONTROLLER.canShowPartnerShip = false;
		}
		if (xMLNode3.ContainsKey("runnerScore"))
		{
			CONTROLLER.canShowPartnerShip = true;
			xMLNodeList3 = (XMLNodeList)xMLNode3["runnerScore"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.NonstrikerPartnershipRuns = int.Parse(s);
		}
		else
		{
			CONTROLLER.canShowPartnerShip = false;
		}
		if (xMLNode3.ContainsKey("runnerBallPlayed"))
		{
			CONTROLLER.canShowPartnerShip = true;
			xMLNodeList3 = (XMLNodeList)xMLNode3["runnerBallPlayed"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.NonstrikerPartnershipBall = int.Parse(s);
		}
		else
		{
			CONTROLLER.canShowPartnerShip = false;
		}
		xMLNodeList2 = (XMLNodeList)xMLNode2["gamemodel"];
		xMLNode3 = (XMLNode)xMLNodeList2[0];
		xMLNodeList3 = (XMLNodeList)xMLNode3["currentBall"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		currentBall = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["runsScoredInOver"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		runsScoredInOver = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["WicketsInOver"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		WicketsInOver = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["maxRunsReached"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		maxRunsReached = bool.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["maxWidesReached"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		maxWidesReached = bool.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["currentPartnership"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		currentPartnership = int.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["fourInOver"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		fourInOver = bool.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["sixInOver"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		sixInOver = bool.Parse(s);
		xMLNodeList3 = (XMLNodeList)xMLNode3["maxSixes"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		s = xMLNode4["_text"] as string;
		maxSixes = bool.Parse(s);
		xMLNodeList2 = (XMLNodeList)xMLNode2["teams"];
		xMLNode3 = (XMLNode)xMLNodeList2[0];
		xMLNodeList3 = (XMLNodeList)xMLNode3["myteam"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		XMLNodeList xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchScores"];
		XMLNode xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores = int.Parse(s);
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchBalls"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchBalls = int.Parse(s);
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchWickets"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets = int.Parse(s);
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchExtras"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchExtras = int.Parse(s);
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchLbs"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchLbs = int.Parse(s);
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchByes"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchbyes = int.Parse(s);
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchWide"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWideBall = int.Parse(s);
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchNoBall"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchNoball = int.Parse(s);
		if (xMLNode4.ContainsKey("myteamDRS"))
		{
			xMLNodeList4 = (XMLNodeList)xMLNode4["myteamDRS"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].noofDRSLeft = int.Parse(s);
		}
		if (CONTROLLER.PlayModeSelected == 7)
		{
			xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchWickets1"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].TMcurrentMatchWickets1 = int.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchWickets2"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].TMcurrentMatchWickets2 = int.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchScores1"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].TMcurrentMatchScores1 = int.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchScores2"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].TMcurrentMatchScores2 = int.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchBalls1"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].TMcurrentMatchBalls1 = int.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchBalls2"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].TMcurrentMatchBalls2 = int.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["isDeclared1"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].isDeclared1 = bool.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["isDeclared2"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].isDeclared2 = bool.Parse(s);
		}
		xMLNodeList3 = (XMLNodeList)xMLNode3["oppteam"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchScores"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores = int.Parse(s);
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchBalls"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchBalls = int.Parse(s);
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchWickets"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchWickets = int.Parse(s);
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchExtras"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchExtras = int.Parse(s);
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchLbs"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchLbs = int.Parse(s);
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchByes"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchbyes = int.Parse(s);
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchWide"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchWideBall = int.Parse(s);
		if (xMLNode4.ContainsKey("oppteamDRS"))
		{
			xMLNodeList4 = (XMLNodeList)xMLNode4["oppteamDRS"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].noofDRSLeft = int.Parse(s);
		}
		if (CONTROLLER.PlayModeSelected == 7)
		{
			xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchWickets1"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].TMcurrentMatchWickets1 = int.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchWickets2"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].TMcurrentMatchWickets2 = int.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchScores1"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].TMcurrentMatchScores1 = int.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchScores2"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].TMcurrentMatchScores2 = int.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchBalls1"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].TMcurrentMatchBalls1 = int.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchBalls2"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].TMcurrentMatchBalls2 = int.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["isDeclared1"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].isDeclared1 = bool.Parse(s);
			xMLNodeList4 = (XMLNodeList)xMLNode4["isDeclared2"];
			xMLNode5 = (XMLNode)xMLNodeList4[0];
			s = xMLNode5["_text"] as string;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].isDeclared2 = bool.Parse(s);
		}
		xMLNodeList4 = (XMLNodeList)xMLNode4["currentMatchNoBall"];
		xMLNode5 = (XMLNode)xMLNodeList4[0];
		s = xMLNode5["_text"] as string;
		CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchNoball = int.Parse(s);
		xMLNodeList2 = (XMLNodeList)xMLNode2["players"];
		xMLNode3 = (XMLNode)xMLNodeList2[0];
		xMLNodeList3 = (XMLNodeList)xMLNode3["myteam"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		xMLNodeList4 = (XMLNodeList)xMLNode4["player"];
		for (int i = 0; i < xMLNodeList4.Count; i++)
		{
			xMLNode5 = (XMLNode)xMLNodeList4[i];
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].PlayerName = xMLNode5["@name"] as string;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.BattingHand = xMLNode5["@battingHand"] as string;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.BowlingHand = xMLNode5["@bowlingHand"] as string;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.bowlingRank = xMLNode5["@bowlingRank"] as string;
			if (!xMLNode5.ContainsKey("@isKeeper"))
			{
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].isKeeper = false;
			}
			else
			{
				string text = xMLNode5["@isKeeper"] as string;
				if (text == "1")
				{
					CONTROLLER.TeamList[CONTROLLER.myTeamIndex].KeeperIndex = i;
					CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].isKeeper = true;
				}
			}
			if (!xMLNode5.ContainsKey("@isCaptain"))
			{
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].isCaptain = false;
			}
			else
			{
				string text2 = xMLNode5["@isCaptain"] as string;
				if (text2 == "1")
				{
					CONTROLLER.TeamList[CONTROLLER.myTeamIndex].CaptainIndex = i;
					CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].isCaptain = true;
				}
			}
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].ConfidenceVal = float.Parse(xMLNode5["@ConfidenceVal"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].LoftMeterFillVal = float.Parse(xMLNode5["@LoftMeterFill"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].reachedHalfCentury = bool.Parse(xMLNode5["@reachedHalfCentury"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].reachedCentury = bool.Parse(xMLNode5["@reachedCentury"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Status = xMLNode5["@Status"] as string;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.RunsScored = int.Parse(xMLNode5["@RunsScored"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.BallsPlayed = int.Parse(xMLNode5["@BallsPlayed"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Fours = int.Parse(xMLNode5["@Fours"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Sixes = int.Parse(xMLNode5["@Sixes"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.FOW = int.Parse(xMLNode5["@FOW"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.RunsGiven = int.Parse(xMLNode5["@RunsGiven"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.BallsBowled = int.Parse(xMLNode5["@BallsBowled"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.Maiden = int.Parse(xMLNode5["@Maiden"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.Wicket = int.Parse(xMLNode5["@Wicket"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.maxBallInMatch = int.Parse(xMLNode5["@maxBallInMatch"] as string);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.WicketsInBallCount = int.Parse(xMLNode5["@WicketsInBallCount"] as string);
			if (CONTROLLER.PlayModeSelected == 7)
			{
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMStatus1 = xMLNode5["@Status1"] as string;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMStatus2 = xMLNode5["@Status2"] as string;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMRunsScored1 = int.Parse(xMLNode5["@RunsScored1"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMBallsPlayed1 = int.Parse(xMLNode5["@BallsPlayed1"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMRunsScored2 = int.Parse(xMLNode5["@RunsScored2"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMBallsPlayed2 = int.Parse(xMLNode5["@BallsPlayed2"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMFours1 = int.Parse(xMLNode5["@Fours1"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMFours2 = int.Parse(xMLNode5["@Fours2"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMSixes1 = int.Parse(xMLNode5["@Sixes1"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMSixes2 = int.Parse(xMLNode5["@Sixes2"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMFOW1 = int.Parse(xMLNode5["@FOW1"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.TMFOW2 = int.Parse(xMLNode5["@FOW2"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].TMreachedCentury1 = bool.Parse(xMLNode5["@TMreachedCentury1"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].TMreachedHalfCentury1 = bool.Parse(xMLNode5["@TMreachedHalfCentury1"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].TMreachedCentury2 = bool.Parse(xMLNode5["@TMreachedCentury2"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].TMreachedHalfCentury2 = bool.Parse(xMLNode5["@TMreachedHalfCentury2"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMRunsGiven1 = int.Parse(xMLNode5["@RunsGiven1"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMBallsBowled1 = int.Parse(xMLNode5["@BallsBowled1"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMWicket1 = int.Parse(xMLNode5["@Wicket1"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMRunsGiven2 = int.Parse(xMLNode5["@RunsGiven2"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMBallsBowled2 = int.Parse(xMLNode5["@BallsBowled2"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMWicket2 = int.Parse(xMLNode5["@Wicket2"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMMaiden1 = int.Parse(xMLNode5["@Maiden1"] as string);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMMaiden2 = int.Parse(xMLNode5["@Maiden2"] as string);
			}
		}
		xMLNodeList3 = (XMLNodeList)xMLNode3["oppteam"];
		xMLNode4 = (XMLNode)xMLNodeList3[0];
		xMLNodeList4 = (XMLNodeList)xMLNode4["player"];
		for (int j = 0; j < xMLNodeList4.Count; j++)
		{
			xMLNode5 = (XMLNode)xMLNodeList4[j];
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].PlayerName = xMLNode5["@name"] as string;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.BattingHand = xMLNode5["@battingHand"] as string;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.BowlingHand = xMLNode5["@bowlingHand"] as string;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.Style = xMLNode5["@bowlingStyle"] as string;
			if (!xMLNode5.ContainsKey("@isKeeper"))
			{
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[j].isKeeper = false;
			}
			else
			{
				string text3 = xMLNode5["@isKeeper"] as string;
				if (text3 == "1")
				{
					CONTROLLER.TeamList[CONTROLLER.myTeamIndex].KeeperIndex = j;
					CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[j].isKeeper = true;
				}
			}
			if (!xMLNode5.ContainsKey("@isCaptain"))
			{
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[j].isCaptain = false;
			}
			else
			{
				string text4 = xMLNode5["@isCaptain"] as string;
				if (text4 == "1")
				{
					CONTROLLER.TeamList[CONTROLLER.myTeamIndex].CaptainIndex = j;
					CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[j].isCaptain = true;
				}
			}
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].ConfidenceVal = float.Parse(xMLNode5["@ConfidenceVal"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].LoftMeterFillVal = float.Parse(xMLNode5["@LoftMeterFill"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].reachedHalfCentury = bool.Parse(xMLNode5["@reachedHalfCentury"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].reachedCentury = bool.Parse(xMLNode5["@reachedCentury"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.Status = xMLNode5["@Status"] as string;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.RunsScored = int.Parse(xMLNode5["@RunsScored"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.BallsPlayed = int.Parse(xMLNode5["@BallsPlayed"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.Fours = int.Parse(xMLNode5["@Fours"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.Sixes = int.Parse(xMLNode5["@Sixes"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.FOW = int.Parse(xMLNode5["@FOW"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.RunsGiven = int.Parse(xMLNode5["@RunsGiven"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.BallsBowled = int.Parse(xMLNode5["@BallsBowled"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.Maiden = int.Parse(xMLNode5["@Maiden"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.Wicket = int.Parse(xMLNode5["@Wicket"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.maxBallInMatch = int.Parse(xMLNode5["@maxBallInMatch"] as string);
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.WicketsInBallCount = int.Parse(xMLNode5["@WicketsInBallCount"] as string);
			if (CONTROLLER.PlayModeSelected == 7)
			{
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMStatus1 = xMLNode5["@Status1"] as string;
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMStatus2 = xMLNode5["@Status2"] as string;
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMRunsScored1 = int.Parse(xMLNode5["@RunsScored1"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMBallsPlayed1 = int.Parse(xMLNode5["@BallsPlayed1"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMRunsScored2 = int.Parse(xMLNode5["@RunsScored2"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMBallsPlayed2 = int.Parse(xMLNode5["@BallsPlayed2"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMFours1 = int.Parse(xMLNode5["@Fours1"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMFours2 = int.Parse(xMLNode5["@Fours2"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMSixes1 = int.Parse(xMLNode5["@Sixes1"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMSixes2 = int.Parse(xMLNode5["@Sixes2"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMFOW1 = int.Parse(xMLNode5["@FOW1"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMFOW2 = int.Parse(xMLNode5["@FOW2"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].TMreachedCentury1 = bool.Parse(xMLNode5["@TMreachedCentury1"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].TMreachedHalfCentury1 = bool.Parse(xMLNode5["@TMreachedHalfCentury1"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].TMreachedCentury2 = bool.Parse(xMLNode5["@TMreachedCentury2"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].TMreachedHalfCentury2 = bool.Parse(xMLNode5["@TMreachedHalfCentury2"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMRunsGiven1 = int.Parse(xMLNode5["@RunsGiven1"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMBallsBowled1 = int.Parse(xMLNode5["@BallsBowled1"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMWicket1 = int.Parse(xMLNode5["@Wicket1"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMRunsGiven2 = int.Parse(xMLNode5["@RunsGiven2"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMBallsBowled2 = int.Parse(xMLNode5["@BallsBowled2"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMWicket2 = int.Parse(xMLNode5["@Wicket2"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMMaiden1 = int.Parse(xMLNode5["@Maiden1"] as string);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMMaiden2 = int.Parse(xMLNode5["@Maiden2"] as string);
			}
		}
		if (xMLNode2.ContainsKey("Game"))
		{
			xMLNodeList2 = (XMLNodeList)xMLNode2["Game"];
			xMLNode3 = (XMLNode)xMLNodeList2[0];
			xMLNodeList3 = (XMLNodeList)xMLNode3["FreeHit"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.isFreeHitBall = bool.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["BIndex"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.noBallFacedBatsmanId = int.Parse(s);
		}
		if (CONTROLLER.PlayModeSelected == 4 && CONTROLLER.SuperOverMode == "bat" && xMLNode2.ContainsKey("SuperOverBatting"))
		{
			xMLNodeList2 = (XMLNodeList)xMLNode2["SuperOverBatting"];
			xMLNode3 = (XMLNode)xMLNodeList2[0];
			xMLNodeList3 = (XMLNodeList)xMLNode3["SOtotFour"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.totalFours = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["SOtotSix"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.totalSixes = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["SOtotContFour"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.continousBoundaries = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["SOtotContSix"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.continousSixes = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["SOLevID"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.LevelId = int.Parse(s);
		}
		if (CONTROLLER.PlayModeSelected == 5 && xMLNode2.ContainsKey("SuperChase"))
		{
			xMLNodeList2 = (XMLNodeList)xMLNode2["SuperChase"];
			xMLNode3 = (XMLNode)xMLNodeList2[0];
			xMLNodeList3 = (XMLNodeList)xMLNode3["SCTotalOvers"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.totalOvers = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["SCTarget"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.TargetToChase = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["SCLevelID"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.CTLevelId = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["SCSubLevelID"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.CTSubLevelCompleted = int.Parse(s);
		}
		if (CONTROLLER.PlayModeSelected == 7 && xMLNode2.ContainsKey("TestMatch"))
		{
			xMLNodeList2 = (XMLNodeList)xMLNode2["TestMatch"];
			xMLNode3 = (XMLNode)xMLNodeList2[0];
			xMLNodeList3 = (XMLNodeList)xMLNode3["ballsBowledPerDay"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.ballsBowledPerDay = int.Parse(s);
			for (int k = 0; k <= CONTROLLER.currentInnings; k++)
			{
				xMLNodeList3 = (XMLNodeList)xMLNode3["TMBatting" + k];
				xMLNode4 = (XMLNode)xMLNodeList3[0];
				s = xMLNode4["_text"] as string;
				CONTROLLER.BattingTeam[k] = int.Parse(s);
				xMLNodeList3 = (XMLNodeList)xMLNode3["TMBowling" + k];
				xMLNode4 = (XMLNode)xMLNodeList3[0];
				s = xMLNode4["_text"] as string;
				CONTROLLER.BowlingTeam[k] = int.Parse(s);
			}
			xMLNodeList3 = (XMLNodeList)xMLNode3["currentSession"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.currentSession = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["currentDay"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.currentDay = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["AIFirstInningsDeclareRuns"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.AIFirstInningsDeclareRuns = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["AIFirstInningsDeclareBalls"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.AIFirstInningsDeclareBalls = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["AiFirstInngsLeadDeclareRuns"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.AiFirstInngsLeadDeclareRuns = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["AiTargetDeclareRuns"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.AiTargetDeclareRuns = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["totalWickets"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.totalWickets = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["TMTotalOvers"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.totalOvers = int.Parse(s);
			xMLNodeList3 = (XMLNodeList)xMLNode3["followOnTarget"];
			xMLNode4 = (XMLNode)xMLNodeList3[0];
			s = xMLNode4["_text"] as string;
			CONTROLLER.followOnTarget = int.Parse(s);
		}
	}
}
