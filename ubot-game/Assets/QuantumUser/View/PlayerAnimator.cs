using Photon.Deterministic;
using Quantum;
using UnityEngine;

public class PlayerAnimator : QuantumEntityViewComponent<SceneContext>
{
    [SerializeField] Animator animator;

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

		animator.SetFloat("Speed", speed);
		animator.SetBool("Carrying", player.IsCarrying);
	}

}
