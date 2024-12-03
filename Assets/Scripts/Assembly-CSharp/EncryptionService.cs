using nxtCrypto.PackData;
using nxtCrypto.SecurityState;
using SecurityForNextwave;
using UnityEngine;

public class EncryptionService : MonoBehaviour
{
	public static EncryptionService instance;

	public NextwaveSecurity nws;

	public bool isSecure;

	private PackData mPackData = new PackData(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

	private SecurityState mSecurityState = new SecurityState();

	public int Coins => mPackData.Coins;

	public int EarnedCoins => mPackData.EarnedCoins;

	public int SpendCoins => mPackData.SpendCoins;

	public int Tickets => mPackData.Tickets;

	public int EarnedTickets => mPackData.EarnedTickets;

	public int SpendTickets => mPackData.SpendTickets;

	public int XP => mPackData.XP;

	public int EarnedXp => mPackData.EarnedXp;

	public int ArcadeXp => mPackData.ArcadeXp;

	public int EarnedArcadeXp => mPackData.EarnedArcadeXp;

	public int FreeSpin => mPackData.FreeSpin;

	public int Sixcount => mPackData.Sixcount;

	public int Powergrade => mPackData.Powergrade;

	public int Controlgrade => mPackData.Controlgrade;

	public int Agilitygrade => mPackData.Agilitygrade;

	public int TotalPowerSubGrade => mPackData.TotalPowerSubGrade;

	public int TotalControlSubGrade => mPackData.TotalControlSubGrade;

	public int TotalAgilitySubGrade => mPackData.TotalAgilitySubGrade;

	public int WeekXp => mPackData.WeekXp;

	public int WeekEarnedXp => mPackData.WeekEarnedXp;

	public int WeekArcadeXp => mPackData.WeekArcadeXp;

	public int WeekEarnedArcadeXp => mPackData.WeekEarnedArcadeXp;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			nws = new NextwaveSecurity("WCCLite", "Nextwave");
		}
		if (instance != this)
		{
			Object.DestroyImmediate(base.gameObject);
		}
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Start()
	{
	}

	public void Encrypt(PackData packdata)
	{
		isSecure = nws.Encrypt(packdata);
		if (isSecure)
		{
			mPackData = packdata;
			CONTROLLER.isGameDataSecure = true;
		}
		else
		{
			CONTROLLER.isGameDataSecure = false;
			Singleton<Google_SignIn>.instance.Force_SignOut();
		}
	}

	public void Decrypt()
	{
		isSecure = nws.Decrypt(out mPackData);
		if (isSecure)
		{
			CONTROLLER.isGameDataSecure = true;
			return;
		}
		CONTROLLER.isGameDataSecure = false;
		Singleton<Google_SignIn>.instance.Force_SignOut();
	}

	public void SaveToPlayerPrefs(PackData packData, string Key)
	{
		Encrypt(packData);
		if (isSecure)
		{
			isSecure = nws.ExportState(out mSecurityState);
			if (isSecure)
			{
				JunkData obj = new JunkData(mSecurityState);
				string value = JsonUtility.ToJson(obj);
				PlayerPrefs.SetString(Key, value);
			}
		}
		else
		{
			CONTROLLER.isGameDataSecure = false;
			Singleton<Google_SignIn>.instance.Force_SignOut();
		}
	}

	public void LoadFromPlayerPrefs(string Key)
	{
		if (PlayerPrefs.HasKey(Key))
		{
			string @string = PlayerPrefs.GetString(Key);
			JunkData junkData = new JunkData();
			junkData = JsonUtility.FromJson<JunkData>(@string);
			junkData.CopyTo(out mSecurityState);
			isSecure = nws.ImportState(mSecurityState);
			if (isSecure)
			{
				Decrypt();
				CONTROLLER.isGameDataSecure = true;
			}
			else
			{
				CONTROLLER.isGameDataSecure = false;
				Singleton<Google_SignIn>.instance.Force_SignOut();
			}
		}
	}
}
