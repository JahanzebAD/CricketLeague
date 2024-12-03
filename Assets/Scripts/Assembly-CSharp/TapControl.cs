using System;
using UnityEngine;
using UnityEngine.UI;

public class TapControl : MonoBehaviour
{
	private GameObject cameraObject;

	private Transform cameraPivot;

	private Image jumpButton;

	private float speed;

	private float jumpSpeed;

	private float inAirMultiplier = 0.25f;

	private float minimumDistanceToMove = 1f;

	private float minimumTimeUntilMove = 0.25f;

	private bool zoomEnabled;

	private float zoomEpsilon;

	private float zoomRate;

	public bool rotateEnabled;

	private float rotateEpsilon = 1f;

	private ZoomCamera zoomCamera;

	private Camera cam;

	private Transform thisTransform;

	private CharacterController character;

	private Vector3 targetLocation;

	private bool moving;

	private float rotationTarget;

	private float rotationVelocity;

	private Vector3 velocity;

	private ControlState state;

	private int[] fingerDown = new int[2];

	private Vector2[] fingerDownPosition = new Vector2[2];

	private int[] fingerDownFrame = new int[2];

	private float firstTouchTime;

	private void Start()
	{
		thisTransform = base.transform;
		zoomCamera = cameraObject.GetComponent<ZoomCamera>();
		cam = cameraObject.GetComponent<Camera>();
		character = GetComponent<CharacterController>();
		ResetControlState();
		GameObject gameObject = GameObject.Find("PlayerSpawn");
		if ((bool)gameObject)
		{
			thisTransform.position = gameObject.transform.position;
		}
	}

	private void OnEndGame()
	{
		base.enabled = false;
	}

	private void FaceMovementDirection()
	{
		Vector3 vector = character.velocity;
		vector = new Vector3(0f, 0f, 0f);
		if (vector.magnitude > 0.1f)
		{
			thisTransform.forward = vector.normalized;
		}
	}

	private void CameraControl(Touch touch0, Touch touch1)
	{
		if (rotateEnabled && state == ControlState.RotatingCamera)
		{
			Vector2 vector = touch1.position - touch0.position;
			Vector2 lhs = vector / vector.magnitude;
			Vector2 vector2 = touch1.position - touch1.deltaPosition - (touch0.position - touch0.deltaPosition);
			Vector2 rhs = vector2 / vector2.magnitude;
			float num = Vector2.Dot(lhs, rhs);
			if (num < 1f)
			{
				Vector3 lhs2 = new Vector3(vector.x, vector.y, 0f);
				Vector3 rhs2 = new Vector3(vector2.x, vector2.y, 0f);
				float z = Vector3.Cross(lhs2, rhs2).normalized.z;
				float num2 = Mathf.Acos(num);
				rotationTarget += num2 * 57.29578f * z;
				if (rotationTarget < 0f)
				{
					rotationTarget += 360f;
				}
				else if (rotationTarget >= 360f)
				{
					rotationTarget -= 360f;
				}
			}
		}
		else if (zoomEnabled && state == ControlState.ZoomingCamera)
		{
			float magnitude = (touch1.position - touch0.position).magnitude;
			float magnitude2 = (touch1.position - touch1.deltaPosition - (touch0.position - touch0.deltaPosition)).magnitude;
			float num3 = magnitude - magnitude2;
			zoomCamera.zoom += num3 * zoomRate * Time.deltaTime;
		}
	}

	private void CharacterControl()
	{
		int touchCount = Input.touchCount;
		if (touchCount == 1 && state == ControlState.MovingCharacter)
		{
			Touch touch = Input.GetTouch(0);
			if (character.isGrounded /*&& jumpButton.HitTest(touch.position)*/)
			{
				velocity = character.velocity;
				velocity.y = jumpSpeed;
			}
			else if (/*!jumpButton.HitTest(touch.position) &&*/ touch.phase != 0)
			{
				Vector3 pos = new Vector3(touch.position.x, touch.position.y);
				Ray ray = cam.ScreenPointToRay(pos);
				if (Physics.Raycast(ray, out var hitInfo))
				{
					float magnitude = (base.transform.position - hitInfo.point).magnitude;
					if (magnitude > minimumDistanceToMove)
					{
						targetLocation = hitInfo.point;
					}
					moving = true;
				}
			}
		}
		Vector3 motion = Vector3.zero;
		if (moving)
		{
			motion = targetLocation - thisTransform.position;
			motion.y = 0f;
			float magnitude2 = motion.magnitude;
			if (magnitude2 < 1f)
			{
				moving = false;
			}
			else
			{
				motion = motion.normalized * speed;
			}
		}
		if (!character.isGrounded)
		{
			velocity.y += Physics.gravity.y * Time.deltaTime;
			motion.x *= inAirMultiplier;
			motion.z *= inAirMultiplier;
		}
		motion += velocity;
		motion += Physics.gravity;
		motion *= Time.deltaTime;
		character.Move(motion);
		if (character.isGrounded)
		{
			velocity = Vector3.zero;
		}
		FaceMovementDirection();
	}

	private void ResetControlState()
	{
		state = ControlState.WaitingForFirstTouch;
		fingerDown[0] = -1;
		fingerDown[1] = -1;
	}

	private void Update()
	{
		int touchCount = Input.touchCount;
		if (touchCount == 0)
		{
			ResetControlState();
		}
		else
		{
			Touch[] touches = Input.touches;
			Touch touch = default(Touch);
			Touch touch2 = default(Touch);
			bool flag = false;
			bool flag2 = false;
			if (state == ControlState.WaitingForFirstTouch)
			{
				for (int i = 0; i < touchCount; i++)
				{
					Touch touch3 = touches[i];
					if (touch3.phase != TouchPhase.Ended && touch3.phase != TouchPhase.Canceled)
					{
						state = ControlState.WaitingForSecondTouch;
						firstTouchTime = Time.time;
						fingerDown[0] = touch3.fingerId;
						ref Vector2 reference = ref fingerDownPosition[0];
						reference = touch3.position;
						fingerDownFrame[0] = Time.frameCount;
						break;
					}
				}
			}
			if (state == ControlState.WaitingForSecondTouch)
			{
				for (int i = 0; i < touchCount; i++)
				{
					Touch touch3 = touches[i];
					if (touch3.phase == TouchPhase.Canceled)
					{
						continue;
					}
					if (touchCount >= 2 && touch3.fingerId != fingerDown[0])
					{
						state = ControlState.WaitingForMovement;
						fingerDown[1] = touch3.fingerId;
						ref Vector2 reference2 = ref fingerDownPosition[1];
						reference2 = touch3.position;
						fingerDownFrame[1] = Time.frameCount;
						break;
					}
					if (touchCount == 1)
					{
						Vector2 vector = touch3.position - fingerDownPosition[0];
						if (touch3.fingerId == fingerDown[0] && (Time.time > firstTouchTime + minimumTimeUntilMove || touch3.phase == TouchPhase.Ended))
						{
							state = ControlState.MovingCharacter;
							break;
						}
					}
				}
			}
			if (state == ControlState.WaitingForMovement)
			{
				for (int i = 0; i < touchCount; i++)
				{
					Touch touch3 = touches[i];
					if (touch3.phase == TouchPhase.Began)
					{
						if (touch3.fingerId == fingerDown[0] && fingerDownFrame[0] == Time.frameCount)
						{
							touch = touch3;
							flag = true;
						}
						else if (touch3.fingerId != fingerDown[0] && touch3.fingerId != fingerDown[1])
						{
							fingerDown[1] = touch3.fingerId;
							touch2 = touch3;
							flag2 = true;
						}
					}
					if (touch3.phase == TouchPhase.Moved || touch3.phase == TouchPhase.Stationary || touch3.phase == TouchPhase.Ended)
					{
						if (touch3.fingerId == fingerDown[0])
						{
							touch = touch3;
							flag = true;
						}
						else if (touch3.fingerId == fingerDown[1])
						{
							touch2 = touch3;
							flag2 = true;
						}
					}
				}
				if (flag)
				{
					if (flag2)
					{
						Vector2 vector2 = fingerDownPosition[1] - fingerDownPosition[0];
						Vector2 vector3 = touch2.position - touch.position;
						Vector2 lhs = vector2 / vector2.magnitude;
						Vector2 rhs = vector3 / vector3.magnitude;
						float num = Vector2.Dot(lhs, rhs);
						if (num < 1f)
						{
							float num2 = Mathf.Acos(num);
							if (num2 > rotateEpsilon * ((float)Math.PI / 180f))
							{
								state = ControlState.RotatingCamera;
							}
						}
						if (state == ControlState.WaitingForMovement)
						{
							float f = vector2.magnitude - vector3.magnitude;
							if (Mathf.Abs(f) > zoomEpsilon)
							{
								state = ControlState.ZoomingCamera;
							}
						}
					}
				}
				else
				{
					state = ControlState.WaitingForNoFingers;
				}
			}
			if (state == ControlState.RotatingCamera || state == ControlState.ZoomingCamera)
			{
				for (int i = 0; i < touchCount; i++)
				{
					Touch touch3 = touches[i];
					if (touch3.phase == TouchPhase.Moved || touch3.phase == TouchPhase.Stationary || touch3.phase == TouchPhase.Ended)
					{
						if (touch3.fingerId == fingerDown[0])
						{
							touch = touch3;
							flag = true;
						}
						else if (touch3.fingerId == fingerDown[1])
						{
							touch2 = touch3;
							flag2 = true;
						}
					}
				}
				if (flag)
				{
					if (flag2)
					{
						CameraControl(touch, touch2);
					}
				}
				else
				{
					state = ControlState.WaitingForNoFingers;
				}
			}
		}
		CharacterControl();
	}

	private void LateUpdate()
	{
		cameraPivot.eulerAngles = new Vector3(0f, Mathf.SmoothDampAngle(cameraPivot.eulerAngles.y, rotationTarget, ref rotationVelocity, 0.3f), 0f);
	}
}
