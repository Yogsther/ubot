using Quantum;
using UnityEngine;

public class StationGizmos : MonoBehaviour
{
	[SerializeField] QPrototypeStation _prototype;

	private void OnDrawGizmos()
	{
		var position = PlayerGizmos.RelativeToWorld(_prototype.Prototype.PlayerPosition, transform).ToUnityVector3();
		var rotation = PlayerGizmos.RelativeToWorldRotation(_prototype.Prototype.PlayerRotation, transform).ToUnityQuaternion();

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(position, 0.1f);

		Gizmos.color = Color.blue;
		Gizmos.DrawLine(position, position + rotation * Vector3.forward);
	}
}
