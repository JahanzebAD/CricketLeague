using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RollABall : MonoBehaviour
{
	private Vector3 tilt = Vector3.zero;

	private float speed;

	private float circ;

	private Vector3 previousPosition;

	private void Start()
	{
		circ = (float)Math.PI * 2f * GetComponent<Collider>().bounds.extents.x;
		previousPosition = base.transform.position;
	}

	private void Update()
	{
		tilt.x = 0f - Input.acceleration.y;
		tilt.z = Input.acceleration.x;
		GetComponent<Rigidbody>().AddForce(tilt * speed * Time.deltaTime);
	}

	private void LateUpdate()
	{
		Vector3 vector = base.transform.position - previousPosition;
		vector = new Vector3(vector.z, 0f, 0f - vector.x);
		base.transform.Rotate(vector / circ * 360f, Space.World);
		previousPosition = base.transform.position;
	}
}
