using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CameraRelativeControl : MonoBehaviour
{
	private Joystick moveJoystick;

	private Joystick rotateJoystick;

	private Transform cameraPivot;

	private Transform cameraTransform;

	private float speed = 5f;

	private float jumpSpeed = 8f;

	private float inAirMultiplier = 0.25f;

	private Vector2 rotationSpeed = new Vector2(50f, 25f);

	private Transform thisTransform;

	private CharacterController character;

	private Vector3 velocity;

	private bool canJump = true;

	private void Start()
	{
		thisTransform = GetComponent<Transform>();
		character = GetComponent<CharacterController>();
		GameObject gameObject = GameObject.Find("PlayerSpawn");
		if ((bool)gameObject)
		{
			thisTransform.position = gameObject.transform.position;
		}
	}

	private void FaceMovementDirection()
	{
		Vector3 vector = character.velocity;
		vector.y = 0f;
		if (vector.magnitude > 0.1f)
		{
			thisTransform.forward = vector.normalized;
		}
	}

	private void OnEndGame()
	{
		moveJoystick.Disable();
		rotateJoystick.Disable();
		base.enabled = false;
	}

	private void Update()
	{
		Vector3 direction = new Vector3(moveJoystick.position.x, 0f, moveJoystick.position.y);
		Vector3 motion = cameraTransform.TransformDirection(direction);
		motion.y = 0f;
		motion.Normalize();
		Vector3 vector = new Vector3(Mathf.Abs(moveJoystick.position.x), Mathf.Abs(moveJoystick.position.y), 0f);
		motion *= speed * ((!(vector.x > vector.y)) ? vector.y : vector.x);
		if (character.isGrounded)
		{
			if (!rotateJoystick.IsFingerDown())
			{
				canJump = true;
			}
			if (canJump && rotateJoystick.tapCount == 2)
			{
				velocity = character.velocity;
				velocity.y = jumpSpeed;
				canJump = false;
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
		FaceMovementDirection();
		Vector2 position = rotateJoystick.position;
		position.x *= rotationSpeed.x;
		position.y *= rotationSpeed.y;
		position *= Time.deltaTime;
		cameraPivot.Rotate(0f, position.x, 0f, Space.World);
		cameraPivot.Rotate(position.y, 0f, 0f);
	}
}
