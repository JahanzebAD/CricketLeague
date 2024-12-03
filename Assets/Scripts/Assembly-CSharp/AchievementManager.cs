using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : Singleton<AchievementManager>
{
	public ScrollRect scroll;

	public ScrollRect KitScroll;

	private string value;

	private int coinsAmount;

	private int tokenAmount;

	private int diff;

	private int totalProgress;

	private int levelNo;

	private string coinValue;

	private string tokenValue;

	private string claimedAt;

	private string levelValue;

	public Text userXP;

	public Text userCoins;

	public Text userTokens;

	private int progressValue;

	private int kitProgress;

	public GameObject holder;

	private int[] xpAmount = new int[34]
	{
		1000, 2500, 5000, 7500, 10000, 12500, 25000, 50000, 75000, 100000,
		125000, 150000, 175000, 200000, 250000, 300000, 350000, 400000, 450000, 500000,
		550000, 600000, 650000, 700000, 750000, 800000, 850000, 900000, 950000, 1000000,
		1500000, 2000000, 2500000, 5000000
	};

	public Transform[] kitButtons;

	public Transform XpButton;

	public Sprite enable;

	public Sprite disable;

	public AchievementDetails[] achievements;

	public KitDetails[] kits;

	public XPDetails xp;

	private void Start()
	{
		totalProgress = 25;
		holder.SetActive(value: false);
		KitTable.InitKitValues();
		SetXPMilestoneIndex();
	}

	public void SetXPMilestoneIndex()
	{
		if (CONTROLLER.XPs < xpAmount[0])
		{
			ObscuredPrefs.SetInt("XPMilestoneIndex", 0);
			return;
		}
		for (int num = xpAmount.Length - 1; num >= 0; num--)
		{
			if (CONTROLLER.XPs > xpAmount[num])
			{
				if (num == xpAmount.Length - 1)
				{
					ObscuredPrefs.SetInt("XPMilestoneIndex", num);
				}
				else
				{
					ObscuredPrefs.SetInt("XPMilestoneIndex", num + 1);
				}
				break;
			}
		}
	}

	public void ValidateAchievements()
	{
		for (int i = 0; i < achievements.Length; i++)
		{
			value = AchievementTable.AchievementName[i + 1];
			coinValue = value + " Coins";
			claimedAt = value + " ClaimedAt";
			levelValue = value + " Level";
			if (ObscuredPrefs.HasKey(levelValue))
			{
				levelNo = ObscuredPrefs.GetInt(levelValue);
			}
			else
			{
				levelNo = 1;
			}
			totalProgress = 25 * PowerOf(2, levelNo - 1);
			if (ObscuredPrefs.HasKey(value))
			{
				progressValue = ObscuredPrefs.GetInt(value);
			}
			else
			{
				progressValue = 0;
			}
			if (progressValue >= totalProgress)
			{
				progressValue = totalProgress;
				ObscuredPrefs.SetInt(value, progressValue);
			}
			if (ObscuredPrefs.HasKey(claimedAt))
			{
				diff = progressValue - ObscuredPrefs.GetInt(claimedAt);
			}
			else
			{
				diff = progressValue;
			}
			int num = 0;
			if (!ObscuredPrefs.HasKey(coinValue) || ObscuredPrefs.GetInt(coinValue) == 0)
			{
				num = Random.Range(40, 61) * diff;
				ObscuredPrefs.SetInt(coinValue, num);
			}
			else
			{
				num = ObscuredPrefs.GetInt(coinValue);
			}
			if (diff > 0)
			{
				achievements[i].claimBtn.image.sprite = enable;
				achievements[i].claimBtn.interactable = true;
			}
			else
			{
				achievements[i].claimBtn.image.sprite = disable;
				achievements[i].claimBtn.interactable = false;
			}
			achievements[i].levelNo.text = "0" + levelNo;
			achievements[i].achievementIndex = i + 1;
			achievements[i].progress.text = progressValue.ToString();
			achievements[i].progressCap.text = totalProgress.ToString();
			achievements[i].stars.fillAmount = (float)progressValue / (float)totalProgress;
			achievements[i].title.text = value;
			if (LocalizationData.instance.refList.Contains(achievements[i].title.text.ToUpper()))
			{
				int index = LocalizationData.instance.refList.IndexOf(achievements[i].title.text.ToUpper());
				achievements[i].title.text = LocalizationData.instance.getText(index);
			}
			achievements[i].content.text = AchievementTable.AchievementGoal[i + 1];
			if (LocalizationData.instance.refList.Contains(achievements[i].content.text.ToUpper()))
			{
				int index2 = LocalizationData.instance.refList.IndexOf(achievements[i].content.text.ToUpper());
				achievements[i].content.text = LocalizationData.instance.getText(index2);
			}
			achievements[i].coinsAmount.text = num.ToString();
		}
	}

	private int PowerOf(int x, int pow)
	{
		int num = 1;
		while (pow != 0)
		{
			if ((pow & 1) == 1)
			{
				num *= x;
			}
			x *= x;
			pow >>= 1;
		}
		return num;
	}

	public void ResetDetails()
	{
		userXP.text = ObscuredPrefs.GetInt(CONTROLLER.XPKey).ToString();
		userCoins.text = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey).ToString();
	}

	public void ValidateKits()
	{
		for (int i = 0; i < kits.Length; i++)
		{
			value = KitTable.KitName[i + 1];
			if (ObscuredPrefs.HasKey(value))
			{
				kitProgress = ObscuredPrefs.GetInt(value);
			}
			else
			{
				kitProgress = 0;
			}
			if (kitProgress < 50)
			{
				kits[i].innerPanel.SetActive(value: false);
				kits[i].claimBtn.image.sprite = disable;
				kits[i].claimBtn.interactable = false;
			}
			else
			{
				kits[i].innerPanel.SetActive(value: true);
				kits[i].claimBtn.image.sprite = enable;
				kits[i].claimBtn.interactable = true;
				kits[i].CoinAmount.text = Random.Range(1000, 3001).ToString();
			}
			kits[i].kitIndex = i + 1;
			kits[i].progress.text = kitProgress.ToString();
			kits[i].fill.fillAmount = (float)kitProgress / 50f;
			kits[i].title.text = value;
			if (LocalizationData.instance.refList.Contains(kits[i].title.text.ToUpper()))
			{
				int index = LocalizationData.instance.refList.IndexOf(kits[i].title.text.ToUpper());
				kits[i].title.text = LocalizationData.instance.getText(index);
			}
		}
	}

	public void ValidateXPMilestones()
	{
		xp.claimBtn.image.sprite = disable;
		xp.claimBtn.interactable = false;
		xp.innerPanel.SetActive(value: false);
		SetXPMilestoneIndex();
		int @int = ObscuredPrefs.GetInt("XPMilestoneIndex");
		int num = ((!ObscuredPrefs.HasKey("MilestoneClaimedIndex")) ? (-1) : ObscuredPrefs.GetInt("MilestoneClaimedIndex"));
		xp.milestone.text = xpAmount[@int].ToString();
		if (@int >= 1 && CONTROLLER.XPs > xpAmount[@int - 1] && @int > num)
		{
			xp.innerPanel.SetActive(value: true);
			xp.claimBtn.image.sprite = enable;
			xp.claimBtn.interactable = true;
		}
		if (@int > 0)
		{
			xp.coinAmount.text = (xpAmount[@int - 1] / 10).ToString();
		}
		xp.tokenAmount.text = (xpAmount[@int] / 500).ToString();
	}

	public void ResetKit(int index)
	{
		value = KitTable.KitName[index];
		ObscuredPrefs.SetInt(value, 0);
		ValidateKits();
		Singleton<AchievementsSyncronizer>.instance.ResetKit(index);
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = Close;
		CONTROLLER.pageName = "achievements";
		ValidateAchievements();
		ValidateKits();
		SetXPMilestoneIndex();
		ValidateXPMilestones();
		ResetDetails();
		holder.SetActive(value: true);
	}

	public void Close()
	{
		Singleton<GameModeTWO>.instance.ResetDetails();
		holder.SetActive(value: false);
		Singleton<GameModeTWO>.instance.showMe();
		CONTROLLER.pageName = "landingPage";
	}
}
