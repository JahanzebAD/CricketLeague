using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	public GameObject[] button;

	public virtual void OnPointerDown(PointerEventData data)
	{
		button[0].transform.DOScale(Vector3.one * 0.9f, 0.1f);
		button[1].SetActive(value: true);
	}

	public virtual void OnPointerUp(PointerEventData data)
	{
		button[0].transform.DOScale(Vector3.one, 0.1f);
		button[1].SetActive(value: false);
	}
}
