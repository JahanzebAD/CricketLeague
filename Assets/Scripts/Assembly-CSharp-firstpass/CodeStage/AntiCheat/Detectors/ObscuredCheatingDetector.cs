using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeStage.AntiCheat.Detectors
{
	[AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Obscured Cheating Detector")]
	[HelpURL("http://codestage.net/uas_files/actk/api/class_code_stage_1_1_anti_cheat_1_1_detectors_1_1_obscured_cheating_detector.html")]
	public class ObscuredCheatingDetector : ActDetectorBase
	{
		internal const string ComponentName = "Obscured Cheating Detector";

		internal const string FinalLogPrefix = "[ACTk] Obscured Cheating Detector: ";

		private static int instancesInScene;

		[Tooltip("Max allowed difference between encrypted and fake values in ObscuredDouble. Increase in case of false positives.")]
		public double doubleEpsilon = 0.0001;

		[Tooltip("Max allowed difference between encrypted and fake values in ObscuredFloat. Increase in case of false positives.")]
		public float floatEpsilon = 0.0001f;

		[Tooltip("Max allowed difference between encrypted and fake values in ObscuredVector2. Increase in case of false positives.")]
		public float vector2Epsilon = 0.1f;

		[Tooltip("Max allowed difference between encrypted and fake values in ObscuredVector3. Increase in case of false positives.")]
		public float vector3Epsilon = 0.1f;

		[Tooltip("Max allowed difference between encrypted and fake values in ObscuredQuaternion. Increase in case of false positives.")]
		public float quaternionEpsilon = 0.1f;

		public static ObscuredCheatingDetector Instance { get; private set; }

		private static ObscuredCheatingDetector GetOrCreateInstance
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
				Instance = ActDetectorBase.detectorsContainer.AddComponent<ObscuredCheatingDetector>();
				return Instance;
			}
		}

		internal static bool ExistsAndIsRunning => (object)Instance != null && Instance.IsRunning;

		private ObscuredCheatingDetector()
		{
		}

		public static ObscuredCheatingDetector AddToSceneOrGetExisting()
		{
			return GetOrCreateInstance;
		}

		public static void StartDetection()
		{
			if (Instance != null)
			{
				Instance.StartDetectionInternal(null);
			}
		}

		public static void StartDetection(Action callback)
		{
			GetOrCreateInstance.StartDetectionInternal(callback);
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
			if (Init(Instance, "Obscured Cheating Detector"))
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

		private void StartDetectionInternal(Action callback)
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
				started = true;
				isRunning = true;
			}
		}

		protected override void StartDetectionAutomatically()
		{
			StartDetectionInternal(null);
		}

		protected override void DisposeInternal()
		{
			base.DisposeInternal();
			if (Instance == this)
			{
				Instance = null;
			}
		}
	}
}
