using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Joystick : MonoBehaviour
{
	private class Boundary
	{
		public Vector2 min = Vector2.zero;

		public Vector2 max = Vector2.zero;
	}

	private static Joystick[] joysticks;

	private static bool enumeratedJoysticks;

	private static float tapTimeDelta = 0.3f;

	private bool touchPad;

	private Rect touchZone;

	private Vector2 deadZone = Vector2.zero;

	private bool normalize;

	public Vector2 position;

	public int tapCount;

	private int lastFingerId = -1;

	private float tapTimeWindow;

	private Vector2 fingerDownPos;

	private float fingerDownTime;

	private float firstDeltaTime = 0.5f;

	private Image gui;

	private Rect defaultRect;

	private Boundary guiBoundary;

	private Vector2 guiTouchOffset;

	private Vector2 guiCenter;

	private void Start()
	{
		gui = GetComponent<Image>();
		//defaultRect = gui.pixelInset;
		defaultRect.x += base.transform.position.x * (float)Screen.width;
		defaultRect.y += base.transform.position.y * (float)Screen.height;
		base.transform.position = new Vector3(0f, 0f, 0f);
		base.transform.position = new Vector3(0f, 0f, 0f);
		if (touchPad)
		{
			if ((bool)gui.sprite)
			{
				touchZone = defaultRect;
			}
			return;
		}
		guiTouchOffset = new Vector2(defaultRect.width * 0.5f, 0f);
		guiTouchOffset = new Vector2(0f, defaultRect.height * 0.5f);
		guiCenter.x = defaultRect.x + guiTouchOffset.x;
		guiCenter.y = defaultRect.y + guiTouchOffset.y;
		guiBoundary.min.x = defaultRect.x - guiTouchOffset.x;
		guiBoundary.max.x = defaultRect.x + guiTouchOffset.x;
		guiBoundary.min.y = defaultRect.y - guiTouchOffset.y;
		guiBoundary.max.y = defaultRect.y + guiTouchOffset.y;
	}

	public void Disable()
	{
		base.gameObject.active = false;
		enumeratedJoysticks = false;
	}

	private void ResetJoystick()
	{
		//gui.pixelInset = defaultRect;
		lastFingerId = -1;
		position = Vector2.zero;
		if (touchPad)
		{
			gui.color = new Color(0.025f, 0f, 0f, 0f);
		}
	}

	public bool IsFingerDown()
	{
		return lastFingerId != -1;
	}

	private void LatchedFinger(int fingerId)
	{
		if (lastFingerId == fingerId)
		{
			ResetJoystick();
		}
	}

	private void Update()
	{
		if (!enumeratedJoysticks)
		{
			joysticks = Object.FindObjectsOfType(typeof(Joystick)) as Joystick[];
			enumeratedJoysticks = true;
		}
		int touchCount = Input.touchCount;
		if (tapTimeWindow > 0f)
		{
			tapTimeWindow -= Time.deltaTime;
		}
		else
		{
			tapCount = 0;
		}
		if (touchCount == 0)
		{
			ResetJoystick();
		}
		else
		{
			for (int i = 0; i < touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				Vector2 vector = touch.position - guiTouchOffset;
				bool flag = false;
				if (touchPad)
				{
					if (touchZone.Contains(touch.position))
					{
						flag = true;
					}
				}
				//else if (gui.HitTest(touch.position))
				//{
				//	flag = true;
				//}
				if (flag && (lastFingerId == -1 || lastFingerId != touch.fingerId))
				{
					if (touchPad)
					{
						gui.color = new Color(0.15f, 0f, 0f, 0f);
						lastFingerId = touch.fingerId;
						fingerDownPos = touch.position;
						fingerDownTime = Time.time;
					}
					lastFingerId = touch.fingerId;
					if (tapTimeWindow > 0f)
					{
						tapCount++;
					}
					else
					{
						tapCount = 1;
						tapTimeWindow = tapTimeDelta;
					}
					Joystick[] array = joysticks;
					foreach (Joystick joystick in array)
					{
						if (joystick != this)
						{
							joystick.LatchedFinger(touch.fingerId);
						}
					}
				}
				if (lastFingerId == touch.fingerId)
				{
					if (touch.tapCount > tapCount)
					{
						tapCount = touch.tapCount;
					}
					if (touchPad)
					{
						position.x = Mathf.Clamp((touch.position.x - fingerDownPos.x) / (touchZone.width / 2f), -1f, 1f);
						position.y = Mathf.Clamp((touch.position.y - fingerDownPos.y) / (touchZone.height / 2f), -1f, 1f);
					}
					else
					{
						//gui.pixelInset = new Rect(Mathf.Clamp(vector.x, guiBoundary.min.x, guiBoundary.max.x), 0f, 0f, 0f);
						//gui.pixelInset = new Rect(0f, Mathf.Clamp(vector.y, guiBoundary.min.y, guiBoundary.max.y), 0f, 0f);
					}
					if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
					{
						ResetJoystick();
					}
				}
			}
		}
		if (!touchPad)
		{
			//position.x = (gui.pixelInset.x + guiTouchOffset.x - guiCenter.x) / guiTouchOffset.x;
			//position.y = (gui.pixelInset.y + guiTouchOffset.y - guiCenter.y) / guiTouchOffset.y;
		}
		float num = Mathf.Abs(position.x);
		float num2 = Mathf.Abs(position.y);
		if (num < deadZone.x)
		{
			position.x = 0f;
		}
		else if (normalize)
		{
			position.x = Mathf.Sign(position.x) * (num - deadZone.x) / (1f - deadZone.x);
		}
		if (num2 < deadZone.y)
		{
			position.y = 0f;
		}
		else if (normalize)
		{
			position.y = Mathf.Sign(position.y) * (num2 - deadZone.y) / (1f - deadZone.y);
		}
	}
}
