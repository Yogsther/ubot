using Photon.Deterministic;
using Quantum;
using UnityEngine;

public class PlayerGizmos : MonoBehaviour
{
	[SerializeField] AssetRef<Config> configAsset;

	private void OnDrawGizmos()
	{
		var config = QuantumUnityDB.GetGlobalAsset<Config>(configAsset.Id);

		var cameraPosition = RelativeToWorld(config.PlayerCameraPosition, transform);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(cameraPosition.ToUnityVector3(), 0.1f);
	}

	public static FPVector3 RelativeToWorld( FPVector3 position, Transform3D transform)
	{
		return (transform.Position + (transform.Rotation * (position) - transform.Position)) + transform.Position;
	}

	public static FPVector3 InversRelativeToWorld( FPVector3 position, Transform3D transform)
	{
		return FPQuaternion.Inverse(transform.Rotation) * (position - transform.Position);
	}

	public static FPQuaternion RelativeToWorldRotation( FPVector3 eulerRotation, Transform3D transform)
	{
		FPQuaternion inputRotation = FPQuaternion.Euler(eulerRotation);
		return transform.Rotation * inputRotation;
	}

	public static FPQuaternion RelativeToWorldRotation(FPVector3 eulerRotation, Transform transform)
	{
		FPQuaternion inputRotation = FPQuaternion.Euler(eulerRotation);
		return transform.rotation.ToFPQuaternion() * inputRotation;
	}

	public static FPVector3 RelativeToWorld( FPVector3 position, Transform transform)
	{
		return (transform.position.ToFPVector3() + (transform.rotation.ToFPQuaternion() * (position) - transform.position.ToFPVector3())) + transform.position.ToFPVector3();
	}
}
