namespace Quantum
{
    using UnityEngine;
	using UnityEngine.Events;

	public unsafe class TorpedoView : QuantumEntityViewComponent
    {
		public UnityEvent OnPickupItem;

        [SerializeField] private GameObject model;


		public override void OnActivate(Frame frame)
		{
			QuantumEvent.Subscribe<EventOnPickup>(this, OnPickup);
		}
		
		public override void OnDeactivate()
		{
			QuantumEvent.UnsubscribeListener(this);
		}


		private void OnPickup(EventOnPickup e)
		{
			OnPickupItem.Invoke();
		}
	}
}
