using UnityEngine;

public class CONTROLLER
{
	public static string PHP_Server_Link = "https://wcclite.cricketbuddies.in/wcc_lite/unity/unity.php";

	public static int lastPlayedMode = 1;

	public static int AdsPurchased = 0;

	public static bool isGameDataSecure = true;

	public static string PopupName = string.Empty;

	public static bool fromPreloader = false;

	public static bool pushNotiClicked = false;

	public static int GetUserStatus = 0;

	public static int sixMeterCount = 0;

	public static int PushScreenNumber = 0;

	public static bool mainMenuHidden = false;

	public static int AppVersionCode = 18;

	public static string CoinsKey = "userCoins";

	public static bool IsFullTossBall = false;

	public static string TicketsKey = "userTickets";

	public static string XPKey = "userXP";

	public static string ArcadeXPKey = "arcadeXPs";

	public static int cameraType = 1;

	public static bool newUser = false;

	public static bool IsQuickPlayFree = false;

	public static bool QPDoubleRewards = false;

	public static bool powerUpgradeTimerStarted = false;

	public static bool controlUpgradeTimerStarted = false;

	public static bool agilityUpgradeTimerStarted = false;

	public static string tempPageName;

	public static string screenToDisplay = "landingPage";

	public static int userid;

	public static string pageName;

	public static string timerReason;

	public const int CURRENT_VERSION = 0;

	public const string VersionString = "VERSION: 1.0";

	public static int userID = 0;

	public static string STORE = "gameloft";

	public static bool isFreeVersion = false;

	public static int maxTrails = 5;

	public static int noOfTrails = 5;

	public static int rewardedVideoType = 0;

	public static int tickets;

	public static int weekly_Xps = 0;

	public static int weekly_EarnedXps = 0;

	public static int weekly_ArcadeXps = 0;

	public static int weekly_EarnedArcadeXps = 0;

	public static string deviceRegistrationID;

	public static bool bShowLoginFailedPopup;

	public static int lastWicketball = 0;

	public static bool isAutoplay = false;

	public static bool isFromAutoPlay = false;

	public static bool TMisFromAutoPlay = false;

	public static bool isAutoPlayed = false;

	public static int interstitialshowDuration = 12;

	public static int isGreedyGameEnabled = 1;

	public static int greedyRefreshRate = 12;

	public static int zaprEnabled = 0;

	public static int greedyFloatAdEnabled = 0;

	public static int canShowbannerGround = 0;

	public static int canShowbannerMainmenu = 0;

	public static bool isSpinReward2XOn;

	public static int SpinReward2X = 2;

	public static float Starttime_2X;

	public static bool isStarttime_FreeEntryOn;

	public static int SpinFreeEntry = 2;

	public static float Starttime_freeEntry;

	public static bool AndroidBackButtonEnable;

	public const float DefaultWidth = 800f;

	public const float DefaultHeight = 600f;

	public const int totalBallInOver = 6;

	public static bool prevPowerShot = false;

	public static string TargetPlatform = string.Empty;

	public static string gameMode = string.Empty;

	public static string matchType = "oneday";

	public static string menuTitle = string.Empty;

	public static string pageFrom = string.Empty;

	public static string tournamentStatus = "LEAGUE MATCH";

	public static string fbLikeLink = "https://www.facebook.com/pages/Allstarcricket/1547772552101087";

	public static float HeigthRatio;

	public static float WidthRatio;

	public static float MinScaleRatio;

	public static bool loadingScreenVisited = true;

	public static string tournamentStr = string.Empty;

	public static int matchIndex = 0;

	public static string quaterFinalList = string.Empty;

	public static string semiFinalList = string.Empty;

	public static string finalList = string.Empty;

	public static string ChallengeType = "bat";

	public static int ChallengeNumber = 0;

	public static string ChallengeStatus = string.Empty;

	public static int ChallengeFactor = 0;

	public static int spendArcade = 0;

	public static int earnedArcade = 0;

	public static int earnedTickets = 0;

	public static int spendTickets = 0;

	public static bool isCoinsSyncing = false;

	public static int Coins = 0;

	public static int earnedCoins = 0;

	public static int spendCoins = 0;

	public static long CoinsAwardTimeDiff = 0L;

	public static int XPs = 0;

	public static int earnedXPs = 0;

	public static int ArcadeXPs = 0;

	public static int FreeSpin = 3;

	public static bool buyFullVersion = false;

	public static int fielderChangeIndex = 1;

	public static int fielderPrevIndex = 1;

	public static int computerFielderChangeIndex = 0;

	public static int closefieldIndex = 1;

	public static int openfieldIndex = 1;

	public static int[] FieldersArray;

	public static int[] FielderArray = new int[9];

	public static SoundController sndController;

	public static int boughtPowerSubGrade = 0;

	public static int earnedPowerSubGrade = 0;

	public static int totalPowerSubGrade = 0;

	public static int boughtControlSubGrade = 0;

	public static int earnedControlSubGrade = 0;

	public static int totalControlSubGrade = 0;

	public static int boughtAgilitySubGrade = 0;

	public static int earnedAgilitySubGrade = 0;

	public static int totalAgilitySubGrade = 0;

	public static bool FirstTimeOpen = false;

	public static int powerGrade = 0;

	public static int controlGrade = 0;

	public static int agilityGrade = 0;

	public static int[] LevelCompletedArray = new int[18];

	public static int LevelId = 0;

	public static int CurrentLevelCompleted = 0;

	public static int totalLevels = 18;

	public static int totalFours;

	public static int totalSixes;

	public static string SuperOverMode = "bat";

	public static int continousBoundaries;

	public static int continousSixes;

	public static int continousWickets;

	public static bool InningsCompleted = false;

	public static bool MPInningsCompleted = false;

	public static int LevelFailed = 0;

	public static bool NewInnings = true;

	public static string[] TargetRangeArray = new string[6];

	public static int CTLevelId = 0;

	public static int CTSubLevelId = 0;

	public static int CTLevelCompleted = 0;

	public static int CTSubLevelCompleted = 0;

	public static int CTCurrentPlayingMainLevel;

	public static int CTWonMatch = 0;

	public static int[] MainLevelCompletedArray = new int[6];

	public static int[] SubLevelCompletedArray = new int[5];

	public static int[] StartRangeArray = new int[5];

	public static int[] EndRangeArray = new int[5];

	public static bool isReplayGame = false;

	public static int TargetRun = 0;

	public static bool CanRetrieveAchievements = false;

	public static HelpInfo[] HelpText;

	public static int PlayModeSelected;

	public static int GroupMatchID;

	public static int QualifierMatchID;

	public static int InterfaceCameraHeight = 300;

	public static int[] Overs = new int[10];

	public static int totalTeams = 16;

	public static int currentPageIndex = 0;

	public static int myTeamIndex = 0;

	public static int opponentTeamIndex = 1;

	public static int oversSelectedIndex = 0;

	public static int MyTeamStateIndex;

	public static int OppTeamStateIndex;

	public static int TournamentStage;

	public static int previousWinCount;

	public static string storedLeagueResult = string.Empty;

	public static int tossWonBy;

	public static int meFirstBatting = 1;

	public static int gameWonBy;

	public static int NextRoundQualification = 4;

	public static int totalWickets = 100;

	public static int totalOvers;

	public static int wicketType;

	public static string[] WCPointsTable;

	public static string tournamentType;

	public static string teamType;

	public static string NplIndiaData = string.Empty;

	public static int NPLIndiaLeagueMatchIndex;

	public static int NPLIndiaMyCurrentMatchIndex;

	public static string NPLIndiaTeamWonIndexStr = string.Empty;

	public static string StoredNPLIndiaSeriesResult = string.Empty;

	public static int NPLIndiaTournamentStage = 0;

	public static string[] NPLIndiaPointsTable;

	public static int[] NPLIndiaSortedPointsTable;

	public static string[] NPLIndiaMatchDetails;

	public static string NPLIndiaSchedule = "0&0-5|1-8|4-7|5-9|2-3|4-8|1-5|6-7|2-8|3-6|0-9|1-6|2-7|4-5|0-8|6-9|1-4|2-5|3-8|0-4|5-7|1-2|4-9|7-8|0-3|1-9|2-6|3-7|8-9|0-6|1-7|2-4|3-5|6-8|0-7|2-9|1-3|5-6|7-9|3-4|0-2|5-8|3-9|4-6|0-1";

	public static string NPLBangladeshSchedule = "0&0-5|3-6|4-7|2-3|1-5|6-7|0-1|2-7|1-4|2-5|0-4|5-7|1-2|0-3|2-6|3-7|4-5|0-6|1-7|2-4|1-6|3-5|0-7|1-3|5-6|3-4|0-2|4-6";

	public static string NPLAustraliaSchedule = "0&0-5|3-6|4-7|2-3|1-5|6-7|0-1|2-7|1-4|2-5|0-4|5-7|1-2|0-3|2-6|3-7|4-5|0-6|1-7|2-4|1-6|3-5|0-7|1-3|5-6|3-4|0-2|4-6";

	public static string NplPakistanSchedule = "0&0-2|0-1|2-4|3-0|4-2|0-4|1-4|2-1|4-0|1-0|1-2|2-3|3-4|4-1|0-3|1-3|3-1|4-3|2-0|3-2";

	public static int CurrentBall = -1;

	public static int WCTournamentStage;

	public static string WCLeagueData = string.Empty;

	public static string StoredWCTournamentResult = string.Empty;

	public static string WCTeamWonIndexStr = string.Empty;

	public static int WCLeagueMatchIndex;

	public static int WCMyCurrentMatchIndex;

	public static int[] WCSortedPointsTable;

	public static string[] WCMatchDetails;

	public static string[] WCGroupDetails = new string[16]
	{
		"A", "A", "A", "A", "B", "B", "B", "B", "A", "B",
		"A", "A", "B", "A", "B", "B"
	};

	public static string WCSchedule = "0&A!1-2|B!4-9|A!0-11|B!7-14|B!9-5|A!8-2|A!1-3|B!7-6|B!4-5|A!0-3|A!8-10|B!6-15|A!1-11|B!12-14|A!0-8|B!15-9|A!13-10|B!5-6|B!7-12|A!10-11|A!3-8|B!14-6|A!13-11|B!9-7|A!0-10|B!15-5|A!2-11|B!12-9|A!3-10|B!4-7|A!13-2|B!9-14|B!15-12|A!1-0|B!5-7|A!8-11|B!4-15|A!13-0|A!10-2|B!5-12|A!3-11|B!14-4|A!1-10|B!7-15|A!13-8|B!9-6|A!3-2|B!12-4|A!3-13|A!1-8|B!15-14|B!4-6|A!1-13|B!5-14|A!0-2|B!6-12";

	public static int TotalMatchBalls = 0;

	public static int WagonWheelRuns = 0;

	public static int[] Wagon_PlayedBy = new int[0];

	public static int[] Wagon_RunScored = new int[0];

	public static int[] Wagon_NoOfBounce = new int[0];

	public static float[] Wagon_BallAngle = new float[0];

	public static Vector3[] Wagon_FirstPitchPoint = new Vector3[0];

	public static Vector3[] Wagon_SecondPitchPoint = new Vector3[0];

	public static Vector3[] Wagon_ThirdPitchPoint = new Vector3[0];

	public static Vector3[] Wagon_FinalPitchPoint = new Vector3[0];

	public static float[] Wagon_FirstPitchHeight = new float[0];

	public static float[] Wagon_SecondPitchHeight = new float[0];

	public static float[] Wagon_ThirdPitchHeight = new float[0];

	public static string WagonString_PlayedBy = string.Empty;

	public static string WagonString_RunScored = string.Empty;

	public static string WagonString_NoOfBounce = string.Empty;

	public static string WagonString_BallAngle = string.Empty;

	public static string WagonString_FirstPitchPoint = string.Empty;

	public static string WagonString_SecondPitchPoint = string.Empty;

	public static string WagonString_ThirdPitchPoint = string.Empty;

	public static string WagonString_FinalPitchPoint = string.Empty;

	public static string WagonString_FirstPitchHeight = string.Empty;

	public static string WagonString_SecondPitchHeight = string.Empty;

	public static string WagonString_ThirdPitchHeight = string.Empty;

	public static int BattingTeamIndex;

	public static int BowlingTeamIndex;

	public static string BattingTeamName;

	public static string BowlingTeamName;

	public static bool HattrickBall;

	public static bool isJokerCall;

	public static bool isFreeHit = false;

	public static int StrikerIndex = 0;

	public static int NonStrikerIndex = 1;

	public static string StrikerHand;

	public static string NonStrikerHand;

	public static bool stumpingAttempted;

	public static bool runoutThirdUmpireAppeal;

	public static int strikerPartnershipRuns;

	public static int strikerPartnershipBall;

	public static int NonstrikerPartnershipRuns;

	public static int NonstrikerPartnershipBall;

	public static bool canShowPartnerShip = true;

	public static int CurrentBowlerIndex;

	public static int BowlerType;

	public static string BowlerHand;

	public static string BowlerSide;

	public static int wickerKeeperIndex;

	public static int isBowlingControl;

	public static bool GoodMatch;

	public static int currentInnings;

	public static int TargetToChase;

	public static int RunsInAOver;

	public static int WideInAOver;

	public static int DotInAOver;

	public static bool NewOver;

	public static bool InBetweenOvers;

	public static bool isFreeHitBall;

	public static bool isLineFreeHitBallCompleted;

	public static int noBallFacedBatsmanId = -1;

	public static bool PowerPlay = true;

	public static int RequiredRun;

	public static float RunRate;

	public static float ReqRunRate;

	public static int BowlingSpeed;

	public static float BowlingAngle;

	public static float BowlingSwing;

	public static string QuaterFinalList;

	public static string SemiFinalList;

	public static string FinalList;

	public static Vector3 HIDEPOS = new Vector3(Screen.width + 500, -(Screen.height + 500), -10f);

	public static Vector3 SHOWPOS = new Vector3(0f, 0f, 0f);

	public static bool isWebVersion = false;

	public static int FreeversionMyTeam = 3;

	public static int FreeversionOppTeam = 8;

	public static int FreeversionOver = 0;

	public static string achievements = string.Empty;

	public static bool postToFB = false;

	public static string username = string.Empty;

	public static bool isConfidenceLevel = false;

	public static bool SlogOvers = false;

	public static string CurrentMenu;

	public static bool PlayerMode;

	public static int bgMusicVal;

	public static int ambientVal;

	public static float menuBgVolume = 1f;

	public static float sfxVolume = 1f;

	public static int ConfidenceVal;

	public static int FacebookVal;

	public static int tutorialToggle = 1;

	public static int EditTeamIndex;

	public static int EditPlayerIndex;

	public static int myTeamPosition;

	public static int oppTeamPosition;

	public static string battingTeamUniform;

	public static string bowlingTeamUniform;

	public static float xOffSet;

	public static string adType;

	public static bool receivedAdEvent = false;

	public static bool gameCompleted = false;

	public static string PlayOffMode;

	public static int PlayOffMatchID;

	public static int TournamentSixes;

	public static bool TournamentStart;

	public static int SixDistance;

	public static string difficultyMode = "medium";

	public static string graphicsLevel = "medium";

	public static bool ReplayShowing;

	public static bool TeamEdited = false;

	public static int TournamentWonCount;

	public static bool isQuit = false;

	public static int PowerUpPoints;

	public static long PointsAwardTimeDiff;

	public static long UpdateWizardTimeDiff;

	public static bool ProductsListReceived = false;

	public static string currentMenuStr;

	public static bool canShowReplay = false;

	public static bool reviewReplay = false;

	public static bool canShowProfiler = true;

	public static bool GameIsOnFocus = true;

	public static bool SceneIsLoading = false;

	public static bool GameStartsFromSave = false;

	public static bool LowEndMobile = false;

	public static string updateAppXMLLink = string.Empty;

	public static int updatedVersion;

	public static string updateDescription = string.Empty;

	public static string updateAppLink = string.Empty;

	public static int isAdPurchased = 0;

	public static bool canPressBackBtn = false;

	public static bool tempCanPressBackBtn = false;

	public static string CurrentPage = string.Empty;

	public static string tempCurrentPage = string.Empty;

	public static string[] ballUpdate = new string[6];

	public static int currentMatchWickets = 0;

	public static int currentMatchScores = 0;

	public static int currentMatchBalls = 0;

	public static int totalPoints;

	public static string BOC_2_Link = "https://play.google.com/store/apps/details?id=com.nextwave.wcclite";

	public static bool bGooglePlayLoginSuccess;

	public static string profilePlayerName = "Guest";

	public static string profilePlayerID = string.Empty;

	public static string profileEmailID = string.Empty;

	public static string profilePicURL = string.Empty;

	public static int gameTotalPoints = 0;

	public static int gameSyncPoint = 0;

	public static TeamInfo[] TeamList;

	public static PlayingTeamInfo[] TM_PlayerInfo = new PlayingTeamInfo[4];

	public static PlayingTeamInfo[] TM_SquadInfo = new PlayingTeamInfo[4];

	public static TMTeamList[] TM_TeamInfo = new TMTeamList[4];

	public static PlayingTeamInfo[] TM_PlayingXIList = new PlayingTeamInfo[11];

	public static int nextWicketball = 0;

	public static int BatTeamIndex = 0;

	public static int BowlTeamIndex = 1;

	public static bool ShowTMGameOver = false;

	public static int[] BattingTeam = new int[4];

	public static int[] BowlingTeam = new int[4];

	public static int followOnTarget = 1;

	public static bool isFollowOn = false;

	public static bool declareMode = false;

	public static int tempBattingTeamIndex = 0;

	public static bool TMNewInnings = false;

	public static int TMcurrentInnings;

	public static string isFollowOnShown = "0";

	public static int noOfOvers = 0;

	public static int ballsBowledPerDay = 0;

	public static int noOfSession = 3;

	public static int currentSession = 0;

	public static int[] currentTestInnings = new int[4];

	public static int[] currentTMPlayingInnings = new int[4];

	public static int currentDay = 1;

	public static int MaxDays = 5;

	public static int inningsLead = 0;

	public static string sessionName = string.Empty;

	public static int AIFirstInningsDeclareRuns = 0;

	public static int AIFirstInningsDeclareBalls = 0;

	public static int AiFirstInngsLeadDeclareRuns = 0;

	public static int AiTargetDeclareRuns = 0;

	public static bool isSkipBallsForSession = false;

	public static string TestSeriesData = string.Empty;

	public static string StoredTestSeriesResult = string.Empty;

	public static string TestTeamWonIndexStr = string.Empty;

	public static int CurrentTestMatch;

	public static int TotalTestMatches = 1;

	public static int MyTestWinCount;

	public static int OppTestWinCount;

	public static int maxBallsPerDay = 18;

	public static void GetRandomScoreForOppTeam(int StartRange, int EndRange)
	{
		TargetToChase = Random.Range(StartRange, EndRange);
	}

	public static string ReplaceStr(string str, string originalChar, string replaceStr)
	{
		string text = string.Empty;
		int length = str.Length;
		for (int i = 0; i < length; i++)
		{
			string text2 = string.Empty + str[i];
			text = ((!(text2 == originalChar)) ? (text + str[i]) : (text + replaceStr));
		}
		return text;
	}

	public static string FirstLetterCaps(string str)
	{
		string text = string.Empty;
		string[] array = str.Split(" "[0]);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != string.Empty && array[i] != " ")
			{
				string text2 = array[i];
				string text3 = text2.Substring(0, 1);
				string text4 = text2.Substring(1);
				text3 = text3.ToUpper();
				text4 = text4.ToLower();
				text2 = (array[i] = text3 + text4);
			}
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != string.Empty && array[i] != " ")
			{
				text = ((i >= array.Length - 1) ? (text + array[i]) : (text + array[i] + " "));
			}
		}
		return text;
	}

	public static void QuitApp()
	{
		Application.Quit();
	}

	public static void GUILog(string str)
	{
	}
}
