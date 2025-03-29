using Photon.Deterministic;

namespace Quantum
{
	public partial class Config : AssetObject {

		public AssetRef<EntityPrototype> PlayerPrototype;
		public LayerMask InteractableMask;
		public FPVector3 PlayerCameraPosition;
		public FP InteractDistance;
		public AssetRef<EntityPrototype> SubmarinePrototype;
	}
}
