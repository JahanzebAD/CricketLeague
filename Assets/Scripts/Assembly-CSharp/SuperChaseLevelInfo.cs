using UnityEngine;
using UnityEngine.UI;

public class SuperChaseLevelInfo : Singleton<SuperChaseLevelInfo>
{
	public GameObject holder;

	public Text ChallengeTitle;

	public Text LevelTitle;

	public Text BowlerType;

	private string[] LevelDescriptionArray = new string[6]
	{
		LocalizationData.instance.getText(105),
		LocalizationData.instance.getText(106),
		LocalizationData.instance.getText(107),
		LocalizationData.instance.getText(108),
		LocalizationData.instance.getText(109),
		LocalizationData.instance.getText(110)
	};

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && holder.activeInHierarchy)
		{
			HideMe();
		}
	}

	private void ValidateThisLevel()
	{
		int num = CONTROLLER.TargetToChase - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores;
		int num2 = CONTROLLER.totalOvers * 6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls;
		ChallengeTitle.text = LevelDescriptionArray[CONTROLLER.CTCurrentPlayingMainLevel];
		LevelTitle.text = LocalizationData.instance.getText(278);
		LevelTitle.text = ReplaceText(LevelTitle.text, CONTROLLER.TargetToChase.ToString(), string.Empty);
		BowlerType.text = LocalizationData.instance.getText(266);
		BowlerType.text = ReplaceText(BowlerType.text, CONTROLLER.totalOvers.ToString(), string.Empty);
	}

	private string ReplaceText(string original, string replace1, string replace2)
	{
		string text = string.Empty;
		Debug.Log(original + " " + replace1 + " " + replace2 + " " + text);
		if (original.Contains("#"))
		{
			text = original.Replace("#", replace1);
		}
		Debug.Log(original + " " + replace1 + " " + replace2 + " " + text);
		if (text.Contains("$"))
		{
			text = text.Replace("$", replace2);
		}
		Debug.Log(original + " " + replace1 + " " + replace2 + " " + text);
		return text;
	}

	public void HideMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = OpenPause;
		Singleton<NavigationBack>.instance.disableDeviceBack = false;
		CONTROLLER.PlayModeSelected = 5;
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
