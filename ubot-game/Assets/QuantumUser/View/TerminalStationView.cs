using Quantum;
using TMPro;
using UnityEngine;

public class TerminalStationView : QuantumEntityViewComponent
{
	[SerializeField] TextMeshProUGUI terminalText;

	private void Start()
	{
		QuantumEvent.Subscribe<EventOnTerminalInput>(this, OnTerminalInput);
	}

	private void OnTerminalInput(EventOnTerminalInput callback)
	{
		if (callback.terminal == EntityRef)
		{
			terminalText.text = callback.text;
		}
	}
}
