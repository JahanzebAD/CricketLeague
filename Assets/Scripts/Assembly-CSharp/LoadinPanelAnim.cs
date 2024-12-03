using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadinPanelAnim : Singleton<LoadinPanelAnim>
{
	public Image[] ball;

	public GameObject holder;

	public void StartAnim()
	{
		holder.SetActive(value: true);
	}

	public void ResetAnim()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(0f, ball[0].DOFade(0f, 0f));
		s.Insert(0f, ball[1].DOFade(0f, 0f));
		s.Insert(0f, ball[2].DOFade(0f, 0f));
	}

	public void StartAnim1()
	{
		InvokeRepeating("Animation", 0f, 1.8f);
	}

	public void Animation()
	{
		ResetAnim1();
		ball[0].gameObject.SetActive(value: true);
		ball[1].gameObject.SetActive(value: true);
		ball[2].gameObject.SetActive(value: true);
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, ball[0].DOFade(1f, 0.4f));
		sequence.Insert(0.4f, ball[1].DOFade(1f, 0.4f));
		sequence.Insert(0.8f, ball[2].DOFade(1f, 0.4f));
		sequence.Insert(1.2f, ball[0].DOFade(0f, 0.2f));
		sequence.Insert(1.4f, ball[1].DOFade(0f, 0.2f));
		sequence.Insert(1.6f, ball[2].DOFade(0f, 0.2f));
		sequence.SetLoops(-1);
	}

	public void ResetAnim1()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(0f, ball[0].DOFade(0f, 0f));
		s.Insert(0f, ball[1].DOFade(0f, 0f));
		s.Insert(0f, ball[2].DOFade(0f, 0f));
	}

	public void CancelAnim()
	{
		holder.SetActive(value: false);
	}
}
