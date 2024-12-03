using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class WorldCupLeague : Singleton<WorldCupLeague>
{
	public GameObject holder;

	public GameObject PlayNow;

	public Text pageTitleText;

	public Text PlayoffsText;

	private int ticketAmount;

	public GameObject ContinueBtn;

	public Image TopPanelFlag;

	public Text TopPanelText;

	public Text BottomPanelText;

	public WorldCupLeagueInfo[] matchInfo;

	private string[] WCTournament;

	private int totalLeagueMatches = 7;

	public GameObject _uiScrollView;

	private Vector3 startPos;

	public Button resetWorldCupBtn;

	public bool disableReset;

	public void Awake()
	{
		HideMe();
	}

	protected void Start()
	{
		totalLeagueMatches = 7;
		startPos = _uiScrollView.transform.position;
	}

	private void ResetTournamentInfo()
	{
		int num = 0;
		pageTitleText.text = LocalizationData.instance.getText(574);
		for (num = 0; num < totalLeagueMatches; num++)
		{
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
		completeWCTournament();
		ResetTournamentInfo();
		string[] array = new string[CONTROLLER.WCMyCurrentMatchIndex];
		array = CONTROLLER.WCTeamWonIndexStr.Split("$"[0]);
		string[] array2 = new string[totalLeagueMatches];
		array2 = CONTROLLER.StoredWCTournamentResult.Split("&"[0]);
		for (int i = 0; i < totalLeagueMatches; i++)
		{
			matchInfo[i].amount.SetActive(value: false);
			int teamID = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].MatchList[i].teamID;
			if (i < CONTROLLER.WCMyCurrentMatchIndex)
			{
				matchInfo[i].amount.SetActive(value: false);
				int num = int.Parse(array[i]);
				matchInfo[i].playedGameGO.SetActive(value: true);
				if (num == -1)
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
			else if (i == CONTROLLER.WCMyCurrentMatchIndex)
			{
				CONTROLLER.opponentTeamIndex = teamID;
				matchInfo[i].playedGameGO.SetActive(value: false);
				string empty = string.Empty;
				empty = AutoSave.ReadFile();
				if (empty == string.Empty)
				{
					matchInfo[i].playNowGO.GetComponentInChildren<Text>().text = LocalizationData.instance.getText(413);
				}
				else
				{
					matchInfo[i].playNowGO.GetComponentInChildren<Text>().text = LocalizationData.instance.getText(558);
				}
				matchInfo[i].amount.SetActive(value: true);
				matchInfo[i].ticketAmount.text = ticketAmount.ToString();
				matchInfo[i].playNowGO.SetActive(value: true);
				teamID = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].MatchList[i].teamID;
				BottomPanelText.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].teamName.ToUpper() + " VS " + CONTROLLER.TeamList[teamID].teamName.ToUpper();
			}
		}
	}

	public void MakePayment()
	{
		if (CONTROLLER.tickets >= ticketAmount)
		{
			DisplaySeriesInfo();
			return;
		}
		Singleton<NavigationBack>.instance.tempDeviceBack = ShowMe;
		CONTROLLER.PopupName = "insuffPopup";
		GameModeTWO.insuffStatus = "tickets";
		Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(152) + "!";
		Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(124);
		Singleton<Popups>.instance.ShowMe();
	}

	private void completeWCTournament()
	{
		if (CONTROLLER.WCLeagueMatchIndex < CONTROLLER.WCMatchDetails.Length && Singleton<LoadPlayerPrefs>.instance.getWCMatch())
		{
			completeWCTournament();
		}
	}

	public void onClick_PointsTableBtn()
	{
		HideMe();
	}

	public void ResetTournament()
	{
		Singleton<IncompleteMatchTWO>.instance.showMe();
	}

	private void CloseTempStore()
	{
		Singleton<TemporaryStore>.instance.Holder.SetActive(value: false);
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
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
			GameModeTWO.insuffStatus = "tickets";
			int num = Singleton<TemporaryStore>.instance.CalculateIndexValue(ticketAmount);
			if (Singleton<Store>.instance.CoinsPrice[num] <= CONTROLLER.Coins)
			{
				Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
				Singleton<NavigationBack>.instance.deviceBack = CloseTempStore;
				Singleton<TemporaryStore>.instance.ShowMe(num);
			}
			else
			{
				Singleton<NavigationBack>.instance.tempDeviceBack = ShowMe;
				CONTROLLER.PopupName = "insuffPopup";
				Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(152) + "!";
				Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(124);
				Singleton<Popups>.instance.ShowMe();
			}
		}
		else
		{
			Singleton<NavigationBack>.instance.deviceBack = null;
			Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
			CONTROLLER.GameStartsFromSave = true;
			AutoSave.LoadGame();
			HideMe();
		}
	}

	public void DeleteLeagueInfo()
	{
		CONTROLLER.WCLeagueData = string.Empty;
		CONTROLLER.WCMyCurrentMatchIndex = 0;
		CONTROLLER.WCTeamWonIndexStr = string.Empty;
		CONTROLLER.StoredWCTournamentResult = string.Empty;
		SetQuarterFinalTeams();
		DeletePlayerPrefs();
		if (ObscuredPrefs.HasKey("wcplayoff"))
		{
			CONTROLLER.WCLeagueData = ObscuredPrefs.GetString("wcplayoff");
			string[] array = CONTROLLER.WCLeagueData.Split("&"[0]);
			CONTROLLER.WCTournamentStage = int.Parse(array[0]);
			CONTROLLER.myTeamIndex = int.Parse(array[1]);
			CONTROLLER.WCMyCurrentMatchIndex = int.Parse(array[2]);
		}
		HideMe();
	}

	private void DeletePlayerPrefs()
	{
		if (ObscuredPrefs.HasKey("worldcup"))
		{
			ObscuredPrefs.DeleteKey("worldcup");
		}
		if (ObscuredPrefs.HasKey("WCLeagueMatchIndex"))
		{
			ObscuredPrefs.DeleteKey("WCLeagueMatchIndex");
		}
	}

	private void SetQuarterFinalTeams()
	{
		string[] array = new string[4];
		string[] array2 = new string[4];
		List<int> list = new List<int>();
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < CONTROLLER.WCSortedPointsTable.Length; i++)
		{
			if (CONTROLLER.WCGroupDetails[CONTROLLER.WCSortedPointsTable[i]] == "A")
			{
				if (num <= 3)
				{
					array[num] = string.Empty + CONTROLLER.WCSortedPointsTable[i];
					num++;
				}
			}
			else if (CONTROLLER.WCGroupDetails[CONTROLLER.WCSortedPointsTable[i]] == "B" && num2 <= 3)
			{
				array2[num2] = string.Empty + CONTROLLER.WCSortedPointsTable[i];
				list.Add(CONTROLLER.WCSortedPointsTable[i]);
				num2++;
			}
		}
		if (ObscuredPrefs.HasKey("WCPointsTable"))
		{
			ObscuredPrefs.DeleteKey("WCPointsTable");
		}
		CONTROLLER.WCTournamentStage = 1;
		CONTROLLER.WCLeagueMatchIndex = 0;
		string text = CONTROLLER.WCTournamentStage + "&";
		string text2 = text;
		text = text2 + CONTROLLER.myTeamIndex + "&" + CONTROLLER.WCLeagueMatchIndex + "&";
		for (int j = 0; j < array.Length; j++)
		{
			int index = list.Count - 1;
			int num3 = list[index];
			list.RemoveAt(index);
			if (j < array.Length - 1)
			{
				text2 = text;
				text = text2 + string.Empty + array[j] + "-" + num3 + "|";
			}
			else
			{
				text2 = text;
				text = text2 + string.Empty + array[j] + "-" + num3 + string.Empty;
			}
		}
		text = (CONTROLLER.WCLeagueData = text + "##");
		ObscuredPrefs.SetString("wcplayoff", CONTROLLER.WCLeagueData);
	}

	public void BackSelected()
	{
		HideMe();
		CONTROLLER.menuTitle = string.Empty;
		Singleton<GameModeTWO>.instance.OpenWorldCupModes();
	}

	public void Continue()
	{
		HideMe();
		if (CONTROLLER.WCMyCurrentMatchIndex >= totalLeagueMatches)
		{
			DeleteLeagueInfo();
			Singleton<WorldCupPlayOff>.instance.ShowMe();
		}
		else
		{
			PlayMatch();
		}
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = BackSelected;
		if (!ObscuredPrefs.HasKey("WCPaid"))
		{
			ObscuredPrefs.SetInt("WCPaid", 0);
		}
		if (CONTROLLER.WCMyCurrentMatchIndex >= totalLeagueMatches)
		{
			ContinueBtn.SetActive(value: true);
			PlayNow.SetActive(value: false);
			BottomPanelText.gameObject.SetActive(value: false);
			PlayoffsText.gameObject.SetActive(value: true);
		}
		else
		{
			ContinueBtn.SetActive(value: false);
			PlayNow.SetActive(value: true);
			BottomPanelText.gameObject.SetActive(value: true);
			PlayoffsText.gameObject.SetActive(value: false);
		}
		CONTROLLER.pageName = "WCLeague";
		if (disableReset)
		{
			resetWorldCupBtn.interactable = true;
			disableReset = false;
		}
		else
		{
			resetWorldCupBtn.interactable = true;
		}
		Singleton<LoadPlayerPrefs>.instance.GetWCTournamentList();
		CONTROLLER.CurrentMenu = "wcleaguepage";
		CONTROLLER.PlayModeSelected = 3;
		Singleton<GameModeTWO>.instance.hideMe();
		string abbrevation = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation;
		TopPanelFlag.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
		holder.SetActive(value: true);
		TopPanelText.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].teamName.ToUpper();
		WCTournament = CONTROLLER.WCSchedule.Split("&"[0]);
		CONTROLLER.WCMatchDetails = WCTournament[1].Split("|"[0]);
		CONTROLLER.WCSortedPointsTable = Singleton<LoadPlayerPrefs>.instance.SortWCPointsTable(CONTROLLER.WCPointsTable, CONTROLLER.WCSortedPointsTable);
		ticketAmount = Singleton<PaymentProcess>.instance.GenerateAmount();
		DisplaySeriesInfo();
		ResetScroll();
	}

	public void HideMe()
	{
		holder.SetActive(value: false);
	}

	private void ResetScroll()
	{
		_uiScrollView.transform.position = startPos;
	}
}
