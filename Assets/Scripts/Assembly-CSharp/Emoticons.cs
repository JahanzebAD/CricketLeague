using System.Collections;
using UnityEngine;

public class Emoticons : MonoBehaviour
{
	public GameObject smileyBtn;

	public GameObject closeBtn;

	public GameObject posi;

	private bool isTransition;

	private float transitionTime;

	private float transitionTotTime;

	private float Moving_Src;

	private float Moving_Dest;

	private void Awake()
	{
		posi.SetActive(value: false);
		smileyBtn.SetActive(value: true);
		closeBtn.SetActive(value: false);
		isTransition = false;
		transitionTotTime = 0.5f;
	}

	public void OpenEmoticons()
	{
		if (!isTransition)
		{
			posi.SetActive(value: true);
			transitionTime = 0f;
			isTransition = true;
		}
	}

	public void CloseEmoticons()
	{
		if (!isTransition)
		{
			posi.GetComponent<Animator>().SetTrigger("close");
			isTransition = true;
			transitionTime = 0f;
			StartCoroutine(StopAnimation());
		}
	}

	private IEnumerator StopAnimation()
	{
		yield return new WaitForSeconds(0.5f);
		posi.SetActive(value: false);
	}

	public void Update()
	{
		if (!isTransition)
		{
			return;
		}
		transitionTime += Time.deltaTime;
		if (transitionTime > transitionTotTime)
		{
			isTransition = false;
			if (smileyBtn.activeSelf)
			{
				smileyBtn.SetActive(value: false);
				closeBtn.SetActive(value: true);
			}
			else
			{
				smileyBtn.SetActive(value: true);
				closeBtn.SetActive(value: false);
			}
		}
	}

	private float Linear(float t, float source, float destination, float duration)
	{
		return (destination - source) * t / duration + source;
	}
}
