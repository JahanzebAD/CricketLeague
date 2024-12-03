using UnityEngine;
using UnityEngine.UI;

public class EditPlayersTWO : Singleton<EditPlayersTWO>
{
	public GameObject Holder;

	public Button SaveBtn;

	public Button CancelBtn;

	private bool edited;

	public Text ErrorTxt;

	public InputField PlayerName;

	public Toggle CaptainToggle;

	public Toggle WicketKeeperToggle;

	public Image myTeamFlag;

	public Toggle LeftBatsman;

	public Toggle RightBatsman;

	public Toggle LeftBowler;

	public Toggle RightBowler;

	public GameObject BowlerHand;

	public GameObject errorPopUpGO;

	public Camera renderCamera;

	public Toggle[] BowlerType;

	private bool isKeeper;

	private string bowlingRank;

	public void hideMe()
	{
		errorPopUpGO.SetActive(value: false);
		Holder.SetActive(value: false);
	}

	public void EditPlayerInfo()
	{
		Singleton<NavigationBack>.instance.deviceBack = CancelEdit;
		CONTROLLER.pageName = "edit";
		edited = false;
		errorPopUpGO.SetActive(value: false);
		ErrorTxt.text = string.Empty;
		isKeeper = CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].isKeeper;
		bowlingRank = CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].BowlerList.bowlingRank;
		SetPlayerInfo();
		SetBowlerType();
		CONTROLLER.menuTitle = "EDIT PLAYER";
		Singleton<GameModeTWO>.instance.updateTitle(_modeSelected: true);
		Singleton<EditPlayersTwoPanelTransition>.instance.ResetTransistion();
		Holder.SetActive(value: true);
		Singleton<EditPlayersTwoPanelTransition>.instance.PanelTransistion();
		CONTROLLER.CurrentMenu = "editplayer";
	}

	private void SetPlayerInfo()
	{
		if (bowlingRank != string.Empty)
		{
			isKeeper = false;
		}
		string abbrevation = CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].abbrevation;
		myTeamFlag.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
		PlayerName.text = CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].PlayerName.ToUpper();
		if (CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].isCaptain)
		{
			CaptainToggle.isOn = true;
		}
		else
		{
			CaptainToggle.isOn = false;
		}
		if (isKeeper)
		{
			WicketKeeperToggle.isOn = true;
			BowlerType[0].isOn = true;
			BowlerType[2].isOn = false;
			BowlerType[3].isOn = false;
			BowlerType[1].isOn = false;
			BowlerType[4].isOn = false;
			BowlerHand.SetActive(value: false);
		}
		else
		{
			WicketKeeperToggle.isOn = false;
		}
		if (CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].BatsmanList.BattingHand == "R")
		{
			LeftBatsman.isOn = false;
			RightBatsman.isOn = true;
		}
		else
		{
			LeftBatsman.isOn = true;
			RightBatsman.isOn = false;
		}
	}

	private void SetBowlerType()
	{
		if (isKeeper)
		{
			bowlingRank = string.Empty;
		}
		if (bowlingRank == string.Empty)
		{
			BowlerHand.SetActive(value: false);
			BowlerType[0].isOn = true;
			BowlerType[1].isOn = false;
			BowlerType[2].isOn = false;
			BowlerType[3].isOn = false;
			BowlerType[4].isOn = false;
			return;
		}
		if (bowlingRank == "0")
		{
			BowlerType[1].isOn = true;
			BowlerHand.SetActive(value: true);
			BowlerType[0].isOn = false;
			BowlerType[2].isOn = false;
			BowlerType[3].isOn = false;
			BowlerType[4].isOn = false;
		}
		else if (bowlingRank == "1")
		{
			BowlerType[3].isOn = true;
			BowlerHand.SetActive(value: true);
			BowlerType[1].isOn = false;
			BowlerType[0].isOn = false;
			BowlerType[2].isOn = false;
			BowlerType[4].isOn = false;
		}
		else if (bowlingRank == "2")
		{
			BowlerType[2].isOn = true;
			BowlerHand.SetActive(value: true);
			BowlerType[3].isOn = false;
			BowlerType[0].isOn = false;
			BowlerType[1].isOn = false;
			BowlerType[4].isOn = false;
		}
		else if (bowlingRank == "3")
		{
			BowlerType[4].isOn = true;
			BowlerHand.SetActive(value: true);
			BowlerType[3].isOn = false;
			BowlerType[0].isOn = false;
			BowlerType[1].isOn = false;
			BowlerType[2].isOn = false;
		}
		BowlerHand.SetActive(value: true);
		if (CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].BowlerList.BowlingHand == "L")
		{
			LeftBowler.isOn = true;
			RightBowler.isOn = false;
		}
		else
		{
			LeftBowler.isOn = false;
			RightBowler.isOn = true;
		}
	}

	public void closePopup()
	{
		errorPopUpGO.SetActive(value: false);
		CONTROLLER.CurrentMenu = "editplayer";
	}

	public void SavePlayerInfo()
	{
		bool flag = true;
		bool flag2 = false;
		for (int i = 0; i < PlayerName.text.Length; i++)
		{
			if (PlayerName.text[i] != ' ')
			{
				flag = false;
				break;
			}
		}
		for (int i = 0; i < PlayerName.text.Length; i++)
		{
			if (!char.IsLetterOrDigit(PlayerName.text[i]) && PlayerName.text[i] != '-' && PlayerName.text[i] != ' ')
			{
				flag2 = true;
				break;
			}
		}
		if (flag2)
		{
			CONTROLLER.CurrentMenu = "editplayererror";
			Singleton<ErrorPopupTWO>.instance.showMe(LocalizationData.instance.getText(559));
			ErrorTxt.text = LocalizationData.instance.getText(559);
			return;
		}
		if (flag)
		{
			CONTROLLER.CurrentMenu = "editplayererror";
			Singleton<ErrorPopupTWO>.instance.showMe(LocalizationData.instance.getText(560));
			ErrorTxt.text = LocalizationData.instance.getText(560);
			return;
		}
		errorPopUpGO.SetActive(value: false);
		ErrorTxt.text = string.Empty;
		while (PlayerName.text[0] == ' ')
		{
			PlayerName.text = PlayerName.text.Substring(1, PlayerName.text.Length - 1);
		}
		CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].PlayerName = PlayerName.text;
		if (CaptainToggle.isOn)
		{
			for (int i = 0; i < CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList.Length; i++)
			{
				CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[i].isCaptain = false;
			}
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].isCaptain = true;
		}
		else
		{
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].isCaptain = false;
		}
		if (WicketKeeperToggle.isOn)
		{
			for (int i = 0; i < CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList.Length; i++)
			{
				CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[i].isKeeper = false;
			}
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].isKeeper = true;
		}
		else
		{
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].isKeeper = false;
		}
		if (RightBatsman.isOn)
		{
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].BatsmanList.BattingHand = "R";
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].BatsmanList.Style = "Right Hand Batsman";
		}
		else if (LeftBatsman.isOn)
		{
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].BatsmanList.BattingHand = "L";
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].BatsmanList.Style = "Left Hand Batsman";
		}
		if (!BowlerType[0].isOn)
		{
			string bowlingHand = " ";
			string style = " ";
			string text = " ";
			if (RightBowler.isOn)
			{
				BowlerHand.SetActive(value: true);
				bowlingHand = "R";
				if (BowlerType[1].isOn)
				{
					style = "Right Arm Medium Fast";
					text = "0";
				}
				else if (BowlerType[2].isOn)
				{
					style = "Legbreak googly";
					text = "2";
				}
				else if (BowlerType[3].isOn)
				{
					style = "Right Arm offbreak";
					text = "1";
				}
				else if (BowlerType[4].isOn)
				{
					style = "Medium Pace";
					text = "3";
				}
			}
			else if (LeftBowler.isOn)
			{
				BowlerHand.SetActive(value: true);
				bowlingHand = "L";
				if (BowlerType[1].isOn)
				{
					style = "Left Arm Medium Fast";
					text = "0";
				}
				else if (BowlerType[2].isOn)
				{
					style = "Slow left-arm orthodox";
					text = "2";
				}
				else if (BowlerType[3].isOn)
				{
					style = "Left Arm offbreak";
					text = "1";
				}
				else if (BowlerType[4].isOn)
				{
					style = "Medium Pace";
					text = "3";
				}
			}
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].BowlerList.BowlingHand = bowlingHand;
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].BowlerList.Style = style;
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].BowlerList.bowlingRank = text;
		}
		else
		{
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].BowlerList.BowlingHand = string.Empty;
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].BowlerList.Style = string.Empty;
			CONTROLLER.TeamList[CONTROLLER.EditTeamIndex].PlayerList[CONTROLLER.EditPlayerIndex].BowlerList.bowlingRank = string.Empty;
		}
		SavePlayerPrefs.SetTeamList();
		CONTROLLER.TeamEdited = true;
		hideMe();
		Singleton<SquadPageTWO>.instance.showMe(CONTROLLER.EditTeamIndex);
	}

	public void CancelEdit()
	{
		if (edited)
		{
			Singleton<WarningEditPlayersTWO>.instance.showMe();
		}
		else if (!edited)
		{
			hideMe();
			Singleton<SquadPageTWO>.instance.showMe(CONTROLLER.EditTeamIndex);
		}
	}

	public void WicketKeeperChoosed()
	{
		if (WicketKeeperToggle.isOn)
		{
			isKeeper = true;
			bowlingRank = string.Empty;
			BowlerHand.SetActive(value: false);
			BowlerType[0].isOn = true;
			BowlerType[1].isOn = false;
			BowlerType[2].isOn = false;
			BowlerType[3].isOn = false;
			BowlerType[4].isOn = false;
		}
		else
		{
			isKeeper = false;
		}
	}

	public void BowlerTypeChoosed(int DataValue)
	{
		if (!BowlerType[0].isOn && DataValue > 0)
		{
			BowlerHand.SetActive(value: true);
			bowlingRank = string.Empty + (DataValue - 1);
			isKeeper = false;
			WicketKeeperToggle.isOn = false;
			if (!LeftBowler.isOn && !RightBowler.isOn)
			{
				RightBowler.isOn = true;
			}
		}
		else
		{
			BowlerHand.SetActive(value: false);
			bowlingRank = string.Empty;
		}
	}

	public void BackKeySelected()
	{
		CancelEdit();
	}

	public void Clicked()
	{
		edited = true;
	}
}
