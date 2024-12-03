using System.Collections.Generic;
using UnityEngine;

public class RewindTime : Singleton<RewindTime>
{
	public bool isRewinding;

	public bool canRecord;

	public bool canRecordActualPath;

	public static int count;

	public List<Vector3> positions;

	public List<Vector3> tempPositions;

	public List<Vector3> forwardPositions;

	private void Start()
	{
		positions = new List<Vector3>();
	}

	private void Update()
	{
		if (isRewinding && canRecord)
		{
			Rewind();
		}
		else if (!isRewinding && canRecord)
		{
			Record();
		}
	}

	private void Rewind()
	{
		if (positions.Count > 0)
		{
			base.transform.position = positions[0];
			RecordTemp();
			positions.RemoveAt(0);
			return;
		}
		count++;
		if (count % 2 != 0)
		{
			Singleton<GroundController>.instance.batsmanAnimationComponent[Singleton<GroundController>.instance.batsmanAnimation].speed = 1f;
		}
		if (count % 2 == 0 && count > 0)
		{
			Singleton<GroundController>.instance.batsmanAnimationComponent[Singleton<GroundController>.instance.batsmanAnimation].speed = -1f;
		}
		TransferPositions();
	}

	private void TransferPositions()
	{
		for (int num = tempPositions.Count - 1; num >= 0; num--)
		{
			positions.Insert(0, tempPositions[num]);
		}
		tempPositions.Clear();
		base.transform.position = positions[0];
	}

	private void Record()
	{
		positions.Insert(0, base.transform.position);
	}

	private void RecordTemp()
	{
		tempPositions.Insert(0, base.transform.position);
	}

	public void StartRewind()
	{
		isRewinding = true;
	}

	public void StopRewind()
	{
		isRewinding = false;
		tempPositions.Clear();
		positions.Clear();
	}
}
