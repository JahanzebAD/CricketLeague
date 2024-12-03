using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using GreedyGame.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameModel : Singleton<GameModel>
{
	private int savedNonStrikerIndex;

	private int savedStrikerIndex;

	private int savedNewStrikerIndex;

	public Text rvPopupText;

	public GameObject rvPopup;

	public GameObject generatingScore;

	public Image RVFillmeter;

	private string RVStatus = string.Empty;

	private bool canShowAnimation = true;

	public Transform ballList;

	public Image fill;

	public Text scrambledScore;

	public Text scrambledWickets;

	public string ScoreStr;

	public string ExtraStr;

	public string OversStr;

	public bool isGamePaused;

	public GameObject resumeGO;

	private bool hitOneFour;

	private bool hitOneSix;

	private bool hitBoth;

	private int runsScoredInBoundaries;

	private TextAsset xmlAsset;

	public int CounterFour;

	public int CounterSix;

	public int CounterMaiden;

	public int CounterDot;

	public int CounterWicketBowled;

	public int CounterWicketCatch;

	public int CounterWicketOthers;

	public int CounterFifty;

	public int CounterCentury;

	public int CounterPerOversPlayed;

	public int CounterOppSix;

	public int CounterPlayerDuckOut;

	private float ballAngle;

	public int BatsmanEntryTime = 3;

	public float fadeTime = 0.2f;

	public Camera renderCamera;

	public Button SkipBtn;

	public Text ActionTxt;

	public Text loadingText;

	public bool SkipUpdate;

	public static float RunRate;

	public int stateVar = -1;

	public bool CanResumeGame;

	public bool CanPauseGame;

	public int NewBatsmanIndex;

	private int PowerPlayOver;

	private bool IsWicketBall;

	private int batsmanOutIndex;

	private bool gameQuit;

	public bool inningsCompleted;

	public int action = -1;

	private int bowlingControl = -1;

	private int userAction = -1;

	private bool[] achievementFlag;

	public Sequence seq;

	private Touch[] touchPhase;

	private bool NewTouch;

	private Vector2 TouchInitPos;

	private Vector2 TouchEndPos;

	private int selectedAngle;

	protected bool isTouchEnded = true;

	protected bool shotCompleted;

	protected float touchInitTime;

	protected float shotTimeLimit = 0.3f;

	protected int SlogOverValue;

	protected int WicketsInOver;

	private bool maxRunsReached;

	private bool maxWidesReached;

	private bool fromMainMenu;

	public GameObject SkipObj;

	public float EscapeResumedTime;

	public bool EscapeGameResumed;

	private float timeOutTextBlinkTime;

	private float ReplayStartTime = 1f;

	private int blinkStatus;

	private bool ReplayCompletedCalledFromMe;

	private Vector3 ReplayTxtScreenPosition;

	public bool levelCompleted;

	protected string username;

	private bool fourInOver;

	private bool sixInOver;

	private float[] loftValues = new float[11]
	{
		70f, 70f, 68f, 68f, 66f, 66f, 60f, 58f, 56f, 54f,
		52f
	};

	private Vector2 prevMousePos;

	private bool isValidBall;

	private bool maxSixes;

	private string skipStatus = string.Empty;

	public int currentBall = -1;

	public int currentPartnership;

	public int runsScoredInOver;

	private LoadPlayerPrefs loadPlayerPrefsScript;

	public GroundController groundControllerScript;

	private bool SuccessfulChase;

	private bool tempSuccessfulChase;

	private Intro IntroScript;

	private Transform _transform;

	private bool isTouched;

	private string[] NamesToDisplayInGoogle = new string[6] { "CubClass", "YoungLion", "PowerPlayer", "SuperPro", "Champion", "PrideOfChepauk" };

	private int CurrentSubLevel;

	private int rebowlcounter;

	public bool rebowled;

	public int rebowlStatus = 2;

	private int RvRunsScored;

	private string templastBowledBall = string.Empty;

	public int[] ultraEdgeVariables = new int[10];

	public bool ultaEdgeBoolean;

	private float[,] confidenceMultiplier = new float[3, 3]
	{
		{ 4f, 6f, 8f },
		{ 6f, 9f, 12f },
		{ 8f, 12f, 16f }
	};

	public int vBall;

	public int bID;

	public int wType;

	public int baID;

	public int cID;

	public int bOut;

	public int RvCanCount;

	public int RvExtras;

	private bool shownRv;

	private int bowlIndex;

	public bool sessionComplete;

	protected void OnLevelWasLoaded(int _levelIndex)
	{
		if (_levelIndex == 2)
		{
			CONTROLLER.SceneIsLoading = false;
		}
	}

	protected void Awake()
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			ResetTM_PlayerScores();
		}
		GetDifficultyMode();
		fromMainMenu = true;
		generatingScore.SetActive(value: false);
		_transform = base.transform;
		IntroScript = GameObject.Find("Intro").GetComponent<Intro>();
		CONTROLLER.isQuit = false;
		if (SkipObj != null)
		{
			SkipObj.SetActive(value: true);
		}
		XPTable.InitXpValues();
		CoinsTable.InitCoinValues();
		SpaceTextValue();
		if (SkipBtn != null)
		{
			ActionTxt.gameObject.SetActive(value: false);
		}
		CONTROLLER.gameCompleted = false;
		if (CONTROLLER.PlayModeSelected != 6)
		{
			ResetPlayerScores();
		}
		ObscuredPrefs.GetInt("prevSixCount", CONTROLLER.sixMeterCount);
	}

	protected void Start()
	{
		rvPopup.SetActive(value: false);
		hitOneSix = (hitOneFour = (hitBoth = false));
		Time.timeScale = 1f;
		loadPlayerPrefsScript = GameObject.Find("LoadPlayerPrefs").GetComponent<LoadPlayerPrefs>();
		groundControllerScript = GameObject.Find("GroundController").GetComponent<GroundController>();
		SkipBtn.gameObject.SetActive(value: false);
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "contsix"))
		{
			CONTROLLER.continousSixes = ObscuredPrefs.GetInt(CONTROLLER.PlayModeSelected + "contsix");
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "contwik"))
		{
			CONTROLLER.continousWickets = ObscuredPrefs.GetInt(CONTROLLER.PlayModeSelected + "contwik");
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "contfour"))
		{
			CONTROLLER.continousBoundaries = ObscuredPrefs.GetInt(CONTROLLER.PlayModeSelected + "contfour");
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "CounterFour"))
		{
			CounterFour = ObscuredPrefs.GetInt(CONTROLLER.PlayModeSelected + "CounterFour");
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "CounterSix"))
		{
			CounterSix = ObscuredPrefs.GetInt(CONTROLLER.PlayModeSelected + "CounterSix");
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "CounterOppSix"))
		{
			CounterOppSix = ObscuredPrefs.GetInt(CONTROLLER.PlayModeSelected + "CounterOppSix");
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "CounterMaiden"))
		{
			CounterMaiden = ObscuredPrefs.GetInt(CONTROLLER.PlayModeSelected + "CounterMaiden");
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "CounterDot"))
		{
			CounterDot = ObscuredPrefs.GetInt(CONTROLLER.PlayModeSelected + "CounterDot");
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "CounterWicketBowled"))
		{
			CounterWicketBowled = ObscuredPrefs.GetInt(CONTROLLER.PlayModeSelected + "CounterWicketBowled");
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "CounterWicketCatch"))
		{
			CounterWicketCatch = ObscuredPrefs.GetInt(CONTROLLER.PlayModeSelected + "CounterWicketCatch");
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "CounterWicketOthers"))
		{
			CounterWicketOthers = ObscuredPrefs.GetInt(CONTROLLER.PlayModeSelected + "CounterWicketOthers");
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "CounterFifty"))
		{
			CounterFifty = ObscuredPrefs.GetInt(CONTROLLER.PlayModeSelected + "CounterFifty");
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "CounterCentury"))
		{
			CounterCentury = ObscuredPrefs.GetInt("CounterCentury");
		}
		if (ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "CounterPerOversPlayed"))
		{
			CounterPerOversPlayed = ObscuredPrefs.GetInt(CONTROLLER.PlayModeSelected + "CounterPerOversPlayed");
		}
		if (ObscuredPrefs.HasKey("CounterPlayerDuckOut"))
		{
			CounterPlayerDuckOut = ObscuredPrefs.GetInt("CounterPlayerDuckOut");
		}
		if (!CONTROLLER.GameStartsFromSave || CONTROLLER.PlayModeSelected == 6)
		{
			if (CONTROLLER.PlayModeSelected == 4)
			{
				ResetCurrentMatchDetails();
			}
			if (CONTROLLER.PlayModeSelected == 7)
			{
				CONTROLLER.currentInnings = 0;
				XMLReader.assignNow();
				initTestMatchDetails();
				SetAIDeclarationParameters();
			}
			NewGame();
		}
		else
		{
			Singleton<NavigationBack>.instance.disableDeviceBack = false;
			ContinueGame();
		}
		Vector2 vector = new Vector2(renderCamera.pixelWidth, renderCamera.pixelHeight);
		Vector3 vector2 = renderCamera.ScreenToWorldPoint(new Vector3(vector.x, 0f, 0f));
		if (ObscuredPrefs.HasKey("tstatus"))
		{
			CONTROLLER.tournamentStatus = ObscuredPrefs.GetString("tstatus");
		}
	}

	private void GetDifficultyMode()
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			CONTROLLER.difficultyMode = ObscuredPrefs.GetString("exdiff");
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			CONTROLLER.difficultyMode = ObscuredPrefs.GetString("tourdiff");
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "PAK")
			{
				CONTROLLER.difficultyMode = ObscuredPrefs.GetString("pakdiff");
			}
			else if (CONTROLLER.tournamentType == "NPL")
			{
				CONTROLLER.difficultyMode = ObscuredPrefs.GetString("npldiff");
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				CONTROLLER.difficultyMode = ObscuredPrefs.GetString("ausdiff");
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			CONTROLLER.difficultyMode = ObscuredPrefs.GetString("wcdiff");
		}
	}

	public void GetMyTeamList()
	{
		xmlAsset = Resources.Load("XML/Teams/CHE") as TextAsset;
		XMLReader.ParseXML(xmlAsset.text);
		GetOppTeamList();
	}

	private void GetOppTeamList()
	{
		xmlAsset = Resources.Load("XML/Teams/CHE") as TextAsset;
		XMLReader.ParseXML(xmlAsset.text);
	}

	private void isGameSaved()
	{
		NewBatsmanIndex = 1;
		CONTROLLER.StrikerIndex = 0;
		CONTROLLER.NonStrikerIndex = 1;
		string overStr = GetOverStr();
		if (ObscuredPrefs.HasKey("SuperOverDetail"))
		{
			string @string = ObscuredPrefs.GetString("SuperOverDetail");
			string[] array = @string.Split("|"[0]);
			Singleton<GroundController>.instance.currentBowlerType = array[0];
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores = int.Parse(array[1]);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets = int.Parse(array[2]);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchBalls = int.Parse(array[3]);
			currentBall = int.Parse(array[4]);
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[0] = array[5];
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[1] = array[6];
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[2] = array[7];
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[3] = array[8];
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[4] = array[9];
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[5] = array[10];
			NewBatsmanIndex = int.Parse(array[11]);
			CONTROLLER.StrikerIndex = int.Parse(array[12]);
			CONTROLLER.NonStrikerIndex = int.Parse(array[13]);
			CONTROLLER.totalFours = int.Parse(array[14]);
			CONTROLLER.totalSixes = int.Parse(array[15]);
			CONTROLLER.continousBoundaries = int.Parse(array[16]);
			CONTROLLER.continousSixes = int.Parse(array[17]);
			if (array.Length == 18)
			{
				CONTROLLER.LevelId = 0;
			}
			else
			{
				CONTROLLER.LevelId = int.Parse(array[18]);
			}
			CONTROLLER.GameStartsFromSave = true;
		}
		overStr = GetOverStr();
		if (currentBall >= 5)
		{
			currentBall = -1;
			for (int i = 0; i < 6; i++)
			{
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[i] = string.Empty;
			}
		}
	}

	private void DetailsSavedBallbyBall()
	{
		string empty = string.Empty;
		empty = empty + Singleton<GroundController>.instance.currentBowlerType + "|";
		empty = empty + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores + "|";
		empty = empty + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets + "|";
		empty = empty + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchBalls + "|";
		empty = empty + currentBall + "|";
		empty = empty + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[0] + "|";
		empty = empty + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[1] + "|";
		empty = empty + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[2] + "|";
		empty = empty + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[3] + "|";
		empty = empty + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[4] + "|";
		empty = empty + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[5] + "|";
		empty = empty + NewBatsmanIndex + "|";
		empty = empty + CONTROLLER.StrikerIndex + "|";
		empty = empty + CONTROLLER.NonStrikerIndex + "|";
		empty = empty + CONTROLLER.totalFours + "|";
		empty = empty + CONTROLLER.totalSixes + "|";
		empty = empty + CONTROLLER.continousBoundaries + "|";
		empty = empty + CONTROLLER.continousSixes + "|";
		empty += CONTROLLER.LevelId;
		string text = string.Empty;
		for (int i = 0; i <= CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets + 1; i++)
		{
			text = text + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.RunsScored + "$";
			text = text + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.BallsPlayed + "$";
			text = text + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Fours + "$";
			text = text + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Sixes + "$";
			text = text + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Status + "|";
		}
	}

	private void MatchStartingFromSaved()
	{
		CONTROLLER.myTeamIndex = ObscuredPrefs.GetInt("SOMyTeamIndex");
		CONTROLLER.opponentTeamIndex = ObscuredPrefs.GetInt("SOOppTeamIndex");
		GetMyTeamList();
		string text = string.Empty;
		if (ObscuredPrefs.HasKey("superoverPlayerDetails"))
		{
			text = ObscuredPrefs.GetString("superoverPlayerDetails");
		}
		if (text != string.Empty && text != null)
		{
			string[] array = text.Split("|"[0]);
			for (int i = 0; i < array.Length - 1; i++)
			{
				string[] array2 = array[i].Split("$"[0]);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.RunsScored = int.Parse(array2[0]);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.BallsPlayed = int.Parse(array2[1]);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Fours = int.Parse(array2[2]);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Sixes = int.Parse(array2[3]);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Status = array2[4];
				if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Status == "out")
				{
					Singleton<BattingScoreCard>.instance.UpdateWicket(i);
				}
				else
				{
					Singleton<BattingScoreCard>.instance.UpdateScoreCard();
				}
			}
		}
		Singleton<Scoreboard>.instance.UpdateScoreCard();
	}

	private bool CheckCurrentLevelCompletedBowling()
	{
		if (CONTROLLER.LevelId == 0)
		{
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores > 27)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 1)
		{
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores > 24)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 2)
		{
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores > 21)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 3)
		{
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores > 18)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 4)
		{
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores > 15)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 5)
		{
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores > 12)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 6)
		{
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores > 9)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 7)
		{
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores > 6)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 8 && CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores > 3)
		{
			return true;
		}
		return false;
	}

	private bool CheckCurrentLevelCompleted()
	{
		if (CONTROLLER.LevelId == 0 || CONTROLLER.LevelId == 1)
		{
			if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores >= 10)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 2 || CONTROLLER.LevelId == 3)
		{
			if (CONTROLLER.totalFours == 3)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 4 || CONTROLLER.LevelId == 5)
		{
			if (CONTROLLER.continousBoundaries == 3)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 6 || CONTROLLER.LevelId == 7)
		{
			if (CONTROLLER.totalSixes == 3)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 8 || CONTROLLER.LevelId == 9)
		{
			if (CONTROLLER.totalFours == 5)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 10 || CONTROLLER.LevelId == 11)
		{
			if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores >= 25)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 12 || CONTROLLER.LevelId == 13)
		{
			if (CONTROLLER.continousSixes == 4)
			{
				return true;
			}
		}
		else if (CONTROLLER.LevelId == 14 || CONTROLLER.LevelId == 15)
		{
			if (CONTROLLER.continousBoundaries == 6)
			{
				return true;
			}
		}
		else if ((CONTROLLER.LevelId == 16 || CONTROLLER.LevelId == 17) && CONTROLLER.continousSixes == 6)
		{
			return true;
		}
		return false;
	}

	private void ShowSuperOverResult()
	{
		InningsCompleteFn();
		Singleton<SuperOverResult>.instance.ShowMe();
	}

	private void UpdateLevelId()
	{
		if (CONTROLLER.LevelId < 18)
		{
			if (CONTROLLER.LevelCompletedArray[CONTROLLER.LevelId] == 0)
			{
				CONTROLLER.LevelCompletedArray[CONTROLLER.CurrentLevelCompleted] = 1;
				CONTROLLER.CurrentLevelCompleted++;
				CONTROLLER.LevelFailed = 0;
				SaveSOLevelDetails();
			}
			if (ObscuredPrefs.HasKey("SuperOverDetail") && CONTROLLER.gameMode == "superover")
			{
				ObscuredPrefs.DeleteKey("SuperOverDetail");
				ObscuredPrefs.DeleteKey("superoverPlayerDetails");
			}
			for (int i = 0; i < CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList.Length; i++)
			{
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.RunsScored = 0;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.BallsPlayed = 0;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Fours = 0;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Sixes = 0;
			}
			Singleton<BattingScoreCard>.instance.ResetBattingCard();
			ShowSuperOverResult();
		}
	}

	private void SaveSOLevelDetails()
	{
		string empty = string.Empty;
		empty = empty + CONTROLLER.CurrentLevelCompleted + "|";
		empty = empty + CONTROLLER.LevelFailed + "|";
		empty = empty + CONTROLLER.LevelId + "|";
		ObscuredPrefs.SetString("SuperOverLevelDetail", empty);
	}

	public void ResetCurrentMatchDetails()
	{
		currentBall = -1;
		CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchBalls = 0;
		CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores = 0;
		CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets = 0;
		NewBatsmanIndex = 1;
		CONTROLLER.StrikerIndex = 0;
		CONTROLLER.NonStrikerIndex = 1;
		CONTROLLER.totalSixes = 0;
		CONTROLLER.totalFours = 0;
		CONTROLLER.continousBoundaries = 0;
		CONTROLLER.continousSixes = 0;
		for (int i = 0; i < CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList.Length; i++)
		{
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.RunsScored = 0;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.BallsPlayed = 0;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Fours = 0;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Sixes = 0;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Status = string.Empty;
		}
		if (CONTROLLER.PlayModeSelected != 6)
		{
			Singleton<BattingScoreCard>.instance.ResetBattingCard();
		}
	}

	public void UpdateChaseTargetLevel()
	{
		if (CONTROLLER.CTSubLevelCompleted < CONTROLLER.SubLevelCompletedArray.Length && CONTROLLER.CTLevelCompleted < CONTROLLER.MainLevelCompletedArray.Length)
		{
			CurrentSubLevel = CONTROLLER.CTSubLevelCompleted;
			if (SuccessfulChase && CONTROLLER.SubLevelCompletedArray[CONTROLLER.CTSubLevelCompleted] == 0 && CONTROLLER.MainLevelCompletedArray[CONTROLLER.CTLevelCompleted] == 0 && !SuperChaseResult.isReplayMode)
			{
				SuccessfulChase = false;
				CONTROLLER.SubLevelCompletedArray[CONTROLLER.CTSubLevelCompleted] = 1;
				CONTROLLER.CTSubLevelCompleted++;
				if (CONTROLLER.CTSubLevelCompleted == 5)
				{
					CONTROLLER.MainLevelCompletedArray[CONTROLLER.CTLevelCompleted] = 1;
					ObscuredPrefs.SetInt("CTMainArray", CONTROLLER.CTLevelCompleted);
					if (CONTROLLER.CTLevelCompleted < NamesToDisplayInGoogle.Length - 1)
					{
						CONTROLLER.CTLevelCompleted++;
						CONTROLLER.CTSubLevelCompleted = 0;
					}
					else
					{
						CONTROLLER.CTSubLevelCompleted = 5;
					}
				}
				string empty = string.Empty;
				empty = empty + CONTROLLER.CTLevelCompleted + "|";
				empty = empty + CONTROLLER.CTSubLevelCompleted + "|";
				ObscuredPrefs.SetString("ChaseTargetLevelDetail", empty);
			}
		}
		ClearLevelDetails();
	}

	public int GetCurrentSubLevel()
	{
		return CurrentSubLevel;
	}

	public void ClearLevelDetails()
	{
		if (ObscuredPrefs.HasKey("ChaseTargetDetail") && CONTROLLER.gameMode == "chasetarget")
		{
			ObscuredPrefs.DeleteKey("ChaseTargetDetail");
			ObscuredPrefs.DeleteKey("chasetargetPlayerDetails");
		}
	}

	private void ResetPlayerScores()
	{
        //if (CONTROLLER.myTeamIndex >= 0)
        //{
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchBalls = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchExtras = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchLbs = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchbyes = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchNoball = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWideBall = 0;
        //}
        //if (CONTROLLER.opponentTeamIndex >= 0)
        //{
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchBalls = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchWickets = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchExtras = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchLbs = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchbyes = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchNoball = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchWideBall = 0;
        //}
        //int num = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList.Length;
        //for (int i = 0; i < num; i++)
        //{
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].ConfidenceVal = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].ConfidenceLevel;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].LoftMeterFillVal = loftValues[i];
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].reachedHalfCentury = false;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].reachedCentury = false;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].ConfidenceVal = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].ConfidenceLevel;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].LoftMeterFillVal = loftValues[i];
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].reachedHalfCentury = false;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].reachedCentury = false;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Status = string.Empty;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.RunsScored = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.BallsPlayed = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Fours = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.Sixes = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BatsmanList.FOW = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BatsmanList.Status = string.Empty;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BatsmanList.RunsScored = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BatsmanList.BallsPlayed = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BatsmanList.Fours = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BatsmanList.Sixes = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BatsmanList.FOW = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.RunsGiven = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.BallsBowled = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.Maiden = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.Wicket = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.maxBallInMatch = 0;
        //    CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.WicketsInBallCount = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BowlerList.RunsGiven = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BowlerList.BallsBowled = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BowlerList.Maiden = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BowlerList.Wicket = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BowlerList.maxBallInMatch = 0;
        //    CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BowlerList.WicketsInBallCount = 0;
       // }
    }

	public void NewGame()
	{
		Singleton<BattingControls>.instance.LoftMeterFill(CONTROLLER.StrikerIndex);
		if (CONTROLLER.PlayModeSelected == 4)
		{
			ObscuredPrefs.SetInt("SOMyTeamIndex", CONTROLLER.myTeamIndex);
			ObscuredPrefs.SetInt("SOOppTeamIndex", CONTROLLER.opponentTeamIndex);
		}
		if (CONTROLLER.PlayModeSelected == 6)
		{
			CONTROLLER.totalOvers = Multiplayer.overs;
		}
		if (CONTROLLER.PlayModeSelected == 7)
		{
			CONTROLLER.NewInnings = true;
		}
		DeleteKeys();
		DeleteAchievementFlags();
		FormatNames();
		TrimLastSpaces();
		StartGame();
		PostGoogleAnalyticsEvent();
	}

	private void ContinueGame()
	{
		Singleton<BattingControls>.instance.LoftMeterFill(CONTROLLER.StrikerIndex);
		if (CONTROLLER.PlayModeSelected == 4)
		{
			CONTROLLER.totalOvers = 1;
			if (CONTROLLER.SuperOverMode == "bat")
			{
				CONTROLLER.totalWickets = 2;
			}
			else
			{
				CONTROLLER.totalWickets = 2;
			}
		}
		else
		{
			CONTROLLER.totalWickets = 10;
		}
		if (CONTROLLER.PlayModeSelected == 1)
		{
			loadPlayerPrefsScript.getTournamentList();
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			loadPlayerPrefsScript.GetNPLIndiaTournamentList();
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			loadPlayerPrefsScript.GetWCTournamentList();
		}
		AutoSave.LoadGame();
		if (CONTROLLER.PlayModeSelected == 7)
		{
			CONTROLLER.oversSelectedIndex = ObscuredPrefs.GetInt("TMOvers");
			CONTROLLER.totalOvers = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex];
		}
		FormatNames();
		TrimLastSpaces();
		RestartGame();
		if (CONTROLLER.PlayModeSelected == 7)
		{
			CheckIfDayIsOver();
		}
	}

	public void StartGame()
	{
		CONTROLLER.NewInnings = true;
		if (CONTROLLER.PlayModeSelected == 4)
		{
			CONTROLLER.totalOvers = 1;
			if (CONTROLLER.SuperOverMode == "bat")
			{
				CONTROLLER.totalWickets = 2;
			}
			else
			{
				CONTROLLER.totalWickets = 2;
			}
		}
		else
		{
			CONTROLLER.totalWickets = 10;
		}
		if (CONTROLLER.PlayModeSelected == 6)
		{
			CONTROLLER.totalOvers = Multiplayer.overs;
		}
		if (CONTROLLER.PlayModeSelected == 7)
		{
			CONTROLLER.currentInnings = 0;
		}
		else
		{
			CONTROLLER.currentInnings = 0;
		}
		ResetVariables();
		CONTROLLER.achievements = string.Empty;
	}

	private void scoreMoreThanCentury(int batsmanId)
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TMscoreMoreThanCentury(batsmanId);
		}
		else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].BatsmanList.RunsScored >= 100 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].BatsmanList.RunsScored % 50 >= 0 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].BatsmanList.RunsScored % 50 < 6 && CONTROLLER.PlayModeSelected != 2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].reachedCentury)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].scoredHundredPlus = true;
		}
		else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].BatsmanList.RunsScored >= 100 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].BatsmanList.RunsScored % 50 >= 10 && CONTROLLER.PlayModeSelected != 2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].scoredHundredPlus)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].scoredHundredPlus = false;
		}
	}

	private void CheckIfDayIsOver()
	{
		if (CONTROLLER.ballsBowledPerDay >= CONTROLLER.Overs[ObscuredPrefs.GetInt("TMOvers")] * 6)
		{
			CONTROLLER.ballsBowledPerDay = 0;
			CONTROLLER.currentDay++;
			CONTROLLER.currentSession = 0;
			Singleton<Scoreboard>.instance.UpdateScoreCard();
		}
	}

	private void TMscoreMoreThanCentury(int batsmanId)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			if (TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, batsmanId) >= 100 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, batsmanId) % 50 >= 0 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, batsmanId) % 50 < 6 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].TMreachedCentury1)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].TMscoredHundredPlus1 = true;
			}
			else if (TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, batsmanId) >= 100 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, batsmanId) % 50 >= 10 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].TMscoredHundredPlus1)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].TMscoredHundredPlus1 = false;
			}
		}
		else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].BatsmanList.TMRunsScored2 >= 100 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].BatsmanList.TMRunsScored2 % 50 >= 0 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].BatsmanList.TMRunsScored2 % 50 < 6 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].TMreachedCentury2)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].TMscoredHundredPlus2 = true;
		}
		else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].BatsmanList.TMRunsScored2 >= 100 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].BatsmanList.TMRunsScored2 % 50 >= 10 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].TMscoredHundredPlus2)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanId].TMscoredHundredPlus2 = false;
		}
	}

	public void RestartGame()
	{
		currentBall = AutoSave.currentBall;
		runsScoredInOver = AutoSave.runsScoredInOver;
		WicketsInOver = AutoSave.WicketsInOver;
		maxRunsReached = AutoSave.maxRunsReached;
		maxWidesReached = AutoSave.maxWidesReached;
		currentPartnership = AutoSave.currentPartnership;
		fourInOver = AutoSave.fourInOver;
		sixInOver = AutoSave.sixInOver;
		maxSixes = AutoSave.maxSixes;
		if (CONTROLLER.PlayModeSelected == 7)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipRuns = CONTROLLER.strikerPartnershipRuns;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipBalls = CONTROLLER.strikerPartnershipBall;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipRuns = CONTROLLER.NonstrikerPartnershipRuns;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipBalls = CONTROLLER.NonstrikerPartnershipBall;
			if (CONTROLLER.currentInnings < 2)
			{
				NewBatsmanIndex = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 + 1;
			}
			else
			{
				NewBatsmanIndex = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets2 + 1;
			}
			if (NewBatsmanIndex >= CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length)
			{
				NewBatsmanIndex = CONTROLLER.totalWickets;
			}
			for (int i = 0; i < AutoSave.ballUpdate.Length; i++)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[i] = Convert.ToString(AutoSave.ballUpdate[i]);
			}
			for (int j = AutoSave.ballUpdate.Length; j < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate.Length; j++)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[j] = string.Empty;
			}
		}
		else
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipRuns = CONTROLLER.strikerPartnershipRuns;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipBalls = CONTROLLER.strikerPartnershipBall;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipRuns = CONTROLLER.NonstrikerPartnershipRuns;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipBalls = CONTROLLER.NonstrikerPartnershipBall;
			NewBatsmanIndex = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets + 1;
			if (NewBatsmanIndex >= CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList.Length)
			{
				NewBatsmanIndex = CONTROLLER.totalWickets;
			}
			for (int k = 0; k < AutoSave.ballUpdate.Length; k++)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[k] = Convert.ToString(AutoSave.ballUpdate[k]);
			}
			for (int l = AutoSave.ballUpdate.Length; l < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate.Length; l++)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[l] = string.Empty;
			}
		}
		scoreMoreThanCentury(CONTROLLER.StrikerIndex);
		scoreMoreThanCentury(CONTROLLER.NonStrikerIndex);
		if (Singleton<BallSimulationManager>.instance.CanShowBallSimulation())
		{
			Singleton<BallSimulationManager>.instance.retriveSavedData = true;
		}
		string[] array = AutoSave.ballListInfo.Split('|');
		for (int m = 0; m < array.Length - 1 && !(array[m] == " "); m++)
		{
			Singleton<ScoreBoardBallList>.instance.AddBall(array[m], " ");
		}
		array = AutoSave.ballextras.Split('|');
		for (int n = 0; n < array.Length - 1; n += 2)
		{
			Singleton<ScoreBoardBallList>.instance.extras.Add(array[n]);
			Singleton<ScoreBoardBallList>.instance.extras.Add(array[n + 1]);
		}
		if (CONTROLLER.currentInnings == 1 && CONTROLLER.PlayModeSelected != 7)
		{
			CONTROLLER.TargetToChase = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].currentMatchScores + 1;
		}
		Singleton<AIFieldingSetupManager>.instance.SetSavedField();
		SetTeamIndex();
		SetFielders();
		if (CONTROLLER.PlayModeSelected == 7)
		{
			if (CONTROLLER.currentInnings < 2)
			{
				ScoreStr = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores1 + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1;
				ExtraStr = LocalizationData.instance.getText(235) + " " + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchExtras;
			}
			else
			{
				ScoreStr = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores2 + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets2;
				ExtraStr = LocalizationData.instance.getText(235) + " " + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchExtras;
			}
		}
		else
		{
			ScoreStr = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets;
			ExtraStr = LocalizationData.instance.getText(235) + " " + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchExtras;
		}
		OversStr = GetOverStr() + "(" + CONTROLLER.totalOvers + ")";
		if (CONTROLLER.PlayModeSelected == 6)
		{
			ResetAllLocalVariables();
		}
		Singleton<BattingScoreCard>.instance.RestartGame();
		Singleton<BowlingScoreCard>.instance.ResetBowlingCard();
		Singleton<PreviewScreen>.instance.SetFieldPreview();
		Singleton<BowlingScoreCard>.instance.RestartGame();
		Singleton<Scoreboard>.instance.UpdateScoreCard();
		groundControllerScript.NewInnings();
		groundControllerScript.ResetFielders();
		groundControllerScript.RestartBowlerSide(CONTROLLER.BowlerSide);
		if (CONTROLLER.PlayModeSelected == 7 && CONTROLLER.currentInnings == 3)
		{
			SetTargetToWin();
		}
		inningsCompleted = CheckForInningsComplete();
		if (inningsCompleted)
		{
			InningsCompleteFn();
		}
		else if (currentBall == 5)
		{
			groundControllerScript.showPreviewCamera(status: false);
			NewOver();
		}
		else
		{
			Singleton<ChangePlayerTexture>.instance.ChangeTexture();
			BowlNextBall();
		}
	}

	public void ResetVariables()
	{
		Singleton<GroundController>.instance.batsman.transform.position = new Vector3(-65f, 0f, -6f);
		CanPauseGame = false;
		NewBatsmanIndex = 0;
		currentBall = -1;
		runsScoredInOver = 0;
		IsWicketBall = false;
		batsmanOutIndex = -1;
		inningsCompleted = false;
		levelCompleted = false;
		ReplayStartTime = 1f;
		action = -1;
		WicketsInOver = 0;
		ObscuredPrefs.DeleteKey("partnershipXpGained");
		currentPartnership = 0;
		blinkStatus = 0;
		maxSixes = false;
		isValidBall = true;
		sixInOver = false;
		fourInOver = false;
		ReplayCompletedCalledFromMe = false;
		prevMousePos = default(Vector2);
		maxWidesReached = false;
		NewInnings();
	}

	public void ResetAllLocalVariables()
	{
		if (CONTROLLER.PlayModeSelected == 6)
		{
			CanPauseGame = false;
			IsWicketBall = false;
			inningsCompleted = false;
			levelCompleted = false;
			action = -1;
			WicketsInOver = 0;
			currentBall = -1;
			CONTROLLER.currentMatchBalls = 0;
			CONTROLLER.currentMatchScores = 0;
			CONTROLLER.currentMatchWickets = 0;
		}
	}

	public void DeleteKeys()
	{
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "duckout");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "3wides");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "aifirstsix");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "lastwicket");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "firstwicket");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "lastball");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "CounterFour");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "CounterSix");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "CounterMaiden");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "CounterDot");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "CounterWicketBowled");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "CounterWicketCatch");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "CounterWicketOthers");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "CounterFifty");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "CounterCentury");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "CounterPerOversPlayed");
		ObscuredPrefs.DeleteKey(CONTROLLER.PlayModeSelected + "CounterOppSix");
		CounterFour = (CounterSix = (CounterMaiden = (CounterWicketBowled = (CounterWicketCatch = (CounterWicketOthers = (CounterFifty = (CounterCentury = (CounterPerOversPlayed = (CounterOppSix = 0)))))))));
	}

	public void NewInnings()
	{
		//if (CONTROLLER.PlayModeSelected == 7)
		//{
		//	CONTROLLER.NewInnings = true;
		//	CONTROLLER.GameStartsFromSave = false;
		//	ObscuredPrefs.DeleteKey("perMatchEdgeCount" + CONTROLLER.PlayModeSelected);
		//}
		//CONTROLLER.TeamList[CONTROLLER.myTeamIndex].noofDRSLeft = 2;
		//CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].noofDRSLeft = 2;
		SetTeamIndex();
		//CONTROLLER.RunRate = 0f;
		//CONTROLLER.NewOver = true;
		//CONTROLLER.StrikerIndex = 0;
		//CONTROLLER.NonStrikerIndex = 1;
		//CONTROLLER.continousWickets = 0;
		//CONTROLLER.continousSixes = 0;
		//CONTROLLER.continousBoundaries = 0;
		//Singleton<PreviewScreen>.instance.alertPopup.SetActive(value: false);
		//CONTROLLER.RunsInAOver = 0;
		//CONTROLLER.WideInAOver = 0;
		//CONTROLLER.DotInAOver = 0;
		//if (CONTROLLER.PlayModeSelected == 7)
		//{
		//	ScoreStr = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets;
  //          ExtraStr = LocalizationData.instance.getText(235) + " " + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchExtras;
  //      }
		//else
		//{
		//	ScoreStr = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets;
  //          ExtraStr = LocalizationData.instance.getText(235) + " " + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchExtras;
  //      }
		//if (CONTROLLER.PlayModeSelected != 6)
		//{
		//	OversStr = GetOverStr() + "(" + CONTROLLER.totalOvers + ")";
		//}
		//int num = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 2 * 6 + 1;
		//CONTROLLER.Wagon_PlayedBy = new int[num];
		//CONTROLLER.Wagon_RunScored = new int[num];
		//CONTROLLER.Wagon_NoOfBounce = new int[num];
		//CONTROLLER.Wagon_BallAngle = new float[num];
		//CONTROLLER.Wagon_FirstPitchPoint = new Vector3[num];
		//CONTROLLER.Wagon_SecondPitchPoint = new Vector3[num];
		//CONTROLLER.Wagon_ThirdPitchPoint = new Vector3[num];
		//CONTROLLER.Wagon_FinalPitchPoint = new Vector3[num];
		//CONTROLLER.Wagon_FirstPitchHeight = new float[num];
		//CONTROLLER.Wagon_SecondPitchHeight = new float[num];
		//CONTROLLER.Wagon_ThirdPitchHeight = new float[num];
		groundControllerScript.NewInnings();
		groundControllerScript.ResetFielders();
		if (CONTROLLER.PlayModeSelected == 6)
		{
			introCompleted();
		}
		else if (CONTROLLER.PlayModeSelected == 4)
		{
			Singleton<SuperOverLevelInfo>.instance.ShowMe();
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			Singleton<SuperChaseLevelInfo>.instance.ShowMe();
		}
		else
		{
			ShowGroundPreview();
		}
		Singleton<Tutorial>.instance.hideTutorial();
	}

	private void SetTeamIndex()
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			if (CONTROLLER.GameStartsFromSave)
			{
				return;
			}
			if (CONTROLLER.isFollowOn)
			{
				if (CONTROLLER.currentInnings == 2)
				{
					if (CONTROLLER.meFirstBatting == 1)
					{
						CONTROLLER.BattingTeamIndex = CONTROLLER.opponentTeamIndex;
						CONTROLLER.BowlingTeamIndex = CONTROLLER.myTeamIndex;
						CONTROLLER.BatTeamIndex = 1;
						CONTROLLER.BowlTeamIndex = 0;
					}
					else
					{
						CONTROLLER.BattingTeamIndex = CONTROLLER.myTeamIndex;
						CONTROLLER.BowlingTeamIndex = CONTROLLER.opponentTeamIndex;
						CONTROLLER.BatTeamIndex = 0;
						CONTROLLER.BowlTeamIndex = 1;
					}
				}
				else if (CONTROLLER.BattingTeam[CONTROLLER.currentInnings - 1] == CONTROLLER.myTeamIndex)
				{
					CONTROLLER.BattingTeamIndex = CONTROLLER.opponentTeamIndex;
					CONTROLLER.BowlingTeamIndex = CONTROLLER.myTeamIndex;
					CONTROLLER.BatTeamIndex = 1;
					CONTROLLER.BowlTeamIndex = 0;
				}
				else
				{
					CONTROLLER.BattingTeamIndex = CONTROLLER.myTeamIndex;
					CONTROLLER.BowlingTeamIndex = CONTROLLER.opponentTeamIndex;
					CONTROLLER.BatTeamIndex = 0;
					CONTROLLER.BowlTeamIndex = 1;
				}
			}
			else if (CONTROLLER.currentInnings == 0 || CONTROLLER.currentInnings == 2)
			{
				if (CONTROLLER.currentInnings == 0)
				{
				}
				if (CONTROLLER.meFirstBatting == 1)
				{
					CONTROLLER.BattingTeamIndex = CONTROLLER.myTeamIndex;
					CONTROLLER.BowlingTeamIndex = CONTROLLER.opponentTeamIndex;
					CONTROLLER.BatTeamIndex = 0;
					CONTROLLER.BowlTeamIndex = 1;
				}
				else
				{
					CONTROLLER.BattingTeamIndex = CONTROLLER.opponentTeamIndex;
					CONTROLLER.BowlingTeamIndex = CONTROLLER.myTeamIndex;
					CONTROLLER.BatTeamIndex = 1;
					CONTROLLER.BowlTeamIndex = 0;
				}
			}
			else if (CONTROLLER.currentInnings == 1 || CONTROLLER.currentInnings == 3)
			{
				if (CONTROLLER.meFirstBatting == 1)
				{
					CONTROLLER.BattingTeamIndex = CONTROLLER.opponentTeamIndex;
					CONTROLLER.BowlingTeamIndex = CONTROLLER.myTeamIndex;
					CONTROLLER.BatTeamIndex = 1;
					CONTROLLER.BowlTeamIndex = 0;
				}
				else
				{
					CONTROLLER.BattingTeamIndex = CONTROLLER.myTeamIndex;
					CONTROLLER.BowlingTeamIndex = CONTROLLER.opponentTeamIndex;
					CONTROLLER.BatTeamIndex = 0;
					CONTROLLER.BowlTeamIndex = 1;
				}
			}
			if (CONTROLLER.currentInnings == 0 && !CONTROLLER.GameStartsFromSave)
			{
				ResetTM_PlayerScores();
				ResetPlayerScores();
			}
			if (CONTROLLER.currentInnings == 2 && !CONTROLLER.GameStartsFromSave)
			{
				ResetTM_PlayerScores();
				ResetPlayerScores();
			}
			CONTROLLER.BattingTeam[CONTROLLER.currentInnings] = CONTROLLER.BattingTeamIndex;
			CONTROLLER.BowlingTeam[CONTROLLER.currentInnings] = CONTROLLER.BowlingTeamIndex;
			CONTROLLER.oversSelectedIndex = ObscuredPrefs.GetInt("TMOvers");
			CONTROLLER.totalOvers = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex];
			SetTarget();
			CONTROLLER.wickerKeeperIndex = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].KeeperIndex;
			CONTROLLER.BattingTeamName = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName;
			CONTROLLER.BowlingTeamName = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName;
		}
		else
		{
			if (CONTROLLER.PlayModeSelected == 6)
			{
				CONTROLLER.BattingTeamIndex = CONTROLLER.myTeamIndex;
				CONTROLLER.BowlingTeamIndex = CONTROLLER.opponentTeamIndex;
			}
			else if (CONTROLLER.PlayModeSelected == 4)
			{
				CONTROLLER.totalOvers = 1;
				if (CONTROLLER.SuperOverMode == "bat")
				{
					CONTROLLER.BattingTeamIndex = CONTROLLER.myTeamIndex;
					CONTROLLER.BowlingTeamIndex = CONTROLLER.opponentTeamIndex;
				}
				else
				{
					CONTROLLER.BattingTeamIndex = CONTROLLER.opponentTeamIndex;
					CONTROLLER.BowlingTeamIndex = CONTROLLER.myTeamIndex;
				}
			}
			else if (CONTROLLER.PlayModeSelected == 5 || CONTROLLER.PlayModeSelected == 7)
			{
				CONTROLLER.BattingTeamIndex = CONTROLLER.myTeamIndex;
				CONTROLLER.BowlingTeamIndex = CONTROLLER.opponentTeamIndex;
			}
			else if (CONTROLLER.currentInnings == 0)
			{
				Singleton<Scoreboard>.instance.ShowTargetScreen(boolean: false);
				if (CONTROLLER.meFirstBatting == 1)
				{
					CONTROLLER.BattingTeamIndex = CONTROLLER.myTeamIndex;
					CONTROLLER.BowlingTeamIndex = CONTROLLER.opponentTeamIndex;
				}
				else
				{
					CONTROLLER.BattingTeamIndex = CONTROLLER.opponentTeamIndex;
					CONTROLLER.BowlingTeamIndex = CONTROLLER.myTeamIndex;
				}
			}
			else
			{
				if (CONTROLLER.meFirstBatting == 1)
				{
					CONTROLLER.BattingTeamIndex = CONTROLLER.opponentTeamIndex;
					CONTROLLER.BowlingTeamIndex = CONTROLLER.myTeamIndex;
				}
				else
				{
					CONTROLLER.BattingTeamIndex = CONTROLLER.myTeamIndex;
					CONTROLLER.BowlingTeamIndex = CONTROLLER.opponentTeamIndex;
				}
				Singleton<Scoreboard>.instance.ShowTargetScreen(boolean: true);
			}
			if (CONTROLLER.ConfidenceVal == 1)
			{
			}
			if (CONTROLLER.PlayModeSelected == 6)
			{
				CONTROLLER.totalOvers = Multiplayer.overs;
				PowerPlayOver = (int)Mathf.Floor((float)CONTROLLER.totalOvers * 0.3f);
				SlogOverValue = (int)((float)CONTROLLER.totalOvers - Mathf.Floor((float)CONTROLLER.totalOvers * 0.2f));
			}
			else if (CONTROLLER.PlayModeSelected == 4)
			{
				CONTROLLER.totalOvers = 1;
				PowerPlayOver = 0;
				SlogOverValue = (int)((float)CONTROLLER.totalOvers - Mathf.Floor((float)CONTROLLER.totalOvers * 0.2f));
			}
			else if (CONTROLLER.PlayModeSelected == 5)
			{
				CONTROLLER.totalOvers = CONTROLLER.Overs[CONTROLLER.CTLevelId];
				CONTROLLER.InningsCompleted = false;
				Singleton<LoadPlayerPrefs>.instance.SetArrayForChaseTarget();
			}
			else
			{
				CONTROLLER.totalOvers = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex];
				PowerPlayOver = (int)Mathf.Floor((float)CONTROLLER.totalOvers * 0.3f);
				SlogOverValue = (int)((float)CONTROLLER.totalOvers - Mathf.Floor((float)CONTROLLER.totalOvers * 0.2f));
			}
			//CONTROLLER.wickerKeeperIndex = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].KeeperIndex;
			//CONTROLLER.BattingTeamName = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].teamName;
			//CONTROLLER.BowlingTeamName = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].teamName;
		}
		Singleton<Scoreboard>.instance.HideStrip(boolean: true);
	}

	public void ShowGroundPreview()
	{
		action = -20;
		Singleton<Intro>.instance.initGameIntro();
		Singleton<ChangePlayerTexture>.instance.ChangeTexture();
	}

	public void introCompleted()
	{
		action = -1;
		CONTROLLER.StrikerIndex = 0;
		CONTROLLER.NonStrikerIndex = 1;
		NewBatsmanIndex = CONTROLLER.NonStrikerIndex;
		//CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.Status = "not out";
		//CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.Status = "not out";
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex, "not out");
			TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, CONTROLLER.NonStrikerIndex, "not out");
		}
		Singleton<BattingScoreCard>.instance.ResetBattingCard();
		//CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.Status = "not out";
		//CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.Status = "not out";
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex, "not out");
			TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, CONTROLLER.NonStrikerIndex, "not out");
		}
		Singleton<BowlingScoreCard>.instance.ResetBowlingCard();
		Singleton<BowlingScoreCard>.instance.OverCompleted();
		Singleton<Scoreboard>.instance.NewOver();
		Singleton<Intro>.instance.groundModel.transform.eulerAngles = Vector3.zero;
		Singleton<Intro>.instance.quad.SetActive(value: true);
		Singleton<GroundController>.instance.StopIntroFielderAnimation();
		Invoke("IntroOver", 0f);
		Cutscenes.instance.EndCutscene();
	}

	public void IntroOver()
	{
		if (Singleton<Intro>.instance != null)
		{
			Singleton<Intro>.instance.destroyGO();
		}
		Singleton<BatsmanRecord>.instance.Hide(boolean: true);
		groundControllerScript.introCompleted();
		if (CONTROLLER.PlayModeSelected == 6 || CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5)
		{
			Singleton<BowlingScoreCard>.instance.Continue();
		}
		else
		{
			ShowBattingScoreCard();
		}
		Invoke("HideFadeView", fadeTime);
	}

	public void HideFadeView()
	{
	}

	private void InitBatsmanInfo()
	{
		CanPauseGame = false;
		Singleton<BatsmanInfo>.instance.UpdateRecord(CONTROLLER.BattingTeamIndex, batsmanOutIndex);
		Invoke("ShowBatsmamInfoView", fadeTime);
	}

	public void ShowBatsmamInfoView()
	{
		Singleton<Scoreboard>.instance.HidePause(boolean: true);
		Singleton<Scoreboard>.instance.Hide(boolean: true);
		Singleton<PreviewScreen>.instance.Hide(boolean: true);
		Singleton<BatsmanInfo>.instance.Hide(boolean: false);
		action = 3;
		Singleton<Intro>.instance.initBatsmanExit();
	}

	public void batsmanExitStopped()
	{
		action = -1;
		Invoke("stoppedExitView", fadeTime);
	}

	public void stoppedExitView()
	{
		Singleton<BatsmanInfo>.instance.Hide(boolean: true);
		if (CONTROLLER.PlayModeSelected == 6)
		{
			return;
		}
		Singleton<Scoreboard>.instance.UpdateScoreCard();
		if (CONTROLLER.currentInnings < 2)
		{
			Singleton<BattingScoreCard>.instance.SelectedInnings = 1;
		}
		else
		{
			Singleton<BattingScoreCard>.instance.SelectedInnings = 2;
		}
		Singleton<BattingScoreCard>.instance.UpdateWicket(batsmanOutIndex);
		Singleton<BattingScoreCard>.instance.UpdateScoreCard();
		Singleton<BowlingScoreCard>.instance.UpdateScoreCard();
		bool flag = CheckForAllOut();
		bool flag2 = CheckForInningsComplete();
		if (CONTROLLER.PlayModeSelected == 4)
		{
			flag = ((CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets >= 2) ? true : false);
			flag2 = ((CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls >= 6) ? true : false);
		}
		if (!flag && !flag2)
		{
			InitBatsmanRecord();
			return;
		}
		if (Singleton<Intro>.instance != null)
		{
			Singleton<Intro>.instance.destroyGO();
		}
		groundControllerScript.introCompleted();
		CheckForOverComplete();
	}

	private void InitBatsmanRecord()
	{
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[NewBatsmanIndex].BatsmanList.Status = "not out";
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, NewBatsmanIndex, "not out");
		}
		Singleton<BatsmanRecord>.instance.UpdateRecord(CONTROLLER.BattingTeamIndex, NewBatsmanIndex);
		ShowBatsmanRecord();
	}

	private void ShowBatsmanRecord()
	{
		if (Singleton<Intro>.instance != null)
		{
			Singleton<Intro>.instance.initBatsmanEntry();
		}
		else
		{
			Singleton<Intro>.instance.initBatsmanEntry();
		}
		Singleton<BatsmanRecord>.instance.Hide(boolean: false);
		Invoke("HoldRecordView", fadeTime);
	}

	public void HoldRecordView()
	{
		action = 2;
	}

	public void batsmanEntryStopped()
	{
		action = -1;
		Invoke("BatsmanEntryView", fadeTime);
	}

	public void BatsmanEntryView()
	{
		if (Singleton<Intro>.instance != null)
		{
			Singleton<Intro>.instance.groundModel.transform.eulerAngles = Vector3.zero;
			Singleton<Intro>.instance.quad.SetActive(value: true);
			Singleton<Intro>.instance.destroyGO();
		}
		Singleton<PreviewScreen>.instance.Hide(boolean: false);
		Singleton<BatsmanRecord>.instance.Hide(boolean: true);
		groundControllerScript.introCompleted();
		CheckForOverComplete();
		Invoke("BatsmanInfoView", fadeTime);
	}

	public void BatsmanInfoView()
	{
	}

	protected void Update()
	{
		if (gameQuit || SkipUpdate || !CONTROLLER.GameIsOnFocus)
		{
			return;
		}
		switch (action)
		{
		case -20:
			if (Singleton<Intro>.instance != null)
			{
				Singleton<Intro>.instance.UpdateIntro();
			}
			break;
		case 2:
			if (Singleton<Intro>.instance != null)
			{
				Singleton<Intro>.instance.UpdateIntro();
			}
			break;
		case 3:
			if (Singleton<Intro>.instance != null)
			{
				Singleton<Intro>.instance.UpdateIntro();
			}
			break;
		}
		GetKeyBoardInput();
		if (!Application.isEditor && CONTROLLER.TargetPlatform != "standalone" && CONTROLLER.TargetPlatform != "web")
		{
			switch (userAction)
			{
			case 10:
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					DetectBatsmanMove();
				}
				break;
			case 11:
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					DetectBatsmanShot();
				}
				break;
			}
			if (CanPauseGame && !CONTROLLER.ReplayShowing)
			{
				Singleton<Scoreboard>.instance.HidePause(boolean: false);
			}
			else
			{
				Singleton<Scoreboard>.instance.HidePause(boolean: true);
			}
		}
		else
		{
			switch (userAction)
			{
			case 10:
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					DetectBatsmanMoveMouse();
				}
				break;
			case 11:
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					DetectBatsmanShotMouse();
				}
				break;
			}
		}
		if (action == 2 || action == 3 || action == -20 || Singleton<BallSimulationManager>.instance.showingBallSimulation)
		{
			SkipObj.SetActive(value: true);
		}
		else
		{
			SkipObj.SetActive(value: false);
		}
		if (CONTROLLER.ReplayShowing || skipStatus == "stumpingReplay")
		{
			BlinkActionReplay();
		}
	}

	public void BlinkActionReplay()
	{
		if (timeOutTextBlinkTime + 0.25f < Time.time)
		{
			timeOutTextBlinkTime = Time.time;
			if (blinkStatus == 0)
			{
				ActionTxt.color = new Color(1f, 0f, 0f, 1f);
				blinkStatus = 1;
			}
			else
			{
				blinkStatus = 0;
				ActionTxt.color = new Color(1f, 1f, 1f, 1f);
			}
		}
		if (ReplayStartTime + 2f < Time.time && ReplayStartTime > 0f)
		{
			ReplayStartTime = -1f;
			if (skipStatus == "stumpingReplay" || skipStatus == "thirdUmpireRunoutReplay")
			{
				ActionTxt.text = LocalizationData.instance.getText(518);
			}
			else
			{
				ActionTxt.text = LocalizationData.instance.getText(519);
			}
		}
	}

	public void ShowBattingScoreCard()
	{
		//Singleton<NavigationBack>.instance.disableDeviceBack = false;
		groundControllerScript.showPreviewCamera(status: false);
		if (CONTROLLER.PlayModeSelected != 6)
		{
			fill.fillAmount = 0f;
			scrambledScore.text = "0";
			scrambledWickets.text = "0";
			generatingScore.SetActive(value: false);
			if (!isGamePaused)
			{
				action = 0;
			}
			Singleton<PauseGameScreen>.instance.Hide(boolean: true);
			CanPauseGame = false;
			groundControllerScript.slipShot = false;
			if (CONTROLLER.PlayModeSelected == 4 && CONTROLLER.SuperOverMode == "bowl")
			{
				Singleton<BowlingScoreCard>.instance.Hide(boolean: false);
				Singleton<BowlingScoreCard>.instance.UpdateScoreCard();
				Singleton<BowlingScoreCard>.instance.SCTeamName.DOFade(1f, 0f);
			}
			else if (CONTROLLER.PlayModeSelected == 5)
			{
				//Singleton<AdIntegrate>.instance.DisplayInterestialAd();
				Singleton<PauseGameScreen>.instance.BG.SetActive(value: true);
				Singleton<BattingScoreCard>.instance.Hide(boolean: false);
				Singleton<BattingScoreCard>.instance.UpdateScoreCard();
				Singleton<BattingScoreCard>.instance.SCTeamName.DOFade(1f, 0f);
				fromMainMenu = false;
			}
			//else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls == 0 || inningsCompleted || fromMainMenu || CONTROLLER.PlayModeSelected > 3)
			//{
			//	Singleton<PauseGameScreen>.instance.BG.SetActive(value: true);
			//	Singleton<BattingScoreCard>.instance.Hide(boolean: false);
			//	Singleton<BattingScoreCard>.instance.UpdateScoreCard();
			//	Singleton<BattingScoreCard>.instance.SCTeamName.DOFade(1f, 0f);
			//	fromMainMenu = false;
			//}
			else
			{
				Singleton<AfterOverSummary>.instance.ShowMe();
			}
		}
	}

	public void ShowScoreCard()
	{
		action = -1;
		if (CONTROLLER.isFromAutoPlay)
		{
			AutoSave.SaveInGameMatch();
		}
		if (!inningsCompleted)
		{
			SetFielders();
			isGamePaused = false;
			groundControllerScript.CanShowCountDown = false;
			groundControllerScript.GameIsPaused(pauseStatus: false);
			Singleton<BowlingScoreCard>.instance.ContinueSelected();
			Singleton<Scoreboard>.instance.UpdateScoreCard();
			BowlNextBall();
			return;
		}
		CanPauseGame = false;
		Singleton<Scoreboard>.instance.HidePause(boolean: true);
		if (CONTROLLER.currentInnings == 0 || CONTROLLER.PlayModeSelected == 7)
		{
			Singleton<Scoreboard>.instance.Hide(boolean: true);
			Singleton<PreviewScreen>.instance.Hide(boolean: true);
			Singleton<BowlingControls>.instance.Hide(boolean: true);
			Singleton<BattingControls>.instance.Hide(boolean: true);
			if (!(CONTROLLER.STORE != "facebook"))
			{
				return;
			}
			if (CONTROLLER.PlayModeSelected == 7)
			{
				if (CONTROLLER.currentInnings == 1)
				{
					if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].TMcurrentMatchScores1 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores1 >= CONTROLLER.followOnTarget)
					{
						if (CONTROLLER.currentInnings != 0 && CONTROLLER.currentInnings != 2)
						{
							if (CONTROLLER.currentInnings == 1)
							{
								CanPauseGame = false;
								Singleton<TMFollowOn>.instance.ShowMe();
								AutoSave.SaveInGameMatch();
							}
							else
							{
								showTargetScreen();
							}
							return;
						}
						int num = UnityEngine.Random.Range(0, 3);
						if (num != 2)
						{
							CanPauseGame = false;
							Singleton<TMFollowOn>.instance.ShowMe();
							AutoSave.SaveInGameMatch();
						}
						else
						{
							showTargetScreen();
						}
					}
					else
					{
						showTargetScreen();
					}
				}
				else
				{
					SetTargetToWin();
					showTargetScreen();
				}
			}
			else
			{
				Singleton<TargetScreen>.instance.Hide(boolean: false);
				action = 10;
			}
		}
		else
		{
			CONTROLLER.gameCompleted = true;
			Singleton<Scoreboard>.instance.Hide(boolean: true);
			Singleton<PreviewScreen>.instance.Hide(boolean: true);
			Singleton<BowlingControls>.instance.Hide(boolean: true);
			Singleton<BattingControls>.instance.Hide(boolean: true);
		}
	}

	public void BowlNextBall()
	{
		fromMainMenu = false;
		canShowAnimation = true;
		savedNonStrikerIndex = CONTROLLER.NonStrikerIndex;
		savedStrikerIndex = CONTROLLER.StrikerIndex;
		savedNewStrikerIndex = NewBatsmanIndex;
		NewTouch = false;
		TouchInitPos = Vector2.zero;
		TouchEndPos = Vector2.zero;
		selectedAngle = 0;
		shotCompleted = false;
		userAction = -1;
		prevMousePos = Vector2.zero;
		NewBall();
		SetGameDatas();
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			groundControllerScript.BowlNextBall("user", "computer");
		}
		else
		{
			groundControllerScript.BowlNextBall("computer", "user");
		}
		Singleton<BattingControls>.instance.LoftMeterFill(CONTROLLER.StrikerIndex);
		if (CONTROLLER.fielderPrevIndex != CONTROLLER.fielderChangeIndex)
		{
			CONTROLLER.fielderPrevIndex = CONTROLLER.fielderChangeIndex;
		}
	}

	private void NewBall()
	{
		groundControllerScript.ClearTrace();
		if (!IsWicketBall)
		{
			CONTROLLER.HattrickBall = false;
		}
		batsmanOutIndex = -1;
		IsWicketBall = false;
		shotCompleted = false;
		if (CONTROLLER.PlayModeSelected == 6)
		{
			ScoreBoardMultiPlayer.instance.Hide(boolean: false);
		}
		else
		{
			Singleton<Scoreboard>.instance.Hide(boolean: false);
		}
		Singleton<PreviewScreen>.instance.Hide(boolean: false);
	}

	private void SetGameDatas()
	{
		BatsmenInfo batsmanList = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList;
		if (batsmanList.BattingHand == "L")
		{
			CONTROLLER.StrikerHand = "left";
		}
		else if (batsmanList.BattingHand == "R")
		{
			CONTROLLER.StrikerHand = "right";
		}
		BowlerInfo bowlerList = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList;
		if (bowlerList.BowlingHand == "L")
		{
			CONTROLLER.BowlerHand = "left";
		}
		else if (bowlerList.BowlingHand == "R")
		{
			CONTROLLER.BowlerHand = "right";
		}
		if (CONTROLLER.PlayModeSelected == 4)
		{
			if (CONTROLLER.SuperOverMode == "bat")
			{
				if (CONTROLLER.LevelId % 2 == 0)
				{
					CONTROLLER.BowlerType = 0;
				}
				else
				{
					CONTROLLER.BowlerType = UnityEngine.Random.Range(1, 3);
				}
			}
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			CONTROLLER.BowlerType = UnityEngine.Random.Range(0, 3);
		}
		else
		{
			CONTROLLER.BowlerType = int.Parse(CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.bowlingRank);
		}
	}

	public void ShowBowlingInterface(bool boolean)
	{
		if (gameQuit)
		{
			return;
		}
		if (boolean)
		{
			if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
			{
				bowlingControl = 0;
			}
			action = 4;
			Singleton<Scoreboard>.instance.HideStrip(boolean: false);
			Singleton<Scoreboard>.instance.BowlerToBatsman();
			Singleton<BowlingControls>.instance.Hide(boolean: false);
			Singleton<BowlingControls>.instance.ActivateBowlingControls();
			if (CONTROLLER.myTeamIndex == CONTROLLER.BowlingTeamIndex)
			{
				Singleton<PreviewScreen>.instance.hideBtns(boolean: true);
			}
		}
		else
		{
			action = -1;
			if (CONTROLLER.currentInnings == 1)
			{
				Singleton<Scoreboard>.instance.HideStrip(boolean: false);
				Singleton<Scoreboard>.instance.TargetToWin();
			}
			else
			{
				Singleton<Scoreboard>.instance.HideStrip(boolean: true);
			}
			Singleton<BowlingControls>.instance.Hide(boolean: true);
		}
	}

	public void speedLocked()
	{
		groundControllerScript.BlockBowlerSideChange();
		Singleton<PreviewScreen>.instance.hideBtns(boolean: false);
	}

	private float AngleBetweenTwoVector3(Vector3 v31, Vector3 v32)
	{
		float num = 180f / (float)Math.PI;
		float y = v31.x - v32.x;
		float x = v31.z - v32.z;
		float num2 = Mathf.Atan2(y, x) * num;
		return (270f - num2 + 360f) % 360f;
	}

	public void GetFinalBallAngle(float angle)
	{
		ballAngle = angle;
		if (ballAngle > 360f)
		{
			ballAngle -= 360f;
		}
	}

	private bool CanCalculateRuns()
	{
		if (CONTROLLER.ChallengeNumber == 0)
		{
			if ((ballAngle < 90f && ballAngle >= 0f) || (ballAngle <= 360f && ballAngle > 270f))
			{
				return true;
			}
		}
		else if (CONTROLLER.ChallengeNumber == 1 && ballAngle < 270f && ballAngle > 90f)
		{
			return true;
		}
		return false;
	}

	private string setToDecimal(float floatValue)
	{
		return floatValue.ToString("F1");
	}

	public void UpdateCurrentBall(int validBall, int canCountBall, int runsScored, int extraRun, int batsmanID, int isWicket, int wicketType, int bowlerID, int catcherID, int batsmanOut, bool isBoundary)
	{
		if (groundControllerScript.UltraEdgeCutscenePlaying && !groundControllerScript.getOverStepBall() && !groundControllerScript.lineFreeHit && (groundControllerScript.UserCanAskReview || groundControllerScript.AiCanAskReview) && groundControllerScript.umpireAnimationPlayed)
		{
			groundControllerScript.validBall = validBall;
			groundControllerScript.canCountBall = canCountBall;
			groundControllerScript.runsScored = runsScored;
			groundControllerScript.extraRun = extraRun;
			groundControllerScript.batsmanID = batsmanID;
			groundControllerScript.isWicket = isWicket;
			groundControllerScript.wicketType = wicketType;
			groundControllerScript.bowlerID = bowlerID;
			groundControllerScript.catcherID = catcherID;
			groundControllerScript.batsmanOut = batsmanOut;
			groundControllerScript.isBoundary = isBoundary;
			Time.timeScale = 0f;
			Singleton<ReviewSystem>.instance.ShowUltraEdgeReview();
			Singleton<ReviewSystem>.instance.StartTimer();
			return;
		}
		shownRv = false;
		EnableRun(boolean: false);
		RvRunsScored = runsScored;
		templastBowledBall = groundControllerScript.lastBowledBall;
		if (CONTROLLER.PlayModeSelected != 6 )
		{
			RVStatus = string.Empty;
			if (Singleton<GroundController>.instance.IsWide() && isWicket == 0 && CONTROLLER.WideInAOver > 0 && CONTROLLER.WideInAOver % 2 == 0 && !ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "3wides") && CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
			{
				RvRunsScored = extraRun;
				if (CONTROLLER.PlayModeSelected < 6)
				{
					SetRVStatus("3wides");
				}
			}
			if (validBall == 1)
			{
				if (runsScored == 6 && CONTROLLER.BattingTeamIndex == CONTROLLER.opponentTeamIndex && CounterOppSix == 0)
				{
					RvRunsScored = runsScored;
					if (CONTROLLER.PlayModeSelected < 6)
					{
						SetRVStatus("aifirstsix");
					}
				}
				if ((CONTROLLER.currentInnings == 1 || CONTROLLER.PlayModeSelected == 5) && CONTROLLER.PlayModeSelected != 4 && (float)CONTROLLER.totalOvers * 6f == (float)(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls + 1) && !ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "lastball") && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex && RVStatus == string.Empty && CONTROLLER.TargetToChase - CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores + runsScored <= 6 && CONTROLLER.TargetToChase - CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores + runsScored > 0)
				{
					RvRunsScored = runsScored;
					if (CONTROLLER.PlayModeSelected < 6)
					{
						SetRVStatus("lastball");
					}
				}
			}
		}
		if (validBall == 1 && Singleton<BallSimulationManager>.instance.CanShowBallSimulation())
		{
			string data = ((isWicket == 1) ? "wicket" : ((runsScored > 4) ? "six" : ((runsScored > 3) ? "four" : ((runsScored <= 0) ? "dot" : "run"))));
			Singleton<BallSimulationManager>.instance.SetBallSimulationData(data, groundControllerScript.currentBowlerType, groundControllerScript.bowlerSide, groundControllerScript.currentBowlerHand);
		}
		bool overStepBall = groundControllerScript.getOverStepBall();
		bool flag = Singleton<GroundController>.instance.IsWide();
		bool flag2 = true;
		if (CONTROLLER.PlayModeSelected == 6)
		{
			flag2 = false;
		}
		Singleton<Scoreboard>.instance.HideStrip(boolean: true);
		bool flag3 = false;
		if (gameQuit)
		{
			return;
		}
		if (CONTROLLER.isConfidenceLevel || CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			IncreaseConfidenceLevel(runsScored, validBall);
		}
		if (Singleton<AIFieldingSetupManager>.instance.enabled && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex && runsScored > 2)
		{
			Singleton<AIFieldingSetupManager>.instance.SaveStrikerHittingDirections();
		}
		Singleton<UILookAt>.instance.show(flag: false);
		if (validBall == 1)
		{
			isValidBall = true;
			currentBall++;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls++;
			if (CONTROLLER.PlayModeSelected == 7)
			{
				CONTROLLER.ballsBowledPerDay++;
				if (CONTROLLER.currentInnings < 2)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls1++;
				}
				else
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls2++;
				}
			}
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.BallsBowled++;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[currentBall] = string.Empty + (runsScored + extraRun);
			if (isWicket == 1)
			{
				Singleton<ScoreBoardBallList>.instance.AddBall("W", " ");
			}
			else if (extraRun >= 4 && groundControllerScript.getWicketKeeperStatus() == "catchMissed")
			{
				Singleton<ScoreBoardBallList>.instance.AddBall(string.Empty + (runsScored + extraRun), "b");
			}
			else
			{
				Singleton<ScoreBoardBallList>.instance.AddBall(string.Empty + (runsScored + extraRun), " ");
			}
			if (CONTROLLER.myTeamIndex != CONTROLLER.BattingTeamIndex)
			{
			}
		}
		else
		{
			isValidBall = false;
			if (flag && overStepBall)
			{
				if (isWicket == 1)
				{
					Singleton<ScoreBoardBallList>.instance.AddBall(string.Empty + "W", string.Empty + "nb");
				}
				else
				{
					Singleton<ScoreBoardBallList>.instance.AddBall(string.Empty + (extraRun - 1), string.Empty + "nb");
				}
			}
			else if (overStepBall)
			{
				if (isWicket == 1)
				{
					Singleton<ScoreBoardBallList>.instance.AddBall(string.Empty + "W", string.Empty + "nb");
				}
				else
				{
					Singleton<ScoreBoardBallList>.instance.AddBall(string.Empty + runsScored, string.Empty + "nb");
				}
			}
			else if (flag)
			{
				if (isWicket == 1)
				{
					Singleton<ScoreBoardBallList>.instance.AddBall(string.Empty + "W", string.Empty + "wd");
				}
				else
				{
					Singleton<ScoreBoardBallList>.instance.AddBall(string.Empty + (extraRun - 1), string.Empty + "wd");
				}
			}
		}
		if (canCountBall == 1)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].BatsmanList.BallsPlayed++;
		}
		CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.RunsGiven += runsScored;
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores += runsScored;
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].BatsmanList.RunsScored += runsScored;
		if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].BatsmanList.RunsScored >= 100 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].BatsmanList.RunsScored % 50 >= 10 && CONTROLLER.PlayModeSelected != 2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].scoredHundredPlus)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].scoredHundredPlus = false;
		}
		if (CONTROLLER.PlayModeSelected == 7)
		{
			if (CONTROLLER.currentInnings < 2)
			{
				if (TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, batsmanID) >= 100 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, batsmanID) % 50 >= 10 && CONTROLLER.PlayModeSelected != 2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].TMscoredHundredPlus1)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].TMscoredHundredPlus1 = false;
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].BatsmanList.TMRunsScored2 >= 100 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].BatsmanList.TMRunsScored2 % 50 >= 10 && CONTROLLER.PlayModeSelected != 2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].TMscoredHundredPlus2)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].TMscoredHundredPlus2 = false;
			}
		}
		if (validBall == 1 || overStepBall)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].BatsmanList.currPatnerShipRuns += runsScored;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].BatsmanList.currPatnerShipBalls++;
		}
		CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.RunsGiven += extraRun;
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores += extraRun;
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TestMatchBatsman.SetRunsScored(CONTROLLER.BattingTeamIndex, batsmanID, runsScored);
			if (CONTROLLER.currentInnings < 2)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores1 += extraRun;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores1 += runsScored;
			}
			else
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores2 += extraRun;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchScores2 += runsScored;
			}
			if (canCountBall == 1)
			{
				TestMatchBatsman.SetBallsPlayed(CONTROLLER.BattingTeamIndex, batsmanID, 1);
			}
			if (CONTROLLER.currentInnings < 2)
			{
				CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.TMRunsGiven1 += runsScored;
				CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.TMRunsGiven1 += extraRun;
				if (isValidBall)
				{
					CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.TMBallsBowled1++;
				}
			}
			else
			{
				CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.TMRunsGiven2 += runsScored;
				CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.TMRunsGiven2 += extraRun;
				if (isValidBall)
				{
					CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.TMBallsBowled2++;
				}
			}
		}
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchExtras += extraRun;
		runsScoredInOver += runsScored;
		runsScoredInOver += extraRun;
		if (runsScored == 0 && extraRun == 0)
		{
			if (isValidBall)
			{
				CONTROLLER.continousSixes = 0;
				CONTROLLER.continousBoundaries = 0;
			}
			if (isWicket == 0)
			{
				CONTROLLER.continousWickets = 0;
				if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
				{
					CounterDot++;
					ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterDot", CounterDot);
				}
			}
			CONTROLLER.DotInAOver++;
			if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
		if (isWicket == 0)
		{
			CONTROLLER.continousWickets = 0;
		}
		if (extraRun > 0 && canCountBall == 0)
		{
			CONTROLLER.continousWickets = 0;
			CONTROLLER.WideInAOver++;
		}
		ScoreStr = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets;
		ExtraStr = LocalizationData.instance.getText(235) + " " + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchExtras;
		if (CONTROLLER.PlayModeSelected == 6)
		{
			OversStr = GetOverStr();
		}
		else
		{
			OversStr = GetOverStr() + "(" + CONTROLLER.totalOvers + ")";
		}
		if (isWicket == 1)
		{
			if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
			{
				KitTable.SetKitValues(4);
			}
			if (isValidBall)
			{
				CONTROLLER.continousWickets++;
				CONTROLLER.continousSixes = 0;
				CONTROLLER.continousBoundaries = 0;
			}
			switch (wicketType)
			{
			case 1:
				if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
				{
					CounterWicketBowled++;
					ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterWicketBowled", CounterWicketBowled);
				}
				break;
			case 3:
			case 5:
				if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
				{
					CounterWicketCatch++;
					ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterWicketCatch", CounterWicketCatch);
				}
				break;
			case 2:
			case 4:
			case 6:
				if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
				{
					CounterWicketOthers++;
					ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterWicketOthers", CounterWicketOthers);
				}
				break;
			}
			WicketBall(validBall, batsmanID, wicketType, bowlerID, catcherID, batsmanOut);
		}
		else if (isBoundary)
		{
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.WicketsInBallCount = 0;
			flag3 = ((CONTROLLER.PlayModeSelected == 7) ? checkForCelebration(runsScored, batsmanID) : checkForCelebration(runsScored, batsmanID));
			if (CONTROLLER.currentInnings < 2)
			{
				Singleton<BattingScoreCard>.instance.SelectedInnings = 1;
			}
			else
			{
				Singleton<BattingScoreCard>.instance.SelectedInnings = 2;
			}
			Singleton<BattingScoreCard>.instance.UpdateScoreCard();
			Singleton<BowlingScoreCard>.instance.UpdateScoreCard();
			switch (runsScored)
			{
			case 4:
				CONTROLLER.totalFours++;
				CONTROLLER.continousSixes = 0;
				CONTROLLER.continousBoundaries++;
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					KitTable.SetKitValues(3);
					CounterFour++;
					ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterFour", CounterFour);
				}
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].BatsmanList.Fours++;
				if (CONTROLLER.PlayModeSelected == 7)
				{
					TestMatchBatsman.SetFours(CONTROLLER.BattingTeamIndex, batsmanID, 1);
				}
				if (!flag3 && !overStepBall)
				{
					InitAnimation(0);
				}
				break;
			case 6:
				CONTROLLER.totalSixes++;
				CONTROLLER.continousSixes++;
				CONTROLLER.continousBoundaries = 0;
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					KitTable.SetKitValues(2);
					CounterSix++;
					int num = (ObscuredPrefs.HasKey("ArcadeSixes") ? ObscuredPrefs.GetInt("ArcadeSixes") : 0);
					num++;
					ObscuredPrefs.SetInt("ArcadeSixes", num);
					SavePlayerPrefs.SaveSixCount();
					ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterSix", CounterSix);
				}
				else if (CONTROLLER.BattingTeamIndex == CONTROLLER.opponentTeamIndex)
				{
					CounterOppSix++;
					ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterOppSix", CounterOppSix);
				}
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].BatsmanList.Sixes++;
				if (CONTROLLER.PlayModeSelected == 7)
				{
					TestMatchBatsman.SetSixes(CONTROLLER.BattingTeamIndex, batsmanID, 1);
				}
				if (!flag3 && !overStepBall)
				{
					InitAnimation(1);
				}
				break;
			}
			Singleton<Scoreboard>.instance.UpdateScoreCard();
		}
		else
		{
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.WicketsInBallCount = 0;
			if (CONTROLLER.currentInnings < 2)
			{
				Singleton<BattingScoreCard>.instance.SelectedInnings = 1;
			}
			else
			{
				Singleton<BattingScoreCard>.instance.SelectedInnings = 2;
			}
			Singleton<BattingScoreCard>.instance.UpdateScoreCard();
			Singleton<BowlingScoreCard>.instance.UpdateScoreCard();
			if (runsScored == 1 || runsScored == 3)
			{
				int strikerIndex = CONTROLLER.StrikerIndex;
				CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
				CONTROLLER.NonStrikerIndex = strikerIndex;
			}
			flag2 = false;
			Singleton<Scoreboard>.instance.UpdateScoreCard();
		}
		if (CONTROLLER.myTeamIndex == CONTROLLER.BattingTeamIndex)
		{
			currentPartnership += runsScored;
			currentPartnership += extraRun;
		}
		AutoSave.ballUpdate = string.Empty;
		for (int i = 0; i < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate.Length; i++)
		{
			AutoSave.ballUpdate += CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[i];
		}
		Singleton<ScoreBoardBallList>.instance.SaveBallList();
		CONTROLLER.isFreeHitBall = groundControllerScript.getOverStepBall();
		if (CONTROLLER.isFreeHitBall)
		{
			CONTROLLER.noBallFacedBatsmanId = batsmanID;
		}
		flag3 = ((CONTROLLER.PlayModeSelected == 7) ? checkForCelebration(runsScored, batsmanID) : checkForCelebration(runsScored, batsmanID));
		if (!flag2)
		{
			if (!flag3 || CONTROLLER.PlayModeSelected == 6)
			{
				CurrentBallUpdate(canCountBall, extraRun);
				CheckForOverComplete();
			}
			else
			{
				StartCoroutine(waitForBatsmanCelebration(runsScored, canCountBall, extraRun));
			}
		}
		else if (!flag3 || CONTROLLER.PlayModeSelected == 6)
		{
			if (!shownRv)
			{
				CurrentBallUpdate(canCountBall, extraRun);
			}
			else
			{
				RvCanCount = canCountBall;
				RvExtras = extraRun;
			}
			if (overStepBall)
			{
				if (CONTROLLER.canShowReplay)
				{
					GameIsOnReplay();
					groundControllerScript.ShowReplay();
				}
				else
				{
					ReplayIsNotShown();
				}
			}
		}
		else if (isWicket == 0)
		{
			StartCoroutine(waitForBatsmanCelebration(runsScored, canCountBall, extraRun));
		}
		if (!groundControllerScript.lineFreeHit && CONTROLLER.PlayModeSelected == 7)
		{
			if (CONTROLLER.prevPowerShot)
			{
				float num2 = 70f;
				if (num2 >= 70f)
				{
					num2 = 70f;
				}
				float num3 = Mathf.Ceil(num2 + (100f - num2) * 0.5f);
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].LoftMeterFillVal = num2;
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets <= 6)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].LoftMeterFillVal -= (float)(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets / 2) * 2f;
				}
				else
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].LoftMeterFillVal -= (float)(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets / 2) * 2.5f;
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].LoftMeterFillVal < 100f)
			{
				bool isBeaten = ((isWicket != 0) ? true : false);
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].LoftMeterFillVal += setConfidence(runsScored, isBeaten);
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].LoftMeterFillVal >= 100f)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].LoftMeterFillVal = 100f;
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].LoftMeterFillVal <= 30f)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].LoftMeterFillVal = 30f;
			}
			if (runsScored == 2)
			{
				Singleton<BattingControls>.instance.LoftMeterFill(batsmanID);
			}
			CONTROLLER.prevPowerShot = false;
			Singleton<BattingControls>.instance.LoftMeterFill(batsmanID);
		}
		if (CONTROLLER.PlayModeSelected != 6)
		{
			ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "contwik", CONTROLLER.continousWickets);
			ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "contsix", CONTROLLER.continousSixes);
			ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "contfour", CONTROLLER.continousBoundaries);
			if (CONTROLLER.PlayModeSelected != 7 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls > 0)
			{
				RunRate = (float)CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores / (float)(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6);
			}
			CheckForAchievementComplete();
			CheckForAchievementAlmostComplete();
			CheckIfAchievementMet();
		}
		else
		{
			CONTROLLER.currentMatchBalls = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls;
			CONTROLLER.currentMatchScores = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores;
			CONTROLLER.currentMatchWickets = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets;
			ScoreBoardMultiPlayer.instance.UpdateScoreCard();
			ServerManager.Instance.SendMatchScore(CONTROLLER.currentMatchScores.ToString(), CONTROLLER.currentMatchWickets, runsScored);
			ScoreBoardMultiPlayer.instance.UpdateMultiplayerBallsLeft();
		}
	}

	private float setConfidence(int scoredRuns, bool isBeaten)
	{
		float f = 0f;
		int num = 1;
		float num2 = 1f;
		int num3 = 1;
		if (CONTROLLER.difficultyMode == "easy")
		{
			num = 0;
		}
		else if (CONTROLLER.difficultyMode == "medium")
		{
			num = 1;
		}
		else if (CONTROLLER.difficultyMode == "hard")
		{
			num = 2;
		}
		num3 = ((CONTROLLER.oversSelectedIndex >= 2) ? ((CONTROLLER.oversSelectedIndex < 4) ? 1 : 2) : 0);
		switch (scoredRuns)
		{
		case 0:
			if (isBeaten)
			{
				f = -1f;
			}
			break;
		case 6:
			f = 0f;
			break;
		default:
			f = (float)scoredRuns * (30f / confidenceMultiplier[num3, num]);
			break;
		}
		return Mathf.Ceil(f);
	}

	private IEnumerator waitForBatsmanCelebration(int noOfRuns, int isValid, int extras)
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			StartCoroutine(TMwaitForBatsmanCelebration(noOfRuns, isValid, extras));
			yield break;
		}
		bool isOverStepBall = groundControllerScript.getOverStepBall();
		if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].reachedHalfCentury && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored >= 50)
		{
			groundControllerScript.batsmanCelebration("halfcentury");
		}
		if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].reachedHalfCentury && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.RunsScored >= 50)
		{
			groundControllerScript.batsmanCelebration("halfcentury");
		}
		if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].reachedCentury && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored >= 100)
		{
			groundControllerScript.batsmanCelebration("century");
		}
		if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].reachedCentury && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.RunsScored >= 100)
		{
			groundControllerScript.batsmanCelebration("century");
		}
		if (CONTROLLER.StrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored > 149 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored % 50 >= 0 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored % 50 < 6 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].reachedCentury && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].scoredHundredPlus)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].scoredHundredPlus = true;
			groundControllerScript.batsmanCelebration("century");
		}
		if (CONTROLLER.NonStrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.RunsScored > 149 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.RunsScored % 50 >= 0 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.RunsScored % 50 < 6 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].reachedCentury && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].scoredHundredPlus)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].scoredHundredPlus = true;
			groundControllerScript.batsmanCelebration("century");
		}
		CurrentBallUpdate(isValid, extras);
		yield return new WaitForSeconds(3f);
		groundControllerScript.destroyCelebrationBatsman();
		if (!isOverStepBall)
		{
			switch (noOfRuns)
			{
			case 4:
				if (!isOverStepBall)
				{
					InitAnimation(0);
				}
				break;
			case 6:
				if (!isOverStepBall)
				{
					InitAnimation(1);
				}
				break;
			default:
				CheckForOverComplete();
				break;
			}
		}
		else if (isOverStepBall && (noOfRuns == 4 || noOfRuns == 6))
		{
			if (CONTROLLER.canShowReplay)
			{
				GameIsOnReplay();
				groundControllerScript.ShowReplay();
			}
			else
			{
				ReplayIsNotShown();
			}
		}
		else
		{
			CheckForOverComplete();
		}
	}

	private IEnumerator TMwaitForBatsmanCelebration(int noOfRuns, int isValid, int extras)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			bool isOverStepBall2 = groundControllerScript.getOverStepBall();
			if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedHalfCentury1 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex) >= 50)
			{
				groundControllerScript.batsmanCelebration("halfcentury");
			}
			if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedHalfCentury1 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.NonStrikerIndex) >= 50)
			{
				groundControllerScript.batsmanCelebration("halfcentury");
			}
			if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedCentury1 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex) >= 100)
			{
				groundControllerScript.batsmanCelebration("century");
			}
			if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedCentury1 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.NonStrikerIndex) >= 100)
			{
				groundControllerScript.batsmanCelebration("century");
			}
			if (CONTROLLER.StrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex) > 149 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex) % 50 >= 0 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex) % 50 < 6 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedCentury1 && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMscoredHundredPlus1)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMscoredHundredPlus1 = true;
				groundControllerScript.batsmanCelebration("century");
			}
			if (CONTROLLER.NonStrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.NonStrikerIndex) > 149 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.NonStrikerIndex) % 50 >= 0 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.NonStrikerIndex) % 50 < 6 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedCentury1 && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMscoredHundredPlus1)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMscoredHundredPlus1 = true;
				groundControllerScript.batsmanCelebration("century");
			}
			CurrentBallUpdate(isValid, extras);
			yield return new WaitForSeconds(3f);
			groundControllerScript.destroyCelebrationBatsman();
			if (!isOverStepBall2)
			{
				switch (noOfRuns)
				{
				case 4:
					if (!isOverStepBall2)
					{
						InitAnimation(0);
					}
					break;
				case 6:
					if (!isOverStepBall2)
					{
						InitAnimation(1);
					}
					break;
				default:
					CheckForOverComplete();
					break;
				}
			}
			else if (isOverStepBall2 && (noOfRuns == 4 || noOfRuns == 6))
			{
				if (CONTROLLER.canShowReplay)
				{
					GameIsOnReplay();
					groundControllerScript.ShowReplay();
				}
				else
				{
					ReplayIsNotShown();
				}
			}
			else
			{
				CheckForOverComplete();
			}
			yield break;
		}
		bool isOverStepBall = groundControllerScript.getOverStepBall();
		if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedHalfCentury2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMRunsScored2 >= 50)
		{
			groundControllerScript.batsmanCelebration("halfcentury");
		}
		if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedHalfCentury2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.TMRunsScored2 >= 50)
		{
			groundControllerScript.batsmanCelebration("halfcentury");
		}
		if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedCentury2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMRunsScored2 >= 100)
		{
			groundControllerScript.batsmanCelebration("century");
		}
		if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedCentury2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.TMRunsScored2 >= 100)
		{
			groundControllerScript.batsmanCelebration("century");
		}
		if (CONTROLLER.StrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMRunsScored2 > 149 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMRunsScored2 % 50 >= 0 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMRunsScored2 % 50 < 6 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedCentury2 && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMscoredHundredPlus2)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMscoredHundredPlus2 = true;
			groundControllerScript.batsmanCelebration("century");
		}
		if (CONTROLLER.NonStrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.TMRunsScored2 > 149 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.TMRunsScored2 % 50 >= 0 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.TMRunsScored2 % 50 < 6 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedCentury2 && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMscoredHundredPlus2)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMscoredHundredPlus2 = true;
			groundControllerScript.batsmanCelebration("century");
		}
		CurrentBallUpdate(isValid, extras);
		yield return new WaitForSeconds(3f);
		groundControllerScript.destroyCelebrationBatsman();
		if (!isOverStepBall)
		{
			switch (noOfRuns)
			{
			case 4:
				if (!isOverStepBall)
				{
					InitAnimation(0);
				}
				break;
			case 6:
				if (!isOverStepBall)
				{
					InitAnimation(1);
				}
				break;
			default:
				CheckForOverComplete();
				break;
			}
		}
		else if (isOverStepBall && (noOfRuns == 4 || noOfRuns == 6))
		{
			if (CONTROLLER.canShowReplay)
			{
				GameIsOnReplay();
				groundControllerScript.ShowReplay();
			}
			else
			{
				ReplayIsNotShown();
			}
		}
		else
		{
			CheckForOverComplete();
		}
	}

	private bool checkForCelebration(int runs, int playerID)
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			return TMcheckForCelebration(runs, playerID);
		}
		if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].reachedHalfCentury)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.RunsScored % 50 <= 5 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.RunsScored >= 50)
			{
				return true;
			}
		}
		else if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].reachedCentury)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.RunsScored % 100 <= 5 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.RunsScored >= 100)
			{
				return true;
			}
		}
		else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.RunsScored % 50 <= 5 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].reachedCentury && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].scoredHundredPlus && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.RunsScored > 149)
		{
			return true;
		}
		return false;
	}

	private bool TMcheckForCelebration(int runs, int playerID)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].TMreachedHalfCentury1)
			{
				if (TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, playerID) % 50 <= 5 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, playerID) >= 50)
				{
					return true;
				}
			}
			else if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].TMreachedCentury1)
			{
				if (TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, playerID) % 100 <= 5 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, playerID) >= 100)
				{
					return true;
				}
			}
			else if (TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, playerID) % 50 <= 5 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].TMreachedCentury1 && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMscoredHundredPlus1 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, playerID) > 149)
			{
				return true;
			}
		}
		else if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].TMreachedHalfCentury2)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMRunsScored2 % 50 <= 5 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMRunsScored2 >= 50)
			{
				return true;
			}
		}
		else if (!CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].TMreachedCentury2)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMRunsScored2 % 100 <= 5 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMRunsScored2 >= 100)
			{
				return true;
			}
		}
		else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMRunsScored2 % 50 <= 5 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].TMreachedCentury2 && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMscoredHundredPlus2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMRunsScored2 > 149)
		{
			return true;
		}
		return false;
	}

	public void stopCommentarySnd()
	{
	}

	public void PlayCollectionSound(string SoundPath)
	{
		if (!gameQuit)
		{
		}
	}

	public void PlayGameSound(string SoundType)
	{
		if (!gameQuit && CONTROLLER.sndController != null)
		{
			CONTROLLER.sndController.PlayGameSnd(SoundType);
		}
	}

	private bool checkForRVWicketBall()
	{
		return false;
	}

	private void WicketChance(int validBall, int batsmanID, int wicketType, int bowlerID, int catcherID, int batsmanOut)
	{
		if (CONTROLLER.PlayModeSelected != 7)
		{
			vBall = validBall;
			bID = batsmanID;
			wType = wicketType;
			baID = bowlerID;
			cID = catcherID;
			bOut = batsmanOut;
			Time.timeScale = 0f;
		}
	}

	public void WicketBall(int validBall, int batsmanID, int wicketType, int bowlerID, int catcherID, int batsmanOut)
	{
		if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
		{
		}
		bool overStepBall = groundControllerScript.getOverStepBall();
		if ( !overStepBall && CONTROLLER.PlayModeSelected != 7 && !groundControllerScript.UltraEdgeCutscenePlaying && rebowlStatus != 0 && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets == 0 && !ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "firstwicket"))
			{
				WicketChance(validBall, batsmanID, wicketType, bowlerID, catcherID, batsmanOut);
				if (CONTROLLER.PlayModeSelected < 6)
				{
					shownRv = true;
					SetRVStatus("firstwicket");
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets == CONTROLLER.totalWickets - 1 && !ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "lastwicket"))
			{
				shownRv = true;
				WicketChance(validBall, batsmanID, wicketType, bowlerID, catcherID, batsmanOut);
				if (CONTROLLER.PlayModeSelected < 6)
				{
					SetRVStatus("lastwicket");
				}
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.RunsScored == 0 && !ObscuredPrefs.HasKey(CONTROLLER.PlayModeSelected + "duckout") && RVStatus != "lastwicket" && RVStatus != "firstwicket")
			{
				shownRv = true;
				WicketChance(validBall, batsmanID, wicketType, bowlerID, catcherID, batsmanOut);
				if (CONTROLLER.PlayModeSelected < 6)
				{
					SetRVStatus("duckout");
				}
			}
		}
		if (rebowlStatus == -1 && CONTROLLER.PlayModeSelected != 7)
		{
			return;
		}
		CONTROLLER.wicketType = wicketType;
		ObscuredPrefs.DeleteKey("partnershipXpGained");
		currentPartnership = 0;
		CanPauseGame = false;
		Singleton<Scoreboard>.instance.HidePause(boolean: true);
		if (NewBatsmanIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length - 1)
		{
			NewBatsmanIndex++;
		}
		if (NewBatsmanIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[NewBatsmanIndex].BatsmanList.Status = "not out";
			if (CONTROLLER.PlayModeSelected == 7)
			{
				TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, NewBatsmanIndex, "not out");
			}
			if (CONTROLLER.StrikerIndex == batsmanOut)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipBalls = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipBalls = 0;
			}
			else
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.currPatnerShipBalls = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipRuns = 0;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipBalls = 0;
			}
		}
		batsmanOutIndex = batsmanOut;
		IsWicketBall = true;
		if (validBall == 1)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[currentBall] = "W";
		}
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets++;
		if (CONTROLLER.PlayModeSelected == 7)
		{
			if (CONTROLLER.currentInnings < 2)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1++;
			}
			else
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets2++;
			}
		}
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.FOW = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores;
		if (CONTROLLER.PlayModeSelected == 7)
		{
			if (CONTROLLER.currentInnings < 2)
			{
				TestMatchBatsman.SetFOW(CONTROLLER.BattingTeamIndex, batsmanOut, CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchScores1);
			}
			else
			{
				TestMatchBatsman.SetFOW(CONTROLLER.BattingTeamIndex, batsmanOut, CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchScores2);
			}
		}
		if (wicketType == 4)
		{
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.WicketsInBallCount = 0;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "run out";
			if (CONTROLLER.PlayModeSelected == 7)
			{
				TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "run out");
			}
			if (CONTROLLER.BattingTeamIndex != CONTROLLER.myTeamIndex)
			{
			}
		}
		else
		{
			WicketsInOver++;
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.Wicket++;
			if (CONTROLLER.PlayModeSelected == 7)
			{
				if (CONTROLLER.currentInnings < 2)
				{
					CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.TMWicket1++;
				}
				else
				{
					CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.TMWicket2++;
				}
			}
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.WicketsInBallCount++;
			switch (wicketType)
			{
			case 1:
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "b " + GetBowlerShortName(bowlerID);
				if (CONTROLLER.PlayModeSelected == 7)
				{
					TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "b " + GetBowlerShortName(bowlerID));
				}
				break;
			case 2:
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "lbw " + GetBowlerShortName(bowlerID);
				if (CONTROLLER.PlayModeSelected == 7)
				{
					TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "lbw " + GetBowlerShortName(bowlerID));
				}
				break;
			case 3:
				catcherID = CONTROLLER.FieldersArray[catcherID];
				if (bowlerID == catcherID)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "c & b " + GetBowlerShortName(bowlerID);
					if (CONTROLLER.PlayModeSelected == 7)
					{
						TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "c & b " + GetBowlerShortName(bowlerID));
					}
				}
				else
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "ct off b " + GetBowlerShortName(bowlerID);
					if (CONTROLLER.PlayModeSelected == 7)
					{
						TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "ct off b " + GetBowlerShortName(bowlerID));
					}
				}
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
				break;
			case 5:
				catcherID = CONTROLLER.FieldersArray[catcherID];
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "ct behind b " + GetBowlerShortName(bowlerID);
				if (CONTROLLER.PlayModeSelected == 7)
				{
					TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "ct behind b " + GetBowlerShortName(bowlerID));
				}
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
				break;
			case 6:
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status = "st, b " + GetBowlerShortName(bowlerID);
				if (CONTROLLER.PlayModeSelected == 7)
				{
					TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, batsmanOut, "st, b " + GetBowlerShortName(bowlerID));
				}
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
				break;
			}
		}
		if (wicketType == 4)
		{
			if (catcherID == 0 && batsmanOut == CONTROLLER.StrikerIndex)
			{
				CONTROLLER.StrikerIndex = NewBatsmanIndex;
			}
			else if (catcherID == 1 && batsmanOut == CONTROLLER.StrikerIndex)
			{
				CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
				CONTROLLER.NonStrikerIndex = NewBatsmanIndex;
			}
			else if (catcherID == 0 && batsmanOut == CONTROLLER.NonStrikerIndex)
			{
				CONTROLLER.NonStrikerIndex = CONTROLLER.StrikerIndex;
				CONTROLLER.StrikerIndex = NewBatsmanIndex;
			}
			else if (catcherID == 1 && batsmanOut == CONTROLLER.NonStrikerIndex)
			{
				CONTROLLER.NonStrikerIndex = NewBatsmanIndex;
			}
		}
		else if (batsmanOut == CONTROLLER.StrikerIndex)
		{
			CONTROLLER.StrikerIndex = NewBatsmanIndex;
		}
		else if (batsmanOut == CONTROLLER.NonStrikerIndex)
		{
			CONTROLLER.NonStrikerIndex = NewBatsmanIndex;
		}
		if (wicketType == 3)
		{
			float num = groundControllerScript.strikerZPos();
			if (num < 0f)
			{
				CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
				CONTROLLER.NonStrikerIndex = NewBatsmanIndex;
			}
			else
			{
				CONTROLLER.StrikerIndex = NewBatsmanIndex;
			}
		}
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			int strikeRate = GetStrikeRate(batsmanOut);
			int runsScored = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.RunsScored;
		}
		if (canShowAnimation)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.WicketsInBallCount % 3 == 0 && CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[bowlerID].BowlerList.WicketsInBallCount > 0)
			{
				InitAnimation(3);
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.RunsScored <= 0)
			{
				if (CONTROLLER.BattingTeamIndex != CONTROLLER.myTeamIndex || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.BallsPlayed != 1 || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanOut].BatsmanList.Status != "run out")
				{
				}
				if (!overStepBall)
				{
					InitAnimation(2);
				}
			}
			else if (!overStepBall)
			{
				InitAnimation(2);
			}
		}
		ScoreStr = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets;
	}

	protected void OnApplicationPause(bool pauseStatus)
	{
		if (CONTROLLER.PlayModeSelected == 6 && !CONTROLLER.MPInningsCompleted)
		{
			ServerManager.Instance.ExitRoom();
			if (ManageScenes._instance.getCurrentLoadedSceneName() == "MainMenu")
			{
				InterfaceHandler._instance.ShowServerDisconnectedPopup();
			}
			else
			{
				GroundScriptHandler.Instance.ShowServerDisconnectedPopup();
			}
		}
		else if (!Singleton<TargetScreen>.instance.holder.activeSelf && Singleton<GameModel>.instance.CanPauseGame && pauseStatus && !CONTROLLER.SceneIsLoading && !resumeGO.activeSelf && !CONTROLLER.ReplayShowing && !Singleton<PauseGameScreen>.instance.BG.activeInHierarchy)
		{
			GamePaused(boolean: true);
		}
	}

	public string GetBowlerShortName(int playerID)
	{
		return CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[playerID].ScoreboardName;
	}

	private void UpdateOverCompleteResult()
	{
		if (runsScoredInOver <= 0 && currentBall >= 5)
		{
			if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
			{
				bool flag = false;
				for (int i = 0; i < 6; i++)
				{
					if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[i] == "W")
					{
						flag = true;
					}
				}
				if (flag && CheckIfAlreadyAchieved(9))
				{
					IncrementAchievement(9);
				}
				if (!flag && CheckIfAlreadyAchieved(10))
				{
					IncrementAchievement(10);
				}
				CounterMaiden++;
				ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterMaiden", CounterMaiden);
				if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
				{
				}
			}
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.Maiden++;
			if (CONTROLLER.PlayModeSelected == 7)
			{
				if (CONTROLLER.currentInnings < 2)
				{
					CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.TMMaiden1++;
				}
				else
				{
					CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.TMMaiden2++;
				}
			}
			Singleton<BowlingScoreCard>.instance.UpdateScoreCard();
		}
		if (runsScoredInOver > 0 && currentBall >= 5 && CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
		{
			CanShowPopupAgain(9);
		}
	}

	private void UpdateBallCompleteResult()
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TMUpdateBallCompleteResult();
			return;
		}
		if (CONTROLLER.StrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].reachedHalfCentury)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored >= 50)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].reachedHalfCentury = true;
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					if (CheckIfAlreadyAchieved(14))
					{
						IncrementAchievement(14);
					}
					CounterFifty++;
					ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterFifty", CounterFifty);
				}
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored >= 45 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored < 50 && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				string text = AchievementTable.AchievementName[14];
				text = text + " Trigger" + CONTROLLER.PlayModeSelected;
				if (!CheckIfAlreadyShown(14) && ObscuredPrefs.GetInt(text) == 0)
				{
					Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[14], 14);
				}
			}
		}
		if (CONTROLLER.NonStrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].reachedHalfCentury)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.RunsScored >= 50)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].reachedHalfCentury = true;
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					if (CheckIfAlreadyAchieved(14))
					{
						IncrementAchievement(14);
					}
					CounterFifty++;
					ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterFifty", CounterFifty);
				}
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.RunsScored >= 45 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.RunsScored < 50 && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				string text = AchievementTable.AchievementName[14];
				text = text + " Trigger" + CONTROLLER.PlayModeSelected;
				if (!CheckIfAlreadyShown(14) && ObscuredPrefs.GetInt(text) == 0)
				{
					Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[14], 14);
				}
			}
		}
		if (CONTROLLER.StrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].reachedCentury)
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored >= 100)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].reachedCentury = true;
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					if (CheckIfAlreadyAchieved(13))
					{
						IncrementAchievement(13);
					}
					CounterCentury++;
					ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterCentury", CounterCentury);
				}
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored >= 90 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored < 100 && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				string text = AchievementTable.AchievementName[13];
				text = text + " Trigger" + CONTROLLER.PlayModeSelected;
				if (!CheckIfAlreadyShown(13) && ObscuredPrefs.GetInt(text) == 0)
				{
					Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[13], 13);
				}
			}
		}
		if (CONTROLLER.NonStrikerIndex >= CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].reachedCentury)
		{
			return;
		}
		if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.RunsScored >= 100)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].reachedCentury = true;
			if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				if (CheckIfAlreadyAchieved(13))
				{
					IncrementAchievement(13);
				}
				CounterCentury++;
				ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterCentury", CounterCentury);
			}
		}
		if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.RunsScored >= 90 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.RunsScored < 100 && CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			string text = AchievementTable.AchievementName[13];
			text = text + " Trigger" + CONTROLLER.PlayModeSelected;
			if (!CheckIfAlreadyShown(13) && ObscuredPrefs.GetInt(text) == 0)
			{
				Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[13], 13);
			}
		}
	}

	private void TMUpdateBallCompleteResult()
	{
		if (CONTROLLER.currentInnings < 2)
		{
			if (CONTROLLER.StrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedHalfCentury1 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex) >= 50)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedHalfCentury1 = true;
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					if (CheckIfAlreadyAchieved(14))
					{
						IncrementAchievement(14);
					}
					CounterFifty++;
					ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterFifty", CounterFifty);
				}
			}
			if (CONTROLLER.NonStrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedHalfCentury1 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.NonStrikerIndex) >= 50)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedHalfCentury1 = true;
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					if (CheckIfAlreadyAchieved(14))
					{
						IncrementAchievement(14);
					}
					CounterFifty++;
					ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterFifty", CounterFifty);
				}
			}
			if (CONTROLLER.StrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedCentury1 && TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex) >= 100)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedCentury1 = true;
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					if (CheckIfAlreadyAchieved(13))
					{
						IncrementAchievement(13);
					}
					CounterCentury++;
					ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterCentury", CounterCentury);
				}
			}
			if (CONTROLLER.NonStrikerIndex >= CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedCentury1 || TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, CONTROLLER.NonStrikerIndex) < 100)
			{
				return;
			}
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedCentury1 = true;
			if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				if (CheckIfAlreadyAchieved(13))
				{
					IncrementAchievement(13);
				}
				CounterCentury++;
				ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterCentury", CounterCentury);
			}
			return;
		}
		if (CONTROLLER.StrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedHalfCentury2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMRunsScored2 >= 50)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedHalfCentury2 = true;
			if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				if (CheckIfAlreadyAchieved(14))
				{
					IncrementAchievement(14);
				}
				CounterFifty++;
				ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterFifty", CounterFifty);
			}
		}
		if (CONTROLLER.NonStrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedHalfCentury2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.TMRunsScored2 >= 50)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedHalfCentury2 = true;
			if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				if (CheckIfAlreadyAchieved(14))
				{
					IncrementAchievement(14);
				}
				CounterFifty++;
				ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterFifty", CounterFifty);
			}
		}
		if (CONTROLLER.StrikerIndex < CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length && !CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedCentury2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.TMRunsScored2 >= 100)
		{
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].TMreachedCentury2 = true;
			if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				if (CheckIfAlreadyAchieved(13))
				{
					IncrementAchievement(13);
				}
				CounterCentury++;
				ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterCentury", CounterCentury);
			}
		}
		if (CONTROLLER.NonStrikerIndex >= CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList.Length || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedCentury2 || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.TMRunsScored2 < 100)
		{
			return;
		}
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].TMreachedCentury2 = true;
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			if (CheckIfAlreadyAchieved(13))
			{
				IncrementAchievement(13);
			}
			CounterCentury++;
			ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterCentury", CounterCentury);
		}
	}

	public void CurrentBallUpdate(int canCountBall, int extraRun)
	{
		inningsCompleted = CheckForInningsComplete();
		if (inningsCompleted)
		{
			UpdateBallCompleteResult();
			UpdateOverCompleteResult();
		}
		else if (currentBall == 5 || (currentBall == -1 && canCountBall == 1 && extraRun != 1))
		{
			UpdateBallCompleteResult();
			UpdateOverCompleteResult();
			int strikerIndex = CONTROLLER.StrikerIndex;
			CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
			CONTROLLER.NonStrikerIndex = strikerIndex;
		}
		else
		{
			UpdateBallCompleteResult();
		}
		AutoSave.currentBall = currentBall;
		AutoSave.runsScoredInOver = runsScoredInOver;
		AutoSave.WicketsInOver = WicketsInOver;
		AutoSave.maxRunsReached = maxRunsReached;
		AutoSave.maxWidesReached = maxWidesReached;
		AutoSave.currentPartnership = currentPartnership;
		AutoSave.fourInOver = fourInOver;
		AutoSave.sixInOver = sixInOver;
		AutoSave.maxSixes = maxSixes;
		CONTROLLER.strikerPartnershipRuns = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipRuns;
		CONTROLLER.strikerPartnershipBall = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.currPatnerShipBalls;
		CONTROLLER.NonstrikerPartnershipRuns = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipRuns;
		CONTROLLER.NonstrikerPartnershipBall = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.currPatnerShipBalls;
		if ((CONTROLLER.currentInnings == 1 && inningsCompleted && CONTROLLER.PlayModeSelected != 7) || (CONTROLLER.PlayModeSelected == 4 && CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets >= CONTROLLER.totalWickets))
		{
			AutoSave.DeleteFile();
		}
		else
		{
			AutoSave.SaveInGameMatch();
		}
		if (CONTROLLER.PlayModeSelected == 7 && CheckForMatchCompletion())
		{
			ObscuredPrefs.SetInt("ShowTMGO", 1);
		}
	}

	public void AutoplayCheckForOverComplete()
	{
		if (CONTROLLER.PlayModeSelected != 7)
		{
			inningsCompleted = CheckForInningsComplete();
			if (inningsCompleted)
			{
				CanPauseGame = false;
				groundControllerScript.fieldRestriction = true;
				groundControllerScript.resetNoBallVairables();
				CONTROLLER.fielderChangeIndex = 1;
				CONTROLLER.computerFielderChangeIndex = 0;
				Singleton<Scoreboard>.instance.Hide(boolean: true);
				Singleton<BowlingScoreCard>.instance.InningsCompleted();
				if (CONTROLLER.currentInnings == 0 && CONTROLLER.PlayModeSelected != 7)
				{
					AutoSave.SaveInGameMatch();
					Time.timeScale = 0f;
					if (CONTROLLER.PlayModeSelected != 6)
					{
						Singleton<PauseGameScreen>.instance.Hide(boolean: true);
						Singleton<PauseGameScreen>.instance.BG.SetActive(value: false);
						generatingScore.SetActive(value: true);
						Singleton<BattingControls>.instance.Hide(boolean: true);
						groundControllerScript.mainCamera.enabled = false;
						groundControllerScript.umpireCamera.enabled = true;
						groundControllerScript.ActivateStadiumAndSkybox(boolean: true);
						Singleton<GroundController>.instance.umpireCamera.transform.localPosition = new Vector3(-86.5f, 18.5f, -3.8f);
						Singleton<GroundController>.instance.umpireCamera.transform.eulerAngles = new Vector3(0f, -90f, 0f);
						Singleton<GroundController>.instance.umpireCamera.fieldOfView = 61f;
						CONTROLLER.pageName = string.Empty;
						Sequence sequence = DOTween.Sequence();
						sequence.Insert(0f, fill.DOFillAmount(0f, 0f));
						sequence.Insert(0f, fill.DOFillAmount(1f, 3f)).OnComplete(ShowBattingScoreCard);
						sequence.Insert(0f, scrambledScore.DOText(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores.ToString(), 2f, richTextEnabled: true, ScrambleMode.Numerals));
						sequence.Insert(1f, scrambledWickets.DOText(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets.ToString(), 2f, richTextEnabled: true, ScrambleMode.Numerals));
						sequence.SetUpdate(isIndependentUpdate: true);
					}
				}
				else
				{
					if (CONTROLLER.isFromAutoPlay)
					{
						CONTROLLER.isFromAutoPlay = false;
						Singleton<PauseGameScreen>.instance.Hide(boolean: true);
					}
					AutoSave.DeleteFile();
					checkForDemoPlay();
					Singleton<PauseGameScreen>.instance.Hide(boolean: true);
					Singleton<PauseGameScreen>.instance.BG.SetActive(value: false);
					generatingScore.SetActive(value: true);
					Singleton<BattingControls>.instance.Hide(boolean: true);
					groundControllerScript.mainCamera.enabled = false;
					groundControllerScript.umpireCamera.enabled = true;
					groundControllerScript.ActivateStadiumAndSkybox(boolean: true);
					Singleton<GroundController>.instance.umpireCamera.transform.localPosition = new Vector3(-86.5f, 18.5f, -3.8f);
					Singleton<GroundController>.instance.umpireCamera.transform.eulerAngles = new Vector3(0f, -90f, 0f);
					Singleton<GroundController>.instance.umpireCamera.fieldOfView = 61f;
					CONTROLLER.pageName = string.Empty;
					Sequence sequence2 = DOTween.Sequence();
					sequence2.Insert(0f, fill.DOFillAmount(0f, 0f));
					sequence2.Insert(0f, fill.DOFillAmount(1f, 3f)).OnComplete(ShowGameOverScreen);
					sequence2.Insert(0f, scrambledScore.DOText(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores.ToString(), 2f, richTextEnabled: true, ScrambleMode.Numerals));
					sequence2.Insert(1f, scrambledWickets.DOText(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets.ToString(), 2f, richTextEnabled: true, ScrambleMode.Numerals));
					sequence2.SetUpdate(isIndependentUpdate: true);
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls % 6 == 0)
			{
				groundControllerScript.showPreviewCamera(status: false);
				NewOver();
			}
			action = 0;
			return;
		}
		DebugLogger.PrintWithSize("CONTROLLER.currentInnings " + CONTROLLER.currentInnings);
		Singleton<BowlingControls>.instance.StopYellowFill();
		inningsCompleted = CheckForInningsComplete();
		if (inningsCompleted)
		{
			if (CONTROLLER.currentInnings == 2)
			{
				SetTargetToWin();
			}
			CanPauseGame = false;
			groundControllerScript.fieldRestriction = true;
			groundControllerScript.resetNoBallVairables();
			CONTROLLER.fielderChangeIndex = 1;
			CONTROLLER.computerFielderChangeIndex = 1;
			Singleton<Scoreboard>.instance.Hide(boolean: true);
			Singleton<PreviewScreen>.instance.Hide(boolean: true);
			if (!CheckForMatchCompletion())
			{
				Time.timeScale = 0f;
				ShowBattingScoreCard();
			}
			else if (CONTROLLER.isFromAutoPlay)
			{
				isGamePaused = false;
				CONTROLLER.isFromAutoPlay = false;
				Singleton<PauseGameScreen>.instance.Hide(boolean: true);
				CONTROLLER.ShowTMGameOver = true;
				ShowBattingScoreCard();
			}
			groundControllerScript.action = -10;
		}
		else if ((CONTROLLER.currentInnings < 2 && CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls1 % 6 == 0) || (CONTROLLER.currentInnings > 1 && CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls2 % 6 == 0))
		{
			CONTROLLER.CurrentBall = -1;
			AutoSave.ballUpdate = string.Empty;
			groundControllerScript.resetNoBallVairables();
			groundControllerScript.action = -10;
			CanPauseGame = false;
			sessionComplete = checkforSessionComplete();
			groundControllerScript.showPreviewCamera(status: false);
			if (!sessionComplete)
			{
				if (CheckForAIDeclaration())
				{
					Singleton<PauseGameScreen>.instance.declare();
				}
				else
				{
					NewOver();
				}
			}
			else if (CONTROLLER.MaxDays == CONTROLLER.currentDay && CONTROLLER.currentSession == 2)
			{
				isGamePaused = false;
				CONTROLLER.ballsBowledPerDay = 0;
				CONTROLLER.isFromAutoPlay = false;
				CONTROLLER.ShowTMGameOver = true;
				ShowBattingScoreCard();
			}
			else
			{
				Time.timeScale = 0f;
				Singleton<TMSessionScreen>.instance.ShowMe();
			}
		}
		AutoSave.SaveInGameMatch();
	}

	private void ShowGameOverScreen()
	{
		Singleton<NavigationBack>.instance.disableDeviceBack = false;
		fill.fillAmount = 0f;
		generatingScore.SetActive(value: false);
		Singleton<GameOverScreen>.instance.Hide(boolean: false);
	}

	private void checkForDemoPlay()
	{
		if (CONTROLLER.noOfTrails > 0)
		{
			CONTROLLER.noOfTrails--;
			ObscuredPrefs.SetInt("NoOfTrails", CONTROLLER.noOfTrails);
		}
	}

	private void CheckForAchievementComplete()
	{
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			int strikeRate = GetStrikeRate(CONTROLLER.StrikerIndex);
			int strikeRate2 = GetStrikeRate(CONTROLLER.NonStrikerIndex);
			if (CONTROLLER.RunRate >= 15f && CheckIfAlreadyAchieved(11))
			{
				IncrementAchievement(11);
			}
			if ((strikeRate >= 200 || strikeRate2 >= 200) && CheckIfAlreadyAchieved(12))
			{
				IncrementAchievement(12);
			}
			if (CONTROLLER.continousBoundaries == 3 && CheckIfAlreadyAchieved(3))
			{
				IncrementAchievement(3);
			}
			if (CONTROLLER.continousSixes == 3 && CheckIfAlreadyAchieved(4))
			{
				IncrementAchievement(4);
			}
			if (CONTROLLER.continousBoundaries == 6 && CheckIfAlreadyAchieved(7))
			{
				IncrementAchievement(7);
			}
			if (CONTROLLER.continousSixes == 6 && CheckIfAlreadyAchieved(8))
			{
				IncrementAchievement(8);
			}
		}
		if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
		{
			if (CONTROLLER.continousWickets == 3 && CheckIfAlreadyAchieved(5))
			{
				IncrementAchievement(5);
			}
			if (CONTROLLER.continousWickets == 6 && CheckIfAlreadyAchieved(6))
			{
				IncrementAchievement(6);
			}
		}
	}

	private void CheckForAchievementAlmostComplete()
	{
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			int strikeRate = GetStrikeRate(CONTROLLER.StrikerIndex);
			int strikeRate2 = GetStrikeRate(CONTROLLER.NonStrikerIndex);
			if (CONTROLLER.RunRate >= 13f && CONTROLLER.RunRate < 15f)
			{
				string text = AchievementTable.AchievementName[11];
				text = text + " Trigger" + CONTROLLER.PlayModeSelected;
				if (!CheckIfAlreadyShown(11) && ObscuredPrefs.GetInt(text) == 0)
				{
					Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[11], 11);
				}
			}
			if ((strikeRate >= 175 && strikeRate < 200) || (strikeRate2 >= 175 && strikeRate2 < 200))
			{
				string text = AchievementTable.AchievementName[12];
				text = text + " Trigger" + CONTROLLER.PlayModeSelected;
				if (!CheckIfAlreadyShown(12) && ObscuredPrefs.GetInt(text) == 0)
				{
					Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[12], 12);
				}
			}
			if (CONTROLLER.continousBoundaries == 2)
			{
				string text = AchievementTable.AchievementName[3];
				text = text + " Trigger" + CONTROLLER.PlayModeSelected;
				if (!CheckIfAlreadyShown(3) && ObscuredPrefs.GetInt(text) == 0)
				{
					Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[3], 3);
				}
			}
			if (CONTROLLER.continousSixes == 2)
			{
				string text = AchievementTable.AchievementName[4];
				text = text + " Trigger" + CONTROLLER.PlayModeSelected;
				if (!CheckIfAlreadyShown(4) && ObscuredPrefs.GetInt(text) == 0)
				{
					Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[4], 4);
				}
			}
			if (CONTROLLER.continousBoundaries == 5)
			{
				string text = AchievementTable.AchievementName[7];
				text = text + " Trigger" + CONTROLLER.PlayModeSelected;
				if (!CheckIfAlreadyShown(7) && ObscuredPrefs.GetInt(text) == 0)
				{
					Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[7], 7);
				}
			}
			if (CONTROLLER.continousSixes == 5)
			{
				string text = AchievementTable.AchievementName[8];
				text = text + " Trigger" + CONTROLLER.PlayModeSelected;
				if (!CheckIfAlreadyShown(8) && ObscuredPrefs.GetInt(text) == 0)
				{
					Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[8], 8);
				}
			}
		}
		if (CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
		{
			return;
		}
		if (runsScoredInOver <= 0 && (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls + 1) % 6 == 0 && CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
		{
			bool flag = false;
			for (int i = 0; i < 1; i++)
			{
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[i] == "W")
				{
					flag = true;
				}
			}
			if (!flag)
			{
				string text = AchievementTable.AchievementName[9];
				text = text + " Trigger" + CONTROLLER.PlayModeSelected;
				if (!CheckIfAlreadyShown(9) && ObscuredPrefs.GetInt(text) == 0)
				{
					Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[9], 9);
				}
			}
		}
		if (CONTROLLER.continousWickets == 2)
		{
			string text = AchievementTable.AchievementName[5];
			text = text + " Trigger" + CONTROLLER.PlayModeSelected;
			if (!CheckIfAlreadyShown(5) && ObscuredPrefs.GetInt(text) == 0)
			{
				Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[5], 5);
			}
		}
		if (CONTROLLER.continousWickets == 5)
		{
			string text = AchievementTable.AchievementName[6];
			text = text + " Trigger" + CONTROLLER.PlayModeSelected;
			if (!CheckIfAlreadyShown(6) && ObscuredPrefs.GetInt(text) == 0)
			{
				Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[6], 6);
			}
		}
	}

	public void SetRVStatus(string status)
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			return;
		}
		RVFillmeter.fillAmount = 1f;
		//if (!Singleton<AdIntegrate>.instance.isRewardedVideoAvailable)
		//{
		//	Singleton<AdIntegrate>.instance.requestRewardedVideo();
		//	RVStatus = string.Empty;
		//	return;
		//}
		rebowlStatus = -1;
		rebowled = false;
		RVStatus = status;
		rvPopup.SetActive(value: true);
		if (CONTROLLER.myTeamIndex == CONTROLLER.BowlingTeamIndex)
		{
			rvPopupText.text = LocalizationData.instance.getText(638);
		}
		else
		{
			rvPopupText.text = LocalizationData.instance.getText(262);
		}
		if (RVStatus == "aifirstsix")
		{
			canShowAnimation = false;
		}
		Time.timeScale = 0.3f;
		TweenCallback callback = delegate
		{
			DisableRVPopUp();
		};
		seq = DOTween.Sequence();
		seq.InsertCallback(5f, callback);
		seq.Insert(0f, RVFillmeter.DOFillAmount(0f, 5f));
		seq.SetUpdate(isIndependentUpdate: true);
		seq.SetLoops(1);
	}

	public void DisableRVPopUp()
	{
		if (rebowled || rebowlStatus == 2)
		{
			return;
		}
		rebowlStatus = 0;
		rvPopup.SetActive(value: false);
		Time.timeScale = 1f;
		Singleton<Scoreboard>.instance.UpdateScoreCard();
		if (RVStatus == "firstwicket" || RVStatus == "duckout" || RVStatus == "lastwicket")
		{
			WicketBall(vBall, bID, wType, baID, cID, bOut);
			if (shownRv)
			{
				CurrentBallUpdate(RvCanCount, RvExtras);
				shownRv = false;
			}
		}
		else
		{
			CheckForOverComplete();
		}
		rebowled = true;
		rebowlStatus = 2;
	}

	//public void RebowlSelected()
	////{
	////	if (Singleton<AdIntegrate>.instance.CheckForInternet())
	////	{
	////		rebowled = true;
	////		Singleton<AdIntegrate>.instance.showRewardedVideo(8);
	////	}
	//}

	public void RebowlLastBall()
	{
		if (rebowlStatus > 0)
		{
			return;
		}
		ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + RVStatus, 1);
		Time.timeScale = 1f;
		rebowlStatus = 1;
		rebowled = true;
		Singleton<BowlingScoreCard>.instance.Hide(boolean: true);
		Singleton<BattingScoreCard>.instance.Hide(boolean: true);
		Singleton<GameOverDisplay>.instance.HideMe();
		groundControllerScript.lastBowledBall = templastBowledBall;
		if (templastBowledBall == "overstep")
		{
			groundControllerScript.lineFreeHit = true;
		}
		else
		{
			groundControllerScript.lineFreeHit = false;
		}
		if (RVStatus == "duckout" || RVStatus == "firstwicket" || RVStatus == "lastwicket")
		{
			rvPopup.SetActive(value: false);
			CONTROLLER.StrikerIndex = savedStrikerIndex;
			CONTROLLER.NonStrikerIndex = savedNonStrikerIndex;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[NewBatsmanIndex].BatsmanList.Status = string.Empty;
			NewBatsmanIndex = savedNewStrikerIndex;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].ballUpdate[currentBall] = string.Empty;
			CONTROLLER.currentMatchBalls--;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchBalls--;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.BallsPlayed--;
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.Status = "not out";
			CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.Status = "not out";
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.BallsBowled--;
			currentBall--;
			if (RvRunsScored > 0)
			{
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores -= RvRunsScored;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored -= RvRunsScored;
				CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.RunsGiven -= RvRunsScored;
			}
			ScoreStr = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets;
			OversStr = Singleton<GameModel>.instance.GetOverStr() + "(" + CONTROLLER.totalOvers + ")";
			CurrentBallUpdate(0, 0);
			groundControllerScript.ResetAll();
			Singleton<ScoreBoardBallList>.instance.RemoveLastBall();
			Singleton<ScoreBoardBallList>.instance.SaveBallList();
			Singleton<Scoreboard>.instance.BallInfoList[Singleton<Scoreboard>.instance.BallInfoList.Count - 1].gameObject.SetActive(value: false);
			Singleton<Scoreboard>.instance.UpdateScoreCard();
			CheckForOverComplete();
		}
		else if (RVStatus == "3wides")
		{
			rvPopup.SetActive(value: false);
			if (IsWicketBall)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[NewBatsmanIndex].BatsmanList.Status = string.Empty;
				NewBatsmanIndex--;
				CONTROLLER.StrikerIndex = savedStrikerIndex;
				WicketsInOver--;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets--;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.Status = "not out";
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.Status = "not out";
				CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.Wicket--;
				CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.WicketsInBallCount--;
			}
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchScores -= RvRunsScored;
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchExtras--;
			ScoreStr = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets;
			OversStr = Singleton<GameModel>.instance.GetOverStr() + "(" + CONTROLLER.totalOvers + ")";
			CurrentBallUpdate(0, 0);
			groundControllerScript.ResetAll();
			Singleton<Scoreboard>.instance.BallInfoList[Singleton<Scoreboard>.instance.BallInfoList.Count - 1].gameObject.SetActive(value: false);
			Singleton<ScoreBoardBallList>.instance.RemoveLastBall();
			Singleton<ScoreBoardBallList>.instance.RemoveLastExtra();
			Singleton<ScoreBoardBallList>.instance.SaveBallList();
			Singleton<Scoreboard>.instance.UpdateScoreCard();
			CheckForOverComplete();
		}
		else if (RVStatus == "aifirstsix" || RVStatus == "lastball")
		{
			CONTROLLER.StrikerIndex = savedStrikerIndex;
			CONTROLLER.NonStrikerIndex = savedNonStrikerIndex;
			if (!isValidBall)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchExtras--;
				Singleton<ScoreBoardBallList>.instance.RemoveLastExtra();
			}
			else
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ballUpdate[currentBall] = string.Empty;
			}
			rvPopup.SetActive(value: false);
			currentBall--;
			if (RVStatus == "aifirstsix")
			{
				Singleton<BallSimulationManager>.instance.noOfBallDataSetted--;
				Singleton<BallSimulationManager>.instance.currentSimulationBallIndex--;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.Sixes--;
				if (RvRunsScored > 0)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored -= RvRunsScored;
					CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.RunsGiven -= RvRunsScored;
				}
			}
			if (RVStatus == "lastball")
			{
				if (RvRunsScored == 6)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.Sixes--;
				}
				else if (RvRunsScored == 4)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.Fours--;
				}
				else if (RvRunsScored > 0)
				{
					CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.RunsScored -= RvRunsScored;
					CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.RunsGiven -= RvRunsScored;
				}
			}
			if (IsWicketBall)
			{
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[NewBatsmanIndex].BatsmanList.Status = string.Empty;
				NewBatsmanIndex--;
				CONTROLLER.StrikerIndex = savedStrikerIndex;
				WicketsInOver--;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets--;
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.Status = "not out";
				CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.Status = "not out";
				CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.Wicket--;
				CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.WicketsInBallCount--;
			}
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores -= RvRunsScored;
			CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList[CONTROLLER.CurrentBowlerIndex].BowlerList.BallsBowled--;
			CONTROLLER.currentMatchBalls--;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls--;
			CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.BallsPlayed--;
			ScoreStr = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores + "/" + CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets;
			OversStr = Singleton<GameModel>.instance.GetOverStr() + "(" + CONTROLLER.totalOvers + ")";
			CurrentBallUpdate(0, 0);
			groundControllerScript.ResetAll();
			Singleton<ScoreBoardBallList>.instance.RemoveLastBall();
			Singleton<ScoreBoardBallList>.instance.SaveBallList();
			Singleton<Scoreboard>.instance.BallInfoList[Singleton<Scoreboard>.instance.BallInfoList.Count - 1].gameObject.SetActive(value: false);
			Singleton<Scoreboard>.instance.UpdateScoreCard();
			if (RVStatus != "lastball")
			{
				CheckForOverComplete();
			}
		}
		RVStatus = string.Empty;
		rebowlStatus = 2;
		Singleton<BattingScoreCard>.instance.UpdateScoreCard();
		Singleton<BowlingScoreCard>.instance.UpdateScoreCard();
	}

	private void CheckIfAchievementMet()
	{
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			int strikeRate = GetStrikeRate(CONTROLLER.StrikerIndex);
			int strikeRate2 = GetStrikeRate(CONTROLLER.NonStrikerIndex);
			if (CONTROLLER.RunRate < 13f)
			{
				CanShowPopupAgain(11);
			}
			if (strikeRate < 175 || strikeRate2 < 175)
			{
				CanShowPopupAgain(12);
			}
			if (CONTROLLER.continousBoundaries < 2)
			{
				CanShowPopupAgain(3);
			}
			if (CONTROLLER.continousSixes < 2)
			{
				CanShowPopupAgain(4);
			}
			if (CONTROLLER.continousBoundaries < 5)
			{
				CanShowPopupAgain(7);
			}
			if (CONTROLLER.continousSixes < 5)
			{
				CanShowPopupAgain(8);
			}
		}
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			if (CONTROLLER.continousWickets < 2)
			{
				CanShowPopupAgain(5);
			}
			if (CONTROLLER.continousWickets < 5)
			{
				CanShowPopupAgain(6);
			}
		}
	}

	private void CheckForOverComplete()
	{
		if (rebowlStatus == -1)
		{
			return;
		}
		rebowlStatus = 2;
		if (CONTROLLER.PlayModeSelected == 7)
		{
			inningsCompleted = CheckForInningsComplete();
			if (inningsCompleted)
			{
				if (CONTROLLER.currentInnings == 2)
				{
					SetTargetToWin();
				}
				CanPauseGame = false;
				groundControllerScript.resetNoBallVairables();
				if (!CheckForMatchCompletion())
				{
					ShowBattingScoreCard();
					return;
				}
				isGamePaused = false;
				CONTROLLER.ShowTMGameOver = true;
				ShowBattingScoreCard();
			}
			else if (currentBall == 5)
			{
				KitTable.SetKitValues(1);
				CanPauseGame = false;
				sessionComplete = checkforSessionComplete();
				if (!sessionComplete)
				{
					if (CheckForAIDeclaration())
					{
						Singleton<TMFollowOn>.instance.ShowAIDeclaration();
					}
					else
					{
						NewOver();
					}
				}
				else if (CONTROLLER.MaxDays == CONTROLLER.currentDay && CONTROLLER.currentSession == 2)
				{
					CONTROLLER.ballsBowledPerDay = 0;
					isGamePaused = false;
					CONTROLLER.ShowTMGameOver = true;
					ShowBattingScoreCard();
				}
				else
				{
					Singleton<TMSessionScreen>.instance.ShowMe();
				}
			}
			else if (CheckForAIDeclaration())
			{
				Singleton<TMFollowOn>.instance.ShowAIDeclaration();
			}
			else
			{
				BowlingScoreCard.newOverCalled = false;
				BowlNextBall();
			}
			return;
		}
		if (CONTROLLER.PlayModeSelected == 4)
		{
			if (CONTROLLER.SuperOverMode == "bat")
			{
				levelCompleted = CheckCurrentLevelCompleted();
				if (levelCompleted)
				{
					AutoSave.DeleteFile();
					CanPauseGame = false;
					groundControllerScript.fieldRestriction = true;
					groundControllerScript.resetNoBallVairables();
					CONTROLLER.fielderChangeIndex = 1;
					CONTROLLER.computerFielderChangeIndex = 0;
					Singleton<Scoreboard>.instance.Hide(boolean: true);
					Singleton<PreviewScreen>.instance.Hide(boolean: true);
					Singleton<BowlingScoreCard>.instance.InningsCompleted();
					UpdateLevelId();
					ResetCurrentMatchDetails();
				}
				else if (currentBall == 5 || CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchWickets >= CONTROLLER.totalWickets)
				{
					if (CONTROLLER.CurrentLevelCompleted <= CONTROLLER.LevelId)
					{
						CONTROLLER.LevelFailed = 1;
					}
					if (currentBall == 5)
					{
						//Singleton<AdIntegrate>.instance.DisplayInterestialAd();
					}
					AutoSave.DeleteFile();
					KitTable.SetKitValues(1);
					SaveSOLevelDetails();
					int strikerIndex = CONTROLLER.StrikerIndex;
					CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
					CONTROLLER.NonStrikerIndex = strikerIndex;
					if (ObscuredPrefs.HasKey("SuperOverDetail") && CONTROLLER.gameMode == "superover")
					{
						ObscuredPrefs.DeleteKey("SuperOverDetail");
						ObscuredPrefs.DeleteKey("superoverPlayerDetails");
					}
					Singleton<GroundController>.instance.action = 10;
					ShowSuperOverResult();
					ResetCurrentMatchDetails();
				}
				else
				{
					BowlNextBall();
				}
				return;
			}
			levelCompleted = CheckCurrentLevelCompletedBowling();
			if (levelCompleted)
			{
				if (CONTROLLER.CurrentLevelCompleted <= CONTROLLER.LevelId)
				{
					CONTROLLER.LevelFailed = 1;
				}
				KitTable.SetKitValues(1);
				SaveSOLevelDetails();
				int strikerIndex2 = CONTROLLER.StrikerIndex;
				CONTROLLER.StrikerIndex = CONTROLLER.NonStrikerIndex;
				CONTROLLER.NonStrikerIndex = strikerIndex2;
				if (ObscuredPrefs.HasKey("SuperOverDetail") && CONTROLLER.gameMode == "superover")
				{
					ObscuredPrefs.DeleteKey("SuperOverDetail");
					ObscuredPrefs.DeleteKey("superoverPlayerDetails");
				}
				Singleton<GroundController>.instance.action = 10;
				ShowSuperOverResult();
				ResetCurrentMatchDetails();
			}
			else if (currentBall == 5 || CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].currentMatchWickets >= CONTROLLER.totalWickets)
			{
				AutoSave.DeleteFile();
				CanPauseGame = false;
				groundControllerScript.fieldRestriction = true;
				groundControllerScript.resetNoBallVairables();
				CONTROLLER.fielderChangeIndex = 1;
				CONTROLLER.computerFielderChangeIndex = 0;
				CONTROLLER.canShowbannerGround = 0;
				Singleton<Scoreboard>.instance.Hide(boolean: true);
				Singleton<PreviewScreen>.instance.Hide(boolean: true);
				Singleton<BowlingScoreCard>.instance.InningsCompleted();
				UpdateLevelId();
				ResetCurrentMatchDetails();
			}
			else
			{
				BowlNextBall();
			}
			return;
		}
		if (CONTROLLER.PlayModeSelected == 5)
		{
			inningsCompleted = CheckForInningsComplete();
			if (inningsCompleted)
			{
				AutoSave.DeleteFile();
				CanPauseGame = false;
				groundControllerScript.fieldRestriction = true;
				groundControllerScript.resetNoBallVairables();
				CONTROLLER.fielderChangeIndex = 1;
				CONTROLLER.computerFielderChangeIndex = 0;
				Singleton<Scoreboard>.instance.Hide(boolean: true);
				Singleton<PreviewScreen>.instance.Hide(boolean: true);
				Singleton<BowlingScoreCard>.instance.InningsCompleted();
				UpdateChaseTargetLevel();
				Singleton<SuperChaseResult>.instance.ShowMe();
				ResetCurrentMatchDetails();
			}
			else if (currentBall == 5)
			{
				KitTable.SetKitValues(1);
				groundControllerScript.showPreviewCamera(status: false);
				NewOver();
			}
			else
			{
				Singleton<Scoreboard>.instance.CTTargetToWin();
				BowlNextBall();
			}
			return;
		}
		inningsCompleted = CheckForInningsComplete();
		if (inningsCompleted)
		{
			float num = 0.7f * (float)CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores;
			CanPauseGame = false;
			groundControllerScript.fieldRestriction = true;
			groundControllerScript.resetNoBallVairables();
			CONTROLLER.fielderChangeIndex = 1;
			CONTROLLER.computerFielderChangeIndex = 0;
			Singleton<Scoreboard>.instance.Hide(boolean: true);
			if (CONTROLLER.PlayModeSelected == 6)
			{
				ScoreBoardMultiPlayer.instance.Hide(boolean: true);
			}
			Singleton<PreviewScreen>.instance.Hide(boolean: true);
			if (CONTROLLER.PlayModeSelected != 6)
			{
				CONTROLLER.InningsCompleted = true;
			}
			Singleton<BowlingScoreCard>.instance.InningsCompleted();
			if (CONTROLLER.currentInnings == 0)
			{
				if (CONTROLLER.PlayModeSelected != 6)
				{
					ShowBattingScoreCard();
				}
			}
			else
			{
				checkForDemoPlay();
				Singleton<GameOverScreen>.instance.Hide(boolean: false);
			}
		}
		else if (currentBall == 5)
		{
			if (CONTROLLER.PlayModeSelected != 6)
			{
				KitTable.SetKitValues(1);
				CounterPerOversPlayed++;
				ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterPerOversPlayed", CounterPerOversPlayed);
				groundControllerScript.showPreviewCamera(status: false);
				NewOver();
			}
		}
		else if (CONTROLLER.PlayModeSelected != 6)
		{
			BowlNextBall();
		}
	}

	private void SpaceTextValue()
	{
		skipStatus = string.Empty;
	}

	public bool CheckIfAlreadyAchieved(int index)
	{
		string text = AchievementTable.AchievementName[index];
		text = text + " Trigger" + CONTROLLER.PlayModeSelected;
		if (ObscuredPrefs.HasKey(text))
		{
			if (ObscuredPrefs.GetInt(text) == 1)
			{
				return false;
			}
			ObscuredPrefs.SetInt(text, 1);
			return true;
		}
		ObscuredPrefs.SetInt(text, 1);
		return true;
	}

	private bool CheckIfAlreadyShown(int index)
	{
		string text = AchievementTable.AchievementName[index];
		text += " Popup";
		if (ObscuredPrefs.HasKey(text))
		{
			if (ObscuredPrefs.GetInt(text) == 1)
			{
				return true;
			}
			ObscuredPrefs.SetInt(text, 1);
			return false;
		}
		ObscuredPrefs.SetInt(text, 1);
		return false;
	}

	private void CanShowPopupAgain(int index)
	{
		string text = AchievementTable.AchievementName[index];
		text += " Popup";
		ObscuredPrefs.SetInt(text, 0);
	}

	public void DeleteAchievementFlags()
	{
		//for (int i = 1; i < 16; i++)
		//{
		//	string key = AchievementTable.AchievementName[i] + " Trigger" + CONTROLLER.PlayModeSelected;
		//	ObscuredPrefs.SetInt(key, 0);
		//}
		//for (int j = 1; j < 16; j++)
		//{
		//	string key = AchievementTable.AchievementName[j] + " Popup";
		//	ObscuredPrefs.SetInt(key, 0);
		//}
	}

	private void ChallengeCompletedCheckpoint()
	{
	}

	private bool CheckForChallengeComplete()
	{
		if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls >= 30)
		{
			return true;
		}
		if (CONTROLLER.ChallengeNumber == 2 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets >= CONTROLLER.totalWickets)
		{
			CONTROLLER.ChallengeStatus = "fail";
			return true;
		}
		return false;
	}

	public bool CheckForInningsComplete()
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			if (CONTROLLER.currentInnings < 2)
			{
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets1 >= CONTROLLER.totalWickets)
				{
					return true;
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= CONTROLLER.totalWickets)
			{
				return true;
			}
			int num = ((CONTROLLER.currentInnings >= 2) ? CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchScores2 : CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchScores1);
			if (CONTROLLER.currentInnings == 3 && num > CONTROLLER.TargetToChase)
			{
				return true;
			}
			if (CONTROLLER.currentInnings < 2)
			{
				if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].isDeclared1)
				{
					return true;
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].isDeclared2)
			{
				return true;
			}
			return false;
		}
		if (CONTROLLER.PlayModeSelected == 6)
		{
			return false;
		}
		int currentMatchScores = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores;
		if (CONTROLLER.PlayModeSelected == 5)
		{
			if (currentMatchScores >= CONTROLLER.TargetToChase)
			{
				SuccessfulChase = true;
				Singleton<SuperChaseResult>.instance.SetResult(SuccessfulChase);
				return true;
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls >= CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6 || CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets >= 10)
			{
				SuccessfulChase = false;
				Singleton<SuperChaseResult>.instance.SetResult(SuccessfulChase);
				return true;
			}
		}
		else
		{
			if (CONTROLLER.currentInnings == 1 && currentMatchScores >= CONTROLLER.TargetToChase)
			{
				if (CONTROLLER.myTeamIndex == CONTROLLER.BattingTeamIndex && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets == 0 && CheckIfAlreadyAchieved(2))
				{
					IncrementAchievement(2);
				}
				return true;
			}
			if (CONTROLLER.currentInnings == 1 && (float)currentMatchScores >= (float)CONTROLLER.TargetToChase * 0.9f && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets == 0 && CONTROLLER.myTeamIndex == CONTROLLER.BattingTeamIndex)
			{
				string text = AchievementTable.AchievementName[2];
				text = text + " Trigger" + CONTROLLER.PlayModeSelected;
				if (!CheckIfAlreadyShown(2) && ObscuredPrefs.GetInt(text) == 0)
				{
					Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[2], 2);
				}
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls >= CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6)
			{
				return true;
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets >= CONTROLLER.totalWickets)
			{
				if (CONTROLLER.myTeamIndex == CONTROLLER.BowlingTeamIndex && CheckIfAlreadyAchieved(1))
				{
					IncrementAchievement(1);
				}
				return true;
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets == CONTROLLER.totalWickets - 1)
			{
				string text2 = AchievementTable.AchievementName[1];
				text2 = text2 + " Trigger" + CONTROLLER.PlayModeSelected;
				if (!CheckIfAlreadyShown(1) && ObscuredPrefs.GetInt(text2) == 0)
				{
					Singleton<AchievementNotif>.instance.AlmostTherePopup(AchievementTable.AchievementDetail[1], 1);
				}
			}
		}
		return false;
	}

	private void IncrementAchievement(int index)
	{
		Singleton<AchievementsSyncronizer>.instance.UpdateAchievement(index);
		AchievementTable.SetAchievementValues(index);
		Singleton<AchievementNotif>.instance.AchievedPopup(index);
		switch (index)
		{
		}
	}

	private void InningsCompleteFn()
	{
		if (CONTROLLER.PlayModeSelected == 4)
		{
			AutoSave.DeleteFile();
			groundControllerScript.fieldRestriction = true;
			CONTROLLER.fielderChangeIndex = 1;
			CONTROLLER.computerFielderChangeIndex = 0;
			Singleton<Scoreboard>.instance.Hide(boolean: true);
			Singleton<PreviewScreen>.instance.Hide(boolean: true);
			Singleton<BowlingScoreCard>.instance.InningsCompleted();
			return;
		}
		groundControllerScript.fieldRestriction = true;
		CONTROLLER.fielderChangeIndex = 1;
		CONTROLLER.computerFielderChangeIndex = 0;
		Singleton<Scoreboard>.instance.Hide(boolean: true);
		Singleton<PreviewScreen>.instance.Hide(boolean: true);
		Singleton<BowlingScoreCard>.instance.InningsCompleted();
		if (CONTROLLER.PlayModeSelected != 7)
		{
			if (CONTROLLER.currentInnings == 0)
			{
				Singleton<TargetScreen>.instance.Hide(boolean: false);
			}
			else
			{
				Singleton<GameOverScreen>.instance.Hide(boolean: false);
			}
		}
		else
		{
			ShowBattingScoreCard();
		}
	}

	public void NewOver()
	{
		CanPauseGame = false;
		fourInOver = false;
		sixInOver = false;
		maxRunsReached = false;
		if (!CONTROLLER.PowerPlay && CONTROLLER.myTeamIndex == CONTROLLER.BattingTeamIndex)
		{
			CONTROLLER.computerFielderChangeIndex = UnityEngine.Random.Range(6, 11);
		}
		else if (CONTROLLER.PowerPlay && CONTROLLER.myTeamIndex == CONTROLLER.BattingTeamIndex)
		{
			CONTROLLER.computerFielderChangeIndex = UnityEngine.Random.Range(1, 6);
		}
		currentBall = -1;
		runsScoredInOver = 0;
		Singleton<Scoreboard>.instance.NewOver();
		Singleton<Scoreboard>.instance.HidePause(boolean: true);
		Singleton<Scoreboard>.instance.Hide(boolean: true);
		Singleton<PreviewScreen>.instance.Hide(boolean: true);
		Singleton<BowlingControls>.instance.Hide(boolean: true);
		Singleton<BattingControls>.instance.Hide(boolean: true);
		Singleton<BattingScoreCard>.instance.SCTeamName.DOFade(1f, 0f);
		if (CONTROLLER.PlayModeSelected == 6)
		{
			Singleton<BowlingScoreCard>.instance.Continue();
		}
		else
		{
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls % CONTROLLER.greedyRefreshRate == 0)
			{
				SingletoneBase<GreedyCampaignLoader>.Instance.refreshGreedyAds();
			}
			if (CONTROLLER.PlayModeSelected == 5)
			{
				Singleton<Scoreboard>.instance.CTTargetToWin();
			}
			ShowBattingScoreCard();
		}
		if (!CONTROLLER.isFromAutoPlay)
		{
			Singleton<BowlingScoreCard>.instance.OverCompleted();
		}
		groundControllerScript.NewOver();
		Singleton<BattingControls>.instance.LoftMeterFill(CONTROLLER.StrikerIndex);
	}

	public void ShowBowlingScoreCard()
	{
		if (!isGamePaused)
		{
			action = 1;
		}
		Singleton<BowlingScoreCard>.instance.Hide(boolean: false);
	}

	public void UpdatePreview(Dictionary<string, object> hash)
	{
		Singleton<PreviewScreen>.instance.UpdatePreviewScreen(hash);
	}

	public void SendBowlingDatasToGame()
	{
		bowlingControl = 2;
		groundControllerScript.StartBowling();
	}

	public void InitAnimation(int type)
	{
		CanPauseGame = false;
		Singleton<Scoreboard>.instance.HidePause(boolean: true);
		if (CONTROLLER.stumpingAttempted || CONTROLLER.runoutThirdUmpireAppeal)
		{
			ReplayCompletedCalledFromMe = true;
			ReplayIsNotShown();
		}
		else if (CONTROLLER.PlayModeSelected == 6 || type == 5 || !canShowAnimation)
		{
			if (Singleton<GroundController>.instance.getOverStepBall() && (type == 0 || type == 1))
			{
				StartCoroutine(Singleton<GroundController>.instance.updateBoundaryBall());
			}
			else
			{
				AnimationCompleted();
			}
		}
		else if (canShowAnimation)
		{
			Singleton<AnimationScreen>.instance.StartAnimation(type);
		}
		stateVar = type;
	}

	public void setReplayCompletedVariable()
	{
		ReplayCompletedCalledFromMe = true;
	}

	public void AnimationCompleted()
	{
		Singleton<UILookAt>.instance.show(flag: false);
		groundControllerScript.HideReplay();
		if (canShowAnimation)
		{
			ReplayCompletedCalledFromMe = true;
			if (stateVar == 5 && CONTROLLER.canShowReplay)
			{
				SkipReplay();
				stateVar = -1;
			}
			else if (CONTROLLER.canShowReplay)
			{
				GameIsOnReplay();
				groundControllerScript.ShowReplay();
			}
			else
			{
				ReplayIsNotShown();
			}
		}
	}

	public void ReplayIsNotShown()
	{
		skipStatus = "replay";
		CONTROLLER.ReplayShowing = true;
		SkipReplay();
	}

	public void GameIsOnReplay()
	{
		CanPauseGame = false;
		CONTROLLER.ReplayShowing = true;
		Singleton<Scoreboard>.instance.Hide(boolean: true);
		Singleton<PreviewScreen>.instance.Hide(boolean: true);
		Singleton<BowlingControls>.instance.Hide(boolean: true);
		Singleton<BattingControls>.instance.Hide(boolean: true);
		Singleton<PauseGameScreen>.instance.Hide(boolean: true);
		if (SkipBtn != null)
		{
			ActionTxt.text = LocalizationData.instance.getText(519);
			ActionTxt.color = new Color(1f, 1f, 1f, 1f);
			ActionTxt.gameObject.SetActive(value: true);
			SkipBtn.gameObject.SetActive(value: true);
			skipStatus = "replay";
			timeOutTextBlinkTime = Time.time;
			ReplayStartTime = Time.time;
			blinkStatus = 0;
		}
	}

	public void GameIsOnStumpingReplay()
	{
		CONTROLLER.ReplayShowing = true;
		Singleton<PreviewScreen>.instance.Hide(boolean: true);
		CanPauseGame = false;
		Singleton<Scoreboard>.instance.Hide(boolean: true);
		if (SkipBtn != null)
		{
			ActionTxt.text = LocalizationData.instance.getText(518);
			ActionTxt.color = new Color(1f, 1f, 1f, 1f);
			ActionTxt.gameObject.SetActive(value: true);
			SkipBtn.gameObject.SetActive(value: true);
			skipStatus = "stumpingReplay";
		}
	}

	public void GameIsNotOnStumpingReplay()
	{
		if (SkipBtn != null)
		{
			ActionTxt.gameObject.SetActive(value: false);
			SkipBtn.gameObject.SetActive(value: false);
			skipStatus = string.Empty;
			Singleton<Scoreboard>.instance.Hide(boolean: true);
			Singleton<PreviewScreen>.instance.Hide(boolean: true);
			Singleton<BowlingControls>.instance.Hide(boolean: true);
			Singleton<BattingControls>.instance.Hide(boolean: true);
			Singleton<PauseGameScreen>.instance.Hide(boolean: true);
		}
	}

	public void GameIsOnThirdUmpireRunoutReplay()
	{
		CanPauseGame = false;
		CONTROLLER.ReplayShowing = true;
		Singleton<Scoreboard>.instance.Hide(boolean: true);
		Singleton<PreviewScreen>.instance.Hide(boolean: true);
		Singleton<BowlingControls>.instance.Hide(boolean: true);
		Singleton<BattingControls>.instance.Hide(boolean: true);
		Singleton<PauseGameScreen>.instance.Hide(boolean: true);
		if (SkipBtn != null)
		{
			ActionTxt.text = LocalizationData.instance.getText(518);
			ActionTxt.color = new Color(1f, 1f, 1f, 1f);
			ActionTxt.gameObject.SetActive(value: true);
			SkipBtn.gameObject.SetActive(value: true);
			skipStatus = "thirdUmpireRunoutReplay";
		}
	}

	public void SkipReplay()
	{
		if (skipStatus == "replay")
		{
			groundControllerScript.SkipReplay();
		}
		else if (skipStatus == "stumpingReplay")
		{
			groundControllerScript.SkipStumpingReplay();
		}
		else if (skipStatus == "thirdUmpireRunoutReplay")
		{
			groundControllerScript.SkipThirdUmpireRunoutReplay();
		}
	}

	public void ReplayCompleted()
	{
		if (!CONTROLLER.ReplayShowing)
		{
			return;
		}
		if (SkipBtn != null)
		{
			ActionTxt.gameObject.SetActive(value: false);
			SkipBtn.gameObject.SetActive(value: false);
			skipStatus = string.Empty;
		}
		CONTROLLER.ReplayShowing = false;
		if (CONTROLLER.PlayModeSelected == 6)
		{
			ScoreBoardMultiPlayer.instance.Hide(boolean: false);
		}
		else
		{
			Singleton<Scoreboard>.instance.Hide(boolean: false);
		}
		Singleton<PreviewScreen>.instance.Hide(boolean: false);
		if (IsWicketBall)
		{
			if (CONTROLLER.PlayModeSelected == 6)
			{
				batsmanExitStopped();
			}
			else
			{
				InitBatsmanInfo();
			}
		}
		else if (ReplayCompletedCalledFromMe && !CONTROLLER.isJokerCall && !CONTROLLER.isFreeHit)
		{
			CheckForOverComplete();
		}
		ReplayCompletedCalledFromMe = false;
	}

	public void GamePaused(bool boolean)
	{
		if (CONTROLLER.PlayModeSelected == 6)
		{
			return;
		}
		if (boolean)
		{
			CanResumeGame = true;
			CanPauseGame = false;
			Singleton<BatsmanRecord>.instance.Hide(boolean: true);
			Singleton<BoundaryAnimation2>.instance.HideMe();
			Singleton<BatsmanInfo>.instance.Hide(boolean: true);
			Singleton<Scoreboard>.instance.Hide(boolean: true);
			Singleton<PreviewScreen>.instance.Hide(boolean: true);
			Singleton<BowlingControls>.instance.Hide(boolean: true);
			Singleton<BattingControls>.instance.Hide(boolean: true);
			if (!Singleton<PauseGameScreen>.instance.midPageGO.activeSelf)
			{
				Singleton<PauseGameScreen>.instance.Hide(boolean: false);
			}
			if (CONTROLLER.currentInnings == 1)
			{
				Singleton<PauseGameScreen>.instance.NeedText1.gameObject.SetActive(value: true);
				Singleton<PauseGameScreen>.instance.NeedText2.gameObject.SetActive(value: true);
			}
			else
			{
				Singleton<PauseGameScreen>.instance.NeedText1.gameObject.SetActive(value: false);
				Singleton<PauseGameScreen>.instance.NeedText2.gameObject.SetActive(value: false);
			}
			Singleton<Scoreboard>.instance.HidePause(boolean: true);
		}
		isGamePaused = boolean;
		groundControllerScript.GameIsPaused(boolean);
		if (!boolean && !gameQuit)
		{
			CanPauseGame = true;
			Singleton<BoundaryAnimation2>.instance.HideMe();
			if (action == 2)
			{
				Singleton<BatsmanRecord>.instance.Hide(boolean: false);
				SkipBtn.gameObject.SetActive(value: true);
			}
			if (action == 3)
			{
				Singleton<BatsmanInfo>.instance.Hide(boolean: false);
				SkipBtn.gameObject.SetActive(value: true);
			}
			if (action == 4)
			{
				Singleton<BowlingControls>.instance.Hide(boolean: false);
				Singleton<Scoreboard>.instance.Hide(boolean: false);
				Singleton<PreviewScreen>.instance.Hide(boolean: false);
			}
			else if (action == -1 || action == 6)
			{
				Singleton<Scoreboard>.instance.Hide(boolean: false);
				Singleton<PreviewScreen>.instance.Hide(boolean: false);
			}
			else if (action == 5)
			{
				Singleton<PreviewScreen>.instance.Hide(boolean: false);
				Singleton<BowlingControls>.instance.Hide(boolean: false);
				Singleton<BattingControls>.instance.Hide(boolean: false);
				Singleton<Scoreboard>.instance.Hide(boolean: false);
			}
		}
	}

	private bool IsPointerOverUIObject()
	{
		PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
		pointerEventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerEventData, list);
		return list.Count > 0;
	}

	public void HideUIMenu(bool hide)
	{
		if (hide)
		{
			_transform.localPosition = new Vector3(_transform.localPosition.x, _transform.localPosition.y, -10f);
		}
		else
		{
			_transform.localPosition = new Vector3(_transform.localPosition.x, _transform.localPosition.y, 1f);
		}
	}

	public void GamePauseMenuSelected()
	{
		CanResumeGame = false;
	}

	public void AgainToGamePauseScreen()
	{
		CanResumeGame = true;
	}

	private bool CheckForAllOut()
	{
		bool result = false;
		if (NewBatsmanIndex > CONTROLLER.totalWickets)
		{
			result = true;
		}
		return result;
	}

	public Vector3 GetScreenPoint(Vector3 pos)
	{
		return renderCamera.WorldToScreenPoint(pos);
	}

	private void GetKeyBoardInput()
	{
		if (Input.GetKeyDown(KeyCode.A) && !isGamePaused)
		{
			if (action == 4 && bowlingControl == 0)
			{
				Singleton<BowlingControls>.instance.ChangeSwingParameter();
			}
		}
		else if ((Input.GetKeyDown(KeyCode.S) || Input.GetMouseButtonDown(0)) && !isGamePaused && !IsPointerOverUIObject())
		{
			if (Singleton<FreeHitValidation>.instance.holder.activeSelf)
			{
				return;
			}
			if (CONTROLLER.ReplayShowing)
			{
				SkipReplay();
			}
			else if (skipStatus == "stumpingReplay")
			{
				groundControllerScript.SkipStumpingReplay();
			}
			else if (action == -20)
			{
				introCompleted();
			}
			else if (action == 0)
			{
				Singleton<BattingScoreCard>.instance.Hide(boolean: true);
				ShowBowlingScoreCard();
			}
			else if (action == 1)
			{
				Singleton<BowlingScoreCard>.instance.Continue();
			}
			else if (action == 2)
			{
				if (Singleton<Intro>.instance != null)
				{
					Singleton<Intro>.instance.batsmanEntryStopped();
				}
			}
			else if (action == 3)
			{
				if (Singleton<Intro>.instance != null)
				{
					Singleton<Intro>.instance.batsmanExitStopped();
				}
			}
			else if (action == 4)
			{
				if (bowlingControl == 0)
				{
					bowlingControl = 1;
					Singleton<BowlingControls>.instance.LockSpeed();
				}
				else if (bowlingControl == 1)
				{
					bowlingControl = 2;
					Singleton<BowlingControls>.instance.LockAngle();
				}
				else if (bowlingControl == 2)
				{
					Singleton<Tutorial>.instance.hideTutorial();
					Time.timeScale = 1f;
				}
			}
			else if (action == 5)
			{
				Singleton<Tutorial>.instance.hideTutorial();
				Time.timeScale = 1f;
			}
			else if (action == 10)
			{
				action = -1;
			}
		}
		int touchCount = Input.touchCount;
		if (!isGamePaused && touchCount == 1 && !isTouched)
		{
			isTouched = true;
			if (action == -20)
			{
				introCompleted();
			}
			else if (action == 2)
			{
				if (Singleton<Intro>.instance != null)
				{
					Singleton<Intro>.instance.batsmanEntryStopped();
				}
			}
			else if (action == 3 && Singleton<Intro>.instance != null)
			{
				Singleton<Intro>.instance.batsmanExitStopped();
			}
		}
		if (Input.GetMouseButtonUp(0) && !IsPointerOverUIObject())
		{
			touchCount = 0;
			isTouched = false;
		}
		if (EscapeGameResumed)
		{
			CheckForResume();
		}
	}

	public string GetOverStr()
	{
		int num = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6;
		int num2 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls % 6;
		string result = num + "." + num2;
		if (num >= PowerPlayOver)
		{
			CONTROLLER.PowerPlay = false;
		}
		else
		{
			CONTROLLER.PowerPlay = true;
		}
		if (num >= SlogOverValue)
		{
			CONTROLLER.SlogOvers = true;
		}
		else
		{
			CONTROLLER.SlogOvers = false;
		}
		float num3 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores;
		float num4 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls;
		float num5 = 0f;
		if (num4 > 0f)
		{
			num5 = num3 / num4 * 6f;
			int num6 = (int)(num5 * 100f);
			num5 = (CONTROLLER.RunRate = num6 / 100);
		}
		if (CONTROLLER.currentInnings == 1)
		{
			float num7 = (float)CONTROLLER.TargetToChase - num3;
			float num8 = (float)(CONTROLLER.totalOvers * 6) - num4;
			if (num8 > 0f)
			{
				num5 = num7 / num8 * 6f;
				int num9 = (int)(num5 * 100f);
				num5 = (CONTROLLER.ReqRunRate = num9 / 100);
			}
		}
		return result;
	}

	public void SetFielders()
	{
		CONTROLLER.FieldersArray = new int[11];
		CONTROLLER.FieldersArray[0] = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].KeeperIndex;
		CONTROLLER.FieldersArray[10] = CONTROLLER.CurrentBowlerIndex;
		int num = 0;
		for (int i = 0; i < CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].PlayerList.Length; i++)
		{
			if (i != CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].KeeperIndex && i != CONTROLLER.CurrentBowlerIndex)
			{
				num++;
				CONTROLLER.FieldersArray[num] = i;
			}
		}
	}

	public void ShowTutorial(int tutorialValue)
	{
		if (CONTROLLER.PlayModeSelected == 6)
		{
			return;
		}
		Singleton<Tutorial>.instance.hideTutorial();
		if (CONTROLLER.tutorialToggle == 1)
		{
			Singleton<Tutorial>.instance.tutorialToggle.SetActive(value: true);
		}
		else
		{
			Singleton<Tutorial>.instance.tutorialToggle.SetActive(value: false);
		}
		if (!CONTROLLER.ReplayShowing && !(CONTROLLER.TargetPlatform == "standalone") && !(CONTROLLER.TargetPlatform == "web") && CONTROLLER.tutorialToggle != 0)
		{
			switch (tutorialValue)
			{
			case 0:
				Singleton<Tutorial>.instance.showPositionHolder();
				break;
			case 1:
				Singleton<Tutorial>.instance.showShotHolder();
				break;
			case 2:
				Singleton<Tutorial>.instance.showBowlingHolder();
				break;
			}
		}
	}

	public bool canShowTutorial()
	{
		if (CONTROLLER.ReplayShowing || CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex || CONTROLLER.TargetPlatform == "standalone" || CONTROLLER.TargetPlatform == "web" || CONTROLLER.tutorialToggle == 0 || CONTROLLER.PlayModeSelected == 6)
		{
			return false;
		}
		return true;
	}

	public void EnableMovement(bool boolean)
	{
		if (boolean)
		{
			EnableShot(boolean: true);
			userAction = 10;
			if (CONTROLLER.tutorialToggle == 1)
			{
				ShowTutorial(0);
			}
			Singleton<GroundController>.instance.isnextball = true;
		}
		else
		{
			ShowTutorial(-1);
		}
	}

	public void DetectBatsmanMoveMouse()
	{
		if (Input.GetMouseButton(0))
		{
			if (prevMousePos.x == 0f && prevMousePos.y == 0f)
			{
				prevMousePos = Input.mousePosition;
			}
			Vector2 vector = new Vector2(Input.mousePosition.x - prevMousePos.x, Input.mousePosition.y - prevMousePos.y);
			prevMousePos = Input.mousePosition;
			if (vector.x < 0f)
			{
				MoveLeft(boolean: true);
			}
			else
			{
				MoveLeft(boolean: false);
			}
			if (vector.x > 0f)
			{
				MoveRight(boolean: true);
			}
			else
			{
				MoveRight(boolean: false);
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			MoveLeft(boolean: false);
			MoveRight(boolean: false);
		}
	}

	public void DetectBatsmanMove()
	{
		int touchCount = Input.touchCount;
		Touch[] touches = Input.touches;
		if (touchCount > 0)
		{
			Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;
			if (deltaPosition.x < 0f)
			{
				MoveLeft(boolean: true);
			}
			else
			{
				MoveLeft(boolean: false);
			}
			if (deltaPosition.x > 0f)
			{
				MoveRight(boolean: true);
			}
			else
			{
				MoveRight(boolean: false);
			}
		}
		else
		{
			MoveLeft(boolean: false);
			MoveRight(boolean: false);
		}
	}

	public void MoveLeft(bool boolean)
	{
		groundControllerScript.MoveLeftSide(boolean);
	}

	public void MoveRight(bool boolean)
	{
		groundControllerScript.MoveRightSide(boolean);
	}

	public void EnableShot(bool boolean)
	{
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex && !CONTROLLER.ReplayShowing)
		{
			Singleton<BattingControls>.instance.Hide(boolean: false);
			Singleton<BattingControls>.instance.EnableShot(boolean);
			Singleton<BattingControls>.instance.ChangeButtonImage(0);
			if (boolean)
			{
				action = 5;
			}
			else
			{
				action = -1;
			}
		}
		if (!boolean)
		{
			ShowTutorial(-1);
		}
	}

	public void EnableShotSelection(bool boolean)
	{
		if (boolean)
		{
			userAction = 11;
		}
	}

	private bool GetCanMakeShot(Vector2 pos)
	{
		if (isGamePaused)
		{
			return false;
		}
		return true;
	}

	public void DetectBatsmanShotMouse()
	{
		if (Input.GetMouseButtonDown(0) && !NewTouch)
		{
			if (!GetCanMakeShot(Input.mousePosition))
			{
				return;
			}
			NewTouch = true;
			isTouchEnded = false;
			TouchInitPos = Input.mousePosition;
			touchInitTime = Time.time;
		}
		if (Input.GetMouseButtonUp(0) && NewTouch)
		{
			TouchEndPos = Input.mousePosition;
			GetTheShot();
		}
	}

	public void DetectBatsmanShot()
	{
		int touchCount = Input.touchCount;
		if (touchCount <= 0 || shotCompleted)
		{
			return;
		}
		touchPhase = Input.touches;
		bool flag = true;
		if (touchPhase.Length > 0 && touchPhase[0].phase == TouchPhase.Began)
		{
			flag = GetCanMakeShot(touchPhase[0].position);
			if (!flag)
			{
				return;
			}
		}
		if (flag && !NewTouch && (touchPhase[0].phase == TouchPhase.Began || touchPhase[0].phase == TouchPhase.Stationary))
		{
			NewTouch = true;
			isTouchEnded = false;
			TouchInitPos = touchPhase[0].position;
			touchInitTime = Time.time;
		}
		else if (touchPhase[0].phase == TouchPhase.Ended && !isTouchEnded)
		{
			TouchEndPos = touchPhase[0].position;
			GetTheShot();
		}
	}

	public void GetShotSelected()
	{
		if (!NewTouch || shotCompleted || inningsCompleted)
		{
			return;
		}
		if ((CONTROLLER.TargetPlatform == "ios" || CONTROLLER.TargetPlatform == "android") && !Application.isEditor && touchPhase.Length > 0)
		{
			touchPhase = Input.touches;
			if (!isTouchEnded)
			{
				TouchEndPos = touchPhase[0].position;
				GetTheShot();
			}
		}
		else if (NewTouch && !isTouchEnded)
		{
			TouchEndPos = Input.mousePosition;
			GetTheShot();
		}
	}

	public float DistanceBetweenTwoVector2(Transform go1, Transform go2)
	{
		float num = go1.position.x - go2.position.x;
		float num2 = go1.position.z - go2.position.z;
		return Mathf.Sqrt(num * num + num2 * num2);
	}

	public void GetTheShot()
	{
		if (!NewTouch || isTouchEnded)
		{
			return;
		}
		NewTouch = false;
		isTouchEnded = true;
		touchInitTime = 0f;
		shotCompleted = true;
		float num = TouchEndPos.x - TouchInitPos.x;
		float num2 = TouchEndPos.y - TouchInitPos.y;
		if (num != 0f || num2 != 0f)
		{
			float num3 = Mathf.Atan2(num2, num) * 57.29578f;
			num3 = (num3 + 360f) % 360f;
			if (num3 == 0f)
			{
				selectedAngle = 5;
			}
			else if ((num3 < 22.5f && num3 >= -22.5f) || (num3 < 360f && num3 >= 337.5f))
			{
				selectedAngle = 7;
			}
			else if (num3 < 67.5f && num3 >= 22.5f)
			{
				selectedAngle = 6;
			}
			else if (num3 < 112.5f && num3 >= 67.5f)
			{
				selectedAngle = 5;
			}
			else if (num3 < 157.5f && num3 >= 112.5f)
			{
				selectedAngle = 4;
			}
			else if (num3 < 202.5f && num3 >= 157.5f)
			{
				selectedAngle = 3;
			}
			else if (num3 < 247.5f && num3 >= 202.5f)
			{
				selectedAngle = 2;
			}
			else if (num3 < 292.5f && num3 >= 247.5f)
			{
				selectedAngle = 1;
			}
			else if (num3 < 337.5f && num3 >= 292.5f)
			{
				selectedAngle = 8;
			}
			Singleton<GroundController>.instance.BallAngle(num3);
			if (Singleton<BattingControls>.instance.InnerRing.sprite == Singleton<BattingControls>.instance.btnStates[0])
			{
				ShotSelected(isPower: false, selectedAngle);
			}
			else
			{
				ShotSelected(isPower: true, selectedAngle);
			}
		}
	}

	public void ShotSelected(bool isPower, int SelectedAngle)
	{
		userAction = -1;
		groundControllerScript.ShotSelected(isPower, SelectedAngle);
	}

	public void EnableRun(bool boolean)
	{
		userAction = -1;
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex && !CONTROLLER.ReplayShowing)
		{
			if (boolean)
			{
				action = 6;
			}
			else
			{
				action = -1;
			}
			StartCoroutine(Singleton<BattingControls>.instance.EnableRun(boolean));
		}
	}

	public void EnableCancelRun(bool boolean)
	{
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex && !CONTROLLER.ReplayShowing)
		{
			Singleton<BattingControls>.instance.EnableCancelRun(boolean);
		}
	}

	public void InitRun(bool boolean)
	{
		groundControllerScript.InitRun(boolean);
		Invoke("initRunFalse", 1f);
	}

	private void initRunFalse()
	{
		groundControllerScript.InitRun(boolean: false);
	}

	public void CancelRun()
	{
		groundControllerScript.touchCancelRun();
	}

	public void ReachedCrease()
	{
		Singleton<BattingControls>.instance.RunCompleted();
	}

	public void ShowQuitPopup()
	{
		GameObject original = Resources.Load("Prefabs/GameQuitConfirm") as GameObject;
		GameObject gameObject = UnityEngine.Object.Instantiate(original);
		gameObject.name = "GameQuitConfirm";
		gameObject.transform.localPosition = new Vector3(0f, 0f, 1f);
	}

	public void GameQuitted()
	{
		gameQuit = true;
		CONTROLLER.isQuit = true;
		GamePaused(boolean: false);
		stopCommentarySnd();
		groundControllerScript.fieldRestriction = true;
		CONTROLLER.fielderChangeIndex = 1;
		CONTROLLER.computerFielderChangeIndex = 0;
		ResetAllLocalVariables();
		if (CONTROLLER.sndController != null)
		{
			CONTROLLER.sndController.RemoveGameSounds();
		}
		CONTROLLER.SceneIsLoading = true;
		if (CONTROLLER.PlayModeSelected == 6)
		{
			CONTROLLER.MPInningsCompleted = false;
			CONTROLLER.screenToDisplay = "landingPage";
			Singleton<GameOverScreen>.instance.multiplayerPanel.SetActive(value: false);
			Singleton<NavigationBack>.instance.deviceBack = null;
			Singleton<GameOverScreen>.instance.loadingScreen.PanelTransition1("MainMenu");
		}
		else if (Singleton<TournamentFailedPopUp>.instance.showMe)
		{
			Singleton<TournamentFailedPopUp>.instance.ShowMe();
		}
		else
		{
			LoadMainMenuScene();
		}
	}

	public void LoadMainMenuScene()
	{
		Singleton<NavigationBack>.instance.deviceBack = null;
		Singleton<LoadingPanelTransition>.instance.PanelTransition1("MainMenu");
	}

	private void IncreaseConfidenceLevel(int runsScored, int validBall)
	{
		if (validBall == 0)
		{
			return;
		}
		float num = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].ConfidenceVal;
		float confidenceInc = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].ConfidenceInc;
		confidenceInc = ((CONTROLLER.totalOvers >= 10) ? (confidenceInc * (10f / (float)CONTROLLER.totalOvers)) : (confidenceInc * (4f / (float)CONTROLLER.totalOvers)));
		if (runsScored <= 0)
		{
			confidenceInc = -0.05f;
		}
		if (num + confidenceInc < 10f)
		{
			if (runsScored < 6)
			{
				num += confidenceInc;
			}
		}
		else
		{
			num = 10f;
		}
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].ConfidenceVal = num;
	}

	private int GetStrikeRate(int playerID)
	{
		float num = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.RunsScored;
		float num2 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.BallsPlayed;
		if (num2 > 0f)
		{
			int num3 = (int)(num / num2 * 100f);
			return (int)(Mathf.Round(num3 * 100) / 100f);
		}
		return 0;
	}

	private void CheckForResume()
	{
		if (Time.realtimeSinceStartup > EscapeResumedTime + 0.1f)
		{
			EscapeGameResumed = false;
			if (CanResumeGame)
			{
				Singleton<PauseGameScreen>.instance.Hide(boolean: true);
				GamePaused(boolean: false);
			}
		}
	}

	private void FormatNames()
	{
		//for (int i = 0; i < CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList.Length; i++)
		//{
		//	string text = string.Empty;
		//	string text2 = string.Empty;
		//	string[] array = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].PlayerName.Split(" "[0]);
		//	string[] array2 = new string[array.Length];
		//	for (int j = 0; j < array.Length; j++)
		//	{
		//		if (array[j] != string.Empty && array[j] != " ")
		//		{
		//			string text3 = array[j];
		//			string text4 = text3.Substring(0, 1);
		//			string text5 = text3.Substring(1);
		//			text4 = text4.ToUpper();
		//			text5 = text5.ToLower();
		//			text3 = text4 + text5;
		//			string text6 = ((j >= array.Length - 1) ? (text4 + text5) : text4);
		//			array[j] = text3;
		//			array2[j] = text6;
		//		}
		//	}
		//	for (int j = 0; j < array.Length; j++)
		//	{
		//		if (array[j] != string.Empty && array[j] != " ")
		//		{
		//			if (j < array.Length - 1)
		//			{
		//				text = text + array[j] + " ";
		//				text2 = text2 + array2[j] + " ";
		//			}
		//			else
		//			{
		//				text += array[j];
		//				text2 += array2[j];
		//			}
		//		}
		//	}
		//	if (text.Length > 12)
		//	{
		//		text = text.Substring(0, 12);
		//	}
		//	if (text2.Length > 12)
		//	{
		//		text2 = text2.Substring(0, 12);
		//	}
		//	CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].PlayerName = text;
		//	CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].ScoreboardName = text2;
		//	text = string.Empty;
		//	text2 = string.Empty;
		//	array = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].PlayerName.Split(" "[0]);
		//	array2 = new string[array.Length];
		//	for (int j = 0; j < array.Length; j++)
		//	{
		//		if (array[j] != string.Empty && array[j] != " ")
		//		{
		//			string text3 = array[j];
		//			string text4 = text3.Substring(0, 1);
		//			string text5 = text3.Substring(1);
		//			text4 = text4.ToUpper();
		//			text5 = text5.ToLower();
		//			text3 = text4 + text5;
		//			string text6 = ((j >= array.Length - 1) ? (text4 + text5) : text4);
		//			array[j] = text3;
		//			array2[j] = text6;
		//		}
		//	}
		//	for (int j = 0; j < array.Length; j++)
		//	{
		//		if (array[j] != string.Empty && array[j] != " ")
		//		{
		//			if (j < array.Length - 1)
		//			{
		//				text = text + array[j] + " ";
		//				text2 = text2 + array2[j] + " ";
		//			}
		//			else
		//			{
		//				text += array[j];
		//				text2 += array2[j];
		//			}
		//		}
		//	}
		//	if (text.Length > 12)
		//	{
		//		text = text.Substring(0, 12);
		//	}
		//	if (text2.Length > 12)
		//	{
		//		text2 = text2.Substring(0, 12);
		//	}
		//	CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].PlayerName = text;
		//	CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].ScoreboardName = text2;
		//}
	}

	private void TrimLastSpaces()
	{
		//for (int i = 0; i < CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList.Length; i++)
		//{
		//	string playerName = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].PlayerName;
		//	if (playerName[playerName.Length - 1] == ' ')
		//	{
		//		TrimSpaces(i, CONTROLLER.myTeamIndex);
		//	}
		//	playerName = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].PlayerName;
		//	if (playerName[playerName.Length - 1] == ' ')
		//	{
		//		TrimSpaces(i, CONTROLLER.opponentTeamIndex);
		//	}
		//}
	}

	private void TrimSpaces(int index, int teamIndex)
	{
		string empty = string.Empty;
		empty = CONTROLLER.TeamList[teamIndex].PlayerList[index].PlayerName;
		empty = empty.Substring(0, empty.Length - 1);
		CONTROLLER.TeamList[teamIndex].PlayerList[index].PlayerName = empty;
		if (empty[empty.Length - 1] == ' ')
		{
			TrimSpaces(index, teamIndex);
		}
	}

	public void UpdateAction(int pageID)
	{
		action = pageID;
	}

	public int GetAction()
	{
		return action;
	}

	public void PostGoogleAnalyticsEvent()
	{
		if (CONTROLLER.PlayModeSelected != 0 && CONTROLLER.PlayModeSelected != 1)
		{
		}
	}

	private string addToPreviousBall(string _ballStr, int _runs)
	{
		string text = _ballStr;
		string[] array = _ballStr.Split("^"[0]);
		string[] array2 = array[array.Length - 2].Split("|"[0]);
		int num = int.Parse(array2[0]);
		num += _runs;
		string text2 = num + "|" + array2[1];
		array[array.Length - 2] = text2;
		text = string.Empty;
		for (int i = 0; i < array.Length - 1; i++)
		{
			text = text + array[i] + "^";
		}
		return text;
	}

	private void CheckCustomInningsComplete()
	{
		if (CONTROLLER.ChallengeType == "bat")
		{
			if (CONTROLLER.ChallengeNumber != 0)
			{
			}
		}
		else if (!(CONTROLLER.ChallengeType == "bowl"))
		{
		}
	}

	private void initTestMatchDetails()
	{
		if (CONTROLLER.currentInnings == 0 || CONTROLLER.currentInnings == 2)
		{
			if (CONTROLLER.meFirstBatting == 0)
			{
				CONTROLLER.BatTeamIndex = 1;
				CONTROLLER.BowlTeamIndex = 0;
			}
			else
			{
				CONTROLLER.BatTeamIndex = 0;
				CONTROLLER.BowlTeamIndex = 1;
			}
		}
	}

	private void SetAIDeclarationParameters()
	{
		switch (CONTROLLER.Overs[ObscuredPrefs.GetInt("TMOvers")])
		{
		case 3:
			CONTROLLER.AIFirstInningsDeclareRuns = UnityEngine.Random.Range(15, 20);
			CONTROLLER.AIFirstInningsDeclareBalls = UnityEngine.Random.Range(10, 15);
			break;
		case 15:
			CONTROLLER.AIFirstInningsDeclareRuns = UnityEngine.Random.Range(150, 201);
			CONTROLLER.AIFirstInningsDeclareBalls = UnityEngine.Random.Range(100, 151);
			break;
		case 30:
			CONTROLLER.AIFirstInningsDeclareRuns = UnityEngine.Random.Range(250, 301);
			CONTROLLER.AIFirstInningsDeclareBalls = UnityEngine.Random.Range(200, 251);
			break;
		case 60:
			CONTROLLER.AIFirstInningsDeclareRuns = UnityEngine.Random.Range(430, 481);
			CONTROLLER.AIFirstInningsDeclareBalls = UnityEngine.Random.Range(400, 450);
			break;
		case 90:
			CONTROLLER.AIFirstInningsDeclareRuns = UnityEngine.Random.Range(500, 601);
			CONTROLLER.AIFirstInningsDeclareBalls = UnityEngine.Random.Range(600, 651);
			break;
		}
		CONTROLLER.AiFirstInngsLeadDeclareRuns = (int)((float)CONTROLLER.Overs[ObscuredPrefs.GetInt("TMOvers")] * 6f / 3f) + UnityEngine.Random.Range(10, 30);
		CONTROLLER.AiTargetDeclareRuns = (int)((float)CONTROLLER.Overs[ObscuredPrefs.GetInt("TMOvers")] * 6f / 2f) + UnityEngine.Random.Range(100, 150);
	}

	public int getBowlingTeamIndex()
	{
		int num = 0;
		return CONTROLLER.BowlingTeam[CONTROLLER.currentInnings];
	}

	private void ResetTM_PlayerScores()
	{
		for (int i = 0; i < CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList.Length; i++)
		{
			TestMatchBatsman.SetRunsScored(CONTROLLER.myTeamIndex, i, 0, increment: false);
			TestMatchBatsman.SetBallsPlayed(CONTROLLER.myTeamIndex, i, 0, increment: false);
			TestMatchBatsman.SetStatus(CONTROLLER.myTeamIndex, i, string.Empty);
			TestMatchBatsman.SetFours(CONTROLLER.myTeamIndex, i, 0, increment: false);
			TestMatchBatsman.SetSixes(CONTROLLER.myTeamIndex, i, 0, increment: false);
			TestMatchBatsman.SetFOW(CONTROLLER.myTeamIndex, i, 0);
			if (CONTROLLER.currentInnings < 2)
			{
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMMaiden1 = 0;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMWicket1 = 0;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMRunsGiven1 = 0;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMBallsBowled1 = 0;
			}
			else
			{
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMWicket2 = 0;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMRunsGiven2 = 0;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMBallsBowled2 = 0;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.TMMaiden2 = 0;
			}
		}
		for (int j = 0; j < CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList.Length; j++)
		{
			TestMatchBatsman.SetRunsScored(CONTROLLER.opponentTeamIndex, j, 0, increment: false);
			TestMatchBatsman.SetBallsPlayed(CONTROLLER.opponentTeamIndex, j, 0, increment: false);
			TestMatchBatsman.SetStatus(CONTROLLER.opponentTeamIndex, j, string.Empty);
			TestMatchBatsman.SetFours(CONTROLLER.opponentTeamIndex, j, 0, increment: false);
			TestMatchBatsman.SetSixes(CONTROLLER.opponentTeamIndex, j, 0, increment: false);
			TestMatchBatsman.SetFOW(CONTROLLER.opponentTeamIndex, j, 0);
			if (CONTROLLER.currentInnings < 2)
			{
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMWicket1 = 0;
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMRunsGiven1 = 0;
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMBallsBowled1 = 0;
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMMaiden1 = 0;
			}
			else
			{
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMWicket2 = 0;
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMRunsScored2 = 0;
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BatsmanList.TMBallsPlayed2 = 0;
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[j].BowlerList.TMMaiden2 = 0;
			}
		}
	}

	public int getCurrentBattingTeam(int _index)
	{
		return CONTROLLER.BattingTeam[CONTROLLER.currentInnings];
	}

	public int getCurrentBowlTeam(int _index)
	{
		return CONTROLLER.BowlingTeam[CONTROLLER.currentInnings];
	}

	private void SetTargetToWin()
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
		int value = num - num2;
		CONTROLLER.TargetToChase = Mathf.Abs(value);
		AutoSave.SaveInGameMatch();
	}

	public bool checkforSessionComplete()
	{
		int num = CONTROLLER.ballsBowledPerDay / 6 / (CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] / CONTROLLER.noOfSession);
		if (CONTROLLER.ballsBowledPerDay / 6 % (CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] / CONTROLLER.noOfSession) == 0 && CONTROLLER.ballsBowledPerDay / 6 != 0)
		{
			if (CONTROLLER.MaxDays == CONTROLLER.currentDay && CONTROLLER.currentSession == 3)
			{
				return true;
			}
			return true;
		}
		return false;
	}

	public void setSession()
	{
		int num = CONTROLLER.ballsBowledPerDay / 6 / (CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] / CONTROLLER.noOfSession);
		if (CONTROLLER.currentSession < num)
		{
			CONTROLLER.currentSession++;
			if (CONTROLLER.currentDay < CONTROLLER.MaxDays && CONTROLLER.currentSession % 3 == 0)
			{
				CONTROLLER.currentDay++;
				CONTROLLER.currentSession = 0;
				CONTROLLER.ballsBowledPerDay = 0;
			}
			sessionComplete = false;
			AutoSave.SaveInGameMatch();
		}
	}

	public void clearData()
	{
	}

	public bool CheckForMatchCompletion()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		bool flag = false;
		if (CONTROLLER.MaxDays == CONTROLLER.currentDay && CONTROLLER.currentSession >= 2)
		{
			int num6 = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6 - CONTROLLER.ballsBowledPerDay;
			for (int i = 0; i <= CONTROLLER.currentInnings; i++)
			{
				num4++;
			}
			if ((num4 >= 4 || num6 <= 0) && num4 < 4 && num6 == 0)
			{
				return true;
			}
		}
		if (CONTROLLER.currentInnings == 2 && CONTROLLER.isFollowOn)
		{
			num4 = 0;
			num5 = 0;
			num = 0;
			num2 = 0;
			int num7 = 0;
			for (int j = 0; j <= CONTROLLER.currentInnings; j++)
			{
				if (j < 2)
				{
					if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
					{
						if (CONTROLLER.BattingTeam[j] == CONTROLLER.myTeamIndex)
						{
							num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[j]].TMcurrentMatchScores1;
							num4++;
							num7++;
						}
						else
						{
							num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[j]].TMcurrentMatchScores1;
							num4++;
							num5++;
						}
					}
					else if (CONTROLLER.BattingTeam[j] == CONTROLLER.myTeamIndex)
					{
						num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[j]].TMcurrentMatchScores1;
						num4++;
						num5++;
					}
					else
					{
						num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[j]].TMcurrentMatchScores1;
						num4++;
						num7++;
					}
				}
				else if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					if (CONTROLLER.BattingTeam[j] == CONTROLLER.myTeamIndex)
					{
						num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[j]].TMcurrentMatchScores2;
						num4++;
						num7++;
					}
					else
					{
						num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[j]].TMcurrentMatchScores2;
						num4++;
						num5++;
					}
				}
				else if (CONTROLLER.BattingTeam[j] == CONTROLLER.myTeamIndex)
				{
					num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[j]].TMcurrentMatchScores2;
					num4++;
					num5++;
				}
				else
				{
					num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[j]].TMcurrentMatchScores2;
					num4++;
					num7++;
				}
			}
			if (num5 == 1)
			{
				flag = true;
			}
			if (num < num2 && CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets1 >= CONTROLLER.totalWickets && CONTROLLER.currentInnings < 2)
			{
				return true;
			}
			if (num < num2 && CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= CONTROLLER.totalWickets && CONTROLLER.currentInnings >= 2)
			{
				return true;
			}
			if (num4 == 3 && num < num2 && !flag)
			{
				return true;
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].isDeclared1 && CONTROLLER.currentInnings < 2)
			{
				if (num4 == 3 && num7 == 2 && num < num2)
				{
					return true;
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].isDeclared2 && CONTROLLER.currentInnings >= 2 && num4 == 3 && num7 == 2 && num < num2)
			{
				return true;
			}
		}
		if (CONTROLLER.currentInnings == 2 && !CONTROLLER.isFollowOn)
		{
			num4 = 0;
			num5 = 0;
			num = 0;
			num2 = 0;
			int num8 = 0;
			for (int k = 0; k <= CONTROLLER.currentInnings; k++)
			{
				if (k < 2)
				{
					if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
					{
						if (CONTROLLER.BattingTeam[k] == CONTROLLER.myTeamIndex)
						{
							num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchScores1;
							num4++;
							num8++;
						}
						else
						{
							num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchScores1;
							num4++;
							num5++;
						}
					}
					else if (CONTROLLER.BattingTeam[k] == CONTROLLER.myTeamIndex)
					{
						num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchScores1;
						num4++;
						num5++;
					}
					else
					{
						num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchScores1;
						num4++;
						num8++;
					}
				}
				else if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					if (CONTROLLER.BattingTeam[k] == CONTROLLER.myTeamIndex)
					{
						num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchScores2;
						num4++;
						num8++;
					}
					else
					{
						num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchScores2;
						num4++;
						num5++;
					}
				}
				else if (CONTROLLER.BattingTeam[k] == CONTROLLER.myTeamIndex)
				{
					num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchScores2;
					num4++;
					num5++;
				}
				else
				{
					num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[k]].TMcurrentMatchScores2;
					num4++;
					num8++;
				}
			}
			if (num5 == 1)
			{
				flag = true;
			}
			if (num < num2 && CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets1 >= CONTROLLER.totalWickets && CONTROLLER.currentInnings < 2)
			{
				return true;
			}
			if (num < num2 && CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= CONTROLLER.totalWickets && CONTROLLER.currentInnings >= 2)
			{
				return true;
			}
			if (num4 == 3 && num < num2 && !flag)
			{
				return true;
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].isDeclared1 && CONTROLLER.currentInnings < 2)
			{
				if (num4 == 3 && num8 == 2 && num < num2)
				{
					return true;
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].isDeclared2 && CONTROLLER.currentInnings >= 2 && num4 == 3 && num8 == 2 && num < num2)
			{
				return true;
			}
		}
		if (CONTROLLER.currentInnings == 3)
		{
			num3 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchScores2;
			num = 0;
			num2 = 0;
			for (int l = 0; l <= CONTROLLER.currentInnings; l++)
			{
				if (l < 2)
				{
					if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
					{
						if (CONTROLLER.BattingTeam[l] == CONTROLLER.myTeamIndex)
						{
							num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchScores1;
						}
						else
						{
							num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchScores1;
						}
					}
					else if (CONTROLLER.BattingTeam[l] == CONTROLLER.myTeamIndex)
					{
						num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchScores1;
					}
					else
					{
						num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchScores1;
					}
				}
				else if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					if (CONTROLLER.BattingTeam[l] == CONTROLLER.myTeamIndex)
					{
						num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchScores2;
					}
					else
					{
						num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchScores2;
					}
				}
				else if (CONTROLLER.BattingTeam[l] == CONTROLLER.myTeamIndex)
				{
					num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchScores2;
				}
				else
				{
					num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[l]].TMcurrentMatchScores2;
				}
			}
			if (num3 > CONTROLLER.TargetToChase)
			{
				return true;
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].TMcurrentMatchWickets1 >= CONTROLLER.totalWickets && CONTROLLER.currentInnings < 2)
			{
				return true;
			}
			if (CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchWickets2 >= CONTROLLER.totalWickets && CONTROLLER.currentInnings >= 2)
			{
				return true;
			}
		}
		return false;
	}

	public void setOversAfterSession()
	{
		setSession();
		CanPauseGame = false;
		maxRunsReached = false;
		currentBall = -1;
		runsScoredInOver = 0;
		Singleton<Scoreboard>.instance.NewOver();
		Singleton<Scoreboard>.instance.HidePause(boolean: true);
		if (CONTROLLER.currentInnings == 0)
		{
			SetFielders();
			groundControllerScript.NewOver();
		}
		CanPauseGame = true;
		isGamePaused = false;
		if (CONTROLLER.BattingTeam[CONTROLLER.currentInnings] == CONTROLLER.opponentTeamIndex)
		{
			if (!inningsCompleted && !BowlingScoreCard.newOverCalled)
			{
				Singleton<BowlingScoreCard>.instance.OverCompleted();
			}
			ShowBattingScoreCard();
		}
		else
		{
			if (!inningsCompleted)
			{
				Singleton<BowlingScoreCard>.instance.OverCompleted();
			}
			ShowBattingScoreCard();
		}
	}

	public bool CheckForAIDeclaration()
	{
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex || CONTROLLER.currentInnings == 3)
		{
			return false;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		if (CONTROLLER.currentDay >= 2 && CONTROLLER.BattingTeam[0] == CONTROLLER.opponentTeamIndex && CONTROLLER.currentInnings < 2)
		{
			num = CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchScores1;
			num2 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls1;
			if (num > CONTROLLER.AIFirstInningsDeclareRuns && num2 > CONTROLLER.AIFirstInningsDeclareBalls)
			{
				return true;
			}
		}
		else if (CONTROLLER.currentDay > 1 && CONTROLLER.BattingTeam[1] == CONTROLLER.opponentTeamIndex && CONTROLLER.currentInnings < 2)
		{
			num = CONTROLLER.TeamList[CONTROLLER.BattingTeam[1]].TMcurrentMatchScores1;
			num3 = CONTROLLER.TeamList[CONTROLLER.BattingTeam[0]].TMcurrentMatchScores1;
			if (num > num3 + CONTROLLER.AiFirstInngsLeadDeclareRuns && CONTROLLER.currentDay < 5)
			{
				return true;
			}
		}
		else if (CONTROLLER.BattingTeam[CONTROLLER.currentInnings] == CONTROLLER.opponentTeamIndex && CONTROLLER.currentDay > 3 && CONTROLLER.currentInnings > 1 && CONTROLLER.currentSession > 1)
		{
			int num4 = 0;
			int tMcurrentMatchBalls = CONTROLLER.TeamList[CONTROLLER.BattingTeam[CONTROLLER.currentInnings]].TMcurrentMatchBalls2;
			num3 = 0;
			for (int i = 0; i <= CONTROLLER.currentInnings; i++)
			{
				if (i < 2)
				{
					if (CONTROLLER.BattingTeam[i] == CONTROLLER.opponentTeamIndex)
					{
						num4 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores1;
					}
					else
					{
						num3 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores1;
					}
				}
				else if (CONTROLLER.BattingTeamIndex == CONTROLLER.opponentTeamIndex)
				{
					num4 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores2;
				}
				else
				{
					num3 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores2;
				}
			}
			if (num4 > num3 + CONTROLLER.AiTargetDeclareRuns)
			{
				return true;
			}
			if (CONTROLLER.currentDay == 5)
			{
				int num5 = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] * 6 - tMcurrentMatchBalls;
				num4 = 0;
				for (int j = 0; j <= CONTROLLER.currentInnings; j++)
				{
					if (CONTROLLER.BattingTeam[j] == CONTROLLER.opponentTeamIndex)
					{
						num4 = ((j >= 2) ? (num4 + CONTROLLER.TeamList[CONTROLLER.BattingTeam[j]].TMcurrentMatchScores2) : (num4 + CONTROLLER.TeamList[CONTROLLER.BattingTeam[j]].TMcurrentMatchScores1));
					}
				}
				if (num4 > num3 + CONTROLLER.AiTargetDeclareRuns && CONTROLLER.currentSession > 0 && num4 - num3 > num5 * 7)
				{
					return true;
				}
			}
		}
		return false;
	}

	public void checkForDeclaration()
	{
		inningsCompleted = CheckForInningsComplete();
		if (!inningsCompleted)
		{
			return;
		}
		CanPauseGame = false;
		isGamePaused = false;
		groundControllerScript.fieldRestriction = true;
		groundControllerScript.resetNoBallVairables();
		CONTROLLER.fielderChangeIndex = 1;
		CONTROLLER.computerFielderChangeIndex = 1;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i <= CONTROLLER.currentInnings; i++)
		{
			if (i < 2)
			{
				if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
				{
					num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores1;
				}
				else
				{
					num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores1;
				}
			}
			else if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				num += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores2;
			}
			else
			{
				num2 += CONTROLLER.TeamList[CONTROLLER.BattingTeam[i]].TMcurrentMatchScores2;
			}
		}
		AutoSave.SaveInGameMatch();
		if (!CheckForMatchCompletion())
		{
			if (CONTROLLER.BattingTeamIndex == CONTROLLER.opponentTeamIndex)
			{
			}
			ShowBattingScoreCard();
		}
		else
		{
			isGamePaused = false;
			CONTROLLER.ShowTMGameOver = true;
			ShowBattingScoreCard();
		}
	}

	public void showTargetScreen()
	{
		int value = CONTROLLER.totalOvers - CONTROLLER.ballsBowledPerDay / 6;
		float num = (float)CONTROLLER.ballsBowledPerDay / 6f - (float)(CONTROLLER.totalOvers / CONTROLLER.noOfSession) * ((float)CONTROLLER.currentSession + 1f);
		if (CONTROLLER.currentDay == CONTROLLER.MaxDays && CONTROLLER.currentSession == 2 && Mathf.Abs(value) <= 1)
		{
			isGamePaused = false;
			CONTROLLER.ShowTMGameOver = true;
			ShowBattingScoreCard();
			return;
		}
		if (num < 0f && num > -1f)
		{
			CONTROLLER.ballsBowledPerDay = CONTROLLER.totalOvers / CONTROLLER.noOfSession * 6 * (CONTROLLER.currentSession + 1);
			CONTROLLER.isSkipBallsForSession = true;
			AutoSave.SaveInGameMatch();
			Singleton<TMSessionScreen>.instance.ShowMe();
			action = 10;
			return;
		}
		int num2 = CONTROLLER.ballsBowledPerDay % 6;
		if (num2 == 0)
		{
			CONTROLLER.ballsBowledPerDay += num2;
		}
		else
		{
			CONTROLLER.ballsBowledPerDay += 6 - num2;
		}
		Singleton<TMSessionScreen>.instance.ShowMe();
		action = 10;
	}

	private void SetTarget()
	{
		if (CONTROLLER.currentInnings == 0)
		{
			switch (CONTROLLER.Overs[ObscuredPrefs.GetInt("TMOvers")])
			{
			case 15:
				CONTROLLER.followOnTarget = 125;
				break;
			case 30:
				CONTROLLER.followOnTarget = 150;
				break;
			case 60:
				CONTROLLER.followOnTarget = 175;
				break;
			case 90:
				CONTROLLER.followOnTarget = 200;
				break;
			case 3:
				CONTROLLER.followOnTarget = 10;
				break;
			default:
				CONTROLLER.followOnTarget = 125;
				break;
			}
		}
	}
}
