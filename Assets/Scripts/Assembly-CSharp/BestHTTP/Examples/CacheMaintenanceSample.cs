using System;
using BestHTTP.Caching;
using UnityEngine;

namespace BestHTTP.Examples
{
	public sealed class CacheMaintenanceSample : MonoBehaviour
	{
		private enum DeleteOlderTypes
		{
			Days,
			Hours,
			Mins,
			Secs
		}

		private DeleteOlderTypes deleteOlderType = DeleteOlderTypes.Secs;

		private int value = 10;

		private int maxCacheSize = 5242880;

		private void OnGUI()
		{
			GUIHelper.DrawArea(GUIHelper.ClientArea, drawHeader: true, delegate
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label("Delete cached entities older then");
				GUILayout.Label(value.ToString(), GUILayout.MinWidth(50f));
				value = (int)GUILayout.HorizontalSlider(value, 1f, 60f, GUILayout.MinWidth(100f));
				GUILayout.Space(10f);
				deleteOlderType = (DeleteOlderTypes)GUILayout.SelectionGrid((int)deleteOlderType, new string[4] { "Days", "Hours", "Mins", "Secs" }, 4);
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();
				GUILayout.Space(10f);
				GUILayout.BeginHorizontal();
				GUILayout.Label("Max Cache Size (bytes): ", GUILayout.Width(150f));
				GUILayout.Label(maxCacheSize.ToString("N0"), GUILayout.Width(70f));
				maxCacheSize = (int)GUILayout.HorizontalSlider(maxCacheSize, 1024f, 1.048576E+07f);
				GUILayout.EndHorizontal();
				GUILayout.Space(10f);
				if (GUILayout.Button("Maintenance"))
				{
					TimeSpan deleteOlder = TimeSpan.FromDays(14.0);
					switch (deleteOlderType)
					{
					case DeleteOlderTypes.Days:
						deleteOlder = TimeSpan.FromDays(value);
						break;
					case DeleteOlderTypes.Hours:
						deleteOlder = TimeSpan.FromHours(value);
						break;
					case DeleteOlderTypes.Mins:
						deleteOlder = TimeSpan.FromMinutes(value);
						break;
					case DeleteOlderTypes.Secs:
						deleteOlder = TimeSpan.FromSeconds(value);
						break;
					}
					HTTPCacheService.BeginMaintainence(new HTTPCacheMaintananceParams(deleteOlder, (ulong)maxCacheSize));
				}
			});
		}
	}
}
