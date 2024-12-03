using UnityEngine;

public class ShaderProperty<T>
{
	public string shaderKey;

	public int ShaderID => Shader.PropertyToID(shaderKey);
}
