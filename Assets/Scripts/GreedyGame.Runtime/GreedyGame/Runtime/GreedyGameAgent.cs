using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using GreedyGame.Commons;
using GreedyGame.Platform;
using UnityEngine;

namespace GreedyGame.Runtime
{
	public class GreedyGameAgent : MonoBehaviour
	{
		public delegate void AdunitListener(string unitId, Texture2D texture);

		private static GreedyGameAgent instance = null;

		public static bool isInitDone = false;

		public static bool isListenerRegistered = false;

		//public static IAgentListener agentListener = null;

		private const string GREEDY_ENGINE = "unity";

		public static List<GGObject> gameObjects;

		public static List<string> unitIdList = new List<string>();

		public static Texture2D transparentTexture;

		private bool isUnityListenerEnabled;

		private static string TAG = "GG[UtyGGAgnt]";

		public static GreedyGameAgent Instance
		{
			get
			{
				if (instance == null)
				{
					GameObject gameObject = GameObject.Find(NativeWrapper.gameObjectName);
					if (gameObject == null)
					{
						gameObject = new GameObject();
						gameObject.name = NativeWrapper.gameObjectName;
					}
					instance = gameObject.AddComponent<GreedyGameAgent>();
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
					Version version = typeof(GreedyGameAgent).Assembly.GetName().Version;
					int build = version.Build;
					gameObjects = new List<GGObject>();
					transparentTexture = new Texture2D(50, 50, TextureFormat.ARGB32, mipChain: false);
					Color[] array = new Color[transparentTexture.width * transparentTexture.height];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Color.clear;
					}
					transparentTexture.SetPixels(array);
					transparentTexture.Apply();
					Debug.Log($" GreedyGameAgent Instance, gameObject = {instance.gameObject.name}, Version = {version.ToString()}");
				}
				return instance;
			}
		}

		public bool supported => RuntimePlatform.Android == Application.platform || Application.platform == RuntimePlatform.IPhonePlayer;

		private void OnApplicationPause(bool pauseStatus)
		{
			Debug.Log($"GG[UtyGGAgnt] GreedyGameAgent.OnApplicationPause - {pauseStatus}");
		}

		[Obsolete("getNativeUnitTexture is deprecated, please use getNativeUnitTexture with AdunitListener Callback instead.", true)]
		public Texture2D getNativeUnitTexture(string unit_id)
		{
			return null;
		}

		public void getFloatUnitTexture(string unit_id, AdunitListener textureListener)
		{
			Debug.Log($"GG[UtyGGAgnt] GreedyGameAgent.getFloatUnitTexture - {unit_id}");
			//string clickableUnitPath = getClickableUnitPath(unit_id);
			//if (clickableUnitPath != null)
			//{
			//	StartCoroutine(_readUnitFromFile(unit_id, clickableUnitPath, textureListener));
			//}
			//else
			//{
			//	textureListener(unit_id, null);
			//}
		}

		public void getNativeUnitTexture(string unit_id, AdunitListener textureListener)
		{
			Debug.Log($"GG[UtyGGAgnt] GreedyGameAgent.getNativeUnitTexture - {unit_id}");
			//string nonClickableUnitPath = getNonClickableUnitPath(unit_id);
			//if (nonClickableUnitPath != null)
			//{
			//	StartCoroutine(_readUnitFromFile(unit_id, nonClickableUnitPath, textureListener));
			//}
			//else
			//{
			//	textureListener(unit_id, null);
			//}
		}

		public void fetchFloatUnit(string unit_id)
		{
			Debug.Log($"GG[UtyGGAgnt] GreedyGameAgent.fetchFloatUnit - {unit_id}");
			NativeWrapper.fetchFloatUnit(unit_id);
		}

		public void showEngagementWindow(string unit_id)
		{
			Debug.Log($"GG[UtyGGAgnt] GreedyGameAgent.showEngagementWindow - {unit_id}");
			NativeWrapper.showEngagementWindow(unit_id);
		}

		public void removeAllFloatUnits()
		{
			Debug.Log("GG[UtyGGAgnt] GreedyGameAgent.removeCurrentFloatUnit");
			NativeWrapper.removeAllFloatUnits();
		}

		public void removeFloatUnit(string unitID)
		{
			Debug.Log("GG[UtyGGAgnt]  GreedyGameAgent.removeCurrentFloatUnit");
			NativeWrapper.removeFloatUnit(unitID);
		}

		public void sendCrashReport(string error, bool isFatal)
		{
			Debug.Log("GG[UtyGGAgnt] GreedyGameAgent.sendCrashReport ");
			NativeWrapper.sendCrashReport(error, isFatal);
		}

		public void registerGameObject(GameObject gameObj, Texture2D texture, string unitId, GGRenderDelegate renderDelegate)
		{
			if (texture == null)
			{
				Debug.Log("GG[UtyGGAgnt]  No default texture supplied using texture with 50 x 50 size ");
				texture = transparentTexture;
			}
			GGObject gGObject = new GGObject(gameObj, texture, unitId, renderDelegate);
			gameObjects.Add(gGObject);
			applyUpdate(gGObject);
		}

		public void registerGameObject(GameObject gameObj, Texture2D texture, string unitId, bool applyToAll)
		{
			if (texture == null)
			{
				Debug.Log("GG[UtyGGAgnt]  No default texture supplied using texture with 50 x 50 size ");
				texture = transparentTexture;
			}
			GGObject gGObject = new GGObject(gameObj, texture, unitId, applyToAll);
			gameObjects.Add(gGObject);
			applyUpdate(gGObject);
		}

		public void registerGameObject(GameObject gameObj, Texture2D texture, string unitId)
		{
			if (texture == null)
			{
				Debug.Log("GG[UtyGGAgnt]  No default texture supplied using texture with 50 x 50 size ");
				texture = transparentTexture;
			}
			GGObject gGObject = new GGObject(gameObj, texture, unitId);
			gameObjects.Add(gGObject);
			applyUpdate(gGObject);
		}

		public void unregisterGameObject(GameObject obj)
		{
			for (int i = 0; i < gameObjects.Count; i++)
			{
				if (gameObjects[i].getGameObject().Equals(obj))
				{
					Debug.Log("GG[UtyGGAgnt]  unregister found copy of object at count" + i + "with hgameobjects size" + gameObjects.Count);
					gameObjects.RemoveAt(i);
					break;
				}
			}
		}

		public void startEventRefresh()
		{
			if (!isInitDone)
			{
				Debug.Log("GG[UtyGGAgnt] GreedyGameAgent.startEventRefresh ignored since init is not yet done.");
				return;
			}
			Debug.Log("GG[UtyGGAgnt] GreedyGameAgent.startEventRefresh ");
			NativeWrapper.startEventRefresh();
		}

		private GreedyGameAgent()
		{
			Debug.Log($"GG[UtyGGAgnt] GreedyGameAgent.Constructor");
		}

		public void init(GGAdConfig adConfig)
		{
			Debug.Log("GG[UtyGGAgnt] GreedyGameAgent init");
			//agentListener = adConfig.getListener();
			//unitIdList = adConfig.getUnitList();
			//isUnityListenerEnabled = adConfig.isReflectionDisabled();
			if (!isListenerRegistered)
			{
				isListenerRegistered = true;
				Debug.Log("GG[UtyGGAgnt] GreedyGameAgent startCoroutine");
				if (isUnityListenerEnabled && Application.platform == RuntimePlatform.Android)
				{
					Debug.Log("GG[UtyGGAgnt] GreedyGameAgent unity send message");
					StartCoroutine(NativeWrapper.init(adConfig, "unity", Application.unityVersion));
				}
				else if (Application.platform == RuntimePlatform.IPhonePlayer)
				{
					Debug.Log("GG[UtyGGAgnt] GreedyGameAgent init to native wrapper for iPhone");
					//StartCoroutine(NativeWrapper.init(new MyUnityStateListener(), adConfig, "unity", Application.unityVersion));
				}
				else if (Application.platform == RuntimePlatform.Android)
				{
					Debug.Log("GG[UtyGGAgnt] GreedyGameAgent using reflection");
					//StartCoroutine(NativeWrapper.init(new MyUnityStateListener(), adConfig, "unity", Application.unityVersion));
				}
			}
			else
			{
				startEventRefresh();
			}
			Debug.Log($"GG[UtyGGAgnt] GreedyGameAgent init-out gameObject = {base.gameObject.name}, platform = {Application.platform}");
		}
		[Obsolete("This API is deprecated. Use getNonClickableUnitPath instead")]
		private IEnumerator _readUnitFromFile(string unit_id, string localPath, AdunitListener textureListener)
		{
			Debug.Log("GG[UtyGGAgnt] readUnitFromFile");
			WWW www = new WWW(localPath);
			yield return www;
			if (!string.IsNullOrEmpty(www.error))
			{
				Debug.LogError("GG[UtyGGAgnt] GreedyGameAgent Error while fetching.");
				textureListener(unit_id, null);
			}
			else
			{
				Texture2D t = new Texture2D(www.texture.width, www.texture.height, TextureFormat.DXT1, mipChain: false);
				www.LoadImageIntoTexture(t);
				www.Dispose();
				textureListener(unit_id, t);
			}
			yield return null;
		}

		public void CallCoroutineForRendering()
		{
			Debug.Log("GG[UtyGGAgnt] CallCoroutineForRendering called from listener");
			StartCoroutine(waitAndApply());
		}

		private IEnumerator waitAndApply()
		{
			if (gameObjects.Count <= 0)
			{
				yield break;
			}
			yield return new WaitForSeconds(0.3f);
			List<GGObject> cloneObjects = new List<GGObject>(gameObjects);
			foreach (GGObject obj in cloneObjects)
			{
				try
				{
					if (obj != null)
					{
						applyUpdate(obj);
					}
				}
				catch (InvalidOperationException)
				{
				}
				yield return new WaitForSeconds(0.3f);
			}
		}

		public void applyUpdate(GGObject obj)
		{
			if (obj.getRenderDelegate() != null)
			{
				StartCoroutine(passTextureToDelegate(obj));
			}
			else
			{
				StartCoroutine(applyUpdateByRenderer(obj));
			}
		}

		private IEnumerator passTextureToDelegate(GGObject obj)
		{
			if (!isInitDone && obj == null)
			{
				Debug.Log("GG[UtyGGObject] Delegate Rendering. Init has not been called or obj is null.");
				yield break;
			}
			GGRenderDelegate renderDelegate = obj.getRenderDelegate();
			yield return null;
		}

		public IEnumerator applyUpdateByRenderer(GGObject obj)
		{
			if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
			{
				yield break;
			}
			if (!isInitDone && obj == null)
			{
				Debug.Log("GG[UtyGGObject] Internal rendering. Init has not been called or obj is null.");
			}
			else
			{
				if (!obj.getRenderer())
				{
					yield break;
				}
				Texture2D texture = new Texture2D(obj.getDefaultTexture().width, obj.getDefaultTexture().height);
					texture = obj.getDefaultTexture();
				if (!(texture != null))
				{
					yield break;
				}
				if ((bool)obj.getSpriteRenderer())
				{
					Sprite s = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
					obj.getSpriteRenderer().sprite = s;
				}
				else if ((bool)obj.getRenderer())
				{
					if (obj.getApplyToAll())
					{
						obj.getRenderer().sharedMaterial.mainTexture = texture;
					}
					else
					{
						obj.getRenderer().material.mainTexture = texture;
					}
				}
				else
				{
					Debug.Log("GG[UtyGGObject] No renderer found");
				}
			}
		}

		private void GG_onAvailable(string campaignId)
		{
			Debug.Log("[GreedyGame] #17: GreedyGameAgent GG_onAvailable");
			isInitDone = true;

			Instance.CallCoroutineForRendering();
		}

		private void GG_onUnavailable()
		{
			isInitDone = true;
			Debug.Log("[GreedyGame] #18: GreedyGameAgent GG_onUnavailable");
			Instance.CallCoroutineForRendering();
		}

		private void GG_onError(string message)
		{
			isInitDone = true;
			Debug.Log("[GreedyGame] #19: GreedyGameAgent GG_onError");
		}
	}
}
