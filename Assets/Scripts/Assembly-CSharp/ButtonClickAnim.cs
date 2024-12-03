using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClickAnim : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
{
	public Button button;

	public virtual void OnPointerDown(PointerEventData data)
	{
		button.transform.DOScale(Vector3.one * 0.9f, 0.1f);
	}

	public virtual void OnPointerUp(PointerEventData data)
	{
		button.transform.DOScale(Vector3.one, 0.1f);
	}
}
