using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RewardVideoLoadingScreen : MonoBehaviour
{
	public Image loadingBar;

	public Text loadedPercentText;

	public GameObject popup;

	private void OnEnable()
	{
		StartCoroutine(runLoadingTimer());
	}

	private IEnumerator runLoadingTimer()
	{
		Singleton<NavigationBack>.instance.disableDeviceBack = true;
		loadingBar.gameObject.SetActive(value: true);
		popup.SetActive(value: false);
		loadingBar.fillAmount = 0f;
		loadedPercentText.text = "0 %";
		yield return new WaitForSecondsRealtime(0.5f);
		//if (!Singleton<AdIntegrate>.instance.isStartedPlayingVideo)
		//{
		//	int initTimer = 15;
		//	int timer = initTimer;
		//	float incrementValue = 100f / ((float)initTimer * 100f);
		//	loadingBar.fillAmount = incrementValue;
		//	loadedPercentText.text = (int)(loadingBar.fillAmount * 100f) + " %";
		//	while (timer > 0)
		//	{
		//		yield return new WaitForSecondsRealtime(1f);
		//		timer--;
		//		loadingBar.fillAmount += incrementValue;
		//		loadedPercentText.text = (int)(loadingBar.fillAmount * 100f) + " %";
		//		if (Singleton<AdIntegrate>.instance.hasRewardedVideoAdLoaded())
		//		{
		//			Singleton<AdIntegrate>.instance.showRewardedVideo(CONTROLLER.rewardedVideoType, fromLoadingScreen: true);
		//			timer = 0;
		//			break;
		//		}
		//	}
		//	if (timer <= 0 && !Singleton<AdIntegrate>.instance.isStartedPlayingVideo)
		//	{
		//		loadingBar.gameObject.SetActive(value: false);
		//		popup.SetActive(value: true);
		//	}
		//	else
		//	{
		//		Singleton<NavigationBack>.instance.disableDeviceBack = false;
		//		Object.Destroy(base.gameObject);
		//	}
		//}
		//else
		//{
			Singleton<NavigationBack>.instance.disableDeviceBack = false;
			Object.Destroy(base.gameObject);
		//}
	}

	private void displayNoVideosAvailablePOpup()
	{
		if (CONTROLLER.pageName == "toss")
		{
			Singleton<TossPageTWO>.instance.Coin.SetActive(value: true);
		}
		Object.Destroy(base.gameObject);
	}

	public void retryButtonClick()
	{
		if (CONTROLLER.pageName == "toss")
		{
			Singleton<TossPageTWO>.instance.Coin.SetActive(value: false);
		}
		StartCoroutine(runLoadingTimer());
	}

	public void cancelButtonClick()
	{
		Singleton<NavigationBack>.instance.disableDeviceBack = false;
		popup.SetActive(value: false);
		displayNoVideosAvailablePOpup();
	}
}
