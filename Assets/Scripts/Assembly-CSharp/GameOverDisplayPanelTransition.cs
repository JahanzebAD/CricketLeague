using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDisplayPanelTransition : Singleton<GameOverDisplayPanelTransition>
{
	public Image[] Panel1;

	public Image[] Panel2;

	public Image[] Panel3;

	public Image[] Panel4;

	public Text[] PanelText1;

	public Text[] PanelText2;

	public Text[] PanelText3;

	public Text[] PanelText4;

	public GameObject[] panel4GameObject;

	private void start()
	{
	}

	public void ResetTransistion()
	{
		for (int i = 1; i < 4; i++)
		{
			Panel1[i].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int j = 0; j < 4; j++)
		{
			PanelText1[j].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		Panel1[0].transform.localScale = new Vector3(0f, 1f, 1f);
	}

	public void PanelTransistion()
	{
		DOTween.Init();
		ResetTransistion();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(Panel1[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.32f));
		sequence.Insert(0.2f, Panel1[1].DOFade(1f, 0f));
		sequence.Insert(0.2f, Panel1[1].transform.DOPunchScale(new Vector3(0.05f, 0.05f, 0.05f), 0.15f, 0));
		sequence.Insert(0.36f, PanelText1[0].DOFade(1f, 0.36f));
		sequence.Insert(0.36f, PanelText1[1].DOFade(1f, 0.36f));
		sequence.Insert(0.5f, Panel1[2].DOFade(1f, 0.2f));
		sequence.Insert(0.5f, Panel1[3].DOFade(1f, 0.2f));
		sequence.Insert(0.5f, PanelText1[2].DOFade(1f, 0.36f));
		sequence.Insert(0.6f, PanelText1[3].DOFade(1f, 0.36f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}

	public void ResetTransistion1()
	{
		for (int i = 1; i < 13; i++)
		{
			Panel2[i].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int j = 0; j < 12; j++)
		{
			PanelText2[j].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		Panel2[0].transform.localScale = new Vector3(0f, 1f, 1f);
		Panel2[1].transform.localScale = new Vector3(0f, 1f, 1f);
		Panel2[5].transform.localScale = new Vector3(0f, 1f, 1f);
		Panel2[10].transform.localScale = new Vector3(0f, 1f, 1f);
	}

	public void PanelTransistion1()
	{
		DOTween.Init();
		ResetTransistion1();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(Panel2[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.32f));
		sequence.Insert(0.2f, PanelText2[0].DOFade(1f, 0.36f));
		sequence.Insert(0.28f, PanelText2[1].DOFade(1f, 0.36f));
		sequence.Insert(0.32f, Panel2[1].DOFade(1f, 0f));
		sequence.Insert(0.36f, Panel2[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.4f, Panel2[2].DOFade(1f, 0.2f));
		sequence.Insert(0.4f, PanelText2[2].DOFade(1f, 0.2f));
		sequence.Insert(0.4f, PanelText2[3].DOFade(1f, 0.2f));
		sequence.Insert(0.48f, Panel2[3].DOFade(1f, 0.2f));
		sequence.Insert(0.48f, PanelText2[4].DOFade(1f, 0.2f));
		sequence.Insert(0.48f, PanelText2[5].DOFade(1f, 0.2f));
		sequence.Insert(0.56f, Panel2[4].DOFade(1f, 0.2f));
		sequence.Insert(0.56f, PanelText2[6].DOFade(1f, 0.2f));
		sequence.Insert(0.56f, PanelText2[7].DOFade(1f, 0.2f));
		sequence.Insert(0.6f, Panel2[5].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.6f, Panel2[5].DOFade(1f, 0f));
		sequence.Insert(0.64f, Panel2[6].DOFade(1f, 0.2f));
		sequence.Insert(0.64f, PanelText2[8].DOFade(1f, 0.2f));
		sequence.Insert(0.64f, PanelText2[9].DOFade(1f, 0.2f));
		sequence.Insert(0.72f, Panel2[7].DOFade(1f, 0.2f));
		sequence.Insert(0.72f, PanelText2[10].DOFade(1f, 0.2f));
		sequence.Insert(0.72f, PanelText2[11].DOFade(1f, 0.2f));
		sequence.Insert(0.76f, Panel2[10].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.76f, Panel2[10].DOFade(1f, 0f));
		sequence.Insert(0.5f, Panel2[8].DOFade(1f, 0.24f));
		sequence.Insert(0.5f, Panel2[9].DOFade(1f, 0.24f));
		sequence.Insert(0.5f, Panel2[11].DOFade(1f, 0.24f));
		sequence.Insert(0.5f, Panel2[12].DOFade(1f, 0.24f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}

	public void ResetTransistion2()
	{
		for (int i = 1; i < 9; i++)
		{
			Panel3[i].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int j = 0; j < 5; j++)
		{
			PanelText3[j].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		Panel3[0].transform.localScale = new Vector3(0f, 1f, 1f);
		Panel3[1].transform.localScale = new Vector3(1f, 0f, 1f);
	}

	public void PanelTransistion2()
	{
		DOTween.Init();
		panel4GameObject[5].transform.localPosition = Vector3.zero;
		ResetTransistion2();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(Panel3[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.32f));
		sequence.Insert(0.2f, PanelText3[0].DOFade(1f, 0.36f));
		sequence.Insert(0.28f, PanelText3[1].DOFade(1f, 0.36f));
		sequence.Insert(0.32f, Panel3[1].DOFade(1f, 0f));
		sequence.Insert(0.32f, Panel3[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.52f, Panel3[2].DOFade(1f, 0.2f));
		sequence.Insert(0.6f, Panel3[3].DOFade(1f, 0.2f));
		sequence.Insert(0.52f, PanelText3[3].DOFade(1f, 0.2f));
		sequence.Insert(0.6f, PanelText3[4].DOFade(1f, 0.2f));
		sequence.Insert(0.7f, PanelText3[2].DOFade(1f, 0.2f));
		sequence.Insert(0.7f, Panel3[4].DOFade(1f, 0.2f));
		sequence.Insert(0.5f, Panel3[5].DOFade(1f, 0.24f));
		sequence.Insert(0.5f, Panel3[6].DOFade(1f, 0.24f));
		sequence.Insert(0.5f, Panel3[7].DOFade(1f, 0.24f));
		sequence.Insert(0.5f, Panel3[8].DOFade(1f, 0.24f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}

	public void ResetTransistion3()
	{
		for (int i = 1; i < 10; i++)
		{
			if (i != 3)
			{
				Panel4[i].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
			}
		}
		for (int j = 0; j < 2; j++)
		{
			PanelText4[j].DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		}
		for (int k = 0; k < 5; k++)
		{
			panel4GameObject[k].transform.localScale = new Vector3(1f, 0f, 1f);
		}
		Panel4[0].transform.localScale = new Vector3(0f, 1f, 1f);
		Panel4[3].transform.localScale = new Vector3(1f, 0f, 1f);
	}

	public void PanelTransistion3()
	{
		DOTween.Init();
		ResetTransistion3();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(Panel4[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.32f));
		sequence.Insert(0.2f, PanelText4[0].DOFade(1f, 0.36f));
		sequence.Insert(0.28f, Panel4[1].DOFade(1f, 0.2f));
		sequence.Insert(0.36f, Panel4[2].DOFade(1f, 0.2f));
		sequence.Insert(0.4f, panel4GameObject[0].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.52f, panel4GameObject[1].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.64f, panel4GameObject[2].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.72f, Panel4[3].transform.DOScale(new Vector3(1f, 1f, 1f), 0.32f));
		sequence.Insert(0.92f, PanelText4[1].DOFade(1f, 0.36f));
		sequence.Insert(1f, Panel4[4].DOFade(1f, 0.2f));
		sequence.Insert(1.08f, Panel4[5].DOFade(1f, 0.2f));
		sequence.Insert(1.12f, panel4GameObject[3].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(1.2f, panel4GameObject[4].transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.5f, Panel4[6].DOFade(1f, 0.24f));
		sequence.Insert(0.5f, Panel4[7].DOFade(1f, 0.24f));
		sequence.Insert(0.5f, Panel4[8].DOFade(1f, 0.24f));
		sequence.Insert(0.5f, Panel4[9].DOFade(1f, 0.24f));
		sequence.SetUpdate(isIndependentUpdate: true);
	}
}
