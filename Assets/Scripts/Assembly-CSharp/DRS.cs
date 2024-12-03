using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DRS : Singleton<DRS>
{
	public GameObject holder;

	public bool enable = true;

	public bool DRSreplay;

	public Transform[] DRSResultUIPanel = new Transform[4];

	public Text[] DRSResultUIText = new Text[4];

	public GameObject DRSResultUIHolder;

	public Material DRSBallTrailMaterial;

	public GameObject DRSRedTrail;

	public GameObject DRSBlueTrail;

	private bool noBtnClickedCalled;

	private bool yesBtnClickedCalled;

	public Image teamFlag;

	public Text teamText;

	public Text noOfdrs;

	public GameObject userReviewPanel;

	public GameObject aiReviewPanel;

	public Image timerImage;

	public Text timerText;

	public Text aiReviewstatus;

	private int secCount;

	public void ShowMe()
	{
		Invoke("DelayShowMe", 2f);
	}

	public void DelayShowMe()
	{
		noBtnClickedCalled = false;
		yesBtnClickedCalled = false;
		Singleton<GroundController>.instance.umpireCamera.enabled = false;
		Singleton<StandbyCam>.instance.RotateStandbyCam();
		holder.SetActive(value: true);
		if (Singleton<GroundController>.instance.drsCalledByBattingTeam == 1)
		{
			teamText.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].ToString();
			noOfdrs.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].noofDRSLeft + " " + LocalizationData.instance.getText(512);
			teamFlag.sprite = Singleton<FlagHolderGround>.instance.searchFlagByName(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation);
		}
		else
		{
			teamText.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].ToString();
			noOfdrs.text = CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].noofDRSLeft + " " + LocalizationData.instance.getText(512);
			teamFlag.sprite = Singleton<FlagHolderGround>.instance.searchFlagByName(CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].abbrevation);
		}
		if (Singleton<GroundController>.instance.drsCalledByUser == 1)
		{
			userReviewPanel.SetActive(value: true);
			aiReviewPanel.SetActive(value: false);
		}
		else
		{
			userReviewPanel.SetActive(value: false);
			aiReviewPanel.SetActive(value: true);
		}
		SetDRSTimer();
	}

	public void YesBtnClicked()
	{
		if (!yesBtnClickedCalled)
		{
			yesBtnClickedCalled = true;
			Singleton<StandbyCam>.instance.PauseTween();
			CONTROLLER.canShowReplay = true;
			CONTROLLER.reviewReplay = true;
			noBtnClickedCalled = true;
			Time.timeScale = 1f;
			holder.SetActive(value: false);
			Singleton<GroundController>.instance.canShowDRS = false;
			DRSreplay = true;
			Singleton<GameModel>.instance.AnimationCompleted();
			//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "DRS_Review" });
		}
	}

	public void NoBtnClicked()
	{
		if (!noBtnClickedCalled)
		{
			noBtnClickedCalled = true;
			Singleton<StandbyCam>.instance.PauseTween();
			Time.timeScale = 1f;
			holder.SetActive(value: false);
			DRSreplay = false;
			Singleton<GroundController>.instance.canShowDRS = false;
			Singleton<GroundController>.instance.umpireCamera.enabled = true;
			Debug.LogError("action setted to 3:::");
			Singleton<GroundController>.instance.action = 3;
		}
	}

	public void SetDRSTimer()
	{
		secCount = 6;
		timerImage.fillAmount = 1f;
		aiReviewstatus.text = LocalizationData.instance.getText(513);
		TweenCallback callback = delegate
		{
			SetSecond();
		};
		Sequence s = DOTween.Sequence();
		s.Append(timerImage.DOFillAmount(0f, 6f));
		for (int i = 0; i < 6; i++)
		{
			s.InsertCallback(i, callback);
		}
		s.InsertCallback(6f, NoBtnClicked);
	}

	public void SetSecond()
	{
		timerText.text = secCount.ToString();
		secCount--;
		if (Singleton<GroundController>.instance.aiGoForDRS())
		{
			if (secCount <= 2)
			{
				int index = LocalizationData.instance.refList.IndexOf(teamText.text.ToUpper());
				teamText.text = LocalizationData.instance.getText(index);
				aiReviewstatus.text = " " + teamText.text + " " + LocalizationData.instance.getText(514);
			}
			if (secCount <= 0)
			{
				YesBtnClicked();
			}
		}
	}

	public void ResetPanelTransition()
	{
		DRSResultUIHolder.SetActive(value: true);
		for (int i = 0; i < DRSResultUIPanel.Length - 1; i++)
		{
			DRSResultUIPanel[i].GetChild(0).DOScaleY(0f, 0f).SetUpdate(isIndependentUpdate: true);
			DRSResultUIPanel[i].GetChild(1).DOScaleY(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
	}

	public void ShowDRSResultPanel(int index, string result, int color)
	{
		if (index == 3 && Singleton<GroundController>.instance.impactOffSideWithAttemptedShot)
		{
			return;
		}
		if (index == 4)
		{
			DRSResultUIPanel[index].gameObject.SetActive(value: true);
			return;
		}
		DRSResultUIPanel[index].GetChild(0).DOScaleY(1f, 0.6f).SetUpdate(isIndependentUpdate: true);
		DRSResultUIPanel[index].GetChild(1).DOScaleY(1f, 0.6f).SetUpdate(isIndependentUpdate: true);
		DRSResultUIText[index].text = result;
		switch (color)
		{
		case 1:
			DRSResultUIPanel[index].GetChild(1).GetComponent<Image>().color = new Color(0f, 0.9f, 0.1f, 0.8f);
			DRSResultUIPanel[index].GetChild(1).GetComponentInChildren<Text>().color = Color.black;
			break;
		case 0:
			DRSResultUIPanel[index].GetChild(1).GetComponent<Image>().color = new Color(1f, 0f, 0f, 0.8f);
			DRSResultUIPanel[index].GetChild(1).GetComponentInChildren<Text>().color = Color.white;
			break;
		default:
			DRSResultUIPanel[index].GetChild(1).GetComponent<Image>().color = new Color(0.9f, 0.35f, 0f, 0.8f);
			DRSResultUIPanel[index].GetChild(1).GetComponentInChildren<Text>().color = Color.white;
			break;
		}
	}

	public void Hide()
	{
		DRSResultUIPanel[4].gameObject.SetActive(value: false);
		DRSResultUIHolder.SetActive(value: false);
	}
}
