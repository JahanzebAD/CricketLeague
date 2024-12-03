using UnityEngine;
using UnityEngine.UI;

public class PreloaderScreen : Singleton<PreloaderScreen>
{
	public Text LoadingTxt;

	public GameObject RotateGo;

	private float rotationSpeed = 150f;

	protected void Awake()
	{
		CONTROLLER.CurrentMenu = string.Empty;
	}

	public void UpdateLoadingTxt(string str)
	{
		LoadingTxt.text = str;
	}

	protected void Update()
	{
		if (RotateGo != null)
		{
			RotateGo.transform.localEulerAngles = new Vector3(RotateGo.transform.localEulerAngles.x, RotateGo.transform.localEulerAngles.y, RotateGo.transform.localEulerAngles.z - rotationSpeed * Time.deltaTime);
		}
	}
}
