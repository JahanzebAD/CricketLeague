using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class SOLevelSelectionPage : Singleton<SOLevelSelectionPage>
{
	public ChallengeManager[] Challenges;

	public GameObject scrollView;

	public GameObject content;

	public GameObject holder;

	public Sprite playButton;

	public Sprite lockButton;

	public Sprite enableBG;

	public Sprite disableBG;

	public Sprite pass;

	public Sprite fail;

	public int[] Loc_LevelNumber = new int[18]
	{
		10, 3, 3, 3, 5, 25, 4, 6, 6, 27,
		24, 21, 18, 15, 12, 9, 6, 3
	};

	private Camera mainCamera;

	private string[] LevelDescriptionArray = new string[18]
	{
		"278", "279", "281", "280", "279", "278", "282", "279", "280", "527",
		"527", "527", "527", "527", "527", "527", "527", "527"
	};

	protected void Start()
	{
		CONTROLLER.LevelId = CONTROLLER.CurrentLevelCompleted;
	}

	private void ValidateLockedLevels()
	{
		int i;
		for (i = 0; i < CONTROLLER.totalLevels; i++)
		{
			Challenges[i].GetComponent<Image>().color = new Color32(0, 35, 94, byte.MaxValue); 

			Text bowlingType = Challenges[i].bowlingType;
			Color white = Color.white;
			Challenges[i].chlgName.color = white;
			bowlingType.color = white;
			Challenges[i].playOrLock.SetActive(value: true);
			Challenges[i].playOrLock.GetComponent<Image>().sprite = lockButton;
			Challenges[i].entryFee.SetActive(value: false);
			Challenges[i].pass.SetActive(value: false);
			Challenges[i].fail.SetActive(value: false);
			if (i <= CONTROLLER.CurrentLevelCompleted)
			{
				Challenges[i].playOrLock.GetComponent<Image>().sprite = playButton;
				Challenges[i].entryFee.SetActive(value: false);
				Challenges[i].GetComponent<Image>().color = new Color32(185, 209, 234, byte.MaxValue);

				Text bowlingType2 = Challenges[i].bowlingType;
				white = Color.white;
				Challenges[i].chlgName.color = white;
				bowlingType2.color = white;
			}
		}
		for (i = 0; i < CONTROLLER.CurrentLevelCompleted; i++)
		{
			Challenges[i].entryFee.SetActive(value: false);
			Challenges[i].pass.SetActive(value: true);
			Challenges[i].playOrLock.SetActive(value: false);
		}
		if (CONTROLLER.LevelId < 17 && Challenges[i].playOrLock.activeSelf && Challenges[i].playOrLock.GetComponent<Image>().sprite == playButton && i > 2)
		{
			if (i > 15)
			{
				i = 15;
			}
			SnapTo(Challenges[i].GetComponent<RectTransform>());
		}
		if (CONTROLLER.CurrentLevelCompleted >= CONTROLLER.LevelId && CONTROLLER.LevelFailed == 1 && CONTROLLER.LevelCompletedArray[CONTROLLER.CurrentLevelCompleted] == 0)
		{
			Challenges[i].entryFee.SetActive(value: false);
			Challenges[i].fail.SetActive(value: true);
			Challenges[i].playOrLock.SetActive(value: false);
		}
	}

	public void ClearLevel()
	{
		if (CONTROLLER.LevelId < 17)
		{
			CONTROLLER.CurrentLevelCompleted++;
			CONTROLLER.LevelId++;
			ValidateLockedLevels();
		}
	}

	public void SnapTo(RectTransform target)
	{
		Canvas.ForceUpdateCanvases();
		content.GetComponent<RectTransform>().anchoredPosition = (Vector2)scrollView.GetComponent<ScrollRect>().transform.InverseTransformPoint(content.GetComponent<RectTransform>().position) - (Vector2)scrollView.GetComponent<ScrollRect>().transform.InverseTransformPoint(target.position);
	}

	public void LevelSelected(int index)
	{
		if (Challenges[index].playOrLock.GetComponent<Image>().sprite == playButton)
		{
			CONTROLLER.LevelId = index;
			CONTROLLER.InningsCompleted = false;
			CONTROLLER.oversSelectedIndex = 0;
			SetBowler();
			HideThis();
		}
	}

	private void ValidateLevels()
	{
		for (int i = 0; i < 18; i++)
		{
			int num = i % 2;
			Challenges[i].levelID = i;
			Challenges[i].chlgNo.text = LocalizationData.instance.getText(277) + " " + (i + 1);
			if (CONTROLLER.SuperOverMode == "bat")
			{
				Challenges[i].gameObject.SetActive(value: true);
				Debug.Log(Challenges[i].chlgName.text);
				Challenges[i].chlgName.text = LocalizationData.instance.getText(int.Parse(LevelDescriptionArray[(int)Mathf.Floor(i / 2)].ToString()));
				Debug.Log(Challenges[i].chlgName.text);
				Challenges[i].chlgName.text = ReplaceText(Challenges[i].chlgName.text, Loc_LevelNumber[i / 2].ToString());
				Debug.LogError(Challenges[i].chlgName.text);
				if (num == 0)
				{
					Challenges[i].bowlingType.text = LocalizationData.instance.getText(284);
				}
				else
				{
					Challenges[i].bowlingType.text = LocalizationData.instance.getText(283);
				}
			}
			else if (i > 8)
			{
				Challenges[i].gameObject.SetActive(value: false);
			}
			else
			{
				Challenges[i].chlgName.text = LocalizationData.instance.getText(int.Parse(LevelDescriptionArray[i + 9].ToString()));
				Challenges[i].chlgName.text = ReplaceText(Challenges[i].chlgName.text, Loc_LevelNumber[i / 2].ToString());
				Challenges[i].bowlingType.text = string.Empty;
			}
		}
	}

	private string ReplaceText(string orginal, string replace)
	{
		string result = string.Empty;
		if (orginal.Contains("#"))
		{
			result = orginal.Replace("#", replace);
		}
		return result;
	}

	private void SetBowler()
	{
		PlayerPrefs.SetInt("userLastPlayed", 4);
		//Singleton<Firebase_Events>.instance.Firebase_SO_MatchPro();
		CONTROLLER.BattingTeamIndex = CONTROLLER.myTeamIndex;
		CONTROLLER.BowlingTeamIndex = CONTROLLER.opponentTeamIndex;
		Singleton<SquadPageTWO>.instance.Continue();
	}

	private void MakePayment()
	{
		SavePlayerPrefs.SaveUserTickets(-1, 1, 0);
		ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "Refund", 1);
	}

	public void GoBack()
	{
		holder.SetActive(value: false);
		Singleton<GameModeTWO>.instance.showMe();
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = GoBack;
		CONTROLLER.pageName = "SOLevels";
		Singleton<ArcadeSuperOverPanelTransition>.instance.panelTransition();
		ValidateLevels();
		ValidateLockedLevels();
		holder.SetActive(value: true);
	}

	private void HideThis()
	{
		holder.SetActive(value: false);
	}
}
