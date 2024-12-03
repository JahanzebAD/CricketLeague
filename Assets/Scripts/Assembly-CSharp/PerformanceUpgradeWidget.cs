using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using UnityEngine;

public class PerformanceUpgradeWidget : Singleton<PerformanceUpgradeWidget>
{
	private bool PGTimerShow;

	private bool CGTimerShow;

	private bool AGTimerShow;

	private bool PGUpgradeShow;

	private bool CGUpgradeShow;

	private bool AGUpgradeShow;

	private GameObject sideScreen;

	private GameObject mainScreen;

	private GameObject tempMainScreen;

	private float sideScreenPos;

	private float mainScreenPos;

	public GameObject PGTimer;

	public GameObject PGUpgrade;

	public GameObject CGTimer;

	public GameObject CGUpgrade;

	public GameObject AGTimer;

	public GameObject AGUpgrade;

	public GameObject DefaultSlide;

	public GameObject PowerPanel;

	public GameObject ControlPanel;

	public GameObject AgilityPanel;

	private int[] XPmilestones = new int[10] { 0, 5000, 10000, 20000, 35000, 55000, 80000, 110000, 145000, 185000 };

	private string[] UpgradePrefs = new string[3] { "PowerUpgradeTimer", "ControlUpgradeTimer", "AgilityUpgradeTimer" };

	private void Start()
	{
		Invoke("CheckValidity", 5f);
	}

	public void CheckValidity()
	{
		if (CONTROLLER.PlayModeSelected == 6)
		{
			return;
		}
		if (!ObscuredPrefs.HasKey(UpgradePrefs[0]))
		{
			PGTimerShow = false;
			if (CONTROLLER.Coins > Singleton<PaymentProcess>.instance.GenerateCoinValue(0))
			{
				float num = 0f;
				float num2 = 0f;
				if (CONTROLLER.powerGrade <= 5)
				{
					num = CONTROLLER.totalPowerSubGrade - CONTROLLER.powerGrade * 50;
					num2 = 50f;
				}
				else if (CONTROLLER.powerGrade >= 6 && CONTROLLER.powerGrade <= 9)
				{
					num = CONTROLLER.totalPowerSubGrade - 300 - (CONTROLLER.powerGrade - 6) * 250;
					num2 = 250f;
				}
				if (num + 10f < num2 || CONTROLLER.XPs >= XPmilestones[CONTROLLER.powerGrade] || CONTROLLER.powerGrade == 10)
				{
					PGUpgradeShow = true;
				}
				else
				{
					PGUpgradeShow = false;
				}
			}
			else
			{
				PGUpgradeShow = false;
			}
		}
		else
		{
			PGUpgradeShow = false;
			PGTimerShow = true;
		}
		if (!ObscuredPrefs.HasKey(UpgradePrefs[1]))
		{
			CGTimerShow = false;
			if (CONTROLLER.Coins > Singleton<PaymentProcess>.instance.GenerateCoinValue(1))
			{
				float num3 = 0f;
				float num4 = 0f;
				if (CONTROLLER.controlGrade <= 5)
				{
					num3 = CONTROLLER.totalControlSubGrade - CONTROLLER.controlGrade * 50;
					num4 = 50f;
				}
				else if (CONTROLLER.controlGrade >= 6 && CONTROLLER.controlGrade <= 9)
				{
					num3 = CONTROLLER.totalControlSubGrade - 300 - (CONTROLLER.controlGrade - 6) * 250;
					num4 = 250f;
				}
				if (num3 + 10f < num4 || CONTROLLER.XPs >= XPmilestones[CONTROLLER.controlGrade] || CONTROLLER.controlGrade == 10)
				{
					CGUpgradeShow = true;
				}
				else
				{
					CGUpgradeShow = false;
				}
			}
			else
			{
				CGUpgradeShow = false;
			}
		}
		else
		{
			CGUpgradeShow = false;
			CGTimerShow = true;
		}
		if (!ObscuredPrefs.HasKey(UpgradePrefs[2]))
		{
			AGTimerShow = false;
			if (CONTROLLER.Coins > Singleton<PaymentProcess>.instance.GenerateCoinValue(2))
			{
				float num5 = 0f;
				float num6 = 0f;
				if (CONTROLLER.agilityGrade <= 5)
				{
					num5 = CONTROLLER.totalAgilitySubGrade - CONTROLLER.agilityGrade * 50;
					num6 = 50f;
				}
				else if (CONTROLLER.agilityGrade >= 6 && CONTROLLER.agilityGrade <= 9)
				{
					num5 = CONTROLLER.totalAgilitySubGrade - 300 - (CONTROLLER.agilityGrade - 6) * 250;
					num6 = 250f;
				}
				if (num5 + 10f < num6 || CONTROLLER.XPs >= XPmilestones[CONTROLLER.agilityGrade] || CONTROLLER.agilityGrade == 10)
				{
					AGUpgradeShow = true;
				}
				else
				{
					AGUpgradeShow = false;
				}
			}
			else
			{
				AGUpgradeShow = false;
			}
		}
		else
		{
			AGUpgradeShow = false;
			AGTimerShow = true;
		}
		StartTrasnsiton();
	}

	public void StartTrasnsiton()
	{
		CloseAllPanels();
		DefaultSlide.SetActive(value: true);
		if (tempMainScreen == null)
		{
			tempMainScreen = DefaultSlide;
		}
		else
		{
			sideScreen = DefaultSlide;
			mainScreen = tempMainScreen;
			AnimatePageMovement();
			tempMainScreen = DefaultSlide;
		}
		if (PGTimerShow || PGUpgradeShow)
		{
			Invoke("ShowPGPanel", 5f);
		}
		else if (CGTimerShow || CGUpgradeShow)
		{
			Invoke("ShowCGPanel", 5f);
		}
		else if (AGTimerShow || AGUpgradeShow)
		{
			Invoke("ShowAGPanel", 5f);
		}
	}

	private void ShowPGPanel()
	{
		PowerPanel.SetActive(value: true);
		sideScreen = PowerPanel;
		mainScreen = tempMainScreen;
		if (PGTimerShow)
		{
			PGTimer.SetActive(value: true);
		}
		else
		{
			PGUpgrade.SetActive(value: true);
		}
		AnimatePageMovement();
		tempMainScreen = PowerPanel;
		if (CGTimerShow || CGUpgradeShow)
		{
			Invoke("ShowCGPanel", 5f);
		}
		else if (AGTimerShow || AGUpgradeShow)
		{
			Invoke("ShowAGPanel", 5f);
		}
		else
		{
			Invoke("CheckValidity", 5f);
		}
	}

	private void ShowCGPanel()
	{
		ControlPanel.SetActive(value: true);
		sideScreen = ControlPanel;
		mainScreen = tempMainScreen;
		if (CGTimerShow)
		{
			CGTimer.SetActive(value: true);
		}
		else
		{
			CGUpgrade.SetActive(value: true);
		}
		AnimatePageMovement();
		tempMainScreen = ControlPanel;
		if (AGTimerShow || AGUpgradeShow)
		{
			Invoke("ShowAGPanel", 5f);
		}
		else
		{
			Invoke("CheckValidity", 5f);
		}
	}

	private void ShowAGPanel()
	{
		AgilityPanel.SetActive(value: true);
		sideScreen = AgilityPanel;
		mainScreen = tempMainScreen;
		if (AGTimerShow)
		{
			AGTimer.SetActive(value: true);
		}
		else
		{
			AGUpgrade.SetActive(value: true);
		}
		AnimatePageMovement();
		tempMainScreen = AgilityPanel;
		Invoke("CheckValidity", 5f);
	}

	private void CloseAllPanels()
	{
		PowerPanel.SetActive(value: false);
		ControlPanel.SetActive(value: false);
		AgilityPanel.SetActive(value: false);
		DefaultSlide.SetActive(value: false);
		AGTimer.SetActive(value: false);
		AGUpgrade.SetActive(value: false);
		CGTimer.SetActive(value: false);
		CGUpgrade.SetActive(value: false);
		PGTimer.SetActive(value: false);
		PGUpgrade.SetActive(value: false);
	}

	private void AnimatePageMovement()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, sideScreen.transform.DOLocalMoveX(-992f, 0f));
		sideScreen.SetActive(value: true);
		sequence.Insert(0f, mainScreen.transform.DOLocalMoveX(992f, 0.5f));
		sequence.Insert(0.1f, sideScreen.transform.DOLocalMoveX(0f, 0.75f));
		sequence.SetUpdate(isIndependentUpdate: true);
		sequence.OnComplete(ScreenCleanup);
	}

	private void ScreenCleanup()
	{
		mainScreen.SetActive(value: false);
		mainScreen.transform.DOLocalMoveX(0f, 0f);
	}
}
