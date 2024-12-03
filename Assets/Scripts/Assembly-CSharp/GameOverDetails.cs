using System.Collections.Generic;
using UnityEngine;

public class GameOverDetails : MonoBehaviour
{
	public static Dictionary<string, string> GameWonDetails;

	public static Dictionary<string, string> GameLostDetails;

	public static Dictionary<string, string> TourStatusDetails;

	public static Dictionary<string, int> SpinsWonDetails;

	public static void InitDescriptionValues()
	{
		GameWonDetails = new Dictionary<string, string>();
		GameLostDetails = new Dictionary<string, string>();
		TourStatusDetails = new Dictionary<string, string>();
		SpinsWonDetails = new Dictionary<string, int>();
		GameWonDetails.Add("10", "You have qualified for the quarter finals of T20 World Cup tournament!");
		TourStatusDetails.Add("10", "THE GROUP KNOCKOUT STAGE");
		GameLostDetails.Add("10", "Tough luck! You have been knocked out of the tournament");
		GameWonDetails.Add("11", "You have qualified for the semi finals of T20 World Cup tournament!");
		TourStatusDetails.Add("11", "THE QUARTER FINALS");
		GameLostDetails.Add("11", "Tough luck! You have been knocked out of the tournament");
		GameWonDetails.Add("12", "You have qualified for the finals of T20 World Cup tournament!");
		TourStatusDetails.Add("12", "THE SEMI FINALS");
		GameLostDetails.Add("12", "Tough luck! You have been knocked out of the tournament");
		GameWonDetails.Add("13", "You are the T20 World Cup CHAMPION!");
		TourStatusDetails.Add("13", "THE FINALS");
		GameLostDetails.Add("13", "You are the T20 WORLD CUP CHAMPIONSHIP runner up!");
		GameWonDetails.Add("20", string.Empty);
		TourStatusDetails.Add("20", "THE LEAGUE MATCH");
		GameLostDetails.Add("20", string.Empty);
		if (CONTROLLER.NPLIndiaLeagueMatchIndex == 0)
		{
			GameWonDetails.Add("21", "You have qualified for the finals of NPL tournament!");
			TourStatusDetails.Add("21", "QUALIFIER 1");
			GameLostDetails.Add("21", "You still have another chance in the Eliminator");
		}
		else
		{
			GameWonDetails.Add("21", "You have qualified for Qualifier 2 of NPL tournament!");
			TourStatusDetails.Add("21", "THE ELIMINATOR");
			GameLostDetails.Add("21", "Tough luck! You are out of the tournament");
		}
		GameWonDetails.Add("22", "You have qualified for the finals of NPL tournament!");
		TourStatusDetails.Add("22", "QUALIFIER 2");
		GameLostDetails.Add("22", "Tough luck! You finished 3rd in the NPL tournament");
		GameWonDetails.Add("23", "You are the NPL CHAMPION!");
		TourStatusDetails.Add("23", "THE FINALS");
		GameLostDetails.Add("23", "You are NPL runner up!");
		GameWonDetails.Add("30", string.Empty);
		TourStatusDetails.Add("30", "THE GROUP MATCH");
		GameLostDetails.Add("30", string.Empty);
		GameWonDetails.Add("31", "You have qualified for the semi finals of World Cup tournament!");
		TourStatusDetails.Add("31", "THE QUARTER FINALS");
		GameLostDetails.Add("31", "Tough luck! You are out of the tournament");
		GameWonDetails.Add("32", "You have qualified for the finals of World Cup tournament!");
		TourStatusDetails.Add("32", "THE SEMI FINALS");
		GameLostDetails.Add("32", "Tough luck! You are out of the tournament");
		GameWonDetails.Add("33", "You are the WORLD CUP CHAMPION!");
		TourStatusDetails.Add("33", "THE FINALS");
		GameLostDetails.Add("33", "You are the WORLD CUP CHAMPIONSHIP runner up!");
		GameWonDetails.Add("00", string.Empty);
		TourStatusDetails.Add("00", "THE MATCH");
		GameLostDetails.Add("00", string.Empty);
		SpinsWonDetails.Add("132", 1);
		SpinsWonDetails.Add("133", 3);
		SpinsWonDetails.Add("232", 2);
		SpinsWonDetails.Add("233", 5);
		SpinsWonDetails.Add("333", 5);
		SpinsWonDetails.Add("334", 9);
		SpinsWonDetails.Add("335", 13);
	}
}
