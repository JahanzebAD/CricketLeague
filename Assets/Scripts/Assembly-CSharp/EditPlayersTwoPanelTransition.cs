using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EditPlayersTwoPanelTransition : Singleton<EditPlayersTwoPanelTransition>
{
	public Image TopPanel;

	public Text HeadingText;

	public Button SaveButton;

	public Button CloseButton;

	public Image CloseButtonImage;

	public Image MidPanel;

	public Image PlayerNameBoxBg;

	public Image playerNameBox;

	public Text PlayerNameSyntax;

	public Text PlayerName2;

	public Image CaptainToggleBg;

	public Text CaptainToggleText;

	public Image WKToggleBg;

	public Text WKToggleText;

	public Text BatsmanHeaderText;

	public Image BatsmanLeftToggleBg;

	public Text BatsmanLeftText;

	public Image BatsmanRightToggleBg;

	public Text BatsmanRightText;

	public Text BowlerTypeHeaderText;

	public Image FastToggleBg;

	public Text FastText;

	public Image OffSpinToggleBg;

	public Text OffSpinText;

	public Image LegSpinToggleBg;

	public Text LegSpinText;

	public Image NoneToggleBg;

	public Text NoneText;

	public Text BowlerHeaderText;

	public Image BowlerLeftToggleBg;

	public Text BowlerLeftText;

	public Image BowlerRightToggleBg;

	public Text BowlerRightText;

	public Image myTeamFlag;

	public Image[] Checkmark;

	public void ResetTransistion()
	{
		PlayerName2.DOFade(0f, 0f);
		TopPanel.DOFade(0f, 0f);
		HeadingText.DOFade(0f, 0f);
		SaveButton.image.DOFade(0f, 0f);
		CloseButton.image.DOFade(0f, 0f);
		CloseButtonImage.DOFade(0f, 0f);
		MidPanel.DOFade(0f, 0f);
		PlayerNameBoxBg.DOFade(0f, 0f);
		PlayerNameSyntax.DOFade(0f, 0f);
		myTeamFlag.DOFade(0f, 0f);
		CaptainToggleBg.DOFade(0f, 0f);
		CaptainToggleText.DOFade(0f, 0f);
		WKToggleBg.DOFade(0f, 0f);
		WKToggleText.DOFade(0f, 0f);
		BatsmanHeaderText.DOFade(0f, 0f);
		BatsmanLeftToggleBg.DOFade(0f, 0f);
		BatsmanLeftText.DOFade(0f, 0f);
		BatsmanRightToggleBg.DOFade(0f, 0f);
		BatsmanRightText.DOFade(0f, 0f);
		BowlerTypeHeaderText.DOFade(0f, 0f);
		FastToggleBg.DOFade(0f, 0f);
		FastText.DOFade(0f, 0f);
		OffSpinToggleBg.DOFade(0f, 0f);
		OffSpinText.DOFade(0f, 0f);
		LegSpinToggleBg.DOFade(0f, 0f);
		LegSpinText.DOFade(0f, 0f);
		NoneToggleBg.DOFade(0f, 0f);
		NoneText.DOFade(0f, 0f);
		BowlerHeaderText.DOFade(0f, 0f);
		BowlerLeftToggleBg.DOFade(0f, 0f);
		BowlerLeftText.DOFade(0f, 0f);
		BowlerRightToggleBg.DOFade(0f, 0f);
		BowlerRightText.DOFade(0f, 0f);
		for (int i = 0; i < Checkmark.Length; i++)
		{
			Checkmark[i].DOFade(0f, 0f);
		}
		TopPanel.transform.DOScaleX(0f, 0f);
		MidPanel.transform.DOScaleY(0f, 0f);
		PlayerNameBoxBg.transform.DOScaleX(0f, 0f);
		PlayerName2.transform.localPosition = new Vector3(75f, 0f, 0f);
		CaptainToggleBg.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		CaptainToggleText.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		WKToggleBg.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		WKToggleText.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		BatsmanLeftText.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		BatsmanLeftToggleBg.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		BatsmanRightText.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		BatsmanRightToggleBg.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		FastText.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		FastToggleBg.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		OffSpinText.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		OffSpinToggleBg.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		LegSpinText.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		LegSpinToggleBg.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		NoneText.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		NoneToggleBg.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		BowlerLeftText.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		BowlerLeftToggleBg.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		BowlerRightText.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
		BowlerRightToggleBg.transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 0f);
	}

	public void PanelTransistion()
	{
		Sequence s = DOTween.Sequence();
		s.Insert(0f, TopPanel.DOFade(1f, 0f));
		s.Append(TopPanel.transform.DOScaleX(1f, 0.3f).SetRelative().SetEase(Ease.InOutQuad));
		s.Insert(0f, MidPanel.DOFade(1f, 0f));
		s.Insert(0.2f, MidPanel.transform.DOScaleY(1f, 0.2f).SetRelative().SetEase(Ease.InOutQuad));
		s.Insert(0.3f, SaveButton.image.DOFade(1f, 0.2f));
		s.Insert(0.3f, CloseButton.image.DOFade(1f, 0.2f));
		s.Insert(0.3f, CloseButtonImage.DOFade(1f, 0.2f));
		s.Insert(0.3f, HeadingText.DOFade(1f, 0.4f));
		s.Insert(0.2f, PlayerNameSyntax.DOFade(1f, 0.3f));
		s.Insert(0.4f, myTeamFlag.DOFade(1f, 0.2f));
		s.Insert(0.3f, PlayerNameBoxBg.DOFade(1f, 0f));
		s.Insert(0.3f, PlayerNameBoxBg.transform.DOScaleX(1f, 0.3f).SetRelative().SetEase(Ease.InOutQuad));
		s.Insert(0.3f, PlayerName2.DOFade(1f, 0f));
		s.Insert(0.3f, PlayerName2.transform.DOLocalMove(new Vector3(5f, 0f, 0f), 0.3f));
		s.Insert(0.2f, Checkmark[0].DOFade(1f, 0f));
		s.Insert(0.2f, CaptainToggleBg.DOFade(1f, 0f));
		s.Insert(0.2f, CaptainToggleText.DOFade(1f, 0f));
		s.Insert(0.2f, CaptainToggleBg.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
		s.Insert(0.2f, CaptainToggleText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
		s.Insert(0.3f, Checkmark[1].DOFade(1f, 0f));
		s.Insert(0.3f, WKToggleBg.DOFade(1f, 0f));
		s.Insert(0.3f, WKToggleText.DOFade(1f, 0f));
		s.Insert(0.3f, WKToggleBg.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		s.Insert(0.3f, WKToggleText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		s.Insert(0.3f, BatsmanHeaderText.DOFade(1f, 0.3f));
		s.Insert(0.4f, Checkmark[2].DOFade(1f, 0f));
		s.Insert(0.4f, BatsmanLeftText.DOFade(1f, 0f));
		s.Insert(0.4f, BatsmanLeftToggleBg.DOFade(1f, 0f));
		s.Insert(0.4f, BatsmanLeftText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		s.Insert(0.4f, BatsmanLeftToggleBg.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		s.Insert(0.4f, Checkmark[3].DOFade(1f, 0f));
		s.Insert(0.4f, BatsmanRightText.DOFade(1f, 0f));
		s.Insert(0.4f, BatsmanRightToggleBg.DOFade(1f, 0f));
		s.Insert(0.4f, BatsmanRightText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
		s.Insert(0.4f, BatsmanRightToggleBg.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
		s.Insert(0.4f, BowlerTypeHeaderText.DOFade(1f, 0.2f));
		s.Insert(0.4f, Checkmark[4].DOFade(1f, 0f));
		s.Insert(0.4f, FastText.DOFade(1f, 0f));
		s.Insert(0.4f, FastToggleBg.DOFade(1f, 0f));
		s.Insert(0.4f, FastText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f));
		s.Insert(0.4f, FastToggleBg.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f));
		s.Insert(0.4f, Checkmark[5].DOFade(1f, 0f));
		s.Insert(0.4f, OffSpinText.DOFade(1f, 0f));
		s.Insert(0.4f, OffSpinToggleBg.DOFade(1f, 0f));
		s.Insert(0.4f, OffSpinText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
		s.Insert(0.4f, OffSpinToggleBg.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
		s.Insert(0.5f, Checkmark[6].DOFade(1f, 0f));
		s.Insert(0.5f, LegSpinText.DOFade(1f, 0f));
		s.Insert(0.5f, LegSpinToggleBg.DOFade(1f, 0f));
		s.Insert(0.5f, LegSpinText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		s.Insert(0.5f, LegSpinToggleBg.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		s.Insert(0.5f, Checkmark[7].DOFade(1f, 0f));
		s.Insert(0.5f, NoneText.DOFade(1f, 0f));
		s.Insert(0.5f, NoneToggleBg.DOFade(1f, 0f));
		s.Insert(0.5f, NoneText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
		s.Insert(0.5f, NoneToggleBg.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
		s.Insert(0.6f, BowlerHeaderText.DOFade(1f, 0.2f));
		s.Insert(0.6f, Checkmark[8].DOFade(1f, 0f));
		s.Insert(0.6f, BowlerLeftText.DOFade(1f, 0f));
		s.Insert(0.6f, BowlerLeftToggleBg.DOFade(1f, 0f));
		s.Insert(0.6f, BowlerLeftText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f));
		s.Insert(0.6f, BowlerLeftToggleBg.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f));
		s.Insert(0.6f, Checkmark[9].DOFade(1f, 0f));
		s.Insert(0.6f, BowlerRightText.DOFade(1f, 0f));
		s.Insert(0.6f, BowlerRightToggleBg.DOFade(1f, 0f));
		s.Insert(0.6f, BowlerRightText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
		s.Insert(0.6f, BowlerRightToggleBg.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f));
	}
}
