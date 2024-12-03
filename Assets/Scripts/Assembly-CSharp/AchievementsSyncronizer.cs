using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using SimpleJSON;
using UnityEngine;

public class AchievementsSyncronizer : Singleton<AchievementsSyncronizer>
{
	public string achDetails;

	public string toUpdateAchDetails;

	public string kitDetails;

	public string toUpdateKitDetails;

	private readonly string toUpdateAchResetValue = "0-0-0|0-0-0|0-0-0|0-0-0|0-0-0|0-0-0|0-0-0|0-0-0|0-0-0|0-0-0|0-0-0|0-0-0|0-0-0|0-0-0|0-0-0";

	private readonly string achResetValue = "1-0-0|1-0-0|1-0-0|1-0-0|1-0-0|1-0-0|1-0-0|1-0-0|1-0-0|1-0-0|1-0-0|1-0-0|1-0-0|1-0-0|1-0-0";

	private readonly string kitResetValue = "0|0|0|0|0";

	private readonly string toUpdateKitResetValue = "0-0|0-0|0-0|0-0|0-0";

	public bool isRetrived;

	private int userId = -1;

	private void Start()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		if (ObscuredPrefs.HasKey("achDetails"))
		{
			achDetails = ObscuredPrefs.GetString("achDetails");
		}
		else
		{
			achDetails = achResetValue;
			ObscuredPrefs.SetString("achDetails", achDetails);
		}
		if (ObscuredPrefs.HasKey("kitDetails"))
		{
			kitDetails = ObscuredPrefs.GetString("kitDetails");
		}
		else
		{
			kitDetails = kitResetValue;
			ObscuredPrefs.SetString("kitDetails", kitDetails);
		}
		if (ObscuredPrefs.HasKey("toUpdateAchDetails"))
		{
			toUpdateAchDetails = ObscuredPrefs.GetString("toUpdateAchDetails");
		}
		else
		{
			toUpdateAchDetails = toUpdateAchResetValue;
			ObscuredPrefs.SetString("toUpdateAchDetails", toUpdateAchDetails);
		}
		if (ObscuredPrefs.HasKey("toUpdateKitDetails"))
		{
			toUpdateKitDetails = ObscuredPrefs.GetString("toUpdateKitDetails");
			return;
		}
		toUpdateKitDetails = toUpdateKitResetValue;
		ObscuredPrefs.SetString("toUpdateKitDetails", toUpdateKitDetails);
	}

	public IEnumerator RetriveAchievements()
	{
		if (!isRetrived)
		{
			if (ObscuredPrefs.HasKey("achDetails"))
			{
				achDetails = ObscuredPrefs.GetString("achDetails");
			}
			else
			{
				achDetails = achResetValue;
				ObscuredPrefs.SetString("achDetails", achDetails);
			}
			if (ObscuredPrefs.HasKey("kitDetails"))
			{
				kitDetails = ObscuredPrefs.GetString("kitDetails");
			}
			else
			{
				kitDetails = kitResetValue;
				ObscuredPrefs.SetString("kitDetails", kitDetails);
			}
			Retrive_ToUpdateDetails();
			if (!Server_Connection.instance.guestUser)
			{
				userId = CONTROLLER.userID;
			}
			else if (Server_Connection.instance.guestUser)
			{
				userId = Google_SignIn.guestUserID;
			}
			if (userId != 0 && userId != -1)
			{
				yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
				if (Network_Connection.isNetworkConnected && CONTROLLER.isGameDataSecure)
				{
					WWWForm form1 = new WWWForm();
					form1.AddField("action", "GetAchievements");
					form1.AddField("uid", userId);
					StartCoroutine(Server_Connection.instance.CheckForTimeOut());
					WWW download1 = new WWW(CONTROLLER.PHP_Server_Link, form1);
					yield return download1;
					if (download1.error == null)
					{
						JSONNode node1 = JSONNode.Parse(download1.text);
						if (string.Empty + node1["GetAchievements"]["status"] == "1" || string.Empty + node1["GetAchievements"]["status"] == "2")
						{
							string tempKitDetails = node1["GetAchievements"]["KitDetails"];
							string tempAchDetails = node1["GetAchievements"]["AchDetails"];
							if (ObscuredPrefs.HasKey("toUpdateAchDetails") && ObscuredPrefs.HasKey("toUpdateKitDetails"))
							{
								toUpdateAchDetails = ObscuredPrefs.GetString("toUpdateAchDetails");
								toUpdateKitDetails = ObscuredPrefs.GetString("toUpdateKitDetails");
								if (toUpdateAchDetails != toUpdateAchResetValue && toUpdateKitDetails != toUpdateKitResetValue)
								{
									yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
									if (Network_Connection.isNetworkConnected && CONTROLLER.isGameDataSecure)
									{
										WWWForm form2 = new WWWForm();
										form2.AddField("action", "UpdateAchievements");
										form2.AddField("uid", userId);
										string tempTotalAch = AddAchievments(tempAchDetails, toUpdateAchDetails);
										form2.AddField("AchDetails", tempTotalAch);
										string tempTotalKit = AddKitDetails(tempKitDetails, toUpdateKitDetails);
										form2.AddField("KitDetails", tempTotalKit);
										WWW download2 = new WWW(CONTROLLER.PHP_Server_Link, form2);
										yield return download2;
										if (download2.error == null)
										{
											JSONNode jSONNode = JSONNode.Parse(download2.text);
											if (string.Empty + jSONNode["UpdateAchievements"]["status"] == "1")
											{
												achDetails = tempTotalAch;
												kitDetails = tempTotalKit;
												toUpdateAchDetails = toUpdateAchResetValue;
												toUpdateKitDetails = toUpdateKitResetValue;
												ObscuredPrefs.SetString("achDetails", achDetails);
												ObscuredPrefs.SetString("toUpdateAchDetails", toUpdateAchDetails);
												ObscuredPrefs.SetString("kitDetails", kitDetails);
												ObscuredPrefs.SetString("toUpdateKitDetails", toUpdateKitDetails);
												isRetrived = true;
											}
										}
									}
								}
								else
								{
									isRetrived = true;
									achDetails = tempAchDetails;
									kitDetails = tempKitDetails;
								}
							}
							else
							{
								toUpdateAchDetails = toUpdateAchResetValue;
								ObscuredPrefs.SetString("toUpdateAchDetails", toUpdateAchDetails);
								toUpdateKitDetails = toUpdateKitResetValue;
								ObscuredPrefs.SetString("toUpdateKitDetails", toUpdateKitDetails);
								isRetrived = true;
							}
						}
						else
						{
							Retrive_ToUpdateDetails();
						}
					}
					else
					{
						Retrive_ToUpdateDetails();
					}
					Singleton<LoadingPanelTransition>.instance.HideMe();
				}
				else
				{
					Retrive_ToUpdateDetails();
				}
			}
			else
			{
				Retrive_ToUpdateDetails();
			}
			if (isRetrived)
			{
				UpdateAchievmentsInIndividualPrefs();
				UpdateKitsInIndividualPrefs();
			}
		}
		Singleton<LoadingPanelTransition>.instance.HideMe();
	}

	public void UpdateAchievement(int index)
	{
		string[] array = achDetails.Split('|');
		string[] array2 = toUpdateAchDetails.Split('|');
		string[] array3 = array[index - 1].Split('-');
		string[] array4 = array2[index - 1].Split('-');
		string text = AchievementTable.AchievementName[index];
		string key = text + " Level";
		string key2 = text + " ClaimedAt";
		string key3 = text + " Limit";
		int num = (ObscuredPrefs.HasKey(text) ? ObscuredPrefs.GetInt(text) : 0);
		int num2 = ((!ObscuredPrefs.HasKey(key3)) ? 25 : ObscuredPrefs.GetInt(key3));
		int num3 = ((!ObscuredPrefs.HasKey(key)) ? 1 : ObscuredPrefs.GetInt(key));
		int num4 = (ObscuredPrefs.HasKey(key2) ? ObscuredPrefs.GetInt(key2) : 0);
		num++;
		if (num == num2)
		{
			num3++;
			array4[0] = (int.Parse(array4[0]) + 1).ToString();
			num = 0;
			array4[1] = "0";
			num4 = 0;
			array4[2] = "0";
		}
		else
		{
			array4[1] = (int.Parse(array4[1]) + 1).ToString();
		}
		array2[index - 1] = array4[0] + "-" + array4[1] + "-" + array4[2];
		array[index - 1] = num3 + "-" + num + "-" + num4;
		string text2 = string.Empty;
		string text3 = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text2 = text2 + array[i] + "|";
			text3 = text3 + array2[i] + "|";
		}
		achDetails = text2.Remove(text2.Length - 1);
		toUpdateAchDetails = text3.Remove(text3.Length - 1);
		ObscuredPrefs.SetString("achDetails", achDetails);
		ObscuredPrefs.SetString("toUpdateAchDetails", toUpdateAchDetails);
	}

	public void UpdateClaimAt(int index)
	{
		string[] array = achDetails.Split('|');
		string[] array2 = toUpdateAchDetails.Split('|');
		string[] array3 = array[index - 1].Split('-');
		string[] array4 = array2[index - 1].Split('-');
		string text = AchievementTable.AchievementName[index];
		string key = text + " ClaimedAt";
		int num = (ObscuredPrefs.HasKey(key) ? ObscuredPrefs.GetInt(key) : 0);
		array3[2] = num.ToString();
		array4[2] = num.ToString();
		array[index - 1] = array3[0] + "-" + array3[1] + "-" + array3[2];
		array2[index - 1] = array4[0] + "-" + array4[1] + "-" + array4[2];
		string text2 = string.Empty;
		string text3 = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text2 = text2 + array[i] + "|";
			text3 = text3 + array2[i] + "|";
		}
		achDetails = text2.Remove(text2.Length - 1);
		toUpdateAchDetails = text3.Remove(text3.Length - 1);
		ObscuredPrefs.SetString("achDetails", achDetails);
		ObscuredPrefs.SetString("toUpdateAchDetails", toUpdateAchDetails);
	}

	public void UpdateKit(int index)
	{
		string[] array = kitDetails.Split('|');
		string[] array2 = toUpdateKitDetails.Split('|');
		string[] array3 = array2[index - 1].Split('-');
		string key = KitTable.KitName[index];
		int @int = ObscuredPrefs.GetInt(key);
		array[index - 1] = @int.ToString();
		if (array3[1] == "1")
		{
			array3[0] = @int.ToString();
		}
		else
		{
			array3[0] = (int.Parse(array3[0]) + 1).ToString();
		}
		array2[index - 1] = array3[0] + "-" + array3[1];
		string text = string.Empty;
		string text2 = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text = text + array[i] + "|";
			text2 = text2 + array2[i] + "|";
		}
		kitDetails = text.Remove(text.Length - 1);
		toUpdateKitDetails = text2.Remove(text2.Length - 1);
		ObscuredPrefs.SetString("kitDetails", kitDetails);
		ObscuredPrefs.SetString("toUpdateKitDetails", toUpdateKitDetails);
	}

	public void UpdateKitFromIndividualPrefs()
	{
		string[] array = kitDetails.Split('|');
		string[] array2 = toUpdateKitDetails.Split('|');
		string text = string.Empty;
		string text2 = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			string key = KitTable.KitName[i + 1];
			int @int = ObscuredPrefs.GetInt(key);
			string[] array3 = array2[i].Split('-');
			if (array3[1] == "1")
			{
				array3[0] = @int.ToString();
			}
			else
			{
				array3[0] = int.Parse(array3[0]) + (@int - int.Parse(array[i])).ToString();
			}
			array2[i] = array3[0] + "-" + array3[1];
			array[i] = @int.ToString();
			text = text + array[i] + "|";
			text2 = text2 + array2[i] + "|";
		}
		kitDetails = text.Remove(text.Length - 1);
		toUpdateKitDetails = text2.Remove(text2.Length - 1);
		ObscuredPrefs.SetString("kitDetails", kitDetails);
		ObscuredPrefs.SetString("toUpdateKitDetails", toUpdateKitDetails);
	}

	public void ResetKit(int index)
	{
		string[] array = kitDetails.Split('|');
		string[] array2 = toUpdateKitDetails.Split('|');
		array[index - 1] = "0";
		array2[index - 1] = "0-1";
		string text = string.Empty;
		string text2 = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text = text + array[i] + "|";
			text2 = text2 + array2[i] + "|";
		}
		kitDetails = text.Remove(text.Length - 1);
		toUpdateKitDetails = text2.Remove(text2.Length - 1);
		ObscuredPrefs.SetString("kitDetails", kitDetails);
		ObscuredPrefs.SetString("toUpdateKitDetails", toUpdateKitDetails);
	}

	public IEnumerator SendAchievementsAndKits()
	{
		achDetails = ObscuredPrefs.GetString("achDetails");
		kitDetails = ObscuredPrefs.GetString("kitDetails");
		userId = -1;
		if (!Server_Connection.instance.guestUser)
		{
			userId = CONTROLLER.userID;
		}
		else if (Server_Connection.instance.guestUser)
		{
			userId = Google_SignIn.guestUserID;
		}
		if (userId == 0 || userId == -1)
		{
			yield break;
		}
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (!Network_Connection.isNetworkConnected || !CONTROLLER.isGameDataSecure)
		{
			yield break;
		}
		if (!isRetrived)
		{
			WWWForm form1 = new WWWForm();
			form1.AddField("action", "GetAchievements");
			form1.AddField("uid", userId);
			WWW download1 = new WWW(CONTROLLER.PHP_Server_Link, form1);
			yield return download1;
			if (download1.error == null)
			{
				JSONNode jSONNode = JSONNode.Parse(download1.text);
				if (string.Empty + jSONNode["GetAchievements"]["status"] == "1" || string.Empty + jSONNode["GetAchievements"]["status"] == "2")
				{
					string ach = jSONNode["GetAchievements"]["AchDetails"];
					string kit = jSONNode["GetAchievements"]["KitDetails"];
					toUpdateAchDetails = ObscuredPrefs.GetString("toUpdateAchDetails");
					toUpdateKitDetails = ObscuredPrefs.GetString("toUpdateKitDetails");
					string tempTotalAch = AddAchievments(ach, toUpdateAchDetails);
					string tempTotalKit = AddKitDetails(kit, toUpdateKitDetails);
					StartCoroutine(_SendAchievementsAndKits(tempTotalAch, tempTotalKit));
				}
			}
		}
		else
		{
			StartCoroutine(_SendAchievementsAndKits(achDetails, kitDetails));
		}
	}

	private IEnumerator _SendAchievementsAndKits(string tempTotalAch, string tempTotalKit)
	{
		yield return StartCoroutine(Network_Connection.instance.CheckInternetConnection());
		if (!Network_Connection.isNetworkConnected || !CONTROLLER.isGameDataSecure)
		{
			yield break;
		}
		WWWForm form2 = new WWWForm();
		form2.AddField("action", "UpdateAchievements");
		form2.AddField("uid", userId);
		form2.AddField("AchDetails", tempTotalAch);
		form2.AddField("KitDetails", tempTotalKit);
		WWW download2 = new WWW(CONTROLLER.PHP_Server_Link, form2);
		yield return download2;
		if (download2.error != null)
		{
			yield break;
		}
		JSONNode jSONNode = JSONNode.Parse(download2.text);
		if (string.Empty + jSONNode["UpdateAchievements"]["status"] == "1")
		{
			achDetails = tempTotalAch;
			kitDetails = tempTotalKit;
			toUpdateAchDetails = toUpdateAchResetValue;
			toUpdateKitDetails = toUpdateKitResetValue;
			ObscuredPrefs.SetString("achDetails", achDetails);
			ObscuredPrefs.SetString("toUpdateAchDetails", toUpdateAchDetails);
			ObscuredPrefs.SetString("kitDetails", kitDetails);
			ObscuredPrefs.SetString("toUpdateKitDetails", toUpdateKitDetails);
			if (!isRetrived)
			{
				UpdateAchievmentsInIndividualPrefs();
				UpdateKitsInIndividualPrefs();
				isRetrived = true;
			}
		}
	}

	private void UpdateAchievmentsInIndividualPrefs()
	{
		string[] array = achDetails.Split('|');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split('-');
			string text = AchievementTable.AchievementName[i + 1];
			string key = text + " Level";
			string key2 = text + " ClaimedAt";
			ObscuredPrefs.SetInt(key, int.Parse(array2[0]));
			ObscuredPrefs.SetInt(text, int.Parse(array2[1]));
			ObscuredPrefs.SetInt(key2, int.Parse(array2[2]));
		}
	}

	private void UpdateKitsInIndividualPrefs()
	{
		string[] array = kitDetails.Split('|');
		for (int i = 0; i < array.Length; i++)
		{
			string key = KitTable.KitName[i + 1];
			ObscuredPrefs.SetInt(key, int.Parse(array[i]));
		}
	}

	private void Retrive_ToUpdateDetails()
	{
		if (ObscuredPrefs.HasKey("toUpdateAchDetails"))
		{
			toUpdateAchDetails = ObscuredPrefs.GetString("toUpdateAchDetails");
		}
		else
		{
			toUpdateAchDetails = toUpdateAchResetValue;
			ObscuredPrefs.SetString("toUpdateAchDetails", toUpdateAchDetails);
		}
		if (ObscuredPrefs.HasKey("toUpdateKitDetails"))
		{
			toUpdateKitDetails = ObscuredPrefs.GetString("toUpdateKitDetails");
			return;
		}
		toUpdateKitDetails = toUpdateKitResetValue;
		ObscuredPrefs.SetString("toUpdateKitDetails", toUpdateKitDetails);
	}

	private string AddAchievments(string ach, string toUpdateAch)
	{
		string text = string.Empty;
		string[] array = ach.Split('|');
		string[] array2 = toUpdateAch.Split('|');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array3 = array[i].Split('-');
			string[] array4 = array2[i].Split('-');
			if (int.Parse(array4[0]) != 0)
			{
				string text2 = text;
				text = text2 + (int.Parse(array3[0]) + int.Parse(array4[0])) + "-" + array4[1] + "-" + array4[2] + "|";
			}
			else
			{
				string text2 = text;
				text = text2 + array3[0] + "-" + (int.Parse(array3[1]) + int.Parse(array4[1])) + "-" + array3[2] + "|";
			}
		}
		return text.Remove(text.Length - 1);
	}

	private string AddKitDetails(string kit, string toUpdateKit)
	{
		string text = string.Empty;
		string[] array = kit.Split('|');
		string[] array2 = toUpdateKit.Split('|');
		for (int i = 0; i < array.Length; i++)
		{
			string[] array3 = array2[i].Split('-');
			text = ((!(array3[1] == "0")) ? (text + array3[0] + "|") : (text + (int.Parse(array[i]) + int.Parse(array3[0])) + "|"));
		}
		return text.Remove(text.Length - 1);
	}

	public void OnSignOut()
	{
		achDetails = achResetValue;
		toUpdateAchDetails = toUpdateAchResetValue;
		kitDetails = kitResetValue;
		toUpdateKitDetails = toUpdateKitResetValue;
		ObscuredPrefs.SetString("achDetails", achDetails);
		ObscuredPrefs.SetString("kitDetails", kitDetails);
		ObscuredPrefs.SetString("toUpdateAchDetails", toUpdateAchDetails);
		ObscuredPrefs.SetString("toUpdateKitDetails", toUpdateKitDetails);
		UpdateAchievmentsInIndividualPrefs();
		UpdateKitsInIndividualPrefs();
		isRetrived = false;
	}
}
