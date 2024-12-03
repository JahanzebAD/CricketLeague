using System;
using UnityEngine;

public class ImageSavers
{
	public static void SaveTexture(Texture2D _tex, string saveAs)
	{
		byte[] inArray = _tex.EncodeToPNG();
		string value = Convert.ToBase64String(inArray);
		PlayerPrefs.SetString(saveAs, value);
		PlayerPrefs.SetInt(saveAs + "_w", _tex.width);
		PlayerPrefs.SetInt(saveAs + "_h", _tex.height);
	}

	public static Texture2D RetriveTexture(string savedImageName)
	{
		string @string = PlayerPrefs.GetString(savedImageName);
		int @int = PlayerPrefs.GetInt(savedImageName + "_w");
		int int2 = PlayerPrefs.GetInt(savedImageName + "_h");
		byte[] data = Convert.FromBase64String(@string);
		Texture2D texture2D = new Texture2D(@int, int2, TextureFormat.ARGB32, mipChain: false);
		texture2D.LoadImage(data);
		return texture2D;
	}
}
