using System;

namespace nxtCrypto.PackData
{
	[Serializable]
	public class PackData
	{
		private int eCoins;

		private int eEarnedCoins;

		private int eSpendCoins;

		private int eTickets;

		private int eEarnedTickets;

		private int eSpendTickets;

		private int eXP;

		private int eEarnedXp;

		private int eArcadeXp;

		private int eEarnedArcadeXp;

		private int eFreeSpin;

		private int eSixcount;

		private int ePowergrade;

		private int eControlgrade;

		private int eAgilitygrade;

		private int eTotalPowerSubGrade;

		private int eTotalControlSubGrade;

		private int eTotalAgilitySubGrade;

		private int eWeekXp;

		private int eWeekEarnedXp;

		private int eWeekArcadeXp;

		private int eWeekEarnedArcadeXp;

		public int Coins => eCoins;

		public int EarnedCoins => eEarnedCoins;

		public int SpendCoins => eSpendCoins;

		public int Tickets => eTickets;

		public int EarnedTickets => eEarnedTickets;

		public int SpendTickets => eSpendTickets;

		public int XP => eXP;

		public int EarnedXp => eEarnedXp;

		public int ArcadeXp => eArcadeXp;

		public int EarnedArcadeXp => eEarnedArcadeXp;

		public int FreeSpin => eFreeSpin;

		public int Sixcount => eSixcount;

		public int Powergrade => ePowergrade;

		public int Controlgrade => eControlgrade;

		public int Agilitygrade => eAgilitygrade;

		public int TotalPowerSubGrade => eTotalPowerSubGrade;

		public int TotalControlSubGrade => eTotalControlSubGrade;

		public int TotalAgilitySubGrade => eTotalAgilitySubGrade;

		public int WeekXp => eWeekXp;

		public int WeekEarnedXp => eWeekEarnedXp;

		public int WeekArcadeXp => eWeekArcadeXp;

		public int WeekEarnedArcadeXp => eWeekEarnedArcadeXp;

		private PackData()
		{
			eCoins = 0;
			eEarnedCoins = 0;
			eSpendCoins = 0;
			eTickets = 0;
			eEarnedTickets = 0;
			eSpendTickets = 0;
			eXP = 0;
			eEarnedXp = 0;
			eArcadeXp = 0;
			eEarnedArcadeXp = 0;
			eFreeSpin = 0;
			eSixcount = 0;
			ePowergrade = 0;
			eControlgrade = 0;
			eAgilitygrade = 0;
			eTotalPowerSubGrade = 0;
			eTotalControlSubGrade = 0;
			eTotalAgilitySubGrade = 0;
			eWeekXp = 0;
			eWeekEarnedXp = 0;
			eWeekArcadeXp = 0;
			eWeekEarnedArcadeXp = 0;
		}

		public PackData(int Coins, int EarnedCoins, int SpendCoins, int Tickets, int EarnedTickets, int SpendTickets, int XP, int EarnedXp, int ArcadeXp, int EarnedArcadeXp, int FreeSpin, int Sixcount, int Powergrade, int Controlgrade, int Agilitygrade, int TotalPowerSubGrade, int TotalControlSubGrade, int TotalAgilitySubGrade, int WeekXp, int WeekEarnedXp, int WeekArcadeXp, int WeekEarnedArcadeXp)
		{
			eCoins = Coins;
			eEarnedCoins = EarnedCoins;
			eSpendCoins = SpendCoins;
			eTickets = Tickets;
			eEarnedTickets = EarnedTickets;
			eSpendTickets = SpendTickets;
			eXP = XP;
			eEarnedXp = EarnedXp;
			eArcadeXp = ArcadeXp;
			eEarnedArcadeXp = EarnedArcadeXp;
			eFreeSpin = FreeSpin;
			eSixcount = Sixcount;
			ePowergrade = Powergrade;
			eControlgrade = Controlgrade;
			eAgilitygrade = Agilitygrade;
			eTotalPowerSubGrade = TotalPowerSubGrade;
			eTotalControlSubGrade = TotalControlSubGrade;
			eTotalAgilitySubGrade = TotalAgilitySubGrade;
			eWeekXp = WeekXp;
			eWeekEarnedXp = WeekEarnedXp;
			eWeekArcadeXp = WeekArcadeXp;
			eWeekEarnedArcadeXp = WeekEarnedArcadeXp;
		}

		public void CopyFrom(PackData Src)
		{
			eCoins = Src.eCoins;
			eEarnedCoins = Src.eEarnedCoins;
			eSpendCoins = Src.eSpendCoins;
			eTickets = Src.eTickets;
			eEarnedTickets = Src.eEarnedTickets;
			eSpendTickets = Src.eSpendTickets;
			eXP = Src.eXP;
			eEarnedXp = Src.eEarnedXp;
			eArcadeXp = Src.eArcadeXp;
			eEarnedArcadeXp = Src.eEarnedArcadeXp;
			eFreeSpin = Src.eFreeSpin;
			eSixcount = Src.eSixcount;
			ePowergrade = Src.ePowergrade;
			eControlgrade = Src.eControlgrade;
			eAgilitygrade = Src.eAgilitygrade;
			eTotalPowerSubGrade = Src.eTotalPowerSubGrade;
			eTotalControlSubGrade = Src.eTotalControlSubGrade;
			eTotalAgilitySubGrade = Src.eTotalAgilitySubGrade;
			eWeekXp = Src.eWeekXp;
			eWeekEarnedXp = Src.eWeekEarnedXp;
			eWeekArcadeXp = Src.eWeekArcadeXp;
			eWeekEarnedArcadeXp = Src.eWeekEarnedArcadeXp;
		}

		public void CopyTo(out PackData Dst)
		{
			Dst = new PackData();
			Dst.eCoins = eCoins;
			Dst.eEarnedCoins = eEarnedCoins;
			Dst.eSpendCoins = eSpendCoins;
			Dst.eTickets = eTickets;
			Dst.eEarnedTickets = eEarnedTickets;
			Dst.eSpendTickets = eSpendTickets;
			Dst.eXP = eXP;
			Dst.eEarnedXp = eEarnedXp;
			Dst.eArcadeXp = eArcadeXp;
			Dst.eEarnedArcadeXp = eEarnedArcadeXp;
			Dst.eFreeSpin = eFreeSpin;
			Dst.eSixcount = eSixcount;
			Dst.ePowergrade = ePowergrade;
			Dst.eControlgrade = eControlgrade;
			Dst.eAgilitygrade = eAgilitygrade;
			Dst.eTotalPowerSubGrade = eTotalPowerSubGrade;
			Dst.eTotalControlSubGrade = eTotalControlSubGrade;
			Dst.eTotalAgilitySubGrade = eTotalAgilitySubGrade;
			Dst.eWeekXp = eWeekXp;
			Dst.eWeekEarnedXp = eWeekEarnedXp;
			Dst.eWeekArcadeXp = eWeekArcadeXp;
			Dst.eWeekEarnedArcadeXp = eWeekEarnedArcadeXp;
		}
	}
}
