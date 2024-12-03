using UnityEngine;

namespace GreedyGame.Platform.Dummy
{
	public class EngineRender : MonoBehaviour
	{
		private static EngineRender instance = null;

		private string _float_id = null;

		private float _float_x = 0f;

		private float _float_y = 0f;

		private GUIContent content = null;

		private Texture button = null;

		public static EngineRender Instance
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
					instance = gameObject.AddComponent<EngineRender>();
					Object.DontDestroyOnLoad(gameObject);
					Debug.Log($"[GreedyGame] EngineRender Instance, gameObject = {instance.gameObject.name}");
				}
				return instance;
			}
		}

		public void drawHeadAd(string unit, int x, int y)
		{
			_float_id = unit;
			if (x < 0)
			{
				_float_x = 0f;
			}
			else
			{
				_float_x = (float)Screen.width * ((float)x / 100f);
			}
			if (y < 0)
			{
				_float_y = 0f;
			}
			else
			{
				_float_y = (float)Screen.width * ((float)y / 100f);
			}
			content = new GUIContent();
			button = createBox(70, 70, Color.gray);
			content.image = button;
			content.text = unit;
			Debug.Log($"[GreedyGame] DrawHeadAd with id = {unit}, x = {x}, y = {y}");
		}

		private void OnGUI()
		{
			if (Application.isEditor && !string.IsNullOrEmpty(_float_id))
			{
				GUI.skin.button.normal.background = (Texture2D)content.image;
				GUI.skin.button.hover.background = (Texture2D)content.image;
				GUI.skin.button.active.background = (Texture2D)content.image;
				if (GUI.Button(new Rect(_float_x, _float_y, 70f, 70f), content))
				{
					Debug.LogWarning("[GreedyGame] Float unit will only work on device");
				}
			}
		}

		private Texture2D createBox(int w, int h, Color c)
		{
			Texture2D texture2D = new Texture2D(w, h);
			for (int i = 0; i < w; i++)
			{
				for (int j = 0; j < h; j++)
				{
					texture2D.SetPixel(i, j, c);
				}
			}
			texture2D.Apply();
			return texture2D;
		}
	}
}
