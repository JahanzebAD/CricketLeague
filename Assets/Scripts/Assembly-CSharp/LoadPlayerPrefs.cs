using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlayerPrefs : Singleton<LoadPlayerPrefs>
{
	public GameObject Marshmallow_Perm;

	public GameObject GDPRPopup;

	public GameObject InstallationPopup;

	public GameObject LocalizationPopup;

	public GameObject[] LanguageButtons;

	public TextAsset xmlAsset;

	private int teamARuns;

	private float teamABallFaced;

	private int teamARank;

	private int teamBRuns;

	private float teamBBallFaced;

	private int teamBRank;

	private int chaseTarget;

	private int PointsWin = 4;

	private int PointsDraw = 2;

	[SerializeField] string PrivacyPolicy;

	protected void Awake()
	{
		if (Application.loadedLevelName == "Preloader")
		{
			if (!PlayerPrefs.HasKey("InstallationPopup"))
			{
				InstallationPopup.SetActive(value: true);
				PlayerPrefs.SetInt("InstallationPopup", 1);
			}
			if (PlayerPrefs.HasKey("InstallationPopup") && !PlayerPrefs.HasKey("GDPRClicked"))
			{
				GDPRPopup.SetActive(value: true);
			}
			CheckForRefund();
			AchievementTable.InitAchievementValues();
			KitTable.InitKitValues();
			CONTROLLER.CanRetrieveAchievements = true;
			CONTROLLER.fromPreloader = true;
			float num = 1.33333337f;
			float num2 = Screen.width;
			float num3 = Screen.height;
			float num4 = num2 / num3;
			CONTROLLER.xOffSet = (num - num4) * 600f / 2f;
			CONTROLLER.gameCompleted = false;
			if (!PlayerPrefs.HasKey("Localization") && PlayerPrefs.HasKey("GDPRClicked"))
			{
				Singleton<LoadPlayerPrefs>.instance.LocalizationPopup.SetActive(value: true);
				Singleton<LoadPlayerPrefs>.instance.OnLanguagesClicked(1);
			}
		}
		else if (Application.loadedLevelName == "MainMenu")
		{
			CheckForRefund();
		}
		else if (Application.loadedLevelName == "Ground")
		{
			PostGoogleAnalyticsEvent();
		}
		CONTROLLER.SceneIsLoading = false;
	}

	private void AdNetworksInit()
	{
	}

	public void CheckForRefund()
	{
		for (int i = 0; i < 8; i++)
		{
			if (ObscuredPrefs.HasKey(i + "Refund"))
			{
				SavePlayerPrefs.SaveUserTickets(ObscuredPrefs.GetInt(i + "Refund"), 0, ObscuredPrefs.GetInt(i + "Refund"));
				ObscuredPrefs.DeleteKey(i + "Refund");
			}
		}
	}

	public void CheckIfNewUser()
	{
		if (!ObscuredPrefs.HasKey("newUser"))
		{
			ObscuredPrefs.SetInt("newUser", 0);
			CONTROLLER.newUser = true;
		}
	}

	public void GetUserTicketDetails()
	{
		if (ObscuredPrefs.HasKey(CONTROLLER.TicketsKey))
		{
			CONTROLLER.tickets = ObscuredPrefs.GetInt(CONTROLLER.TicketsKey);
		}
		else
		{
			ObscuredPrefs.SetInt(CONTROLLER.TicketsKey, 0);
		}
		if (PlayerPrefs.HasKey("EncrytionAddedTickets"))
		{
			CONTROLLER.tickets = EncryptionService.instance.Tickets;
			CONTROLLER.spendTickets = EncryptionService.instance.SpendTickets;
			CONTROLLER.earnedTickets = EncryptionService.instance.EarnedTickets;
		}
		PlayerPrefs.SetInt("EncrytionAddedTickets", 1);
	}

	public void GetUserSpinDetails()
	{
		if (ObscuredPrefs.HasKey("userSpins"))
		{
			CONTROLLER.FreeSpin = ObscuredPrefs.GetInt("userSpins");
		}
		else
		{
			ObscuredPrefs.SetInt("userSpins", 0);
		}
		CONTROLLER.FreeSpin = EncryptionService.instance.FreeSpin;
	}

	public void GetSixesCount()
	{
		if (ObscuredPrefs.HasKey("ArcadeSixes"))
		{
			CONTROLLER.sixMeterCount = ObscuredPrefs.GetInt("ArcadeSixes");
		}
		else
		{
			ObscuredPrefs.SetInt("ArcadeSixes", 0);
		}
		CONTROLLER.sixMeterCount = EncryptionService.instance.Sixcount;
	}

	public void GetUserXPDetails()
	{
		if (ObscuredPrefs.HasKey(CONTROLLER.XPKey))
		{
			CONTROLLER.XPs = ObscuredPrefs.GetInt(CONTROLLER.XPKey);
		}
		else
		{
			ObscuredPrefs.SetInt(CONTROLLER.XPKey, 0);
		}
		if (ObscuredPrefs.HasKey("userEarnedXP"))
		{
			CONTROLLER.earnedXPs = ObscuredPrefs.GetInt("userEarnedXP");
		}
		else
		{
			ObscuredPrefs.SetInt(CONTROLLER.XPKey, 0);
		}
		if (PlayerPrefs.HasKey("EncrytionAddedXP"))
		{
			CONTROLLER.XPs = EncryptionService.instance.XP;
			CONTROLLER.earnedXPs = EncryptionService.instance.EarnedXp;
		}
		PlayerPrefs.SetInt("EncrytionAddedXP", 1);
	}

	public void GetUserTokensDetails()
	{
	}

	public void GetUserEarnedTokensDetails()
	{
	}

	public void GetUserSpendTokensDetails()
	{
	}

	public void GetUserCoinDetails()
	{
		if (ObscuredPrefs.HasKey(CONTROLLER.CoinsKey))
		{
			CONTROLLER.Coins = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey);
		}
		else
		{
			ObscuredPrefs.SetInt(CONTROLLER.CoinsKey, 0);
		}
		if (PlayerPrefs.HasKey("EncrytionAddedCoins"))
		{
			CONTROLLER.Coins = EncryptionService.instance.Coins;
			CONTROLLER.earnedCoins = EncryptionService.instance.EarnedCoins;
			CONTROLLER.spendCoins = EncryptionService.instance.SpendCoins;
		}
		PlayerPrefs.SetInt("EncrytionAddedCoins", 1);
	}

	public void GetUserEarnedCoinDetails()
	{
		if (ObscuredPrefs.HasKey("userEarnedCoins"))
		{
			CONTROLLER.earnedCoins = ObscuredPrefs.GetInt("userEarnedCoins");
		}
		else
		{
			ObscuredPrefs.SetInt("userEarnedCoins", 0);
		}
		CONTROLLER.earnedCoins = EncryptionService.instance.EarnedCoins;
	}

	public void GetPowerUpDetails()
	{
		if (ObscuredPrefs.HasKey("powerGrade"))
		{
			CONTROLLER.powerGrade = ObscuredPrefs.GetInt("powerGrade");
		}
		else
		{
			ObscuredPrefs.SetInt("powerGrade", 0);
		}
		if (ObscuredPrefs.HasKey("controlGrade"))
		{
			CONTROLLER.controlGrade = ObscuredPrefs.GetInt("controlGrade");
		}
		else
		{
			ObscuredPrefs.SetInt("controlGrade", 0);
		}
		if (ObscuredPrefs.HasKey("agilityGrade"))
		{
			CONTROLLER.agilityGrade = ObscuredPrefs.GetInt("agilityGrade");
		}
		else
		{
			ObscuredPrefs.SetInt("agilityGrade", 0);
		}
		if (ObscuredPrefs.HasKey("powerESG"))
		{
			CONTROLLER.earnedPowerSubGrade = ObscuredPrefs.GetInt("powerESG");
		}
		else
		{
			ObscuredPrefs.SetInt("powerESG", 0);
		}
		if (ObscuredPrefs.HasKey("controlESG"))
		{
			CONTROLLER.earnedControlSubGrade = ObscuredPrefs.GetInt("controlESG");
		}
		else
		{
			ObscuredPrefs.SetInt("controlESG", 0);
		}
		if (ObscuredPrefs.HasKey("agilityESG"))
		{
			CONTROLLER.earnedAgilitySubGrade = ObscuredPrefs.GetInt("agilityESG");
		}
		else
		{
			ObscuredPrefs.SetInt("agilityESG", 0);
		}
		if (ObscuredPrefs.HasKey("powerBSG"))
		{
			CONTROLLER.boughtPowerSubGrade = ObscuredPrefs.GetInt("powerBSG");
		}
		else
		{
			ObscuredPrefs.SetInt("powerBSG", 0);
		}
		if (ObscuredPrefs.HasKey("controlBSG"))
		{
			CONTROLLER.boughtControlSubGrade = ObscuredPrefs.GetInt("controlBSG");
		}
		else
		{
			ObscuredPrefs.SetInt("controlBSG", 0);
		}
		if (ObscuredPrefs.HasKey("agilityBSG"))
		{
			CONTROLLER.boughtAgilitySubGrade = ObscuredPrefs.GetInt("agilityBSG");
		}
		else
		{
			ObscuredPrefs.SetInt("agilityBSG", 0);
		}
		CONTROLLER.totalPowerSubGrade = CONTROLLER.boughtPowerSubGrade + CONTROLLER.earnedPowerSubGrade;
		CONTROLLER.totalControlSubGrade = CONTROLLER.boughtControlSubGrade + CONTROLLER.earnedControlSubGrade;
		CONTROLLER.totalAgilitySubGrade = CONTROLLER.boughtAgilitySubGrade + CONTROLLER.earnedAgilitySubGrade;
		CONTROLLER.powerGrade = EncryptionService.instance.Powergrade;
		CONTROLLER.controlGrade = EncryptionService.instance.Controlgrade;
		CONTROLLER.agilityGrade = EncryptionService.instance.Agilitygrade;
		CONTROLLER.totalPowerSubGrade = EncryptionService.instance.TotalPowerSubGrade;
		CONTROLLER.totalControlSubGrade = EncryptionService.instance.TotalControlSubGrade;
		CONTROLLER.totalAgilitySubGrade = EncryptionService.instance.TotalAgilitySubGrade;
	}

	private void SetSuperOverLevels()
	{
		for (int i = 0; i < CONTROLLER.totalLevels; i++)
		{
			CONTROLLER.LevelCompletedArray[i] = 0;
		}
		for (int i = 0; i < CONTROLLER.CurrentLevelCompleted; i++)
		{
			CONTROLLER.LevelCompletedArray[i] = 1;
		}
	}

	public void GetSuperOverLevelDetails()
	{
		if (ObscuredPrefs.HasKey("SuperOverLevelDetail"))
		{
			string @string = ObscuredPrefs.GetString("SuperOverLevelDetail");
			string[] array = @string.Split("|"[0]);
			CONTROLLER.CurrentLevelCompleted = int.Parse(array[0]);
			CONTROLLER.LevelFailed = int.Parse(array[1]);
			CONTROLLER.LevelId = int.Parse(array[2]);
		}
		else
		{
			CONTROLLER.CurrentLevelCompleted = 0;
			CONTROLLER.LevelFailed = 0;
			CONTROLLER.LevelId = 0;
		}
		SetSuperOverLevels();
	}

	public void SetChaseTargetLevels()
	{
		for (int i = 0; i < CONTROLLER.TargetRangeArray.Length; i++)
		{
			CONTROLLER.MainLevelCompletedArray[i] = 0;
		}
		if (ObscuredPrefs.HasKey("CTMainArray"))
		{
			int @int = ObscuredPrefs.GetInt("CTMainArray");
			for (int i = 0; i <= @int; i++)
			{
				CONTROLLER.MainLevelCompletedArray[i] = 1;
			}
		}
		for (int i = 0; i < 5; i++)
		{
			CONTROLLER.SubLevelCompletedArray[i] = 0;
			if (i < CONTROLLER.CTSubLevelCompleted)
			{
				CONTROLLER.SubLevelCompletedArray[i] = 1;
			}
		}
	}

	public void GetChaseTargetLevelDetails()
	{
		CONTROLLER.TargetRangeArray[0] = "14-16$18-20$22-24$24-26$28-30";
		CONTROLLER.TargetRangeArray[1] = "40-50$60-70$80-90$100-120$120-150";
		CONTROLLER.TargetRangeArray[2] = "70-90$100-120$125-140$150-170$180-200";
		CONTROLLER.TargetRangeArray[3] = "120-140$145-160$165-190$195-220$225-260";
		CONTROLLER.TargetRangeArray[4] = "180-200$210-230$235-260$265-280$285-320";
		CONTROLLER.TargetRangeArray[5] = "300-330$340-370$380-410$420-450$460-500";
		if (ObscuredPrefs.HasKey("ChaseTargetLevelDetail"))
		{
			string @string = ObscuredPrefs.GetString("ChaseTargetLevelDetail");
			string[] array = @string.Split("|"[0]);
			CONTROLLER.CTLevelCompleted = int.Parse(array[0]);
			CONTROLLER.CTSubLevelCompleted = int.Parse(array[1]);
		}
		else
		{
			CONTROLLER.CTLevelCompleted = 0;
			CONTROLLER.CTSubLevelCompleted = 0;
		}
		if (ObscuredPrefs.HasKey("ctWonMatch"))
		{
			CONTROLLER.CTWonMatch = ObscuredPrefs.GetInt("ctWonMatch");
		}
		else
		{
			CONTROLLER.CTWonMatch = 1;
		}
		SetChaseTargetLevels();
	}

	public void SetArrayForChaseTarget()
	{
		if (!ObscuredPrefs.HasKey("ChaseTargetLevelDetail"))
		{
			return;
		}
		string @string = ObscuredPrefs.GetString("ChaseTargetLevelDetail");
		string[] array = @string.Split("|"[0]);
		int num = int.Parse(array[0]);
		int num2 = int.Parse(array[1]);
		if (CONTROLLER.CTCurrentPlayingMainLevel <= num && CONTROLLER.MainLevelCompletedArray[CONTROLLER.CTCurrentPlayingMainLevel] == 1)
		{
			for (int i = 0; i < 5; i++)
			{
				CONTROLLER.SubLevelCompletedArray[i] = 1;
			}
			return;
		}
		for (int i = 0; i < 5; i++)
		{
			CONTROLLER.SubLevelCompletedArray[i] = 0;
			if (i < num2)
			{
				CONTROLLER.SubLevelCompletedArray[i] = 1;
			}
		}
	}

	public void GetNPLIndiaTournamentList()
	{
		CONTROLLER.NplIndiaData = string.Empty;
		CONTROLLER.NPLIndiaTournamentStage = 0;
		CONTROLLER.NPLIndiaMyCurrentMatchIndex = 0;
		CONTROLLER.NPLIndiaTeamWonIndexStr = string.Empty;
		CONTROLLER.StoredNPLIndiaSeriesResult = string.Empty;
		CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
		if (CONTROLLER.tournamentType == "NPL")
		{
			if (ObscuredPrefs.HasKey("NPLIndiaLeague"))
			{
				CONTROLLER.NplIndiaData = ObscuredPrefs.GetString("NPLIndiaLeague");
				string[] array = CONTROLLER.NplIndiaData.Split("|"[0]);
				CONTROLLER.NPLIndiaTournamentStage = int.Parse(array[0]);
				CONTROLLER.myTeamIndex = int.Parse(array[1]);
				CONTROLLER.NPLIndiaMyCurrentMatchIndex = int.Parse(array[2]);
				CONTROLLER.NPLIndiaTeamWonIndexStr = array[3];
				Debug.LogError("NPL stored string");
				CONTROLLER.StoredNPLIndiaSeriesResult = array[4];
			}
			else
			{
				string empty = string.Empty;
				empty = AutoSave.ReadFile();
				if (empty == string.Empty)
				{
					CONTROLLER.NplIndiaData = string.Empty;
					CONTROLLER.NPLIndiaTournamentStage = 0;
					CONTROLLER.NPLIndiaMyCurrentMatchIndex = 0;
					CONTROLLER.NPLIndiaTeamWonIndexStr = string.Empty;
					CONTROLLER.StoredNPLIndiaSeriesResult = string.Empty;
					CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
				}
			}
			if (ObscuredPrefs.HasKey("NPLOvers"))
			{
				CONTROLLER.oversSelectedIndex = ObscuredPrefs.GetInt("NPLOvers");
			}
			if (ObscuredPrefs.HasKey("NPLIndiaLeagueMatchIndex"))
			{
				CONTROLLER.NPLIndiaLeagueMatchIndex = ObscuredPrefs.GetInt("NPLIndiaLeagueMatchIndex");
			}
			if (ObscuredPrefs.HasKey("NPLIndiaPlayOff") && !ObscuredPrefs.HasKey("NPLIndiaLeague"))
			{
				CONTROLLER.NplIndiaData = ObscuredPrefs.GetString("NPLIndiaPlayOff");
				string[] array2 = CONTROLLER.NplIndiaData.Split("&"[0]);
				CONTROLLER.NPLIndiaTournamentStage = int.Parse(array2[0]);
				CONTROLLER.myTeamIndex = int.Parse(array2[1]);
				CONTROLLER.NPLIndiaLeagueMatchIndex = int.Parse(array2[2]);
			}
		}
		else if (CONTROLLER.tournamentType == "PAK")
		{
			if (ObscuredPrefs.HasKey("NPLPakistanLeague"))
			{
				CONTROLLER.NplIndiaData = ObscuredPrefs.GetString("NPLPakistanLeague");
				string[] array3 = CONTROLLER.NplIndiaData.Split("|"[0]);
				CONTROLLER.NPLIndiaTournamentStage = int.Parse(array3[0]);
				CONTROLLER.myTeamIndex = int.Parse(array3[1]);
				CONTROLLER.NPLIndiaMyCurrentMatchIndex = int.Parse(array3[2]);
				CONTROLLER.NPLIndiaTeamWonIndexStr = array3[3];
				Debug.LogError("AUS stored string");
				CONTROLLER.StoredNPLIndiaSeriesResult = array3[4];
			}
			else
			{
				string empty2 = string.Empty;
				empty2 = AutoSave.ReadFile();
				if (empty2 == string.Empty)
				{
					CONTROLLER.NplIndiaData = string.Empty;
					CONTROLLER.NPLIndiaTournamentStage = 0;
					CONTROLLER.NPLIndiaMyCurrentMatchIndex = 0;
					CONTROLLER.NPLIndiaTeamWonIndexStr = string.Empty;
					CONTROLLER.StoredNPLIndiaSeriesResult = string.Empty;
					CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
				}
			}
			if (ObscuredPrefs.HasKey("PakOvers"))
			{
				CONTROLLER.oversSelectedIndex = ObscuredPrefs.GetInt("PakOvers");
			}
			if (ObscuredPrefs.HasKey("NPLPakistanLeagueMatchIndex"))
			{
				CONTROLLER.NPLIndiaLeagueMatchIndex = ObscuredPrefs.GetInt("NPLPakistanLeagueMatchIndex");
			}
			if (ObscuredPrefs.HasKey("NPLPakistanPlayOff") && !ObscuredPrefs.HasKey("NPLPakistanLeague"))
			{
				CONTROLLER.NplIndiaData = ObscuredPrefs.GetString("NPLPakistanPlayOff");
				string[] array4 = CONTROLLER.NplIndiaData.Split("&"[0]);
				CONTROLLER.NPLIndiaTournamentStage = int.Parse(array4[0]);
				CONTROLLER.myTeamIndex = int.Parse(array4[1]);
				CONTROLLER.NPLIndiaLeagueMatchIndex = int.Parse(array4[2]);
				CONTROLLER.tournamentStr = CONTROLLER.TournamentStage + "&" + CONTROLLER.myTeamIndex + "&" + CONTROLLER.oversSelectedIndex + "&" + CONTROLLER.matchIndex + "&" + array4[3];
			}
		}
		else
		{
			if (!(CONTROLLER.tournamentType == "AUS"))
			{
				return;
			}
			if (ObscuredPrefs.HasKey("NplAUS"))
			{
				CONTROLLER.NplIndiaData = ObscuredPrefs.GetString("NplAUS");
				string[] array5 = CONTROLLER.NplIndiaData.Split("|"[0]);
				CONTROLLER.NPLIndiaTournamentStage = int.Parse(array5[0]);
				CONTROLLER.myTeamIndex = int.Parse(array5[1]);
				CONTROLLER.NPLIndiaMyCurrentMatchIndex = int.Parse(array5[2]);
				CONTROLLER.NPLIndiaTeamWonIndexStr = array5[3];
				Debug.LogError("AUS stored string");
				CONTROLLER.StoredNPLIndiaSeriesResult = array5[4];
			}
			else
			{
				string empty3 = string.Empty;
				empty3 = AutoSave.ReadFile();
				if (empty3 == string.Empty)
				{
					CONTROLLER.NplIndiaData = string.Empty;
					CONTROLLER.NPLIndiaTournamentStage = 0;
					CONTROLLER.NPLIndiaMyCurrentMatchIndex = 0;
					CONTROLLER.NPLIndiaTeamWonIndexStr = string.Empty;
					CONTROLLER.StoredNPLIndiaSeriesResult = string.Empty;
					CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
				}
			}
			if (ObscuredPrefs.HasKey("AusOvers"))
			{
				CONTROLLER.oversSelectedIndex = ObscuredPrefs.GetInt("AusOvers");
			}
			if (ObscuredPrefs.HasKey("NPLAustraliaLeagueMatchIndex"))
			{
				CONTROLLER.NPLIndiaLeagueMatchIndex = ObscuredPrefs.GetInt("NPLAustraliaLeagueMatchIndex");
			}
			if (ObscuredPrefs.HasKey("NplAustraliaPlayoff") && !ObscuredPrefs.HasKey("NplAUS"))
			{
				CONTROLLER.NplIndiaData = ObscuredPrefs.GetString("NplAustraliaPlayoff");
				string[] array6 = CONTROLLER.NplIndiaData.Split("&"[0]);
				CONTROLLER.NPLIndiaTournamentStage = int.Parse(array6[0]);
				CONTROLLER.myTeamIndex = int.Parse(array6[1]);
				CONTROLLER.NPLIndiaLeagueMatchIndex = int.Parse(array6[2]);
				CONTROLLER.tournamentStr = CONTROLLER.TournamentStage + "&" + CONTROLLER.myTeamIndex + "&" + CONTROLLER.oversSelectedIndex + "&" + CONTROLLER.matchIndex + "&" + array6[3];
			}
		}
	}

	public bool getNPLMatches(string[] matchDetail, int index)
	{
		string text = matchDetail[index];
		string[] array = text.Split("-"[0]);
		if (int.Parse(array[0]) != CONTROLLER.myTeamIndex && int.Parse(array[1]) != CONTROLLER.myTeamIndex)
		{
			autoGenetrate(int.Parse(array[0]), int.Parse(array[1]), CONTROLLER.NPLIndiaPointsTable);
			return true;
		}
		return false;
	}

	public bool getWCMatch()
	{
		string text = CONTROLLER.WCMatchDetails[CONTROLLER.WCLeagueMatchIndex];
		string[] array = text.Split("!"[0]);
		string[] array2 = array[1].Split("-"[0]);
		if (int.Parse(array2[0]) != CONTROLLER.myTeamIndex && int.Parse(array2[1]) != CONTROLLER.myTeamIndex)
		{
			autoGenetrate(int.Parse(array2[0]), int.Parse(array2[1]), CONTROLLER.WCPointsTable);
			return true;
		}
		return false;
	}

	public void GetWCTournamentList()
	{
		if (ObscuredPrefs.HasKey("worldcup"))
		{
			CONTROLLER.WCLeagueData = ObscuredPrefs.GetString("worldcup");
			string[] array = CONTROLLER.WCLeagueData.Split("|"[0]);
			CONTROLLER.WCTournamentStage = int.Parse(array[0]);
			CONTROLLER.myTeamIndex = int.Parse(array[1]);
			CONTROLLER.WCMyCurrentMatchIndex = int.Parse(array[2]);
			CONTROLLER.WCTeamWonIndexStr = array[3];
			CONTROLLER.StoredWCTournamentResult = array[4];
		}
		if (ObscuredPrefs.HasKey("WCLeagueMatchIndex"))
		{
			CONTROLLER.WCLeagueMatchIndex = ObscuredPrefs.GetInt("WCLeagueMatchIndex");
		}
		if (ObscuredPrefs.HasKey("WCOvers"))
		{
			CONTROLLER.oversSelectedIndex = ObscuredPrefs.GetInt("WCOvers");
		}
		if (ObscuredPrefs.HasKey("wcplayoff") && !ObscuredPrefs.HasKey("worldcup"))
		{
			CONTROLLER.WCLeagueData = ObscuredPrefs.GetString("wcplayoff");
			string[] array2 = CONTROLLER.WCLeagueData.Split("&"[0]);
			CONTROLLER.WCTournamentStage = int.Parse(array2[0]);
			CONTROLLER.myTeamIndex = int.Parse(array2[1]);
			CONTROLLER.WCMyCurrentMatchIndex = int.Parse(array2[2]);
		}
	}

	private void autoGenetrate(int Team1, int Team2, string[] pointsTable)
	{
		teamARuns = 0;
		teamABallFaced = 0f;
		teamARank = 0;
		teamBRuns = 0;
		teamBBallFaced = 0f;
		teamBRank = 0;
		int overs = 20;
		CONTROLLER.currentInnings = 0;
		AutoplayOvers(Team1, Team2, overs, 0, pointsTable);
		CONTROLLER.currentInnings = 1;
		AutoplayOvers(Team2, Team1, overs, 0, pointsTable);
	}

	private void AutoplayOvers(int battingteamRank, int bowlingteamRank, int Overs, int balls, string[] pointsTable)
	{
		int teamId = CONTROLLER.TeamList[battingteamRank].teamId;
		int teamId2 = CONTROLLER.TeamList[bowlingteamRank].teamId;
		int num = teamId - teamId2;
		float num2 = 0f;
		int num3 = 0;
		float num4 = 0f;
		int num5 = 0;
		if (CONTROLLER.currentInnings == 1)
		{
			num4 = (float)chaseTarget / (float)Overs;
		}
		else
		{
			if (num < -9)
			{
				num2 = float.Parse(string.Empty + ((float)Random.Range(6, 8) + Random.Range(0.8f, 1f)));
			}
			else if (num >= -9 && num < -5)
			{
				num2 = float.Parse(string.Empty + ((float)Random.Range(5, 7) + Random.Range(0.5f, 1f)));
			}
			else if (num >= -5 && num < -3)
			{
				num2 = float.Parse(string.Empty + ((float)Random.Range(5, 7) + Random.Range(0.3f, 0.8f)));
			}
			else if (num >= -3 && num < -1)
			{
				num2 = float.Parse(string.Empty + ((float)Random.Range(5, 7) + Random.Range(0.3f, 0.8f)));
			}
			else if (num >= -1 && num < 1)
			{
				num2 = float.Parse(string.Empty + ((float)Random.Range(5, 7) + Random.Range(0.3f, 0.8f)));
			}
			else if (num >= 1 && num < 3)
			{
				num2 = float.Parse(string.Empty + ((float)Random.Range(5, 7) - Random.Range(0.3f, 0.8f)));
			}
			else if (num >= 3 && num < 5)
			{
				num2 = float.Parse(string.Empty + ((float)Random.Range(5, 8) - Random.Range(0.3f, 0.8f)));
			}
			else if (num >= 5 && num < 9)
			{
				num2 = float.Parse(string.Empty + ((float)Random.Range(4, 7) - Random.Range(0.5f, 1f)));
			}
			else if (num >= 9)
			{
				num2 = float.Parse(string.Empty + ((float)Random.Range(4, 6) - Random.Range(0.8f, 1f)));
			}
			if (battingteamRank >= 10)
			{
				num2 = ((num < 4) ? float.Parse(string.Empty + ((float)Random.Range(3, 5) + Random.Range(0.3f, 0.6f))) : float.Parse(string.Empty + ((float)Random.Range(3, 4) + Random.Range(0.1f, 0.3f))));
			}
			num3 = int.Parse(string.Empty + Mathf.Round(num2 * (float)Overs));
		}
		if (CONTROLLER.currentInnings == 0)
		{
			chaseTarget = num3;
			teamARuns = num3;
			teamARank = battingteamRank;
			if (battingteamRank >= 10)
			{
				num5 = ((Random.Range(0, 100) % battingteamRank != 0) ? Random.Range(7, 10) : 10);
			}
			if (num5 >= 10)
			{
				teamABallFaced = (float)Overs * 6f;
			}
			else
			{
				teamABallFaced = (float)Overs * 6f;
			}
		}
		else
		{
			if (CONTROLLER.currentInnings != 1)
			{
				return;
			}
			num3 = (teamBRuns = int.Parse(string.Concat(arg1: Mathf.Round(((num < 5) ? ((teamId2 < 3) ? (num4 + Random.Range(-0.3f, 0.8f)) : ((teamId2 >= 7) ? (num4 + Random.Range(-1f, 0.3f)) : (num4 + Random.Range(-0.5f, 1.2f)))) : ((teamId < 3) ? (num4 + Random.Range(-0.2f, 0.5f)) : ((teamId >= 7) ? (num4 + Random.Range(-0.2f, 0.3f)) : (num4 + Random.Range(-0.3f, 0.25f))))) * (float)Overs), arg0: string.Empty)));
			teamBRank = battingteamRank;
			if (teamBRuns < teamARuns)
			{
				int num6 = Random.Range(0, 50);
				if (num6 % 12 == 0)
				{
					num5 = 10;
				}
				else
				{
					num5 = Random.Range(6, 10);
				}
				teamBBallFaced = (float)Overs * 6f;
			}
			else
			{
				teamBBallFaced = Random.Range(96, 121);
				num5 = Random.Range(2, 10);
			}
			teamABallFaced /= 6f;
			teamBBallFaced /= 6f;
			savePlayerDetails(teamARank, teamARuns, teamABallFaced, teamBRank, teamBRuns, teamBBallFaced, pointsTable);
		}
	}

	public int[] SortWCPointsTable(string[] pointsTable, int[] sortedPointsTable)
	{
		sortedPointsTable = new int[pointsTable.Length];
		string[] array = new string[sortedPointsTable.Length];
		string[] array2 = new string[10];
		for (int i = 0; i < sortedPointsTable.Length; i++)
		{
			string text = pointsTable[i];
			array2 = text.Split("&"[0]);
			string text2 = (array[i] = string.Empty + i + "_" + array2[5] + "_" + array2[6]);
		}
		string[] array3 = new string[2];
		string[] array4 = new string[2];
		string[] array5 = new string[2];
		for (int i = 0; i < sortedPointsTable.Length; i++)
		{
			string text2 = array[i];
			array3 = text2.Split("_"[0]);
			for (int j = i + 1; j < sortedPointsTable.Length; j++)
			{
				text2 = array[j];
				array4 = text2.Split("_"[0]);
				if (int.Parse(array3[1]) == int.Parse(array4[1]))
				{
					if (float.Parse(array3[2]) < float.Parse(array4[2]))
					{
						string text3 = array[i];
						array[i] = array[j];
						array[j] = text3;
						array5 = array3;
						array3 = array4;
						array4 = array5;
					}
				}
				else if (int.Parse(array3[1]) < int.Parse(array4[1]))
				{
					string text3 = array[i];
					array[i] = array[j];
					array[j] = text3;
					array5 = array3;
					array3 = array4;
					array4 = array5;
				}
			}
		}
		for (int i = 0; i < sortedPointsTable.Length; i++)
		{
			string text2 = array[i];
			array3 = text2.Split("_"[0]);
			sortedPointsTable[i] = int.Parse(array3[0]);
		}
		return sortedPointsTable;
	}

	public void savePlayerDetails(int teamARank, int teamAscore, float teamAballFaced, int teamBRank, int teamBscore, float teamBballFaced, string[] pointsTable)
	{
		float num = 0f;
		float num2 = 0f;
		string text = pointsTable[teamARank];
		string text2 = pointsTable[teamBRank];
		string[] array = text.Split("&"[0]);
		string[] array2 = text2.Split("&"[0]);
		int num3 = teamAscore;
		float num4 = teamAballFaced;
		int num5 = teamBscore;
		float num6 = teamBballFaced;
		teamAscore = num3 + int.Parse(array[7]);
		teamAballFaced += float.Parse(array[8]);
		int num7 = num5 + int.Parse(array[9]);
		float num8 = num6 + float.Parse(array[10]);
		teamBscore = num5 + int.Parse(array2[7]);
		teamBballFaced += float.Parse(array2[8]);
		int num9 = num3 + int.Parse(array2[9]);
		float num10 = num4 + float.Parse(array2[10]);
		array[7] = string.Empty + teamAscore;
		array[8] = string.Empty + teamAballFaced;
		array[9] = string.Empty + num7;
		array[10] = string.Empty + num8;
		array2[7] = string.Empty + teamBscore;
		array2[8] = string.Empty + teamBballFaced;
		array2[9] = string.Empty + num9;
		array2[10] = string.Empty + num10;
		num = float.Parse(string.Empty + teamAscore) / float.Parse(string.Empty + teamAballFaced) - float.Parse(string.Empty + num7) / float.Parse(string.Empty + num8);
		num2 = float.Parse(string.Empty + teamBscore) / float.Parse(string.Empty + teamBballFaced) - float.Parse(string.Empty + num9) / float.Parse(string.Empty + num10);
		if (CONTROLLER.tournamentType == "PAK")
		{
			PointsWin = 2;
			PointsDraw = 1;
		}
		else
		{
			PointsWin = 4;
			PointsDraw = 2;
		}
		if (num3 > num5)
		{
			int num11 = int.Parse(array[0]) + 1;
			array[0] = string.Empty + num11;
			num11 = int.Parse(array[1]) + 1;
			array[1] = string.Empty + num11;
			num11 = int.Parse(array[5]) + PointsWin;
			array[5] = string.Empty + num11;
			if (num > 0f)
			{
				array[6] = "+" + num.ToString("F3");
			}
			else if (num < 0f)
			{
				array[6] = string.Empty + num.ToString("F3");
			}
			else
			{
				array[6] = "0";
			}
			text = (pointsTable[teamARank] = string.Join("&", array));
			num11 = int.Parse(array2[0]) + 1;
			array2[0] = string.Empty + num11;
			num11 = int.Parse(array2[2]) + 1;
			array2[2] = string.Empty + num11;
			if (num2 > 0f)
			{
				array2[6] = "+" + num2.ToString("F3");
			}
			else if (num2 < 0f)
			{
				array2[6] = string.Empty + num2.ToString("F3");
			}
			else
			{
				array2[6] = "0";
			}
			text2 = (pointsTable[teamBRank] = string.Join("&", array2));
		}
		else if (num5 > num3)
		{
			int num11 = int.Parse(array[0]) + 1;
			array[0] = string.Empty + num11;
			num11 = int.Parse(array[2]) + 1;
			array[2] = string.Empty + num11;
			if (num > 0f)
			{
				array[6] = "+" + num.ToString("F3");
			}
			else if (num < 0f)
			{
				array[6] = string.Empty + num.ToString("F3");
			}
			else
			{
				array[6] = "0";
			}
			text = (pointsTable[teamARank] = string.Join("&", array));
			num11 = int.Parse(array2[0]) + 1;
			array2[0] = string.Empty + num11;
			num11 = int.Parse(array2[1]) + 1;
			array2[1] = string.Empty + num11;
			num11 = int.Parse(array2[5]) + PointsWin;
			array2[5] = string.Empty + num11;
			if (num2 > 0f)
			{
				array2[6] = "+" + num2.ToString("F3");
			}
			else if (num2 < 0f)
			{
				array2[6] = string.Empty + num2.ToString("F3");
			}
			else
			{
				array2[6] = "0";
			}
			text2 = (pointsTable[teamBRank] = string.Join("&", array2));
		}
		else if (num3 == num5)
		{
			int num11 = int.Parse(array[0]) + 1;
			array[0] = string.Empty + num11;
			num11 = int.Parse(array[3]) + 1;
			array[3] = string.Empty + num11;
			num11 = int.Parse(array[5]) + PointsDraw;
			array[5] = string.Empty + num11;
			if (num > 0f)
			{
				array[6] = "+" + num.ToString("F3");
			}
			else if (num < 0f)
			{
				array[6] = string.Empty + num.ToString("F3");
			}
			else
			{
				array[6] = "0";
			}
			text = (pointsTable[teamARank] = string.Join("&", array));
			num11 = int.Parse(array2[0]) + 1;
			array2[0] = string.Empty + num11;
			num11 = int.Parse(array2[3]) + 1;
			array2[3] = string.Empty + num11;
			num11 = int.Parse(array2[5]) + PointsDraw;
			array2[5] = string.Empty + num11;
			if (num2 > 0f)
			{
				array2[6] = "+" + num2.ToString("F3");
			}
			else if (num2 < 0f)
			{
				array2[6] = string.Empty + num2.ToString("F3");
			}
			else
			{
				array2[6] = "0";
			}
			text2 = (pointsTable[teamBRank] = string.Join("&", array2));
		}
		string text3 = string.Empty;
		for (int i = 0; i < CONTROLLER.TeamList.Length; i++)
		{
			text3 = text3 + pointsTable[i] + "|";
		}
		if (CONTROLLER.PlayModeSelected == 3)
		{
			ObscuredPrefs.SetString("WCPointsTable", text3);
			CONTROLLER.WCLeagueData = string.Empty + CONTROLLER.WCTournamentStage + "|" + CONTROLLER.myTeamIndex + "|" + CONTROLLER.WCMyCurrentMatchIndex + "|" + CONTROLLER.WCTeamWonIndexStr + "|" + CONTROLLER.StoredWCTournamentResult;
			ObscuredPrefs.SetString("worldcup", CONTROLLER.WCLeagueData);
			CONTROLLER.WCLeagueMatchIndex++;
			ObscuredPrefs.SetInt("WCLeagueMatchIndex", CONTROLLER.WCLeagueMatchIndex);
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "PAK")
			{
				ObscuredPrefs.SetString("NPLPakistanPointsTable", text3);
				CONTROLLER.NplIndiaData = string.Empty + CONTROLLER.NPLIndiaTournamentStage + "|" + CONTROLLER.myTeamIndex + "|" + CONTROLLER.NPLIndiaMyCurrentMatchIndex + "|" + CONTROLLER.NPLIndiaTeamWonIndexStr + "|" + CONTROLLER.StoredNPLIndiaSeriesResult;
				ObscuredPrefs.SetString("NPLPakistanLeague", CONTROLLER.NplIndiaData);
				CONTROLLER.NPLIndiaLeagueMatchIndex++;
				ObscuredPrefs.SetInt("NPLPakistanLeagueMatchIndex", CONTROLLER.NPLIndiaLeagueMatchIndex);
			}
			else if (CONTROLLER.tournamentType == "NPL")
			{
				ObscuredPrefs.SetString("NPLIndiaPointsTable", text3);
				CONTROLLER.NplIndiaData = string.Empty + CONTROLLER.NPLIndiaTournamentStage + "|" + CONTROLLER.myTeamIndex + "|" + CONTROLLER.NPLIndiaMyCurrentMatchIndex + "|" + CONTROLLER.NPLIndiaTeamWonIndexStr + "|" + CONTROLLER.StoredNPLIndiaSeriesResult;
				ObscuredPrefs.SetString("NPLIndiaLeague", CONTROLLER.NplIndiaData);
				CONTROLLER.NPLIndiaLeagueMatchIndex++;
				ObscuredPrefs.SetInt("NPLIndiaLeagueMatchIndex", CONTROLLER.NPLIndiaLeagueMatchIndex);
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				ObscuredPrefs.SetString("NPLAustraliaPointsTable", text3);
				CONTROLLER.NplIndiaData = string.Empty + CONTROLLER.NPLIndiaTournamentStage + "|" + CONTROLLER.myTeamIndex + "|" + CONTROLLER.NPLIndiaMyCurrentMatchIndex + "|" + CONTROLLER.NPLIndiaTeamWonIndexStr + "|" + CONTROLLER.StoredNPLIndiaSeriesResult;
				ObscuredPrefs.SetString("NplAUS", CONTROLLER.NplIndiaData);
				CONTROLLER.NPLIndiaLeagueMatchIndex++;
				ObscuredPrefs.SetInt("NPLAustraliaLeagueMatchIndex", CONTROLLER.NPLIndiaLeagueMatchIndex);
			}
		}
	}

	public string[] setPointsTable(int teamCount, string[] pointsTable)
	{
		int num = 0;
		string text = string.Empty;
		string[] array = new string[10];
		pointsTable = new string[teamCount];
		if (CONTROLLER.PlayModeSelected == 3)
		{
			if (ObscuredPrefs.HasKey("WCPointsTable"))
			{
				num = 1;
				text = ObscuredPrefs.GetString("WCPointsTable");
			}
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "PAK")
			{
				if (ObscuredPrefs.HasKey("NPLPakistanPointsTable"))
				{
					num = 1;
					text = ObscuredPrefs.GetString("NPLPakistanPointsTable");
				}
			}
			else if (CONTROLLER.tournamentType == "NPL")
			{
				if (ObscuredPrefs.HasKey("NPLIndiaPointsTable"))
				{
					num = 1;
					text = ObscuredPrefs.GetString("NPLIndiaPointsTable");
				}
			}
			else if (CONTROLLER.tournamentType == "AUS" && ObscuredPrefs.HasKey("NPLAustraliaPointsTable"))
			{
				num = 1;
				text = ObscuredPrefs.GetString("NPLAustraliaPointsTable");
			}
		}
		if (num == 1)
		{
			array = text.Split("|"[0]);
			for (int i = 0; i < teamCount; i++)
			{
				pointsTable[i] = array[i];
			}
		}
		else
		{
			text = string.Empty;
			for (int i = 0; i < teamCount; i++)
			{
				pointsTable[i] = "0&0&0&0&0&0&0&0&0&0&0";
				text = text + pointsTable[i] + "|";
			}
		}
		return pointsTable;
	}

	private void PostGoogleAnalyticsEvent()
	{
	}

	protected void Start()
	{
		if (Application.loadedLevelName == "Preloader")
		{
			if (!PlayerPrefs.HasKey("LoftMeterAdded"))
			{
				for (int i = 0; i < 7; i++)
				{
					CONTROLLER.PlayModeSelected = i;
					AutoSave.DeleteFile();
				}
				PlayerPrefs.SetInt("LoftMeterAdded", 1);
				CONTROLLER.PlayModeSelected = 0;
			}
			if (PlayerPrefs.HasKey("AppVersionCode") && PlayerPrefs.GetInt("AppVersionCode") != CONTROLLER.AppVersionCode && !PlayerPrefs.HasKey("ClaimedBetaReward"))
			{
				PlayerPrefs.SetInt("CanShowBetaReward", 1);
			}
			PlayerPrefs.SetInt("AppVersionCode", CONTROLLER.AppVersionCode);
			if (PlayerPrefs.HasKey("EncrytionDone"))
			{
				EncryptionService.instance.LoadFromPlayerPrefs("TotalGameData");
			}
			PlayerPrefs.SetInt("EncrytionDone", 1);
			CheckIfNewUser();
			GetPowerUpDetails();
			GetSixesCount();
			GetUserTicketDetails();
			GetUserSpinDetails();
			GetUserXPDetails();
			GetUserCoinDetails();
			GetUserEarnedCoinDetails();
		}
		if (CONTROLLER.TeamList == null)
		{
			InitializeGame();
			GetTeamList();
			getSettingsList();
			PurchaseValidation();
		}
	}

	public void InitializeGame()
	{
		CONTROLLER.Overs[0] = 3;
		CONTROLLER.Overs[1] = 5;
		CONTROLLER.Overs[2] = 10;
		CONTROLLER.Overs[3] = 20;
		CONTROLLER.Overs[4] = 30;
		CONTROLLER.Overs[5] = 50;
		CONTROLLER.Overs[6] = 15;
		CONTROLLER.Overs[7] = 30;
		CONTROLLER.Overs[8] = 60;
		CONTROLLER.Overs[9] = 90;
	}

	public void getSettingsList()
	{
		if (ObscuredPrefs.HasKey("Settings"))
		{
			string @string = ObscuredPrefs.GetString("Settings");
			string[] array = @string.Split("|"[0]);
			CONTROLLER.bgMusicVal = int.Parse(array[0]);
			CONTROLLER.ambientVal = int.Parse(array[1]);
			CONTROLLER.menuBgVolume = float.Parse(array[2]);
			CONTROLLER.sfxVolume = float.Parse(array[3]);
			CONTROLLER.tutorialToggle = int.Parse(array[4]);
		}
		else
		{
			CONTROLLER.bgMusicVal = 1;
			CONTROLLER.ambientVal = 1;
			CONTROLLER.menuBgVolume = 1f;
			CONTROLLER.sfxVolume = 1f;
			CONTROLLER.tutorialToggle = 1;
			CONTROLLER.PlayerMode = false;
			SavePlayerPrefs.SetSettingsList();
		}
		if (CONTROLLER.sndController != null)
		{
			CONTROLLER.sndController.bgMusicToggle();
			CONTROLLER.sndController.ambientToggle();
		}
	}

	public void PurchaseValidation()
	{
		if (ObscuredPrefs.HasKey("NoOfTrails"))
		{
			CONTROLLER.noOfTrails = ObscuredPrefs.GetInt("NoOfTrails");
			if (CONTROLLER.noOfTrails == -1)
			{
				CONTROLLER.isFreeVersion = false;
			}
			else
			{
				CONTROLLER.isFreeVersion = true;
			}
		}
		else
		{
			CONTROLLER.noOfTrails = CONTROLLER.maxTrails;
			ObscuredPrefs.SetInt("NoOfTrails", CONTROLLER.maxTrails);
			CONTROLLER.isFreeVersion = true;
		}
		CONTROLLER.isFreeVersion = false;
	}

	public void GetTeamList()
	{
		if (PlayerPrefs.HasKey("GDPRClicked") && Application.loadedLevelName == "Preloader")
		{
			Marshmallow_Perm.gameObject.SetActive(value: true);
		}
	}

	public void OpenPrivacyPolicy()
	{
		Application.OpenURL(PrivacyPolicy);
	}

	public void GDPRCompliance()
	{
		PlayerPrefs.SetInt("GDPRClicked", 1);
		GDPRPopup.SetActive(value: false);
		if (!PlayerPrefs.HasKey("Localization"))
		{
			Singleton<LoadPlayerPrefs>.instance.LocalizationPopup.SetActive(value: true);
			Singleton<LoadPlayerPrefs>.instance.OnLanguagesClicked(1);
		}
	}

	public void OnLanguagesClicked(int index)
	{
		Debug.Log(index);
		if (ManageScene.activeSceneName() == "Preloader")
		{
			for (int i = 0; i < LanguageButtons.Length; i++)
			{
				if (index == i)
				{
					LanguageButtons[i].GetComponent<Image>().color = Color.red;
					LanguageButtons[i].GetComponentInChildren<Text>().color = Color.white;
				}
				else
				{
					LanguageButtons[i].GetComponent<Image>().color = Color.yellow;
					LanguageButtons[i].GetComponentInChildren<Text>().color = Color.black;
				}
			}
		}
		else if (ManageScene.activeSceneName() == "MainMenu")
		{
			for (int j = 0; j < Singleton<SettingsPageTWO>.instance.LanguageButtons.Length; j++)
			{
				if (index == j)
				{
					Singleton<SettingsPageTWO>.instance.LanguageButtons[j].GetComponent<Image>().color = Color.red;
					Singleton<SettingsPageTWO>.instance.LanguageButtons[j].GetComponentInChildren<Text>().color = Color.white;
				}
				else
				{
					Singleton<SettingsPageTWO>.instance.LanguageButtons[j].GetComponent<Image>().color = Color.yellow;
					Singleton<SettingsPageTWO>.instance.LanguageButtons[j].GetComponentInChildren<Text>().color = Color.black;
				}
			}
		}
		else if (ManageScene.activeSceneName() == "Ground")
		{
			for (int k = 0; k < Singleton<SettingsPageTWO>.instance.LanguageButtons.Length; k++)
			{
				if (index == k)
				{
					Singleton<SettingsPageTWO>.instance.LanguageButtons[k].GetComponent<Image>().color = Color.red;
					Singleton<SettingsPageTWO>.instance.LanguageButtons[k].GetComponentInChildren<Text>().color = Color.white;
				}
				else
				{
					Singleton<SettingsPageTWO>.instance.LanguageButtons[k].GetComponent<Image>().color = Color.yellow;
					Singleton<SettingsPageTWO>.instance.LanguageButtons[k].GetComponentInChildren<Text>().color = Color.black;
				}
			}
		}
		LocalizationData.instance.setSelectedLanguage(index);
		LocalizationData.instance.loadTheSelectedLanguageFromResources();
	}

	public void LocalizationOKButton()
	{
		if (ManageScene.activeSceneName() == "Preloader")
		{
			PlayerPrefs.SetInt("Localization", 1);
			LocalizationPopup.SetActive(value: false);
			Invoke("ActivatePermissions", 0.3f);
		}
		else if (ManageScene.activeSceneName() == "MainMenu" || ManageScene.activeSceneName() == "Ground")
		{
			Singleton<SettingsPageTWO>.instance.LocalizationHolder.SetActive(value: false);
			Singleton<SettingsPageTWO>.instance.Holder.SetActive(value: true);
		}
	}

	private void ActivatePermissions()
	{
		Marshmallow_Perm.gameObject.SetActive(value: true);
	}

	public void InstallationOKBtn()
	{
		InstallationPopup.SetActive(value: false);
		if (!PlayerPrefs.HasKey("GDPRClicked"))
		{
			GDPRPopup.SetActive(value: true);
		}
	}

	public void XMLLoaded()
	{
	}

	public void SetTeamList()
	{
		SavePlayerPrefs.SetTeamList();
	}

	public void LoadMenuScene()
	{
		Singleton<LoadingPanelTransition>.instance.PanelTransition1("MainMenu");
	}

	public void getTournamentList()
	{
		string text = " ";
		if (ObscuredPrefs.HasKey("tour"))
		{
			text = ObscuredPrefs.GetString("tour");
			string[] array = text.Split("&"[0]);
			CONTROLLER.TournamentStage = int.Parse(array[0]);
			CONTROLLER.myTeamIndex = int.Parse(array[1]);
			CONTROLLER.oversSelectedIndex = int.Parse(array[2]);
			CONTROLLER.matchIndex = int.Parse(array[3]);
			CONTROLLER.quaterFinalList = array[5];
			CONTROLLER.semiFinalList = array[6];
			CONTROLLER.finalList = array[7];
			CONTROLLER.tournamentStr = CONTROLLER.TournamentStage + "&" + CONTROLLER.myTeamIndex + "&" + CONTROLLER.oversSelectedIndex + "&" + CONTROLLER.matchIndex + "&" + array[4];
		}
	}

	protected void Update()
	{
	}
}
