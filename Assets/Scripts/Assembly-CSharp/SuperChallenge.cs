using System.Collections.Generic;

public class SuperChallenge
{
	public static Dictionary<int, string> BattingConstraints;

	public static void GetConstraints()
	{
		BattingConstraints = new Dictionary<int, string>();
		BattingConstraints.Add(0, "Shots in the off side won't be counted");
		BattingConstraints.Add(1, "Shots in the leg side won't be counted");
		BattingConstraints.Add(2, "With only one wicket");
		BattingConstraints.Add(3, "Without leaving a dot ball");
		BattingConstraints.Add(4, "Boundaries won't be counted");
		BattingConstraints.Add(5, "Score a half-century with at least one batsman");
		BattingConstraints.Add(6, "Leave a maiden over");
		BattingConstraints.Add(7, "Only straight shots counted");
		BattingConstraints.Add(8, "Lofted shots won't be counted");
		BattingConstraints.Add(9, "Score only with lofted shots");
		BattingConstraints.Add(10, "Batsman Running speed limited to 70%");
		BattingConstraints.Add(11, "Fast bowler's bowling speed increased by 200%");
	}
}
