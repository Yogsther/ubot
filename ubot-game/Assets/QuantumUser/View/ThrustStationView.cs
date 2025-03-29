using Quantum;
using UnityEngine;
using UnityEngine.UI;

public class ThrustStationView : QuantumEntityViewComponent
{
	[SerializeField] private Transform throttle;
	[SerializeField] private float maxAngle = 50;
	[SerializeField] private float centerAngle = 17f;
	[SerializeField] Slider slider;
	[SerializeField] Image sliderFill;
	[SerializeField] Color forwardColor, reverseColor;

	public override void OnUpdateView()
	{
		ThrustStation station = GetPredictedQuantumComponent<ThrustStation>();

		float angle = centerAngle - station.Throttle.AsFloat * maxAngle;
		throttle.localRotation = Quaternion.Euler(throttle.localRotation.eulerAngles.x, throttle.localRotation.eulerAngles.y, angle);

		slider.value = Mathf.Abs(station.Throttle.AsFloat);
		sliderFill.color = station.Throttle.AsFloat > 0 ? forwardColor : reverseColor;
	}
}
