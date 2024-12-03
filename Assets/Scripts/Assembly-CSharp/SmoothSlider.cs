using UnityEngine;
using UnityEngine.UI;

public class SmoothSlider : MonoBehaviour
{
	public float fillSpeed = 1f;

	private Slider slider;

	private RectTransform fillRect;

	private float targetValue;

	private float curValue;

	private void Awake()
	{
		slider = GetComponent<Slider>();
		slider.onValueChanged.AddListener(delegate
		{
			ValueChange();
		});
		fillRect = slider.fillRect;
		targetValue = (curValue = slider.value);
	}

	public void ValueChange()
	{
		targetValue = slider.value;
	}

	private void Update()
	{
		curValue = Mathf.MoveTowards(curValue, targetValue, Time.deltaTime * fillSpeed);
		slider.value = curValue;
	}
}
