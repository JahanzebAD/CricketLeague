using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelectionTWO : Singleton<TeamSelectionTWO>
{
	public GameObject Holder;

	public GameObject panel1;

	public GameObject panel2;

	public GameObject outerPanel;

	public GameObject SOPanel;

	public ButtonSpriteSwap quickPlay;

	public ButtonSpriteSwap others;

	public GameObject entryFeePanel;

	public ScreenExitAnimation exitAnim;

	public ButtonSpriteSwap overSpriteSwap;

	public Text MyTeamName;

	public Text MyTeamName2;

	public Text SOMyTeamName;

	private bool canClick = true;

	public Text OppTeamName;

	public Text SOOppTeamName;

	public Text Tittle;

	public Text SubTittle;

	public Image MyTeamFlag;

	public Image MyTeamFlag2;

	public Image SOMyTeamFlag;

	public Image OppTeamFlag;

	public Image SOOppTeamFlag;

	public Image leftFlag;

	public Image midFlag;

	public Image rightFlag;

	public Image leftFlag2;

	public Image midFlag2;

	public Image rightFlag2;

	public Sprite[] btnSprite;

	public Button[] overs;

	public Button[] difficulty;

	private string oversText;

	private List<int> topTeams;

	private List<int> qualifiedTeams;

	private int topEightTeam = 8;

	private List<int> groupAteam;

	private List<int> groupBteam;

	protected void Start()
	{
		hideMe();
	}

	public void myTeamSelectionLeftButton()
	{
		CONTROLLER.myTeamIndex--;
		myTeamLeftArrowSelected();
		if (CONTROLLER.PlayModeSelected == 7 && (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation.ToUpper() == "KEN" || CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation.ToUpper() == "NED" || CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation.ToUpper() == "SCO" || CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation.ToUpper() == "UAE"))
		{
			myTeamSelectionLeftButton();
		}
		else if (CONTROLLER.PlayModeSelected == 0 || CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5 || CONTROLLER.PlayModeSelected == 7)
		{
			SetQuickPlay();
		}
		else
		{
			SetTournamentPlay();
		}
	}

	private void myTeamLeft()
	{
		CONTROLLER.myTeamIndex--;
		myTeamLeftArrowSelected();
		if (CONTROLLER.PlayModeSelected == 0 || CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5 || CONTROLLER.PlayModeSelected == 7)
		{
			SetQuickPlay();
		}
		else
		{
			SetTournamentPlay();
		}
		Sequence s = DOTween.Sequence();
		s.Insert(0f, midFlag.transform.DOScaleX(1f, 0.2f)).OnComplete(EnableClick);
	}

	private void EnableClick()
	{
		canClick = true;
	}

	public void myTeamSelectionRightButton()
	{
		CONTROLLER.myTeamIndex++;
		myTeamRightArrowSelected();
		if (CONTROLLER.PlayModeSelected == 7 && (CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation.ToUpper() == "KEN" || CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation.ToUpper() == "NED" || CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation.ToUpper() == "SCO" || CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation.ToUpper() == "UAE"))
		{
			myTeamSelectionRightButton();
		}
		else if (CONTROLLER.PlayModeSelected == 0 || CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5 || CONTROLLER.PlayModeSelected == 7)
		{
			SetQuickPlay();
		}
		else
		{
			SetTournamentPlay();
		}
	}

	private void myTeamLeftArrowSelected()
	{
		if (CONTROLLER.PlayModeSelected == 0 || CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5 || CONTROLLER.PlayModeSelected == 7)
		{
			if (CONTROLLER.myTeamIndex == CONTROLLER.opponentTeamIndex)
			{
				CONTROLLER.myTeamIndex--;
			}
			if (CONTROLLER.myTeamIndex < 0)
			{
				CONTROLLER.myTeamIndex = CONTROLLER.TeamList.Length - 1;
				if (CONTROLLER.myTeamIndex == CONTROLLER.opponentTeamIndex)
				{
					CONTROLLER.myTeamIndex--;
				}
			}
		}
		else if (CONTROLLER.myTeamIndex < 0)
		{
			CONTROLLER.myTeamIndex = CONTROLLER.TeamList.Length - 1;
		}
	}

	private void myTeamRightArrowSelected()
	{
		if (CONTROLLER.PlayModeSelected == 0 || CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5 || CONTROLLER.PlayModeSelected == 7)
		{
			if (CONTROLLER.myTeamIndex >= CONTROLLER.TeamList.Length)
			{
				CONTROLLER.myTeamIndex = 0;
			}
			if (CONTROLLER.myTeamIndex == CONTROLLER.opponentTeamIndex)
			{
				CONTROLLER.myTeamIndex++;
				if (CONTROLLER.myTeamIndex >= CONTROLLER.TeamList.Length)
				{
					CONTROLLER.myTeamIndex = 0;
				}
			}
		}
		else if (CONTROLLER.myTeamIndex >= CONTROLLER.TeamList.Length)
		{
			CONTROLLER.myTeamIndex = 0;
		}
	}

	public void oppTeamSelectionLeftArrow()
	{
		CONTROLLER.opponentTeamIndex--;
		oppTeamLeftArrowSelected();
		if (CONTROLLER.PlayModeSelected == 7 && (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation.ToUpper() == "KEN" || CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation.ToUpper() == "NED" || CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation.ToUpper() == "SCO" || CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation.ToUpper() == "UAE"))
		{
			oppTeamSelectionLeftArrow();
		}
		else if (CONTROLLER.PlayModeSelected == 0 || CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5 || CONTROLLER.PlayModeSelected == 7)
		{
			SetQuickPlay();
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			SetTournamentPlay();
		}
	}

	public void oppTeamSelectionRightArrow()
	{
		CONTROLLER.opponentTeamIndex++;
		oppTeamRightArrowSelected();
		if (CONTROLLER.PlayModeSelected == 7 && (CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation.ToUpper() == "KEN" || CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation.ToUpper() == "NED" || CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation.ToUpper() == "SCO" || CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation.ToUpper() == "UAE"))
		{
			oppTeamSelectionRightArrow();
		}
		else if (CONTROLLER.PlayModeSelected == 0 || CONTROLLER.PlayModeSelected == 4 || CONTROLLER.PlayModeSelected == 5 || CONTROLLER.PlayModeSelected == 7)
		{
			SetQuickPlay();
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			SetTournamentPlay();
		}
	}

	private void oppTeamLeftArrowSelected()
	{
		if (CONTROLLER.myTeamIndex == CONTROLLER.opponentTeamIndex)
		{
			CONTROLLER.opponentTeamIndex--;
		}
		if (CONTROLLER.opponentTeamIndex < 0)
		{
			CONTROLLER.opponentTeamIndex = CONTROLLER.TeamList.Length - 1;
			if (CONTROLLER.myTeamIndex == CONTROLLER.opponentTeamIndex)
			{
				CONTROLLER.opponentTeamIndex--;
			}
		}
	}

	private void oppTeamRightArrowSelected()
	{
		if (CONTROLLER.opponentTeamIndex >= CONTROLLER.TeamList.Length)
		{
			CONTROLLER.opponentTeamIndex = 0;
		}
		if (CONTROLLER.myTeamIndex == CONTROLLER.opponentTeamIndex)
		{
			CONTROLLER.opponentTeamIndex++;
			if (CONTROLLER.opponentTeamIndex >= CONTROLLER.TeamList.Length)
			{
				CONTROLLER.opponentTeamIndex = 0;
			}
		}
	}

	public void SetQuickPlay()
	{
		if (CONTROLLER.myTeamIndex >= 0 && CONTROLLER.myTeamIndex < CONTROLLER.TeamList.Length)
		{
			Text sOMyTeamName = SOMyTeamName;
			string text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].teamName.ToUpper();
			MyTeamName.text = text;
			sOMyTeamName.text = text;
			Debug.Log("Started :" + SOMyTeamName.text);
			if (LocalizationData.instance.refList.Contains(SOMyTeamName.text.ToUpper()))
			{
				Debug.Log("entered");
				int num = LocalizationData.instance.refList.IndexOf(SOMyTeamName.text);
				Text sOMyTeamName2 = SOMyTeamName;
				text = LocalizationData.instance.getText(num);
				MyTeamName.text = text;
				sOMyTeamName2.text = text;
				Debug.Log(num + " " + SOMyTeamName.text);
			}
			string abbrevation = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation;
			Image sOMyTeamFlag = SOMyTeamFlag;
			Sprite sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
			MyTeamFlag.sprite = sprite;
			sOMyTeamFlag.sprite = sprite;
		}
		if (CONTROLLER.opponentTeamIndex >= 0 && CONTROLLER.opponentTeamIndex < CONTROLLER.TeamList.Length)
		{
			Text sOOppTeamName = SOOppTeamName;
			string text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].teamName.ToUpper();
			OppTeamName.text = text;
			sOOppTeamName.text = text;
			Debug.Log("Started1 :" + SOOppTeamName.text);
			if (LocalizationData.instance.refList.Contains(SOOppTeamName.text.ToUpper()))
			{
				Debug.Log("entered1");
				int num2 = LocalizationData.instance.refList.IndexOf(SOOppTeamName.text);
				Text sOOppTeamName2 = SOOppTeamName;
				text = LocalizationData.instance.getText(num2);
				OppTeamName.text = text;
				sOOppTeamName2.text = text;
				Debug.Log(num2 + " " + SOOppTeamName.text);
			}
			string abbrevation2 = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation;
			Image sOOppTeamFlag = SOOppTeamFlag;
			Sprite sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation2);
			OppTeamFlag.sprite = sprite;
			sOOppTeamFlag.sprite = sprite;
		}
		//GoogleAllAds.Instance.InterstitialAdPlz();
	}

	public void SetTournamentPlay()
	{
		if (CONTROLLER.myTeamIndex >= 0 && CONTROLLER.myTeamIndex < CONTROLLER.TeamList.Length)
		{
			MyTeamName2.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].teamName.ToUpper();
			if (LocalizationData.instance.refList.Contains(MyTeamName2.text.ToUpper()))
			{
				int index = LocalizationData.instance.refList.IndexOf(MyTeamName2.text);
				MyTeamName2.text = LocalizationData.instance.getText(index);
			}
			string abbrevation = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation;
			MyTeamFlag2.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
		}
		//GoogleAllAds.Instance.InterstitialAdPlz();
	}

	private void setDifficulty()
	{
		if (CONTROLLER.difficultyMode == "easy")
		{
			if (CONTROLLER.PlayModeSelected == 0 || CONTROLLER.PlayModeSelected == 7)
			{
				difficulty[3].image.sprite = btnSprite[0];
				difficulty[3].GetComponentInChildren<Text>().color = Color.black;
				difficulty[4].image.sprite = btnSprite[1];
				difficulty[4].GetComponentInChildren<Text>().color = Color.white;
				difficulty[5].image.sprite = btnSprite[1];
				difficulty[5].GetComponentInChildren<Text>().color = Color.white;
				quickPlay.changeSprite(0);
			}
			else
			{
				difficulty[0].image.sprite = btnSprite[0];
				difficulty[0].GetComponentInChildren<Text>().color = Color.black;
				difficulty[1].image.sprite = btnSprite[1];
				difficulty[1].GetComponentInChildren<Text>().color = Color.white;
				difficulty[2].image.sprite = btnSprite[1];
				difficulty[2].GetComponentInChildren<Text>().color = Color.white;
				others.changeSprite(0);
			}
		}
		else if (CONTROLLER.difficultyMode == "medium")
		{
			if (CONTROLLER.PlayModeSelected == 0 || CONTROLLER.PlayModeSelected == 7)
			{
				difficulty[3].image.sprite = btnSprite[1];
				difficulty[3].GetComponentInChildren<Text>().color = Color.white;
				difficulty[4].image.sprite = btnSprite[0];
				difficulty[4].GetComponentInChildren<Text>().color = Color.black;
				difficulty[5].image.sprite = btnSprite[1];
				difficulty[5].GetComponentInChildren<Text>().color = Color.white;
				quickPlay.changeSprite(1);
			}
			else
			{
				difficulty[0].image.sprite = btnSprite[1];
				difficulty[0].GetComponentInChildren<Text>().color = Color.white;
				difficulty[1].image.sprite = btnSprite[0];
				difficulty[1].GetComponentInChildren<Text>().color = Color.black;
				difficulty[2].image.sprite = btnSprite[1];
				difficulty[2].GetComponentInChildren<Text>().color = Color.white;
				others.changeSprite(1);
			}
		}
		else if (CONTROLLER.difficultyMode == "hard")
		{
			if (CONTROLLER.PlayModeSelected == 0 || CONTROLLER.PlayModeSelected == 7)
			{
				difficulty[3].image.sprite = btnSprite[1];
				difficulty[3].GetComponentInChildren<Text>().color = Color.white;
				difficulty[4].image.sprite = btnSprite[1];
				difficulty[4].GetComponentInChildren<Text>().color = Color.white;
				difficulty[5].image.sprite = btnSprite[0];
				difficulty[5].GetComponentInChildren<Text>().color = Color.black;
				quickPlay.changeSprite(2);
			}
			else
			{
				difficulty[0].image.sprite = btnSprite[1];
				difficulty[0].GetComponentInChildren<Text>().color = Color.white;
				difficulty[1].image.sprite = btnSprite[1];
				difficulty[1].GetComponentInChildren<Text>().color = Color.white;
				difficulty[2].image.sprite = btnSprite[0];
				difficulty[2].GetComponentInChildren<Text>().color = Color.black;
				others.changeSprite(2);
			}
		}
		saveDifficulty();
	}

	private void SendFirebaseOvers()
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			PlayerPrefs.SetInt("userLastPlayed", 1);
			//Singleton<Firebase_Events>.instance.Firebase_QP_Mode();
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			PlayerPrefs.SetInt("userLastPlayed", 2);
			//Singleton<Firebase_Events>.instance.Firebase_T20_Mode();
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			PlayerPrefs.SetInt("userLastPlayed", 3);
			//Singleton<Firebase_Events>.instance.Firebase_WC_Mode();
		}
		else if (CONTROLLER.PlayModeSelected != 2)
		{
		}
	}

	private void saveDifficulty()
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			ObscuredPrefs.SetString("exdiff", CONTROLLER.difficultyMode);
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			ObscuredPrefs.SetString("tourdiff", CONTROLLER.difficultyMode);
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "PAK")
			{
				ObscuredPrefs.SetString("pakdiff", CONTROLLER.difficultyMode);
			}
			else if (CONTROLLER.tournamentType == "NPL")
			{
				ObscuredPrefs.SetString("npldiff", CONTROLLER.difficultyMode);
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				ObscuredPrefs.SetString("ausdiff", CONTROLLER.difficultyMode);
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			ObscuredPrefs.SetString("wcdiff", CONTROLLER.difficultyMode);
		}
	}

	public void EasyButtonOnClicked()
	{
		CONTROLLER.difficultyMode = "easy";
		setDifficulty();
		saveDifficulty();
	}

	public void MediumButtonOnClicked()
	{
		CONTROLLER.difficultyMode = "medium";
		setDifficulty();
		saveDifficulty();
	}

	public void HardButtonOnClicked()
	{
		CONTROLLER.difficultyMode = "hard";
		setDifficulty();
		saveDifficulty();
	}

	public void setTeams()
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			string empty = string.Empty;
			empty = empty + CONTROLLER.myTeamIndex + "|";
			empty += CONTROLLER.opponentTeamIndex;
			ObscuredPrefs.SetString("ETM", empty);
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			ObscuredPrefs.SetInt("TTM", CONTROLLER.myTeamIndex);
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "NPL")
			{
				ObscuredPrefs.SetInt("nplindiamti", CONTROLLER.myTeamIndex);
			}
			else if (CONTROLLER.tournamentType == "PAK")
			{
				ObscuredPrefs.SetInt("pakmti", CONTROLLER.myTeamIndex);
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				ObscuredPrefs.SetInt("ausmti", CONTROLLER.myTeamIndex);
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			ObscuredPrefs.SetInt("wmti", CONTROLLER.myTeamIndex);
		}
		else if (CONTROLLER.PlayModeSelected == 4)
		{
			string empty2 = string.Empty;
			empty2 = empty2 + CONTROLLER.myTeamIndex + "|";
			empty2 += CONTROLLER.opponentTeamIndex;
			ObscuredPrefs.SetString("somti", empty2);
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			string empty3 = string.Empty;
			empty3 = empty3 + CONTROLLER.myTeamIndex + "|";
			empty3 += CONTROLLER.opponentTeamIndex;
			ObscuredPrefs.SetString("scmti", empty3);
		}
		else if (CONTROLLER.PlayModeSelected == 7)
		{
			string empty4 = string.Empty;
			empty4 = empty4 + CONTROLLER.myTeamIndex + "|";
			empty4 += CONTROLLER.opponentTeamIndex;
			ObscuredPrefs.SetString("TMTI", empty4);
		}
		//GoogleAllAds.Instance.InterstitialAdPlz();
	}

	private int getCorrectTeam(int teamId)
	{
		for (int i = 0; i < CONTROLLER.TeamList.Length; i++)
		{
			if (int.Parse(CONTROLLER.TeamList[i].rank) == teamId + 1)
			{
				return i;
			}
		}
		return -1;
	}

	public void selectTopTeams()
	{
		topTeams = new List<int>();
		qualifiedTeams = new List<int>();
		groupAteam = new List<int>();
		groupBteam = new List<int>();
		for (int i = 0; i < topEightTeam; i++)
		{
			topTeams.Add(i);
			qualifiedTeams.Add(topEightTeam + i);
		}
		selectGroup();
	}

	private void selectGroup()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (num3 = 0; num3 < 4; num3++)
		{
			num = Random.Range(0, topTeams.Count);
			num2 = topTeams[num];
			topTeams.RemoveAt(num);
			num2 = getCorrectTeam(num2);
			groupAteam.Add(num2);
		}
		for (num3 = 0; num3 < 4; num3++)
		{
			num = Random.Range(0, qualifiedTeams.Count);
			num2 = qualifiedTeams[num];
			qualifiedTeams.RemoveAt(num);
			num2 = getCorrectTeam(num2);
			groupAteam.Add(num2);
		}
		for (num3 = 0; num3 < 4; num3++)
		{
			num = Random.Range(0, topTeams.Count);
			num2 = topTeams[num];
			topTeams.RemoveAt(num);
			num2 = getCorrectTeam(num2);
			groupBteam.Add(num2);
		}
		for (num3 = 0; num3 < 4; num3++)
		{
			num = Random.Range(0, qualifiedTeams.Count);
			num2 = qualifiedTeams[num];
			qualifiedTeams.RemoveAt(num);
			num2 = getCorrectTeam(num2);
			groupBteam.Add(num2);
		}
		CONTROLLER.matchIndex = 0;
		int num4 = 0;
		string text = CONTROLLER.TournamentStage + "&" + CONTROLLER.myTeamIndex + "&" + CONTROLLER.oversSelectedIndex + "&" + CONTROLLER.matchIndex + "&";
		string text2;
		for (num3 = 0; num3 < groupAteam.Count / 2; num3++)
		{
			num4 = Random.Range(0, 10);
			if (num4 >= 8)
			{
				text2 = text;
				text = text2 + string.Empty + groupAteam[groupAteam.Count - num3 - 1] + "-" + groupAteam[num3] + "-A|";
			}
			else
			{
				text2 = text;
				text = text2 + string.Empty + groupAteam[num3] + "-" + groupAteam[groupAteam.Count - num3 - 1] + "-A|";
			}
		}
		for (num3 = 0; num3 < groupBteam.Count / 2; num3++)
		{
			num4 = Random.Range(0, 10);
			if (num3 >= groupBteam.Count / 2 - 1)
			{
				if (num4 >= 8)
				{
					text2 = text;
					text = text2 + string.Empty + groupBteam[groupBteam.Count - num3 - 1] + "-" + groupBteam[num3] + "-B";
				}
				else
				{
					text2 = text;
					text = text2 + string.Empty + groupBteam[num3] + "-" + groupBteam[groupBteam.Count - num3 - 1] + "-B";
				}
			}
			else if (num4 >= 8)
			{
				text2 = text;
				text = text2 + string.Empty + groupBteam[groupBteam.Count - num3 - 1] + "-" + groupBteam[num3] + "-B|";
			}
			else
			{
				text2 = text;
				text = text2 + string.Empty + groupBteam[num3] + "-" + groupBteam[groupBteam.Count - num3 - 1] + "-B|";
			}
		}
		CONTROLLER.quaterFinalList = string.Empty;
		CONTROLLER.semiFinalList = string.Empty;
		CONTROLLER.finalList = string.Empty;
		text2 = text;
		text = text2 + "&" + CONTROLLER.quaterFinalList + "&" + CONTROLLER.semiFinalList + "&" + CONTROLLER.finalList;
		ObscuredPrefs.SetString("tour", text);
		CONTROLLER.tournamentStr = text;
		Singleton<FixturesTWO>.instance.showMe();
	}

	public void ChooseOtherLeague(int index)
	{
		switch (index)
		{
		case 0:
			XMLReader.LoadTeams(1);
			break;
		case 1:
			XMLReader.LoadTeams(0);
			break;
		}
	}

	public void Continue()
	{
		if (CONTROLLER.PlayModeSelected == 4)
		{
			Singleton<LoadPlayerPrefs>.instance.GetSuperOverLevelDetails();
			CONTROLLER.LevelId = CONTROLLER.CurrentLevelCompleted;
			PaidContinue();
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			Singleton<LoadPlayerPrefs>.instance.GetChaseTargetLevelDetails();
			PaidContinue();
		}
		else
		{
			PaidContinue();
		}
		//GoogleAllAds.Instance.InterstitialAdPlz();
	}

	public void PaidContinue()
	{
		SendFirebaseOvers();
		if (CONTROLLER.PlayModeSelected == 0)
		{
			oversText = "QPOvers";
			Singleton<SquadPageTWO>.instance.showMe(1);
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			oversText = "T20Overs";
			if (ObscuredPrefs.HasKey("tour"))
			{
				CONTROLLER.tournamentStr = ObscuredPrefs.GetString("tour");
				Singleton<FixturesTWO>.instance.showMe();
			}
			else
			{
				Singleton<TeamSelectionTWO>.instance.selectTopTeams();
			}
			ObscuredPrefs.SetInt("T20TeamsSelected", 1);
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "PAK")
			{
				oversText = "PAKOvers";
				CONTROLLER.NPLIndiaPointsTable = Singleton<LoadPlayerPrefs>.instance.setPointsTable(CONTROLLER.TeamList.Length, CONTROLLER.NPLIndiaPointsTable);
				Singleton<NplGroupMatchesTWOPanelTransistion>.instance.ResetTransistion();
				Singleton<NPLIndiaLeague>.instance.ShowMe();
				ObscuredPrefs.SetInt("PAKTeamsSelected", 1);
			}
			else if (CONTROLLER.tournamentType == "NPL")
			{
				oversText = "NPLOvers";
				CONTROLLER.NPLIndiaPointsTable = Singleton<LoadPlayerPrefs>.instance.setPointsTable(CONTROLLER.TeamList.Length, CONTROLLER.NPLIndiaPointsTable);
				Singleton<NplGroupMatchesTWOPanelTransistion>.instance.ResetTransistion();
				Singleton<NPLIndiaLeague>.instance.ShowMe();
				ObscuredPrefs.SetInt("NPLTeamsSelected", 1);
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				oversText = "AUSOvers";
				CONTROLLER.NPLIndiaPointsTable = Singleton<LoadPlayerPrefs>.instance.setPointsTable(CONTROLLER.TeamList.Length, CONTROLLER.NPLIndiaPointsTable);
				Singleton<NplGroupMatchesTWOPanelTransistion>.instance.ResetTransistion();
				Singleton<NPLIndiaLeague>.instance.ShowMe();
				ObscuredPrefs.SetInt("AUSTeamsSelected", 1);
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			oversText = "WCOvers";
			Singleton<WCTeamFixturesTWOPanelTransistion>.instance.ResetTransistion();
			Singleton<WorldCupLeague>.instance.ShowMe();
			Singleton<WCTeamFixturesTWOPanelTransistion>.instance.PanelTransistion();
			ObscuredPrefs.SetInt("WCTeamsSelected", 1);
		}
		else if (CONTROLLER.PlayModeSelected == 4)
		{
			Singleton<SOLevelSelectionPage>.instance.ShowMe();
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			Singleton<CTMenuScreen>.instance.ShowMe();
			Singleton<SuperChaseCTPanelTransition>.instance.panelTransition();
		}
		else if (CONTROLLER.PlayModeSelected == 7)
		{
			oversText = "TMOvers";
			Singleton<GUIRoot>.instance.GetMyTeamList();
			Singleton<SquadPageTWO>.instance.showMe(1);
		}
		hideMe();
		CONTROLLER.oversSelectedIndex = ObscuredPrefs.GetInt(oversText);
		setTeams();
	}

	private void NPL()
	{
		Singleton<NplGroupMatchesTWOPanelTransistion>.instance.ResetTransistion();
		Singleton<NPLIndiaLeague>.instance.ShowMe();
	}

	public void ReselectOver()
	{
		Holder.SetActive(value: false);
		Singleton<ReselectOvers>.instance.ShowMe();
	}

	public void Back()
	{
        if (CONTROLLER.PlayModeSelected < 4)
        {
            Singleton<EntryFeesAndRewards>.instance.ShowMe();
        }
        else
        {
            Singleton<GameModeTWO>.instance.showMe();
            CONTROLLER.pageName = "landingPage";
        }
		hideMe();
		setTeams();
	}

	public void showMe()
	{
		//Singleton<AdIntegrate>.instance.ShowAd();
		Singleton<NavigationBack>.instance.deviceBack = Back;
		CONTROLLER.GameStartsFromSave = false;
		CONTROLLER.pageName = "teamSelection";
		Singleton<LoadPlayerPrefs>.instance.InitializeGame();
		Singleton<TeamSelectionPanelTransition>.instance.panelTransition();
		if (CONTROLLER.PlayModeSelected == 0)
		{
			if (ObscuredPrefs.HasKey("exdiff"))
			{
				CONTROLLER.difficultyMode = "easy";
			}
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			Tittle.text = LocalizationData.instance.getText(185);
			SubTittle.text = LocalizationData.instance.getText(397);
			if (ObscuredPrefs.HasKey("tourdiff"))
			{
				CONTROLLER.difficultyMode = "easy";
			}
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			Tittle.text = LocalizationData.instance.getText(562);
			SubTittle.text = LocalizationData.instance.getText(563);
			if (CONTROLLER.tournamentType == "PAK")
			{
				if (ObscuredPrefs.HasKey("pakdiff"))
				{
					CONTROLLER.difficultyMode = "easy";
				}
			}
			else if (CONTROLLER.tournamentType == "NPL")
			{
				if (ObscuredPrefs.HasKey("npldiff"))
				{
					CONTROLLER.difficultyMode = "easy";
				}
			}
			else if (CONTROLLER.tournamentType == "AUS" && ObscuredPrefs.HasKey("ausdiff"))
			{
				CONTROLLER.difficultyMode = "easy";
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			Tittle.text = LocalizationData.instance.getText(185);
			SubTittle.text = LocalizationData.instance.getText(397);
			if (ObscuredPrefs.HasKey("wcdiff"))
			{
				CONTROLLER.difficultyMode = "easy";
			}
		}
		if (CONTROLLER.PlayModeSelected == 0 || CONTROLLER.PlayModeSelected == 7)
		{
			panel1.SetActive(value: true);
			panel2.SetActive(value: false);
			SOPanel.SetActive(value: false);
		}
		else if (CONTROLLER.PlayModeSelected == 1 || CONTROLLER.PlayModeSelected == 2 || CONTROLLER.PlayModeSelected == 3)
		{
			panel1.SetActive(value: false);
			panel2.SetActive(value: true);
		}
		if (CONTROLLER.PlayModeSelected == 0)
		{
			if (ObscuredPrefs.HasKey("ETM"))
			{
				string @string = ObscuredPrefs.GetString("ETM");
				string[] array = @string.Split("|"[0]);
				CONTROLLER.myTeamIndex = int.Parse(array[0]);
				CONTROLLER.opponentTeamIndex = int.Parse(array[1]);
			}
			if (CONTROLLER.myTeamIndex == CONTROLLER.opponentTeamIndex)
			{
				if (CONTROLLER.opponentTeamIndex < CONTROLLER.TeamList.Length && CONTROLLER.opponentTeamIndex != 0)
				{
					CONTROLLER.opponentTeamIndex--;
				}
				else if (CONTROLLER.opponentTeamIndex == 0)
				{
					CONTROLLER.opponentTeamIndex++;
				}
			}
			SetQuickPlay();
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			if (ObscuredPrefs.HasKey("TTM"))
			{
				CONTROLLER.myTeamIndex = ObscuredPrefs.GetInt("TTM");
			}
			else
			{
				CONTROLLER.myTeamIndex = 0;
			}
			SetTournamentPlay();
			Singleton<LoadPlayerPrefs>.instance.getTournamentList();
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "NPL")
			{
				if (ObscuredPrefs.HasKey("nplindiamti"))
				{
					CONTROLLER.myTeamIndex = ObscuredPrefs.GetInt("nplindiamti");
				}
				else
				{
					CONTROLLER.myTeamIndex = 0;
				}
			}
			else if (CONTROLLER.tournamentType == "PAK")
			{
				if (ObscuredPrefs.HasKey("pakmti"))
				{
					CONTROLLER.myTeamIndex = ObscuredPrefs.GetInt("pakmti");
				}
				else
				{
					CONTROLLER.myTeamIndex = 0;
				}
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				if (ObscuredPrefs.HasKey("ausmti"))
				{
					CONTROLLER.myTeamIndex = ObscuredPrefs.GetInt("ausmti");
				}
				else
				{
					CONTROLLER.myTeamIndex = 0;
				}
			}
			SetTournamentPlay();
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			if (ObscuredPrefs.HasKey("wmti"))
			{
				CONTROLLER.myTeamIndex = ObscuredPrefs.GetInt("wmti");
			}
			else
			{
				CONTROLLER.myTeamIndex = 0;
			}
			SetTournamentPlay();
		}
		if (CONTROLLER.PlayModeSelected == 4)
		{
			outerPanel.SetActive(value: false);
			SOPanel.SetActive(value: true);
			if (ObscuredPrefs.HasKey("somti"))
			{
				string string2 = ObscuredPrefs.GetString("somti");
				string[] array2 = string2.Split("|"[0]);
				CONTROLLER.myTeamIndex = int.Parse(array2[0]);
				CONTROLLER.opponentTeamIndex = int.Parse(array2[1]);
			}
			oppTeamSelectionRightArrow();
			oppTeamSelectionLeftArrow();
			myTeamSelectionLeftButton();
			myTeamSelectionRightButton();
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			outerPanel.SetActive(value: false);
			SOPanel.SetActive(value: true);
			if (ObscuredPrefs.HasKey("scmti"))
			{
				string string3 = ObscuredPrefs.GetString("scmti");
				string[] array3 = string3.Split("|"[0]);
				CONTROLLER.myTeamIndex = int.Parse(array3[0]);
				CONTROLLER.opponentTeamIndex = int.Parse(array3[1]);
			}
			oppTeamSelectionRightArrow();
			oppTeamSelectionLeftArrow();
			myTeamSelectionLeftButton();
			myTeamSelectionRightButton();
		}
		else if (CONTROLLER.PlayModeSelected == 7)
		{
			outerPanel.SetActive(value: true);
			panel1.SetActive(value: true);
			if (ObscuredPrefs.HasKey("TMTI"))
			{
				string string4 = ObscuredPrefs.GetString("TMTI");
				string[] array4 = string4.Split("|"[0]);
				CONTROLLER.myTeamIndex = int.Parse(array4[0]);
				CONTROLLER.opponentTeamIndex = int.Parse(array4[1]);
			}
			else
			{
				CONTROLLER.myTeamIndex = 0;
				CONTROLLER.opponentTeamIndex = 1;
			}
			if (CONTROLLER.myTeamIndex == CONTROLLER.opponentTeamIndex)
			{
				if (CONTROLLER.opponentTeamIndex < CONTROLLER.TeamList.Length && CONTROLLER.opponentTeamIndex != 0)
				{
					CONTROLLER.opponentTeamIndex--;
				}
				else if (CONTROLLER.opponentTeamIndex == 0)
				{
					CONTROLLER.opponentTeamIndex++;
				}
			}
			SetQuickPlay();
		}
		else
		{
			outerPanel.SetActive(value: true);
			SOPanel.SetActive(value: false);
		}
		CONTROLLER.difficultyMode = "medium";
		setDifficulty();
		Holder.SetActive(value: true);
		CONTROLLER.CurrentMenu = "teamselection";
		CONTROLLER.menuTitle = "TEAM SELECTION";
		Singleton<GameModeTWO>.instance.updateTitle(_modeSelected: true);
	}

	public void hideMe()
	{
		Holder.SetActive(value: false);
	}
}