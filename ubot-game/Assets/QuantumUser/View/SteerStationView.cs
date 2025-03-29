using Quantum;
using UnityEngine;

public class SteerStationView : QuantumEntityViewComponent
{
	[SerializeField] private Transform wheel;
	[SerializeField] private RectTransform compassNeedle;

	public override void OnUpdateView()
	{
		SteerStation steerStation = GetPredictedQuantumComponent<SteerStation>();
		float steer = steerStation.Steering.AsFloat;
		wheel.localRotation = Quaternion.Euler(wheel.localRotation.eulerAngles.x, wheel.localRotation.eulerAngles.y, steer * 360);
	}
}
