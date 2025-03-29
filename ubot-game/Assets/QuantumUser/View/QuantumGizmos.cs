
using Quantum;
using System.Collections.Generic;
using UnityEngine;

public class QuantumGizmos : MonoBehaviour
{
	struct GizmoLine
	{
		public Vector3 From, To;
	}

	List<GizmoLine> lines = new List<GizmoLine>();

    void Start()
    {
		QuantumEvent.Subscribe<EventOnGizmoLine>(this, OnGizmoLine);
    }

	private void OnGizmoLine(EventOnGizmoLine callback)
	{
		lines.Add(new GizmoLine
		{
			From = callback.From.ToUnityVector3(),
			To = callback.To.ToUnityVector3()
		});
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		foreach (var line in lines)
		{
			Gizmos.DrawLine(line.From, line.To);
		}
	}
}
