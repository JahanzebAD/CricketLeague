using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;

public class KitTable
{
	public static Dictionary<int, string> KitName;

	public static Dictionary<int, string> KitGoal;

	public static void InitKitValues()
	{
		KitName = new Dictionary<int, string>();
		KitGoal = new Dictionary<int, string>();
		KitName.Add(1, "Overs Played");
		KitName.Add(2, "Sixes Hit");
		KitName.Add(3, "Fours Hit");
		KitName.Add(4, "Wickets Taken");
		KitName.Add(5, "Matches Won");
	}

	public static void SetKitValues(int index)
	{
		if (CONTROLLER.PlayModeSelected != 6)
		{
			switch (index)
			{
			}
			string key = KitName[index];
			int num = (ObscuredPrefs.HasKey(key) ? ObscuredPrefs.GetInt(key) : 0);
			int value = num + 1;
			ObscuredPrefs.SetInt(key, value);
			Singleton<AchievementsSyncronizer>.instance.UpdateKit(index);
		}
	}

	public static void ResetAmount(int index)
	{
		string key = KitName[index];
		ObscuredPrefs.SetInt(key, 0);
		Singleton<AchievementsSyncronizer>.instance.ResetKit(index);
	}
}
