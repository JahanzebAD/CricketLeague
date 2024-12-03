using UnityEngine;

public class QuitConfirm : Singleton<QuitConfirm>
{
	private string prevPage = string.Empty;

	public GameObject holder;

	protected void Start()
	{
		holder.SetActive(value: false);
	}

	public void CloseErrorMsg(int index)
	{
		holder.SetActive(value: false);
		switch (index)
		{
		case 1:
			CONTROLLER.screenToDisplay = "landingPage";
			Singleton<PauseGameScreen>.instance.hideAll();
			Singleton<PauseGameScreen>.instance.midPageGO.SetActive(value: false);
			Time.timeScale = 1f;
			Singleton<GameModel>.instance.GameQuitted();
			break;
		case 0:
			Singleton<GameModel>.instance.AgainToGamePauseScreen();
			Singleton<PauseGameScreen>.instance.Hide(boolean: false);
			break;
		}
	}

	//public void FireBaseEvents()
	//{
	//	if (CONTROLLER.PlayModeSelected == 0)
	//	{
	//		Singleton<Firebase_Events>.instance.Firebase_QP_Quit();
	//	}
	//	else if (CONTROLLER.PlayModeSelected == 1)
	//	{
	//		Singleton<Firebase_Events>.instance.Firebase_T20_Quit();
	//	}
	//	else if (CONTROLLER.PlayModeSelected == 2)
	//	{
	//		Singleton<Firebase_Events>.instance.Firebase_PRL_Quit();
	//	}
	//	else if (CONTROLLER.PlayModeSelected == 3)
	//	{
	//		Singleton<Firebase_Events>.instance.Firebase_WC_Quit();
	//	}
	//	else if (CONTROLLER.PlayModeSelected == 4)
	//	{
	//		Singleton<Firebase_Events>.instance.Firebase_SO_Quit();
	//	}
	//	else if (CONTROLLER.PlayModeSelected == 5)
	//	{
	//		Singleton<Firebase_Events>.instance.Firebase_SC_Quit();
	//	}
	//	else if (CONTROLLER.PlayModeSelected == 7)
	//	{
	//		Singleton<Firebase_Events>.instance.Firebase_TC_Quit();
	//	}
	//}

	public void showMe()
	{
		CONTROLLER.pageName = "quitGame";
		prevPage = CONTROLLER.CurrentMenu;
		holder.SetActive(value: true);
		CONTROLLER.CurrentMenu = "quitconfirm";
	}

	public void hideMe()
	{
		Singleton<GameModel>.instance.AgainToGamePauseScreen();
		Singleton<PauseGameScreen>.instance.Hide(boolean: false);
		holder.SetActive(value: false);
	}
}
