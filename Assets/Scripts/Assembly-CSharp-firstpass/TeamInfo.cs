public class TeamInfo
{
	public string teamName;

	public string teamGroup;

	public string abbrevation;

	public string rank;

	public int KeeperIndex;

	public int CaptainIndex;

	public MatchInfo[] MatchList;

	public PlayerInfo[] PlayerList;

	public int teamId;

	public int currentMatchScores;

	public int TMcurrentMatchScores1;

	public int TMcurrentMatchScores2;

	public int currentMatchBalls;

	public int TMcurrentMatchBalls1;

	public int TMcurrentMatchBalls2;

	public int currentMatchWickets;

	public int TMcurrentMatchWickets1;

	public int TMcurrentMatchWickets2;

	public int currentMatchExtras;

	public int currentMatchLbs;

	public int currentMatchbyes;

	public int currentMatchNoball;

	public int currentMatchWideBall;

	public string[] ballUpdate = new string[6];

	public string controlType;

	public string inningsPlayed = string.Empty;

	public bool isDeclared1;

	public bool isDeclared2;

	public int noofDRSLeft;
}
