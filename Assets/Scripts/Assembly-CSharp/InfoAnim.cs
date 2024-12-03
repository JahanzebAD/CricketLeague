using DG.Tweening;
using UnityEngine.UI;

public class InfoAnim : Singleton<InfoAnim>
{
	public Image dot;

	public void loop1()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(0f, dot.transform.DOLocalMoveY(23f, 0.5f));
		s.Insert(0f, dot.transform.DOScaleX(0.95f, 0.45f));
		s.Insert(0f, dot.transform.DOScaleY(1.05f, 0.45f));
		s.Insert(0.45f, dot.transform.DOScaleX(1.1f, 0.05f));
		s.Insert(0.45f, dot.transform.DOScaleY(0.9f, 0.05f));
		Invoke("loop2", 0.6f);
	}

	private void loop2()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(0f, dot.transform.DOScaleY(1.05f, 0.05f));
		s.Insert(0f, dot.transform.DOScaleX(0.95f, 0.05f));
		s.Insert(0f, dot.transform.DOLocalMoveY(16f, 0.5f));
		s.Insert(0.45f, dot.transform.DOScaleX(1.1f, 0.05f));
		s.Insert(0.45f, dot.transform.DOScaleY(0.9f, 0.05f));
		s.Insert(0.5f, dot.transform.DOScaleX(1f, 0.05f));
		s.Insert(0.5f, dot.transform.DOScaleY(1f, 0.05f));
	}
}
