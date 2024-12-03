using UnityEngine;

public class movement : MonoBehaviour
{
	public float velocity;

	public float xMax;

	public float zMax;

	public float xMin;

	public float zMin;

	private float x;

	private float z;

	private float time;

	private float angle;

	private void Start()
	{
		x = Random.Range(0f - velocity, velocity);
		z = Random.Range(0f - velocity, velocity);
		angle = Mathf.Atan2(x, z) * 57.29579f + 90f;
		base.transform.localRotation = Quaternion.Euler(0f, angle, 0f);
	}

	private void Update()
	{
		time += Time.deltaTime;
		if (base.transform.localPosition.x > xMax)
		{
			x = Random.Range(0f - velocity, velocity);
			z = Random.Range(0f - velocity, velocity);
			angle = Mathf.Atan2(x, z) * 57.29579f + 90f;
			base.transform.localRotation = Quaternion.Euler(0f, angle, 0f);
			time = 0f;
		}
		if (base.transform.localPosition.x < xMin)
		{
			x = Random.Range(0f - velocity, velocity);
			z = Random.Range(0f - velocity, velocity);
			angle = Mathf.Atan2(x, z) * 57.29579f + 90f;
			base.transform.localRotation = Quaternion.Euler(0f, angle, 0f);
			time = 0f;
		}
		if (base.transform.localPosition.z > zMax)
		{
			x = Random.Range(0f - velocity, velocity);
			z = Random.Range(0f - velocity, velocity);
			angle = Mathf.Atan2(x, z) * 57.29579f + 90f;
			base.transform.localRotation = Quaternion.Euler(0f, angle, 0f);
			time = 0f;
		}
		if (base.transform.localPosition.z < zMin)
		{
			x = Random.Range(0f - velocity, velocity);
			z = Random.Range(0f - velocity, velocity);
			angle = Mathf.Atan2(x, z) * 57.29579f + 90f;
			base.transform.localRotation = Quaternion.Euler(0f, angle, 0f);
			time = 0f;
		}
		base.transform.localPosition = new Vector3(base.transform.localPosition.x + x, base.transform.localPosition.y, base.transform.localPosition.z + z);
	}
}
