using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeColor : Singleton<ChangeColor>, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	public Color changeTo;

	public Color original;

	public Text[] txt;

	public Color txtChange;

	public Color txtOriginal;

	public virtual void OnPointerDown(PointerEventData data)
	{
		base.gameObject.GetComponent<Image>().color = changeTo;
		if (txt.Length > 0)
		{
			for (int i = 0; i < txt.Length; i++)
			{
				txt[i].color = txtChange;
			}
		}
	}

	public virtual void OnPointerUp(PointerEventData data)
	{
		base.gameObject.GetComponent<Image>().color = original;
		if (txt.Length > 0)
		{
			for (int i = 0; i < txt.Length; i++)
			{
				txt[i].color = txtOriginal;
			}
		}
	}
}
