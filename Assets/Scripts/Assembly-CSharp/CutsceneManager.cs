using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
	public delegate void camEvent();

	public Camera cutsceneCam;

	private Animation m_camAnimation;

	public List<Cutscene> cutscenes = new List<Cutscene>();

	public string currentCutscene;

	private Cutscene currentScene;

	public event camEvent onCutsceneEnded;

	private void Start()
	{
		for (int i = 0; i < cutscenes.Count; i++)
		{
			cutscenes[i].Init();
		}
		m_camAnimation = cutsceneCam.GetComponent<Animation>();
	}

	public void PlayCutscene(string name)
	{
		for (int i = 0; i < cutscenes.Count; i++)
		{
			if (cutscenes[i].name == name)
			{
				cutsceneCam.enabled = true;
				currentCutscene = name;
				currentScene = cutscenes[i];
				cutscenes[i].PlayCutscene();
				if (m_camAnimation != null)
				{
					m_camAnimation.Play(name);
				}
			}
		}
	}

	public void EndCutscene()
	{
		if (m_camAnimation != null)
		{
			m_camAnimation.Stop();
		}
		if (currentScene != null)
		{
			currentScene.Stop();
		}
		cutsceneCam.enabled = false;
		currentCutscene = string.Empty;
		if (this.onCutsceneEnded != null)
		{
			this.onCutsceneEnded();
		}
	}

	public void PlayCutsceneObjectAnimation(string cutsceneName, string objectName, string animationName)
	{
		for (int i = 0; i < cutscenes.Count; i++)
		{
			if (cutscenes[i].name == base.name)
			{
				cutscenes[i].PlayObjectAnimation(objectName, animationName);
			}
		}
	}
}
