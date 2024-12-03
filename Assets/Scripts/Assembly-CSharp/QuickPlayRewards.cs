using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class QuickPlayRewards : Singleton<QuickPlayRewards>
{
	public Text minutes;

	public Text minutesTwo;

	public Text seconds;

	public Text secondsTwo;

	public GameObject holder;

	public GameObject doubleRewards;

	public void FreePlayStarted()
	{
		holder.SetActive(value: true);
		CONTROLLER.IsQuickPlayFree = true;
	}

	public void FreePlayEnded()
	{
		ObscuredPrefs.DeleteKey("freeEntry");
		holder.SetActive(value: false);
		CONTROLLER.IsQuickPlayFree = false;
	}

	public void DoubleRewardsStarted()
	{
		doubleRewards.SetActive(value: true);
		CONTROLLER.QPDoubleRewards = true;
	}

	public void DoubleRewardsEnded()
	{
		ObscuredPrefs.DeleteKey("doubleRewards");
		doubleRewards.SetActive(value: false);
		CONTROLLER.QPDoubleRewards = false;
	}
}
