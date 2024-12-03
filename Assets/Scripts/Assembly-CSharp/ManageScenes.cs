using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
	public static ManageScenes _instance;

	private string LoadedSceneName;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		LoadedSceneName = scene.name;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	public string getCurrentLoadedSceneName()
	{
		return LoadedSceneName;
	}
}
