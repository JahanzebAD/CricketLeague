using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpinAndWinPanelTransition : Singleton<SpinAndWinPanelTransition>
{
	public Transform holder;

	public Button spinButton;

	public Transform wheel;

	public Transform wheelref;

	public Transform wheelpointerref;

	public Image wheelPointer;

	private TweenCallback activeSpinBtn;

	public Transform subPanel;

	private void Start()
	{
		activeSpinBtn = EnableSpinBtn;
	}

	public void ResetTransition()
	{
		holder.localScale = new Vector3(0f, 1f, 1f);
		wheel.localPosition = wheelref.localPosition;
		wheel.DORotate(new Vector3(0f, 0f, 0f), 0f).SetUpdate(isIndependentUpdate: true);
		wheelPointer.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		spinButton.gameObject.SetActive(value: false);
	}

	public void PanelTransition()
	{
		Singleton<SpinWheel>.instance.SpinButton.interactable = false;
		for (int i = 0; i < Singleton<SpinWheel>.instance.YellowDot.Length; i++)
		{
			Singleton<SpinWheel>.instance.YellowDot[i].DOFade(0f, 0f);
			Singleton<SpinWheel>.instance.BlackDot[i].DOFade(0f, 0f);
			Singleton<SpinWheel>.instance.YellowDot[i].gameObject.SetActive(value: false);
			Singleton<SpinWheel>.instance.BlackDot[i].gameObject.SetActive(value: false);
		}
		ResetTransition();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(holder.DOScaleX(1f, 0.4f));
		sequence.Insert(0.2f, wheel.DOLocalMoveX(0f, 1f));
		sequence.Insert(0.2f, wheel.DORotate(new Vector3(0f, 0f, -360f), 1f, RotateMode.WorldAxisAdd));
		sequence.Insert(1f, wheelPointer.DOFade(1f, 0f));
		sequence.Insert(1f, wheelPointer.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.4f, 0));
		sequence.InsertCallback(1.1f, activeSpinBtn);
		sequence.Insert(1.1f, spinButton.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.4f, 0)).OnComplete(Singleton<SpinWheel>.instance.StartYellowDot);
		sequence.SetUpdate(isIndependentUpdate: true);
	}

	private void EnableSpinBtn()
	{
		spinButton.gameObject.SetActive(value: true);
	}

	public void SubPanelTransition()
	{
		subPanel.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f, 0);
	}
}
