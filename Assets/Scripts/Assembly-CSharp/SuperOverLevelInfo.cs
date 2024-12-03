using UnityEngine;
using UnityEngine.UI;

public class SuperOverLevelInfo : Singleton<SuperOverLevelInfo>
{
	public GameObject holder;

	public Text ChallengeTitle;

	public Text LevelTitle;

	public Text BowlerType;

	private string[] LevelDescriptionArray = new string[18]
	{
		LocalizationData.instance.getText(278),
		LocalizationData.instance.getText(279),
		LocalizationData.instance.getText(281),
		LocalizationData.instance.getText(280),
		LocalizationData.instance.getText(279),
		LocalizationData.instance.getText(278),
		LocalizationData.instance.getText(282),
		LocalizationData.instance.getText(279),
		LocalizationData.instance.getText(280),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527),
		LocalizationData.instance.getText(527)
	};

	private int[] LevelDescriptionArrayNumber = new int[18]
	{
		10, 3, 3, 3, 5, 25, 4, 6, 6, 27,
		24, 21, 18, 15, 12, 9, 6, 3
	};

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && holder.activeInHierarchy)
		{
			HideMe();
		}
	}

	private string ReplaceText(string original, string replace)
	{
		string result = string.Empty;
		Debug.Log(original + " " + replace + " ");
		if (original.Contains("#"))
		{
			result = original.Replace("#", replace);
		}
		return result;
	}

	private void ValidateThisLevel()
	{
		int num = CONTROLLER.LevelId % 2;
		ChallengeTitle.text = LocalizationData.instance.getText(277) + " " + (CONTROLLER.LevelId + 1);
		if (CONTROLLER.SuperOverMode == "bat")
		{
			LevelTitle.text = string.Empty + LevelDescriptionArray[(int)Mathf.Floor(CONTROLLER.LevelId / 2)];
			LevelTitle.text = ReplaceText(LevelTitle.text, LevelDescriptionArrayNumber[(int)Mathf.Floor(CONTROLLER.LevelId / 2)].ToString());
			if (num == 0)
			{
				BowlerType.text = LocalizationData.instance.getText(284);
			}
			else
			{
				BowlerType.text = LocalizationData.instance.getText(283);
			}
		}
		else
		{
			LevelTitle.text = string.Empty + LevelDescriptionArray[CONTROLLER.LevelId + 9];
			LevelTitle.text = ReplaceText(LevelTitle.text, LevelDescriptionArrayNumber[CONTROLLER.LevelId + 9].ToString());
			BowlerType.text = string.Empty;
		}
	}

	public void HideMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = OpenPause;
		Singleton<NavigationBack>.instance.disableDeviceBack = false;
		Singleton<Scoreboard>.instance.ShowChallengeTitle();
		CONTROLLER.PlayModeSelected = 4;
		AutoSave.DeleteFile();
		CONTROLLER.NewInnings = true;
		Singleton<Scoreboard>.instance.NewOver();
		Singleton<Scoreboard>.instance.UpdateScoreCard();
		Singleton<GroundController>.instance.ResetAll();
		Singleton<GameModel>.instance.ResetVariables();
		CONTROLLER.GameStartsFromSave = false;
		Singleton<GameModel>.instance.levelCompleted = false;
		CONTROLLER.NewInnings = true;
		CONTROLLER.InningsCompleted = false;
		holder.SetActive(value: false);
		Singleton<GameModel>.instance.introCompleted();
		Singleton<StandbyCam>.instance.PauseTween();
	}

	private void OpenPause()
	{
		if (Singleton<Scoreboard>.instance.pauseBtn.gameObject.activeInHierarchy && Singleton<Scoreboard>.instance.pauseBtn.enabled)
		{
			Singleton<PauseGameScreen>.instance.Hide(boolean: false);
		}
	}

	public void ShowMe()
	{
		Singleton<StandbyCam>.instance.RotateStandbyCam();
		ValidateThisLevel();
		holder.SetActive(value: true);
	}
}
