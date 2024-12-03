using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpriteSwap : MonoBehaviour
{
	public Button[] buttons;

	public Image seletedbutton;

	public int previousSelectedBtn;

	public int currentSelectedBtn;

	public Sprite[] textures;

	private void Start()
	{
		previousSelectedBtn = (currentSelectedBtn = 0);
	}

	public void changeSprite(int index)
	{
		previousSelectedBtn = currentSelectedBtn;
		currentSelectedBtn = index;
		buttons[previousSelectedBtn].image.sprite = textures[0];
		buttons[currentSelectedBtn].image.sprite = textures[1];
		moveSelectedButton();
	}

	public void moveSelectedButton()
	{
		seletedbutton.transform.DOLocalMove(buttons[currentSelectedBtn].transform.localPosition, 0.4f, snapping: true);
	}
}
