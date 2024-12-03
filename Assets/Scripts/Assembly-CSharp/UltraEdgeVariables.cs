using System.Collections.Generic;
using UnityEngine;

public class UltraEdgeVariables : MonoBehaviour
{
	public static Dictionary<string, float> ultraEdgeStartPosition;

	public static Dictionary<string, float> ultraEdgeEndPosition;

	public static void InitUltraEdgeVariables()
	{
		ultraEdgeStartPosition = new Dictionary<string, float>();
		ultraEdgeEndPosition = new Dictionary<string, float>();
		ultraEdgeStartPosition.Add("backFootStraightDrive", 7.6f);
		ultraEdgeEndPosition.Add("backFootStraightDrive", 8.2f);
		ultraEdgeStartPosition.Add("bt6StraightDrive", 7.6f);
		ultraEdgeEndPosition.Add("bt6StraightDrive", 8.2f);
	}
}
