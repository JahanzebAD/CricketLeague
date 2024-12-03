using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene
{
	public static string activeSceneName()
	{
		return SceneManager.GetActiveScene().name;
	}

	public static void loadScene(string _sceneName)
	{
		SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
	}

	public static AsyncOperation loadSceneAsync(string _sceneName)
	{
		return SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Single);
	}
}
