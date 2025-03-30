namespace Quantum
{
    using UnityEngine;

    public unsafe class TorpedoView : QuantumEntityViewComponent
    {
        [SerializeField] private GameObject model;

		public override void OnActivate(Frame frame)
		{
			if (!frame.Unsafe.TryGetPointer(EntityRef, out Torpedo* torpedo))
				return;

			model.layer = torpedo->IsFired ? 9 : 6;
		}
	}
}
