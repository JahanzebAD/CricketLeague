using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BoundaryAnimation : Singleton<BoundaryAnimation>
{
	public GameObject holder;

	public Sprite[] resultsprites;

	public Image ResultImage;

	public Transform ResultBG;

	public Transform[] shine;

	public Transform[] shineRef;

	public Image bg;

	private TweenCallback DisableAll;

	private void Start()
	{
		HideMe();
	}

	public void ResetPanel()
	{
		ResultImage.transform.DOLocalRotate(new Vector3(0f, 180f, 0f), 0f, RotateMode.WorldAxisAdd).SetUpdate(isIndependentUpdate: true);
		ResultImage.transform.localScale = new Vector3(3f, 3f, 3f);
		ResultBG.localScale = new Vector3(5f, 5f, 5f);
		bg.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		for (int i = 0; i < shine.Length; i++)
		{
			shine[i].DOLocalMove(shineRef[i].localPosition, 0f);
		}
	}

	public void PanelTransition()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0.4f, ResultImage.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.3f));
		sequence.Insert(0.2f, ResultImage.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.2f, ResultBG.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.65f, ResultImage.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 1.7f));
		sequence.Insert(2.2f, ResultBG.DOScale(new Vector3(5f, 5f, 5f), 0.5f));
		sequence.Insert(2.2f, ResultImage.transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f));
		sequence.Insert(0.3f, bg.DOFade(1f, 1.7f));
		sequence.Insert(0.2f, shine[0].DOLocalMoveX(0f, 0.5f));
		sequence.Insert(0.2f, shine[1].DOLocalMoveX(-489f, 0.5f));
		sequence.Insert(0.2f, shine[3].DOLocalMoveX(0f, 0.5f));
		sequence.Insert(0.2f, shine[2].DOLocalMoveX(489f, 0.5f));
		sequence.Insert(0.7f, shine[0].DOLocalMoveX(-200f, 5f));
		sequence.Insert(0.7f, shine[1].DOLocalMoveX(-789f, 5f));
		sequence.Insert(0.7f, shine[3].DOLocalMoveX(200f, 5f));
		sequence.Insert(0.7f, shine[2].DOLocalMoveX(789f, 5f));
		sequence.Insert(2.2f, shine[0].DOLocalMoveX(-5600f, 0.5f));
		sequence.Insert(2.2f, shine[1].DOLocalMoveX(-5600f, 0.5f));
		sequence.Insert(2.2f, shine[2].DOLocalMoveX(5600f, 0.5f));
		sequence.Insert(2.2f, shine[3].DOLocalMoveX(5600f, 0.5f));
		sequence.Insert(2f, bg.DOFade(0f, 1.1f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}

	public void ShowMe(int index)
	{
		ResultImage.sprite = resultsprites[index];
		PanelTransition();
		holder.SetActive(value: true);
	}

	public void HideMe()
	{
		holder.SetActive(value: false);
		ResetPanel();
	}
}
