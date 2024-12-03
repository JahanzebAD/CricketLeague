using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class DailySpin : Singleton<DailySpin>
{
	public GameObject holder;

	public void ClaimFreeSpin()
	{
		ObscuredPrefs.SetInt("freeSpinTimer", 1);
		SavePlayerPrefs.SaveSpins(1);
		Server_Connection.instance.Get_User_Sync();
		QuickPlayFreeEntry.Instance.StartSpinTimer();
		holder.SetActive(value: false);
		Singleton<GameModeTWO>.instance.getSpinWheel();
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = HideMe;
		CONTROLLER.pageName = "dailySpin";
		holder.SetActive(value: true);
	}

	public void HideMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		ObscuredPrefs.SetInt("freeSpinTimer", 1);
		SavePlayerPrefs.SaveSpins(1);
		Server_Connection.instance.Get_User_Sync();
		QuickPlayFreeEntry.Instance.StartSpinTimer();
		holder.SetActive(value: false);
	}
}
