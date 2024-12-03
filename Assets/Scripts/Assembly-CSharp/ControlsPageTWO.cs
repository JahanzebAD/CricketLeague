using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlsPageTWO : Singleton<ControlsPageTWO>
{
	public GameObject Holder;

	public Camera renderCamera;

	public Text instructionText;

	public GameObject battingControlGO;

	public GameObject bowlingControlGO;

	public GameObject instructionsGO;

	public GameObject continuebtn;

	protected void Start()
	{
		hideMe();
	}

	private void addEventListener()
	{
	}

	public void showMe()
	{
		continuebtn.SetActive(value: true);
		tabSelected(0);
		CONTROLLER.menuTitle = "CONTROLS";
		if (Singleton<GameModeTWO>.instance != null)
		{
			Singleton<GameModeTWO>.instance.updateTitle(_modeSelected: false);
		}
		instructionText.text = "INSTRUCTIONS";
		Holder.SetActive(value: true);
		CONTROLLER.CurrentMenu = "controls";
		
	}

	public void hideMe()
	{
		Holder.SetActive(value: false);
	}

	private void showBattingControls()
	{
		instructionText.text = "INSTRUCTIONS";
		battingControlGO.SetActive(value: true);
		bowlingControlGO.SetActive(value: false);
		instructionsGO.SetActive(value: false);
	}

	private void showBowlingControls()
	{
		battingControlGO.SetActive(value: false);
		bowlingControlGO.SetActive(value: true);
		instructionsGO.SetActive(value: false);
	}

	private void showInstructions()
	{
		battingControlGO.SetActive(value: false);
		bowlingControlGO.SetActive(value: false);
		instructionsGO.SetActive(value: true);
		CONTROLLER.menuTitle = "INSTRUCTIONS";
		if (Singleton<GameModeTWO>.instance != null)
		{
			Singleton<GameModeTWO>.instance.updateTitle(_modeSelected: false);
		}
		instructionText.text = "CONTROLS";
		
	}

	public void tabSelected(int index)
	{
		switch (index)
		{
		}
	}

	public void backSelected(int index)
	{
		switch (index)
		{
		case 0:
			hideMe();
			if (SceneManager.GetActiveScene().name == "MainMenu")
			{
				Singleton<GameModeTWO>.instance.showMe();
				Singleton<GameModeTWO>.instance.displayGameMode(_bool: true);
			}
			else if (SceneManager.GetActiveScene().name == "Ground")
			{
				Singleton<GameModel>.instance.AgainToGamePauseScreen();
			}
			break;
		case 1:
			if (instructionText.text == "INSTRUCTIONS")
			{
				showInstructions();
			}
			else
			{
				showBattingControls();
			}
			break;
		}
	}
}
