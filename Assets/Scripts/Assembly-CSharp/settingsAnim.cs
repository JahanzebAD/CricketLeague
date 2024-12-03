using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class settingsAnim : Singleton<settingsAnim>
{
	public Image settings;

	public void Settingsloop()
	{
		Sequence s = DOTween.Sequence();
		s.Append(settings.transform.DORotate(new Vector3(0f, 0f, settings.transform.localEulerAngles.z + 113f), 5f, RotateMode.FastBeyond360));
	}
}
