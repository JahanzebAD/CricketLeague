using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class IncompleteMatchTWO : Singleton<IncompleteMatchTWO>
{
	public GameObject Holder;

	protected void Start()
	{
		Holder.SetActive(value: false);
	}

	public void YesButton()
	{
		Singleton<Popups>.instance.HideMe();
		CONTROLLER.GameStartsFromSave = false;
		CONTROLLER.isFreeHitBall = false;
		AutoSave.DeleteFile();
		if (CONTROLLER.PlayModeSelected == 0)
		{
			Singleton<GameModeTWO>.instance.getExhibitionState();
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			//Singleton<Firebase_Events>.instance.Firebase_T20_Quit();
			ObscuredPrefs.SetInt("T20Paid", 0);
			ObscuredPrefs.SetInt("T20TeamsSelected", 0);
			if (ObscuredPrefs.HasKey("tour"))
			{
				ObscuredPrefs.DeleteKey("tour");
			}
			if (ObscuredPrefs.HasKey("tstatus"))
			{
				ObscuredPrefs.DeleteKey("tstatus");
			}
			CONTROLLER.TournamentStage = 0;
			CONTROLLER.myTeamIndex = 0;
			CONTROLLER.matchIndex = 0;
			CONTROLLER.tournamentStatus = "LEAGUE MATCH";
			CONTROLLER.quaterFinalList = string.Empty;
			CONTROLLER.semiFinalList = string.Empty;
			CONTROLLER.finalList = string.Empty;
			CONTROLLER.tournamentStr = string.Empty;
			CONTROLLER.menuTitle = "TEAM SELECTION";
			Singleton<GameModeTWO>.instance.hideMe();
			Singleton<GameModeTWO>.instance.updateTitle(_modeSelected: true);
			Singleton<GameModeTWO>.instance.getTournamentState();
			Singleton<FixturesTWO>.instance.disableReset = true;
			Singleton<FixturesTWO>.instance.Holder.SetActive(value: false);
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "NPL")
			{
				//Singleton<Firebase_Events>.instance.Firebase_PRL_Quit();
				ObscuredPrefs.SetInt("NPLTeamsSelected", 0);
				ObscuredPrefs.SetInt("NPLPaid", 0);
				AutoSave.DeleteFile();
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
				//Singleton<Firebase_Events>.instance.Firebase_PRL_Quit();
				ObscuredPrefs.SetInt("PAKTeamsSelected", 0);
				ObscuredPrefs.SetInt("PAKPaid", 0);
				AutoSave.DeleteFile();
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
				//Singleton<Firebase_Events>.instance.Firebase_PRL_Quit();
				ObscuredPrefs.SetInt("AUSTeamsSelected", 0);
				ObscuredPrefs.SetInt("AUSPaid", 0);
				AutoSave.DeleteFile();
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
			Singleton<NPLIndiaLeague>.instance.DeletePlayerPrefs();
			CONTROLLER.TournamentStage = 0;
			CONTROLLER.myTeamIndex = 0;
			CONTROLLER.matchIndex = 0;
			CONTROLLER.menuTitle = "TEAM SELECTION";
			CONTROLLER.NPLIndiaPointsTable = Singleton<LoadPlayerPrefs>.instance.setPointsTable(CONTROLLER.TeamList.Length, CONTROLLER.WCPointsTable);
			CONTROLLER.NplIndiaData = string.Empty;
			CONTROLLER.NPLIndiaTournamentStage = 0;
			CONTROLLER.myTeamIndex = 0;
			CONTROLLER.NPLIndiaMyCurrentMatchIndex = 0;
			CONTROLLER.NPLIndiaTeamWonIndexStr = string.Empty;
			CONTROLLER.StoredNPLIndiaSeriesResult = string.Empty;
			CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
			Singleton<EntryFeesAndRewards>.instance.ShowMe();
			Singleton<NPLIndiaLeague>.instance.disableReset = true;
			Singleton<NPLIndiaLeague>.instance.holder.SetActive(value: false);
			Singleton<NPLIndiaPlayOff>.instance.holder.SetActive(value: false);
			if (CONTROLLER.tournamentType == "NPL")
			{
				Singleton<GameModeTWO>.instance.getNplState();
			}
			else if (CONTROLLER.tournamentType == "PAK")
			{
				Singleton<GameModeTWO>.instance.getPakState();
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				Singleton<GameModeTWO>.instance.getAusState();
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			//Singleton<Firebase_Events>.instance.Firebase_WC_Quit();
			ObscuredPrefs.SetInt("WCTeamsSelected", 0);
			ObscuredPrefs.SetInt("WCPaid", 0);
			AutoSave.DeleteFile();
			if (ObscuredPrefs.HasKey("worldcup"))
			{
				ObscuredPrefs.DeleteKey("worldcup");
			}
			if (ObscuredPrefs.HasKey("wcplayoff"))
			{
				ObscuredPrefs.DeleteKey("wcplayoff");
			}
			if (ObscuredPrefs.HasKey("WCLeagueMatchIndex"))
			{
				ObscuredPrefs.DeleteKey("WCLeagueMatchIndex");
			}
			if (ObscuredPrefs.HasKey("WCPointsTable"))
			{
				ObscuredPrefs.DeleteKey("WCPointsTable");
			}
			CONTROLLER.WCLeagueMatchIndex = 0;
			CONTROLLER.WCPointsTable = Singleton<LoadPlayerPrefs>.instance.setPointsTable(CONTROLLER.TeamList.Length, CONTROLLER.WCPointsTable);
			CONTROLLER.WCTournamentStage = 0;
			CONTROLLER.WCLeagueData = string.Empty;
			CONTROLLER.StoredWCTournamentResult = string.Empty;
			CONTROLLER.WCTeamWonIndexStr = string.Empty;
			CONTROLLER.WCLeagueMatchIndex = 0;
			CONTROLLER.WCMyCurrentMatchIndex = 0;
			CONTROLLER.myTeamIndex = 0;
			Singleton<EntryFeesAndRewards>.instance.ShowMe();
			Singleton<WorldCupLeague>.instance.disableReset = true;
			Singleton<WorldCupLeague>.instance.holder.SetActive(value: false);
			Singleton<WorldCupPlayOff>.instance.holder.SetActive(value: false);
		}
	}

	public void NoButton()
	{
		Singleton<Popups>.instance.HideMe();
	}

	public void CancelButton()
	{
		hideMe();
		CONTROLLER.CurrentMenu = "landingpage";
	}

	public void showMe()
	{
		CONTROLLER.PopupName = "incompletePopup2";
		CONTROLLER.CurrentMenu = "unfinishedmatch";
		Singleton<Popups>.instance.ShowMe();
	}

	public void hideMe()
	{
		Singleton<GameModeTWO>.instance.showMe();
		Holder.SetActive(value: false);
	}
}
