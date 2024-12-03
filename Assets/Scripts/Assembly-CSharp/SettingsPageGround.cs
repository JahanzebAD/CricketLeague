using UnityEngine;
using UnityEngine.UI;

public class SettingsPageGround : Singleton<SettingsPageGround>
{
	private LoadPlayerPrefs LoadPlayerPrefsScript;

	public GameObject holder;

	public Button[] soundBtns;

	public Sprite[] btnSprites;

	public Slider BGMSlider;

	public Slider SFXSlider;

	protected void Start()
	{
		LoadPlayerPrefsScript = GameObject.Find("LoadPlayerPrefs").GetComponent<LoadPlayerPrefs>();
		setSettingsPage();
		addEventListener();
		hideMe();
	}

	private void addEventListener()
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
			soundBtns[0].image.sprite = btnSprites[1];
		}
		else
		{
			soundBtns[0].image.sprite = btnSprites[0];
		}
		if (CONTROLLER.ambientVal == 1)
		{
			soundBtns[1].image.sprite = btnSprites[1];
		}
		else
		{
			soundBtns[1].image.sprite = btnSprites[0];
		}
		if (CONTROLLER.tutorialToggle == 1)
		{
			soundBtns[2].image.sprite = btnSprites[1];
		}
		else
		{
			soundBtns[2].image.sprite = btnSprites[0];
		}
		setSliderPos();
	}

	public void soundClicked(int index)
	{
		switch (index)
		{
		case 0:
			if (soundBtns[0].image.sprite == btnSprites[1])
			{
				CONTROLLER.bgMusicVal = 0;
				break;
			}
			CONTROLLER.bgMusicVal = 1;
			CONTROLLER.sndController.PlayButtonSnd();
			break;
		case 1:
			if (soundBtns[1].image.sprite == btnSprites[1])
			{
				CONTROLLER.ambientVal = 0;
				break;
			}
			CONTROLLER.ambientVal = 1;
			CONTROLLER.sndController.PlayButtonSnd();
			break;
		case 2:
			if (soundBtns[2].image.sprite == btnSprites[1])
			{
				CONTROLLER.tutorialToggle = 0;
				break;
			}
			CONTROLLER.tutorialToggle = 1;
			CONTROLLER.sndController.PlayButtonSnd();
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
		hideMe();
		Singleton<GameModel>.instance.AgainToGamePauseScreen();
		Singleton<PauseGameScreen>.instance.Hide(boolean: false);
	}

	public void showMe()
	{
		CONTROLLER.CurrentMenu = "settings";
		holder.SetActive(value: true);
		setSliderPos();
		
	}

	public void hideMe()
	{
		SavePlayerPrefs.SetSettingsList();
		holder.SetActive(value: false);
	}
}
