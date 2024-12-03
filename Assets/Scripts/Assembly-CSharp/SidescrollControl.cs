using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SidescrollControl : MonoBehaviour
{
	private Joystick moveTouchPad;

	private Joystick jumpTouchPad;

	private float forwardSpeed = 4f;

	private float backwardSpeed = 4f;

	private float jumpSpeed = 16f;

	private float inAirMultiplier = 0.25f;

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

	private void OnEndGame()
	{
		moveTouchPad.Disable();
		jumpTouchPad.Disable();
		base.enabled = false;
	}

	private void Update()
	{
		Vector3 motion = Vector3.zero;
		motion = ((!(moveTouchPad.position.x > 0f)) ? (Vector3.right * backwardSpeed * moveTouchPad.position.x) : (Vector3.right * forwardSpeed * moveTouchPad.position.x));
		if (character.isGrounded)
		{
			bool flag = false;
			Joystick joystick = jumpTouchPad;
			if (!joystick.IsFingerDown())
			{
				canJump = true;
			}
			if (canJump && joystick.IsFingerDown())
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
		}
		motion += velocity;
		motion += Physics.gravity;
		motion *= Time.deltaTime;
		character.Move(motion);
		if (character.isGrounded)
		{
			velocity = Vector3.zero;
		}
	}
}
