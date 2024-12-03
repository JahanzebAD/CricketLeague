using DG.Tweening;
using UnityEngine;

public class AchievementsPanelTransition : Singleton<AchievementsPanelTransition>
{
	public Transform MidContent;

	public Transform BottomContent;

	public Transform topPanel;

	public Transform midPanel;

	public Transform[] achievements;

	private Transform[] bluePanels;

	private Transform[] whitePanels;

	private Transform[] ribbons;

	private Transform[] claimBtns;

	private Transform[] starProgress;

	public Transform[] kits;

	private Transform[] innerTab;

	private Transform[] bottomClaim;

	private float seqTime;

	private void Start()
	{
		bluePanels = new Transform[15];
		whitePanels = new Transform[15];
		ribbons = new Transform[15];
		claimBtns = new Transform[15];
		starProgress = new Transform[15];
		bottomClaim = new Transform[6];
		innerTab = new Transform[6];
		for (int i = 0; i < achievements.Length; i++)
		{
			bluePanels[i] = achievements[i].GetChild(0);
			whitePanels[i] = achievements[i].GetChild(1);
			ribbons[i] = achievements[i].GetChild(3);
			claimBtns[i] = whitePanels[i].GetChild(0);
			starProgress[i] = bluePanels[i].GetChild(2);
			if (i < 6)
			{
				innerTab[i] = kits[i].GetChild(1);
				bottomClaim[i] = innerTab[i].GetChild(0);
			}
		}
	}

	public void ResetTransition()
	{
		MidContent.localPosition = Vector3.zero;
		BottomContent.localPosition = Vector3.zero;
		topPanel.localScale = new Vector3(0f, 1f, 1f);
		midPanel.localScale = new Vector3(1f, 0f, 1f);
		Transform[] array = bluePanels;
		foreach (Transform transform in array)
		{
			transform.localScale = new Vector3(1f, 0f, 1f);
		}
		Transform[] array2 = whitePanels;
		foreach (Transform transform2 in array2)
		{
			transform2.localScale = new Vector3(0f, 1f, 1f);
		}
		Transform[] array3 = ribbons;
		foreach (Transform transform3 in array3)
		{
			transform3.localScale = new Vector3(1f, 0f, 1f);
		}
		Transform[] array4 = starProgress;
		foreach (Transform transform4 in array4)
		{
			transform4.localScale = new Vector3(0f, 1f, 1f);
		}
		Transform[] array5 = kits;
		foreach (Transform transform5 in array5)
		{
			transform5.localScale = new Vector3(0f, 1f, 1f);
		}
		Transform[] array6 = innerTab;
		foreach (Transform transform6 in array6)
		{
			transform6.localScale = new Vector3(1f, 0f, 1f);
		}
	}

	public void PanelTransition()
	{
		ResetTransition();
		Sequence s = DOTween.Sequence();
		s.Append(topPanel.DOScaleX(1f, 0.4f));
		s.Insert(0f, midPanel.DOScaleY(1f, 0.4f));
		seqTime = 0.2f;
		Transform[] array = bluePanels;
		foreach (Transform target in array)
		{
			seqTime += 0.2f;
			s.Insert(seqTime, target.DOScaleY(1f, 0.3f));
		}
		seqTime = 0.4f;
		Transform[] array2 = whitePanels;
		foreach (Transform target2 in array2)
		{
			seqTime += 0.2f;
			s.Insert(seqTime, target2.DOScaleX(1f, 0.3f));
		}
		seqTime = 0.6f;
		Transform[] array3 = ribbons;
		foreach (Transform target3 in array3)
		{
			seqTime += 0.2f;
			s.Insert(seqTime, target3.DOScaleY(1f, 0.3f));
		}
		seqTime = 0.6f;
		Transform[] array4 = starProgress;
		foreach (Transform target4 in array4)
		{
			seqTime += 0.2f;
			s.Insert(seqTime, target4.DOScaleX(1f, 0.3f));
		}
		seqTime = 0.8f;
		Transform[] array5 = claimBtns;
		foreach (Transform target5 in array5)
		{
			seqTime += 0.1f;
			s.Insert(seqTime, target5.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f), 0.3f, 0));
		}
		seqTime = 0.2f;
		Transform[] array6 = kits;
		foreach (Transform target6 in array6)
		{
			seqTime += 0.3f;
			s.Insert(seqTime, target6.DOScaleX(1f, 0.3f));
		}
		seqTime = 0.4f;
		Transform[] array7 = innerTab;
		foreach (Transform target7 in array7)
		{
			seqTime += 0.2f;
			s.Insert(seqTime, target7.DOScaleY(1f, 0.3f));
		}
		seqTime = 0.8f;
		Transform[] array8 = bottomClaim;
		foreach (Transform target8 in array8)
		{
			seqTime += 0.1f;
			s.Insert(seqTime, target8.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f), 0.3f, 0));
		}
	}
}
