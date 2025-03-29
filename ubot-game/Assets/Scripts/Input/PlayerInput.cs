using Photon.Deterministic;
using Quantum;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
	private Vector2Accumulator _lookRotationAccumulator = new Vector2Accumulator(0.02f, true);

	private BasePlayerInput _accumulatedInput;
	private bool _resetAccumulatedInput;
	private int _lastAccumulateFrame;
	private PolledInput[] _polledInputs = new PolledInput[20];

	private void Update()
	{
		AccumulateInput();
	}

	private struct PolledInput
	{
		public int Frame;
		public BasePlayerInput Input;
	}

	private void AccumulateInput()
	{
		// The accumulation must be processed once per Unity frame.
		// This method is triggered from Update() and PollInput() methods.
		if (_lastAccumulateFrame == Time.frameCount)
			return;

		_lastAccumulateFrame = Time.frameCount;

		if (_resetAccumulatedInput == true)
		{
			_resetAccumulatedInput = false;
			_accumulatedInput = default;
		}

		ProcessStandaloneInput();
	}

	private void ProcessStandaloneInput()
	{
		// Enter key is used for locking/unlocking cursor in game view.
		if (UnityEngine.Input.GetKeyDown(KeyCode.Return) || UnityEngine.Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			else
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}

		// Accumulate input only if the cursor is locked.
		if (Cursor.lockState != CursorLockMode.Locked)
			return;

		Vector2 lookRotationDelta = new Vector2(-UnityEngine.Input.GetAxisRaw("Mouse Y"), UnityEngine.Input.GetAxisRaw("Mouse X"));
		_lookRotationAccumulator.Accumulate(lookRotationDelta);

		Vector2 moveDirection = Vector2.zero;

		if (UnityEngine.Input.GetKey(KeyCode.W)) { moveDirection += Vector2.up; }
		if (UnityEngine.Input.GetKey(KeyCode.S)) { moveDirection += Vector2.down; }
		if (UnityEngine.Input.GetKey(KeyCode.A)) { moveDirection += Vector2.left; }
		if (UnityEngine.Input.GetKey(KeyCode.D)) { moveDirection += Vector2.right; }

		var alphabet = "abcdefghijklmnopqrstuvwxyz!";
		_accumulatedInput.TextInput = -1;
		for (int i = 0; i < alphabet.Length; i++)
		{
			if (UnityEngine.Input.GetKey(alphabet[i].ToString()))
			{
				_accumulatedInput.TextInput = i;
			}
		}

		if (UnityEngine.Input.GetKeyDown(KeyCode.Return))
		{
			_accumulatedInput.TextInput = 26;
		}


		if (UnityEngine.Input.GetMouseButtonDown(0))
		{
			_accumulatedInput.Interact = true;
		}

		if (UnityEngine.Input.GetMouseButtonDown(1))
		{
			_accumulatedInput.SecondInteract = true;
		}

		_accumulatedInput.MoveDirection = moveDirection.normalized.ToFPVector2();

		_accumulatedInput.Jump |= UnityEngine.Input.GetKey(KeyCode.Space);
	}

	private BasePlayerInput GetInputForFrame(int frame)
	{
		if (frame <= 0)
			return default;

		PolledInput polledInput = _polledInputs[frame % _polledInputs.Length];
		if (polledInput.Frame == frame)
			return polledInput.Input;

		return default;
	}

	private void OnEnable()
	{
		QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
	}
	public Vector2 GetPendingLookRotationDelta(QuantumGame game)
	{
		Vector2 pendingLookRotationDelta = default;

		for (int i = 0; i < game.Session.LocalInputOffset; ++i)
		{
			// To get responsive look rotation, we need to apply all pending inputs ahead of predicted tick => these can be delayed by local input offset.
			// For example if the LocalInputOffset == 2, PredictedFrame.Number == 100, inputs for ticks 101 and 102 are already polled and we should apply them as well.
			BasePlayerInput polledInput = GetInputForFrame(game.Frames.Predicted.Number + i + 1);
			pendingLookRotationDelta.x += polledInput.LookRotationDelta.X.AsFloat;
			pendingLookRotationDelta.y += polledInput.LookRotationDelta.Y.AsFloat;
		}

		// The simulation runs with a fixed tick rate which is not aligned with render rate.
		// For local player we also need to add look rotation accumulated since last fixed update to get super smooth look.
		// The _lookRotationAccumulator contains remaining delta since last polled input (which is always simulation tick aligned).
		pendingLookRotationDelta.x += _lookRotationAccumulator.AccumulatedValue.x;
		pendingLookRotationDelta.y += _lookRotationAccumulator.AccumulatedValue.y;

		return pendingLookRotationDelta;
	}

	private void PollInput(CallbackPollInput callback)
	{
		AccumulateInput();

		_resetAccumulatedInput = true;

		Vector2 consumeLookRotation = _lookRotationAccumulator.ConsumeFrameAligned(callback.Game);
		FPVector2 pollLookRotation = BasePlayerInput.GetPollLookRotationDelta(consumeLookRotation.ToFPVector2());

		_lookRotationAccumulator.Add(consumeLookRotation - pollLookRotation.ToUnityVector2());

		_accumulatedInput.LookRotationDelta = pollLookRotation;


		_polledInputs[callback.Frame % _polledInputs.Length] = new PolledInput() { Frame = callback.Frame, Input = _accumulatedInput };
		callback.SetInput(_accumulatedInput, DeterministicInputFlags.Repeatable);
	}
}
