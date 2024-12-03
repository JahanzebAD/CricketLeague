using UnityEngine;
using UnityEngine.UI;

public class KitDetails : Singleton<KitDetails>
{
	public Text progress;

	public Text title;

	public Text CoinAmount;

	public Image fill;

	public Button claimBtn;

	public GameObject innerPanel;

	public int temp;

	public GameObject CoinClaim;

	public GameObject StarClaim;

	public GameObject TokenClaim;

	public int kitIndex;

	public void Claim()
	{
		Singleton<AchievementManager>.instance.scroll.enabled = false;
		Singleton<AchievementManager>.instance.KitScroll.enabled = false;
		temp = int.Parse(CoinAmount.text);
		if (base.gameObject.name == "Kit1 (1)")
		{
			Singleton<GameModeTWO>.instance.kitValue = temp;
		}
		else if (base.gameObject.name == "Kit1 (2)")
		{
			Singleton<GameModeTWO>.instance.kitValue = temp;
		}
		else if (base.gameObject.name == "Kit1 (3)")
		{
			Singleton<GameModeTWO>.instance.kitValue = temp;
		}
		else if (base.gameObject.name == "Kit1 (4)")
		{
			Singleton<GameModeTWO>.instance.kitValue = temp;
		}
		else if (base.gameObject.name == "Kit1 (5)")
		{
			Singleton<GameModeTWO>.instance.kitValue = temp;
		}
		SavePlayerPrefs.SaveUserCoins(temp, 0, temp);
		innerPanel.SetActive(value: false);
		Singleton<AchievementDetails>.instance.ResetCoinAnim(kitIndex + 1);
		Singleton<AchievementManager>.instance.ResetKit(kitIndex);
	}
}
