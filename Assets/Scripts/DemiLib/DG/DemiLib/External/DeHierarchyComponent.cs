using UnityEngine;
using System;
using System.Collections.Generic;

namespace DG.DemiLib.External
{
	public class DeHierarchyComponent : MonoBehaviour
	{
		[Serializable]
		public class CustomizedItem
		{
			public CustomizedItem(GameObject gameObject, DeHierarchyComponent.HColor hColor)
			{
			}

			public GameObject gameObject;
			public DeHierarchyComponent.HColor hColor;
			public DeHierarchyComponent.IcoType icoType;
		}

		public enum HColor
		{
			None = 0,
			Blue = 1,
			Green = 2,
			Orange = 3,
			Purple = 4,
			Red = 5,
			Yellow = 6,
			BrightGrey = 7,
			DarkGrey = 8,
			Black = 9,
			White = 10,
		}

		public enum IcoType
		{
			Dot = 0,
			Star = 1,
			Cog = 2,
			Comment = 3,
			UI = 4,
			Play = 5,
			Heart = 6,
			Skull = 7,
			Camera = 8,
		}

		public List<DeHierarchyComponent.CustomizedItem> customizedItems;
	}
}
