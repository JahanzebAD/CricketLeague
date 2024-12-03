using UnityEngine;
using UnityEngine.UI;

public class ChangeToggleImage : MonoBehaviour
{
	public Image Toggle1;

	public Image Toggle2;

	public Sprite _Enable;

	public Sprite _Disable;

	public Text Text1;

	public Text Text2;

	public Text Text3;

	public Text Text4;

	public void roomToggleChangeEvent(int type)
	{
		if (type == 0)
		{
			Toggle1.sprite = _Enable;
			Toggle2.sprite = _Disable;
			Text1.color = Color.black;
			Text2.color = Color.white;
			if (Text3 != null)
			{
				Text3.color = Color.black;
			}
			if (Text4 != null)
			{
				Text4.color = Color.white;
			}
		}
		else
		{
			Toggle1.sprite = _Disable;
			Toggle2.sprite = _Enable;
			Text1.color = Color.white;
			Text2.color = Color.black;
			if (Text3 != null)
			{
				Text3.color = Color.white;
			}
			if (Text4 != null)
			{
				Text4.color = Color.black;
			}
		}
	}
}
