using DG.Tweening;
using UnityEngine;

public class TransitionWorldCup : Singleton<TransitionWorldCup>
{
	public Transform t20Box;

	public Transform wcBox;

	public Transform mainPanel;

	public Transform sideImg;

	private Vector3 startPos = new Vector3(453f, 30f);

	private Vector3 endPos = new Vector3(-136f, -58f);

	public void ResetPositions()
	{
		sideImg.DOLocalMove(endPos, 0f);
		mainPanel.transform.localScale = new Vector3(0f, 1f, 1f);
		AnimateScreen();
	}

	private void AnimateScreen()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(0f, sideImg.DOLocalMove(startPos, 0.5f));
		s.Insert(0.5f, mainPanel.DOScaleX(1f, 0.25f));
	}
}
