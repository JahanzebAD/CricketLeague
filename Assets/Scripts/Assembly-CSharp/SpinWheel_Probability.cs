using System.Collections;
using LitJson;
using UnityEngine;

public class SpinWheel_Probability : Singleton<SpinWheel_Probability>
{
	public static int prizeItemNumber;

	private JsonData itemData;

	public float[] jsonItemPercent = new float[14];

	public float randomGenerator;

	public float tempCalc;

	private string itemFilePath = "http://assets.cricketbuddies.in/wcc-lite/prize_json.json";

	private string jsonString;

	private string playerPrefs_ItemNumber;

	public bool isNetworkConnected;

	public bool fileDownloadCompleted;

	private void Start()
	{
		//StartCoroutine("CheckInternet");
	}

	//public IEnumerator CheckInternet()
	//{
	//	yield return new WWW("http://clients3.google.com/generate_204");
	//	isNetworkConnected = Singleton<AdIntegrate>.instance.CheckForInternet();
	//	if (isNetworkConnected)
	//	{
	//		RetriveFile();
	//	}
	//}

	public void RetriveFile()
	{
		StartCoroutine("JsonConnect");
	}

	private IEnumerator JsonConnect()
	{
		if (itemFilePath.Contains("://"))
		{
			WWW www = new WWW(itemFilePath);
			yield return www;
			if (string.IsNullOrEmpty(www.error) && !string.IsNullOrEmpty(www.text))
			{
				fileDownloadCompleted = true;
				jsonString = www.text;
				itemData = JsonMapper.ToObject(jsonString);
			}
			else
			{
				fileDownloadCompleted = false;
			}
		}
	}

	public void RetriveItemValue()
	{
		if (fileDownloadCompleted)
		{
			for (int i = 0; i <= jsonItemPercent.Length - 1; i++)
			{
				jsonItemPercent[i] = float.Parse(itemData["data"][i]["Item_Precentage"].ToString());
			}
			CalculateProbality();
		}
		else
		{
			CalculateProbality();
		}
	}

	public void CalculateProbality()
	{
		randomGenerator = Random.Range(0f, 100.01f);
		if (randomGenerator >= 0f && randomGenerator <= SumItemProbability(0))
		{
			prizeItemNumber = 0;
			return;
		}
		for (int i = 0; i <= 12; i++)
		{
			if (randomGenerator >= SumItemProbability(i) + 0.01f && randomGenerator <= SumItemProbability(i + 1))
			{
				prizeItemNumber = i + 1;
				break;
			}
		}
	}

	public float SumItemProbability(int n)
	{
		tempCalc = 0f;
		for (int i = 0; i <= n; i++)
		{
			tempCalc += jsonItemPercent[i];
		}
		return tempCalc;
	}
}
