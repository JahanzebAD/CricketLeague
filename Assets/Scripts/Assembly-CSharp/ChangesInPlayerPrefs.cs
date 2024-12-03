using System.Collections.Generic;
using UnityEngine;

public class ChangesInPlayerPrefs
{
	public void updatePlayerPrefs(int _savedVersion)
	{
	}

	public void updateAllPlayerPrefs()
	{
		updateWCCTeamList();
	}

	private void updateWCCTeamList()
	{
		if (PlayerPrefs.HasKey("teamlist"))
		{
			checkForDefaultPlayerName();
		}
	}

	private void checkForDefaultPlayerName()
	{
		List<string> newNames = GetNewNames();
		List<string> list = new List<string>();
		for (int i = 0; i < CONTROLLER.TeamList.Length; i++)
		{
			list.Add(newNames[i]);
			for (int j = 0; j < CONTROLLER.TeamList[i].PlayerList.Length; j++)
			{
				string playerName = CONTROLLER.TeamList[i].PlayerList[j].PlayerName;
				string text = ((j >= 9) ? ("Player" + (j + 1)) : ("Player0" + (j + 1)));
				if (playerName == text)
				{
					CONTROLLER.TeamList[i].PlayerList[j].PlayerName = list[j];
				}
			}
		}
		CONTROLLER.gameMode = "WCC";
		SavePlayerPrefs.SetTeamList();
	}

	private List<string> GetNewNames()
	{
		List<string> list = new List<string>();
		TextAsset textAsset = Resources.Load("WCCPlayerNames") as TextAsset;
		return XMLReader.LoadPlayerNames(textAsset.text);
	}
}
