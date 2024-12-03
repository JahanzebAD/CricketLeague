using System;
using UnityEngine;

[Serializable]
public class CutsceneObject
{
	public GameObject go;

	private Animation m_animation;

	public AnimationClip m_initAnimation;

	public void Init()
	{
		m_animation = go.GetComponent<Animation>();
	}

	public void PlayAnimation(string clipName)
	{
		if (m_animation != null)
		{
			m_animation.CrossFade(clipName);
		}
	}

	public void PlayAnimation()
	{
		if (m_animation != null)
		{
			m_animation.CrossFade(m_initAnimation.name);
		}
	}

	public void Stop()
	{
		if (m_animation != null)
		{
			m_animation.Stop();
		}
	}
}
