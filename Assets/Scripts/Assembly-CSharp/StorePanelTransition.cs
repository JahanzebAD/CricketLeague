using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StorePanelTransition : Singleton<StorePanelTransition>
{
	public Image[] TopPanel;

	public Image[] RefTopPanel;

	public Image[] PanelHolder;

	public Image[] DetailsPanel;

	public Image[] CoinsPanel1;

	public Image[] CoinsPanel2;

	public Image[] CoinsPanel3;

	public Image[] CoinsPanel4;

	public Image[] CoinsPanel5;

	public Image[] CoinsPanel6;

	public Image[] CoinsPanel7;

	public Image[] TokenPanel1;

	public Image[] TokenPanel2;

	public Image[] TokenPanel3;

	public Image[] TokenPanel4;

	public Image[] TokenPanel5;

	public Image[] TokenPanel6;

	public Image[] TicketPanel1;

	public Image[] TicketPanel2;

	public Image[] TicketPanel3;

	public Image[] TicketPanel4;

	public Image[] TicketPanel5;

	public Image[] TicketPanel6;

	public Image[] IAPPanel1;

	public Image[] IAPPanel2;

	public Image[] IAPPanel3;

	public Text[] TopText1;

	public Text[] DetailsText;

	public Text[] CoinsText1;

	public Text[] CoinsText2;

	public Text[] CoinsText3;

	public Text[] CoinsText4;

	public Text[] CoinsText5;

	public Text[] CoinsText6;

	public Text[] CoinsText7;

	public Text[] TokenText1;

	public Text[] TokenText2;

	public Text[] TokenText3;

	public Text[] TokenText4;

	public Text[] TokenText5;

	public Text[] TokenText6;

	public Text[] TicketText1;

	public Text[] TicketText2;

	public Text[] TicketText3;

	public Text[] TicketText4;

	public Text[] TicketText5;

	public Text[] TicketText6;

	public Text[] IAPText1;

	public Text[] IAPText2;

	public Text[] IAPText3;

	public void resetTransition()
	{
		for (int i = 0; i < CoinsPanel1.Length; i++)
		{
			CoinsPanel1[i].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			CoinsPanel3[i].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			CoinsPanel4[i].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			CoinsPanel5[i].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			CoinsPanel6[i].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			CoinsPanel7[i].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int j = 0; j < CoinsPanel2.Length; j++)
		{
			CoinsPanel2[j].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int k = 0; k < TokenPanel1.Length; k++)
		{
			TokenPanel1[k].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int l = 0; l < TokenPanel2.Length; l++)
		{
			TokenPanel2[l].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int m = 0; m < TokenPanel3.Length; m++)
		{
			TokenPanel3[m].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TokenPanel4[m].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TokenPanel5[m].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TokenPanel6[m].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int n = 0; n < TicketPanel1.Length; n++)
		{
			TicketPanel1[n].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int num = 0; num < TicketPanel2.Length; num++)
		{
			TicketPanel2[num].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TicketPanel3[num].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TicketPanel4[num].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TicketPanel5[num].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TicketPanel6[num].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int num2 = 0; num2 < IAPPanel1.Length; num2++)
		{
			IAPPanel1[num2].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			IAPPanel2[num2].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			IAPPanel3[num2].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int num3 = 0; num3 < CoinsText1.Length; num3++)
		{
			CoinsText1[num3].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			CoinsText2[num3].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int num4 = 0; num4 < CoinsText3.Length; num4++)
		{
			CoinsText3[num4].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			CoinsText4[num4].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			CoinsText5[num4].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			CoinsText6[num4].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			CoinsText7[num4].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int num5 = 0; num5 < TokenText1.Length; num5++)
		{
			TokenText1[num5].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TokenText2[num5].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int num6 = 0; num6 < TokenText3.Length; num6++)
		{
			TokenText3[num6].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TokenText4[num6].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TokenText5[num6].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TokenText6[num6].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int num7 = 0; num7 < TicketText1.Length; num7++)
		{
			TicketText1[num7].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int num8 = 0; num8 < TicketText2.Length; num8++)
		{
			TicketText2[num8].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TicketText3[num8].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TicketText4[num8].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TicketText5[num8].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			TicketText6[num8].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int num9 = 0; num9 < IAPText1.Length; num9++)
		{
			IAPText1[num9].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			IAPText2[num9].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			IAPText3[num9].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		CoinsPanel1[0].transform.localScale = new Vector3(1f, 0f, 1f);
		CoinsPanel1[1].transform.localScale = new Vector3(0f, 1f, 1f);
		CoinsPanel2[0].transform.localScale = new Vector3(1f, 0f, 1f);
		CoinsPanel2[1].transform.localScale = new Vector3(0f, 1f, 1f);
		CoinsPanel3[0].transform.localScale = new Vector3(1f, 0f, 1f);
		CoinsPanel3[1].transform.localScale = new Vector3(0f, 1f, 1f);
		CoinsPanel4[0].transform.localScale = new Vector3(1f, 0f, 1f);
		CoinsPanel4[1].transform.localScale = new Vector3(0f, 1f, 1f);
		CoinsPanel5[0].transform.localScale = new Vector3(1f, 0f, 1f);
		CoinsPanel5[1].transform.localScale = new Vector3(0f, 1f, 1f);
		CoinsPanel6[0].transform.localScale = new Vector3(1f, 0f, 1f);
		CoinsPanel6[1].transform.localScale = new Vector3(0f, 1f, 1f);
		CoinsPanel7[0].transform.localScale = new Vector3(1f, 0f, 1f);
		CoinsPanel7[1].transform.localScale = new Vector3(0f, 1f, 1f);
		TokenPanel1[0].transform.localScale = new Vector3(1f, 0f, 1f);
		TokenPanel1[1].transform.localScale = new Vector3(0f, 1f, 1f);
		TokenPanel2[0].transform.localScale = new Vector3(1f, 0f, 1f);
		TokenPanel2[1].transform.localScale = new Vector3(0f, 1f, 1f);
		TokenPanel3[0].transform.localScale = new Vector3(1f, 0f, 1f);
		TokenPanel3[1].transform.localScale = new Vector3(0f, 1f, 1f);
		TokenPanel4[0].transform.localScale = new Vector3(1f, 0f, 1f);
		TokenPanel4[1].transform.localScale = new Vector3(0f, 1f, 1f);
		TokenPanel5[0].transform.localScale = new Vector3(1f, 0f, 1f);
		TokenPanel5[1].transform.localScale = new Vector3(0f, 1f, 1f);
		TokenPanel6[0].transform.localScale = new Vector3(1f, 0f, 1f);
		TokenPanel6[1].transform.localScale = new Vector3(0f, 1f, 1f);
		TicketPanel1[0].transform.localScale = new Vector3(1f, 0f, 1f);
		TicketPanel1[1].transform.localScale = new Vector3(0f, 1f, 1f);
		TicketPanel2[0].transform.localScale = new Vector3(1f, 0f, 1f);
		TicketPanel2[1].transform.localScale = new Vector3(0f, 1f, 1f);
		TicketPanel3[0].transform.localScale = new Vector3(1f, 0f, 1f);
		TicketPanel3[1].transform.localScale = new Vector3(0f, 1f, 1f);
		TicketPanel4[0].transform.localScale = new Vector3(1f, 0f, 1f);
		TicketPanel4[1].transform.localScale = new Vector3(0f, 1f, 1f);
		TicketPanel5[0].transform.localScale = new Vector3(1f, 0f, 1f);
		TicketPanel5[1].transform.localScale = new Vector3(0f, 1f, 1f);
		TicketPanel6[0].transform.localScale = new Vector3(1f, 0f, 1f);
		TicketPanel6[1].transform.localScale = new Vector3(0f, 1f, 1f);
		IAPPanel1[0].transform.localScale = new Vector3(1f, 0f, 1f);
		IAPPanel2[0].transform.localScale = new Vector3(1f, 0f, 1f);
		IAPPanel3[0].transform.localScale = new Vector3(1f, 0f, 1f);
	}

	public void TopPanelResetTransition()
	{
		Singleton<Store>.instance.StoreOpenState = 0;
		for (int i = 0; i < TopPanel.Length; i++)
		{
			TopPanel[i].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int j = 0; j < PanelHolder.Length; j++)
		{
			PanelHolder[j].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int k = 0; k < DetailsPanel.Length; k++)
		{
			DetailsPanel[k].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int l = 0; l < TopText1.Length; l++)
		{
			TopText1[l].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int m = 0; m < DetailsText.Length; m++)
		{
			DetailsText[m].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		TopPanel[0].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		for (int n = 0; n < PanelHolder.Length; n++)
		{
			PanelHolder[n].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		}
		TopPanel[1].transform.localPosition = new Vector3(-454f, 0f, 0f);
		TopPanel[2].transform.localPosition = new Vector3(-454f, 0f, 0f);
		TopPanel[3].transform.localPosition = new Vector3(-454f, 0f, 0f);
		TopPanel[4].transform.localPosition = new Vector3(-454f, 0f, 0f);
		TopPanel[5].transform.localPosition = new Vector3(-454f, 0f, 0f);
		DetailsPanel[0].transform.localScale = new Vector3(0f, 1f, 1f);
	}

	public void panelTransition()
	{
		resetTransition();
		TopPanelResetTransition();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(TopPanel[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
		sequence.Insert(0f, TopPanel[0].DOFade(1f, 0f));
		for (int i = 0; i < PanelHolder.Length; i++)
		{
			sequence.Insert(0.2f, PanelHolder[i].DOFade(1f, 0f));
			sequence.Insert(0.2f, PanelHolder[i].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		}
		sequence.Insert(0.8f, TopPanel[1].DOFade(1f, 0f));
		sequence.Insert(0.7f, TopPanel[2].DOFade(1f, 0f));
		sequence.Insert(0.6f, TopPanel[3].DOFade(1f, 0f));
		sequence.Insert(0.5f, TopPanel[4].DOFade(1f, 0f));
		sequence.Insert(0.4f, TopPanel[5].DOFade(1f, 0f));
		sequence.Insert(0.8f, TopPanel[1].transform.DOLocalMove(RefTopPanel[0].transform.localPosition, 0.2f));
		sequence.Insert(0.7f, TopPanel[2].transform.DOLocalMove(RefTopPanel[1].transform.localPosition, 0.2f));
		sequence.Insert(0.6f, TopPanel[3].transform.DOLocalMove(RefTopPanel[2].transform.localPosition, 0.2f));
		sequence.Insert(0.5f, TopPanel[4].transform.DOLocalMove(RefTopPanel[3].transform.localPosition, 0.2f));
		sequence.Insert(0.4f, TopPanel[5].transform.DOLocalMove(RefTopPanel[4].transform.localPosition, 0.2f));
		for (int j = 0; j < TopText1.Length; j++)
		{
			sequence.Insert(1f, TopText1[j].DOFade(1f, 0.05f));
		}
		sequence.Insert(0.8f, DetailsPanel[0].DOFade(0.65f, 0.1f));
		for (int k = 1; k < DetailsPanel.Length; k++)
		{
			sequence.Insert(1f, DetailsPanel[k].DOFade(1f, 0.1f));
		}
		sequence.Insert(0.8f, DetailsPanel[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f));
		for (int l = 0; l < DetailsText.Length; l++)
		{
			sequence.Insert(1f, DetailsText[l].DOFade(1f, 0.01f));
		}
		sequence.SetUpdate(isIndependentUpdate: true);
		StoreState();
	}

	public void StoreState()
	{
		if (Singleton<Store>.instance.StoreOpenState == 1 || Singleton<Store>.instance.StoreOpenState == 0)
		{
			CoinTransition();
		}
		else if (Singleton<Store>.instance.StoreOpenState == 2)
		{
			TokenTransition();
		}
		else if (Singleton<Store>.instance.StoreOpenState == 3)
		{
			TicketTransition();
		}
		else if (Singleton<Store>.instance.StoreOpenState == 4)
		{
			IAPTransition();
		}
		else if (Singleton<Store>.instance.StoreOpenState != 5)
		{
		}
	}

	public void IAPTransition()
	{
		float num = 0.5f;
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0.5f - num, IAPPanel1[0].DOFade(1f, 0.05f));
		sequence.Insert(0.55f - num, IAPPanel1[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.7f - num, IAPPanel1[1].DOFade(1f, 0.1f));
		sequence.Insert(0.8f - num, IAPPanel1[2].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, IAPPanel1[3].DOFade(1f, 0.1f));
		sequence.Insert(0.8f - num, IAPText1[0].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, IAPText1[1].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, IAPText1[2].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, IAPText1[3].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, IAPText1[4].DOFade(1f, 0.1f));
		num = 0.4f;
		sequence.Insert(0.5f - num, IAPPanel2[0].DOFade(1f, 0.05f));
		sequence.Insert(0.55f - num, IAPPanel2[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.7f - num, IAPPanel2[1].DOFade(1f, 0.1f));
		sequence.Insert(0.8f - num, IAPPanel2[2].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, IAPPanel2[3].DOFade(1f, 0.1f));
		sequence.Insert(0.8f - num, IAPText2[0].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, IAPText2[1].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, IAPText2[2].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, IAPText2[3].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, IAPText2[4].DOFade(1f, 0.1f));
		num = 0.3f;
		sequence.Insert(0.5f - num, IAPPanel3[0].DOFade(1f, 0.05f));
		sequence.Insert(0.55f - num, IAPPanel3[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.7f - num, IAPPanel3[1].DOFade(1f, 0.1f));
		sequence.Insert(0.8f - num, IAPPanel3[2].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, IAPPanel3[3].DOFade(1f, 0.1f));
		sequence.Insert(0.8f - num, IAPText3[0].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, IAPText3[1].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, IAPText3[2].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, IAPText3[3].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, IAPText3[4].DOFade(1f, 0.1f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}

	public void TicketTransition()
	{
		float num = 0.5f;
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0.5f - num, TicketPanel1[4].DOFade(1f, 0.05f));
		sequence.Insert(0.5f - num, TicketPanel1[0].DOFade(1f, 0.05f));
		sequence.Insert(0.55f - num, TicketPanel1[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.65f - num, TicketPanel1[1].DOFade(1f, 0.05f));
		sequence.Insert(0.65f - num, TicketPanel1[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.85f - num, TicketPanel1[3].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, TicketText1[0].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, TicketText1[1].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, TicketPanel1[2].DOFade(1f, 0.1f));
		sequence.Insert(0.6f - num, TicketPanel2[4].DOFade(1f, 0.05f));
		sequence.Insert(0.6f - num, TicketPanel2[0].DOFade(1f, 0.05f));
		sequence.Insert(0.65f - num, TicketPanel2[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.75f - num, TicketPanel2[1].DOFade(1f, 0.05f));
		sequence.Insert(0.75f - num, TicketPanel2[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.95f - num, TicketPanel2[3].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, TicketText2[1].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, TicketText2[2].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, TicketPanel2[2].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, TicketText2[0].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, TicketPanel2[5].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, TicketText2[3].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, TicketText2[4].DOFade(1f, 0.1f));
		sequence.Insert(0.7f - num, TicketPanel3[4].DOFade(1f, 0.05f));
		sequence.Insert(0.7f - num, TicketPanel3[0].DOFade(1f, 0.05f));
		sequence.Insert(0.75f - num, TicketPanel3[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.85f - num, TicketPanel3[1].DOFade(1f, 0.05f));
		sequence.Insert(0.85f - num, TicketPanel3[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.05f - num, TicketPanel3[3].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, TicketText3[1].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, TicketText3[2].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, TicketPanel3[2].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, TicketText3[0].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TicketPanel3[5].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TicketText3[3].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TicketText3[4].DOFade(1f, 0.1f));
		sequence.Insert(0.8f - num, TicketPanel4[4].DOFade(1f, 0.05f));
		sequence.Insert(0.8f - num, TicketPanel4[0].DOFade(1f, 0.05f));
		sequence.Insert(0.85f - num, TicketPanel4[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.95f - num, TicketPanel4[1].DOFade(1f, 0.05f));
		sequence.Insert(0.95f - num, TicketPanel4[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.15f - num, TicketPanel4[3].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, TicketText4[1].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, TicketText4[2].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TicketPanel4[2].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TicketText4[0].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TicketPanel4[5].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TicketText4[3].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TicketText4[4].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, TicketPanel5[4].DOFade(1f, 0.05f));
		sequence.Insert(0.9f - num, TicketPanel5[0].DOFade(1f, 0.05f));
		sequence.Insert(0.95f - num, TicketPanel5[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.05f - num, TicketPanel5[1].DOFade(1f, 0.05f));
		sequence.Insert(1.05f - num, TicketPanel5[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.25f - num, TicketPanel5[3].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TicketText5[1].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TicketText5[2].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TicketPanel5[2].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TicketText5[0].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, TicketPanel5[5].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, TicketText5[3].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, TicketText5[4].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, TicketPanel6[4].DOFade(1f, 0.05f));
		sequence.Insert(0.9f - num, TicketPanel6[0].DOFade(1f, 0.05f));
		sequence.Insert(0.95f - num, TicketPanel6[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.05f - num, TicketPanel6[1].DOFade(1f, 0.05f));
		sequence.Insert(1.05f - num, TicketPanel6[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.25f - num, TicketPanel6[3].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TicketText6[1].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TicketText6[2].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TicketPanel6[2].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TicketText6[0].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, TicketPanel6[5].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, TicketText6[3].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, TicketText6[4].DOFade(1f, 0.1f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}

	public void TokenTransition()
	{
		float num = 0.5f;
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0.5f - num, TokenPanel1[6].DOFade(1f, 0.05f));
		sequence.Insert(0.5f - num, TokenPanel1[0].DOFade(1f, 0.05f));
		sequence.Insert(0.55f - num, TokenPanel1[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.65f - num, TokenPanel1[1].DOFade(1f, 0.05f));
		sequence.Insert(0.65f - num, TokenPanel1[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.85f - num, TokenPanel1[5].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, TokenText1[1].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, TokenText1[2].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, TokenPanel1[2].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, TokenText1[0].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, TokenPanel1[3].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, TokenPanel1[4].DOFade(1f, 0.1f));
		sequence.Insert(0.6f - num, TokenPanel2[4].DOFade(1f, 0.05f));
		sequence.Insert(0.6f - num, TokenPanel2[0].DOFade(1f, 0.05f));
		sequence.Insert(0.65f - num, TokenPanel2[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.75f - num, TokenPanel2[1].DOFade(1f, 0.05f));
		sequence.Insert(0.75f - num, TokenPanel2[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.95f - num, TokenPanel2[3].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, TokenText2[1].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, TokenText2[2].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, TokenPanel2[2].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, TokenText2[0].DOFade(1f, 0.1f));
		sequence.Insert(0.7f - num, TokenPanel3[4].DOFade(1f, 0.05f));
		sequence.Insert(0.7f - num, TokenPanel3[0].DOFade(1f, 0.05f));
		sequence.Insert(0.75f - num, TokenPanel3[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.85f - num, TokenPanel3[1].DOFade(1f, 0.05f));
		sequence.Insert(0.85f - num, TokenPanel3[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.05f - num, TokenPanel3[3].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, TokenText3[1].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, TokenText3[2].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, TokenPanel3[2].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, TokenText3[0].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TokenPanel3[5].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TokenText3[3].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TokenText3[4].DOFade(1f, 0.1f));
		sequence.Insert(0.8f - num, TokenPanel4[4].DOFade(1f, 0.05f));
		sequence.Insert(0.8f - num, TokenPanel4[0].DOFade(1f, 0.05f));
		sequence.Insert(0.85f - num, TokenPanel4[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.95f - num, TokenPanel4[1].DOFade(1f, 0.05f));
		sequence.Insert(0.95f - num, TokenPanel4[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.15f - num, TokenPanel4[3].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, TokenText4[1].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, TokenText4[2].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TokenPanel4[2].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TokenText4[0].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TokenPanel4[5].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TokenText4[3].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TokenText4[4].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, TokenPanel5[4].DOFade(1f, 0.05f));
		sequence.Insert(0.9f - num, TokenPanel5[0].DOFade(1f, 0.05f));
		sequence.Insert(0.95f - num, TokenPanel5[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.05f - num, TokenPanel5[1].DOFade(1f, 0.05f));
		sequence.Insert(1.05f - num, TokenPanel5[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.25f - num, TokenPanel5[3].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TokenText5[1].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, TokenText5[2].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TokenPanel5[2].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TokenText5[0].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, TokenPanel5[5].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, TokenText5[3].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, TokenText5[4].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, TokenPanel6[4].DOFade(1f, 0.05f));
		sequence.Insert(1f - num, TokenPanel6[0].DOFade(1f, 0.05f));
		sequence.Insert(1.05f - num, TokenPanel6[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.15f - num, TokenPanel6[1].DOFade(1f, 0.05f));
		sequence.Insert(1.15f - num, TokenPanel6[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.35f - num, TokenPanel6[3].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TokenText6[1].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, TokenText6[2].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, TokenPanel6[2].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, TokenText6[0].DOFade(1f, 0.1f));
		sequence.Insert(1.6f - num, TokenPanel6[5].DOFade(1f, 0.1f));
		sequence.Insert(1.6f - num, TokenText6[3].DOFade(1f, 0.1f));
		sequence.Insert(1.6f - num, TokenText6[4].DOFade(1f, 0.1f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}

	public void CoinTransition()
	{
		float num = ((Singleton<Store>.instance.StoreOpenState != 1) ? 0f : 0.5f);
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0.5f - num, CoinsPanel1[6].DOFade(1f, 0.05f));
		sequence.Insert(0.5f - num, CoinsPanel1[0].DOFade(1f, 0.05f));
		sequence.Insert(0.55f - num, CoinsPanel1[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.65f - num, CoinsPanel1[1].DOFade(1f, 0.05f));
		sequence.Insert(0.65f - num, CoinsPanel1[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.85f - num, CoinsPanel1[5].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, CoinsText1[1].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, CoinsText1[2].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, CoinsPanel1[2].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, CoinsText1[0].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, CoinsPanel1[3].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, CoinsPanel1[4].DOFade(1f, 0.1f));
		sequence.Insert(0.6f - num, CoinsPanel2[5].DOFade(1f, 0.05f));
		sequence.Insert(0.6f - num, CoinsPanel2[0].DOFade(1f, 0.05f));
		sequence.Insert(0.65f - num, CoinsPanel2[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.75f - num, CoinsPanel2[1].DOFade(1f, 0.05f));
		sequence.Insert(0.75f - num, CoinsPanel2[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.95f - num, CoinsPanel2[4].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, CoinsText2[1].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, CoinsText2[2].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, CoinsPanel2[2].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, CoinsText2[0].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, CoinsPanel2[3].DOFade(1f, 0.1f));
		sequence.Insert(0.7f - num, CoinsPanel3[5].DOFade(1f, 0.05f));
		sequence.Insert(0.7f - num, CoinsPanel3[0].DOFade(1f, 0.05f));
		sequence.Insert(0.75f - num, CoinsPanel3[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.85f - num, CoinsPanel3[1].DOFade(1f, 0.05f));
		sequence.Insert(0.85f - num, CoinsPanel3[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.05f - num, CoinsPanel3[5].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, CoinsText3[1].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, CoinsText3[2].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, CoinsPanel3[2].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, CoinsText3[0].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, CoinsPanel3[3].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, CoinsPanel3[4].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, CoinsPanel3[6].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, CoinsText3[3].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, CoinsText3[4].DOFade(1f, 0.1f));
		sequence.Insert(0.8f - num, CoinsPanel4[5].DOFade(1f, 0.05f));
		sequence.Insert(0.8f - num, CoinsPanel4[0].DOFade(1f, 0.05f));
		sequence.Insert(0.85f - num, CoinsPanel4[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.95f - num, CoinsPanel4[1].DOFade(1f, 0.05f));
		sequence.Insert(0.95f - num, CoinsPanel4[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.15f - num, CoinsPanel4[5].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, CoinsText4[1].DOFade(1f, 0.1f));
		sequence.Insert(1.2f - num, CoinsText4[2].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, CoinsPanel4[2].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, CoinsText4[0].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, CoinsPanel4[3].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, CoinsPanel4[4].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, CoinsPanel4[6].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, CoinsText4[3].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, CoinsText4[4].DOFade(1f, 0.1f));
		sequence.Insert(0.9f - num, CoinsPanel5[5].DOFade(1f, 0.05f));
		sequence.Insert(0.9f - num, CoinsPanel5[0].DOFade(1f, 0.05f));
		sequence.Insert(0.95f - num, CoinsPanel5[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.05f - num, CoinsPanel5[1].DOFade(1f, 0.05f));
		sequence.Insert(1.05f - num, CoinsPanel5[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.25f - num, CoinsPanel5[5].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, CoinsText5[1].DOFade(1f, 0.1f));
		sequence.Insert(1.3f - num, CoinsText5[2].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, CoinsPanel5[2].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, CoinsText5[0].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, CoinsPanel5[3].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, CoinsPanel5[4].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, CoinsPanel5[6].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, CoinsText5[3].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, CoinsText5[4].DOFade(1f, 0.1f));
		sequence.Insert(1f - num, CoinsPanel6[5].DOFade(1f, 0.05f));
		sequence.Insert(1f - num, CoinsPanel6[0].DOFade(1f, 0.05f));
		sequence.Insert(1.05f - num, CoinsPanel6[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.15f - num, CoinsPanel6[1].DOFade(1f, 0.05f));
		sequence.Insert(1.15f - num, CoinsPanel6[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.35f - num, CoinsPanel6[5].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, CoinsText6[1].DOFade(1f, 0.1f));
		sequence.Insert(1.4f - num, CoinsText6[2].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, CoinsPanel6[2].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, CoinsText6[0].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, CoinsPanel6[3].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, CoinsPanel6[4].DOFade(1f, 0.1f));
		sequence.Insert(1.6f - num, CoinsPanel6[6].DOFade(1f, 0.1f));
		sequence.Insert(1.6f - num, CoinsText6[3].DOFade(1f, 0.1f));
		sequence.Insert(1.6f - num, CoinsText6[4].DOFade(1f, 0.1f));
		sequence.Insert(1.1f - num, CoinsPanel7[5].DOFade(1f, 0.05f));
		sequence.Insert(1.1f - num, CoinsPanel7[0].DOFade(1f, 0.05f));
		sequence.Insert(1.15f - num, CoinsPanel7[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.25f - num, CoinsPanel7[1].DOFade(1f, 0.05f));
		sequence.Insert(1.25f - num, CoinsPanel7[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.45f - num, CoinsPanel7[5].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, CoinsText7[1].DOFade(1f, 0.1f));
		sequence.Insert(1.5f - num, CoinsText7[2].DOFade(1f, 0.1f));
		sequence.Insert(1.6f - num, CoinsPanel7[2].DOFade(1f, 0.1f));
		sequence.Insert(1.6f - num, CoinsText7[0].DOFade(1f, 0.1f));
		sequence.Insert(1.6f - num, CoinsPanel7[3].DOFade(1f, 0.1f));
		sequence.Insert(1.6f - num, CoinsPanel7[4].DOFade(1f, 0.1f));
		sequence.Insert(1.7f - num, CoinsPanel7[6].DOFade(1f, 0.1f));
		sequence.Insert(1.7f - num, CoinsText7[3].DOFade(1f, 0.1f));
		sequence.Insert(1.7f - num, CoinsText7[4].DOFade(1f, 0.1f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}
}
