using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AboutPageTWOPanelTransistion : Singleton<AboutPageTWOPanelTransistion>
{
	public Image TopPanel;

	public Text Tittle;

	public Image MidPanel;

	public Image InnerPanel1;

	public Image InnerPanel2;

	public Button CloseButton;

	public Image CloseButtonImage;

	public Image Logo;

	public Text LogoText;

	public Text Credits;

	public Text ReleaseNoteTittle;

	public Text ReleaseNoteContent;

	public Text CricketBuddiesText;

	public Text NextwaveText;

	public Text[] SlashANDButtons;

	public Text CreditsContentText;

	public void ResetTransistion()
	{
		TopPanel.DOFade(0f, 0f);
		MidPanel.DOFade(0f, 0f);
		InnerPanel1.DOFade(0f, 0f);
		InnerPanel2.DOFade(0f, 0f);
		Tittle.DOFade(0f, 0f);
		CloseButton.image.DOFade(0f, 0f);
		CloseButtonImage.DOFade(0f, 0f);
		Logo.DOFade(0f, 0f);
		LogoText.DOFade(0f, 0f);
		Credits.DOFade(0f, 0f);
		ReleaseNoteContent.DOFade(0f, 0f);
		ReleaseNoteTittle.DOFade(0f, 0f);
		CricketBuddiesText.DOFade(0f, 0f);
		NextwaveText.DOFade(0f, 0f);
		for (int i = 0; i < SlashANDButtons.Length; i++)
		{
			SlashANDButtons[i].DOFade(0f, 0f);
		}
		TopPanel.transform.DOScaleX(0.25f, 0f);
		MidPanel.transform.DOScaleY(0.25f, 0f);
		InnerPanel1.transform.localScale = new Vector3(1f, 0f, 1f);
		InnerPanel2.transform.localScale = new Vector3(1f, 0f, 1f);
		Logo.transform.localScale = new Vector3(0f, 0f, 1f);
		LogoText.transform.localScale = new Vector3(0f, 0f, 1f);
		PanelTransistion();
	}

	public void PanelTransistion()
	{
		Sequence s = DOTween.Sequence();
		s.Append(TopPanel.transform.DOScaleX(0.75f, 0.2f).SetRelative().SetEase(Ease.InOutQuad));
		s.Insert(0f, TopPanel.DOFade(1f, 0f));
		s.Insert(0.1f, Tittle.DOFade(1f, 0.2f));
		s.Insert(0.1f, MidPanel.DOFade(1f, 0f));
		s.Insert(0.1f, MidPanel.transform.DOScaleY(1f, 0.2f));
		s.Insert(0.1f, CloseButton.image.DOFade(1f, 0.2f));
		s.Insert(0.1f, CloseButtonImage.DOFade(1f, 0.2f));
		s.Insert(0.16f, InnerPanel1.DOFade(1f, 0f));
		s.Insert(0.16f, InnerPanel1.transform.DOScale(new Vector3(1f, 1f, 1f), 0.32f));
		s.Insert(0.16f, InnerPanel2.DOFade(1f, 0f));
		s.Insert(0.16f, InnerPanel2.transform.DOScale(new Vector3(1f, 1f, 1f), 0.32f));
		s.Insert(0.32f, Logo.DOFade(1f, 0.24f));
		s.Insert(0.32f, Logo.transform.DOScale(new Vector3(0.7f, 0.7f, 1f), 0.24f));
		s.Insert(0.32f, LogoText.DOFade(1f, 0.24f));
		s.Insert(0.32f, LogoText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		s.Insert(0.36f, Credits.DOFade(1f, 0.32f));
		s.Insert(0.36f, ReleaseNoteContent.DOFade(1f, 0.32f));
		s.Insert(0.36f, ReleaseNoteTittle.DOFade(1f, 0.32f));
		s.Insert(0.36f, CricketBuddiesText.DOFade(1f, 0.32f));
		s.Insert(0.36f, NextwaveText.DOFade(1f, 0.32f));
		for (int i = 0; i < SlashANDButtons.Length; i++)
		{
			s.Insert(0.36f, SlashANDButtons[i].DOFade(1f, 0.32f));
		}
	}
}
