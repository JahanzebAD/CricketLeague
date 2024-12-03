using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FreeCamera : MonoBehaviour
{
	public float m_MoveSpeed;

	public float m_RotateSpeed;

	public KeyCode m_ForwardButton = KeyCode.W;

	public KeyCode m_BackwardButton = KeyCode.S;

	public KeyCode m_RightButton = KeyCode.D;

	public KeyCode m_LeftButton = KeyCode.A;

	public KeyCode m_UpButton = KeyCode.Q;

	public KeyCode m_DownButton = KeyCode.E;

	private void Update()
	{
		Vector3 moveTo = Vector3.zero;
		Move(m_ForwardButton, ref moveTo, base.transform.forward);
		Move(m_BackwardButton, ref moveTo, -base.transform.forward);
		Move(m_RightButton, ref moveTo, base.transform.right);
		Move(m_LeftButton, ref moveTo, -base.transform.right);
		Move(m_UpButton, ref moveTo, base.transform.up);
		Move(m_DownButton, ref moveTo, -base.transform.up);
		base.transform.position += moveTo * m_MoveSpeed * Time.deltaTime;
		if (Input.GetMouseButton(0))
		{
			Vector3 eulerAngles = base.transform.eulerAngles;
			eulerAngles.x += (0f - Input.GetAxis("Mouse Y")) * 359f * m_RotateSpeed;
			eulerAngles.y += Input.GetAxis("Mouse X") * 359f * m_RotateSpeed;
			base.transform.eulerAngles = eulerAngles;
		}
	}

	private void Move(KeyCode key, ref Vector3 moveTo, Vector3 dir)
	{
		if (Input.GetKey(key))
		{
			moveTo = dir;
		}
	}
}
