using System;
using System.Collections.Generic;
using UnityEngine;

public class AIFieldingSetupManager : Singleton<AIFieldingSetupManager>
{
	public new bool enabled = true;

	public int[] strikerHittingDirection = new int[12];

	public int[] priorityList = new int[5];

	public List<int> checkFieldList = new List<int>();

	private List<int> tempFieldList = new List<int>();

	private int[] sortedHittingDirection = new int[12];

	private int[] tempPriorityList = new int[5];

	private int i;

	private int j;

	public bool needFieldingChange;

	public int previousFieldSet;

	public int currentFieldSet;

	private int noofOuterCircleFielders;

	private int attemptsToFindBestField;

	public float lastHittedAngle;

	private int skipFieldChangeBallCount;

	private string[] fieldSetCodeString = new string[25]
	{
		"010010000000", "101000000000", "100000100000", "001000000100", "010001000000", "000000010001", "110000000000", "010001000000", "010000000010", "001000000001",
		"010000010111", "111100000001", "001010010101", "001100010011", "101010010001", "001010100010", "001001001100", "100010101001", "100010101100", "000001110101",
		"110010001010", "101101010000", "101010010010", "001100000001", "101010000101"
	};

	private string tempString = string.Empty;

	public void SetSavedField()
	{
		previousFieldSet = (currentFieldSet = CONTROLLER.computerFielderChangeIndex);
	}

	public void SaveStrikerHittingDirections()
	{
		if ((int)(lastHittedAngle / 30f) >= 12)
		{
			strikerHittingDirection[0]++;
		}
		else
		{
			strikerHittingDirection[(int)(lastHittedAngle / 30f)]++;
		}
	}

	private void CalculateHittingDirectionPriority()
	{
		needFieldingChange = true;
		for (i = 0; i < 12; i++)
		{
			sortedHittingDirection[i] = strikerHittingDirection[i];
		}
		Array.Sort(sortedHittingDirection);
		for (i = 0; i < 5; i++)
		{
			tempPriorityList[i] = 0;
			if (sortedHittingDirection[11 - i] > 0)
			{
				tempPriorityList[i] = sortedHittingDirection[11 - i];
			}
			else
			{
				tempPriorityList[i] = -1;
			}
			priorityList[i] = -1;
		}
		for (i = 0; i < 5; i++)
		{
			if (tempPriorityList[i] == -1)
			{
				if (i == 0)
				{
					needFieldingChange = false;
				}
				break;
			}
			for (j = 0; j < 12; j++)
			{
				if (strikerHittingDirection[j] == tempPriorityList[i])
				{
					strikerHittingDirection[j] = -1;
					priorityList[i] = j;
					break;
				}
			}
		}
		i = 0;
		while (i < 5 && tempPriorityList[i] != -1)
		{
			strikerHittingDirection[priorityList[i]] = tempPriorityList[i];
			i++;
		}
	}

	public int GetAIFieldingIndex()
	{
		skipFieldChangeBallCount++;
		if (CONTROLLER.difficultyMode == "easy")
		{
			if (5 - skipFieldChangeBallCount != 0)
			{
				return currentFieldSet;
			}
		}
		else if (CONTROLLER.difficultyMode == "medium")
		{
			if (3 - skipFieldChangeBallCount != 0)
			{
				return currentFieldSet;
			}
		}
		else if (CONTROLLER.difficultyMode == "hard")
		{
			skipFieldChangeBallCount = 0;
		}
		skipFieldChangeBallCount = 0;
		CalculateHittingDirectionPriority();
		if (!needFieldingChange)
		{
			return previousFieldSet;
		}
		checkFieldList.Clear();
		checkFieldList.TrimExcess();
		if (CONTROLLER.PowerPlay || CONTROLLER.PlayModeSelected == 7)
		{
			noofOuterCircleFielders = 2;
			for (i = 0; i < 10; i++)
			{
				checkFieldList.Add(i);
			}
		}
		else
		{
			noofOuterCircleFielders = 5;
			for (i = 10; i < 25; i++)
			{
				checkFieldList.Add(i);
			}
		}
		for (i = 0; i < noofOuterCircleFielders; i++)
		{
			if (priorityList[i] != -1)
			{
				FilterCheckFieldList(priorityList[i]);
				if (checkFieldList.Count < 2)
				{
					break;
				}
			}
		}
		needFieldingChange = false;
		if (checkFieldList.Count == 0)
		{
			return previousFieldSet;
		}
		previousFieldSet = currentFieldSet;
		currentFieldSet = checkFieldList[UnityEngine.Random.Range(0, checkFieldList.Count)] + 1;
		return currentFieldSet;
	}

	private void FilterCheckFieldList(int fielderPosition)
	{
		for (j = 0; j < checkFieldList.Count; j++)
		{
			tempString = fieldSetCodeString[checkFieldList[j]];
			if (tempString[fielderPosition] == '1')
			{
				tempFieldList.Add(checkFieldList[j]);
			}
		}
		if (tempFieldList.Count != 0)
		{
			checkFieldList.Clear();
			checkFieldList.TrimExcess();
			for (j = 0; j < tempFieldList.Count; j++)
			{
				checkFieldList.Add(tempFieldList[j]);
			}
			tempFieldList.Clear();
			tempFieldList.TrimExcess();
			attemptsToFindBestField = 0;
			return;
		}
		switch (attemptsToFindBestField)
		{
		case 0:
			fielderPosition++;
			attemptsToFindBestField++;
			break;
		case 1:
			fielderPosition -= 2;
			attemptsToFindBestField++;
			break;
		default:
			attemptsToFindBestField = 0;
			return;
		}
		fielderPosition = (fielderPosition + 12) % 12;
		FilterCheckFieldList(fielderPosition);
	}

	public bool IsLongFielderInAngle(float angle)
	{
		int num = ((CONTROLLER.BattingTeamIndex != CONTROLLER.opponentTeamIndex) ? CONTROLLER.computerFielderChangeIndex : CONTROLLER.fielderChangeIndex);
		if (num != 0)
		{
			num--;
		}
		int index = (((int)(angle / 30f) < 12) ? ((int)(angle / 30f)) : 0);
		if (fieldSetCodeString[num][index] == '1')
		{
			return true;
		}
		return false;
	}
}
