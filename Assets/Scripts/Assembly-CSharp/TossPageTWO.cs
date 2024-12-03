using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TossPageTWO : Singleton<TossPageTWO>
{
	public ScreenEntryAnimation entryAnim;

	public GameObject Holder;

	public GameObject SwipeUpPanel;

	public GameObject MakeYourCallPanel;

	public Text Tittle;

	public Text tossOutcome;

	public Text displayText1;

	public Text displayText2;

	public GameObject Tossinfo;

	public Text Button1Text;

	public Text Button2Text;

	public Button BatButton;

	public Button BowlButton;

	public Button BackButton;

	public Button continueButton;

	public GameObject Coin;

	private int random;

	public Image flag1;

	public Image flag2;

	public Transform flagEndPosition;

	private string coinStatus;

	private Vector3 flag1StartPos;

	private Vector3 flag1EndPos;

	private Vector3 flag2StartPos;

	private Vector3 flag2EndPos;

	private Vector3 finalPos;

	private string oversText;

	public Button RV_Button;

	public GameObject RV_Panel;

	private Vector2 firstPressPos;

	private Vector2 secondPressPos;

	private Vector2 currentSwipe;

	private bool firstTimeToss;

	public string userSelectedTo = string.Empty;

	private int angle;

	public Image[] sideArrow;

	protected void Start()
	{
		MakeYourCallPanel.GetComponent<Image>().DOFade(0f, 0.5f).SetLoops(-1, LoopType.Yoyo)
			.SetUpdate(isIndependentUpdate: true);
		firstTimeToss = true;
		flag1StartPos = new Vector3(-135f, 33.6f, 0f);
		flag2StartPos = new Vector3(135f, 33.6f, 0f);
		flag1EndPos = new Vector3(-350f, 0f, 0f);
		flag2EndPos = new Vector3(350f, 0f, 0f);
		finalPos = flagEndPosition.localPosition;
		MakeYourCallPanel.SetActive(value: false);
		ArrowAnimation();
	}

	public void ArrowAnimation()
	{
		ResetAnim1();
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, sideArrow[0].DOFade(1f, 0.3f));
		sequence.Insert(0.3f, sideArrow[1].DOFade(1f, 0.3f));
		sequence.Insert(0.6f, sideArrow[2].DOFade(1f, 0.3f));
		sequence.Insert(0.9f, sideArrow[0].DOFade(0f, 0.15f));
		sequence.Insert(1.05f, sideArrow[1].DOFade(0f, 0.15f));
		sequence.Insert(1.2f, sideArrow[2].DOFade(0f, 0.15f));
		sequence.SetLoops(-1);
	}

	public void ResetAnim1()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(0f, sideArrow[0].DOFade(0f, 0f));
		s.Insert(0f, sideArrow[1].DOFade(0f, 0f));
		s.Insert(0f, sideArrow[2].DOFade(0f, 0f));
	}

	public void RVTossAgainAdCall()
	{
		ResetPosition();
		Singleton<Popups>.instance.HideMe();
		ResetAllAnimations();
		resetButtons();
		Coin.gameObject.transform.localPosition = new Vector3(628.3f, -428f, 743f);
		Coin.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		RV_Button.gameObject.SetActive(value: false);
		continueButton.gameObject.SetActive(value: false);
		Tittle.gameObject.SetActive(value: true);
		Coin.SetActive(value: true);
		tossOutcome.gameObject.SetActive(value: true);
		CONTROLLER.pageName = "toss";
		flag1.gameObject.SetActive(value: true);
		RV_Panel.SetActive(value: false);
		flag2.gameObject.SetActive(value: true);
		firstTimeToss = true;
		SwipeUpPanel.SetActive(value: true);
		DeactivateButtons();
	}

	private void ResetPosition()
	{
		Tossinfo.SetActive(value: false);
		Tittle.gameObject.SetActive(value: false);
		continueButton.gameObject.SetActive(value: false);
		Coin.SetActive(value: false);
		tossOutcome.gameObject.SetActive(value: false);
		flag1.gameObject.SetActive(value: false);
		flag2.gameObject.SetActive(value: false);
	}

	public void RVButtonClicked()
	{
		RVSelected(1);
	}

	public void RVSelected(int output)
	{
		switch (output)
		{
		case 1:
			//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_TossAgain" });
			//Singleton<AdIntegrate>.instance.showRewardedVideo(2);
			break;
		case 0:
			continueButton.gameObject.SetActive(value: true);
			//if (Singleton<AdIntegrate>.instance.isRewardedVideoAvailable)
			//{
			//	RV_Button.gameObject.SetActive(value: true);
			//}
			//else
			//{
			//	RV_Button.gameObject.SetActive(value: false);
			//	Singleton<AdIntegrate>.instance.requestRewardedVideo();
			//}
			Singleton<Popups>.instance.HideMe();
			Tossinfo.SetActive(value: true);
			continueButton.gameObject.SetActive(value: true);
			Tittle.gameObject.SetActive(value: true);
			Coin.SetActive(value: true);
			tossOutcome.gameObject.SetActive(value: true);
			CONTROLLER.pageName = "toss";
			flag1.gameObject.SetActive(value: true);
			RV_Panel.SetActive(value: false);
			flag2.gameObject.SetActive(value: true);
			break;
		}
	}

	public void HeadsButton()
	{
		if (userSelectedTo == string.Empty)
		{
			deactivateBtn("heads");
			hideMePanelTransition();
			CONTROLLER.pageName = string.Empty;
		}
		else
		{
			Coin.gameObject.transform.localPosition = new Vector3(628.3f, -428f, 743f);
			Coin.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			Coin.SetActive(value: false);
			hideMe();
			CONTROLLER.meFirstBatting = 1;
			Continue();
		}
		MakeYourCallPanel.SetActive(value: false);
	}

	public void TailButton()
	{
		if (userSelectedTo == string.Empty)
		{
			deactivateBtn("tail");
			hideMePanelTransition();
			CONTROLLER.pageName = string.Empty;
		}
		else
		{
			Coin.gameObject.transform.localPosition = new Vector3(628.3f, -428f, 743f);
			Coin.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
			Coin.SetActive(value: false);
			hideMe();
			CONTROLLER.meFirstBatting = 0;
			Continue();
		}
		MakeYourCallPanel.SetActive(value: false);
	}

	private void deactivateBtn(string askedFor)
	{
		userSelectedTo = askedFor;
		showResult();
		BackButton.gameObject.SetActive(value: false);
		CONTROLLER.CurrentMenu = string.Empty;
	}

	private void AfterUpSwipe()
	{
		Sequence s = DOTween.Sequence();
		s.Append(flag1.transform.DOLocalMove(flag1EndPos, 0.3f));
		s.Insert(0f, flag2.transform.DOLocalMove(flag2EndPos, 0.3f));
		s.Insert(0f, flag1.transform.DOScale(Vector3.one * 1.5f, 0.3f));
		s.Insert(0f, flag2.transform.DOScale(Vector3.one * 1.5f, 0.3f));
		BackButton.gameObject.SetActive(value: false);
		CONTROLLER.CurrentMenu = string.Empty;
		BatButton.image.DOFade(1f, 0.15f);
		BowlButton.image.DOFade(1f, 0.15f);
		Button1Text.DOFade(1f, 0.15f);
		Button2Text.DOFade(1f, 0.15f);
		Coin.gameObject.transform.localPosition = new Vector3(628.3f, -428f, 743f);
		Coin.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		Coin.SetActive(value: true);
		SwipeUpPanel.SetActive(value: false);
		BackButton.gameObject.SetActive(value: false);
		CONTROLLER.CurrentMenu = string.Empty;
		Coin.transform.DOLocalMove(new Vector3(628.3f, -400f, 732f), 0.5f, snapping: true);
		Coin.transform.DORotate(new Vector3(3625f, 0f, 0f), 0.5f, RotateMode.FastBeyond360);
		Invoke("ActivateButtons", 0.5f);
	}

	private void activateBtn()
	{
		Button1Text.text = LocalizationData.instance.getText(231);
		Button2Text.text = LocalizationData.instance.getText(232);
	}

	public void resetButtons()
	{
		Tossinfo.SetActive(value: false);
		BatButton.gameObject.SetActive(value: true);
		BowlButton.gameObject.SetActive(value: true);
		Button1Text.text = LocalizationData.instance.getText(227);
		Button2Text.text = LocalizationData.instance.getText(228);
		userSelectedTo = string.Empty;
	}

	private void setTitle()
	{
		CONTROLLER.menuTitle = "TOSS";
		Singleton<GameModeTWO>.instance.updateTitle(_modeSelected: true);
		BackButton.gameObject.SetActive(value: false);
		continueButton.gameObject.SetActive(value: false);
		CONTROLLER.CurrentMenu = "toss";
	}

	public void showMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = null;
		CONTROLLER.pageName = "toss";
		Invoke("showMeDelay", 0f);
	}

	public void showMeDelay()
	{
		Singleton<NavigationBack>.instance.deviceBack = null;
		CONTROLLER.pageName = "toss";
		flag1StartPos = new Vector3(-135f, 33.6f, 0f);
		flag2StartPos = new Vector3(135f, 33.6f, 0f);
		RV_Button.gameObject.SetActive(value: false);
		CONTROLLER.GameStartsFromSave = false;
		CONTROLLER.isFreeHitBall = false;
		AutoSave.DeleteFile();
		string abbrevation = CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation;
		flag1.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
		abbrevation = CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation;
		flag2.sprite = Singleton<FlagHolder>.instance.searchFlagByName(abbrevation);
		Coin.gameObject.transform.localPosition = new Vector3(628.3f, -428f, 743f);
		Coin.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		Coin.SetActive(value: true);
		panelTransition();
		ResetAllAnimations();
		resetButtons();
		setTitle();
		DeactivateButtons();
		Holder.SetActive(value: true);
	}

	private void DeactivateButtons()
	{
		BatButton.gameObject.SetActive(value: false);
		BowlButton.gameObject.SetActive(value: false);
	}

	private void ActivateButtons()
	{
		MakeYourCallPanel.SetActive(value: true);
		BatButton.gameObject.SetActive(value: true);
		BowlButton.gameObject.SetActive(value: true);
	}

	public void hideMe()
	{
		hideMePanelTransition();
		Invoke("hideMeDelay", 0f);
		Coin.SetActive(value: false);
		Coin.gameObject.transform.localPosition = new Vector3(628.3f, -428f, 743f);
		Coin.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
	}

	public void hideMeDelay()
	{
		resetButtons();
		Holder.SetActive(value: false);
		ResetAllAnimations();
	}

	private void showResult()
	{
		random = Random.Range(0, 100);
		panelTransition();
		coinStatus = string.Empty;
		DOTween.Init();
		if (random > 50)
		{
			coinStatus = "tail";
			Tittle.DOFade(0f, 0.15f);
			Invoke("SetTail", 0.2f);
		}
		else
		{
			coinStatus = "heads";
			Tittle.DOFade(0f, 0.15f);
			Invoke("SetHead", 0.2f);
		}
	}

	public void SetTail()
	{
		Coin.transform.DORotate(new Vector3(25f, 0f, 0f), 0.01f);
		Invoke("Transistion", 0.01f);
	}

	public void SetHead()
	{
		Coin.transform.DORotate(new Vector3(25f, 0f, 180f), 0.01f);
		Invoke("Transistion", 0.01f);
	}

	public void Transistion()
	{
		if (coinStatus == "heads")
		{
			tossOutcome.text = LocalizationData.instance.getText(227);
			Coin.transform.DOJump(new Vector3(628.3f, -408f, 705f), 15f, 1, 1.5f, snapping: true);
			Coin.transform.DORotate(new Vector3(11430f, 0f, 180f), 1.5f, RotateMode.FastBeyond360);
		}
		else if (coinStatus == "tail")
		{
			tossOutcome.text = LocalizationData.instance.getText(228);
			Coin.transform.DOJump(new Vector3(628.3f, -408f, 705f), 15f, 1, 1.5f, snapping: true);
			Coin.transform.DORotate(new Vector3(11430f, 0f, 0f), 1.5f, RotateMode.FastBeyond360);
		}
		Invoke("CoinShow", 2f);
	}

	private string ReplaceText(string original, string replace1, string replace2)
	{
		string text = string.Empty;
		Debug.Log(original + " " + replace1 + " " + replace2 + " " + text);
		if (original.Contains("# "))
		{
			text = original.Replace("# ", replace1);
		}
		Debug.Log(original + " " + replace1 + " " + replace2 + " " + text);
		if (text.Contains("$"))
		{
			text = text.Replace("$", replace2);
		}
		Debug.Log(original + " " + replace1 + " " + replace2 + " " + text);
		return text;
	}

	public void CoinShow()
	{
		CONTROLLER.pageName = "toss";
		if (userSelectedTo == coinStatus)
		{
			Tittle.DOFade(1f, 0.15f);
			displayText1.text = LocalizationData.instance.getText(230);
			displayText2.text = string.Empty;
			activateBtn();
		}
		else
		{
			BatButton.gameObject.SetActive(value: false);
			BowlButton.gameObject.SetActive(value: false);
			continueButton.gameObject.SetActive(value: true);
			random = Random.Range(0, 4);
			if (random < 2)
			{
				CONTROLLER.meFirstBatting = 0;
				displayText1.text = LocalizationData.instance.getText(408);
				displayText2.text = " " + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation + " ";
				displayText1.text = ReplaceText(displayText1.text, string.Empty, LocalizationData.instance.getText(231));
			}
			else
			{
				CONTROLLER.meFirstBatting = 1;
				displayText1.text = LocalizationData.instance.getText(408);
				displayText2.text = " " + CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation + " ";
				displayText1.text = ReplaceText(displayText1.text, string.Empty, LocalizationData.instance.getText(232));
			}
		}
		if (userSelectedTo == coinStatus)
		{
			Tittle.text = LocalizationData.instance.getText(229) + "!";
			Sequence s = DOTween.Sequence();
			s.Append(flag1.transform.DOLocalMove(finalPos, 0.5f));
			s.Insert(0f, flag2.transform.DOLocalMove(new Vector3(1000f, 0f, 0f), 0.5f));
			s.Insert(0f, flag1.transform.DOScale(Vector3.one * 0.65f, 0.5f));
			Tossinfo.SetActive(value: true);
			s.Insert(0.5f, Tossinfo.transform.DOScaleX(1f, 0.5f));
			s.Insert(0.5f, displayText1.transform.DOScaleX(1f, 0.5f));
			s.Insert(0.5f, Tittle.DOFade(1f, 0.5f));
			s.Insert(0.5f, tossOutcome.DOFade(1f, 0.5f));
		}
		else
		{
			Tittle.text = LocalizationData.instance.getText(407);
			//if (Singleton<AdIntegrate>.instance.isRewardedVideoAvailable)
			//{
			//	RV_Button.gameObject.SetActive(value: true);
			//}
			//else
			//{
			//	RV_Button.gameObject.SetActive(value: false);
			//	Singleton<AdIntegrate>.instance.requestRewardedVideo();
			//}
			Sequence s2 = DOTween.Sequence();
			s2.Append(flag2.transform.DOLocalMove(finalPos, 0.5f));
			s2.Insert(0f, flag1.transform.DOLocalMove(new Vector3(-1000f, 0f, 0f), 0.5f));
			s2.Insert(0f, flag2.transform.DOScale(Vector3.one * 0.65f, 0.5f));
			Tossinfo.SetActive(value: true);
			s2.Insert(0.5f, Tossinfo.transform.DOScaleX(1f, 0.5f));
			s2.Insert(0.5f, displayText1.transform.DOScaleX(1f, 0.5f));
			s2.Insert(0.5f, Tittle.DOFade(1f, 0.5f));
			s2.Insert(0.5f, tossOutcome.DOFade(1f, 0.5f));
		}
		resetTransition();
		panelTransition();
	}

	public void panelTransition()
	{
		BatButton.gameObject.transform.DOLocalMove(new Vector3(-130f, -273.9f, 0f), 0.5f);
		BowlButton.gameObject.transform.DOLocalMove(new Vector3(130f, -273.9f, 0f), 0.5f);
		BatButton.image.DOFade(1f, 0.15f);
		BatButton.interactable = true;
		BowlButton.image.DOFade(1f, 0.15f);
		BowlButton.interactable = true;
		Button1Text.DOFade(1f, 0.15f);
		Button2Text.DOFade(1f, 0.15f);
	}

	public void resetTransition()
	{
	}

	public void hideMePanelTransition()
	{
		BatButton.gameObject.transform.DOLocalMove(new Vector3(-305f, -273.9f, 0f), 0f);
		BowlButton.gameObject.transform.DOLocalMove(new Vector3(305f, -273.9f, 0f), 0f);
		BatButton.image.DOFade(0f, 0.15f);
		BatButton.interactable = false;
		BowlButton.image.DOFade(0f, 0.15f);
		BowlButton.interactable = false;
		Button1Text.DOFade(0f, 0.15f);
		Button2Text.DOFade(0f, 0.15f);
	}

	private void ResetAllAnimations()
	{
		Sequence s = DOTween.Sequence();
		s.Append(flag1.transform.DOLocalMove(flag1StartPos, 0f));
		s.Insert(0f, flag1.transform.DOScale(Vector3.one, 0f));
		s.Insert(0f, flag2.transform.DOLocalMove(flag2StartPos, 0f));
		s.Insert(0f, flag2.transform.DOScale(Vector3.one, 0f));
		s.Insert(0f, Tittle.DOFade(0f, 0f));
		s.Insert(0f, tossOutcome.DOFade(0f, 0f));
		s.Insert(0f, Tossinfo.transform.DOScaleX(0f, 0f));
		s.Insert(0f, displayText1.transform.DOScaleX(0f, 0f));
	}

	public void Continue()
	{
		hideMe();
		if (CONTROLLER.PlayModeSelected == 0)
		{
			oversText = "QPOvers";
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			oversText = "T20Overs";
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.tournamentType == "NPL")
			{
				oversText = "NPLOvers";
			}
			else if (CONTROLLER.tournamentType == "PAK")
			{
				oversText = "PAKOvers";
			}
			else if (CONTROLLER.tournamentType == "AUS")
			{
				oversText = "AUSOvers";
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			oversText = "WCOvers";
		}
		CONTROLLER.oversSelectedIndex = ObscuredPrefs.GetInt(oversText);
		Coin.gameObject.transform.localPosition = new Vector3(628.3f, -428f, 743f);
		Coin.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		CONTROLLER.SceneIsLoading = true;
		Singleton<LoadPlayerPrefs>.instance.InitializeGame();
		Singleton<NavigationBack>.instance.deviceBack = null;
		Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
	}

	public void GotoGroundScene()
	{
		CONTROLLER.SceneIsLoading = true;
		Singleton<LoadPlayerPrefs>.instance.InitializeGame();
		Singleton<NavigationBack>.instance.deviceBack = null;
		Singleton<LoadingPanelTransition>.instance.PanelTransition1("Ground");
	}

	public void Back()
	{
		hideMe();
		Coin.gameObject.transform.localPosition = new Vector3(628.3f, -428f, 743f);
		Coin.gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
		if (CONTROLLER.PlayModeSelected == 0)
		{
			Singleton<TeamSelectionTWO>.instance.showMe();
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			if (CONTROLLER.TournamentStage == 0)
			{
				Singleton<TeamSelectionTWO>.instance.Continue();
			}
			else
			{
				Singleton<FixturesTWO>.instance.showMe();
			}
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			if (CONTROLLER.NPLIndiaTournamentStage == 0)
			{
				Singleton<TeamSelectionTWO>.instance.Continue();
			}
			else
			{
				Singleton<NPLIndiaPlayOff>.instance.ShowMe();
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			if (CONTROLLER.WCTournamentStage == 0)
			{
				Singleton<WorldCupLeague>.instance.ShowMe();
			}
			else
			{
				Singleton<WorldCupPlayOff>.instance.ShowMe();
			}
		}
	}

	private void Update()
	{
		if (Holder.activeInHierarchy && firstTimeToss)
		{
			InputViaTouch();
			InputViaMouse();
		}
	}

	public void InputViaTouch()
	{
		if (Input.touches.Length <= 0)
		{
			return;
		}
		Touch touch = Input.GetTouch(0);
		if (touch.phase == TouchPhase.Began)
		{
			firstPressPos = new Vector2(touch.position.x, touch.position.y);
		}
		if (touch.phase == TouchPhase.Ended)
		{
			secondPressPos = new Vector2(touch.position.x, touch.position.y);
			currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
			currentSwipe.Normalize();
			if (currentSwipe.y > 0f && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
			{
				AfterUpSwipe();
				firstTimeToss = false;
			}
			if (!(currentSwipe.y < 0f) || !(currentSwipe.x > -0.5f) || currentSwipe.x < 0.5f)
			{
			}
			if (!(currentSwipe.x < 0f) || !(currentSwipe.y > -0.5f) || currentSwipe.y < 0.5f)
			{
			}
			if (currentSwipe.x > 0f && currentSwipe.y > -0.5f && !(currentSwipe.y < 0.5f))
			{
			}
		}
	}

	public void InputViaMouse()
	{
		if (Input.GetMouseButtonDown(0))
		{
			firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		}
		if (Input.GetMouseButtonUp(0))
		{
			secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
			currentSwipe.Normalize();
			if (currentSwipe.y > 0f && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
			{
				AfterUpSwipe();
				firstTimeToss = false;
			}
			if (!(currentSwipe.y < 0f) || !(currentSwipe.x > -0.5f) || currentSwipe.x < 0.5f)
			{
			}
			if (!(currentSwipe.x < 0f) || !(currentSwipe.y > -0.5f) || currentSwipe.y < 0.5f)
			{
			}
			if (currentSwipe.x > 0f && currentSwipe.y > -0.5f && !(currentSwipe.y < 0.5f))
			{
			}
		}
	}
}
