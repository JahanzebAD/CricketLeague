using System.Collections;
using LitJson;
using UnityEngine;

public class UpdateSettings : Singleton<UpdateSettings>
{
	public static int Buildno;

	public static int AppUpdate = 0;

	public static int AppForceUpdate = 0;

	public static int MultiplayerUpdate = 0;

	public static int MultiplayerMaintain = 0;

	public static int[] QuickPlayEntry = new int[3];

	public static int[] T20Entry = new int[3];

	public static int[] WorldcupEntry = new int[2];

	public static int[] NPLEntry = new int[2];

	public static int[,] CoinStoreDiscount = new int[6, 4];

	public static int[,] TicketStoreDiscount = new int[5, 3];

	public static int[] TicketStore = new int[5];

	private JsonData itemData;

	private string itemFilePath = "http://assets.cricketbuddies.in/wcc-lite/settings.json";

	private string jsonString;

	public bool isNetworkConnected;

	public bool fileDownloadCompleted;

	//private void Awake()
	//{
	//	StartCoroutine("CheckInternet");
	//}

	//public IEnumerator CheckInternet()
	//{
	//	yield return new WWW("http://clients3.google.com/generate_204");
	//	isNetworkConnected = Singleton<AdIntegrate>.instance.CheckForInternet();
	//	if (isNetworkConnected)
	//	{
	//		RetriveFile();
	//	}
	//}

	public void RetriveFile()
	{
		StartCoroutine("JsonConnect");
	}

	private IEnumerator JsonConnect()
	{
		if (itemFilePath.Contains("://"))
		{
			WWW www = new WWW(itemFilePath);
			yield return www;
			if (string.IsNullOrEmpty(www.error) && !string.IsNullOrEmpty(www.text))
			{
				fileDownloadCompleted = true;
				jsonString = www.text;
				itemData = JsonMapper.ToObject(jsonString);
				RetriveItemValue();
			}
			else
			{
				fileDownloadCompleted = false;
			}
		}
	}

	public void RetriveItemValue()
	{
		if (fileDownloadCompleted)
		{
			Buildno = int.Parse(itemData["Buildno"].ToString());
			AppUpdate = int.Parse(itemData["AppUpdate"].ToString());
			AppForceUpdate = int.Parse(itemData["AppForceUpdate"].ToString());
			MultiplayerUpdate = int.Parse(itemData["MultiplayerUpdate"].ToString());
			MultiplayerMaintain = int.Parse(itemData["MultiplayerMaintain"].ToString());
			CONTROLLER.canShowbannerMainmenu = int.Parse(itemData["BannerMenu"].ToString());
			CONTROLLER.canShowbannerGround = int.Parse(itemData["BannerGround"].ToString());
			CONTROLLER.interstitialshowDuration = int.Parse(itemData["Interstitialshow"].ToString());
			CONTROLLER.isGreedyGameEnabled = int.Parse(itemData["Greedy"].ToString());
			PlayerPrefs.SetInt("isGreedyGameEnabled", CONTROLLER.isGreedyGameEnabled);
			CONTROLLER.zaprEnabled = int.Parse(itemData["Zapr"].ToString());
			CONTROLLER.greedyRefreshRate = int.Parse(itemData["GreedyAdsRefresh"].ToString());
			CONTROLLER.greedyFloatAdEnabled = int.Parse(itemData["GreedyFloatAd"].ToString());
			if (AppForceUpdate == 1 && Buildno > CONTROLLER.AppVersionCode)
			{
				InterfaceHandler._instance.gameUpdatePopup.SetActive(value: true);
				InterfaceHandler._instance.gameupdatePopupNoButton.SetActive(value: false);
				InterfaceHandler._instance.gameupdatePopupYesButton.SetActive(value: true);
				InterfaceHandler._instance.gameupdatePopupYesButtonText.text = LocalizationData.instance.getText(388);
				InterfaceHandler._instance.gameUpdateContent.text = LocalizationData.instance.getText(387);
				InterfaceHandler._instance.gameUpdateTitle.text = LocalizationData.instance.getText(386);
			}
			else if (AppUpdate == 1 && Buildno > CONTROLLER.AppVersionCode)
			{
				InterfaceHandler._instance.gameUpdatePopup.SetActive(value: true);
				InterfaceHandler._instance.gameupdatePopupNoButton.SetActive(value: true);
				InterfaceHandler._instance.gameupdatePopupYesButton.SetActive(value: true);
				InterfaceHandler._instance.gameupdatePopupYesButtonText.text = LocalizationData.instance.getText(388);
				InterfaceHandler._instance.gameupdatePopupNoButtonText.text = LocalizationData.instance.getText(389);
				InterfaceHandler._instance.gameUpdateContent.text = LocalizationData.instance.getText(387);
				InterfaceHandler._instance.gameUpdateTitle.text = LocalizationData.instance.getText(386);
			}
			else if (Buildno > CONTROLLER.AppVersionCode)
			{
				InterfaceHandler._instance.gameUpdatePopup.SetActive(value: true);
				InterfaceHandler._instance.gameupdatePopupNoButton.SetActive(value: true);
				InterfaceHandler._instance.gameupdatePopupYesButton.SetActive(value: true);
				InterfaceHandler._instance.gameupdatePopupYesButtonText.text = LocalizationData.instance.getText(386);
				InterfaceHandler._instance.gameupdatePopupNoButtonText.text = LocalizationData.instance.getText(389);
				InterfaceHandler._instance.gameUpdateContent.text = LocalizationData.instance.getText(387);
				InterfaceHandler._instance.gameUpdateTitle.text = LocalizationData.instance.getText(386);
			}
		}
		CoinStoreDiscount[0, 0] = int.Parse(itemData["CoinStoreDiscount"][0]["CD1"][0].ToString());
		CoinStoreDiscount[0, 1] = int.Parse(itemData["CoinStoreDiscount"][0]["CD1"][1].ToString());
		CoinStoreDiscount[0, 2] = int.Parse(itemData["CoinStoreDiscount"][0]["CD1"][2].ToString());
		CoinStoreDiscount[0, 3] = int.Parse(itemData["CoinStoreDiscount"][0]["CD1"][3].ToString());
		CoinStoreDiscount[1, 0] = int.Parse(itemData["CoinStoreDiscount"][0]["CD2"][0].ToString());
		CoinStoreDiscount[1, 1] = int.Parse(itemData["CoinStoreDiscount"][0]["CD2"][1].ToString());
		CoinStoreDiscount[1, 2] = int.Parse(itemData["CoinStoreDiscount"][0]["CD2"][2].ToString());
		CoinStoreDiscount[1, 3] = int.Parse(itemData["CoinStoreDiscount"][0]["CD2"][3].ToString());
		CoinStoreDiscount[2, 0] = int.Parse(itemData["CoinStoreDiscount"][0]["CD3"][0].ToString());
		CoinStoreDiscount[2, 1] = int.Parse(itemData["CoinStoreDiscount"][0]["CD3"][1].ToString());
		CoinStoreDiscount[2, 2] = int.Parse(itemData["CoinStoreDiscount"][0]["CD3"][2].ToString());
		CoinStoreDiscount[2, 3] = int.Parse(itemData["CoinStoreDiscount"][0]["CD3"][3].ToString());
		CoinStoreDiscount[3, 0] = int.Parse(itemData["CoinStoreDiscount"][0]["CD4"][0].ToString());
		CoinStoreDiscount[3, 1] = int.Parse(itemData["CoinStoreDiscount"][0]["CD4"][1].ToString());
		CoinStoreDiscount[3, 2] = int.Parse(itemData["CoinStoreDiscount"][0]["CD4"][2].ToString());
		CoinStoreDiscount[3, 3] = int.Parse(itemData["CoinStoreDiscount"][0]["CD4"][3].ToString());
		CoinStoreDiscount[4, 0] = int.Parse(itemData["CoinStoreDiscount"][0]["CD5"][0].ToString());
		CoinStoreDiscount[4, 1] = int.Parse(itemData["CoinStoreDiscount"][0]["CD5"][1].ToString());
		CoinStoreDiscount[4, 2] = int.Parse(itemData["CoinStoreDiscount"][0]["CD5"][2].ToString());
		CoinStoreDiscount[4, 3] = int.Parse(itemData["CoinStoreDiscount"][0]["CD5"][3].ToString());
		CoinStoreDiscount[5, 0] = int.Parse(itemData["CoinStoreDiscount"][0]["CD6"][0].ToString());
		CoinStoreDiscount[5, 1] = int.Parse(itemData["CoinStoreDiscount"][0]["CD6"][1].ToString());
		CoinStoreDiscount[5, 2] = int.Parse(itemData["CoinStoreDiscount"][0]["CD6"][2].ToString());
		CoinStoreDiscount[5, 3] = int.Parse(itemData["CoinStoreDiscount"][0]["CD6"][3].ToString());
		TicketStoreDiscount[0, 0] = int.Parse(itemData["TicketStoreDiscount"][0]["SD1"][0].ToString());
		TicketStoreDiscount[0, 1] = int.Parse(itemData["TicketStoreDiscount"][0]["SD1"][1].ToString());
		TicketStoreDiscount[0, 2] = int.Parse(itemData["TicketStoreDiscount"][0]["SD1"][2].ToString());
		TicketStoreDiscount[1, 0] = int.Parse(itemData["TicketStoreDiscount"][0]["SD2"][0].ToString());
		TicketStoreDiscount[1, 1] = int.Parse(itemData["TicketStoreDiscount"][0]["SD2"][1].ToString());
		TicketStoreDiscount[1, 2] = int.Parse(itemData["TicketStoreDiscount"][0]["SD2"][2].ToString());
		TicketStoreDiscount[2, 0] = int.Parse(itemData["TicketStoreDiscount"][0]["SD3"][0].ToString());
		TicketStoreDiscount[2, 1] = int.Parse(itemData["TicketStoreDiscount"][0]["SD3"][1].ToString());
		TicketStoreDiscount[2, 2] = int.Parse(itemData["TicketStoreDiscount"][0]["SD3"][2].ToString());
		TicketStoreDiscount[3, 0] = int.Parse(itemData["TicketStoreDiscount"][0]["SD4"][0].ToString());
		TicketStoreDiscount[3, 1] = int.Parse(itemData["TicketStoreDiscount"][0]["SD4"][1].ToString());
		TicketStoreDiscount[3, 2] = int.Parse(itemData["TicketStoreDiscount"][0]["SD4"][2].ToString());
		TicketStoreDiscount[4, 0] = int.Parse(itemData["TicketStoreDiscount"][0]["SD5"][0].ToString());
		TicketStoreDiscount[4, 1] = int.Parse(itemData["TicketStoreDiscount"][0]["SD5"][1].ToString());
		TicketStoreDiscount[4, 2] = int.Parse(itemData["TicketStoreDiscount"][0]["SD5"][2].ToString());
		TicketStore[0] = int.Parse(itemData["TicketStore"][0]["TS1"].ToString());
		TicketStore[1] = int.Parse(itemData["TicketStore"][0]["TS2"].ToString());
		TicketStore[2] = int.Parse(itemData["TicketStore"][0]["TS3"].ToString());
		TicketStore[3] = int.Parse(itemData["TicketStore"][0]["TS4"].ToString());
		TicketStore[4] = int.Parse(itemData["TicketStore"][0]["TS5"].ToString());
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 4; j++)
			{
			}
		}
		for (int k = 0; k < 5; k++)
		{
			for (int l = 0; l < 3; l++)
			{
			}
		}
		for (int m = 0; m < 5; m++)
		{
		}
		Singleton<Store>.instance.UpdateStoreValues();
	}
}
