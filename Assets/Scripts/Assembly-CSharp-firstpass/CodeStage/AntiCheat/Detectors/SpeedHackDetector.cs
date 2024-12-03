using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeStage.AntiCheat.Detectors
{
	[AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Speed Hack Detector")]
	[HelpURL("http://codestage.net/uas_files/actk/api/class_code_stage_1_1_anti_cheat_1_1_detectors_1_1_speed_hack_detector.html")]
	public class SpeedHackDetector : ActDetectorBase
	{
		internal const string ComponentName = "Speed Hack Detector";

		internal const string LogPrefix = "[ACTk] Speed Hack Detector: ";

		private const long TicksPerSecond = 10000000L;

		private const int Threshold = 5000000;

		private const float ThresholdFloat = 0.5f;

		private const string RoutinesClassPath = "net.codestage.actk.androidnative.ACTkAndroidRoutines";

		private static int instancesInScene;

		[Tooltip("Time (in seconds) between detector checks.")]
		public float interval = 1f;

		[Tooltip("Maximum false positives count allowed before registering speed hack.")]
		public byte maxFalsePositives = 3;

		[Tooltip("Amount of sequential successful checks before clearing internal false positives counter.\nSet 0 to disable Cool Down feature.")]
		public int coolDown = 30;

		private byte currentFalsePositives;

		private int currentCooldownShots;

		private long ticksOnStart;

		private long vulnerableTicksOnStart;

		private long previousTicks;

		private long previousIntervalTicks;

		private float vulnerableTimeOnStart;

		private AndroidJavaClass routinesClass;

		private bool androidTimeReadAttemptWasMade;

		public static SpeedHackDetector Instance { get; private set; }

		private static SpeedHackDetector GetOrCreateInstance
		{
			get
			{
				if (Instance != null)
				{
					return Instance;
				}
				if (ActDetectorBase.detectorsContainer == null)
				{
					ActDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
				}
				Instance = ActDetectorBase.detectorsContainer.AddComponent<SpeedHackDetector>();
				return Instance;
			}
		}

		private SpeedHackDetector()
		{
		}

		public static SpeedHackDetector AddToSceneOrGetExisting()
		{
			return GetOrCreateInstance;
		}

		public static void StartDetection()
		{
			if (Instance != null)
			{
				Instance.StartDetectionInternal(null, Instance.interval, Instance.maxFalsePositives, Instance.coolDown);
			}
		}

		public static void StartDetection(Action callback)
		{
			StartDetection(callback, GetOrCreateInstance.interval);
		}

		public static void StartDetection(Action callback, float interval)
		{
			StartDetection(callback, interval, GetOrCreateInstance.maxFalsePositives);
		}

		public static void StartDetection(Action callback, float interval, byte maxFalsePositives)
		{
			StartDetection(callback, interval, maxFalsePositives, GetOrCreateInstance.coolDown);
		}

		public static void StartDetection(Action callback, float interval, byte maxFalsePositives, int coolDown)
		{
			GetOrCreateInstance.StartDetectionInternal(callback, interval, maxFalsePositives, coolDown);
		}

		public static void StopDetection()
		{
			if (Instance != null)
			{
				Instance.StopDetectionInternal();
			}
		}

		public static void Dispose()
		{
			if (Instance != null)
			{
				Instance.DisposeInternal();
			}
		}

		private void Awake()
		{
			instancesInScene++;
			if (Init(Instance, "Speed Hack Detector"))
			{
				Instance = this;
			}
			SceneManager.sceneLoaded += OnLevelWasLoadedNew;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			instancesInScene--;
		}

		private void OnLevelWasLoadedNew(Scene scene, LoadSceneMode mode)
		{
			OnLevelLoadedCallback();
		}

		private void OnLevelLoadedCallback()
		{
			if (instancesInScene < 2)
			{
				if (!keepAlive)
				{
					DisposeInternal();
				}
			}
			else if (!keepAlive && Instance != this)
			{
				DisposeInternal();
			}
		}

		private void OnApplicationPause(bool pause)
		{
			if (!pause)
			{
				ResetStartTicks();
			}
		}

		private void Update()
		{
			if (!isRunning)
			{
				return;
			}
			long reliableTicks = GetReliableTicks();
			long num = reliableTicks - previousTicks;
			if (num < 0 || num > 10000000)
			{
				ResetStartTicks();
				return;
			}
			previousTicks = reliableTicks;
			long num2 = (long)(interval * 1E+07f);
			if (reliableTicks - previousIntervalTicks < num2)
			{
				return;
			}
			long num3 = reliableTicks - ticksOnStart;
			long num4 = (long)Environment.TickCount * 10000L;
			long num5 = num4 - vulnerableTicksOnStart;
			bool flag = Mathf.Abs(num5 - num3) > 5000000f;
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float num6 = realtimeSinceStartup - vulnerableTimeOnStart;
			bool flag2 = Math.Abs((float)num3 / 1E+07f - num6) > 0.5f;
			if (flag || flag2)
			{
				currentFalsePositives++;
				if (currentFalsePositives > maxFalsePositives)
				{
					OnCheatingDetected();
				}
				else
				{
					currentCooldownShots = 0;
					ResetStartTicks();
				}
			}
			else if (currentFalsePositives > 0 && coolDown > 0)
			{
				currentCooldownShots++;
				if (currentCooldownShots >= coolDown)
				{
					currentFalsePositives = 0;
				}
			}
			previousIntervalTicks = reliableTicks;
		}

		private void StartDetectionInternal(Action callback, float checkInterval, byte falsePositives, int shotsTillCooldown)
		{
			if (!isRunning && base.enabled)
			{
				if (callback == null || detectionEventHasListener)
				{
				}
				if (callback == null && !detectionEventHasListener)
				{
					base.enabled = false;
					return;
				}
				base.CheatDetected += callback;
				interval = checkInterval;
				maxFalsePositives = falsePositives;
				coolDown = shotsTillCooldown;
				ResetStartTicks();
				currentFalsePositives = 0;
				currentCooldownShots = 0;
				started = true;
				isRunning = true;
			}
		}

		protected override void StartDetectionAutomatically()
		{
			StartDetectionInternal(null, interval, maxFalsePositives, coolDown);
		}

		protected override void DisposeInternal()
		{
			base.DisposeInternal();
			if (Instance == this)
			{
				Instance = null;
			}
			ReleaseAndroidClass();
		}

		private void ResetStartTicks()
		{
			ticksOnStart = GetReliableTicks();
			vulnerableTicksOnStart = (long)Environment.TickCount * 10000L;
			previousTicks = ticksOnStart;
			previousIntervalTicks = ticksOnStart;
			vulnerableTimeOnStart = Time.realtimeSinceStartup;
		}

		private long GetReliableTicks()
		{
			long num = 0L;
			num = TryReadTicksFromAndroidRoutine();
			if (num == 0)
			{
				num = DateTime.UtcNow.Ticks;
			}
			return num;
		}

		private long TryReadTicksFromAndroidRoutine()
		{
			long num = 0L;
			if (!androidTimeReadAttemptWasMade)
			{
				androidTimeReadAttemptWasMade = true;
				try
				{
					routinesClass = new AndroidJavaClass("net.codestage.actk.androidnative.ACTkAndroidRoutines");
				}
				catch (Exception)
				{
				}
			}
			if (routinesClass == null)
			{
				return num;
			}
			try
			{
				num = routinesClass.CallStatic<long>("GetSystemNanoTime", new object[0]);
				num /= 100;
				return num;
			}
			catch (Exception)
			{
				return num;
			}
		}

		private void ReleaseAndroidClass()
		{
			routinesClass.Dispose();
		}
	}
}
