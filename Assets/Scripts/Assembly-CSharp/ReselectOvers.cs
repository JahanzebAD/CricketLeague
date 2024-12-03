using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class ReselectOvers : Singleton<ReselectOvers>
{
	public GameObject holder;

	private void Start()
	{
		holder.SetActive(value: false);
	}

	public void Reselect()
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
		holder.SetActive(value: false);
		CONTROLLER.GameStartsFromSave = false;
		CONTROLLER.isFreeHitBall = false;
		AutoSave.DeleteFile();
		Singleton<EntryFeesAndRewards>.instance.ShowMe();
	}

	public void ShowMe()
	{
		if (CONTROLLER.PlayModeSelected == 0 && CONTROLLER.IsQuickPlayFree)
		{
			Reselect();
			return;
		}
		CONTROLLER.pageName = "reselectOvers";
		holder.SetActive(value: true);
		Singleton<TeamSelectionTWO>.instance.Holder.SetActive(value: false);
	}

	public void HideMe()
	{
		CONTROLLER.pageName = "teamSelection";
		holder.SetActive(value: false);
		Singleton<TeamSelectionTWO>.instance.Holder.SetActive(value: true);
	}
}
