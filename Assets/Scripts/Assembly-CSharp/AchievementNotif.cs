using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AchievementNotif : Singleton<AchievementNotif>
{
	public Text almostThereText;

	public Text title;

	public Text goal;

	public Image outerGlow;

	public GameObject almostTherePopup;

	public GameObject achievementPopup;

	private Tweener tween;

	public void AlmostTherePopup(string detail, int index)
	{
		if (CONTROLLER.PlayModeSelected != 5 && CONTROLLER.PlayModeSelected != 4)
		{
			string text = AchievementTable.AchievementGoal[index];
			goal.text = text;
			almostThereText.text = detail;
			tween = outerGlow.DOFade(1f, 0.7f).SetLoops(-1, LoopType.Yoyo);
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(4.4f);
			sequence.Insert(0f, almostTherePopup.transform.DOScale(new Vector3(1f, 1f, 0f), 0.5f));
			sequence.SetLoops(1).SetUpdate(isIndependentUpdate: true);
			sequence.OnComplete(HideAlmostTherePopup);
		}
	}

	public void AchievedPopup(int index)
	{
		if (CONTROLLER.PlayModeSelected != 5 && CONTROLLER.PlayModeSelected != 4)
		{
			string text = AchievementTable.AchievementName[index];
			title.text = text;
			Sequence sequence = DOTween.Sequence();
			sequence.AppendInterval(4.4f);
			sequence.Insert(0f, achievementPopup.transform.DOScale(new Vector3(0.5f, 0.5f, 0f), 0.5f));
			sequence.SetLoops(1).SetUpdate(isIndependentUpdate: true);
			sequence.OnComplete(HideAchievedPopup);
		}
	}

	public void HideAchievedPopup()
	{
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, achievementPopup.transform.DOScale(Vector3.zero, 0.4f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}

	public void HideAlmostTherePopup()
	{
		tween = outerGlow.DOFade(0f, 0.4f).SetLoops(1);
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, almostTherePopup.transform.DOScale(Vector3.zero, 0.4f));
		sequence.Insert(0f, outerGlow.DOFade(0f, 0.4f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}
}
