namespace Quantum
{
	using UnityEngine;
	using UnityEngine.Events;
	using UnityEngine.UI;

	public unsafe class InteriorView : QuantumEntityViewComponent
	{
		public UnityEvent OnFiredTorpedo;
		public UnityEvent OnTookDamage;
		

		[SerializeField] private RawImage telescopeView;

		[SerializeField] private RenderTexture attackerTelescopeViewTexture;
		[SerializeField] private RenderTexture defenderTelescopeViewTexture;


		[SerializeField] private RectTransform compassNeedle;
		[SerializeField] private RectTransform compassLegend;

		[SerializeField] private RectTransform map;
		[SerializeField] private RectTransform mapIndicator;


		public override void OnActivate(Frame frame)
		{
			QuantumEvent.Subscribe<EventOnTorpedoFired>(this, OnTorpedoFired);
			QuantumEvent.Subscribe<EventSubmarineDamaged>(this, OnSubmarineDamaged);

			telescopeView.texture = frame.Unsafe.GetPointer<TeamLink>(EntityRef)->Team == TeamRef.Attacker ? attackerTelescopeViewTexture : defenderTelescopeViewTexture;
		}

		public override void OnDeactivate()
		{
			QuantumEvent.UnsubscribeListener(this);
		}

		public override void OnUpdateView()
		{
			var f = QuantumRunner.Default.Game.Frames.Predicted;

			var team = GetPredictedQuantumComponent<TeamLink>().Team;

			var subFilter = f.Filter<Submarine, TeamLink, Transform3D>();
			Submarine* submarine = null;

			while (subFilter.NextUnsafe(out _, out Submarine* sub, out TeamLink* subTeamLink, out Transform3D* subTransform))
			{
				if (subTeamLink->Team == team)
				{
					var subPosition = subTransform->Position;

					compassLegend.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -subTransform->Rotation.AsEuler.Y.AsFloat));
					compassNeedle.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, subTransform->Rotation.AsEuler.Y.AsFloat));

					mapIndicator.anchoredPosition = subPosition.XZ.ToUnityVector2() * 0.001f; // new Vector3(Mathf.InverseLerp(-1200.0f, 1200.0f, subPosition.X.AsFloat), Mathf.InverseLerp(-1000.0f, 1000.0f, subPosition.Z.AsFloat), 0.0f);
					mapIndicator.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -subTransform->Rotation.AsEuler.Y.AsFloat));
				}
			}
		}

		private void OnTorpedoFired(EventOnTorpedoFired e)
		{
			if (e.submarineEntity == EntityRef)
			{
				OnFiredTorpedo.Invoke();
			}
		}

		private void OnSubmarineDamaged(EventSubmarineDamaged e)
		{
			var playerTeam = GetPredictedQuantumComponent<TeamLink>().Team;

			if (e.Team == playerTeam)
			{
				OnTookDamage.Invoke();
			}
		}
	}
}
