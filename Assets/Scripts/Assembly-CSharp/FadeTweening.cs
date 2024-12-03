using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeTweening : MonoBehaviour
{
	private Tweener tween;

	private void Start()
	{
		FadeIn();
	}

	private void FadeOut()
	{
		tween = GetComponent<Image>().DOFade(0.2f, 1f).OnComplete(FadeIn);
	}

	private void FadeIn()
	{
		tween = GetComponent<Image>().DOFade(1f, 1f).OnComplete(FadeOut);
	}
}
