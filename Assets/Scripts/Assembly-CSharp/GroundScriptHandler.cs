using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GroundScriptHandler : MonoBehaviour
{
	private static GroundScriptHandler _instance;

	public Image BG;

	public GameObject GroundUI;

	public GameObject loadingScreen;

	public GameObject noInternetPopup;

	public GameObject serverDisconnectedPopup;

	public static GroundScriptHandler Instance => _instance;

	private void Start()
	{
		if (_instance == null)
		{
			_instance = this;
		}
	}

	public void ShowLoadingScreen()
	{
		GroundUI.SetActive(value: true);
		loadingScreen.SetActive(value: true);
	}

	public void HideLoadingScreen()
	{
		GroundUI.SetActive(value: false);
		loadingScreen.SetActive(value: false);
	}

	public void ShowNoInternetPopup()
	{
		GroundUI.SetActive(value: true);
		noInternetPopup.SetActive(value: true);
	}

	public void HideNoInternetPopup()
	{
		GroundUI.SetActive(value: false);
		serverDisconnectedPopup.SetActive(value: false);
		noInternetPopup.SetActive(value: false);
		if (CONTROLLER.PlayModeSelected == 6)
		{
			LoadMainMenuScene();
		}
	}

	public void ShowServerDisconnectedPopup()
	{
		CONTROLLER.canPressBackBtn = true;
		GroundUI.SetActive(value: true);
		noInternetPopup.SetActive(value: false);
		serverDisconnectedPopup.SetActive(value: true);
	}

	public void HideServerDisconnectedPopup()
	{
		BG.gameObject.SetActive(value: true);
		GroundUI.SetActive(value: false);
		serverDisconnectedPopup.SetActive(value: false);
		LoadMainMenuScene();
	}

	public void LoadMainMenuScene()
	{
		CONTROLLER.MPInningsCompleted = false;
		Singleton<GameModel>.instance.ResetCurrentMatchDetails();
		Singleton<GameModel>.instance.ResetVariables();
		Singleton<GameModel>.instance.ResetAllLocalVariables();
		CONTROLLER.PlayModeSelected = -1;
		ShowLoadingScreen();
		SceneManager.LoadSceneAsync("MainMenu");
	}
}
