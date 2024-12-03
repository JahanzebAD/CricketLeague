using System;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class QuickPlayFreeEntry : MonoBehaviour
{
	public bool loggedIn;

	public bool canCheckFreeEntry;

	public bool firstTime;

	public static QuickPlayFreeEntry Instance;

	[HideInInspector]
	public long timeOffset;

	private bool PowerUBSet;

	private bool ControlUBSet;

	private bool AgilityUBSet;

	private bool powerTimerEnded;

	private bool controlTimerEnded;

	private bool agilityTimerEnded;

	private DateTime freeEntryUB;

	private DateTime doubleRewardsUB;

	private DateTime sevendayRewardsUB;

	private DateTime dailyRewardUB;

	private DateTime powerUpgradeUB;

	private DateTime controlUpgradeUB;

	private DateTime agilityUpgradeUB;

	private DateTime freeSpinUB;

	private string _sessionVal = string.Empty;

	private string[] UBPrefs = new string[3] { "ubPowerUpgrade", "ubControlUpgrade", "ubAgilityUpgrade" };

	private string[] UpgradePrefs = new string[3] { "PowerUpgradeTimer", "ControlUpgradeTimer", "AgilityUpgradeTimer" };

	public TimeSpan powerUBTime;

	public TimeSpan ControlUBTime;

	public TimeSpan AgilityUBTime;

	private DateTime RT_DateTime;

	private string RT_Prefs = "RetentionPrefs";

	private string RT_PrefsTimer = "RetentionPrefsTimer";

	public TimeSpan RT_StartTime;

	public Text guiLabel;

	private DateTime RTE_DateTime;

	private string RTE_Prefs = "RetentionPrefsEnd";

	private string RTE_PrefsTimer = "RetentionPrefsTimerEnd";

	public TimeSpan RT_EndTime;

	private bool calledOnceA;

	private bool calledOnceB;

	private bool PUCalledOnce;

	private bool CUCalledOnce;

	private bool AUCalledOnce;

	private bool spinCalledOnce;

	public bool showSpinPopup;

	private void Awake()
	{
		if (Instance == null)
		{
			canCheckFreeEntry = false;
			Instance = this;
			CustomStart();
			StartUpgradeTimer(0);
			StartSpinTimer();
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		StartRT_Timer();
	}

	public void StartRT_Timer()
	{
		if (!ObscuredPrefs.HasKey(RT_Prefs))
		{
			SessionStart();
			RT_DateTime = Now();
			WriteTimestamp(RT_Prefs, RT_DateTime);
			ObscuredPrefs.SetInt(RT_Prefs, 1);
		}
		else
		{
			SessionStart();
			RT_DateTime = ReadTimestamp(RT_Prefs, Now().AddSeconds(0.0));
		}
		RT_EndTime = Now() - RT_DateTime;
		if (!(RT_EndTime.TotalDays < 1.0))
		{
			if (RT_EndTime.TotalDays >= 1.0 && RT_EndTime.TotalDays < 2.0)
			{
				//FirebaseAnalyticsManager.instance.logEvent("Retention_Day1", "Retention", CONTROLLER.userID);
			}
			else if (RT_EndTime.TotalDays >= 2.0 && RT_EndTime.TotalDays < 5.0)
			{
				//FirebaseAnalyticsManager.instance.logEvent("Retention_Day4", "Retention", CONTROLLER.userID);
			}
			else if (RT_EndTime.TotalDays >= 5.0 && RT_EndTime.TotalDays < 8.0)
			{
				//FirebaseAnalyticsManager.instance.logEvent("Retention_Day7", "Retention", CONTROLLER.userID);
			}
			else if (RT_EndTime.TotalDays >= 8.0 && RT_EndTime.TotalDays < 15.0)
			{
				//FirebaseAnalyticsManager.instance.logEvent("Retention_Day14", "Retention", CONTROLLER.userID);
			}
			else if (RT_EndTime.TotalDays >= 15.0 && RT_EndTime.TotalDays < 31.0)
			{
				//FirebaseAnalyticsManager.instance.logEvent("Retention_Day30", "Retention", CONTROLLER.userID);
			}
			else if (RT_EndTime.TotalDays >= 1.0)
			{
				RT_DateTime = Now();
			}
		}
	}

	public void CustomStart()
	{
		if (ManageScene.activeSceneName() == "MainMenu" && ObscuredPrefs.HasKey("doubleRewards"))
		{
			if (!ObscuredPrefs.HasKey("ubDoubleRewards"))
			{
				int num = SetSessionTime();
				SessionStart();
				doubleRewardsUB = Now().AddSeconds(num);
				WriteTimestamp("ubDoubleRewards", doubleRewardsUB);
			}
			else
			{
				SessionStart();
				doubleRewardsUB = ReadTimestamp("ubDoubleRewards", Now().AddSeconds(0.0));
			}
		}
	}

	public void Set_UpgradeKeys()
	{
		if (CONTROLLER.powerUpgradeTimerStarted && !powerTimerEnded)
		{
			ObscuredPrefs.SetInt(UpgradePrefs[0], 1);
			StartUpgradeTimer(1);
		}
		if (CONTROLLER.controlUpgradeTimerStarted && !controlTimerEnded)
		{
			ObscuredPrefs.SetInt(UpgradePrefs[1], 1);
			StartUpgradeTimer(2);
		}
		if (CONTROLLER.agilityUpgradeTimerStarted && !agilityTimerEnded)
		{
			ObscuredPrefs.SetInt(UpgradePrefs[2], 1);
			StartUpgradeTimer(3);
		}
	}

	public void StartSpinTimer()
	{
		if (ObscuredPrefs.HasKey("freeSpinTimer"))
		{
			if (!ObscuredPrefs.HasKey("ubFreeSpin"))
			{
				int num = SetSpinTime();
				showSpinPopup = false;
				SessionStart();
				freeSpinUB = Now().AddSeconds(num);
				WriteTimestamp("ubFreeSpin", freeSpinUB);
			}
			else
			{
				SessionStart();
				freeSpinUB = ReadTimestamp("ubFreeSpin", Now().AddSeconds(0.0));
			}
		}
		else
		{
			showSpinPopup = true;
		}
	}

	public void StartUpgradeTimer(int index)
	{
		if ((index == 0 || index == 1) && ObscuredPrefs.HasKey(UpgradePrefs[0]))
		{
			if (!ObscuredPrefs.HasKey(UBPrefs[0]))
			{
				int num = Singleton<PaymentProcess>.instance.GenerateTimerValue(0);
				SessionStart();
				powerUpgradeUB = Now().AddSeconds(num);
				WriteTimestamp(UBPrefs[0], powerUpgradeUB);
			}
			else
			{
				SessionStart();
				powerUpgradeUB = ReadTimestamp(UBPrefs[0], Now().AddSeconds(0.0));
			}
			PowerUBSet = true;
		}
		if ((index == 0 || index == 2) && ObscuredPrefs.HasKey(UpgradePrefs[1]))
		{
			if (!ObscuredPrefs.HasKey(UBPrefs[1]))
			{
				int num2 = Singleton<PaymentProcess>.instance.GenerateTimerValue(1);
				SessionStart();
				controlUpgradeUB = Now().AddSeconds(num2);
				WriteTimestamp(UBPrefs[1], controlUpgradeUB);
			}
			else
			{
				SessionStart();
				controlUpgradeUB = ReadTimestamp(UBPrefs[1], Now().AddSeconds(0.0));
			}
			ControlUBSet = true;
		}
		if ((index == 0 || index == 3) && ObscuredPrefs.HasKey(UpgradePrefs[2]))
		{
			if (!ObscuredPrefs.HasKey(UBPrefs[2]))
			{
				int num3 = Singleton<PaymentProcess>.instance.GenerateTimerValue(2);
				SessionStart();
				agilityUpgradeUB = Now().AddSeconds(num3);
				WriteTimestamp(UBPrefs[2], agilityUpgradeUB);
			}
			else
			{
				SessionStart();
				agilityUpgradeUB = ReadTimestamp(UBPrefs[2], Now().AddSeconds(0.0));
			}
			AgilityUBSet = true;
		}
	}

	public void StartDayRewards()
	{
		if (Singleton<SevenDaysRewards>.instance.GetRemaingingDays() > 0 && Singleton<SevenDaysRewards>.instance.enable)
		{
			if (!loggedIn)
			{
				return;
			}
			if (Singleton<SevenDaysRewards>.instance.GetRemaingingDays() == 7)
			{
				Singleton<SevenDaysRewards>.instance.showMe = true;
				Singleton<SevenDaysRewards>.instance.ShowMe();
				return;
			}
			sevendayRewardsUB = ReadTimestamp("SevenDayRewards", Now().AddSeconds(0.0));
			if (firstTime)
			{
				firstTime = false;
				SetSevenDayTimer();
				Singleton<SevenDaysRewards>.instance.claimed = true;
			}
			else if ((sevendayRewardsUB - Now()).TotalSeconds < 0.0)
			{
				Singleton<SevenDaysRewards>.instance.showMe = true;
				Singleton<SevenDaysRewards>.instance.claimed = false;
			}
			else
			{
				SessionStart();
				Singleton<SevenDaysRewards>.instance.claimed = true;
			}
		}
		else
		{
			dailyRewardUB = ReadTimestamp("DailyRewards", Now().AddSeconds(0.0));
			if (firstTime)
			{
				firstTime = false;
				SetDailyRewardsTimer();
				Singleton<CheckinRewardManager>.instance.claimed = true;
			}
			else if ((dailyRewardUB - Now()).TotalSeconds < 0.0)
			{
				Singleton<CheckinRewardManager>.instance.showMe = true;
				Singleton<CheckinRewardManager>.instance.claimed = false;
			}
			else
			{
				SessionStart();
				Singleton<CheckinRewardManager>.instance.claimed = true;
			}
		}
	}

	public DateTime GetDailyRewardUB()
	{
		return ReadTimestamp("DailyRewards", Now().AddSeconds(0.0));
	}

	public static float Tosingle(double value)
	{
		return (float)value;
	}

	public void ReduceTime(int index)
	{
		switch (index)
		{
		case 1:
			if (CONTROLLER.powerGrade == 0)
			{
				powerUpgradeUB = powerUpgradeUB.AddSeconds(-600.0);
				WriteTimestamp(UBPrefs[0], powerUpgradeUB);
			}
			else
			{
				float num3 = Singleton<PaymentProcess>.instance.GenerateTimerValue(0) * 10 / 100;
				powerUpgradeUB = powerUpgradeUB.AddSeconds(0f - num3);
				WriteTimestamp(UBPrefs[0], powerUpgradeUB);
			}
			break;
		case 2:
			if (CONTROLLER.controlGrade == 0)
			{
				controlUpgradeUB = controlUpgradeUB.AddSeconds(-600.0);
				WriteTimestamp(UBPrefs[1], controlUpgradeUB);
			}
			else
			{
				float num2 = Singleton<PaymentProcess>.instance.GenerateTimerValue(1) * 10 / 100;
				controlUpgradeUB = controlUpgradeUB.AddSeconds(0f - num2);
				WriteTimestamp(UBPrefs[1], controlUpgradeUB);
			}
			break;
		case 3:
			if (CONTROLLER.agilityGrade == 0)
			{
				agilityUpgradeUB = agilityUpgradeUB.AddSeconds(-600.0);
				WriteTimestamp(UBPrefs[2], agilityUpgradeUB);
			}
			else
			{
				float num = Singleton<PaymentProcess>.instance.GenerateTimerValue(2) * 10 / 100;
				agilityUpgradeUB = agilityUpgradeUB.AddSeconds(0f - num);
				WriteTimestamp(UBPrefs[2], agilityUpgradeUB);
			}
			break;
		}
	}

	public void FinishTime(int index)
	{
		switch (index)
		{
		case 1:
			powerUpgradeUB = Now();
			WriteTimestamp(UBPrefs[0], powerUpgradeUB);
			break;
		case 2:
			controlUpgradeUB = Now();
			WriteTimestamp(UBPrefs[1], controlUpgradeUB);
			break;
		case 3:
			agilityUpgradeUB = Now();
			WriteTimestamp(UBPrefs[2], agilityUpgradeUB);
			break;
		}
		Singleton<PowerUps>.instance.timerStarted[index - 1] = false;
	}

	public void SetSevenDayTimer()
	{
		int num = SetOneDaySessionTime();
		SessionStart();
		sevendayRewardsUB = Now().AddSeconds(num);
		WriteTimestamp("SevenDayRewards", sevendayRewardsUB);
		Singleton<SevenDaysRewards>.instance.claimed = true;
	}

	public void SetDailyRewardsTimer()
	{
		SessionStart();
		dailyRewardUB = Now().AddSeconds(SetOneDaySessionTime());
		WriteTimestamp("DailyRewards", dailyRewardUB);
		Singleton<DailyRewards>.instance.claimed = true;
	}

	private void OnDisable()
	{
		SessionEnd();
		if (CONTROLLER.IsQuickPlayFree)
		{
			WriteTimestamp("ubFreeEntry", freeEntryUB);
		}
		if (CONTROLLER.powerUpgradeTimerStarted)
		{
			WriteTimestamp(UBPrefs[0], powerUpgradeUB);
		}
		if (CONTROLLER.controlUpgradeTimerStarted)
		{
			WriteTimestamp(UBPrefs[1], controlUpgradeUB);
		}
		if (CONTROLLER.agilityUpgradeTimerStarted)
		{
			WriteTimestamp(UBPrefs[2], agilityUpgradeUB);
		}
		if (ObscuredPrefs.HasKey("ubDoubleRewards"))
		{
			WriteTimestamp("ubDoubleRewards", doubleRewardsUB);
		}
		if (ObscuredPrefs.HasKey("SevenDayRewards"))
		{
			WriteTimestamp("SevenDayRewards", sevendayRewardsUB);
		}
		if (ObscuredPrefs.HasKey("DailyRewards"))
		{
			WriteTimestamp("DailyRewards", dailyRewardUB);
		}
		if (ObscuredPrefs.HasKey(RT_Prefs))
		{
			WriteTimestamp(RT_Prefs, RT_DateTime);
		}
		if (ObscuredPrefs.HasKey(RTE_Prefs))
		{
			WriteTimestamp(RTE_Prefs, RTE_DateTime);
		}
	}

	private int SetSessionTimeQP()
	{
		return 60;
	}

	private int SetSpinTime()
	{
		return 86400;
	}

	private int SetSessionTime()
	{
		return 3600;
	}

	private int SetOneDaySessionTime()
	{
		return 86400;
	}

	private void OnGUI()
	{
		if (ManageScene.activeSceneName() == "MainMenu")
		{
			TimeSpan timeSpan;
			if (ObscuredPrefs.HasKey("freeEntry"))
			{
				timeSpan = freeEntryUB - Now();
				if (timeSpan.TotalSeconds > 0.0)
				{
					calledOnceA = false;
					string text = "00";
					Singleton<QuickPlayRewards>.instance.FreePlayStarted();
					string text2 = ((timeSpan.Seconds <= 9) ? ("0" + timeSpan.Seconds) : timeSpan.Seconds.ToString());
					string text3 = ((timeSpan.Minutes <= 9) ? ("0" + timeSpan.Minutes) : timeSpan.Minutes.ToString());
					Singleton<QuickPlayRewards>.instance.minutes.text = text + ":" + text3 + ":" + text2;
				}
				else
				{
					_sessionVal = string.Empty;
					if (!calledOnceA)
					{
						Singleton<QuickPlayRewards>.instance.FreePlayEnded();
						ObscuredPrefs.DeleteKey("ubFreeEntry");
						calledOnceA = true;
					}
				}
			}
			if (Singleton<CheckinRewardManager>.instance.claimed && !((dailyRewardUB - Now()).TotalSeconds > 0.0))
			{
				_sessionVal = string.Empty;
				Singleton<CheckinRewardManager>.instance.showMe = true;
				Singleton<CheckinRewardManager>.instance.claimed = false;
			}
			timeSpan = doubleRewardsUB - Now();
			if (timeSpan.TotalSeconds > 0.0 && ObscuredPrefs.HasKey("doubleRewards"))
			{
				Singleton<QuickPlayRewards>.instance.DoubleRewardsStarted();
				if (timeSpan.Seconds > 9)
				{
					Singleton<QuickPlayRewards>.instance.secondsTwo.text = timeSpan.Seconds.ToString();
				}
				else
				{
					Singleton<QuickPlayRewards>.instance.secondsTwo.text = "0" + timeSpan.Seconds;
				}
				if (timeSpan.Minutes > 9)
				{
					Singleton<QuickPlayRewards>.instance.minutesTwo.text = timeSpan.Minutes + ":";
				}
				else
				{
					Singleton<QuickPlayRewards>.instance.minutesTwo.text = "0" + timeSpan.Minutes + ":";
				}
			}
			else
			{
				_sessionVal = string.Empty;
				Singleton<QuickPlayRewards>.instance.DoubleRewardsEnded();
				ObscuredPrefs.DeleteKey("ubDoubleRewards");
				calledOnceB = true;
			}
			if (loggedIn && Singleton<SevenDaysRewards>.instance.GetRemaingingDays() > 0 && Singleton<SevenDaysRewards>.instance.claimed && !((sevendayRewardsUB - Now()).TotalSeconds > 0.0))
			{
				_sessionVal = string.Empty;
				Singleton<SevenDaysRewards>.instance.showMe = true;
				Singleton<SevenDaysRewards>.instance.claimed = false;
			}
		}
		if (ObscuredPrefs.HasKey(UpgradePrefs[0]) && PowerUBSet)
		{
			TimeSpan timeSpan = (powerUBTime = powerUpgradeUB - Now());
			if (timeSpan.TotalSeconds > 0.0)
			{
				PUCalledOnce = false;
				Singleton<PowerUps>.instance.TimerStarted(1);
				string text4 = ((timeSpan.Seconds <= 9) ? ("0" + timeSpan.Seconds) : timeSpan.Seconds.ToString());
				string text5 = ((timeSpan.Minutes <= 9) ? ("0" + timeSpan.Minutes) : timeSpan.Minutes.ToString());
				string text6 = ((timeSpan.Hours <= 9) ? ("0" + timeSpan.Hours) : timeSpan.Hours.ToString());
				string text7 = ((timeSpan.Days <= 9) ? ("0" + timeSpan.Days) : timeSpan.Days.ToString());
				if (timeSpan.Days > 1)
				{
					Singleton<PowerUps>.instance.timerText[0].text = text7 + " " + LocalizationData.instance.getText(549);
					if (ManageScene.activeSceneName() == "MainMenu")
					{
						Singleton<GameModeTWO>.instance.timerText[0].text = text7 + " " + LocalizationData.instance.getText(549);
					}
				}
				else
				{
					Singleton<PowerUps>.instance.timerText[0].text = text6 + ":" + text5 + ":" + text4;
					if (ManageScene.activeSceneName() == "MainMenu")
					{
						Singleton<GameModeTWO>.instance.timerText[0].text = text6 + ":" + text5 + ":" + text4;
					}
				}
			}
			else
			{
				_sessionVal = string.Empty;
				powerUBTime = new TimeSpan(0, 0, 0, 0);
				if (!PUCalledOnce)
				{
					powerTimerEnded = true;
					Singleton<PowerUps>.instance.TimerEnded(1);
					ObscuredPrefs.DeleteKey(UBPrefs[0]);
					PUCalledOnce = true;
				}
			}
		}
		if (ObscuredPrefs.HasKey(UpgradePrefs[1]) && ControlUBSet)
		{
			TimeSpan timeSpan = (ControlUBTime = controlUpgradeUB - Now());
			if (timeSpan.TotalSeconds > 0.0)
			{
				CUCalledOnce = false;
				Singleton<PowerUps>.instance.TimerStarted(2);
				string text8 = ((timeSpan.Seconds <= 9) ? ("0" + timeSpan.Seconds) : timeSpan.Seconds.ToString());
				string text9 = ((timeSpan.Minutes <= 9) ? ("0" + timeSpan.Minutes) : timeSpan.Minutes.ToString());
				string text10 = ((timeSpan.Hours <= 9) ? ("0" + timeSpan.Hours) : timeSpan.Hours.ToString());
				string text11 = ((timeSpan.Days <= 9) ? ("0" + timeSpan.Days) : timeSpan.Days.ToString());
				if (timeSpan.Days > 1)
				{
					Singleton<PowerUps>.instance.timerText[1].text = text11 + " " + LocalizationData.instance.getText(549);
					if (ManageScene.activeSceneName() == "MainMenu")
					{
						Singleton<GameModeTWO>.instance.timerText[1].text = text11 + " " + LocalizationData.instance.getText(549);
					}
				}
				else
				{
					Singleton<PowerUps>.instance.timerText[1].text = text10 + ":" + text9 + ":" + text8;
					if (ManageScene.activeSceneName() == "MainMenu")
					{
						Singleton<GameModeTWO>.instance.timerText[1].text = text10 + ":" + text9 + ":" + text8;
					}
				}
			}
			else
			{
				_sessionVal = string.Empty;
				ControlUBTime = new TimeSpan(0, 0, 0, 0);
				if (!CUCalledOnce)
				{
					controlTimerEnded = true;
					Singleton<PowerUps>.instance.TimerEnded(2);
					ObscuredPrefs.DeleteKey(UBPrefs[1]);
					CUCalledOnce = true;
				}
			}
		}
		if (ObscuredPrefs.HasKey(UpgradePrefs[2]) && AgilityUBSet)
		{
			TimeSpan timeSpan = (AgilityUBTime = agilityUpgradeUB - Now());
			if (timeSpan.TotalSeconds > 0.0)
			{
				AUCalledOnce = false;
				Singleton<PowerUps>.instance.TimerStarted(3);
				string text12 = ((timeSpan.Seconds <= 9) ? ("0" + timeSpan.Seconds) : timeSpan.Seconds.ToString());
				string text13 = ((timeSpan.Minutes <= 9) ? ("0" + timeSpan.Minutes) : timeSpan.Minutes.ToString());
				string text14 = ((timeSpan.Hours <= 9) ? ("0" + timeSpan.Hours) : timeSpan.Hours.ToString());
				string text15 = ((timeSpan.Days <= 9) ? ("0" + timeSpan.Days) : timeSpan.Days.ToString());
				if (timeSpan.Days > 1)
				{
					Singleton<PowerUps>.instance.timerText[2].text = text15 + " " + LocalizationData.instance.getText(549);
					if (ManageScene.activeSceneName() == "MainMenu")
					{
						Singleton<GameModeTWO>.instance.timerText[2].text = text15 + " " + LocalizationData.instance.getText(549);
					}
				}
				else
				{
					Singleton<PowerUps>.instance.timerText[2].text = text14 + ":" + text13 + ":" + text12;
					if (ManageScene.activeSceneName() == "MainMenu")
					{
						Singleton<GameModeTWO>.instance.timerText[2].text = text14 + ":" + text13 + ":" + text12;
					}
				}
			}
			else
			{
				_sessionVal = string.Empty;
				AgilityUBTime = new TimeSpan(0, 0, 0, 0);
				if (!AUCalledOnce)
				{
					agilityTimerEnded = true;
					Singleton<PowerUps>.instance.TimerEnded(3);
					ObscuredPrefs.DeleteKey(UBPrefs[2]);
					AUCalledOnce = true;
				}
			}
		}
		if (!ObscuredPrefs.HasKey("freeSpinTimer"))
		{
			return;
		}
		if ((freeSpinUB - Now()).TotalSeconds > 0.0)
		{
			spinCalledOnce = false;
			return;
		}
		_sessionVal = string.Empty;
		if (!spinCalledOnce)
		{
			ObscuredPrefs.DeleteKey("ubFreeSpin");
			ObscuredPrefs.DeleteKey("freeSpinTimer");
			spinCalledOnce = true;
			if (ManageScene.activeSceneName() == "MainMenu")
			{
				showSpinPopup = true;
			}
			else
			{
				showSpinPopup = true;
			}
		}
	}

	private DateTime ReadTimestamp(string key, DateTime defaultValue)
	{
		long num = Convert.ToInt64(ObscuredPrefs.GetString(key, "0"));
		if (num == 0)
		{
			if (key == "SevenDayRewards" || key == "DailyRewards")
			{
				firstTime = true;
			}
			else
			{
				firstTime = false;
			}
			return defaultValue;
		}
		return DateTime.FromBinary(num);
	}

	private void WriteTimestamp(string key, DateTime time)
	{
		ObscuredPrefs.SetString(key, time.ToBinary().ToString());
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			SessionEnd();
			if (CONTROLLER.IsQuickPlayFree)
			{
				WriteTimestamp("ubFreeEntry", freeEntryUB);
			}
			if (CONTROLLER.powerUpgradeTimerStarted)
			{
				WriteTimestamp(UBPrefs[0], powerUpgradeUB);
			}
			if (CONTROLLER.controlUpgradeTimerStarted)
			{
				WriteTimestamp(UBPrefs[1], controlUpgradeUB);
			}
			if (CONTROLLER.agilityUpgradeTimerStarted)
			{
				WriteTimestamp(UBPrefs[2], agilityUpgradeUB);
			}
			if (ObscuredPrefs.HasKey("ubDoubleRewards"))
			{
				WriteTimestamp("ubDoubleRewards", doubleRewardsUB);
			}
			if (ObscuredPrefs.HasKey("SevenDayRewards"))
			{
				WriteTimestamp("SevenDayRewards", sevendayRewardsUB);
			}
			if (ObscuredPrefs.HasKey("DailyRewards"))
			{
				WriteTimestamp("DailyRewards", dailyRewardUB);
			}
			if (ObscuredPrefs.HasKey("freeSpinTimer"))
			{
				WriteTimestamp("ubFreeSpin", freeSpinUB);
			}
			if (ObscuredPrefs.HasKey(RT_Prefs))
			{
				WriteTimestamp(RT_Prefs, RT_DateTime);
			}
			if (ObscuredPrefs.HasKey(RTE_Prefs))
			{
				WriteTimestamp(RTE_Prefs, RTE_DateTime);
			}
		}
		else
		{
			SessionStart();
			if (CONTROLLER.IsQuickPlayFree)
			{
				freeEntryUB = ReadTimestamp("ubFreeEntry", Now().AddSeconds(0.0));
			}
			if (CONTROLLER.powerUpgradeTimerStarted)
			{
				powerUpgradeUB = ReadTimestamp(UBPrefs[0], Now().AddSeconds(0.0));
			}
			if (CONTROLLER.controlUpgradeTimerStarted)
			{
				controlUpgradeUB = ReadTimestamp(UBPrefs[1], Now().AddSeconds(0.0));
			}
			if (CONTROLLER.agilityUpgradeTimerStarted)
			{
				agilityUpgradeUB = ReadTimestamp(UBPrefs[2], Now().AddSeconds(0.0));
			}
			if (ObscuredPrefs.HasKey("ubDoubleRewards"))
			{
				doubleRewardsUB = ReadTimestamp("ubDoubleRewards", Now().AddSeconds(0.0));
			}
			if (ObscuredPrefs.HasKey("SevenDayRewards"))
			{
				sevendayRewardsUB = ReadTimestamp("SevenDayRewards", Now().AddSeconds(0.0));
			}
			if (ObscuredPrefs.HasKey("DailyRewards"))
			{
				dailyRewardUB = ReadTimestamp("DailyRewards", Now().AddSeconds(0.0));
			}
			if (ObscuredPrefs.HasKey("freeSpinTimer"))
			{
				freeSpinUB = ReadTimestamp("ubFreeSpin", Now().AddSeconds(0.0));
			}
			if (ObscuredPrefs.HasKey(RT_Prefs))
			{
				RT_DateTime = ReadTimestamp(RT_Prefs, Now());
			}
			if (ObscuredPrefs.HasKey(RTE_Prefs))
			{
				RTE_DateTime = ReadTimestamp(RTE_Prefs, Now());
			}
		}
	}

	private void OnApplicationFocus(bool focus)
	{
		if (!focus)
		{
			SessionEnd();
			if (CONTROLLER.IsQuickPlayFree)
			{
				WriteTimestamp("ubFreeEntry", freeEntryUB);
			}
			if (CONTROLLER.powerUpgradeTimerStarted)
			{
				WriteTimestamp(UBPrefs[0], powerUpgradeUB);
			}
			if (CONTROLLER.controlUpgradeTimerStarted)
			{
				WriteTimestamp(UBPrefs[1], controlUpgradeUB);
			}
			if (CONTROLLER.agilityUpgradeTimerStarted)
			{
				WriteTimestamp(UBPrefs[2], agilityUpgradeUB);
			}
			if (ObscuredPrefs.HasKey("ubDoubleRewards"))
			{
				WriteTimestamp("ubDoubleRewards", doubleRewardsUB);
			}
			if (ObscuredPrefs.HasKey("SevenDayRewards"))
			{
				WriteTimestamp("SevenDayRewards", sevendayRewardsUB);
			}
			if (ObscuredPrefs.HasKey("DailyRewards"))
			{
				WriteTimestamp("DailyRewards", dailyRewardUB);
			}
			if (ObscuredPrefs.HasKey("freeSpinTimer"))
			{
				WriteTimestamp("ubFreeSpin", freeSpinUB);
			}
			if (ObscuredPrefs.HasKey(RT_Prefs))
			{
				WriteTimestamp(RT_Prefs, RT_DateTime);
			}
			if (ObscuredPrefs.HasKey(RTE_Prefs))
			{
				WriteTimestamp(RTE_Prefs, RTE_DateTime);
			}
		}
		else
		{
			SessionStart();
			if (CONTROLLER.IsQuickPlayFree)
			{
				freeEntryUB = ReadTimestamp("ubFreeEntry", Now().AddSeconds(0.0));
			}
			if (CONTROLLER.powerUpgradeTimerStarted)
			{
				powerUpgradeUB = ReadTimestamp(UBPrefs[0], Now().AddSeconds(0.0));
			}
			if (CONTROLLER.controlUpgradeTimerStarted)
			{
				controlUpgradeUB = ReadTimestamp(UBPrefs[1], Now().AddSeconds(0.0));
			}
			if (CONTROLLER.agilityUpgradeTimerStarted)
			{
				agilityUpgradeUB = ReadTimestamp(UBPrefs[2], Now().AddSeconds(0.0));
			}
			if (ObscuredPrefs.HasKey("ubDoubleRewards"))
			{
				doubleRewardsUB = ReadTimestamp("ubDoubleRewards", Now().AddSeconds(0.0));
			}
			if (ObscuredPrefs.HasKey("SevenDayRewards"))
			{
				sevendayRewardsUB = ReadTimestamp("SevenDayRewards", Now().AddSeconds(0.0));
			}
			if (ObscuredPrefs.HasKey("DailyRewards"))
			{
				dailyRewardUB = ReadTimestamp("DailyRewards", Now().AddSeconds(0.0));
			}
			if (ObscuredPrefs.HasKey("freeSpinTimer"))
			{
				freeSpinUB = ReadTimestamp("ubFreeSpin", Now().AddSeconds(0.0));
			}
			if (ObscuredPrefs.HasKey(RT_Prefs))
			{
				RT_DateTime = ReadTimestamp(RT_Prefs, Now());
			}
			if (ObscuredPrefs.HasKey(RTE_Prefs))
			{
				RTE_DateTime = ReadTimestamp(RTE_Prefs, Now());
			}
		}
	}

	private void OnApplicationQuit()
	{
		SessionEnd();
		if (CONTROLLER.IsQuickPlayFree)
		{
			WriteTimestamp("ubFreeEntry", freeEntryUB);
		}
		if (CONTROLLER.powerUpgradeTimerStarted)
		{
			WriteTimestamp(UBPrefs[0], powerUpgradeUB);
		}
		if (CONTROLLER.controlUpgradeTimerStarted)
		{
			WriteTimestamp(UBPrefs[1], controlUpgradeUB);
		}
		if (CONTROLLER.agilityUpgradeTimerStarted)
		{
			WriteTimestamp(UBPrefs[2], agilityUpgradeUB);
		}
		if (ObscuredPrefs.HasKey("ubDoubleRewards"))
		{
			WriteTimestamp("ubDoubleRewards", doubleRewardsUB);
		}
		if (ObscuredPrefs.HasKey("SevenDayRewards"))
		{
			WriteTimestamp("SevenDayRewards", sevendayRewardsUB);
		}
		if (ObscuredPrefs.HasKey("DailyRewards"))
		{
			WriteTimestamp("DailyRewards", dailyRewardUB);
		}
		if (ObscuredPrefs.HasKey("freeSpinTimer"))
		{
			WriteTimestamp("ubFreeSpin", freeSpinUB);
		}
		if (!ObscuredPrefs.HasKey(RT_Prefs))
		{
			WriteTimestamp(RT_Prefs, RT_DateTime);
		}
		if (ObscuredPrefs.HasKey(RTE_Prefs))
		{
			WriteTimestamp(RTE_Prefs, RTE_DateTime);
		}
	}

	public DateTime Now()
	{
		return DateTime.Now.AddSeconds(-1f * (float)timeOffset);
	}

	public void UpdateTimeOffset()
	{
		UpdateTimeOffsetAndroid();
	}

	public bool IsUsingSystemTime()
	{
		return UsingSystemTimeAndroid();
	}

	private void SessionStart()
	{
		StartAndroid();
	}

	private void SessionEnd()
	{
		EndAndroid();
	}

	private void UpdateTimeOffsetAndroid()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		using AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		using AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.vasilij.unbiasedtime.UnbiasedTime");
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		if (@static != null && androidJavaClass2 != null)
		{
			timeOffset = androidJavaClass2.CallStatic<long>("vtcTimestampOffset", new object[1] { @static });
		}
	}

	private void StartAndroid()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		using AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		using AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.vasilij.unbiasedtime.UnbiasedTime");
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		if (@static != null && androidJavaClass2 != null)
		{
			androidJavaClass2.CallStatic("vtcOnSessionStart", @static);
			timeOffset = androidJavaClass2.CallStatic<long>("vtcTimestampOffset", new object[0]);
		}
	}

	private void EndAndroid()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		using AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		using AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.vasilij.unbiasedtime.UnbiasedTime");
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		if (@static != null)
		{
			androidJavaClass2?.CallStatic("vtcOnSessionEnd", @static);
		}
	}

	private bool UsingSystemTimeAndroid()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return true;
		}
		using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.vasilij.unbiasedtime.UnbiasedTime");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			if (@static != null && androidJavaClass2 != null)
			{
				return androidJavaClass2.CallStatic<bool>("vtcUsingDeviceTime", new object[0]);
			}
		}
		return true;
	}
}
