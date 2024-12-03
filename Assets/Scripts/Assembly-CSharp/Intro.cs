using UnityEngine;

public class Intro : Singleton<Intro>
{
	private float animTime = 3f;

	private int introAction = -1;

	private float introTime;

	private bool firstTimeCalled;

	public Camera cutScene;

	public Camera tempCutScene;

	private float time;

	private Camera introCamera;

	private Animation m_camAnimation;

	private GameObject groundCenterPoint;

	private GameObject introCameraPivot;

	private GameObject Striker;

	private GameObject NonStriker;

	private GameObject batsmanExit;

	private int[] randomWarmUpAnimation = new int[9];

	private Transform introCameraTransform;

	private Transform groundCenterPointTransform;

	private Transform introCameraPivotTransform;

	private Transform StrikerTransform;

	private Transform NonStrikerTransform;

	private Transform batsmanExitTransform;

	private Transform batsmanRefPosition;

	public GameObject groundModel;

	public GameObject quad;

	private GroundController GroundControllerScript;

	private float batsmanEntryWalkingSpeed = 1.5f;

	private float batsmanExitWalkingSpeed = 0.9f;

	protected void Awake()
	{
		m_camAnimation = tempCutScene.GetComponent<Animation>();
		GroundControllerScript = GameObject.Find("GroundController").GetComponent<GroundController>();
		groundCenterPoint = GameObject.Find("GroundCenterPoint");
		groundCenterPointTransform = groundCenterPoint.transform;
		introCameraPivot = GameObject.Find("IntroCameraPivot");
		introCameraPivotTransform = introCameraPivot.transform;
		introCamera = GameObject.Find("IntroCamera").GetComponent("Camera") as Camera;
		introCameraTransform = GameObject.Find("IntroCamera").transform;
		batsmanRefPosition = GameObject.Find("Batsman/Armature/BatsmanRefPoint").transform;
	}

	public void UpdateIntro()
	{
		time += Time.deltaTime;
		Singleton<GameModel>.instance.CanPauseGame = false;
		if (introAction != 0 && introAction != 1 && introAction != 3)
		{
			if (introAction == 2 || introAction == 6)
			{
				if (StrikerTransform.localScale.x == 1f)
				{
					introCameraTransform.localPosition += new Vector3(Time.deltaTime * 1.8f, 0f, Time.deltaTime * -3.5f);
				}
				else
				{
					introCameraTransform.localPosition += new Vector3(Time.deltaTime * -1.8f, 0f, Time.deltaTime * -3.5f);
				}
			}
			else if (introAction == 4)
			{
				introCameraTransform.localPosition += new Vector3(Time.deltaTime * batsmanExitWalkingSpeed / 2f, 0f, Time.deltaTime * batsmanExitWalkingSpeed);
			}
			else if (introAction == 5)
			{
				introCameraTransform.localPosition -= new Vector3(0f, 0f, Time.deltaTime * (0f - batsmanExitWalkingSpeed) * 1.7f);
			}
		}
		if (introTime + animTime < Time.time)
		{
			if (introAction == 0)
			{
				zoomCameraToStriker();
			}
			else if (introAction == 1)
			{
				moveCameraToNonStriker();
			}
			else if (introAction == 2)
			{
				focussedOnNonStriker();
			}
			else if (introAction == 3)
			{
				introCompleted();
			}
			else if (introAction == 4)
			{
				batsmanExitStopped();
			}
			else if (introAction == 5)
			{
				batsmanEntryStopped();
			}
		}
	}

	private void WarmUpAnims()
	{
		for (int i = 0; i <= 8; i++)
		{
			randomWarmUpAnimation[i] = Random.Range(1, 6);
		}
		firstTimeCalled = true;
	}

	public void initGameIntro()
	{
		Cutscenes.instance.PlayCutscene("Intro");
		if (GroundControllerScript != null)
		{
			GroundControllerScript.initGameIntro();
		}
		introTime = Time.time;
		animTime = 5f;
		introAction = 0;
		Singleton<GroundController>.instance.StartIntroFielderAnimation();
	}

	private void zoomCameraToStriker()
	{
		cutScene.enabled = false;
		tempCutScene.enabled = true;
		m_camAnimation.Play("TempIntro");
		groundModel.transform.eulerAngles = new Vector3(0f, -272.5f, 0f);
		quad.SetActive(value: false);
		introAction = 1;
		animTime = 2f;
		introTime = Time.time;
		Singleton<GroundController>.instance.StopIntroFielderAnimation();
		Striker = Singleton<GroundController>.instance.batsman;
		StrikerTransform = Striker.transform;
		Striker.transform.position = new Vector3(-60f, 0f, -6f);
		Striker.transform.eulerAngles = new Vector3(StrikerTransform.eulerAngles.x, 90f, StrikerTransform.eulerAngles.z);
		int num = Random.Range(1, 4);
		int num2 = 4 - num;
		string animation = "WCCLite_BatsmanIntro01";
		string animation2 = "WCCLite_BatsmanIntro02";
		float length = Striker.GetComponent<Animation>()[animation].length;
		int num3 = (int)Random.Range(0f, length);
		Striker.GetComponent<Animation>().Play(animation);
		Striker.GetComponent<Animation>()[animation].time = 0f;
		NonStriker = Singleton<GroundController>.instance.runner;
		NonStrikerTransform = NonStriker.transform;
		NonStrikerTransform.position = new Vector3(-67f, 0f, -8f);
		NonStrikerTransform.eulerAngles = new Vector3(NonStrikerTransform.eulerAngles.x, 90f, NonStrikerTransform.eulerAngles.z);
		length = NonStriker.GetComponent<Animation>()[animation2].length;
		num3 = (int)Random.Range(0f, length);
		NonStriker.GetComponent<Animation>().Play(animation2);
		NonStriker.GetComponent<Animation>()[animation2].time = 0f;
		//CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.StrikerIndex].BatsmanList.Status = "not out";
		//if (CONTROLLER.PlayModeSelected == 7)
		//{
		//	TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex, "not out");
		//}
		Singleton<BatsmanRecord>.instance.UpdateRecord(CONTROLLER.BattingTeamIndex, CONTROLLER.StrikerIndex);
		Singleton<BatsmanRecord>.instance.Hide(boolean: false);
		introCameraTransform.parent = StrikerTransform;
		if (StrikerTransform.localScale.x == 1f)
		{
			introCameraTransform.localPosition = new Vector3(7f, 0.8f, -0.05f);
			introCameraTransform.localEulerAngles = new Vector3(4f, 190f, 0f);
		}
		else
		{
			introCameraTransform.localPosition = new Vector3(-7f, 0.8f, -0.05f);
			introCameraTransform.localEulerAngles = new Vector3(4f, 190f, 0f);
		}
	}

	private void moveCameraToNonStriker()
	{
		introAction = 2;
		introTime = Time.time;
		animTime = 1f;
		Singleton<BatsmanRecord>.instance.Hide(boolean: true);
	}

	private void focussedOnNonStriker()
	{
		introAction = 3;
		introTime = Time.time;
		animTime = 2f;
		CONTROLLER.TeamList[CONTROLLER.BattingTeamIndex].PlayerList[CONTROLLER.NonStrikerIndex].BatsmanList.Status = "not out";
		if (CONTROLLER.PlayModeSelected == 7)
		{
			TestMatchBatsman.SetStatus(CONTROLLER.BattingTeamIndex, CONTROLLER.NonStrikerIndex, "not out");
		}
		Singleton<BatsmanRecord>.instance.UpdateRecord(CONTROLLER.BattingTeamIndex, CONTROLLER.NonStrikerIndex);
		Singleton<BatsmanRecord>.instance.Hide(boolean: false);
	}

	private void introCompleted()
	{
		tempCutScene.enabled = false;
		introAction = 6;
		if (Singleton<GameModel>.instance != null)
		{
			Singleton<GameModel>.instance.introCompleted();
		}
		groundModel.transform.eulerAngles = Vector3.zero;
		quad.SetActive(value: true);
		firstTimeCalled = false;
	}

	public void initBatsmanExit()
	{
		introAction = 4;
		introTime = Time.time;
		animTime = 2.5f;
		batsmanExit = GroundControllerScript.batsman;
		batsmanExitTransform = batsmanExit.transform;
		batsmanExitTransform.position = new Vector3(-50f, 0f, -8f);
		batsmanExitTransform.eulerAngles = new Vector3(batsmanExitTransform.eulerAngles.x, 270f, batsmanExitTransform.eulerAngles.z);
		if (GroundControllerScript != null)
		{
			GroundControllerScript.EnableFielders(boolean: false);
			GroundControllerScript.initBatsmanExit();
		}
		batsmanExit.GetComponent<Animation>().Play("WCCLite_BatsmanExit");
		batsmanExit.GetComponent<Animation>()["WCCLite_BatsmanExit"].time = 0f;
		introCameraTransform.parent = batsmanExitTransform;
		introCameraTransform.localPosition = new Vector3(-1f, 2f, 4f);
		introCameraTransform.localEulerAngles = new Vector3(15f, 180f, 0f);
		introCamera.fieldOfView = 40f;
		introCamera.enabled = true;
	}

	public void batsmanExitStopped()
	{
		if (Singleton<GameModel>.instance != null)
		{
			Singleton<GameModel>.instance.batsmanExitStopped();
		}
	}

	public void initBatsmanEntry()
	{
		introAction = 5;
		introTime = Time.time;
		animTime = 2.5f;
		Striker = GroundControllerScript.batsman;
		StrikerTransform = Striker.transform;
		StrikerTransform.position = new Vector3(-67f, 0f, -8f);
		StrikerTransform.eulerAngles = new Vector3(StrikerTransform.eulerAngles.x, 90f, StrikerTransform.eulerAngles.z);
		groundModel.transform.eulerAngles = new Vector3(0f, -272.5f, 0f);
		quad.SetActive(value: false);
		int num = Random.Range(0, 3);
		string animation = ((num <= 1) ? "WCCLite_BatsmanIntro01" : ((num > 2) ? "WCCLite_BatsmanIntro03" : "WCCLite_BatsmanIntro02"));
		float length = Striker.GetComponent<Animation>()[animation].length;
		int num2 = (int)Random.Range(0f, length);
		Striker.GetComponent<Animation>().Play(animation);
		Striker.GetComponent<Animation>()[animation].time = 1f;
		introCameraTransform.parent = StrikerTransform;
		introCameraTransform.localPosition = new Vector3(-7f, 5.5f, -6f);
		introCameraTransform.localEulerAngles = new Vector3(20f, 50f, 0f);
	}

	public void batsmanEntryStopped()
	{
		introAction = -1;
		if (Singleton<GameModel>.instance != null)
		{
			Singleton<GameModel>.instance.batsmanEntryStopped();
		}
	}

	public void destroyGO()
	{
		introAction = -1;
		introCameraTransform.parent = null;
		if (Striker != null)
		{
			Striker = null;
		}
		if (NonStriker != null)
		{
			NonStriker = null;
		}
		if (batsmanExit != null)
		{
			batsmanExit = null;
		}
	}
}
