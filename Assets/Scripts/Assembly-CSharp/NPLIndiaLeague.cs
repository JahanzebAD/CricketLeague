using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class NPLIndiaLeague : Singleton<NPLIndiaLeague>
{
	private int ticketAmount;

	public GameObject scrollView;

	public GameObject content;

	public GameObject Playnow;

	public Text pageTitleText;

	public GameObject ContinueBtn;

	public NPLIndiaLeagueInfo[] matchInfo;

	private string[] NPLIndiaTournament;

	private int totalLeagueMatches = 9;

	public GameObject _uiScrollView;

	public GameObject holder;

	public Image TopPanelFlag;

	public Text TopPanelText;

	public Text PlayoffsText;

	public Text BottomPanelText;

	public GameObject[] matchDetailsGO;

	public GameObject footer;

	public Button resetTournament;

	public bool disableReset;

	private void Awake()
	{
		holder.SetActive(value: false);
	}

	protected void Start()
	{
	}

	private void ResetTournamentInfo()
	{
		if (CONTROLLER.tournamentType == "PAK")
		{
			totalLeagueMatches = 8;
		}
		else if (CONTROLLER.tournamentType == "AUS")
		{
			totalLeagueMatches = 7;
		}
		else if (CONTROLLER.tournamentType == "NPL")
		{
			totalLeagueMatches = 9;
		}
		int num = 0;
		for (num = 0; num < totalLeagueMatches; num++)
		{
			Debug.Log(matchInfo[num].matchNoText.text);
			Debug.Log(LocalizationData.instance.getText(454));
			matchInfo[num].matchNoText.text = LocalizationData.instance.getText(454) + " " + (num + 1);
			matchInfo[num].myTeamAbbrText.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation;
			string abbrevation = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation;
			matchInfo[num].myTeamFlag.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
			int teamID = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].MatchList[num].teamID;
			matchInfo[num].oppTeamAbbrText.text = CONTROLLER.TeamList[teamID].abbrevation;
			abbrevation = CONTROLLER.TeamList[teamID].abbrevation;
			matchInfo[num].oppTeamFlag.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
			matchInfo[num].matchResult.text = string.Empty;
			matchInfo[num].wonBy.text = string.Empty;
			matchInfo[num].playNowGO.SetActive(value: false);
			matchInfo[num].playedGameGO.SetActive(value: true);
			matchInfo[num].matchTieGO.SetActive(value: false);
			matchInfo[num].yetToPlayGO.SetActive(value: true);
		}
	}

	private void DisplaySeriesInfo()
	{
		checkForLeagueMatchCompletion();
		ResetTournamentInfo();
		string[] array = new string[CONTROLLER.NPLIndiaMyCurrentMatchIndex];
		array = CONTROLLER.NPLIndiaTeamWonIndexStr.Split("$"[0]);
		string[] array2 = new string[totalLeagueMatches];
		array2 = CONTROLLER.StoredNPLIndiaSeriesResult.Split("&"[0]);
		int num = 0;
		for (int i = 0; i < totalLeagueMatches; i++)
		{
			matchInfo[i].amount.SetActive(value: false);
			int teamID = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].MatchList[i].teamID;
			if (i < CONTROLLER.NPLIndiaMyCurrentMatchIndex)
			{
				matchInfo[i].amount.SetActive(value: false);
				int num2 = int.Parse(array[i]);
				matchInfo[i].playedGameGO.SetActive(value: true);
				if (num2 == -1)
				{
					matchInfo[i].matchTieGO.SetActive(value: true);
				}
				else
				{
					matchInfo[i].matchTieGO.SetActive(value: false);
					matchInfo[i].matchResult.text = array2[i].Substring(0, 7);
					matchInfo[i].wonBy.text = array2[i].Substring(7);
				}
				matchInfo[i].yetToPlayGO.SetActive(value: false);
			}
			else if (i == CONTROLLER.NPLIndiaMyCurrentMatchIndex)
			{
				CONTROLLER.opponentTeamIndex = teamID;
				matchInfo[i].playedGameGO.SetActive(value: false);
				matchInfo[i].playNowGO.SetActive(value: true);
				string empty = string.Empty;
				empty = AutoSave.ReadFile();
				if (empty == string.Empty)
				{
					matchInfo[i].playNowGO.GetComponentInChildren<Text>().text = LocalizationData.instance.getText(413);
				}
				else
				{
					matchInfo[i].playNowGO.GetComponentInChildren<Text>().text = "CONTINUE";
				}
				matchInfo[i].amount.SetActive(value: true);
				matchInfo[i].ticketAmount.text = ticketAmount.ToString();
				teamID = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].MatchList[i].teamID;
				BottomPanelText.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].teamName.ToUpper() + " VS " + CONTROLLER.TeamList[teamID].teamName.ToUpper();
			}
		}
		num = CONTROLLER.NPLIndiaMyCurrentMatchIndex;
		if (num < totalLeagueMatches && matchInfo[num].playNowGO.activeSelf && num > 1)
		{
			if (num > totalLeagueMatches - 2)
			{
				num = totalLeagueMatches - 2;
			}
			SnapTo(matchInfo[num].GetComponent<RectTransform>());
		}
	}

	private void checkForLeagueMatchCompletion()
	{
		if (CONTROLLER.NPLIndiaLeagueMatchIndex < CONTROLLER.NPLIndiaMatchDetails.Length && Singleton<LoadPlayerPrefs>.instance.getNPLMatches(CONTROLLER.NPLIndiaMatchDetails, CONTROLLER.NPLIndiaLeagueMatchIndex))
		{
			checkForLeagueMatchCompletion();
		}
	}

	public void onClick_PointsTableBtn()
	{
		HideMe();
	}

	public void PlayMatch()
	{
		string empty = string.Empty;
		empty = AutoSave.ReadFile();
		if (empty == string.Empty)
		{
			if (CONTROLLER.tickets >= ticketAmount)
			{
				CONTROLLER.GameStartsFromSave = false;
				CONTROLLER.isFreeHitBall = false;
				AutoSave.DeleteFile();
				Singleton<SquadPageTWO>.instance.showMe(1);
				HideMe();
				return;
			}
			int num = Singleton<TemporaryStore>.instance.CalculateIndexValue(ticketAmount);
			if (Singleton<Store>.instance.CoinsPrice[num] <= CONTROLLER.Coins)
			{
				Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
				Singleton<NavigationBack>.instance.deviceBack = Singleton<TemporaryStore>.instance.CancelButton;
				Singleton<TemporaryStore>.instance.ShowMe(num);
				return;
			}
			Singleton<NavigationBack>.instance.tempDeviceBack = ShowMe;
			GameModeTWO.insuffStatus = "tickets";
			CONTROLLER.PopupName = "insuffPopup";
			Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(152) + "!";
			Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(124);
			Singleton<Popups>.instance.ShowMe();
		}
		else
		{
			CONTROLLER.GameStartsFromSave = true;
			Singleton<LoadingPanelTransition>.instance.PanelTransition();
			HideMe();
			AutoSave.LoadGame();
			Singleton<NavigationBack>.instance.deviceBack = null;
			Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
		}
	}

	public void ResetTournament()
	{
		Singleton<IncompleteMatchTWO>.instance.showMe();
	}

	public void DeleteLeagueInfo()
	{
		CONTROLLER.NplIndiaData = string.Empty;
		CONTROLLER.NPLIndiaMyCurrentMatchIndex = 0;
		CONTROLLER.NPLIndiaTeamWonIndexStr = string.Empty;
		CONTROLLER.StoredNPLIndiaSeriesResult = string.Empty;
		setQualifierTeams();
		DeletePlayerPrefs();
		if (CONTROLLER.tournamentType == "NPL")
		{
			if (ObscuredPrefs.HasKey("NPLIndiaPlayOff"))
			{
				CONTROLLER.NplIndiaData = ObscuredPrefs.GetString("NPLIndiaPlayOff");
				string[] array = CONTROLLER.NplIndiaData.Split("&"[0]);
				CONTROLLER.NPLIndiaTournamentStage = int.Parse(array[0]);
				CONTROLLER.myTeamIndex = int.Parse(array[1]);
				CONTROLLER.NPLIndiaMyCurrentMatchIndex = int.Parse(array[2]);
			}
		}
		else if (CONTROLLER.tournamentType == "PAK")
		{
			if (ObscuredPrefs.HasKey("NPLPakistanPlayOff"))
			{
				CONTROLLER.NplIndiaData = ObscuredPrefs.GetString("NPLPakistanPlayOff");
				string[] array2 = CONTROLLER.NplIndiaData.Split("&"[0]);
				CONTROLLER.NPLIndiaTournamentStage = int.Parse(array2[0]);
				CONTROLLER.myTeamIndex = int.Parse(array2[1]);
				CONTROLLER.NPLIndiaMyCurrentMatchIndex = int.Parse(array2[2]);
			}
		}
		else if (CONTROLLER.tournamentType == "AUS" && ObscuredPrefs.HasKey("NplAustraliaPlayoff"))
		{
			CONTROLLER.NplIndiaData = ObscuredPrefs.GetString("NplAustraliaPlayoff");
			string[] array3 = CONTROLLER.NplIndiaData.Split("&"[0]);
			CONTROLLER.NPLIndiaTournamentStage = int.Parse(array3[0]);
			CONTROLLER.myTeamIndex = int.Parse(array3[1]);
			CONTROLLER.NPLIndiaMyCurrentMatchIndex = int.Parse(array3[2]);
		}
		HideMe();
	}

	private void setQualifierTeams()
	{
		string[] array = new string[4];
		CONTROLLER.NPLIndiaSortedPointsTable = Singleton<LoadPlayerPrefs>.instance.SortWCPointsTable(CONTROLLER.NPLIndiaPointsTable, CONTROLLER.NPLIndiaSortedPointsTable);
		int num = 4;
		for (int i = 0; i < num; i++)
		{
			array[i] = string.Empty + CONTROLLER.NPLIndiaSortedPointsTable[i];
		}
		if (CONTROLLER.tournamentType == "NPL")
		{
			if (ObscuredPrefs.HasKey("NPLIndiaPointsTable"))
			{
				ObscuredPrefs.DeleteKey("NPLIndiaPointsTable");
			}
		}
		else if (CONTROLLER.tournamentType == "PAK")
		{
			if (ObscuredPrefs.HasKey("NPLPakistanPointsTable"))
			{
				ObscuredPrefs.DeleteKey("NPLPakistanPointsTable");
			}
		}
		else if (CONTROLLER.tournamentType == "AUS" && ObscuredPrefs.HasKey("NPLAustraliaPointsTable"))
		{
			ObscuredPrefs.DeleteKey("NPLAustraliaPointsTable");
		}
		CONTROLLER.NPLIndiaTournamentStage = 1;
		CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
		string text = CONTROLLER.NPLIndiaTournamentStage + "&";
		string text2 = text;
		text = text2 + CONTROLLER.myTeamIndex + "&" + CONTROLLER.NPLIndiaLeagueMatchIndex + "&";
		for (int j = 0; j < array.Length / 2; j++)
		{
			if (j < array.Length / 2 - 1)
			{
				num = 0;
				text2 = text;
				text = text2 + string.Empty + array[num] + "-" + array[num + 1] + "|";
			}
			else
			{
				num = 2;
				text2 = text;
				text = text2 + string.Empty + array[num] + "-" + array[num + 1] + string.Empty;
			}
		}
		text = (CONTROLLER.NplIndiaData = text + "##");
		if (CONTROLLER.tournamentType == "NPL")
		{
			ObscuredPrefs.SetString("NPLIndiaPlayOff", CONTROLLER.NplIndiaData);
		}
		else if (CONTROLLER.tournamentType == "PAK")
		{
			ObscuredPrefs.SetString("NPLPakistanPlayOff", CONTROLLER.NplIndiaData);
		}
		else if (CONTROLLER.tournamentType == "AUS")
		{
			ObscuredPrefs.SetString("NplAustraliaPlayoff", CONTROLLER.NplIndiaData);
		}
	}

	public void DeletePlayerPrefs()
	{
		if (CONTROLLER.tournamentType == "NPL")
		{
			if (ObscuredPrefs.HasKey("NPLIndiaLeague"))
			{
				ObscuredPrefs.DeleteKey("NPLIndiaLeague");
			}
			if (ObscuredPrefs.HasKey("NPLIndiaLeagueMatchIndex"))
			{
				ObscuredPrefs.DeleteKey("NPLIndiaLeagueMatchIndex");
			}
		}
		else if (CONTROLLER.tournamentType == "PAK")
		{
			if (ObscuredPrefs.HasKey("NPLPakistanLeague"))
			{
				ObscuredPrefs.DeleteKey("NPLPakistanLeague");
			}
			if (ObscuredPrefs.HasKey("NPLPakistanLeagueMatchIndex"))
			{
				ObscuredPrefs.DeleteKey("NPLPakistanLeagueMatchIndex");
			}
		}
		else if (CONTROLLER.tournamentType == "AUS")
		{
			if (ObscuredPrefs.HasKey("NplAUS"))
			{
				ObscuredPrefs.DeleteKey("NplAUS");
			}
			if (ObscuredPrefs.HasKey("NPLAustraliaLeagueMatchIndex"))
			{
				ObscuredPrefs.DeleteKey("NPLAustraliaLeagueMatchIndex");
			}
		}
	}

	public void BackSelected()
	{
		HideMe();
		CONTROLLER.menuTitle = string.Empty;
		Singleton<GameModeTWO>.instance.OpenPremierLeagueModes();
	}

	public void SnapTo(RectTransform target)
	{
		Canvas.ForceUpdateCanvases();
		content.GetComponent<RectTransform>().anchoredPosition = (Vector2)scrollView.GetComponent<ScrollRect>().transform.InverseTransformPoint(content.GetComponent<RectTransform>().position) - (Vector2)scrollView.GetComponent<ScrollRect>().transform.InverseTransformPoint(target.position);
		content.transform.localPosition = new Vector3(content.transform.localPosition.x, 35f, content.transform.localPosition.z);
	}

	public void ShowMe()
	{
		Singleton<NplGroupMatchesTWOPanelTransistion>.instance.Content.localPosition = new Vector3(563f, 42f, 0f);
		CONTROLLER.PlayModeSelected = 2;
		Singleton<NavigationBack>.instance.deviceBack = BackSelected;
		CONTROLLER.pageName = "nplleague";
		Singleton<LoadPlayerPrefs>.instance.GetNPLIndiaTournamentList();
		if (CONTROLLER.tournamentType == "PAK")
		{
			totalLeagueMatches = 8;
		}
		else if (CONTROLLER.tournamentType == "AUS")
		{
			totalLeagueMatches = 7;
		}
		else if (CONTROLLER.tournamentType == "NPL")
		{
			totalLeagueMatches = 9;
		}
		if (CONTROLLER.NPLIndiaMyCurrentMatchIndex >= totalLeagueMatches)
		{
			ContinueBtn.SetActive(value: true);
			PlayoffsText.gameObject.SetActive(value: true);
			Playnow.SetActive(value: false);
			BottomPanelText.gameObject.SetActive(value: false);
		}
		else
		{
			ContinueBtn.SetActive(value: false);
			PlayoffsText.gameObject.SetActive(value: false);
			Playnow.SetActive(value: true);
			BottomPanelText.gameObject.SetActive(value: true);
		}
		if (CONTROLLER.tournamentType == "PAK")
		{
			pageTitleText.text = "PAKISTAN LEAGUE - LEAGUE MATCHES";
			NPLIndiaTournament = CONTROLLER.NplPakistanSchedule.Split("&"[0]);
		}
		else if (CONTROLLER.tournamentType == "NPL")
		{
			pageTitleText.text = "NATIONAL PREMIER LEAGUE - INDIA";
			NPLIndiaTournament = CONTROLLER.NPLIndiaSchedule.Split("&"[0]);
		}
		else if (CONTROLLER.tournamentType == "AUS")
		{
			pageTitleText.text = "GREAT AUZI BASH - LEAGUE MATCHES";
			NPLIndiaTournament = CONTROLLER.NPLAustraliaSchedule.Split("&"[0]);
		}
		CONTROLLER.NPLIndiaMatchDetails = NPLIndiaTournament[1].Split("|"[0]);
		ticketAmount = Singleton<PaymentProcess>.instance.GenerateAmount();
		DisplaySeriesInfo();
		resetTournament.interactable = true;
		if (CONTROLLER.NPLIndiaMyCurrentMatchIndex >= totalLeagueMatches)
		{
			ContinueBtn.SetActive(value: true);
		}
		else
		{
			ContinueBtn.SetActive(value: false);
		}
		Singleton<GameModeTWO>.instance.hideMe();
		string abbrevation = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation;
		TopPanelFlag.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
		holder.SetActive(value: true);
		TopPanelText.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].teamName.ToUpper();
		for (int i = 0; i < matchDetailsGO.Length; i++)
		{
			if (i < totalLeagueMatches)
			{
				matchDetailsGO[i].SetActive(value: true);
			}
			else
			{
				matchDetailsGO[i].SetActive(value: false);
			}
		}
		CONTROLLER.CurrentMenu = "nplindialeaguepage";
		base.gameObject.SetActive(value: true);
	}

	public void Continue()
	{
		if (CONTROLLER.NPLIndiaMyCurrentMatchIndex >= totalLeagueMatches)
		{
			HideMe();
			DeleteLeagueInfo();
			Singleton<NPLIndiaPlayOff>.instance.ShowMe();
			return;
		}
		if (CONTROLLER.tickets >= Singleton<PaymentProcess>.instance.GenerateAmount())
		{
			HideMe();
			PlayMatch();
			return;
		}
		Singleton<NavigationBack>.instance.tempDeviceBack = ShowMe;
		CONTROLLER.PopupName = "insuffPopup";
		GameModeTWO.insuffStatus = "tickets";
		Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(152) + "!";
		Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(124);
		Singleton<Popups>.instance.ShowMe();
	}

	public void HideMe()
	{
		ContinueBtn.SetActive(value: true);
		CONTROLLER.PlayModeSelected = 2;
		holder.SetActive(value: false);
	}
}
