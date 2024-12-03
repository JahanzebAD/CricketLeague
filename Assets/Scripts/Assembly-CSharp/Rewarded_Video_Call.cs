using DG.Tweening;
using UnityEngine;

public class Rewarded_Video_Call : MonoBehaviour
{
	private void Start()
	{
	}

	public void RewardedVideo_Upgrade(int i)
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
		switch (i)
		{
		case 1:
			//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_UpgradePowerTimer" });
			//Singleton<AdIntegrate>.instance.showRewardedVideo(11);
			break;
		case 2:
			//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_UpgradeControlTimer" });
			//Singleton<AdIntegrate>.instance.showRewardedVideo(12);
			break;
		case 3:
			//FirebaseAnalyticsManager.instance.logEvent("RewardedVideoAds", new string[2] { "Ad_Network_Rewarded_Actions", "Spot_Generated_UpgradeAgilityTimer" });
			//Singleton<AdIntegrate>.instance.showRewardedVideo(13);
			break;
		}
	}
}
