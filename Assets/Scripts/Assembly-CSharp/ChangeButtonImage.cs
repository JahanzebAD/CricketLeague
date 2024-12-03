using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeButtonImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IEventSystemHandler
{
	private Sprite defaultButton;

	public Sprite changeButton;

	private void Start()
	{
		defaultButton = GetComponent<Image>().sprite;
	}

	private void Update()
	{
	}

	public virtual void OnPointerEnter(PointerEventData point)
	{
		GetComponent<Image>().sprite = changeButton;
	}

	public virtual void OnPointerExit(PointerEventData point)
	{
		GetComponent<Image>().sprite = defaultButton;
	}

	public virtual void OnPointerClick(PointerEventData point)
	{
		GetComponent<Image>().sprite = defaultButton;
	}
}
