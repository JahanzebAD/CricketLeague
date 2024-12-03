using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardAnim : MonoBehaviour
{
	public Image bar1;

	public Image bar2;

	public Image bar3;

	public Image star1;

	public Image star2;

	public Image star3;

	private void Start()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.AppendInterval(5f);
		sequence.Append(bar1.transform.DOScaleX(1f, 0.7f));
		sequence.Append(bar2.transform.DOScaleX(1f, 0.7f));
		sequence.Append(bar3.transform.DOScaleX(1f, 0.7f));
		sequence.SetLoops(-1);
	}
}
