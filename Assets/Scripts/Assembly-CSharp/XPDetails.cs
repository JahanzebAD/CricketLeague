using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class XPDetails : Singleton<XPDetails>
{
	public Text milestone;

	public Text coinAmount;

	public Text tokenAmount;

	public Button claimBtn;

	public GameObject innerPanel;

	public GameObject CoinClaim;

	public GameObject StarClaim;

	public GameObject TokenClaim;

	public void Claim()
	{
		Singleton<AchievementManager>.instance.scroll.enabled = false;
		Singleton<AchievementManager>.instance.KitScroll.enabled = false;
		innerPanel.SetActive(value: false);
		SavePlayerPrefs.SaveUserCoins(int.Parse(coinAmount.text), 0, int.Parse(coinAmount.text));
		Singleton<AchievementDetails>.instance.ResetCoinAnim(1);
		innerPanel.SetActive(value: false);
		claimBtn.image.sprite = Singleton<AchievementManager>.instance.disable;
		claimBtn.interactable = false;
		ObscuredPrefs.SetInt("MilestoneClaimedIndex", ObscuredPrefs.GetInt("XPMilestoneIndex"));
		Singleton<AchievementManager>.instance.ValidateXPMilestones();
	}
}
