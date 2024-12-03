using UnityEngine;

public class Cutscenes : CutsceneManager
{
	public static Cutscenes instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		if (instance != this)
		{
			Object.DestroyImmediate(base.gameObject);
		}
	}

	private void onIntroEnded()
	{
		if (currentCutscene == "Intro" && Singleton<GameModel>.instance != null)
		{
			Singleton<GameModel>.instance.introCompleted();
		}
	}
}
