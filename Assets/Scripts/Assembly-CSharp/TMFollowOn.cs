using UnityEngine;
using UnityEngine.UI;

public class TMFollowOn : Singleton<TMFollowOn>
{
	public GameObject holder;

	public GameObject AIDeclarationHolder;

	public Image flag;

	public Text TeamName;

	public Text declarationText;

	public void ShowAIDeclaration()
	{
		Singleton<NavigationBack>.instance.deviceBack = null;
		Singleton<PauseGameScreen>.instance.BG.SetActive(value: true);
		AIDeclarationHolder.SetActive(value: true);
		if (CONTROLLER.currentInnings < 2)
		{
			declarationText.text = LocalizationData.instance.getText(551);
		}
		else
		{
			declarationText.text = LocalizationData.instance.getText(552);
		}
	}

	public void AIDeclare()
	{
		AIDeclarationHolder.SetActive(value: false);
		Singleton<PauseGameScreen>.instance.declare();
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = null;
		Time.timeScale = 0f;
		holder.SetActive(value: true);
	}

	public void _yes()
	{
		CONTROLLER.isFollowOn = true;
		HideMe();
		Singleton<GameModel>.instance.showTargetScreen();
	}

	public void _no()
	{
		CONTROLLER.isFollowOn = false;
		HideMe();
		Singleton<GameModel>.instance.showTargetScreen();
	}

	private void HideMe()
	{
		holder.SetActive(value: false);
	}
}
