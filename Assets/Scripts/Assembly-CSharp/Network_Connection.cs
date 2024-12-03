using System.Collections;
using UnityEngine;

public class Network_Connection : MonoBehaviour
{
	public static bool isNetworkConnected;

	public static Network_Connection instance;

	public Network_Connection Instance => instance;

	private void Awake()
	{
		if (!(instance != null) || !(instance != this))
		{
			instance = this;
		}
	}

	private void Start()
	{
		isNetworkConnected = false;
		Check_Internet();
		//InitiateAllAdNetworks();
	}

	public void Check_Internet()
	{
		StartCoroutine("CheckInternetConnection");
	}

	public IEnumerator CheckInternetConnection()
	{
		//isNetworkConnected = Singleton<AdIntegrate>.instance.CheckForInternet();
		yield return null;
	}

	//public void InitiateAllAdNetworks()
	//{
	//	if (Application.loadedLevelName == "MainMenu")
	//	{
	//		Singleton<AdIntegrate>.instance.InitiateAllAdNetworks();
	//	}
	//}
}
