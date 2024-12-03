using System;
using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDetails : Singleton<AchievementDetails>
{
	public Text title;

	public Text content;

	public Text coinsAmount;

	public Text tokenAmount;

	public Text progress;

	public Text levelNo;

	public Text progressCap;

	public Text RewardCoinsText;

	public Image ribbon;

	public Image stars;

	public Button claimBtn;

	public Vector3 Buttonpos;

	public GameObject coins;

	public GameObject tokens;

	public GameObject CoinClaim;

	public GameObject StarClaim;

	public GameObject TokenClaim;

	private bool canCall;

	private bool calledOnce = true;

	public GameObject[] coinDupe;

	public GameObject[] tokenDupe;

	public GameObject[] StarDupe;

	public Transform coinTarget;

	public Transform tokenTarget;

	public Transform parent;

	private Tweener tween1;

	private Tweener tween2;

	public int achievementIndex;

	public Sequence seq;

	public void Start()
	{
		coinDupe = new GameObject[5];
		tokenDupe = new GameObject[5];
		StarDupe = new GameObject[15];
	}

	public void Claim()
	{
		Singleton<AchievementManager>.instance.scroll.enabled = false;
		Singleton<AchievementManager>.instance.KitScroll.enabled = false;
		SavePlayerPrefs.SaveUserCoins(int.Parse(coinsAmount.text), 0, int.Parse(coinsAmount.text));
		RewardCoinsText.text = coinsAmount.text + " " + LocalizationData.instance.getText(74);
		string text = AchievementTable.AchievementName[achievementIndex];
		if (ObscuredPrefs.HasKey(text + " ReachedMax"))
		{
			AchievementTable.GoNextLevel(achievementIndex);
		}
		else
		{
			AchievementTable.ResetAmount(achievementIndex);
		}
		canCall = true;
		calledOnce = false;
		Singleton<AchievementManager>.instance.ValidateAchievements();
		Singleton<AchievementManager>.instance.ResetDetails();
		ResetCoinAnim(0);
		Singleton<AchievementManager>.instance.ResetDetails();
		
			StartCoroutine(Singleton<AchievementsSyncronizer>.instance.SendAchievementsAndKits());
		
	}

	public void ResetCoinAnim(int index)
	{
		RewardCoinsText.gameObject.SetActive(value: true);
		for (int i = 0; i < 5; i++)
		{
		}
		if (index == 0)
		{
			CoinClaim.SetActive(value: true);
			TokenClaim.SetActive(value: true);
			StarClaim.SetActive(value: true);
			for (int j = 0; j < 5; j++)
			{
				coinDupe[j] = UnityEngine.Object.Instantiate(CoinClaim, CoinClaim.transform.position, CoinClaim.transform.rotation, parent);
				coinDupe[j].transform.localScale = Vector3.zero;
			}
			for (int k = 0; k < 5; k++)
			{
				tokenDupe[k] = UnityEngine.Object.Instantiate(TokenClaim, TokenClaim.transform.position, TokenClaim.transform.rotation, parent);
				tokenDupe[k].transform.localScale = Vector3.zero;
			}
			for (int l = 0; l < 15; l++)
			{
				StarDupe[l] = UnityEngine.Object.Instantiate(StarClaim, StarClaim.transform.position, StarClaim.transform.rotation, parent);
				StarDupe[l].transform.localScale = Vector3.zero;
			}
			CoinClaim.SetActive(value: false);
			TokenClaim.SetActive(value: false);
			StarClaim.SetActive(value: false);
			Buttonpos = claimBtn.transform.position;
			RewardCoinsText.transform.localScale = Vector3.zero;
			RewardCoinsText.transform.position = Buttonpos;
			CoinAnim();
		}
		if (index == 1)
		{
			Singleton<XPDetails>.instance.CoinClaim.SetActive(value: true);
			Singleton<XPDetails>.instance.TokenClaim.SetActive(value: true);
			Singleton<XPDetails>.instance.StarClaim.SetActive(value: true);
			for (int m = 0; m < 5; m++)
			{
				coinDupe[m] = UnityEngine.Object.Instantiate(Singleton<XPDetails>.instance.CoinClaim, Singleton<AchievementManager>.instance.XpButton.transform.position, Singleton<AchievementManager>.instance.XpButton.transform.rotation, parent);
				coinDupe[m].transform.localScale = Vector3.zero;
			}
			for (int n = 0; n < 5; n++)
			{
				tokenDupe[n] = UnityEngine.Object.Instantiate(Singleton<XPDetails>.instance.TokenClaim, Singleton<AchievementManager>.instance.XpButton.transform.position, Singleton<AchievementManager>.instance.XpButton.transform.rotation, parent);
				tokenDupe[n].transform.localScale = Vector3.zero;
			}
			for (int num = 0; num < 15; num++)
			{
				StarDupe[num] = UnityEngine.Object.Instantiate(Singleton<XPDetails>.instance.StarClaim, Singleton<AchievementManager>.instance.XpButton.transform.position, Singleton<AchievementManager>.instance.XpButton.transform.rotation, parent);
				StarDupe[num].transform.localScale = Vector3.zero;
			}
			Singleton<XPDetails>.instance.CoinClaim.SetActive(value: false);
			Singleton<XPDetails>.instance.TokenClaim.SetActive(value: false);
			Singleton<XPDetails>.instance.StarClaim.SetActive(value: false);
			Buttonpos = Singleton<AchievementManager>.instance.XpButton.transform.position;
			RewardCoinsText.text = Singleton<KitDetails>.instance.temp.ToString();
			RewardCoinsText.transform.localScale = Vector3.zero;
			RewardCoinsText.transform.position = Buttonpos;
			CoinAnim();
		}
		if (index >= 2)
		{
			Singleton<KitDetails>.instance.CoinClaim.SetActive(value: true);
			Singleton<KitDetails>.instance.TokenClaim.SetActive(value: true);
			Singleton<KitDetails>.instance.StarClaim.SetActive(value: true);
			for (int num2 = 0; num2 < 5; num2++)
			{
				coinDupe[num2] = UnityEngine.Object.Instantiate(Singleton<KitDetails>.instance.CoinClaim, Singleton<AchievementManager>.instance.kitButtons[index - 2].transform.position, Singleton<AchievementManager>.instance.kitButtons[index - 2].transform.rotation, parent);
				coinDupe[num2].transform.localScale = Vector3.zero;
			}
			for (int num3 = 0; num3 < 5; num3++)
			{
				tokenDupe[num3] = UnityEngine.Object.Instantiate(Singleton<KitDetails>.instance.TokenClaim, Singleton<AchievementManager>.instance.kitButtons[index - 2].transform.position, Singleton<AchievementManager>.instance.kitButtons[index - 2].transform.rotation, parent);
				tokenDupe[num3].transform.localScale = Vector3.zero;
			}
			for (int num4 = 0; num4 < 15; num4++)
			{
				StarDupe[num4] = UnityEngine.Object.Instantiate(Singleton<KitDetails>.instance.StarClaim, Singleton<AchievementManager>.instance.kitButtons[index - 2].transform.position, Singleton<AchievementManager>.instance.kitButtons[index - 2].transform.rotation, parent);
				StarDupe[num4].transform.localScale = Vector3.zero;
			}
			Singleton<KitDetails>.instance.CoinClaim.SetActive(value: false);
			Singleton<KitDetails>.instance.TokenClaim.SetActive(value: false);
			Singleton<KitDetails>.instance.StarClaim.SetActive(value: false);
			Buttonpos = Singleton<AchievementManager>.instance.kitButtons[index - 2].transform.position;
			RewardCoinsText.text = Singleton<GameModeTWO>.instance.kitValue.ToString();
			RewardCoinsText.transform.localScale = Vector3.zero;
			RewardCoinsText.transform.position = Buttonpos;
			CoinAnim();
		}
		for (int num5 = 0; num5 < 5; num5++)
		{
		}
	}

	public void LerpingFunction(Vector3 IndexVector, GameObject IndexGameObject, string indexname)
	{
		switch (indexname)
		{
		case "text":
			seq.Insert(0f, IndexGameObject.transform.DOMove(IndexVector, 0.4f));
			break;
		case "gameobject":
			seq.Insert(0f, IndexGameObject.transform.DOMove(IndexVector, 0.4f));
			seq.Insert(0f, IndexGameObject.transform.DORotate(new Vector3(0f, 0f, UnityEngine.Random.Range(0, 360)), 0.4f));
			seq.Insert(0f, IndexGameObject.transform.DOScale(Vector3.one, 0.4f));
			break;
		case "star":
			seq.Insert(0.1f, IndexGameObject.transform.DOMove(IndexVector, 1.3f));
			seq.Insert(0f, IndexGameObject.transform.DORotate(new Vector3(0f, 0f, UnityEngine.Random.Range(0, 360)), 1.6f));
			seq.Insert(0f, IndexGameObject.transform.DOScale(Vector3.one, 0.8f));
			break;
		case "gameobject1":
			seq.Insert(0.4f, IndexGameObject.transform.DOMove(IndexVector, 1.2f));
			break;
		}
	}

	public void CoinAnim()
	{
		seq = DOTween.Sequence();
		seq.Append(RewardCoinsText.transform.DOScale(Vector3.one, 0.4f));
		seq.Insert(0f, RewardCoinsText.DOFade(1f, 0.4f));
		LerpingFunction(new Vector3(Buttonpos.x, Buttonpos.y + 30f, Buttonpos.z), RewardCoinsText.gameObject, "text");
		LerpingFunction(new Vector3(Buttonpos.x - 2.5f, Buttonpos.y + 20f, Buttonpos.z), coinDupe[0].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x + 4f, Buttonpos.y + 20f, Buttonpos.z), tokenDupe[0].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x + 10f, Buttonpos.y + 16f, Buttonpos.z), coinDupe[1].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x - 12.5f, Buttonpos.y + 16f, Buttonpos.z), tokenDupe[1].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x - 2f, Buttonpos.y + 14f, Buttonpos.z), coinDupe[2].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x + 3f, Buttonpos.y + 14f, Buttonpos.z), tokenDupe[2].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x - 6f, Buttonpos.y + 10f, Buttonpos.z), coinDupe[3].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x + 11f, Buttonpos.y + 7f, Buttonpos.z), tokenDupe[3].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x - 0.5f, Buttonpos.y + 8.5f, Buttonpos.z), coinDupe[4].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x + 4f, Buttonpos.y + 8f, Buttonpos.z), tokenDupe[4].gameObject, "gameobject");
		for (int i = 0; i < 15; i++)
		{
			LerpingFunction(new Vector3(Buttonpos.x + (float)UnityEngine.Random.Range(-13, 13), Buttonpos.y + (float)UnityEngine.Random.Range(8, 25), Buttonpos.z), StarDupe[i].gameObject, "star");
		}
		seq.Insert(0.4f, RewardCoinsText.transform.DOMove(new Vector3(Buttonpos.x, Buttonpos.y + 33f, Buttonpos.z), 1.2f));
		LerpingFunction(new Vector3(Buttonpos.x - 5f, Buttonpos.y + 25f, Buttonpos.z), coinDupe[0].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x + 5f, Buttonpos.y + 25f, Buttonpos.z), tokenDupe[0].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x + 12f, Buttonpos.y + 20f, Buttonpos.z), coinDupe[1].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x - 15f, Buttonpos.y + 20f, Buttonpos.z), tokenDupe[1].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x - 3f, Buttonpos.y + 18f, Buttonpos.z), coinDupe[2].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x + 4f, Buttonpos.y + 16f, Buttonpos.z), tokenDupe[2].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x - 8f, Buttonpos.y + 13f, Buttonpos.z), coinDupe[3].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x + 13f, Buttonpos.y + 9f, Buttonpos.z), tokenDupe[3].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x - 1f, Buttonpos.y + 11f, Buttonpos.z), coinDupe[4].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x + 5f, Buttonpos.y + 10f, Buttonpos.z), tokenDupe[4].gameObject, "gameobject1");
		Invoke("CoinAnim2", 1.6f);
	}

	public void CoinAnim2()
	{
		seq = DOTween.Sequence();
		seq.Insert(0.4f, RewardCoinsText.DOFade(0f, 0.8f));
		float num = 0.16f;
		float num2 = 0.06f;
		for (int i = 0; i < 5; i++)
		{
			seq.Insert(0.08f + num * (float)i, coinDupe[i].transform.DOMove(coinTarget.position, 0.8f));
			seq.Insert(0.08f + num * (float)i, coinDupe[i].transform.DORotate(Vector3.zero, 0.4f));
			seq.Insert(0.88f + num * (float)i, coinDupe[i].transform.DOScale(Vector3.zero, 0f));
		}
		for (int j = 0; j < 5; j++)
		{
			seq.Insert(0.16f + num * (float)j, tokenDupe[j].transform.DOMove(tokenTarget.position, 0.8f));
			seq.Insert(0.16f + num * (float)j, tokenDupe[j].transform.DORotate(Vector3.zero, 0.4f));
			seq.Insert(0.96f + num * (float)j, tokenDupe[j].transform.DOScale(Vector3.zero, 0f));
		}
		for (int k = 0; k < 15; k++)
		{
			if (k % 2 == 0)
			{
				seq.Insert(0.08f + num2 * (float)k, StarDupe[k].transform.DOMove(tokenTarget.position, 0.8f));
				seq.Insert(0.6f + num2 * (float)k, StarDupe[k].transform.DOScale(Vector3.zero, 0.2f));
			}
			else
			{
				seq.Insert(0.08f + num2 * (float)k, StarDupe[k].transform.DOMove(coinTarget.position, 0.8f));
				seq.Insert(0.6f + num2 * (float)k, StarDupe[k].transform.DOScale(Vector3.zero, 0.2f));
			}
		}
		Invoke("DestroyCoins", 1.7f);
	}

	public void DestroyCoins()
	{
		RewardCoinsText.gameObject.SetActive(value: false);
		Singleton<AchievementManager>.instance.scroll.enabled = true;
		Singleton<AchievementManager>.instance.KitScroll.enabled = true;
		Singleton<AchievementManager>.instance.ResetDetails();
		IEnumerator enumerator = parent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = enumerator as IDisposable) != null)
			{
				disposable.Dispose();
			}
		}
	}
}
