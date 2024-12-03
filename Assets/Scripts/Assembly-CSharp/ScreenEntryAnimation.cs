using DG.Tweening;
using UnityEngine;

public class ScreenEntryAnimation : Singleton<ScreenEntryAnimation>
{
	public GameObject holder;

	private Tweener tween;

	public void SwipeScreen()
	{
		tween = holder.transform.DOLocalMoveX(0f, 0.6f);
	}
}
