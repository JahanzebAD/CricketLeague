using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NplKnockoutTWOTransistionPanel : Singleton<NplKnockoutTWOTransistionPanel>
{
	public Image MidPanel;

	public Text HeadingText;

	public Image BackBtn;

	public Image BackBtnImage;

	public Image OKButton;

	public Image OkImage;

	public Image[] Panel1;

	public Image[] Panel2;

	public Image[] Panel3;

	public Image[] Panel4;

	public Text[] PanelText1;

	public Text[] PanelText2;

	public Text[] PanelText3;

	public Text[] PanelText4;

	public Image Cup;

	public Image[] Connections;

	public void ResetTransistion()
	{
		MidPanel.DOFade(0f, 0f);
		HeadingText.DOFade(0f, 0f);
		BackBtn.DOFade(0f, 0f);
		BackBtnImage.DOFade(0f, 0f);
		OkImage.DOFade(0f, 0f);
		OKButton.DOFade(0f, 0f);
		for (int i = 0; i < Panel1.Length; i++)
		{
			Panel1[i].DOFade(0f, 0f);
			Panel2[i].DOFade(0f, 0f);
			Panel3[i].DOFade(0f, 0f);
			Panel4[i].DOFade(0f, 0f);
		}
		for (int j = 0; j < PanelText1.Length; j++)
		{
			PanelText1[j].DOFade(0f, 0f);
			PanelText2[j].DOFade(0f, 0f);
			PanelText3[j].DOFade(0f, 0f);
			PanelText4[j].DOFade(0f, 0f);
		}
		for (int k = 0; k < Connections.Length; k++)
		{
			Connections[k].DOFade(0f, 0f);
		}
		Cup.DOFade(0f, 0f);
		MidPanel.transform.DOScale(new Vector3(0f, 1f, 1f), 0f);
		Cup.transform.localScale = new Vector3(0f, 0f, 0f);
		Panel1[0].transform.localScale = new Vector3(0f, 0f, 0f);
		Panel2[0].transform.localScale = new Vector3(0f, 0f, 0f);
		Panel3[0].transform.localScale = new Vector3(0f, 0f, 0f);
		Panel4[0].transform.localScale = new Vector3(0f, 0f, 0f);
		Panel1[1].transform.localPosition = new Vector3(30f, 5f, 0f);
		Panel1[2].transform.localPosition = new Vector3(-30f, 5f, 0f);
		Panel2[1].transform.localPosition = new Vector3(30f, 5f, 0f);
		Panel2[2].transform.localPosition = new Vector3(-30f, 5f, 0f);
		Panel3[1].transform.localPosition = new Vector3(30f, 5f, 0f);
		Panel3[2].transform.localPosition = new Vector3(-30f, 5f, 0f);
		Panel4[1].transform.localPosition = new Vector3(30f, 5f, 0f);
		Panel4[2].transform.localPosition = new Vector3(-30f, 5f, 0f);
	}

	public void PanelTransistion()
	{
		ResetTransistion();
		Sequence s = DOTween.Sequence();
		s.Insert(0f, MidPanel.DOFade(1f, 0f));
		s.Append(MidPanel.transform.DOScale(new Vector3(1f, 1f, 1f), 0.32f));
		s.Insert(0f, HeadingText.DOFade(1f, 0.32f));
		s.Insert(0.6f, BackBtn.DOFade(1f, 0.36f));
		s.Insert(0.6f, BackBtnImage.DOFade(1f, 0.36f));
		s.Insert(0.6f, OkImage.DOFade(1f, 0.36f));
		s.Insert(0.6f, OKButton.DOFade(1f, 0.36f));
		s.Insert(0.32f, Panel1[0].DOFade(1f, 0.24f));
		s.Insert(0.32f, Panel2[0].DOFade(1f, 0.24f));
		s.Insert(0.32f, Panel3[0].DOFade(1f, 0.24f));
		s.Insert(0.32f, Panel4[0].DOFade(1f, 0.24f));
		s.Insert(0.2f, Panel1[1].DOFade(1f, 0.24f));
		s.Insert(0.2f, Panel1[2].DOFade(1f, 0.24f));
		s.Insert(0.2f, Panel2[1].DOFade(1f, 0.24f));
		s.Insert(0.2f, Panel2[2].DOFade(1f, 0.24f));
		s.Insert(0.2f, Panel3[1].DOFade(1f, 0.24f));
		s.Insert(0.2f, Panel3[2].DOFade(1f, 0.24f));
		s.Insert(0.2f, Panel4[1].DOFade(1f, 0.24f));
		s.Insert(0.2f, Panel4[2].DOFade(1f, 0.24f));
		s.Insert(0.48f, Panel1[1].transform.DOLocalMove(new Vector3(50f, 5f, 0f), 0.32f, snapping: true));
		s.Insert(0.48f, Panel1[2].transform.DOLocalMove(new Vector3(-50f, 5f, 0f), 0.32f, snapping: true));
		s.Insert(0.48f, Panel2[1].transform.DOLocalMove(new Vector3(50f, 5f, 0f), 0.32f, snapping: true));
		s.Insert(0.48f, Panel2[2].transform.DOLocalMove(new Vector3(-50f, 5f, 0f), 0.32f, snapping: true));
		s.Insert(0.48f, Panel3[1].transform.DOLocalMove(new Vector3(50f, 5f, 0f), 0.32f, snapping: true));
		s.Insert(0.48f, Panel3[2].transform.DOLocalMove(new Vector3(-50f, 5f, 0f), 0.32f, snapping: true));
		s.Insert(0.48f, Panel4[1].transform.DOLocalMove(new Vector3(50f, 5f, 0f), 0.32f, snapping: true));
		s.Insert(0.48f, Panel4[2].transform.DOLocalMove(new Vector3(-50f, 5f, 0f), 0.32f, snapping: true));
		for (int i = 0; i < PanelText1.Length; i++)
		{
			s.Insert(0.2f, PanelText1[i].DOFade(1f, 0.24f));
			s.Insert(0.2f, PanelText2[i].DOFade(1f, 0.24f));
			s.Insert(0.2f, PanelText3[i].DOFade(1f, 0.24f));
			s.Insert(0.2f, PanelText4[i].DOFade(1f, 0.24f));
		}
		for (int j = 0; j < Connections.Length; j++)
		{
			s.Insert(0.32f, Connections[j].DOFade(1f, 0.24f));
		}
		s.Insert(0.2f, Cup.DOFade(1f, 0.4f));
		s.Insert(0.2f, Cup.transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.4f));
		s.Insert(0.6f, Cup.transform.DOScale(new Vector3(1f, 1f, 1f), 0.16f));
		s.Insert(0.2f, Panel1[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		s.Insert(0.2f, Panel2[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		s.Insert(0.2f, Panel3[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		s.Insert(0.2f, Panel4[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		Invoke("SetSelectedImage", 0.9f);
	}

	public void SetSelectedImage()
	{
		Singleton<NPLIndiaPlayOff>.instance.selectedFlag.gameObject.SetActive(value: true);
		Singleton<NPLIndiaPlayOff>.instance.PlaceSelectedImage();
	}
}
