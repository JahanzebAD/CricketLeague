using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimationScreen : Singleton<AnimationScreen>
{
	public Sprite[] numberSprites;

	public Image number;

	public GameObject animObj;

	public GameObject animRed1;

	public GameObject animRed2;

	public Image Red1;

	public Image Red2;

	private int animType;

	private Transform _transform;

	protected void Awake()
	{
		Hide(boolean: true);
	}

	public void StartAnimation(int type)
	{
		Singleton<BoundaryAnimation2>.instance.ShowMe(type);
		Invoke("StartSA", 2.5f);
	}

	public void StartSA()
	{
		StartCoroutine(StopAnimation());
	}

	public IEnumerator StopAnimation()
	{
		bool isOverStepBall = Singleton<GroundController>.instance.getOverStepBall();
		if (CONTROLLER.gameMode == "WPL" && (animType == 0 || animType == 1) && !isOverStepBall)
		{
			yield return new WaitForSeconds(2f);
		}
		if (isOverStepBall && (animType == 0 || animType == 1))
		{
			StartCoroutine(Singleton<GroundController>.instance.updateBoundaryBall());
		}
		else
		{
			Singleton<GameModel>.instance.AnimationCompleted();
		}
	}

	public void Hide(bool boolean)
	{
		if (boolean)
		{
			animObj.SetActive(value: false);
		}
		else
		{
			animObj.SetActive(value: true);
		}
	}
}
