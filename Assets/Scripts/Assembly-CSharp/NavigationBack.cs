using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavigationBack : Singleton<NavigationBack>
{
	public delegate void DeviceBack();

	public DeviceBack deviceBack;

	private bool escapeKeyPressed;

	public bool disableDeviceBack;

	public DeviceBack tempDeviceBack;

	public Button BtnComponent;

	private void Awake()
	{
		deviceBack = Singleton<GameModeTWO>.instance.OpenQuitGamePopup;
	}

	protected void Update()
	{
		if (Input.GetMouseButtonUp(0) && EventSystem.current.currentSelectedGameObject != null)
		{
			CONTROLLER.sndController.PlayButtonSnd();
		}
		if (Input.GetKeyUp(KeyCode.Escape) && !disableDeviceBack)
		{
			if (CONTROLLER.pageName == "store")
			{
				Singleton<Store>.instance.BackButtonClicked();
			}
			else if (CONTROLLER.pageName == "powerHelp")
			{
				Singleton<PowerUps>.instance.CloseHelp(Singleton<PowerUps>.instance.tempHelp);
			}
			else if (CONTROLLER.pageName == "popup")
			{
				Singleton<Popups>.instance.HideMe();
			}
			else if (deviceBack != null)
			{
				deviceBack();
			}
		}
	}
}
