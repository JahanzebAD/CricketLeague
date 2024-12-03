using UnityEngine;

[AddComponentMenu("Camera-Control/Smooth Look At")]
public class SmoothLookAt : MonoBehaviour
{
	public static bool canLookAt;

	public Transform target;

	public float damping = 6f;

	public bool smooth = true;

	private void LateUpdate()
	{
		if (!Singleton<GroundController>.instance.ballOnboundaryLine)
		{
			Quaternion b = Quaternion.LookRotation(target.position - base.transform.position);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * damping);
		}
	}

	private void Start()
	{
		canLookAt = true;
		if ((bool)GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
	}
}
