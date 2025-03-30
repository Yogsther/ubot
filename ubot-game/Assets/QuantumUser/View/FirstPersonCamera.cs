namespace Quantum
{
	using System.Collections;
	using UnityEngine;


	

	/// <summary>
	/// Updates main Camera (first person view) based on look rotation stored in KCC component.
	/// </summary>
	public class FirstPersonCamera : QuantumEntityViewComponent<SceneContext>
	{
		

		public Transform Handle;

		[SerializeField] private Transform cameraHandleOrigin;

		public override void OnActivate(Frame frame)
		{
			QuantumEvent.Subscribe<EventSubmarineDamaged>(this, OnSubmarineDamaged);

			
		}

		public override void OnDeactivate()
		{
			QuantumEvent.UnsubscribeListener(this);
		}

		public override void OnLateUpdateView()
		{
			KCC kcc = GetPredictedQuantumComponent<KCC>();

			float lookPitch = kcc.Data.LookPitch.AsFloat;
			float lookYaw   = kcc.Data.LookYaw.AsFloat;

			if (EntityRef == ViewContext.LocalPlayerEntity)
			{
				// The simulation runs with a fixed tick rate which is not aligned with render rate.
				// For local player we also need to add look rotation accumulated since last fixed update to get super smooth look.

				Vector2 pendingLookRotationDelta = ViewContext.Input.GetPendingLookRotationDelta(EntityView.Game);
				lookPitch += pendingLookRotationDelta.x;
				lookYaw   += pendingLookRotationDelta.y;

				lookPitch = Mathf.Clamp(lookPitch, -90.0f, 90.0f);

				while (lookYaw >  180.0f) { lookYaw -= 360.0f; }
				while (lookYaw < -180.0f) { lookYaw += 360.0f; }

				// For local player we also set transform rotation => this overrides default interpolation.
				transform.rotation = Quaternion.Euler(0.0f, lookYaw, 0.0f);
			}

			// Handle transform is updated for every player.
			Handle.localRotation = Quaternion.Euler(lookPitch, 0.0f, 0.0f);

			if (EntityRef == ViewContext.LocalPlayerEntity)
			{
				// Only local player propagates Handle transform to main Camera.
				Handle.GetPositionAndRotation(out Vector3 cameraPosition, out Quaternion cameraRotation);
				Camera.main.transform.SetPositionAndRotation(cameraPosition, cameraRotation);
			}
		}

		private void OnSubmarineDamaged(EventSubmarineDamaged e)
		{
			var playerTeam = GetPredictedQuantumComponent<TeamLink>().Team;

			if (e.Team == playerTeam)
			{
				StartCoroutine(CameraShakeRoutine(0.5f, 0.15f));
			}
		}


		private IEnumerator CameraShakeRoutine(float duration, float magnitude) 
		{
			float time = 0.0f;

			while (time < duration)
			{
				time += Time.fixedDeltaTime;

				Handle.localPosition = cameraHandleOrigin.localPosition + Random.onUnitSphere * magnitude;

				yield return new WaitForFixedUpdate();
			}

			Handle.localPosition = cameraHandleOrigin.localPosition;
		}
		
	}
}
