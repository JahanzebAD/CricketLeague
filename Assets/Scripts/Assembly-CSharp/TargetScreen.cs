using UnityEngine;
using UnityEngine.UI;

public class TargetScreen : Singleton<TargetScreen>
{
	public Image LogoFlag;

	public Text okText;

	public Text TargetTxt;

	public GameObject holder;

	protected void Awake()
	{
		Hide(boolean: true);
	}

	public void addEventListener()
	{
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

	private string ReplaceText(string original, string replace1, string replace2)
	{
		string text = string.Empty;
		int index = 0;
		Debug.Log(original + " " + replace1 + " " + replace2 + " " + text);
		Debug.Log(replace1);
		if (LocalizationData.instance.refList.Contains(replace1.ToUpper()))
		{
			index = LocalizationData.instance.refList.IndexOf(replace1.ToUpper());
		}
		if (original.Contains("#"))
		{
			text = original.Replace("#", LocalizationData.instance.getText(index));
		}
		Debug.Log(original + " " + replace1 + " " + replace2 + " " + text);
		if (text.Contains("$"))
		{
			text = text.Replace("$", replace2);
		}
		Debug.Log(original + " " + replace1 + " " + replace2 + " " + text);
		return text;
	}

	public void UpdateTarget()
	{
		if (CONTROLLER.PlayModeSelected != 7)
		{
			CONTROLLER.TargetToChase = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores + 1;
		}
		if (CONTROLLER.BattingTeamIndex == CONTROLLER.opponentTeamIndex)
		{
			Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
			foreach (Sprite sprite in flags)
			{
				if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.myTeamIndex].abbrevation)
				{
					LogoFlag.sprite = sprite;
				}
			}
			okText.text = LocalizationData.instance.getText(231);
			TargetTxt.text = LocalizationData.instance.getText(256);
			TargetTxt.text = ReplaceText(TargetTxt.text, CONTROLLER.TargetToChase.ToString());
			return;
		}
		Sprite[] flags2 = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite2 in flags2)
		{
			if (sprite2.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].abbrevation)
			{
				LogoFlag.sprite = sprite2;
			}
		}
		okText.text = LocalizationData.instance.getText(232);
		TargetTxt.text = LocalizationData.instance.getText(410);
		TargetTxt.text = ReplaceText(TargetTxt.text, CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].teamName, (CONTROLLER.TargetToChase - 1).ToString());
	}

	public void Continue()
	{
		CONTROLLER.isAutoPlayed = false;
		Hide(boolean: true);
		CONTROLLER.currentInnings = 1;
		Singleton<GameModel>.instance.ResetVariables();
	}

	public void Hide(bool boolean)
	{
		if (boolean)
		{
			holder.SetActive(value: false);
			Singleton<StandbyCam>.instance.PauseTween();
		}
		else
		{
			Singleton<StandbyCam>.instance.RotateStandbyCam();
			UpdateTarget();
			holder.SetActive(value: true);
		}
	}
}
