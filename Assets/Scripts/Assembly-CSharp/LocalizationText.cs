using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : Singleton<LocalizationText>
{
	public Font[] fontAssets;

	public int index = -1;

	public int languageIndex = -1;

	private Text text;

	private bool textAvailable;

	private bool firstTime = true;

	public string tempValue = string.Empty;

	public int stringNoOfChar;

	public string defaultString;

	public void OnEnable()
	{
		if (firstTime)
		{
			defaultString = GetComponent<Text>().text;
			languageIndex = LocalizationData.instance.languageIndex;
			firstTime = false;
		}
		if (text == null)
		{
			text = GetComponent<Text>();
		}
		if (index == -2)
		{
			text.text = ReplaceOriginalText(GetComponent<Text>().text, stringNoOfChar);
			index = LocalizationData.instance.refList.IndexOf(text.text);
			text.text = LocalizationData.instance.getText(index);
			text.text = ReplaceText(text.text, tempValue);
			text.font = fontAssets[LocalizationData.instance.languageIndex];
			if (LocalizationData.instance.languageIndex != 1)
			{
				text.fontStyle = FontStyle.Normal;
			}
			return;
		}
		index = -1;
		if (defaultString == string.Empty)
		{
			if (LocalizationData.instance.refList.Contains(text.text.ToUpper()))
			{
				index = LocalizationData.instance.refList.IndexOf(text.text.ToUpper());
			}
		}
		else if (PlayerPrefs.HasKey(defaultString))
		{
			index = PlayerPrefs.GetInt(defaultString);
		}
		else if (LocalizationData.instance.refList.Contains(defaultString.ToUpper()))
		{
			Debug.Log("found" + index);
			index = LocalizationData.instance.refList.IndexOf(defaultString.ToUpper());
			PlayerPrefs.SetInt(defaultString, index);
		}
		text.font = fontAssets[LocalizationData.instance.languageIndex];
		text.fontStyle = FontStyle.Normal;
		text.alignByGeometry = true;
		if (LocalizationData.instance.languageIndex == 0)
		{
			text.lineSpacing = 1.5f;
		}
		else
		{
			text.lineSpacing = 1f;
		}
		Debug.Log(index + " " + text.text);
		if (index != -1)
		{
			Debug.Log(index + " " + text.text);
			text.text = LocalizationData.instance.getText(index);
		}
		if (index == 18)
		{
			text.text = LocalizationData.instance.getText(18) + " " + LocalizationData.instance.getText(19);
		}
		if (index == 124)
		{
			text.text = LocalizationData.instance.getText(124) + "!";
		}
		if (index == 20)
		{
			text.text = LocalizationData.instance.getText(20) + " " + LocalizationData.instance.getText(21);
		}
		if (index == 22)
		{
			text.text = LocalizationData.instance.getText(22) + " \n\n" + LocalizationData.instance.getText(23) + " \n\n" + LocalizationData.instance.getText(24) + " \n\t\t" + LocalizationData.instance.getText(25) + " \n\t\t" + LocalizationData.instance.getText(26) + " \n\t\t" + LocalizationData.instance.getText(27) + " \n" + LocalizationData.instance.getText(28) + " \n\t\t" + LocalizationData.instance.getText(29) + " \n\t\t" + LocalizationData.instance.getText(30) + " \n\t\t" + LocalizationData.instance.getText(31) + " \n" + LocalizationData.instance.getText(32) + " \n\t\t" + LocalizationData.instance.getText(33) + " \n\t\t" + LocalizationData.instance.getText(34) + " \n\t\t" + LocalizationData.instance.getText(35);
		}
		if (index == 36)
		{
			text.text = LocalizationData.instance.getText(36) + " \n" + LocalizationData.instance.getText(37) + " \n\t- " + LocalizationData.instance.getText(38) + " \n\t- " + LocalizationData.instance.getText(39) + " \n\t- " + LocalizationData.instance.getText(40) + " \n\t- " + LocalizationData.instance.getText(41) + " \n\t- " + LocalizationData.instance.getText(42) + " \n\t- " + LocalizationData.instance.getText(43) + " \n\t- " + LocalizationData.instance.getText(44) + " \n\t- " + LocalizationData.instance.getText(45) + " \n\t- " + LocalizationData.instance.getText(46) + " \n\t- " + LocalizationData.instance.getText(47);
		}
		if (index == 48)
		{
			text.text = LocalizationData.instance.getText(48) + " \n1 " + LocalizationData.instance.getText(49) + ": " + LocalizationData.instance.getText(50) + " " + LocalizationData.instance.getText(51) + " \n2 " + LocalizationData.instance.getText(52) + ": " + LocalizationData.instance.getText(53);
		}
		if (index == 54)
		{
			text.text = LocalizationData.instance.getText(54) + " \n\n1 " + LocalizationData.instance.getText(11) + ": \n" + LocalizationData.instance.getText(55) + " " + LocalizationData.instance.getText(56) + " " + LocalizationData.instance.getText(57) + " \n\n2 " + LocalizationData.instance.getText(12) + ": \n" + LocalizationData.instance.getText(58) + " " + LocalizationData.instance.getText(59) + " " + LocalizationData.instance.getText(60);
		}
		if (index == 61)
		{
			text.text = string.Empty + LocalizationData.instance.getText(61) + " \n" + LocalizationData.instance.getText(62) + " \n" + LocalizationData.instance.getText(63) + " \n" + LocalizationData.instance.getText(64);
		}
		if (index == 65)
		{
			text.text = "- " + LocalizationData.instance.getText(65) + " \n- " + LocalizationData.instance.getText(66) + " \n- " + LocalizationData.instance.getText(67);
		}
		if (index == 69)
		{
			text.text = LocalizationData.instance.getText(68) + " \n" + LocalizationData.instance.getText(69) + " \n\t\t* " + LocalizationData.instance.getText(70) + ":4 " + LocalizationData.instance.getText(74) + " \n\t\t* " + LocalizationData.instance.getText(71) + ":6 " + LocalizationData.instance.getText(74) + " \n\t\t* " + LocalizationData.instance.getText(72) + ":2 " + LocalizationData.instance.getText(74) + " \n\t\t* " + LocalizationData.instance.getText(73) + ":2 " + LocalizationData.instance.getText(74) + " \n\t\t* " + LocalizationData.instance.getText(75) + ":2 " + LocalizationData.instance.getText(74) + " \n\t\t" + LocalizationData.instance.getText(76) + "\n\n" + LocalizationData.instance.getText(77) + "\n" + LocalizationData.instance.getText(78) + " \n\t\t* " + LocalizationData.instance.getText(79) + ":2 " + LocalizationData.instance.getText(74) + " \n\t\t* " + LocalizationData.instance.getText(80) + ":6 " + LocalizationData.instance.getText(74) + " \n\t\t* " + LocalizationData.instance.getText(81) + ":4 " + LocalizationData.instance.getText(74) + " \n\t\t* " + LocalizationData.instance.getText(82) + ":2 " + LocalizationData.instance.getText(74);
		}
		if (index == 83)
		{
			text.text = LocalizationData.instance.getText(83) + " \n\n" + LocalizationData.instance.getText(84) + " \n\n\t\t* " + LocalizationData.instance.getText(85) + " \n\t\t* " + LocalizationData.instance.getText(86) + " \n\t\t* " + LocalizationData.instance.getText(87) + " \n\t\t* " + LocalizationData.instance.getText(88) + " \n\t\t* " + LocalizationData.instance.getText(89) + " \n\t\t* " + LocalizationData.instance.getText(90) + " \n\t\t* " + LocalizationData.instance.getText(91) + " \n\t\t* " + LocalizationData.instance.getText(92) + " \n\t\t* " + LocalizationData.instance.getText(93) + " \n\t\t* " + LocalizationData.instance.getText(94);
		}
		if (index == 96)
		{
			text.text = LocalizationData.instance.getText(96) + " " + LocalizationData.instance.getText(97) + " " + LocalizationData.instance.getText(98) + " " + LocalizationData.instance.getText(99) + " " + LocalizationData.instance.getText(100);
		}
		if (index == 101)
		{
			text.text = LocalizationData.instance.getText(101) + " " + LocalizationData.instance.getText(102) + " " + LocalizationData.instance.getText(103) + " \n" + LocalizationData.instance.getText(104) + " \n" + LocalizationData.instance.getText(105) + " \n" + LocalizationData.instance.getText(106) + " \n" + LocalizationData.instance.getText(107) + " \n" + LocalizationData.instance.getText(108) + " \n" + LocalizationData.instance.getText(109) + " \n" + LocalizationData.instance.getText(110);
		}
		if (index == 267)
		{
			text.text = LocalizationData.instance.getText(267);
			text.text = ReplaceText((CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6).ToString());
		}
		if (index == 268)
		{
			text.text = LocalizationData.instance.getText(268) + " " + ((float)CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchScores / (float)(CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls / 6)).ToString("F2");
		}
		if (index == 637)
		{
			text.text = LocalizationData.instance.getText(637);
			text.text = ReplaceText(CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].currentMatchBalls / 6 + "." + CONTROLLER.TeamList[CONTROLLER.BowlingTeamIndex].currentMatchBalls % 6);
		}
		if (index == 223)
		{
			text.text = LocalizationData.instance.getText(223);
		}
		if (index == 355)
		{
			text.text = LocalizationData.instance.getText(355) + " " + LocalizationData.instance.getText(356) + " " + LocalizationData.instance.getText(357);
		}
		if (index == 375)
		{
			text.text = LocalizationData.instance.getText(375) + " " + LocalizationData.instance.getText(376);
		}
		if (index == 377)
		{
			text.text = LocalizationData.instance.getText(377) + " " + LocalizationData.instance.getText(378);
		}
		if (index == 380)
		{
			text.text = LocalizationData.instance.getText(380) + " " + LocalizationData.instance.getText(381);
		}
		if (index == 383)
		{
			text.text = LocalizationData.instance.getText(383) + "\n" + LocalizationData.instance.getText(384);
		}
		if (index == 392)
		{
			text.text = LocalizationData.instance.getText(392);
			text.text = ReplaceText(text.text, "50");
		}
		if (index == 594)
		{
			text.text = LocalizationData.instance.getText(594) + "\n" + LocalizationData.instance.getText(595) + "\n" + LocalizationData.instance.getText(596);
		}
		if (index == 597)
		{
			text.text = LocalizationData.instance.getText(597) + "\n" + LocalizationData.instance.getText(598) + "\n" + LocalizationData.instance.getText(599);
		}
		if (index == 600)
		{
			text.text = LocalizationData.instance.getText(600) + "\n" + LocalizationData.instance.getText(601) + "\n" + LocalizationData.instance.getText(602);
		}
		if (index == 626)
		{
			text.text = LocalizationData.instance.getText(626) + "\n\n" + LocalizationData.instance.getText(627) + "\n" + LocalizationData.instance.getText(628) + "\n" + LocalizationData.instance.getText(629) + "\n\n" + LocalizationData.instance.getText(630) + "\n" + LocalizationData.instance.getText(631) + "\n\n" + LocalizationData.instance.getText(632) + "\n" + LocalizationData.instance.getText(633) + "\n" + LocalizationData.instance.getText(634) + "\n" + LocalizationData.instance.getText(635) + "\n" + LocalizationData.instance.getText(636);
		}
		if (index == 288)
		{
			text.text = LocalizationData.instance.getText(288) + ".\n" + LocalizationData.instance.getText(178) + "!";
		}
		if (index == 492)
		{
			text.text = LocalizationData.instance.getText(492) + "\n\n" + LocalizationData.instance.getText(493);
		}
		if (index == 642)
		{
			text.text = LocalizationData.instance.getText(642) + ".\n" + LocalizationData.instance.getText(567) + "!";
		}
	}

	public string ReplaceText(string replace)
	{
		string result = string.Empty;
		if (text.text.Contains("#"))
		{
			result = text.text.Replace("#", replace);
		}
		return result;
	}

	public string ReplaceText(string original, string replace)
	{
		string result = string.Empty;
		Debug.Log(original + " " + replace + " ");
		if (original.Contains("#"))
		{
			result = original.Replace("#", replace);
		}
		return result;
	}

	public string ReplaceOriginalText(string replace, int charCount)
	{
		string result = string.Empty;
		if (replace.Contains("over match".ToUpper()))
		{
			Debug.Log(replace);
			tempValue = replace.Substring(0, charCount);
			result = "# OVER MATCH";
		}
		return result;
	}
}
