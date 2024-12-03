using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class WarningTossTWO : Singleton<WarningTossTWO>
{
	public GameObject Holder;

	public Text ErrorTxt;

	protected void Start()
	{
		hideMe();
	}

	public void showMe()
	{
		CONTROLLER.PopupName = "squadWarning";
		ErrorTxt.text = LocalizationData.instance.getText(646);
		Singleton<Popups>.instance.ShowMe();
	}

	public void hideMe()
	{
		Holder.SetActive(value: false);
	}

	public void YesButton()
	{
		if (CONTROLLER.PlayModeSelected == 0)
		{
			ObscuredPrefs.SetInt("QPPaid", 0);
		}
		else if (CONTROLLER.PlayModeSelected == 1)
		{
			ObscuredPrefs.SetInt("T20Paid", 0);
		}
		else if (CONTROLLER.PlayModeSelected == 2)
		{
			ObscuredPrefs.SetInt("NPLPaid", 0);
		}
		else if (CONTROLLER.PlayModeSelected == 3)
		{
			ObscuredPrefs.SetInt("WCPaid", 0);
		}
		Singleton<GameModeTWO>.instance.showMe();
		hideMe();
	}

	public void NoButton()
	{
		Singleton<Popups>.instance.HideMe();
		Singleton<SquadPageTWO>.instance.showMe(CONTROLLER.myTeamIndex);
	}
}
