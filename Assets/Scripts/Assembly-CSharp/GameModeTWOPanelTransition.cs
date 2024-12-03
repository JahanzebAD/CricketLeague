using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameModeTWOPanelTransition : Singleton<GameModeTWOPanelTransition>
{
	public ShineAnim[] shineAnim;

	public Transform logo;

	public Transform cricketGamingImage;

	public Transform quickPlay;

	public Text quickPlayText;

	public Transform worldCup;

	public Text worldCupText;

	public Transform tournament;

	public Text tournamentText;

	public Transform npl;

	public Text nplText;

	public Transform superOver;

	public Text superOverText;

	public Transform spinTheWheel;

	public Text spinTheWheelText;

	public Transform quickPlayicon;

	public Transform worldCupIcon;

	public Transform tournamentIcon;

	public Transform nplIcon;

	public Transform superOverIcon;

	public Transform spinTheWheelIcon;

	public Transform aboutPanel;

	public Transform aboutbtnRedBG;

	public Text aboutbtnText;

	public Transform settingsbtnRedBG;

	public Text settingsbtnText;

	public Transform helpbtnRedBG;

	public Text helpbtnText;

	public Transform leaderboardbtnRedBG;

	public Text leaderboardbtnText;

	public Transform aboutbtnIcon;

	public Transform settingsbtnIcon;

	public Transform helpbtnIcon;

	public Transform leaderboardbtnIcon;

	public Transform followusPanel;

	public Text followusFBText;

	public Transform followusFBIcon;

	public Transform followusFBRedBG;

	public Text followusTweeterText;

	public Transform followusTweeterIcon;

	public Transform followusTweeterRedBG;

	public Text rateusText;

	public Transform rateusIcon;

	public Transform rateusRedBG;

	public Text shareusText;

	public Transform shareusIcon;

	public Transform shareusRedBG;

	public Image userPic;

	public Text userName;

	public Image storeIcon;

	public Text storeText;

	public Image storeBG;

	public Image spinIcon;

	public Text spinText;

	public Image spinBG;

	public Image star;

	public Text starCount;

	public Text starText;

	public Image tokenIcon;

	public Text tokenText;

	public Text tokenText2;

	public Image xpIcon;

	public Text xpText;

	public Text xpText2;

	public Image line;

	public Image line2;

	public Image rewardsIcon;

	public Image rewardsRedBG;

	public Transform content;

	public Transform[] comboBox;

	public Transform achievementsRedBG;

	public Transform achievementsIcon;

	private int count;

	public float fadetime;

	private void Start()
	{
		Invoke("StartShineAnim", 1f);
	}

	public void PanelTransition()
	{
		resetTransition();
		Sequence sequence = DOTween.Sequence();
		sequence.Append(logo.DOScaleX(0.1f, 0f));
		sequence.Insert(0.6f, logo.DOScale(Vector3.one, 0.3f));
		sequence.Insert(0.7f, cricketGamingImage.DOScaleX(1f, 0.2f));
		sequence.Insert(0.9f, cricketGamingImage.DOPunchScale(new Vector3(0.05f, 0.05f, 0.05f), 0.15f, 0));
		sequence.Insert(1f, cricketGamingImage.DOScaleX(1f, 0f));
		sequence.Insert(0.2f, userPic.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
		sequence.Insert(0.3f, userPic.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f, 0));
		sequence.Insert(0.25f, userName.DOFade(1f, fadetime));
		sequence.Insert(0.3f, line.transform.DOScaleY(1f, 0.4f));
		sequence.Insert(0.3f, line2.transform.DOScaleY(1f, 0.4f));
		sequence.Insert(0.4f, star.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.4f));
		sequence.Insert(0.4f, star.transform.DOScale(Vector3.one, 0.4f));
		sequence.Insert(0.45f, starCount.DOFade(1f, fadetime));
		sequence.Insert(0.45f, starText.DOFade(1f, fadetime));
		sequence.Insert(0.6f, xpIcon.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.4f));
		sequence.Insert(0.6f, xpIcon.transform.DOScale(Vector3.one, 0.4f));
		sequence.Insert(0.65f, xpText.DOFade(1f, fadetime));
		sequence.Insert(0.65f, xpText2.DOFade(1f, fadetime));
		sequence.Insert(0.85f, storeBG.transform.DOScaleX(1f, 0.4f));
		sequence.Insert(0.9f, spinBG.transform.DOScaleX(1f, 0.4f));
		sequence.Insert(0.85f, storeIcon.DOFade(1f, fadetime));
		sequence.Insert(0.85f, storeIcon.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 0.5f, snapping: true));
		sequence.Insert(0.55f, storeText.DOFade(1f, fadetime));
		sequence.Insert(0.5f, tokenIcon.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.4f));
		sequence.Insert(0.5f, tokenIcon.transform.DOScale(Vector3.one, 0.4f));
		sequence.Insert(0.55f, tokenText.DOFade(1f, fadetime));
		sequence.Insert(0.55f, tokenText2.DOFade(1f, fadetime));
		sequence.Insert(0.2f, rewardsIcon.transform.DOScale(Vector3.one, 0.4f));
		sequence.Insert(0.8f, rewardsIcon.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.2f, 0));
		sequence.Insert(0.4f, rewardsRedBG.transform.DOScaleX(1f, 0.4f));
		sequence.Insert(0.4f, achievementsIcon.transform.DOScale(Vector3.one, 0.4f));
		sequence.Insert(1f, achievementsIcon.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.2f, 0));
		sequence.Insert(0.75f, achievementsRedBG.transform.DOScaleX(1f, 0.4f));
		sequence.Insert(0.6f, comboBox[0].DOScale(new Vector3(0.85f, 0.85f, 1f), 0.3f));
		sequence.Insert(0.7f, comboBox[1].DOScale(new Vector3(0.85f, 0.85f, 1f), 0.3f));
		sequence.Insert(0.7f, quickPlay.DOScale(Vector3.one, 0.4f));
		sequence.Insert(0.75f, quickPlayText.DOFade(1f, fadetime));
		sequence.Insert(1f, quickPlayicon.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.3f, 0));
		sequence.Insert(0.8f, tournament.DOScale(Vector3.one, 0.4f));
		sequence.Insert(0.85f, tournamentText.DOFade(1f, fadetime));
		sequence.Insert(1.1f, tournamentIcon.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.3f, 0));
		sequence.Insert(0.9f, worldCup.DOScale(Vector3.one, 0.4f));
		sequence.Insert(0.95f, worldCupText.DOFade(1f, fadetime));
		sequence.Insert(1.2f, worldCupIcon.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.3f, 0));
		sequence.Insert(1f, spinTheWheel.DOScale(Vector3.one, 0.4f));
		sequence.Insert(1.5f, spinTheWheelText.DOFade(1f, fadetime));
		sequence.Insert(1.3f, spinTheWheelIcon.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.3f, 0));
		sequence.Insert(1.1f, superOver.DOScale(Vector3.one, 0.4f));
		sequence.Insert(1.15f, superOverText.DOFade(1f, fadetime));
		sequence.Insert(1.4f, superOverIcon.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.3f, 0));
		sequence.Insert(1.2f, npl.DOScale(Vector3.one, 0.4f));
		sequence.Insert(1.25f, nplText.DOFade(1f, fadetime));
		sequence.Insert(1.5f, nplIcon.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.3f, 0));
		sequence.Insert(1f, aboutPanel.DOScaleX(1f, 0.4f));
		sequence.Insert(1.05f, aboutbtnText.DOFade(1f, fadetime));
		sequence.Insert(1.1f, aboutbtnRedBG.DOScaleX(1f, 0.4f));
		sequence.Insert(1.2f, aboutbtnIcon.DOScale(Vector3.one, 0.4f));
		sequence.Insert(1.2f, settingsbtnRedBG.DOScaleX(1f, 0.4f));
		sequence.Insert(1.25f, settingsbtnText.DOFade(1f, fadetime));
		sequence.Insert(1.3f, settingsbtnIcon.DOScale(Vector3.one, 0.4f));
		sequence.Insert(1.3f, helpbtnRedBG.DOScaleX(1f, 0.4f));
		sequence.Insert(1.35f, helpbtnText.DOFade(1f, fadetime));
		sequence.Insert(1.4f, helpbtnIcon.DOScale(Vector3.one, 0.4f));
		sequence.Insert(1.4f, leaderboardbtnRedBG.DOScaleX(1f, 0.4f));
		sequence.Insert(1.45f, leaderboardbtnText.DOFade(1f, fadetime));
		sequence.Insert(1.5f, leaderboardbtnIcon.DOScale(Vector3.one, 0.4f));
		sequence.Insert(1.2f, followusPanel.DOScaleX(1f, 0.4f));
		sequence.Insert(1.25f, followusFBText.DOFade(1f, fadetime));
		sequence.Insert(1.3f, followusFBRedBG.DOScaleX(1f, 0.4f));
		sequence.Insert(1.4f, followusFBIcon.DOScale(Vector3.one, 0.4f));
		sequence.Insert(1.4f, followusTweeterRedBG.DOScaleX(1f, 0.4f));
		sequence.Insert(1.45f, followusTweeterText.DOFade(1f, fadetime));
		sequence.Insert(1.5f, followusTweeterIcon.DOScale(Vector3.one, 0.4f));
		sequence.Insert(1.5f, rateusRedBG.DOScaleX(1f, 0.4f));
		sequence.Insert(1.55f, rateusText.DOFade(1f, fadetime));
		sequence.Insert(1.6f, helpbtnIcon.DOScale(Vector3.one, 0.4f));
		sequence.Insert(1.6f, shareusRedBG.DOScaleX(1f, 0.4f));
		sequence.Insert(1.65f, shareusText.DOFade(1f, fadetime));
		sequence.Insert(1.7f, shareusIcon.DOScale(Vector3.one, 0.4f));
		sequence.SetUpdate(isIndependentUpdate: true);
		sequence.SetLoops(1);
	}

	private void StartShineAnim()
	{
		if (count < 7)
		{
			shineAnim[count].StartAnim();
			count++;
			Invoke("StartShineAnim", 1f);
		}
		else
		{
			count = 0;
		}
	}

	public void resetTransition()
	{
		content.localPosition = Vector3.zero;
		storeIcon.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		storeBG.transform.localScale = new Vector3(0f, 1f, 1f);
		storeIcon.transform.localPosition = new Vector3(-6f, 0f, 0f);
		spinBG.transform.localScale = new Vector3(0f, 1f, 1f);
		logo.localScale = Vector3.zero;
		cricketGamingImage.localScale = new Vector3(0f, 1f, 1f);
		comboBox[0].localScale = Vector3.zero;
		comboBox[1].localScale = Vector3.zero;
		quickPlay.localScale = Vector3.zero;
		worldCup.localScale = Vector3.zero;
		tournament.localScale = Vector3.zero;
		npl.localScale = Vector3.zero;
		superOver.localScale = Vector3.zero;
		spinTheWheel.localScale = Vector3.zero;
		aboutPanel.localScale = new Vector3(0f, 1f, 1f);
		aboutbtnRedBG.localScale = new Vector3(0f, 1f, 1f);
		settingsbtnRedBG.localScale = new Vector3(0f, 1f, 1f);
		helpbtnRedBG.localScale = new Vector3(0f, 1f, 1f);
		leaderboardbtnRedBG.localScale = new Vector3(0f, 1f, 1f);
		aboutbtnIcon.localScale = Vector3.zero;
		settingsbtnIcon.localScale = Vector3.zero;
		helpbtnIcon.localScale = Vector3.zero;
		leaderboardbtnIcon.localScale = Vector3.zero;
		followusPanel.localScale = new Vector3(0f, 1f, 1f);
		followusFBRedBG.localScale = new Vector3(0f, 1f, 1f);
		followusTweeterRedBG.localScale = new Vector3(0f, 1f, 1f);
		rateusRedBG.localScale = new Vector3(0f, 1f, 1f);
		shareusRedBG.localScale = new Vector3(0f, 1f, 1f);
		aboutbtnText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		quickPlayText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		worldCupText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		tournamentText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		nplText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		superOverText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		spinTheWheelText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		helpbtnText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		helpbtnText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		leaderboardbtnText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		followusFBText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		followusTweeterText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		rateusText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		shareusText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		userPic.transform.localScale = Vector3.zero;
		userName.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		line.transform.localScale = new Vector3(1f, 0f, 1f);
		line2.transform.localScale = new Vector3(1f, 0f, 1f);
		starCount.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		starText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		star.transform.DOLocalRotate(new Vector3(0f, 0f, -90f), 0f);
		star.transform.localScale = Vector3.zero;
		xpText.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		xpText2.DOFade(0f, 0f).SetUpdate(isIndependentUpdate: true);
		xpIcon.transform.DOLocalRotate(new Vector3(0f, 0f, -90f), 0f);
		xpIcon.transform.localScale = Vector3.zero;
		storeIcon.transform.localPosition = new Vector3(-85f, 0f, 0f);
		storeText.DOFade(0f, fadetime).SetUpdate(isIndependentUpdate: true);
		tokenIcon.transform.DOLocalRotate(new Vector3(0f, 0f, -90f), 0f);
		tokenIcon.transform.localScale = Vector3.zero;
		tokenText.DOFade(0f, fadetime).SetUpdate(isIndependentUpdate: true);
		tokenText2.DOFade(0f, fadetime).SetUpdate(isIndependentUpdate: true);
		rewardsIcon.transform.localScale = Vector3.zero;
		rewardsRedBG.transform.localScale = new Vector3(0f, 1f, 1f);
		achievementsIcon.transform.localScale = Vector3.zero;
		achievementsRedBG.transform.localScale = new Vector3(0f, 1f, 1f);
	}
}
