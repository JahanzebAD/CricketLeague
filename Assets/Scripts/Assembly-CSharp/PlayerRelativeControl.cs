using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerRelativeControl : MonoBehaviour
{
	private Joystick moveJoystick;

	private Joystick rotateJoystick;

	private Transform cameraPivot;

	private float forwardSpeed = 4f;

	private float backwardSpeed = 1f;

	private float sidestepSpeed = 1f;

	private float jumpSpeed = 8f;

	private float inAirMultiplier = 0.25f;

	private Vector2 rotationSpeed = new Vector2(50f, 25f);

	private Transform thisTransform;

	private CharacterController character;

	private Vector3 cameraVelocity;

	private Vector3 velocity;

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
		moveJoystick.Disable();
		rotateJoystick.Disable();
		base.enabled = false;
	}

	private void Update()
	{
		Vector3 direction = new Vector3(moveJoystick.position.x, 0f, moveJoystick.position.y);
		Vector3 motion = thisTransform.TransformDirection(direction);
		motion.y = 0f;
		motion.Normalize();
		Vector3 vector = Vector3.zero;
		Vector2 vector2 = new Vector2(Mathf.Abs(moveJoystick.position.x), Mathf.Abs(moveJoystick.position.y));
		if (vector2.y > vector2.x)
		{
			if (moveJoystick.position.y > 0f)
			{
				motion *= forwardSpeed * vector2.y;
			}
			else
			{
				motion *= backwardSpeed * vector2.y;
				vector = new Vector3(0f, 0f, moveJoystick.position.y * 0.75f);
			}
		}
		else
		{
			motion *= sidestepSpeed * vector2.x;
			vector = new Vector3((0f - moveJoystick.position.x) * 0.5f, 0f, 0f);
		}
		if (character.isGrounded)
		{
			if (rotateJoystick.tapCount == 2)
			{
				velocity = character.velocity;
				velocity.y = jumpSpeed;
			}
		}
		else
		{
			velocity.y += Physics.gravity.y * Time.deltaTime;
			vector = new Vector3(0f, 0f, (0f - jumpSpeed) * 0.25f);
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
		Vector3 localPosition = cameraPivot.localPosition;
		localPosition.x = Mathf.SmoothDamp(localPosition.x, vector.x, ref cameraVelocity.x, 0.3f);
		localPosition.z = Mathf.SmoothDamp(localPosition.z, vector.z, ref cameraVelocity.z, 0.5f);
		cameraPivot.localPosition = localPosition;
		if (character.isGrounded)
		{
			Vector2 position = rotateJoystick.position;
			position.x *= rotationSpeed.x;
			position.y *= rotationSpeed.y;
			position *= Time.deltaTime;
			thisTransform.Rotate(0f, position.x, 0f, Space.World);
			cameraPivot.Rotate(position.y, 0f, 0f);
		}
	}
}
