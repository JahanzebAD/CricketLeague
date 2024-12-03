using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheel : Singleton<SpinWheel>
{
	public static int itemNumber;

	public Image[] YellowDot;

	public Image[] BlackDot;

	public AchievementAnimation AchieveAnimScript;

	public AnimationCoinsFalling AnimationCoinFalling;

	public Image SubGradeTopPanelImage;

	public Image SubGradeTopPanelValue;

	public Text SubUpgradeValue1;

	public Text SubUpgradeValue2;

	public Text TopPanelCoinsText;

	public Text ToppanelTicketText;

	public Text TopPanelXpText;

	public Sprite[] SubUpgradeSprites;

	public Sprite[] colorcode;

	public List<AnimationCurve> animationCurves;

	private bool checkspin;

	private float anglePerItem;

	private float angleSpinRotate;

	private int randomTime;

	public Text SpinWheelSubPanelText;

	public Text SpinAgainText;

	public Text remainingSpins;

	public Text TopText;

	public GameObject Wheel;

	public GameObject TopPanelUpgradeHolder;

	public GameObject SpinWheelHolder;

	public GameObject SpinWheelSubHolder;

	public GameObject TextHolder;

	public GameObject BottomTab;

	public GameObject spinAgainBtn;

	public GameObject mainHolder;

	public GameObject SpinAgainImage;

	public GameObject SpinAgainCloseButton;

	public GameObject spinTextBG;

	public GameObject spinButtonHolder;

	public GameObject rewardButtonHolder;

	public Button SW_Back;

	public Button SW_OK;

	public Button SpinButton;

	public Image RVImg;

	public Transform parent;

	public Image SW_SubImageReward;

	private GameObject dupeImg;

	public Sprite[] Reward;

	public Sequence seq1;

	private void Start()
	{
		for (int i = 0; i < YellowDot.Length; i++)
		{
			YellowDot[i].DOFade(0f, 0f);
			BlackDot[i].DOFade(0f, 0f);
		}
		checkspin = false;
		anglePerItem = 30f;
		SpinWheelHolder.SetActive(value: false);
		SW_OK.gameObject.SetActive(value: false);
		SpinWheelSubHolder.SetActive(value: false);
	}

	public void StartYellowDot()
	{
		StopYellowDot();
		Singleton<SpinWheel>.instance.SpinButton.interactable = true;
		seq1 = DOTween.Sequence();
		for (int i = 0; i < YellowDot.Length; i++)
		{
			YellowDot[i].gameObject.SetActive(value: true);
			BlackDot[i].gameObject.SetActive(value: true);
			YellowDot[i].DOFade(0f, 0f);
			BlackDot[i].DOFade(0.5f, 0f);
		}
		for (int j = 0; j < YellowDot.Length; j++)
		{
			seq1.Insert(0.5f + 0.05f * (float)j, YellowDot[j].DOFade(1f, 0.25f));
		}
		for (int num = YellowDot.Length - 1; num >= 0; num--)
		{
			seq1.Insert(2f + 0.05f * (float)num, YellowDot[num].DOFade(0f, 0.25f));
		}
		seq1.SetLoops(-1).SetEase(Ease.Linear);
	}

	public void StopYellowDot()
	{
		seq1.Kill(complete: true);
		for (int i = 0; i < YellowDot.Length; i++)
		{
			YellowDot[i].DOFade(0f, 0f);
			BlackDot[i].DOFade(0f, 0f);
			YellowDot[i].gameObject.SetActive(value: false);
			BlackDot[i].gameObject.SetActive(value: false);
		}
	}

	public void Spin_Wheel_Button()
	{
		
			if (CONTROLLER.FreeSpin >= 1)
			{
				Singleton<NavigationBack>.instance.deviceBack = null;
				Singleton<GameModeTWO>.instance.PlayGameSound("wheel");
				CONTROLLER.AndroidBackButtonEnable = false;
				randomTime = Random.Range(3, 3);
				itemNumber = SpinWheel_Probability.prizeItemNumber;
				float spinMaxAngle = (float)(360 * randomTime) + (float)itemNumber * anglePerItem;
				StartCoroutine(SpinTheWheel(2 * randomTime, spinMaxAngle));
				SpinButtonClicked();
				SpinWheelSubHolder.SetActive(value: false);
				spinTextBG.SetActive(value: false);
			}
			else
			{
				//Singleton<AdIntegrate>.instance.showRewardedVideo(5);
			}
		//else
		//{
		//	CONTROLLER.PopupName = "noInternet";
		//	Singleton<Popups>.instance.ShowMe();
		//}
	}

	public void SpinAgain()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		if (SpinAgainText.text == "SPIN AGAIN")
		{
			ResetAnim();
			CONTROLLER.AndroidBackButtonEnable = false;
			randomTime = Random.Range(3, 3);
			itemNumber = SpinWheel_Probability.prizeItemNumber;
			float spinMaxAngle = (float)(360 * randomTime) + (float)itemNumber * anglePerItem;
			StartCoroutine(SpinTheWheel(2 * randomTime, spinMaxAngle));
			SpinButtonClicked();
			SpinWheelSubHolder.SetActive(value: false);
		}
		else
		{
			SubPanelCloseButton();
		}
	}

	public void ControlledSpin(int index)
	{
		itemNumber = index;
		CONTROLLER.AndroidBackButtonEnable = false;
		randomTime = Random.Range(3, 3);
		float spinMaxAngle = (float)(360 * randomTime) + (float)itemNumber * anglePerItem;
		StartCoroutine(SpinTheWheel(2 * randomTime, spinMaxAngle));
		SpinButtonClicked();
		SpinWheelSubHolder.SetActive(value: false);
	}

	private IEnumerator SpinTheWheel(float spinRotateTime, float spinMaxAngle)
	{
		checkspin = true;
		spinRotateTime = 4.5f;
		float timer = 0f;
		float spinStartAngle = Wheel.transform.eulerAngles.z;
		spinMaxAngle -= spinStartAngle;
		int animationCurveNumber = Random.Range(0, animationCurves.Count);
		while (timer < spinRotateTime)
		{
			angleSpinRotate = spinMaxAngle * animationCurves[animationCurveNumber].Evaluate(timer / spinRotateTime);
			Wheel.transform.eulerAngles = new Vector3(0f, 0f, 360f - (angleSpinRotate + spinStartAngle));
			timer += Time.deltaTime;
			yield return 0;
		}
		Invoke("DisplaySubPanelText", 0.2f);
		Wheel.transform.eulerAngles = new Vector3(0f, 0f, 360f - (angleSpinRotate + spinStartAngle));
		checkspin = false;
	}

	public void SpinWheelSubPanelPopup()
	{
		SpinWheelSubHolder.SetActive(value: true);
		SW_Back.gameObject.SetActive(value: false);
	}

	public void DisplaySubPanelText()
	{
		Singleton<NavigationBack>.instance.deviceBack = BackButtonClicked;
		SavePlayerPrefs.SaveSpins(-1);
		Server_Connection.instance.Get_User_Sync();
		if (remainingSpins != null)
		{
			remainingSpins.text = ObscuredPrefs.GetInt("userSpins").ToString();
		}
		spinTextBG.SetActive(value: true);
		AnimationCoinFalling.ResetAnimation();
		int num = itemNumber;
		switch (num)
		{
		case 0:
		case 3:
		case 8:
		{
			int num7 = 0;
			switch (num)
			{
			case 3:
				num7 = Random.Range(500, 1001);
				AnimationCoinFalling.AnimationTransition(2);
				break;
			case 8:
				num7 = Random.Range(1000, 3001);
				AnimationCoinFalling.AnimationTransition(3);
				break;
			case 0:
				num7 = Random.Range(300, 601);
				AnimationCoinFalling.AnimationTransition(1);
				break;
			}
			TopText.text = LocalizationData.instance.getText(181);
			SpinAgainText.text = "CLAIM NOW";
			SpinWheelSubPanelText.text = string.Empty + num7 + " " + LocalizationData.instance.getText(74);
			SavePlayerPrefs.SaveUserCoins(num7, 0, num7);
			TopPanelUpgradeHolder.SetActive(value: false);
			break;
		}
		case 2:
		case 10:
		{
			int num6 = 0;
			switch (num)
			{
			case 2:
				num6 = Random.Range(3, 6);
				break;
			case 10:
				num6 = Random.Range(6, 11);
				break;
			}
			TopText.text = LocalizationData.instance.getText(181);
			SpinAgainText.text = "CLAIM NOW";
			SpinWheelSubPanelText.text = string.Empty + num6 + " " + LocalizationData.instance.getText(172);
			SavePlayerPrefs.SaveUserTickets(num6, 0, num6);
			TopPanelUpgradeHolder.SetActive(value: false);
			break;
		}
		case 4:
		{
			int num4 = 1;
			int num5 = Mathf.RoundToInt(CONTROLLER.totalControlSubGrade / 13);
			if (num5 > 100)
			{
				num5 = 100;
			}
			float endValue2 = (float)num5 / 100f;
			SubGradeTopPanelValue.DOFillAmount(endValue2, 2f);
			SubUpgradeValue2.text = LocalizationData.instance.getText(175);
			SubUpgradeValue1.text = num5 + "%";
			SubGradeTopPanelImage.GetComponent<Image>().sprite = SubUpgradeSprites[1];
			SubGradeTopPanelValue.GetComponent<Image>().sprite = colorcode[1];
			if (CONTROLLER.controlGrade == 10 && CONTROLLER.totalControlSubGrade > 1290)
			{
				TopText.text = LocalizationData.instance.getText(500);
				SpinWheelSubPanelText.text = string.Empty;
				SpinAgainText.text = "OK";
			}
			else if (CONTROLLER.controlGrade <= 9 && CONTROLLER.totalControlSubGrade <= 1290)
			{
				CONTROLLER.earnedControlSubGrade += num4;
				SavePlayerPrefs.SetPowerUpDetails();
				TopText.text = string.Empty;
				SpinWheelSubPanelText.text = "+" + num4 + " Control Upgraded!";
				SpinWheelSubPanelText.text = "+" + num4 + " " + LocalizationData.instance.getText(500);
				SpinAgainText.text = "CLAIM NOW";
			}
			Server_Connection.instance.Set_StoreUpgrades();
			TopPanelUpgradeHolder.SetActive(value: true);
			break;
		}
		case 11:
		{
			int num8 = 1;
			int num9 = Mathf.RoundToInt(CONTROLLER.totalPowerSubGrade / 13);
			if (num9 > 100)
			{
				num9 = 100;
			}
			float endValue3 = (float)num9 / 100f;
			SubGradeTopPanelValue.DOFillAmount(endValue3, 2f);
			SubUpgradeValue2.text = LocalizationData.instance.getText(174);
			SubUpgradeValue1.text = num9 + "%";
			SubGradeTopPanelImage.GetComponent<Image>().sprite = SubUpgradeSprites[0];
			if (CONTROLLER.powerGrade == 10 && CONTROLLER.totalPowerSubGrade > 1290)
			{
				TopText.text = LocalizationData.instance.getText(500);
				SpinWheelSubPanelText.text = string.Empty;
				SpinAgainText.text = "OK";
			}
			else if (CONTROLLER.powerGrade <= 9 && CONTROLLER.totalPowerSubGrade <= 1290)
			{
				CONTROLLER.earnedPowerSubGrade += num8;
				SavePlayerPrefs.SetPowerUpDetails();
				TopText.text = string.Empty;
				SpinWheelSubPanelText.text = "+" + num8 + " " + LocalizationData.instance.getText(502);
				SpinAgainText.text = "CLAIM NOW";
			}
			Server_Connection.instance.Set_StoreUpgrades();
			TopPanelUpgradeHolder.SetActive(value: true);
			break;
		}
		case 1:
		{
			int num2 = 1;
			int num3 = Mathf.RoundToInt(CONTROLLER.totalAgilitySubGrade / 13);
			if (num3 > 100)
			{
				num3 = 100;
			}
			float endValue = (float)num3 / 100f;
			SubGradeTopPanelValue.DOFillAmount(endValue, 2f);
			SubUpgradeValue2.text = LocalizationData.instance.getText(176);
			SubUpgradeValue1.text = num3 + "%";
			SubGradeTopPanelImage.GetComponent<Image>().sprite = SubUpgradeSprites[2];
			SubGradeTopPanelValue.GetComponent<Image>().sprite = colorcode[2];
			if (CONTROLLER.agilityGrade == 10 && CONTROLLER.totalAgilitySubGrade > 1290)
			{
				SpinWheelSubPanelText.text = string.Empty;
				TopText.text = LocalizationData.instance.getText(500);
				SpinAgainText.text = "OK";
			}
			if (CONTROLLER.agilityGrade <= 9 && CONTROLLER.totalAgilitySubGrade <= 1290)
			{
				CONTROLLER.earnedAgilitySubGrade += num2;
				SavePlayerPrefs.SetPowerUpDetails();
				TopText.text = string.Empty;
				SpinWheelSubPanelText.text = "+" + num2 + " " + LocalizationData.instance.getText(503);
				SpinAgainText.text = "CLAIM NOW";
			}
			Server_Connection.instance.Set_StoreUpgrades();
			TopPanelUpgradeHolder.SetActive(value: true);
			break;
		}
		case 6:
			TopText.text = LocalizationData.instance.getText(181);
			SpinAgainText.text = "CLAIM NOW";
			ObscuredPrefs.SetInt("doubleRewards", 1);
			QuickPlayFreeEntry.Instance.CustomStart();
			SpinWheelSubPanelText.text = LocalizationData.instance.getText(504);
			TopPanelUpgradeHolder.SetActive(value: false);
			break;
		case 5:
			TopText.text = LocalizationData.instance.getText(181);
			SpinAgainText.text = "CLAIM NOW";
			SpinWheelSubPanelText.text = LocalizationData.instance.getText(505);
			SavePlayerPrefs.SaveSpins(1);
			UpdateRemainingSpinUI();
			TopPanelUpgradeHolder.SetActive(value: false);
			break;
		case 9:
			TopText.text = LocalizationData.instance.getText(181);
			SpinWheelSubPanelText.text = "500 " + LocalizationData.instance.getText(173) + "!";
			SpinAgainText.text = "CLAIM NOW";
			SavePlayerPrefs.SaveUserXPs(500, 500);
			TopPanelUpgradeHolder.SetActive(value: false);
			break;
		case 7:
			TopText.text = LocalizationData.instance.getText(181);
			SpinWheelSubPanelText.text = "750 " + LocalizationData.instance.getText(173) + "!";
			SpinAgainText.text = "CLAIM NOW";
			SavePlayerPrefs.SaveUserXPs(750, 750);
			TopPanelUpgradeHolder.SetActive(value: false);
			break;
		}
		SW_SubImageReward.gameObject.SetActive(value: false);
		SpinWheelSubHolder.SetActive(value: true);
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = SpinAgain;
		mainHolder.SetActive(value: false);
		PopupAnim(num);
		CONTROLLER.AndroidBackButtonEnable = true;
		CONTROLLER.pageName = "spinAgain";
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	Server_Connection.instance.SyncPoints();
		//}
	}

	private void PopupAnim(int index)
	{
		SpinAgainImage.SetActive(value: false);
		SpinAgainCloseButton.SetActive(value: false);
		Sequence s = DOTween.Sequence();
		if (index == 1 || index == 4 || index == 5 || index == 6 || index == 11 || index == 7 || index == 9 || index == 2 || index == 10)
		{
			SW_SubImageReward.sprite = Reward[index];
			SW_SubImageReward.gameObject.SetActive(value: true);
			s.Append(SW_SubImageReward.transform.DOPunchScale(new Vector3(1.1f, 1.1f, 0f), 0.55f, 3));
			s.Insert(0f, SW_SubImageReward.DOFade(0f, 0f));
			s.Insert(0f, SW_SubImageReward.DOFade(1f, 0.25f));
		}
		s.Insert(0.5f, BottomTab.transform.DOScaleY(1f, 0.25f));
		s.Insert(0.65f, spinAgainBtn.transform.DOScale(Vector3.one, 0.15f));
	}

	private void ResetAnim()
	{
		BottomTab.transform.DOScaleY(0f, 0f);
		spinAgainBtn.transform.DOScale(Vector3.zero, 0.15f);
		mainHolder.SetActive(value: true);
		SpinWheelSubHolder.SetActive(value: false);
	}

	public void BackButtonClicked()
	{
		ResetAnim();
		SpinWheelHolder.SetActive(value: false);
		SpinWheelSubHolder.SetActive(value: false);
		Singleton<GameModeTWO>.instance.showMe();
	}

	public void SpinButtonClicked()
	{
		SW_Back.gameObject.SetActive(value: false);
		SpinButton.gameObject.SetActive(value: false);
	}

	public void SpinAgainButtonClicked()
	{
		if (SpinAgainText.text == "CLAIMNOW")
		{
			SpinAgainCloseButton.SetActive(value: true);
			SpinAgainText.text = "SPIN AGAIN";
			SpinAgainImage.SetActive(value: true);
		}
		else
		{
			Singleton<SpinWheel_Probability>.instance.RetriveItemValue();
			Spin_Wheel_Button();
		}
	}

	public void CheckForAnimation()
	{
		if (itemNumber == 0 || itemNumber == 3 || itemNumber == 8)
		{
			AchieveAnimScript.CoinAnim(0);
		}
		else if (itemNumber == 2 || itemNumber == 10)
		{
			AchieveAnimScript.CoinAnim(1);
		}
		else if (itemNumber == 7 || itemNumber == 9)
		{
			AchieveAnimScript.CoinAnim(2);
		}
		else
		{
			LandingSpinWheelPage();
		}
	}

	public void LandingSpinWheelPage()
	{
		ResetAnim();
		if (CONTROLLER.FreeSpin >= 1)
		{
			SpinWheelSubHolder.SetActive(value: false);
			SpinButton.gameObject.SetActive(value: true);
			SW_Back.gameObject.SetActive(value: true);
		}
		else
		{
			CONTROLLER.pageName = "landingPage";
			Singleton<GameModeTWO>.instance.SpinWheelPanel.SetActive(value: false);
			Singleton<GameModeTWO>.instance.showMe();
		}
	}

	public void SubPanelCloseButton()
	{
		CheckForAnimation();
	}

	public void ShowMe()
	{
		ToppanelTicketText.text = ObscuredPrefs.GetInt(CONTROLLER.TicketsKey).ToString();
		TopPanelXpText.text = ObscuredPrefs.GetInt(CONTROLLER.XPKey).ToString();
		TopPanelCoinsText.text = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey).ToString();
		Singleton<NavigationBack>.instance.deviceBack = BackButtonClicked;
		int num = Random.Range(0, 12);
		Singleton<GameModeTWO>.instance.ResetDetails();
		Singleton<SpinAndWinPanelTransition>.instance.PanelTransition();
		Singleton<GameModeTWO>.instance.SpinWheelPanel.SetActive(value: true);
		SpinButton.gameObject.SetActive(value: true);
		SW_Back.gameObject.SetActive(value: true);
		Singleton<GameModeTWO>.instance.Holder.SetActive(value: false);
		if (CONTROLLER.FreeSpin > 0)
		{
			spinButtonHolder.SetActive(value: true);
			rewardButtonHolder.SetActive(value: false);
		}
		else
		{
			spinButtonHolder.SetActive(value: false);
			rewardButtonHolder.SetActive(value: true);
		}
		UpdateRemainingSpinUI();
	}

	public void UpdateRemainingSpinUI()
	{
		if (remainingSpins != null)
		{
			remainingSpins.text = ObscuredPrefs.GetInt("userSpins").ToString();
		}
	}
}
