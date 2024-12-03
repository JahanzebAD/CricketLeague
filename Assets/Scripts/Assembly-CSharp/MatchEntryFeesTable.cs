public class MatchEntryFeesTable
{
	public enum QuickPlayEntryFees
	{
		Overs2 = 1,
		Overs5 = 2,
		Overs10 = 5,
		Overs20 = 10,
		Overs30 = 0xF,
		Overs50 = 25
	}

	public enum WorldCupEntryFees
	{
		Overs50 = 125,
		Overs30 = 18000,
		Overs20 = 12000
	}

	public enum T20CupEntryFees
	{
		Overs20 = 15000,
		Overs10 = 7500
	}

	public enum NPLEntryFees
	{
		Overs20 = 7800,
		Overs10 = 7800
	}

	public static int RVAmount = 125;
}
