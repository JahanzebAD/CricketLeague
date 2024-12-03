using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
	public Transform origin;

	public float zoom;

	private float zoomMin = -5f;

	private float zoomMax = 5f;

	private float seekTime = 1f;

	private bool smoothZoomIn;

	private Vector3 defaultLocalPosition;

	private Transform thisTransform;

	private float currentZoom;

	private float targetZoom;

	private float zoomVelocity;

	private void Start()
	{
		thisTransform = base.transform;
		defaultLocalPosition = thisTransform.localPosition;
		currentZoom = zoom;
	}

	private void Update()
	{
		zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
		int layerMask = -261;
		Vector3 position = origin.position;
		Vector3 position2 = defaultLocalPosition + thisTransform.parent.InverseTransformDirection(thisTransform.forward * zoom);
		Vector3 end = thisTransform.parent.TransformPoint(position2);
		if (Physics.Linecast(position, end, out var hitInfo, layerMask))
		{
			Vector3 vector = hitInfo.point + thisTransform.TransformDirection(Vector3.forward);
			targetZoom = (vector - thisTransform.parent.TransformPoint(defaultLocalPosition)).magnitude;
		}
		else
		{
			targetZoom = zoom;
		}
		targetZoom = Mathf.Clamp(targetZoom, zoomMin, zoomMax);
		if (!smoothZoomIn && targetZoom - currentZoom > 0f)
		{
			currentZoom = targetZoom;
		}
		else
		{
			currentZoom = Mathf.SmoothDamp(currentZoom, targetZoom, ref zoomVelocity, seekTime);
		}
		position2 = defaultLocalPosition + thisTransform.parent.InverseTransformDirection(thisTransform.forward * currentZoom);
		thisTransform.localPosition = position2;
	}
}
