using UnityEngine;
using UnityEngine.UI;

public class BatsmanInfo : Singleton<BatsmanInfo>
{
	public Image LogoFlag;

	public Text LogoTxt;

	public Text NameTxt;

	public Text StatusTxt;

	public Text RunsTxt;

	public Text BallsTxt;

	public Text FoursTxt;

	public Text SixesTxt;

	public Text StrikeRate;

	public GameObject holder;

	protected void Awake()
	{
		Hide(boolean: true);
	}

	public void addEventListener()
	{
	}

	public void UpdateRecord(int TeamID, int playerID)
	{
		Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite in flags)
		{
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].abbrevation)
			{
				LogoFlag.sprite = sprite;
			}
		}
		LogoTxt.text = CONTROLLER.TeamList[TeamID].teamName;
		NameTxt.text = CONTROLLER.TeamList[TeamID].PlayerList[playerID].PlayerName.ToUpper();
		if (CONTROLLER.PlayModeSelected != 7)
		{
			StatusTxt.text = CONTROLLER.TeamList[TeamID].PlayerList[playerID].BatsmanList.Status;
			RunsTxt.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[playerID].BatsmanList.RunsScored;
			BallsTxt.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[playerID].BatsmanList.BallsPlayed;
			FoursTxt.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[playerID].BatsmanList.Fours;
			SixesTxt.text = string.Empty + CONTROLLER.TeamList[TeamID].PlayerList[playerID].BatsmanList.Sixes;
			StrikeRate.text = GetStrikeRate(playerID);
		}
		else
		{
			StatusTxt.text = TestMatchBatsman.GetStatus(TeamID, playerID);
			RunsTxt.text = string.Empty + TestMatchBatsman.GetRunsScored(TeamID, playerID);
			BallsTxt.text = string.Empty + TestMatchBatsman.GetBallsPlayed(TeamID, playerID);
			FoursTxt.text = string.Empty + TestMatchBatsman.GetFours(TeamID, playerID);
			SixesTxt.text = string.Empty + TestMatchBatsman.GetSixes(TeamID, playerID);
			StrikeRate.text = TMGetStrikeRate(playerID);
		}
	}

	private string GetStrikeRate(int playerID)
	{
		float num = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.RunsScored;
		float num2 = CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.BallsPlayed;
		float num3;
		if (num2 > 0f)
		{
			num3 = num / num2 * 100f;
			num3 = Mathf.Round(num3 * 100f) / 100f;
		}
		else
		{
			num3 = 0f;
		}
		return string.Empty + num3;
	}

	private string TMGetStrikeRate(int playerID)
	{
		float num = ((CONTROLLER.currentInnings >= 2) ? ((float)CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[playerID].BatsmanList.TMRunsScored2) : ((float)TestMatchBatsman.GetRunsScored(CONTROLLER.BattingTeamIndex, playerID)));
		float num2 = TestMatchBatsman.GetBallsPlayed(CONTROLLER.BattingTeamIndex, playerID);
		float num3;
		if (num2 > 0f)
		{
			num3 = num / num2 * 100f;
			num3 = Mathf.Round(num3 * 100f) / 100f;
		}
		else
		{
			num3 = 0f;
		}
		return string.Empty + num3;
	}

	public void Hide(bool boolean)
	{
		if (boolean)
		{
			holder.SetActive(value: false);
			CancelInvoke("playDuckAnimation");
		}
		else
		{
			holder.SetActive(value: true);
		}
	}
}
