using CodeStage.AntiCheat.ObscuredTypes;

public class PaymentProcess : Singleton<PaymentProcess>
{
	public int CoinKey;

	public int TimeKey;

	public int FinishKey;

	private string key = string.Empty;

	public string[] oversKey = new string[8] { "QPOvers", "T20Overs", "NPLOvers", "WCOvers", "SC", "SO", "MP", "TMOvers" };

	public int GenerateAmount()
	{
		oversKey = new string[8] { "QPOvers", "T20Overs", "NPLOvers", "WCOvers", "SC", "SO", "MP", "TMOvers" };
		PaymentDetails.InitPaymentValues();
		if (CONTROLLER.PlayModeSelected == 2)
		{
			key = CONTROLLER.PlayModeSelected.ToString() + ObscuredPrefs.GetInt(CONTROLLER.tournamentType + "Overs");
		}
		else
		{
			key = CONTROLLER.PlayModeSelected.ToString() + ObscuredPrefs.GetInt(oversKey[CONTROLLER.PlayModeSelected]);
		}
		return PaymentDetails.PaymentAmount[key];
	}

	public int GenerateCoinValue(int index)
	{
		PaymentDetails.InitUpgradeCoinValues();
		switch (index)
		{
		case 0:
			CoinKey = CONTROLLER.powerGrade;
			break;
		case 1:
			CoinKey = CONTROLLER.controlGrade;
			break;
		case 2:
			CoinKey = CONTROLLER.agilityGrade;
			break;
		}
		return PaymentDetails.StoreUpgradeAmount[CoinKey.ToString()];
	}

	public int GenerateTimerValue(int index)
	{
		PaymentDetails.InitUpgradeTimerValues();
		switch (index)
		{
		case 0:
			TimeKey = CONTROLLER.powerGrade;
			break;
		case 1:
			TimeKey = CONTROLLER.controlGrade;
			break;
		case 2:
			TimeKey = CONTROLLER.agilityGrade;
			break;
		}
		return PaymentDetails.StoreUpgradeTimer[TimeKey.ToString()];
	}

	public int GenerateFinishTimeCoinValue(int index)
	{
		PaymentDetails.InitUpgradeFinishCoinsValues();
		switch (index)
		{
		case 0:
			FinishKey = CONTROLLER.powerGrade;
			break;
		case 1:
			FinishKey = CONTROLLER.controlGrade;
			break;
		case 2:
			FinishKey = CONTROLLER.agilityGrade;
			break;
		}
		return PaymentDetails.StoreUpgradeFinishCoins[FinishKey.ToString()];
	}
}
