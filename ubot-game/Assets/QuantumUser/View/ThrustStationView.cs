using Quantum;
using UnityEngine;

public class ThrustStationView : QuantumEntityViewComponent
{
	[SerializeField] private Transform throttle;
	[SerializeField] private float maxAngle = 50;
	[SerializeField] private float centerAngle = 17f;

	public override void OnUpdateView()
	{
		ThrustStation station = GetPredictedQuantumComponent<ThrustStation>();

		float angle = centerAngle - station.Throttle.AsFloat * maxAngle;
		throttle.localRotation = Quaternion.Euler(throttle.localRotation.eulerAngles.x, throttle.localRotation.eulerAngles.y, angle);
	}
}
