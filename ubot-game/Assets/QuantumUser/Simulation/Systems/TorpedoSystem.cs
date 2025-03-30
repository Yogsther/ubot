namespace Quantum
{
    using Photon.Deterministic;
    using UnityEngine.Scripting;

    [Preserve]
    public unsafe class TorpedoSystem : SystemMainThreadFilter<TorpedoSystem.Filter>, ISignalOnTorpedoFired, ISignalOnCollisionEnter3D
    {
		

		public void OnTorpedoFired(Frame f, TeamRef firingTeam)
		{
			var subFilter = f.Filter<Submarine, TeamLink, Transform3D>();
			Submarine* submarine = null;
			
			while (subFilter.NextUnsafe(out _, out Submarine* sub, out TeamLink* subTeamLink, out Transform3D* subTransform))
			{
				if (subTeamLink->Team == firingTeam)
				{
					var subPosition = subTransform->Position + (subTransform->Forward * FP._10);
					var subDirection = subTransform->Forward;
					
					SpawnTorpedo(f, subPosition, subDirection, firingTeam);
				}
			}
		}

		public void OnCollisionEnter3D(Frame f, CollisionInfo3D info)
		{
			if (!f.Unsafe.TryGetPointer(info.Entity, out Torpedo* torpedo))
				return;

			if (!f.Unsafe.TryGetPointer(info.Other, out Submarine* hitSubmarine))
				return;

			if (!f.Unsafe.TryGetPointer(info.Other, out TeamLink* hitSubmarineTeamLink))
				return;

			f.Destroy(info.Entity);
			f.Signals.OnSubmarineDamaged(info.Other);
			
		}

		public override void Update(Frame f, ref Filter filter)
        {
			if (!filter.Torpedo->IsFired)
			{
				return;
			}

			filter.PhysicsBody->AddForce(filter.Transform->Forward * filter.Torpedo->Acceleration * f.DeltaTime);
		}

		private void SpawnTorpedo(Frame f, FPVector3 position, FPVector3 direction, TeamRef team)
		{
			var torpedoEntity = f.Create(f.Config.TorpedoProjectilePrototype);
			var torpedo = f.Unsafe.GetPointer<Torpedo>(torpedoEntity);
			var physicsBody = f.Unsafe.GetPointer<PhysicsBody3D>(torpedoEntity);
			var transform = f.Unsafe.GetPointer<Transform3D>(torpedoEntity);

			TeamLink teamLink = new TeamLink();
			teamLink.Team = team;

			f.Add(torpedoEntity, teamLink);

			physicsBody->GravityScale = FP._0;

			transform->Position = position;
			transform->Rotation = FPQuaternion.LookRotation(direction);

			torpedo->IsFired = true;
		}

        public struct Filter
        {
            public EntityRef Entity;
            public Torpedo* Torpedo;
			public Transform3D* Transform;
			public PhysicsBody3D* PhysicsBody;
			
		}
    }
}
