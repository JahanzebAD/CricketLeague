using DG.Tweening;
using UnityEngine;

public class SmoothLookAtTwo : Singleton<SmoothLookAtTwo>
{
	public Transform target;

	public float damping = 1f;

	public bool smooth = true;

	private GroundController GroundControllerScript;

	public bool canMove;

	private Vector3 offset;

	private Vector3 targetLastPos;

	private Tweener tween;

	protected void Start()
	{
		GroundControllerScript = GameObject.Find("GroundController").GetComponent<GroundController>();
		if ((bool)GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}
	}

	protected void LateUpdate()
	{
		if (!GroundControllerScript.ballOnboundaryLine)
		{
			Vector3 forward = target.position - base.transform.position;
			Quaternion b = Quaternion.LookRotation(forward);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, Time.deltaTime * damping);
			if (canMove && !(targetLastPos == target.position + offset))
			{
				tween.ChangeEndValue(target.position + offset, snapStartValue: true).Restart();
				targetLastPos = target.position + offset;
			}
		}
	}

	public void StartFollowTarget()
	{
		offset = base.transform.position - target.transform.position;
		tween = base.transform.DOMove(target.position + offset, 1f).SetAutoKill(autoKillOnCompletion: false);
		canMove = true;
	}

	public void Reset()
	{
		tween.Kill();
		canMove = false;
	}
}
