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

			if (f.Unsafe.TryGetPointer<WeaponFireStation>(stationEntity, out var weaponFireStation))
			{
				var team = f.Unsafe.GetPointer<TeamLink>(station->Room);
				var submarineEntity = f.GetSubmarine(team->Team);
				var submarine = f.Unsafe.GetPointer<Submarine>(submarineEntity);

				if (weaponFireStation->IsOpen)
				{
					if (submarine->HasLoadedTorpedo)
					{
						Log.Debug("Fire!"); // TODO!
						submarine->HasLoadedTorpedo = false;
						weaponFireStation->CanFire = false;
					}
				}

				weaponFireStation->IsOpen = !weaponFireStation->IsOpen;
				return;
			}

			if (f.Unsafe.TryGetPointer<WeaponLoaderStation>(stationEntity, out WeaponLoaderStation* weaponLoaderStation))
			{
				if (f.Unsafe.TryGetPointer<Torpedo>(player->CurrentlyCarrying, out Torpedo* torpedo))
				{
					if (weaponLoaderStation->CurrentTorpedo.IsValid) return;
					else
					{
						var submarineFilter = f.Filter<Submarine, TeamLink>();
						var teamLink = f.Unsafe.GetPointer<TeamLink>(station->Room);
						Submarine* submarine = null;

						while (submarineFilter.NextUnsafe(out _, out Submarine* sub, out TeamLink* subTeamLink))
						{
							if (subTeamLink->Team == teamLink->Team) submarine = sub;
						}

						if (submarine->HasLoadedTorpedo) return;

						var torpedoCarryable = f.Unsafe.GetPointer<Carryable>(player->CurrentlyCarrying);
						torpedoCarryable->Player = EntityRef.None;

						var torpedoBody = f.Unsafe.GetPointer<PhysicsBody3D>(player->CurrentlyCarrying);
						weaponLoaderStation->CurrentTorpedo = player->CurrentlyCarrying;
						player->CurrentlyCarrying = EntityRef.None;
						torpedo->LoadedIn = stationEntity;
						weaponLoaderStation->LoadingProgress = 0;

						torpedoBody->IsKinematic = true;
						TrackTorpedoToWeaponsStation(f, stationEntity);
					}
				}
			}
			else if (player->CurrentlyCarrying.IsValid) return;

			if (player->CurrentStation.IsValid) return;
			if (station->Player.IsValid) return;

			player->CurrentStation = stationEntity;
			station->Player = playerEntity;

			var kcc = f.Unsafe.GetPointer<KCC>(playerEntity);
			kcc->SetActive(false);

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

			var kcc = f.Unsafe.GetPointer<KCC>(playerEntity);
			kcc->SetActive(true);
		}

		public override void Update(Frame f, ref Filter filter)
		{
			var teamLink = f.Unsafe.GetPointer<TeamLink>(filter.Station->Room);
			var subFilter = f.Filter<Submarine, TeamLink>();
			Submarine* submarine = null;

			while (subFilter.NextUnsafe(out _, out Submarine* sub, out TeamLink* subTeamLink))
			{
				if (subTeamLink->Team == teamLink->Team) submarine = sub;
			}


			if (filter.Station->Player.IsValid)
			{
				TrackPlayerToStation(f, filter.Entity);

				var player = f.Unsafe.GetPointer<Player>(filter.Station->Player);
				BasePlayerInput input = *f.GetPlayerInput(player->PlayerRef);

				if (f.Unsafe.TryGetPointer<SteerStation>(filter.Entity, out SteerStation* steerStation))
				{
					var moveDirection = input.MoveDirection.XOY;
					steerStation->Steering += moveDirection.X * steerStation->SteeringSpeed * f.DeltaTime;
					steerStation->Steering = FPMath.Clamp(steerStation->Steering, -1, 1);
					submarine->Steering = steerStation->Steering;
				}

				if(f.Unsafe.TryGetPointer(filter.Entity, out TelescopeStation* telescopeStation))
				{
					var moveDirection = input.MoveDirection;
					telescopeStation->Rotation += moveDirection.X * telescopeStation->RotationSpeed * f.DeltaTime;
					telescopeStation->Rotation = FPMath.Clamp(telescopeStation->Rotation, -telescopeStation->MaxRotation, telescopeStation->MaxRotation);

					var stationTransform = f.Unsafe.GetPointer<Transform3D>(filter.Entity);

					var centerRotation = telescopeStation->CenterRotation;
					stationTransform->Rotation = FPQuaternion.Euler(centerRotation.X, centerRotation.Y + telescopeStation->Rotation, centerRotation.Z);

					submarine->TelescopeRotation = telescopeStation->Rotation;
				}

				if (f.Unsafe.TryGetPointer(filter.Entity, out ThrustStation* thrustStation))
				{
					var moveDirection = input.MoveDirection;
					thrustStation->Throttle += moveDirection.Y * thrustStation->ThrottleSpeed * f.DeltaTime;
					thrustStation->Throttle = FPMath.Clamp(thrustStation->Throttle, -1, 1);
					submarine->Throttle = thrustStation->Throttle;
				}

				if (f.Unsafe.TryGetPointer(filter.Entity, out TerminalStation* terminalStation))
				{
					if (input.TextInput != -1)
					{
						string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
						f.Events.OnTerminalInput(filter.Entity, alphabet[input.TextInput].ToString());
					}
				}

				if (f.Unsafe.TryGetPointer(filter.Entity, out WeaponLoaderStation* weaponLoaderStation))
				{
					if (weaponLoaderStation->CurrentTorpedo.IsValid)
					{
						var moveDirection = input.MoveDirection;
						weaponLoaderStation->LoadingProgress += moveDirection.Y * weaponLoaderStation->LoadingSpeed * f.DeltaTime;
						if (weaponLoaderStation->LoadingProgress < 0)
						{
							weaponLoaderStation->LoadingProgress = 0;
						}
						else if (weaponLoaderStation->LoadingProgress > 1)
						{
							submarine->HasLoadedTorpedo = true;

							var weaponsFireStations = f.Filter<WeaponFireStation, Station>();
							while (weaponsFireStations.NextUnsafe(out _, out WeaponFireStation* fireStation, out Station* station))
							{
								var fireStationTeamLink = f.Unsafe.GetPointer<TeamLink>(station->Room);
								if (fireStationTeamLink->Team == teamLink->Team)
								{
									fireStation->CanFire = true;
								}
							}

							weaponLoaderStation->LoadingProgress = 0;
							f.Destroy(weaponLoaderStation->CurrentTorpedo);
							weaponLoaderStation->CurrentTorpedo = EntityRef.None;
							return;
						}

						TrackTorpedoToWeaponsStation(f, filter.Entity);
					}
				}
			}
		}

		public void TrackTorpedoToWeaponsStation(Frame f, EntityRef stationEntity)
		{
			var weaponsLoadingStation = f.Unsafe.GetPointer<WeaponLoaderStation>(stationEntity);
			var torpedoPhysicsBody = f.Unsafe.GetPointer<PhysicsBody3D>(weaponsLoadingStation->CurrentTorpedo);
			var torpedoTransform = f.Unsafe.GetPointer<Transform3D>(weaponsLoadingStation->CurrentTorpedo);
			var stationTransform = f.Unsafe.GetPointer<Transform3D>(stationEntity);

			var fromPosition = weaponsLoadingStation->WeaponPositionFrom;
			var toPosition = weaponsLoadingStation->WeaponPositionTo;
			var progress = weaponsLoadingStation->LoadingProgress;

			var localPosition = FPVector3.Lerp(fromPosition, toPosition, progress);
			var position = InteractSystem.RelativeToWorld(localPosition, *stationTransform);
			var rotation = InteractSystem.RelativeToWorldRotation(weaponsLoadingStation->WeaponRotation, *stationTransform);

			torpedoTransform->Position = position;
			torpedoTransform->Rotation = rotation;
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
