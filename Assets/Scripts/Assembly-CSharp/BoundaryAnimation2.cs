using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoundaryAnimation2 : Singleton<BoundaryAnimation2>
{
	public Image result;

	public Sprite[] resultSprites;

	public GameObject Holder;

	public Transform[] bigFlareRef;

	public Transform[] bigFlare;

	public Transform[] smallFlare;

	public Transform[] smallFlareRef;

	private TweenCallback AnimationCompletedCallback;

	private void Start()
	{
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
		result.transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 0f).SetUpdate(isIndependentUpdate: true);
		result.transform.localScale = new Vector3(0f, 1.5f, 1.5f);
	}

	public void PanelTransition()
	{
		ResetTransition();
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, result.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.3f));
		sequence.Insert(0f, result.transform.DOScaleX(1.5f, 0.4f));
		sequence.Insert(0.35f, result.transform.DOScale(1.65f, 4.2f));
		sequence.InsertCallback(2.5f, AnimationCompletedCallback);
		sequence.SetUpdate(isIndependentUpdate: true);
		sequence.SetLoops(1);
	}

	public void ShowMe(int index)
	{
		result.sprite = resultSprites[index];
		Holder.SetActive(value: true);
		PanelTransition();
	}

	public void HideMe()
	{
		Holder.SetActive(value: false);
		ResetTransition();
	}
}
