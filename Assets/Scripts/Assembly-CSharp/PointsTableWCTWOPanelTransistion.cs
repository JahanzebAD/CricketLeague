using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PointsTableWCTWOPanelTransistion : Singleton<PointsTableWCTWOPanelTransistion>
{
	public Image TopPanel;

	public Image MidPanel;

	public Text HeadingText;

	public Text[] Header;

	public Image[] TeamsPanelImage;

	public Button CloseButton;

	public Image CloseButtonImage;

	public void ResetTransistion()
	{
		TopPanel.DOFade(0f, 0f);
		MidPanel.DOFade(0f, 0f);
		HeadingText.DOFade(0f, 0f);
		CloseButton.image.DOFade(0f, 0f);
		CloseButtonImage.DOFade(0f, 0f);
		for (int i = 0; i < Header.Length; i++)
		{
			Header[i].DOFade(0f, 0f);
		}
		for (int j = 0; j < TeamsPanelImage.Length; j++)
		{
			TeamsPanelImage[j].transform.DOScale(new Vector3(0f, 0f, 0f), 0f);
		}
		TopPanel.transform.DOScaleX(0.25f, 0f);
		MidPanel.transform.DOScaleY(0.25f, 0f);
		PanelTransistion();
	}

	public void PanelTransistion()
	{
		Sequence s = DOTween.Sequence();
		s.Append(TopPanel.transform.DOScaleX(0.75f, 0.3f).SetRelative().SetEase(Ease.InOutQuad));
		s.Insert(0f, TopPanel.DOFade(1f, 0f));
		s.Insert(0.2f, HeadingText.DOFade(1f, 0.4f));
		s.Insert(0.3f, CloseButton.image.DOFade(1f, 0.2f));
		s.Insert(0.3f, CloseButtonImage.DOFade(1f, 0.2f));
		s.Insert(0.2f, MidPanel.DOFade(1f, 0f));
		s.Insert(0.2f, MidPanel.transform.DOScaleY(1f, 0.2f));
		Invoke("TeamTextFade", 0.2f);
		s.Insert(0.2f, TeamsPanelImage[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		s.Insert(0.24f, TeamsPanelImage[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		s.Insert(0.28f, TeamsPanelImage[2].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		s.Insert(0.32f, TeamsPanelImage[3].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		s.Insert(0.36f, TeamsPanelImage[4].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		s.Insert(0.4f, TeamsPanelImage[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		s.Insert(0.44f, TeamsPanelImage[6].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		s.Insert(0.48f, TeamsPanelImage[7].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
	}

	public void TeamTextFade()
	{
		for (int i = 0; i < Header.Length; i++)
		{
			Header[i].DOFade(1f, 0.2f);
		}
	}
}
