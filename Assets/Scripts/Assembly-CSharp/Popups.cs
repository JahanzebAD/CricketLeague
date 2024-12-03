using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Popups : Singleton<Popups>
{
	public AchievementAnimation MainmenuAnimate;

	public AchievementAnimation StoreAnimate;

	public Button[] buttons;

	public AnimationCoinsFalling AnimationCoinFalling;

	public Image rewardedItemIcon;

	public Text rewardedItemText;

	public Text rewardedAmount;

	public Sprite[] earnedItemSprite;

	private string[] earnedItemText = new string[2] { "74", "172" };

	public Text topBarTitle;

	public Text content;

	public Text button1Text;

	public Text button2Text;

	public GameObject holder;

	public GameObject innerHolder;

	public GameObject innerContent;

	public GameObject BG;

	public GameObject coinReward;

	public GameObject fallingCoins;

	public static string insuffStatus = string.Empty;

	public Transform giftImg;

	public Transform btnText;

	public GameObject doubleRewardsBtn;

	public GameObject claimBtn;

	public Tweener giftTween;

	public Tweener textTween;

	public Text doubleRewardsText;

	public Text countText;

	private void ResetAnim()
	{
		innerHolder.transform.DOLocalMoveY(650f, 0f);
		BG.transform.DOScaleY(0f, 0f).SetUpdate(isIndependentUpdate: true);
		innerContent.transform.DOLocalMoveX(-950f, 0f);
	}

	private void Start()
	{
		textTween = btnText.DOPunchRotation(new Vector3(0f, 0f, 0.75f), 1f, 20, 0.8f).SetLoops(-1).SetUpdate(isIndependentUpdate: true);
		giftTween = giftImg.DOScale(Vector3.one, 0.5f).SetLoops(-1, LoopType.Yoyo).SetUpdate(isIndependentUpdate: true);
	}

	private void StartAnim()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, innerHolder.transform.DOLocalMoveY(-10f, 0.4f));
		sequence.Insert(0f, BG.transform.DOScaleY(1f, 0.35f));
		sequence.Insert(0.25f, innerContent.transform.DOLocalMoveX(0f, 0.25f));
		sequence.Insert(0.4f, innerHolder.transform.DOLocalMoveY(0f, 0.1f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}

	public void ShowCoinRewardPopup(int index, int amount)
	{
		CONTROLLER.tempPageName = CONTROLLER.pageName;
		CONTROLLER.pageName = string.Empty;
		rewardedItemIcon.DOFade(1f, 0f).SetUpdate(isIndependentUpdate: true);
		fallingCoins.SetActive(value: false);
		rewardedItemIcon.sprite = earnedItemSprite[index];
		rewardedItemText.text = LocalizationData.instance.getText(int.Parse(earnedItemText[index]));
		rewardedAmount.text = amount.ToString();
		if (FreeRewards.count == 0)
		{
			Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
			Singleton<NavigationBack>.instance.deviceBack = HideCoinRewardPopup;
		}
		coinReward.SetActive(value: true);
		if (index == 0)
		{
			if (FreeRewards.count < 4)
			{
				countText.text = (4 - FreeRewards.count).ToString();
				doubleRewardsBtn.SetActive(value: true);
				claimBtn.SetActive(value: false);
				int num = 100 + (FreeRewards.count + 1) * 50;
				doubleRewardsText.text = LocalizationData.instance.getText(182);
				doubleRewardsText.text = ReplaceText(doubleRewardsText.text, num.ToString());
			}
			else
			{
				doubleRewardsBtn.SetActive(value: false);
				claimBtn.SetActive(value: true);
			}
			fallingCoins.SetActive(value: true);
			rewardedItemIcon.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			AnimationCoinFalling.AnimationTransition(2);
		}
		else
		{
			doubleRewardsBtn.SetActive(value: false);
			claimBtn.SetActive(value: true);
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

	public void HideCoinRewardPopup()
	{
		if (Singleton<GameModeTWO>.instance != null && Singleton<GameModeTWO>.instance.Holder.activeInHierarchy)
		{
			MainmenuAnimate = Singleton<GameModeTWO>.instance.gameObject.GetComponent<AchievementAnimation>();
			MainmenuAnimate.CoinAnim(0);
		}
		else if (Singleton<Store>.instance.Holder.activeInHierarchy)
		{
			if (rewardedItemText.text == LocalizationData.instance.getText(74))
			{
				StoreAnimate.CoinAnim(0);
			}
			if (rewardedItemText.text == LocalizationData.instance.getText(172))
			{
				StoreAnimate.CoinAnim(1);
			}
		}
		FreeRewards.count = 0;
		FreeRewards.status = string.Empty;
		CONTROLLER.pageName = CONTROLLER.tempPageName;
		if (FreeRewards.count == 0)
		{
			Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		}
		coinReward.SetActive(value: false);
	}

	public void HideCoinRewardPopupDeviceback()
	{
		CONTROLLER.pageName = CONTROLLER.tempPageName;
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		coinReward.SetActive(value: false);
		CONTROLLER.pageName = "store";
	}

	public void ShowMe()
	{
		CONTROLLER.tempPageName = CONTROLLER.pageName;
		CONTROLLER.pageName = "popup";
		innerHolder.transform.DOLocalMoveY(650f, 0f);
		StartAnim();
		holder.SetActive(value: true);
		buttons[0].onClick.RemoveAllListeners();
		buttons[1].onClick.RemoveAllListeners();
		if (CONTROLLER.PopupName == "incompletePopup1")
		{
			buttons[0].gameObject.SetActive(value: true);
			buttons[1].gameObject.SetActive(value: true);
			buttons[0].onClick.AddListener(delegate
			{
				Singleton<IncompleteMatch>.instance.YesButton();
			});
			buttons[1].onClick.AddListener(delegate
			{
				Singleton<IncompleteMatch>.instance.NoButton();
			});
			topBarTitle.text = LocalizationData.instance.getText(115);
			content.text = LocalizationData.instance.getText(133);
			button1Text.text = LocalizationData.instance.getText(164);
			button2Text.text = LocalizationData.instance.getText(165);
		}
		else if (CONTROLLER.PopupName == "incompletePopup2")
		{
			buttons[0].gameObject.SetActive(value: true);
			buttons[1].gameObject.SetActive(value: true);
			buttons[0].onClick.AddListener(delegate
			{
				Singleton<IncompleteMatchTWO>.instance.YesButton();
			});
			buttons[1].onClick.AddListener(delegate
			{
				Singleton<IncompleteMatchTWO>.instance.NoButton();
			});
			topBarTitle.text = LocalizationData.instance.getText(402);
			content.text = LocalizationData.instance.getText(134) + " " + LocalizationData.instance.getText(135);
			button1Text.text = LocalizationData.instance.getText(164);
			button2Text.text = LocalizationData.instance.getText(166);
		}
		else if (CONTROLLER.PopupName == "toss")
		{
			buttons[0].gameObject.SetActive(value: true);
			buttons[1].gameObject.SetActive(value: true);
			buttons[0].onClick.AddListener(delegate
			{
				Singleton<TossPageTWO>.instance.RVSelected(1);
			});
			buttons[1].onClick.AddListener(delegate
			{
				Singleton<TossPageTWO>.instance.RVSelected(0);
			});
			topBarTitle.text = LocalizationData.instance.getText(117);
			content.text = LocalizationData.instance.getText(136);
			button1Text.text = LocalizationData.instance.getText(164);
			button2Text.text = LocalizationData.instance.getText(166);
		}
		else if (CONTROLLER.PopupName == "RemoveAdsBuyPopup")
		{
			buttons[1].gameObject.SetActive(value: true);
			buttons[0].gameObject.SetActive(value: false);
			buttons[1].onClick.AddListener(delegate
			{
				Singleton<Store>.instance.ShowMe();
				Singleton<Store>.instance.RemoveAdsClicked();
			});
			topBarTitle.text = LocalizationData.instance.getText(118);
			content.text = LocalizationData.instance.getText(137);
			button1Text.text = LocalizationData.instance.getText(167);
		}
		else if (CONTROLLER.PopupName == "squadWarning")
		{
			buttons[1].gameObject.SetActive(value: true);
			buttons[0].onClick.AddListener(delegate
			{
				Singleton<WarningTossTWO>.instance.YesButton();
			});
			buttons[1].onClick.AddListener(delegate
			{
				Singleton<WarningTossTWO>.instance.NoButton();
			});
			topBarTitle.text = LocalizationData.instance.getText(119);
			content.text = LocalizationData.instance.getText(138) + " " + LocalizationData.instance.getText(139);
			button1Text.text = LocalizationData.instance.getText(164);
			button2Text.text = LocalizationData.instance.getText(166);
		}
		else if (CONTROLLER.PopupName == "editPopup")
		{
			buttons[0].gameObject.SetActive(value: true);
			buttons[1].gameObject.SetActive(value: true);
			buttons[0].onClick.AddListener(delegate
			{
				Singleton<WarningEditPlayersTWO>.instance.YesButton();
			});
			buttons[1].onClick.AddListener(delegate
			{
				Singleton<WarningEditPlayersTWO>.instance.NoButton();
			});
			topBarTitle.text = LocalizationData.instance.getText(120);
			content.text = LocalizationData.instance.getText(140);
			button1Text.text = LocalizationData.instance.getText(164);
			button2Text.text = LocalizationData.instance.getText(166);
		}
		else if (CONTROLLER.PopupName == "errorPopup")
		{
			buttons[0].gameObject.SetActive(value: false);
			buttons[1].onClick.AddListener(delegate
			{
				HideMe();
			});
			topBarTitle.text = string.Empty;
			content.text = Singleton<ErrorPopupTWO>.instance.ErrorTxt.text;
			button2Text.text = LocalizationData.instance.getText(167);
		}
		else if (CONTROLLER.PopupName == "exitPopup")
		{
			buttons[0].gameObject.SetActive(value: true);
			buttons[1].gameObject.SetActive(value: true);
			buttons[0].onClick.AddListener(delegate
			{
				Singleton<GameModeTWO>.instance.GameExit();
			});
			buttons[1].onClick.AddListener(delegate
			{
				HideMe();
				Singleton<GameModeTWO>.instance.CloseExitPopup();
			});
			topBarTitle.text = LocalizationData.instance.getText(121);
			content.text = LocalizationData.instance.getText(149);
			button1Text.text = LocalizationData.instance.getText(164);
			button2Text.text = LocalizationData.instance.getText(166);
		}
		else if (CONTROLLER.PopupName == "MaxUpgradePopup")
		{
			buttons[0].gameObject.SetActive(value: true);
			buttons[1].gameObject.SetActive(value: false);
			buttons[0].onClick.AddListener(delegate
			{
				Singleton<Store>.instance.ShowMe();
				Singleton<Store>.instance.PerformanceUpgradeButtonClicked();
			});
			topBarTitle.text = LocalizationData.instance.getText(122);
			content.text = LocalizationData.instance.getText(150);
			button1Text.text = LocalizationData.instance.getText(167);
		}
		else if (CONTROLLER.PopupName == "insuffPopup")
		{
			buttons[0].gameObject.SetActive(value: true);
			buttons[1].gameObject.SetActive(value: true);
			buttons[0].onClick.AddListener(delegate
			{
				//if (Singleton<AdIntegrate>.instance.CheckForInternet())
				//{
				//	Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
				//	if (ManageScene.activeSceneName() == "MainMenu")
				//	{
				//		Singleton<GameModeTWO>.instance.GoToStorePage();
				//	}
				//	else
				//	{
				//		Singleton<Store>.instance.CoinButtonClicked();
				//		HideMe();
				//		CONTROLLER.pageName = "store";
				//	}
				//}
				//else
				//{
					if (ManageScene.activeSceneName() == "MainMenu")
					{
						Singleton<GameModeTWO>.instance.CloseInsufficientPopup();
					}
					else
					{
						HideMe();
					}
					CONTROLLER.PopupName = "noInternet";
					Singleton<Popups>.instance.ShowMe();
				//}
			});
			buttons[1].onClick.AddListener(delegate
			{
				if (ManageScene.activeSceneName() == "MainMenu")
				{
					Singleton<GameModeTWO>.instance.CloseInsufficientPopup();
				}
				else
				{
					HideMe();
				}
			});
			button1Text.text = LocalizationData.instance.getText(168);
			button2Text.text = LocalizationData.instance.getText(169);
		}
		else if (CONTROLLER.PopupName == "insuffPopupGround")
		{
			buttons[0].gameObject.SetActive(value: true);
			buttons[1].gameObject.SetActive(value: true);
			buttons[0].onClick.AddListener(delegate
			{
				//if (Singleton<AdIntegrate>.instance.CheckForInternet())
				//{
				//	Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
				//	CONTROLLER.pageName = "store";
				//	Singleton<GameOverDisplay>.instance.HideMe();
				//	Singleton<SuperOverResult>.instance.Holder.SetActive(value: false);
				//	Singleton<Store>.instance.ShowMe();
				//	HideMe();
				//	if (insuffStatus == "tickets")
				//	{
				//		Singleton<Store>.instance.TicketButtonClicked();
				//	}
				//}
				//else
				//{
					HideMe();
					CONTROLLER.PopupName = "noInternet";
					Singleton<Popups>.instance.ShowMe();
					holder.SetActive(value: true);
				//}
			});
			buttons[1].onClick.AddListener(delegate
			{
				HideMe();
			});
			button1Text.text = LocalizationData.instance.getText(168);
			button2Text.text = LocalizationData.instance.getText(169);
		}
		else if (CONTROLLER.PopupName == "insuffTicketsGround")
		{
			buttons[0].gameObject.SetActive(value: true);
			buttons[1].gameObject.SetActive(value: true);
			buttons[0].onClick.AddListener(delegate
			{
				//if (Singleton<AdIntegrate>.instance.CheckForInternet())
				//{
				//	Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
				//	CONTROLLER.pageName = "store";
				//	HideMe();
				//	Singleton<Store>.instance.ShowMe();
				//	Singleton<Store>.instance.TicketButtonClicked();
				//}
				//else
				//{
					HideMe();
					CONTROLLER.PopupName = "noInternet";
					Singleton<Popups>.instance.ShowMe();
					holder.SetActive(value: true);
				//}
			});
			buttons[1].onClick.AddListener(delegate
			{
				HideMe();
			});
			button1Text.text = LocalizationData.instance.getText(168);
			button2Text.text = LocalizationData.instance.getText(169);
		}
		else if (CONTROLLER.PopupName == "insuffXP")
		{
			buttons[0].gameObject.SetActive(value: true);
			buttons[1].gameObject.SetActive(value: false);
			buttons[0].onClick.AddListener(delegate
			{
				HideMe();
				CONTROLLER.pageName = "store";
			});
			button1Text.text = LocalizationData.instance.getText(167);
		}
		else if (CONTROLLER.PopupName == "confirmPurchasePopup" || CONTROLLER.PopupName == "confirmPurchasePopup2" || CONTROLLER.PopupName == "confirmPurchasePopup3" || CONTROLLER.PopupName == "confirmPurchasePopup5")
		{
			buttons[0].gameObject.SetActive(value: true);
			buttons[1].gameObject.SetActive(value: true);
			if (CONTROLLER.PopupName == "confirmPurchasePopup")
			{
				buttons[0].onClick.AddListener(delegate
				{
					Singleton<IAP_Manager>.instance.BuyCoinPack2();
					Singleton<PowerUps>.instance.ResetDetails();
					if (ManageScene.activeSceneName() == "MainMenu")
					{
						Singleton<GameModeTWO>.instance.ResetDetails();
					}
					CONTROLLER.pageName = "store";
				});
				buttons[1].onClick.AddListener(delegate
				{
					Singleton<Store>.instance.ShowMe();
				});
			}
			else if (CONTROLLER.PopupName == "confirmPurchasePopup2")
			{
				buttons[0].onClick.AddListener(delegate
				{
					Singleton<IAP_Manager>.instance.BuyTokenPack2();
					Singleton<PowerUps>.instance.ResetDetails();
					Singleton<GameModeTWO>.instance.ResetDetails();
					CONTROLLER.pageName = "store";
				});
				buttons[1].onClick.AddListener(delegate
				{
					Singleton<Store>.instance.ShowMe();
					Singleton<Store>.instance.TokenButtonClicked();
				});
			}
			else if (CONTROLLER.PopupName == "confirmPurchasePopup3")
			{
				buttons[0].onClick.AddListener(delegate
				{
					Singleton<Store>.instance.BuyTicketsThruCoins();
					if (ManageScene.activeSceneName() == "MainMenu")
					{
						Singleton<GameModeTWO>.instance.ResetDetails();
					}
					Singleton<PowerUps>.instance.ResetDetails();
					CONTROLLER.pageName = "store";
				});
				buttons[1].onClick.AddListener(delegate
				{
					Singleton<Store>.instance.ShowMe();
					Singleton<Store>.instance.TicketButtonClicked();
				});
			}
			else if (CONTROLLER.PopupName == "confirmPurchasePopup5")
			{
				buttons[0].onClick.AddListener(delegate
				{
					Singleton<IAP_Manager>.instance.BuyRemoveAds2();
					CONTROLLER.pageName = "store";
				});
				buttons[1].onClick.AddListener(delegate
				{
					Singleton<Store>.instance.ShowMe();
					Singleton<Store>.instance.RemoveAdsClicked();
				});
			}
			topBarTitle.text = LocalizationData.instance.getText(126);
			content.text = LocalizationData.instance.getText(154);
			button1Text.text = LocalizationData.instance.getText(164);
			button2Text.text = LocalizationData.instance.getText(166);
		}
		else if (CONTROLLER.PopupName == "noInternet")
		{
			buttons[0].gameObject.SetActive(value: false);
			buttons[1].gameObject.SetActive(value: true);
			buttons[1].onClick.AddListener(delegate
			{
				if (ManageScene.activeSceneName() == "MainMenu")
				{
					Singleton<GameModeTWO>.instance.CloseNoInternetPopup();
				}
				HideMe();
			});
			topBarTitle.text = LocalizationData.instance.getText(127);
			content.text = LocalizationData.instance.getText(155);
			button2Text.text = LocalizationData.instance.getText(167);
		}
		else if (CONTROLLER.PopupName == "noVid")
		{
			buttons[0].gameObject.SetActive(value: false);
			buttons[1].gameObject.SetActive(value: true);
			buttons[1].onClick.AddListener(delegate
			{
				HideMe();
			});
			topBarTitle.text = LocalizationData.instance.getText(128);
			content.text = LocalizationData.instance.getText(156);
			button2Text.text = LocalizationData.instance.getText(167);
		}
		else if (CONTROLLER.PopupName == "googlesignin")
		{
			buttons[0].gameObject.SetActive(value: false);
			buttons[1].gameObject.SetActive(value: true);
			buttons[1].onClick.AddListener(delegate
			{
				HideMe();
			});
			topBarTitle.text = LocalizationData.instance.getText(129);
			content.text = LocalizationData.instance.getText(157);
			button2Text.text = LocalizationData.instance.getText(167);
		}
		else if (CONTROLLER.PopupName == "FreeRewardsInfo")
		{
			buttons[0].gameObject.SetActive(value: false);
			buttons[1].gameObject.SetActive(value: true);
			buttons[1].onClick.AddListener(delegate
			{
				HideMe();
				if (Singleton<Store>.instance.Holder.activeInHierarchy)
				{
					CONTROLLER.pageName = "store";
				}
				else if (Singleton<GameModeTWO>.instance.Holder.activeInHierarchy)
				{
					CONTROLLER.pageName = "landingPage";
				}
			});
			topBarTitle.text = LocalizationData.instance.getText(130);
			content.text = LocalizationData.instance.getText(158);
			button2Text.text = LocalizationData.instance.getText(167);
		}
		else if (CONTROLLER.PopupName == "FreeTickets")
		{
			buttons[0].gameObject.SetActive(value: false);
			buttons[1].gameObject.SetActive(value: true);
			buttons[1].onClick.AddListener(delegate
			{
				HideMe();
				if (Singleton<Store>.instance.Holder.activeInHierarchy)
				{
					CONTROLLER.pageName = "store";
				}
				else if (Singleton<GameModeTWO>.instance.Holder.activeInHierarchy)
				{
					CONTROLLER.pageName = "landingPage";
				}
			});
			topBarTitle.text = LocalizationData.instance.getText(130);
			content.text = LocalizationData.instance.getText(159);
			button2Text.text = LocalizationData.instance.getText(167);
		}
		else if (CONTROLLER.PopupName == "PurchaseFailed")
		{
			buttons[0].gameObject.SetActive(value: false);
			buttons[1].gameObject.SetActive(value: true);
			buttons[1].onClick.AddListener(delegate
			{
				HideMe();
			});
			topBarTitle.text = LocalizationData.instance.getText(131);
			content.text = LocalizationData.instance.getText(160);
			button2Text.text = LocalizationData.instance.getText(167);
		}
		else if (CONTROLLER.PopupName == "PurchaseSuccess")
		{
			buttons[0].gameObject.SetActive(value: false);
			buttons[1].gameObject.SetActive(value: true);
			buttons[1].onClick.AddListener(delegate
			{
				HideMe();
				Singleton<PowerUps>.instance.ResetDetails();
			});
			topBarTitle.text = LocalizationData.instance.getText(131);
			content.text = LocalizationData.instance.getText(161);
			button2Text.text = LocalizationData.instance.getText(167);
		}
		else if (CONTROLLER.PopupName == "FreeTokens")
		{
			buttons[0].gameObject.SetActive(value: false);
			buttons[1].gameObject.SetActive(value: true);
			buttons[1].onClick.AddListener(delegate
			{
				HideMe();
				if (Singleton<Store>.instance.Holder.activeInHierarchy)
				{
					CONTROLLER.pageName = "store";
				}
				else if (Singleton<GameModeTWO>.instance.Holder.activeInHierarchy)
				{
					CONTROLLER.pageName = "landingPage";
				}
			});
			topBarTitle.text = LocalizationData.instance.getText(130);
			content.text = LocalizationData.instance.getText(159);
			button2Text.text = LocalizationData.instance.getText(167);
		}
		else if (CONTROLLER.PopupName == "InsufficientFreeSpin")
		{
			//if (Singleton<AdIntegrate>.instance.isRewardedVideoAvailable)
			//{
			//	buttons[1].gameObject.SetActive(value: true);
			//	buttons[0].gameObject.SetActive(value: true);
			//	buttons[0].onClick.AddListener(delegate
			//	{
			//	});
			//	buttons[1].onClick.AddListener(delegate
			//	{
			//	});
			//	topBarTitle.text = LocalizationData.instance.getText(132);
			//	content.text = LocalizationData.instance.getText(162);
			//	button1Text.text = LocalizationData.instance.getText(167);
			//	button2Text.text = LocalizationData.instance.getText(169);
			//}
			//else
			//{
				buttons[1].gameObject.SetActive(value: true);
				buttons[0].gameObject.SetActive(value: false);
				buttons[1].onClick.AddListener(delegate
				{
				});
				topBarTitle.text = LocalizationData.instance.getText(132);
				content.text = LocalizationData.instance.getText(370);
				button1Text.text = LocalizationData.instance.getText(167);
				//Singleton<AdIntegrate>.instance.requestRewardedVideo();
			//}
		}
		else if (CONTROLLER.PopupName == "nospin")
		{
			//if (Singleton<AdIntegrate>.instance.isRewardedVideoAvailable)
			//{
			//	buttons[1].gameObject.SetActive(value: true);
			//	buttons[0].gameObject.SetActive(value: true);
			//	buttons[0].onClick.AddListener(delegate
			//	{
			//		HideMe();
			//	});
			//	buttons[1].onClick.AddListener(delegate
			//	{
			//		ResetAnim();
			//		holder.SetActive(value: false);
			//	});
			//	topBarTitle.text = LocalizationData.instance.getText(132);
			//	content.text = LocalizationData.instance.getText(162);
			//	button1Text.text = LocalizationData.instance.getText(167);
			//	button2Text.text = LocalizationData.instance.getText(169);
			//}
			//else if (Singleton<AdIntegrate>.instance.isInterstitialAvailable)
			//{
			//	//FirebaseAnalyticsManager.instance.logEvent("InterstitialAds", new string[2] { "Ad_Network_Interstitial_Actions", "Spot_Generated_FreeSpin" });
			//	Singleton<AdIntegrate>.instance.DisplayInterestialAd();
			//	Singleton<GameModeTWO>.instance.GetExtraSpin();
			//}
			//else
			//{
				holder.SetActive(value: true);
				buttons[1].gameObject.SetActive(value: true);
				buttons[0].gameObject.SetActive(value: false);
				buttons[1].onClick.AddListener(delegate
				{
					ResetAnim();
					holder.SetActive(value: false);
				});
				topBarTitle.text = LocalizationData.instance.getText(132);
				content.text = LocalizationData.instance.getText(155);
				button2Text.text = LocalizationData.instance.getText(167);
			//	Singleton<AdIntegrate>.instance.requestRewardedVideo();
			//}
		}
		Singleton<PowerUps>.instance.ResetDetails();
		if (ManageScene.activeSceneName() == "MainMenu")
		{
			Singleton<GameModeTWO>.instance.ResetDetails();
		}
	}

	public void Setholderactive()
	{
		holder.SetActive(value: true);
	}

	public void HideMe()
	{
		CONTROLLER.pageName = CONTROLLER.tempPageName;
		if (ManageScene.activeSceneName() == "MainMenu" && Singleton<GameModeTWO>.instance.Holder.activeInHierarchy)
		{
			//Singleton<AdIntegrate>.instance.HideAd();
		}
		ResetAnim();
		holder.SetActive(value: false);
	}
}
