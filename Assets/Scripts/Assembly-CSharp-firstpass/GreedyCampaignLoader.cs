using System.Collections.Generic;
//using GreedyGame.Commons;
using GreedyGame.Runtime;
using UnityEngine;

public class GreedyCampaignLoader : SingletoneBase<GreedyCampaignLoader>
{
	public class GreedyAgentListener /*: IAgentListener*/
	{
		public void onAvailable(string campaignId)
		{
			Debug.Log("GreedyAgentListener onAvailable");
		}

		public void onUnavailable()
		{
			Debug.Log("GreedyAgentListener onUnavailable");
		}

		public void onError(string error)
		{
			Debug.Log("GreedyAgentListener onError");
		}
	}

	public List<string> unitList;

	public bool AdMobMediation;

	public bool MoPubMediation;

	public string GameId = "60933686";

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	public void Call_GreedyAdInit()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			GGAdConfig gGAdConfig = new GGAdConfig();
			//gGAdConfig.setListener(new GreedyAgentListener());
			//gGAdConfig.setGameId(GameId);
			//gGAdConfig.enableAdmobMediation(AdMobMediation);
			//gGAdConfig.enableMopubMediation(MoPubMediation);
			//gGAdConfig.addUnitList(unitList);
			//GreedyGameAgent.Instance.init(gGAdConfig);
		}
	}

	private static void moveToNextScene()
	{
		if (Application.loadedLevel == 0)
		{
			Application.LoadLevel(1);
		}
	}

	public void refreshGreedyAds()
	{
		//GreedyGameAgent.Instance.startEventRefresh();
	}
}
