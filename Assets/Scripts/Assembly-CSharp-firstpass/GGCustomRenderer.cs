using GreedyGame.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class GGCustomRenderer : MonoBehaviour
{
	private RawImage rawImage;

	public Texture defaultTexture;

	public string unitId;

	private void Start()
	{
		if (CONTROLLER.PlayModeSelected == 6 || CONTROLLER.AdsPurchased != 0 || CONTROLLER.isGreedyGameEnabled != 1)
		{
			return;
		}
		GreedyGameAgent.Instance.registerGameObject(base.gameObject, defaultTexture as Texture2D, unitId, delegate(string unitID, Texture2D brandedTexture, bool isBrandedTexture)
		{
			if (brandedTexture != null)
			{
				Debug.Log("UnityGG branded texture found");
				rawImage = GetComponent<RawImage>();
				if (rawImage != null)
				{
					rawImage.texture = brandedTexture;
				}
			}
			if (!isBrandedTexture)
			{
				Debug.Log("UnityGG branded texture not found");
			}
		});
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
		GreedyGameAgent.Instance.unregisterGameObject(base.gameObject);
	}
}
