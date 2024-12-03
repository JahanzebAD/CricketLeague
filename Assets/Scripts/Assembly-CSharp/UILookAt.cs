using UnityEngine;
using UnityEngine.UI;

public class UILookAt : Singleton<UILookAt>
{
	public Image stripBg;

	public Text stripText;

	public Camera mainCamera;

	public Camera rightSideCamera;

	public Camera leftSideCamera;

	public Camera RenderCamera;

	protected void Awake()
	{
		show(flag: false);
	}

	public void show(bool flag)
	{
		stripBg.gameObject.SetActive(flag);
		stripText.gameObject.SetActive(flag);
	}

	public void PositionSixDistanceProfile(GameObject ballGO, string battingHand)
	{
		if (!Singleton<GroundController>.instance.ballOnboundaryLine)
		{
			Vector3 position = RenderCamera.WorldToScreenPoint(mainCamera.WorldToScreenPoint(ballGO.transform.position));
			if (mainCamera.enabled)
			{
				position = RenderCamera.WorldToScreenPoint(mainCamera.WorldToScreenPoint(ballGO.transform.position));
			}
			else if (leftSideCamera.enabled)
			{
				position = RenderCamera.WorldToScreenPoint(leftSideCamera.WorldToScreenPoint(ballGO.transform.position));
			}
			else if (rightSideCamera.enabled)
			{
				position = RenderCamera.WorldToScreenPoint(rightSideCamera.WorldToScreenPoint(ballGO.transform.position));
			}
			Vector3 vector = RenderCamera.ScreenToWorldPoint(position);
			stripBg.transform.localPosition = new Vector3(vector.x - (float)(Screen.width / 2), vector.y - (float)(Screen.height / 2) - 25f, -2f);
		}
	}
}
