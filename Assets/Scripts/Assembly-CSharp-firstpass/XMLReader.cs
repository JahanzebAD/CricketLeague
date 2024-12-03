using System.Collections.Generic;
using System.IO;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class XMLReader : CONTROLLER
{
	public static GameObject Loader;

	public static GameObject PHPHandler;

	public static GameObject FBLoader;

	public static GameObject PHPChallengeHandler;

	private static TextAsset xmlAsset;

	public static int teamLength;

	public static void assignNow()
	{
		for (int i = 0; i < CONTROLLER.TM_TeamInfo.Length; i++)
		{
			CONTROLLER.TM_TeamInfo[i] = new TMTeamList();
		}
	}

	public static void LoadXML(string XMLName)
	{
		string path = Application.dataPath + XMLName;
		if (File.Exists(path))
		{
			StreamReader streamReader = File.OpenText(path);
			string datas = streamReader.ReadToEnd();
			streamReader.Close();
			ParseXML(datas);
		}
	}

	public static void ParseNPLIndiaTeams(string datas)
	{
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(datas);
		XMLNodeList xMLNodeList = (XMLNodeList)xMLNode["cricket"];
		XMLNode xMLNode2 = (XMLNode)xMLNodeList[0];
		XMLNodeList xMLNodeList2 = (XMLNodeList)xMLNode2["team"];
		int count = xMLNodeList2.Count;
		CONTROLLER.TeamList = new TeamInfo[count];
		for (int i = 0; i < count; i++)
		{
			XMLNode xMLNode3 = (XMLNode)xMLNodeList2[i];
			CONTROLLER.TeamList[i] = new TeamInfo();
			CONTROLLER.TeamList[i].teamName = xMLNode3["@name"] as string;
			CONTROLLER.TeamList[i].teamName = CONTROLLER.FirstLetterCaps(CONTROLLER.TeamList[i].teamName);
			if (xMLNode3.ContainsKey("@abbr"))
			{
				CONTROLLER.TeamList[i].abbrevation = xMLNode3["@abbr"] as string;
			}
			if (xMLNode3.ContainsKey("@ranking"))
			{
				CONTROLLER.TeamList[i].rank = xMLNode3["@ranking"] as string;
			}
			if (xMLNode3.ContainsKey("@id"))
			{
				CONTROLLER.TeamList[i].teamId = int.Parse(xMLNode3["@id"] as string);
			}
			XMLNodeList xMLNodeList3 = (XMLNodeList)xMLNode3["match"];
			int count2 = xMLNodeList3.Count;
			CONTROLLER.TeamList[i].MatchList = new MatchInfo[count2];
			for (int j = 0; j < count2; j++)
			{
				XMLNode xMLNode4 = (XMLNode)xMLNodeList3[j];
				CONTROLLER.TeamList[i].MatchList[j] = new MatchInfo();
				string s = xMLNode4["_text"] as string;
				CONTROLLER.TeamList[i].MatchList[j].teamID = int.Parse(s);
			}
		}
		xmlAsset = Resources.Load("XML/Teams/CHE") as TextAsset;
		if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "NPL")
			{
				if (ObscuredPrefs.HasKey("teamlistNPL"))
				{
					string @string = ObscuredPrefs.GetString("teamlistNPL");
					ParsePlayerDetails(@string);
				}
				else
				{
					ParsePlayerDetails(xmlAsset.text);
				}
			}
			else if (CONTROLLER.tournamentType == "PAK")
			{
				xmlAsset = Resources.Load("XML/Teams/PAK") as TextAsset;
				if (ObscuredPrefs.HasKey("teamlistPak"))
				{
					string string2 = ObscuredPrefs.GetString("teamlistPak");
					ParsePlayerDetails(string2);
				}
				else
				{
					ParsePlayerDetails(xmlAsset.text);
				}
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				xmlAsset = Resources.Load("XML/Teams/AUS") as TextAsset;
				if (ObscuredPrefs.HasKey("teamlistAus"))
				{
					string string3 = ObscuredPrefs.GetString("teamlistAus");
					ParsePlayerDetails(string3);
				}
				else
				{
					ParsePlayerDetails(xmlAsset.text);
				}
			}
		}
		else if (ObscuredPrefs.HasKey("teamlistArcade"))
		{
			string string4 = ObscuredPrefs.GetString("teamlistArcade");
			ParseXML(string4);
		}
		else
		{
			ParseXML(xmlAsset.text);
		}
	}

	public static void LoadTeams(int index)
	{
		TextAsset textAsset = Resources.Load("XML/GameMode/NplIndia") as TextAsset;
		string key = string.Empty;
		string empty = string.Empty;
		if (CONTROLLER.PlayModeSelected < 2)
		{
			key = "teamlist";
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "NPL")
			{
				key = "teamlistNPL";
			}
			else if (CONTROLLER.tournamentType == "PAK")
			{
				key = "teamlistPak";
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			key = "teamlistWC";
		}
		else if (CONTROLLER.PlayModeSelected > 3)
		{
			key = "teamlistArcade";
		}
		switch (index)
		{
		case 0:
			textAsset = Resources.Load("XML/GameMode/NplIndia") as TextAsset;
			xmlAsset = Resources.Load("XML/Teams/CHE") as TextAsset;
			break;
		case 1:
			textAsset = Resources.Load("XML/GameMode/NplPak") as TextAsset;
			xmlAsset = Resources.Load("XML/Teams/PAK") as TextAsset;
			break;
		}
		empty = textAsset.text;
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(empty);
		XMLNodeList xMLNodeList = (XMLNodeList)xMLNode["cricket"];
		XMLNode xMLNode2 = (XMLNode)xMLNodeList[0];
		XMLNodeList xMLNodeList2 = (XMLNodeList)xMLNode2["team"];
		int count = xMLNodeList2.Count;
		CONTROLLER.TeamList = new TeamInfo[count];
		for (int i = 0; i < count; i++)
		{
			XMLNode xMLNode3 = (XMLNode)xMLNodeList2[i];
			CONTROLLER.TeamList[i] = new TeamInfo();
			CONTROLLER.TeamList[i].teamName = xMLNode3["@name"] as string;
			CONTROLLER.TeamList[i].teamName = CONTROLLER.FirstLetterCaps(CONTROLLER.TeamList[i].teamName);
			if (xMLNode3.ContainsKey("@abbr"))
			{
				CONTROLLER.TeamList[i].abbrevation = xMLNode3["@abbr"] as string;
			}
			if (xMLNode3.ContainsKey("@ranking"))
			{
				CONTROLLER.TeamList[i].rank = xMLNode3["@ranking"] as string;
			}
			if (xMLNode3.ContainsKey("@id"))
			{
				CONTROLLER.TeamList[i].teamId = int.Parse(xMLNode3["@id"] as string);
			}
			XMLNodeList xMLNodeList3 = (XMLNodeList)xMLNode3["match"];
			int count2 = xMLNodeList3.Count;
			CONTROLLER.TeamList[i].MatchList = new MatchInfo[count2];
			for (int j = 0; j < count2; j++)
			{
				XMLNode xMLNode4 = (XMLNode)xMLNodeList3[j];
				CONTROLLER.TeamList[i].MatchList[j] = new MatchInfo();
				string s = xMLNode4["_text"] as string;
				CONTROLLER.TeamList[i].MatchList[j].teamID = int.Parse(s);
			}
		}
		if (ObscuredPrefs.HasKey(key))
		{
			string @string = ObscuredPrefs.GetString(key);
			ParsePlayerDetails(@string);
		}
		else
		{
			ParsePlayerDetails(xmlAsset.text);
		}
	}

	public static void ParseWorldCupTeams(string datas)
	{
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(datas);
		XMLNodeList xMLNodeList = (XMLNodeList)xMLNode["cricket"];
		XMLNode xMLNode2 = (XMLNode)xMLNodeList[0];
		XMLNodeList xMLNodeList2 = (XMLNodeList)xMLNode2["team"];
		int count = xMLNodeList2.Count;
		CONTROLLER.TeamList = new TeamInfo[count];
		for (int i = 0; i < count; i++)
		{
			XMLNode xMLNode3 = (XMLNode)xMLNodeList2[i];
			CONTROLLER.TeamList[i] = new TeamInfo();
			CONTROLLER.TeamList[i].teamName = xMLNode3["@name"] as string;
			CONTROLLER.TeamList[i].teamName = CONTROLLER.FirstLetterCaps(CONTROLLER.TeamList[i].teamName);
			if (xMLNode3.ContainsKey("@group"))
			{
				CONTROLLER.TeamList[i].teamGroup = xMLNode3["@group"] as string;
			}
			if (xMLNode3.ContainsKey("@abbr"))
			{
				CONTROLLER.TeamList[i].abbrevation = xMLNode3["@abbr"] as string;
			}
			if (xMLNode3.ContainsKey("@ranking"))
			{
				CONTROLLER.TeamList[i].rank = xMLNode3["@ranking"] as string;
			}
			if (xMLNode3.ContainsKey("@id"))
			{
				CONTROLLER.TeamList[i].teamId = int.Parse(xMLNode3["@id"] as string);
			}
			XMLNodeList xMLNodeList3 = (XMLNodeList)xMLNode3["match"];
			int count2 = xMLNodeList3.Count;
			CONTROLLER.TeamList[i].MatchList = new MatchInfo[count2];
			for (int j = 0; j < count2; j++)
			{
				XMLNode xMLNode4 = (XMLNode)xMLNodeList3[j];
				CONTROLLER.TeamList[i].MatchList[j] = new MatchInfo();
				string s = xMLNode4["_text"] as string;
				CONTROLLER.TeamList[i].MatchList[j].teamID = int.Parse(s);
			}
		}
		xmlAsset = Resources.Load("WorldCupSchedule") as TextAsset;
		if (CONTROLLER.PlayModeSelected == 3)
		{
			if (ObscuredPrefs.HasKey("teamlistWC"))
			{
				string @string = ObscuredPrefs.GetString("teamlistWC");
				ParsePlayerDetails(@string);
			}
			else
			{
				ParsePlayerDetails(xmlAsset.text);
			}
		}
		else if (CONTROLLER.PlayModeSelected == 7)
		{
			if (ObscuredPrefs.HasKey("teamlistTM"))
			{
				string string2 = ObscuredPrefs.GetString("teamlistTM");
				ParseXML(string2);
			}
			else
			{
				ParseXML(xmlAsset.text);
			}
			ParseXML(xmlAsset.text);
		}
		else if (ObscuredPrefs.HasKey("teamlist"))
		{
			string string3 = ObscuredPrefs.GetString("teamlist");
			ParseXML(string3);
		}
		else
		{
			ParseXML(xmlAsset.text);
		}
	}

	public static void ParsePlayerDetails(string datas)
	{
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(datas);
		XMLNodeList xMLNodeList = (XMLNodeList)xMLNode["cricket"];
		XMLNode xMLNode2 = (XMLNode)xMLNodeList[0];
		XMLNodeList xMLNodeList2 = (XMLNodeList)xMLNode2["schedule"];
		XMLNode xMLNode3 = (XMLNode)xMLNodeList2[0];
		XMLNodeList xMLNodeList3 = (XMLNodeList)xMLNode3["League"];
		XMLNode xMLNode4 = (XMLNode)xMLNodeList3[0];
		XMLNodeList xMLNodeList4 = (XMLNodeList)xMLNode4["team"];
		int count = xMLNodeList4.Count;
		for (int i = 0; i < count; i++)
		{
			XMLNode xMLNode5 = (XMLNode)xMLNodeList4[i];
			XMLNodeList xMLNodeList5 = (XMLNodeList)xMLNode5["PlayerDetails"];
			XMLNode xMLNode6 = (XMLNode)xMLNodeList5[0];
			XMLNodeList xMLNodeList6 = (XMLNodeList)xMLNode6["player"];
			int count2 = xMLNodeList6.Count;
			CONTROLLER.TeamList[i].PlayerList = new PlayerInfo[count2];
			for (int j = 0; j < count2; j++)
			{
				XMLNode xMLNode7 = (XMLNode)xMLNodeList6[j];
				CONTROLLER.TeamList[i].PlayerList[j] = new PlayerInfo();
				CONTROLLER.TeamList[i].PlayerList[j] = new PlayerInfo();
				CONTROLLER.TeamList[i].PlayerList[j].BatsmanList = new BatsmenInfo();
				CONTROLLER.TeamList[i].PlayerList[j].BowlerList = new BowlerInfo();
				CONTROLLER.TeamList[i].PlayerList[j].PlayerName = xMLNode7["@name"] as string;
				CONTROLLER.TeamList[i].PlayerList[j].facebookName = xMLNode7["@name"] as string;
				if (!xMLNode7.ContainsKey("@isKeeper"))
				{
					CONTROLLER.TeamList[i].PlayerList[j].isKeeper = false;
				}
				else
				{
					string text = xMLNode7["@isKeeper"] as string;
					if (text == "1")
					{
						CONTROLLER.TeamList[i].KeeperIndex = j;
						CONTROLLER.TeamList[i].PlayerList[j].isKeeper = true;
					}
				}
				if (!xMLNode7.ContainsKey("@isCaptain"))
				{
					CONTROLLER.TeamList[i].PlayerList[j].isCaptain = false;
				}
				else
				{
					string text2 = xMLNode7["@isCaptain"] as string;
					if (text2 == "1")
					{
						CONTROLLER.TeamList[i].CaptainIndex = j;
						CONTROLLER.TeamList[i].PlayerList[j].isCaptain = true;
					}
				}
				if (xMLNode7.ContainsKey("@battingHand"))
				{
					CONTROLLER.TeamList[i].PlayerList[j].BatsmanList.BattingHand = xMLNode7["@battingHand"] as string;
				}
				if (xMLNode7.ContainsKey("@bowlingHand"))
				{
					CONTROLLER.TeamList[i].PlayerList[j].BowlerList.BowlingHand = xMLNode7["@bowlingHand"] as string;
				}
				if (xMLNode7.ContainsKey("@bowlingStyle"))
				{
					CONTROLLER.TeamList[i].PlayerList[j].BowlerList.Style = xMLNode7["@bowlingStyle"] as string;
				}
				if (xMLNode7.ContainsKey("@bowlingType"))
				{
					CONTROLLER.TeamList[i].PlayerList[j].BowlerList.bowlingRank = xMLNode7["@bowlingType"] as string;
				}
				if (xMLNode7.ContainsKey("@bowlingOrder"))
				{
					if (CONTROLLER.TeamList[i].PlayerList[j].BowlerList.bowlingRank == null)
					{
						CONTROLLER.TeamList[i].PlayerList[j].BowlerList.bowlingRank = string.Empty;
					}
					string text3 = xMLNode7["@bowlingOrder"] as string;
					if (text3 != null && text3 != string.Empty)
					{
						CONTROLLER.TeamList[i].PlayerList[j].BowlerList.bowlingOrder = int.Parse(text3);
					}
				}
				if (j < 4)
				{
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceLevel = 5f;
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceInc = 0.5f;
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceDec = 0.25f;
				}
				else if (j >= 4 && j <= 7)
				{
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceLevel = 4f;
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceInc = 0.2f;
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceDec = 0.15f;
				}
				else if (j >= 8 && j < 11)
				{
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceLevel = 3f;
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceInc = 0.1f;
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceDec = 0.05f;
				}
			}
		}
	}

	public static void ParseXML(string datas)
	{
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(datas);
		XMLNodeList xMLNodeList = (XMLNodeList)xMLNode["cricket"];
		XMLNode xMLNode2 = (XMLNode)xMLNodeList[0];
		XMLNodeList xMLNodeList2 = (XMLNodeList)xMLNode2["schedule"];
		XMLNode xMLNode3 = (XMLNode)xMLNodeList2[0];
		XMLNodeList xMLNodeList3 = (XMLNodeList)xMLNode3["League"];
		XMLNode xMLNode4 = (XMLNode)xMLNodeList3[0];
		XMLNodeList xMLNodeList4 = (XMLNodeList)xMLNode4["team"];
		int count = xMLNodeList4.Count;
		CONTROLLER.TeamList = new TeamInfo[count];
		for (int i = 0; i < count; i++)
		{
			XMLNode xMLNode5 = (XMLNode)xMLNodeList4[i];
			CONTROLLER.TeamList[i] = new TeamInfo();
			CONTROLLER.TeamList[i].teamName = xMLNode5["@name"] as string;
			if (xMLNode5.ContainsKey("@abbrevation"))
			{
				CONTROLLER.TeamList[i].abbrevation = xMLNode5["@abbrevation"] as string;
			}
			if (xMLNode5.ContainsKey("@ranking"))
			{
				CONTROLLER.TeamList[i].rank = xMLNode5["@ranking"] as string;
			}
			XMLNodeList xMLNodeList5 = (XMLNodeList)xMLNode5["PlayerDetails"];
			XMLNode xMLNode6 = (XMLNode)xMLNodeList5[0];
			XMLNodeList xMLNodeList6 = (XMLNodeList)xMLNode6["player"];
			int count2 = xMLNodeList6.Count;
			CONTROLLER.TeamList[i].PlayerList = new PlayerInfo[count2];
			for (int j = 0; j < count2; j++)
			{
				XMLNode xMLNode7 = (XMLNode)xMLNodeList6[j];
				CONTROLLER.TeamList[i].PlayerList[j] = new PlayerInfo();
				CONTROLLER.TeamList[i].PlayerList[j] = new PlayerInfo();
				CONTROLLER.TeamList[i].PlayerList[j].BatsmanList = new BatsmenInfo();
				CONTROLLER.TeamList[i].PlayerList[j].BowlerList = new BowlerInfo();
				CONTROLLER.TeamList[i].PlayerList[j].PlayerName = xMLNode7["@name"] as string;
				CONTROLLER.TeamList[i].PlayerList[j].facebookName = xMLNode7["@name"] as string;
				if (!xMLNode7.ContainsKey("@isKeeper"))
				{
					CONTROLLER.TeamList[i].PlayerList[j].isKeeper = false;
				}
				else
				{
					string text = xMLNode7["@isKeeper"] as string;
					if (text == "1")
					{
						CONTROLLER.TeamList[i].KeeperIndex = j;
						CONTROLLER.TeamList[i].PlayerList[j].isKeeper = true;
					}
				}
				if (!xMLNode7.ContainsKey("@isCaptain"))
				{
					CONTROLLER.TeamList[i].PlayerList[j].isCaptain = false;
				}
				else
				{
					string text2 = xMLNode7["@isCaptain"] as string;
					if (text2 == "1")
					{
						CONTROLLER.TeamList[i].CaptainIndex = j;
						CONTROLLER.TeamList[i].PlayerList[j].isCaptain = true;
					}
				}
				if (xMLNode7.ContainsKey("@battingHand"))
				{
					CONTROLLER.TeamList[i].PlayerList[j].BatsmanList.BattingHand = xMLNode7["@battingHand"] as string;
				}
				if (xMLNode7.ContainsKey("@bowlingHand"))
				{
					CONTROLLER.TeamList[i].PlayerList[j].BowlerList.BowlingHand = xMLNode7["@bowlingHand"] as string;
				}
				if (xMLNode7.ContainsKey("@bowlingStyle"))
				{
					CONTROLLER.TeamList[i].PlayerList[j].BowlerList.Style = xMLNode7["@bowlingStyle"] as string;
				}
				if (xMLNode7.ContainsKey("@bowlingType"))
				{
					CONTROLLER.TeamList[i].PlayerList[j].BowlerList.bowlingRank = xMLNode7["@bowlingType"] as string;
				}
				if (xMLNode7.ContainsKey("@bowlingOrder"))
				{
					if (CONTROLLER.TeamList[i].PlayerList[j].BowlerList.bowlingRank == null)
					{
						CONTROLLER.TeamList[i].PlayerList[j].BowlerList.bowlingRank = string.Empty;
					}
					string text3 = xMLNode7["@bowlingOrder"] as string;
					if (text3 != null && text3 != string.Empty)
					{
						CONTROLLER.TeamList[i].PlayerList[j].BowlerList.bowlingOrder = int.Parse(text3);
					}
				}
				if (j < 4)
				{
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceLevel = 5f;
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceInc = 0.5f;
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceDec = 0.25f;
				}
				else if (j >= 4 && j <= 7)
				{
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceLevel = 4f;
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceInc = 0.2f;
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceDec = 0.15f;
				}
				else if (j >= 8 && j < 11)
				{
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceLevel = 3f;
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceInc = 0.1f;
					CONTROLLER.TeamList[i].PlayerList[j].ConfidenceDec = 0.05f;
				}
			}
		}
		if (Loader != null)
		{
			Loader.SendMessage("XMLLoaded");
		}
	}

	public static void ParsePlayerDetailsForTest(string _data)
	{
	}

	public static List<string> LoadPlayerNames(string _xmlData)
	{
		List<string> list = new List<string>();
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(_xmlData);
		XMLNodeList xMLNodeList = (XMLNodeList)xMLNode["PlayerNames"];
		XMLNode xMLNode2 = (XMLNode)xMLNodeList[0];
		XMLNodeList xMLNodeList2 = (XMLNodeList)xMLNode2["wcc"];
		XMLNode xMLNode3 = (XMLNode)xMLNodeList2[0];
		XMLNodeList xMLNodeList3 = (XMLNodeList)xMLNode3["team"];
		int count = xMLNodeList3.Count;
		for (int i = 0; i < count; i++)
		{
			XMLNode xMLNode4 = (XMLNode)xMLNodeList3[i];
			XMLNodeList xMLNodeList4 = (XMLNodeList)xMLNode4["player"];
			int count2 = xMLNodeList4.Count;
			string[] array = new string[count2];
			for (int j = 0; j < count2; j++)
			{
				XMLNode xMLNode5 = (XMLNode)xMLNodeList4[j];
				array[j] = xMLNode5["@name"] as string;
				list.Add(array[j]);
			}
		}
		return list;
	}

	public static void LoadHelpText(string xmlData)
	{
		XMLParser xMLParser = new XMLParser();
		XMLNode xMLNode = xMLParser.Parse(xmlData);
		XMLNodeList xMLNodeList = (XMLNodeList)xMLNode["cricket"];
		XMLNode xMLNode2 = (XMLNode)xMLNodeList[0];
		xMLNodeList = (XMLNodeList)xMLNode2["WCC"];
		xMLNode2 = (XMLNode)xMLNodeList[0];
		xMLNodeList = (XMLNodeList)xMLNode2["body"];
		int count = xMLNodeList.Count;
		CONTROLLER.HelpText = new HelpInfo[count];
		for (int i = 0; i < count; i++)
		{
			CONTROLLER.HelpText[i] = new HelpInfo();
			xMLNode2 = (XMLNode)xMLNodeList[i];
			XMLNodeList xMLNodeList2 = (XMLNodeList)xMLNode2["title"];
			XMLNode xMLNode3 = (XMLNode)xMLNodeList2[0];
			CONTROLLER.HelpText[i].title = xMLNode3["_text"] as string;
			xMLNodeList2 = (XMLNodeList)xMLNode2["desc"];
			xMLNode3 = (XMLNode)xMLNodeList2[0];
			CONTROLLER.HelpText[i].desc = xMLNode3["_text"] as string;
		}
		Loader.SendMessage("HelpLoaded");
	}
}
