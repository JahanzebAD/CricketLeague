using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoundaryAnimationNew : Singleton<BoundaryAnimationNew>
{
	public GameObject Holder;

	public Transform[] bigFlareRef;

	public Transform[] bigFlare;

	public Transform[] smallFlare;

	public Transform[] smallFlareRef;

	public Image ResultContainer;

	public Image YellowBG;

	public Image Shadow;

	public Image[] RedImage;

	public Image Result;

	private TweenCallback AnimationCompletedCallback;

	public Sprite[] ResultSprites;

	public float fadeTime;

	private void Start()
	{
		Holder.SetActive(value: false);
		AnimationCompletedCallback = delegate
		{
			HideMe();
		};
	}

	private void ResetTransition()
	{
		int num = 0;
		for (num = 0; num < bigFlare.Length; num++)
		{
			bigFlare[num].localPosition = bigFlareRef[num].localPosition;
			smallFlare[num].localPosition = smallFlareRef[num].localPosition;
		}
		ResultContainer.transform.localScale = new Vector3(5f, 5f, 5f);
		ResultContainer.transform.DOLocalRotate(new Vector3(0f, 0f, 90f), 0f).SetUpdate(isIndependentUpdate: true);
		YellowBG.rectTransform.DOSizeDelta(new Vector2(50.7f, 337.1f), 0f).SetUpdate(isIndependentUpdate: true);
		YellowBG.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		Shadow.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		RedImage[0].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		RedImage[1].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		Result.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
	}

	private void PanelTransition()
	{
		ResetTransition();
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, YellowBG.DOFade(1f, fadeTime));
		sequence.Insert(0f, RedImage[0].DOFade(1f, fadeTime));
		sequence.Insert(0f, RedImage[1].DOFade(1f, fadeTime));
		sequence.Insert(0f, ResultContainer.transform.DOLocalRotate(Vector3.zero, 0.2f));
		sequence.Insert(0f, ResultContainer.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.2f, Shadow.DOFade(1f, 0f));
		sequence.Insert(0.2f, Result.DOFade(1f, 0f));
		sequence.Insert(0.2f, YellowBG.DOFade(1f, 0f));
		sequence.Insert(0.2f, YellowBG.rectTransform.DOSizeDelta(new Vector2(556.6f, 337.1f), 0.2f));
		sequence.Insert(0.18f, ResultContainer.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 2.9f));
		sequence.Insert(2.9f, YellowBG.rectTransform.DOSizeDelta(new Vector2(50.7f, 337.1f), 0.2f));
		sequence.Insert(3.1f, Shadow.DOFade(0f, 0f));
		sequence.Insert(3.2f, ResultContainer.transform.DOLocalRotate(new Vector3(0f, 0f, 90f), 0.2f));
		sequence.Insert(3.2f, YellowBG.DOFade(0f, fadeTime));
		sequence.Insert(3.2f, RedImage[0].DOFade(0f, fadeTime));
		sequence.Insert(3.2f, RedImage[1].DOFade(0f, fadeTime));
		sequence.Insert(3.2f, ResultContainer.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.2f));
		sequence.InsertCallback(3.4f, AnimationCompletedCallback);
		sequence.SetUpdate(isIndependentUpdate: true);
		sequence.SetLoops(1);
	}

	public void ShowMe(int index)
	{
		Result.sprite = ResultSprites[index];
		Holder.SetActive(value: true);
		PanelTransition();
	}

	public void HideMe()
	{
		Holder.SetActive(value: false);
		ResetTransition();
	}
}
