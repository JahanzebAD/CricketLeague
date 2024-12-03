using UnityEngine;

public class ObliqueNear : MonoBehaviour
{
	private Transform plane;

	private Matrix4x4 CalculateObliqueMatrix(Matrix4x4 projection, Vector4 clipPlane)
	{
		Vector4 b = projection.inverse * new Vector4(Mathf.Sign(clipPlane.x), Mathf.Sign(clipPlane.y), 1f, 1f);
		Vector4 vector = clipPlane * (2f / Vector4.Dot(clipPlane, b));
		projection[2] = vector.x - projection[3];
		projection[6] = vector.y - projection[7];
		projection[10] = vector.z - projection[11];
		projection[14] = vector.w - projection[15];
		return projection;
	}

	private void OnPreCull()
	{
		Matrix4x4 projectionMatrix = GetComponent<Camera>().projectionMatrix;
		Matrix4x4 worldToCameraMatrix = GetComponent<Camera>().worldToCameraMatrix;
		Vector3 rhs = worldToCameraMatrix.MultiplyPoint(plane.position);
		Vector3 vector = worldToCameraMatrix.MultiplyVector(-Vector3.up);
		vector.Normalize();
		Vector4 clipPlane = vector;
		clipPlane.w = 0f - Vector3.Dot(vector, rhs);
		GetComponent<Camera>().projectionMatrix = CalculateObliqueMatrix(projectionMatrix, clipPlane);
	}
}
