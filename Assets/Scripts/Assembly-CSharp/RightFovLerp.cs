using System;
using DG.Tweening;
using UnityEngine;

public class RightFovLerp : Singleton<RightFovLerp>
{
	public GameObject BLC;

	public float FovStart = 60f;

	public float FovEnd = 30f;

	private float angle = 90f;

	private float velo;

	private Vector3 prevPos;

	public float TransitionTime = 3.5f;

	public bool canMove;

	private float _currentFov;

	private float _lerpTime;

	private Camera _camera;

	public Transform target;

	public float damping = 6f;

	private float fov;

	private float limit;

	private Camera cam;

	public Camera mainCam;

	public bool canRotate;

	public bool canLookAt;

	public float fovOffset = 5f;

	public float smoothSpeed = 0.125f;

	public float zoomSpeed = 3f;

	public Vector3 offset;

	public Vector3 start;

	public Vector3 end;

	private Vector3 desiredPosition;

	private Vector3 smoothPosition;

	private float yPos;

	private Vector3 endPos;

	private Vector3 pos;

	private float bouncesAt;

	private Vector3 targetLastPos;

	private Tweener tween;

	public bool boundaryCamEnable;

	public float RotateAngle = 90f;

	private void Start()
	{
		if (CONTROLLER.cameraType == 0)
		{
			BLC.GetComponent<Camera>().enabled = false;
			tween = base.transform.DOMove(target.position, 1f).SetAutoKill(autoKillOnCompletion: false);
			targetLastPos = target.position;
			canMove = true;
			canLookAt = true;
			_camera = GetComponent<Camera>();
			cam = GetComponent<Camera>();
			fov = GetComponent<Camera>().fieldOfView;
			if ((bool)GetComponent<Rigidbody>())
			{
				GetComponent<Rigidbody>().freezeRotation = true;
			}
			limit = 70f;
		}
	}

	private void Update()
	{
		if ((bool)target && boundaryCamEnable && bouncesAt < 70f && DistanceBetweenTwoVector2(Singleton<GroundController>.instance.groundCenterPoint, target.gameObject) > 40f)
		{
			BLC.GetComponent<Camera>().enabled = true;
			mainCam.enabled = false;
		}
	}

	private void resetTween()
	{
		if (CONTROLLER.cameraType == 0 && !canMove)
		{
			tween = base.transform.DOMove(target.position + offset, 3f).SetAutoKill(autoKillOnCompletion: false);
		}
	}

	public void setCameraPosition(Vector3 pos, GameObject target)
	{
		if (CONTROLLER.cameraType == 0)
		{
			offset = Singleton<SmoothLookAtTwo>.instance.transform.position - Singleton<SmoothLookAtTwo>.instance.target.transform.position;
			Singleton<SmoothLookAtTwo>.instance.transform.position = Singleton<SmoothLookAtTwo>.instance.target.position + offset;
			Singleton<SmoothLookAtTwo>.instance.StartFollowTarget();
			pos = new Vector3(pos.x + Mathf.Cos(angle * ((float)Math.PI / 180f)) * 8f, 4f, pos.z + Mathf.Sin(angle * ((float)Math.PI / 180f)) * 8f);
			BLC.transform.position = pos;
			Invoke("SetBoundaryCam", 0.5f);
		}
		else
		{
			offset = Singleton<SmoothLookAtTwo>.instance.transform.position - Singleton<SmoothLookAtTwo>.instance.target.transform.position;
			Singleton<SmoothLookAtTwo>.instance.transform.position = Singleton<SmoothLookAtTwo>.instance.target.position + offset;
			Singleton<SmoothLookAtTwo>.instance.StartFollowTarget();
			pos = new Vector3(pos.x + Mathf.Cos((RotateAngle + angle) * ((float)Math.PI / 180f)) * 8f, 4f, pos.z + Mathf.Sin((RotateAngle + angle) * ((float)Math.PI / 180f)) * 8f);
			BLC.transform.position = pos;
			Invoke("SetBoundaryCam", 0.5f);
		}
	}

	public void setCameraPositionWicket(Vector3 pos, GameObject target)
	{
		if (CONTROLLER.cameraType == 0)
		{
			pos = new Vector3(pos.x - Mathf.Cos((135f + angle) * ((float)Math.PI / 180f)) * 8f, 4f, pos.z - Mathf.Sin((135f + angle) * ((float)Math.PI / 180f)) * 8f);
			BLC.transform.position = pos;
			Invoke("SetBoundaryCam", 0.3f);
			return;
		}
		offset = Singleton<SmoothLookAtTwo>.instance.transform.position - Singleton<SmoothLookAtTwo>.instance.target.transform.position;
		Singleton<SmoothLookAtTwo>.instance.transform.position = Singleton<SmoothLookAtTwo>.instance.target.position + offset;
		Singleton<SmoothLookAtTwo>.instance.StartFollowTarget();
		pos = new Vector3(pos.x + Mathf.Cos((RotateAngle + angle) * ((float)Math.PI / 180f)) * 8f, 4f, pos.z + Mathf.Sin((RotateAngle + angle) * ((float)Math.PI / 180f)) * 8f);
		BLC.transform.position = pos;
		Invoke("SetBoundaryCam", 0.5f);
	}

	public void SetBoundaryCam()
	{
		if (CONTROLLER.cameraType == 0)
		{
			Singleton<MainCameraMovement>.instance.tween.Pause();
			Singleton<MainCameraMovement>.instance.reverseCamTween.Pause();
			boundaryCamEnable = true;
		}
		else
		{
			boundaryCamEnable = true;
		}
	}

	public void CallDisableBoundaryCam()
	{
	}

	private void DisableBoundaryCam()
	{
		if (CONTROLLER.cameraType == 0)
		{
			boundaryCamEnable = false;
			BLC.GetComponent<Camera>().enabled = false;
		}
	}

	private void StopCameraLookAt()
	{
		if (CONTROLLER.cameraType == 0)
		{
			Singleton<RightCameraLookAt>.instance.canLookAt = false;
		}
	}

	private void PauseTween()
	{
		if (CONTROLLER.cameraType == 0)
		{
			tween = base.transform.DOMove(target.position, 2f).SetAutoKill(autoKillOnCompletion: false);
		}
	}

	public float DistanceBetweenTwoVector2(GameObject go1, GameObject go2)
	{
		float num = go1.transform.position.x - go2.transform.position.x;
		float num2 = go1.transform.position.z - go2.transform.position.z;
		return Mathf.Sqrt(num * num + num2 * num2);
	}

	public void setBallAngle(float ballAngle)
	{
	}

	public void setBallFirstBounce(float bounceDistance)
	{
		bouncesAt = bounceDistance;
	}

	private void ChangeFOV()
	{
		if (CONTROLLER.cameraType == 0)
		{
			_lerpTime += Time.deltaTime;
			float t = _lerpTime / 5f;
			t = Mathf.SmoothStep(0f, 1f, t);
			t = SmootherStep(t);
			t *= t;
			t = t * t * t;
			_currentFov = Mathf.Lerp(50f, 30f, t);
			GetComponent<Camera>().fieldOfView = _currentFov;
		}
	}

	public void Reset()
	{
		if (CONTROLLER.cameraType == 0)
		{
			tween.Kill();
			tween = base.transform.DOMove(new Vector3(0f, 0f, 8.8f), 1f).SetAutoKill(autoKillOnCompletion: false);
			endPos = new Vector3(30f, 10f, 30f);
			BLC.GetComponent<Camera>().enabled = false;
			boundaryCamEnable = false;
			FovStart = 50f;
			FovEnd = 25f;
			_lerpTime = 0f;
			Singleton<SmoothLookAtTwo>.instance.Reset();
			Singleton<RightCameraLookAt>.instance._lerpTime = 0f;
			canMove = true;
			Singleton<RightCameraLookAt>.instance.canLookAt = true;
		}
		else
		{
			BLC.GetComponent<Camera>().enabled = false;
			boundaryCamEnable = false;
		}
	}

	private float SmootherStep(float t)
	{
		return t * t * t * (t * (6f * t - 15f) + 10f);
	}
}
