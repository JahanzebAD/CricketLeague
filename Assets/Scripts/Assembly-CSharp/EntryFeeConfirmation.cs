using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class EntryFeeConfirmation : Singleton<EntryFeeConfirmation>
{
	public GameObject holder;

	public Image cupImg;

	public Text subText;

	public Sprite[] cupSprites;

	public Text modeDetails;

	public Text matchDetails;

	private string[] modeTitle = new string[8]
	{
		"183",
		"17",
		"14",
		"13",
		"11",
		"12",
		string.Empty,
		"462"
	};

	private string ticketAmount;

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = HideMe;
		CONTROLLER.pageName = "EFPopup";
		holder.SetActive(value: true);
		if (CONTROLLER.PlayModeSelected < 4)
		{
			modeDetails.text = CONTROLLER.Overs[CONTROLLER.oversSelectedIndex] + " " + LocalizationData.instance.getText(532);
			PaymentDetails.InitPaymentValues();
			string key = CONTROLLER.PlayModeSelected.ToString() + CONTROLLER.oversSelectedIndex;
			if (CONTROLLER.PlayModeSelected == 2)
			{
				modeDetails.text = CONTROLLER.Overs[ObscuredPrefs.GetInt(CONTROLLER.tournamentType + "Overs")] + " " + LocalizationData.instance.getText(532);
				key = CONTROLLER.PlayModeSelected.ToString() + ObscuredPrefs.GetInt(CONTROLLER.tournamentType + "Overs");
			}
			ticketAmount = PaymentDetails.PaymentAmount[key].ToString();
		}
		else
		{
			modeDetails.text = LocalizationData.instance.getText(int.Parse(modeTitle[CONTROLLER.PlayModeSelected])) + " " + LocalizationData.instance.getText(277);
			ticketAmount = "1";
		}
		if (CONTROLLER.PlayModeSelected != 2)
		{
			subText.text = LocalizationData.instance.getText(int.Parse(modeTitle[CONTROLLER.PlayModeSelected]));
			cupImg.sprite = cupSprites[CONTROLLER.PlayModeSelected];
		}
		else if (CONTROLLER.tournamentType == "NPL")
		{
			cupImg.sprite = cupSprites[CONTROLLER.PlayModeSelected];
			subText.text = LocalizationData.instance.getText(int.Parse(modeTitle[CONTROLLER.PlayModeSelected]));
		}
		else if (CONTROLLER.tournamentType == "PAK")
		{
			cupImg.sprite = cupSprites[6];
			subText.text = LocalizationData.instance.getText(15);
		}
		else if (CONTROLLER.tournamentType == "AUS")
		{
			cupImg.sprite = cupSprites[7];
			subText.text = LocalizationData.instance.getText(16);
		}
		if (ticketAmount == "1")
		{
			matchDetails.text = LocalizationData.instance.getText(406) + ": " + ticketAmount + " " + LocalizationData.instance.getText(533);
		}
		else
		{
			matchDetails.text = LocalizationData.instance.getText(406) + ": " + ticketAmount + " " + LocalizationData.instance.getText(172);
		}
	}

	public void HideMe()
	{
		holder.SetActive(value: false);
		Singleton<SquadPageTWO>.instance.showMe(1);
	}
}
