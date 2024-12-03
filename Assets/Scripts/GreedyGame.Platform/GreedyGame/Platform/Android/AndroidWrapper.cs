using System;
using System.Collections;
//using GreedyGame.Commons;
using UnityEngine;

namespace GreedyGame.Platform.Android
{
	public class AndroidWrapper
	{
		private static string TAG = "GG[UtyAndWrp]";

		private static string GAME_ENGINE = "gameEngine";

		private static string ENGINE_VERSION = "engineVersion";

		private static string UNITY_PLAYER_CLASS = "com.unity3d.player.UnityPlayer";

		private static string CURRENT_ACTIVITY_FIELD = "currentActivity";

		private static string AGENT_BUILDER_CLASS = "com.greedygame.android.agent.GreedyGameAgent$Builder";

		private static string PRIVACY_OPTIONS_CLASS = "com.greedygame.android.agent.PrivacyOptions";

		private static string WITH_AGENT_LISTENER_FUNCTION = "withAgentListener";

		private static string DISABLE_REFLECTION = "disableReflection";

		private static string ENABLE_ADMOB_FUNCTION = "enableAdmob";

		private static string ENABLE_FAN_FUNCTION = "enableFacebook";

		private static string ENABLE_MOPUB_FUNCTION = "enableMopub";

		private static string ENABLE_CRASH_FUNCTION = "enableCrash";

		private static string SET_GG_NPA_FUNCTION = "setGgNpa";

		private static string ADD_UNITID_FUNCTION = "addUnitId";

		private static string SET_GAME_ID = "setGameId";

		private static string AGENT_BUILD_FUNCTION = "build";

		private static string RUN_ON_UI_THREAD_FUNCTION = "runOnUiThread";

		private static string WITH_PRIVACY_OPTIONS_FUNCTION = "withPrivacyOptions";

		private static string INIT_FUNCTION = "init";

		private static string SHOW_FLOAT_FUNCTION = "showFloat";

		private static string GET_PATH_FUNCTION = "getPath";

		private static string REMOVE_FLOAT_FUNCTION = "removeFloat";

		private static string REMOVE_ALL_FLOAT_FUNCTION = "removeAllFloat";

		private static string SHOW_UII_FUNCTION = "showUII";

		private static string START_EVENT_REFRESH_FUNCTION = "startEventRefresh";

		private static string SEND_CRASH_FUNCTION = "sendCrash";

		private static string ENABLE_ADMOB_COPPA = "enableCOPPAFilter";

		private static AndroidWrapper instance = null;

		private AndroidJavaObject greedyJavaAgent;

		private AndroidJavaObject greedyBuilder;

		private AndroidJavaObject ggPrivacyOptions;

		private AndroidJavaClass greedyJavaActivity;

		private AndroidJavaObject activity;

		public static AndroidWrapper Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new AndroidWrapper();
				}
				return instance;
			}
		}

		//public IEnumerator init(UnityStateListener listener, GGAdConfig adConfig, string gameEngine, string engineVersion)
		//{
		//	Debug.Log(TAG + " Android wrapper with reflection");
		//	greedyJavaActivity = new AndroidJavaClass(UNITY_PLAYER_CLASS);
		//	if (greedyJavaActivity != null)
		//	{
		//		activity = instance.greedyJavaActivity.GetStatic<AndroidJavaObject>(CURRENT_ACTIVITY_FIELD);
		//		if (activity != null)
		//		{
		//			Debug.Log(TAG + "activity is not null");
		//			greedyBuilder = new AndroidJavaObject(AGENT_BUILDER_CLASS, activity);
		//			ggPrivacyOptions = new AndroidJavaObject(PRIVACY_OPTIONS_CLASS);
		//		}
		//	}
		//	if (greedyBuilder != null && instance != null && activity != null && ggPrivacyOptions != null)
		//	{
		//		try
		//		{
		//			greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(WITH_AGENT_LISTENER_FUNCTION, new object[1] { listener });
		//			greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(ENABLE_ADMOB_FUNCTION, new object[1] { adConfig.getAdmobEnable() });
		//			greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(ENABLE_FAN_FUNCTION, new object[1] { adConfig.getFanEnable() });
		//			greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(ENABLE_MOPUB_FUNCTION, new object[1] { adConfig.getMopubEnable() });
		//			greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(ENABLE_CRASH_FUNCTION, new object[1] { adConfig.getCrashEnable() });
		//			greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(GAME_ENGINE, new object[1] { gameEngine });
		//			greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(ENGINE_VERSION, new object[1] { engineVersion });
		//			greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(ENABLE_ADMOB_COPPA, new object[1] { adConfig.getCoppa() });
		//			instance.ggPrivacyOptions.Call(SET_GG_NPA_FUNCTION, adConfig.getGgNpa());
		//			greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(SET_GAME_ID, new object[1] { adConfig.getGameId() });
		//			for (int i = 0; i < adConfig.getUnitList().Count; i++)
		//			{
		//				instance.greedyBuilder.Call<AndroidJavaObject>(ADD_UNITID_FUNCTION, new object[1] { adConfig.getUnitList()[i] });
		//			}
		//			activity.Call(RUN_ON_UI_THREAD_FUNCTION, (AndroidJavaRunnable)delegate
		//			{
		//				greedyJavaAgent = instance.greedyBuilder.Call<AndroidJavaObject>(AGENT_BUILD_FUNCTION, new object[0]);
		//				if (greedyJavaAgent != null)
		//				{
		//					instance.greedyJavaAgent.Call(WITH_PRIVACY_OPTIONS_FUNCTION, ggPrivacyOptions);
		//					instance.greedyJavaAgent.Call(INIT_FUNCTION);
		//				}
		//			});
		//		}
		//		catch (Exception ex)
		//		{
		//			Exception exception = ex;
		//			Debug.Log(TAG + " ERROR in calling runOnUiThread init " + exception);
		//		}
		//	}
		//	else
		//	{
		//		Debug.LogWarning(TAG + "Couldnt initialize inside Android wrapper [10]");
		//	}
		//	yield return null;
		//}

		public IEnumerator init(GGAdConfig adConfig, string gameEngine, string engineVersion)
		{
			Debug.Log(TAG + " Android wrapper init without reflection");
			greedyJavaActivity = new AndroidJavaClass(UNITY_PLAYER_CLASS);
			if (greedyJavaActivity != null)
			{
				activity = instance.greedyJavaActivity.GetStatic<AndroidJavaObject>(CURRENT_ACTIVITY_FIELD);
				if (activity != null)
				{
					Debug.Log(TAG + "activity is not null");
					greedyBuilder = new AndroidJavaObject(AGENT_BUILDER_CLASS, activity);
					ggPrivacyOptions = new AndroidJavaObject(PRIVACY_OPTIONS_CLASS);
				}
			}
			if (greedyBuilder != null && instance != null && activity != null && ggPrivacyOptions != null)
			{
				try
				{
					//greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(DISABLE_REFLECTION, new object[1] { adConfig.isReflectionDisabled() });
					//greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(ENABLE_ADMOB_FUNCTION, new object[1] { adConfig.getAdmobEnable() });
					//greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(ENABLE_FAN_FUNCTION, new object[1] { adConfig.getFanEnable() });
					//greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(ENABLE_MOPUB_FUNCTION, new object[1] { adConfig.getMopubEnable() });
					//greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(ENABLE_CRASH_FUNCTION, new object[1] { adConfig.getCrashEnable() });
					//greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(GAME_ENGINE, new object[1] { gameEngine });
					//greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(ENGINE_VERSION, new object[1] { engineVersion });
					//greedyBuilder = instance.greedyBuilder.Call<AndroidJavaObject>(SET_GAME_ID, new object[1] { adConfig.getGameId() });
					//instance.ggPrivacyOptions.Call(SET_GG_NPA_FUNCTION, adConfig.getGgNpa());
					//for (int i = 0; i < adConfig.getUnitList().Count; i++)
					//{
					//	instance.greedyBuilder.Call<AndroidJavaObject>(ADD_UNITID_FUNCTION, new object[1] { adConfig.getUnitList()[i] });
					//}
					activity.Call(RUN_ON_UI_THREAD_FUNCTION, (AndroidJavaRunnable)delegate
					{
						greedyJavaAgent = instance.greedyBuilder.Call<AndroidJavaObject>(AGENT_BUILD_FUNCTION, new object[0]);
						if (greedyJavaAgent != null)
						{
							instance.greedyJavaAgent.Call(WITH_PRIVACY_OPTIONS_FUNCTION, ggPrivacyOptions);
							instance.greedyJavaAgent.Call(INIT_FUNCTION);
						}
					});
				}
				catch (Exception ex)
				{
					Exception exception = ex;
					Debug.Log(TAG + " ERROR in calling runOnUiThread init " + exception);
				}
			}
			else
			{
				Debug.LogWarning(TAG + "Couldnt initialize inside Android wrapper [10]");
			}
			yield return null;
		}

		public void showFloat(string unit_id)
		{
			Debug.Log(TAG + " showFloat");
			if (isInitialized())
			{
				activity.Call(RUN_ON_UI_THREAD_FUNCTION, (AndroidJavaRunnable)delegate
				{
					instance.greedyJavaAgent.Call(SHOW_FLOAT_FUNCTION, activity, unit_id);
				});
			}
		}

		public string getFloatUnitPath(string unit_id)
		{
			Debug.Log(TAG + " getFloatUnitPath");
			if (!isInitialized())
			{
				return null;
			}
			return instance.greedyJavaAgent.Call<string>(GET_PATH_FUNCTION, new object[1] { unit_id });
		}

		public string getNativeUnitPath(string unit_id)
		{
			Debug.Log(TAG + " getNativeUnitPath");
			if (!isInitialized())
			{
				return null;
			}
			return instance.greedyJavaAgent.Call<string>(GET_PATH_FUNCTION, new object[1] { unit_id });
		}

		public void removeFloatUnit(string unitID)
		{
			Debug.Log(TAG + " removeFloatUnit");
			if (isInitialized())
			{
				instance.greedyJavaAgent.Call(REMOVE_FLOAT_FUNCTION, unitID);
			}
		}

		public void removeAllFloatUnits()
		{
			Debug.Log(TAG + " removeAllFloatUnits");
			if (isInitialized())
			{
				instance.greedyJavaAgent.Call(REMOVE_ALL_FLOAT_FUNCTION);
			}
		}

		public void showEngagementWindow(string unit_id)
		{
			Debug.Log(TAG + " showEngagementWindow ");
			if (isInitialized())
			{
				instance.greedyJavaAgent.Call(SHOW_UII_FUNCTION, unit_id);
			}
		}

		public void setDebugLog(bool debug)
		{
		}

		public void forcedExit()
		{
		}

		public void startEventRefresh()
		{
			Debug.Log(TAG + " startEventRefresh");
			if (isInitialized())
			{
				instance.greedyJavaAgent.Call(START_EVENT_REFRESH_FUNCTION);
			}
		}

		public void sendCrashReport(string error, bool isFatal)
		{
			Debug.Log(TAG + "sendCrashReport");
			if (isInitialized())
			{
				instance.greedyJavaAgent.Call(SEND_CRASH_FUNCTION, error, isFatal);
			}
		}

		public bool isInitialized()
		{
			if (instance == null || instance.greedyJavaAgent == null)
			{
				Debug.LogWarning(TAG + " android wrapper is null or agent is not initialized");
				return false;
			}
			return true;
		}
	}
}
