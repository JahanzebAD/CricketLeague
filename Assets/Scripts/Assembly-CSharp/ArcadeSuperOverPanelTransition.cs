using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeSuperOverPanelTransition : Singleton<ArcadeSuperOverPanelTransition>
{
	public Transform Content;

	public Image[] MatchPanel1;

	public Image[] MatchPanel2;

	public Image[] MatchPanel3;

	public Image[] MatchPanel4;

	public Image[] MatchPanel5;

	public Image[] MatchPanel6;

	public Image[] MatchPanel7;

	public Image[] MatchPanel8;

	public Image[] MatchPanel9;

	public Image[] MatchPanel10;

	public Image[] MatchPanel11;

	public Image[] MatchPanel12;

	public Image[] MatchPanel13;

	public Image[] MatchPanel14;

	public Image[] MatchPanel15;

	public Image[] MatchPanel16;

	public Image[] MatchPanel17;

	public Image[] MatchPanel18;

	public Text[] MatchText1;

	public Text[] MatchText2;

	public Text[] MatchText3;

	public Text[] MatchText4;

	public Text[] MatchText5;

	public Text[] MatchText6;

	public Text[] MatchText7;

	public Text[] MatchText8;

	public Text[] MatchText9;

	public Text[] MatchText10;

	public Text[] MatchText11;

	public Text[] MatchText12;

	public Text[] MatchText13;

	public Text[] MatchText14;

	public Text[] MatchText15;

	public Text[] MatchText16;

	public Text[] MatchText17;

	public Text[] MatchText18;

	public void resetTransition()
	{
		for (int i = 0; i < MatchPanel1.Length; i++)
		{
			MatchPanel1[i].DOFade(0f, 0f);
			MatchPanel2[i].DOFade(0f, 0f);
			MatchPanel3[i].DOFade(0f, 0f);
			MatchPanel4[i].DOFade(0f, 0f);
			MatchPanel5[i].DOFade(0f, 0f);
			MatchPanel6[i].DOFade(0f, 0f);
			MatchPanel7[i].DOFade(0f, 0f);
			MatchPanel8[i].DOFade(0f, 0f);
			MatchPanel9[i].DOFade(0f, 0f);
			MatchPanel10[i].DOFade(0f, 0f);
			MatchPanel11[i].DOFade(0f, 0f);
			MatchPanel12[i].DOFade(0f, 0f);
			MatchPanel13[i].DOFade(0f, 0f);
			MatchPanel14[i].DOFade(0f, 0f);
			MatchPanel15[i].DOFade(0f, 0f);
			MatchPanel16[i].DOFade(0f, 0f);
			MatchPanel17[i].DOFade(0f, 0f);
			MatchPanel18[i].DOFade(0f, 0f);
		}
		for (int j = 0; j < MatchText1.Length; j++)
		{
			MatchText1[j].DOFade(0f, 0f);
			MatchText2[j].DOFade(0f, 0f);
			MatchText3[j].DOFade(0f, 0f);
			MatchText4[j].DOFade(0f, 0f);
			MatchText5[j].DOFade(0f, 0f);
			MatchText6[j].DOFade(0f, 0f);
			MatchText7[j].DOFade(0f, 0f);
			MatchText8[j].DOFade(0f, 0f);
			MatchText9[j].DOFade(0f, 0f);
			MatchText10[j].DOFade(0f, 0f);
			MatchText11[j].DOFade(0f, 0f);
			MatchText12[j].DOFade(0f, 0f);
			MatchText13[j].DOFade(0f, 0f);
			MatchText14[j].DOFade(0f, 0f);
			MatchText15[j].DOFade(0f, 0f);
			MatchText16[j].DOFade(0f, 0f);
			MatchText17[j].DOFade(0f, 0f);
			MatchText18[j].DOFade(0f, 0f);
		}
		MatchPanel1[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel1[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel2[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel2[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel3[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel3[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel4[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel4[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel5[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel5[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel6[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel6[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel7[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel7[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel8[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel8[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel9[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel9[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel10[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel10[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel11[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel11[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel12[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel12[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel13[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel13[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel14[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel14[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel15[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel15[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel16[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel16[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel17[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel17[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel18[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel18[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
	}

	public void panelTransition()
	{
		Content.transform.localPosition = new Vector3(2900f, -275f, 0f);
		resetTransition();
		Sequence s = DOTween.Sequence();
		s.Append(MatchPanel1[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(0.25f, MatchPanel1[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(0.1f, MatchPanel2[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(0.35f, MatchPanel2[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(0.2f, MatchPanel3[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(0.45f, MatchPanel3[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(0.3f, MatchPanel4[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(0.55f, MatchPanel4[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(0.4f, MatchPanel5[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(0.65f, MatchPanel5[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(0.5f, MatchPanel6[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(0.75f, MatchPanel6[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(0.6f, MatchPanel7[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(0.85f, MatchPanel7[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(0.7f, MatchPanel8[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(0.95f, MatchPanel8[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(0.8f, MatchPanel9[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(1.05f, MatchPanel9[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(0.9f, MatchPanel10[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(1.15f, MatchPanel10[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(1f, MatchPanel11[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(1.25f, MatchPanel11[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(1.1f, MatchPanel12[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(1.35f, MatchPanel12[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(1.2f, MatchPanel13[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(1.45f, MatchPanel13[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(1.3f, MatchPanel14[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(1.55f, MatchPanel14[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(1.4f, MatchPanel15[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(1.65f, MatchPanel15[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(1.5f, MatchPanel16[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(1.75f, MatchPanel16[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(1.6f, MatchPanel17[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(1.85f, MatchPanel17[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		s.Insert(1.7f, MatchPanel18[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		s.Insert(1.95f, MatchPanel18[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		int num;
		for (num = 0; num < 6; num++)
		{
			s.Insert(0f, MatchPanel1[num].DOFade(1f, 0f));
			s.Insert(0.1f, MatchPanel2[num].DOFade(1f, 0f));
			s.Insert(0.2f, MatchPanel3[num].DOFade(1f, 0f));
			s.Insert(0.3f, MatchPanel4[num].DOFade(1f, 0f));
			s.Insert(0.4f, MatchPanel5[num].DOFade(1f, 0f));
			s.Insert(0.5f, MatchPanel6[num].DOFade(1f, 0f));
			s.Insert(0.6f, MatchPanel7[num].DOFade(1f, 0f));
			s.Insert(0.7f, MatchPanel8[num].DOFade(1f, 0f));
			s.Insert(0.8f, MatchPanel9[num].DOFade(1f, 0f));
			s.Insert(0.9f, MatchPanel10[num].DOFade(1f, 0f));
			s.Insert(1f, MatchPanel11[num].DOFade(1f, 0f));
			s.Insert(1.1f, MatchPanel12[num].DOFade(1f, 0f));
			s.Insert(1.2f, MatchPanel13[num].DOFade(1f, 0f));
			s.Insert(1.3f, MatchPanel14[num].DOFade(1f, 0f));
			s.Insert(1.4f, MatchPanel15[num].DOFade(1f, 0f));
			s.Insert(1.5f, MatchPanel16[num].DOFade(1f, 0f));
			s.Insert(1.6f, MatchPanel17[num].DOFade(1f, 0f));
			s.Insert(1.7f, MatchPanel18[num].DOFade(1f, 0f));
			num = 4 + num;
		}
		for (int i = 2; i < 4; i++)
		{
			s.Insert(0.75f, MatchPanel1[i].DOFade(1f, 0.35f));
			s.Insert(0.85f, MatchPanel2[i].DOFade(1f, 0.35f));
			s.Insert(0.95f, MatchPanel3[i].DOFade(1f, 0.35f));
			s.Insert(1.05f, MatchPanel4[i].DOFade(1f, 0.35f));
			s.Insert(1.15f, MatchPanel5[i].DOFade(1f, 0.35f));
			s.Insert(1.25f, MatchPanel6[i].DOFade(1f, 0.35f));
			s.Insert(1.35f, MatchPanel7[i].DOFade(1f, 0.35f));
			s.Insert(1.45f, MatchPanel8[i].DOFade(1f, 0.35f));
			s.Insert(1.55f, MatchPanel9[i].DOFade(1f, 0.35f));
			s.Insert(1.65f, MatchPanel10[i].DOFade(1f, 0.35f));
			s.Insert(1.75f, MatchPanel11[i].DOFade(1f, 0.35f));
			s.Insert(1.85f, MatchPanel12[i].DOFade(1f, 0.35f));
			s.Insert(1.95f, MatchPanel13[i].DOFade(1f, 0.35f));
			s.Insert(2.05f, MatchPanel14[i].DOFade(1f, 0.35f));
			s.Insert(2.15f, MatchPanel15[i].DOFade(1f, 0.35f));
			s.Insert(2.25f, MatchPanel16[i].DOFade(1f, 0.35f));
			s.Insert(2.35f, MatchPanel17[i].DOFade(1f, 0.35f));
			s.Insert(2.45f, MatchPanel18[i].DOFade(1f, 0.35f));
		}
		int num2;
		for (num2 = 1; num2 < 5; num2++)
		{
			s.Insert(1f, MatchPanel1[num2].DOFade(1f, 0.35f));
			s.Insert(1.1f, MatchPanel2[num2].DOFade(1f, 0.35f));
			s.Insert(1.2f, MatchPanel3[num2].DOFade(1f, 0.35f));
			s.Insert(1.3f, MatchPanel4[num2].DOFade(1f, 0.35f));
			s.Insert(1.4f, MatchPanel5[num2].DOFade(1f, 0.35f));
			s.Insert(1.5f, MatchPanel6[num2].DOFade(1f, 0.35f));
			s.Insert(1.6f, MatchPanel7[num2].DOFade(1f, 0.35f));
			s.Insert(1.7f, MatchPanel8[num2].DOFade(1f, 0.35f));
			s.Insert(1.8f, MatchPanel9[num2].DOFade(1f, 0.35f));
			s.Insert(1.9f, MatchPanel10[num2].DOFade(1f, 0.35f));
			s.Insert(2f, MatchPanel11[num2].DOFade(1f, 0.35f));
			s.Insert(2.1f, MatchPanel12[num2].DOFade(1f, 0.35f));
			s.Insert(2.2f, MatchPanel13[num2].DOFade(1f, 0.35f));
			s.Insert(2.3f, MatchPanel14[num2].DOFade(1f, 0.35f));
			s.Insert(2.4f, MatchPanel15[num2].DOFade(1f, 0.35f));
			s.Insert(2.5f, MatchPanel16[num2].DOFade(1f, 0.35f));
			s.Insert(2.6f, MatchPanel17[num2].DOFade(1f, 0.35f));
			s.Insert(2.7f, MatchPanel18[num2].DOFade(1f, 0.35f));
			num2 += 2;
		}
		for (int j = 0; j < 2; j++)
		{
			s.Insert(0.85f, MatchText1[j].DOFade(1f, 0.35f));
			s.Insert(0.95f, MatchText2[j].DOFade(1f, 0.35f));
			s.Insert(1.05f, MatchText3[j].DOFade(1f, 0.35f));
			s.Insert(1.15f, MatchText4[j].DOFade(1f, 0.35f));
			s.Insert(1.25f, MatchText5[j].DOFade(1f, 0.35f));
			s.Insert(1.35f, MatchText6[j].DOFade(1f, 0.35f));
			s.Insert(1.45f, MatchText7[j].DOFade(1f, 0.35f));
			s.Insert(1.55f, MatchText8[j].DOFade(1f, 0.35f));
			s.Insert(1.65f, MatchText9[j].DOFade(1f, 0.35f));
			s.Insert(1.75f, MatchText10[j].DOFade(1f, 0.35f));
			s.Insert(1.85f, MatchText11[j].DOFade(1f, 0.35f));
			s.Insert(1.95f, MatchText12[j].DOFade(1f, 0.35f));
			s.Insert(2.05f, MatchText13[j].DOFade(1f, 0.35f));
			s.Insert(2.15f, MatchText14[j].DOFade(1f, 0.35f));
			s.Insert(2.25f, MatchText15[j].DOFade(1f, 0.35f));
			s.Insert(2.35f, MatchText16[j].DOFade(1f, 0.35f));
			s.Insert(2.45f, MatchText17[j].DOFade(1f, 0.35f));
			s.Insert(2.55f, MatchText18[j].DOFade(1f, 0.35f));
		}
		for (int k = 2; k < 4; k++)
		{
			s.Insert(0.65f, MatchText1[k].DOFade(1f, 0.35f));
			s.Insert(0.75f, MatchText2[k].DOFade(1f, 0.35f));
			s.Insert(0.85f, MatchText3[k].DOFade(1f, 0.35f));
			s.Insert(0.95f, MatchText4[k].DOFade(1f, 0.35f));
			s.Insert(1.05f, MatchText5[k].DOFade(1f, 0.35f));
			s.Insert(1.15f, MatchText6[k].DOFade(1f, 0.35f));
			s.Insert(1.25f, MatchText7[k].DOFade(1f, 0.35f));
			s.Insert(1.35f, MatchText8[k].DOFade(1f, 0.35f));
			s.Insert(1.45f, MatchText9[k].DOFade(1f, 0.35f));
			s.Insert(1.55f, MatchText10[k].DOFade(1f, 0.35f));
			s.Insert(1.65f, MatchText11[k].DOFade(1f, 0.35f));
			s.Insert(1.75f, MatchText12[k].DOFade(1f, 0.35f));
			s.Insert(1.85f, MatchText13[k].DOFade(1f, 0.35f));
			s.Insert(1.95f, MatchText14[k].DOFade(1f, 0.35f));
			s.Insert(2.05f, MatchText15[k].DOFade(1f, 0.35f));
			s.Insert(2.15f, MatchText16[k].DOFade(1f, 0.35f));
			s.Insert(2.25f, MatchText17[k].DOFade(1f, 0.35f));
			s.Insert(2.35f, MatchText18[k].DOFade(1f, 0.35f));
		}
		s.Insert(0.5f, MatchText1[4].DOFade(1f, 0.35f));
		s.Insert(0.6f, MatchText2[4].DOFade(1f, 0.35f));
		s.Insert(0.7f, MatchText3[4].DOFade(1f, 0.35f));
		s.Insert(0.8f, MatchText4[4].DOFade(1f, 0.35f));
		s.Insert(0.9f, MatchText5[4].DOFade(1f, 0.35f));
		s.Insert(0.95f, MatchText6[4].DOFade(1f, 0.35f));
		s.Insert(1f, MatchText7[4].DOFade(1f, 0.35f));
		s.Insert(1.1f, MatchText8[4].DOFade(1f, 0.35f));
		s.Insert(1.2f, MatchText9[4].DOFade(1f, 0.35f));
		s.Insert(1.3f, MatchText10[4].DOFade(1f, 0.35f));
		s.Insert(1.4f, MatchText11[4].DOFade(1f, 0.35f));
		s.Insert(1.5f, MatchText12[4].DOFade(1f, 0.35f));
		s.Insert(1.6f, MatchText13[4].DOFade(1f, 0.35f));
		s.Insert(1.7f, MatchText14[4].DOFade(1f, 0.35f));
		s.Insert(1.8f, MatchText15[4].DOFade(1f, 0.35f));
		s.Insert(1.9f, MatchText16[4].DOFade(1f, 0.35f));
		s.Insert(2f, MatchText17[4].DOFade(1f, 0.35f));
		s.Insert(2.1f, MatchText18[4].DOFade(1f, 0.35f));
	}
}
