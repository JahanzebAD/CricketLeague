using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class LeftFovLerp : Singleton<LeftFovLerp>
{
	public float FovStart = 60f;

	private float angle = 90f;

	public GameObject BLC;

	public float FovEnd = 30f;

	public float TransitionTime = 3.5f;

	public bool canMove;

	private float _currentFov;

	private float _lerpTime;

	private float lerpTime;

	private Camera _camera;

	public Transform target;

	public float damping = 6f;

	private float fov;

	private Camera cam;

	public CustomSmoothLookAt lineCamera;

	public Camera mainCam;

	public bool canRotate;

	public float fovOffset = 5f;

	public float smoothSpeed = 0.125f;

	public float zoomSpeed = 3f;

	public Vector3 offset;

	public Vector3 rOffset;

	public Vector3 start;

	public Vector3 end;

	private Vector3 desiredPosition;

	private Vector3 smoothPosition;

	private float limit;

	private float yPos;

	private float zPos;

	private Vector3 endPos;

	private Vector3 pos;

	public Camera boundaryCam;

	private float bouncesAt;

	private Vector3 targetLastPos;

	private Vector3 velocity;

	private Vector3 direction;

	private Vector3 pos1;

	private Vector3 pos2;

	private Tweener tween;

	private Tweener tween2;

	public bool canLookAt;

	public bool canMoveDown;

	public bool boundaryCamEnable;

	public float RotateAngle = 270f;

	private void Start()
	{
		if (CONTROLLER.cameraType == 0)
		{
			boundaryCam.enabled = false;
			tween = base.transform.DOMove(target.position, 1f).SetAutoKill(autoKillOnCompletion: false);
			targetLastPos = target.position;
			canMoveDown = false;
			rOffset = new Vector3(90f, 0f, 0f);
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
			tween = base.transform.DOMove(target.position + offset, 1f).SetAutoKill(autoKillOnCompletion: false);
		}
	}

	private void StopCameraLookAt()
	{
		if (CONTROLLER.cameraType == 0)
		{
			Singleton<LeftCameraLookAt>.instance.canLookAt = false;
		}
	}

	private void MoverCameraDown()
	{
		if (CONTROLLER.cameraType == 0)
		{
			Sequence s = DOTween.Sequence();
			s.Append(base.transform.DOMove(new Vector3(base.transform.position.x, 10f, base.transform.position.z), 4f));
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

	public void setCameraPosition(Vector3 pos, GameObject target)
	{
		if (!Singleton<GroundController>.instance.replayMode)
		{
			offset = lineCamera.transform.position - lineCamera.target.transform.position;
			lineCamera.transform.position = lineCamera.target.position + offset;
			lineCamera.StartFollowTarget();
			pos = new Vector3(pos.x + Mathf.Cos((RotateAngle + angle) * ((float)Math.PI / 180f)) * 8f, 4f, pos.z + Mathf.Sin((RotateAngle + angle) * ((float)Math.PI / 180f)) * 8f);
			BLC.transform.position = pos;
			Invoke("EnableBoundaryCam", 0.5f);
		}
	}

	public void setCameraPositionWicket(Vector3 pos, GameObject target)
	{
		if (CONTROLLER.cameraType == 0)
		{
			pos = new Vector3(pos.x - Mathf.Cos((135f + angle) * ((float)Math.PI / 180f)) * 8f, 4f, pos.z - Mathf.Sin((135f + angle) * ((float)Math.PI / 180f)) * 8f);
			BLC.transform.position = pos;
			Invoke("EnableBoundaryCam", 0.3f);
			return;
		}
		offset = lineCamera.transform.position - lineCamera.target.transform.position;
		lineCamera.transform.position = lineCamera.target.position + offset;
		lineCamera.StartFollowTarget();
		pos = new Vector3(pos.x + Mathf.Cos((RotateAngle + angle) * ((float)Math.PI / 180f)) * 8f, 4f, pos.z + Mathf.Sin((RotateAngle + angle) * ((float)Math.PI / 180f)) * 8f);
		BLC.transform.position = pos;
		Invoke("EnableBoundaryCam", 0.5f);
	}

	public void EnableBoundaryCam()
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

	public void setBallFirstBounce(float bounceDistance)
	{
		bouncesAt = bounceDistance;
	}

	private void ChangeFOV()
	{
		if (CONTROLLER.cameraType == 0 && Singleton<LeftCameraLookAt>.instance.canLookAt)
		{
			_lerpTime += Time.deltaTime;
			float t = _lerpTime / 5f;
			t = Mathf.SmoothStep(0f, 1f, t);
			t = SmootherStep(t);
			_currentFov = Mathf.Lerp(60f, 40f, t);
			GetComponent<Camera>().fieldOfView = _currentFov;
		}
	}

	public void Reset()
	{
		if (CONTROLLER.cameraType == 0)
		{
			tween.Kill();
			boundaryCamEnable = false;
			tween = base.transform.DOMove(new Vector3(0f, 0f, 8.8f), 1f).SetAutoKill(autoKillOnCompletion: false);
			endPos = new Vector3(30f, 10f, 30f);
			FovStart = 50f;
			boundaryCam.enabled = false;
			Singleton<LeftCameraLookAt>.instance.canChangeFov = false;
			FovEnd = 25f;
			_lerpTime = 0f;
			Singleton<CustomSmoothLookAt>.instance.Reset();
			Singleton<LeftCameraLookAt>.instance._lerpTime = 0f;
			canMove = true;
			canMoveDown = false;
			Singleton<LeftCameraLookAt>.instance.canLookAt = true;
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

	private IEnumerator delay()
	{
		if (CONTROLLER.cameraType == 0)
		{
			yield return new WaitForSecondsRealtime(0.5f);
			canMove = true;
			yield return 0;
		}
	}
}
