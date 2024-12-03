using System;
using System.Collections;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.UI;

public class IAP_Manager : Singleton<IAP_Manager>
{
	public Text[] tokenPrices;

	public Text[] coinPrices;

	public Text removeAdsPrice;

	public GameObject removeAdsButton;

	public GameObject AdRemovedText;

	public static string removeAds = "removeads";

	public string pendingVerificationID = "pendingverifyID";

	public string pendingVerificationReceipt = "pendingverifyReceipt";

	private int[] tokenValues = new int[5] { 10, 25, 160, 800, 3000 };

	private int[] ticketValues = new int[5] { 10, 25, 160, 800, 3000 };

	public int[] coinValues = new int[6];

	private int mainIndex;

	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void BuyTokenPack(int index)
	{
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	mainIndex = index;
		//	CONTROLLER.PopupName = "confirmPurchasePopup2";
		//	Singleton<Popups>.instance.ShowMe();
		//}
		//else
		//{
		//	CONTROLLER.PopupName = "noInternet";
		//	Singleton<Popups>.instance.ShowMe();
		//}
	}

	public void BuyTokenPack2()
	{
		BuyProductID("token" + mainIndex);
		Singleton<Popups>.instance.HideMe();
	}

	public void BuyCoinPack(int index)
	{
		if ( CONTROLLER.isGameDataSecure)
		{
			if (Google_SignIn.guestUserID == -1 && CONTROLLER.userID == 0)
			{
				Server_Connection.instance.guestUser = false;
				Server_Connection.instance.Get_User_Guest();
			}
			BuyProductID("coin" + index);
			Singleton<PowerUps>.instance.ResetDetails();
			if (ManageScene.activeSceneName() == "MainMenu")
			{
				Singleton<GameModeTWO>.instance.ResetDetails();
			}
		}
		else
		{
			CONTROLLER.PopupName = "noInternet";
			Singleton<Popups>.instance.ShowMe();
		}
	}

	public void BuyCoinPack2()
	{
		BuyProductID("coin" + mainIndex);
		Singleton<Popups>.instance.HideMe();
	}

	public void BuyTicketPack(int index)
	{
		mainIndex = index;
		CONTROLLER.PopupName = "confirmPurchasePopup3";
		Singleton<Popups>.instance.ShowMe();
	}

	public void BuyTicketPack2()
	{
		BuyProductID("ticket" + mainIndex);
		Singleton<Popups>.instance.HideMe();
	}

	public void BuyRemoveAds()
	{
		if ( CONTROLLER.isGameDataSecure)
		{
			if (Server_Connection.instance.RemoveAdsPurchased == 1)
			{
				CONTROLLER.PopupName = "RemoveAdsBuyPopup";
				Singleton<Popups>.instance.ShowMe();
			}
			else if (Server_Connection.instance.RemoveAdsPurchased == 0)
			{
				CONTROLLER.PopupName = "confirmPurchasePopup5";
				Singleton<Popups>.instance.ShowMe();
			}
		}
		else
		{
			CONTROLLER.PopupName = "noInternet";
			Singleton<Popups>.instance.ShowMe();
		}
	}

	public void showRemoveAdsText()
	{
		if (Server_Connection.instance.RemoveAdsPurchased == 1)
		{
			removeAdsPrice.text = LocalizationData.instance.getText(546);
		}
	}

	public void BuyRemoveAds2()
	{
		if (Server_Connection.instance.RemoveAdsPurchased == 0)
		{
			BuyProductID(removeAds);
			Singleton<Popups>.instance.HideMe();
		}
	}

	private void BuyProductID(string productId)
	{

	}

	public void AssignPricevalue()
	{
		if (coinPrices[0].text != string.Empty.ToString())
		{
			for (int i = 0; i <= 5; i++)
			{
				coinPrices[i].text = Server_Connection.instance.IAP_Values[i].ToString();
			}
			removeAdsPrice.text = Server_Connection.instance.IAP_Values[6].ToString();
		}
	}

	private void SuccessFullPurchase(string productId, string purchaseDetails)
	{
		StartCoroutine(updateStatusToServer(productId, purchaseDetails));
	}

	public IEnumerator updateStatusToServer(string productId, string purchaseDetails)
	{
		if (CONTROLLER.userID != 0)
		{
			Server_Connection.instance.guestUser = false;
		}
		else if (Google_SignIn.guestUserID != -1)
		{
			Server_Connection.instance.guestUser = true;
		}
		Singleton<LoadingPanelTransition>.instance.PanelTransition();
		PlayerPrefs.SetString(pendingVerificationID, productId);
		PlayerPrefs.SetString(pendingVerificationReceipt, purchaseDetails);
		WWWForm form = new WWWForm();
		form.AddField("action", "InApp");
		if (!Server_Connection.instance.guestUser)
		{
			form.AddField("uid", CONTROLLER.userID);
		}
		else if (Server_Connection.instance.guestUser)
		{
			form.AddField("uid", Google_SignIn.guestUserID);
		}
		form.AddField("pid", productId);
		form.AddField("pdet", purchaseDetails);
		WWW download = new WWW(CONTROLLER.PHP_Server_Link, form);
		yield return download;
		JSONNode node = JSONNode.Parse(download.text);
		if (!string.IsNullOrEmpty(download.error) || string.Empty + node["InApp"]["status"] == "0")
		{
			Singleton<LoadingPanelTransition>.instance.HideMe();
		}
		else
		{
			if (string.Empty + node["InApp"]["status"] == "1")
			{
				switch (productId)
				{
				case "coin1":
					AddCoins(0);
					break;
				case "coin2":
					AddCoins(1);
					break;
				case "coin3":
					AddCoins(2);
					break;
				case "coin4":
					AddCoins(3);
					break;
				case "coin5":
					AddCoins(4);
					break;
				case "coin6":
					AddCoins(5);
					break;
				case "ticket1":
					AddTickets(0);
					break;
				case "ticket2":
					AddTickets(1);
					break;
				case "ticket3":
					AddTickets(2);
					break;
				case "ticket4":
					AddTickets(3);
					break;
				case "ticket5":
					AddTickets(4);
					break;
				case "removeads":
					RemoveAds();
					break;
				}
				CONTROLLER.PopupName = "PurchaseSuccess";
				Singleton<Popups>.instance.ShowMe();
			}
			Singleton<LoadingPanelTransition>.instance.HideMe();
		}
		Singleton<LoadingPanelTransition>.instance.HideMe();
	}

	private void AddTokens(int index)
	{
		Singleton<Store>.instance.UpdateDetails();
		Singleton<PowerUps>.instance.ResetDetails();
		Singleton<GameModeTWO>.instance.ResetDetails();
		PlayerPrefs.DeleteKey(pendingVerificationID);
		PlayerPrefs.DeleteKey(pendingVerificationReceipt);
	}

	private void AddCoins(int index)
	{
		SavePlayerPrefs.SaveUserCoins(coinValues[index], 0, coinValues[index]);
		Singleton<Store>.instance.UpdateDetails();
		Singleton<PowerUps>.instance.ResetDetails();
		if (ManageScene.activeSceneName() == "MainMenu")
		{
			Singleton<GameModeTWO>.instance.ResetDetails();
		}
		PlayerPrefs.DeleteKey(pendingVerificationID);
		PlayerPrefs.DeleteKey(pendingVerificationReceipt);
		Server_Connection.instance.SyncPoints();
		//FirebaseAnalyticsManager.instance.logEvent("IAP", "IAP", CONTROLLER.userID);
		//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2]
		//{
		//	"ExtrasAction",
		//	"IAP_Coin" + coinValues[index] + "_Pass"
		//});
		Singleton<Store>.instance.userCoins.text = (CONTROLLER.Coins + coinValues[index]).ToString();
	}

	private void AddTickets(int index)
	{
		//FirebaseAnalyticsManager.instance.logEvent("Store", "Store", CONTROLLER.userID);
		//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2]
		//{
		//	"ExtrasAction",
		//	"Store_Ticket_" + ticketValues[index]
		//});
		Singleton<Store>.instance.userTickets.text = (int.Parse(Singleton<Store>.instance.userTickets.text) + tokenValues[index]).ToString();
		CONTROLLER.earnedTickets += tokenValues[index];
		Singleton<Store>.instance.UpdateDetails();
		SavePlayerPrefs.SaveUserTickets();
		Singleton<PowerUps>.instance.ResetDetails();
		Singleton<GameModeTWO>.instance.ResetDetails();
		PlayerPrefs.DeleteKey(pendingVerificationID);
		PlayerPrefs.DeleteKey(pendingVerificationReceipt);
	}

	private void RemoveAds()
	{
		PlayerPrefs.DeleteKey(pendingVerificationID);
		PlayerPrefs.DeleteKey(pendingVerificationReceipt);
		CONTROLLER.isAdPurchased = 1;
		//FirebaseAnalyticsManager.instance.logEvent("IAP", "IAP", CONTROLLER.userID);
		//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "IAP_RemoveAd_Pass" });
		//Singleton<AdIntegrate>.instance.RemoveAds();
		removeAdsPrice.text = LocalizationData.instance.getText(546);
		RemoveAdsonSpot();
		Server_Connection.instance.Get_User_Sync();
		Server_Connection.instance.RemoveAdsPurchased = 1;
		PlayerPrefs.SetInt("ud_removeads", Server_Connection.instance.RemoveAdsPurchased);
		PlayerPrefs.Save();
	}

	//public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	//{
	//	string text = product.metadata.localizedDescription.Split("|"[0]).GetValue(0).ToString()
	//		.Trim();
	//	if (product.definition.storeSpecificId == "removeads")
	//	{
	//		FirebaseAnalyticsManager.instance.logEvent("IAP", "IAP", CONTROLLER.userID);
	//		FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "IAP_Removead_Fail" });
	//		return;
	//	}
	//	FirebaseAnalyticsManager.instance.logEvent("IAP", "IAP", CONTROLLER.userID);
	//	if (product.definition.storeSpecificId == "coin1")
	//	{
	//		FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2]
	//		{
	//			"ExtrasAction",
	//			"IAP_Coin" + coinValues[0] + "_Fail"
	//		});
	//	}
	//	else if (product.definition.storeSpecificId == "coin2")
	//	{
	//		FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2]
	//		{
	//			"ExtrasAction",
	//			"IAP_Coin" + coinValues[1] + "_Fail"
	//		});
	//	}
	//	else if (product.definition.storeSpecificId == "coin3")
	//	{
	//		FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2]
	//		{
	//			"ExtrasAction",
	//			"IAP_Coin" + coinValues[2] + "_Fail"
	//		});
	//	}
	//	else if (product.definition.storeSpecificId == "coin4")
	//	{
	//		FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2]
	//		{
	//			"ExtrasAction",
	//			"IAP_Coin" + coinValues[3] + "_Fail"
	//		});
	//	}
	//	else if (product.definition.storeSpecificId == "coin5")
	//	{
	//		FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2]
	//		{
	//			"ExtrasAction",
	//			"IAP_Coin" + coinValues[4] + "_Fail"
	//		});
	//	}
	//	else if (product.definition.storeSpecificId == "coin6")
	//	{
	//		FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2]
	//		{
	//			"ExtrasAction",
	//			"IAP_Coin" + coinValues[5] + "_Fail"
	//		});
	//	}
	//}

	public void PurchasedRemoveAds()
	{
		removeAdsButton.gameObject.SetActive(value: false);
		AdRemovedText.gameObject.SetActive(value: true);
	}

	public void RemoveAdsDone()
	{
		if (Server_Connection.instance.RemoveAdsPurchased == 1)
		{
			removeAdsPrice.text = LocalizationData.instance.getText(546);
			//Singleton<AdIntegrate>.instance.RemoveAds();
			PurchasedRemoveAds();
		}
	}

	public void RemoveAdsonSpot()
	{
		removeAdsPrice.text = LocalizationData.instance.getText(546);
		//Singleton<AdIntegrate>.instance.RemoveAds();
		PurchasedRemoveAds();
	}

	public void Reset_PurchasedRemoveAds()
	{
		removeAdsButton.gameObject.SetActive(value: true);
		AdRemovedText.gameObject.SetActive(value: false);
		removeAdsPrice.text = Server_Connection.instance.IAP_Values[6].ToString();
	}
}
