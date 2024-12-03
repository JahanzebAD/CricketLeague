using UnityEngine;
using UnityEngine.UI;

public class CTLevelSelectionPage : Singleton<CTLevelSelectionPage>
{
	public Button[] playBtn;

	public Button[] lockBtn;

	public Button[] replayBtn;

	public Sprite lockSprite;

	public Sprite unlockSprite;

	public Sprite replaySprite;

	public Sprite enableBG;

	public Sprite disableBG;

	public GameObject[] challenges;

	public GameObject[] star;

	public GameObject[] entryFees;

	public GameObject holder;

	public Text LevelTitle;

	public Text[] RangeText;

	public Text[] target;

	public Text[] runs;

	public GameObject[] tryAgain;

	private string[] levelNames = new string[6]
	{
		LocalizationData.instance.getText(105) + " (3 " + LocalizationData.instance.getText(532) + ")",
		LocalizationData.instance.getText(106) + " (5 " + LocalizationData.instance.getText(532) + ")",
		LocalizationData.instance.getText(107) + " (10 " + LocalizationData.instance.getText(532) + ")",
		LocalizationData.instance.getText(108) + " (20 " + LocalizationData.instance.getText(532) + ")",
		LocalizationData.instance.getText(109) + " (30 " + LocalizationData.instance.getText(532) + ")",
		LocalizationData.instance.getText(110) + " (50 " + LocalizationData.instance.getText(532) + ")"
	};

	private string[] NamesToDisplayInGoogle = new string[6] { "CubClass", "YoungLion", "PowerPlayer", "SuperPro", "Champion", "PrideOfChepauk" };

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = GoBack;
		CONTROLLER.pageName = "CTLevels";
		Singleton<SuperChaseCTPanelTransition>.instance.SubClassPanelTransition();
		ValidateLockedLevels();
		holder.SetActive(value: true);
	}

	private void SetOverRange()
	{
		string text = CONTROLLER.TargetRangeArray[CONTROLLER.CTCurrentPlayingMainLevel];
		string[] array = text.Split("$"[0]);
		for (int i = 0; i < RangeText.Length; i++)
		{
			RangeText[i].text = array[i];
			string[] array2 = array[i].Split("-"[0]);
			string[] array3 = array[i].Split("-"[0]);
			CONTROLLER.StartRangeArray[i] = int.Parse(array2[0]);
			CONTROLLER.EndRangeArray[i] = int.Parse(array3[1]);
		}
	}

	private void ValidateLockedLevels()
	{
		Singleton<LoadPlayerPrefs>.instance.GetChaseTargetLevelDetails();
		CONTROLLER.CTLevelId = CONTROLLER.CTCurrentPlayingMainLevel;
		CONTROLLER.CTLevelCompleted = CONTROLLER.CTCurrentPlayingMainLevel;
		SetOverRange();
		LevelTitle.text = levelNames[CONTROLLER.CTCurrentPlayingMainLevel];
		if (LocalizationData.instance.languageIndex != 1)
		{
			LevelTitle.GetComponent<Text>().fontStyle = FontStyle.Normal;
		}
		for (int i = 0; i < lockBtn.Length; i++)
		{
			tryAgain[i].SetActive(value: false);
			if (CONTROLLER.MainLevelCompletedArray[CONTROLLER.CTLevelId] == 0 && i > CONTROLLER.CTSubLevelCompleted)
			{
				lockBtn[i].gameObject.SetActive(value: true);
				playBtn[i].gameObject.SetActive(value: false);
				entryFees[i].SetActive(value: false);
				replayBtn[i].gameObject.SetActive(value: false);
				challenges[i].GetComponent<Image>().color = new Color32(0, 35, 94, byte.MaxValue);

				RangeText[i].color = Color.white;
				target[i].color = Color.white;
				runs[i].color = Color.white;
				star[i].SetActive(value: false);
			}
			else if (CONTROLLER.MainLevelCompletedArray[CONTROLLER.CTLevelId] == 0 && i <= CONTROLLER.CTSubLevelCompleted)
			{
				lockBtn[i].gameObject.SetActive(value: false);
				playBtn[i].gameObject.SetActive(value: false);
				entryFees[i].SetActive(value: false);
				replayBtn[i].gameObject.SetActive(value: true);
				star[i].SetActive(value: true);
				if (i == CONTROLLER.CTSubLevelCompleted)
				{
					star[i].SetActive(value: false);
					lockBtn[i].gameObject.SetActive(value: false);
					playBtn[i].gameObject.SetActive(value: true);
					entryFees[i].SetActive(value: false);
					replayBtn[i].gameObject.SetActive(value: false);
					if (CONTROLLER.CTWonMatch == 0)
					{
						tryAgain[i].SetActive(value: true);
						entryFees[i].SetActive(value: false);
						lockBtn[i].gameObject.SetActive(value: false);
						playBtn[i].gameObject.SetActive(value: false);
						replayBtn[i].gameObject.SetActive(value: true);
					}
				}
			}
			else if (CONTROLLER.MainLevelCompletedArray[CONTROLLER.CTLevelId] == 1)
			{
				lockBtn[i].gameObject.SetActive(value: false);
				playBtn[i].gameObject.SetActive(value: false);
				entryFees[i].SetActive(value: false);
				replayBtn[i].gameObject.SetActive(value: true);
				star[i].SetActive(value: true);
				challenges[i].GetComponent<Image>().color = new Color32(185, 209, 234, byte.MaxValue);
				RangeText[i].color = Color.white;
                target[i].color = Color.white;
                runs[i].color = Color.white;
            }
		}
	}

	public void ClearLevel()
	{
		CONTROLLER.CTLevelId = CONTROLLER.CTCurrentPlayingMainLevel;
		CONTROLLER.CTLevelCompleted = CONTROLLER.CTCurrentPlayingMainLevel;
		if (CONTROLLER.MainLevelCompletedArray[CONTROLLER.CTLevelId] == 0 && CONTROLLER.CTLevelId < 5)
		{
			CONTROLLER.CTSubLevelCompleted++;
			CONTROLLER.CTLevelId++;
			if (CONTROLLER.CTLevelId == 4)
			{
				CONTROLLER.MainLevelCompletedArray[CONTROLLER.CTLevelId] = 1;
				Singleton<CTMenuScreen>.instance.ValidateLockedLevels();
			}
			ValidateLockedLevels();
		}
	}

	public void LevelSelected(int index)
	{
		PlayerPrefs.SetInt("userLastPlayed", 5);
		FBAppEvents.instance.LogSuperChaseEvent("SC_Played", "Start");
		//FirebaseAnalyticsManager.instance.logEvent("SC_Played", "SuperChase", CONTROLLER.userID);
		if (!lockBtn[index].gameObject.activeSelf)
		{
			CONTROLLER.CTSubLevelId = index;
			CONTROLLER.CTSubLevelCompleted = index;
			CONTROLLER.totalOvers = CONTROLLER.Overs[CONTROLLER.CTLevelId];
			CONTROLLER.oversSelectedIndex = CONTROLLER.CTLevelId;
			//if (index == 0)
			//{
			//	FirebaseAnalyticsManager.instance.logEvent("Arcademode", new string[2]
			//	{
			//		"ArcademodeAction",
			//		"SC_" + CONTROLLER.Overs[CONTROLLER.CTCurrentPlayingMainLevel] + "_1_Start"
			//	});
			//}
			CONTROLLER.GetRandomScoreForOppTeam(CONTROLLER.StartRangeArray[CONTROLLER.CTSubLevelId], CONTROLLER.EndRangeArray[CONTROLLER.CTSubLevelId]);
			CONTROLLER.InningsCompleted = false;
			HideThis();
			Singleton<LoadPlayerPrefs>.instance.SetArrayForChaseTarget();
			CONTROLLER.BattingTeamIndex = CONTROLLER.myTeamIndex;
			CONTROLLER.BowlingTeamIndex = CONTROLLER.opponentTeamIndex;
			Singleton<SquadPageTWO>.instance.Continue();
		}
		//GoogleAllAds.Instance.InterstitialAdPlz();
	}

	public void GoBack()
	{
		holder.SetActive(value: false);
		Singleton<CTMenuScreen>.instance.ShowMe();
		//GoogleAllAds.Instance.InterstitialAdPlz();
	}

	public void HideThis()
	{
		holder.SetActive(value: false);
	}
}
