using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattingControls : Singleton<BattingControls>
{
	public Button RunBtn;

	public Button CancelBtn;

	public Button LoftBtn;

	public Image outerRing;

	public Image InnerRing;

	public GameObject allBtns;

	public GameObject LoftMeter;

	public GameObject battingMeterPointer;

	public GameObject perfectRegion;

	public GameObject battingMeter;

	public GameObject GreedyAdsImage;

	public Sprite[] btnStates;

	private bool canToggle;

	private Vector3 PowerPosition;

	private Vector2 PowerSize;

	private Transform _transform;

	public Text battingTimingNeedleText;

	public Image LoftFill;

	public Text loftProgress;

	public int btmPerfectValue = 10;

	protected void Awake()
	{
		battingMeter.SetActive(value: false);
		GreedyAdsImage.SetActive(value: true);
		Hide(boolean: true);
		_transform = base.transform;
	}

	public void AlignMe()
	{
		_transform.localPosition = new Vector3(_transform.localPosition.x, 0f, _transform.localPosition.z);
	}

	public void addEventListener()
	{
	}

	public void EnableShot(bool boolean)
	{
		if (boolean)
		{
			LoftBtn.gameObject.SetActive(value: true);
			RunBtn.gameObject.SetActive(value: false);
			EnableCancelRun(boolean: false);
		}
		else
		{
			LoftBtn.gameObject.SetActive(value: false);
			Singleton<GameModel>.instance.rvPopup.SetActive(value: false);
		}
	}

	public IEnumerator EnableRun(bool boolean)
	{
		if (boolean)
		{
			canToggle = false;
			LoftBtn.gameObject.SetActive(value: false);
			Singleton<GameModel>.instance.rvPopup.SetActive(value: false);
			RunBtn.gameObject.SetActive(value: true);
			yield return new WaitForSeconds(0.001f);
			canToggle = true;
		}
		else
		{
			RunBtn.gameObject.SetActive(value: false);
		}
	}

	public void EnableCancelRun(bool boolean)
	{
		CancelBtn.gameObject.SetActive(boolean);
	}

	public void ChangeButtonImage(int index)
	{
		InnerRing.sprite = btnStates[index];
	}

	public void RunClicked()
	{
		if (canToggle)
		{
			PassToRun();
		}
	}

	public void hideCancelBtn()
	{
		CancelBtn.gameObject.SetActive(value: false);
		RunBtn.gameObject.SetActive(value: false);
	}

	public void showCancelBtn()
	{
	}

	public void CancelClicked()
	{
		EnableCancelRun(boolean: false);
		Singleton<GameModel>.instance.CancelRun();
	}

	private void PassToRun()
	{
		canToggle = false;
		Singleton<GameModel>.instance.InitRun(boolean: true);
		Invoke("toggleRun", 1f);
	}

	private void toggleRun()
	{
		canToggle = true;
	}

	public void RunCompleted()
	{
		canToggle = false;
		Invoke("toggleRun", 0.001f);
	}

	public void Hide(bool boolean)
	{
		if (boolean)
		{
			allBtns.SetActive(value: false);
		}
		else
		{
			allBtns.SetActive(value: true);
		}
	}

	public void loftButtonClivked()
	{
		if (InnerRing.sprite == btnStates[0])
		{
			InnerRing.sprite = btnStates[1];
			PassToRun();
			CONTROLLER.prevPowerShot = true;
		}
		else if (InnerRing.sprite == btnStates[1])
		{
			InnerRing.sprite = btnStates[0];
			EnableCancelRun(boolean: false);
			Singleton<GameModel>.instance.CancelRun();
			CONTROLLER.prevPowerShot = false;
		}
	}

	public bool GetPowerIconStatus()
	{
		if (InnerRing.sprite == btnStates[0])
		{
			return false;
		}
		return true;
	}

	public Vector3 GetShotPos()
	{
		return PowerPosition;
	}

	public Vector2 GetShotSize()
	{
		return PowerSize;
	}

	public void LoftMeterFill(int batsmanID)
	{
		if (Singleton<GroundController>.instance.lineFreeHit || CONTROLLER.PlayModeSelected != 7)
		{
			LoftMeter.SetActive(value: false);
			LoftBtn.interactable = true;
			return;
		}
		LoftMeter.SetActive(value: true);
		LoftFill.fillAmount = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].LoftMeterFillVal / 100f;
		if (CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].LoftMeterFillVal >= 100f)
		{
			LoftBtn.interactable = true;
		}
		else
		{
			LoftBtn.interactable = false;
		}
		loftProgress.text = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[batsmanID].LoftMeterFillVal + "%";
	}

	public void updateBatsmanTiming(float x)
	{
		int num = 100;
		int num2 = 30;
		if (x < (float)num2)
		{
			x = num2;
		}
		float num3 = 0.007f;
		num3 = ((CONTROLLER.difficultyMode == "hard") ? 0.002f : ((!(CONTROLLER.difficultyMode == "medium")) ? 0.004f : 0.003f));
		float num4 = x;
		if (num4 == 0f)
		{
			num4 = num2;
		}
		num4 *= num3;
		perfectRegion.transform.localScale = new Vector3(num4, 1f, 1f);
		float num5 = num4 * 10f / 0.5f;
		if (num5 < 2.5f)
		{
			num5 = 2.5f;
		}
		float num6 = Mathf.Ceil(num5);
		btmPerfectValue = (int)num6;
	}
}
