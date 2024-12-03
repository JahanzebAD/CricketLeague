using UnityEngine;
using UnityEngine.UI;

public class CTMenuScreen : Singleton<CTMenuScreen>
{
	public Sprite enableBG;

	public Sprite disableBG;

	public Sprite progressBar;

	public Sprite lockBtn;

	public Sprite playBtn;

	public Sprite[] progress;

	public CTChallenges[] challenges;

	public GameObject holder;

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = GoBack;
		CONTROLLER.PlayModeSelected = 5;
		CONTROLLER.pageName = "CTMenu";
		ValidateLockedLevels();
		holder.SetActive(value: true);
	}

	public void ValidateLockedLevels()
	{
		Singleton<LoadPlayerPrefs>.instance.GetChaseTargetLevelDetails();
		float num = (float)CONTROLLER.CTSubLevelCompleted / 5f;
		for (int i = 0; i < CONTROLLER.TargetRangeArray.Length; i++)
		{
			challenges[i].lockButton.gameObject.SetActive(value: true);
			challenges[i].continueBtn.gameObject.SetActive(value: false);
			challenges[i].GetComponent<Image>().color = new Color32(0, 35, 94, byte.MaxValue);

			challenges[i].title.color = Color.white;
			challenges[i].progressBar.SetActive(value: false);
			if (i <= CONTROLLER.CTLevelCompleted)
			{
				challenges[i].lockButton.gameObject.SetActive(value: false);
				challenges[i].progressBar.SetActive(value: true);
				challenges[i].continueBtn.gameObject.SetActive(value: true);
				challenges[i].GetComponent<Image>().color = new Color32(185, 209, 234, byte.MaxValue);
				challenges[i].title.color = Color.white;

			}
		}
		for (int i = 0; i < CONTROLLER.CTLevelCompleted; i++)
		{
			if (CONTROLLER.MainLevelCompletedArray[i] == 1)
			{
				for (i = 0; i < 5; i++)
				{
					challenges[CONTROLLER.CTLevelCompleted].progress[i].sprite = playBtn;
				}
			}
		}
		if (CONTROLLER.CTLevelCompleted >= CONTROLLER.TargetRangeArray.Length)
		{
			return;
		}
		float num2 = num * 100f;
		if (num2 > 0f && num2 <= 22f)
		{
			challenges[CONTROLLER.CTLevelCompleted].progress[0].sprite = playBtn;
			for (int i = 1; i < 5; i++)
			{
				challenges[CONTROLLER.CTLevelCompleted].progress[i].sprite = lockBtn;
			}
		}
		else if (num2 > 20f && num2 <= 42f)
		{
			for (int i = 0; i < 2; i++)
			{
				challenges[CONTROLLER.CTLevelCompleted].progress[i].sprite = playBtn;
			}
			for (int i = 2; i < 5; i++)
			{
				challenges[CONTROLLER.CTLevelCompleted].progress[i].sprite = lockBtn;
			}
		}
		else if (num2 > 40f && num2 <= 62f)
		{
			for (int i = 0; i < 3; i++)
			{
				challenges[CONTROLLER.CTLevelCompleted].progress[i].sprite = playBtn;
			}
			for (int i = 3; i < 5; i++)
			{
				challenges[CONTROLLER.CTLevelCompleted].progress[i].sprite = lockBtn;
			}
		}
		else if (num2 > 60f && num2 <= 82f)
		{
			for (int i = 0; i < 4; i++)
			{
				challenges[CONTROLLER.CTLevelCompleted].progress[i].sprite = playBtn;
			}
			challenges[CONTROLLER.CTLevelCompleted].progress[4].sprite = lockBtn;
		}
		else if (num2 == 0f)
		{
			for (int i = 0; i < 5; i++)
			{
				challenges[CONTROLLER.CTLevelCompleted].progress[i].sprite = lockBtn;
			}
		}
		else
		{
			for (int i = 0; i < 5; i++)
			{
				challenges[CONTROLLER.CTLevelCompleted].progress[i].sprite = playBtn;
			}
		}
	}

	public void LevelSelected(int index)
	{
		if (!challenges[index].lockButton.gameObject.activeInHierarchy)
		{
			CONTROLLER.CTLevelId = index;
			CONTROLLER.CTLevelCompleted = index;
			CONTROLLER.CTCurrentPlayingMainLevel = index;
			ShowCTLevelScreen();
		}
		//GoogleAllAds.Instance.InterstitialAdPlz();
	}

	public void GoBack()
	{
		holder.SetActive(value: false);
		Singleton<GameModeTWO>.instance.showMe();
	}

	private void HideThis()
	{
		holder.SetActive(value: false);
	}

	private void ShowCTLevelScreen()
	{
		Singleton<CTLevelSelectionPage>.instance.ShowMe();
		HideThis();
	}
}
