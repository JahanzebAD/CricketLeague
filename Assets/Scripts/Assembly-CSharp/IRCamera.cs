using UnityEngine;

public class IRCamera : Singleton<IRCamera>
{
	public Material IRMat;

	[Range(0f, 1f)]
	public float intensity;

	[SerializeField]
	private bool _realTime;

	[SerializeField]
	private LayerMask _excludeLayers;

	private Camera _mainCamera;

	private Camera _tempCamera;

	private GameObject _tempObj;

	public void OnEnable()
	{
		Singleton<GroundController>.instance.SaveJerseyColor();
	}

	public void OnDisable()
	{
		Singleton<GroundController>.instance.RevertJerseyColor();
	}

	private void Start()
	{
		if (!_realTime)
		{
			IRMat.SetFloat(Shader.PropertyToID("_Intensity"), intensity);
		}
	}

	private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
	{
		if (IRMat != null)
		{
			if (_realTime)
			{
				IRMat.SetFloat(Shader.PropertyToID("_Intensity"), intensity);
			}
			Graphics.Blit(_source, _destination, IRMat);
		}
		else
		{
			Graphics.Blit(_source, _destination);
			Debug.LogError("IR Material not assigned. Disabling IR effect");
			base.enabled = false;
		}
	}

	public void ChangeColor()
	{
	}

	public void RevertColor()
	{
	}
}
