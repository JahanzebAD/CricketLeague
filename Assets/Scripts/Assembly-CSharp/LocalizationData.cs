using System.Collections.Generic;
using System.IO;
using UnityEngine;
//using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class LocalizationData : MonoBehaviour
{
	public List<string> DATALIST = new List<string>();

	public List<string> refList = new List<string>();

	public Font[] PreloaderFonts = new Font[9];

	public string[] data;

	public Button[] buttons;

	public const byte BENGALI = 0;

	public const byte ENGLISH = 1;

	public const byte GUJARTHI = 2;

	public const byte HINDI = 3;

	public const byte KANNADA = 4;

	public const byte MALAYALAM = 5;

	public const byte MARATHI = 6;

	public const byte TAMIL = 7;

	public const byte TELUGU = 8;

	public static LocalizationData instance;

	[HideInInspector]
	public int languageIndex = 1;

	private string[] languageCode = new string[9] { "BE", "EN", "GU", "HI", "KA", "MAL", "MAR", "TA", "TE" };

	public Text titleText;

	public Text noteText;

	public Text OkText;

	public string[] tempTitleText = new string[9] { "Avcbvi fvlv wbe©vPb Ki“b!", "Select your language!", "tamaarI BaaYaa pasaMd krao!", "viuh Hkk\"kk dk p; u djsa!", "Select your language!", "\n§fpsS `mj XncsªSp¡pI!", "rqeph Hkk\"kk fuoMk", "cq;fs; nkhopiaj; Nju;e;njLf;fTk;!", "Select your language!" };

	public string[] tempNoteText = new string[9] { "<size=50>wet`«t:</size> Avcwb ‡mwUs \u00afŒxb ‡_‡K ‡h ‡Kv‡bv mgh\u09bc fvlv cwieZ©b Ki‡Z cv‡ib|", "<size=50>Note:</size> You can change language at any time from the setting screen.", "<size=50>naa^\u0ac5Xa:</size> tamao saoiTMga sk`InaqaI kao{paNa samayao BaaYaa badlaI Sakao Cao.", "<size=50>/;ku nsa:</size> vki lsfVax LØhu ls fdlh Hkh le; Hkk\"kk cny ldrs gSaA", "<size=50>Note:</size> You can change language at any time from the setting screen.", "<size=50>Ipdn\u00b8v:</size> \n§Ä¡v GXv kab\u00afpw {IaoIcWw kv{Io\nð \nóv `mj amämmIpw.", "<size=50>Vhi%</size> vki.k lsfVax LØhue/kwu dks.kR;kgh osGh Hkk\"kk cnyw 'kdrk-", "<size=50>Fwpg;G:</size>mikg;igj; Nju;e;njLg;gjpypUe;J ve;j Neuj;jpYk; ePq;fs; nkhopia khw;wyhk;.", "<size=50>Note:</size> You can change language at any time from the setting screen." };

	public string[] OKTextArray = new string[9] { "wVK Av‡Q", "OK", "Aaoko", "Bhd gS", "\u00b8Àj", "icn", "Bhd vkgs", "rup", "dŸ¹s›" };

	public void Awake()
	{
		instance = this;
		Object.DontDestroyOnLoad(this);
		if (PlayerPrefs.HasKey("LastUsedLanguage"))
		{
			languageIndex = PlayerPrefs.GetInt("LastUsedLanguage", 1);
		}
	}

	public string getLanguageCode()
	{
		return languageCode[languageIndex];
	}

	public void Start()
	{
		if (PlayerPrefs.HasKey("LastUsedLanguage"))
		{
			loadLocalizationData(PlayerPrefs.GetInt("LastUsedLanguage", 1));
		}
		else
		{
			loadLocalizationData(1);
		}
		if (titleText != null && tempTitleText != null)
		{
			titleText.text = tempTitleText[languageIndex];
		}
		if (noteText != null && tempNoteText != null)
		{
			noteText.text = tempNoteText[languageIndex];
		}
	}

	public List<string> ReadMyFile(string fileName)
	{
		List<string> list = new List<string>();
		string empty = string.Empty;
		Debug.Log("got");
		TextAsset textAsset = Resources.Load<TextAsset>(fileName);
		Debug.Log(fileName);
		Debug.Log(textAsset.text);
		if (textAsset != null)
		{
			Debug.Log("got level ");
			using StreamReader streamReader = new StreamReader(new MemoryStream(textAsset.bytes));
			Debug.Log("got sr");
			string item;
			while ((item = streamReader.ReadLine()) != null)
			{
				list.Add(item);
			}
			return list;
		}
		return list;
	}

	private void loadLocalizationData(int language)
	{
		setSelectedLanguage(language);
		PlayerPrefs.SetInt("LastUsedLanguage", language);
		if (data != null)
		{
			data = null;
			Resources.UnloadUnusedAssets();
		}
		string text = "Localization_";
		string fileName = "Reference";
		text += getLanguageCode();
		//Debug.LogError(text);
		if (DATALIST.Count > 0)
		{
			DATALIST.Clear();
		}
		DATALIST = ReadMyFile(text);
		if (refList.Count > 0)
		{
			refList.Clear();
		}
		refList = ReadMyFile(fileName);
		refList = refList.ConvertAll((string d) => d.ToUpper());
		if (DATALIST != null)
		{
			Debug.Log("assigned languageIndex:" + language);
			return;
		}
		Debug.LogError(2);
		Debug.Log("file not found:" + text);
	}

	public void setSelectedLanguage(int language)
	{
		languageIndex = language;
		if (titleText != null && tempTitleText != null && OkText != null)
		{
			titleText.text = tempTitleText[languageIndex];
			noteText.text = tempNoteText[languageIndex];
			OkText.text = OKTextArray[languageIndex];
			Text text = titleText;
			Font font = PreloaderFonts[languageIndex];
			OkText.font = font;
			font = font;
			noteText.font = font;
			text.font = font;
		}
	}

	public void loadTheSelectedLanguageFromResources()
	{
		loadLocalizationData(languageIndex);
	}

	public string getText(int index)
	{
		string result = string.Empty;
		if (DATALIST != null)
		{
			result = DATALIST[index];
		}
		return result;
	}

	public string removeTheLastWord(string name)
	{
		string[] array = name.Split(" "[0]);
		if (array.Length > 1)
		{
			int num = array[array.Length - 1].Length + 1;
			string result = name.Remove(name.Length - num, num);
			array = null;
			return result;
		}
		return array[0];
	}

	public string removeAllTags(string temp)
	{
		string text = temp;
		while (temp.Contains("<") && temp.Contains(">"))
		{
			int num = temp.IndexOf("<");
			int num2 = temp.IndexOf(">");
			temp = temp.Remove(num, num2 - num + 1);
		}
		return temp;
	}
}
