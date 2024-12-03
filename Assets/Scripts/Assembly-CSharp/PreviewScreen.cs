using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewScreen : Singleton<PreviewScreen>
{
	public GameObject previewScreen;

	public GameObject GroundBG;

	public GameObject BallIcon;

	public GameObject StrikerIcon;

	public GameObject RunnerIcon;

	public GameObject[] FielderIcon;

	public GameObject alertPopup;

	public Button NextBtn;

	public Button PrevBtn;

	public GameObject BowlerSideBtn;

	public Text FieldStatus;

	public GameObject FieldRestrictBG;

	private Transform _transform;

	protected void Awake()
	{
		_transform = base.transform;
		Hide(boolean: true);
		alertPopup.SetActive(value: false);
	}

	public void Start()
	{
		FieldStatus.text = LocalizationData.instance.getText(238) + CONTROLLER.fielderChangeIndex;
		BowlerSideBtn.SetActive(value: false);
		hideBtns(boolean: false);
		if (CONTROLLER.myTeamIndex == CONTROLLER.BowlingTeamIndex)
		{
			SetFieldPreview();
		}
	}

	public void addEventListener()
	{
	}

	public void hideBtns(bool boolean)
	{
		if (CONTROLLER.fielderChangeIndex == 1)
		{
		}
		if (CONTROLLER.PowerPlay)
		{
			if (CONTROLLER.fielderChangeIndex < 10)
			{
			}
		}
		else if (CONTROLLER.fielderChangeIndex < 25)
		{
		}
		BowlerSideBtn.gameObject.SetActive(boolean);
		FieldRestrictBG.SetActive(boolean);
	}

	public void BowlerSideChangeClicked()
	{
		Singleton<GroundController>.instance.ActivateBowlerSideChangeViaUI();
	}

	private bool canChangeFieldPlacement()
	{
		bool lineFreeHitBall = Singleton<GroundController>.instance.getLineFreeHitBall();
		if (CONTROLLER.noBallFacedBatsmanId == CONTROLLER.StrikerIndex && lineFreeHitBall)
		{
			Singleton<FreeHitValidation>.instance.showMe();
			return false;
		}
		return true;
	}

	public void NavigationClicked(int index)
	{
		if (!canChangeFieldPlacement())
		{
			return;
		}
		switch (index)
		{
		case 1:
			if (CONTROLLER.PowerPlay && CONTROLLER.fielderChangeIndex < 10)
			{
				CONTROLLER.fielderChangeIndex++;
			}
			if (!CONTROLLER.PowerPlay && CONTROLLER.fielderChangeIndex < 25)
			{
				CONTROLLER.fielderChangeIndex++;
			}
			break;
		case 0:
			if (CONTROLLER.fielderChangeIndex > 1)
			{
				CONTROLLER.fielderChangeIndex--;
			}
			break;
		}
		SetFieldPreview();
		Singleton<GroundController>.instance.ResetFielders();
	}

	public void SetFieldPreview()
	{
		if (CONTROLLER.fielderChangeIndex > 10)
		{
			FieldStatus.text = LocalizationData.instance.getText(239) + (CONTROLLER.fielderChangeIndex - 10);
		}
		else
		{
			FieldStatus.text = LocalizationData.instance.getText(238) + CONTROLLER.fielderChangeIndex;
		}
		if (CONTROLLER.fielderChangeIndex == 1 || CONTROLLER.fielderChangeIndex > 1)
		{
		}
		if (CONTROLLER.PowerPlay)
		{
			if (CONTROLLER.fielderChangeIndex != 5 && CONTROLLER.fielderChangeIndex >= 5)
			{
			}
		}
		else if (!CONTROLLER.PowerPlay && CONTROLLER.fielderChangeIndex != 10 && CONTROLLER.fielderChangeIndex >= 10)
		{
		}
	}

	public void UpdateBowlerSideChangeUI(string side)
	{
		if (side == "left")
		{
			BowlerSideBtn.transform.localPosition = new Vector3(-15f, BowlerSideBtn.transform.localPosition.y, BowlerSideBtn.transform.localPosition.z);
		}
		else if (side == "right")
		{
			BowlerSideBtn.transform.localPosition = new Vector3(15f, BowlerSideBtn.transform.localPosition.y, BowlerSideBtn.transform.localPosition.z);
		}
	}

	public void UpdatePreviewScreen(Dictionary<string, object> hashtable)
	{
		float num = 1f;
		Vector3 vector;
		if (hashtable.ContainsKey("Ball"))
		{
			BallIcon.SetActive(value: true);
			vector = (Vector3)hashtable["Ball"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			BallIcon.transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
		else
		{
			BallIcon.SetActive(value: false);
		}
		if (hashtable.ContainsKey("Striker"))
		{
			StrikerIcon.SetActive(value: true);
			vector = (Vector3)hashtable["Striker"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			StrikerIcon.transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
		else
		{
			StrikerIcon.SetActive(value: false);
		}
		if (hashtable.ContainsKey("NonStriker"))
		{
			RunnerIcon.SetActive(value: true);
			vector = (Vector3)hashtable["NonStriker"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			RunnerIcon.transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
		else
		{
			RunnerIcon.SetActive(value: false);
		}
		if (hashtable.ContainsKey("field_01"))
		{
			vector = (Vector3)hashtable["field_01"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			FielderIcon[0].transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
		if (hashtable.ContainsKey("field_02"))
		{
			vector = (Vector3)hashtable["field_02"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			FielderIcon[1].transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
		if (hashtable.ContainsKey("field_03"))
		{
			vector = (Vector3)hashtable["field_03"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			FielderIcon[2].transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
		if (hashtable.ContainsKey("field_04"))
		{
			vector = (Vector3)hashtable["field_04"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			FielderIcon[3].transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
		if (hashtable.ContainsKey("field_05"))
		{
			vector = (Vector3)hashtable["field_05"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			FielderIcon[4].transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
		if (hashtable.ContainsKey("field_06"))
		{
			vector = (Vector3)hashtable["field_06"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			FielderIcon[5].transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
		if (hashtable.ContainsKey("field_07"))
		{
			vector = (Vector3)hashtable["field_07"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			FielderIcon[6].transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
		if (hashtable.ContainsKey("field_08"))
		{
			vector = (Vector3)hashtable["field_08"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			FielderIcon[7].transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
		if (hashtable.ContainsKey("field_09"))
		{
			vector = (Vector3)hashtable["field_09"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			FielderIcon[8].transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
		if (hashtable.ContainsKey("field_10"))
		{
			vector = (Vector3)hashtable["field_10"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			FielderIcon[9].transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
		if (hashtable.ContainsKey("field_11"))
		{
			vector = (Vector3)hashtable["field_11"];
			vector *= num;
			vector = new Vector3(vector.x + GroundBG.transform.localPosition.x, vector.y, vector.z + GroundBG.transform.localPosition.y);
			FielderIcon[10].transform.localPosition = new Vector3(vector.x, vector.z, 0f);
		}
	}

	public void Hide(bool boolean)
	{
		if (boolean)
		{
			previewScreen.SetActive(value: false);
		}
		else
		{
			previewScreen.SetActive(value: true);
		}
	}
}
