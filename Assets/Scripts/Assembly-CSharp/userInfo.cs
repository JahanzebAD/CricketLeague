using UnityEngine;

public class userInfo : Singleton<userInfo>
{
	public GameObject Holder;

	public void Start()
	{
		Holder.SetActive(value: false);
	}

	public void hideMe()
	{
		Holder.SetActive(value: false);
		Singleton<GameModeTWO>.instance.showMe();
		Singleton<Google_SignIn>.instance.GoogleSignOut();
	}

	public void CancelMe()
	{
		Holder.SetActive(value: false);
		Singleton<GameModeTWO>.instance.showMe();
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = CancelMe;
		Holder.SetActive(value: true);
	}
}
