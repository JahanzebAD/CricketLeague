using UnityEngine;

public class FreeHitValidation : Singleton<FreeHitValidation>
{
	public GameObject holder;

	protected void Start()
	{
		hideMe();
	}

	public void showMe()
	{
		holder.SetActive(value: true);
	}

	public void hideMe()
	{
		holder.SetActive(value: false);
	}
}
