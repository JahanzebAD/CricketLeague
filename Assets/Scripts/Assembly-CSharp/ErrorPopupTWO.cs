using UnityEngine;
using UnityEngine.UI;

public class ErrorPopupTWO : Singleton<ErrorPopupTWO>
{
	public GameObject Holder;

	public Text ErrorTxt;

	protected void Start()
	{
		Holder.SetActive(value: false);
	}

	public void OkButton()
	{
		CONTROLLER.CurrentMenu = string.Empty;
		hideMe();
	}

	public void showMe(string str)
	{
		CONTROLLER.PopupName = "errorPopup";
		ErrorTxt.text = str;
		Singleton<Popups>.instance.ShowMe();
		CONTROLLER.CurrentMenu = "teamvalidate";
	}

	public void hideMe()
	{
		CONTROLLER.pageName = "edit";
		Holder.SetActive(value: false);
	}
}
