using System;
using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AchievementAnimation : Singleton<AchievementAnimation>
{
	public Text RewardCoinsText;

	public Text CoinsTopPanel;

	public Text TicketTopPanel;

	public Text XpTopPanel;

	public Button claimBtn;

	public Vector3 Buttonpos;

	public Sprite[] ClaimAnimSprite;

	public Transform[] ClaimTransform;

	public GameObject CoinClaim;

	public GameObject StarClaim;

	public GameObject TokenClaim;

	public GameObject[] coinDupe;

	public GameObject[] tokenDupe;

	public GameObject[] StarDupe;

	public Transform coinTarget;

	public Transform tokenTarget;

	public Transform parent;

	public Sequence seq;

	public int TicketPackIndex = -1;

	public void Start()
	{
		coinDupe = new GameObject[5];
		tokenDupe = new GameObject[5];
		StarDupe = new GameObject[15];
	}

	public void ChangeSprite(int index)
	{
		CoinClaim.GetComponent<Image>().sprite = ClaimAnimSprite[index];
		coinTarget.position = ClaimTransform[index].position;
		tokenTarget.position = ClaimTransform[index].position;
	}

	public void ResetCoinAnim()
	{
		claimBtn.interactable = false;
		seq.SetUpdate(isIndependentUpdate: true);
		RewardCoinsText.gameObject.SetActive(value: true);
		CoinClaim.SetActive(value: true);
		TokenClaim.SetActive(value: true);
		StarClaim.SetActive(value: true);
		for (int i = 0; i < 5; i++)
		{
			coinDupe[i] = UnityEngine.Object.Instantiate(CoinClaim, CoinClaim.transform.position, CoinClaim.transform.rotation, parent);
			coinDupe[i].transform.localScale = Vector3.zero;
		}
		for (int j = 0; j < 5; j++)
		{
			tokenDupe[j] = UnityEngine.Object.Instantiate(TokenClaim, TokenClaim.transform.position, TokenClaim.transform.rotation, parent);
			tokenDupe[j].transform.localScale = Vector3.zero;
		}
		for (int k = 0; k < 15; k++)
		{
			StarDupe[k] = UnityEngine.Object.Instantiate(StarClaim, StarClaim.transform.position, StarClaim.transform.rotation, parent);
			StarDupe[k].transform.localScale = Vector3.zero;
		}
		CoinClaim.SetActive(value: false);
		TokenClaim.SetActive(value: false);
		StarClaim.SetActive(value: false);
		if (TicketPackIndex == -1)
		{
			Buttonpos = claimBtn.transform.position;
		}
		else if (TicketPackIndex >= 0)
		{
			Buttonpos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		}
		RewardCoinsText.transform.localScale = Vector3.zero;
		RewardCoinsText.transform.position = Buttonpos;
	}

	public void LerpingFunction(Vector3 IndexVector, GameObject IndexGameObject, string indexname)
	{
		switch (indexname)
		{
		case "text":
			seq.Insert(0f, IndexGameObject.transform.DOMove(IndexVector, 0.3f));
			break;
		case "gameobject":
			seq.Insert(0f, IndexGameObject.transform.DOMove(IndexVector, 0.3f));
			seq.Insert(0f, IndexGameObject.transform.DORotate(new Vector3(0f, 0f, UnityEngine.Random.Range(0, 360)), 0.3f));
			seq.Insert(0f, IndexGameObject.transform.DOScale(Vector3.one, 0.3f));
			break;
		case "star":
			seq.Insert(0.1f, IndexGameObject.transform.DOMove(IndexVector, 1f));
			seq.Insert(0f, IndexGameObject.transform.DORotate(new Vector3(0f, 0f, UnityEngine.Random.Range(0, 360)), 1.3f));
			seq.Insert(0f, IndexGameObject.transform.DOScale(Vector3.one, 0.5f));
			break;
		case "gameobject1":
			seq.Insert(0.3f, IndexGameObject.transform.DOMove(IndexVector, 0.9f));
			break;
		}
	}

	public void PlaySound()
	{
		CONTROLLER.sndController.PlayCoinSnd();
	}

	public void CoinAnim(int index)
	{
		Invoke("PlaySound", 2f);
		ChangeSprite(index);
		ResetCoinAnim();
		float num = 1f;
		if (ManageScene.activeSceneName() == "MainMenu")
		{
			num = 0.75f;
			if (Singleton<Store>.instance.Holder.activeInHierarchy)
			{
				num = 4f;
				Singleton<Store>.instance.StoreBlocker.SetActive(value: true);
			}
		}
		else
		{
			num = 1f;
		}
		if (TicketPackIndex >= 0)
		{
			num = 3f;
		}
		seq = DOTween.Sequence();
		seq.SetUpdate(isIndependentUpdate: true);
		seq.Append(RewardCoinsText.transform.DOScale(Vector3.one, 0.3f));
		seq.Insert(0f, RewardCoinsText.DOFade(1f, 0.3f));
		LerpingFunction(new Vector3(Buttonpos.x, Buttonpos.y + 120f, Buttonpos.z), RewardCoinsText.gameObject, "text");
		LerpingFunction(new Vector3(Buttonpos.x - 10f * num, Buttonpos.y + 80f * num, Buttonpos.z), coinDupe[0].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x + 16f * num, Buttonpos.y + 80f * num, Buttonpos.z), tokenDupe[0].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x + 40f * num, Buttonpos.y + 64f * num, Buttonpos.z), coinDupe[1].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x - 50f * num, Buttonpos.y + 64f * num, Buttonpos.z), tokenDupe[1].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x - 8f * num, Buttonpos.y + 56f * num, Buttonpos.z), coinDupe[2].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x + 12f * num, Buttonpos.y + 56f * num, Buttonpos.z), tokenDupe[2].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x - 24f * num, Buttonpos.y + 14f * num, Buttonpos.z), coinDupe[3].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x + 44f * num, Buttonpos.y + 28f * num, Buttonpos.z), tokenDupe[3].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x - 2f * num, Buttonpos.y + 34f * num, Buttonpos.z), coinDupe[4].gameObject, "gameobject");
		LerpingFunction(new Vector3(Buttonpos.x + 16f * num, Buttonpos.y + 32f * num, Buttonpos.z), tokenDupe[4].gameObject, "gameobject");
		for (int i = 0; i < 15; i++)
		{
			LerpingFunction(new Vector3(Buttonpos.x + UnityEngine.Random.Range(-52f * num, 52f * num), Buttonpos.y + UnityEngine.Random.Range(32f * num, 100f * num), Buttonpos.z), StarDupe[i].gameObject, "star");
		}
		seq.Insert(0.3f, RewardCoinsText.transform.DOMove(new Vector3(Buttonpos.x, Buttonpos.y + 132f, Buttonpos.z), 1f));
		LerpingFunction(new Vector3(Buttonpos.x - 20f * num, Buttonpos.y + 100f * num, Buttonpos.z), coinDupe[0].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x + 20f * num, Buttonpos.y + 100f * num, Buttonpos.z), tokenDupe[0].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x + 48f * num, Buttonpos.y + 80f * num, Buttonpos.z), coinDupe[1].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x - 60f * num, Buttonpos.y + 80f * num, Buttonpos.z), tokenDupe[1].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x - 12f * num, Buttonpos.y + 72f * num, Buttonpos.z), coinDupe[2].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x + 16f * num, Buttonpos.y + 64f * num, Buttonpos.z), tokenDupe[2].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x - 32f * num, Buttonpos.y + 52f * num, Buttonpos.z), coinDupe[3].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x + 52f * num, Buttonpos.y + 36f * num, Buttonpos.z), tokenDupe[3].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x - 4f * num, Buttonpos.y + 44f * num, Buttonpos.z), coinDupe[4].gameObject, "gameobject1");
		LerpingFunction(new Vector3(Buttonpos.x + 20f * num, Buttonpos.y + 40f * num, Buttonpos.z), tokenDupe[4].gameObject, "gameobject1");
		seq.OnComplete(CoinAnim2);
	}

	public void CoinAnim2()
	{
		seq = DOTween.Sequence();
		seq.SetUpdate(isIndependentUpdate: true);
		seq.Insert(0.3f, RewardCoinsText.DOFade(0f, 0.8f));
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
		if (ManageScene.activeSceneName() == "MainMenu")
		{
			Singleton<SpinWheel>.instance.TopPanelCoinsText.text = ObscuredPrefs.GetInt(CONTROLLER.CoinsKey).ToString();
			Singleton<SpinWheel>.instance.ToppanelTicketText.text = ObscuredPrefs.GetInt(CONTROLLER.TicketsKey).ToString();
			Singleton<SpinWheel>.instance.TopPanelXpText.text = ObscuredPrefs.GetInt(CONTROLLER.XPKey).ToString();
			Singleton<PowerUps>.instance.ResetDetails();
		}
		seq.OnComplete(DestroyCoins);
	}

	public void DestroyCoins()
	{
		claimBtn.interactable = true;
		RewardCoinsText.gameObject.SetActive(value: false);
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
		if (ManageScene.activeSceneName() == "MainMenu")
		{
			Singleton<AchievementManager>.instance.scroll.enabled = true;
			Singleton<AchievementManager>.instance.KitScroll.enabled = true;
			Singleton<AchievementManager>.instance.ResetDetails();
			if (Singleton<SpinWheel>.instance.SpinWheelHolder.activeInHierarchy)
			{
				Singleton<SpinWheel>.instance.LandingSpinWheelPage();
			}
			Singleton<Store>.instance.StoreBlocker.SetActive(value: false);
		}
	}
}
