using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Ball" && (Singleton<GroundController>.instance.leftSideCamera.enabled || Singleton<GroundController>.instance.rightSideCamera.enabled))
		{
			Singleton<RightFovLerp>.instance.canMove = false;
		}
	}
}
