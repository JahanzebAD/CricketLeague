using System.Collections.Generic;

public class XMLNode : Dictionary<string, object>
{
	public XMLNodeList GetNodeList(string path)
	{
		return GetObject(path) as XMLNodeList;
	}

	public XMLNode GetNode(string path)
	{
		return GetObject(path) as XMLNode;
	}

	public string GetValue(string path)
	{
		return GetObject(path) as string;
	}

	private object GetObject(string path)
	{
		string[] array = path.Split('>');
		XMLNode xMLNode = this;
		XMLNodeList xMLNodeList = null;
		bool flag = false;
		for (int i = 0; i < array.Length; i++)
		{
			object obj;
			if (flag)
			{
				xMLNode = (XMLNode)xMLNodeList[int.Parse(array[i])];
				obj = xMLNode;
				flag = false;
				continue;
			}
			obj = xMLNode[array[i]];
			if (obj is List<Dictionary<string, object>>)
			{
				xMLNodeList = (XMLNodeList)(obj as List<Dictionary<string, object>>);
				flag = true;
				continue;
			}
			if (i != array.Length - 1)
			{
				string text = string.Empty;
				for (int j = 0; j <= i; j++)
				{
					text = text + ">" + array[j];
				}
			}
			return obj;
		}
		if (flag)
		{
			return xMLNodeList;
		}
		return xMLNode;
	}
}
