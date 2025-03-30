using Photon.Deterministic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Quantum
{
	[Preserve]
	public unsafe class GameSystem : SystemMainThread, ISignalOnPlayerAdded
	{
		public override void OnInit(Frame f)
		{
			SpawnSubmarine(f, TeamRef.Attacker, new FPVector3(FP._200, FP._0, FP._200));
			SpawnSubmarine(f, TeamRef.Defender, new FPVector3(-FP._200, FP._0, -FP._200));
		}

		public unsafe void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime)
		{
			var playerEntity = f.Create(f.Config.PlayerPrototype);
			var fields = f.Unsafe.GetPointer<Player>(playerEntity);
			var kcc = f.Unsafe.GetPointer<KCC>(playerEntity);
			var transform = f.Unsafe.GetPointer<Transform3D>(playerEntity);

			TeamRef playerTeam = (player % 2) == 0 ? TeamRef.Attacker : TeamRef.Defender;

			TeamLink teamLink = new TeamLink();
			teamLink.Team = playerTeam;

			f.Add(playerEntity, teamLink);

			var spawnPosition = playerTeam == TeamRef.Attacker ? f.Global->AttackerSpawnPoint.Position : f.Global->DefenderSpawnPoint.Position;

			Debug.Log(spawnPosition);

			kcc->Teleport(f, spawnPosition);

			fields->PlayerRef = player;
		}

		public override void Update(Frame f)
		{
		}

		private void SpawnSubmarine(Frame f, TeamRef team, FPVector3 position)
		{
			var submarineEntity = f.Create(f.Config.SubmarinePrototype);

			TeamLink teamLink = new TeamLink();
			teamLink.Team = team;

			f.Add(submarineEntity, teamLink);

			var submarine = f.Unsafe.GetPointer<Submarine>(submarineEntity);
			submarine->Health = submarine->StartHealth;

			if (f.Unsafe.TryGetPointer(submarineEntity, out Transform3D* transform))
			{
				transform->Position = position;
				transform->LookAt(FPVector3.Zero);
			}
		}
	}
}
