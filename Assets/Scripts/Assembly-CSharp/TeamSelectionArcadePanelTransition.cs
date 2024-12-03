using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelectionArcadePanelTransition : Singleton<TeamSelectionArcadePanelTransition>
{
	public Button MyTeamLeftButton;

	public Button MyTeamRightButton;

	public Button OppTeamLeftButton;

	public Button OppTeamRightButton;

	public Image MyTeamLeftButtonArrow;

	public Image MyTeamRightButtonArrow;

	public Image OppTeamLeftButtonArrow;

	public Image OppTeamRightButtonArrow;

	public GameObject MyTeamPanel;

	public GameObject OppTeamPanel;

	public void resetTransition()
	{
		MyTeamLeftButton.image.DOFade(0f, 0f);
		MyTeamRightButton.image.DOFade(0f, 0f);
		OppTeamLeftButton.image.DOFade(0f, 0f);
		OppTeamRightButton.image.DOFade(0f, 0f);
		MyTeamLeftButtonArrow.DOFade(0f, 0f);
		MyTeamRightButtonArrow.DOFade(0f, 0f);
		OppTeamLeftButtonArrow.DOFade(0f, 0f);
		OppTeamRightButtonArrow.DOFade(0f, 0f);
		MyTeamPanel.transform.localPosition = new Vector3(-860f, -50f, 0f);
		OppTeamPanel.transform.localPosition = new Vector3(860f, -50f, 0f);
	}

	public void panelTransition()
	{
		resetTransition();
		Sequence s = DOTween.Sequence();
		s.Append(MyTeamPanel.transform.DOLocalMove(new Vector3(-275f, -50f, 0f), 0.5f));
		s.Insert(0f, OppTeamPanel.transform.DOLocalMove(new Vector3(275f, -50f, 0f), 0.5f));
		s.Insert(0.4f, MyTeamPanel.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f, 0));
		s.Insert(0.4f, OppTeamPanel.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.3f, 0));
		s.Insert(0.25f, MyTeamLeftButton.image.DOFade(1f, 0.25f));
		s.Insert(0.25f, MyTeamLeftButtonArrow.DOFade(1f, 0.25f));
		s.Insert(0.25f, MyTeamRightButton.image.DOFade(1f, 0.25f));
		s.Insert(0.25f, MyTeamRightButtonArrow.DOFade(1f, 0.25f));
		s.Insert(0.25f, OppTeamLeftButton.image.DOFade(1f, 0.25f));
		s.Insert(0.25f, OppTeamLeftButtonArrow.DOFade(1f, 0.25f));
		s.Insert(0.25f, OppTeamRightButton.image.DOFade(1f, 0.25f));
		s.Insert(0.25f, OppTeamRightButtonArrow.DOFade(1f, 0.25f));
	}
}
