using System.Collections;
using Beebyte.Obfuscator;
using UnityEngine;
using UnityEngine.UI;

public class BowlingControls : Singleton<BowlingControls>
{
	private GroundController groundControllerScript;

	public Image YelloFillMeter;

	public GameObject controls;

	public GameObject SpeedArrow;

	private Transform SpeedArrowTransform;

	public GameObject SpinBall;

	public GameObject spinBallHolder;

	private Transform spinBallTransform;

	private float startValue = 0.05f;

	private float endValue = 0.85f;

	public GameObject SwingLines;

	public Sprite[] anim;

	private int speedBarWidth = 150;

	private int currentSpeed;

	private int action;

	private int endAngle;

	private int swingValue = 2;

	private Transform _transform;

	protected void Awake()
	{
		_transform = base.transform;
		groundControllerScript = GameObject.Find("GroundController").GetComponent<GroundController>();
		Hide(boolean: true);
		SpeedArrowTransform = SpeedArrow.transform;
		spinBallTransform = SpinBall.transform;
	}

	protected void Start()
	{
		speedBarWidth = 123;
	}

	private void ResetSpeedOMeter()
	{
		currentSpeed = 0;
		swingValue = 2;
		YelloFillMeter.fillAmount = 0.4f;
		SpeedArrowTransform.localPosition = SpeedArrowTransform.localPosition;
		spinBallTransform.localEulerAngles = new Vector3(spinBallTransform.localEulerAngles.x, spinBallTransform.localEulerAngles.y, 0f);
		SetSwingParameter();
		action = 0;
		if (CONTROLLER.PlayModeSelected == 6)
		{
			spinBallHolder.SetActive(value: false);
			SwingLines.SetActive(value: false);
			controls.SetActive(value: false);
			SpeedArrow.SetActive(value: false);
			SpinBall.SetActive(value: false);
			spinBallHolder.SetActive(value: false);
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			if (groundControllerScript.currentBowlerType == "fast" || groundControllerScript.currentBowlerType == "medium")
			{
				spinBallHolder.SetActive(value: false);
				SwingLines.SetActive(value: true);
			}
			else
			{
				spinBallHolder.SetActive(value: true);
				SwingLines.SetActive(value: false);
			}
		}
		else if (CONTROLLER.BowlerType == 2 || CONTROLLER.BowlerType == 1)
		{
			spinBallHolder.SetActive(value: true);
			SwingLines.SetActive(value: false);
		}
		else
		{
			spinBallHolder.SetActive(value: false);
			SwingLines.SetActive(value: true);
		}
	}

	public void ActivateBowlingControls()
	{
		Singleton<GameModel>.instance.CanPauseGame = true;
		ResetSpeedOMeter();
		groundControllerScript.ShowAllPlayers();
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			swingValue = 2;
			if (Random.Range(0f, 10f) > 5f)
			{
				swingValue = Random.Range(0, 5);
			}
			SetSwingParameter();
		}
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			CalculateBowlingSpeed();
			return;
		}
		AnimateSpeedOMeter();
		AnimateSpeedOMeter2();
	}

	private void SetSwingParameter()
	{
		SwingLines.GetComponent<Image>().sprite = anim[swingValue];
	}

	[Skip]
	public void AnimateSpeedOMeter2()
	{
		iTween.MoveTo(SpeedArrow, iTween.Hash("x", (speedBarWidth - 7) / 2, "time", 1, "islocal", true, "oncomplete", "MoveDownBall", "oncompletetarget", base.gameObject));
	}

	[Skip]
	public void MoveDownBall()
	{
		iTween.MoveTo(SpeedArrow, iTween.Hash("x", -(speedBarWidth - 7) / 2, "time", 1, "islocal", true, "oncomplete", "AnimateSpeedOMeter2", "oncompletetarget", base.gameObject));
	}

	public void StopYellowFill()
	{
		iTween.Stop(base.gameObject);
		stopSwingAngle();
	}

	[Skip]
	public void AnimateSpeedOMeter()
	{
		DebugLogger.PrintError("Called bowling control");
		YelloFillMeter.fillAmount = startValue;
		iTween.ValueTo(base.gameObject, iTween.Hash("from", startValue, "to", endValue, "time", 1f, "onupdate", "updateForward", "oncompletetarget", base.gameObject));
	}

	[Skip]
	private void updateForward(float upValue)
	{
		YelloFillMeter.fillAmount = upValue;
		if (upValue >= endValue)
		{
			iTween.ValueTo(base.gameObject, iTween.Hash("from", endValue, "to", startValue, "time", 1f, "onupdate", "updateBackward", "oncompletetarget", base.gameObject));
		}
	}

	[Skip]
	private void updateBackward(float downValue)
	{
		YelloFillMeter.fillAmount = downValue;
		if (downValue <= startValue)
		{
			iTween.ValueTo(base.gameObject, iTween.Hash("from", startValue, "to", endValue, "time", 1f, "onupdate", "updateForward", "oncompletetarget", base.gameObject));
		}
	}

	private void CalculateBowlingSpeed()
	{
		float fillAmount = Random.Range(0.2f, 0.7f);
		if (canAiBowlLineNoBall())
		{
			fillAmount = Random.Range(0.7f, 0.813f);
			groundControllerScript.computerCanNowBowlNoBall();
		}
		YelloFillMeter.fillAmount = fillAmount;
		currentSpeed = CalculatedSpeed();
		LockSpeed();
	}

	public float getArrowPos()
	{
		return YelloFillMeter.fillAmount;
	}

	private int setBallSpeed()
	{
		int num = 0;
		int num2 = 1;
		int num3 = (int)((YelloFillMeter.fillAmount - (float)num) / (float)(num2 - num) * 10f);
		if (num3 <= 0)
		{
			num3 = 1;
		}
		return num3;
	}

	private bool canAiBowlLineNoBall()
	{
		int num = Random.Range(0, 100);
		if (num > 97 && CONTROLLER.PlayModeSelected != 6)
		{
			return true;
		}
		return false;
	}

	private IEnumerator setSwingAngle()
	{
		SwingLines.GetComponent<Image>().sprite = anim[swingValue];
		yield return new WaitForSeconds(0.2f);
		swingValue++;
		if (swingValue >= 5)
		{
			swingValue = 0;
		}
		StartCoroutine("setSwingAngle");
	}

	public void LockSpeed()
	{
		StopYellowFill();
		Singleton<GameModel>.instance.speedLocked();
		if (CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex && action == 0)
		{
			iTween.Stop(SpeedArrow);
			iTween.Stop(base.gameObject);
			currentSpeed = setBallSpeed();
		}
		CONTROLLER.BowlingSpeed = currentSpeed;
		if (CONTROLLER.BowlerType == 0 && CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
		{
			CONTROLLER.BowlingSwing = swingValue;
			Singleton<GameModel>.instance.SendBowlingDatasToGame();
		}
		else if (CONTROLLER.BowlerType == 0 && CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
		{
			swingValue = 0;
			StartCoroutine("setSwingAngle");
		}
		else if ((CONTROLLER.BowlerType == 1 && CONTROLLER.BowlerHand == "right") || (CONTROLLER.BowlerType == 2 && CONTROLLER.BowlerHand == "left"))
		{
			endAngle = -52;
			InitAngleOMeter();
		}
		else if ((CONTROLLER.BowlerType == 2 && CONTROLLER.BowlerHand == "right") || (CONTROLLER.BowlerType == 1 && CONTROLLER.BowlerHand == "left"))
		{
			endAngle = 50;
			InitAngleOMeter();
		}
		else if (CONTROLLER.BowlerType == 3 && CONTROLLER.BowlingTeamIndex != CONTROLLER.myTeamIndex)
		{
			CONTROLLER.BowlingSwing = swingValue;
			Singleton<GameModel>.instance.SendBowlingDatasToGame();
		}
		else if (CONTROLLER.BowlerType == 3 && CONTROLLER.BowlingTeamIndex == CONTROLLER.myTeamIndex)
		{
			swingValue = 0;
			StartCoroutine("setSwingAngle");
		}
		action = 1;
	}

	public void stopSwingAngle()
	{
		StopCoroutine("setSwingAngle");
	}

	public void ChangeSwing()
	{
		if (CONTROLLER.myTeamIndex == CONTROLLER.BowlingTeamIndex)
		{
			ChangeSwingParameter();
		}
	}

	public void ChangeSwingParameter()
	{
		if (CONTROLLER.BowlerType == 0 || CONTROLLER.BowlerType == 3)
		{
			swingValue++;
			if (swingValue >= 5)
			{
				swingValue = 0;
			}
			SetSwingParameter();
		}
	}

	[Skip]
	public void InitAngleOMeter()
	{
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			CalculateBowlingAngle();
			return;
		}
		iTween.RotateTo(SpinBall, iTween.Hash("rotation", new Vector3(0f, 0f, endAngle), "islocal", true, "time", 0.5, "oncomplete", "RotateToOpp", "oncompletetarget", base.gameObject));
	}

	[Skip]
	public void RotateToOpp()
	{
		iTween.RotateTo(SpinBall, iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "islocal", true, "time", 0.5, "oncomplete", "InitAngleOMeter", "oncompletetarget", base.gameObject));
	}

	private void CalculateBowlingAngle()
	{
		int num = Random.Range(0, endAngle);
		spinBallTransform.eulerAngles = new Vector3(0f, 0f, num);
		LockAngle();
	}

	private int CalculatedSpeed()
	{
		float num = 10f * YelloFillMeter.fillAmount / 0.715f;
		currentSpeed = (int)num;
		if (currentSpeed < 1)
		{
			currentSpeed = 1;
		}
		else if (currentSpeed >= 10)
		{
			currentSpeed = 10;
		}
		return currentSpeed;
	}

	public void LockAngle()
	{
		action = 2;
		stopSwingAngle();
		if (CONTROLLER.BowlerType == 2 || CONTROLLER.BowlerType == 1)
		{
			iTween.Stop(SpinBall);
			float num = spinBallTransform.eulerAngles.z;
			if (num >= 300f)
			{
				num = 360f - num;
			}
			CONTROLLER.BowlingAngle = Mathf.Abs(num * 100f) / 40f / 10f;
		}
		else
		{
			CONTROLLER.BowlingSwing = swingValue;
		}
		Singleton<GameModel>.instance.SendBowlingDatasToGame();
	}

	public void Hide(bool boolean)
	{
		if (boolean)
		{
			controls.SetActive(value: false);
			return;
		}
		Singleton<PauseGameScreen>.instance.Hide(boolean: true);
		controls.SetActive(value: true);
	}
}
