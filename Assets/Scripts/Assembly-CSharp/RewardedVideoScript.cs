using UnityEngine;
using UnityEngine.UI;

public class RewardedVideoScript : Singleton<RewardedVideoScript>
{
	public GameObject RVPanel;

	public Text RVText;

	public bool isForRV;

	public Text YesButtonText;

	public Button[] RewardPanelButton;

	public void Start()
	{
		RVPanel.SetActive(value: false);
	}

	public void AutoPlayYesButton()
	{
		if (isForRV)
		{
			//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_AutoPlay" });
			//Singleton<AdIntegrate>.instance.showRewardedVideo(3);
			return;
		}
		int num = Singleton<PaymentProcess>.instance.GenerateAmount() / 2;
		if (num == 0)
		{
			num = 1;
		}
		if (CONTROLLER.tickets >= num)
		{
			SavePlayerPrefs.SaveUserTickets(-num, num, 0);
			Singleton<PauseGameScreen>.instance.autoPlayOperations();
		}
		else
		{
			Singleton<NavigationBack>.instance.tempDeviceBack = OpenPauseScreen;
			CONTROLLER.PopupName = "insuffTicketsGround";
			Singleton<Popups>.instance.content.text = LocalizationData.instance.getText(152);
			Singleton<Popups>.instance.topBarTitle.text = LocalizationData.instance.getText(124);
			Singleton<Popups>.instance.ShowMe();
		}
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	Singleton<AdIntegrate>.instance.requestRewardedVideo();
		//}
	}

	private void OpenPauseScreen()
	{
		RVPanel.SetActive(value: false);
		Singleton<PauseGameScreen>.instance.Hide(boolean: false);
	}

	public void AutoPlayNoButton()
	{
		RVPanel.SetActive(value: false);
		Singleton<PauseGameScreen>.instance.Hide(boolean: false);
		if ( ManageScene.activeSceneName() == "MainMenu")
		{
			Singleton<TossPageTWO>.instance.RV_Button.gameObject.SetActive(value: true);
		}
	}
}
