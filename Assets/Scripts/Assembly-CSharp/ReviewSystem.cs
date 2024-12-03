using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ReviewSystem : Singleton<ReviewSystem>
{
	public GameObject userReview;

	public GameObject AiReview;

	public GameObject yesBtn;

	public GameObject noBtn;

	public Text timerText;

	public Text AiReviewText;

	public Text reviewsLeftText;

	public Text yesBtnText;

	public Text countryName;

	public Image timeFill;

	public Image AiFlag;

	private bool askedForReview;

	public int TeamIndex;

	public void ShowUltraEdgeReview()
	{
		if (Singleton<GroundController>.instance.UserCanAskReview)
		{
			noBtn.SetActive(value: true);
			yesBtn.SetActive(value: true);
			TeamIndex = CONTROLLER.myTeamIndex;
			AiReviewText.text = LocalizationData.instance.getText(611);
			yesBtnText.text = LocalizationData.instance.getText(164);
		}
		else if (Singleton<GroundController>.instance.AiCanAskReview)
		{
			noBtn.SetActive(value: false);
			TeamIndex = CONTROLLER.opponentTeamIndex;
			AiReviewText.text = LocalizationData.instance.getText(614);
			yesBtnText.text = LocalizationData.instance.getText(167);
			yesBtn.SetActive(value: false);
		}
		userReview.SetActive(value: true);
		Sprite[] flags = Singleton<FlagHolderGround>.instance.flags;
		foreach (Sprite sprite in flags)
		{
			if (sprite.name.ToUpper() == CONTROLLER.TeamList[TeamIndex].abbrevation)
			{
				AiFlag.sprite = sprite;
			}
		}
		countryName.text = CONTROLLER.TeamList[TeamIndex].abbrevation.ToUpper();
		reviewsLeftText.text = LocalizationData.instance.getText(613) + " " + CONTROLLER.TeamList[TeamIndex].noofDRSLeft;
	}

	public void StartTimer()
	{
		askedForReview = false;
		timeFill.fillAmount = 1f;
		timerText.text = "7";
		DOTween.To(delegate(float x)
		{
			timerText.text = string.Empty + (int)x;
		}, 6f, 1f, 5f).OnComplete(NoBtnClicked).SetUpdate(isIndependentUpdate: true)
			.SetEase(Ease.Linear);
		if (Singleton<GroundController>.instance.AiCanAskReview)
		{
			AiFlag.DOFade(1f, 3f).SetUpdate(isIndependentUpdate: true).OnComplete(AiTimerComplete);
		}
		timeFill.DOFillAmount(0f, 5f).SetUpdate(isIndependentUpdate: true).SetEase(Ease.Linear);
	}

	public void YesBtnClicked()
	{
		CONTROLLER.TeamList[TeamIndex].noofDRSLeft--;
		CONTROLLER.canShowReplay = true;
		CONTROLLER.reviewReplay = true;
		Time.timeScale = 1f;
		userReview.SetActive(value: false);
		AiReview.SetActive(value: false);
		Singleton<GameModel>.instance.AnimationCompleted();
		askedForReview = true;
		//FirebaseAnalyticsManager.instance.logEvent("Extras", new string[2] { "ExtrasAction", "UltraEdge_Review" });
	}

	private void AiTimerComplete()
	{
		int index = LocalizationData.instance.refList.IndexOf(CONTROLLER.TeamList[CONTROLLER.opponentTeamIndex].teamName.ToUpper());
		string text = LocalizationData.instance.getText(index);
		AiReviewText.text = text + " " + LocalizationData.instance.getText(615);
	}

	public void NoBtnClicked()
	{
		if (!askedForReview && !Singleton<GroundController>.instance.AiCanAskReview)
		{
			askedForReview = true;
			Time.timeScale = 1f;
			Singleton<GroundController>.instance.UltraEdgeCutscenePlaying = false;
			int num = 0;
			num = ((Singleton<GroundController>.instance.UmpireInitialDecision == "out") ? 1 : 0);
			Singleton<GameModel>.instance.UpdateCurrentBall(Singleton<GroundController>.instance.validBall, Singleton<GroundController>.instance.canCountBall, Singleton<GroundController>.instance.runsScored, Singleton<GroundController>.instance.extraRun, Singleton<GroundController>.instance.batsmanID, num, Singleton<GroundController>.instance.wicketType, Singleton<GroundController>.instance.bowlerID, Singleton<GroundController>.instance.catcherID, Singleton<GroundController>.instance.batsmanOut, Singleton<GroundController>.instance.isBoundary);
			userReview.SetActive(value: false);
			AiReview.SetActive(value: false);
		}
		else if (Singleton<GroundController>.instance.AiCanAskReview)
		{
			timerText.text = "0";
			YesBtnClicked();
		}
	}
}
