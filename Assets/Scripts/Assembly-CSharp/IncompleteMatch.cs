using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class IncompleteMatch : Singleton<IncompleteMatch>
{
	public GameObject Holder;

	protected void Start()
	{
		Holder.SetActive(value: false);
	}

	public void NoButton()
	{
		//if (CONTROLLER.PlayModeSelected == 0)
		//{
		//	Singleton<Firebase_Events>.instance.Firebase_QP_Quit();
		//}
		//else if (CONTROLLER.PlayModeSelected == 4)
		//{
		//	Singleton<Firebase_Events>.instance.Firebase_SO_Quit();
		//}
		//else if (CONTROLLER.PlayModeSelected == 5)
		//{
		//	Singleton<Firebase_Events>.instance.Firebase_SC_Quit();
		//}
		ObscuredPrefs.SetInt("QPPaid", 0);
		Singleton<Popups>.instance.HideMe();
		CONTROLLER.GameStartsFromSave = false;
		CONTROLLER.isFreeHitBall = false;
		AutoSave.DeleteFile();
		if (CONTROLLER.PlayModeSelected <= 3 || CONTROLLER.PlayModeSelected == 7)
		{
			Singleton<EntryFeesAndRewards>.instance.ShowMe();
			//if (CONTROLLER.PlayModeSelected == 7)
			//{
			//	Singleton<Firebase_Events>.instance.Firebase_TC_Quit();
			//}
			Singleton<GameModeTWO>.instance.hideMe();
		}
		else if (CONTROLLER.PlayModeSelected == 4)
		{
			Singleton<TeamSelectionTWO>.instance.showMe();
			Singleton<GameModeTWO>.instance.hideMe();
			CONTROLLER.pageName = "teamSelection";
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			Singleton<TeamSelectionTWO>.instance.showMe();
			Singleton<GameModeTWO>.instance.hideMe();
			CONTROLLER.pageName = "teamSelection";
		}
		else
		{
			Singleton<GameModeTWO>.instance.showMe();
			CONTROLLER.pageName = "landingPage";
		}
	}

	public void YesButton()
	{
		Holder.SetActive(value: false);
		Singleton<Popups>.instance.HideMe();
		Singleton<LoadingPanelTransition>.instance.PanelTransition();
		if (CONTROLLER.PlayModeSelected == 0)
		{
			//FirebaseAnalyticsManager.instance.logEvent("QP_Continue", "QuickPlay", CONTROLLER.userID);
			CONTROLLER.GameStartsFromSave = true;
			AutoSave.LoadGame();
			CONTROLLER.SceneIsLoading = true;
			Singleton<GameModeTWO>.instance.hideMe();
			CONTROLLER.CurrentMenu = string.Empty;
			Singleton<TossPageTWO>.instance.Continue();
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			//FirebaseAnalyticsManager.instance.logEvent("T20_Continue", "T20WorldCup", CONTROLLER.userID);
			Singleton<NplGroupMatchesTWOPanelTransistion>.instance.ResetTransistion();
			Singleton<NPLIndiaLeague>.instance.ShowMe();
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			Singleton<NplGroupMatchesTWOPanelTransistion>.instance.ResetTransistion();
			Singleton<NPLIndiaLeague>.instance.ShowMe();
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			Singleton<WCTeamFixturesTWOPanelTransistion>.instance.ResetTransistion();
			Singleton<WorldCupLeague>.instance.ShowMe();
			Singleton<WCTeamFixturesTWOPanelTransistion>.instance.PanelTransistion();
			//FirebaseAnalyticsManager.instance.logEvent("WC_Continue", "WorldCup", CONTROLLER.userID);
		}
		else if (CONTROLLER.PlayModeSelected == 4)
		{
			ResumeSuperOverSavedGame();
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			ResumeSuperOverSavedGame();
		}
		else if (CONTROLLER.PlayModeSelected == 7)
		{
			//FirebaseAnalyticsManager.instance.logEvent("TC_Continue", "Start", CONTROLLER.userID);
			CONTROLLER.GameStartsFromSave = true;
			AutoSave.LoadGame();
			CONTROLLER.SceneIsLoading = true;
			Singleton<GameModeTWO>.instance.hideMe();
			CONTROLLER.CurrentMenu = string.Empty;
			Singleton<TossPageTWO>.instance.Continue();
		}
	}

	public void ResumeSuperOverSavedGame()
	{
		CONTROLLER.GameStartsFromSave = true;
		AutoSave.LoadGame();
		CONTROLLER.SceneIsLoading = true;
		Singleton<GameModeTWO>.instance.hideMe();
		CONTROLLER.CurrentMenu = string.Empty;
		Singleton<NavigationBack>.instance.deviceBack = null;
		Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
	}

	private IEnumerator loadPreloader()
	{
		GameObject prefabGO = Resources.Load("Prefabs/Preloader") as GameObject;
		GameObject tempGO = Object.Instantiate(prefabGO);
		tempGO.name = "Preloader";
		yield return new WaitForSeconds(1f);
		yield return 0;
	}

	public void CancelButton()
	{
		CONTROLLER.CurrentMenu = "landingpage";
		hideMe();
	}

	public void showMe()
	{
		CONTROLLER.PopupName = "incompletePopup1";
		Singleton<Popups>.instance.ShowMe();
		CONTROLLER.CurrentMenu = "unfinishedmatch";
	}

	public void hideMe()
	{
		Singleton<GameModeTWO>.instance.showMe();
		CONTROLLER.pageName = "landingPage";
		Holder.SetActive(value: false);
	}
}
