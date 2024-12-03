using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : Singleton<PowerUps>
{
	public GameObject holder;

	public GameObject[] beforePayment;

	public GameObject[] afterPayment;

	public GameObject[] MaxUpgrade;

	public Button PowerButton;

	public Button ControlButton;

	public Button AgilityButton;

	public Image powerFill;

	public Image controlFill;

	public Image agilityFill;

	public Image[] fillClone;

	public Image[] gradeFill;

	public Text[] tokenAmount;

	public Text[] lowerLimit;

	public Text[] upperLimit;

	public Text[] grade;

	public Text[] timerText;

	public Text userXP;

	public Text userCoins;

	public Text userTokens;

	private Tweener[] tween = new Tweener[3];

	public GameObject tempHelp;

	public GameObject[] packs;

	public GameObject[] glow;

	public Tweener packTween;

	public int packIndex = -1;

	private int[] XPmilestones = new int[10] { 0, 5000, 10000, 20000, 35000, 55000, 80000, 110000, 145000, 185000 };

	public int CoinAmount;

	public int TimeAmount;

	private int[] prefix = new int[3]
	{
		CONTROLLER.powerGrade,
		CONTROLLER.controlGrade,
		CONTROLLER.agilityGrade
	};

	private string[] UBPrefs = new string[3] { "ubPowerUpgrade", "ubControlUpgrade", "ubAgilityUpgrade" };

	private string[] UpgradePrefs = new string[3] { "PowerUpgradeTimer", "ControlUpgradeTimer", "AgilityUpgradeTimer" };

	public bool[] timerStarted = new bool[3]
	{
		CONTROLLER.powerUpgradeTimerStarted,
		CONTROLLER.controlUpgradeTimerStarted,
		CONTROLLER.agilityUpgradeTimerStarted
	};

	public Text PowerFinishNowCoins;

	public Text AgilityFinishNowCoins;

	public Text ControlFinishNowCoins;

	public void BuySubGrade(int index)
	{
		switch (index)
		{
		case 0:
			if (CONTROLLER.powerGrade == 10)
			{
				return;
			}
			if (CONTROLLER.powerGrade <= 5)
			{
				float num5 = CONTROLLER.totalPowerSubGrade - CONTROLLER.powerGrade * 50;
				if (num5 + 10f < 50f)
				{
					num5 += 10f;
				}
				else
				{
					num5 = 0f;
					CONTROLLER.powerGrade++;
				}
				//FirebaseAnalyticsManager.instance.logEvent("Upgrade", "Upgrade", CONTROLLER.userID);
				//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "Store_Up_Power" });
				num5 = Mathf.RoundToInt(num5);
				CONTROLLER.boughtPowerSubGrade += 10;
			}
			else if (CONTROLLER.powerGrade >= 6 && CONTROLLER.powerGrade <= 9)
			{
				float num6 = CONTROLLER.totalPowerSubGrade - 300 - (CONTROLLER.powerGrade - 6) * 250;
				num6 /= 250f;
				if (num6 + 10f < 250f)
				{
					num6 += 10f;
				}
				else
				{
					num6 = 0f;
					CONTROLLER.powerGrade++;
				}
				//FirebaseAnalyticsManager.instance.logEvent("Upgrade", "Upgrade", CONTROLLER.userID);
				//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "Store_Up_Power" });
				num6 = Mathf.RoundToInt(num6);
				CONTROLLER.boughtPowerSubGrade += 10;
			}
			break;
		case 1:
			if (CONTROLLER.controlGrade == 10)
			{
				return;
			}
			if (CONTROLLER.controlGrade <= 5)
			{
				float num3 = CONTROLLER.totalControlSubGrade - CONTROLLER.controlGrade * 50;
				if (num3 + 10f < 50f)
				{
					num3 += 10f;
				}
				else
				{
					num3 = 0f;
					CONTROLLER.controlGrade++;
				}
				//FirebaseAnalyticsManager.instance.logEvent("Upgrade", "Upgrade", CONTROLLER.userID);
				//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "Store_Up_Control" });
				num3 = Mathf.RoundToInt(num3);
				CONTROLLER.boughtControlSubGrade += 10;
			}
			else if (CONTROLLER.controlGrade >= 6 && CONTROLLER.controlGrade <= 9)
			{
				float num4 = CONTROLLER.totalControlSubGrade - 300 - (CONTROLLER.controlGrade - 6) * 250;
				num4 /= 250f;
				if (num4 + 10f < 250f)
				{
					num4 += 10f;
				}
				else
				{
					num4 = 0f;
					CONTROLLER.controlGrade++;
				}
				//FirebaseAnalyticsManager.instance.logEvent("Upgrade", "Upgrade", CONTROLLER.userID);
				//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "Store_Up_Control" });
				num4 = Mathf.RoundToInt(num4);
				CONTROLLER.boughtControlSubGrade += 10;
			}
			break;
		case 2:
			if (CONTROLLER.agilityGrade == 10)
			{
				return;
			}
			if (CONTROLLER.agilityGrade <= 5)
			{
				float num = CONTROLLER.totalAgilitySubGrade - CONTROLLER.agilityGrade * 50;
				if (num + 10f < 50f)
				{
					num += 10f;
				}
				else
				{
					num = 0f;
					CONTROLLER.agilityGrade++;
				}
				//FirebaseAnalyticsManager.instance.logEvent("Upgrade", "Upgrade", CONTROLLER.userID);
				//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "Store_Up_Agility" });
				num = Mathf.RoundToInt(num);
				CONTROLLER.boughtAgilitySubGrade += 10;
			}
			else if (CONTROLLER.agilityGrade >= 6 && CONTROLLER.agilityGrade <= 9)
			{
				float num2 = CONTROLLER.totalAgilitySubGrade - 300 - (CONTROLLER.agilityGrade - 6) * 250;
				if (num2 + 10f < 250f)
				{
					num2 += 10f;
				}
				else
				{
					num2 = 0f;
					CONTROLLER.agilityGrade++;
				}
				//FirebaseAnalyticsManager.instance.logEvent("Upgrade", "Upgrade", CONTROLLER.userID);
				//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "Store_Up_Agility" });
				num2 = Mathf.RoundToInt(num2);
				CONTROLLER.boughtAgilitySubGrade += 10;
			}
			break;
		}
		ResetDetails();
		SavePlayerPrefs.SetPowerUpDetails();
		FillAnim(index);
		gradeFill[0].fillAmount = (float)CONTROLLER.powerGrade / 10f;
		gradeFill[1].fillAmount = (float)CONTROLLER.controlGrade / 10f;
		gradeFill[2].fillAmount = (float)CONTROLLER.agilityGrade / 10f;
	}

	public void HoverAnimation()
	{
		if (packIndex >= 0 && packIndex < 3)
		{
			packTween = packs[packIndex].transform.DOScale(new Vector3(1.02f, 1.02f, 1f), 0.75f).SetLoops(-1, LoopType.Yoyo).SetUpdate(isIndependentUpdate: true);
			glow[packIndex].SetActive(value: true);
		}
	}

	private void CheckForMaxUpgrade()
	{
		float num = 250f;
		float endValue = num / 250f;
		if (CONTROLLER.powerGrade == 10)
		{
			MaxUpgrade[0].SetActive(value: true);
			beforePayment[0].SetActive(value: false);
			lowerLimit[0].text = (int)(num / 250f * 100f) + "%";
			tween[0] = powerFill.DOFillAmount(endValue, 2f);
		}
		if (CONTROLLER.controlGrade == 10)
		{
			MaxUpgrade[1].SetActive(value: true);
			beforePayment[1].SetActive(value: false);
			lowerLimit[1].text = (int)(num / 250f * 100f) + "%";
			tween[1] = controlFill.DOFillAmount(endValue, 2f);
		}
		if (CONTROLLER.agilityGrade == 10)
		{
			MaxUpgrade[2].SetActive(value: true);
			beforePayment[2].SetActive(value: false);
			lowerLimit[2].text = (int)(num / 250f * 100f) + "%";
			tween[2] = agilityFill.DOFillAmount(endValue, 2f);
		}
		if (MaxUpgrade[0].activeInHierarchy)
		{
			afterPayment[0].SetActive(value: false);
			QuickPlayFreeEntry.Instance.FinishTime(1);
		}
		if (MaxUpgrade[1].activeInHierarchy)
		{
			afterPayment[1].SetActive(value: false);
			QuickPlayFreeEntry.Instance.FinishTime(2);
		}
		if (MaxUpgrade[2].activeInHierarchy)
		{
			afterPayment[2].SetActive(value: false);
			QuickPlayFreeEntry.Instance.FinishTime(3);
		}
	}

	private void OpenInsufficientTokensPopup()
	{
		if (ManageScene.activeSceneName() == "MainMenu")
		{
			Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
			CONTROLLER.PopupName = "insuffPopup";
			GameModeTWO.insuffStatus = "coins";
		}
		else
		{
			CONTROLLER.PopupName = "insuffPopupGround";
			Popups.insuffStatus = "coins";
		}
		Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(151);
		Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(123);
		Singleton<Popups>.instance.ShowMe();
	}

	//public void ReduceTokenAmount(int index)
	//{
	//	if (Singleton<AdIntegrate>.instance.CheckForInternet())
	//	{
	//		switch (index)
	//		{
	//		case 1:
	//			CoinAmount = Singleton<PaymentProcess>.instance.GenerateCoinValue(0);
	//			if (CONTROLLER.Coins >= CoinAmount)
	//			{
	//				SavePlayerPrefs.SaveUserCoins(-CoinAmount, CoinAmount, 0);
	//			}
	//			break;
	//		case 2:
	//			CoinAmount = Singleton<PaymentProcess>.instance.GenerateCoinValue(1);
	//			if (CONTROLLER.Coins >= CoinAmount)
	//			{
	//				SavePlayerPrefs.SaveUserCoins(-CoinAmount, CoinAmount, 0);
	//			}
	//			break;
	//		case 3:
	//			CoinAmount = Singleton<PaymentProcess>.instance.GenerateCoinValue(2);
	//			if (CONTROLLER.Coins >= CoinAmount)
	//			{
	//				SavePlayerPrefs.SaveUserCoins(-CoinAmount, CoinAmount, 0);
	//			}
	//			break;
	//		}
	//	}
	//	Singleton<PowerUps>.instance.ResetDetails();
	//	if (ManageScene.activeSceneName() == "MainMenu")
	//	{
	//		Singleton<GameModeTWO>.instance.ResetDetails();
	//	}
	//}

	private string ReplaceText(string original, string replace)
	{
		string result = string.Empty;
		Debug.Log(original + " " + replace + " ");
		if (original.Contains("#"))
		{
			result = original.Replace("#", replace);
		}
		return result;
	}

	//public void StartTimer(int index)
	//{
	//	if (Singleton<AdIntegrate>.instance.CheckForInternet())
	//	{
	//		if (Singleton<PowerUps>.instance.packTween != null && Singleton<PowerUps>.instance.packTween.IsPlaying())
	//		{
	//			Singleton<PowerUps>.instance.packTween.Pause();
	//			GameObject[] array = Singleton<PowerUps>.instance.glow;
	//			foreach (GameObject gameObject in array)
	//			{
	//				gameObject.SetActive(value: false);
	//			}
	//			GameObject[] array2 = Singleton<PowerUps>.instance.packs;
	//			foreach (GameObject gameObject2 in array2)
	//			{
	//				gameObject2.transform.localScale = Vector3.one;
	//			}
	//		}
	//		ReinitializeVariables();
	//		switch (index)
	//		{
	//		case 1:
	//			CoinAmount = Singleton<PaymentProcess>.instance.GenerateCoinValue(0);
	//			if (CONTROLLER.Coins >= int.Parse(tokenAmount[0].text))
	//			{
	//				float num3 = 0f;
	//				float num4 = 0f;
	//				if (CONTROLLER.powerGrade <= 5)
	//				{
	//					num3 = CONTROLLER.totalPowerSubGrade - CONTROLLER.powerGrade * 50;
	//					num4 = 50f;
	//				}
	//				else if (CONTROLLER.powerGrade >= 6 && CONTROLLER.powerGrade <= 9)
	//				{
	//					num3 = CONTROLLER.totalPowerSubGrade - 300 - (CONTROLLER.powerGrade - 6) * 250;
	//					num4 = 250f;
	//				}
	//				if (num3 + 10f < num4 || CONTROLLER.XPs >= XPmilestones[CONTROLLER.powerGrade] || CONTROLLER.powerGrade == 10)
	//				{
	//					//ReduceTokenAmount(1);
	//					if (ObscuredPrefs.HasKey(UBPrefs[index - 1]))
	//					{
	//						ObscuredPrefs.DeleteKey(UBPrefs[index - 1]);
	//					}
	//					ObscuredPrefs.SetInt(UpgradePrefs[index - 1], 1);
	//					QuickPlayFreeEntry.Instance.StartUpgradeTimer(1);
	//					Server_Connection.instance.Set_StoreUpgrades();
	//				}
	//				else
	//				{
	//					CONTROLLER.PopupName = "insuffXP";
	//					Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(125);
	//					Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(153);
	//					Singleton<Popups>.instance.content.text = ReplaceText(Singleton<Popups>.instance.content.text, XPmilestones[CONTROLLER.powerGrade].ToString());
	//					Singleton<Popups>.instance.ShowMe();
	//				}
	//			}
	//			else
	//			{
	//				OpenInsufficientTokensPopup();
	//			}
	//			break;
	//		case 2:
	//			CoinAmount = Singleton<PaymentProcess>.instance.GenerateCoinValue(1);
	//			if (CONTROLLER.Coins >= int.Parse(tokenAmount[1].text))
	//			{
	//				float num5 = 0f;
	//				float num6 = 0f;
	//				if (CONTROLLER.controlGrade <= 5)
	//				{
	//					num5 = CONTROLLER.totalControlSubGrade - CONTROLLER.controlGrade * 50;
	//					num6 = 50f;
	//				}
	//				else if (CONTROLLER.controlGrade >= 6 && CONTROLLER.controlGrade <= 9)
	//				{
	//					num5 = CONTROLLER.totalControlSubGrade - 300 - (CONTROLLER.controlGrade - 6) * 250;
	//					num6 = 250f;
	//				}
	//				if (num5 + 10f < num6 || CONTROLLER.XPs >= XPmilestones[CONTROLLER.controlGrade] || CONTROLLER.controlGrade == 10)
	//				{
	//					//ReduceTokenAmount(2);
	//					if (ObscuredPrefs.HasKey(UBPrefs[index - 1]))
	//					{
	//						ObscuredPrefs.DeleteKey(UBPrefs[index - 1]);
	//					}
	//					ObscuredPrefs.SetInt(UpgradePrefs[index - 1], 1);
	//					QuickPlayFreeEntry.Instance.StartUpgradeTimer(2);
	//					Server_Connection.instance.Set_StoreUpgrades();
	//				}
	//				else
	//				{
	//					CONTROLLER.PopupName = "insuffXP";
	//					Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(125);
	//					Singleton<Popups>.instance.content.text = "Minimum of " + XPmilestones[CONTROLLER.controlGrade] + "XP needed to upgrade this further.";
	//					Singleton<Popups>.instance.ShowMe();
	//				}
	//			}
	//			else
	//			{
	//				OpenInsufficientTokensPopup();
	//			}
	//			break;
	//		case 3:
	//			CoinAmount = Singleton<PaymentProcess>.instance.GenerateCoinValue(2);
	//			if (CONTROLLER.Coins >= int.Parse(tokenAmount[2].text))
	//			{
	//				float num = 0f;
	//				float num2 = 0f;
	//				if (CONTROLLER.agilityGrade <= 5)
	//				{
	//					num = CONTROLLER.totalAgilitySubGrade - CONTROLLER.agilityGrade * 50;
	//					num2 = 50f;
	//				}
	//				else if (CONTROLLER.agilityGrade >= 6 && CONTROLLER.agilityGrade <= 9)
	//				{
	//					num = CONTROLLER.totalAgilitySubGrade - 300 - (CONTROLLER.agilityGrade - 6) * 250;
	//					num2 = 250f;
	//				}
	//				if (num + 10f < num2 || CONTROLLER.XPs >= XPmilestones[CONTROLLER.agilityGrade] || CONTROLLER.agilityGrade == 10)
	//				{
	//					//ReduceTokenAmount(3);
	//					if (ObscuredPrefs.HasKey(UBPrefs[index - 1]))
	//					{
	//						ObscuredPrefs.DeleteKey(UBPrefs[index - 1]);
	//					}
	//					ObscuredPrefs.SetInt(UpgradePrefs[index - 1], 1);
	//					QuickPlayFreeEntry.Instance.StartUpgradeTimer(3);
	//					Server_Connection.instance.Set_StoreUpgrades();
	//				}
	//				else
	//				{
	//					CONTROLLER.PopupName = "insuffXP";
	//					Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(125);
	//					Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(153);
	//					Singleton<Popups>.instance.content.text = ReplaceText(Singleton<Popups>.instance.content.text, XPmilestones[CONTROLLER.agilityGrade].ToString());
	//					Singleton<Popups>.instance.ShowMe();
	//				}
	//			}
	//			else
	//			{
	//				OpenInsufficientTokensPopup();
	//			}
	//			break;
	//		}
	//	}
	//	else
	//	{
	//		CONTROLLER.PopupName = "noInternet";
	//		Singleton<Popups>.instance.ShowMe();
	//	}
	//}

	public void ReduceTime(int index)
	{
		QuickPlayFreeEntry.Instance.ReduceTime(index);
	}

	public void FinishTime(int index)
	{
		int num = 0;
		switch (index)
		{
		case 1:
			num = int.Parse(PowerFinishNowCoins.text);
			break;
		case 2:
			num = int.Parse(ControlFinishNowCoins.text);
			break;
		case 3:
			num = int.Parse(AgilityFinishNowCoins.text);
			break;
		}
		if (Singleton<PowerUps>.instance.packTween != null && Singleton<PowerUps>.instance.packTween.IsPlaying())
		{
			Singleton<PowerUps>.instance.packTween.Pause();
			GameObject[] array = Singleton<PowerUps>.instance.glow;
			foreach (GameObject gameObject in array)
			{
				gameObject.SetActive(value: false);
			}
			GameObject[] array2 = Singleton<PowerUps>.instance.packs;
			foreach (GameObject gameObject2 in array2)
			{
				gameObject2.transform.localScale = Vector3.one;
			}
		}
		if (CONTROLLER.Coins >= num)
		{
			SavePlayerPrefs.SaveUserCoins(-num, num, 0);
			QuickPlayFreeEntry.Instance.FinishTime(index);
			ResetDetails();
			return;
		}
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		if (ManageScene.activeSceneName() == "MainMenu")
		{
			CONTROLLER.PopupName = "insuffPopup";
			GameModeTWO.insuffStatus = "coins";
		}
		else
		{
			CONTROLLER.PopupName = "insuffPopupGround";
			Popups.insuffStatus = "coins";
		}
		Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(151) + "!";
		Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(123);
		Singleton<Popups>.instance.ShowMe();
	}

	public void TimerStarted(int index)
	{
		ReinitializeVariables();
		fillClone[index - 1].gameObject.SetActive(value: true);
		fillClone[index - 1].fillAmount = GetFillAmount(index - 1);
		timerStarted[index - 1] = true;
		AfterPaymentActivation(index - 1, status: true);
		CheckForMaxUpgrade();
		ChangeFinishNowCoins();
		Set_UpgradeTimer(index - 1, timer_set: true);
	}

	private void ChangeFinishNowCoins()
	{
		if (timerStarted[0])
		{
			float num = Singleton<PaymentProcess>.instance.GenerateTimerValue(0);
			double totalSeconds = QuickPlayFreeEntry.Instance.powerUBTime.TotalSeconds;
			string s = (totalSeconds / (double)num).ToString();
			float num2 = float.Parse(s);
			int num3 = Mathf.CeilToInt(num2 / 0.05f);
			int num4 = Singleton<PaymentProcess>.instance.GenerateFinishTimeCoinValue(0);
			PowerFinishNowCoins.text = (num4 * num3).ToString();
		}
		if (timerStarted[1])
		{
			float num5 = Singleton<PaymentProcess>.instance.GenerateTimerValue(1);
			double totalSeconds2 = QuickPlayFreeEntry.Instance.ControlUBTime.TotalSeconds;
			string s2 = (totalSeconds2 / (double)num5).ToString();
			float num6 = float.Parse(s2);
			int num7 = Mathf.CeilToInt(num6 / 0.05f);
			int num8 = Singleton<PaymentProcess>.instance.GenerateFinishTimeCoinValue(1);
			ControlFinishNowCoins.text = (num8 * num7).ToString();
		}
		if (timerStarted[2])
		{
			float num9 = Singleton<PaymentProcess>.instance.GenerateTimerValue(2);
			double totalSeconds3 = QuickPlayFreeEntry.Instance.AgilityUBTime.TotalSeconds;
			string s3 = (totalSeconds3 / (double)num9).ToString();
			float num10 = float.Parse(s3);
			int num11 = Mathf.CeilToInt(num10 / 0.05f);
			int num12 = Singleton<PaymentProcess>.instance.GenerateFinishTimeCoinValue(2);
			AgilityFinishNowCoins.text = (num12 * num11).ToString();
		}
	}

	public void TimerEnded(int index)
	{
		ReinitializeVariables();
		timerStarted[index - 1] = false;
		ObscuredPrefs.DeleteKey(UpgradePrefs[index - 1]);
		BuySubGrade(index - 1);
		AfterPaymentActivation(index - 1, status: false);
		CheckForMaxUpgrade();
		Set_UpgradeTimer(index - 1, timer_set: false);
		Server_Connection.instance.Set_StoreUpgrades();
	}

	public void Set_UpgradeTimer(int index, bool timer_set)
	{
		switch (index)
		{
		case 0:
			CONTROLLER.powerUpgradeTimerStarted = timer_set;
			break;
		case 1:
			CONTROLLER.controlUpgradeTimerStarted = timer_set;
			break;
		case 2:
			CONTROLLER.agilityUpgradeTimerStarted = timer_set;
			break;
		}
	}

	private void AfterPaymentActivation(int index, bool status)
	{
		fillClone[index].gameObject.SetActive(status);
		afterPayment[index].SetActive(status);
		beforePayment[index].SetActive(!status);
	}

	private void ReinitializeVariables()
	{
		prefix = new int[3]
		{
			CONTROLLER.powerGrade,
			CONTROLLER.controlGrade,
			CONTROLLER.agilityGrade
		};
		UBPrefs = new string[3] { "ubPowerUpgrade", "ubControlUpgrade", "ubAgilityUpgrade" };
		UpgradePrefs = new string[3] { "PowerUpgradeTimer", "ControlUpgradeTimer", "AgilityUpgradeTimer" };
		timerStarted = new bool[3]
		{
			CONTROLLER.powerUpgradeTimerStarted,
			CONTROLLER.controlUpgradeTimerStarted,
			CONTROLLER.agilityUpgradeTimerStarted
		};
	}

	private float GetFillAmount(int index)
	{
		switch (index)
		{
		case 0:
			if (CONTROLLER.totalPowerSubGrade < 300)
			{
				return (float)(CONTROLLER.totalPowerSubGrade + 10 - CONTROLLER.powerGrade * 50) / 50f;
			}
			if (CONTROLLER.totalPowerSubGrade >= 300)
			{
				int num3 = CONTROLLER.totalPowerSubGrade + 10 - 300 - (CONTROLLER.powerGrade - 6) * 250;
				return (float)num3 / 250f;
			}
			return 0f;
		case 1:
			if (CONTROLLER.totalControlSubGrade < 300)
			{
				return (float)(CONTROLLER.totalControlSubGrade + 10 - CONTROLLER.controlGrade * 50) / 50f;
			}
			if (CONTROLLER.totalControlSubGrade >= 300)
			{
				int num2 = CONTROLLER.totalControlSubGrade + 10 - 300 - (CONTROLLER.controlGrade - 6) * 250;
				return (float)num2 / 250f;
			}
			return 0f;
		case 2:
			if (CONTROLLER.totalAgilitySubGrade < 300)
			{
				return (float)(CONTROLLER.totalAgilitySubGrade + 10 - CONTROLLER.agilityGrade * 50) / 50f;
			}
			if (CONTROLLER.totalAgilitySubGrade >= 300)
			{
				int num = CONTROLLER.totalAgilitySubGrade + 10 - 300 - (CONTROLLER.agilityGrade - 6) * 250;
				return (float)num / 250f;
			}
			return 0f;
		default:
			return 0f;
		}
	}

	public void ShowMe()
	{
		ReinitializeVariables();
		Image image = powerFill;
		float num = 0f;
		agilityFill.fillAmount = num;
		num = num;
		controlFill.fillAmount = num;
		image.fillAmount = num;
		for (int i = 0; i < UpgradePrefs.Length; i++)
		{
			AfterPaymentActivation(i, ObscuredPrefs.HasKey(UpgradePrefs[i]));
		}
		ResetDetails();
		holder.SetActive(value: true);
		ValidatePowerUps();
		if (Server_Connection.instance.guestUser)
		{
			UpgradeUI_Reset();
		}
		else
		{
			CheckForMaxUpgrade();
		}
		Singleton<LoadPlayerPrefs>.instance.GetPowerUpDetails();
	}

	public void UpgradeUI_Reset()
	{
		for (int i = 0; i <= 2; i++)
		{
			MaxUpgrade[i].SetActive(value: false);
			beforePayment[i].SetActive(value: true);
		}
	}

	public void ResetDetails()
	{
		userXP.text = ObscuredPrefs.GetInt(CONTROLLER.XPKey).ToString();
		userCoins.text = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey).ToString();
		userTokens.text = ObscuredPrefs.GetInt(CONTROLLER.TicketsKey).ToString();
	}

	public void Close()
	{
		if (ManageScene.activeSceneName() == "MainMenu")
		{
			Singleton<GameModeTWO>.instance.ResetDetails();
			holder.SetActive(value: false);
			Singleton<GameModeTWO>.instance.ShowWithoutAnim();
			CONTROLLER.pageName = "landingPage";
		}
	}

	private void FillAnim(int index)
	{
		ReinitializeVariables();
		switch (index)
		{
		case 0:
			if (CONTROLLER.totalPowerSubGrade <= 300)
			{
				float num = (float)(CONTROLLER.totalPowerSubGrade - CONTROLLER.powerGrade * 50) / 50f;
				tween[index] = powerFill.DOFillAmount(num, 2f).SetUpdate(isIndependentUpdate: true);
				tokenAmount[0].text = Singleton<PaymentProcess>.instance.GenerateCoinValue(0).ToString();
				int num4 = Mathf.RoundToInt(num * 50f);
				lowerLimit[0].text = (int)((float)num4 / 50f * 100f) + "%";
				if (CONTROLLER.totalPowerSubGrade == 300)
				{
					lowerLimit[0].text = (int)((float)num4 / 250f * 100f) + "%";
				}
				grade[0].text = CONTROLLER.powerGrade + "/10";
			}
			else if (CONTROLLER.totalPowerSubGrade > 300)
			{
				float num = (float)(CONTROLLER.totalPowerSubGrade - 300) - (float)((CONTROLLER.powerGrade - 6) * 250);
				num /= 250f;
				tween[index] = powerFill.DOFillAmount(num, 2f).SetUpdate(isIndependentUpdate: true);
				tokenAmount[0].text = Singleton<PaymentProcess>.instance.GenerateCoinValue(0).ToString();
				int num5 = CONTROLLER.totalPowerSubGrade - 300 - (CONTROLLER.powerGrade - 6) * 250;
				lowerLimit[0].text = (int)((float)num5 / 250f * 100f) + "%";
				grade[0].text = CONTROLLER.powerGrade + "/10";
			}
			break;
		case 1:
			if (CONTROLLER.totalControlSubGrade <= 300)
			{
				float num = (float)(CONTROLLER.totalControlSubGrade - CONTROLLER.controlGrade * 50) / 50f;
				tween[index] = controlFill.DOFillAmount(num, 2f).SetUpdate(isIndependentUpdate: true);
				tokenAmount[1].text = Singleton<PaymentProcess>.instance.GenerateCoinValue(1).ToString();
				int num6 = Mathf.RoundToInt(num * 50f);
				lowerLimit[1].text = (int)((float)num6 / 50f * 100f) + "%";
				if (CONTROLLER.totalControlSubGrade == 300)
				{
					lowerLimit[1].text = (int)((float)num6 / 250f * 100f) + "%";
				}
				grade[1].text = CONTROLLER.controlGrade + "/10";
			}
			else if (CONTROLLER.totalControlSubGrade > 300)
			{
				float num = (float)(CONTROLLER.totalControlSubGrade - 300) - (float)((CONTROLLER.controlGrade - 6) * 250);
				num /= 250f;
				tween[index] = controlFill.DOFillAmount(num, 2f).SetUpdate(isIndependentUpdate: true);
				tokenAmount[1].text = Singleton<PaymentProcess>.instance.GenerateCoinValue(1).ToString();
				int num7 = CONTROLLER.totalControlSubGrade - 300 - (CONTROLLER.controlGrade - 6) * 250;
				lowerLimit[1].text = (int)((float)num7 / 250f * 100f) + "%";
				grade[1].text = CONTROLLER.controlGrade + "/10";
			}
			break;
		case 2:
			if (CONTROLLER.totalAgilitySubGrade <= 300)
			{
				float num = (float)(CONTROLLER.totalAgilitySubGrade - CONTROLLER.agilityGrade * 50) / 50f;
				tween[index] = agilityFill.DOFillAmount(num, 2f).SetUpdate(isIndependentUpdate: true);
				tokenAmount[2].text = Singleton<PaymentProcess>.instance.GenerateCoinValue(2).ToString();
				int num2 = Mathf.RoundToInt(num * 50f);
				lowerLimit[2].text = (int)((float)num2 / 50f * 100f) + "%";
				if (CONTROLLER.totalAgilitySubGrade == 300)
				{
					lowerLimit[2].text = (int)((float)num2 / 250f * 100f) + "%";
				}
				grade[2].text = CONTROLLER.agilityGrade + "/10";
			}
			else if (CONTROLLER.totalAgilitySubGrade > 300)
			{
				float num = (float)(CONTROLLER.totalAgilitySubGrade - 300) - (float)((CONTROLLER.agilityGrade - 6) * 250);
				num /= 250f;
				tween[index] = agilityFill.DOFillAmount(num, 2f).SetUpdate(isIndependentUpdate: true);
				tokenAmount[2].text = Singleton<PaymentProcess>.instance.GenerateCoinValue(2).ToString();
				int num3 = CONTROLLER.totalAgilitySubGrade - 300 - (CONTROLLER.agilityGrade - 6) * 250;
				lowerLimit[2].text = (int)((float)num3 / 250f * 100f) + "%";
				grade[2].text = CONTROLLER.agilityGrade + "/10";
			}
			break;
		}
	}

	public void ValidatePowerUps()
	{
		gradeFill[0].fillAmount = (float)CONTROLLER.powerGrade / 10f;
		gradeFill[1].fillAmount = (float)CONTROLLER.controlGrade / 10f;
		gradeFill[2].fillAmount = (float)CONTROLLER.agilityGrade / 10f;
		float[] array = new float[3];
		if (CONTROLLER.totalPowerSubGrade <= 300)
		{
			array[0] = (float)(CONTROLLER.totalPowerSubGrade - CONTROLLER.powerGrade * 50) / 50f;
			int num = Mathf.RoundToInt(array[0] * 50f);
			lowerLimit[0].text = (int)((float)num / 50f * 100f) + "%";
			tween[0] = powerFill.DOFillAmount(array[0], 2f).SetUpdate(isIndependentUpdate: true);
			grade[0].text = CONTROLLER.powerGrade + "/10";
		}
		else if (CONTROLLER.totalPowerSubGrade > 300)
		{
			array[0] = CONTROLLER.totalPowerSubGrade - 300 - (CONTROLLER.powerGrade - 6) * 250;
			float endValue = array[0] / 250f;
			lowerLimit[0].text = (int)(array[0] / 250f * 100f) + "%";
			tween[0] = powerFill.DOFillAmount(endValue, 2f).SetUpdate(isIndependentUpdate: true);
			grade[0].text = CONTROLLER.powerGrade + "/10";
		}
		if (CONTROLLER.totalControlSubGrade <= 300)
		{
			array[1] = (float)(CONTROLLER.totalControlSubGrade - CONTROLLER.controlGrade * 50) / 50f;
			int num2 = Mathf.RoundToInt(array[1] * 50f);
			lowerLimit[1].text = (int)((float)num2 / 50f * 100f) + "%";
			tween[1] = controlFill.DOFillAmount(array[1], 2f).SetUpdate(isIndependentUpdate: true);
			grade[1].text = CONTROLLER.controlGrade + "/10";
		}
		else if (CONTROLLER.totalControlSubGrade > 300)
		{
			array[1] = CONTROLLER.totalControlSubGrade - 300 - (CONTROLLER.controlGrade - 6) * 250;
			float endValue2 = array[1] / 250f;
			lowerLimit[1].text = (int)(array[1] / 250f * 100f) + "%";
			tween[1] = controlFill.DOFillAmount(endValue2, 2f).SetUpdate(isIndependentUpdate: true);
			grade[1].text = CONTROLLER.controlGrade + "/10";
		}
		if (CONTROLLER.totalAgilitySubGrade <= 300)
		{
			array[2] = (float)(CONTROLLER.totalAgilitySubGrade - CONTROLLER.agilityGrade * 50) / 50f;
			int num3 = Mathf.RoundToInt(array[2] * 50f);
			lowerLimit[2].text = (int)((float)num3 / 50f * 100f) + "%";
			tween[2] = agilityFill.DOFillAmount(array[2], 2f).SetUpdate(isIndependentUpdate: true);
			grade[2].text = CONTROLLER.agilityGrade + "/10";
		}
		else if (CONTROLLER.totalAgilitySubGrade > 300)
		{
			array[2] = CONTROLLER.totalAgilitySubGrade - 300 - (CONTROLLER.agilityGrade - 6) * 250;
			float endValue3 = array[2] / 250f;
			lowerLimit[2].text = (int)(array[2] / 250f * 100f) + "%";
			tween[2] = agilityFill.DOFillAmount(endValue3, 2f).SetUpdate(isIndependentUpdate: true);
			grade[2].text = CONTROLLER.agilityGrade + "/10";
		}
		for (int i = 0; i < 3; i++)
		{
			tokenAmount[i].text = Singleton<PaymentProcess>.instance.GenerateCoinValue(i).ToString();
		}
	}

	public void OpenHelp(GameObject help)
	{
		tempHelp = help;
		CONTROLLER.pageName = "powerHelp";
		help.transform.localScale = Vector3.zero;
		help.transform.DOScale(Vector3.one, 0.4f).SetUpdate(isIndependentUpdate: true);
	}

	public void CloseHelp(GameObject help)
	{
		help.transform.DOScale(Vector3.zero, 0.4f).SetUpdate(isIndependentUpdate: true);
		CONTROLLER.pageName = "store";
	}

	public int UpgradePower(int upgradeValue)
	{
		if (Singleton<Google_SignIn>.instance.googleAuthenticated != 1)
		{
			return -1;
		}
		if (CONTROLLER.powerGrade == 10 && CONTROLLER.totalPowerSubGrade > 1290)
		{
			return 2;
		}
		if (CONTROLLER.powerGrade <= 9 && CONTROLLER.totalPowerSubGrade <= 1290)
		{
			CONTROLLER.boughtPowerSubGrade += upgradeValue;
			SavePlayerPrefs.SetPowerUpDetails();
			Server_Connection.instance.Set_StoreUpgrades();
			return 1;
		}
		return 0;
	}

	public int UpgradeControl(int upgradeValue)
	{
		if (Singleton<Google_SignIn>.instance.googleAuthenticated != 1)
		{
			return -1;
		}
		if (CONTROLLER.controlGrade == 10 && CONTROLLER.totalControlSubGrade > 1290)
		{
			return 2;
		}
		if (CONTROLLER.controlGrade <= 9 && CONTROLLER.totalControlSubGrade <= 1290)
		{
			CONTROLLER.boughtControlSubGrade += upgradeValue;
			SavePlayerPrefs.SetPowerUpDetails();
			Server_Connection.instance.Set_StoreUpgrades();
			return 1;
		}
		return 0;
	}

	public int UpgradeAgility(int upgradeValue)
	{
		if (Singleton<Google_SignIn>.instance.googleAuthenticated != 1)
		{
			return -1;
		}
		if (CONTROLLER.agilityGrade == 10 && CONTROLLER.totalAgilitySubGrade > 1290)
		{
			return 2;
		}
		if (CONTROLLER.agilityGrade <= 9 && CONTROLLER.totalAgilitySubGrade <= 1290)
		{
			CONTROLLER.boughtAgilitySubGrade += upgradeValue;
			SavePlayerPrefs.SetPowerUpDetails();
			Server_Connection.instance.Set_StoreUpgrades();
			return 1;
		}
		return 0;
	}
}
