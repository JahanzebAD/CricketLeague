using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class QuickPlayResultsTWoTransisitonPanel : Singleton<QuickPlayResultsTWoTransisitonPanel>
{
	public Image TopPanel;

	public Image MidPanel;

	public Text HeadingText;

	public Image HeadingImage;

	public Image Flag;

	public Text Text1;

	public Text Text2;

	public Button CloseButton;

	public Text CloseButtonText;

	public Button OKButton;

	public Image OkButtonImage;

	public void ResetTransistion()
	{
		TopPanel.DOFade(0f, 0f);
		MidPanel.DOFade(0f, 0f);
		HeadingText.DOFade(0f, 0f);
		HeadingImage.DOFade(0f, 0f);
		Flag.DOFade(0f, 0f);
		Text1.DOFade(0f, 0f);
		Text2.DOFade(0f, 0f);
		CloseButton.image.DOFade(0f, 0f);
		CloseButtonText.DOFade(0f, 0f);
		OKButton.image.DOFade(0f, 0f);
		OkButtonImage.DOFade(0f, 0f);
		TopPanel.transform.DOScaleX(0.25f, 0f);
		MidPanel.transform.DOScaleY(0f, 0f);
		Flag.transform.localScale = new Vector3(0f, 0f, 0f);
		HeadingImage.transform.localScale = new Vector3(2f, 2f, 2f);
		HeadingText.transform.localScale = new Vector3(0f, 0f, 0f);
		Text1.transform.localScale = new Vector3(2f, 2f, 2f);
		Text2.transform.localScale = new Vector3(0f, 0f, 0f);
		PanelTransistion();
	}

	public void PanelTransistion()
	{
		Sequence s = DOTween.Sequence();
		s.Append(TopPanel.transform.DOScaleX(0.75f, 0.36f).SetRelative().SetEase(Ease.InOutQuad));
		s.Insert(0f, TopPanel.DOFade(1f, 0f));
		s.Insert(0.16f, MidPanel.DOFade(1f, 0f));
		s.Insert(0.16f, MidPanel.transform.DOScaleY(1f, 0.36f));
		s.Insert(0.32f, Flag.DOFade(1f, 0f));
		s.Insert(0.32f, Flag.transform.DOScale(new Vector3(1f, 1f, 1f), 0.32f));
		s.Insert(0.24f, HeadingImage.DOFade(1f, 0f));
		s.Insert(0.24f, HeadingImage.transform.DOScale(new Vector3(1f, 1f, 1f), 0.32f));
		s.Insert(0.28f, HeadingText.DOFade(1f, 0f));
		s.Insert(0.28f, HeadingText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.32f));
		s.Insert(0.36f, Text1.DOFade(1f, 0f));
		s.Insert(0.36f, Text1.transform.DOScale(new Vector3(1f, 1f, 1f), 0.32f));
		s.Insert(0.44f, Text2.DOFade(1f, 0f));
		s.Insert(0.44f, Text2.transform.DOScale(new Vector3(1f, 1f, 1f), 0.32f));
		s.Insert(0.3f, CloseButton.image.DOFade(1f, 0.2f));
		s.Insert(0.3f, CloseButtonText.DOFade(1f, 0.2f));
		s.Insert(0.3f, OKButton.image.DOFade(1f, 0.2f));
		s.Insert(0.3f, OkButtonImage.DOFade(1f, 0.2f));
	}
}
