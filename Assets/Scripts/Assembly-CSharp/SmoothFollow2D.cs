using UnityEngine;

public class SmoothFollow2D : MonoBehaviour
{
	private Transform target;

	private float smoothTime = 0.3f;

	private Transform thisTransform;

	private Vector2 velocity;

	private void Start()
	{
		thisTransform = base.transform;
	}

	private void Update()
	{
		thisTransform.position = new Vector3(Mathf.SmoothDamp(thisTransform.position.x, target.position.x, ref velocity.x, smoothTime), 0f, 0f);
		thisTransform.position = new Vector3(0f, Mathf.SmoothDamp(thisTransform.position.y, target.position.y, ref velocity.y, smoothTime), 0f);
	}
}
