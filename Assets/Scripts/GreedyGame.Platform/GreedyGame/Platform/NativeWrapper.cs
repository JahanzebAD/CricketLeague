using System;
using System.Collections;
//using GreedyGame.Commons;
//using GreedyGame.iOS;
using GreedyGame.Platform.Android;
using GreedyGame.Platform.Dummy;
using UnityEngine;

namespace GreedyGame.Platform
{
	public class NativeWrapper
	{
		public static string gameObjectName = "GreedyGameRootObject";

		//public static IEnumerator init(UnityStateListener listener, GGAdConfig adConfig, string gameEngine, string engineVersion)
		//{
		//	Version version = typeof(NativeWrapper).Assembly.GetName().Version;
		//	Debug.Log($"[GGNatWrp] reflection NativeWrapper init, Version = {version.ToString()}");
		//	IEnumerator e = null;
		//	if (Application.platform == RuntimePlatform.Android)
		//	{
		//		e = AndroidWrapper.Instance.init(listener, adConfig, gameEngine, engineVersion);
		//	}
		//	else if (Application.platform == RuntimePlatform.IPhonePlayer)
		//	{
		//		e = iOSWrapper.Instance.init(listener, adConfig, gameEngine, engineVersion);
		//	}
		//	else if (Application.isEditor)
		//	{
		//		e = DummyWrapper.Instance.init();
		//	}
		//	else
		//	{
		//		GameObject gameObject = GameObject.Find(gameObjectName);
		//		gameObject.SendMessage("GG_onUnavailable", "");
		//	}
		//	if (e != null)
		//	{
		//		while (e.MoveNext())
		//		{
		//			yield return e.Current;
		//		}
		//	}
		//	else
		//	{
		//		yield return null;
		//	}
		//}

		public static IEnumerator init(GGAdConfig adConfig, string gameEngine, string engineVersion)
		{
			Version version = typeof(NativeWrapper).Assembly.GetName().Version;
			Debug.Log($"[GGNatWrp] NativeWrapper init, Version = {version.ToString()}");
			IEnumerator e = null;
			if (Application.platform == RuntimePlatform.Android)
			{
				e = AndroidWrapper.Instance.init(adConfig, gameEngine, engineVersion);
			}
			else if (Application.isEditor)
			{
				e = DummyWrapper.Instance.init();
			}
			else
			{
				GameObject gameObject = GameObject.Find(gameObjectName);
				gameObject.SendMessage("GG_onUnavailable", "");
			}
			if (e != null)
			{
				while (e.MoveNext())
				{
					yield return e.Current;
				}
			}
			else
			{
				yield return null;
			}
		}

		public static void fetchFloatUnit(string unit_id)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidWrapper.Instance.showFloat(unit_id);
			}
			else if (Application.isEditor)
			{
				DummyWrapper.Instance.showFloat(unit_id);
			}
		}

		public static string FloatUnitPath(string unit_id)
		{
			string result = null;
			if (Application.platform == RuntimePlatform.Android)
			{
				result = AndroidWrapper.Instance.getFloatUnitPath(unit_id);
			}
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				//result = iOSWrapper.Instance.getPath(unit_id);
			}
			else if (Application.isEditor)
			{
				DummyWrapper.Instance.getFloatUnitPath(unit_id);
			}
			return result;
		}

		public static string NativeUnitPath(string unit_id)
		{
			string result = null;
			if (Application.platform == RuntimePlatform.Android)
			{
				result = AndroidWrapper.Instance.getNativeUnitPath(unit_id);
			}
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				//result = iOSWrapper.Instance.getPath(unit_id);
			}
			else if (Application.isEditor)
			{
				DummyWrapper.Instance.getNativeUnitPath(unit_id);
			}
			return result;
		}

		public static void showEngagementWindow(string unit_id)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidWrapper.Instance.showEngagementWindow(unit_id);
			}
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				//iOSWrapper.Instance.showUII(unit_id);
			}
			else if (Application.isEditor)
			{
				DummyWrapper.Instance.showEngagementWindow(unit_id);
			}
		}

		public static void removeFloatUnit(string unitID)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidWrapper.Instance.removeFloatUnit(unitID);
			}
			else if (Application.isEditor)
			{
				DummyWrapper.Instance.removeFloatUnit(unitID);
			}
		}

		public static void forcedExit()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidWrapper.Instance.forcedExit();
			}
			else if (Application.isEditor)
			{
				DummyWrapper.Instance.forcedExit();
			}
		}

		public static void removeAllFloatUnits()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidWrapper.Instance.removeAllFloatUnits();
			}
			else if (Application.isEditor)
			{
				DummyWrapper.Instance.removeAllFloatUnits();
			}
		}

		public static void setDebugLog(bool b)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidWrapper.Instance.setDebugLog(b);
			}
			else if (Application.isEditor)
			{
				DummyWrapper.Instance.setDebugLog(b);
			}
		}

		public static void startEventRefresh()
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidWrapper.Instance.startEventRefresh();
			}
			else if (Application.platform == RuntimePlatform.IPhonePlayer)
			{
				//iOSWrapper.Instance.startEventRefresh();
			}
			else if (Application.isEditor)
			{
				DummyWrapper.Instance.startEventRefresh();
			}
		}

		public static void sendCrashReport(string error, bool isFatal)
		{
			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidWrapper.Instance.sendCrashReport(error, isFatal);
			}
			else if (Application.isEditor)
			{
				DummyWrapper.Instance.sendCrashReport(error, isFatal);
			}
		}
	}
}
