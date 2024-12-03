using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Rate_Anim : Singleton<Rate_Anim>
{
	public Image Star;

	public void loop1()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(0f, Star.transform.DOScale(new Vector3(1f, 1f, 1f), 0f));
		s.Insert(0.9f, Star.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0f));
		s.Insert(1f, Star.transform.DOLocalRotate(new Vector3(0f, 0f, 720f), 3f, RotateMode.FastBeyond360));
		s.Insert(1f, Star.transform.DOScale(new Vector3(1f, 1f, 1f), 3f));
		s.Insert(5f, Star.transform.DOScale(new Vector3(1f, 1f, 1f), 0f));
		Invoke("RepeatInGameMode", 5f);
	}

	private void RepeatInGameMode()
	{
		Singleton<GameModeTWO>.instance.ResetTab1 = false;
	}
}
