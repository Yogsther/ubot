namespace Quantum
{
    using UnityEngine;

    public class PlayerViewBody : QuantumEntityViewComponent
    {
		public Transform Body;
		public float BodyRotationSpeed = 10.0f;

		private Quaternion currentRotation = Quaternion.identity;
		private Quaternion targetRotation;

		public override void OnLateUpdateView()
		{
			KCC kcc = GetPredictedQuantumComponent<KCC>();

			float lookPitch = kcc.Data.LookPitch.AsFloat;
			float lookYaw = kcc.Data.LookYaw.AsFloat;


			targetRotation = Quaternion.Euler(0.0f, lookYaw, 0.0f);

			currentRotation = Quaternion.Slerp(currentRotation, targetRotation, BodyRotationSpeed * Time.deltaTime);

			Body.rotation = currentRotation;
		}
	}
}
