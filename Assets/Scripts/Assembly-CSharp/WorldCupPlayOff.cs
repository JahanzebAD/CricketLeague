using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class WorldCupPlayOff : Singleton<WorldCupPlayOff>
{
	public GameObject holder;

	public Image highlightedFlag;

	public Text pageTitle;

	public Text[] teamName;

	public Image[] quaterFinalTeams;

	public Image[] semiFinalTeams;

	public Sprite whiteBG;

	public Image[] finalMatchTeams;

	public GameObject selectedFlag;

	private string[] getQuarterFinalData;

	private string[] getSemiFinalData;

	private string[] getFinalData;

	private string WCQuarter = string.Empty;

	private string WCSemiFinal = string.Empty;

	private string WCFinal = string.Empty;

	protected void Start()
	{
	}

	private void CheckForTeam()
	{
		if (CONTROLLER.WCTournamentStage == 1)
		{
			if (CONTROLLER.WCLeagueMatchIndex < getQuarterFinalData.Length)
			{
				string[] array = getQuarterFinalData[CONTROLLER.WCLeagueMatchIndex].Split("-"[0]);
				int num = int.Parse(array[0]);
				int num2 = int.Parse(array[1]);
				if (num == CONTROLLER.myTeamIndex)
				{
					CONTROLLER.myTeamIndex = num;
					CONTROLLER.opponentTeamIndex = num2;
				}
				else if (num2 == CONTROLLER.myTeamIndex)
				{
					CONTROLLER.myTeamIndex = num2;
					CONTROLLER.opponentTeamIndex = num;
				}
				else
				{
					SaveData(num, num2);
				}
			}
			else
			{
				CONTROLLER.WCTournamentStage = 2;
				CONTROLLER.WCLeagueMatchIndex = 0;
			}
		}
		if (CONTROLLER.WCTournamentStage == 2)
		{
			if (CONTROLLER.WCLeagueMatchIndex < getSemiFinalData.Length)
			{
				string[] array2 = getSemiFinalData[CONTROLLER.WCLeagueMatchIndex].Split("-"[0]);
				int num3 = int.Parse(array2[0]);
				int num4 = int.Parse(array2[1]);
				if (num3 == CONTROLLER.myTeamIndex)
				{
					CONTROLLER.myTeamIndex = num3;
					CONTROLLER.opponentTeamIndex = num4;
				}
				else if (num4 == CONTROLLER.myTeamIndex)
				{
					CONTROLLER.myTeamIndex = num4;
					CONTROLLER.opponentTeamIndex = num3;
				}
				else
				{
					SaveData(num3, num4);
				}
			}
			else
			{
				CONTROLLER.WCTournamentStage = 3;
				CONTROLLER.WCLeagueMatchIndex = 0;
			}
		}
		if (CONTROLLER.WCTournamentStage != 3)
		{
			return;
		}
		if (CONTROLLER.WCLeagueMatchIndex < getFinalData.Length)
		{
			string[] array3 = getFinalData[CONTROLLER.WCLeagueMatchIndex].Split("-"[0]);
			int num5 = int.Parse(array3[0]);
			int num6 = int.Parse(array3[1]);
			if (num5 == CONTROLLER.myTeamIndex)
			{
				CONTROLLER.myTeamIndex = num5;
				CONTROLLER.opponentTeamIndex = num6;
			}
			else if (num6 == CONTROLLER.myTeamIndex)
			{
				CONTROLLER.myTeamIndex = num6;
				CONTROLLER.opponentTeamIndex = num5;
			}
		}
		else
		{
			CONTROLLER.WCTournamentStage = 4;
		}
	}

	private void PlaceSelectedImage()
	{
		if (CONTROLLER.WCTournamentStage == 1)
		{
			Image[] array = quaterFinalTeams;
			foreach (Image image in array)
			{
				if (image.sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation)
				{
					selectedFlag.transform.SetParent(image.transform.parent);
					selectedFlag.transform.SetAsFirstSibling();
					selectedFlag.transform.localScale = Vector3.one;
					selectedFlag.transform.localPosition = image.gameObject.transform.localPosition;
				}
			}
		}
		else if (CONTROLLER.WCTournamentStage == 2)
		{
			Image[] array2 = semiFinalTeams;
			foreach (Image image2 in array2)
			{
				if (image2.sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation)
				{
					selectedFlag.transform.SetParent(image2.transform.parent);
					selectedFlag.transform.SetAsFirstSibling();
					selectedFlag.transform.localScale = Vector3.one;
					selectedFlag.transform.localPosition = image2.gameObject.transform.localPosition;
				}
			}
		}
		else
		{
			if (CONTROLLER.WCTournamentStage != 3)
			{
				return;
			}
			Image[] array3 = finalMatchTeams;
			foreach (Image image3 in array3)
			{
				if (image3.sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation)
				{
					selectedFlag.transform.SetParent(image3.transform.parent);
					selectedFlag.transform.SetAsFirstSibling();
					selectedFlag.transform.localScale = Vector3.one;
					selectedFlag.transform.localPosition = image3.gameObject.transform.localPosition;
				}
			}
		}
	}

	private void SaveData(int team1, int team2)
	{
		int num = AutoGenerate(team1, team2);
		string empty = string.Empty;
		string wCSemiFinal;
		if (CONTROLLER.WCTournamentStage == 1)
		{
			string[] array = WCSemiFinal.Split("|"[0]);
			empty = array[array.Length - 1];
			if (array.Length < semiFinalTeams.Length / 2)
			{
				if (empty.Contains("-"))
				{
					wCSemiFinal = WCSemiFinal;
					WCSemiFinal = wCSemiFinal + string.Empty + num + "|";
				}
				else
				{
					wCSemiFinal = WCSemiFinal;
					WCSemiFinal = wCSemiFinal + string.Empty + num + "-";
				}
			}
			else if (empty.Contains("-"))
			{
				WCSemiFinal = WCSemiFinal + string.Empty + num;
			}
			else
			{
				wCSemiFinal = WCSemiFinal;
				WCSemiFinal = wCSemiFinal + string.Empty + num + "-";
			}
			SetSemiFinalMatches();
		}
		if (CONTROLLER.WCTournamentStage == 2)
		{
			string[] array = WCFinal.Split("|"[0]);
			empty = array[array.Length - 1];
			if (array.Length < finalMatchTeams.Length / 2)
			{
				if (empty.Contains("-"))
				{
					wCSemiFinal = WCFinal;
					WCFinal = wCSemiFinal + string.Empty + num + "|";
				}
				else
				{
					wCSemiFinal = WCFinal;
					WCFinal = wCSemiFinal + string.Empty + num + "-";
				}
			}
			else if (empty.Contains("-"))
			{
				WCFinal = WCFinal + string.Empty + num;
			}
			else
			{
				wCSemiFinal = WCFinal;
				WCFinal = wCSemiFinal + string.Empty + num + "-";
			}
			SetFinalMatches();
		}
		CONTROLLER.WCLeagueMatchIndex++;
		if (CONTROLLER.WCTournamentStage == 1)
		{
			if (CONTROLLER.WCLeagueMatchIndex >= getQuarterFinalData.Length)
			{
				CONTROLLER.WCTournamentStage = 2;
				CONTROLLER.WCLeagueMatchIndex = 0;
			}
		}
		else if (CONTROLLER.WCTournamentStage == 2 && CONTROLLER.WCLeagueMatchIndex >= getSemiFinalData.Length)
		{
			CONTROLLER.WCTournamentStage = 3;
			CONTROLLER.WCLeagueMatchIndex = 0;
		}
		CONTROLLER.WCMyCurrentMatchIndex = CONTROLLER.WCLeagueMatchIndex;
		string text = CONTROLLER.WCTournamentStage + "&" + CONTROLLER.myTeamIndex + "&" + CONTROLLER.WCMyCurrentMatchIndex;
		wCSemiFinal = text;
		text = (CONTROLLER.WCLeagueData = wCSemiFinal + "&" + WCQuarter + "#" + WCSemiFinal + "#" + WCFinal);
		ObscuredPrefs.SetString("wcplayoff", CONTROLLER.WCLeagueData);
		CheckForTeam();
	}

	private int AutoGenerate(int team1, int team2)
	{
		int num = int.Parse(CONTROLLER.TeamList[team1].rank);
		int num2 = int.Parse(CONTROLLER.TeamList[team2].rank);
		int num3 = 0;
		int max = 36;
		int num4 = Mathf.Abs(num - num2);
		if (CONTROLLER.WCTournamentStage == 1)
		{
			num4 += 7;
		}
		else if (CONTROLLER.WCTournamentStage == 2)
		{
			num4++;
		}
		num3 = Random.Range(1, max);
		if (num < num2)
		{
			if (num3 % num4 == 0)
			{
				if (num4 < 4)
				{
					return team2;
				}
				return team1;
			}
			return team1;
		}
		if (num > num2)
		{
			if (num3 % num4 == 0)
			{
				if (num4 < 4)
				{
					return team1;
				}
				return team2;
			}
			return team2;
		}
		return -1;
	}

	private void SetQuaterFinalMatches()
	{
		getQuarterFinalData = WCQuarter.Split("|"[0]);
		for (int i = 0; i < getQuarterFinalData.Length; i++)
		{
			int num = -1;
			int num2 = -1;
			string[] array = getQuarterFinalData[i].Split("-"[0]);
			if (array.Length == 0 || array[0] == null || array[0] == string.Empty || array[0] == " ")
			{
				break;
			}
			if (array[0] != string.Empty)
			{
				num = int.Parse(array[0]);
			}
			if (array[1] != string.Empty)
			{
				num2 = int.Parse(array[1]);
			}
			if (num != -1)
			{
				string abbrevation = CONTROLLER.TeamList[num].abbrevation;
				quaterFinalTeams[i * 2].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
				teamName[i * 2].text = CONTROLLER.TeamList[num].abbrevation;
				if (num != CONTROLLER.myTeamIndex)
				{
				}
			}
			if (num2 != -1)
			{
				string abbrevation2 = CONTROLLER.TeamList[num2].abbrevation;
				quaterFinalTeams[i * 2 + 1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation2);
				teamName[i * 2 + 1].text = CONTROLLER.TeamList[num2].abbrevation;
				if (num2 != CONTROLLER.myTeamIndex)
				{
				}
			}
		}
	}

	private void SetSemiFinalMatches()
	{
		getSemiFinalData = WCSemiFinal.Split("|"[0]);
		for (int i = 0; i < getSemiFinalData.Length; i++)
		{
			int num = -1;
			int num2 = -1;
			string[] array = getSemiFinalData[i].Split("-"[0]);
			if (array.Length == 0 || array[0] == null || array[0] == string.Empty || array[0] == " ")
			{
				break;
			}
			if (array[0] != string.Empty)
			{
				num = int.Parse(array[0]);
			}
			if (array[1] != string.Empty)
			{
				num2 = int.Parse(array[1]);
			}
			if (num != -1)
			{
				string abbrevation = CONTROLLER.TeamList[num].abbrevation;
				semiFinalTeams[i * 2].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
				if (num != CONTROLLER.myTeamIndex)
				{
				}
			}
			if (num2 != -1)
			{
				string abbrevation2 = CONTROLLER.TeamList[num2].abbrevation;
				semiFinalTeams[i * 2 + 1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation2);
				if (num2 != CONTROLLER.myTeamIndex)
				{
				}
			}
		}
	}

	private void SetFinalMatches()
	{
		getFinalData = WCFinal.Split("|"[0]);
		for (int i = 0; i < getFinalData.Length; i++)
		{
			int num = -1;
			int num2 = -1;
			string[] array = getFinalData[i].Split("-"[0]);
			if (array.Length == 0 || array[0] == null || array[0] == string.Empty || array[0] == " ")
			{
				break;
			}
			if (array[0] != string.Empty)
			{
				num = int.Parse(array[0]);
			}
			if (array[1] != string.Empty)
			{
				num2 = int.Parse(array[1]);
			}
			if (num != -1)
			{
				string abbrevation = CONTROLLER.TeamList[num].abbrevation;
				finalMatchTeams[i * 2].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
				if (num != CONTROLLER.myTeamIndex)
				{
				}
			}
			if (num2 != -1)
			{
				string abbrevation2 = CONTROLLER.TeamList[num2].abbrevation;
				finalMatchTeams[i * 2 + 1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation2);
				if (num2 != CONTROLLER.myTeamIndex)
				{
				}
			}
		}
	}

	public void navigateToNextPage()
	{
		string empty = string.Empty;
		empty = AutoSave.ReadFile();
		if (empty == string.Empty)
		{
			Singleton<SquadPageTWO>.instance.showMe(1);
		}
		else
		{
			CONTROLLER.GameStartsFromSave = true;
			AutoSave.LoadGame();
			Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
		}
		HideMe();
	}

	private void DrawFixtures()
	{
		int num = 0;
		string[] array = CONTROLLER.WCLeagueData.Split("&"[0]);
		CONTROLLER.WCTournamentStage = int.Parse(array[0]);
		CONTROLLER.myTeamIndex = int.Parse(array[1]);
		CONTROLLER.WCMyCurrentMatchIndex = int.Parse(array[2]);
		CONTROLLER.WCLeagueMatchIndex = CONTROLLER.WCMyCurrentMatchIndex;
		string[] array2 = array[3].Split("#"[0]);
		for (num = 0; num < quaterFinalTeams.Length; num++)
		{
			quaterFinalTeams[num].sprite = whiteBG;
		}
		for (num = 0; num < semiFinalTeams.Length; num++)
		{
			semiFinalTeams[num].sprite = whiteBG;
		}
		for (num = 0; num < finalMatchTeams.Length; num++)
		{
			finalMatchTeams[num].sprite = whiteBG;
		}
		getQuarterFinalData = array2[0].Split("|"[0]);
		getSemiFinalData = array2[1].Split("|"[0]);
		getFinalData = array2[2].Split("|"[0]);
		WCQuarter = string.Empty + array2[0];
		WCSemiFinal = string.Empty + array2[1];
		WCFinal = string.Empty + array2[2];
		SetQuaterFinalMatches();
		SetSemiFinalMatches();
		SetFinalMatches();
		CheckForTeam();
	}

	private void SetPageTitle()
	{
		if (CONTROLLER.WCTournamentStage == 1)
		{
			pageTitle.text = LocalizationData.instance.getText(52) + " - " + LocalizationData.instance.getText(399);
		}
		else if (CONTROLLER.WCTournamentStage == 2)
		{
			pageTitle.text = LocalizationData.instance.getText(52) + " - " + LocalizationData.instance.getText(400);
		}
		else if (CONTROLLER.WCTournamentStage == 3)
		{
			pageTitle.text = LocalizationData.instance.getText(52) + " - " + LocalizationData.instance.getText(401);
		}
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = BackSelected;
		CONTROLLER.PlayModeSelected = 3;
		CONTROLLER.pageName = "wcplayoff";
		Singleton<LoadPlayerPrefs>.instance.GetWCTournamentList();
		CONTROLLER.CurrentMenu = "wcplayoff";
		holder.SetActive(value: true);
		DrawFixtures();
		SetPageTitle();
		PlaceSelectedImage();
	}

	private void HideMe()
	{
		holder.SetActive(value: false);
	}

	public void BackSelected()
	{
		HideMe();
		Singleton<GameModeTWO>.instance.showMe();
	}
}
