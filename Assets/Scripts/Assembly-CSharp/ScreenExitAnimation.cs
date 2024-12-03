using DG.Tweening;
using UnityEngine;

public class ScreenExitAnimation : Singleton<ScreenExitAnimation>
{
	public GameObject holder;

	private Tweener tween;

	public void SwipeScreen()
	{
		tween = holder.transform.DOLocalMoveX(-2500f, 0.6f).OnComplete(Reset);
		holder.transform.DOScale(Vector3.zero, 0.6f);
	}

	private void Reset()
	{
		tween = holder.transform.DOLocalMoveX(0f, 0f);
		holder.transform.DOScale(Vector3.one, 0.6f);
	}
}
