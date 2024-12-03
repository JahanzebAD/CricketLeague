using UnityEngine;

public class GetAppInfo : MonoBehaviour
{
	public static string AppVersionCode;

	public static string deviceID;

	private void Start()
	{
		AppVersionCode = "18";
		deviceID = SystemInfo.deviceUniqueIdentifier;
	}

	private void GetAppdetails()
	{
		AppVersionCode = "18";
		deviceID = SystemInfo.deviceUniqueIdentifier;
	}
}
