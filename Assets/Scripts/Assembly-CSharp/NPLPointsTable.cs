using UnityEngine;
using UnityEngine.UI;

public class NPLPointsTable : Singleton<NPLPointsTable>
{
	public GameObject holder;

	public NPLPointsTableInfo[] pointsInfo;

	public GameObject[] pointsTableContainerGO;

	public Text Title;

	private Color32 normalColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	private Color32 activeColor = new Color32(158, 113, 46, byte.MaxValue);

	protected void Start()
	{
		HideMe();
	}

	private void SetPointsTable(string[] pointsTable, int[] sortedPointsTable)
	{
		sortedPointsTable = Singleton<LoadPlayerPrefs>.instance.SortWCPointsTable(pointsTable, sortedPointsTable);
		for (int i = 0; i < sortedPointsTable.Length; i++)
		{
			int num = sortedPointsTable[i];
			pointsInfo[i].TeamName.text = CONTROLLER.TeamList[num].teamName;
			string abbrevation = CONTROLLER.TeamList[num].abbrevation;
			pointsInfo[i].TeamLogo.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
			string text = pointsTable[num];
			string[] array = text.Split("&"[0]);
			if (num == CONTROLLER.myTeamIndex)
			{
				pointsInfo[i].StripBG.color = activeColor;
			}
			else
			{
				pointsInfo[i].StripBG.color = normalColor;
			}
			pointsInfo[i].matchesPlayed.text = string.Empty + array[0];
			pointsInfo[i].matchesWon.text = string.Empty + array[1];
			pointsInfo[i].matchesLost.text = string.Empty + array[2];
			pointsInfo[i].matchesTie.text = string.Empty + array[3];
			pointsInfo[i].noResult.text = "0";
			pointsInfo[i].points.text = string.Empty + array[5];
			pointsInfo[i].netRunRate.text = string.Empty + array[6];
		}
	}

	public void BackSelected()
	{
		Singleton<NPLIndiaLeague>.instance.ShowMe();
		CONTROLLER.pageName = "nplleague";
		HideMe();
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = BackSelected;
		CONTROLLER.pageName = "nplpoints";
		Singleton<NPLIndiaLeague>.instance.holder.SetActive(value: false);
		for (int i = 0; i < pointsTableContainerGO.Length; i++)
		{
			pointsTableContainerGO[i].SetActive(value: true);
			if (i > CONTROLLER.TeamList.Length - 1)
			{
				pointsTableContainerGO[i].SetActive(value: false);
			}
		}
		if (CONTROLLER.tournamentType == "PAK")
		{
			Title.text = LocalizationData.instance.getText(428) + " - " + LocalizationData.instance.getText(411);
		}
		else if (CONTROLLER.tournamentType == "NPL")
		{
			Title.text = LocalizationData.instance.getText(427);
		}
		else if (CONTROLLER.tournamentType == "AUS")
		{
			Title.text = LocalizationData.instance.getText(16) + " - " + LocalizationData.instance.getText(411);
		}
		SetPointsTable(CONTROLLER.NPLIndiaPointsTable, CONTROLLER.NPLIndiaSortedPointsTable);
		holder.SetActive(value: true);
		CONTROLLER.CurrentMenu = "nplpointstable";
	}

	private void HideMe()
	{
		holder.SetActive(value: false);
	}
}
