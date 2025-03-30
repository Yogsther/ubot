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

		public void OnSubmarineDamaged(Frame f, EntityRef submarineEntity, FP damage)
		{
			f.Events.SubmarineDamaged(f.Unsafe.GetPointer<TeamLink>(submarineEntity)->Team);
			var submarine = f.Unsafe.GetPointer<Submarine>(submarineEntity);
			submarine->Health -= damage;

			if(submarine->Health <= FP._0)
			{
				var team = f.Unsafe.GetPointer<TeamLink>(submarineEntity);
				var playerFilter = f.Filter<Player, TeamLink, KCC>();
				var submarineTransform = f.Unsafe.GetPointer<Transform3D>(submarineEntity);

				while (playerFilter.NextUnsafe(out var playerEntity, out Player* player, out TeamLink* teamLink, out KCC* kcc)){

					if (teamLink->Team == team->Team)
					{
						kcc->Teleport(f, submarineTransform->Position);
						kcc->SetGravity(FPVector3.Zero);
					}
				}

				f.Destroy(submarineEntity);

			}
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

		public static EntityRef GetSubmarine(Frame f, TeamRef team)
		{
			var submarines = f.Filter<Submarine, TeamLink>();
			while (submarines.Next(out var entity, out var submarine, out var teamLink))
			{
				if (teamLink.Team == team)
				{
					return entity;
				}
			}
			return EntityRef.None;
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
