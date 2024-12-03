using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class KnockOutPanelTransition : Singleton<KnockOutPanelTransition>
{
	public Transform outerPanel;

	public Transform flagPanel;

	public Image[] leagueFlags;

	public Image[] quaterFinalFlags;

	public Image[] semiFinalFlags;

	public Image[] finalFlags;

	public Transform cupImage;

	public Image[] vsTeamflags;

	public Text[] texts;

	public float fadeTime;

	public void Start()
	{
		leagueFlags = Singleton<FixturesTWO>.instance.leagueTeams;
		quaterFinalFlags = Singleton<FixturesTWO>.instance.quaterFinalTeams;
		semiFinalFlags = Singleton<FixturesTWO>.instance.semiFinalTeams;
		finalFlags = Singleton<FixturesTWO>.instance.finalMatchTeams;
		vsTeamflags = Singleton<FixturesTWO>.instance.sideFlags;
	}

	public void panelTransition()
	{
		resetTransition();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(outerPanel.DOScaleX(1f, 0.35f));
		sequence.Insert(0.1f, flagPanel.DOScaleY(1f, 0.35f));
		for (int i = 0; i < leagueFlags.Length; i++)
		{
			sequence.Insert(0.2f, leagueFlags[i].DOFade(1f, fadeTime));
		}
		for (int j = 0; j < quaterFinalFlags.Length; j++)
		{
			sequence.Insert(0.3f, quaterFinalFlags[j].DOFade(1f, fadeTime));
		}
		for (int k = 0; k < semiFinalFlags.Length; k++)
		{
			sequence.Insert(0.4f, semiFinalFlags[k].DOFade(1f, fadeTime));
		}
		for (int l = 0; l < finalFlags.Length; l++)
		{
			sequence.Insert(0.5f, finalFlags[l].DOFade(1f, fadeTime));
		}
		for (int m = 0; m < vsTeamflags.Length; m++)
		{
			sequence.Insert(0.6f, vsTeamflags[m].DOFade(1f, fadeTime));
		}
		sequence.Insert(0.2f, texts[0].DOFade(1f, fadeTime));
		sequence.Insert(0.3f, texts[1].DOFade(1f, fadeTime));
		sequence.Insert(0.4f, texts[2].DOFade(1f, fadeTime));
		sequence.Insert(0.5f, texts[3].DOFade(1f, fadeTime));
		sequence.Insert(0.6f, texts[4].DOFade(1f, fadeTime));
		sequence.Insert(0.6f, texts[5].DOFade(1f, fadeTime));
		sequence.Insert(0.8f, cupImage.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.2f, 0));
		sequence.SetUpdate(isIndependentUpdate: true);
		sequence.SetLoops(1);
	}

	public void resetTransition()
	{
		outerPanel.localScale = new Vector3(0f, 1f, 1f);
		flagPanel.localScale = new Vector3(1f, 0f, 1f);
		for (int i = 0; i < leagueFlags.Length; i++)
		{
			leagueFlags[i].DOFade(0f, 0f);
		}
		for (int j = 0; j < quaterFinalFlags.Length; j++)
		{
			quaterFinalFlags[j].DOFade(0f, 0f);
		}
		for (int k = 0; k < semiFinalFlags.Length; k++)
		{
			semiFinalFlags[k].DOFade(0f, 0f);
		}
		for (int l = 0; l < finalFlags.Length; l++)
		{
			finalFlags[l].DOFade(0f, 0f);
		}
		for (int m = 0; m < texts.Length; m++)
		{
			texts[m].DOFade(0f, 0f);
		}
		for (int n = 0; n < vsTeamflags.Length; n++)
		{
			vsTeamflags[n].DOFade(0f, 0f);
		}
	}
}
