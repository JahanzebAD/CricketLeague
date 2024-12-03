using DG.Tweening;
using UnityEngine;

public class StandbyCam : Singleton<StandbyCam>
{
	public Camera standbyCam;

	public Camera standByCamArcadeMode;

	public Transform standbyCamPivot;

	private Tweener tween;

	public void RotateStandbyCam()
	{
		tween = null;
		standbyCam.enabled = true;
		standbyCamPivot.eulerAngles = Vector3.zero;
		tween = standbyCamPivot.DOLocalRotate(new Vector3(0f, 360f, 0f), 480f, RotateMode.WorldAxisAdd).SetLoops(-1, LoopType.Yoyo).SetUpdate(isIndependentUpdate: true);
		tween.Play();
	}

	public void RotateStandbyCamArcadeMode()
	{
		tween = null;
		standByCamArcadeMode.enabled = true;
		float duration = 30f;
		if (standbyCamPivot.eulerAngles.y < 355f)
		{
			duration = 60f;
		}
		else if (standbyCamPivot.eulerAngles.y > 355f || standbyCamPivot.eulerAngles.y == 0f)
		{
			duration = 30f;
		}
		tween = standbyCamPivot.DOLocalRotate(new Vector3(0f, 368f, 0f), duration, RotateMode.FastBeyond360).SetUpdate(isIndependentUpdate: true).OnComplete(RotateBack);
		tween.Play();
	}

	private void RotateBack()
	{
		tween = null;
		tween = standbyCamPivot.DOLocalRotate(new Vector3(0f, -8f, 0f), 60f, RotateMode.FastBeyond360).SetUpdate(isIndependentUpdate: true).OnComplete(RotateStandbyCamArcadeMode);
		tween.Play();
	}

	public void PauseTween()
	{
		standbyCam.enabled = false;
		if (tween != null && tween.IsPlaying())
		{
			tween.Pause();
		}
	}

	public void PauseTweenArcadeModes()
	{
		standByCamArcadeMode.enabled = false;
		tween = null;
	}

	public void DisableAllCams()
	{
		standbyCam.enabled = false;
		standByCamArcadeMode.enabled = false;
	}
}
