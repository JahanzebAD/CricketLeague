using UnityEngine;

public class FlagHolder : Singleton<FlagHolder>
{
	public Sprite[] flags;

	public Sprite searchFlagByName(string state)
	{
		Sprite result = null;
		Sprite[] array = flags;
		foreach (Sprite sprite in array)
		{
			if (sprite.name.ToUpper() == state)
			{
				result = sprite;
				break;
			}
		}
		return result;
	}
}
