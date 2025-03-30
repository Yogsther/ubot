namespace Quantum
{
    using UnityEngine;
	
    public unsafe class MapView : QuantumEntityViewComponent
    {
		[SerializeField] private RectTransform map;
		[SerializeField] private RectTransform indicator;

		public override void OnUpdateView()
        {
			var f = QuantumRunner.Default.Game.Frames.Predicted;

			var team = GetPredictedQuantumComponent<TeamLink>().Team;

			Debug.Log(team);
			
			var subFilter = f.Filter<Submarine, TeamLink, Transform3D>();
			Submarine* submarine = null;

			while (subFilter.NextUnsafe(out _, out Submarine* sub, out TeamLink* subTeamLink, out Transform3D* subTransform))
			{
				if (subTeamLink->Team == team)
				{
					var subPosition = subTransform->Position;
					
					indicator.anchoredPosition = subPosition.XZ.ToUnityVector2() * 0.001f; // new Vector3(Mathf.InverseLerp(-1200.0f, 1200.0f, subPosition.X.AsFloat), Mathf.InverseLerp(-1000.0f, 1000.0f, subPosition.Z.AsFloat), 0.0f);
					indicator.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -subTransform->Rotation.AsEuler.Y.AsFloat));
				}
			}
		}
    }
}
