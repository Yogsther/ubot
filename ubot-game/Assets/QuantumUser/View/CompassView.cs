namespace Quantum
{
    using UnityEngine;

    public unsafe class CompassView : QuantumEntityViewComponent
    {

		[SerializeField] private RectTransform compassNeedle;

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
					Debug.Log("Got here!");

					Debug.Log(Mathf.Rad2Deg * subTransform->Rotation.Y.AsFloat);
					compassNeedle.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, Mathf.Rad2Deg * subTransform->Rotation.Y.AsFloat));
				}
			}
		}
    }
}
