using UnityEngine;

public class AutoFocus : MonoBehaviour
{
	public GameObject centerPoint;

	public GameObject ball;

	public Camera cam;

	[SerializeField]
	private float maxDistance = 60f;

	[SerializeField]
	private float startFov = 48f;

	[SerializeField]
	private float dampingFactor = 1.5f;

	private void Update()
	{
		if (CONTROLLER.cameraType == 1 && Singleton<GroundController>.instance.action > 2 && DistanceBetweenTwoGameObjects(ball, centerPoint) < maxDistance)
		{
			cam.fieldOfView = startFov - DistanceBetweenTwoGameObjects(ball, centerPoint) / dampingFactor;
		}
	}

	private float DistanceBetweenTwoGameObjects(GameObject go1, GameObject go2)
	{
		return Vector3.Distance(go1.transform.position, go2.transform.position);
	}
}
