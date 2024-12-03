using UnityEngine;

public class PointsTable : Singleton<PointsTable>
{
	public PointsTableInfo[] pointsInfo;

	public GameObject holder;

	private string GroupID = "A";

	private int teamsInGroup;

	private Color32 normalColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	private Color32 activeColor = new Color32(158, 113, 46, byte.MaxValue);

	protected void Start()
	{
		holder.SetActive(value: false);
	}

	private void SetPointsTable()
	{
		CONTROLLER.WCSortedPointsTable = Singleton<LoadPlayerPrefs>.instance.SortWCPointsTable(CONTROLLER.WCPointsTable, CONTROLLER.WCSortedPointsTable);
		for (int i = 0; i < CONTROLLER.WCSortedPointsTable.Length; i++)
		{
			if (!(CONTROLLER.WCGroupDetails[CONTROLLER.WCSortedPointsTable[i]] != GroupID))
			{
				int num = CONTROLLER.WCSortedPointsTable[i];
				pointsInfo[teamsInGroup].TeamName.text = CONTROLLER.TeamList[num].teamName;
				string abbrevation = CONTROLLER.TeamList[num].abbrevation;
				pointsInfo[teamsInGroup].TeamLogo.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
				string text = CONTROLLER.WCPointsTable[num];
				string[] array = text.Split("&"[0]);
				if (num == CONTROLLER.myTeamIndex)
				{
					pointsInfo[teamsInGroup].StripBG.color = activeColor;
				}
				else
				{
					pointsInfo[teamsInGroup].StripBG.color = normalColor;
				}
				pointsInfo[teamsInGroup].matchesPlayed.text = string.Empty + array[0];
				pointsInfo[teamsInGroup].matchesWon.text = string.Empty + array[1];
				pointsInfo[teamsInGroup].matchesLost.text = string.Empty + array[2];
				pointsInfo[teamsInGroup].matchesTie.text = string.Empty + array[3];
				pointsInfo[teamsInGroup].noResult.text = "0";
				pointsInfo[teamsInGroup].points.text = string.Empty + array[5];
				pointsInfo[teamsInGroup].netRunRate.text = string.Empty + array[6];
				teamsInGroup++;
			}
		}
	}

	public void showWCGroupA()
	{
		GroupID = "A";
		teamsInGroup = 0;
		SetPointsTable();
	}

	public void showWCGroupB()
	{
		GroupID = "B";
		teamsInGroup = 0;
		SetPointsTable();
	}

	public void moveToPrevPage()
	{
		HideMe();
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = HideMe;
		CONTROLLER.pageName = "WCPointsTable";
		GroupID = CONTROLLER.WCGroupDetails[CONTROLLER.myTeamIndex];
		teamsInGroup = 0;
		SetPointsTable();
		holder.SetActive(value: true);
		Singleton<WorldCupLeague>.instance.holder.SetActive(value: false);
		CONTROLLER.CurrentMenu = "pointstable";
	}

	public void HideMe()
	{
		Singleton<WorldCupLeague>.instance.ShowMe();
		holder.SetActive(value: false);
		CONTROLLER.pageName = "WCLeague";
	}
}
