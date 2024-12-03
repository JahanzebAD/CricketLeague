using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNamePanelTransition : Singleton<PlayerNamePanelTransition>
{
	public Transform leftPanel;

	public Transform rightPanel;

	public Text[] texts;

	private void Start()
	{
	}

	public void panelTransition()
	{
		resetTransition();
		Sequence s = DOTween.Sequence();
		s.Append(leftPanel.DOScaleY(1f, 0.5f));
		s.Insert(0f, rightPanel.DOScale(1f, 0.5f));
		for (int i = 0; i < texts.Length; i++)
		{
			texts[i].DOFade(1f, 1f);
		}
	}

	public void resetTransition()
	{
		leftPanel.localScale = new Vector3(1f, 0f, 1f);
		rightPanel.localScale = new Vector3(1f, 0f, 1f);
		for (int i = 0; i < texts.Length; i++)
		{
			texts[i].DOFade(0f, 0f);
		}
	}
}
