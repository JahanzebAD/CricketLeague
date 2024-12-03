using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPageTWO : Singleton<SettingsPageTWO>
{
	public GameObject Holder;

	public GameObject LocalizationHolder;

	public Text Loc_Heading1;

	public Text Loc_Heading2;

	public Text Loc_Heading3;

	private int LanguageWhenOpened;

	public Button[] LanguageButtons;

	public Button LocalizeOKButton;

	public Text CurrentLanguageName;

	private string[] languageCode = new string[9] { "evsjv", "ENGLISH", "gaujrataI", "fganh", "ಕನ\u0ccdನಡ\u0cbfಗ", "aebmfw", "ekjrh", "jkpo;", "త\u0c46ల\u0c41గ\u0c41" };

	public Button[] soundBtns;

	public Slider BGMSlider;

	public Slider SFXSlider;

	public GameObject bgmSlider;

	public GameObject sfxSlider;

	public Text[] heading;

	private string previousPage;

	protected void Start()
	{
		Singleton<LoadPlayerPrefs>.instance.getSettingsList();
		setSettingsPage();
		if (SceneManager.GetActiveScene().name == "Ground")
		{
			ValidateQualitySettings();
		}
		LocalizationData.instance.titleText = Loc_Heading1;
		LocalizationData.instance.noteText = Loc_Heading2;
		LocalizationData.instance.OkText = Loc_Heading3;
	}

	public void SelectingCurrentLanguage()
	{
		for (int i = 0; i < Singleton<SettingsPageTWO>.instance.LanguageButtons.Length; i++)
		{
			if (LocalizationData.instance.languageIndex == i)
			{
				Singleton<SettingsPageTWO>.instance.LanguageButtons[i].GetComponent<Image>().color = Color.red;
				Singleton<SettingsPageTWO>.instance.LanguageButtons[i].GetComponentInChildren<Text>().color = Color.white;
			}
			else
			{
				Singleton<SettingsPageTWO>.instance.LanguageButtons[i].GetComponent<Image>().color = Color.yellow;
				Singleton<SettingsPageTWO>.instance.LanguageButtons[i].GetComponentInChildren<Text>().color = Color.black;
			}
		}
	}

	public void OpenLocalizationPopup()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = CloseLocalizationPopup;
		LanguageWhenOpened = LocalizationData.instance.languageIndex;
		Loc_Heading1.text = LocalizationData.instance.tempTitleText[LanguageWhenOpened];
		Loc_Heading2.text = LocalizationData.instance.tempNoteText[LanguageWhenOpened];
		Loc_Heading3.text = LocalizationData.instance.OKTextArray[LanguageWhenOpened];
		Text loc_Heading = Loc_Heading1;
		Font font = LocalizationData.instance.PreloaderFonts[LanguageWhenOpened];
		Loc_Heading3.font = font;
		font = font;
		Loc_Heading2.font = font;
		loc_Heading.font = font;
		LocalizationHolder.SetActive(value: true);
		Holder.SetActive(value: false);
		SelectingCurrentLanguage();
		for (int i = 0; i < LanguageButtons.Length; i++)
		{
			LanguageButtons[i].onClick.RemoveAllListeners();
		}
		LocalizeOKButton.onClick.RemoveAllListeners();
		LanguageButtons[0].onClick.AddListener(delegate
		{
			Singleton<LoadPlayerPrefs>.instance.OnLanguagesClicked(0);
		});
		LanguageButtons[1].onClick.AddListener(delegate
		{
			Singleton<LoadPlayerPrefs>.instance.OnLanguagesClicked(1);
		});
		LanguageButtons[2].onClick.AddListener(delegate
		{
			Singleton<LoadPlayerPrefs>.instance.OnLanguagesClicked(2);
		});
		LanguageButtons[3].onClick.AddListener(delegate
		{
			Singleton<LoadPlayerPrefs>.instance.OnLanguagesClicked(3);
		});
		LanguageButtons[4].onClick.AddListener(delegate
		{
			Singleton<LoadPlayerPrefs>.instance.OnLanguagesClicked(4);
		});
		LanguageButtons[5].onClick.AddListener(delegate
		{
			Singleton<LoadPlayerPrefs>.instance.OnLanguagesClicked(5);
		});
		LanguageButtons[6].onClick.AddListener(delegate
		{
			Singleton<LoadPlayerPrefs>.instance.OnLanguagesClicked(6);
		});
		LanguageButtons[7].onClick.AddListener(delegate
		{
			Singleton<LoadPlayerPrefs>.instance.OnLanguagesClicked(7);
		});
		LanguageButtons[8].onClick.AddListener(delegate
		{
			Singleton<LoadPlayerPrefs>.instance.OnLanguagesClicked(8);
		});
		LocalizeOKButton.onClick.AddListener(delegate
		{
			CloseLocalizationPopup();
		});
	}

	public void CloseLocalizationPopup()
	{
		Singleton<NavigationBack>.instance.deviceBack = hideMe;
		Holder.SetActive(value: true);
		setSettingsPage();
		LocalizationHolder.SetActive(value: false);
		CurrentLanguageName.text = languageCode[LocalizationData.instance.languageIndex];
		Debug.Log(LanguageWhenOpened + " " + LocalizationData.instance.languageIndex);
		if (LanguageWhenOpened == LocalizationData.instance.languageIndex)
		{
			return;
		}
		for (int i = 0; i < LocalizationData.instance.refList.Count; i++)
		{
			if (PlayerPrefs.HasKey(LocalizationData.instance.refList[i]))
			{
				PlayerPrefs.DeleteKey(LocalizationData.instance.refList[i]);
			}
		}
	}

	private void ValidateQualitySettings()
	{
	}

	public void setBGMSound(float value)
	{
		setBGMVolume(value);
	}

	public void setSFXSound(float value)
	{
		setSFXVolume(value);
	}

	private void setSFXVolume(float _bgVol)
	{
		CONTROLLER.sfxVolume = _bgVol;
		CONTROLLER.sndController.updateSFXVolume();
	}

	private void setBGMVolume(float _bgVol)
	{
		CONTROLLER.menuBgVolume = _bgVol;
		CONTROLLER.sndController.updateBGMVolume();
	}

	private void setSliderPos()
	{
		BGMSlider.value = CONTROLLER.menuBgVolume;
		SFXSlider.value = CONTROLLER.sfxVolume;
	}

	private void setSettingsPage()
	{
		if (CONTROLLER.bgMusicVal == 1)
		{
			soundBtns[1].gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
			heading[1].color = Color.white;
			soundBtns[0].gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
			heading[0].color = Color.black;
			bgmSlider.SetActive(value: true);
		}
		else
		{
			soundBtns[1].gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
			heading[1].color = Color.black;
			soundBtns[0].gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
			heading[0].color = Color.white;
			bgmSlider.SetActive(value: true);
		}
		if (CONTROLLER.ambientVal == 1)
		{
			soundBtns[3].gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
			heading[3].color = Color.white;
			soundBtns[2].gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
			heading[2].color = Color.black;
			sfxSlider.SetActive(value: true);
		}
		else
		{
			soundBtns[3].gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
			heading[3].color = Color.black;
			soundBtns[2].gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
			heading[2].color = Color.white;
			sfxSlider.SetActive(value: true);
		}
		if (CONTROLLER.tutorialToggle == 1)
		{
			soundBtns[5].gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
			heading[5].color = Color.white;
			soundBtns[4].gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
			heading[4].color = Color.black;
		}
		else
		{
			soundBtns[5].gameObject.GetComponent<CanvasRenderer>().SetAlpha(1f);
			heading[5].color = Color.black;
			soundBtns[4].gameObject.GetComponent<CanvasRenderer>().SetAlpha(0f);
			heading[4].color = Color.white;
		}
		setSliderPos();
	}

	public void soundClicked(int index)
	{
		switch (index)
		{
		case 0:
			if (soundBtns[0].gameObject.GetComponent<CanvasRenderer>().GetAlpha() == 1f)
			{
				CONTROLLER.bgMusicVal = 0;
			}
			else
			{
				CONTROLLER.bgMusicVal = 1;
			}
			break;
		case 1:
			if (soundBtns[2].gameObject.GetComponent<CanvasRenderer>().GetAlpha() == 1f)
			{
				CONTROLLER.ambientVal = 0;
			}
			else
			{
				CONTROLLER.ambientVal = 1;
			}
			break;
		case 2:
			if (soundBtns[4].gameObject.GetComponent<CanvasRenderer>().GetAlpha() == 1f)
			{
				CONTROLLER.tutorialToggle = 0;
			}
			else
			{
				CONTROLLER.tutorialToggle = 1;
			}
			break;
		case 3:
			if (soundBtns[6].gameObject.GetComponent<CanvasRenderer>().GetAlpha() == 1f)
			{
				CONTROLLER.PlayerMode = false;
			}
			else
			{
				CONTROLLER.PlayerMode = true;
			}
			break;
		}
		if (CONTROLLER.sndController != null)
		{
			CONTROLLER.sndController.bgMusicToggle();
			CONTROLLER.sndController.ambientToggle();
		}
		setSettingsPage();
	}

	public void backSelected()
	{
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			Singleton<GameModeTWO>.instance.ShowWithoutAnim();
			CONTROLLER.pageName = "landingPage";
			Singleton<GameModeTWO>.instance.displayGameMode(_bool: true);
		}
		else
		{
			hideMe();
			Singleton<GameModel>.instance.AgainToGamePauseScreen();
			Singleton<PauseGameScreen>.instance.Hide(boolean: false);
		}
	}

	public void ChangeQualitySettings(int index)
	{
		QualitySettings.SetQualityLevel(index, applyExpensiveChanges: true);
		ValidateQualitySettings();
	}

	public void showMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = hideMe;
		previousPage = CONTROLLER.CurrentMenu;
		CONTROLLER.CurrentMenu = "settings";
		Holder.SetActive(value: true);
		CurrentLanguageName.text = languageCode[LocalizationData.instance.languageIndex];
		Singleton<SettingsPageTWOPanelTransition>.instance.panelTransition();
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			Singleton<GameModeTWO>.instance.Holder.SetActive(value: false);
			CONTROLLER.pageName = "settings";
			if (CONTROLLER.canShowbannerMainmenu == 1)
			{
				//Singleton<AdIntegrate>.instance.ShowAd();
			}
		}
		else
		{
			CONTROLLER.pageName = "settingsGround";
		}
		setSettingsPage();
		
	}

	public void hideMe()
	{
		//Singleton<AdIntegrate>.instance.HideAd();
		SavePlayerPrefs.SetSettingsList();
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			Singleton<GameModeTWO>.instance.ShowWithoutAnim();
			CONTROLLER.pageName = "landingPage";
		}
		else if (previousPage == "pauseScreen")
		{
			Singleton<PauseGameScreen>.instance.Hide(boolean: false);
		}
		else if (previousPage == "SuperOverResult")
		{
			Singleton<SuperOverResult>.instance.ShowMe();
		}
		else if (previousPage == "SuperChaseResult")
		{
			Singleton<SuperChaseResult>.instance.ShowMe();
		}
		Holder.SetActive(value: false);
	}
}
