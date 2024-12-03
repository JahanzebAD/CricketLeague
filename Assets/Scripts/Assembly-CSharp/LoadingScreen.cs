using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : Singleton<LoadingScreen>
{
	public Image fill;

	private AsyncOperation async;

	public void LoadingBarAnim()
	{
		CallBack();
	}

	private IEnumerator ShowProgress()
	{
		yield return null;
		fill.fillAmount = async.progress;
		StartCoroutine("ShowProgress");
	}

	private void CallBack()
	{
		StartCoroutine(LoadMenuScene());
	}

	public IEnumerator LoadMenuScene()
	{
		yield return StartCoroutine(ProceedLoadingMenuScene());
	}

	public IEnumerator ProceedLoadingMenuScene()
	{
		yield return new WaitForSeconds(0.5f);
		StartCoroutine("ShowProgress");
		async = ManageScene.loadSceneAsync("MainMenu");
	}
}
