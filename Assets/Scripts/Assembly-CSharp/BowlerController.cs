using UnityEngine;

public class BowlerController : MonoBehaviour
{
	private GroundController ballScript;

	private void Start()
	{
		ballScript = GameObject.Find("GroundController").GetComponent<GroundController>();
	}

	private void TriggerCameraZoom()
	{
		ballScript.ZoomCameraToPitch();
	}

	private void FreezeTheBowlingSpot()
	{
		ballScript.FreezeTheBowlingSpot();
	}

	private void ReleaseTheBall()
	{
		ballScript.ReleaseTheBall();
	}

	private void HideBowler()
	{
	}
}
