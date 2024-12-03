using System;
using System.Collections;
using System.Collections.Generic;
using BestHTTP.SocketIO;
using CodeStage.AntiCheat.ObscuredTypes;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerManager : MonoBehaviour
{
	public static ServerManager Instance;

	public bool isOnlineNodeServer = true;

	public string LoadedSceneName;

	private SocketManager _SocketManager;

	public ScoreBoardMultiPlayer scoreBoardScript;

	private bool fireOnce;

	private string SocketUri
	{
		get
		{
			if (isOnlineNodeServer)
			{
				return "ws://boc.batattackcricket.com:8081/socket.io/";
			}
			return "ws://192.168.1.151:3001/socket.io/";
		}
	}

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		SetupSocketManager();
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		LoadedSceneName = scene.name;
		if (scene.buildIndex == 1)
		{
			Application.runInBackground = false;
			Screen.sleepTimeout = -2;
		}
		if (scene.buildIndex == 2 && CONTROLLER.PlayModeSelected == 6)
		{
			Application.runInBackground = true;
			Screen.sleepTimeout = -1;
			scoreBoardScript = GameObject.Find("ScoreBoard_MultiPlayer").GetComponent<ScoreBoardMultiPlayer>();
			scoreBoardScript.SortAndRankScores();
			StartGameAfterCountdown();
		}
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public void StartGameAfterCountdown()
	{
	}

	private void SetupSocketManager()
	{
		SocketOptions socketOptions = new SocketOptions();
		socketOptions.AutoConnect = false;
		socketOptions.Reconnection = true;
		socketOptions.Timeout = TimeSpan.FromSeconds(10.0);
		_SocketManager = new SocketManager(new Uri(SocketUri), socketOptions);
		SetupSocketManagerEvents();
	}

	private void SetupSocketManagerEvents()
	{
		_SocketManager.Socket.On("player connected", Connected);
		_SocketManager.Socket.On("disconnect", Disconnected);
		_SocketManager.Socket.On("room info", RoomInfo);
		_SocketManager.Socket.On("player duplicate", DuplicatePlayer);
		_SocketManager.Socket.On("player entered room", PlayerEnteredRoom);
		_SocketManager.Socket.On("player exit room", PlayerExitedRoom);
		_SocketManager.Socket.On("countdown", Countdown);
		_SocketManager.Socket.On("game start", GameStart);
		_SocketManager.Socket.On("game update", GameUpdate);
		_SocketManager.Socket.On("game next ball", GameNextBall);
		_SocketManager.Socket.On("game finish", GameFinish);
		_SocketManager.Socket.On("game cancel", GameCancel);
		_SocketManager.Socket.On("game abandoned", GameAbandoned);
		_SocketManager.Socket.On("room found", RoomFound);
		_SocketManager.Socket.On("room not found", RoomNotFound);
		_SocketManager.Socket.On("countdown rematch", CountdownRematch);
		_SocketManager.Socket.On("emoticon recieve", RecieveEmoticon);
		_SocketManager.Socket.On(SocketIOEventTypes.Error, OnError);
	}

	private void OnError(Socket socket, Packet packet, params object[] args)
	{
		Error error = args[0] as Error;
		SocketIOErrors code = error.Code;
		if (code != SocketIOErrors.User && code == SocketIOErrors.Internal)
		{
			StartCoroutine(CheckNet());
		}
		string[] array = error.ToString().Split('"');
		if (array[1] == "ConnectionTimedOut")
		{
			InterfaceHandler._instance.HideMultiplayerMode();
		}
	}

	private IEnumerator CheckNet()
	{
		yield return StartCoroutine(NetworkManager.Instance.CheckInternetConnection());
		if (CONTROLLER.PlayModeSelected == 6 && !NetworkManager.Instance.IsNetworkConnected && !fireOnce)
		{
			StartCoroutine(HandleDisconnect());
			UserProfile.isInMultiplayer = false;
			fireOnce = true;
		}
	}

	private void Disconnected(Socket socket, Packet packet, object[] args)
	{
		if (UserProfile.isInMultiplayer)
		{
			StartCoroutine(HandleDisconnect());
			UserProfile.isInMultiplayer = false;
		}
	}

	private IEnumerator HandleDisconnect()
	{
		if (LoadedSceneName == "MainMenu")
		{
			InterfaceHandler._instance.ShowLoadingScreen();
		}
		else if (LoadedSceneName == "Ground")
		{
			GroundScriptHandler.Instance.loadingScreen.SetActive(value: true);
		}
		yield return StartCoroutine(NetworkManager.Instance.CheckInternetConnection());
		if (LoadedSceneName == "MainMenu")
		{
			InterfaceHandler._instance.HideLoadingScreen();
		}
		else if (LoadedSceneName == "Ground")
		{
			GroundScriptHandler.Instance.loadingScreen.SetActive(value: false);
		}
		if (CONTROLLER.PlayModeSelected != 6)
		{
			yield break;
		}
		if (NetworkManager.Instance.IsNetworkConnected)
		{
			if (LoadedSceneName == "MainMenu")
			{
				InterfaceHandler._instance.ShowServerDisconnectedPopup();
			}
			else
			{
				if (!(LoadedSceneName == "Ground"))
				{
					yield break;
				}
				if (!ScoreBoardMultiPlayer.instance.MultiplayerGameOverPage.activeSelf)
				{
					GroundScriptHandler.Instance.ShowServerDisconnectedPopup();
				}
				if (Multiplayer.roomType == 0)
				{
					if (Multiplayer.overs == 2)
					{
						//FirebaseAnalyticsManager.instance.logEvent("MP_PB_2_Disconnect", "Multiplayer", CONTROLLER.userID);
					}
					else if (Multiplayer.overs == 5)
					{
						//FirebaseAnalyticsManager.instance.logEvent("MP_PB_5_Disconnect", "Multiplayer", CONTROLLER.userID);
					}
				}
				else if (Multiplayer.overs == 2)
				{
					//FirebaseAnalyticsManager.instance.logEvent("MP_PV_2_Disconnect", "Multiplayer", CONTROLLER.userID);
				}
				else if (Multiplayer.overs == 5)
				{
					//FirebaseAnalyticsManager.instance.logEvent("MP_PV_5_Disconnect", "Multiplayer", CONTROLLER.userID);
				}
			}
		}
		else if (LoadedSceneName == "MainMenu")
		{
			InterfaceHandler._instance.ShowNoInternetPopup();
			InterfaceHandler._instance.HideNoPlayersFoundPopup();
		}
		else if (LoadedSceneName == "Ground")
		{
			GroundScriptHandler.Instance.ShowNoInternetPopup();
		}
	}

	private void Connected(Socket socket, Packet packet, object[] args)
	{
		string aJSON = packet.ToString();
		JSONNode jSONNode = JSONNode.Parse(aJSON);
		Multiplayer.entryTickets = int.Parse(string.Empty + jSONNode[1]["entryCoins"]);
		InterfaceHandler._instance.ShowMultiplayerMode();
	}

	private void RoomInfo(Socket socket, Packet packet, object[] args)
	{
		if (Multiplayer.roomType == 2)
		{
			InterfaceHandler._instance.multiplayerMode.HideWaitingForOtherPlayers();
		}
		UserProfile.isInMultiplayer = true;
		Multiplayer.oversData.Clear();
		Array.Clear(Multiplayer.playerList, 0, Multiplayer.playerList.Length);
		string aJSON = packet.ToString();
		JSONNode jSONNode = JSONNode.Parse(aJSON);
		Multiplayer.overs = int.Parse(string.Empty + jSONNode[1]["overs"]);
		Multiplayer.roomID = string.Empty + jSONNode[1]["roomID"];
		InterfaceHandler._instance.multiplayerMode.SetRoomIDValue(Multiplayer.roomID);
		string[] array = (string.Empty + jSONNode[1]["playerList"]).Split('|');
		int num = (Multiplayer.playerCount = array.Length / 2);
		int num2 = 0;
		int num3 = 0;
		while (num3 < 5)
		{
			Multiplayer.playerList[num3] = new PlayerList();
			if (num3 < num)
			{
				Multiplayer.playerList[num3].PlayerId = int.Parse(array[num2]);
				Multiplayer.playerList[num3].PlayerName = array[num2 + 1];
			}
			else
			{
				Multiplayer.playerList[num3].PlayerId = -1;
				Multiplayer.playerList[num3].PlayerName = "waiting for player...";
			}
			num3++;
			num2 += 2;
		}
		for (int i = 0; i < Multiplayer.overs; i++)
		{
			MultiplayerOver multiplayerOver = new MultiplayerOver();
			if (int.Parse(string.Empty + jSONNode[1]["ballParameters"][i]["bowlerHand"]) == 0)
			{
				multiplayerOver.bowlerHand = "left";
			}
			else
			{
				multiplayerOver.bowlerHand = "right";
			}
			if (int.Parse(string.Empty + jSONNode[1]["ballParameters"][i]["bowlerSide"]) == 0)
			{
				multiplayerOver.bowlerSide = "left";
			}
			else
			{
				multiplayerOver.bowlerSide = "right";
			}
			switch (int.Parse(string.Empty + jSONNode[1]["ballParameters"][i]["bowlerType"]))
			{
			case 0:
				multiplayerOver.bowlerType = "fast";
				break;
			case 1:
				multiplayerOver.bowlerType = "offspin";
				break;
			default:
				multiplayerOver.bowlerType = "legspin";
				break;
			}
			for (int j = 0; j < 6; j++)
			{
				multiplayerOver.bowlingAngle[j] = int.Parse(string.Empty + jSONNode[1]["ballParameters"][i]["bowlingAngle"][j]);
				multiplayerOver.bowlingSpeed[j] = int.Parse(string.Empty + jSONNode[1]["ballParameters"][i]["bowlingSpeed"][j]);
				float x = float.Parse(string.Empty + jSONNode[1]["ballParameters"][i]["bowlingSpot"]["LHB"][j][0]);
				float y = float.Parse(string.Empty + jSONNode[1]["ballParameters"][i]["bowlingSpot"]["LHB"][j][1]);
				float z = float.Parse(string.Empty + jSONNode[1]["ballParameters"][i]["bowlingSpot"]["LHB"][j][2]);
				Vector3 vector = new Vector3(x, y, z);
				multiplayerOver.bowlingSpotL[j] = vector;
				float x2 = float.Parse(string.Empty + jSONNode[1]["ballParameters"][i]["bowlingSpot"]["RHB"][j][0]);
				float y2 = float.Parse(string.Empty + jSONNode[1]["ballParameters"][i]["bowlingSpot"]["RHB"][j][1]);
				float z2 = float.Parse(string.Empty + jSONNode[1]["ballParameters"][i]["bowlingSpot"]["RHB"][j][2]);
				Vector3 vector2 = new Vector3(x2, y2, z2);
				multiplayerOver.bowlingSpotR[j] = vector2;
			}
			Multiplayer.oversData.Add(multiplayerOver);
		}
		InterfaceHandler._instance.multiplayerMode.UpdatePlayerList();
	}

	private void DuplicatePlayer(Socket socket, Packet packet, object[] args)
	{
		InterfaceHandler._instance.HideLoadingScreen();
		InterfaceHandler._instance.ShowDuplicatePlayerPopup();
	}

	private void PlayerEnteredRoom(Socket socket, Packet packet, object[] args)
	{
		Debug.LogError("Player Entered Room." + packet.ToString());
		string aJSON = packet.ToString();
		JSONNode jSONNode = JSONNode.Parse(aJSON);
		Multiplayer.playerList[Multiplayer.playerCount].PlayerName = string.Empty + jSONNode[1]["playerName"];
		Multiplayer.playerList[Multiplayer.playerCount].PlayerId = int.Parse(string.Empty + jSONNode[1]["playerID"]);
		Multiplayer.playerCount++;
		InterfaceHandler._instance.multiplayerMode.UpdatePlayerList();
	}

	private void PlayerExitedRoom(Socket socket, Packet packet, object[] args)
	{
		string aJSON = packet.ToString();
		JSONNode jSONNode = JSONNode.Parse(aJSON);
		int id = int.Parse(string.Empty + jSONNode[1]["playerID"]);
		if (id != CONTROLLER.userID && ManageScenes._instance.getCurrentLoadedSceneName() == "Ground" && !ScoreBoardMultiPlayer.instance.MultiplayerGameOverPage.activeSelf)
		{
			ScoreBoardMultiPlayer.instance.oppLeft.SetActive(value: true);
			ScoreBoardMultiPlayer.instance.oppLeftText.text = string.Concat(string.Empty, jSONNode[1]["playerName"], " has left");
			Invoke("HideFakeToast", 5f);
		}
		PlayerList playerList = Array.Find(Multiplayer.playerList, (PlayerList t) => t.PlayerId == id);
		playerList.PlayerId = -1;
		playerList.PlayerName = "waiting for player...";
		List<PlayerList> list = new List<PlayerList>();
		for (int i = 0; i < 5; i++)
		{
			if (Multiplayer.playerList[i] != null && Multiplayer.playerList[i].PlayerId != -1)
			{
				PlayerList item = Multiplayer.playerList[i];
				list.Add(item);
			}
		}
		for (int j = 0; j < 5; j++)
		{
			if (j < list.Count)
			{
				Multiplayer.playerList[j] = list[j];
				continue;
			}
			PlayerList playerList2 = new PlayerList();
			playerList2.PlayerId = -1;
			playerList2.PlayerName = "waiting for player...";
			Multiplayer.playerList[j] = playerList2;
		}
		Multiplayer.playerCount--;
		if (ManageScenes._instance.getCurrentLoadedSceneName() == "MainMenu")
		{
			InterfaceHandler._instance.multiplayerMode.UpdatePlayerList();
			return;
		}
		MultiplayerScore multiplayerScore = Array.Find(Multiplayer.playerScores, (MultiplayerScore t) => t.PlayerId == id);
		int num = Array.FindIndex(Multiplayer.playerScores, (MultiplayerScore t) => t.PlayerId == id);
		StartCoroutine(EmoticonAnimator.Instance.playerEmoticon[num].StopAnimation());
		multiplayerScore.PlayerId = -1;
		multiplayerScore.Username = "waiting for player...";
		List<MultiplayerScore> list2 = new List<MultiplayerScore>();
		for (int k = 0; k < Multiplayer.playerCount + 1; k++)
		{
			if (Multiplayer.playerScores[k] != null && Multiplayer.playerScores[k].PlayerId != -1)
			{
				list2.Add(Multiplayer.playerScores[k]);
			}
		}
		for (int l = 0; l < 5; l++)
		{
			if (l < list2.Count)
			{
				Multiplayer.playerScores[l] = list2[l];
			}
		}
		scoreBoardScript.UpdateMultiplayerScores();
	}

	private void HideFakeToast()
	{
		ScoreBoardMultiPlayer.instance.oppLeft.SetActive(value: false);
	}

	private void Countdown(Socket socket, Packet packet, object[] args)
	{
		if (Multiplayer.roomType == 2)
		{
			InterfaceHandler._instance.multiplayerMode.HideWaitingForOtherPlayers();
		}
		InterfaceHandler._instance.multiplayerMode.UpdateCountdown(int.Parse(args[0].ToString()));
	}

	private void GameStart(Socket socket, Packet packet, params object[] args)
	{
		if (ManageScenes._instance.getCurrentLoadedSceneName() == "MainMenu" && InterfaceHandler._instance.serverDisconnectedPopup.activeSelf)
		{
			return;
		}
		Multiplayer.playerScores = new MultiplayerScore[Multiplayer.playerCount];
		for (int i = 0; i < Multiplayer.playerCount; i++)
		{
			MultiplayerScore multiplayerScore = new MultiplayerScore();
			multiplayerScore.Username = Multiplayer.playerList[i].PlayerName;
			multiplayerScore.PlayerId = Multiplayer.playerList[i].PlayerId;
			multiplayerScore.Rank = 0;
			multiplayerScore.Score = "0";
			multiplayerScore.Wickets = 0;
			multiplayerScore.LastBallScore = 0;
			Multiplayer.playerScores[i] = multiplayerScore;
		}
		CONTROLLER.totalOvers = Multiplayer.overs;
		CONTROLLER.currentMatchWickets = 0;
		CONTROLLER.totalWickets = 10;
		CONTROLLER.tickets -= Multiplayer.entryTickets;
		CONTROLLER.spendTickets += Multiplayer.entryTickets;
		SavePlayerPrefs.SaveUserTickets(-1, 1, 0);
		Singleton<MultiplayerPage>.instance.TicketsCount.text = CONTROLLER.tickets.ToString();
		StartCoroutine(InterfaceHandler._instance.LoadGroundScene());
		//FirebaseAnalyticsManager.instance.logEvent("MP_Played", "Multiplayer", CONTROLLER.userID);
		FBAppEvents.instance.LogMultiplayerEvent("MP_Played", "Start");
		if (Multiplayer.roomType == 0)
		{
			//FirebaseAnalyticsManager.instance.logEvent("MP_Public", "Multiplayer", CONTROLLER.userID);
			FBAppEvents.instance.LogMultiplayerEvent("MP_Public", "Start");
			if (Multiplayer.overs == 2)
			{
				//FirebaseAnalyticsManager.instance.logEvent("MP_PB_2_Start", "Multiplayer", CONTROLLER.userID);
			}
			else if (Multiplayer.overs == 5)
			{
				//FirebaseAnalyticsManager.instance.logEvent("MP_PB_5_Start", "Multiplayer", CONTROLLER.userID);
			}
		}
		else if (Multiplayer.roomType == 1)
		{
			//FirebaseAnalyticsManager.instance.logEvent("MP_Private", "Multiplayer", CONTROLLER.userID);
			FBAppEvents.instance.LogMultiplayerEvent("MP_Private", "Start");
			if (Multiplayer.overs == 2)
			{
				//FirebaseAnalyticsManager.instance.logEvent("MP_PV_2_Start", "Multiplayer", CONTROLLER.userID);
			}
			else if (Multiplayer.overs == 5)
			{
				//FirebaseAnalyticsManager.instance.logEvent("MP_PV_5_Start", "Multiplayer", CONTROLLER.userID);
			}
		}
	}

	private void GameUpdate(Socket socket, Packet packet, object[] args)
	{
		string aJSON = packet.ToString();
		JSONNode jSONNode = JSONNode.Parse(aJSON);
		int playerId = int.Parse(string.Empty + jSONNode[1]["playerID"]);
		string score = string.Empty + jSONNode[1]["score"];
		int wickets = int.Parse(string.Empty + jSONNode[1]["wickets"]);
		int lastBallScore = int.Parse(string.Empty + jSONNode[1]["lastBallScore"]);
		int num = Array.FindIndex(Multiplayer.playerScores, (MultiplayerScore t) => t.PlayerId == playerId);
		if (num != -1 && num <= Multiplayer.playerScores.Length)
		{
			MultiplayerScore multiplayerScore = Multiplayer.playerScores[num];
			multiplayerScore.Score = score;
			multiplayerScore.Wickets = wickets;
			multiplayerScore.LastBallScore = lastBallScore;
			Multiplayer.playerScores[num] = multiplayerScore;
			scoreBoardScript.SortAndRankScores();
		}
	}

	private void GameNextBall(Socket socket, Packet packet, object[] args)
	{
		if (CONTROLLER.currentMatchWickets < 10 && CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].currentMatchBalls < Multiplayer.overs * 6)
		{
			if (Singleton<GameModel>.instance.currentBall == 5)
			{
				KitTable.SetKitValues(1);
				Singleton<GameModel>.instance.CounterPerOversPlayed++;
				ObscuredPrefs.SetInt(CONTROLLER.PlayModeSelected + "CounterPerOversPlayed", Singleton<GameModel>.instance.CounterPerOversPlayed);
				Singleton<GroundController>.instance.showPreviewCamera(status: false);
				Singleton<GameModel>.instance.NewOver();
			}
			else
			{
				Singleton<GameModel>.instance.BowlNextBall();
				scoreBoardScript.HideWait();
				scoreBoardScript.AnimateBoard();
			}
			ScoreBoardMultiPlayer.instance.WaitForOthersPanel.SetActive(value: false);
			ScoreBoardMultiPlayer.instance.WaitForOthersWicketsOverPanel.SetActive(value: false);
		}
		else
		{
			scoreBoardScript.HideWait();
			scoreBoardScript.ShowWicketsWait();
			SendMatchScore(CONTROLLER.currentMatchScores.ToString(), CONTROLLER.currentMatchWickets, 0);
		}
	}

	private void GameFinish(Socket socket, Packet packet, object[] args)
	{
		int num = 0;
		string aJSON = packet.ToString();
		JSONNode jSONNode = JSONNode.Parse(aJSON);
		CONTROLLER.MPInningsCompleted = true;
		int num2 = int.Parse(string.Empty + jSONNode[1].Count);
		for (int i = 0; i < num2; i++)
		{
			MultiplayerScore multiplayerScore = new MultiplayerScore();
			multiplayerScore.PlayerId = int.Parse(string.Empty + jSONNode[1][i]["id"]);
			multiplayerScore.Username = string.Empty + jSONNode[1][i]["name"];
			multiplayerScore.Score = string.Empty + jSONNode[1][i]["runs"];
			multiplayerScore.Rank = int.Parse(string.Empty + jSONNode[1][i]["rank"]);
			multiplayerScore.Wickets = int.Parse(string.Empty + jSONNode[1][i]["wickets"]);
			Multiplayer.playerScores[i] = multiplayerScore;
			if (multiplayerScore.PlayerId == CONTROLLER.userID)
			{
				int num3 = multiplayerScore.Rank - 1;
				if (Multiplayer.overs == 2)
				{
					num = Multiplayer.winningCoins2[num3];
				}
				else
				{
					num = Multiplayer.winningCoins5[num3];
				}
			}
		}
		scoreBoardScript.HideWait();
		scoreBoardScript.HideWicketsWait();
		scoreBoardScript.SetMultiplayerGameOverTexts();
		scoreBoardScript.ShowMultiPlayerGameOver();
		ExitRoom();
	}

	private void GameCancel(Socket socket, Packet packet, object[] args)
	{
		if (ManageScenes._instance.getCurrentLoadedSceneName() == "MainMenu")
		{
			InterfaceHandler._instance.multiplayerMode.HideWaitingForOtherPlayers();
			InterfaceHandler._instance.ShowNoPlayersFoundPopup();
		}
	}

	private void GameAbandoned(Socket socket, Packet packet, object[] args)
	{
		string hostName = args[0].ToString();
		InterfaceHandler._instance.ShowGameAbandonedPopup(hostName);
	}

	private void RoomFound(Socket socket, Packet packet, object[] args)
	{
		InterfaceHandler._instance.multiplayerMode.ShowLobbyForPrivateJoin();
	}

	private void RoomNotFound(Socket socket, Packet packet, object[] args)
	{
		InterfaceHandler._instance.ShowRoomNotFoundPopup(LocalizationData.instance.getText(654));
	}

	private void CountdownRematch(Socket socket, Packet packet, object[] args)
	{
		if (ManageScenes._instance.getCurrentLoadedSceneName() == "Ground")
		{
			scoreBoardScript.ReplayCountdown(int.Parse(args[0].ToString()));
		}
		else if (ManageScenes._instance.getCurrentLoadedSceneName() == "MainMenu" && CONTROLLER.CurrentPage != "multiplayerpage")
		{
			Multiplayer.roomType = -1;
		}
	}

	private void RecieveEmoticon(Socket socket, Packet packet, object[] args)
	{
		string aJSON = packet.ToString();
		JSONNode jSONNode = JSONNode.Parse(aJSON);
		int player = int.Parse(string.Empty + jSONNode[1]["playerID"]);
		string emoticon = string.Empty + jSONNode[1]["emoticonindex"];
		EmoticonAnimator.Instance.AnimateEmoticon(player, emoticon, -1);
	}

	public void Connect()
	{
		if (_SocketManager.State != SocketManager.States.Open)
		{
			_SocketManager.Open();
		}
		PlayerConnect();
	}

	public void Disconnect()
	{
		UserProfile.isInMultiplayer = false;
	}

	public void PlayerConnect()
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject.AddField("playerID", CONTROLLER.userID);
		_SocketManager.Socket.Emit("player connect", jSONObject.ToDictionary());
	}

	public void FindRoom()
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject.AddField("playerID", CONTROLLER.userID);
		jSONObject.AddField("playerName", CONTROLLER.username);
		jSONObject.AddField("roomOvers", Multiplayer.overs);
		_SocketManager.Socket.Emit("find room", jSONObject.ToDictionary());
	}

	public void CreateRoom()
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject.AddField("playerID", CONTROLLER.userID);
		jSONObject.AddField("playerName", CONTROLLER.username);
		jSONObject.AddField("roomOvers", Multiplayer.overs);
		_SocketManager.Socket.Emit("create room", jSONObject.ToDictionary());
	}

	public void JoinRoom()
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject.AddField("playerID", CONTROLLER.userID);
		jSONObject.AddField("playerName", CONTROLLER.username);
		jSONObject.AddField("roomID", Multiplayer.roomID);
		_SocketManager.Socket.Emit("join room", jSONObject.ToDictionary());
	}

	public void ExitRoom()
	{
		Multiplayer.roomID = string.Empty;
		if (Multiplayer.roomType == 2)
		{
			Multiplayer.roomType = -1;
		}
		JSONObject jSONObject = new JSONObject();
		jSONObject.AddField("playerID", CONTROLLER.userID);
		_SocketManager.Socket.Emit("exit room", jSONObject.ToDictionary());
	}

	public void StartPrivateMatch()
	{
		_SocketManager.Socket.Emit("match start");
	}

	public void SendMatchScore(string matchScore, int wickets, int lastBallScore)
	{
		if (UserProfile.isInMultiplayer)
		{
			JSONObject jSONObject = new JSONObject();
			jSONObject.AddField("playerID", CONTROLLER.userID);
			jSONObject.AddField("playerName", CONTROLLER.username);
			jSONObject.AddField("score", matchScore);
			jSONObject.AddField("wickets", wickets);
			jSONObject.AddField("lastBallScore", lastBallScore);
			_SocketManager.Socket.Emit("match score", jSONObject.ToDictionary());
			if (CONTROLLER.currentMatchWickets < 10)
			{
				scoreBoardScript.ShowWait();
			}
		}
	}

	public void RematchRoom()
	{
		_SocketManager.Socket.Emit("rematch room");
		Multiplayer.roomType = 2;
		SceneManager.LoadSceneAsync("MainMenu");
	}

	public void SendEmoticon(string _emoticon)
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject.AddField("playerID", CONTROLLER.userID);
		jSONObject.AddField("emoticonindex", _emoticon);
		_SocketManager.Socket.Emit("emoticon send", jSONObject.ToDictionary());
	}
}
