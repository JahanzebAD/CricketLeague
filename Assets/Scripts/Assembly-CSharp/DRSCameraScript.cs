using DG.Tweening;
using UnityEngine;

public class DRSCameraScript : Singleton<DRSCameraScript>
{
	private Camera DRSCamera;

	private Vector3 moveToPosition = Vector3.zero;

	private bool cameraFixedinStumpCrease;

	private bool cameraMovedtoSideOfStump;

	private bool allRenderersDisabled;

	private bool cameraMovedtoTopofPitch;

	private bool hitStump;

	public float timeInterval = 4f;

	private TweenCallback callbackFunctionRef;

	public GameObject LBWINlineIndicator;

	private void Start()
	{
		DRSCamera = GetComponent<Camera>();
		EnableDRSCamera(state: false);
		LBWINlineIndicator.SetActive(value: false);
	}

	public void FixAtStump2Crease()
	{
		if (!cameraFixedinStumpCrease)
		{
			cameraFixedinStumpCrease = true;
			LBWINlineIndicator.SetActive(value: true);
			Singleton<GroundController>.instance.SetDRSTrailRenderer();
			SetTimeScale(0.2f);
			moveToPosition = new Vector3(Singleton<GroundController>.instance.groundCenterPoint.transform.position.x, 1.5f, Singleton<GroundController>.instance.groundCenterPoint.transform.position.z - 2f);
			DRSCamera.transform.eulerAngles = new Vector3(0f, -0.4f, 0f);
			DRSCamera.fieldOfView = 40f;
			DRSCamera.transform.DOMove(moveToPosition, 0f).SetUpdate(isIndependentUpdate: true);
			EnableDRSCamera(state: true);
			Singleton<DRS>.instance.ResetPanelTransition();
		}
	}

	public void DisableAllRenderers()
	{
		if (!allRenderersDisabled)
		{
			allRenderersDisabled = true;
			Singleton<GroundController>.instance.ProcessOnImpact();
			SetTimeScale(0f);
			Sequence sequence = DOTween.Sequence();
			callbackFunctionRef = delegate
			{
				Singleton<GroundController>.instance.EnableAllSkinRenderers(state: false);
			};
			sequence.InsertCallback(2.5f, callbackFunctionRef);
			callbackFunctionRef = delegate
			{
				SetTimeScale(0.1f);
			};
			sequence.InsertCallback(4f, callbackFunctionRef);
			sequence.SetUpdate(isIndependentUpdate: true);
		}
	}

	private void SetTimeScale(float timescale)
	{
		Time.timeScale = timescale;
	}

	public void HitStump(float state)
	{
		if (!hitStump)
		{
			hitStump = true;
			if (state == 1f)
			{
				state = 0.001f;
			}
			Sequence sequence = DOTween.Sequence();
			callbackFunctionRef = delegate
			{
				SetTimeScale(0f);
			};
			sequence.InsertCallback(state, callbackFunctionRef);
			sequence.InsertCallback(1f, ShowStumpImpacts);
			sequence.SetUpdate(isIndependentUpdate: true);
		}
	}

	public void ShowStumpImpacts()
	{
		if (!cameraMovedtoSideOfStump)
		{
			cameraMovedtoSideOfStump = true;
			moveToPosition = new Vector3(Singleton<GroundController>.instance.groundCenterPoint.transform.position.x + 2.68f, 0.58f, Singleton<GroundController>.instance.groundCenterPoint.transform.position.z + 9.5f);
			DRSCamera.transform.DORotate(new Vector3(0.1f, -97f, 0f), 1f).SetUpdate(isIndependentUpdate: true);
			DRSCamera.fieldOfView = 30f;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(DRSCamera.transform.DOMove(moveToPosition, 1f));
			sequence.InsertCallback(timeInterval, MoveCameraToBackOfStump);
			sequence.InsertCallback(timeInterval * 2f, MoveCameraToTopOfStump);
			sequence.InsertCallback(timeInterval * 3f, DRSCompleted);
			sequence.SetUpdate(isIndependentUpdate: true);
		}
	}

	public void MoveCameraToBackOfStump()
	{
		moveToPosition = new Vector3(Singleton<GroundController>.instance.groundCenterPoint.transform.position.x, 0.55f, Singleton<GroundController>.instance.groundCenterPoint.transform.position.z + 12.51f);
		DRSCamera.transform.DORotate(new Vector3(0.1f, -184.6f, 0f), 2f).SetUpdate(isIndependentUpdate: true);
		DRSCamera.fieldOfView = 30f;
		DRSCamera.transform.DOMove(moveToPosition, 2f).SetUpdate(isIndependentUpdate: true);
	}

	public void MoveCameraToTopOfStump()
	{
		moveToPosition = new Vector3(Singleton<GroundController>.instance.groundCenterPoint.transform.position.x + 0.02f, 3.02f, Singleton<GroundController>.instance.groundCenterPoint.transform.position.z + 9.97f);
		DRSCamera.transform.DORotate(new Vector3(90f, -180f, 0f), 2f).SetUpdate(isIndependentUpdate: true);
		DRSCamera.fieldOfView = 30f;
		DRSCamera.transform.DOMove(moveToPosition, 2f).SetUpdate(isIndependentUpdate: true);
	}

	public void MoveCameraToTopOfPitch()
	{
		if (!cameraMovedtoTopofPitch)
		{
			cameraMovedtoTopofPitch = true;
			SetTimeScale(0f);
			Singleton<GroundController>.instance.EnableAllSkinRenderers(state: false);
			moveToPosition = new Vector3(Singleton<GroundController>.instance.groundCenterPoint.transform.position.x - 0.29f, 6.79f, Singleton<GroundController>.instance.groundCenterPoint.transform.position.z + 5.56f);
			DRSCamera.transform.DORotate(new Vector3(90f, 0f, 0f), 2f).SetUpdate(isIndependentUpdate: true);
			DRSCamera.fieldOfView = 60f;
			Sequence sequence = DOTween.Sequence();
			sequence.Append(DRSCamera.transform.DOMove(moveToPosition, 2f));
			sequence.InsertCallback(timeInterval, DRSCompleted);
			sequence.SetUpdate(isIndependentUpdate: true);
		}
	}

	public void EnableDRSCamera(bool state)
	{
		DRSCamera.enabled = state;
	}

	private void ResetAllVariables()
	{
		cameraFixedinStumpCrease = false;
		cameraMovedtoSideOfStump = false;
		allRenderersDisabled = false;
		cameraMovedtoTopofPitch = false;
		hitStump = false;
	}

	public void DRSCompleted()
	{
		CONTROLLER.cameraType = 1;
		EnableDRSCamera(state: false);
		ResetAllVariables();
		LBWINlineIndicator.SetActive(value: false);
		Singleton<DRS>.instance.Hide();
		Singleton<GroundController>.instance.EnableAllSkinRenderers(state: true);
		Singleton<GroundController>.instance.ShowBowler(showStatus: false);
		Singleton<GroundController>.instance.showUmpireAfterDrs();
		Singleton<GroundController>.instance.bDRSPitchingOutsideLeg = false;
	}
}
