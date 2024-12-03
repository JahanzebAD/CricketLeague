using DG.Tweening;
using UnityEngine;

public class GamePausePanelTransition : Singleton<GamePausePanelTransition>
{
	public Transform topPanel;

	public Transform midPanel;

	public Transform innerPanel;

	public Transform[] buttons;

	public Transform[] flagAndText;

	public Transform[] optionsbtns;

	public void panelTransition()
	{
		resetTransition();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(topPanel.DOScaleX(1f, 0.3f));
		sequence.Insert(0.1f, midPanel.DOScaleY(1f, 0.3f));
		sequence.Insert(0.2f, innerPanel.DOScaleY(1f, 0.3f));
		for (int i = 0; i < buttons.Length; i++)
		{
			sequence.Insert(0.6f, buttons[i].DOScale(Vector3.one, 0.3f));
		}
		for (int j = 0; j < flagAndText.Length; j++)
		{
			sequence.Insert(0.4f, flagAndText[j].DOScale(Vector3.one, 0.3f));
		}
		for (int k = 0; k < optionsbtns.Length; k++)
		{
			sequence.Insert(0.45f, optionsbtns[k].DOScale(Vector3.one, 0.3f));
		}
		sequence.SetLoops(1).SetUpdate(isIndependentUpdate: true);
	}

	public void resetTransition()
	{
		topPanel.localScale = new Vector3(0f, 1f, 1f);
		midPanel.localScale = new Vector3(1f, 0f, 1f);
		innerPanel.localScale = new Vector3(1f, 0f, 1f);
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].localScale = Vector3.zero;
		}
		for (int j = 0; j < flagAndText.Length; j++)
		{
			flagAndText[j].localScale = Vector3.zero;
		}
		for (int k = 0; k < optionsbtns.Length; k++)
		{
			optionsbtns[k].localScale = Vector3.zero;
		}
	}
}
