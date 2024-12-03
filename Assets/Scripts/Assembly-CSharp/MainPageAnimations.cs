using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainPageAnimations : MonoBehaviour
{
	public Image cart;

	private void Start()
	{
		CartAnimationStart();
	}

	private void OnDisable()
	{
	}

	private void CartAnimationStart()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(0f, cart.transform.DOLocalMoveX(-50f, 0f));
		s.Insert(0f, cart.DOFade(0f, 0f));
		s.Insert(0.1f, cart.transform.DOLocalMoveX(0f, 1f));
		s.Insert(0.1f, cart.DOFade(1f, 0.5f));
		s.Insert(0.5f, cart.DOFade(1f, 2.5f)).OnComplete(CartAnimationStop);
	}

	private void CartAnimationStop()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(0.1f, cart.transform.DOLocalMoveX(50f, 0.5f));
		s.Insert(0.1f, cart.DOFade(0f, 0.5f));
		s.Insert(0.5f, cart.DOFade(0f, 0.2f)).OnComplete(CartAnimationStart);
	}
}
