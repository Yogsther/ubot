namespace Quantum
{
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
			if(filter.Station->Player.IsValid)
			{
				TrackPlayerToStation(f, filter.Entity);
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
