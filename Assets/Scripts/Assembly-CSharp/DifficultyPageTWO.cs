using UnityEngine;
using UnityEngine.UI;

public class DifficultyPageTWO : Singleton<DifficultyPageTWO>
{
	public GameObject Holder;

	public Camera renderCamera;

	public Image myTeamFlag;

	public Image oppTeamFlag;

	public GameObject continueBtn;

	public Text myTeamAbbr;

	public Text oppTeamAbbr;

	protected void Start()
	{
	}

	private void setDifficulty()
	{
		if (CONTROLLER.difficultyMode == "easy")
		{
			EasyButtonOnClicked();
		}
		else if (CONTROLLER.difficultyMode == "medium")
		{
			MediumButtonOnClicked();
		}
		else if (CONTROLLER.difficultyMode == "hard")
		{
			HardButtonOnClicked();
		}
		saveDifficulty();
	}

	private void saveDifficulty()
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			PlayerPrefs.SetString("exdiff", CONTROLLER.difficultyMode);
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			PlayerPrefs.SetString("tourdiff", CONTROLLER.difficultyMode);
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			PlayerPrefs.SetString("npldiff", CONTROLLER.difficultyMode);
		}
	}

	public void EasyButtonOnClicked()
	{
		CONTROLLER.difficultyMode = "easy";
		saveDifficulty();
	}

	public void MediumButtonOnClicked()
	{
		CONTROLLER.difficultyMode = "medium";
		saveDifficulty();
	}

	public void HardButtonOnClicked()
	{
		CONTROLLER.difficultyMode = "hard";
		saveDifficulty();
	}

	public void BackButtonOnClicked()
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			Singleton<TeamSelectionTWO>.instance.showMe();
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			Singleton<FixturesTWO>.instance.showMe();
		}
	}

	public void ContinueButtonOnClicked()
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			Singleton<TossPageTWO>.instance.showMe();
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			Singleton<TossPageTWO>.instance.showMe();
		}
	}

	private void setTitle()
	{
		CONTROLLER.menuTitle = "DIFFICULTY";
		Singleton<GameModeTWO>.instance.updateTitle(_modeSelected: true);
		CONTROLLER.CurrentMenu = "difficulty";
		string abbrevation = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation;
		myTeamFlag.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
		abbrevation = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation;
		oppTeamFlag.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
		myTeamAbbr.text = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation;
		oppTeamAbbr.text = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation;
		setDifficulty();
	}

	public void showMe()
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			if (PlayerPrefs.HasKey("exdiff"))
			{
				CONTROLLER.difficultyMode = PlayerPrefs.GetString("exdiff");
			}
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			if (PlayerPrefs.HasKey("tourdiff"))
			{
				CONTROLLER.difficultyMode = PlayerPrefs.GetString("tourdiff");
			}
		}
		else if (CONTROLLER.PlayModeSelected == 2 && PlayerPrefs.HasKey("npldiff"))
		{
			CONTROLLER.difficultyMode = PlayerPrefs.GetString("npldiff");
		}
		Holder.SetActive(value: true);
		continueBtn.SetActive(value: true);
		setTitle();
	}

	public void hideMe()
	{
		Holder.SetActive(value: false);
	}
}
