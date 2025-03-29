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

		public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime)
		{
			var playerEntity = f.Create(f.Config.PlayerPrototype);

			Player fields = new Player()
			{
				PlayerRef = player,
				JumpForce = 10,
			};

			f.Add(playerEntity, fields);
		}

		public override void Update(Frame f)
		{
		}
	}
}
