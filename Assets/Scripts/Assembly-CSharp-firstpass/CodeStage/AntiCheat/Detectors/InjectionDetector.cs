using System;
using System.IO;
using System.Reflection;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeStage.AntiCheat.Detectors
{
	[AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Injection Detector")]
	[HelpURL("http://codestage.net/uas_files/actk/api/class_code_stage_1_1_anti_cheat_1_1_detectors_1_1_injection_detector.html")]
	public class InjectionDetector : ActDetectorBase
	{
		private class AllowedAssembly
		{
			public readonly string name;

			public readonly int[] hashes;

			public AllowedAssembly(string name, int[] hashes)
			{
				this.name = name;
				this.hashes = hashes;
			}
		}

		internal const string ComponentName = "Injection Detector";

		internal const string FinalLogPrefix = "[ACTk] Injection Detector: ";

		private static int instancesInScene;

		private bool signaturesAreNotGenuine;

		private AllowedAssembly[] allowedAssemblies;

		private string[] hexTable;

		public static InjectionDetector Instance { get; private set; }

		private static InjectionDetector GetOrCreateInstance
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
				Instance = ActDetectorBase.detectorsContainer.AddComponent<InjectionDetector>();
				return Instance;
			}
		}

		public new event Action<string> CheatDetected;

		private InjectionDetector()
		{
		}

		public static InjectionDetector AddToSceneOrGetExisting()
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

		public static void StartDetection(Action<string> callback)
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
			if (Init(Instance, "Injection Detector"))
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

		private void StartDetectionInternal(Action<string> callback)
		{
			if (isRunning || !base.enabled)
			{
				return;
			}
			if ((this.CheatDetected == null && callback == null) || detectionEventHasListener)
			{
			}
			if (this.CheatDetected == null && callback == null && !detectionEventHasListener)
			{
				base.enabled = false;
				return;
			}
			CheatDetected += callback;
			started = true;
			isRunning = true;
			if (allowedAssemblies == null)
			{
				LoadAndParseAllowedAssemblies();
			}
			string cause;
			if (signaturesAreNotGenuine)
			{
				OnCheatingDetected("signatures");
			}
			else if (!FindInjectionInCurrentAssemblies(out cause))
			{
				AppDomain.CurrentDomain.AssemblyLoad += OnNewAssemblyLoaded;
			}
			else
			{
				OnCheatingDetected(cause);
			}
		}

		protected override void StartDetectionAutomatically()
		{
			StartDetectionInternal(null);
		}

		protected override void PauseDetector()
		{
			AppDomain.CurrentDomain.AssemblyLoad -= OnNewAssemblyLoaded;
			base.PauseDetector();
		}

		protected override bool ResumeDetector()
		{
			if (!base.ResumeDetector())
			{
				return false;
			}
			AppDomain.CurrentDomain.AssemblyLoad += OnNewAssemblyLoaded;
			return true;
		}

		protected override bool DetectorHasCallbacks()
		{
			return base.DetectorHasCallbacks() || this.CheatDetected != null;
		}

		protected override void StopDetectionInternal()
		{
			if (started)
			{
				AppDomain.CurrentDomain.AssemblyLoad -= OnNewAssemblyLoaded;
			}
			base.StopDetectionInternal();
		}

		protected override void DisposeInternal()
		{
			base.DisposeInternal();
			if (Instance == this)
			{
				Instance = null;
			}
		}

		private void OnCheatingDetected(string cause)
		{
			if (this.CheatDetected != null)
			{
				this.CheatDetected(cause);
			}
			if (detectionEventHasListener)
			{
				detectionEvent.Invoke();
			}
			if (autoDispose)
			{
				DisposeInternal();
			}
			else
			{
				StopDetectionInternal();
			}
		}

		private void OnNewAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
		{
			if (!AssemblyAllowed(args.LoadedAssembly))
			{
				OnCheatingDetected(args.LoadedAssembly.FullName);
			}
		}

		private bool FindInjectionInCurrentAssemblies(out string cause)
		{
			cause = null;
			bool result = false;
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			if (assemblies.Length == 0)
			{
				cause = "no assemblies";
				result = true;
			}
			else
			{
				Assembly[] array = assemblies;
				foreach (Assembly assembly in array)
				{
					if (!AssemblyAllowed(assembly))
					{
						cause = assembly.FullName;
						result = true;
						break;
					}
				}
			}
			return result;
		}

		private bool AssemblyAllowed(Assembly ass)
		{
			string text = ass.GetName().Name;
			int assemblyHash = GetAssemblyHash(ass);
			bool result = false;
			for (int i = 0; i < allowedAssemblies.Length; i++)
			{
				AllowedAssembly allowedAssembly = allowedAssemblies[i];
				if (allowedAssembly.name == text && Array.IndexOf(allowedAssembly.hashes, assemblyHash) != -1)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private void LoadAndParseAllowedAssemblies()
		{
			TextAsset textAsset = (TextAsset)Resources.Load("fndid", typeof(TextAsset));
			if (textAsset == null)
			{
				signaturesAreNotGenuine = true;
				return;
			}
			string[] separator = new string[1] { ":" };
			MemoryStream memoryStream = new MemoryStream(textAsset.bytes);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			int num = binaryReader.ReadInt32();
			allowedAssemblies = new AllowedAssembly[num];
			for (int i = 0; i < num; i++)
			{
				string value = binaryReader.ReadString();
				value = ObscuredString.EncryptDecrypt(value, "Elina");
				string[] array = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
				int num2 = array.Length;
				if (num2 > 1)
				{
					string text = array[0];
					int[] array2 = new int[num2 - 1];
					for (int j = 1; j < num2; j++)
					{
						array2[j - 1] = int.Parse(array[j]);
					}
					allowedAssemblies[i] = new AllowedAssembly(text, array2);
					continue;
				}
				signaturesAreNotGenuine = true;
				binaryReader.Close();
				memoryStream.Close();
				return;
			}
			binaryReader.Close();
			memoryStream.Close();
			Resources.UnloadAsset(textAsset);
			hexTable = new string[256];
			for (int k = 0; k < 256; k++)
			{
				hexTable[k] = k.ToString("x2");
			}
		}

		private int GetAssemblyHash(Assembly ass)
		{
			AssemblyName assemblyName = ass.GetName();
			byte[] publicKeyToken = assemblyName.GetPublicKeyToken();
			string text = ((publicKeyToken.Length < 8) ? assemblyName.Name : (assemblyName.Name + PublicKeyTokenToString(publicKeyToken)));
			int num = 0;
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				num += text[i];
				num += num << 10;
				num ^= num >> 6;
			}
			num += num << 3;
			num ^= num >> 11;
			return num + (num << 15);
		}

		private string PublicKeyTokenToString(byte[] bytes)
		{
			string text = string.Empty;
			for (int i = 0; i < 8; i++)
			{
				text += hexTable[bytes[i]];
			}
			return text;
		}
	}
}
