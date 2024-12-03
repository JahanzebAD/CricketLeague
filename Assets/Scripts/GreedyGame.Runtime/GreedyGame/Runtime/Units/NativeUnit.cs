using UnityEngine;

namespace GreedyGame.Runtime.Units
{
	public class NativeUnit : MonoBehaviour
	{
		public string NativeUnitId = null;

		private SpriteRenderer _spriteRenderer = null;

		private Renderer _renderer = null;

		public Texture2D defaultTexture = null;

		public bool autoUpdateOnRefresh = true;

		public bool isSprite
		{
			get
			{
				_spriteRenderer = GetComponent<SpriteRenderer>();
				return _spriteRenderer != null;
			}
		}

		public Texture2D texture
		{
			get
			{
				if (isSprite && (bool)_spriteRenderer.sprite && (bool)_spriteRenderer.sprite.texture)
				{
					return _spriteRenderer.sprite.texture;
				}
				_renderer = GetComponent<Renderer>();
				if ((bool)_renderer && (bool)_renderer.material.mainTexture)
				{
					return _renderer.material.mainTexture as Texture2D;
				}
				return null;
			}
		}

		private void Awake()
		{
			_renderer = GetComponent<Renderer>();
			if (!_renderer)
			{
				Debug.LogError("[GGNatUnit] #25: NativeUnit ThemeUnit no renderer found " + base.gameObject.name);
			}
			else if (autoUpdateOnRefresh)
			{
				GreedyGameAgent.Instance.registerGameObject(base.gameObject, defaultTexture, NativeUnitId, applyToAll: false);
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
				Debug.LogError("[GreedyGame] #26: NativeUnit GreedyAdManager.init has not been called. First call GreedyGameAgent's init.");
			}
			else if ((bool)_renderer)
			{
				_spriteRenderer = GetComponent<SpriteRenderer>();
				GreedyGameAgent.Instance.getNativeUnitTexture(NativeUnitId, delegate(string _NativeUnitId, Texture2D brandedTexture)
				{
					if (brandedTexture != null)
					{
						if ((bool)_spriteRenderer)
						{
							Sprite sprite = Sprite.Create(brandedTexture, new Rect(0f, 0f, brandedTexture.width, brandedTexture.height), new Vector2(0.5f, 0.5f));
							_spriteRenderer.sprite = sprite;
						}
						else
						{
							_renderer.material.SetTexture("_MainTex", brandedTexture);
						}
					}
					else if (defaultTexture != null)
					{
						_renderer.material.SetTexture("_MainTex", defaultTexture);
					}
					else
					{
						Debug.LogWarning($"[GGNatUnit] SharedNativeUnit Couldnt Download {_NativeUnitId}. Couldn't use default texture since no texture was assigned");
					}
				});
			}
			else
			{
				Debug.LogError("[GGNatUnit] NativeUnit no renderer found");
			}
		}
	}
}
