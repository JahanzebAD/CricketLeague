using CodeStage.AntiCheat.ObscuredTypes;
using nxtCrypto.PackData;
using UnityEngine;

public class SavePlayerPrefs : MonoBehaviour
{
	private static int[] xpAmount = new int[34]
	{
		1000, 2500, 5000, 7500, 10000, 12500, 25000, 50000, 75000, 100000,
		125000, 150000, 175000, 200000, 250000, 300000, 350000, 400000, 450000, 500000,
		550000, 600000, 650000, 700000, 750000, 800000, 850000, 900000, 950000, 1000000,
		1500000, 2000000, 2500000, 5000000
	};

	public static void SetSettingsList()
	{
		string empty = string.Empty;
		empty = empty + CONTROLLER.bgMusicVal + "|";
		empty = empty + CONTROLLER.ambientVal + "|";
		empty = empty + CONTROLLER.menuBgVolume + "|";
		empty = empty + CONTROLLER.sfxVolume + "|";
		empty = empty + CONTROLLER.tutorialToggle + "|";
		ObscuredPrefs.SetString("Settings", empty);
	}

	public static void SetTeamList()
	{
		string empty = string.Empty;
		empty += "<cricket>";
		empty += "<schedule>";
		empty += "<League>";
		for (int i = 0; i < CONTROLLER.TeamList.Length; i++)
		{
			string text = empty;
			empty = text + "<team name=\"" + CONTROLLER.TeamList[i].teamName + "\" abbrevation=\"" + CONTROLLER.TeamList[i].abbrevation + "\" ranking=\"" + CONTROLLER.TeamList[i].rank + "\">";
			empty += "<PlayerDetails>";
			for (int j = 0; j < CONTROLLER.TeamList[i].PlayerList.Length; j++)
			{
				empty += "<player";
				empty = empty + " name=\"" + CONTROLLER.TeamList[i].PlayerList[j].PlayerName + "\"";
				empty = empty + " battingHand=\"" + CONTROLLER.TeamList[i].PlayerList[j].BatsmanList.BattingHand + "\"";
				empty = empty + " bowlingHand=\"" + CONTROLLER.TeamList[i].PlayerList[j].BowlerList.BowlingHand + "\"";
				empty = empty + " bowlingStyle=\"" + CONTROLLER.TeamList[i].PlayerList[j].BowlerList.Style + "\"";
				empty = empty + " bowlingType=\"" + CONTROLLER.TeamList[i].PlayerList[j].BowlerList.bowlingRank + "\"";
				text = empty;
				empty = text + " bowlingOrder=\"" + CONTROLLER.TeamList[i].PlayerList[j].BowlerList.bowlingOrder + "\"";
				if (CONTROLLER.TeamList[i].KeeperIndex == j)
				{
					text = empty;
					empty = text + " isKeeper=\"" + 1 + "\"";
				}
				if (CONTROLLER.TeamList[i].CaptainIndex == j)
				{
					text = empty;
					empty = text + " isCaptain=\"" + 1 + "\"";
				}
				empty += "/>";
			}
			empty += "</PlayerDetails>";
			empty += "</team>";
		}
		empty += "</cricket>";
		empty += "</schedule>";
		empty += "</League>";
		if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "NPL")
			{
				ObscuredPrefs.SetString("teamlistNPL", empty);
			}
			else if (CONTROLLER.tournamentType == "PAK")
			{
				ObscuredPrefs.SetString("teamlistPak", empty);
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				ObscuredPrefs.SetString("teamlistAus", empty);
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			ObscuredPrefs.SetString("teamlistWC", empty);
		}
		else if (CONTROLLER.PlayModeSelected == 7)
		{
			ObscuredPrefs.SetString("teamlistTM", empty);
		}
		else if (CONTROLLER.PlayModeSelected > 3)
		{
			ObscuredPrefs.SetString("teamlistArcade", empty);
		}
		else
		{
			ObscuredPrefs.SetString("teamlist", empty);
		}
	}

	public static void InitialPowerUpDetails()
	{
		CONTROLLER.boughtPowerSubGrade = CONTROLLER.totalPowerSubGrade;
		CONTROLLER.boughtControlSubGrade = CONTROLLER.totalControlSubGrade;
		CONTROLLER.boughtAgilitySubGrade = CONTROLLER.totalAgilitySubGrade;
	}

	public static void SetPowerUpDetails()
	{
		CONTROLLER.totalPowerSubGrade = CONTROLLER.boughtPowerSubGrade + CONTROLLER.earnedPowerSubGrade;
		CONTROLLER.totalControlSubGrade = CONTROLLER.boughtControlSubGrade + CONTROLLER.earnedControlSubGrade;
		CONTROLLER.totalAgilitySubGrade = CONTROLLER.boughtAgilitySubGrade + CONTROLLER.earnedAgilitySubGrade;
		if (CONTROLLER.totalPowerSubGrade <= 300)
		{
			CONTROLLER.powerGrade = CONTROLLER.totalPowerSubGrade / 50;
		}
		else
		{
			int num = (CONTROLLER.totalPowerSubGrade - 300) / 250;
			CONTROLLER.powerGrade = 6 + num;
		}
		if (CONTROLLER.totalControlSubGrade <= 300)
		{
			CONTROLLER.controlGrade = CONTROLLER.totalControlSubGrade / 50;
		}
		else
		{
			int num2 = (CONTROLLER.totalControlSubGrade - 300) / 250;
			CONTROLLER.controlGrade = 6 + num2;
		}
		if (CONTROLLER.totalAgilitySubGrade <= 300)
		{
			CONTROLLER.agilityGrade = CONTROLLER.totalAgilitySubGrade / 50;
		}
		else
		{
			int num3 = (CONTROLLER.totalAgilitySubGrade - 300) / 250;
			CONTROLLER.agilityGrade = 6 + num3;
		}
		ObscuredPrefs.SetInt("powerGrade", CONTROLLER.powerGrade);
		ObscuredPrefs.SetInt("controlGrade", CONTROLLER.controlGrade);
		ObscuredPrefs.SetInt("agilityGrade", CONTROLLER.agilityGrade);
		ObscuredPrefs.SetInt("powerESG", CONTROLLER.earnedPowerSubGrade);
		ObscuredPrefs.SetInt("controlESG", CONTROLLER.earnedControlSubGrade);
		ObscuredPrefs.SetInt("agilityESG", CONTROLLER.earnedAgilitySubGrade);
		ObscuredPrefs.SetInt("powerBSG", CONTROLLER.boughtPowerSubGrade);
		ObscuredPrefs.SetInt("controlBSG", CONTROLLER.boughtControlSubGrade);
		ObscuredPrefs.SetInt("agilityBSG", CONTROLLER.boughtAgilitySubGrade);
		EncryptionProtectData();
	}

	public static void SetQuickPlayList()
	{
		string empty = string.Empty;
		empty = empty + CONTROLLER.myTeamIndex + "|";
		empty = empty + CONTROLLER.opponentTeamIndex + "|";
		empty += CONTROLLER.oversSelectedIndex;
		ObscuredPrefs.SetString("QuickPlayTeams", empty);
	}

	public static void SetTournamentStage(string str)
	{
		ObscuredPrefs.SetString("tour", str);
	}

	public static void SetTournamentStatus(string str)
	{
		ObscuredPrefs.SetString("tstatus", str);
	}

	public static void SaveUserCoins()
	{
		ObscuredPrefs.SetInt(CONTROLLER.CoinsKey, CONTROLLER.Coins);
		EncryptionProtectData();
	}

	public static void SaveUserTokens()
	{
	}

	public static void SaveUserTickets()
	{
		ObscuredPrefs.SetInt(CONTROLLER.TicketsKey, CONTROLLER.tickets);
		EncryptionProtectData();
	}

	public static void SaveWeeklyXps()
	{
		CONTROLLER.weekly_Xps = ObscuredPrefs.GetInt("weeklyXP");
		if (EncryptionService.instance != null)
		{
			CONTROLLER.weekly_Xps = EncryptionService.instance.WeekXp;
		}
	}

	public static void SaveWeeklyArcadeXps()
	{
		CONTROLLER.weekly_ArcadeXps = ObscuredPrefs.GetInt("weeklyArcadeXP");
		CONTROLLER.weekly_EarnedArcadeXps = ObscuredPrefs.GetInt("weeklyEarnedArcadeXP");
		if (EncryptionService.instance != null)
		{
			CONTROLLER.weekly_ArcadeXps = EncryptionService.instance.WeekArcadeXp;
			CONTROLLER.weekly_EarnedArcadeXps = EncryptionService.instance.WeekEarnedArcadeXp;
		}
	}

	public static void SaveUserXP()
	{
		ObscuredPrefs.SetInt(CONTROLLER.XPKey, CONTROLLER.XPs);
		ObscuredPrefs.SetInt("userEarnedXP", CONTROLLER.earnedXPs);
		EncryptionProtectData();
	}

	public static void SaveUserArcadeXP()
	{
		ObscuredPrefs.SetInt(CONTROLLER.ArcadeXPKey, CONTROLLER.ArcadeXPs);
		ObscuredPrefs.SetInt("userEarnedArcadeXP", CONTROLLER.earnedArcade);
		EncryptionProtectData();
	}

	public static void SaveSpins(int amount)
	{
		CONTROLLER.FreeSpin += amount;
		ObscuredPrefs.SetInt("userSpins", CONTROLLER.FreeSpin);
		EncryptionProtectData();
	}

	public static void SaveSixCount()
	{
		CONTROLLER.sixMeterCount = ObscuredPrefs.GetInt("ArcadeSixes");
	}

	public static void SetSixesCount()
	{
		ObscuredPrefs.SetInt("ArcadeSixes", CONTROLLER.sixMeterCount);
		EncryptionProtectData();
	}

	public static void ResetAllDetails()
	{
		CONTROLLER.spendCoins = 0;
		CONTROLLER.earnedCoins = 0;
		CONTROLLER.spendTickets = 0;
		CONTROLLER.earnedTickets = 0;
		CONTROLLER.earnedXPs = 0;
		CONTROLLER.earnedArcade = 0;
		ObscuredPrefs.SetInt("userSpendTickets", CONTROLLER.spendTickets);
		ObscuredPrefs.SetInt("userEarnedTickets", CONTROLLER.earnedTickets);
		ObscuredPrefs.SetInt("userSpendCoins", CONTROLLER.spendCoins);
		ObscuredPrefs.SetInt("userEarnedCoins", CONTROLLER.earnedCoins);
		ObscuredPrefs.SetInt("userEarnedXP", CONTROLLER.earnedXPs);
		EncryptionProtectData();
	}

	public static void SaveUserDetails(int coins, int tokens, int xp)
	{
		CONTROLLER.Coins = coins;
		CONTROLLER.XPs = xp;
		SaveUserCoins();
		Singleton<GameModeTWO>.instance.ResetDetails();
	}

	public static void SaveUserCoins(int coinAmount, int spentCoins, int earnedCoins)
	{
		if (ObscuredPrefs.HasKey(CONTROLLER.CoinsKey))
		{
			CONTROLLER.Coins = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey);
		}
		if (ObscuredPrefs.HasKey("userSpendCoins"))
		{
			CONTROLLER.spendCoins = ObscuredPrefs.GetInt("userSpendCoins");
		}
		if (ObscuredPrefs.HasKey("userEarnedCoins"))
		{
			CONTROLLER.earnedCoins = ObscuredPrefs.GetInt("userEarnedCoins");
		}
		if (EncryptionService.instance != null)
		{
			CONTROLLER.Coins = EncryptionService.instance.Coins;
			CONTROLLER.spendCoins = EncryptionService.instance.SpendCoins;
			CONTROLLER.earnedCoins = EncryptionService.instance.EarnedCoins;
		}
		if (CONTROLLER.Coins + coinAmount < 0)
		{
			CONTROLLER.Coins = 0;
		}
		else
		{
			CONTROLLER.Coins += coinAmount;
		}
		CONTROLLER.spendCoins += spentCoins;
		CONTROLLER.earnedCoins += earnedCoins;
		ObscuredPrefs.SetInt(CONTROLLER.CoinsKey, CONTROLLER.Coins);
		ObscuredPrefs.SetInt("userSpendCoins", CONTROLLER.spendCoins);
		ObscuredPrefs.SetInt("userEarnedCoins", CONTROLLER.earnedCoins);
		EncryptionProtectData();
	}

	public static void SaveUserXPs(int XPAmount, int earnedXP)
	{
		if (ObscuredPrefs.HasKey(CONTROLLER.XPKey))
		{
			CONTROLLER.XPs = ObscuredPrefs.GetInt(CONTROLLER.XPKey);
		}
		if (ObscuredPrefs.HasKey("userEarnedXP"))
		{
			CONTROLLER.earnedXPs = ObscuredPrefs.GetInt("userEarnedXP");
		}
		if (EncryptionService.instance != null)
		{
			CONTROLLER.XPs = EncryptionService.instance.XP;
			CONTROLLER.earnedXPs = EncryptionService.instance.EarnedXp;
		}
		CONTROLLER.XPs += XPAmount;
		CONTROLLER.earnedXPs += earnedXP;
		ObscuredPrefs.SetInt(CONTROLLER.XPKey, CONTROLLER.XPs);
		ObscuredPrefs.SetInt("userEarnedXP", CONTROLLER.earnedXPs);
		SetXPMilestoneIndex();
		EncryptionProtectData();
		SaveUserWeeklyXPs(XPAmount, 0);
	}

	public static void SaveUserWeeklyXPs(int XPAmount, int earnedXP)
	{
		if (ObscuredPrefs.HasKey("weeklyXP"))
		{
			CONTROLLER.weekly_Xps = ObscuredPrefs.GetInt("weeklyXP");
		}
		if (EncryptionService.instance != null)
		{
			CONTROLLER.weekly_Xps = EncryptionService.instance.WeekXp;
		}
		CONTROLLER.weekly_Xps += XPAmount;
		ObscuredPrefs.SetInt("weeklyXP", CONTROLLER.weekly_Xps);
		EncryptionProtectData();
		SetXPMilestoneIndex();
	}

	public static void SetXPMilestoneIndex()
	{
		for (int num = xpAmount.Length - 1; num >= 0; num--)
		{
			if (CONTROLLER.XPs > xpAmount[num])
			{
				if (num == xpAmount.Length - 1)
				{
					ObscuredPrefs.SetInt("XPMilestoneIndex", num);
				}
				else
				{
					ObscuredPrefs.SetInt("XPMilestoneIndex", num + 1);
				}
				break;
			}
		}
	}

	public static void SetArcadeXPMilestoneIndex()
	{
		for (int num = xpAmount.Length - 1; num >= 0; num--)
		{
			if (CONTROLLER.ArcadeXPs > xpAmount[num])
			{
				ObscuredPrefs.SetInt("ArcadeXPMilestoneIndex", num + 1);
				break;
			}
		}
	}

	public static void SaveUserArcadeXPs(int XPAmount, int earnedXP)
	{
		if (ObscuredPrefs.HasKey(CONTROLLER.ArcadeXPKey))
		{
			CONTROLLER.ArcadeXPs = ObscuredPrefs.GetInt(CONTROLLER.ArcadeXPKey);
		}
		if (ObscuredPrefs.HasKey("userEarnedArcadeXP"))
		{
			CONTROLLER.earnedArcade = ObscuredPrefs.GetInt("userEarnedArcadeXP");
		}
		if (EncryptionService.instance != null)
		{
			CONTROLLER.ArcadeXPs = EncryptionService.instance.ArcadeXp;
			CONTROLLER.earnedArcade = EncryptionService.instance.EarnedArcadeXp;
		}
		CONTROLLER.ArcadeXPs += XPAmount;
		CONTROLLER.earnedArcade += earnedXP;
		ObscuredPrefs.SetInt(CONTROLLER.ArcadeXPKey, CONTROLLER.ArcadeXPs);
		ObscuredPrefs.SetInt("userEarnedArcadeXP", CONTROLLER.earnedArcade);
		SetArcadeXPMilestoneIndex();
		EncryptionProtectData();
		SaveUserWeeklyArcadeXPs(XPAmount, 0);
	}

	public static void SaveUserWeeklyArcadeXPs(int XPAmount, int earnedXP)
	{
		if (ObscuredPrefs.HasKey("weeklyArcadeXP"))
		{
			CONTROLLER.weekly_ArcadeXps = ObscuredPrefs.GetInt("weeklyArcadeXP");
		}
		if (EncryptionService.instance != null)
		{
			CONTROLLER.weekly_ArcadeXps = EncryptionService.instance.WeekArcadeXp;
		}
		CONTROLLER.weekly_ArcadeXps += XPAmount;
		ObscuredPrefs.SetInt("weeklyArcadeXP", CONTROLLER.weekly_ArcadeXps);
		EncryptionProtectData();
		SetXPMilestoneIndex();
	}

	public static void SaveUserTickets(int TicketAmount, int spentTickets, int earnedTickets)
	{
		if (ObscuredPrefs.HasKey(CONTROLLER.TicketsKey))
		{
			CONTROLLER.tickets = ObscuredPrefs.GetInt(CONTROLLER.TicketsKey);
		}
		if (ObscuredPrefs.HasKey("userEarnedTickets"))
		{
			CONTROLLER.earnedTickets = ObscuredPrefs.GetInt("userEarnedTickets");
		}
		if (ObscuredPrefs.HasKey("userSpendTickets"))
		{
			CONTROLLER.spendTickets = ObscuredPrefs.GetInt("userSpendTickets");
		}
		if (EncryptionService.instance != null)
		{
			CONTROLLER.tickets = EncryptionService.instance.Tickets;
			CONTROLLER.earnedTickets = EncryptionService.instance.EarnedTickets;
			CONTROLLER.spendTickets = EncryptionService.instance.SpendTickets;
		}
		if (CONTROLLER.tickets + TicketAmount < 0)
		{
			CONTROLLER.tickets = 0;
		}
		else
		{
			CONTROLLER.tickets += TicketAmount;
		}
		CONTROLLER.earnedTickets += earnedTickets;
		CONTROLLER.spendTickets += spentTickets;
		ObscuredPrefs.SetInt(CONTROLLER.TicketsKey, CONTROLLER.tickets);
		ObscuredPrefs.SetInt("userEarnedTickets", CONTROLLER.earnedTickets);
		ObscuredPrefs.SetInt("userSpendTickets", CONTROLLER.spendTickets);
		EncryptionProtectData();
	}

	public static void SaveUserTokens(int TokenAmount, int spentTokens, int earnedTokens)
	{
	}

	public static void ClearAllTournamentProgress()
	{
		CONTROLLER.WCLeagueMatchIndex = 0;
		CONTROLLER.WCTournamentStage = 0;
		CONTROLLER.WCLeagueData = string.Empty;
		CONTROLLER.StoredWCTournamentResult = string.Empty;
		CONTROLLER.WCTeamWonIndexStr = string.Empty;
		CONTROLLER.WCLeagueMatchIndex = 0;
		CONTROLLER.WCMyCurrentMatchIndex = 0;
		CONTROLLER.NplIndiaData = string.Empty;
		CONTROLLER.NPLIndiaTournamentStage = 0;
		CONTROLLER.NPLIndiaMyCurrentMatchIndex = 0;
		CONTROLLER.NPLIndiaTeamWonIndexStr = string.Empty;
		CONTROLLER.StoredNPLIndiaSeriesResult = string.Empty;
		CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
		CONTROLLER.CurrentLevelCompleted = 0;
		CONTROLLER.LevelFailed = 0;
		CONTROLLER.LevelId = 0;
	}

	public static void OnSignOut()
	{
		CONTROLLER.Coins = 0;
		CONTROLLER.tickets = 0;
		CONTROLLER.XPs = 0;
		CONTROLLER.sixMeterCount = 0;
		CONTROLLER.FreeSpin = 0;
		CONTROLLER.totalAgilitySubGrade = 0;
		CONTROLLER.totalPowerSubGrade = 0;
		CONTROLLER.totalControlSubGrade = 0;
		InitialPowerUpDetails();
		ObscuredPrefs.SetInt(CONTROLLER.CoinsKey, CONTROLLER.Coins);
		ObscuredPrefs.SetInt(CONTROLLER.TicketsKey, CONTROLLER.tickets);
		ObscuredPrefs.SetInt(CONTROLLER.XPKey, CONTROLLER.XPs);
		ClearAllTournamentProgress();
		SetSettingsList();
		SetSixesCount();
		Singleton<GameModeTWO>.instance.ValidateArcadeMeter();
		ResetAllDetails();
	}

	public static void EncryptionProtectData()
	{
		if (EncryptionService.instance != null)
		{
			EncryptionService.instance.SaveToPlayerPrefs(new PackData(CONTROLLER.Coins, CONTROLLER.earnedCoins, CONTROLLER.spendCoins, CONTROLLER.tickets, CONTROLLER.earnedTickets, CONTROLLER.spendTickets, CONTROLLER.XPs, CONTROLLER.earnedXPs, CONTROLLER.ArcadeXPs, CONTROLLER.earnedArcade, CONTROLLER.FreeSpin, CONTROLLER.sixMeterCount, CONTROLLER.powerGrade, CONTROLLER.controlGrade, CONTROLLER.agilityGrade, CONTROLLER.totalPowerSubGrade, CONTROLLER.totalControlSubGrade, CONTROLLER.totalAgilitySubGrade, CONTROLLER.weekly_Xps, CONTROLLER.weekly_EarnedXps, CONTROLLER.weekly_ArcadeXps, CONTROLLER.weekly_EarnedArcadeXps), "TotalGameData");
		}
	}
}
