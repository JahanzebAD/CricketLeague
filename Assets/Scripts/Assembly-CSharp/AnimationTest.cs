using UnityEngine;

public class AnimationTest : MonoBehaviour
{
	public AnimationClip anim;

	public void PlayAnim(AnimationClip anime)
	{
		GetComponent<Animation>().CrossFade(anim.name, 0.1f);
	}
}
