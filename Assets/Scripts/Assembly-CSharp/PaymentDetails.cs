using System.Collections.Generic;

public class PaymentDetails : Singleton<PaymentDetails>
{
	public static Dictionary<string, int> PaymentAmount;

	public static Dictionary<string, int> StoreUpgradeAmount;

	public static Dictionary<string, int> StoreUpgradeTimer;

	public static Dictionary<string, int> StoreUpgradeFinishCoins;

	public static void InitPaymentValues()
	{
		PaymentAmount = new Dictionary<string, int>();
		PaymentAmount.Add("00", 1);
		PaymentAmount.Add("01", 2);
		PaymentAmount.Add("02", 5);
		PaymentAmount.Add("12", 20);
		PaymentAmount.Add("13", 40);
		PaymentAmount.Add("22", 20);
		PaymentAmount.Add("23", 40);
		PaymentAmount.Add("33", 40);
		PaymentAmount.Add("34", 60);
		PaymentAmount.Add("35", 100);
		PaymentAmount.Add("70", 0);
		PaymentAmount.Add("76", 30);
		PaymentAmount.Add("77", 60);
		PaymentAmount.Add("78", 120);
		PaymentAmount.Add("79", 150);
	}

	public static void InitUpgradeCoinValues()
	{
		StoreUpgradeAmount = new Dictionary<string, int>();
		StoreUpgradeAmount.Add("0", 400);
		StoreUpgradeAmount.Add("1", 1200);
		StoreUpgradeAmount.Add("2", 4000);
		StoreUpgradeAmount.Add("3", 12000);
		StoreUpgradeAmount.Add("4", 20000);
		StoreUpgradeAmount.Add("5", 40000);
		StoreUpgradeAmount.Add("6", 80000);
		StoreUpgradeAmount.Add("7", 100000);
		StoreUpgradeAmount.Add("8", 120000);
		StoreUpgradeAmount.Add("9", 150000);
		StoreUpgradeAmount.Add("10", 150000);
	}

	public static void InitUpgradeTimerValues()
	{
		StoreUpgradeTimer = new Dictionary<string, int>();
		StoreUpgradeTimer.Add("0", 300);
		StoreUpgradeTimer.Add("1", 10800);
		StoreUpgradeTimer.Add("2", 21600);
		StoreUpgradeTimer.Add("3", 43200);
		StoreUpgradeTimer.Add("4", 57600);
		StoreUpgradeTimer.Add("5", 64800);
		StoreUpgradeTimer.Add("6", 86400);
		StoreUpgradeTimer.Add("7", 86400);
		StoreUpgradeTimer.Add("8", 86400);
		StoreUpgradeTimer.Add("9", 86400);
		StoreUpgradeTimer.Add("10", 86400);
	}

	public static void InitUpgradeFinishCoinsValues()
	{
		StoreUpgradeFinishCoins = new Dictionary<string, int>();
		StoreUpgradeFinishCoins.Add("0", 40);
		StoreUpgradeFinishCoins.Add("1", 120);
		StoreUpgradeFinishCoins.Add("2", 400);
		StoreUpgradeFinishCoins.Add("3", 1200);
		StoreUpgradeFinishCoins.Add("4", 2000);
		StoreUpgradeFinishCoins.Add("5", 4000);
		StoreUpgradeFinishCoins.Add("6", 8000);
		StoreUpgradeFinishCoins.Add("7", 10000);
		StoreUpgradeFinishCoins.Add("8", 12000);
		StoreUpgradeFinishCoins.Add("9", 15000);
		StoreUpgradeFinishCoins.Add("10", 15000);
	}
}
