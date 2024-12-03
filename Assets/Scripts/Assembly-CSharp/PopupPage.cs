using UnityEngine;

public class PopupPage : MonoBehaviour
{
	private void OnEnable()
	{
		CONTROLLER.tempCurrentPage = CONTROLLER.CurrentPage;
		CONTROLLER.CurrentPage = "PopupPage";
	}

	private void OnDisable()
	{
		CONTROLLER.CurrentPage = CONTROLLER.tempCurrentPage;
	}
}
