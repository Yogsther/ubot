namespace Quantum
{
    using UnityEngine;

    public unsafe class SubmarineView : QuantumEntityViewComponent
    {
        [SerializeField] private Camera telescopeCamera;

        [SerializeField] private RenderTexture attackerTelescopeViewTexture;
        [SerializeField] private RenderTexture defenderTelescopeViewTexture;

        public override void OnActivate(Frame frame)
		{
			
			telescopeCamera.targetTexture = frame.Unsafe.GetPointer<TeamLink>(EntityRef)->Team == TeamRef.Attacker ? attackerTelescopeViewTexture : defenderTelescopeViewTexture;
        }

		public override void OnUpdateView()
		{
			Submarine submarine = GetPredictedQuantumComponent<Submarine>();
			telescopeCamera.transform.localRotation = Quaternion.Euler(0, submarine.TelescopeRotation.AsFloat, 0);
		}
	}
}
