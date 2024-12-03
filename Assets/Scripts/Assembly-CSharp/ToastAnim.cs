using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ToastAnim : Singleton<ToastAnim>
{
	public Transform holder;

	public Image glow;

	public GameObject dummy;

	public Text message;

	private bool inProgress;

	private Tweener mainTween;

	private Tweener glowTween;

	private void Start()
	{
		holder.DOLocalMove(new Vector3(0f, -550f, 0f), 0f).SetUpdate(isIndependentUpdate: true);
	}

	private void ResetAnim()
	{
		dummy.transform.DOScale(0f, 0f).SetUpdate(isIndependentUpdate: true);
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, holder.DOLocalMove(new Vector3(0f, -320f, 0f), 0.1f));
		sequence.Insert(0.1f, holder.DOLocalMove(new Vector3(0f, -550f, 0f), 0.5f)).OnComplete(ResetGlow);
		sequence.SetUpdate(isIndependentUpdate: true);
	}

	private void ResetGlow()
	{
		inProgress = false;
		glowTween = glow.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
	}

	public void StartAnim()
	{
		if (!inProgress)
		{
			mainTween = holder.DOLocalMove(new Vector3(0f, -340f, 0f), 0.5f).SetUpdate(isIndependentUpdate: true);
			glowTween = glow.DOFade(1f, 0.5f).SetLoops(4, LoopType.Yoyo).SetUpdate(isIndependentUpdate: true);
			dummy.transform.DOScale(1f, 3f).SetUpdate(isIndependentUpdate: true).OnComplete(ResetAnim);
			inProgress = true;
		}
	}
}
