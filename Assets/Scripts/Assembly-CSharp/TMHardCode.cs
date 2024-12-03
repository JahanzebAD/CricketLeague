using UnityEngine;
using UnityEngine.UI;

public class TMHardCode : Singleton<TMHardCode>
{
	public GameObject holder;

	public Text overs;

	public Text wickets;

	public void ChangeWickets(float x)
	{
		wickets.text = x.ToString();
		CONTROLLER.totalWickets = (int)x;
	}

	public void Close()
	{
		Time.timeScale = 1f;
		holder.SetActive(value: false);
	}
}
