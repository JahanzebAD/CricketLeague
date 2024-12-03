using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BowlingScoreBoardPanelTransition : Singleton<BowlingScoreBoardPanelTransition>
{
	public Image TopPanel;

	public Image MidPanel;

	public Text HeadingText;

	public Text[] Header;

	public Image[] TeamsPanelImage;

	public Button CloseButton;

	public Image CloseButtonImage;

	public Image OKButton;

	public Image OKButtonImage;

	public Image MainSubTotalImage1;

	public Image MainSubTotalImage2;

	public Image Team2Box;

	public Image Team1Flag;

	private TweenCallback fadetext;

	private void Start()
	{
		fadetext = TeamTextFade;
	}

	public void resetTransition()
	{
		TopPanel.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		MidPanel.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		HeadingText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		CloseButton.image.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		CloseButtonImage.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		OKButton.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		OKButtonImage.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		Team1Flag.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		for (int i = 0; i < Header.Length; i++)
		{
			Header[i].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int j = 0; j < TeamsPanelImage.Length; j++)
		{
			TeamsPanelImage[j].transform.DOScale(new Vector3(0f, 0f, 0f), 0f).SetUpdate(isIndependentUpdate: true);
		}
		TopPanel.transform.DOScale(new Vector3(0f, 0f, 0f), 0f).SetUpdate(isIndependentUpdate: true);
		MidPanel.transform.DOScale(new Vector3(0f, 0f, 0f), 0f).SetUpdate(isIndependentUpdate: true);
		Team2Box.transform.DOScale(new Vector3(0f, 0f, 0f), 0f).SetUpdate(isIndependentUpdate: true);
		MainSubTotalImage1.transform.DOScale(new Vector3(0f, 0f, 0f), 0f).SetUpdate(isIndependentUpdate: true);
		MainSubTotalImage2.transform.DOScale(new Vector3(0f, 0f, 0f), 0f).SetUpdate(isIndependentUpdate: true);
	}

	public void PanelTransition()
	{
		resetTransition();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(TopPanel.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetRelative().SetEase(Ease.InOutQuad));
		sequence.Insert(0f, TopPanel.DOFade(1f, 0f));
		sequence.Insert(0.2f, HeadingText.DOFade(1f, 0.4f));
		sequence.Insert(0.3f, CloseButton.image.DOFade(1f, 0.2f));
		sequence.Insert(0.3f, CloseButtonImage.DOFade(1f, 0.2f));
		sequence.Insert(0.3f, OKButton.DOFade(1f, 0.2f));
		sequence.Insert(0.3f, OKButtonImage.DOFade(1f, 0.2f));
		sequence.Insert(0.2f, MidPanel.DOFade(1f, 0f));
		sequence.Insert(0.2f, Team1Flag.DOFade(1f, 0f));
		sequence.Insert(0.2f, MidPanel.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.InsertCallback(0.2f, fadetext);
		sequence.Insert(0.2f, TeamsPanelImage[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		sequence.Insert(0.24f, TeamsPanelImage[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		sequence.Insert(0.28f, TeamsPanelImage[2].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		sequence.Insert(0.32f, TeamsPanelImage[3].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		sequence.Insert(0.36f, TeamsPanelImage[4].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		sequence.Insert(0.4f, TeamsPanelImage[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		sequence.Insert(0.44f, TeamsPanelImage[6].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		sequence.Insert(0.48f, TeamsPanelImage[7].transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		sequence.Insert(0.6f, MainSubTotalImage1.transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		sequence.Insert(0.6f, MainSubTotalImage2.transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		sequence.Insert(0.6f, Team2Box.transform.DOScale(new Vector3(1f, 1f, 1f), 0.24f));
		sequence.SetUpdate(isIndependentUpdate: true).SetLoops(1);
	}

	public void TeamTextFade()
	{
		for (int i = 0; i < Header.Length; i++)
		{
			Header[i].DOFade(1f, 0.2f).SetUpdate(isIndependentUpdate: true);
		}
	}
}
