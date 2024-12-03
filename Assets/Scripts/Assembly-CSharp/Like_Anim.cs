using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Like_Anim : Singleton<Like_Anim>
{
	public Image Hand;

	private void Start()
	{
		loop1();
	}

	public void loop1()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(2f, Hand.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0f), 1f, 3));
		s.Insert(5f, Hand.transform.DOScale(new Vector3(1f, 1f, 1f), 0f));
	}
}
