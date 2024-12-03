using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class EntryFeesAndRewards : Singleton<EntryFeesAndRewards>
{
	private enum EntryFeesIndexes
	{
		QuickMatch2Overs,
		QuickMatch5Overs,
		QuickMatch10Overs,
		QuickMatch20Overs,
		QuickMatch30Overs,
		QuickMatch50Overs,
		WorldCup50Overs,
		WorldCup30Overs,
		WorldCup20Overs,
		T20Cup20Overs,
		T20Cup10Overs,
		NPL20Overs,
		NPL10Overs
	}

	public GameObject Holder;

	public GameObject RVPanel;

	public Text Title;

	public GameObject[] CoinPackage;

	public GameObject[] coinButtons;

	public Text[] oversText;

	public Text[] coinText;

	private bool sawRV;

	private bool showTotalAmountToBePaid;

	private int RVAmount = 125;

	public int[] FeesPaidList;

	private bool freeMode;

	private bool showRV;

	public GameObject test;

	public GameObject TMHolder;

	private int noOfPackage;

	private int[] quickMatchOvers = new int[3] { 3, 5, 10 };

	private int[] WCOvers = new int[3] { 20, 30, 50 };

	private int[] otherOvers = new int[2] { 10, 20 };

	private void Start()
	{
		freeMode = false;
		Holder.SetActive(value: false);
		test.SetActive(value: false);
		FeesPaidList = new int[13];
		for (int i = 0; i < FeesPaidList.Length; i++)
		{
			FeesPaidList[i] = 0;
		}
		DisableCoinPackages();
	}

	private void FindMatchEntryFee()
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			switch (quickMatchOvers[noOfPackage])
			{
			case 20:
				ProcessPayment(10, 3);
				break;
			case 30:
				ProcessPayment(15, 4);
				break;
			case 50:
				ProcessPayment(25, 5);
				break;
			case 3:
				SetOverKeys("QPOvers", 0);
				ProcessPayment(1, 0);
				break;
			case 5:
				SetOverKeys("QPOvers", 1);
				ProcessPayment(2, 1);
				break;
			case 10:
				SetOverKeys("QPOvers", 2);
				ProcessPayment(5, 2);
				break;
			}
		}
		if (CONTROLLER.PlayModeSelected == 3)
		{
			switch (WCOvers[noOfPackage])
			{
			case 20:
				SetOverKeys("WCOvers", 3);
				ProcessPayment(12000, 8);
				break;
			case 30:
				SetOverKeys("WCOvers", 4);
				ProcessPayment(18000, 7);
				break;
			case 50:
				SetOverKeys("WCOvers", 5);
				ProcessPayment(125, 6);
				break;
			}
		}
		if (CONTROLLER.PlayModeSelected == 1)
		{
			switch (otherOvers[noOfPackage])
			{
			case 20:
				SetOverKeys("T20Overs", 3);
				ProcessPayment(15000, 9);
				break;
			case 10:
				SetOverKeys("T20Overs", 2);
				ProcessPayment(7500, 10);
				break;
			}
		}
		if (CONTROLLER.PlayModeSelected == 2)
		{
			switch (otherOvers[noOfPackage])
			{
			case 20:
				SetOverKeys(CONTROLLER.tournamentType + "Overs", 3);
				ProcessPayment(7800, 11);
				break;
			case 10:
				SetOverKeys(CONTROLLER.tournamentType + "Overs", 2);
				ProcessPayment(7800, 12);
				break;
			}
		}
	}

	public void TestingMode()
	{
		test.SetActive(value: true);
		Text[] array = coinText;
		foreach (Text text in array)
		{
			text.text = "FREE";
		}
		freeMode = true;
	}

	public void ForTesting(int index)
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			SetOverKeys("QPOvers", index);
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			SetOverKeys("T20Overs", index);
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			SetOverKeys("NPLOvers", index);
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			SetOverKeys("WCOvers", index);
		}
		PaymentCompleted();
	}

	public void SelectOvers(int index)
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			if (freeMode)
			{
				SetOverKeys("QPOvers", index);
				CONTROLLER.oversSelectedIndex = index;
				PaymentCompleted();
				return;
			}
			PaymentDetails.InitPaymentValues();
			string key = "0" + index;
			int num = PaymentDetails.PaymentAmount[key];
			SetOverKeys("QPOvers", index);
			CONTROLLER.oversSelectedIndex = index;
			PaymentCompleted();
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			SetOverKeys("T20Overs", index + 2);
			CONTROLLER.oversSelectedIndex = index + 2;
			PaymentCompleted();
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			SetOverKeys(CONTROLLER.tournamentType + "Overs", index + 2);
			CONTROLLER.oversSelectedIndex = index + 2;
			PaymentCompleted();
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			SetOverKeys("WCOvers", index + 3);
			CONTROLLER.oversSelectedIndex = index + 3;
			PaymentCompleted();
		}
		//GoogleAllAds.Instance.InterstitialAdPlz();
	}

	public void SetOverKeys(string mode, int index)
	{
		ObscuredPrefs.SetInt(mode, index);
	}

	private void ProcessPayment(int totalFee, int feeListIndex)
	{
		if (showTotalAmountToBePaid)
		{
			SetCoinsToBePaid(totalFee - FeesPaidList[feeListIndex]);
			showTotalAmountToBePaid = false;
		}
		else if (sawRV)
		{
			sawRV = false;
			FeesPaidList[feeListIndex] += RVAmount;
			if (FeesPaidList[feeListIndex] >= totalFee)
			{
				FeesPaidList[feeListIndex] = 0;
				PaymentCompleted();
			}
			else
			{
				SetCoinsToBePaid(totalFee - FeesPaidList[feeListIndex]);
			}
		}
		else if (totalFee - FeesPaidList[feeListIndex] <= CONTROLLER.Coins)
		{
			SavePlayerPrefs.SaveUserCoins(FeesPaidList[feeListIndex] - totalFee, totalFee - FeesPaidList[feeListIndex], 0);
			FeesPaidList[feeListIndex] = 0;
			PaymentCompleted();
		}
		else
		{
			HasInsufficientCoin();
		}
	}

	private void DisableCoinPackages()
	{
		GameObject[] coinPackage = CoinPackage;
		foreach (GameObject gameObject in coinPackage)
		{
			gameObject.SetActive(value: false);
		}
	}

	public void ShowTotalAmountToBePaid()
	{
		DisableCoinPackages();
		if (CONTROLLER.PlayModeSelected == 0)
		{
			Title.text = LocalizationData.instance.getText(183);
			for (int i = 0; i < quickMatchOvers.Length; i++)
			{
				CoinPackage[i].SetActive(value: true);
				oversText[i].text = quickMatchOvers[i].ToString();
				showTotalAmountToBePaid = true;
				noOfPackage = i;
				FindMatchEntryFee();
			}
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			Title.text = LocalizationData.instance.getText(561);
			for (int i = 0; i < WCOvers.Length; i++)
			{
				CoinPackage[i].SetActive(value: true);
				oversText[i].text = WCOvers[i].ToString();
				showTotalAmountToBePaid = true;
				noOfPackage = i;
				FindMatchEntryFee();
			}
		}
		else
		{
			if (CONTROLLER.PlayModeSelected != 1 && CONTROLLER.PlayModeSelected != 2)
			{
				return;
			}
			if (CONTROLLER.PlayModeSelected == 1)
			{
				Title.text = LocalizationData.instance.getText(396);
			}
			else if (CONTROLLER.PlayModeSelected == 2)
			{
				if (CONTROLLER.tournamentType == "PAK")
				{
					Title.text = LocalizationData.instance.getText(428) + " - " + LocalizationData.instance.getText(411);
				}
				else if (CONTROLLER.tournamentType == "NPL")
				{
					Title.text = LocalizationData.instance.getText(427);
				}
				else if (CONTROLLER.tournamentType == "AUS")
				{
					Title.text = LocalizationData.instance.getText(16) + " - " + LocalizationData.instance.getText(411);
				}
			}
			for (int i = 0; i < otherOvers.Length; i++)
			{
				CoinPackage[i].SetActive(value: true);
				oversText[i].text = otherOvers[i].ToString();
				showTotalAmountToBePaid = true;
				noOfPackage = i;
				FindMatchEntryFee();
			}
		}
	}

	public void EntryFeeRewardVideoCall(int index)
	{
		//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_EntryFee" });
		//Singleton<AdIntegrate>.instance.showRewardedVideo(4);
		noOfPackage = index;
	}

	public void RVDeduction()
	{
		sawRV = true;
		showTotalAmountToBePaid = false;
		FindMatchEntryFee();
	}

	public void MakePaymentByCoins(int index)
	{
	}

	private void SetCoinsToBePaid(int value)
	{
		coinText[noOfPackage].text = value.ToString();
	}

	private void HasInsufficientCoin()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = ShowMe;
		CONTROLLER.PopupName = "insuffPopup";
		GameModeTWO.insuffStatus = "coins";
		Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(151);
		Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(123);
		Singleton<Popups>.instance.ShowMe();
	}

	private void PaymentCompleted()
	{
		freeMode = false;
		test.SetActive(value: false);
		SavePlayerPrefs.SaveUserCoins();
		Singleton<TeamSelectionTWO>.instance.showMe();
		HideMe();
	}

	public void TMTotalMatches(int index)
	{
		CONTROLLER.TotalTestMatches = index;
	}

	public void TMTesting()
	{
		SetOverKeys("TMOvers", 0);
		CONTROLLER.oversSelectedIndex = 0;
		PaymentCompleted();
	}

	public void TMSelectOvers(int index)
	{
		PaymentDetails.InitPaymentValues();
		SetOverKeys("TMOvers", index + 6);
		int num = Singleton<PaymentProcess>.instance.GenerateAmount();
		if (CONTROLLER.tickets >= num)
		{
			SetOverKeys("TMOvers", index + 6);
			CONTROLLER.oversSelectedIndex = index + 6;
			PaymentCompleted();
			return;
		}
		int num2 = Singleton<TemporaryStore>.instance.CalculateIndexValue(num);
		if (Singleton<Store>.instance.CoinsPrice[num2] <= CONTROLLER.Coins)
		{
			Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
			Singleton<NavigationBack>.instance.deviceBack = Singleton<TemporaryStore>.instance.CancelButton;
			Singleton<TemporaryStore>.instance.ShowMe(num2);
			return;
		}
		Singleton<NavigationBack>.instance.tempDeviceBack = ShowMe;
		GameModeTWO.insuffStatus = "tickets";
		CONTROLLER.PopupName = "insuffPopup";
		Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(152) + "!";
		Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(124);
		Singleton<Popups>.instance.ShowMe();
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = Back;
		if (CONTROLLER.PlayModeSelected == 0)
		{
			RVPanel.SetActive(value: false);
		}
		else
		{
			RVPanel.SetActive(value: false);
		}
		CONTROLLER.pageName = "entryFees";
		Singleton<EntryFeesPanelTransition>.instance.PanelTransition();
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TMHolder.SetActive(value: true);
		}
		else
		{
			Holder.SetActive(value: true);
		}
		ShowTotalAmountToBePaid();
		if (ObscuredPrefs.HasKey("freeEntry") && CONTROLLER.PlayModeSelected == 0)
		{
			TestingMode();
		}
		else
		{
			test.SetActive(value: false);
		}
	}

	private void HideMe()
	{
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TMHolder.SetActive(value: false);
		}
		else
		{
			Holder.SetActive(value: false);
		}
	}

	public void Back()
	{
		HideMe();
		Singleton<GameModeTWO>.instance.showMe();
	}
}
