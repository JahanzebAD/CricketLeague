using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpTWOPanelTransistion : Singleton<HelpTWOPanelTransistion>
{
	public GameObject NormalHelpPanel;

	public GameObject SuperChaseHelpPanel;

	public GameObject SuperChaseHelpPanelContent;

	public Image TopPanel;

	public Text HeadingText;

	public Button CloseButton;

	public Image CloseButtonImage;

	public Sprite YellowCube;

	public Sprite RedCube;

	public Image MidPanel;

	public Image TittleOneBox;

	public Text TittleOneHeader;

	public Image TittleOneButton1;

	public Image TittleOneButtonImage1;

	public Image TittleOnePanel;

	public Text TittleOneText;

	public Image TittleTwoBox;

	public Text TittleTwoHeader;

	public Image TittleTwoButton1;

	public Image TittleTwoButtonImage1;

	public Image TittleTwoPanel;

	public Text TittleTwoText;

	public Image TittleThreeBox;

	public Text TittleThreeHeader;

	public Image TittleThreeButton1;

	public Image TittleThreeButtonImage1;

	public Image TittleThreePanel;

	public Text TittleThreeText;

	public Image TittleFourBox;

	public Text TittleFourHeader;

	public Image TittleFourButton1;

	public Image TittleFourButtonImage1;

	public Image TittleFourPanel;

	public Text TittleFourText;

	public Image TittleFiveBox;

	public Text TittleFiveHeader;

	public Image TittleFiveButton1;

	public Image TittleFiveButtonImage1;

	public Image TittleFivePanel;

	public Text TittleFiveText;

	public Image TittleSixBox;

	public Text TittleSixHeader;

	public Image TittleSixButton1;

	public Image TittleSixButtonImage1;

	public Image TittleSixPanel;

	public Text TittleSixText;

	public Image TittleSevenBox;

	public Text TittleSevenHeader;

	public Image TittleSevenButton1;

	public Image TittleSevenButtonImage1;

	public Image TittleSevenPanel;

	public Text TittleSevenText;

	public Transform SCTopPanel;

	public Transform SCMidPanel;

	public Text topTitle;

	public Text desc;

	public void PanelTransition()
	{
		if (SceneManager.GetActiveScene().name == "Ground")
		{
			if (CONTROLLER.PlayModeSelected == 0)
			{
				Singleton<PauseGameScreen>.instance.midPageGO.SetActive(value: false);
				NormalHelpPanel.SetActive(value: true);
				SuperChaseHelpPanel.SetActive(value: false);
				NormalPanelTransistion();
				return;
			}
			if (CONTROLLER.PlayModeSelected == 4)
			{
				topTitle.text = LocalizationData.instance.getText(11);
				desc.text = LocalizationData.instance.getText(96);
			}
			else if (CONTROLLER.PlayModeSelected == 5)
			{
				topTitle.text = LocalizationData.instance.getText(12);
				desc.text = LocalizationData.instance.getText(101);
			}
			else if (CONTROLLER.PlayModeSelected == 7)
			{
				topTitle.text = LocalizationData.instance.getText(462);
				desc.text = LocalizationData.instance.getText(96);
			}
			else if (CONTROLLER.PlayModeSelected == 3)
			{
				topTitle.text = LocalizationData.instance.getText(13);
				desc.text = LocalizationData.instance.getText(111);
			}
			else if (CONTROLLER.PlayModeSelected == 2)
			{
				if (CONTROLLER.tournamentType == "NPL")
				{
					topTitle.text = LocalizationData.instance.getText(14);
					desc.text = LocalizationData.instance.getText(112);
				}
				else if (CONTROLLER.tournamentType == "PAK")
				{
					topTitle.text = LocalizationData.instance.getText(428);
					desc.text = LocalizationData.instance.getText(112);
				}
				else if (CONTROLLER.tournamentType == "AUS")
				{
					topTitle.text = LocalizationData.instance.getText(429);
					desc.text = LocalizationData.instance.getText(112);
				}
			}
			else if (CONTROLLER.PlayModeSelected == 1)
			{
				topTitle.text = LocalizationData.instance.getText(17);
				desc.text = LocalizationData.instance.getText(113);
			}
			NormalHelpPanel.SetActive(value: false);
			SuperChaseHelpPanel.SetActive(value: true);
			SuperChasePanelTransition();
		}
		else
		{
			NormalHelpPanel.SetActive(value: true);
			SuperChaseHelpPanel.SetActive(value: false);
			NormalPanelTransistion();
		}
	}

	public void ResetNormalPanelTransistion()
	{
		TopPanel.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		HeadingText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		CloseButton.image.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		CloseButtonImage.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		MidPanel.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleOneBox.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleOneHeader.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleOneButtonImage1.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleOnePanel.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleOneText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleTwoBox.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleTwoHeader.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleTwoButtonImage1.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleTwoPanel.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleTwoText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleThreeBox.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleThreeHeader.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleThreeButtonImage1.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleThreePanel.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleThreeText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleFourBox.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleFourHeader.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleFourButtonImage1.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleFourPanel.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleFourText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleFiveBox.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleFiveHeader.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleFiveButtonImage1.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleFivePanel.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleFiveText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleSixBox.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleSixHeader.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleSixButtonImage1.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleSixPanel.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleSixText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleSevenBox.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleSevenHeader.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleSevenButtonImage1.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleSevenPanel.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleSevenText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TopPanel.transform.DOScale(new Vector3(0f, 1f, 1f), 0f).SetUpdate(isIndependentUpdate: true);
		MidPanel.transform.DOScaleY(0.25f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleOneBox.transform.DOScaleX(0.25f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleOnePanel.transform.DOScaleY(0f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleTwoBox.transform.DOScaleX(0.25f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleThreeBox.transform.DOScaleX(0.25f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleFourBox.transform.DOScaleX(0.25f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleFiveBox.transform.DOScaleX(0.25f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleSixBox.transform.DOScaleX(0.25f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleSevenBox.transform.DOScaleX(0.25f, 0f).SetUpdate(isIndependentUpdate: true);
		TittleOnePanel.gameObject.SetActive(value: true);
		TittleTwoPanel.gameObject.SetActive(value: false);
		TittleThreePanel.gameObject.SetActive(value: false);
		TittleFourPanel.gameObject.SetActive(value: false);
		TittleFivePanel.gameObject.SetActive(value: false);
		TittleSixPanel.gameObject.SetActive(value: false);
		TittleSevenPanel.gameObject.SetActive(value: false);
		TittleOneBox.sprite = YellowCube;
		TittleTwoBox.sprite = RedCube;
		TittleThreeBox.sprite = RedCube;
		TittleFourBox.sprite = RedCube;
		TittleFiveBox.sprite = RedCube;
		TittleSixBox.sprite = RedCube;
		TittleSevenBox.sprite = RedCube;
	}

	public void NormalPanelTransistion()
	{
		ResetNormalPanelTransistion();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(TopPanel.transform.DOScale(new Vector3(1f, 0f, 0f), 0f).SetRelative().SetEase(Ease.InOutQuad));
		sequence.Insert(0f, TopPanel.DOFade(1f, 0f));
		sequence.Insert(0.2f, HeadingText.DOFade(1f, 0.4f));
		sequence.Insert(0.3f, CloseButton.image.DOFade(1f, 0.2f));
		sequence.Insert(0.3f, CloseButtonImage.DOFade(1f, 0.2f));
		sequence.Insert(0.2f, MidPanel.DOFade(1f, 0f));
		sequence.Insert(0.2f, MidPanel.transform.DOScaleY(1f, 0.2f));
		sequence.Insert(0.5f, TittleOneBox.DOFade(1f, 0f));
		sequence.Insert(0.5f, TittleOneBox.transform.DOScaleX(1f, 0.3f));
		sequence.Insert(0.5f, TittleOneHeader.DOFade(1f, 0.3f));
		sequence.Insert(0.5f, TittleOneButtonImage1.DOFade(1f, 0f));
		sequence.Insert(0.9f, TittleOnePanel.DOFade(1f, 0.4f));
		sequence.Insert(0.9f, TittleOnePanel.transform.DOScaleY(1f, 0.4f));
		sequence.Insert(1.3f, TittleOneText.DOFade(1f, 0.3f));
		sequence.Insert(0.7f, TittleTwoBox.DOFade(1f, 0f));
		sequence.Insert(0.7f, TittleTwoBox.transform.DOScaleX(1f, 0.3f));
		sequence.Insert(0.8f, TittleTwoHeader.DOFade(1f, 0.4f));
		sequence.Insert(1f, TittleTwoButtonImage1.DOFade(1f, 0.4f));
		sequence.Insert(1.4f, TittleTwoText.DOFade(1f, 0f));
		sequence.Insert(0.9f, TittleThreeBox.DOFade(1f, 0f));
		sequence.Insert(0.9f, TittleThreeBox.transform.DOScaleX(1f, 0.3f));
		sequence.Insert(1f, TittleThreeHeader.DOFade(1f, 0.4f));
		sequence.Insert(1.2f, TittleThreeButtonImage1.DOFade(1f, 0.4f));
		sequence.Insert(1.6f, TittleThreeText.DOFade(1f, 0f));
		sequence.Insert(1.1f, TittleFourBox.DOFade(1f, 0f));
		sequence.Insert(1.1f, TittleFourBox.transform.DOScaleX(1f, 0.3f));
		sequence.Insert(1.2f, TittleFourHeader.DOFade(1f, 0.4f));
		sequence.Insert(1.4f, TittleFourButtonImage1.DOFade(1f, 0.4f));
		sequence.Insert(1.8f, TittleFourText.DOFade(1f, 0f));
		sequence.Insert(1.3f, TittleFiveBox.DOFade(1f, 0f));
		sequence.Insert(1.3f, TittleFiveBox.transform.DOScaleX(1f, 0.3f));
		sequence.Insert(1.2f, TittleFiveHeader.DOFade(1f, 0.4f));
		sequence.Insert(1.4f, TittleFiveButtonImage1.DOFade(1f, 0.4f));
		sequence.Insert(1.8f, TittleFiveText.DOFade(1f, 0f));
		sequence.Insert(1.5f, TittleSixBox.DOFade(1f, 0f));
		sequence.Insert(1.5f, TittleSixBox.transform.DOScaleX(1f, 0.3f));
		sequence.Insert(1.4f, TittleSixHeader.DOFade(1f, 0.4f));
		sequence.Insert(1.6f, TittleSixButtonImage1.DOFade(1f, 0.4f));
		sequence.Insert(2f, TittleSixText.DOFade(1f, 0f));
		sequence.Insert(1.7f, TittleSevenBox.DOFade(1f, 0f));
		sequence.Insert(1.7f, TittleSevenBox.transform.DOScaleX(1f, 0.3f));
		sequence.Insert(1.6f, TittleSevenHeader.DOFade(1f, 0.4f));
		sequence.Insert(1.8f, TittleSevenButtonImage1.DOFade(1f, 0.4f));
		sequence.Insert(2.2f, TittleSevenText.DOFade(1f, 0f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}

	public void ResetSCPanelTransition()
	{
		SCTopPanel.localScale = new Vector3(0f, 1f, 1f);
		SCMidPanel.localScale = new Vector3(1f, 0f, 1f);
	}

	public void SuperChasePanelTransition()
	{
		ResetSCPanelTransition();
		SuperChaseHelpPanelContent.transform.localPosition = new Vector3(0f, -53.5f, 0f);
		Sequence sequence = DOTween.Sequence();
		sequence.Append(SCTopPanel.DOScaleX(1f, 0.4f));
		sequence.Insert(0.2f, SCMidPanel.DOScaleY(1f, 0.4f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}
}
