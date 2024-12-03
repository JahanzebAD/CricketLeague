using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonControl : MonoBehaviour
{
	private Joystick moveTouchPad;

	private Joystick rotateTouchPad;

	private Transform cameraPivot;

	private float forwardSpeed = 4f;

	private float backwardSpeed = 1f;

	private float sidestepSpeed = 1f;

	private float jumpSpeed = 8f;

	private float inAirMultiplier = 0.25f;

	private Vector2 rotationSpeed = new Vector2(50f, 25f);

	private float tiltPositiveYAxis = 0.6f;

	private float tiltNegativeYAxis = 0.4f;

	private float tiltXAxisMinimum = 0.1f;

	private Transform thisTransform;

	private CharacterController character;

	private Vector3 cameraVelocity;

	private Vector3 velocity;

	private bool canJump = true;

	private void Start()
	{
		thisTransform = GetComponent<Transform>();
		character = GetComponent<CharacterController>();
		GameObject gameObject = GameObject.Find("PlayerSpawn");
		if (gameObject != null)
		{
			thisTransform.position = gameObject.transform.position;
		}
	}

	private void OnEndGame()
	{
		moveTouchPad.Disable();
		if ((bool)rotateTouchPad)
		{
			rotateTouchPad.Disable();
		}
		base.enabled = false;
	}

	private void Update()
	{
		Vector3 direction = new Vector3(moveTouchPad.position.x, 0f, moveTouchPad.position.y);
		Vector3 motion = thisTransform.TransformDirection(direction);
		motion.y = 0f;
		motion.Normalize();
		Vector2 vector = new Vector2(Mathf.Abs(moveTouchPad.position.x), Mathf.Abs(moveTouchPad.position.y));
		if (vector.y > vector.x)
		{
			if (moveTouchPad.position.y > 0f)
			{
				motion *= forwardSpeed * vector.y;
			}
			else
			{
				motion *= backwardSpeed * vector.y;
			}
		}
		else
		{
			motion *= sidestepSpeed * vector.x;
		}
		if (character.isGrounded)
		{
			bool flag = false;
			Joystick joystick = ((!rotateTouchPad) ? moveTouchPad : rotateTouchPad);
			if (!joystick.IsFingerDown())
			{
				canJump = true;
			}
			if (canJump && joystick.tapCount >= 2)
			{
				flag = true;
				canJump = false;
			}
			if (flag)
			{
				velocity = character.velocity;
				velocity.y = jumpSpeed;
			}
		}
		else
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
		if (!character.isGrounded)
		{
			return;
		}
		Vector2 vector2 = Vector2.zero;
		if ((bool)rotateTouchPad)
		{
			vector2 = rotateTouchPad.position;
		}
		else
		{
			Vector3 acceleration = Input.acceleration;
			float num = Mathf.Abs(acceleration.x);
			if (acceleration.z < 0f && acceleration.x < 0f)
			{
				if (num >= tiltPositiveYAxis)
				{
					vector2.y = (num - tiltPositiveYAxis) / (1f - tiltPositiveYAxis);
				}
				else if (num <= tiltNegativeYAxis)
				{
					vector2.y = (0f - (tiltNegativeYAxis - num)) / tiltNegativeYAxis;
				}
			}
			if (Mathf.Abs(acceleration.y) >= tiltXAxisMinimum)
			{
				vector2.x = (0f - (acceleration.y - tiltXAxisMinimum)) / (1f - tiltXAxisMinimum);
			}
		}
		vector2.x *= rotationSpeed.x;
		vector2.y *= rotationSpeed.y;
		vector2 *= Time.deltaTime;
		thisTransform.Rotate(0f, vector2.x, 0f, Space.World);
		cameraPivot.Rotate(0f - vector2.y, 0f, 0f);
	}
}
