using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class SquadPageTWO : Singleton<SquadPageTWO>
{
	public GameObject Holder;

	public GameObject team2Panel;

	public GameObject entryFeePopup;

	public Image WarningToss;

	public Text MyTeamName;

	public Text OppTeamName;

	public Image MyTeamFlag;

	public Image OppTeamFlag;

	public GameObject loadingText;

	public Text[] team1playerName;

	public Text[] team2playerName;

	public Image[] team1playerType;

	public Image[] team2playerType;

	public Sprite[] PlayerTypeImage;

	public bool BackKeyEnable;

	private string teamToEdit = "myteam";

	private string[] paidKey = new string[8]
	{
		"QPPaid",
		"T20Paid",
		"NPLPaid",
		"WCPaid",
		string.Empty,
		string.Empty,
		string.Empty,
		"TMOvers"
	};

	protected void Start()
	{
		hideMe();
	}

	public void showMe(int teamIndex)
	{
		Singleton<NavigationBack>.instance.deviceBack = Back;
		if (CONTROLLER.PlayModeSelected > 3 && CONTROLLER.PlayModeSelected != 7)
		{
			team2Panel.SetActive(value: false);
		}
		else
		{
			team2Panel.SetActive(value: true);
		}
		CONTROLLER.pageName = "squads";
		CONTROLLER.menuTitle = "PLAYER LIST";
		Singleton<GameModeTWO>.instance.updateTitle(_modeSelected: true);
		SetSquadPage();
		Holder.SetActive(value: true);
		Singleton<PlayerNamePanelTransition>.instance.panelTransition();
		BackKeyEnable = true;
	}

	public void hideMe()
	{
		BackKeyEnable = false;
		Holder.SetActive(value: false);
	}

	private void SetSquadPage()
	{
		teamToEdit = "myteam";
		if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation.ToUpper() == "UAE")
		{
			MyTeamName.text = LocalizationData.instance.getText(201);
		}
		else
		{
			MyTeamName.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].teamName.ToUpper();
			int index = LocalizationData.instance.refList.IndexOf(MyTeamName.text.ToUpper());
			MyTeamName.text = LocalizationData.instance.getText(index);
		}
		if (CONTROLLER.PlayModeSelected < 4 || CONTROLLER.PlayModeSelected == 7)
		{
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation.ToUpper() == "UAE")
			{
				OppTeamName.text = LocalizationData.instance.getText(201);
			}
			else
			{
				OppTeamName.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].teamName.ToUpper();
				int index = LocalizationData.instance.refList.IndexOf(OppTeamName.text.ToUpper());
				OppTeamName.text = LocalizationData.instance.getText(index);
			}
		}
		string abbrevation = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation;
		MyTeamFlag.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
		if (CONTROLLER.PlayModeSelected < 4 || CONTROLLER.PlayModeSelected == 7)
		{
			abbrevation = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation;
			OppTeamFlag.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
		}
		for (int i = 0; i < team1playerName.Length; i++)
		{
			string playerName = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].PlayerName;
			if (playerName.Length > 12)
			{
				playerName = playerName.Substring(0, 10);
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].PlayerName = playerName;
			}
			team1playerName[i].text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].PlayerName.ToUpper();
			if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].isCaptain)
			{
				team1playerName[i].text += " (C)";
			}
			if (i <= 6)
			{
				if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.bowlingRank != string.Empty)
				{
					team1playerType[i].sprite = PlayerTypeImage[2];
				}
				else
				{
					team1playerType[i].sprite = PlayerTypeImage[0];
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].BowlerList.bowlingRank != string.Empty)
			{
				team1playerType[i].sprite = PlayerTypeImage[1];
			}
			else
			{
				team1playerType[i].sprite = PlayerTypeImage[0];
			}
			if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[i].isKeeper)
			{
				team1playerType[i].sprite = PlayerTypeImage[3];
			}
		}
		if (CONTROLLER.PlayModeSelected >= 4 && CONTROLLER.PlayModeSelected != 7)
		{
			return;
		}
		for (int i = 0; i < team2playerName.Length; i++)
		{
			string playerName2 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].PlayerName;
			if (playerName2.Length > 12)
			{
				playerName2 = playerName2.Substring(0, 10);
				CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].PlayerName = playerName2;
			}
			team2playerName[i].text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].PlayerName.ToUpper();
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].isCaptain)
			{
				team2playerName[i].text += " (C)";
			}
			if (i <= 6)
			{
				if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BowlerList.bowlingRank != string.Empty)
				{
					team2playerType[i].sprite = PlayerTypeImage[2];
				}
				else
				{
					team2playerType[i].sprite = PlayerTypeImage[0];
				}
				if (!(CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BowlerList.bowlingRank != string.Empty))
				{
				}
			}
			else if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].BowlerList.bowlingRank != string.Empty)
			{
				team2playerType[i].sprite = PlayerTypeImage[1];
			}
			else
			{
				team2playerType[i].sprite = PlayerTypeImage[0];
			}
			if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[i].isKeeper)
			{
				team2playerType[i].sprite = PlayerTypeImage[3];
			}
		}
	}

	public void OnClickContinueButton()
	{
		if (CONTROLLER.PlayModeSelected < 4 && CONTROLLER.PlayModeSelected != 0)
		{
			if ((CONTROLLER.PlayModeSelected == 2 && CONTROLLER.NPLIndiaTournamentStage > 0) || (CONTROLLER.PlayModeSelected == 3 && CONTROLLER.WCTournamentStage > 0))
			{
				Continue();
			}
			else
			{
				Singleton<EntryFeeConfirmation>.instance.ShowMe();
			}
		}
		else if (CONTROLLER.PlayModeSelected == 7 || CONTROLLER.PlayModeSelected == 0)
		{
			Continue();
		}
		else if (CONTROLLER.PlayModeSelected == 4)
		{
			Singleton<EntryFeeConfirmation>.instance.ShowMe();
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			Singleton<EntryFeeConfirmation>.instance.ShowMe();
		}
	}

	private void MakePayment(int index)
	{
		PaymentDetails.InitPaymentValues();
		int num = Singleton<PaymentProcess>.instance.GenerateAmount();
		SavePlayerPrefs.SaveUserTickets(-num, num, 0);
		ObscuredPrefs.SetInt(paidKey[index], 1);
		if (CONTROLLER.PlayModeSelected != 0 && CONTROLLER.PlayModeSelected < 4)
		{
			ObscuredPrefs.SetInt(index + "Refund", num);
		}
	}

	public void Continue()
	{
		if (CONTROLLER.PlayModeSelected < 4 && CONTROLLER.PlayModeSelected != 0)
		{
			if ((CONTROLLER.PlayModeSelected != 2 || CONTROLLER.NPLIndiaTournamentStage <= 0) && (CONTROLLER.PlayModeSelected != 3 || CONTROLLER.WCTournamentStage <= 0))
			{
				MakePayment(CONTROLLER.PlayModeSelected);
			}
		}
		else if (CONTROLLER.PlayModeSelected == 7)
		{
			PaymentDetails.InitPaymentValues();
			int num = Singleton<PaymentProcess>.instance.GenerateAmount();
			SavePlayerPrefs.SaveUserTickets(-num, num, 0);
			ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "Refund", num);
		}
		CONTROLLER.GameStartsFromSave = false;
		SendToFirebase();
		ConfigureGameBeforeToss();
	}

	public void CloseEntryFeePopup()
	{
		Singleton<EntryFeeConfirmation>.instance.HideMe();
		CONTROLLER.pageName = "squads";
	}

	private void SendToFirebase()
	{
		//if (CONTROLLER.PlayModeSelected == 1)
		//{
		//	Singleton<Firebase_Events>.instance.Firebase_T20_MatchPro();
		//}
		//else if (CONTROLLER.PlayModeSelected == 3)
		//{
		//	Singleton<Firebase_Events>.instance.Firebase_WC_MatchPro();
		//}
		//else if (CONTROLLER.PlayModeSelected == 2)
		//{
		//	Singleton<Firebase_Events>.instance.Firebase_PRL_MatchPro();
		//}
		//else if (CONTROLLER.PlayModeSelected == 7)
		//{
		//	Singleton<Firebase_Events>.instance.Firebase_TC_MatchPro();
		//}
	}

	public void ValidateTeam()
	{
		int num = 0;
		int captainIndex = 0;
		int num2 = 0;
		for (num = 0; num < team1playerName.Length; num++)
		{
			if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[num].isCaptain)
			{
				captainIndex = num;
				num2++;
				if (num2 >= 2)
				{
					ShowError(1);
					return;
				}
			}
		}
		if (num2 == 0)
		{
			ShowError(2);
			return;
		}
		CONTROLLER.TeamList[CONTROLLER.myTeamIndex].CaptainIndex = captainIndex;
		if (CONTROLLER.PlayModeSelected < 4 || CONTROLLER.PlayModeSelected == 7)
		{
			num2 = 0;
			for (num = 0; num < team2playerName.Length; num++)
			{
				if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[num].isCaptain)
				{
					captainIndex = num;
					num2++;
					if (num2 >= 2)
					{
						ShowError(3);
						return;
					}
				}
			}
			if (num2 == 0)
			{
				ShowError(4);
				return;
			}
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].CaptainIndex = captainIndex;
		}
		int keeperIndex = 0;
		int num3 = 0;
		for (num = 0; num < team1playerName.Length; num++)
		{
			if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[num].isKeeper)
			{
				keeperIndex = num;
				num3++;
				if (num3 >= 2)
				{
					ShowError(5);
					return;
				}
			}
		}
		if (num3 == 0)
		{
			ShowError(6);
			return;
		}
		CONTROLLER.TeamList[CONTROLLER.myTeamIndex].KeeperIndex = keeperIndex;
		if (CONTROLLER.PlayModeSelected < 4 || CONTROLLER.PlayModeSelected == 7)
		{
			num3 = 0;
			for (num = 0; num < team2playerName.Length; num++)
			{
				if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[num].isKeeper)
				{
					keeperIndex = num;
					num3++;
					if (num3 >= 2)
					{
						ShowError(7);
						return;
					}
				}
			}
			if (num3 == 0)
			{
				ShowError(8);
				return;
			}
			CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].KeeperIndex = keeperIndex;
		}
		int num4 = 0;
		for (num = 0; num < team1playerName.Length; num++)
		{
			if (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[num].BowlerList.bowlingRank != string.Empty)
			{
				num4++;
				CONTROLLER.TeamList[CONTROLLER.myTeamIndex].PlayerList[num].BowlerList.bowlingOrder = num4;
			}
		}
		if (num4 < 5 || num4 > 7)
		{
			ShowError(9);
			return;
		}
		if (CONTROLLER.PlayModeSelected < 4 || CONTROLLER.PlayModeSelected == 7)
		{
			num4 = 0;
			for (num = 0; num < team2playerName.Length; num++)
			{
				if (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[num].BowlerList.bowlingRank != string.Empty)
				{
					num4++;
					CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].PlayerList[num].BowlerList.bowlingOrder = num4;
				}
			}
			if (num4 < 5 || num4 > 7)
			{
				ShowError(10);
				return;
			}
		}
		ShowError(0);
		//GoogleAllAds.Instance.InterstitialAdPlz();
	}

	private void ShowError(int id)
	{
		if (id > 0)
		{
			string str = string.Empty;
			switch (id)
			{
			case 1:
				str = LocalizationData.instance.getText(644);
				break;
			case 2:
				str = LocalizationData.instance.getText(141);
				break;
			case 3:
				str = LocalizationData.instance.getText(142);
				break;
			case 4:
				str = LocalizationData.instance.getText(143);
				break;
			case 5:
				str = LocalizationData.instance.getText(144);
				break;
			case 6:
				str = LocalizationData.instance.getText(145);
				break;
			case 7:
				str = LocalizationData.instance.getText(146);
				break;
			case 8:
				str = LocalizationData.instance.getText(147);
				break;
			case 9:
				str = LocalizationData.instance.getText(148);
				break;
			case 10:
				str = LocalizationData.instance.getText(645);
				break;
			}
			Singleton<ErrorPopupTWO>.instance.showMe(str);
		}
		else
		{
			OnClickContinueButton();
		}
	}

	private void ConfigureGameBeforeToss()
	{
		int num = int.Parse(CONTROLLER.TeamList[CONTROLLER.myTeamIndex].rank);
		int num2 = int.Parse(CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].rank);
		if (num <= 8 && num2 <= 8)
		{
			CONTROLLER.GoodMatch = true;
		}
		else
		{
			CONTROLLER.GoodMatch = false;
		}
		Singleton<NavigationBack>.instance.deviceBack = null;
		Singleton<LoadingPanelTransition>.instance.PanelTransition();
		hideMe();
		Singleton<GameModeTWO>.instance.hideMe();
		Singleton<EntryFeeConfirmation>.instance.holder.SetActive(value: false);
		CONTROLLER.pageName = string.Empty;
		SetTeamIndex();
		SetSquadPage();
		SavePlayerPrefs.SetTeamList();
		Invoke("GoNextPage", 1.5f);
	}

	private void GoNextPage()
	{
		if (CONTROLLER.PlayModeSelected < 4 || CONTROLLER.PlayModeSelected == 7)
		{
			Singleton<TossPageTWO>.instance.showMe();
			hideMe();
			Singleton<LoadingPanelTransition>.instance.HideMe();
		}
		else
		{
			CONTROLLER.SceneIsLoading = true;
			Singleton<LoadPlayerPrefs>.instance.InitializeGame();
			Singleton<NavigationBack>.instance.deviceBack = null;
			Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
		}
	}

	private void SetTeamIndex()
	{
		if (CONTROLLER.currentInnings == 0)
		{
			if (CONTROLLER.meFirstBatting == 1)
			{
				CONTROLLER.BattingTeamIndex = CONTROLLER.myTeamIndex;
				CONTROLLER.BowlingTeamIndex = CONTROLLER.opponentTeamIndex;
			}
			else
			{
				CONTROLLER.BattingTeamIndex = CONTROLLER.opponentTeamIndex;
				CONTROLLER.BowlingTeamIndex = CONTROLLER.myTeamIndex;
			}
		}
		else if (CONTROLLER.meFirstBatting == 1)
		{
			CONTROLLER.BattingTeamIndex = CONTROLLER.opponentTeamIndex;
			CONTROLLER.BowlingTeamIndex = CONTROLLER.myTeamIndex;
		}
		else
		{
			CONTROLLER.BattingTeamIndex = CONTROLLER.myTeamIndex;
			CONTROLLER.BowlingTeamIndex = CONTROLLER.opponentTeamIndex;
		}
	}

	public void editTeam(int index)
	{
		if (index < 11)
		{
			CONTROLLER.EditPlayerIndex = 0;
			CONTROLLER.EditTeamIndex = CONTROLLER.myTeamIndex;
		}
		else
		{
			index -= 11;
			CONTROLLER.EditPlayerIndex = 1;
			CONTROLLER.EditTeamIndex = CONTROLLER.opponentTeamIndex;
		}
		CONTROLLER.EditPlayerIndex = index;
		hideMe();
		Singleton<EditPlayersTWO>.instance.EditPlayerInfo();
	}

	public void Back()
	{
		if (CONTROLLER.PlayModeSelected == 0 || CONTROLLER.PlayModeSelected == 7)
		{
			Singleton<TeamSelectionTWO>.instance.showMe();
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			if (CONTROLLER.TournamentStage == 0)
			{
				Singleton<TeamSelectionTWO>.instance.Continue();
			}
			else
			{
				Singleton<FixturesTWO>.instance.showMe();
			}
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.NPLIndiaTournamentStage == 0)
			{
				Singleton<TeamSelectionTWO>.instance.Continue();
			}
			else
			{
				Singleton<NPLIndiaPlayOff>.instance.ShowMe();
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			if (CONTROLLER.WCTournamentStage == 0)
			{
				Singleton<WorldCupLeague>.instance.ShowMe();
			}
			else
			{
				Singleton<WorldCupPlayOff>.instance.ShowMe();
			}
		}
		else if (CONTROLLER.PlayModeSelected == 4)
		{
			Singleton<SOLevelSelectionPage>.instance.ShowMe();
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			Singleton<CTLevelSelectionPage>.instance.ShowMe();
		}
		hideMe();
	}
}
