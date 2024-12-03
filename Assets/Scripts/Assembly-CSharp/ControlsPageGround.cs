using UnityEngine;
using UnityEngine.UI;

public class ControlsPageGround : Singleton<ControlsPageGround>
{
	public Button battingBtn;

	public Button bowlingBtn;

	public Sprite[] blueStrip;

	public GameObject holder;

	public GameObject battingControlGO;

	public GameObject bowlingControlGO;

	public GameObject controlTabGO;

	public GameObject instructionsGO;

	public GameObject scrollList;

	private Vector3 startPos;

	protected void Start()
	{
		startPos = scrollList.transform.position;
		hideMe();
	}

	private void addEventListener()
	{
	}

	public void showMe()
	{
		scrollList.transform.position = startPos;
		showBattingControls();
		CONTROLLER.menuTitle = "CONTROLS";
		holder.SetActive(value: true);
		CONTROLLER.CurrentMenu = "controls";
		
	}

	public void hideMe()
	{
		holder.SetActive(value: false);
	}

	public void showBattingControls()
	{
		bowlingBtn.image.sprite = blueStrip[0];
		battingBtn.image.sprite = blueStrip[1];
		controlTabGO.SetActive(value: true);
		battingControlGO.SetActive(value: true);
		bowlingControlGO.SetActive(value: false);
		instructionsGO.SetActive(value: false);
	}

	public void showBowlingControls()
	{
		bowlingBtn.image.sprite = blueStrip[1];
		battingBtn.image.sprite = blueStrip[0];
		controlTabGO.SetActive(value: true);
		battingControlGO.SetActive(value: false);
		bowlingControlGO.SetActive(value: true);
		instructionsGO.SetActive(value: false);
	}

	public void showInstructions()
	{
		CONTROLLER.menuTitle = "INSTRUCTIONS";
		controlTabGO.SetActive(value: false);
		battingControlGO.SetActive(value: false);
		bowlingControlGO.SetActive(value: false);
		instructionsGO.SetActive(value: true);
		scrollList.transform.position = startPos;
		
	}

	public void backSelected(int index)
	{
		switch (index)
		{
		case 0:
			hideMe();
			Singleton<GameModel>.instance.AgainToGamePauseScreen();
			Singleton<PauseGameScreen>.instance.Hide(boolean: false);
			break;
		case 1:
			showMe();
			break;
		}
	}
}
