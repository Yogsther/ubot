namespace Quantum
{
    using UnityEngine;

    public class PlayerViewEyes : QuantumEntityViewComponent
    {

		public Transform[] Eyes;

		public override void OnLateUpdateView()
		{
			KCC kcc = GetPredictedQuantumComponent<KCC>();

			float lookPitch = kcc.Data.LookPitch.AsFloat;
			float lookYaw = kcc.Data.LookYaw.AsFloat;

			foreach (Transform eyeTransform in Eyes)
			{
				eyeTransform.rotation = Quaternion.Euler(lookPitch, lookYaw, 0.0f);
			}
		}
	}
}
