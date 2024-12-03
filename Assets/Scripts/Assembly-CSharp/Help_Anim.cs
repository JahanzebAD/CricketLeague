using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Help_Anim : Singleton<Help_Anim>
{
	public Image QuestionMark;

	public void loop1()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(1f, QuestionMark.transform.DORotate(new Vector3(0f, 0f, -20f), 1f));
		s.Insert(2f, QuestionMark.transform.DOLocalRotate(new Vector3(0f, 0f, 20f), 2f));
		s.Insert(3f, QuestionMark.transform.DORotate(new Vector3(0f, 0f, -20f), 1f));
		s.Insert(4f, QuestionMark.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 1f));
	}
}
