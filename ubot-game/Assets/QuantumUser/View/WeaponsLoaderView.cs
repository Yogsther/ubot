using Quantum;
using UnityEngine;

public class WeaponsLoaderView : QuantumEntityViewComponent
{
	

	public override void OnUpdateView()
	{
		WeaponLoaderStation loaderStation = GetPredictedQuantumComponent<WeaponLoaderStation>();
		
	}
}
