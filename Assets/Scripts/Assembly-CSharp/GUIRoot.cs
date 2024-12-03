using UnityEngine;

public class GUIRoot : Singleton<GUIRoot>
{
	private TextAsset xmlAsset;

	private string dataStr;

	public void GetMyTeamList()
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TextAsset textAsset = Resources.Load("XML/Test/" + CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation) as TextAsset;
			XMLReader.ParsePlayerDetailsForTest(textAsset.text);
		}
		GetOppTeamList();
	}

	private void GetOppTeamList()
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TextAsset textAsset = Resources.Load("XML/Test/" + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation) as TextAsset;
			XMLReader.ParsePlayerDetailsForTest(textAsset.text);
		}
	}

	public void GetNPLIndiaTeams(string _xmlPath)
	{
		TextAsset textAsset = Resources.Load(_xmlPath) as TextAsset;
		if (textAsset != null)
		{
			XMLReader.ParseNPLIndiaTeams(textAsset.text);
		}
	}

	public void GetWorldCupTeams(string _xmlPath)
	{
		TextAsset textAsset = Resources.Load(_xmlPath) as TextAsset;
		if (textAsset != null)
		{
			XMLReader.ParseWorldCupTeams(textAsset.text);
		}
	}

	public void GetOtherTourTeams(string _xmlPath)
	{
		TextAsset textAsset = Resources.Load(_xmlPath) as TextAsset;
		if (textAsset != null)
		{
			XMLReader.ParseXML(textAsset.text);
		}
	}
}
