using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPlayerPrefsTWO : Singleton<LoadPlayerPrefsTWO>
{
	public TextAsset xmlAsset;

	private BattingScoreCard BattingScoreCardScript;

	private BowlingScoreCard BowlingScoreCardScript;

	private PauseGameScreen PauseGameScreenScript;

	private GameOverScreen GameOverScreenScript;

	private MatchSummary MatchSummaryClass;

	private FreeHitValidation FreeHitValidationScript;

	private QuitConfirm QuitConfirmClass;

	protected void Awake()
	{
		if (SceneManager.GetActiveScene().name == "Preloader")
		{
			float num = 1.33333337f;
			float num2 = Screen.width;
			float num3 = Screen.height;
			float num4 = num2 / num3;
			CONTROLLER.xOffSet = (num - num4) * 600f / 2f;
			CONTROLLER.gameCompleted = false;
		}
		else if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			
		}
		else if (SceneManager.GetActiveScene().name == "Ground")
		{
			//PostGoogleAnalyticsEvent();
		}
		CONTROLLER.SceneIsLoading = false;
	}

	
	protected void Start()
	{
		if (CONTROLLER.TeamList == null)
		{
			InitializeGame();
			GetTeamList();
			getSettingsList();
			PurchaseValidation();
		}
	}

	private void InitializeGame()
	{
		if (Application.isEditor)
		{
			CONTROLLER.Overs[0] = 1;
		}
		else
		{
			CONTROLLER.Overs[0] = 2;
		}
		CONTROLLER.Overs[1] = 5;
		CONTROLLER.Overs[2] = 10;
		CONTROLLER.Overs[3] = 15;
		CONTROLLER.Overs[4] = 20;
		CONTROLLER.Overs[5] = 25;
		CONTROLLER.Overs[6] = 30;
	}

	public void getSettingsList()
	{
		if (PlayerPrefs.HasKey("Settings"))
		{
			string @string = PlayerPrefs.GetString("Settings");
			string[] array = @string.Split("|"[0]);
			CONTROLLER.bgMusicVal = int.Parse(array[0]);
			CONTROLLER.ambientVal = int.Parse(array[1]);
			CONTROLLER.menuBgVolume = float.Parse(array[2]);
			CONTROLLER.sfxVolume = float.Parse(array[3]);
			CONTROLLER.tutorialToggle = int.Parse(array[4]);
		}
		else
		{
			CONTROLLER.bgMusicVal = 1;
			CONTROLLER.ambientVal = 1;
			CONTROLLER.menuBgVolume = 1f;
			CONTROLLER.sfxVolume = 1f;
			CONTROLLER.tutorialToggle = 1;
			SavePlayerPrefs.SetSettingsList();
		}
		if (CONTROLLER.sndController != null)
		{
			CONTROLLER.sndController.bgMusicToggle();
			CONTROLLER.sndController.ambientToggle();
		}
	}

	public void PurchaseValidation()
	{
		if (PlayerPrefs.HasKey("NoOfTrails"))
		{
			CONTROLLER.noOfTrails = PlayerPrefs.GetInt("NoOfTrails");
			if (CONTROLLER.noOfTrails == -1)
			{
				CONTROLLER.isFreeVersion = false;
			}
			else
			{
				CONTROLLER.isFreeVersion = true;
			}
		}
		else
		{
			CONTROLLER.noOfTrails = CONTROLLER.maxTrails;
			PlayerPrefs.SetInt("NoOfTrails", CONTROLLER.maxTrails);
			CONTROLLER.isFreeVersion = true;
		}
		CONTROLLER.isFreeVersion = false;
	}

	public void GetTeamList()
	{
		XMLReader.Loader = base.gameObject;
		if (PlayerPrefs.HasKey("teamlist"))
		{
			string @string = PlayerPrefs.GetString("teamlist");
			XMLReader.ParseXML(@string);
		}
		else
		{
			XMLReader.ParseXML(xmlAsset.text);
		}
	}

	public void XMLLoaded()
	{
		Invoke("LoadMenuScene", 2f);
	}

	private void LoadMenuScene()
	{
		if (SceneManager.GetActiveScene().name == "Preloader")
		{
			CONTROLLER.SceneIsLoading = true;
			SceneManager.LoadScene("MainMenu");
		}
	}

	public void getTournamentList()
	{
		string text = " ";
		if (PlayerPrefs.HasKey("tour"))
		{
			text = PlayerPrefs.GetString("tour");
			string[] array = text.Split("&"[0]);
			CONTROLLER.TournamentStage = int.Parse(array[0]);
			CONTROLLER.myTeamIndex = int.Parse(array[1]);
			CONTROLLER.oversSelectedIndex = int.Parse(array[2]);
			CONTROLLER.matchIndex = int.Parse(array[3]);
			CONTROLLER.quaterFinalList = array[5];
			CONTROLLER.semiFinalList = array[6];
			CONTROLLER.finalList = array[7];
			CONTROLLER.tournamentStr = CONTROLLER.TournamentStage + "&" + CONTROLLER.myTeamIndex + "&" + CONTROLLER.oversSelectedIndex + "&" + CONTROLLER.matchIndex + "&" + array[4];
		}
	}

	protected void Update()
	{
		BackKeyInput();
	}

	private void BackKeyInput()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (SceneManager.GetActiveScene().name == "MainMenu")
			{
				Singleton<ButtonEvents>.instance.BackBtnClicked();
			}
			else if (!(SceneManager.GetActiveScene().name == "Ground"))
			{
			}
		}
	}
}
