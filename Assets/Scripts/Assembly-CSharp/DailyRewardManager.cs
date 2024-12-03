using System;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class DailyRewardManager : Singleton<DailyRewardManager>
{
	[HideInInspector]
	public Dictionary<string, string> DailyRewardDictionary = new Dictionary<string, string>();

	private string[] secondSplit;

	private string[] rewardAmountPairs;

	public int day = 1;

	private string[] firstSplit = new string[2];

	public DateTime timeOfLastClaim;

	public bool InitializedDictionary;

	public void InitializeDailyRewardDictionary()
	{
		if (!InitializedDictionary)
		{
			DailyRewardDictionary.Add("day1", "1-1000|c");
			DailyRewardDictionary.Add("day2", "1-2000|c");
			DailyRewardDictionary.Add("day3", "1-3000|c");
			DailyRewardDictionary.Add("day4", "1-4000|c");
			DailyRewardDictionary.Add("day5", "1-5000|c");
			DailyRewardDictionary.Add("day6", "1-6000|c");
			DailyRewardDictionary.Add("day7", "1-7000|c");
			DailyRewardDictionary.Add("day8", "2-1000|c+1|s");
			DailyRewardDictionary.Add("day9", "2-2000|c+3|s");
			DailyRewardDictionary.Add("day10", "2-3000|c+5|s");
			DailyRewardDictionary.Add("day11", "2-4000|c+7|s");
			DailyRewardDictionary.Add("day12", "2-5000|c+10|s");
			DailyRewardDictionary.Add("day13", "2-1000|c+25|t");
			DailyRewardDictionary.Add("day14", "2-2000|c+50|t");
			DailyRewardDictionary.Add("day15", "2-3000|c+75|t");
			DailyRewardDictionary.Add("day16", "2-4000|c+100|t");
			DailyRewardDictionary.Add("day17", "2-5000|c+125|t");
			DailyRewardDictionary.Add("day18", "3-1000|c+25|t+1|s");
			DailyRewardDictionary.Add("day19", "3-2000|c+50|t+3|s");
			DailyRewardDictionary.Add("day20", "3-3000|c+75|t+5|s");
			DailyRewardDictionary.Add("day21", "3-4000|c+100|t+10|s");
			DailyRewardDictionary.Add("day22", "3-5000|c+125|t+15|s");
			DailyRewardDictionary.Add("day23", "3-1000|c+25|t+150|x");
			DailyRewardDictionary.Add("day24", "3-2000|c+50|t+500|x");
			DailyRewardDictionary.Add("day25", "3-3000|c+75|t+750|x");
			DailyRewardDictionary.Add("day26", "3-4000|c+100|t+1500|x");
			DailyRewardDictionary.Add("day27", "3-5000|c+125|t+2500|x");
			DailyRewardDictionary.Add("day28", "3-5000|c+1|a+1500|x");
			DailyRewardDictionary.Add("day29", "3-5000|c+1|p+1500|x");
			DailyRewardDictionary.Add("day30", "3-5000|c+1|o+1500|x");
			InitializedDictionary = true;
		}
		firstSplit = DailyRewardDictionary["day" + day].Split('-');
		secondSplit = new string[int.Parse(firstSplit[0])];
		rewardAmountPairs = new string[secondSplit.Length * 2];
	}

	private void ResetDailyRewardsDay()
	{
		ObscuredPrefs.SetInt("dailyRewardDay", 1);
		day = 1;
	}

	public int GetDailyRewardsDay()
	{
		InitializeDailyRewardDictionary();
		timeOfLastClaim = QuickPlayFreeEntry.Instance.GetDailyRewardUB();
		if (ObscuredPrefs.HasKey("dailyRewardDay") && (QuickPlayFreeEntry.Instance.Now() - timeOfLastClaim).TotalDays < 1.0)
		{
			day = ObscuredPrefs.GetInt("dailyRewardDay");
		}
		else
		{
			ResetDailyRewardsDay();
		}
		return day;
	}

	public void ClaimCompleted()
	{
		AddDailyReward();
		Singleton<GameModeTWO>.instance.ResetDetails();
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	Server_Connection.instance.SyncPoints();
		//}
		ObscuredPrefs.SetInt("NewCheckin", 1);
		if (day == 30)
		{
			ResetDailyRewardsDay();
		}
		else
		{
			day++;
			ObscuredPrefs.SetInt("dailyRewardDay", day);
		}
		QuickPlayFreeEntry.Instance.SetDailyRewardsTimer();
	}

	public void AddDailyReward()
	{
		GetDailyRewardsDay();
		InitializeDailyRewardDictionary();
		secondSplit = firstSplit[1].Split('+');
		for (int i = 0; i < secondSplit.Length; i++)
		{
			rewardAmountPairs = secondSplit[i].Split('|');
			Debug.LogError("reward char::" + rewardAmountPairs[1] + " rewardAmount::" + rewardAmountPairs[0]);
			switch (rewardAmountPairs[1])
			{
			case "c":
				SavePlayerPrefs.SaveUserCoins(int.Parse(rewardAmountPairs[0]), 0, int.Parse(rewardAmountPairs[0]));
				break;
			case "s":
				Debug.LogError("!!!!!!!!!!!!!!!!!!!!!!free spin: " + CONTROLLER.FreeSpin);
				SavePlayerPrefs.SaveSpins(int.Parse(rewardAmountPairs[0]));
				break;
			case "t":
				SavePlayerPrefs.SaveUserTickets(int.Parse(rewardAmountPairs[0]), 0, int.Parse(rewardAmountPairs[0]));
				break;
			case "x":
				SavePlayerPrefs.SaveUserXPs(int.Parse(rewardAmountPairs[0]), int.Parse(rewardAmountPairs[0]));
				break;
			case "p":
				Singleton<PowerUps>.instance.UpgradePower(int.Parse(rewardAmountPairs[0]));
				break;
			case "a":
				Singleton<PowerUps>.instance.UpgradeAgility(int.Parse(rewardAmountPairs[0]));
				break;
			case "o":
				Singleton<PowerUps>.instance.UpgradeControl(int.Parse(rewardAmountPairs[0]));
				break;
			}
		}
	}
}
