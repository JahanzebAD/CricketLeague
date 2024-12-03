using System.Collections.Generic;

public class CoinsTable
{
	public static Dictionary<string, int> CoinsEarned;

	public static Dictionary<string, string> CoinsDetail;

	public static Dictionary<string, int> WorldCupQuaterFinalCoins;

	public static Dictionary<string, int> WorldCupSemiFinalCoins;

	public static Dictionary<string, int> WorldCupFinalCoins;

	public static void InitCoinValues()
	{
		CoinsEarned = new Dictionary<string, int>();
		CoinsDetail = new Dictionary<string, string>();
		WorldCupFinalCoins = new Dictionary<string, int>();
		WorldCupQuaterFinalCoins = new Dictionary<string, int>();
		WorldCupSemiFinalCoins = new Dictionary<string, int>();
		CoinsEarned.Add("2OverMatchWonEasy", 50);
		CoinsEarned.Add("2OverMatchWonMedium", 60);
		CoinsEarned.Add("2OverMatchWonHard", 80);
		CoinsDetail.Add("2OverMatchWonEasy", "Win a 2 over match under easy difficulty");
		CoinsDetail.Add("2OverMatchWonMedium", "Win a 2 over match under medium difficulty");
		CoinsDetail.Add("2OverMatchWonHard", "Win a 2 over match under hard difficulty");
		CoinsEarned.Add("5OverMatchWonEasy", 125);
		CoinsEarned.Add("5OverMatchWonMedium", 150);
		CoinsEarned.Add("5OverMatchWonHard", 200);
		CoinsDetail.Add("5OverMatchWonEasy", "Win a 5 over match under easy difficulty");
		CoinsDetail.Add("5OverMatchWonMedium", "Win a 5 over match under medium difficulty");
		CoinsDetail.Add("5OverMatchWonHard", "Win a 5 over match under hard difficulty");
		CoinsEarned.Add("10OverMatchWonEasy", 250);
		CoinsEarned.Add("10OverMatchWonMedium", 300);
		CoinsEarned.Add("10OverMatchWonHard", 400);
		CoinsDetail.Add("10OverMatchWonEasy", "Win a 10 over match under easy difficulty");
		CoinsDetail.Add("10OverMatchWonMedium", "Win a 10 over match under medium difficulty");
		CoinsDetail.Add("10OverMatchWonHard", "Win a 10 over match under hard difficulty");
		CoinsEarned.Add("20OverMatchWonEasy", 500);
		CoinsEarned.Add("20OverMatchWonMedium", 600);
		CoinsEarned.Add("20OverMatchWonHard", 800);
		CoinsDetail.Add("20OverMatchWonEasy", "Win a 20 over match under easy difficulty");
		CoinsDetail.Add("20OverMatchWonMedium", "Win a 20 over match under medium difficulty");
		CoinsDetail.Add("20OverMatchWonHard", "Win a 20 over match under hard difficulty");
		CoinsEarned.Add("30OverMatchWonEasy", 750);
		CoinsEarned.Add("30OverMatchWonMedium", 960);
		CoinsEarned.Add("30OverMatchWonHard", 1200);
		CoinsDetail.Add("30OverMatchWonEasy", "Win a 30 over match under easy difficulty");
		CoinsDetail.Add("30OverMatchWonMedium", "Win a 30 over match under medium difficulty");
		CoinsDetail.Add("30OverMatchWonHard", "Win a 30 over match under hard difficulty");
		CoinsEarned.Add("50OverMatchWonEasy", 1250);
		CoinsEarned.Add("50OverMatchWonMedium", 1500);
		CoinsEarned.Add("50OverMatchWonHard", 2000);
		CoinsDetail.Add("50OverMatchWonEasy", "Win a 50 over match under easy difficulty");
		CoinsDetail.Add("50OverMatchWonMedium", "Win a 50 over match under medium difficulty");
		CoinsDetail.Add("50OverMatchWonHard", "Win a 50 over match under hard difficulty");
		CoinsEarned.Add("WCQFWon50OverMatchEasy", 3000);
		CoinsEarned.Add("WCQFLost50OverMatchEasy", 2000);
		CoinsEarned.Add("WCQFWon30OverMatchEasy", 1800);
		CoinsEarned.Add("WCQFLost30OverMatchEasy", 1200);
		CoinsEarned.Add("WCQFWon20OverMatchEasy", 1200);
		CoinsEarned.Add("WCQFLost20OverMatchEasy", 800);
		CoinsEarned.Add("WCSFWon50OverMatchEasy", 5000);
		CoinsEarned.Add("WCSFLost50OverMatchEasy", 4000);
		CoinsEarned.Add("WCSFWon30OverMatchEasy", 3000);
		CoinsEarned.Add("WCSFLost30OverMatchEasy", 2400);
		CoinsEarned.Add("WCSFWon20OverMatchEasy", 2000);
		CoinsEarned.Add("WCSFLost20OverMatchEasy", 1600);
		CoinsEarned.Add("WCFWon50OverMatchEasy", 15000);
		CoinsEarned.Add("WCFLost50OverMatchEasy", 10000);
		CoinsEarned.Add("WCFWon30OverMatchEasy", 9000);
		CoinsEarned.Add("WCFLost30OverMatchEasy", 6000);
		CoinsEarned.Add("WCFWon20OverMatchEasy", 6000);
		CoinsEarned.Add("WCFLost20OverMatchEasy", 4000);
		CoinsEarned.Add("T20QUWon20OverMatchEasy", 1000);
		CoinsEarned.Add("T20QULost20OverMatchEasy", 0);
		CoinsEarned.Add("T20QFWon20OverMatchEasy", 3000);
		CoinsEarned.Add("T20QFLost20OverMatchEasy", 2000);
		CoinsEarned.Add("T20SFWon20OverMatchEasy", 5000);
		CoinsEarned.Add("T20SFLost20OverMatchEasy", 4000);
		CoinsEarned.Add("T20FWon20OverMatchEasy", 10000);
		CoinsEarned.Add("T20FLost20OverMatchEasy", 5000);
		CoinsEarned.Add("NPLQUWon20OverMatchEasy", 5000);
		CoinsEarned.Add("NPLQULost20OverMatchEasy", 0);
		CoinsEarned.Add("NPLQFWon20OverMatchEasy", 2000);
		CoinsEarned.Add("NPLQFLost20OverMatchEasy", 1500);
		CoinsEarned.Add("NPLSFWon20OverMatchEasy", 3000);
		CoinsEarned.Add("NPLSFLost20OverMatchEasy", 2500);
		CoinsEarned.Add("NPLFWon20OverMatchEasy", 12500);
		CoinsEarned.Add("NPLFLost20OverMatchEasy", 7500);
		CoinsEarned.Add("FoursHitCoins", 4);
		CoinsEarned.Add("MaidenOverCoins", 12);
		CoinsEarned.Add("DotBallCoins", 2);
		CoinsEarned.Add("SixHitCoins", 6);
		CoinsEarned.Add("BowledCoins", 6);
		CoinsEarned.Add("CaughtCoins", 4);
		CoinsEarned.Add("WicketOthersCoins", 3);
		CoinsEarned.Add("FiftyCoins", 10);
		CoinsEarned.Add("CenturyCoins", 20);
	}
}
