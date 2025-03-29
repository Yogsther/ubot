using UnityEngine;
using UnityEngine.Scripting;

namespace Quantum
{
	[Preserve]
	public class GameSystem : SystemMainThread, ISignalOnPlayerAdded
	{
		public override void OnInit(Frame f)
		{
			
		}

		public unsafe void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime)
		{
			var playerEntity = f.Create(f.Config.PlayerPrototype);
			var fields = f.Unsafe.GetPointer<Player>(playerEntity);
			var kcc = f.Unsafe.GetPointer<KCC>(playerEntity);

			fields->PlayerRef = player;
		}

		public override void Update(Frame f)
		{
		}
	}
}
