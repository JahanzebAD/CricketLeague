using UnityEngine;
using UnityEngine.UI;

public class TemporaryStore : Singleton<TemporaryStore>
{
	private int index;

	public Text RecommendedTicketText;

	public Text RecommendedCoinText;

	public Text VideoPackTicketText;

	public GameObject Holder;

	private int[] TicketsPack = new int[4] { 10, 75, 270, 1000 };

	public void GotoButton()
	{
		HideMe();
		//if (Singleton<AdIntegrate>.instance.CheckForInternet())
		//{
		//	Singleton<Store>.instance.ShowMe();
		//	return;
		//}
		CONTROLLER.PopupName = "noInternet";
		Singleton<Popups>.instance.ShowMe();
	}

	public void CancelButton()
	{
		Singleton<NavigationBack>.instance.deviceBack = Singleton<NavigationBack>.instance.tempDeviceBack;
		HideMe();
	}

	public void RecommendedPackButtonClicked()
	{
		HideMe();
		Singleton<Store>.instance.OpenConfirmationPopupTicket(index);
	}

	public void ShowMe(int index)
	{
		RecommendedTicketText.text = Singleton<Store>.instance.TicketsPack[index].ToString();
		RecommendedCoinText.text = Singleton<Store>.instance.CoinsPrice[index].ToString();
		Holder.SetActive(value: true);
	}

	private void HideMe()
	{
		Holder.SetActive(value: false);
	}

	public int CalculateIndexValue(int ticketAmountNeeded)
	{
		int i = 0;
		int num = 0;
		for (num = ticketAmountNeeded - CONTROLLER.tickets; TicketsPack[i] < num; i++)
		{
		}
		index = i;
		return i;
	}

	//public void WatchVideo()
	//{
	//	Singleton<FreeRewards>.instance.ShowMe(3);
	//}
}
