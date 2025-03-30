using Photon.Deterministic;
using Quantum;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimator : QuantumEntityViewComponent<SceneContext>
{
	public UnityEvent OnFootstep;
	

    [SerializeField] Animator animator;


	[SerializeField] private float footstepThreshold = 2.0f;
	private float footstepValue;

	public override void OnUpdateView()
	{
		KCC kcc = GetPredictedQuantumComponent<KCC>();
		Player player = GetPredictedQuantumComponent<Player>();
		FPQuaternion rotation = kcc.Data.LookRotation;
		FPVector3 velocity = kcc.RealVelocity;
		FPVector3 forward = rotation * FPVector3.Forward;

		float dotProduct = FPVector3.Dot(velocity, forward).AsFloat;
		float speed = velocity.Magnitude.AsFloat;
		if (dotProduct < 0) 
		{
			speed = -speed;
		}

		if (player.CurrentStation.IsValid) speed = 0;

		footstepValue += speed;
		if (footstepValue > footstepThreshold)
		{
			footstepValue = 0;
			OnFootstep.Invoke();
		}

		animator.SetFloat("Speed", speed);
		animator.SetBool("Carrying", player.CurrentlyCarrying.IsValid || player.CurrentStation.IsValid);
	}

}
