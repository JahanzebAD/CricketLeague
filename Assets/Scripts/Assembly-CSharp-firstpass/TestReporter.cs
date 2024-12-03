using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestReporter : MonoBehaviour
{
	public int logTestCount = 100;

	public int threadLogTestCount = 100;

	public bool logEverySecond = true;

	private int currentLogTestCount;

	private Reporter reporter;

	private GUIStyle style;

	private Rect rect1;

	private Rect rect2;

	private Rect rect3;

	private Rect rect4;

	private Rect rect5;

	private Rect rect6;

	private Thread thread;

	private float elapsed;

	private void Start()
	{
		Application.runInBackground = true;
		reporter = Object.FindObjectOfType(typeof(Reporter)) as Reporter;
		style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		style.normal.textColor = Color.white;
		style.wordWrap = true;
		for (int i = 0; i < 10; i++)
		{
		}
		for (int j = 0; j < 10; j++)
		{
		}
		rect1 = new Rect(Screen.width / 2 - 120, Screen.height / 2 - 225, 240f, 50f);
		rect2 = new Rect(Screen.width / 2 - 120, Screen.height / 2 - 175, 240f, 100f);
		rect3 = new Rect(Screen.width / 2 - 120, Screen.height / 2 - 50, 240f, 50f);
		rect4 = new Rect(Screen.width / 2 - 120, Screen.height / 2, 240f, 50f);
		rect5 = new Rect(Screen.width / 2 - 120, Screen.height / 2 + 50, 240f, 50f);
		rect6 = new Rect(Screen.width / 2 - 120, Screen.height / 2 + 100, 240f, 50f);
		thread = new Thread(threadLogTest);
		thread.Start();
	}

	private void OnDestroy()
	{
		thread.Abort();
	}

	private void threadLogTest()
	{
		for (int i = 0; i < threadLogTestCount; i++)
		{
		}
	}

	private void Update()
	{
		int num = 0;
		while (currentLogTestCount < logTestCount && num < 10)
		{
			num++;
			currentLogTestCount++;
		}
		elapsed += Time.deltaTime;
		if (elapsed >= 1f)
		{
			elapsed = 0f;
		}
	}

	private void OnGUI()
	{
		if ((bool)reporter && !reporter.show)
		{
			GUI.Label(rect1, "Draw circle on screen to show logs", style);
			GUI.Label(rect2, "To use Reporter just create reporter from reporter menu at first scene your game start", style);
			if (GUI.Button(rect3, "Load ReporterScene"))
			{
				SceneManager.LoadScene("ReporterScene");
			}
			if (GUI.Button(rect4, "Load test1"))
			{
				SceneManager.LoadScene("test1");
			}
			if (GUI.Button(rect5, "Load test2"))
			{
				SceneManager.LoadScene("test2");
			}
			GUI.Label(rect6, "fps : " + reporter.fps.ToString("0.0"), style);
		}
	}
}
