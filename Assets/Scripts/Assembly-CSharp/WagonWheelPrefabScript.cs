using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class WagonWheelPrefabScript : MonoBehaviour
{
	private float DEG2RAD = (float)Math.PI / 180f;

	private float RAD2DEG = 180f / (float)Math.PI;

	public Material material1;

	public Material material2;

	public Material material4;

	public Material material6;

	private MeshRenderer mr;

	private float TubeRadius = 0.3f;

	private Vector3[] Vertices;

	private int crossSegments = 4;

	private Vector3[] crossPoints;

	private float Angle;

	private Vector3 wagonOriginPosition = new Vector3(0f, 0.1f, 8.75f);

	private int noOfBounce;

	private int runsScored;

	private Vector3 wagonFirstPitchPos;

	private Vector3 wagonSecondPitchPos;

	private Vector3 wagonThirdPitchPos;

	private Vector3 wagonFinalPitchPos;

	private float firstBounceHeight;

	private float secondBounceHeight;

	private float thirdBounceHeight;

	private float firstBounceDistance;

	private float secondBounceDistance;

	private float thirdBounceDistance;

	private float tubeAngle1;

	private float tubeAngle2;

	private float tubeAngle3;

	private int noOfSegmentsForFirstBounce;

	private int noOfSegmentsForSecondBounce;

	private int noOfSegmentsForThirdBounce;

	private float AngleInc;

	private float vertexStepIncrement;

	private float vertexXPos;

	private float vertexZPos;

	private int BounceCount = 1;

	private int VerticesArraySize;

	private int ArrayFilledPos;

	private float AngleBetweenTwoGameObjects(GameObject go1, GameObject go2)
	{
		float y = go1.transform.position.x - go2.transform.position.x;
		float x = go1.transform.position.z - go2.transform.position.z;
		float num = Mathf.Atan2(y, x) * RAD2DEG;
		return (270f - num + 360f) % 360f;
	}

	private float AngleBetweenTwoVector3(Vector3 v31, Vector3 v32)
	{
		float y = v31.x - v32.x;
		float x = v31.z - v32.z;
		float num = Mathf.Atan2(y, x) * RAD2DEG;
		return (270f - num + 360f) % 360f;
	}

	private float DistanceBetweenTwoGameObjects(GameObject go1, GameObject go2)
	{
		return Vector3.Distance(go1.transform.position, go2.transform.position);
	}

	private void Start()
	{
		base.gameObject.AddComponent<MeshFilter>();
		mr = base.gameObject.AddComponent<MeshRenderer>();
	}

	public void AssignValuestoPrefab(int Numberofbounce, float ballangle, int runs, Vector3 firstpoint, float firstheight, Vector3 secondpoint, float secondheight, Vector3 thirdpoint, float thirdheight, Vector3 finalpoint)
	{
		noOfBounce = Numberofbounce;
		runsScored = runs;
		wagonFirstPitchPos = firstpoint;
		wagonSecondPitchPos = secondpoint;
		wagonThirdPitchPos = thirdpoint;
		wagonFinalPitchPos = finalpoint;
		firstBounceHeight = firstheight;
		secondBounceHeight = secondheight;
		thirdBounceHeight = thirdheight;
		FindValuesForWagonWheel();
		SizeOfVerticesArray();
		StartCoroutine(MyUpdate());
	}

	private void FindValuesForWagonWheel()
	{
		tubeAngle1 = AngleBetweenTwoVector3(wagonOriginPosition, wagonFirstPitchPos);
		tubeAngle2 = AngleBetweenTwoVector3(wagonFirstPitchPos, wagonSecondPitchPos);
		tubeAngle3 = AngleBetweenTwoVector3(wagonSecondPitchPos, wagonThirdPitchPos);
		firstBounceDistance = Vector3.Distance(wagonOriginPosition, wagonFirstPitchPos);
		secondBounceDistance = Vector3.Distance(wagonFirstPitchPos, wagonSecondPitchPos);
		thirdBounceDistance = Vector3.Distance(wagonSecondPitchPos, wagonThirdPitchPos);
		noOfSegmentsForFirstBounce = (int)Mathf.Floor(firstBounceDistance / 2f);
		noOfSegmentsForSecondBounce = (int)Mathf.Floor(secondBounceDistance / 2f);
		noOfSegmentsForThirdBounce = (int)Mathf.Floor(thirdBounceDistance / 2f);
	}

	private void SizeOfVerticesArray()
	{
		if (noOfSegmentsForFirstBounce < 8)
		{
			noOfSegmentsForFirstBounce = 8;
		}
		if (noOfSegmentsForSecondBounce < 8)
		{
			noOfSegmentsForSecondBounce = 8;
		}
		if (noOfSegmentsForThirdBounce < 8)
		{
			noOfSegmentsForThirdBounce = 8;
		}
		if (noOfBounce == 0)
		{
			VerticesArraySize = 2;
			Vertices = new Vector3[VerticesArraySize];
		}
		if (noOfBounce == 1)
		{
			VerticesArraySize = noOfSegmentsForFirstBounce + 1 + 1;
			Vertices = new Vector3[VerticesArraySize];
		}
		if (noOfBounce == 2)
		{
			VerticesArraySize = noOfSegmentsForFirstBounce + noOfSegmentsForSecondBounce + 2 + 1;
			Vertices = new Vector3[VerticesArraySize];
		}
		if (noOfBounce == 3)
		{
			VerticesArraySize = noOfSegmentsForFirstBounce + noOfSegmentsForSecondBounce + noOfSegmentsForThirdBounce + 3 + 1;
			Vertices = new Vector3[VerticesArraySize];
		}
	}

	private void ResetForBounceCount()
	{
		if (noOfBounce == 0)
		{
			ref Vector3 reference = ref Vertices[0];
			reference = wagonOriginPosition;
			ref Vector3 reference2 = ref Vertices[1];
			reference2 = new Vector3(wagonFinalPitchPos.x, 0.1f, wagonFinalPitchPos.z);
			return;
		}
		if (BounceCount == 1)
		{
			vertexStepIncrement = firstBounceDistance / (float)noOfSegmentsForFirstBounce;
			Angle = 0f;
			AngleInc = 180f / (firstBounceDistance / vertexStepIncrement);
			vertexXPos = 0f;
			vertexZPos = 0f;
			for (int i = 0; i < noOfSegmentsForFirstBounce + 1; i++)
			{
				float num = Mathf.Sin((float)Math.PI / 180f * Angle);
				ref Vector3 reference3 = ref Vertices[i];
				reference3 = new Vector3(vertexXPos + wagonOriginPosition.x, wagonOriginPosition.y + num * firstBounceHeight, vertexZPos + wagonOriginPosition.z);
				Angle += AngleInc;
				vertexXPos += vertexStepIncrement * Mathf.Cos(tubeAngle1 * DEG2RAD);
				vertexZPos += vertexStepIncrement * Mathf.Sin(tubeAngle1 * DEG2RAD);
			}
			ArrayFilledPos = noOfSegmentsForFirstBounce + 1;
		}
		if (BounceCount == 2)
		{
			wagonOriginPosition.x = wagonFirstPitchPos.x;
			wagonOriginPosition.z = wagonFirstPitchPos.z;
			vertexStepIncrement = secondBounceDistance / (float)noOfSegmentsForSecondBounce;
			Angle = 0f;
			AngleInc = 180f / (secondBounceDistance / vertexStepIncrement);
			vertexXPos = 0f;
			vertexZPos = 0f;
			for (int i = ArrayFilledPos; i < noOfSegmentsForFirstBounce + noOfSegmentsForSecondBounce + 2; i++)
			{
				float num = Mathf.Sin((float)Math.PI / 180f * Angle);
				ref Vector3 reference4 = ref Vertices[i];
				reference4 = new Vector3(vertexXPos + wagonOriginPosition.x, wagonOriginPosition.y + num * secondBounceHeight, vertexZPos + wagonOriginPosition.z);
				Angle += AngleInc;
				vertexXPos += vertexStepIncrement * Mathf.Cos(tubeAngle2 * DEG2RAD);
				vertexZPos += vertexStepIncrement * Mathf.Sin(tubeAngle2 * DEG2RAD);
			}
			ArrayFilledPos = noOfSegmentsForFirstBounce + noOfSegmentsForSecondBounce + 2;
		}
		if (BounceCount == 3)
		{
			wagonOriginPosition.x = wagonSecondPitchPos.x;
			wagonOriginPosition.z = wagonSecondPitchPos.z;
			vertexStepIncrement = thirdBounceDistance / (float)noOfSegmentsForThirdBounce;
			Angle = 0f;
			AngleInc = 180f / (thirdBounceDistance / vertexStepIncrement);
			vertexXPos = 0f;
			vertexZPos = 0f;
			for (int i = ArrayFilledPos; i < VerticesArraySize - 1; i++)
			{
				float num = Mathf.Sin((float)Math.PI / 180f * Angle);
				ref Vector3 reference5 = ref Vertices[i];
				reference5 = new Vector3(vertexXPos + wagonOriginPosition.x, wagonOriginPosition.y + num * thirdBounceHeight, vertexZPos + wagonOriginPosition.z);
				Angle += AngleInc;
				vertexXPos += vertexStepIncrement * Mathf.Cos(tubeAngle3 * DEG2RAD);
				vertexZPos += vertexStepIncrement * Mathf.Sin(tubeAngle3 * DEG2RAD);
			}
			ArrayFilledPos = VerticesArraySize;
		}
		ref Vector3 reference6 = ref Vertices[VerticesArraySize - 1];
		reference6 = new Vector3(wagonFinalPitchPos.x, 0.1f, wagonFinalPitchPos.z);
	}

	private IEnumerator MyUpdate()
	{
		if (runsScored == 6)
		{
			mr.material = material6;
		}
		if (runsScored == 4)
		{
			mr.material = material4;
		}
		if (runsScored == 1)
		{
			mr.material = material1;
		}
		if (runsScored == 2 || runsScored == 3)
		{
			mr.material = material2;
		}
		mr.shadowCastingMode = ShadowCastingMode.Off;
		mr.receiveShadows = false;
		mr.lightProbeUsage = LightProbeUsage.Off;
		mr.reflectionProbeUsage = ReflectionProbeUsage.Off;
		for (int b = 0; b <= noOfBounce; b++)
		{
			BounceCount = b;
			ResetForBounceCount();
			crossPoints = new Vector3[crossSegments];
			float theta = (float)Math.PI * 2f / (float)crossSegments;
			//for (int c3 = 0; c3 < crossSegments; c3++)
			//{
			//	ref Vector3 reference = ref crossPoints[c3];
			//	reference = new Vector3(Mathf.Cos(theta * (float)c3), Mathf.Sin(theta * (float)c3), 0f);
			//}
			Vector3[] meshVertices = new Vector3[VerticesArraySize * crossSegments];
			Vector2[] uvs = new Vector2[VerticesArraySize * crossSegments];
			int[] tris = new int[VerticesArraySize * crossSegments * 6];
			int[] lastVertices = new int[crossSegments];
			int[] theseVertices = new int[crossSegments];
			Quaternion rotation = Quaternion.identity;
			if (b != noOfBounce)
			{
				continue;
			}
			Mesh mesh = GetComponent<MeshFilter>().mesh;
			for (int p = 0; p < Vertices.Length; p++)
			{
				if (p < noOfSegmentsForFirstBounce + 1)
				{
					yield return new WaitForSeconds(0.05f);
				}
				else if (p > noOfSegmentsForFirstBounce + 1 && p < noOfSegmentsForFirstBounce + noOfSegmentsForSecondBounce + 2)
				{
					yield return new WaitForSeconds(0.08f);
				}
				else
				{
					yield return new WaitForSeconds(0.08f);
				}
				if (p < Vertices.Length - (noOfSegmentsForSecondBounce + noOfSegmentsForThirdBounce))
				{
					rotation = Quaternion.FromToRotation(Vector3.forward, Vertices[p + 1] - Vertices[p]);
				}
				for (int c3 = 0; c3 < crossSegments; c3++)
				{
					int num = p * crossSegments + c3;
					//ref Vector3 reference2 = ref meshVertices[num];
					//reference2 = Vertices[p] + rotation * crossPoints[c3] * TubeRadius;
					//ref Vector2 reference3 = ref uvs[num];
					//reference3 = new Vector2((float)c3 / (float)crossSegments, (float)p / (float)Vertices.Length);
					lastVertices[c3] = theseVertices[c3];
					theseVertices[c3] = p * crossSegments + c3;
				}
				if (p > 0)
				{
					for (int c3 = 0; c3 < crossSegments; c3++)
					{
						int num2 = (p * crossSegments + c3) * 6;
						tris[num2] = lastVertices[c3];
						tris[num2 + 1] = lastVertices[(c3 + 1) % crossSegments];
						tris[num2 + 2] = theseVertices[c3];
						tris[num2 + 3] = tris[num2 + 2];
						tris[num2 + 4] = tris[num2 + 1];
						tris[num2 + 5] = theseVertices[(c3 + 1) % crossSegments];
					}
				}
				if (!mesh)
				{
					mesh = new Mesh();
				}
				mesh.vertices = meshVertices;
				mesh.triangles = tris;
				mesh.RecalculateNormals();
				mesh.uv = uvs;
			}
		}
	}
}
