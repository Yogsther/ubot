using Quantum;
using UnityEngine;

public class WeaponsLoaderView : QuantumEntityViewComponent
{

	[SerializeField] QPrototypeWeaponLoaderStation _prototype;

	public override void OnUpdateView()
	{
		WeaponLoaderStation loaderStation = GetPredictedQuantumComponent<WeaponLoaderStation>();
		
	}

	private void OnDrawGizmos()
	{
		var fromPosition = PlayerGizmos.RelativeToWorld(_prototype.Prototype.WeaponPositionFrom, transform).ToUnityVector3();
		var toPosition = PlayerGizmos.RelativeToWorld(_prototype.Prototype.WeaponPositionTo, transform).ToUnityVector3();
		var rotation = PlayerGizmos.RelativeToWorldRotation(_prototype.Prototype.WeaponRotation, transform).ToUnityQuaternion();

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(fromPosition, 0.1f);
		Gizmos.DrawWireSphere(toPosition, 0.1f);

		Gizmos.color = Color.blue;
		Gizmos.DrawLine(fromPosition, fromPosition + rotation * Vector3.forward);
	}
}
