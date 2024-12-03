using UnityEngine;

public class BallFollow : MonoBehaviour
{
	public Transform target;

	public Vector3 offset;

	public float distanceDamp = 10f;

	public float rotationDamp = 10f;

	private Transform thisTransform;

	private bool canMoce;

	public Vector3 velocity = Vector3.one;

	private void Awake()
	{
		thisTransform = base.transform;
		canMoce = true;
	}

	private void LateUpdate()
	{
		if (Singleton<GroundController>.instance.action > 2 && Singleton<AnimationScreen>.instance != null && Singleton<GroundController>.instance.leftSideCamera.enabled && canMoce)
		{
			SmoothFollow();
		}
	}

	private void SmoothFollow()
	{
		canMoce = false;
	}
}
