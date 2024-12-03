using UnityEngine;
using UnityEngine.UI;

public class ToggleCamera : MonoBehaviour
{
	public Text btnContent;

	private void Start()
	{
		CheckCamera();
	}

	private void CheckCamera()
	{
		btnContent.text = "Camera " + (CONTROLLER.cameraType + 1);
	}

	public void ToggleCameraType()
	{
		CONTROLLER.cameraType = ((CONTROLLER.cameraType != 1) ? 1 : 0);
		CheckCamera();
	}
}
