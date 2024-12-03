using System.Collections;
using SimpleJSON;
using UnityEngine;

public class PHPManager : MonoBehaviour
{
	public static PHPManager Instance;

	public bool isOnlinePhpServer;

	private bool _IsSetLeaderboardScoreSuccess;

	private bool _IsGetLeaderboardScoreSuccess;

	private bool _IsUserSyncInProgress;

	private int _CreateUserType = -1;

	private int _FetchUserType = -1;

	private int _SendPasscodeType = -1;

	private string PHPUri
	{
		get
		{
			if (isOnlinePhpServer)
			{
				return "https://www.cricketbuddies.com/boc2/unity/unity.php";
			}
			return "http://192.168.1.220/boc/unity/unity.php";
		}
	}

	public bool IsSetLeaderboardScoreSuccess => _IsSetLeaderboardScoreSuccess;

	public bool IsGetLeaderboardScoreSuccess => _IsGetLeaderboardScoreSuccess;

	public bool IsUserSyncInProgress => _IsUserSyncInProgress;

	public string GetPHPURL => PHPUri;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	public IEnumerator UserLogin(string userName, string emailID)
	{
		yield return StartCoroutine(Instance.CreateUser(userName, emailID));
		UserProfile.OnlineSyncingState = true;
	}

	public IEnumerator CreateUser(string Username, string EmailID)
	{
		_CreateUserType = -1;
		yield return StartCoroutine(NetworkManager.Instance.CheckInternetConnection());
		if (!NetworkManager.Instance.IsNetworkConnected)
		{
			yield break;
		}
		WWWForm form = new WWWForm();
		form.AddField("action", "CreateUser");
		form.AddField("username", Username);
		form.AddField("uid", CONTROLLER.profilePlayerID);
		form.AddField("emailid", EmailID);
		form.AddField("platform", AppInfo.Platform);
		form.AddField("version", AppInfo.VersionCode);
		form.AddField("deviceid", AppInfo.DeviceID);
		form.AddField("ltype", "gp");
		WWW download = new WWW(PHPUri, form);
		yield return download;
		if (string.IsNullOrEmpty(download.text) || !string.IsNullOrEmpty(download.error))
		{
			yield break;
		}
		JSONNode jSONNode = JSONNode.Parse(download.text);
		if (string.Empty + jSONNode["CreateUser"]["status"] == "1" && string.Empty + jSONNode["CreateUser"]["response"] != null && string.Empty + jSONNode["CreateUser"]["response"]["type"] != null)
		{
			CONTROLLER.userID = int.Parse(string.Empty + jSONNode["CreateUser"]["response"]["userid"]);
			CONTROLLER.username = Username;
			if (InterfaceHandler._instance != null && InterfaceHandler._instance.bForceConnectMultiplayer)
			{
				InterfaceHandler._instance.bForceConnectMultiplayer = false;
				ServerManager.Instance.Connect();
			}
			if (int.Parse(string.Empty + jSONNode["CreateUser"]["response"]["type"]) == 0)
			{
				_CreateUserType = 0;
			}
			else
			{
				_CreateUserType = 1;
			}
		}
	}

	public IEnumerator UserSync(bool isSignoutSync = false)
	{
		if (_IsUserSyncInProgress)
		{
			yield break;
		}
		_IsUserSyncInProgress = true;
		yield return StartCoroutine(NetworkManager.Instance.CheckInternetConnection());
		if (NetworkManager.Instance.IsNetworkConnected && CONTROLLER.userID != 0)
		{
			WWWForm form = new WWWForm();
			form.AddField("action", "UserSync");
			form.AddField("userid", CONTROLLER.userID);
			int keyValue1 = Random.Range(1, 10);
			form.AddField("etickets", Encryption.encryptPointsSystem(UserProfile.EarnedTickets, keyValue1));
			form.AddField("eticketskey", keyValue1);
			int keyValue2 = Random.Range(1, 10);
			form.AddField("stickets", Encryption.encryptPointsSystem(UserProfile.SpentTickets, keyValue2));
			form.AddField("sticketskey", keyValue2);
			int keyValue3 = Random.Range(1, 10);
			form.AddField("epoints", Encryption.encryptPointsSystem(CONTROLLER.gameSyncPoint, keyValue3));
			form.AddField("epointskey", keyValue3);
			form.AddField("platform", AppInfo.Platform);
			form.AddField("version", AppInfo.VersionCode);
			WWW download = new WWW(PHPUri, form);
			yield return download;
			if (!string.IsNullOrEmpty(download.text) && string.IsNullOrEmpty(download.error))
			{
				JSONNode jSONNode = JSONNode.Parse(download.text);
				if (string.Empty + jSONNode["UserSync"]["status"] == "1" && string.Empty + jSONNode["UserSync"]["coins"] != null)
				{
					if (!isSignoutSync)
					{
						CONTROLLER.tickets = int.Parse(Encryption.DecryptPointsSystem(string.Empty + jSONNode["UserSync"]["etickets"], int.Parse(string.Empty + jSONNode["UserSync"]["eticketskey"])));
						CONTROLLER.earnedTickets = 0;
						CONTROLLER.spendTickets = 0;
						CONTROLLER.gameTotalPoints = int.Parse(Encryption.DecryptPointsSystem(string.Empty + jSONNode["UserSync"]["epoints"], int.Parse(string.Empty + jSONNode["UserSync"]["epointskey"])));
						CONTROLLER.gameSyncPoint = 0;
						if (Singleton<MultiplayerPage>.instance != null)
						{
							Singleton<MultiplayerPage>.instance.TicketsCount.text = CONTROLLER.tickets.ToString();
						}
					}
					else
					{
						ProceedSignout();
					}
					if (InterfaceHandler._instance != null && InterfaceHandler._instance.bForceConnectMultiplayer)
					{
						InterfaceHandler._instance.bForceConnectMultiplayer = false;
						ServerManager.Instance.Connect();
					}
				}
			}
			_IsUserSyncInProgress = false;
		}
		else
		{
			_IsUserSyncInProgress = false;
		}
	}

	public void ProceedSignout()
	{
	}
}
