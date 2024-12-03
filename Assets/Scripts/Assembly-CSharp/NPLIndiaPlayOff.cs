using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class NPLIndiaPlayOff : Singleton<NPLIndiaPlayOff>
{
	public GameObject footer;

	public GameObject holder;

	public Text pageTitle;

	public Text[] _nameQualifier1;

	public Text[] _nameQualifier2;

	public Text[] _nameFinal;

	public Sprite TBD;

	public GameObject selectedFlag;

	public Image[] _flagQualifier1;

	public Image[] _flagQualifier2;

	public Image[] _flagFinal;

	public Image CupImg;

	public Sprite[] CupSprites;

	private string[] getQualify1Data;

	private string[] getQualify2Data;

	private string[] getFinalData;

	private string Round1 = string.Empty;

	private string Round2 = string.Empty;

	private string Final = string.Empty;

	protected void Start()
	{
		selectedFlag.gameObject.SetActive(value: false);
	}

	private void CheckForTeam()
	{
		if (CONTROLLER.NPLIndiaTournamentStage == 1)
		{
			if (CONTROLLER.NPLIndiaLeagueMatchIndex < getQualify1Data.Length)
			{
				string[] array = getQualify1Data[CONTROLLER.NPLIndiaLeagueMatchIndex].Split("-"[0]);
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
				CONTROLLER.NPLIndiaTournamentStage = 2;
				CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
			}
		}
		if (CONTROLLER.NPLIndiaTournamentStage == 2)
		{
			if (CONTROLLER.NPLIndiaLeagueMatchIndex < getQualify2Data.Length)
			{
				string[] array2 = getQualify2Data[CONTROLLER.NPLIndiaLeagueMatchIndex].Split("-"[0]);
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
				CONTROLLER.NPLIndiaTournamentStage = 3;
				CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
			}
		}
		if (CONTROLLER.NPLIndiaTournamentStage != 3)
		{
			return;
		}
		if (CONTROLLER.NPLIndiaLeagueMatchIndex < getFinalData.Length)
		{
			string[] array3 = getFinalData[CONTROLLER.NPLIndiaLeagueMatchIndex].Split("-"[0]);
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
			CONTROLLER.NPLIndiaTournamentStage = 4;
		}
	}

	public void PlaceSelectedImage()
	{
		if (CONTROLLER.NPLIndiaTournamentStage == 1)
		{
			Image[] flagQualifier = _flagQualifier1;
			foreach (Image image in flagQualifier)
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
		else if (CONTROLLER.NPLIndiaTournamentStage == 2)
		{
			Image[] flagQualifier2 = _flagQualifier2;
			foreach (Image image2 in flagQualifier2)
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
			if (CONTROLLER.NPLIndiaTournamentStage != 3)
			{
				return;
			}
			Image[] flagFinal = _flagFinal;
			foreach (Image image3 in flagFinal)
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
		//Discarded unreachable code: IL_025e
		int num = AutoGenerate(team1, team2);
		string empty = string.Empty;
		string final;
		if (CONTROLLER.NPLIndiaTournamentStage == 1)
		{
			if (CONTROLLER.NPLIndiaLeagueMatchIndex == 0)
			{
				string[] array = Final.Split("|"[0]);
				if (array.Length <= 1)
				{
					final = Final;
					Final = final + string.Empty + num + "-";
				}
				SetFinalMatches();
				array = Round2.Split("|"[0]);
				if (array.Length <= 1)
				{
					if (num == team1)
					{
						final = Round2;
						Round2 = final + string.Empty + team2 + "-";
					}
					else if (num == team2)
					{
						final = Round2;
						Round2 = final + string.Empty + team1 + "-";
					}
				}
				SetQualify2Matches();
			}
			else
			{
				string[] array = Round2.Split("|"[0]);
				if (array.Length <= 1)
				{
					final = Round2;
					Round2 = final + string.Empty + num + "-";
				}
				string[] array2 = Round1.Split("|"[0]);
				string text = string.Empty + array2[0];
				string[] array3 = Final.Split("-"[0]);
				string[] array4 = text.Split("-"[0]);
				int num2 = 0;
				if (num2 < array4.Length)
				{
					if (array4[0] == array3[0])
					{
						Round2 = Round2 + string.Empty + array4[1];
					}
					else
					{
						Round2 = Round2 + string.Empty + array4[0];
					}
				}
				SetQualify2Matches();
			}
		}
		if (CONTROLLER.NPLIndiaTournamentStage == 2)
		{
			string[] array = Final.Split("|"[0]);
			empty = array[array.Length - 1];
			if (empty.Contains("-"))
			{
				Final = Final + string.Empty + num;
			}
			else
			{
				final = Final;
				Final = final + string.Empty + num + "-";
			}
			SetFinalMatches();
		}
		CONTROLLER.NPLIndiaLeagueMatchIndex++;
		if (CONTROLLER.NPLIndiaTournamentStage == 1)
		{
			if (CONTROLLER.NPLIndiaLeagueMatchIndex >= getQualify1Data.Length)
			{
				CONTROLLER.NPLIndiaTournamentStage = 2;
				CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
			}
		}
		else if (CONTROLLER.NPLIndiaTournamentStage == 2 && CONTROLLER.NPLIndiaLeagueMatchIndex >= getQualify2Data.Length)
		{
			CONTROLLER.NPLIndiaTournamentStage = 3;
			CONTROLLER.NPLIndiaLeagueMatchIndex = 0;
		}
		string text2 = CONTROLLER.NPLIndiaTournamentStage + "&" + CONTROLLER.myTeamIndex + "&" + CONTROLLER.NPLIndiaLeagueMatchIndex;
		final = text2;
		text2 = (CONTROLLER.NplIndiaData = final + "&" + Round1 + "#" + Round2 + "#" + Final);
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
		CheckForTeam();
	}

	private int AutoGenerate(int team1, int team2)
	{
		int num = int.Parse(CONTROLLER.TeamList[team1].rank);
		int num2 = int.Parse(CONTROLLER.TeamList[team2].rank);
		int num3 = 0;
		int max = 36;
		int num4 = Mathf.Abs(num - num2);
		if (CONTROLLER.NPLIndiaTournamentStage == 1)
		{
			num4 += 7;
		}
		else if (CONTROLLER.NPLIndiaTournamentStage == 2)
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

	private void SetQualify1Matches()
	{
		getQualify1Data = Round1.Split("|"[0]);
		for (int i = 0; i < getQualify1Data.Length; i++)
		{
			int num = -1;
			int num2 = -1;
			string[] array = getQualify1Data[i].Split("-"[0]);
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
				_flagQualifier1[i * 2].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
				_nameQualifier1[i * 2].text = CONTROLLER.TeamList[num].abbrevation;
			}
			if (num2 != -1)
			{
				string abbrevation2 = CONTROLLER.TeamList[num2].abbrevation;
				_flagQualifier1[i * 2 + 1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation2);
				_nameQualifier1[i * 2 + 1].text = CONTROLLER.TeamList[num2].abbrevation;
			}
		}
	}

	private void SetQualify2Matches()
	{
		getQualify2Data = Round2.Split("|"[0]);
		for (int i = 0; i < getQualify2Data.Length; i++)
		{
			int num = -1;
			int num2 = -1;
			string[] array = getQualify2Data[i].Split("-"[0]);
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
				_flagQualifier2[i * 2].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
				_nameQualifier2[i * 2].text = CONTROLLER.TeamList[num].abbrevation;
			}
			if (num2 != -1)
			{
				string abbrevation2 = CONTROLLER.TeamList[num2].abbrevation;
				_flagQualifier2[i * 2 + 1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation2);
				_nameQualifier2[i * 2 + 1].text = CONTROLLER.TeamList[num2].abbrevation;
			}
		}
	}

	private void SetFinalMatches()
	{
		getFinalData = Final.Split("|"[0]);
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
				_flagFinal[i * 2].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
				_nameFinal[i * 2].text = CONTROLLER.TeamList[num].abbrevation;
			}
			if (num2 != -1)
			{
				string abbrevation2 = CONTROLLER.TeamList[num2].abbrevation;
				_flagFinal[i * 2 + 1].sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation2);
				_nameFinal[i * 2 + 1].text = CONTROLLER.TeamList[num2].abbrevation;
			}
		}
	}

	public void ContinueSelected()
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
			Singleton<NavigationBack>.instance.deviceBack = null;
			Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
		}
		HideMe();
	}

	private void DrawFixtures()
	{
		int num = 0;
		string[] array = CONTROLLER.NplIndiaData.Split("&"[0]);
		for (num = 0; num < array.Length; num++)
		{
		}
		CONTROLLER.NPLIndiaTournamentStage = int.Parse(array[0]);
		CONTROLLER.myTeamIndex = int.Parse(array[1]);
		CONTROLLER.NPLIndiaLeagueMatchIndex = int.Parse(array[2]);
		string[] array2 = array[3].Split("#"[0]);
		for (num = 0; num < _flagQualifier1.Length; num++)
		{
			_flagQualifier1[num].sprite = TBD;
			_nameQualifier1[num].text = "TBD";
		}
		for (num = 0; num < _flagQualifier2.Length; num++)
		{
			_flagQualifier2[num].sprite = TBD;
			_nameQualifier2[num].text = "TBD";
		}
		for (num = 0; num < _flagFinal.Length; num++)
		{
			_flagFinal[num].sprite = TBD;
			_nameFinal[num].text = "TBD";
		}
		getQualify1Data = array2[0].Split("|"[0]);
		getQualify2Data = array2[1].Split("|"[0]);
		getFinalData = array2[2].Split("|"[0]);
		Round1 = string.Empty + array2[0];
		Round2 = string.Empty + array2[1];
		Final = string.Empty + array2[2];
		SetQualify1Matches();
		SetQualify2Matches();
		SetFinalMatches();
		CheckForTeam();
	}

	public void BackSelected()
	{
		holder.SetActive(value: false);
		Singleton<GameModeTWO>.instance.showMe();
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = BackSelected;
		CONTROLLER.pageName = "nplfixtures";
		Singleton<GameModeTWO>.instance.hideMe();
		Singleton<NPLIndiaLeague>.instance.ContinueBtn.SetActive(value: true);
		if (CONTROLLER.tournamentType == "NPL")
		{
			CupImg.sprite = CupSprites[0];
			pageTitle.text = LocalizationData.instance.getText(427) + " - " + LocalizationData.instance.getText(548);
		}
		else if (CONTROLLER.tournamentType == "PAK")
		{
			CupImg.sprite = CupSprites[1];
			pageTitle.text = LocalizationData.instance.getText(428) + " - " + LocalizationData.instance.getText(548);
		}
		else if (CONTROLLER.tournamentType == "AUS")
		{
			CupImg.sprite = CupSprites[2];
			pageTitle.text = LocalizationData.instance.getText(429) + " - " + LocalizationData.instance.getText(548);
		}
		Singleton<LoadPlayerPrefs>.instance.GetNPLIndiaTournamentList();
		Singleton<NplKnockoutTWOTransistionPanel>.instance.ResetTransistion();
		holder.SetActive(value: true);
		Singleton<NplKnockoutTWOTransistionPanel>.instance.PanelTransistion();
		CONTROLLER.CurrentMenu = "nplindiaplayoff";
		base.gameObject.SetActive(value: true);
		DrawFixtures();
	}

	public void HideMe()
	{
		holder.SetActive(value: false);
	}
}
