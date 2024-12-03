using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
{
	public static Dictionary<string, int> BaseValues;

	public static void GetBaseEarnings()
	{
		BaseValues = new Dictionary<string, int>();
		BaseValues.Add("3|0|Win", 2000);
		BaseValues.Add("3|0|Tie", 2000);
		BaseValues.Add("3|0|Loss", 0);
		BaseValues.Add("3|1|Win", 2700);
		BaseValues.Add("3|1|Tie", 2700);
		BaseValues.Add("3|1|Loss", 1000);
		BaseValues.Add("3|2|Win", 3700);
		BaseValues.Add("3|2|Tie", 3700);
		BaseValues.Add("3|2|Loss", 1000);
		BaseValues.Add("3|3|Win", 4700);
		BaseValues.Add("3|3|Tie", 4700);
		BaseValues.Add("3|3|Loss", 1000);
		BaseValues.Add("1|0|Win", 1000);
		BaseValues.Add("1|0|Tie", 1000);
		BaseValues.Add("1|0|Loss", 0);
		BaseValues.Add("1|1|Win", 1050);
		BaseValues.Add("1|1|Tie", 1050);
		BaseValues.Add("1|1|Loss", 500);
		BaseValues.Add("1|2|Win", 1550);
		BaseValues.Add("1|2|Loss", 500);
		BaseValues.Add("1|2|Tie", 1550);
		BaseValues.Add("1|3|Win", 2050);
		BaseValues.Add("1|3|Tie", 2050);
		BaseValues.Add("1|3|Loss", 500);
		BaseValues.Add("2|0|Win", 1000);
		BaseValues.Add("2|0|Tie", 1000);
		BaseValues.Add("2|0|Loss", 0);
		BaseValues.Add("2|1|0|Win", 1950);
		BaseValues.Add("2|1|0|Tie", 1950);
		BaseValues.Add("2|1|0|Loss", 0);
		BaseValues.Add("2|1|1|Win", 975);
		BaseValues.Add("2|1|1|Tie", 975);
		BaseValues.Add("2|1|1|Loss", 500);
		BaseValues.Add("2|2|Win", 975);
		BaseValues.Add("2|2|Tie", 975);
		BaseValues.Add("2|2|Loss", 500);
		BaseValues.Add("2|3|Win", 2450);
		BaseValues.Add("2|3|Tie", 2450);
		BaseValues.Add("2|3|Loss", 500);
		BaseValues.Add("0|Win", 50);
		BaseValues.Add("0|Tie", 50);
		BaseValues.Add("0|Loss", 0);
		BaseValues.Add("7|Win", 750);
		BaseValues.Add("7|Tie", 625);
		BaseValues.Add("7|Loss", 375);
	}
}
