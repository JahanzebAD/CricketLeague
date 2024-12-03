using UnityEngine;

public class TestMatchTeam : MonoBehaviour
{
	public static void SetCurrentMatchScores(int teamID, int runs, bool increment = true)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			if (increment)
			{
				CONTROLLER.TeamList[teamID].TMcurrentMatchScores1 += runs;
			}
			else
			{
				CONTROLLER.TeamList[teamID].TMcurrentMatchScores1 = runs;
			}
		}
		else if (increment)
		{
			CONTROLLER.TeamList[teamID].TMcurrentMatchScores2 += runs;
		}
		else
		{
			CONTROLLER.TeamList[teamID].TMcurrentMatchScores2 = runs;
		}
	}

	public static void SetCurrentMatchBalls(int teamID, int balls, bool increment = true)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			if (increment)
			{
				CONTROLLER.TeamList[teamID].TMcurrentMatchBalls1 += balls;
			}
			else
			{
				CONTROLLER.TeamList[teamID].TMcurrentMatchBalls1 = balls;
			}
		}
		else if (increment)
		{
			CONTROLLER.TeamList[teamID].TMcurrentMatchBalls2 += balls;
		}
		else
		{
			CONTROLLER.TeamList[teamID].TMcurrentMatchBalls2 = balls;
		}
	}

	public static void SetCurrentMatchWickets(int teamID, int wickets, bool increment = true)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			if (increment)
			{
				CONTROLLER.TeamList[teamID].TMcurrentMatchWickets1 += wickets;
			}
			else
			{
				CONTROLLER.TeamList[teamID].TMcurrentMatchWickets1 = wickets;
			}
		}
		else if (increment)
		{
			CONTROLLER.TeamList[teamID].TMcurrentMatchWickets2 += wickets;
		}
		else
		{
			CONTROLLER.TeamList[teamID].TMcurrentMatchWickets2 = wickets;
		}
	}

	public static int GetCurrentMatchScores(int teamID)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			return CONTROLLER.TeamList[teamID].TMcurrentMatchScores1;
		}
		return CONTROLLER.TeamList[teamID].TMcurrentMatchScores2;
	}

	public static int GetCurrentMatchBalls(int teamID)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			return CONTROLLER.TeamList[teamID].TMcurrentMatchBalls1;
		}
		return CONTROLLER.TeamList[teamID].TMcurrentMatchBalls2;
	}

	public static int GetCurrentMatchWickets(int teamID)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			return CONTROLLER.TeamList[teamID].TMcurrentMatchWickets1;
		}
		return CONTROLLER.TeamList[teamID].TMcurrentMatchWickets2;
	}
}
