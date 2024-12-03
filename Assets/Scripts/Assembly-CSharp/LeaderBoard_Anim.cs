using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard_Anim : Singleton<LeaderBoard_Anim>
{
	public Image[] bar;

	public Image[] star;

	public void loop1()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(0f, bar[2].transform.DOScale(new Vector3(1f, 1f, 1f), 0f));
		s.Insert(0f, bar[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0f));
		s.Insert(0f, bar[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0f));
		s.Insert(0f, star[2].transform.DOScale(new Vector3(1f, 1f, 1f), 0f));
		s.Insert(0f, star[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0f));
		s.Insert(0f, star[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0f));
		s.Insert(1f, bar[2].transform.DOScale(new Vector3(1f, 0f, 1f), 0f));
		s.Insert(1f, bar[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f));
		s.Insert(1f, bar[1].transform.DOScale(new Vector3(1f, 0f, 1f), 0f));
		s.Insert(1f, star[2].transform.DOScale(new Vector3(0f, 0f, 0f), 0f));
		s.Insert(1f, star[0].transform.DOScale(new Vector3(0f, 0f, 0f), 0f));
		s.Insert(1f, star[1].transform.DOScale(new Vector3(0f, 0f, 0f), 0f));
		s.Insert(1.1f, bar[2].transform.DOScale(new Vector3(1f, 1f, 1f), 1f));
		s.Insert(2f, bar[0].transform.DOScale(new Vector3(1f, 1f, 1f), 1f));
		s.Insert(3f, bar[1].transform.DOScale(new Vector3(1f, 1f, 1f), 1f));
		s.Insert(2f, star[2].transform.DOScale(new Vector3(1f, 1f, 1f), 1f));
		s.Insert(3f, star[0].transform.DOScale(new Vector3(1f, 1f, 1f), 1f));
		s.Insert(4f, star[1].transform.DOScale(new Vector3(1f, 1f, 1f), 1f));
	}
}
