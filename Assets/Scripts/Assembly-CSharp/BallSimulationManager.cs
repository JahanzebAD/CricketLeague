using System;
using UnityEngine;

public class BallSimulationManager : Singleton<BallSimulationManager>
{
	private bool enable = true;

	public Transform simulatedBallHolder;

	public Texture[] ballTextures;

	private Transform[] simulatedBall;

	private TrailRenderer[] simulatedBallTrial;

	private float[] simulatedBallAngle;

	private float[] simulatedBallHorizontalSpeed;

	private float[] simulatedBallProjectileHeight;

	private float[] simulatedBallProjectileAngle;

	private float[] simulatedBallSpotLength;

	private int[] simulatedBallNoOfbounce;

	private float[] simulatedballProjectileAnglePerSecond;

	private float[] simulatedswingProjectileAnglePerSecond;

	private float[] simulatedSwingProjectileAngle;

	private float[] simulatedBallSwingValue;

	private float[] simulatedBallSpinValue;

	private string currentBowlerType;

	private string currentBowlerHand;

	private string[] bowlerSide;

	private string[] ballScoreData;

	public bool startSimulation;

	private bool ballSimulationSkipped;

	public bool retriveSavedData;

	public bool settedSimulationData;

	public int noOfBallDataSetted;

	public int noOfSimulatedBalls;

	public int currentSimulationBallIndex;

	private Vector3 ballOriginPosition = Vector3.zero;

	private float[] tempData;

	public bool showingBallSimulation;

	private float ballSpinningSpeedInX;

	private float ballSpinningSpeedInZ;

	private float DEG2RAD = (float)Math.PI / 180f;

	private float ballRadius = 0.05f;

	private int i;

	private void Start()
	{
		InitializeVariables();
		Singleton<BallSimulationCameraScripts>.instance.InitializeCamera();
		ShowAllBall(state: false);
	}

	private void SetTestingData()
	{
		for (i = 0; i < noOfSimulatedBalls; i++)
		{
			if (i == 1)
			{
				SetTempData(93.4f, 16.6f, 2.15f, 14f, 2f, 2f);
				SetBallSimulationData("six", "spin", "left", "left");
			}
			else if (i == 2)
			{
				SetTempData(93.4f, 16.6f, 2.15f, 14f, 2f, 2f);
				SetBallSimulationData("six", "spin", "left", "right");
			}
			else if (i == 0)
			{
				SetTempData(93.4f, 16.6f, 2.15f, 14f, 2f, 2f);
				SetBallSimulationData("six", "spin", "right", "left");
			}
			else
			{
				SetTempData(93.4f, 16.6f, 2.15f, 14f, 2f, 2f);
				SetBallSimulationData("six", "spin", "right", "right");
			}
		}
	}

	public void InitializeVariables()
	{
		noOfSimulatedBalls = simulatedBallHolder.childCount;
		simulatedBall = new Transform[noOfSimulatedBalls];
		for (i = 0; i < noOfSimulatedBalls; i++)
		{
			simulatedBall[i] = simulatedBallHolder.GetChild(i);
		}
		simulatedBallAngle = new float[noOfSimulatedBalls];
		simulatedBallHorizontalSpeed = new float[noOfSimulatedBalls];
		simulatedBallProjectileHeight = new float[noOfSimulatedBalls];
		simulatedBallSpotLength = new float[noOfSimulatedBalls];
		simulatedBallNoOfbounce = new int[noOfSimulatedBalls];
		simulatedBallProjectileAngle = new float[noOfSimulatedBalls];
		simulatedballProjectileAnglePerSecond = new float[noOfSimulatedBalls];
		simulatedswingProjectileAnglePerSecond = new float[noOfSimulatedBalls];
		simulatedSwingProjectileAngle = new float[noOfSimulatedBalls];
		simulatedBallSwingValue = new float[noOfSimulatedBalls];
		simulatedBallSpinValue = new float[noOfSimulatedBalls];
		ballScoreData = new string[noOfSimulatedBalls];
		bowlerSide = new string[noOfSimulatedBalls];
		simulatedBallTrial = new TrailRenderer[noOfSimulatedBalls];
		for (i = 0; i < simulatedBallTrial.Length; i++)
		{
			simulatedBallTrial[i] = simulatedBall[i].GetComponent<TrailRenderer>();
			simulatedBallTrial[i].time = 300f;
			if (CONTROLLER.PlayModeSelected == 7)
			{
				simulatedBall[i].GetComponent<Renderer>().material.mainTexture = ballTextures[0];
			}
			else
			{
				simulatedBall[i].GetComponent<Renderer>().material.mainTexture = ballTextures[1];
			}
		}
		ballOriginPosition = simulatedBall[0].position;
		currentSimulationBallIndex = 0;
		tempData = new float[7];
	}

	public void StartBallSimulation()
	{
		currentSimulationBallIndex = 0;
		ShowAllBall(state: true);
		showingBallSimulation = true;
		ClearAllSaveData();
		Singleton<GroundController>.instance.Stump1AnimationComponent.Play("idle");
		Singleton<GroundController>.instance.Stump2AnimationComponent.Play("idle");
		Singleton<GameModel>.instance.SkipObj.SetActive(value: true);
		for (i = 0; i < noOfBallDataSetted; i++)
		{
			simulatedBallProjectileAngle[i] = 270f;
			SetBallOrigin(i);
			SetProjectileAnglePerSecond(i);
		}
		SetTrailColors();
		Time.timeScale = 0.7f;
		Singleton<BallSimulationCameraScripts>.instance.StartBallSimulationCamera();
	}

	public void StartBallMovement()
	{
		startSimulation = true;
	}

	private void ShowAllBall(bool state)
	{
		for (i = 0; i < noOfSimulatedBalls; i++)
		{
			simulatedBall[i].gameObject.SetActive(state);
			simulatedBallNoOfbounce[i] = 0;
			if (!state)
			{
				simulatedBallTrial[i].Clear();
			}
		}
	}

	public void BallSimulationCompleted()
	{
		startSimulation = false;
		settedSimulationData = false;
		currentSimulationBallIndex = 0;
		noOfBallDataSetted = 0;
		ShowAllBall(state: false);
		Singleton<BallSimulationCameraScripts>.instance.EnableCamera(state: false);
		ballSimulationSkipped = false;
		showingBallSimulation = false;
		Singleton<GameModel>.instance.SkipObj.SetActive(value: false);
		Singleton<GameModel>.instance.ShowScoreCard();
	}

	public void BallSimulationSkipped()
	{
		ballSimulationSkipped = true;
		Singleton<BallSimulationCameraScripts>.instance.skip();
		BallSimulationCompleted();
	}

	public bool CanShowBallSimulation()
	{
		if (CONTROLLER.myTeamIndex == CONTROLLER.BowlingTeamIndex && enable)
		{
			return true;
		}
		return false;
	}

	public void SetTempData(float ballAngleL, float horizontalSpeedL, float projectileHeightL, float spotLengthL, float swingValueL, float spinValueL)
	{
		tempData[0] = ballAngleL;
		tempData[1] = horizontalSpeedL;
		tempData[2] = projectileHeightL;
		tempData[3] = spotLengthL;
		tempData[4] = spinValueL;
		tempData[5] = swingValueL;
	}

	public void SetBallSimulationData(string data, string currentBowlerTypeL, string bowlerSideL, string currentBowlerHandL)
	{
		if (noOfBallDataSetted == 0)
		{
			currentSimulationBallIndex = 0;
		}
		if (retriveSavedData)
		{
			retriveSavedData = false;
			RetriveBallSimulationData();
		}
		simulatedBallAngle[currentSimulationBallIndex] = tempData[0];
		simulatedBallHorizontalSpeed[currentSimulationBallIndex] = tempData[1];
		simulatedBallProjectileHeight[currentSimulationBallIndex] = tempData[2];
		simulatedBallSpotLength[currentSimulationBallIndex] = tempData[3];
		simulatedBallSpinValue[currentSimulationBallIndex] = tempData[4];
		simulatedBallSwingValue[currentSimulationBallIndex] = tempData[5];
		currentBowlerType = currentBowlerTypeL;
		ballScoreData[currentSimulationBallIndex] = data;
		currentBowlerHand = currentBowlerHandL;
		bowlerSide[currentSimulationBallIndex] = bowlerSideL;
		if (noOfBallDataSetted > 4)
		{
			settedSimulationData = true;
		}
		noOfBallDataSetted++;
		SaveBallSimulationData();
		currentSimulationBallIndex++;
	}

	private void SaveBallSimulationData()
	{
		AutoSave.simulatedBallAngle = AutoSave.simulatedBallAngle + simulatedBallAngle[currentSimulationBallIndex] + "|";
		AutoSave.simulatedBallHorizontalSpeed = AutoSave.simulatedBallHorizontalSpeed + simulatedBallHorizontalSpeed[currentSimulationBallIndex] + "|";
		AutoSave.simulatedBallProjectileHeight = AutoSave.simulatedBallProjectileHeight + simulatedBallProjectileHeight[currentSimulationBallIndex] + "|";
		AutoSave.simulatedBallSpotLength = AutoSave.simulatedBallSpotLength + simulatedBallSpotLength[currentSimulationBallIndex] + "|";
		AutoSave.simulatedBallSpinValue = AutoSave.simulatedBallSpinValue + simulatedBallSpinValue[currentSimulationBallIndex] + "|";
		AutoSave.simulatedBallSwingValue = AutoSave.simulatedBallSwingValue + simulatedBallSwingValue[currentSimulationBallIndex] + "|";
		AutoSave.currentBowlerType = currentBowlerType;
		AutoSave.ballScoreData = AutoSave.ballScoreData + ballScoreData[currentSimulationBallIndex] + "|";
		AutoSave.currentBowlerHand = currentBowlerHand;
		AutoSave.bowlerSide = AutoSave.bowlerSide + bowlerSide[currentSimulationBallIndex] + "|";
		if (noOfBallDataSetted < 6)
		{
			AutoSave.noOfBallDataSetted = noOfBallDataSetted.ToString();
		}
		else
		{
			AutoSave.noOfBallDataSetted = "0";
		}
	}

	public void RetriveBallSimulationData()
	{
		noOfBallDataSetted = int.Parse(AutoSave.noOfBallDataSetted);
		if (noOfBallDataSetted != 0)
		{
			string[] array = new string[6];
			array = AutoSave.simulatedBallAngle.Split('|');
			for (i = 0; i < array.Length - 1; i++)
			{
				simulatedBallAngle[i] = float.Parse(array[i]);
			}
			array = AutoSave.simulatedBallHorizontalSpeed.Split('|');
			for (i = 0; i < array.Length - 1; i++)
			{
				simulatedBallHorizontalSpeed[i] = float.Parse(array[i]);
			}
			array = AutoSave.simulatedBallProjectileHeight.Split('|');
			for (i = 0; i < array.Length - 1; i++)
			{
				simulatedBallProjectileHeight[i] = float.Parse(array[i]);
			}
			array = AutoSave.simulatedBallSpotLength.Split('|');
			for (i = 0; i < array.Length - 1; i++)
			{
				simulatedBallSpotLength[i] = float.Parse(array[i]);
			}
			array = AutoSave.simulatedBallSpinValue.Split('|');
			for (i = 0; i < array.Length - 1; i++)
			{
				simulatedBallSpinValue[i] = float.Parse(array[i]);
			}
			array = AutoSave.simulatedBallSwingValue.Split('|');
			for (i = 0; i < array.Length - 1; i++)
			{
				simulatedBallSwingValue[i] = float.Parse(array[i]);
			}
			array = AutoSave.ballScoreData.Split('|');
			for (i = 0; i < array.Length - 1; i++)
			{
				ballScoreData[i] = array[i];
			}
			array = AutoSave.bowlerSide.Split('|');
			for (i = 0; i < array.Length - 1; i++)
			{
				bowlerSide[i] = array[i];
			}
			currentBowlerHand = AutoSave.currentBowlerHand;
			currentBowlerType = AutoSave.currentBowlerType;
			currentSimulationBallIndex = noOfBallDataSetted;
		}
	}

	public void ClearAllSaveData()
	{
		AutoSave.simulatedBallAngle = string.Empty;
		AutoSave.simulatedBallHorizontalSpeed = string.Empty;
		AutoSave.simulatedBallProjectileHeight = string.Empty;
		AutoSave.simulatedBallSpotLength = string.Empty;
		AutoSave.simulatedBallSpinValue = string.Empty;
		AutoSave.simulatedBallSwingValue = string.Empty;
		AutoSave.currentBowlerType = string.Empty;
		AutoSave.ballScoreData = string.Empty;
		AutoSave.currentBowlerHand = string.Empty;
		AutoSave.bowlerSide = string.Empty;
		AutoSave.noOfBallDataSetted = "0";
		AutoSave.SaveInGameMatch();
	}

	private void SetTrailColors()
	{
		for (i = 0; i < ballScoreData.Length; i++)
		{
			switch (ballScoreData[i])
			{
			case "dot":
				simulatedBallTrial[i].material.color = Color.green;
				break;
			case "run":
				simulatedBallTrial[i].material.color = Color.yellow;
				break;
			case "four":
				simulatedBallTrial[i].material.color = Color.magenta;
				break;
			case "six":
				simulatedBallTrial[i].material.color = Color.red;
				break;
			case "wicket":
				simulatedBallTrial[i].material.color = Color.white;
				break;
			}
		}
	}

	private void SetBallOrigin(int index)
	{
		if (bowlerSide[index] == "left")
		{
			if (currentBowlerHand == "left")
			{
				simulatedBall[index].position = new Vector3(-0.89f, ballOriginPosition.y, ballOriginPosition.z);
			}
			else
			{
				simulatedBall[index].position = new Vector3(-0.7f, ballOriginPosition.y, ballOriginPosition.z);
			}
		}
		else if (currentBowlerHand == "left")
		{
			simulatedBall[index].position = new Vector3(0.7f, ballOriginPosition.y, ballOriginPosition.z);
		}
		else
		{
			simulatedBall[index].position = new Vector3(0.91f, ballOriginPosition.y, ballOriginPosition.z);
		}
	}

	private void Update()
	{
		if (!startSimulation)
		{
			return;
		}
		if (Input.GetMouseButtonDown(0))
		{
			BallSimulationSkipped();
		}
		if (simulatedBall[currentSimulationBallIndex].transform.position.z <= 9.95f)
		{
			BowlingBallMovement(currentSimulationBallIndex);
		}
		else
		{
			currentSimulationBallIndex++;
			if (currentSimulationBallIndex > noOfBallDataSetted - 1)
			{
				startSimulation = false;
			}
		}
		Singleton<GameModel>.instance.BlinkActionReplay();
	}

	public void BowlingBallMovement(int ballIndex)
	{
		BallMovement(ballIndex);
		BallSwingMovement(ballIndex);
		if (!(simulatedBallProjectileAngle[ballIndex] >= 360f))
		{
			return;
		}
		simulatedBallNoOfbounce[ballIndex]++;
		simulatedBallProjectileAngle[ballIndex] = 180f;
		simulatedballProjectileAnglePerSecond[ballIndex] *= 1.1f;
		simulatedBallProjectileHeight[ballIndex] *= 0.6f;
		ballSpinningSpeedInX = UnityEngine.Random.Range(-3600, -1800);
		ballSpinningSpeedInZ = UnityEngine.Random.Range(-3600, -1800);
		if (simulatedBallNoOfbounce[ballIndex] == 1)
		{
			if (currentBowlerType == "spin")
			{
				simulatedBallAngle[ballIndex] += simulatedBallSpinValue[ballIndex];
			}
			else if (currentBowlerType == "fast" && simulatedBallSwingValue[ballIndex] != 0f)
			{
				simulatedBallAngle[ballIndex] += simulatedBallSpinValue[ballIndex];
				simulatedBallSwingValue[ballIndex] = 0f;
			}
			else if (currentBowlerType == "medium" && simulatedBallSwingValue[ballIndex] != 0f)
			{
				simulatedBallAngle[ballIndex] += simulatedBallSpinValue[ballIndex];
				simulatedBallSwingValue[ballIndex] = 0f;
			}
		}
	}

	public void SetProjectileAnglePerSecond(int ballIndex)
	{
		simulatedballProjectileAnglePerSecond[ballIndex] = 90f / simulatedBallSpotLength[ballIndex] * simulatedBallHorizontalSpeed[ballIndex];
		simulatedswingProjectileAnglePerSecond[ballIndex] = 180f / simulatedBallSpotLength[ballIndex] * simulatedBallHorizontalSpeed[ballIndex];
	}

	public void BallMovement(int ballIndex)
	{
		float x = Mathf.Cos(simulatedBallAngle[ballIndex] * DEG2RAD) * simulatedBallHorizontalSpeed[ballIndex] * Time.deltaTime;
		float z = Mathf.Sin(simulatedBallAngle[ballIndex] * DEG2RAD) * simulatedBallHorizontalSpeed[ballIndex] * Time.deltaTime;
		float num = Mathf.Sin(simulatedBallProjectileAngle[ballIndex] * DEG2RAD) * simulatedBallProjectileHeight[ballIndex] - ballRadius;
		if (float.IsNaN(num))
		{
			num = 0f;
		}
		simulatedBall[ballIndex].position = new Vector3(simulatedBall[ballIndex].position.x, 0f - num, simulatedBall[ballIndex].position.z);
		simulatedBall[ballIndex].position += new Vector3(x, 0f, z);
		simulatedBallProjectileAngle[ballIndex] += simulatedballProjectileAnglePerSecond[ballIndex] * Time.deltaTime;
		simulatedBall[ballIndex].Rotate(Vector3.right * Time.deltaTime * ballSpinningSpeedInX, Space.World);
		simulatedBall[ballIndex].Rotate(Vector3.forward * Time.deltaTime * ballSpinningSpeedInZ, Space.World);
	}

	private void BallSwingMovement(int ballIndex)
	{
		if (simulatedBallSwingValue[ballIndex] != 0f)
		{
			float x = Mathf.Cos(simulatedSwingProjectileAngle[ballIndex] * DEG2RAD) * simulatedBallSwingValue[ballIndex] * Time.deltaTime;
			simulatedSwingProjectileAngle[ballIndex] += simulatedswingProjectileAnglePerSecond[ballIndex] * Time.deltaTime;
			simulatedBall[ballIndex].position -= new Vector3(x, 0f, 0f);
		}
	}
}
