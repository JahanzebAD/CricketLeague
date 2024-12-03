using UnityEngine;

namespace GreedyGame.Runtime.Units
{
	public class SharedNativeUnit : MonoBehaviour
	{
		public string NativeUnitId = null;

		private Renderer _renderer = null;

		private Material sharedMaterial = null;

		public Texture2D defaultTexture = null;

		public bool autoUpdateOnRefresh = true;

		public bool isSprite
		{
			get
			{
				SpriteRenderer component = GetComponent<SpriteRenderer>();
				return component != null;
			}
		}

		public Texture2D texture
		{
			get
			{
				_renderer = GetComponent<Renderer>();
				if ((bool)_renderer && (bool)_renderer.sharedMaterial.mainTexture)
				{
					return _renderer.sharedMaterial.mainTexture as Texture2D;
				}
				return null;
			}
		}

		private void Awake()
		{
			_renderer = GetComponent<Renderer>();
			if (!_renderer)
			{
				Debug.LogError("[GGSNatUnit] SharedNativeUnit no renderer found " + base.gameObject.name);
			}
			else if (autoUpdateOnRefresh)
			{
				GreedyGameAgent.Instance.registerGameObject(base.gameObject, defaultTexture, NativeUnitId, applyToAll: true);
			}
			else
			{
				GG_SetUpTexture();
			}
		}

		private void OnDestroy()
		{
			GreedyGameAgent.Instance.unregisterGameObject(base.gameObject);
		}

		public GameObject getGameObject()
		{
			return base.gameObject;
		}

		public void GG_SetUpTexture()
		{
			if (!Application.isEditor && Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
			{
				return;
			}
			if (!GreedyGameAgent.isInitDone)
			{
				Debug.LogError("[GGSNatUnit] SharedNativeUnit GreedyAdManager.init has not been called. First call GreedyGameAgent's init.");
			}
			else
			{
				if (!_renderer)
				{
					return;
				}
				sharedMaterial = _renderer.sharedMaterial;
				GreedyGameAgent.Instance.getNativeUnitTexture(NativeUnitId, delegate(string _NativeUnitId, Texture2D brandedTexture)
				{
					if (brandedTexture != null)
					{
						sharedMaterial.SetTexture("_MainTex", brandedTexture);
					}
					else if (defaultTexture != null)
					{
						sharedMaterial.SetTexture("_MainTex", defaultTexture);
					}
				});
			}
		}
	}
}
