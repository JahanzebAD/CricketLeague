using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Follow_Anim : Singleton<Follow_Anim>
{
	public Image Bird;

	public void loop1()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(0f, Bird.transform.DORotate(new Vector3(0f, 0f, 0f), 0f));
		s.Insert(1.9f, Bird.transform.DORotate(new Vector3(0f, 0f, -30f), 0f));
		s.Insert(2f, Bird.transform.DORotate(new Vector3(0f, 0f, 0f), 3f));
	}
}
