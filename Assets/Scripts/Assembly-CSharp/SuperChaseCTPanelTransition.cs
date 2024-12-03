using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SuperChaseCTPanelTransition : Singleton<SuperChaseCTPanelTransition>
{
	public Transform Content;

	public Image[] MatchPanel1;

	public Image[] MatchPanel2;

	public Image[] MatchPanel3;

	public Image[] MatchPanel4;

	public Image[] MatchPanel5;

	public Image[] MatchPanel6;

	public Text[] MatchText1;

	public Text[] MatchText2;

	public Text[] MatchText3;

	public Text[] MatchText4;

	public Text[] MatchText5;

	public Text[] MatchText6;

	public Image[] SubMatchPanel1;

	public Image[] SubMatchPanel2;

	public Image[] SubMatchPanel3;

	public Image[] SubMatchPanel4;

	public Image[] SubMatchPanel5;

	public Text[] SubMatchText1;

	public Text[] SubMatchText2;

	public Text[] SubMatchText3;

	public Text[] SubMatchText4;

	public Text[] SubMatchText5;

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
		}
		for (int j = 0; j < MatchText1.Length; j++)
		{
			MatchText1[j].DOFade(0f, 0f);
			MatchText2[j].DOFade(0f, 0f);
			MatchText3[j].DOFade(0f, 0f);
			MatchText4[j].DOFade(0f, 0f);
			MatchText5[j].DOFade(0f, 0f);
			MatchText6[j].DOFade(0f, 0f);
		}
		MatchPanel1[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel1[9].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel2[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel2[9].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel3[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel3[9].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel4[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel4[9].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel5[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel5[9].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel6[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel6[9].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
	}

	public void panelTransition()
	{
		Content.transform.localPosition = new Vector3(930f, 0f, 0f);
		resetTransition();
		Sequence sequence = DOTween.Sequence();
		sequence.SetUpdate(isIndependentUpdate: true);
		sequence.Append(MatchPanel1[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.25f, MatchPanel1[9].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.1f, MatchPanel2[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.35f, MatchPanel2[9].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.2f, MatchPanel3[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.45f, MatchPanel3[9].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.3f, MatchPanel4[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.55f, MatchPanel4[9].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.4f, MatchPanel5[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.65f, MatchPanel5[9].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.5f, MatchPanel6[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.75f, MatchPanel6[9].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0f, MatchPanel1[0].DOFade(1f, 0f));
		sequence.Insert(0.1f, MatchPanel1[9].DOFade(1f, 0f));
		sequence.Insert(0.65f, MatchText1[1].DOFade(1f, 0.35f));
		sequence.Insert(0.8f, MatchText1[0].DOFade(1f, 0.35f));
		sequence.Insert(1f, MatchPanel1[1].DOFade(1f, 0.35f));
		sequence.Insert(1.1f, MatchPanel1[2].DOFade(1f, 0.35f));
		sequence.Insert(1.2f, MatchPanel1[3].DOFade(1f, 0.35f));
		sequence.Insert(1.3f, MatchPanel1[4].DOFade(1f, 0.35f));
		sequence.Insert(1.4f, MatchPanel1[5].DOFade(1f, 0.35f));
		sequence.Insert(1.5f, MatchPanel1[6].DOFade(1f, 0.35f));
		sequence.Insert(1.8f, MatchPanel1[7].DOFade(1f, 0.35f));
		sequence.Insert(1.8f, MatchPanel1[8].DOFade(1f, 0.35f));
		sequence.Insert(0.1f, MatchPanel2[0].DOFade(1f, 0f));
		sequence.Insert(0.2f, MatchPanel2[9].DOFade(1f, 0f));
		sequence.Insert(0.75f, MatchText2[1].DOFade(1f, 0.35f));
		sequence.Insert(0.9f, MatchText2[0].DOFade(1f, 0.35f));
		sequence.Insert(1.1f, MatchPanel2[1].DOFade(1f, 0.35f));
		sequence.Insert(1.2f, MatchPanel2[2].DOFade(1f, 0.35f));
		sequence.Insert(1.3f, MatchPanel2[3].DOFade(1f, 0.35f));
		sequence.Insert(1.4f, MatchPanel2[4].DOFade(1f, 0.35f));
		sequence.Insert(1.5f, MatchPanel2[5].DOFade(1f, 0.35f));
		sequence.Insert(1.6f, MatchPanel2[6].DOFade(1f, 0.35f));
		sequence.Insert(1.9f, MatchPanel2[7].DOFade(1f, 0.35f));
		sequence.Insert(1.9f, MatchPanel2[8].DOFade(1f, 0.35f));
		sequence.Insert(0.2f, MatchPanel3[0].DOFade(1f, 0f));
		sequence.Insert(0.3f, MatchPanel3[9].DOFade(1f, 0f));
		sequence.Insert(0.85f, MatchText3[1].DOFade(1f, 0.35f));
		sequence.Insert(1f, MatchText3[0].DOFade(1f, 0.35f));
		sequence.Insert(1.2f, MatchPanel3[1].DOFade(1f, 0.35f));
		sequence.Insert(1.3f, MatchPanel3[2].DOFade(1f, 0.35f));
		sequence.Insert(1.4f, MatchPanel3[3].DOFade(1f, 0.35f));
		sequence.Insert(1.5f, MatchPanel3[4].DOFade(1f, 0.35f));
		sequence.Insert(1.6f, MatchPanel3[5].DOFade(1f, 0.35f));
		sequence.Insert(1.7f, MatchPanel3[6].DOFade(1f, 0.35f));
		sequence.Insert(2f, MatchPanel3[7].DOFade(1f, 0.35f));
		sequence.Insert(2f, MatchPanel3[8].DOFade(1f, 0.35f));
		sequence.Insert(0.3f, MatchPanel4[0].DOFade(1f, 0f));
		sequence.Insert(0.4f, MatchPanel4[9].DOFade(1f, 0f));
		sequence.Insert(0.95f, MatchText4[1].DOFade(1f, 0.35f));
		sequence.Insert(1.1f, MatchText4[0].DOFade(1f, 0.35f));
		sequence.Insert(1.3f, MatchPanel4[1].DOFade(1f, 0.35f));
		sequence.Insert(1.4f, MatchPanel4[2].DOFade(1f, 0.35f));
		sequence.Insert(1.5f, MatchPanel4[3].DOFade(1f, 0.35f));
		sequence.Insert(1.6f, MatchPanel4[4].DOFade(1f, 0.35f));
		sequence.Insert(1.7f, MatchPanel4[5].DOFade(1f, 0.35f));
		sequence.Insert(1.8f, MatchPanel4[6].DOFade(1f, 0.35f));
		sequence.Insert(2.1f, MatchPanel4[7].DOFade(1f, 0.35f));
		sequence.Insert(2.1f, MatchPanel4[8].DOFade(1f, 0.35f));
		sequence.Insert(0.4f, MatchPanel5[0].DOFade(1f, 0f));
		sequence.Insert(0.5f, MatchPanel5[9].DOFade(1f, 0f));
		sequence.Insert(1.05f, MatchText5[1].DOFade(1f, 0.35f));
		sequence.Insert(1.2f, MatchText5[0].DOFade(1f, 0.35f));
		sequence.Insert(1.4f, MatchPanel5[1].DOFade(1f, 0.35f));
		sequence.Insert(1.5f, MatchPanel5[2].DOFade(1f, 0.35f));
		sequence.Insert(1.6f, MatchPanel5[3].DOFade(1f, 0.35f));
		sequence.Insert(1.7f, MatchPanel5[4].DOFade(1f, 0.35f));
		sequence.Insert(1.8f, MatchPanel5[5].DOFade(1f, 0.35f));
		sequence.Insert(1.9f, MatchPanel5[6].DOFade(1f, 0.35f));
		sequence.Insert(2.2f, MatchPanel5[7].DOFade(1f, 0.35f));
		sequence.Insert(2.2f, MatchPanel5[8].DOFade(1f, 0.35f));
		sequence.Insert(0.5f, MatchPanel6[0].DOFade(1f, 0f));
		sequence.Insert(0.6f, MatchPanel6[9].DOFade(1f, 0f));
		sequence.Insert(1.15f, MatchText6[1].DOFade(1f, 0.35f));
		sequence.Insert(1.3f, MatchText6[0].DOFade(1f, 0.35f));
		sequence.Insert(1.5f, MatchPanel6[1].DOFade(1f, 0.35f));
		sequence.Insert(1.6f, MatchPanel6[2].DOFade(1f, 0.35f));
		sequence.Insert(1.7f, MatchPanel6[3].DOFade(1f, 0.35f));
		sequence.Insert(1.8f, MatchPanel6[4].DOFade(1f, 0.35f));
		sequence.Insert(1.9f, MatchPanel6[5].DOFade(1f, 0.35f));
		sequence.Insert(2f, MatchPanel6[6].DOFade(1f, 0.35f));
		sequence.Insert(2.3f, MatchPanel6[7].DOFade(1f, 0.35f));
		sequence.Insert(2.3f, MatchPanel6[8].DOFade(1f, 0.35f));
	}

	public void SubClassPanelTransition()
	{
		float num = 0f;
		SubClassResetTransition();
		Sequence sequence = DOTween.Sequence();
		sequence.SetUpdate(isIndependentUpdate: true);
		sequence.Append(SubMatchPanel1[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.35f, SubMatchPanel1[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.35f, SubMatchPanel1[6].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.1f, SubMatchPanel2[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.45f, SubMatchPanel2[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.45f, SubMatchPanel2[6].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.2f, SubMatchPanel3[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.55f, SubMatchPanel3[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.55f, SubMatchPanel3[6].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.3f, SubMatchPanel4[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.65f, SubMatchPanel4[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.65f, SubMatchPanel4[6].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.4f, SubMatchPanel5[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.75f, SubMatchPanel5[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(0.75f, SubMatchPanel5[6].transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
		sequence.Insert(num, SubMatchPanel1[0].DOFade(1f, 0f));
		sequence.Insert(0.1f + num, SubMatchPanel1[5].DOFade(1f, 0f));
		sequence.Insert(0.1f + num, SubMatchPanel1[6].DOFade(1f, 0f));
		sequence.Insert(0.45f + num, SubMatchText1[1].DOFade(1f, 0.35f));
		sequence.Insert(0.6f + num, SubMatchText1[0].DOFade(1f, 0.35f));
		sequence.Insert(0.75f + num, SubMatchText1[2].DOFade(1f, 0.35f));
		sequence.Insert(0.45f + num, SubMatchPanel1[1].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel1[2].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel1[3].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel1[4].DOFade(1f, 0.35f));
		num = 0.1f;
		sequence.Insert(num, SubMatchPanel2[0].DOFade(1f, 0f));
		sequence.Insert(0.1f + num, SubMatchPanel2[5].DOFade(1f, 0f));
		sequence.Insert(0.1f + num, SubMatchPanel2[6].DOFade(1f, 0f));
		sequence.Insert(0.45f + num, SubMatchText2[1].DOFade(1f, 0.35f));
		sequence.Insert(0.6f + num, SubMatchText2[0].DOFade(1f, 0.35f));
		sequence.Insert(0.75f + num, SubMatchText2[2].DOFade(1f, 0.35f));
		sequence.Insert(0.45f + num, SubMatchPanel2[1].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel2[2].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel2[3].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel2[4].DOFade(1f, 0.35f));
		num = 0.2f;
		sequence.Insert(num, SubMatchPanel3[0].DOFade(1f, 0f));
		sequence.Insert(0.1f + num, SubMatchPanel3[5].DOFade(1f, 0f));
		sequence.Insert(0.1f + num, SubMatchPanel3[6].DOFade(1f, 0f));
		sequence.Insert(0.45f + num, SubMatchText3[1].DOFade(1f, 0.35f));
		sequence.Insert(0.6f + num, SubMatchText3[0].DOFade(1f, 0.35f));
		sequence.Insert(0.75f + num, SubMatchText3[2].DOFade(1f, 0.35f));
		sequence.Insert(0.45f + num, SubMatchPanel3[1].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel3[2].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel3[3].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel3[4].DOFade(1f, 0.35f));
		num = 0.3f;
		sequence.Insert(num, SubMatchPanel4[0].DOFade(1f, 0f));
		sequence.Insert(0.1f + num, SubMatchPanel4[5].DOFade(1f, 0f));
		sequence.Insert(0.1f + num, SubMatchPanel4[6].DOFade(1f, 0f));
		sequence.Insert(0.45f + num, SubMatchText4[1].DOFade(1f, 0.35f));
		sequence.Insert(0.6f + num, SubMatchText4[0].DOFade(1f, 0.35f));
		sequence.Insert(0.75f + num, SubMatchText4[2].DOFade(1f, 0.35f));
		sequence.Insert(0.45f + num, SubMatchPanel4[1].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel4[2].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel4[3].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel4[4].DOFade(1f, 0.35f));
		num = 0.4f;
		sequence.Insert(num, SubMatchPanel5[0].DOFade(1f, 0f));
		sequence.Insert(0.1f + num, SubMatchPanel5[5].DOFade(1f, 0f));
		sequence.Insert(0.1f + num, SubMatchPanel5[6].DOFade(1f, 0f));
		sequence.Insert(0.55f + num, SubMatchText5[1].DOFade(1f, 0.35f));
		sequence.Insert(0.7f + num, SubMatchText5[0].DOFade(1f, 0.35f));
		sequence.Insert(0.85f + num, SubMatchText5[2].DOFade(1f, 0.35f));
		sequence.Insert(0.55f + num, SubMatchPanel5[1].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel5[2].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel5[3].DOFade(1f, 0.35f));
		sequence.Insert(0.8f + num, SubMatchPanel5[4].DOFade(1f, 0.35f));
	}

	public void SubClassResetTransition()
	{
		for (int i = 0; i < SubMatchPanel1.Length; i++)
		{
			SubMatchPanel1[i].DOFade(0f, 0f);
			SubMatchPanel2[i].DOFade(0f, 0f);
			SubMatchPanel3[i].DOFade(0f, 0f);
			SubMatchPanel4[i].DOFade(0f, 0f);
			SubMatchPanel5[i].DOFade(0f, 0f);
		}
		for (int j = 0; j < SubMatchText1.Length; j++)
		{
			SubMatchText1[j].DOFade(0f, 0f);
			SubMatchText2[j].DOFade(0f, 0f);
			SubMatchText3[j].DOFade(0f, 0f);
			SubMatchText4[j].DOFade(0f, 0f);
			SubMatchText5[j].DOFade(0f, 0f);
		}
		SubMatchPanel1[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		SubMatchPanel1[5].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		SubMatchPanel1[6].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		SubMatchPanel2[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		SubMatchPanel2[5].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		SubMatchPanel2[6].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		SubMatchPanel3[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		SubMatchPanel3[5].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		SubMatchPanel3[6].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		SubMatchPanel4[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		SubMatchPanel4[5].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		SubMatchPanel4[6].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		SubMatchPanel5[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		SubMatchPanel5[5].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		SubMatchPanel5[6].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
	}
}
