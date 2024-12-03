using DG.Tweening;
using UnityEngine;

public class UIScroller : Singleton<UIScroller>
{
	private float initialYPos;

	private float initialHeight;

	[SerializeField]
	private RectTransform CreditsText;

	private void OnEnable()
	{
		initialYPos = CreditsText.anchoredPosition.y;
		initialHeight = CreditsText.rect.height;
		MoveAnimation();
	}

	private void MoveAnimation()
	{
		CreditsText.DOLocalMoveY(CreditsText.rect.height + CreditsText.anchoredPosition.y / 2f, 20f, snapping: true).OnComplete(ResetPosition).SetEase(Ease.Linear);
	}

	private void ResetPosition()
	{
		CreditsText.anchoredPosition = new Vector2(0f, initialYPos);
		MoveAnimation();
	}

	private void OnDisable()
	{
		CreditsText.anchoredPosition = new Vector2(0f, initialYPos);
		DOTween.Kill(CreditsText);
	}
}
