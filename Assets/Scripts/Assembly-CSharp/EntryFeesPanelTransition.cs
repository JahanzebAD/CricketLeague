using DG.Tweening;
using UnityEngine;

public class EntryFeesPanelTransition : Singleton<EntryFeesPanelTransition>
{
	public Transform TopPanel;

	public Transform mainPanel;

	public Transform[] CoinPackages;

	public Transform[] CoinButtons;

	public Transform[] RVButtons;

	public void ResetTransition()
	{
		TopPanel.localScale = new Vector3(0f, 1f, 1f);
		mainPanel.localScale = new Vector3(1f, 0f, 1f);
	}

	public void PanelTransition()
	{
		ResetTransition();
		Sequence s = DOTween.Sequence();
		s.Append(mainPanel.DOScaleY(1f, 0.3f));
		s.Insert(0.05f, TopPanel.DOScaleX(1f, 0.3f));
		Transform[] coinPackages = CoinPackages;
		foreach (Transform transform in coinPackages)
		{
			if (transform != null)
			{
				s.Insert(0.2f, transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f, 0));
				s.Insert(0.5f, transform.DOScale(new Vector3(1f, 1f, 1f), 0.15f));
			}
		}
		Transform[] coinButtons = CoinButtons;
		foreach (Transform transform2 in coinButtons)
		{
			if (transform2 != null)
			{
				s.Insert(0.4f, transform2.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f, 0));
				s.Insert(0.7f, transform2.DOScale(new Vector3(1f, 1f, 1f), 0.15f));
			}
		}
		Transform[] rVButtons = RVButtons;
		foreach (Transform transform3 in rVButtons)
		{
			if (transform3 != null)
			{
				s.Insert(0.4f, transform3.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f, 0));
				s.Insert(0.7f, transform3.DOScale(new Vector3(1f, 1f, 1f), 0.15f));
			}
		}
	}
}
