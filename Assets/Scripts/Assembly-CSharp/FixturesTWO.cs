using UnityEngine;
using UnityEngine.UI;

public class FixturesTWO : Singleton<FixturesTWO>
{
	public GameObject Holder;

	public GameObject continueBtn;

	public GameObject amountBtn;

	public GameObject selectedFlag;

	public Text[] teamName;

	public Sprite TBA;

	public Image[] leagueTeams;

	public Image[] quaterFinalTeams;

	public Image[] semiFinalTeams;

	public Image[] finalMatchTeams;

	public Text[] abbrText;

	public Image[] sideFlags;

	public Text ticketAmount;

	public Text availableTickets;

	private string[] leagueMatchData;

	private string[] getQuaterFinalData;

	private string[] getSemiFinalData;

	private string[] getFinalData;

	public Button resetTournament;

	public bool disableReset;

	public void Awake()
	{
		hideMe();
	}

	protected void Start()
	{
	}

	private void PlaceSelectedImage()
	{
		if (CONTROLLER.TournamentStage == 0)
		{
			Image[] array = leagueTeams;
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
		else if (CONTROLLER.TournamentStage == 1)
		{
			Image[] array2 = quaterFinalTeams;
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
		else if (CONTROLLER.TournamentStage == 2)
		{
			Image[] array3 = semiFinalTeams;
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
		else
		{
			if (CONTROLLER.TournamentStage != 3)
			{
				return;
			}
			Image[] array4 = finalMatchTeams;
			foreach (Image image4 in array4)
			{
				if (image4.sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation)
				{
					selectedFlag.transform.SetParent(image4.transform.parent);
					selectedFlag.transform.SetAsFirstSibling();
					selectedFlag.transform.localScale = Vector3.one;
					selectedFlag.transform.localPosition = image4.gameObject.transform.localPosition;
				}
			}
		}
	}

	public void ResetTournament()
	{
		Singleton<IncompleteMatchTWO>.instance.showMe();
	}

	public void CheckForMyTeam()
	{
		string empty = string.Empty;
		empty = AutoSave.ReadFile();
		if (CONTROLLER.tickets >= int.Parse(ticketAmount.text) || empty != string.Empty)
		{
			if (CONTROLLER.TournamentStage == 0)
			{
				if (CONTROLLER.matchIndex < leagueMatchData.Length)
				{
					string empty2 = string.Empty;
					empty2 = AutoSave.ReadFile();
					if (empty2 == string.Empty)
					{
						string[] array = leagueMatchData[CONTROLLER.matchIndex].Split("-"[0]);
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
						GoNextPage();
					}
					else
					{
						CONTROLLER.GameStartsFromSave = true;
						AutoSave.LoadGame();
						hideMe();
						CONTROLLER.SceneIsLoading = true;
						Singleton<GameModeTWO>.instance.displayGameMode(_bool: false);
						Singleton<GameModeTWO>.instance.hideHomeBtn(_bool: false);
						Singleton<GameModeTWO>.instance.updateTitle(_modeSelected: true);
						Singleton<GameModeTWO>.instance.hideMe();
						Singleton<NavigationBack>.instance.deviceBack = null;
						Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
						CONTROLLER.CurrentMenu = string.Empty;
					}
				}
				else
				{
					CONTROLLER.TournamentStage = 1;
				}
			}
			if (CONTROLLER.TournamentStage == 1)
			{
				if (CONTROLLER.matchIndex < getQuaterFinalData.Length)
				{
					string empty3 = string.Empty;
					empty3 = AutoSave.ReadFile();
					if (empty3 == string.Empty)
					{
						string[] array2 = getQuaterFinalData[CONTROLLER.matchIndex].Split("-"[0]);
						int num3 = int.Parse(array2[0]);
						int num4 = int.Parse(array2[1]);
						if (num3 == CONTROLLER.myTeamIndex)
						{
							CONTROLLER.myTeamIndex = num3;
							CONTROLLER.opponentTeamIndex = num4;
							GoNextPage();
						}
						else if (num4 == CONTROLLER.myTeamIndex)
						{
							CONTROLLER.myTeamIndex = num4;
							CONTROLLER.opponentTeamIndex = num3;
							GoNextPage();
						}
					}
					else
					{
						CONTROLLER.GameStartsFromSave = true;
						AutoSave.LoadGame();
						hideMe();
						CONTROLLER.SceneIsLoading = true;
						Singleton<GameModeTWO>.instance.displayGameMode(_bool: false);
						Singleton<GameModeTWO>.instance.hideHomeBtn(_bool: false);
						Singleton<GameModeTWO>.instance.updateTitle(_modeSelected: true);
						Singleton<GameModeTWO>.instance.hideMe();
						Singleton<NavigationBack>.instance.deviceBack = null;
						Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
						CONTROLLER.CurrentMenu = string.Empty;
					}
				}
				else
				{
					CONTROLLER.TournamentStage = 2;
				}
			}
			if (CONTROLLER.TournamentStage == 2)
			{
				if (CONTROLLER.matchIndex < getSemiFinalData.Length)
				{
					string empty4 = string.Empty;
					empty4 = AutoSave.ReadFile();
					if (empty4 == string.Empty)
					{
						string[] array3 = getSemiFinalData[CONTROLLER.matchIndex].Split("-"[0]);
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
						GoNextPage();
					}
					else
					{
						CONTROLLER.GameStartsFromSave = true;
						AutoSave.LoadGame();
						hideMe();
						CONTROLLER.SceneIsLoading = true;
						Singleton<GameModeTWO>.instance.displayGameMode(_bool: false);
						Singleton<GameModeTWO>.instance.hideHomeBtn(_bool: false);
						Singleton<GameModeTWO>.instance.updateTitle(_modeSelected: true);
						Singleton<GameModeTWO>.instance.hideMe();
						Singleton<NavigationBack>.instance.deviceBack = null;
						Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
						CONTROLLER.CurrentMenu = string.Empty;
					}
				}
				else
				{
					CONTROLLER.TournamentStage = 3;
				}
			}
			if (CONTROLLER.TournamentStage != 3)
			{
				return;
			}
			if (CONTROLLER.matchIndex < getFinalData.Length)
			{
				string empty5 = string.Empty;
				empty5 = AutoSave.ReadFile();
				if (empty5 == string.Empty)
				{
					string[] array4 = getFinalData[CONTROLLER.matchIndex].Split("-"[0]);
					int num7 = int.Parse(array4[0]);
					int num8 = int.Parse(array4[1]);
					if (num7 == CONTROLLER.myTeamIndex)
					{
						CONTROLLER.myTeamIndex = num7;
						CONTROLLER.opponentTeamIndex = num8;
						GoNextPage();
					}
					else if (num8 == CONTROLLER.myTeamIndex)
					{
						CONTROLLER.myTeamIndex = num8;
						CONTROLLER.opponentTeamIndex = num7;
						GoNextPage();
					}
				}
				else
				{
					CONTROLLER.GameStartsFromSave = true;
					AutoSave.LoadGame();
					hideMe();
					CONTROLLER.SceneIsLoading = true;
					Singleton<GameModeTWO>.instance.displayGameMode(_bool: false);
					Singleton<GameModeTWO>.instance.hideHomeBtn(_bool: false);
					Singleton<GameModeTWO>.instance.updateTitle(_modeSelected: true);
					Singleton<GameModeTWO>.instance.hideMe();
					Singleton<NavigationBack>.instance.deviceBack = null;
					Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
					CONTROLLER.CurrentMenu = string.Empty;
				}
			}
			else
			{
				CONTROLLER.TournamentStage = 4;
			}
		}
		else
		{
			int num9 = Singleton<TemporaryStore>.instance.CalculateIndexValue(int.Parse(ticketAmount.text));
			if (Singleton<Store>.instance.CoinsPrice[num9] <= CONTROLLER.Coins)
			{
				Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
				Singleton<NavigationBack>.instance.deviceBack = Singleton<TemporaryStore>.instance.CancelButton;
				Singleton<TemporaryStore>.instance.ShowMe(num9);
				return;
			}
			Singleton<NavigationBack>.instance.tempDeviceBack = showMe;
			GameModeTWO.insuffStatus = "tickets";
			CONTROLLER.PopupName = "insuffPopup";
			Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(152) + "!";
			Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(124);
			Singleton<Popups>.instance.ShowMe();
		}
	}

	public void CheckForTeam()
	{
		if (CONTROLLER.TournamentStage == 0)
		{
			if (CONTROLLER.matchIndex < leagueMatchData.Length)
			{
				string[] array = leagueMatchData[CONTROLLER.matchIndex].Split("-"[0]);
				int num = int.Parse(array[0]);
				int num2 = int.Parse(array[1]);
				if (num == CONTROLLER.myTeamIndex)
				{
					abbrText[0].text = CONTROLLER.TeamList[num].abbrevation;
					sideFlags[0].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[0].text);
					abbrText[1].text = CONTROLLER.TeamList[num2].abbrevation;
					sideFlags[1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[1].text);
				}
				else if (num2 == CONTROLLER.myTeamIndex)
				{
					abbrText[0].text = CONTROLLER.TeamList[num2].abbrevation;
					sideFlags[0].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[0].text);
					abbrText[1].text = CONTROLLER.TeamList[num].abbrevation;
					sideFlags[1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[1].text);
				}
				else
				{
					saveData(num, num2);
				}
			}
			else
			{
				CONTROLLER.TournamentStage = 1;
				CONTROLLER.matchIndex = 0;
			}
		}
		if (CONTROLLER.TournamentStage == 1)
		{
			if (CONTROLLER.matchIndex < getQuaterFinalData.Length)
			{
				string[] array2 = getQuaterFinalData[CONTROLLER.matchIndex].Split("-"[0]);
				int num3 = int.Parse(array2[0]);
				int num4 = int.Parse(array2[1]);
				if (num3 == CONTROLLER.myTeamIndex)
				{
					abbrText[0].text = CONTROLLER.TeamList[num3].abbrevation;
					sideFlags[0].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[0].text);
					abbrText[1].text = CONTROLLER.TeamList[num4].abbrevation;
					sideFlags[1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[1].text);
				}
				else if (num4 == CONTROLLER.myTeamIndex)
				{
					abbrText[0].text = CONTROLLER.TeamList[num4].abbrevation;
					sideFlags[0].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[0].text);
					abbrText[1].text = CONTROLLER.TeamList[num3].abbrevation;
					sideFlags[1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[1].text);
				}
				else
				{
					saveData(num3, num4);
				}
			}
			else
			{
				CONTROLLER.TournamentStage = 2;
				CONTROLLER.matchIndex = 0;
			}
		}
		if (CONTROLLER.TournamentStage == 2)
		{
			if (CONTROLLER.matchIndex < getSemiFinalData.Length)
			{
				string[] array3 = getSemiFinalData[CONTROLLER.matchIndex].Split("-"[0]);
				int num5 = int.Parse(array3[0]);
				int num6 = int.Parse(array3[1]);
				if (num5 == CONTROLLER.myTeamIndex)
				{
					abbrText[0].text = CONTROLLER.TeamList[num5].abbrevation;
					sideFlags[0].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[0].text);
					abbrText[1].text = CONTROLLER.TeamList[num6].abbrevation;
					sideFlags[1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[1].text);
				}
				else if (num6 == CONTROLLER.myTeamIndex)
				{
					abbrText[0].text = CONTROLLER.TeamList[num6].abbrevation;
					sideFlags[0].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[0].text);
					abbrText[1].text = CONTROLLER.TeamList[num5].abbrevation;
					sideFlags[1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[1].text);
				}
				else
				{
					saveData(num5, num6);
				}
			}
			else
			{
				CONTROLLER.TournamentStage = 3;
				CONTROLLER.matchIndex = 0;
			}
		}
		if (CONTROLLER.TournamentStage != 3)
		{
			return;
		}
		if (CONTROLLER.matchIndex < getFinalData.Length)
		{
			string[] array4 = getFinalData[CONTROLLER.matchIndex].Split("-"[0]);
			int num7 = int.Parse(array4[0]);
			int num8 = int.Parse(array4[1]);
			if (num7 == CONTROLLER.myTeamIndex)
			{
				abbrText[0].text = CONTROLLER.TeamList[num7].abbrevation;
				sideFlags[0].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[0].text);
				abbrText[1].text = CONTROLLER.TeamList[num8].abbrevation;
				sideFlags[1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[1].text);
			}
			else if (num8 == CONTROLLER.myTeamIndex)
			{
				abbrText[0].text = CONTROLLER.TeamList[num8].abbrevation;
				sideFlags[0].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[0].text);
				abbrText[1].text = CONTROLLER.TeamList[num7].abbrevation;
				sideFlags[1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrText[1].text);
			}
		}
		else
		{
			CONTROLLER.TournamentStage = 4;
		}
	}

	private void GoNextPage()
	{
		Singleton<SquadPageTWO>.instance.showMe(1);
		hideMe();
	}

	private void saveData(int team1, int team2)
	{
		int num = AutoGenerate(team1, team2);
		string empty = string.Empty;
		string quaterFinalList;
		if (CONTROLLER.TournamentStage == 0)
		{
			string[] array = CONTROLLER.quaterFinalList.Split("|"[0]);
			empty = array[array.Length - 1];
			if (array.Length < quaterFinalTeams.Length / 2)
			{
				if (empty.Contains("-"))
				{
					quaterFinalList = CONTROLLER.quaterFinalList;
					CONTROLLER.quaterFinalList = quaterFinalList + string.Empty + num + "|";
				}
				else
				{
					quaterFinalList = CONTROLLER.quaterFinalList;
					CONTROLLER.quaterFinalList = quaterFinalList + string.Empty + num + "-";
				}
			}
			else if (empty.Contains("-"))
			{
				CONTROLLER.quaterFinalList = CONTROLLER.quaterFinalList + string.Empty + num;
			}
			else
			{
				quaterFinalList = CONTROLLER.quaterFinalList;
				CONTROLLER.quaterFinalList = quaterFinalList + string.Empty + num + "-";
			}
			setQuaterFinalMatches();
		}
		if (CONTROLLER.TournamentStage == 1)
		{
			string[] array = CONTROLLER.semiFinalList.Split("|"[0]);
			empty = array[array.Length - 1];
			if (array.Length < semiFinalTeams.Length / 2)
			{
				if (empty.Contains("-"))
				{
					quaterFinalList = CONTROLLER.semiFinalList;
					CONTROLLER.semiFinalList = quaterFinalList + string.Empty + num + "|";
				}
				else
				{
					quaterFinalList = CONTROLLER.semiFinalList;
					CONTROLLER.semiFinalList = quaterFinalList + string.Empty + num + "-";
				}
			}
			else if (empty.Contains("-"))
			{
				CONTROLLER.semiFinalList = CONTROLLER.semiFinalList + string.Empty + num;
			}
			else
			{
				quaterFinalList = CONTROLLER.semiFinalList;
				CONTROLLER.semiFinalList = quaterFinalList + string.Empty + num + "-";
			}
			setSemiFinalMatches();
		}
		if (CONTROLLER.TournamentStage == 2)
		{
			string[] array = CONTROLLER.finalList.Split("|"[0]);
			empty = array[array.Length - 1];
			if (array.Length < finalMatchTeams.Length / 2)
			{
				if (empty.Contains("-"))
				{
					quaterFinalList = CONTROLLER.finalList;
					CONTROLLER.finalList = quaterFinalList + string.Empty + num + "|";
				}
				else
				{
					quaterFinalList = CONTROLLER.finalList;
					CONTROLLER.finalList = quaterFinalList + string.Empty + num + "-";
				}
			}
			else if (empty.Contains("-"))
			{
				CONTROLLER.finalList = CONTROLLER.finalList + string.Empty + num;
			}
			else
			{
				quaterFinalList = CONTROLLER.finalList;
				CONTROLLER.finalList = quaterFinalList + string.Empty + num + "-";
			}
			setFinalMatches();
		}
		CONTROLLER.matchIndex++;
		if (CONTROLLER.TournamentStage == 0)
		{
			if (CONTROLLER.matchIndex >= leagueMatchData.Length)
			{
				CONTROLLER.TournamentStage = 1;
				CONTROLLER.matchIndex = 0;
			}
		}
		else if (CONTROLLER.TournamentStage == 1)
		{
			if (CONTROLLER.matchIndex >= getQuaterFinalData.Length)
			{
				CONTROLLER.TournamentStage = 2;
				CONTROLLER.matchIndex = 0;
			}
		}
		else if (CONTROLLER.TournamentStage == 2 && CONTROLLER.matchIndex >= getSemiFinalData.Length)
		{
			CONTROLLER.TournamentStage = 3;
			CONTROLLER.matchIndex = 0;
		}
		string text = CONTROLLER.TournamentStage + "&" + CONTROLLER.myTeamIndex + "&" + CONTROLLER.oversSelectedIndex + "&" + CONTROLLER.matchIndex + "&";
		for (int i = 0; i < leagueMatchData.Length; i++)
		{
			text = ((i >= leagueMatchData.Length - 1) ? (text + leagueMatchData[i]) : (text + leagueMatchData[i] + "|"));
		}
		quaterFinalList = text;
		SavePlayerPrefs.SetTournamentStage(CONTROLLER.tournamentStr = quaterFinalList + "&" + CONTROLLER.quaterFinalList + "&" + CONTROLLER.semiFinalList + "&" + CONTROLLER.finalList);
		CheckForTeam();
	}

	private int AutoGenerate(int team1, int team2)
	{
		int num = int.Parse(CONTROLLER.TeamList[team1].rank);
		int num2 = int.Parse(CONTROLLER.TeamList[team2].rank);
		int num3 = 0;
		int max = 36;
		int num4 = Mathf.Abs(num - num2);
		if (CONTROLLER.TournamentStage == 0)
		{
			num4 = ((num4 >= 4) ? (num4 + 14) : (num4 + 7));
		}
		else if (CONTROLLER.TournamentStage == 1)
		{
			num4 += 7;
		}
		else if (CONTROLLER.TournamentStage == 2)
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

	private void setLeagueMatches()
	{
		string[] array = CONTROLLER.tournamentStr.Split("&"[0]);
		string text = array[4];
		leagueMatchData = text.Split("|"[0]);
		int num = 0;
		for (num = 0; num < leagueMatchData.Length; num++)
		{
			string[] array2 = leagueMatchData[num].Split("-"[0]);
			int num2 = int.Parse(array2[0]);
			int num3 = int.Parse(array2[1]);
			string abbrevation = CONTROLLER.TeamList[num2].abbrevation;
			leagueTeams[num * 2].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
			abbrevation = CONTROLLER.TeamList[num3].abbrevation;
			leagueTeams[num * 2 + 1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
			teamName[num * 2].text = CONTROLLER.TeamList[num2].abbrevation;
			teamName[num * 2 + 1].text = CONTROLLER.TeamList[num3].abbrevation;
		}
	}

	private void setQuaterFinalMatches()
	{
		getQuaterFinalData = CONTROLLER.quaterFinalList.Split("|"[0]);
		for (int i = 0; i < getQuaterFinalData.Length; i++)
		{
			int num = -1;
			int num2 = -1;
			string[] array = getQuaterFinalData[i].Split("-"[0]);
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
				if (num != CONTROLLER.myTeamIndex)
				{
				}
			}
			if (num2 != -1)
			{
				string abbrevation2 = CONTROLLER.TeamList[num2].abbrevation;
				quaterFinalTeams[i * 2 + 1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation2);
				if (num2 != CONTROLLER.myTeamIndex)
				{
				}
			}
		}
	}

	private void setSemiFinalMatches()
	{
		getSemiFinalData = CONTROLLER.semiFinalList.Split("|"[0]);
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

	private void setFinalMatches()
	{
		getFinalData = CONTROLLER.finalList.Split("|"[0]);
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

	private void init()
	{
		int num = 0;
		float num2 = getGroupDetails();
		for (num = 0; num < quaterFinalTeams.Length; num++)
		{
			quaterFinalTeams[num].sprite = TBA;
		}
		for (num = 0; num < semiFinalTeams.Length; num++)
		{
			semiFinalTeams[num].sprite = TBA;
		}
		for (num = 0; num < finalMatchTeams.Length; num++)
		{
			finalMatchTeams[num].sprite = TBA;
		}
		getFinalData = CONTROLLER.finalList.Split("|"[0]);
		getSemiFinalData = CONTROLLER.semiFinalList.Split("|"[0]);
		getQuaterFinalData = CONTROLLER.quaterFinalList.Split("|"[0]);
		setLeagueMatches();
		setQuaterFinalMatches();
		setSemiFinalMatches();
		setFinalMatches();
		CheckForTeam();
	}

	private int getGroupDetails()
	{
		string[] array = CONTROLLER.tournamentStr.Split("&"[0]);
		string text = array[1];
		string text2 = array[4];
		string[] array2 = text2.Split("|"[0]);
		int num = 0;
		for (num = 0; num < array2.Length; num++)
		{
			string[] array3 = array2[num].Split("-"[0]);
			if (text == string.Empty + array3[0] || text == string.Empty + array3[1])
			{
				if (array3[2] == "A")
				{
					return 0;
				}
				if (array3[2] == "B")
				{
					return 1;
				}
			}
		}
		return -1;
	}

	public void MakePayment()
	{
		if (CONTROLLER.tickets < int.Parse(ticketAmount.text))
		{
			int num = Singleton<TemporaryStore>.instance.CalculateIndexValue(int.Parse(ticketAmount.text));
			if (Singleton<Store>.instance.CoinsPrice[num] <= CONTROLLER.Coins)
			{
				Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
				Singleton<NavigationBack>.instance.deviceBack = Singleton<TemporaryStore>.instance.CancelButton;
				Singleton<TemporaryStore>.instance.ShowMe(num);
				return;
			}
			Singleton<NavigationBack>.instance.tempDeviceBack = showMe;
			GameModeTWO.insuffStatus = "tickets";
			CONTROLLER.PopupName = "insuffPopup";
			Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(152) + "!";
			Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(124);
			Singleton<Popups>.instance.ShowMe();
		}
	}

	public void showMe()
	{
		availableTickets.text = CONTROLLER.tickets.ToString();
		Singleton<NavigationBack>.instance.deviceBack = Back;
		CONTROLLER.PlayModeSelected = 1;
		if (disableReset)
		{
			resetTournament.gameObject.SetActive(value: false);
			disableReset = false;
		}
		else
		{
			resetTournament.gameObject.SetActive(value: true);
		}
		ticketAmount.text = Singleton<PaymentProcess>.instance.GenerateAmount().ToString();
		CONTROLLER.menuTitle = "FIXTURES";
		Singleton<GameModeTWO>.instance.updateTitle(_modeSelected: true);
		Holder.SetActive(value: true);
		CONTROLLER.pageName = "T20Fixtures";
		Singleton<KnockOutPanelTransition>.instance.panelTransition();
		CONTROLLER.PlayModeSelected = 1;
		init();
		CONTROLLER.CurrentMenu = "fixtures";
		Text[] array = abbrText;
		foreach (Text text in array)
		{
			text.gameObject.SetActive(value: true);
		}
		PlaceSelectedImage();
	}

	public void Back()
	{
		hideMe();
		Singleton<GameModeTWO>.instance.OpenWorldCupModes();
	}

	public void hideMe()
	{
		Text[] array = abbrText;
		foreach (Text text in array)
		{
			text.gameObject.SetActive(value: false);
		}
		Holder.SetActive(value: false);
	}
}
