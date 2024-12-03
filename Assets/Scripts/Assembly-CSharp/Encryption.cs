using System.Collections;
using UnityEngine;

public class Encryption
{
	public static ArrayList AlphaArray1 = new ArrayList();

	public static ArrayList AlphaArray2 = new ArrayList();

	public static ArrayList AlphaArray3 = new ArrayList();

	public static ArrayList AlphaArray4 = new ArrayList();

	public static ArrayList AlphaArray5 = new ArrayList();

	public static ArrayList AlphaArray6 = new ArrayList();

	public static ArrayList AlphaArray7 = new ArrayList();

	public static ArrayList AlphaArray8 = new ArrayList();

	public static ArrayList AlphaArray9 = new ArrayList();

	public static ArrayList AlphaArray10 = new ArrayList();

	public static string SpecialCharacterString = "#$()*+,-.:;<>@";

	public static string SpecialNumericString = "0123456789";

	public static void Initialize()
	{
		AlphaArray1.Add("A");
		AlphaArray1.Add("s");
		AlphaArray1.Add("n");
		AlphaArray1.Add("L");
		AlphaArray1.Add("b");
		AlphaArray1.Add("E");
		AlphaArray1.Add("W");
		AlphaArray1.Add("u");
		AlphaArray1.Add("j");
		AlphaArray1.Add("o");
		AlphaArray1.Add("V");
		AlphaArray2.Add("k");
		AlphaArray2.Add("E");
		AlphaArray2.Add("y");
		AlphaArray2.Add("S");
		AlphaArray2.Add("D");
		AlphaArray2.Add("q");
		AlphaArray2.Add("L");
		AlphaArray2.Add("b");
		AlphaArray2.Add("i");
		AlphaArray2.Add("R");
		AlphaArray2.Add("Z");
		AlphaArray3.Add("n");
		AlphaArray3.Add("G");
		AlphaArray3.Add("J");
		AlphaArray3.Add("A");
		AlphaArray3.Add("m");
		AlphaArray3.Add("C");
		AlphaArray3.Add("h");
		AlphaArray3.Add("T");
		AlphaArray3.Add("s");
		AlphaArray3.Add("Q");
		AlphaArray3.Add("p");
		AlphaArray4.Add("T");
		AlphaArray4.Add("Y");
		AlphaArray4.Add("o");
		AlphaArray4.Add("l");
		AlphaArray4.Add("s");
		AlphaArray4.Add("W");
		AlphaArray4.Add("B");
		AlphaArray4.Add("i");
		AlphaArray4.Add("J");
		AlphaArray4.Add("X");
		AlphaArray4.Add("N");
		AlphaArray5.Add("d");
		AlphaArray5.Add("I");
		AlphaArray5.Add("n");
		AlphaArray5.Add("e");
		AlphaArray5.Add("S");
		AlphaArray5.Add("h");
		AlphaArray5.Add("K");
		AlphaArray5.Add("U");
		AlphaArray5.Add("M");
		AlphaArray5.Add("a");
		AlphaArray5.Add("R");
		AlphaArray6.Add("P");
		AlphaArray6.Add("a");
		AlphaArray6.Add("R");
		AlphaArray6.Add("m");
		AlphaArray6.Add("S");
		AlphaArray6.Add("i");
		AlphaArray6.Add("V");
		AlphaArray6.Add("C");
		AlphaArray6.Add("j");
		AlphaArray6.Add("O");
		AlphaArray6.Add("q");
		AlphaArray7.Add("n");
		AlphaArray7.Add("E");
		AlphaArray7.Add("x");
		AlphaArray7.Add("t");
		AlphaArray7.Add("W");
		AlphaArray7.Add("a");
		AlphaArray7.Add("V");
		AlphaArray7.Add("m");
		AlphaArray7.Add("U");
		AlphaArray7.Add("l");
		AlphaArray7.Add("i");
		AlphaArray8.Add("M");
		AlphaArray8.Add("G");
		AlphaArray8.Add("A");
		AlphaArray8.Add("u");
		AlphaArray8.Add("n");
		AlphaArray8.Add("J");
		AlphaArray8.Add("C");
		AlphaArray8.Add("p");
		AlphaArray8.Add("i");
		AlphaArray8.Add("L");
		AlphaArray8.Add("t");
		AlphaArray9.Add("x");
		AlphaArray9.Add("B");
		AlphaArray9.Add("I");
		AlphaArray9.Add("e");
		AlphaArray9.Add("R");
		AlphaArray9.Add("g");
		AlphaArray9.Add("K");
		AlphaArray9.Add("n");
		AlphaArray9.Add("a");
		AlphaArray9.Add("H");
		AlphaArray9.Add("s");
		AlphaArray10.Add("z");
		AlphaArray10.Add("v");
		AlphaArray10.Add("D");
		AlphaArray10.Add("o");
		AlphaArray10.Add("S");
		AlphaArray10.Add("i");
		AlphaArray10.Add("L");
		AlphaArray10.Add("U");
		AlphaArray10.Add("b");
		AlphaArray10.Add("M");
		AlphaArray10.Add("a");
	}

	public static string SecuritySystem(int pointsEarned, int randArrayNumber)
	{
		string text = string.Empty + pointsEarned;
		string text2 = string.Empty;
		ArrayList arrayList = new ArrayList();
		switch (randArrayNumber)
		{
		case 1:
			arrayList = AlphaArray1;
			break;
		case 2:
			arrayList = AlphaArray2;
			break;
		case 3:
			arrayList = AlphaArray3;
			break;
		case 4:
			arrayList = AlphaArray4;
			break;
		case 5:
			arrayList = AlphaArray5;
			break;
		case 6:
			arrayList = AlphaArray6;
			break;
		case 7:
			arrayList = AlphaArray7;
			break;
		case 8:
			arrayList = AlphaArray8;
			break;
		case 9:
			arrayList = AlphaArray9;
			break;
		case 10:
			arrayList = AlphaArray10;
			break;
		}
		for (int i = 0; i < text.Length; i++)
		{
			int index = 10;
			string text3 = string.Empty + text[i];
			string text4 = string.Empty + SpecialCharacterString[Random.Range(0, SpecialCharacterString.Length)];
			string text5 = string.Empty + SpecialNumericString[Random.Range(0, SpecialNumericString.Length)];
			text4 += text5;
			if (text3 != "-")
			{
				index = int.Parse(text3);
			}
			text2 = text2 + text4 + arrayList[index];
		}
		return text2;
	}

	public static string encryptPointsSystem(long pointsEarned, int keyValue)
	{
		string text = string.Empty + pointsEarned;
		string text2 = string.Empty;
		ArrayList arrayList = new ArrayList();
		switch (keyValue)
		{
		case 1:
			arrayList = AlphaArray1;
			break;
		case 2:
			arrayList = AlphaArray2;
			break;
		case 3:
			arrayList = AlphaArray3;
			break;
		case 4:
			arrayList = AlphaArray4;
			break;
		case 5:
			arrayList = AlphaArray5;
			break;
		case 6:
			arrayList = AlphaArray6;
			break;
		case 7:
			arrayList = AlphaArray7;
			break;
		case 8:
			arrayList = AlphaArray8;
			break;
		case 9:
			arrayList = AlphaArray9;
			break;
		case 10:
			arrayList = AlphaArray10;
			break;
		}
		for (int i = 0; i < text.Length; i++)
		{
			int index = 10;
			string text3 = string.Empty + text[i];
			string text4 = string.Empty + SpecialCharacterString[Random.Range(0, SpecialCharacterString.Length)];
			string text5 = string.Empty + SpecialNumericString[Random.Range(0, SpecialNumericString.Length)];
			text4 += text5;
			if (text3 != "-")
			{
				index = int.Parse(text3);
			}
			text2 = text2 + text4 + arrayList[index];
		}
		return text2;
	}

	public static string DecryptPointsSystem(string encryptedPoint, int keyValue)
	{
		ArrayList arrayList = new ArrayList();
		switch (keyValue)
		{
		case 1:
			arrayList = AlphaArray1;
			break;
		case 2:
			arrayList = AlphaArray2;
			break;
		case 3:
			arrayList = AlphaArray3;
			break;
		case 4:
			arrayList = AlphaArray4;
			break;
		case 5:
			arrayList = AlphaArray5;
			break;
		case 6:
			arrayList = AlphaArray6;
			break;
		case 7:
			arrayList = AlphaArray7;
			break;
		case 8:
			arrayList = AlphaArray8;
			break;
		case 9:
			arrayList = AlphaArray9;
			break;
		case 10:
			arrayList = AlphaArray10;
			break;
		}
		int num = encryptedPoint.Length / 2 - 1;
		if (num < 1)
		{
			num = 1;
		}
		int num2 = 0;
		int num3 = 2;
		int num4 = 0;
		string text = string.Empty;
		string text2 = string.Empty;
		for (int i = 0; i < num; i++)
		{
			if (num2 + num3 < encryptedPoint.Length)
			{
				encryptedPoint = encryptedPoint.Remove(num2, num3);
				text += encryptedPoint[0];
				encryptedPoint = encryptedPoint.Remove(0, 1);
				if (arrayList.IndexOf(string.Empty + text[i]) == 10)
				{
					text2 += "-";
					continue;
				}
				num4 = arrayList.IndexOf(string.Empty + text[i]);
				text2 += num4;
			}
		}
		return text2;
	}
}
