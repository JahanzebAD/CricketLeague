using GreedyGame.Runtime;
using UnityEngine;

public class FloatUnitLoader : MonoBehaviour
{
	public string FloatUnit;

	private GreedyGameAgent greedyGameAgent;

	private void Awake()
	{
		Debug.Log($"FloatUnitLoader - awake {FloatUnit}");
	}

	private void Start()
	{
		//greedyGameAgent.fetchFloatUnit(FloatUnit);
		//greedyGameAgent.removeFloatUnit(FloatUnit);
		//greedyGameAgent.fetchFloatUnit(FloatUnit);
	}

	private void OnDestroy()
	{
		//greedyGameAgent.removeFloatUnit(FloatUnit);
	}
}
