using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPageTWOPanelTransition : Singleton<SettingsPageTWOPanelTransition>
{
	public Transform settingPanel;

	public Transform[] buttonBG;

	public Transform[] selectionImage;

	public Transform[] sliders;

	public Text title;

	public Text bgMusic;

	public Text TutorialText;

	public Text sound;

	public float fadeTime;

	public void panelTransition()
	{
		resetTransition();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(settingPanel.DOScale(Vector3.one, 0.3f));
		sequence.Insert(0.2f, title.DOFade(1f, fadeTime));
		sequence.Insert(0.2f, bgMusic.DOFade(1f, fadeTime));
		sequence.Insert(0.2f, sound.DOFade(1f, fadeTime));
		sequence.Insert(0.2f, TutorialText.DOFade(1f, fadeTime));
		sequence.Insert(0.4f, buttonBG[0].DOScaleX(1f, 0.3f));
		sequence.Insert(0.4f, buttonBG[1].DOScaleX(1f, 0.3f));
		sequence.Insert(0.4f, buttonBG[2].DOScaleX(1f, 0.3f));
		sequence.Insert(0.4f, sliders[0].DOScaleX(1f, 0.3f));
		sequence.Insert(0.4f, sliders[1].DOScaleX(1f, 0.3f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}

	public void resetTransition()
	{
		settingPanel.localScale = new Vector3(0f, 0f, 0f);
		title.DOFade(0f, 0f);
		bgMusic.DOFade(0f, 0f);
		sound.DOFade(0f, 0f);
		buttonBG[0].localScale = new Vector3(0f, 1f, 1f);
		buttonBG[1].localScale = new Vector3(0f, 1f, 1f);
		buttonBG[2].localScale = new Vector3(0f, 1f, 1f);
		sliders[0].localScale = new Vector3(0f, 1f, 1f);
		sliders[1].localScale = new Vector3(0f, 1f, 1f);
	}
}
