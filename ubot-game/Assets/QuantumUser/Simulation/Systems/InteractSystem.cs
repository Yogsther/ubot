namespace Quantum
{
	using JetBrains.Annotations;
	using Photon.Deterministic;
    using UnityEngine.Scripting;

    [Preserve]
    public unsafe class InteractSystem : SystemMainThreadFilter<InteractSystem.Filter>
    {
        public override void Update(Frame f, ref Filter filter)
        {
            var input = f.GetPlayerInput(filter.Player->PlayerRef);

			if (input->SecondInteract.WasPressed)
			{
				f.Signals.OnDrop(filter.Entity);
			}

			if (input->Interact.WasPressed)
            {
				FPVector3 from = RelativeToWorld(f.Config.PlayerCameraPosition, *filter.Transform);
				FPVector2 lookRotation = filter.KCC->GetLookRotation();

				FPQuaternion rotation = FPQuaternion.Euler(lookRotation.X, lookRotation.Y, 0);

				var direction = rotation * FPVector3.Forward;

				direction = direction.Normalized;
				var hit = f.Physics3D.Raycast(from, direction, f.Config.InteractDistance, f.Config.InteractableMask);

				if (hit.HasValue)
				{
					var entity = hit.Value.Entity;
					if (f.Unsafe.TryGetPointer(entity, out Carryable* carryable))
					{
						f.Signals.OnCarry(entity, filter.Entity);
					}

					var hitPosition = hit.Value.Point;
					f.Events.OnGizmoLine(from, hitPosition);
				} else
				{
					f.Events.OnGizmoLine(from, from + direction * f.Config.InteractDistance);
				}
				
			}
		}

		public static FPVector3 RelativeToWorld(FPVector3 position, Transform3D transform)
		{
			return (transform.Position + (transform.Rotation * (position) - transform.Position)) + transform.Position;
		}

		public static FPQuaternion RelativeToWorldRotation(FPVector3 eulerRotation, Transform3D transform)
		{
			FPQuaternion inputRotation = FPQuaternion.Euler(eulerRotation);
			return transform.Rotation * inputRotation;
		}

		public struct Filter
        {
            public EntityRef Entity;
            public Player* Player;
            public Transform3D* Transform;
            public KCC* KCC;
		}
    }
}
