using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreBoardMultiPlayer : MonoBehaviour
{
	public static ScoreBoardMultiPlayer instance;

	public GameObject _ScoreHandler_Multiplayer;

	public GameObject GreedyFloatingIcon;

	public GameObject spinsWon;

	public GameObject coinsWon;

	public GameObject rewardsPanel;

	public Text CurrentScore;

	public Text BallsFaced;

	public Text positionTxt;

	public Text numberTxt;

	public Text rewardsButtonText;

	public GameObject postionGO;

	public int StartingPlayersCount = 5;

	public Text MultiplayerTotalOversLabel;

	public Text[] multiplayerRank;

	public Text[] multiplayerUsername;

	public Text[] multiplayerScore;

	public Text[] multiplayerLastScore;

	public GameObject MultiplayerGameOverPage;

	public GameObject EmoticonPanel;

	public Text multiplayerPositionTxt;

	public Text multiplayerPositionTxt2;

	private string multiplayerPosSuperTxt;

	public Text multiplayerEarningsPoints;

	public Text multiplayerBonusPoints;

	public Image[] multiplayerGameOverBG;

	public Sprite[] multiplayerGameOverUserBG;

	public Text[] multiplayerGameOverRank;

	public Text[] multiplayerGameOverUsername;

	public Text[] multiplayerGameOverScore;

	public GameObject oppLeft;

	public Text oppLeftText;

	public GameObject WaitForOthersPanel;

	public GameObject WaitForOthersWicketsOverPanel;

	public GameObject[] MultiplayerPlayerScoreBG;

	public GameObject multiplayerFinalBallCountdown;

	public Text ballsLeftInMultiplayer;

	public Text coinsWonText;

	private string contentToShare = string.Empty;

	private float[] yPos = new float[5] { -76f, -102f, -128f, -154f, -181f };

	private int playerCount;

	private int tempRank;

	public GameObject[] tweenPos;

	private int totalExpInaMatch;

	private int XPFromFour;

	private int XPFromMaiden;

	private int XPFromDot;

	private int XPFromSix;

	private int XPFromWicketBowled;

	private int XPFromWicketCatch;

	private int XPFromWicketOthers;

	private int XPFromFifty;

	private int XPFromCentury;

	private int XPFromPerOver;

	private int plyrCount;

	private int[] rnk;

	private string[] uname;

	private string[] scr;

	private void CalculateXPforCurrentMatch()
	{
		XPFromFour = Singleton<GameModel>.instance.CounterFour * 48;
		XPFromSix = Singleton<GameModel>.instance.CounterSix * 72;
		XPFromFifty = Singleton<GameModel>.instance.CounterFifty * 120;
		XPFromCentury = Singleton<GameModel>.instance.CounterCentury * 240;
		XPFromPerOver = Singleton<GameModel>.instance.CounterPerOversPlayed * 12;
		totalExpInaMatch = XPFromFour + XPFromSix + XPFromFifty + XPFromCentury + XPFromPerOver;
		SavePlayerPrefs.SaveUserArcadeXPs(totalExpInaMatch, totalExpInaMatch);
		Singleton<GameModel>.instance.DeleteKeys();
	}

	public bool ReturnWaitState()
	{
		return WaitForOthersPanel.activeSelf;
	}

	public void ReplayCountdown(int time)
	{
		if (CONTROLLER.tickets >= Multiplayer.entryTickets)
		{
		}
	}

	public void ReplayMatch()
	{
		ServerManager.Instance.RematchRoom();
	}

	public void UpdateMultiplayerBallsLeft()
	{
		int num = -1;
		if (Multiplayer.overs == 2)
		{
			if (CONTROLLER.currentMatchBalls > 8)
			{
				num = 12 - CONTROLLER.currentMatchBalls;
			}
		}
		else if (Multiplayer.overs == 5 && CONTROLLER.currentMatchBalls > 23)
		{
			num = 30 - CONTROLLER.currentMatchBalls;
		}
		if (num > 0)
		{
			multiplayerFinalBallCountdown.SetActive(value: true);
			if (num == 1)
			{
				ballsLeftInMultiplayer.text = num + " " + LocalizationData.instance.getText(531);
			}
			else
			{
				ballsLeftInMultiplayer.text = num + " " + LocalizationData.instance.getText(531);
			}
		}
		else
		{
			multiplayerFinalBallCountdown.SetActive(value: false);
		}
	}

	public void ShowWait()
	{
		WaitForOthersPanel.SetActive(value: true);
	}

	public void HideWait()
	{
		WaitForOthersPanel.SetActive(value: false);
	}

	public void ShowWicketsWait()
	{
		WaitForOthersWicketsOverPanel.SetActive(value: true);
	}

	public void HideWicketsWait()
	{
		WaitForOthersWicketsOverPanel.SetActive(value: false);
	}

	public void SetMultiplayerGameOverTexts()
	{
		plyrCount = Multiplayer.playerCount;
		rnk = new int[plyrCount];
		uname = new string[plyrCount];
		scr = new string[plyrCount];
		for (int i = 0; i < plyrCount; i++)
		{
			rnk[i] = Multiplayer.playerScores[i].Rank;
			uname[i] = Multiplayer.playerScores[i].Username;
			scr[i] = Multiplayer.playerScores[i].Score;
		}
	}

	public void ShowMultiPlayerGameOver()
	{
		CalculateXPforCurrentMatch();
		CONTROLLER.CurrentPage = string.Empty;
		_ScoreHandler_Multiplayer.SetActive(value: false);
		EmoticonPanel.SetActive(value: false);
		WaitForOthersPanel.SetActive(value: false);
		WaitForOthersWicketsOverPanel.SetActive(value: false);
		multiplayerFinalBallCountdown.SetActive(value: false);
		postionGO.SetActive(value: false);
		MultiplayerGameOverPage.SetActive(value: true);
		CONTROLLER.pageName = "PopupPage";
		int num = 0;
		for (int i = 0; i < 5; i++)
		{
			if (i < plyrCount)
			{
				multiplayerGameOverRank[i].text = rnk[i].ToString();
				multiplayerGameOverUsername[i].text = uname[i];
				multiplayerGameOverScore[i].text = scr[i];
				if (uname[i] == CONTROLLER.username)
				{
					multiplayerGameOverBG[i].sprite = multiplayerGameOverUserBG[0];
					if (rnk[i] == 1)
					{
						if (Multiplayer.roomType == 0)
						{
							spinsWon.SetActive(value: true);
							SavePlayerPrefs.SaveSpins(1);
						}
						else
						{
							spinsWon.SetActive(value: false);
						}
						multiplayerPosSuperTxt = "ST";
					}
					else
					{
						spinsWon.SetActive(value: false);
						if (Multiplayer.playerScores[i].Rank == 2)
						{
							multiplayerPosSuperTxt = "ND";
						}
						else if (Multiplayer.playerScores[i].Rank == 3)
						{
							multiplayerPosSuperTxt = "RD";
						}
						else
						{
							multiplayerPosSuperTxt = "TH";
						}
					}
					int num2 = 0;
					coinsWon.SetActive(value: false);
					if (i != StartingPlayersCount - 1 && Multiplayer.roomType == 0)
					{
						if (rnk[i] == 1)
						{
							num2 = 1000;
						}
						else if (rnk[i] == 2)
						{
							num2 = 750;
						}
						else if (rnk[i] == 3)
						{
							num2 = 500;
						}
						else if (rnk[i] == 4)
						{
							num2 = 250;
						}
						if (num2 > 0)
						{
							coinsWon.SetActive(value: true);
							coinsWonText.text = num2.ToString();
							SavePlayerPrefs.SaveUserCoins(num2, 0, num2);
						}
					}
					multiplayerPositionTxt.text = rnk[i].ToString();
					multiplayerPositionTxt2.text = multiplayerPosSuperTxt;
					if (plyrCount == 1)
					{
						if (num == 0)
						{
							contentToShare = CONTROLLER.username + " came " + rnk[i] + multiplayerPosSuperTxt + " in WCC Lite - " + (StartingPlayersCount + 1) + " player Multiplayer match.";
						}
						num = 1;
					}
					else
					{
						if (num == 0)
						{
							contentToShare = CONTROLLER.username + " came " + rnk[i] + multiplayerPosSuperTxt + " in WCC Lite - " + StartingPlayersCount + " player Multiplayer match.";
						}
						num = 1;
					}
					multiplayerGameOverRank[i].color = Color.black;
					multiplayerGameOverUsername[i].color = Color.black;
					multiplayerGameOverScore[i].color = Color.black;
				}
				else
				{
					multiplayerGameOverBG[i].sprite = multiplayerGameOverUserBG[1];
					multiplayerGameOverRank[i].color = Color.white;
					multiplayerGameOverUsername[i].color = Color.white;
					multiplayerGameOverScore[i].color = Color.white;
				}
			}
			else
			{
				multiplayerGameOverBG[i].gameObject.SetActive(value: false);
				multiplayerGameOverRank[i].text = string.Empty;
				multiplayerGameOverUsername[i].text = string.Empty;
				multiplayerGameOverScore[i].text = string.Empty;
			}
		}
		multiplayerEarningsPoints.text = totalExpInaMatch.ToString();
		if (Multiplayer.roomType == 0)
		{
			if (Multiplayer.overs == 2)
			{
				//FirebaseAnalyticsManager.instance.logEvent("MP_PB_2_Rank", "Multiplayer", CONTROLLER.userID);
			}
			else if (Multiplayer.overs == 5)
			{
				//FirebaseAnalyticsManager.instance.logEvent("MP_PB_5_Rank", "Multiplayer", CONTROLLER.userID);
			}
		}
		else if (Multiplayer.overs == 2)
		{
			//FirebaseAnalyticsManager.instance.logEvent("MP_PV_2_Rank", "Multiplayer", CONTROLLER.userID);
		}
		else if (Multiplayer.overs == 5)
		{
			//FirebaseAnalyticsManager.instance.logEvent("MP_PV_5_Rank", "Multiplayer", CONTROLLER.userID);
		}
		ServerManager.Instance.Disconnect();
		Multiplayer.roomType = -1;
	}

	public void ToggleRewardsPanel()
	{
		if (rewardsButtonText.text.ToUpper() == "REWARDS")
		{
			rewardsPanel.SetActive(value: true);
			rewardsButtonText.text = "CLOSE";
		}
		else
		{
			rewardsPanel.SetActive(value: false);
			rewardsButtonText.text = "REWARDS";
		}
	}

	public void ShareMultiplayer()
	{
		string text = "WCC Lite";
		string text2 = contentToShare + " You can download WCC Lite from " + CONTROLLER.BOC_2_Link;
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
		AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent");
		androidJavaObject.Call<AndroidJavaObject>("setAction", new object[1] { androidJavaClass.GetStatic<string>("ACTION_SEND") });
		androidJavaObject.Call<AndroidJavaObject>("setType", new object[1] { "text/plain" });
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
		{
			androidJavaClass.GetStatic<string>("EXTRA_SUBJECT"),
			text
		});
		androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
		{
			androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
			text2
		});
		AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass2.GetStatic<AndroidJavaObject>("currentActivity");
		@static.Call("startActivity", androidJavaObject);
	}

	public void GoToMainMenu()
	{
		ServerManager.Instance.ExitRoom();
		Singleton<GameModel>.instance.ResetCurrentMatchDetails();
		Singleton<GameModel>.instance.ResetVariables();
		Singleton<GameModel>.instance.ResetAllLocalVariables();
		CONTROLLER.PlayModeSelected = -1;
		GroundScriptHandler.Instance.ShowLoadingScreen();
		Singleton<NavigationBack>.instance.deviceBack = null;
		SceneManager.LoadSceneAsync("MainMenu");
	}

	public void SortAndRankScores()
	{
		MultiplayerScore[] array = new MultiplayerScore[Multiplayer.playerCount];
		MultiplayerScore multiplayerScore = array[0];
		for (int i = 0; i < Multiplayer.playerCount; i++)
		{
			array[i] = Multiplayer.playerScores[i];
		}
		for (int j = 0; j < array.Length; j++)
		{
			for (int k = j + 1; k < array.Length; k++)
			{
				if (int.Parse(array[j].Score) < int.Parse(array[k].Score))
				{
					multiplayerScore = array[j];
					array[j] = array[k];
					array[k] = multiplayerScore;
				}
			}
		}
		for (int l = 0; l < array.Length; l++)
		{
			for (int m = l + 1; m < array.Length; m++)
			{
				if (int.Parse(array[l].Score) == int.Parse(array[m].Score) && array[l].Wickets > array[m].Wickets)
				{
					multiplayerScore = array[l];
					array[l] = array[m];
					array[m] = multiplayerScore;
				}
			}
		}
		int num = 1;
		int num2 = 1;
		for (int n = 0; n < array.Length; n++)
		{
			if (n < array.Length - 1)
			{
				if (array[n].Score == array[n + 1].Score && array[n].Wickets == array[n + 1].Wickets)
				{
					array[n].Rank = num;
					array[n].RankPos = num2;
					num2++;
				}
				else
				{
					array[n].Rank = num;
					num++;
					array[n].RankPos = num2;
					num2++;
				}
			}
			else if (array[n - 1].Score == array[n].Score && array[n - 1].Wickets == array[n].Wickets)
			{
				array[n].Rank = num;
				array[n].RankPos = num2;
				num2++;
			}
			else
			{
				array[n].Rank = num;
				num++;
				array[n].RankPos = num2;
				num2++;
			}
		}
		UpdateMultiplayerScores();
	}

	public void UpdateMultiplayerScores()
	{
		for (int i = 0; i < 5; i++)
		{
			if (i < Multiplayer.playerCount)
			{
				multiplayerRank[i].text = Multiplayer.playerScores[i].Rank.ToString();
				multiplayerUsername[i].text = Multiplayer.playerScores[i].Username;
				multiplayerScore[i].text = Multiplayer.playerScores[i].Score.ToString() + "/" + Multiplayer.playerScores[i].Wickets;
				multiplayerLastScore[i].text = Multiplayer.playerScores[i].LastBallScore.ToString();
				MultiplayerPlayerScoreBG[i].SetActive(value: true);
				if (Multiplayer.playerScores[i].PlayerId == CONTROLLER.userID)
				{
					if (Multiplayer.playerScores[i].Rank == 1)
					{
						numberTxt.text = 1.ToString();
						positionTxt.text = "ST";
					}
					else if (Multiplayer.playerScores[i].Rank == 2)
					{
						numberTxt.text = 2.ToString();
						positionTxt.text = "ND";
					}
					else if (Multiplayer.playerScores[i].Rank == 3)
					{
						numberTxt.text = 3.ToString();
						positionTxt.text = "RD";
					}
					else
					{
						numberTxt.text = Multiplayer.playerScores[i].Rank.ToString();
						positionTxt.text = "TH";
					}
					multiplayerUsername[i].color = new Color32(byte.MaxValue, 175, 24, byte.MaxValue);
				}
				else
				{
					multiplayerUsername[i].color = Color.white;
				}
			}
			else
			{
				multiplayerRank[i].text = string.Empty;
				multiplayerUsername[i].text = string.Empty;
				multiplayerScore[i].text = string.Empty;
				multiplayerLastScore[i].text = string.Empty;
				MultiplayerPlayerScoreBG[i].SetActive(value: false);
			}
		}
		UpdateScoreCard();
		AnimateBoard();
		if (playerCount != Multiplayer.playerCount)
		{
			playerCount = Multiplayer.playerCount;
		}
	}

	public void AnimateBoard()
	{
		for (int i = 0; i < Multiplayer.playerCount; i++)
		{
			tempRank = Multiplayer.playerScores[i].RankPos;
			if (tempRank == 0 || tempRank > Multiplayer.playerScores.Length)
			{
				break;
			}
			tweenPos[i].transform.DOLocalMoveY(yPos[tempRank - 1], 1f);
		}
	}

	public void OnCompleteAnimateBoard(GameObject go)
	{
	}

	protected void Awake()
	{
		yPos = new float[5] { 24.65f, -15.05f, -54.75f, -94.45f, -134.15f };
		instance = this;
		playerCount = Multiplayer.playerCount;
		StartingPlayersCount = Multiplayer.playerCount;
		Hide(boolean: true);
	}

	public void pauseGame()
	{
		Singleton<GameModel>.instance.GamePaused(boolean: true);
	}

	public void UpdateScoreCard()
	{
		CurrentScore.text = (string.Empty + Singleton<GameModel>.instance.ScoreStr).ToUpper();
		BallsFaced.text = "(" + Singleton<GameModel>.instance.OversStr + ")";
		if (BallsFaced.text == "()")
		{
			BallsFaced.text = "(0.0)";
		}
		MultiplayerTotalOversLabel.text = Multiplayer.overs + LocalizationData.instance.getText(184) + " :";
	}

	public IEnumerator NewOver()
	{
		yield return new WaitForSeconds(0.01f);
		CONTROLLER.ballUpdate[0] = string.Empty;
		CONTROLLER.ballUpdate[1] = string.Empty;
		CONTROLLER.ballUpdate[2] = string.Empty;
		CONTROLLER.ballUpdate[3] = string.Empty;
		CONTROLLER.ballUpdate[4] = string.Empty;
		CONTROLLER.ballUpdate[5] = string.Empty;
	}

	public void Hide(bool boolean)
	{
		float num = 1.33333337f;
		float num2 = Screen.width;
		float num3 = Screen.height;
		float num4 = num2 / num3;
		CONTROLLER.xOffSet = (num - num4) * 300f;
		Singleton<PreviewScreen>.instance.Hide(boolean);
		if (boolean)
		{
			_ScoreHandler_Multiplayer.SetActive(value: false);
			WaitForOthersPanel.SetActive(value: false);
			WaitForOthersWicketsOverPanel.SetActive(value: false);
			postionGO.SetActive(value: false);
			MultiplayerGameOverPage.SetActive(value: false);
			EmoticonPanel.SetActive(value: false);
			multiplayerFinalBallCountdown.SetActive(value: false);
			return;
		}
		UpdateScoreCard();
		GreedyFloatingIcon.SetActive(value: false);
		if (CONTROLLER.receivedAdEvent)
		{
			//Singleton<AdIntegrate>.instance.HideAd();
		}
		CONTROLLER.pageName = string.Empty;
		_ScoreHandler_Multiplayer.SetActive(value: true);
		postionGO.SetActive(value: true);
		EmoticonPanel.SetActive(value: true);
	}

	public void FadeThisObject(bool state)
	{
	}

	public void watchVideo()
	{
	}

	private void ShowToast()
	{
		GameObject original = Resources.Load("Prefabs/Toast") as GameObject;
		GameObject gameObject = Object.Instantiate(original);
		gameObject.name = "Toast";
		gameObject.GetComponent<Toast>().setMessge("No video Available");
	}
}
