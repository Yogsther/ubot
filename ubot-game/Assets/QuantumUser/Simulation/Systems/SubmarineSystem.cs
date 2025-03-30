namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine.Scripting;

    [Preserve]
    public unsafe class SubmarineSystem : SystemMainThreadFilter<SubmarineSystem.Filter>, ISignalOnSubmarineDamaged
    {

		public override void OnInit(Frame f)
		{
			var interiors = f.Filter<SubmarineInterior>();

			while (interiors.Next(out var entity, out var component))
			{
				if (f.Unsafe.TryGetPointer(entity, out Transform3D* transform) && f.Unsafe.TryGetPointer(entity, out TeamLink* teamLink))
				{
					if (teamLink->Team == TeamRef.Attacker)
					{
						f.Global->AttackerSpawnPoint = Transform3D.Create(transform->Position);
					}
					else
					{
						f.Global->DefenderSpawnPoint = Transform3D.Create(transform->Position);
					}
				}
			}
		}

		public void OnSubmarineDamaged(Frame f, EntityRef submarineEntity)
		{

			f.Events.SubmarineDamaged(f.Unsafe.GetPointer<TeamLink>(submarineEntity)->Team);
		}

		public override void Update(Frame f, ref Filter filter)
        {
            FPVector3 accelerationForce = filter.Transform->Forward * filter.Submarine->Throttle * filter.Submarine->Acceleration * f.DeltaTime;
            FPVector3 buoyancyForce = new FPVector3(FP._0, filter.Submarine->TargetDepth - filter.Transform->Position.Y, FP._0) * f.DeltaTime;
            FPVector3 steeringTorque = filter.Transform->Up * filter.Submarine->Steering * filter.Submarine->TurnSpeed * f.DeltaTime;

            filter.PhysicsBody->AddForce(accelerationForce);
			filter.PhysicsBody->AddForce(buoyancyForce);
			filter.PhysicsBody->AddTorque(steeringTorque);
		}
		
        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public PhysicsBody3D* PhysicsBody;
			public Submarine* Submarine;
		}
    }
}
