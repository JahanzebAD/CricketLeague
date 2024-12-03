using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GreedyGame.Platform.Dummy
{
	public class DummyWrapper
	{
		private static DummyWrapper instance = null;

		public const string gameObjectName = "GreedyGameRootObject";

		private const string ANDROID_VALUES = "Assets/Plugins/Android/GreedyAndroidWrapper/res/values/greedy.xml";

		private static string[] _units;

		private static string _game_id;

		private static string _theme_id;

		private static string _campaignPath;

		private GameObject gameObject = null;

		private static EngineRender render = null;

		public static DummyWrapper Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new DummyWrapper();
				}
				return instance;
			}
		}

		public string CampaignPath => _campaignPath;

		private DummyWrapper()
		{
			render = EngineRender.Instance;
		}

		private static string init2API(string game_id)
		{
			return $"http://api.greedygame.com/v1.5/{game_id}/init?edit=1";
		}

		private static string unitAPI(string game_id, string theme_id, string unit)
		{
			return $"http://api.greedygame.com/v1.5/{game_id}/delivery/{theme_id}/{unit}";
		}

		public IEnumerator init()
		{
			Debug.Log("Editor Wrapper");
			string game_id = null;
			string[] units = null;
			gameObject = GameObject.Find("GreedyGameRootObject");
			if (!Application.isEditor || !gameObject)
			{
				onUnavailable();
			}
			WWW www = new WWW(headers: new Dictionary<string, string>
			{
				{ "Game-Engine", "unity" },
				{ "Debug", "1" }
			}, url: init2API(game_id), postData: null);
			yield return www;
			Dictionary<string, object> dict = Json.Deserialize(www.text) as Dictionary<string, object>;
			Debug.Log(www.text);
			try
			{
				_theme_id = (string)dict["theme_id"];
				string parent = Directory.GetParent(Application.dataPath).FullName;
				char p = Path.DirectorySeparatorChar;
				_campaignPath = parent + p + "Temp" + p + "GreedyGame" + p + "run" + p + _theme_id;
			}
			catch (KeyNotFoundException e2)
			{
				_theme_id = null;
				_campaignPath = null;
				Debug.LogWarning($"No campaign found!\n {e2.Message}");
			}
			Debug.Log($"Incoming theme is {_theme_id}");
			if (_theme_id != null)
			{
				_units = units;
				_game_id = game_id;
				if (!Directory.Exists(_campaignPath))
				{
					Directory.CreateDirectory(_campaignPath);
					IEnumerator e = Func1();
					while (e.MoveNext())
					{
						yield return e.Current;
					}
				}
				onAvailable();
			}
			else
			{
				onUnavailable();
			}
		}

		private IEnumerator Func1()
		{
			Dictionary<string, string> headers = new Dictionary<string, string>
			{
				{ "Game-Engine", "unity" },
				{ "Debug", "1" }
			};
			for (int i = 0; i < _units.Length; i++)
			{
				string unit_api = unitAPI(_game_id, _theme_id, _units[i]);
				Debug.Log($"Download from url {unit_api}");
				WWW www = new WWW(unit_api, null, headers);
				yield return www;
				string status = null;
				if (www.responseHeaders.ContainsKey("STATUS"))
				{
					status = www.responseHeaders["STATUS"];
				}
				Debug.Log($"Response {status}");
				if (status != null && !status.Contains("204"))
				{
					int _progress = (int)((double)i * 1.0) / _units.Length;
					onProgress(_progress);
					string savePath = _campaignPath + "/" + _units[i];
					string folder = Path.GetDirectoryName(savePath);
					if (!Directory.Exists(folder))
					{
						Directory.CreateDirectory(folder);
					}
					File.WriteAllBytes(savePath, www.bytes);
				}
			}
		}

		private void onUnavailable()
		{
			gameObject.SendMessage("GG_onUnavailable", "");
		}

		private void onAvailable()
		{
			gameObject.SendMessage("GG_onAvailable", "");
		}

		private void onProgress(int p)
		{
			gameObject.SendMessage("GG_onProgress", p.ToString());
		}

		public void showFloat(string unit_id)
		{
			Debug.LogWarning("Dummy-Wrapper fetchFloatUnit will run on device only");
			render.drawHeadAd(unit_id, -1, -1);
		}

		public void showEngagementWindow(string unit_id)
		{
			Debug.Log($"Dummy-Wrapper showEngagementWindow - {unit_id}");
		}

		public void forcedExit()
		{
			Debug.Log($"Dummy-Wrapper forcedExit ");
		}

		public void getFloatUnitPath(string unit_id)
		{
			Debug.Log($"Dummy-Wrapper FloatUnitPath - {unit_id}");
		}

		public void getNativeUnitPath(string unit_id)
		{
			Debug.Log($"Dummy-Wrapper NativeUnitPath - {unit_id}");
		}

		public void removeAllFloatUnits()
		{
			Debug.LogWarning("Dummy-Wrapper removeAllFloatUnits will run on device only");
			render.drawHeadAd(null, -1, -1);
		}

		public void removeFloatUnit(string unitID)
		{
			Debug.LogWarning("Dummy-Wrapper removeFloatUnit will run on device only");
			render.drawHeadAd(null, -1, -1);
		}

		public void setDebugLog(bool b)
		{
			Debug.LogWarning("Dummy-Wrapper setDebugLog will run on device only");
		}

		public void startEventRefresh()
		{
			Debug.LogWarning("Dummy-Wrapper event refresh");
		}

		public void sendCrashReport(string error, bool isFatal)
		{
			Debug.LogWarning("Dummy-Wrapper send crash report");
		}
	}
}
