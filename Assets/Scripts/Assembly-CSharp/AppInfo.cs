public class AppInfo
{
	public static string AppName = "WCC Lite";

	public static string Version = "1.2.1";

	public static int BuildNumber = 18;

	public static int VersionCode = 1;

	public static string Platform = CONTROLLER.TargetPlatform;

	public static string senderID = "616246148531";

	public static string DeviceID = string.Empty;

	public static string deviceRegistrationID = string.Empty;

	public static string Flurry_Code
	{
		get
		{
			string result = string.Empty;
			if (Platform == "android")
			{
				result = "SGNT5PHNVWF6KT3BY5WG";
			}
			else if (Platform == "ios")
			{
				result = "SGNT5PHNVWF6KT3BY5WG";
			}
			return result;
		}
	}
}
