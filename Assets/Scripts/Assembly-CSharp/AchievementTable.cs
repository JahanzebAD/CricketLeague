using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class AchievementTable
{
	public static Dictionary<int, string> AchievementDetail;

	public static Dictionary<int, string> AchievementName;

	public static Dictionary<int, string> AchievementGoal;

	public static bool achie15;

	public static void InitAchievementValues()
	{
		AchievementDetail = new Dictionary<int, string>();
		AchievementName = new Dictionary<int, string>();
		AchievementGoal = new Dictionary<int, string>();
		AchievementDetail.Add(1, "One more wicket for Bowler's Delight!");
		AchievementName.Add(1, "Bowler's Delight");
		AchievementGoal.Add(1, "Win a match by taking all the wickets");
		AchievementDetail.Add(2, "Don't lose your wicket! Gain The Run Feast.");
		AchievementName.Add(2, "Run Feast");
		AchievementGoal.Add(2, "Win a match by not losing a wicket");
		AchievementDetail.Add(3, "One more four for Master Class.");
		AchievementName.Add(3, "Master Class");
		AchievementGoal.Add(3, "Continuous 3 fours in a match");
		AchievementDetail.Add(4, "One more six for Extreme Slogger.");
		AchievementName.Add(4, "Extreme Slogger");
		AchievementGoal.Add(4, "Continuous 3 sixes in a match");
		AchievementDetail.Add(5, "One more consecutive wicket for Bowling Wizard.");
		AchievementName.Add(5, "Bowling Wizard");
		AchievementGoal.Add(5, "Continuous 3 wickets in a match");
		AchievementDetail.Add(6, "5 wickets in a row! One more for Batsman's Nightmare.");
		AchievementName.Add(6, "Batsman's Nightmare");
		AchievementGoal.Add(6, "Continuous 6 wickets in a match");
		AchievementDetail.Add(7, "5 fours in a row! One more for Batting Maestro.");
		AchievementName.Add(7, "Batting Maestro");
		AchievementGoal.Add(7, "Continuous 6 fours in a match");
		AchievementDetail.Add(8, "5 sixes in a row! One more for Merciless Fury.");
		AchievementName.Add(8, "Merciless Fury");
		AchievementGoal.Add(8, "Continuous 6 sixes in a match");
		AchievementDetail.Add(9, "5 dot balls in a row! Take a wicket now and get Rhythm and Glory.");
		AchievementName.Add(9, "Rhythm And Glory");
		AchievementGoal.Add(9, "Take a wicket in a maiden over");
		AchievementDetail.Add(10, "5 dot balls in a row! Keep it up and Choke the Batsman.");
		AchievementName.Add(10, "Choke The Batsman");
		AchievementGoal.Add(10, "Take a maiden over without any wickets");
		AchievementDetail.Add(11, "Nice run rate! Can you raise it for Slog Fest?");
		AchievementName.Add(11, "Slog Fest");
		AchievementGoal.Add(11, "Team run rate should be 15 run or above per over");
		AchievementDetail.Add(12, "Great strike rate. Can you raise it for Pinch Hitter?");
		AchievementName.Add(12, "Pinch Hitter");
		AchievementGoal.Add(12, "Individual batsman should have a strike rate 200% and above ");
		AchievementDetail.Add(13, "Nervous nineties? Be a Centurion!");
		AchievementName.Add(13, "Centurion");
		AchievementGoal.Add(13, "Take centuries");
		AchievementDetail.Add(14, "Make it a half century and Milk the Bowling!");
		AchievementName.Add(14, "Milk The Bowling");
		AchievementGoal.Add(14, "Take half centuries");
		AchievementDetail.Add(15, "N/A");
		AchievementName.Add(15, "Excelsior!");
		AchievementGoal.Add(15, "Win matches");
	}

	public static void SetAchievementValues(int index)
	{
		if (index == 15)
		{
			achie15 = true;
		}
		else
		{
			achie15 = false;
		}
		string text = AchievementName[index];
		string key = text + " Coins";
		string key2 = text + " Level";
		string key3 = text + " Limit";
		string key4 = text + " ClaimedAt";
		int num = (ObscuredPrefs.HasKey(text) ? ObscuredPrefs.GetInt(text) : 0);
		int num2 = ((!ObscuredPrefs.HasKey(key3)) ? 25 : ObscuredPrefs.GetInt(key3));
		int value = ((!ObscuredPrefs.HasKey(key2)) ? 1 : ObscuredPrefs.GetInt(key2));
		if (num + 1 <= num2)
		{
			int num3 = num + 1;
			int num4 = ((!ObscuredPrefs.HasKey(key4)) ? num3 : (num3 - ObscuredPrefs.GetInt(key4)));
			int num5 = Random.Range(40, 61) * num4;
			int num6 = Random.Range(2, 4) * num4;
			if (num3 == num2)
			{
				num5 += num2 * Random.Range(40, 61);
				num6 += 10 * (num2 / 25) * Random.Range(2, 4);
				ObscuredPrefs.SetInt(text + " ReachedMax", 1);
			}
			ObscuredPrefs.SetInt(text, num3);
			ObscuredPrefs.SetInt(key, num5);
			ObscuredPrefs.SetInt(key3, num2);
			ObscuredPrefs.SetInt(key2, value);
		}
	}

	public static void GoNextLevel(int index)
	{
		string text = AchievementName[index];
		string key = text + " Limit";
		string key2 = text + " Level";
		int num = ((!ObscuredPrefs.HasKey(key)) ? 25 : ObscuredPrefs.GetInt(key));
		int num2 = ((!ObscuredPrefs.HasKey(key2)) ? 1 : ObscuredPrefs.GetInt(key2));
		int value = 0;
		num *= 2;
		num2++;
		ObscuredPrefs.DeleteKey(text + " ReachedMax");
		ObscuredPrefs.SetInt(text, value);
		ObscuredPrefs.SetInt(key, num);
		ObscuredPrefs.SetInt(key2, num2);
		ResetAmount(index);
	}

	public static void ResetAmount(int index)
	{
		string text = AchievementName[index];
		string key = text + " Coins";
		string key2 = text + " ClaimedAt";
		ObscuredPrefs.SetInt(key, 0);
		ObscuredPrefs.SetInt(key2, ObscuredPrefs.GetInt(text));
		Singleton<AchievementsSyncronizer>.instance.UpdateClaimAt(index);
	}
}
