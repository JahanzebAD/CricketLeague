using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : Singleton<Tutorial>
{
	public GameObject holder;

	public GameObject posHolder;

	public GameObject shotHolder;

	public GameObject bowlingHolder;

	public GameObject bowlingSpotArrow;

	public GameObject tutorialToggle;

	private Transform _transform;

	public Transform hand;

	public Transform[] arrows;

	private Sequence sq;

	private Vector3[] toPos = new Vector3[8]
	{
		new Vector3(0f, 60f, 0f),
		new Vector3(-41f, 41f, 0f),
		new Vector3(-60f, 0f, 0f),
		new Vector3(-41f, -41f, 0f),
		new Vector3(0f, -60f, 0f),
		new Vector3(41f, -41f, 0f),
		new Vector3(60f, 0f, 0f),
		new Vector3(41f, 41f, 0f)
	};

	private bool batsmanArrowState;

	private bool bowlingSpotState;

	private bool shotHolderXPos;

	private bool isPaused;

	private bool tutorialBtn;

	protected void Awake()
	{
		sq = DOTween.Sequence();
		_transform = base.transform;
		hideTutorial();
	}

	public void hideTutorial()
	{
		posHolder.SetActive(value: false);
		StopHandAnim();
		bowlingHolder.SetActive(value: false);
		StopArrowAnim();
		shotHolder.SetActive(value: false);
	}

	private void StartArrowAnim()
	{
		sq = DOTween.Sequence();
		sq.SetUpdate(isIndependentUpdate: true);
		for (int i = 0; i < arrows.Length; i++)
		{
			sq.Insert(0f, arrows[i].DOLocalMove(toPos[i], 1f));
			sq.Insert(0.8f, arrows[i].GetComponent<Image>().DOFade(0f, 0.4f));
		}
	}

	private void StopArrowAnim()
	{
		for (int i = 0; i < arrows.Length; i++)
		{
			sq.Insert(0f, arrows[i].DOLocalMove(Vector3.zero, 0f));
			sq.Insert(0f, arrows[i].GetComponent<Image>().DOFade(1f, 0f));
		}
	}

	private void StartHandAnim()
	{
		sq = DOTween.Sequence();
		sq.Insert(0f, hand.DOLocalMoveX(50f, 1f)).SetLoops(-1, LoopType.Yoyo);
		sq.SetUpdate(isIndependentUpdate: true);
	}

	private void StopHandAnim()
	{
		hand.DOLocalMoveX(150f, 0f).SetUpdate(isIndependentUpdate: true);
	}

	public void showPositionHolder()
	{
		Time.timeScale = 0f;
		posHolder.SetActive(value: true);
		StartHandAnim();
	}

	public void showShotHolder()
	{
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
		{
			Time.timeScale = 0f;
			if (CONTROLLER.BattingTeamIndex == CONTROLLER.myTeamIndex)
			{
				shotHolder.SetActive(value: true);
				StartArrowAnim();
			}
		}
	}

	public void showBowlingHolder()
	{
		Invoke("showNow", 0.01f);
	}

	private void showNow()
	{
		Time.timeScale = 0f;
		bowlingHolder.SetActive(value: true);
	}

	public void updateBowlingHolderPos(Vector3 bowlingSpotPos)
	{
		bowlingSpotArrow.transform.localPosition = new Vector3(0f, 0.5f, 0f);
	}

	public void setBoolean()
	{
		if (CONTROLLER.tutorialToggle == 1 && CONTROLLER.PlayModeSelected != 6)
		{
			if (CONTROLLER.myTeamIndex == CONTROLLER.BattingTeamIndex)
			{
				posHolder.SetActive(batsmanArrowState);
				if (batsmanArrowState)
				{
					StartHandAnim();
				}
				else
				{
					StopHandAnim();
				}
				if (shotHolderXPos)
				{
					showShotHolder();
				}
			}
			else
			{
				bowlingHolder.SetActive(bowlingSpotState);
			}
		}
		else
		{
			hideTutorial();
		}
		batsmanArrowState = false;
		bowlingSpotState = false;
		shotHolderXPos = false;
		isPaused = false;
		tutorialBtn = false;
	}

	public void SwitchOffTutorial()
	{
		CONTROLLER.tutorialToggle = 0;
		if (Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
		}
		hideTutorial();
		tutorialToggle.SetActive(value: false);
		SavePlayerPrefs.SetSettingsList();
	}

	public void getBoolean()
	{
		if (!isPaused)
		{
			tutorialBtn = tutorialToggle.activeSelf;
			batsmanArrowState = posHolder.activeSelf;
			bowlingSpotState = bowlingHolder.activeSelf;
			shotHolderXPos = shotHolder.activeSelf;
			posHolder.SetActive(value: false);
			bowlingHolder.SetActive(value: false);
			isPaused = true;
		}
	}

	public void ShowMe()
	{
		holder.SetActive(value: true);
	}

	public void HideMe()
	{
		holder.SetActive(value: false);
	}
}
