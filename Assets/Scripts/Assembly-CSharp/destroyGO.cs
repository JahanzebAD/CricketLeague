using UnityEngine;

public class destroyGO : MonoBehaviour
{
	public float destroyTime = 5f;

	private void Start()
	{
		Object.Destroy(base.gameObject, destroyTime);
	}
}
