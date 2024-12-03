using DG.Tweening;
using UnityEngine;

public class TeamSelectionPanelTransition : Singleton<TeamSelectionPanelTransition>
{
	public Transform outerPanel;

	public Transform topPanel;

	public Transform midPanel;

	public Transform flagBG;

	public Transform flagWhiteBG;

	public Transform overPanel;

	public Transform difficultyPanel;

	public Transform[] buttons;

	public Transform flag;

	public Transform[] arrows;

	public void panelTransition()
	{
		resetTransition();
		Sequence s = DOTween.Sequence();
		s.Append(outerPanel.DOScale(Vector3.one, 0.3f));
		s.Insert(0.2f, topPanel.DOScaleX(1f, 0.3f));
		s.Insert(0.2f, midPanel.DOScaleY(1f, 0.3f));
		s.Insert(0.3f, flagBG.DOScale(new Vector3(1f, 1f, 1f), 0.25f));
		s.Insert(0.25f, flagWhiteBG.DOScaleX(1f, 0.25f));
		s.Insert(0.3f, overPanel.DOScaleY(1f, 0.3f));
		s.Insert(0.3f, difficultyPanel.DOScaleY(1f, 0.4f));
		for (int i = 0; i < buttons.Length; i++)
		{
			s.Insert(0.4f, buttons[i].DOScale(Vector3.one, 0.3f));
		}
		s.Insert(0.55f, flag.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.2f));
		Transform[] array = arrows;
		foreach (Transform target in array)
		{
			s.Insert(0.55f, target.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.2f));
		}
	}

	public void resetTransition()
	{
		outerPanel.localScale = new Vector3(0f, 0f, 0f);
		topPanel.localScale = new Vector3(0f, 1f, 1f);
		midPanel.localScale = new Vector3(1f, 0f, 1f);
		flagWhiteBG.localScale = new Vector3(0f, 1f, 1f);
		flagBG.localScale = Vector3.zero;
		overPanel.localScale = new Vector3(1f, 0f, 1f);
		difficultyPanel.localScale = new Vector3(1f, 0f, 1f);
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].localScale = Vector3.zero;
		}
	}
}
