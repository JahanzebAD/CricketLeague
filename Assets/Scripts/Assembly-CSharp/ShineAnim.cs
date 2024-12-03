using DG.Tweening;
using UnityEngine;

public class ShineAnim : Singleton<ShineAnim>
{
	public Transform shine;

	private Tweener tween;

	public void StartAnim()
	{
		tween = shine.DOLocalMoveX(247f, 1.5f).OnComplete(StopAnim);
	}

	public void CancelAnim()
	{
		CancelInvoke("StartAnim");
	}

	public void StopAnim()
	{
		tween = shine.DOLocalMoveX(-227f, 0f);
		Invoke("StartAnim", 10f);
	}
}
