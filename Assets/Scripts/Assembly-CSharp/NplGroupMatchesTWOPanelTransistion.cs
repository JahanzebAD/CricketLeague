using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NplGroupMatchesTWOPanelTransistion : Singleton<NplGroupMatchesTWOPanelTransistion>
{
	public Transform Content;

	public Image TopPanel;

	public Image MidPanel;

	public Image TeamImage;

	public Text TeamText;

	public Text Title;

	public Image[] MatchPanel1;

	public Image[] MatchPanel2;

	public Image[] MatchPanel3;

	public Image[] MatchPanel4;

	public Image[] MatchPanel5;

	public Image[] MatchPanel6;

	public Image[] MatchPanel7;

	public Image[] MatchPanel8;

	public Image[] MatchPanel9;

	public Text[] MatchText1;

	public Text[] MatchText2;

	public Text[] MatchText3;

	public Text[] MatchText4;

	public Text[] MatchText5;

	public Text[] MatchText6;

	public Text[] MatchText7;

	public Text[] MatchText8;

	public Text[] MatchText9;

	public Image BackBtn;

	public Image BackBtnImage;

	public Image OKButton;

	public Image OkImage;

	public Button PointsTable;

	public Text PointsTableText;

	public Text B_PlayNowText;

	public Text B_Content;

	public void ResetTransistion()
	{
		PointsTable.interactable = false;
		MatchPanel1[3].transform.localPosition = new Vector3(30f, 9.5f, 0f);
		MatchPanel1[4].transform.localPosition = new Vector3(-30f, 9.5f, 0f);
		MatchPanel2[3].transform.localPosition = new Vector3(30f, 9.5f, 0f);
		MatchPanel2[4].transform.localPosition = new Vector3(-30f, 9.5f, 0f);
		MatchPanel3[3].transform.localPosition = new Vector3(30f, 9.5f, 0f);
		MatchPanel3[4].transform.localPosition = new Vector3(-30f, 9.5f, 0f);
		MatchPanel4[3].transform.localPosition = new Vector3(30f, 9.5f, 0f);
		MatchPanel4[4].transform.localPosition = new Vector3(-30f, 9.5f, 0f);
		MatchPanel5[3].transform.localPosition = new Vector3(30f, 9.5f, 0f);
		MatchPanel5[4].transform.localPosition = new Vector3(-30f, 9.5f, 0f);
		TopPanel.DOFade(0f, 0f);
		MidPanel.DOFade(0f, 0f);
		TeamImage.DOFade(0f, 0f);
		TeamText.DOFade(0f, 0f);
		Title.DOFade(0f, 0f);
		for (int i = 0; i < MatchPanel1.Length; i++)
		{
			MatchPanel1[i].DOFade(0f, 0f);
			MatchPanel2[i].DOFade(0f, 0f);
			MatchPanel3[i].DOFade(0f, 0f);
			MatchPanel4[i].DOFade(0f, 0f);
			MatchPanel5[i].DOFade(0f, 0f);
		}
		for (int j = 0; j < MatchText1.Length; j++)
		{
			MatchText1[j].DOFade(0f, 0f);
			MatchText2[j].DOFade(0f, 0f);
			MatchText3[j].DOFade(0f, 0f);
			MatchText4[j].DOFade(0f, 0f);
			MatchText5[j].DOFade(0f, 0f);
		}
		TopPanel.transform.DOScaleX(0f, 0f);
		MidPanel.transform.DOScaleY(0f, 0f);
		MatchPanel1[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel1[1].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		MatchPanel1[2].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		MatchPanel1[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel2[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel2[1].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		MatchPanel2[2].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		MatchPanel2[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel3[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel3[1].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		MatchPanel3[2].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		MatchPanel3[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel4[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel4[1].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		MatchPanel4[2].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		MatchPanel4[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel5[0].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		MatchPanel5[1].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		MatchPanel5[2].transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		MatchPanel5[5].transform.DOScale(new Vector3(1f, 0f, 1f), 0f);
		BackBtn.DOFade(0f, 0f);
		PointsTable.image.DOFade(0f, 0f);
		BackBtnImage.DOFade(0f, 0f);
		PointsTableText.DOFade(0f, 0f);
		B_Content.DOFade(0f, 0f);
		B_PlayNowText.DOFade(0f, 0f);
		OKButton.DOFade(0f, 0f);
		OkImage.DOFade(0f, 0f);
		PanelTransistion();
	}

	public void PanelTransistion()
	{
		Singleton<NPLIndiaLeague>.instance.scrollView.GetComponent<ScrollRect>().enabled = false;
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, TopPanel.DOFade(1f, 0f));
		sequence.Append(TopPanel.transform.DOScaleX(1f, 0.3f).SetRelative().SetEase(Ease.InOutQuad));
		sequence.Insert(0f, MidPanel.DOFade(1f, 0f));
		sequence.Insert(0.2f, MidPanel.transform.DOScaleY(1f, 0.2f).SetRelative().SetEase(Ease.InOutQuad));
		sequence.Insert(0.2f, Title.DOFade(1f, 0.2f));
		sequence.Insert(0.2f, TeamImage.DOFade(1f, 0.2f));
		sequence.Insert(0.2f, TeamText.DOFade(1f, 0.2f));
		for (int i = 0; i < MatchPanel1.Length; i++)
		{
			sequence.Insert(0f, MatchPanel1[i].DOFade(1f, 0f));
			sequence.Insert(0.08f, MatchPanel2[i].DOFade(1f, 0f));
			sequence.Insert(0.16f, MatchPanel3[i].DOFade(1f, 0f));
			sequence.Insert(0.24f, MatchPanel4[i].DOFade(1f, 0f));
			sequence.Insert(0.32f, MatchPanel5[i].DOFade(1f, 0f));
			sequence.Insert(0.4f, MatchPanel6[i].DOFade(1f, 0f));
			sequence.Insert(0.48f, MatchPanel7[i].DOFade(1f, 0f));
			sequence.Insert(0.4f, MatchPanel8[i].DOFade(1f, 0f));
			sequence.Insert(0.48f, MatchPanel9[i].DOFade(1f, 0f));
		}
		sequence.Insert(0.08f, MatchPanel1[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0f, MatchPanel1[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0f, MatchPanel1[2].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.16f, MatchPanel1[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.08f));
		sequence.Insert(0.24f, MatchPanel2[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.16f, MatchPanel2[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.16f, MatchPanel2[2].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.32f, MatchPanel2[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.08f));
		sequence.Insert(0.4f, MatchPanel3[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.32f, MatchPanel3[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.32f, MatchPanel3[2].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.48f, MatchPanel3[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.08f));
		sequence.Insert(0.56f, MatchPanel4[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.48f, MatchPanel4[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.48f, MatchPanel4[2].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.64f, MatchPanel4[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.08f));
		sequence.Insert(0.72f, MatchPanel5[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.64f, MatchPanel5[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.64f, MatchPanel5[2].transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
		sequence.Insert(0.8f, MatchPanel5[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.08f));
		sequence.Insert(0.4f, MatchPanel1[3].transform.DOLocalMove(new Vector3(51f, 9.5f, 0f), 0.36f));
		sequence.Insert(0.4f, MatchPanel1[4].transform.DOLocalMove(new Vector3(-51f, 9.5f, 0f), 0.36f));
		sequence.Insert(0.55f, MatchPanel2[3].transform.DOLocalMove(new Vector3(51f, 9.5f, 0f), 0.36f));
		sequence.Insert(0.55f, MatchPanel2[4].transform.DOLocalMove(new Vector3(-51f, 9.5f, 0f), 0.36f));
		sequence.Insert(0.7f, MatchPanel3[3].transform.DOLocalMove(new Vector3(51f, 9.5f, 0f), 0.36f));
		sequence.Insert(0.7f, MatchPanel3[4].transform.DOLocalMove(new Vector3(-51f, 9.5f, 0f), 0.36f));
		sequence.Insert(0.85f, MatchPanel4[3].transform.DOLocalMove(new Vector3(51f, 9.5f, 0f), 0.36f));
		sequence.Insert(0.85f, MatchPanel4[4].transform.DOLocalMove(new Vector3(-51f, 9.5f, 0f), 0.36f));
		sequence.Insert(1f, MatchPanel5[3].transform.DOLocalMove(new Vector3(51f, 9.5f, 0f), 0.36f));
		sequence.Insert(1f, MatchPanel5[4].transform.DOLocalMove(new Vector3(-51f, 9.5f, 0f), 0.36f));
		for (int j = 0; j < MatchText1.Length; j++)
		{
			sequence.Insert(0.4f, MatchText1[j].DOFade(1f, 0.36f));
			sequence.Insert(0.4f, MatchText2[j].DOFade(1f, 0.36f));
			sequence.Insert(0.4f, MatchText3[j].DOFade(1f, 0.36f));
			sequence.Insert(0.4f, MatchText4[j].DOFade(1f, 0.36f));
			sequence.Insert(0.4f, MatchText5[j].DOFade(1f, 0.36f));
			sequence.Insert(0.4f, MatchText6[j].DOFade(1f, 0.36f));
			sequence.Insert(0.4f, MatchText7[j].DOFade(1f, 0.36f));
			sequence.Insert(0.4f, MatchText8[j].DOFade(1f, 0.36f));
			sequence.Insert(0.4f, MatchText9[j].DOFade(1f, 0.36f));
		}
		sequence.Insert(0.6f, BackBtn.DOFade(1f, 0.36f));
		sequence.Insert(0.6f, PointsTable.image.DOFade(1f, 0.36f));
		sequence.Insert(0.6f, BackBtnImage.DOFade(1f, 0.36f));
		sequence.Insert(0.6f, PointsTableText.DOFade(1f, 0.36f));
		sequence.Insert(0.6f, B_Content.DOFade(1f, 0.36f));
		sequence.Insert(0.6f, B_PlayNowText.DOFade(1f, 0.36f));
		sequence.Insert(0.6f, OKButton.DOFade(1f, 0.36f));
		sequence.Insert(0.6f, OkImage.DOFade(1f, 0.36f));
		sequence.OnComplete(ResetPosition);
	}

	public void ResetPosition()
	{
		Singleton<NPLIndiaLeague>.instance.scrollView.GetComponent<ScrollRect>().enabled = true;
		PointsTable.interactable = true;
	}
}
