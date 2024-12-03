using System.IO;
using CodeStage.AntiCheat.ObscuredTypes;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : Singleton<LeaderBoard>
{
	public GameObject common;

	public GameObject arcade;

	public GameObject holder;

	private string jsonString;

	private JsonData playerData;

	public new Text[] name;

	public Text[] xp;

	public Text[] arcadeName;

	public Text[] arcadeXp;

	public Text[] rank;

	public Text[] arcadeRank;

	public Text myName;

	public Text myXp;

	public Text myRank;

	public Text desc;

	public Text myArcadeName;

	public Text myArcadeXp;

	public Text myArcadeRank;

	private JsonData playerJson;

	private int CallLeaderboard;

	private void Start()
	{
		holder.SetActive(value: false);
	}

	private void ValidateLeaderBoard()
	{
		jsonString = File.ReadAllText(Application.dataPath + "/Resources/DummyLeaderboard.json");
		playerData = JsonMapper.ToObject(jsonString);
		myXp.text = CONTROLLER.XPs.ToString();
		myName.text = LocalizationData.instance.getText(547);
		for (int i = 0; i < name.Length; i++)
		{
			name[i].text = playerData["LeaderBoard"][i]["name"].ToString();
			xp[i].text = playerData["LeaderBoard"][i]["points"].ToString();
		}
	}

	public void ResetWeeklyLeaderBoard()
	{
		ObscuredPrefs.SetInt("weeklyXP", 0);
		CONTROLLER.weekly_Xps = 0;
		SavePlayerPrefs.EncryptionProtectData();
		SavePlayerPrefs.SaveWeeklyXps();
	}

	public void ResetWeeklyArcadeLeaderBoard()
	{
		ObscuredPrefs.SetInt("weeklyArcadeXP", 0);
		CONTROLLER.weekly_ArcadeXps = 0;
		SavePlayerPrefs.EncryptionProtectData();
		SavePlayerPrefs.SaveWeeklyArcadeXps();
	}

	public void SwitchLeaderboard()
	{
		if (desc.text == LocalizationData.instance.getText(302))
		{
			if (CallLeaderboard == 0)
			{
				Server_Connection.instance.Get_User_ArcadeLeaderboard();
				CallLeaderboard = 1;
			}
			arcade.SetActive(value: true);
			common.SetActive(value: false);
			desc.text = LocalizationData.instance.getText(305);
		}
		else
		{
			arcade.SetActive(value: false);
			common.SetActive(value: true);
			desc.text = LocalizationData.instance.getText(302);
		}
	}

	//public void ShowMe()
	//{
	//	if (Singleton<AdIntegrate>.instance.CheckForInternet())
	//	{
	//		int num = 1;
	//		num = 1;
	//		if (Singleton<Google_SignIn>.instance.googleAuthenticated == 1)
	//		{
	//			Singleton<NavigationBack>.instance.deviceBack = HideMe;
	//			desc.text = LocalizationData.instance.getText(302);
	//			Reset_Leaderboard();
	//			if (ManageScene.activeSceneName() == "MainMenu")
	//			{
	//				Singleton<GameModeTWO>.instance.Holder.SetActive(value: false);
	//			}
	//			Server_Connection.instance.Get_User_Leaderboard();
	//			if (CONTROLLER.canShowbannerMainmenu == 1)
	//			{
	//				Singleton<AdIntegrate>.instance.ShowAd();
	//			}
	//			holder.SetActive(value: true);
	//			if (ManageScene.activeSceneName() == "Ground")
	//			{
	//				Singleton<GameOverDisplay>.instance.HideMe();
	//				if (CONTROLLER.PlayModeSelected == 4)
	//				{
	//					Singleton<SuperOverResult>.instance.EarningsIntheMatch.SetActive(value: false);
	//					Singleton<SuperOverResult>.instance.Holder.SetActive(value: false);
	//				}
	//				else if (CONTROLLER.PlayModeSelected == 5)
	//				{
	//					Singleton<SuperChaseResult>.instance.EarningsInTheMatch.SetActive(value: false);
	//					Singleton<SuperChaseResult>.instance.holder.SetActive(value: false);
	//				}
	//			}
	//			arcade.SetActive(value: false);
	//			common.SetActive(value: true);
	//			Singleton<LeaderBoardTWOTransisitionPanel>.instance.ResetTransistion();
	//			CONTROLLER.pageName = "leaderboard";
	//			Singleton<LeaderBoardTWOTransisitionPanel>.instance.ResetTransistion();
	//		}
	//		else
	//		{
	//			CONTROLLER.PopupName = "googlesignin";
	//			Singleton<Popups>.instance.ShowMe();
	//		}
	//	}
	//	else
	//	{
	//		CONTROLLER.PopupName = "noInternet";
	//		Singleton<Popups>.instance.ShowMe();
	//	}
	//}

	public void HideMe()
	{
		CallLeaderboard = 0;
		holder.SetActive(value: false);
		Singleton<LoadingPanelTransition>.instance.HideMe();
		if (ManageScene.activeSceneName() == "MainMenu")
		{
			Singleton<GameModeTWO>.instance.ShowWithoutAnim();
			CONTROLLER.pageName = "landingPage";
			//Singleton<AdIntegrate>.instance.HideAd();
		}
		else if (CONTROLLER.PlayModeSelected == 4)
		{
			Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
			Singleton<SuperOverResult>.instance.Holder.SetActive(value: true);
			if (Singleton<SuperOverResult>.instance.pageNumber == 1)
			{
				Singleton<SuperOverResult>.instance.MatchStatsScreen.SetActive(value: true);
			}
			else
			{
				Singleton<SuperOverResult>.instance.EarningsIntheMatch.SetActive(value: true);
			}
		}
		else if (CONTROLLER.PlayModeSelected == 5)
		{
			Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
			Singleton<SuperChaseResult>.instance.holder.SetActive(value: true);
			if (Singleton<SuperChaseResult>.instance.pageNumber == 2)
			{
				Singleton<SuperChaseResult>.instance.EarningsInTheMatch.SetActive(value: true);
			}
			else
			{
				Singleton<SuperChaseResult>.instance.MatchStatsResult.SetActive(value: true);
			}
		}
		else if (CONTROLLER.PlayModeSelected == 7)
		{
			Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
			Singleton<TMGameOverDisplay>.instance.holder.SetActive(value: true);
			Singleton<TMGameOverDisplay>.instance.screenTwo.SetActive(value: true);
		}
		else
		{
			Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
			Singleton<GameOverDisplay>.instance.holder.SetActive(value: true);
			if (GameOverDisplay.pageNumber == 2)
			{
				Singleton<GameOverDisplay>.instance.screenTwo.SetActive(value: true);
			}
			else
			{
				Singleton<GameOverDisplay>.instance.screenOne.SetActive(value: true);
			}
		}
	}

	public void Reset_Leaderboard()
	{
		for (int i = 0; i <= 10; i++)
		{
			name[i].text = "-";
			xp[i].text = "-";
			arcadeName[i].text = "-";
			arcadeXp[i].text = "-";
			rank[i].text = "-";
			arcadeRank[i].text = "-";
		}
		myName.text = "-";
		myXp.text = "-";
		myRank.text = "-";
		myArcadeName.text = "-";
		myArcadeXp.text = "-";
		myArcadeRank.text = "-";
	}
}
