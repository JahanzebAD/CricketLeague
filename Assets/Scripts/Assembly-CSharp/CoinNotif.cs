using UnityEngine;
using UnityEngine.UI;

public class CoinNotif : Singleton<CoinNotif>
{
	public Text coinsGained;

	public Text coinStatus;

	public GameObject holder;

	public void ShowPopup(int amount, string detail)
	{
		holder.SetActive(value: true);
		coinsGained.text = amount.ToString();
		coinStatus.text = detail;
	}

	private void HidePopup()
	{
		holder.SetActive(value: false);
	}
}
