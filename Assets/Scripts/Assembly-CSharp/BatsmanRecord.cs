using UnityEngine;
using UnityEngine.UI;

public class BatsmanRecord : Singleton<BatsmanRecord>
{
	public Image LogoFlag;

	public Text LogoTxt;

	public Text NameTxt;

	public Text StyleTxt;

	private Transform _transform;

	public GameObject batsmanRecord;

	protected void Awake()
	{
		_transform = base.transform;
		Hide(boolean: true);
	}

	public void UpdateRecord(int TeamID, int playerID)
	{
		Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite in flags)
		{
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[TeamID].abbrevation)
			{
				LogoFlag.sprite = sprite;
			}
		}
		LogoTxt.text = CONTROLLER.TeamList[TeamID].teamName;
		NameTxt.text = CONTROLLER.TeamList[TeamID].PlayerList[playerID].PlayerName.ToUpper();
		if (CONTROLLER.TeamList[TeamID].PlayerList[playerID].BatsmanList.BattingHand == "L")
		{
			StyleTxt.text = LocalizationData.instance.getText(515);
		}
		else
		{
			StyleTxt.text = LocalizationData.instance.getText(516);
		}
	}

	public void FadeRecord(int alpha, int sec)
	{
		iTween.FadeTo(batsmanRecord, iTween.Hash("alpha", alpha, "time", sec));
	}

	public void Hide(bool boolean)
	{
		if (boolean)
		{
			batsmanRecord.SetActive(value: false);
		}
		else
		{
			batsmanRecord.SetActive(value: true);
		}
	}
}
