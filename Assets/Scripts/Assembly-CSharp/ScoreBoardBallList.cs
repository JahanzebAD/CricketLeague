using System.Collections.Generic;

public class ScoreBoardBallList : Singleton<ScoreBoardBallList>
{
	public List<string> ballList;

	public List<string> extras;

	public int ballCount;

	private void Awake()
	{
		ResetBallList();
		ballCount = 0;
	}

	public void ResetBallList()
	{
		ballList.Clear();
		extras.Clear();
		ballList.TrimExcess();
		extras.TrimExcess();
		ballCount = 0;
	}

	public void AddBall(string score, string extraInfo)
	{
		ballCount++;
		ballList.Add(score);
		if (extraInfo != " ")
		{
			extras.Add((ballCount - 1).ToString());
			extras.Add(extraInfo);
		}
	}

	public void RemoveLastExtra()
	{
		extras.RemoveRange(extras.Count - 2, 2);
		extras.TrimExcess();
	}

	public void RemoveLastBall()
	{
		ballList.RemoveAt(ballList.Count - 1);
		ballList.TrimExcess();
		ballCount--;
	}

	public void SaveBallList()
	{
		AutoSave.ballListInfo = string.Empty;
		for (int i = 0; i < ballList.Count; i++)
		{
			AutoSave.ballListInfo = AutoSave.ballListInfo + ballList[i] + "|";
		}
		AutoSave.ballextras = string.Empty;
		for (int j = 0; j < extras.Count; j++)
		{
			AutoSave.ballextras = AutoSave.ballextras + extras[j] + "|";
		}
	}
}
