using UnityEngine;

public class SavePlayerPrefsTWO : MonoBehaviour
{
	public static void SetSettingsList()
	{
		string empty = string.Empty;
		empty = empty + CONTROLLER.bgMusicVal + "|";
		empty = empty + CONTROLLER.ambientVal + "|";
		empty = empty + CONTROLLER.menuBgVolume + "|";
		empty = empty + CONTROLLER.sfxVolume + "|";
		empty = empty + CONTROLLER.tutorialToggle + "|";
		PlayerPrefs.SetString("Settings", empty);
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
		PlayerPrefs.SetString("teamlist", empty);
	}

	public static void SetQuickPlayList()
	{
		string empty = string.Empty;
		empty = empty + CONTROLLER.myTeamIndex + "|";
		empty = empty + CONTROLLER.opponentTeamIndex + "|";
		empty += CONTROLLER.oversSelectedIndex;
		PlayerPrefs.SetString("QuickPlayTeams", empty);
	}

	public static void SetTournamentStage(string str)
	{
		PlayerPrefs.SetString("tour", str);
	}

	public static void SetTournamentStatus(string str)
	{
		PlayerPrefs.SetString("tstatus", str);
	}
}
