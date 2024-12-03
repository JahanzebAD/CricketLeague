using UnityEngine;

public class ProgressDialog : MonoBehaviour
{
	private string tempStr = string.Empty;

	protected void Start()
	{
	}

	public void showProgressDialog(string _title, string _desc)
	{
		tempStr = CONTROLLER.CurrentMenu;
		CONTROLLER.CurrentMenu = string.Empty;
		base.gameObject.transform.localPosition = new Vector3(0f, 0f, 0.5f);
	}

	public void hideProgressDialog()
	{
		CONTROLLER.CurrentMenu = tempStr;
		base.gameObject.transform.localPosition = CONTROLLER.HIDEPOS;
	}
}
