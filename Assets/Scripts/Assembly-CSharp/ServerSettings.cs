using System.Collections;
using SimpleJSON;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerSettings : MonoBehaviour
{
	private static ServerSettings _Instance;

	private string _ServerSettingsUri;

	private string _AppUpdateUri;

	private string _AdConfigUri;

	public bool _IsAdFetched;

	private string platform;

	public static ServerSettings Instance => _Instance;

	public string ServerSettingsUri
	{
		get
		{
			if (PHPManager.Instance.isOnlinePhpServer)
			{
				_ServerSettingsUri = "http://assets.batattackcricket.com/battleofchepauk2/ServerStatus.json";
				return _ServerSettingsUri;
			}
			_ServerSettingsUri = "http://192.168.1.220/boc/serversettings/ServerStatus.json";
			return _ServerSettingsUri;
		}
		set
		{
			_ServerSettingsUri = value;
		}
	}

	public string AppUpdaetUri
	{
		get
		{
			if (PHPManager.Instance.isOnlinePhpServer)
			{
				_AppUpdateUri = "http://assets.batattackcricket.com/battleofchepauk2/AppUpdate/" + AppInfo.VersionCode + "/";
				return _AppUpdateUri;
			}
			_AppUpdateUri = "http://192.168.1.220/boc/serversettings/" + AppInfo.VersionCode + "/";
			return _AppUpdateUri;
		}
		set
		{
			_AppUpdateUri = value;
		}
	}

	public string AdConfigUri
	{
		get
		{
			if (PHPManager.Instance.isOnlinePhpServer)
			{
				_AdConfigUri = "https://s3-ap-southeast-1.amazonaws.com/batattack-assets/battleofchepauk2/AdConfig/" + platform + "/AdConfig_2.0.json";
				return _AdConfigUri;
			}
			_AdConfigUri = "http://192.168.1.220/boc/serversettings/AdConfig.json";
			return _AdConfigUri;
		}
		set
		{
			_AdConfigUri = value;
		}
	}

	private void Start()
	{
		if (_Instance == null)
		{
			_Instance = this;
			platform = "Android";
			AppUpdaetUri += "android_AppUpdate.json";
		}
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (scene.buildIndex != 1)
		{
		}
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public IEnumerator FetchServerSettings()
	{
		yield return StartCoroutine(NetworkManager.Instance.CheckInternetConnection());
		if (!NetworkManager.Instance.IsNetworkConnected)
		{
			yield break;
		}
		WWW download = new WWW(ServerSettingsUri);
		yield return download;
		if (string.IsNullOrEmpty(download.error) && !string.IsNullOrEmpty(download.text))
		{
			JSONNode jSONNode = JSONNode.Parse(download.text);
			if (string.Empty + jSONNode["ServerStatus"]["IsMaintenance"] == "true")
			{
				ServerStatus.IsMaintenance = true;
			}
			else
			{
				ServerStatus.IsMaintenance = false;
			}
		}
	}

	public IEnumerator FetchAppUpdate()
	{
		yield return StartCoroutine(NetworkManager.Instance.CheckInternetConnection());
		if (!NetworkManager.Instance.IsNetworkConnected)
		{
			yield break;
		}
		WWW download = new WWW(_AppUpdateUri);
		yield return download;
		if (!string.IsNullOrEmpty(download.error) || string.IsNullOrEmpty(download.text))
		{
			yield break;
		}
		JSONNode jSONNode = JSONNode.Parse(download.text);
		AppUpdate.LatestVersion = int.Parse(string.Empty + jSONNode["AppUpdate"]["Version"]);
		if (AppUpdate.LatestVersion > AppInfo.VersionCode)
		{
			AppUpdate.AppUpdateTitle = string.Empty + jSONNode["AppUpdate"]["Title"];
			AppUpdate.AppUpdateContent = string.Empty + jSONNode["AppUpdate"]["Content"];
			AppUpdate.AppUpdateUri = string.Empty + jSONNode["AppUpdate"]["UpdateUrl"];
			if (string.Empty + jSONNode["AppUpdate"]["CanUserPlay"] == "true")
			{
				AppUpdate.CanUserPlay = true;
			}
			else
			{
				AppUpdate.CanUserPlay = false;
			}
		}
	}
}
