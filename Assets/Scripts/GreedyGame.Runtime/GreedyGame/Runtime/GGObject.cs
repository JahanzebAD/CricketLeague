using UnityEngine;

namespace GreedyGame.Runtime
{
	public class GGObject
	{
		private Renderer _renderer = null;

		private SpriteRenderer _spriteRenderer = null;

		private GameObject gameObject = null;

		private Texture2D defaultTexture = null;

		private string unitId = null;

		private bool applyToAll = true;

		public GGRenderDelegate renderDelegate = null;

		public GGObject(GameObject obj, Texture2D texture, string unitId, bool applyToAll)
		{
			gameObject = obj;
			defaultTexture = texture;
			this.unitId = unitId;
			this.applyToAll = applyToAll;
			_renderer = gameObject.GetComponent<Renderer>();
			_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		}

		public GGObject(GameObject obj, Texture2D texture, string unitId)
		{
			gameObject = obj;
			defaultTexture = texture;
			this.unitId = unitId;
			applyToAll = true;
			_renderer = gameObject.GetComponent<Renderer>();
			_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		}

		public GGObject(GameObject obj, Texture2D texture, string unitId, GGRenderDelegate renderDelegate)
		{
			gameObject = obj;
			defaultTexture = texture;
			this.unitId = unitId;
			this.renderDelegate = renderDelegate;
			_renderer = gameObject.GetComponent<Renderer>();
			_spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		}

		public void setGameObject(GameObject gameObj)
		{
			gameObject = gameObj;
		}

		public Renderer getRenderer()
		{
			return _renderer;
		}

		public SpriteRenderer getSpriteRenderer()
		{
			return _spriteRenderer;
		}

		public string getUnitId()
		{
			return unitId;
		}

		public GGRenderDelegate getRenderDelegate()
		{
			return renderDelegate;
		}

		public void setDefaultTexture(Texture2D texture)
		{
			defaultTexture = texture;
		}

		public GameObject getGameObject()
		{
			return gameObject;
		}

		public Texture2D getDefaultTexture()
		{
			return defaultTexture;
		}

		public bool getApplyToAll()
		{
			return applyToAll;
		}
	}
}
