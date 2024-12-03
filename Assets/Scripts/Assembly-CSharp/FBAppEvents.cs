using System.Collections.Generic;
using UnityEngine;

public class FBAppEvents : MonoBehaviour
{
	public static FBAppEvents instance;

	private void Awake()
	{
		instance = this;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	public void LogQuickPlayEvent(string Event, string status)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["Event"] = Event;
		dictionary["Status"] = status;
	}

	public void LogT20Event(string Event, string status)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["Event"] = Event;
		dictionary["Status"] = status;
	}

	public void LogWorldCupEvent(string Event, string status)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["Event"] = Event;
		dictionary["Status"] = status;
	}

	public void LogPLEvent(string Event, string status)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["Event"] = Event;
		dictionary["Status"] = status;
	}

	public void LogSuperOverEvent(string Event, string status)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["Event"] = Event;
		dictionary["Status"] = status;
	}

	public void LogSuperChaseEvent(string Event, string status)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["Event"] = Event;
		dictionary["Status"] = status;
	}

	public void LogMultiplayerEvent(string Event, string status)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["Event"] = Event;
		dictionary["Status"] = status;
	}

	public void LogTestCricketEvent(string Event, string status)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary["Event"] = Event;
		dictionary["Status"] = status;
	}
}
