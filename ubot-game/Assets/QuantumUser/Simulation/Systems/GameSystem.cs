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
			Log.Debug("Player added!");

		}

		public override void Update(Frame f)
		{
		}
	}
}
