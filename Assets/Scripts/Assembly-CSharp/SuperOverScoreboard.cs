using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SuperOverScoreboard : Singleton<SuperOverScoreboard>
{
	public GameObject holder;

	public Transform scoreCard;

	public Transform wonText;

	public Text target;

	public Text type;

	public Text ballsRemaining;

	public Text wicketsLeft;

	public Text fromText;

	public Text hitText;

	public void UpdateScoreboard()
	{
		if (CONTROLLER.PlayModeSelected == 4)
		{
			if (CONTROLLER.LevelId == 0 || CONTROLLER.LevelId == 1)
			{
				hitText.text = LocalizationData.instance.getText(278);
				hitText.text = ReplaceText(hitText.text, (10 - CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores).ToString());
			}
			else if (CONTROLLER.LevelId == 2 || CONTROLLER.LevelId == 3)
			{
				hitText.text = LocalizationData.instance.getText(279);
				hitText.text = ReplaceText(hitText.text, (3 - CONTROLLER.totalFours).ToString());
			}
			else if (CONTROLLER.LevelId == 4 || CONTROLLER.LevelId == 5)
			{
				hitText.text = LocalizationData.instance.getText(281);
				hitText.text = ReplaceText(hitText.text, (3 - CONTROLLER.continousBoundaries).ToString());
			}
			else if (CONTROLLER.LevelId == 6 || CONTROLLER.LevelId == 7)
			{
				hitText.text = LocalizationData.instance.getText(280);
				hitText.text = ReplaceText(hitText.text, (3 - CONTROLLER.totalSixes).ToString());
			}
			else if (CONTROLLER.LevelId == 8 || CONTROLLER.LevelId == 9)
			{
				hitText.text = LocalizationData.instance.getText(279);
				hitText.text = ReplaceText(hitText.text, (5 - CONTROLLER.totalFours).ToString());
			}
			else if (CONTROLLER.LevelId == 10 || CONTROLLER.LevelId == 11)
			{
				hitText.text = LocalizationData.instance.getText(278);
				hitText.text = ReplaceText(hitText.text, (25 - CONTROLLER.TeamList[CONTROLLER.myTeamIndex].currentMatchScores).ToString());
			}
			else if (CONTROLLER.LevelId == 12 || CONTROLLER.LevelId == 13)
			{
				hitText.text = LocalizationData.instance.getText(282);
				hitText.text = ReplaceText(hitText.text, (4 - CONTROLLER.continousSixes).ToString());
			}
			else if (CONTROLLER.LevelId == 14 || CONTROLLER.LevelId == 15)
			{
				hitText.text = LocalizationData.instance.getText(279);
				hitText.text = ReplaceText(hitText.text, (6 - CONTROLLER.continousBoundaries).ToString());
			}
			else if (CONTROLLER.LevelId == 16 || CONTROLLER.LevelId == 17)
			{
				hitText.text = LocalizationData.instance.getText(280);
				hitText.text = ReplaceText(hitText.text, (6 - CONTROLLER.continousSixes).ToString());
			}
			fromText.text = LocalizationData.instance.getText(285);
			fromText.text = ReplaceText(fromText.text, (5 - Singleton<GameModel>.instance.currentBall).ToString());
		}
		else
		{
			hitText.text = LocalizationData.instance.getText(278);
			hitText.text = ReplaceText(hitText.text, (CONTROLLER.TargetToChase - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores).ToString());
			fromText.text = LocalizationData.instance.getText(285);
			fromText.text = ReplaceText(fromText.text, (CONTROLLER.totalOvers * 6 - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls).ToString());
		}
		wicketsLeft.text = (CONTROLLER.totalWickets - CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchWickets).ToString();
		if (int.Parse(target.text.ToString()) <= 0)
		{
			Sequence s = DOTween.Sequence();
			s.Insert(0f, scoreCard.DOScaleX(0f, 0.35f));
			s.Insert(0.35f, wonText.DOScaleX(1f, 0.35f));
		}
	}

	private string ReplaceText(string original, string replace)
	{
		string result = string.Empty;
		Debug.Log(original + " " + replace + " ");
		if (original.Contains("#"))
		{
			result = original.Replace("#", replace);
		}
		return result;
	}
}
