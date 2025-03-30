using Photon.Deterministic;

namespace Quantum
{
	public partial class Config : AssetObject {

		public AssetRef<EntityPrototype> PlayerPrototype;
		public AssetRef<EntityPrototype> DeadPlayerPrototype;
		public FP DeathVelocity;
		public LayerMask InteractableMask;
		public FPVector3 PlayerCameraPosition;
		public FP InteractDistance;
		public AssetRef<EntityPrototype> SubmarinePrototype;
		public AssetRef<EntityPrototype> TorpedoProjectilePrototype;
		public bool SameTeam;
	}
}
