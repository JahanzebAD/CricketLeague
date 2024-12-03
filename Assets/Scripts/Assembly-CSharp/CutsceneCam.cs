using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(Animation))]
public class CutsceneCam : MonoBehaviour
{
	[SerializeField]
	private Camera _cam;

	public bool isPlaying;

	private void Start()
	{
		_cam = GetComponent<Camera>();
		Cutscenes.instance.cutsceneCam = _cam;
	}

	public void AnimationStarted()
	{
		_cam.enabled = true;
		isPlaying = true;
	}

	public void AnimationEnded()
	{
		Cutscenes.instance.EndCutscene();
		isPlaying = false;
	}
}
