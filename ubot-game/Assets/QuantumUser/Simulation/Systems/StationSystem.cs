namespace Quantum
{
	using Photon.Deterministic;
	using UnityEngine.Scripting;

	[Preserve]
	public unsafe class StationSystem : SystemMainThreadFilter<StationSystem.Filter>, ISignalOnPlayerEnterStation, ISignalOnPlayerLeaveStation
	{
		public unsafe void OnPlayerEnterStation(Frame f, EntityRef playerEntity, EntityRef stationEntity)
		{
			var player = f.Unsafe.GetPointer<Player>(playerEntity);
			var station = f.Unsafe.GetPointer<Station>(stationEntity);

			if(player->CurrentlyCarrying.IsValid) return;
			if (player->CurrentStation.IsValid) return;
			if (station->Player.IsValid) return;

			player->CurrentStation = stationEntity;
			station->Player = playerEntity;

			TrackPlayerToStation(f, stationEntity);
		}

		public unsafe void OnPlayerLeaveStation(Frame f, EntityRef playerEntity)
		{
			var player = f.Unsafe.GetPointer<Player>(playerEntity);
			if (!player->CurrentStation.IsValid) return;

			var station = f.Unsafe.GetPointer<Station>(player->CurrentStation);

			if (station->Player != playerEntity) return;

			player->CurrentStation = EntityRef.None;
			station->Player = EntityRef.None;
		}

		public override void Update(Frame f, ref Filter filter)
		{
			var teamLink = f.Unsafe.GetPointer<TeamLink>(filter.Station->Room);
			var subFilter = f.Filter<Submarine, TeamLink>();
			Submarine* submarine = null;

			while (subFilter.NextUnsafe(out _, out Submarine* sub, out TeamLink* subTeamLink))
			{
				if(subTeamLink->Team == teamLink->Team) submarine = sub;
			}


			if (filter.Station->Player.IsValid)
			{
				TrackPlayerToStation(f, filter.Entity);

				var player = f.Unsafe.GetPointer<Player>(filter.Station->Player);
				BasePlayerInput input = *f.GetPlayerInput(player->PlayerRef);

				if(f.Unsafe.TryGetPointer<SteerStation>(filter.Entity, out SteerStation* steerStation))
				{
					var moveDirection = input.MoveDirection.XOY;
					steerStation->Steering += moveDirection.X * steerStation->SteeringSpeed * f.DeltaTime;
					steerStation->Steering = FPMath.Clamp(steerStation->Steering, -1, 1);
					submarine->Steering = steerStation->Steering;
				}

				if(f.Unsafe.TryGetPointer(filter.Entity, out ThrustStation* thrustStation))
				{
					var moveDirection = input.MoveDirection;
					thrustStation->Throttle += moveDirection.Y * thrustStation->ThrottleSpeed * f.DeltaTime;
					thrustStation->Throttle = FPMath.Clamp(thrustStation->Throttle, -1, 1);
					submarine->Throttle = thrustStation->Throttle;
				}
			}
		}

		public void TrackPlayerToStation(Frame f, EntityRef stationEntity)
		{
			var station = f.Unsafe.GetPointer<Station>(stationEntity);
			var player = f.Unsafe.GetPointer<Player>(station->Player);

			var playerTransform = f.Unsafe.GetPointer<Transform3D>(station->Player);
			var stationTransform = f.Unsafe.GetPointer<Transform3D>(stationEntity);

			var position = InteractSystem.RelativeToWorld(station->PlayerPosition, *stationTransform);
			var rotation = InteractSystem.RelativeToWorldRotation(station->PlayerRotation, *stationTransform);

			playerTransform->Position = position;
			playerTransform->Rotation = rotation;
		}

		public struct Filter
		{
			public EntityRef Entity;
			public Station* Station;
		}
	}
}
