public class TestMatchBatsman : Singleton<TestMatchBatsman>
{
	public static void SetRunsScored(int teamID, int playerID, int runs, bool increment = true)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			if (increment)
			{
				CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMRunsScored1 += runs;
			}
			else
			{
				CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMRunsScored1 = runs;
			}
		}
		else if (increment)
		{
			CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMRunsScored2 += runs;
		}
		else
		{
			CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMRunsScored2 = runs;
		}
	}

	public static void SetBallsPlayed(int teamID, int playerID, int balls, bool increment = true)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			if (increment)
			{
				CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMBallsPlayed1 += balls;
			}
			else
			{
				CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMBallsPlayed1 = balls;
			}
		}
		else if (increment)
		{
			CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMBallsPlayed2 += balls;
		}
		else
		{
			CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMBallsPlayed2 = balls;
		}
	}

	public static void SetStatus(int teamID, int playerID, string status)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMStatus1 = status;
		}
		else
		{
			CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMStatus2 = status;
		}
	}

	public static void SetFours(int teamID, int playerID, int fours, bool increment = true)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			if (increment)
			{
				CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMFours1 += fours;
			}
			else
			{
				CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMFours1 = fours;
			}
		}
		else if (increment)
		{
			CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMFours2 += fours;
		}
		else
		{
			CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMFours2 = fours;
		}
	}

	public static void SetSixes(int teamID, int playerID, int sixes, bool increment = true)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			if (increment)
			{
				CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMSixes1 = sixes;
			}
			else
			{
				CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMSixes1 += sixes;
			}
		}
		else if (increment)
		{
			CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMSixes2 = sixes;
		}
		else
		{
			CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMSixes2 += sixes;
		}
	}

	public static void SetFOW(int teamID, int playerID, int FOW)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMFOW1 = FOW;
		}
		else
		{
			CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMFOW2 = FOW;
		}
	}

	public static int GetRunsScored(int teamID, int playerID)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			return CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMRunsScored1;
		}
		return CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMRunsScored2;
	}

	public static int GetBallsPlayed(int teamID, int playerID)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			return CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMBallsPlayed1;
		}
		return CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMBallsPlayed2;
	}

	public static string GetStatus(int teamID, int playerID)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			return CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMStatus1;
		}
		return CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMStatus2;
	}

	public static int GetFours(int teamID, int playerID)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			return CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMFours1;
		}
		return CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMFours2;
	}

	public static int GetSixes(int teamID, int playerID)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			return CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMSixes1;
		}
		return CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMSixes2;
	}

	public static int GetFOW(int teamID, int playerID)
	{
		if (CONTROLLER.currentInnings < 2)
		{
			return CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMFOW1;
		}
		return CONTROLLER.TeamList[teamID].PlayerList[playerID].BatsmanList.TMFOW2;
	}
}
