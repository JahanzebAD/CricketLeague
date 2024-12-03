using DG.Tweening;
using UnityEngine;

public class MoveBG : MonoBehaviour
{
	private Tweener tween;

	public GameObject BG;

	private void OnEnable()
	{
		StartAnimRight();
	}

	private void StartAnimRight()
	{
		tween = BG.transform.DOLocalMoveX(-150f, 60f).OnComplete(StartAnimLeft);
	}

	private void StartAnimLeft()
	{
		tween = BG.transform.DOLocalMoveX(160f, 60f).OnComplete(StartAnimRight);
	}
}
