namespace Quantum
{
	using Photon.Deterministic;
	using UnityEngine.Scripting;

	[Preserve]
	public unsafe class CarrySystem : SystemMainThreadFilter<CarrySystem.Filter>, ISignalOnCarry, ISignalOnDrop
	{
		public void OnCarry(Frame f, EntityRef item, EntityRef playerEntity)
		{

			var player = f.Unsafe.GetPointer<Player>(playerEntity);

			if (player->CurrentlyCarrying.IsValid) return;
			if (player->CurrentStation.IsValid) return;

			var carryable = f.Unsafe.GetPointer<Carryable>(item);

			if (carryable->Player != EntityRef.None)
			{
				OnDrop(f, carryable->Player);
			}

			carryable->Player = playerEntity;

			player->CurrentlyCarrying = item;

			var phyicsBody = f.Unsafe.GetPointer<PhysicsBody3D>(item);
			phyicsBody->IsKinematic = true;
			phyicsBody->Velocity = FPVector3.Zero;
		}

		public void OnDrop(Frame f, EntityRef playerEntity)
		{
			var filter = f.Filter<Carryable>();
			while (filter.NextUnsafe(out EntityRef item, out Carryable* carryable))
			{
				if (carryable->Player == playerEntity)
				{
					carryable->Player = EntityRef.None;
					var phyicsBody = f.Unsafe.GetPointer<PhysicsBody3D>(item);
					var playerKCC = f.Unsafe.GetPointer<KCC>(playerEntity);
					var player = f.Unsafe.GetPointer<Player>(playerEntity);
					player->CurrentlyCarrying = EntityRef.None;
					phyicsBody->IsKinematic = false;
					phyicsBody->Velocity = playerKCC->RealVelocity;

				}
			}
		}



		public override void Update(Frame f, ref Filter filter)
		{
			if (filter.Carryable->Player.IsValid)
			{
				TrackToPlayer(f, filter.Entity);
			}
		}

		static unsafe void TrackToPlayer(Frame f, EntityRef item)
		{
			var carryable = f.Unsafe.GetPointer<Carryable>(item);
			var playerTransform = f.Unsafe.GetPointer<Transform3D>(carryable->Player);

			var position = InteractSystem.RelativeToWorld(carryable->PositionOffset, *playerTransform);
			var rotation = InteractSystem.RelativeToWorldRotation(carryable->RotationOffset, *playerTransform);

			var transform = f.Unsafe.GetPointer<Transform3D>(item);
			transform->Position = position;
			transform->Rotation = rotation;
		}



		public struct Filter
		{
			public EntityRef Entity;
			public Carryable* Carryable;
		}
	}
}
