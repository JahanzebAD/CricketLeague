using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EmoticonHelper : MonoBehaviour
{
	public RawImage thisSprite;

	private string spriteName = string.Empty;

	private int currentLoopCount = -1;

	private int totalLoopCount = -1;

	private IEnumerator Start()
	{
		yield return new WaitForSeconds(0.2f);
		thisSprite.enabled = false;
	}

	public void PlayAnimation(string _spriteName, int _totalLoopCount)
	{
		if (ScoreBoardMultiPlayer.instance.EmoticonPanel.activeSelf)
		{
			StartCoroutine(AssignAnimationData(_spriteName, _totalLoopCount));
		}
	}

	private IEnumerator AssignAnimationData(string _spriteName, int _totalLoopCount)
	{
		if (currentLoopCount != -1)
		{
			yield return StartCoroutine("StopAnimation");
		}
		yield return new WaitForSeconds(0.1f);
		spriteName = _spriteName;
		totalLoopCount = _totalLoopCount;
		thisSprite.enabled = true;
		StartCoroutine("StartAnimation");
	}

	private IEnumerator StartAnimation()
	{
		for (int i = 0; i < 5; i++)
		{
			thisSprite.texture = EmoticonAnimator.Instance.getTexture(spriteName, i);
			yield return new WaitForSeconds(0.05f);
		}
		if (currentLoopCount < totalLoopCount)
		{
			currentLoopCount++;
			StartCoroutine("StartAnimation");
		}
		else
		{
			StartCoroutine("StopAnimation");
		}
	}

	public IEnumerator StopAnimation()
	{
		if (currentLoopCount != -1)
		{
			StopCoroutine("StartAnimation");
		}
		yield return null;
		thisSprite.texture = EmoticonAnimator.Instance.alphaImage;
		currentLoopCount = -1;
		totalLoopCount = -1;
		thisSprite.enabled = false;
	}
}
