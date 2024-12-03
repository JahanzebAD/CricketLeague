using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTouchEvent : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	public virtual void OnPointerClick(PointerEventData data)
	{
		Singleton<EditPlayersTWO>.instance.Clicked();
	}
}
