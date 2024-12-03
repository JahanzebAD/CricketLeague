using System;
using System.Collections.Generic;

[Serializable]
public class Cutscene
{
	public string name;

	public List<CutsceneObject> objects = new List<CutsceneObject>();

	public void Init()
	{
		for (int i = 0; i < objects.Count; i++)
		{
			objects[i].Init();
		}
	}

	public void PlayCutscene()
	{
		for (int i = 0; i < objects.Count; i++)
		{
			objects[i].PlayAnimation();
		}
	}

	public void Stop()
	{
		for (int i = 0; i < objects.Count; i++)
		{
			objects[i].Stop();
		}
	}

	public void PlayObjectAnimation(string cutsceneObject, string animation)
	{
		for (int i = 0; i < objects.Count; i++)
		{
			if (objects[i].go.name == cutsceneObject)
			{
				objects[i].PlayAnimation(animation);
			}
		}
	}
}
