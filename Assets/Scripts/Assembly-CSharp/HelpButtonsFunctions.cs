using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpButtonsFunctions : Singleton<HelpButtonsFunctions>
{
	public Sprite[] buttonSprites;

	public Text[] HeadingText;

	public GameObject holder;

	public GameObject GroundHolder;

	public Image[] tapBtns;

	public GameObject MainContent;

	public GameObject[] contents;

	private int previousTab;

	public Image[] UISprites;

	public Text[] ContentText;

	private void Start()
	{
		holder.SetActive(value: false);
		previousTab = 0;
		openTab(0);
	}

	public void openTab(int index)
	{
		if (index == previousTab)
		{
			SameTabPressed(index);
			return;
		}
		ContentReset(index);
		contents[index].SetActive(value: true);
		tapBtns[index].sprite = buttonSprites[1];
		HeadingText[index].color = Color.black;
		UISprites[index].transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		contents[previousTab].SetActive(value: false);
		UISprites[previousTab].transform.localEulerAngles = Vector3.zero;
		tapBtns[previousTab].sprite = buttonSprites[0];
		HeadingText[previousTab].color = Color.white;
		previousTab = index;
	}

	public void ContentReset(int index1)
	{
		switch (index1)
		{
		case 0:
			MainContent.transform.localPosition = new Vector3(0f, 0f, 0f);
			break;
		case 1:
			MainContent.transform.localPosition = new Vector3(0f, 55f, 0f);
			break;
		case 2:
			ContentText[index1].transform.localPosition = new Vector3(7.5f, -320f, 0f);
			MainContent.transform.localPosition = new Vector3(0f, 120f, 0f);
			break;
		case 3:
			ContentText[index1].transform.localPosition = new Vector3(7.5f, -180f, 0f);
			MainContent.transform.localPosition = new Vector3(0f, 175f, 0f);
			break;
		case 4:
			MainContent.transform.localPosition = new Vector3(0f, 235f, 0f);
			break;
		case 5:
			ContentText[index1].transform.localPosition = new Vector3(7.5f, -100f, 0f);
			MainContent.transform.localPosition = new Vector3(0f, 293f, 0f);
			break;
		case 6:
			MainContent.transform.localPosition = new Vector3(0f, 355f, 0f);
			break;
		case 7:
			MainContent.transform.localPosition = new Vector3(0f, 370f, 0f);
			break;
		case 8:
			ContentText[index1].transform.localPosition = new Vector3(7.5f, -220f, 0f);
			MainContent.transform.localPosition = new Vector3(0f, 480f, 0f);
			break;
		case 9:
			ContentText[index1].transform.localPosition = new Vector3(7.5f, -240f, 0f);
			MainContent.transform.localPosition = new Vector3(0f, 454f, 0f);
			break;
		case 10:
			MainContent.transform.localPosition = new Vector3(0f, 323f, 0f);
			break;
		}
	}

	public void SameTabPressed(int index)
	{
		bool active;
		float y;
		int num;
		if (contents[index].activeSelf)
		{
			active = false;
			y = 0f;
			num = 0;
		}
		else
		{
			active = true;
			y = 180f;
			num = 1;
		}
		contents[index].SetActive(active);
		UISprites[index].transform.localEulerAngles = new Vector3(0f, y, 0f);
		tapBtns[index].sprite = buttonSprites[num];
		if (num == 0)
		{
			HeadingText[index].color = Color.white;
		}
		else
		{
			HeadingText[index].color = Color.black;
		}
	}

	public void ShowMe()
	{
		Singleton<NavigationBack>.instance.tempDeviceBack = Singleton<NavigationBack>.instance.deviceBack;
		Singleton<NavigationBack>.instance.deviceBack = HideMe;
		if (Singleton<GameModeTWO>.instance != null)
		{
			Singleton<GameModeTWO>.instance.Holder.SetActive(value: false);
		}
		if (CONTROLLER.canShowbannerMainmenu == 1)
		{
			//Singleton<AdIntegrate>.instance.ShowAd();
		}
		MainContent.transform.localPosition = new Vector3(0f, 0f, 0f);
		openTab(0);
		holder.SetActive(value: true);
		Singleton<HelpTWOPanelTransistion>.instance.PanelTransition();
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			CONTROLLER.pageName = "help";
			return;
		}
		GroundHolder.transform.localPosition = new Vector3(0f, -53.4f, 0f);
		CONTROLLER.pageName = "helpGround";
	}

	public void HideMe()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		//Singleton<AdIntegrate>.instance.HideAd();
		holder.SetActive(value: false);
		if (!(SceneManager.GetActiveScene().name == "MainMenu"))
		{
			CONTROLLER.pageName = "GamePause";
		}
		if (Singleton<GameModeTWO>.instance != null)
		{
			Singleton<GameModeTWO>.instance.ShowWithoutAnim();
			CONTROLLER.pageName = "landingPage";
		}
		else
		{
			Singleton<PauseGameScreen>.instance.midPageGO.SetActive(value: true);
		}
	}
}
