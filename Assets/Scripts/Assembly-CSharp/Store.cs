using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Store : Singleton<Store>
{
	public GameObject FindLocalPosition;

	public GameObject Holder;

	public GameObject Coin_Content;

	public GameObject Coin_ContentSub;

	public GameObject Token_Content;

	public GameObject Token_ContentSub;

	public GameObject Ticket_Content;

	public GameObject Ticket_ContentSub;

	public GameObject NoAds_Content;

	public GameObject NoAds_ContentSub;

	public GameObject IAPHolder;

	public GameObject[] IapInfo;

	public GameObject[] Upgrade_MaxGrade;

	public Vector3[] ReferencePositionWithDiscount = new Vector3[14];

	public Vector3[] ReferencePositionWithoutDiscount = new Vector3[6];

	public GameObject[] OnlineCoinGameObjects = new GameObject[42];

	public GameObject[] OnlineTicketGameObjects = new GameObject[35];

	public Text[] OnlineCoinText = new Text[24];

	public Text[] OnlineTicketText = new Text[15];

	public Text[] OnlineTicketAmount = new Text[5];

	private int[] CoinsPrice_Coins = new int[6] { 2000, 5000, 15000, 40000, 120000, 500000 };

	private int[] TokensPack_Coins = new int[6] { 20, 40, 80, 160, 200, 600 };

	public int[] CoinsPrice = new int[5] { 250, 1500, 5000, 12500, 25000 };

	public int[] TicketsPack = new int[5] { 10, 70, 250, 750, 1750 };

	public Text[] CoinsDiscountText = new Text[6];

	public GameObject[] RedPatchCoins = new GameObject[6];

	public int CurrentIndex;

	public int SelectedIndex;

	public int StoreOpenState;

	public Text[] HeaderText;

	public Sprite[] Buttons;

	public Text userCoins;

	public Text userTokens;

	public Text userXP;

	public Text userTickets;

	public Vector3[] AnimationClaim = new Vector3[5];

	public Button[] UIButtons;

	public Button[] TicketButtons;

	public int coinvalue;

	public int tokenvalue;

	public int TicketIndex;

	public int CoinsIndex;

	public AchievementAnimation[] AnimationAchieve = new AchievementAnimation[5];

	public GameObject StoreBlocker;

	public void Start()
	{
		StoreBlocker.SetActive(value: false);
		for (int i = 0; i < TicketButtons.Length; i++)
		{
			ref Vector3 reference = ref AnimationClaim[i];
			reference = TicketButtons[i].transform.position;
		}
		Holder.SetActive(value: false);
	}

	public void UpdateStoreValues()
	{
		OnlineCoinText[0].text = UpdateSettings.CoinStoreDiscount[0, 0].ToString();
		OnlineCoinText[1].text = "+" + UpdateSettings.CoinStoreDiscount[0, 1] + "%";
		OnlineCoinText[2].text = UpdateSettings.CoinStoreDiscount[0, 2].ToString();
		OnlineCoinText[4].text = UpdateSettings.CoinStoreDiscount[1, 0].ToString();
		OnlineCoinText[5].text = "+" + UpdateSettings.CoinStoreDiscount[1, 1] + "%";
		OnlineCoinText[6].text = UpdateSettings.CoinStoreDiscount[1, 2].ToString();
		OnlineCoinText[8].text = UpdateSettings.CoinStoreDiscount[2, 0].ToString();
		OnlineCoinText[9].text = "+" + UpdateSettings.CoinStoreDiscount[2, 1] + "%";
		OnlineCoinText[10].text = UpdateSettings.CoinStoreDiscount[2, 2].ToString();
		OnlineCoinText[12].text = UpdateSettings.CoinStoreDiscount[3, 0].ToString();
		OnlineCoinText[13].text = "+" + UpdateSettings.CoinStoreDiscount[3, 1] + "%";
		OnlineCoinText[14].text = UpdateSettings.CoinStoreDiscount[3, 2].ToString();
		OnlineCoinText[16].text = UpdateSettings.CoinStoreDiscount[4, 0].ToString();
		OnlineCoinText[17].text = "+" + UpdateSettings.CoinStoreDiscount[4, 1] + "%";
		OnlineCoinText[18].text = UpdateSettings.CoinStoreDiscount[4, 2].ToString();
		OnlineCoinText[20].text = UpdateSettings.CoinStoreDiscount[5, 0].ToString();
		OnlineCoinText[21].text = "+" + UpdateSettings.CoinStoreDiscount[5, 1] + "%";
		OnlineCoinText[22].text = UpdateSettings.CoinStoreDiscount[5, 2].ToString();
		OnlineTicketText[0].text = UpdateSettings.TicketStoreDiscount[0, 0].ToString();
		OnlineTicketText[1].text = "+" + UpdateSettings.TicketStoreDiscount[0, 1] + "%";
		OnlineTicketText[2].text = UpdateSettings.TicketStoreDiscount[0, 2].ToString();
		OnlineTicketText[3].text = UpdateSettings.TicketStoreDiscount[1, 0].ToString();
		OnlineTicketText[4].text = "+" + UpdateSettings.TicketStoreDiscount[1, 1] + "%";
		OnlineTicketText[5].text = UpdateSettings.TicketStoreDiscount[1, 2].ToString();
		OnlineTicketText[6].text = UpdateSettings.TicketStoreDiscount[2, 0].ToString();
		OnlineTicketText[7].text = "+" + UpdateSettings.TicketStoreDiscount[2, 1] + "%";
		OnlineTicketText[8].text = UpdateSettings.TicketStoreDiscount[2, 2].ToString();
		OnlineTicketText[9].text = UpdateSettings.TicketStoreDiscount[3, 0].ToString();
		OnlineTicketText[10].text = "+" + UpdateSettings.TicketStoreDiscount[3, 1] + "%";
		OnlineTicketText[11].text = UpdateSettings.TicketStoreDiscount[3, 2].ToString();
		OnlineTicketText[12].text = UpdateSettings.TicketStoreDiscount[4, 0].ToString();
		OnlineTicketText[13].text = "+" + UpdateSettings.TicketStoreDiscount[4, 1] + "%";
		OnlineTicketText[14].text = UpdateSettings.TicketStoreDiscount[4, 2].ToString();
		for (int i = 0; i < 5; i++)
		{
			OnlineTicketAmount[i].text = UpdateSettings.TicketStore[i].ToString();
		}
		CoinsPrice[0] = UpdateSettings.TicketStore[0];
		CoinsPrice[1] = UpdateSettings.TicketStore[1];
		CoinsPrice[2] = UpdateSettings.TicketStore[2];
		CoinsPrice[3] = UpdateSettings.TicketStore[3];
		CoinsPrice[4] = UpdateSettings.TicketStore[4];
		TicketsPack[0] = UpdateSettings.TicketStoreDiscount[0, 2];
		TicketsPack[1] = UpdateSettings.TicketStoreDiscount[1, 2];
		TicketsPack[2] = UpdateSettings.TicketStoreDiscount[2, 2];
		TicketsPack[3] = UpdateSettings.TicketStoreDiscount[3, 2];
		TicketsPack[4] = UpdateSettings.TicketStoreDiscount[4, 2];
		Singleton<IAP_Manager>.instance.coinValues[0] = UpdateSettings.CoinStoreDiscount[0, 2];
		Singleton<IAP_Manager>.instance.coinValues[1] = UpdateSettings.CoinStoreDiscount[1, 2];
		Singleton<IAP_Manager>.instance.coinValues[2] = UpdateSettings.CoinStoreDiscount[2, 2];
		Singleton<IAP_Manager>.instance.coinValues[3] = UpdateSettings.CoinStoreDiscount[3, 2];
		Singleton<IAP_Manager>.instance.coinValues[4] = UpdateSettings.CoinStoreDiscount[4, 2];
		Singleton<IAP_Manager>.instance.coinValues[5] = UpdateSettings.CoinStoreDiscount[5, 2];
		CoinsDiscountText[0].text = UpdateSettings.CoinStoreDiscount[0, 3] + "%";
		CoinsDiscountText[1].text = UpdateSettings.CoinStoreDiscount[1, 3] + "%";
		CoinsDiscountText[2].text = UpdateSettings.CoinStoreDiscount[2, 3] + "%";
		CoinsDiscountText[3].text = UpdateSettings.CoinStoreDiscount[3, 3] + "%";
		CoinsDiscountText[4].text = UpdateSettings.CoinStoreDiscount[4, 3] + "%";
		CoinsDiscountText[5].text = UpdateSettings.CoinStoreDiscount[5, 3] + "%";
		for (int j = 0; j < 6; j++)
		{
			if (CoinsDiscountText[j].text == "0%")
			{
				RedPatchCoins[j].SetActive(value: false);
			}
			else
			{
				RedPatchCoins[j].SetActive(value: true);
			}
		}
		for (int k = 0; k < 6; k++)
		{
			if (UpdateSettings.CoinStoreDiscount[k, 1] == 0)
			{
				Debug.LogWarning("!!!!!!!");
				OnlineCoinGameObjects[k * 7].transform.localPosition = ReferencePositionWithoutDiscount[0];
				OnlineCoinGameObjects[k * 7 + 1].transform.localPosition = ReferencePositionWithoutDiscount[1];
				OnlineCoinGameObjects[k * 7 + 6].transform.localPosition = ReferencePositionWithoutDiscount[2];
				OnlineCoinGameObjects[k * 7 + 2].SetActive(value: false);
				OnlineCoinGameObjects[k * 7 + 3].SetActive(value: false);
				OnlineCoinGameObjects[k * 7 + 4].SetActive(value: false);
				OnlineCoinGameObjects[k * 7 + 5].SetActive(value: false);
			}
			else
			{
				Debug.LogWarning("@@@@@@@");
				OnlineCoinGameObjects[k * 7].transform.localPosition = ReferencePositionWithDiscount[0];
				OnlineCoinGameObjects[k * 7 + 1].transform.localPosition = ReferencePositionWithDiscount[1];
				OnlineCoinGameObjects[k * 7 + 2].transform.localPosition = ReferencePositionWithDiscount[2];
				OnlineCoinGameObjects[k * 7 + 3].transform.localPosition = ReferencePositionWithDiscount[3];
				OnlineCoinGameObjects[k * 7 + 4].transform.localPosition = ReferencePositionWithDiscount[4];
				OnlineCoinGameObjects[k * 7 + 5].transform.localPosition = ReferencePositionWithDiscount[5];
				OnlineCoinGameObjects[k * 7 + 6].transform.localPosition = ReferencePositionWithDiscount[6];
				OnlineCoinGameObjects[k * 7 + 2].SetActive(value: true);
				OnlineCoinGameObjects[k * 7 + 3].SetActive(value: true);
				OnlineCoinGameObjects[k * 7 + 4].SetActive(value: true);
				OnlineCoinGameObjects[k * 7 + 5].SetActive(value: true);
			}
		}
		int num = 6;
		int num2 = 0;
		while (num < 11)
		{
			if (UpdateSettings.TicketStoreDiscount[num2, 1] == 0)
			{
				Debug.Log(num);
				OnlineTicketGameObjects[num2 * 7].transform.localPosition = ReferencePositionWithoutDiscount[3];
				OnlineTicketGameObjects[num2 * 7 + 1].transform.localPosition = ReferencePositionWithoutDiscount[4];
				OnlineTicketGameObjects[num2 * 7 + 6].transform.localPosition = ReferencePositionWithoutDiscount[5];
				OnlineTicketGameObjects[num2 * 7 + 2].SetActive(value: false);
				OnlineTicketGameObjects[num2 * 7 + 3].SetActive(value: false);
				OnlineTicketGameObjects[num2 * 7 + 4].SetActive(value: false);
				OnlineTicketGameObjects[num2 * 7 + 5].SetActive(value: false);
			}
			else
			{
				Debug.Log(num + "i");
				OnlineTicketGameObjects[num2 * 7].transform.localPosition = ReferencePositionWithDiscount[7];
				OnlineTicketGameObjects[num2 * 7 + 1].transform.localPosition = ReferencePositionWithDiscount[8];
				OnlineTicketGameObjects[num2 * 7 + 2].transform.localPosition = ReferencePositionWithDiscount[9];
				OnlineTicketGameObjects[num2 * 7 + 3].transform.localPosition = ReferencePositionWithDiscount[10];
				OnlineTicketGameObjects[num2 * 7 + 4].transform.localPosition = ReferencePositionWithDiscount[11];
				OnlineTicketGameObjects[num2 * 7 + 5].transform.localPosition = ReferencePositionWithDiscount[12];
				OnlineTicketGameObjects[num2 * 7 + 6].transform.localPosition = ReferencePositionWithDiscount[13];
				OnlineTicketGameObjects[num2 * 7 + 2].SetActive(value: true);
				OnlineTicketGameObjects[num2 * 7 + 3].SetActive(value: true);
				OnlineTicketGameObjects[num2 * 7 + 4].SetActive(value: true);
				OnlineTicketGameObjects[num2 * 7 + 5].SetActive(value: true);
			}
			num++;
			num2++;
		}
	}

	private int GetDiscountedPercentage(int amount)
	{
		return 100 - 10000 / (amount + 100);
	}

	public void IapInfoClose()
	{
		for (int i = 0; i <= 2; i++)
		{
			Singleton<PowerUps>.instance.CloseHelp(IapInfo[i]);
		}
	}

	public void CoinButtonClicked()
	{
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
			if (Google_SignIn.guestUserID == -1 && CONTROLLER.userID == 0)
			{
				Server_Connection.instance.guestUser = false;
				Server_Connection.instance.Get_User_Guest();
			}
			if (Singleton<PowerUps>.instance.packTween != null && Singleton<PowerUps>.instance.packTween.IsPlaying())
			{
				Singleton<PowerUps>.instance.packTween.Pause();
				GameObject[] glow = Singleton<PowerUps>.instance.glow;
				foreach (GameObject gameObject in glow)
				{
					gameObject.SetActive(value: false);
				}
				GameObject[] packs = Singleton<PowerUps>.instance.packs;
				foreach (GameObject gameObject2 in packs)
				{
					gameObject2.transform.localScale = Vector3.one;
				}
			}
			SelectedIndex = 0;
			if (CurrentIndex == SelectedIndex)
			{
				return;
			}
			CurrentIndex = 0;
			if (StoreOpenState != 0)
			{
				StoreOpenState = 1;
			}
			Singleton<StorePanelTransition>.instance.resetTransition();
			Singleton<StorePanelTransition>.instance.StoreState();
			Token_Content.SetActive(value: false);
			Coin_ContentSub.transform.localPosition = Vector3.zero;
			Ticket_Content.SetActive(value: false);
			IAPHolder.SetActive(value: false);
			NoAds_Content.SetActive(value: false);
			Coin_Content.SetActive(value: true);
			for (int k = 0; k < HeaderText.Length; k++)
			{
				if (k == 0)
				{
					UIButtons[k].image.sprite = Buttons[0];
					HeaderText[k].color = new Color32(46, 62, 85, byte.MaxValue);
				}
				else
				{
					UIButtons[k].image.sprite = Buttons[1];
					HeaderText[k].color = Color.white;
				}
			}
			if (Singleton<PowerUps>.instance != null)
			{
				IapInfoClose();
			}
		//}
		//else
		//{
		//	CONTROLLER.PopupName = "noInternet";
		//	Singleton<Popups>.instance.ShowMe();
		//}
	}

	public void RemoveAdsClicked()
	{
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
			if (Singleton<PowerUps>.instance.packTween != null && Singleton<PowerUps>.instance.packTween.IsPlaying())
			{
				Singleton<PowerUps>.instance.packTween.Pause();
				GameObject[] glow = Singleton<PowerUps>.instance.glow;
				foreach (GameObject gameObject in glow)
				{
					gameObject.SetActive(value: false);
				}
				GameObject[] packs = Singleton<PowerUps>.instance.packs;
				foreach (GameObject gameObject2 in packs)
				{
					gameObject2.transform.localScale = Vector3.one;
				}
			}
			if (Server_Connection.instance.RemoveAdsPurchased == 1)
			{
				Singleton<IAP_Manager>.instance.RemoveAdsDone();
			}
			SelectedIndex = 4;
			//if (!Singleton<AdIntegrate>.instance.CheckForInternet())
			//{
			//	CurrentIndex = -1;
			//}
			if (CurrentIndex == SelectedIndex)
			{
				return;
			}
			CurrentIndex = 4;
			if (Singleton<PowerUps>.instance != null)
			{
				IapInfoClose();
			}
			//if (Singleton<AdIntegrate>.instance.CheckForInternet())
			//{
				if (StoreOpenState != 0)
				{
					StoreOpenState = 1;
					Singleton<StorePanelTransition>.instance.resetTransition();
					Singleton<StorePanelTransition>.instance.StoreState();
				}
				NoAds_Content.SetActive(value: true);
				Ticket_Content.SetActive(value: false);
				IAPHolder.SetActive(value: false);
				Coin_Content.SetActive(value: false);
				for (int k = 0; k < HeaderText.Length; k++)
				{
					if (k == 4)
					{
						UIButtons[k].image.sprite = Buttons[0];
						HeaderText[k].color = new Color32(46, 62, 85, byte.MaxValue);
					}
					else
					{
						UIButtons[k].image.sprite = Buttons[1];
						HeaderText[k].color = Color.white;
					}
				}
			//}
			//else
			//{
			//	OpenNoInternetPopup();
			//}
		//}
		//else
		//{
		//	CONTROLLER.PopupName = "noInternet";
		//	Singleton<Popups>.instance.ShowMe();
		//}
	}

	public void TokenButtonClicked()
	{
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
			SelectedIndex = 1;
			//if (!Singleton<AdIntegrate>.instance.CheckForInternet())
			//{
			//	CurrentIndex = -1;
			//}
			if (CurrentIndex == SelectedIndex)
			{
				return;
			}
			CurrentIndex = 1;
			if (Singleton<PowerUps>.instance != null)
			{
				IapInfoClose();
			}
			//if (Singleton<AdIntegrate>.instance.CheckForInternet())
			//{
				StoreOpenState = 2;
				Singleton<StorePanelTransition>.instance.resetTransition();
				Singleton<StorePanelTransition>.instance.StoreState();
				Coin_Content.SetActive(value: false);
				Token_ContentSub.transform.localPosition = Vector3.zero;
				Ticket_Content.SetActive(value: false);
				IAPHolder.SetActive(value: false);
				NoAds_Content.SetActive(value: false);
				Token_Content.SetActive(value: true);
				for (int i = 0; i < HeaderText.Length; i++)
				{
					if (i == 1)
					{
						UIButtons[i].image.sprite = Buttons[0];
						HeaderText[i].color = new Color32(46, 62, 85, byte.MaxValue);
					}
					else
					{
						UIButtons[i].image.sprite = Buttons[1];
						HeaderText[i].color = Color.white;
					}
				}
			//}
			//else
			//{
			//	OpenNoInternetPopup();
			//}
		//}
		//else
		//{
		//	CONTROLLER.PopupName = "noInternet";
		//	Singleton<Popups>.instance.ShowMe();
		//}
	}

	public void OpenConfirmationPopupCoins(int index)
	{
	}

	//public void FreeRewards(int index)
	//{
	//	if (Singleton<AdIntegrate>.instance.CheckForInternet())
	//	{
	//		switch (index)
	//		{
	//		case 1:
	//			//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_FreeRewards" });
	//			Singleton<AdIntegrate>.instance.showRewardedVideo(1);
	//			break;
	//		case 2:
	//			//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_FreeToken" });
	//			Singleton<AdIntegrate>.instance.showRewardedVideo(9);
	//			break;
	//		case 3:
	//			//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_FreeTicket" });
	//			Singleton<AdIntegrate>.instance.showRewardedVideo(10);
	//			break;
	//		}
	//	}
	//	else
	//	{
	//		CONTROLLER.PopupName = "noInternet";
	//		Singleton<Popups>.instance.ShowMe();
	//	}
	//}

	public void OpenConfirmationPopupTicket(int index)
	{
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
			if (CONTROLLER.Coins >= CoinsPrice[index])
			{
				TicketIndex = index;
				Singleton<Store>.instance.BuyTicketsThruCoins();
				if (Singleton<Store>.instance.Holder.activeInHierarchy)
				{
					AnimationAchieve[index].CoinAnim(1);
				}
				if (ManageScene.activeSceneName() == "MainMenu")
				{
					Singleton<GameModeTWO>.instance.ResetDetails();
				}
				CONTROLLER.pageName = "store";
			}
			else
			{
				Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
				Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(151);
				Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(123);
				CONTROLLER.PopupName = "insuffPopup";
				if (ManageScene.activeSceneName() == "MainMenu")
				{
					GameModeTWO.insuffStatus = "coins";
				}
				Singleton<Popups>.instance.ShowMe();
			}
		//}
		//else
		//{
		//	CONTROLLER.PopupName = "noInternet";
		//	Singleton<Popups>.instance.ShowMe();
		//}
	}

	public void BuyCoins()
	{
		SavePlayerPrefs.SaveUserCoins(CoinsPrice_Coins[CoinsIndex], 0, CoinsPrice_Coins[CoinsIndex]);
		Singleton<ToastAnim>.instance.StartAnim();
		Singleton<ToastAnim>.instance.message.text = CoinsPrice_Coins[CoinsIndex] + "  " + LocalizationData.instance.getText(550) + "!";
		ResetDetails();
		Singleton<Popups>.instance.HideMe();
	}

	public void BuyTicketsThruCoins()
	{
		SavePlayerPrefs.SaveUserCoins(-CoinsPrice[TicketIndex], CoinsPrice[TicketIndex], 0);
		SavePlayerPrefs.SaveUserTickets(TicketsPack[TicketIndex], 0, TicketsPack[TicketIndex]);
		Singleton<ToastAnim>.instance.StartAnim();
		Server_Connection.instance.SyncPoints();
		Singleton<ToastAnim>.instance.message.text = LocalizationData.instance.getText(393) + "!";
		Singleton<ToastAnim>.instance.message.text = ReplaceText(Singleton<ToastAnim>.instance.message.text, TicketsPack[TicketIndex].ToString());
		Singleton<ToastAnim>.instance.message.font = Singleton<LocalizationText>.instance.fontAssets[LocalizationData.instance.languageIndex];
		ResetDetails();
		Singleton<Popups>.instance.HideMe();
	}

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

	public void ClosePopup()
	{
		CONTROLLER.pageName = "store";
	}

	public void TicketButtonClicked()
	{
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
			if (Singleton<PowerUps>.instance.packTween != null && Singleton<PowerUps>.instance.packTween.IsPlaying())
			{
				Singleton<PowerUps>.instance.packTween.Pause();
				GameObject[] glow = Singleton<PowerUps>.instance.glow;
				foreach (GameObject gameObject in glow)
				{
					gameObject.SetActive(value: false);
				}
				GameObject[] packs = Singleton<PowerUps>.instance.packs;
				foreach (GameObject gameObject2 in packs)
				{
					gameObject2.transform.localScale = Vector3.one;
				}
			}
			SelectedIndex = 2;
			if (CurrentIndex == SelectedIndex)
			{
				return;
			}
			CurrentIndex = 2;
			if (Singleton<PowerUps>.instance != null)
			{
				IapInfoClose();
			}
			StoreOpenState = 3;
			Singleton<StorePanelTransition>.instance.resetTransition();
			Singleton<StorePanelTransition>.instance.StoreState();
			Token_Content.SetActive(value: false);
			Ticket_ContentSub.transform.localPosition = Vector3.zero;
			Coin_Content.SetActive(value: false);
			IAPHolder.SetActive(value: false);
			NoAds_Content.SetActive(value: false);
			Ticket_Content.SetActive(value: true);
			for (int k = 0; k < HeaderText.Length; k++)
			{
				if (k == 2)
				{
					UIButtons[k].image.sprite = Buttons[0];
					HeaderText[k].color = new Color32(46, 62, 85, byte.MaxValue);
				}
				else
				{
					UIButtons[k].image.sprite = Buttons[1];
					HeaderText[k].color = Color.white;
				}
			}
		//}
		//else
		//{
		//	CONTROLLER.PopupName = "noInternet";
		//	Singleton<Popups>.instance.ShowMe();
		//}
	}

	public void PerformanceUpgradeButtonClicked()
	{
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
			//if (Singleton<Google_SignIn>.instance.googleAuthenticated == 1)
			//{
				SelectedIndex = 3;
				if (CurrentIndex == SelectedIndex)
				{
					return;
				}
				CurrentIndex = 3;
				if (Singleton<PowerUps>.instance != null)
				{
					IapInfoClose();
				}
				StoreOpenState = 4;
				Singleton<StorePanelTransition>.instance.resetTransition();
				Singleton<StorePanelTransition>.instance.StoreState();
				Coin_Content.SetActive(value: false);
				Token_Content.SetActive(value: false);
				Ticket_Content.SetActive(value: false);
				NoAds_Content.SetActive(value: false);
				Singleton<PowerUps>.instance.ShowMe();
				for (int i = 0; i < HeaderText.Length; i++)
				{
					if (i == 3)
					{
						UIButtons[i].image.sprite = Buttons[0];
						HeaderText[i].color = new Color32(46, 62, 85, byte.MaxValue);
					}
					else
					{
						UIButtons[i].image.sprite = Buttons[1];
						HeaderText[i].color = Color.white;
					}
				}
			//}
			//else
			//{
			//	CONTROLLER.PopupName = "googlesignin";
			//	Singleton<Popups>.instance.ShowMe();
			//}
		//}
		//else
		//{
		//	CONTROLLER.PopupName = "noInternet";
		//	Singleton<Popups>.instance.ShowMe();
		//}
	}

	public void RemoveAdsButtonClicked()
	{
		StoreOpenState = 5;
	}

	public void BackButtonClicked()
	{
		if (Singleton<PowerUps>.instance.packTween != null && Singleton<PowerUps>.instance.packTween.IsPlaying())
		{
			Singleton<PowerUps>.instance.packTween.Pause();
			GameObject[] glow = Singleton<PowerUps>.instance.glow;
			foreach (GameObject gameObject in glow)
			{
				gameObject.SetActive(value: false);
			}
			GameObject[] packs = Singleton<PowerUps>.instance.packs;
			foreach (GameObject gameObject2 in packs)
			{
				gameObject2.transform.localScale = Vector3.one;
			}
		}
		Holder.SetActive(value: false);
		FindObjectOfType<LoadingPanelTransition>().Holder.SetActive(false);
		Singleton<NavigationBack>.instance.deviceBack();
		CONTROLLER.pageName = string.Empty;
	}

	public void CoinsPackPurchaseButton1(int Tokenvalue)
	{
	}

	public void CoinsPackPurchaseButton2(int Coinvalue)
	{
	}

	public void HideMe()
	{
		Holder.SetActive(value: false);
		Singleton<GameModeTWO>.instance.showMe();
	}

	public void ShowMe()
	{
		if (Server_Connection.instance.guestUser)
		{
			SavePlayerPrefs.SetPowerUpDetails();
		}
		CONTROLLER.pageName = "store";
		Singleton<PowerUps>.instance.ResetDetails();
		if (ManageScene.activeSceneName() == "MainMenu")
		{
			Singleton<GameModeTWO>.instance.ResetDetails();
		}
		Singleton<Popups>.instance.HideMe();
		//Singleton<AdIntegrate>.instance.HideAd();
		ResetDetails();
		Singleton<Store>.instance.StoreOpenState = 0;
		CurrentIndex = 5;
		SelectedIndex = -1;
		CoinButtonClicked();
		Holder.SetActive(value: true);
	}

	public void ResetDetails()
	{
		userXP.text = ObscuredPrefs.GetInt(CONTROLLER.XPKey).ToString();
		userCoins.text = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey).ToString();
		userTickets.text = ObscuredPrefs.GetInt(CONTROLLER.TicketsKey).ToString();
	}

	public void UpdateDetails()
	{
		ObscuredPrefs.SetInt(CONTROLLER.XPKey, int.Parse(userXP.text));
		ObscuredPrefs.SetInt(CONTROLLER.CoinsKey, int.Parse(userCoins.text));
		ObscuredPrefs.SetInt(CONTROLLER.TicketsKey, int.Parse(userTickets.text));
		SavePlayerPrefs.EncryptionProtectData();
		CONTROLLER.XPs = ObscuredPrefs.GetInt(CONTROLLER.XPKey);
		CONTROLLER.Coins = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey);
		CONTROLLER.tickets = ObscuredPrefs.GetInt(CONTROLLER.TicketsKey);
		CONTROLLER.Coins = EncryptionService.instance.Tickets;
		CONTROLLER.tickets = EncryptionService.instance.Coins;
		CONTROLLER.XPs = EncryptionService.instance.XP;
	}

	public void UpdateCoinPackPurchase()
	{
	}

	public void BuyTokens(int index)
	{
		Singleton<IAP_Manager>.instance.BuyTokenPack(index);
	}

	public void BuyTickets(int index)
	{
		Singleton<IAP_Manager>.instance.BuyTicketPack(index);
	}

	public void OpenNoInternetPopup()
	{
		CONTROLLER.PopupName = "noInternet";
		Singleton<Popups>.instance.ShowMe();
	}

	public void CloseNoInternetPopup()
	{
		Singleton<Popups>.instance.HideMe();
	}
}
