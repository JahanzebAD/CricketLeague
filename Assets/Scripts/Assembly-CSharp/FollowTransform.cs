using UnityEngine;

public class FollowTransform : MonoBehaviour
{
	private Transform targetTransform;

	private bool faceForward;

	private Transform thisTransform;

	private void Start()
	{
		thisTransform = base.transform;
	}

	private void Update()
	{
		thisTransform.position = targetTransform.position;
		if (faceForward)
		{
			thisTransform.forward = targetTransform.forward;
		}
	}
}
