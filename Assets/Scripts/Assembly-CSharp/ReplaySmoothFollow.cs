using UnityEngine;

public class ReplaySmoothFollow : MonoBehaviour
{
	public Transform target;

	public float distance = 10f;

	public float height = 5f;

	public float heightDamping = 2f;

	public float rotationDamping = 3f;

	private float wantedRotationAngle;

	private float wantedHeight;

	private float currentRotationAngle;

	private float currentHeight;

	private Quaternion currentRotation;

	private GroundController groundControllerScript;

	protected void Awake()
	{
		groundControllerScript = GameObject.Find("GroundController").GetComponent<GroundController>();
	}

	private void LateUpdate()
	{
		if ((bool)target)
		{
			wantedRotationAngle = target.transform.eulerAngles.y;
			wantedHeight = target.transform.position.y + height;
			if (groundControllerScript.limitReplayCameraHeight && wantedHeight > 6f)
			{
				wantedHeight = 6f;
			}
			currentRotationAngle = base.transform.eulerAngles.y;
			currentHeight = base.transform.position.y;
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);
			base.transform.position = target.transform.position;
			base.transform.position -= currentRotation * Vector3.forward * distance;
			base.transform.position = new Vector3(base.transform.position.x, currentHeight, base.transform.position.z);
			base.transform.LookAt(target);
		}
	}
}
