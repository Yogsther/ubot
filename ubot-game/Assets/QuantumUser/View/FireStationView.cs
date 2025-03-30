using Quantum;
using UnityEngine;

public class FireStationView : QuantumEntityViewComponent
{
	[SerializeField] Animator animator;
	[SerializeField] GameObject fireButton, depressedButton;

	public override void OnUpdateView()
	{
		WeaponFireStation station = GetPredictedQuantumComponent<WeaponFireStation>();
		animator.SetBool("Open", station.IsOpen);
		fireButton.SetActive(station.CanFire);
		depressedButton.SetActive(!station.CanFire);
	}
}
