using System.Collections.Generic;

public class XMLNodeList : List<Dictionary<string, object>>
{
	public XMLNode Pop()
	{
		XMLNode xMLNode = null;
		xMLNode = (XMLNode)base[base.Count - 1];
		Remove(xMLNode);
		return xMLNode;
	}

	public int Push(XMLNode item)
	{
		Add(item);
		return base.Count;
	}
}
