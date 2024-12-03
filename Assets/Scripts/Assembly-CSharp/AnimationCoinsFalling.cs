using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnimationCoinsFalling : Singleton<AnimationCoinsFalling>
{
	public Transform[] ObjectTransform;

	public Transform StartPosition;

	public Sprite[] ObjectSprite;

	public void ResetAnimation()
	{
		for (int i = 0; i < ObjectTransform.Length; i++)
		{
			ObjectTransform[i].localPosition = StartPosition.localPosition;
			ObjectTransform[i].gameObject.SetActive(value: false);
		}
	}

	public void CheckForSpriteChange(int index)
	{
		if (index == 1 || index == 2 || index == 3)
		{
			for (int i = 0; i < ObjectTransform.Length; i++)
			{
				ObjectTransform[i].gameObject.GetComponent<Image>().sprite = ObjectSprite[0];
			}
		}
		if (index == 4 || index == 5 || index == 6)
		{
			for (int j = 0; j < ObjectTransform.Length; j++)
			{
				ObjectTransform[j].gameObject.GetComponent<Image>().sprite = ObjectSprite[1];
			}
		}
		if (index == 7 || index == 8 || index == 9)
		{
			for (int k = 0; k < ObjectTransform.Length; k++)
			{
				ObjectTransform[k].gameObject.GetComponent<Image>().sprite = ObjectSprite[2];
			}
		}
	}

	public void AnimationTransition(int index)
	{
		ResetAnimation();
		CheckForSpriteChange(index);
		float num = 0f;
		float num2 = 0.1f;
		Sequence s = DOTween.Sequence();
		switch (index)
		{
		case 0:
		{
			for (int k = 0; k < ObjectTransform.Length; k++)
			{
				ObjectTransform[k].gameObject.SetActive(value: false);
			}
			break;
		}
		case 1:
		{
			for (int j = 9; j < ObjectTransform.Length; j++)
			{
				ObjectTransform[j].gameObject.SetActive(value: true);
			}
			s.Insert(num + 6f * num2, ObjectTransform[9].DOLocalMove(new Vector3(13.1f, -22.4f, 0f), 0.25f));
			s.Insert(num + 7f * num2, ObjectTransform[10].DOLocalMove(new Vector3(9f, -11.4f, 0f), 0.25f));
			s.Insert(num + 8f * num2, ObjectTransform[11].DOLocalMove(new Vector3(13.1f, -0.6f, 0f), 0.25f));
			break;
		}
		case 2:
		{
			for (int l = 6; l < ObjectTransform.Length; l++)
			{
				ObjectTransform[l].gameObject.SetActive(value: true);
			}
			if (Singleton<SpinWheel>.instance.mainHolder.activeInHierarchy && ManageScene.activeSceneName() == "MainMenu")
			{
				s.Insert(num + 6f * num2, ObjectTransform[6].DOLocalMove(new Vector3(3.1f, 2.9f, 0f), 0.25f));
				s.Insert(num + 8f * num2, ObjectTransform[7].DOLocalMove(new Vector3(-0.4f, 13.3f, 0f), 0.25f));
				s.Insert(num + 10f * num2, ObjectTransform[8].DOLocalMove(new Vector3(1.5f, 25f, 0f), 0.25f));
				s.Insert(num + 7f * num2, ObjectTransform[9].DOLocalMove(new Vector3(-33.1f, -9.1f, 0f), 0.25f));
				s.Insert(num + 9f * num2, ObjectTransform[10].DOLocalMove(new Vector3(-37f, 3f, 0f), 0.25f));
				s.Insert(num + 11f * num2, ObjectTransform[11].DOLocalMove(new Vector3(-38.9f, 15f, 0f), 0.25f));
			}
			else
			{
				s.Insert(num + 2f * num2, ObjectTransform[6].DOLocalMove(new Vector3(-15.6f, -30.8f, 0f), 0.25f));
				s.Insert(num + 4f * num2, ObjectTransform[7].DOLocalMove(new Vector3(-23.1f, -9.7f, 0f), 0.25f));
				s.Insert(num + 6f * num2, ObjectTransform[8].DOLocalMove(new Vector3(-15.6f, 12.1f, 0f), 0.25f));
				s.Insert(num + 1f * num2, ObjectTransform[9].DOLocalMove(new Vector3(29.5f, -3.2f, 0f), 0.25f));
				s.Insert(num + 3f * num2, ObjectTransform[10].DOLocalMove(new Vector3(29.5f, 18.9f, 0f), 0.25f));
				s.Insert(num + 5f * num2, ObjectTransform[11].DOLocalMove(new Vector3(29.5f, 39.1f, 0f), 0.25f));
			}
			break;
		}
		case 3:
		{
			for (int i = 0; i < ObjectTransform.Length; i++)
			{
				ObjectTransform[i].gameObject.SetActive(value: true);
			}
			s.Insert(num + 1f * num2, ObjectTransform[0].DOLocalMove(new Vector3(32.6f, 7.4f, 0f), 0.25f));
			s.Insert(num + 5f * num2, ObjectTransform[1].DOLocalMove(new Vector3(27.1f, 17.6f, 0f), 0.25f));
			s.Insert(num + 9f * num2, ObjectTransform[2].DOLocalMove(new Vector3(30.5f, 28.1f, 0f), 0.25f));
			s.Insert(num + 2f * num2, ObjectTransform[3].DOLocalMove(new Vector3(3.1f, 2.9f, 0f), 0.25f));
			s.Insert(num + 6f * num2, ObjectTransform[4].DOLocalMove(new Vector3(-0.4f, 13.3f, 0f), 0.25f));
			s.Insert(num + 10f * num2, ObjectTransform[5].DOLocalMove(new Vector3(1.5f, 25f, 0f), 0.25f));
			s.Insert(num + 3f * num2, ObjectTransform[6].DOLocalMove(new Vector3(-33.1f, -9.1f, 0f), 0.25f));
			s.Insert(num + 7f * num2, ObjectTransform[7].DOLocalMove(new Vector3(-37f, 3f, 0f), 0.25f));
			s.Insert(num + 11f * num2, ObjectTransform[8].DOLocalMove(new Vector3(-38.9f, 15f, 0f), 0.25f));
			s.Insert(num + 4f * num2, ObjectTransform[9].DOLocalMove(new Vector3(13.1f, -22.4f, 0f), 0.25f));
			s.Insert(num + 8f * num2, ObjectTransform[10].DOLocalMove(new Vector3(9f, -11.4f, 0f), 0.25f));
			s.Insert(num + 12f * num2, ObjectTransform[11].DOLocalMove(new Vector3(13.1f, -0.6f, 0f), 0.25f));
			break;
		}
		}
	}
}
