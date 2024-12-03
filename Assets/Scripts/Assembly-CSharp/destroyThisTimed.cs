using UnityEngine;

public class destroyThisTimed : MonoBehaviour
{
	private float destroyTime = 5f;

	private void Start()
	{
		Object.Destroy(base.gameObject, destroyTime);
	}
}
