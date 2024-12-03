using UnityEngine;

public class ButtonSource : MonoBehaviour
{
	public static ButtonSource instance;

	public AudioSource audioSource;

	protected void Awake()
	{
		if (CONTROLLER.bgMusicVal == 0)
		{
			audioSource.mute = true;
		}
		else
		{
			audioSource.mute = false;
		}
	}
}
