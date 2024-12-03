using GreedyGame.Runtime;
using UnityEngine;

public class GGRenderer : MonoBehaviour
{
	public Texture2D texture;

	public string unitId;

	private void Start()
	{
		Debug.Log("Calling register : ");
		GreedyGameAgent.Instance.registerGameObject(base.gameObject, texture, unitId, applyToAll: true);
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
		Debug.Log("Calling unregister : ");
		GreedyGameAgent.Instance.unregisterGameObject(base.gameObject);
	}
}
