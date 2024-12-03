using DG.Tweening;
using UnityEngine;

public class BallSimulationCameraScripts : Singleton<BallSimulationCameraScripts>
{
	private Vector3 initialPosition;

	private Camera ballSimulationCamera;

	private bool cameraRightRearToStumpSetted;

	private bool cameraLeftRearToStumpSetted;

	private bool cameraSideToStumpSetted;

	private bool cameraBottomRearToStumpSetted;

	private Sequence mainSeq;

	private TweenCallback callbackfunction;

	private float timeInterval;

	public void InitializeCamera()
	{
		ballSimulationCamera = GetComponent<Camera>();
		ballSimulationCamera.fieldOfView = 40f;
		EnableCamera(state: false);
	}

	public void StartBallSimulationCamera()
	{
		ResetAllVariables();
		EnableCamera(state: true);
		timeInterval = 0.6f;
		timeInterval *= Singleton<BallSimulationManager>.instance.noOfBallDataSetted;
		mainSeq = DOTween.Sequence();
		callbackfunction = SetCameraRightRearToStump;
		mainSeq.InsertCallback(0f * timeInterval, callbackfunction);
		callbackfunction = delegate
		{
			Singleton<BallSimulationManager>.instance.StartBallMovement();
		};
		mainSeq.InsertCallback(2f, callbackfunction);
		callbackfunction = SetCameraBottomReartoStump;
		mainSeq.InsertCallback(3f * timeInterval, callbackfunction);
		callbackfunction = SetCameraSidetoStump;
		mainSeq.InsertCallback(4f * timeInterval, callbackfunction);
		callbackfunction = SetCameraLeftRearToStump;
		mainSeq.InsertCallback(5f * timeInterval, callbackfunction);
		mainSeq.SetUpdate(isIndependentUpdate: true);
	}

	public void EnableCamera(bool state)
	{
		ballSimulationCamera.enabled = state;
	}

	public void skip()
	{
		if (mainSeq != null)
		{
			mainSeq.Kill();
		}
	}

	private void SetCameraRightRearToStump()
	{
		if (!cameraRightRearToStumpSetted)
		{
			cameraRightRearToStumpSetted = true;
			ballSimulationCamera.fieldOfView = 40f;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(base.transform.DOMove(new Vector3(-21.937f, 6.44491f, -2.4234f), 0f));
			sequence.Insert(0f, base.transform.DORotate(new Vector3(7.428f, 88.762f, 0f), 0f));
			sequence.Insert(0.2f, base.transform.DOMove(new Vector3(-1.7783f, 2.3475f, 16.7059f), 1.2f));
			sequence.Insert(0.22f, base.transform.DORotate(new Vector3(14.647f, 171.956f, 0f), 1.2f));
			sequence.SetUpdate(isIndependentUpdate: true);
		}
	}

	private void SetCameraLeftRearToStump()
	{
		if (!cameraLeftRearToStumpSetted)
		{
			cameraLeftRearToStumpSetted = true;
			Sequence sequence = DOTween.Sequence();
			sequence.Insert(0f, base.transform.DOMove(new Vector3(1.7783f, 2.3475f, 16.7059f), 1.2f));
			sequence.Insert(0f, base.transform.DORotate(new Vector3(14.647f, -171.956f, 0f), 1.2f));
			sequence.SetUpdate(isIndependentUpdate: true);
		}
	}

	private void SetCameraSidetoStump()
	{
		if (!cameraSideToStumpSetted)
		{
			cameraSideToStumpSetted = true;
			Sequence sequence = DOTween.Sequence();
			sequence.Insert(0f, base.transform.DOMove(new Vector3(7.95074f, 1.05222f, 9.2357f), 1.2f));
			sequence.Insert(0f, base.transform.DORotate(new Vector3(2.1f, -94.71f, 0f), 1.2f));
			sequence.SetUpdate(isIndependentUpdate: true);
		}
	}

	private void SetCameraBottomReartoStump()
	{
		if (!cameraBottomRearToStumpSetted)
		{
			cameraBottomRearToStumpSetted = true;
			Sequence sequence = DOTween.Sequence();
			sequence.Insert(0f, base.transform.DOMove(new Vector3(-0.0428f, 1.3475f, 14.4192f), 1.2f));
			sequence.Insert(0f, base.transform.DORotate(new Vector3(7.944f, -181.85f, 0f), 1.2f));
			sequence.SetUpdate(isIndependentUpdate: true);
		}
	}

	private void ResetAllVariables()
	{
		cameraRightRearToStumpSetted = false;
		cameraLeftRearToStumpSetted = false;
		cameraSideToStumpSetted = false;
		cameraBottomRearToStumpSetted = false;
	}
}
