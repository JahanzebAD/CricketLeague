using UnityEngine;

public class LeftCameraLookAt : Singleton<LeftCameraLookAt>
{
	public bool canLookAt;

	public Transform target;

	public float damping = 6f;

	public float _lerpTime;

	public bool canChangeFov;

	private void LateUpdate()
	{
		if (CONTROLLER.cameraType == 0 && (bool)target && canLookAt)
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

	private void ChangeFOV()
	{
		if (CONTROLLER.cameraType == 0)
		{
			_lerpTime += Time.deltaTime;
			float t = _lerpTime / 4.5f;
			t = Mathf.SmoothStep(0f, 1f, t);
			t = SmootherStep(t);
			t *= t;
			t = t * t * t;
			GetComponent<Camera>().fieldOfView = Mathf.Lerp(60f, 20f, t);
		}
	}

	private float SmootherStep(float t)
	{
		return t * t * t * (t * (6f * t - 15f) + 10f);
	}
}
