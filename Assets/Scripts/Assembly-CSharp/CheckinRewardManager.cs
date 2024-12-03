using UnityEngine;

public class CheckinRewardManager : Singleton<CheckinRewardManager>
{
	public CheckinRewardDetails[] checkinDetails;

	public GameObject holder;

	public bool showMe;

	public bool claimed;

	public void ShowMe()
	{
		holder.SetActive(value: true);
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = HideMe;
		showMe = false;
		claimed = false;
		ValidateDailyRewardDetails();
	}

	private void ValidateDailyRewardDetails()
	{
		for (int i = 0; i < checkinDetails.Length; i++)
		{
			checkinDetails[i].disableBG.SetActive(value: false);
			checkinDetails[i].todayBG.SetActive(value: false);
			for (int j = 0; j < checkinDetails[i].dayNumber.Length; j++)
			{
				checkinDetails[i].dayNumber[j].text = LocalizationData.instance.getText(466) + " " + (i + 1);
			}
			string[] array = Singleton<DailyRewardManager>.instance.DailyRewardDictionary["day" + (i + 1)].Split('-');
			array = array[1].Split('+');
			for (int k = 0; k < array.Length; k++)
			{
				string[] array2 = array[k].Split('|');
				checkinDetails[i].rewards[k].text = array2[0].ToString();
			}
		}
	}

	public void HighlightTodaysReward(int dayNumber)
	{
		if (dayNumber > 1)
		{
			for (int i = 0; i < dayNumber - 1; i++)
			{
				checkinDetails[i].disableBG.SetActive(value: true);
			}
		}
		checkinDetails[dayNumber - 1].todayBG.SetActive(value: true);
	}

	public void HideMe()
	{
		holder.SetActive(value: false);
		Singleton<DailyRewardManager>.instance.ClaimCompleted();
		claimed = true;
		showMe = false;
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
	}
}
