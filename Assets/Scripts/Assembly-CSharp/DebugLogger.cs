using UnityEngine;

public class DebugLogger
{
	private bool canShowDebug = true;

	public static void PrintWithColor(string content, int colortype = 1)
	{
		if (Debug.unityLogger.logEnabled)
		{
			switch (colortype)
			{
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			case 4:
				break;
			}
		}
	}

	public static void PrintWithBold(string content)
	{
		if (!Debug.unityLogger.logEnabled)
		{
		}
	}

	public static void PrintWithItalic(string content)
	{
		if (!Debug.unityLogger.logEnabled)
		{
		}
	}

	public static void PrintWithSize(string content, int size = 20)
	{
		if (Debug.unityLogger.logEnabled)
		{
			string text = "<size=" + size + ">";
		}
	}

	public static void Print(string content)
	{
		if (!Debug.unityLogger.logEnabled)
		{
		}
	}

	public static void PrintError(string content)
	{
		if (!Debug.unityLogger.logEnabled)
		{
		}
	}
}
