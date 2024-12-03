using System;
using DG.Tweening;
using UnityEngine;

public class MainCameraMovement : Singleton<MainCameraMovement>
{
	public GameObject BLC;

	public GameObject DummyCam;

	public float FovStart = 60f;

	public float FovEnd = 30f;

	private float angle = 90f;

	private float shotAngle;

	private float velo;

	private Vector3 prevPos;

	private bool firstTimeCalled;

	private float tweenTime = 5f;

	public float TransitionTime = 3.5f;

	public bool canMove;

	private float _currentFov;

	private float _lerpTime;

	private Camera _camera;

	public Transform target;

	public Transform camTarget;

	public float damping = 6f;

	private float fov;

	private float limit;

	private Camera cam;

	public bool canRotate;

	public bool canLookAt;

	public float fovOffset = 5f;

	public float smoothSpeed = 0.125f;

	public float zoomSpeed = 3f;

	public Vector3 offset;

	public Vector3 start;

	public Vector3 end;

	public Vector3 BLCstart;

	private Vector3 desiredPosition;

	private Vector3 smoothPosition;

	private float yPos;

	private float offsetAngle;

	private Vector3 endPos;

	private Vector3 pos;

	private float bouncesAt;

	private Vector3 targetLastPos;

	public Tweener tween;

	public Tweener reverseCamTween;

	public bool boundaryCamEnable;

	private void Start()
	{
		if (CONTROLLER.cameraType == 0)
		{
			firstTimeCalled = true;
			BLC.GetComponent<Camera>().enabled = false;
			start = base.transform.position;
			BLCstart = new Vector3(0f, 6.8f, 8f);
			targetLastPos = target.position;
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
		if (CONTROLLER.cameraType != 0)
		{
			return;
		}
		Vector3 vector = (base.transform.position - prevPos) / Time.deltaTime;
		prevPos = base.transform.position;
		velo = vector.magnitude;
		if (!target)
		{
			return;
		}
		if (bouncesAt > 40f)
		{
			tweenTime = 5f;
		}
		else
		{
			tweenTime = 5f;
		}
		if (Singleton<GroundController>.instance.action <= 2 || !canMove)
		{
			return;
		}
		if (!Singleton<GroundController>.instance.ballOnboundaryLine)
		{
			if (shotAngle > 220f && shotAngle <= 320f)
			{
				GetComponent<Camera>().enabled = false;
				BLC.GetComponent<Camera>().enabled = true;
			}
			if (bouncesAt > 45f && GetComponent<Camera>().fieldOfView > 20f)
			{
				GetComponent<Camera>().fieldOfView -= 0.05f;
			}
			if (DistanceBetweenTwoVector2(Singleton<GroundController>.instance.groundCenterPoint, target.gameObject) > 20f && BLC.GetComponent<Camera>().fieldOfView > 20f)
			{
				BLC.GetComponent<Camera>().fieldOfView -= 0.05f;
			}
			Vector3 forward = target.position - base.transform.position;
			Quaternion b = Quaternion.LookRotation(forward);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * damping);
		}
		else
		{
			tween.Pause();
			reverseCamTween.Pause();
		}
	}

	public void StartFollowTween()
	{
		if (CONTROLLER.cameraType == 0 && !(Singleton<GroundController>.instance.shotPlayed == "bt6Defense") && !(Singleton<GroundController>.instance.shotPlayed == "backFootDefenseHighBall") && firstTimeCalled)
		{
			if ((shotAngle < 270f && shotAngle > 90f && CONTROLLER.StrikerHand == "right") || ((shotAngle < 90f || shotAngle > 270f) && CONTROLLER.StrikerHand == "left"))
			{
				offsetAngle = UnityEngine.Random.Range(-30f, -15f);
			}
			else
			{
				offsetAngle = UnityEngine.Random.Range(15f, 30f);
			}
			tween = base.transform.DOMove(new Vector3(Mathf.Cos((angle + offsetAngle) * ((float)Math.PI / 180f)) * 40f, 4f, Mathf.Sin((angle + offsetAngle) * ((float)Math.PI / 180f)) * 40f), tweenTime).SetAutoKill(autoKillOnCompletion: false);
			reverseCamTween = BLC.transform.DOMove(new Vector3(Mathf.Cos(angle * ((float)Math.PI / 180f)) * 30f, 4f, Mathf.Sin(angle * ((float)Math.PI / 180f)) * 30f), 6f).SetAutoKill(autoKillOnCompletion: false);
			setDummyCameraPosition();
			canMove = true;
			firstTimeCalled = false;
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
			Singleton<SmoothLookAtTwo>.instance.target = this.target;
			pos = new Vector3(pos.x + Mathf.Cos(angle * ((float)Math.PI / 180f)) * 8f, 4f, pos.z + Mathf.Sin(angle * ((float)Math.PI / 180f)) * 8f);
			BLC.transform.position = pos;
			boundaryCamEnable = true;
			ReverseTween();
		}
	}

	public void ReverseTween()
	{
		if (CONTROLLER.cameraType == 0)
		{
			tween.SmoothRewind();
			tween.Play();
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
			tween = base.transform.DOMove(target.position, 2f);
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
		if (CONTROLLER.cameraType == 0)
		{
			shotAngle = ballAngle;
			if (Singleton<GroundController>.instance.currentBatsmanHand == "right")
			{
				angle = ballAngle;
			}
			else
			{
				angle = 180f - ballAngle;
			}
		}
	}

	public void setDummyCameraPosition()
	{
		if (CONTROLLER.cameraType == 0)
		{
			Singleton<DummyScript>.instance.StartFollowTarget();
			DummyCam.transform.position = new Vector3(target.position.x + Mathf.Cos(angle * ((float)Math.PI / 180f)) * 8f, 4f, target.position.z + Mathf.Sin(angle * ((float)Math.PI / 180f)) * 8f);
		}
	}

	public void MoveCamForward()
	{
		if (CONTROLLER.cameraType == 0)
		{
			tween = base.transform.DOMove(camTarget.position, 7f).SetAutoKill(autoKillOnCompletion: false);
		}
	}

	public void setBallFirstBounce(float bounceDistance)
	{
		if (CONTROLLER.cameraType == 0)
		{
			bouncesAt = bounceDistance;
		}
	}

	private void ChangeFOV()
	{
		if (CONTROLLER.cameraType == 0)
		{
			_lerpTime += Time.deltaTime;
			float t = _lerpTime / TransitionTime;
			t = Mathf.SmoothStep(0f, 1f, t);
			t = SmootherStep(t);
			_currentFov = Mathf.Lerp(GetComponent<Camera>().fieldOfView, FovEnd, t);
			GetComponent<Camera>().fieldOfView = _currentFov;
		}
	}

	public void Reset()
	{
		if (CONTROLLER.cameraType == 0)
		{
			firstTimeCalled = true;
			tween.Complete();
			tween.Kill();
			reverseCamTween.Complete();
			reverseCamTween.Kill();
			Singleton<DummyScript>.instance.Reset();
			base.transform.position = start;
			BLC.transform.position = BLCstart;
			BLC.GetComponent<Camera>().fieldOfView = 40f;
			_lerpTime = 0f;
		}
	}

	private float SmootherStep(float t)
	{
		return t * t * t * (t * (6f * t - 15f) + 10f);
	}
}
