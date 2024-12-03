using UnityEngine;

public class DailyRewards : Singleton<DailyRewards>
{
	public AnimationCoinsFalling AnimationCoinFallingHolder;

	public GameObject holder;

	public bool showMe;

	private int claimAmount = 250;

	public bool claimed;

	public bool doubleBonus;

	public GameObject claimBtn;

	public GameObject RVBtn;

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = ClaimDailyReward;
		RVBtn.SetActive(value: true);
		CONTROLLER.pageName = "daily";
		holder.SetActive(value: true);
		showMe = false;
		claimed = false;
		AnimationCoinFallingHolder.AnimationTransition(3);
	}

	public void HideMe()
	{
		holder.SetActive(value: false);
	}

	public void ClaimDailyReward()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		claimed = true;
		if (!doubleBonus)
		{
			SavePlayerPrefs.SaveUserCoins(claimAmount, 0, claimAmount);
		}
		else
		{
			SavePlayerPrefs.SaveUserCoins(claimAmount * 2, 0, claimAmount * 2);
		}
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	Server_Connection.instance.SyncPoints();
		//}
		Singleton<GameModeTWO>.instance.ResetDetails();
		SetTimer();
		HideMe();
	}

	public void DoubleRewardSelected()
	{
		//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_DoubleDailyBonus" });
		//Singleton<AdIntegrate>.instance.showRewardedVideo(14);
	}

	public void SetTimer()
	{
		QuickPlayFreeEntry.Instance.SetDailyRewardsTimer();
	}
}
