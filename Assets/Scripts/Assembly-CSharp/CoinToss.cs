using DG.Tweening;
using UnityEngine;

public class CoinToss : MonoBehaviour
{
	public Transform target;

	public PathType pathType = PathType.CatmullRom;

	public Vector3[] waypoints = new Vector3[5]
	{
		new Vector3(4f, 2f, 6f),
		new Vector3(8f, 6f, 14f),
		new Vector3(4f, 6f, 14f),
		new Vector3(0f, 6f, 6f),
		new Vector3(-3f, 0f, 0f)
	};

	private void Start()
	{
		Tween t = target.DOPath(waypoints, 4f, pathType).SetOptions(closePath: true).SetLookAt(0.001f);
		t.SetEase(Ease.Linear).SetLoops(-1);
	}
}
