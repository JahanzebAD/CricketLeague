using System;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class LocalNotificationScript : MonoBehaviour
{
	public AndroidJavaObject objNative;

	private AndroidJavaObject playerActivityContext;

	private List<string> dailyCoinsNotificationTexts = new List<string>();

	public static int GeneralNotifiationLength;

	public List<string> notificationTexts = new List<string>();

	private DateTime unbiasedTimerEndTimestamp;

	private int iosNotificationCount;

	public double DailyBonusDelay;

	public string clubSuperChaseName = string.Empty;

	private string savedStr = string.Empty;

	private string[] tournamentArray;

	private string roundname;

	private int notificationid;

	public int index = -1;

	private bool ismessreceived;

	private long timeOffset;

	private double dailyCoinsDelay = 5.0;

	private double DynamicDelay = 43200000.0;

	private double Generaldelay = 79200000.0;

	public static LocalNotificationScript instance;

	private void Awake()
	{
		ismessreceived = false;
		if (PlayerPrefs.HasKey("userLastPlayed"))
		{
			CONTROLLER.lastPlayedMode = PlayerPrefs.GetInt("userLastPlayed");
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		instance = this;
		notificationTexts.Add("Rush Hour! Hurry up and start stocking your coins.");
		notificationTexts.Add("There are Achievements to unlock.Complete them for more Coins!");
		notificationTexts.Add("If you are a real champ, try winning in HARD difficulty mode.");
		notificationTexts.Add("Move up mate! It is time to rank up in the Leader boards. Play now!");
		notificationTexts.Add("Welcome back champ! Start a new match and get going!");
		notificationTexts.Add("Finding WCC Lite tough? Try upgrading your team!");
		notificationTexts.Add("Try out the ultra fast Premier league in WCC Lite");
		switch (UnityEngine.Random.Range(0, 4))
		{
		case 0:
			notificationTexts.Add("Climb the ranks in WCC Lite Multiplayer");
			break;
		case 1:
			notificationTexts.Add("Prove them all that you are way out of league in WCC Lite Multiplayer");
			break;
		case 2:
			notificationTexts.Add("Destroy your opponents in WCC Lite Multiplayer");
			break;
		case 3:
			notificationTexts.Add("No joking, 5 batsmen, winner takes all. Face the challenge in WCC Lite Multiplayer");
			break;
		}
		switch (UnityEngine.Random.Range(0, 3))
		{
		case 0:
			notificationTexts.Add("Test your mettle in the Test matches");
			break;
		case 1:
			notificationTexts.Add("Patience is a virtue, test yours in our Test matches");
			break;
		case 2:
			notificationTexts.Add("Be the lord of Cricket, decimate everyone in the Test match");
			break;
		}
		if (objNative == null)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			//playerActivityContext = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			//AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.nextwave.nativeLocalNotification.NativeNotification");
			//if (androidJavaClass2 != null)
			//{
				//objNative = androidJavaClass2.CallStatic<AndroidJavaObject>("instance", new object[0]);
				//objNative.Call("setActivity", playerActivityContext);
				//objNative.Call("setContext", playerActivityContext);
				//objNative.Call("setTheGameName", "WCC Lite");
			//}
		}
		//objNative.Call("checkAnyNewLocalNotificationClicked", "LocalNotification", "onNewLocalNotificationClicked");
		//objNative.Call("cancelNotificationFromUnity");
		//objNative.Call("setGeneralNotifiactionLength", notificationTexts.Count.ToString());
		if (PlayerPrefs.HasKey("RepeatNoti"))
		{
			PlayerPrefs.DeleteKey("RepeatNoti");
		}
	}

	public void Start()
	{
	}

	public void onNewLocalNotificationClicked(string clickedNotification)
	{
		if (notificationTexts.Contains(clickedNotification))
		{
			CONTROLLER.pushNotiClicked = true;
			index = notificationTexts.IndexOf(clickedNotification);
			if (index >= 0 && index <= 8)
			{
				if (index == 0)
				{
					CONTROLLER.PushScreenNumber = 8;
				}
				else if (index == 1)
				{
					CONTROLLER.PushScreenNumber = 12;
				}
				else if (index == 2)
				{
					Goto_LastPlayedGamemode();
				}
				else if (index == 3)
				{
					CONTROLLER.PushScreenNumber = 13;
				}
				else if (index == 4)
				{
					CONTROLLER.PushScreenNumber = 0;
				}
				else if (index == 5)
				{
					CONTROLLER.PushScreenNumber = 10;
				}
				else if (index == 6)
				{
					CONTROLLER.PushScreenNumber = 7;
				}
				else if (index == 7)
				{
					CONTROLLER.PushScreenNumber = 6;
				}
				else if (index == 8)
				{
					CONTROLLER.PushScreenNumber = 18;
				}
				else
				{
					CONTROLLER.PushScreenNumber = 0;
				}
				if (Singleton<GameModeTWO>.instance != null)
				{
					Singleton<GameModeTWO>.instance.DirectLink();
				}
			}
		}
		else
		{
			CONTROLLER.pushNotiClicked = true;
			if (notificationid == 4 || CONTROLLER.PushScreenNumber == 4)
			{
				CONTROLLER.PushScreenNumber = 4;
			}
			else if (notificationid == 5 || CONTROLLER.PushScreenNumber == 5)
			{
				CONTROLLER.PushScreenNumber = 5;
			}
			else if (notificationid == 14 || CONTROLLER.PushScreenNumber == 14)
			{
				CONTROLLER.PushScreenNumber = 14;
			}
			else if (notificationid == 15 || CONTROLLER.PushScreenNumber == 15)
			{
				CONTROLLER.PushScreenNumber = 15;
			}
			else if (notificationid == 16 || CONTROLLER.PushScreenNumber == 16)
			{
				CONTROLLER.PushScreenNumber = 16;
			}
			else if (notificationid == 17 || CONTROLLER.PushScreenNumber == 17)
			{
				CONTROLLER.PushScreenNumber = 17;
			}
			else if (notificationid == 18 || CONTROLLER.PushScreenNumber == 18)
			{
				CONTROLLER.PushScreenNumber = 18;
			}
			else if (notificationid == 0 || CONTROLLER.PushScreenNumber == 0)
			{
				CONTROLLER.PushScreenNumber = 0;
			}
			else
			{
				CONTROLLER.PushScreenNumber = 0;
			}
			if (Singleton<GameModeTWO>.instance != null)
			{
				Singleton<GameModeTWO>.instance.DirectLink();
			}
		}
	}

	public void Goto_LastPlayedGamemode()
	{
		if (CONTROLLER.lastPlayedMode == 1)
		{
			CONTROLLER.PushScreenNumber = 1;
		}
		else if (CONTROLLER.lastPlayedMode == 2)
		{
			CONTROLLER.PushScreenNumber = 2;
		}
		else if (CONTROLLER.lastPlayedMode == 3)
		{
			CONTROLLER.PushScreenNumber = 3;
		}
		else if (CONTROLLER.lastPlayedMode == 4)
		{
			CONTROLLER.PushScreenNumber = 4;
		}
		else if (CONTROLLER.lastPlayedMode == 5)
		{
			CONTROLLER.PushScreenNumber = 5;
		}
		else if (CONTROLLER.lastPlayedMode == 6)
		{
			CONTROLLER.PushScreenNumber = 6;
		}
		else
		{
			CONTROLLER.PushScreenNumber = 1;
		}
	}

	private void OnApplicationPause(bool _bool)
	{
		if (!_bool)
		{
			//objNative.Call("checkAnyNewLocalNotificationClicked", "LocalNotification", "onNewLocalNotificationClicked");
			//objNative.Call("cancelNotificationFromUnity");
			ismessreceived = false;
		}
		else
		{
			ScriptingDynamicNotification();
			ScriptingGeneralNotification();
		}
	}

	public void scriptingLocalNotificationFromExit()
	{
		if (objNative != null)
		{
			//objNative.Call("cancelNotificationFromUnity");
		}
		ScriptingDynamicNotification();
		ScriptingGeneralNotification();
	}

	private void ScheduleTestCricketSessionTimeOverNotification()
	{
	}

	private DateTime ReadTimestamp(string key, DateTime defaultValue)
	{
		long num = Convert.ToInt64(PlayerPrefs.GetString(key, "0"));
		if (num == 0)
		{
			return defaultValue;
		}
		return DateTime.FromBinary(num);
	}

	private DateTime Now()
	{
		return DateTime.Now.AddSeconds(-1f * (float)timeOffset);
	}

	private void ScheduleForDailyCoins()
	{
	}

	private void SessionStart()
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

	private void StartScheduleNotification(string notificationId, double delay, string notificationtxt, string title, int index)
	{
		if (!ismessreceived)
		{
			objNative.Call("intializeTheNotifications", notificationId, delay, notificationtxt, title);
			ismessreceived = true;
		}
	}

	public void StartScheduleNotificationForInvite(string notificationId, double delay, string notificationtxt, string title, int index)
	{
		notificationId = "455";
		objNative.Call("intializeTheNotifications", notificationId, delay, notificationtxt, title);
	}

	public void StartScheduleNotificationForStore(string notificationId, double delay, string notificationtxt, string title, int index)
	{
		notificationtxt = getHiddenNotificationText(notificationtxt);
		objNative.Call("intializeTheNotifications", notificationId, delay, notificationtxt, title);
	}

	private string getHiddenNotificationText(string txt)
	{
		string[] array = new string[5] { "Time's running out! Grab your goodies in the Store offers section!", "Clock's ticking! Rush to the store offers section before the offer ends!", "Seal the deal before your time's up! Visit the Store offers!", "Hurry up! Stock up on goodies from the Store offers section!", "Check the Store offers for amazing deals!" };
		return array[UnityEngine.Random.Range(0, array.Length)];
	}

	private string getHiddenStoreNotificationText(string txt)
	{
		return string.Empty;
	}

	private void ScriptingDynamicNotification()
	{
		switch (UnityEngine.Random.Range(0, 6))
		{
		case 0:
			Spin_Notification();
			break;
		case 1:
			SC_Notification();
			break;
		case 2:
			WCF_Notification();
			break;
		case 3:
			SO_Notification();
			break;
		case 4:
			T20_Notification();
			break;
		case 5:
			WC_Notification();
			break;
		}
	}

	public void T20_Notification()
	{
		if (ObscuredPrefs.HasKey("tour"))
		{
			savedStr = ObscuredPrefs.GetString("tour");
			string[] array = savedStr.Split("&"[0]);
			CONTROLLER.TournamentStage = int.Parse(array[0]);
			if (CONTROLLER.TournamentStage == 0)
			{
				roundname = "Knock Out Stage";
			}
			else if (CONTROLLER.TournamentStage == 1)
			{
				roundname = "QuaterFinal";
			}
			else if (CONTROLLER.TournamentStage == 2)
			{
				roundname = "SemiFinal";
			}
			else if (CONTROLLER.TournamentStage == 3)
			{
				roundname = "Final";
			}
			notificationid = 15;
			CONTROLLER.PushScreenNumber = 15;
			StartScheduleNotification("1", DynamicDelay, "You've reached the " + roundname + " of the T20 World Cup Tournament. Don't give up now!", "Tournament", notificationid);
		}
	}

	public void WC_Notification()
	{
		if (ObscuredPrefs.HasKey("worldcup"))
		{
			CONTROLLER.WCLeagueData = ObscuredPrefs.GetString("worldcup");
			string[] array = CONTROLLER.WCLeagueData.Split("|"[0]);
			CONTROLLER.WCTournamentStage = int.Parse(array[0]);
			if (CONTROLLER.WCTournamentStage == 0)
			{
				roundname = "League Stage";
				notificationid = 16;
				CONTROLLER.PushScreenNumber = 16;
			}
			else if (CONTROLLER.WCTournamentStage == 1)
			{
				roundname = "QuaterFinal";
				notificationid = 17;
				CONTROLLER.PushScreenNumber = 17;
			}
			else if (CONTROLLER.WCTournamentStage == 2)
			{
				roundname = "SemiFinal";
				notificationid = 17;
				CONTROLLER.PushScreenNumber = 17;
			}
			else if (CONTROLLER.WCTournamentStage == 3)
			{
				roundname = "Final";
				notificationid = 17;
				CONTROLLER.PushScreenNumber = 17;
			}
			StartScheduleNotification("2", DynamicDelay, "You've reached the " + roundname + " of the World Cup ODI Tournament. Don't give up now!", "Tournament", notificationid);
		}
	}

	public void SC_Notification()
	{
		if (ObscuredPrefs.HasKey("CTMainArray"))
		{
			CONTROLLER.CTCurrentPlayingMainLevel = ObscuredPrefs.GetInt("CTMainArray");
			if (CONTROLLER.CTCurrentPlayingMainLevel == 0)
			{
				clubSuperChaseName = "CUB CLASS";
			}
			else if (CONTROLLER.CTCurrentPlayingMainLevel == 1)
			{
				clubSuperChaseName = "YOUNG LION";
			}
			else if (CONTROLLER.CTCurrentPlayingMainLevel == 2)
			{
				clubSuperChaseName = "POWER PLAYER";
			}
			else if (CONTROLLER.CTCurrentPlayingMainLevel == 3)
			{
				clubSuperChaseName = "SUPER PRO";
			}
			else if (CONTROLLER.CTCurrentPlayingMainLevel == 4)
			{
				clubSuperChaseName = "CHAMPION";
			}
			else if (CONTROLLER.CTCurrentPlayingMainLevel == 5)
			{
				clubSuperChaseName = "PRIDE OF WCC";
			}
			if (clubSuperChaseName != string.Empty.ToString())
			{
				notificationid = 5;
				CONTROLLER.PushScreenNumber = 5;
				StartScheduleNotification("9", DynamicDelay, "You've reached " + clubSuperChaseName + " in Super Chase. Is that all you've got?", "Tournament", notificationid);
			}
		}
	}

	public void SO_Notification()
	{
		if (ObscuredPrefs.HasKey("SuperOverLevelDetail"))
		{
			string @string = ObscuredPrefs.GetString("SuperOverLevelDetail");
			string[] array = @string.Split("|"[0]);
			CONTROLLER.CurrentLevelCompleted = int.Parse(array[0]);
			if (18 - CONTROLLER.CurrentLevelCompleted >= 0 && 18 - CONTROLLER.CurrentLevelCompleted <= 18)
			{
				notificationid = 4;
				CONTROLLER.PushScreenNumber = 4;
				StartScheduleNotification("8", DynamicDelay, "Can you master the Super Over? You've only got " + (18 - CONTROLLER.CurrentLevelCompleted) + " more challenges to go!", "Tournament", notificationid);
			}
		}
	}

	public void WCF_Notification()
	{
		if (ObscuredPrefs.HasKey("wcplayoff") || ObscuredPrefs.HasKey("worldcup") || ObscuredPrefs.HasKey("tour"))
		{
			if (ObscuredPrefs.HasKey("wcplayoff") || ObscuredPrefs.HasKey("worldcup"))
			{
				notificationid = 16;
				CONTROLLER.PushScreenNumber = 16;
				StartScheduleNotification("3", DynamicDelay, "You're doing great in the WorldCup Tournament. Keep going!", "Tournament", notificationid);
			}
			else if (ObscuredPrefs.HasKey("tour"))
			{
				notificationid = 15;
				CONTROLLER.PushScreenNumber = 15;
				StartScheduleNotification("4", DynamicDelay, "You're doing great in the T20 WorldCup Tournament. Keep going!", "Tournament", notificationid);
			}
			else
			{
				notificationid = 15;
				CONTROLLER.PushScreenNumber = 15;
				StartScheduleNotification("5", DynamicDelay, "Play & Win unfinished tournaments to grab Coins and Rewards!", "Tournament", notificationid);
			}
		}
	}

	public void TC_Notification()
	{
	}

	public void Spin_Notification()
	{
		if (ObscuredPrefs.HasKey("userSpins") && ObscuredPrefs.GetInt("userSpins") >= 1)
		{
			notificationid = 14;
			CONTROLLER.PushScreenNumber = 14;
			int num = UnityEngine.Random.Range(0, 10);
			if (num >= 5)
			{
				StartScheduleNotification("6", DynamicDelay, "You've got " + ObscuredPrefs.GetInt("userSpins") + " free spins that are going to waste!", "Tournament", notificationid);
			}
			else if (num < 5)
			{
				StartScheduleNotification("7", DynamicDelay, "Grab your " + ObscuredPrefs.GetInt("userSpins") + " free spins quickly for free rewards!", "Tournament", notificationid);
			}
		}
	}

	public void CallAgainFn()
	{
		int @int = PlayerPrefs.GetInt("NewRepeatNoti");
		if (@int != -1)
		{
			PlayerPrefs.SetInt("NewRepeatNoti", -1);
		}
	}

	public void checkGeneralLength(string length)
	{
		GeneralNotifiationLength = int.Parse(length);
	}

	public void ScriptingGeneralNotification()
	{
		int num = notificationTexts.Count - GeneralNotifiationLength;
		int num2 = 0;
		for (int i = num; i < notificationTexts.Count; i++)
		{
			num2++;
			StartScheduleNotification((1000 + i).ToString(), Generaldelay * (double)num2, notificationTexts[i], "WCC Lite", 222);
		}
	}

	public void DailyBonusLocalNotification(double delay)
	{
		switch (UnityEngine.Random.Range(0, 5))
		{
		case 0:
			StartScheduleNotification("10", delay, "Perks of the Day! Claim your daily bonus coins ", "Daily Bonus", 21);
			break;
		case 1:
			StartScheduleNotification("11", delay, "Big Bonanza! Time to collect your bonus coins today", "Daily Bonus", 22);
			break;
		case 2:
			StartScheduleNotification("12", delay, "Your Bonus Coins will expire if not collected!", "Daily Bonus", 23);
			break;
		case 3:
			StartScheduleNotification("13", delay, "It is Bonus Time! Collect your bonus coins today", "Daily Bonus", 24);
			break;
		default:
			StartScheduleNotification("13", delay, "Bonus coins are up for grabs. Get them now!", "Daily Bonus", 24);
			break;
		}
		CONTROLLER.PushScreenNumber = 0;
	}
}
