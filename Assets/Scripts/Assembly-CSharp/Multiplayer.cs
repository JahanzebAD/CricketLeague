using System.Collections.Generic;

public class Multiplayer
{
	public static int roomType = 0;

	public static int oversCount = 0;

	public static int overs = 0;

	public static int playerCount = 0;

	public static int isHost = 0;

	public static string roomID = string.Empty;

	public static PlayerList[] playerList = new PlayerList[5];

	public static List<MultiplayerOver> oversData = new List<MultiplayerOver>();

	public static MultiplayerScore[] playerScores;

	public static int entryTickets = 1;

	public static int[] winningCoins2 = new int[5] { 500, 400, 300, 200, 100 };

	public static int[] winningCoins5 = new int[5] { 1000, 800, 600, 400, 200 };

	public static int GetWinningCostIndex(int i)
	{
		int result = 0;
		if (playerCount == 2)
		{
			switch (i)
			{
			case 0:
				result = 0;
				break;
			case 1:
				result = 4;
				break;
			}
		}
		else if (playerCount == 3)
		{
			switch (i)
			{
			case 0:
				result = 0;
				break;
			case 1:
				result = 3;
				break;
			case 2:
				result = 4;
				break;
			}
		}
		else if (playerCount == 4)
		{
			switch (i)
			{
			case 0:
				result = 0;
				break;
			case 1:
				result = 2;
				break;
			case 2:
				result = 3;
				break;
			case 3:
				result = 4;
				break;
			}
		}
		else if (playerCount == 5)
		{
			result = i;
		}
		return result;
	}
}
