using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanelTransition : Singleton<LoadingPanelTransition>
{
	private AsyncOperation async;

	public Image[] BallImage;

	public GameObject Holder;

	public GameObject ModeLoading;

	public GameObject SceneLoading;

	public Image Box1;

	public Text LoadingVal;

	public int counter;

	private Coroutine loadingCoroutine;

	public void HideMe()
	{
		Singleton<NavigationBack>.instance.disableDeviceBack = false;
		if (Time.timeScale == 0f)
		{
			hideInLater();
		}
		else
		{
			Invoke("hideInLater", 0.2f);
		}
	}

	public void hideInLater()
	{
		StopLoadingSequence();
		Holder.SetActive(value: false);
		if (Singleton<NavigationBack>.instance != null)
		{
			Singleton<NavigationBack>.instance.disableDeviceBack = false;
		}
		if (Singleton<NavigationBackGround>.instance != null)
		{
			Singleton<NavigationBackGround>.instance.disableDeviceBack = false;
		}
	}

	public void PanelTransition()
	{
		if (ManageScene.activeSceneName() == "MainMenu")
		{
			Singleton<NavigationBack>.instance.disableDeviceBack = true;
			CancelInvoke("hideInLater");
			if (Singleton<NavigationBack>.instance != null)
			{
				Singleton<NavigationBack>.instance.disableDeviceBack = true;
			}
			if (Singleton<NavigationBackGround>.instance != null)
			{
				Singleton<NavigationBackGround>.instance.disableDeviceBack = true;
			}
			Holder.SetActive(value: true);
			SceneLoading.SetActive(value: false);
			ModeLoading.SetActive(value: true);
			if (loadingCoroutine == null)
			{
				loadingCoroutine = StartCoroutine(MoveLoadingBalls());
			}
		}
	}

	private IEnumerator MoveLoadingBalls()
	{
		if (counter >= 4)
		{
			Image[] ballImage = BallImage;
			foreach (Image image in ballImage)
			{
				image.color = new Vector4(1f, 1f, 1f, 0f);
			}
			counter = -1;
		}
		counter++;
		BallImage[counter].color = new Vector4(1f, 1f, 1f, 1f);
		if (counter > 0)
		{
			BallImage[counter - 1].color = new Vector4(1f, 1f, 1f, 0.5f);
		}
		if (counter > 1)
		{
			BallImage[counter - 2].color = new Vector4(1f, 1f, 1f, 0f);
		}
		yield return new WaitForSeconds(0.15f);
		loadingCoroutine = StartCoroutine(MoveLoadingBalls());
	}

	public void StopLoadingSequence()
	{
		if (loadingCoroutine != null)
		{
			StopCoroutine(loadingCoroutine);
			loadingCoroutine = null;
		}
	}

	public void PanelTransition1(string sceneName)
	{
		Singleton<NavigationBack>.instance.disableDeviceBack = true;
		CancelInvoke("hideInLater");
		CONTROLLER.pageName = string.Empty;
		Holder.SetActive(value: true);
		ModeLoading.SetActive(value: false);
		SceneLoading.SetActive(value: true);
		Box1.fillAmount = 0f;
		CallBack(sceneName);
	}

	private IEnumerator ShowProgress()
	{
		yield return null;
		Box1.fillAmount = async.progress;
		LoadingVal.text = LocalizationData.instance.getText(224) + "     " + (async.progress * 100f).ToString("F0") + LocalizationData.instance.getText(655);
		StartCoroutine("ShowProgress");
	}

	private void CallBack(string sceneName)
	{
		StartCoroutine(LoadMenuScene(sceneName));
	}

	public IEnumerator LoadMenuScene(string sceneName)
	{
		yield return StartCoroutine(ProceedLoadingMenuScene(sceneName));
	}

	public IEnumerator ProceedLoadingMenuScene(string sceneName)
	{
		yield return new WaitForSeconds(0.5f);
		StartCoroutine("ShowProgress");
		async = ManageScene.loadSceneAsync(sceneName);
	}
}
