using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameModeClickAnim : MonoBehaviour
{
	public Image bluePanel;

	public Image redPanel;

	public Image whitePanel;

	public Image cup;

	public Image[] borderLine;

	private Vector3 bluePanelTransform;

	private Vector3 cupTransform;

	private Color redPanelColor;

	private Color bluePanelColor;

	public Text title;

	private int index;

	public GameObject blocker;

	private void Awake()
	{
		bluePanelTransform = bluePanel.transform.localPosition;
		cupTransform = cup.transform.localPosition;
		redPanelColor = redPanel.color;
		bluePanelColor = Color.blue;
	}

	public void SetGameModeIndex(int index)
	{
		this.index = index;
	}

	public void OnClickGameMode()
	{
		blocker.SetActive(value: true);
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, whitePanel.transform.DOScaleX(0f, 0.5f));
		sequence.Insert(0f, redPanel.DOColor(bluePanelColor, 0.5f));
		sequence.Insert(0f, title.DOFade(0f, 0.2f));
		sequence.Insert(0f, bluePanel.transform.DOScaleY(1.44f, 0.45f));
		sequence.Insert(0f, redPanel.transform.DOScaleY(0f, 0.5f));
		sequence.Insert(0f, bluePanel.transform.DOLocalMoveY(0f, 0.5f));
		sequence.Insert(0f, bluePanel.DOColor(redPanelColor, 0.5f));
		sequence.Insert(0f, cup.transform.DOScale(Vector3.one * 1.3f, 0.5f));
		sequence.Insert(0f, cup.transform.DOLocalMoveY(0f, 0.5f));
		sequence.SetLoops(1).SetUpdate(isIndependentUpdate: true);
		sequence.OnComplete(OpenGameMode);
	}

	private void OpenGameMode()
	{
		if (index == 0)
		{
			Singleton<GameModeTWO>.instance.getExhibitionState();
		}
		else if (index == 1)
		{
			Singleton<GameModeTWO>.instance.getTournamentState();
		}
		else if (index == 2)
		{
			Singleton<GameModeTWO>.instance.getNplState();
		}
		else if (index == 3)
		{
			Singleton<GameModeTWO>.instance.getWorldCupState();
		}
		blocker.SetActive(value: false);
		Sequence sequence = DOTween.Sequence();
		sequence.Insert(0f, bluePanel.transform.DOScaleY(1f, 0.1f));
		sequence.Insert(0f, whitePanel.transform.DOScaleX(1f, 0.1f));
		sequence.Insert(0f, redPanel.DOColor(redPanelColor, 0.1f));
		sequence.Insert(0f, redPanel.transform.DOScaleY(1f, 0.1f));
		sequence.Insert(0f, title.DOFade(1f, 0.1f));
		sequence.Insert(0f, cup.transform.DOScale(Vector3.one, 0.1f));
		sequence.Insert(0f, cup.transform.DOLocalMoveY(cupTransform.y, 0.1f));
		sequence.Insert(0f, bluePanel.DOColor(bluePanelColor, 0.1f));
		sequence.Insert(0f, bluePanel.transform.DOLocalMoveY(bluePanelTransform.y, 0.1f));
		sequence.SetLoops(1).SetUpdate(isIndependentUpdate: true);
	}
}
