// <auto-generated>
// This code was auto-generated by a tool, every time
// the tool executes this code will be reset.
//
// If you need to extend the classes generated to add
// fields or methods to them, please create partial
// declarations in another file.
// </auto-generated>
#pragma warning disable 0109
#pragma warning disable 1591


namespace Quantum.Prototypes.Unity {
  using Photon.Deterministic;
  using Quantum;
  using Quantum.Core;
  using Quantum.Collections;
  using Quantum.Inspector;
  using Quantum.Physics2D;
  using Quantum.Physics3D;
  using Byte = System.Byte;
  using SByte = System.SByte;
  using Int16 = System.Int16;
  using UInt16 = System.UInt16;
  using Int32 = System.Int32;
  using UInt32 = System.UInt32;
  using Int64 = System.Int64;
  using UInt64 = System.UInt64;
  using Boolean = System.Boolean;
  using String = System.String;
  using Object = System.Object;
  using FlagsAttribute = System.FlagsAttribute;
  using SerializableAttribute = System.SerializableAttribute;
  using MethodImplAttribute = System.Runtime.CompilerServices.MethodImplAttribute;
  using MethodImplOptions = System.Runtime.CompilerServices.MethodImplOptions;
  using FieldOffsetAttribute = System.Runtime.InteropServices.FieldOffsetAttribute;
  using StructLayoutAttribute = System.Runtime.InteropServices.StructLayoutAttribute;
  using LayoutKind = System.Runtime.InteropServices.LayoutKind;
  #if QUANTUM_UNITY //;
  using TooltipAttribute = UnityEngine.TooltipAttribute;
  using HeaderAttribute = UnityEngine.HeaderAttribute;
  using SpaceAttribute = UnityEngine.SpaceAttribute;
  using RangeAttribute = UnityEngine.RangeAttribute;
  using HideInInspectorAttribute = UnityEngine.HideInInspector;
  using PreserveAttribute = UnityEngine.Scripting.PreserveAttribute;
  using FormerlySerializedAsAttribute = UnityEngine.Serialization.FormerlySerializedAsAttribute;
  using MovedFromAttribute = UnityEngine.Scripting.APIUpdating.MovedFromAttribute;
  using CreateAssetMenu = UnityEngine.CreateAssetMenuAttribute;
  using RuntimeInitializeOnLoadMethodAttribute = UnityEngine.RuntimeInitializeOnLoadMethodAttribute;
  #endif //;
  
  [System.SerializableAttribute()]
  public unsafe partial class CarryablePrototype : Quantum.QuantumUnityPrototypeAdapter<Quantum.Prototypes.CarryablePrototype> {
    public Quantum.QuantumEntityPrototype Player;
    public FPVector3 PositionOffset;
    public FPVector3 RotationOffset;
    partial void ConvertUser(Quantum.QuantumEntityPrototypeConverter converter, ref Quantum.Prototypes.CarryablePrototype prototype);
    public override Quantum.Prototypes.CarryablePrototype Convert(Quantum.QuantumEntityPrototypeConverter converter) {
      var result = new Quantum.Prototypes.CarryablePrototype();
      converter.Convert(this.Player, out result.Player);
      converter.Convert(this.PositionOffset, out result.PositionOffset);
      converter.Convert(this.RotationOffset, out result.RotationOffset);
      ConvertUser(converter, ref result);
      return result;
    }
  }
  [System.SerializableAttribute()]
  public unsafe partial class KCCPrototype : Quantum.QuantumUnityPrototypeAdapter<Quantum.Prototypes.KCCPrototype> {
    public AssetRef<KCCSettings> Settings;
    partial void ConvertUser(Quantum.QuantumEntityPrototypeConverter converter, ref Quantum.Prototypes.KCCPrototype prototype);
    public override Quantum.Prototypes.KCCPrototype Convert(Quantum.QuantumEntityPrototypeConverter converter) {
      var result = new Quantum.Prototypes.KCCPrototype();
      converter.Convert(this.Settings, out result.Settings);
      ConvertUser(converter, ref result);
      return result;
    }
  }
  [System.SerializableAttribute()]
  public unsafe partial class KCCCollisionPrototype : Quantum.QuantumUnityPrototypeAdapter<Quantum.Prototypes.KCCCollisionPrototype> {
    public Quantum.QEnum8<EKCCCollisionSource> Source;
    public Quantum.QuantumEntityPrototype Reference;
    public AssetRef Processor;
    partial void ConvertUser(Quantum.QuantumEntityPrototypeConverter converter, ref Quantum.Prototypes.KCCCollisionPrototype prototype);
    public override Quantum.Prototypes.KCCCollisionPrototype Convert(Quantum.QuantumEntityPrototypeConverter converter) {
      var result = new Quantum.Prototypes.KCCCollisionPrototype();
      converter.Convert(this.Source, out result.Source);
      converter.Convert(this.Reference, out result.Reference);
      converter.Convert(this.Processor, out result.Processor);
      ConvertUser(converter, ref result);
      return result;
    }
  }
  [System.SerializableAttribute()]
  public unsafe partial class KCCIgnorePrototype : Quantum.QuantumUnityPrototypeAdapter<Quantum.Prototypes.KCCIgnorePrototype> {
    public Quantum.QEnum8<EKCCIgnoreSource> Source;
    public Quantum.QuantumEntityPrototype Reference;
    partial void ConvertUser(Quantum.QuantumEntityPrototypeConverter converter, ref Quantum.Prototypes.KCCIgnorePrototype prototype);
    public override Quantum.Prototypes.KCCIgnorePrototype Convert(Quantum.QuantumEntityPrototypeConverter converter) {
      var result = new Quantum.Prototypes.KCCIgnorePrototype();
      converter.Convert(this.Source, out result.Source);
      converter.Convert(this.Reference, out result.Reference);
      ConvertUser(converter, ref result);
      return result;
    }
  }
  [System.SerializableAttribute()]
  public unsafe partial class KCCModifierPrototype : Quantum.QuantumUnityPrototypeAdapter<Quantum.Prototypes.KCCModifierPrototype> {
    public AssetRef Processor;
    public Quantum.QuantumEntityPrototype Entity;
    partial void ConvertUser(Quantum.QuantumEntityPrototypeConverter converter, ref Quantum.Prototypes.KCCModifierPrototype prototype);
    public override Quantum.Prototypes.KCCModifierPrototype Convert(Quantum.QuantumEntityPrototypeConverter converter) {
      var result = new Quantum.Prototypes.KCCModifierPrototype();
      converter.Convert(this.Processor, out result.Processor);
      converter.Convert(this.Entity, out result.Entity);
      ConvertUser(converter, ref result);
      return result;
    }
  }
  [System.SerializableAttribute()]
  public unsafe partial class PlayerPrototype : Quantum.QuantumUnityPrototypeAdapter<Quantum.Prototypes.PlayerPrototype> {
    public FP JumpForce;
    [HideInInspector()]
    public PlayerRef PlayerRef;
    public Quantum.QuantumEntityPrototype CurrentlyCarrying;
    public Quantum.QuantumEntityPrototype CurrentStation;
    partial void ConvertUser(Quantum.QuantumEntityPrototypeConverter converter, ref Quantum.Prototypes.PlayerPrototype prototype);
    public override Quantum.Prototypes.PlayerPrototype Convert(Quantum.QuantumEntityPrototypeConverter converter) {
      var result = new Quantum.Prototypes.PlayerPrototype();
      converter.Convert(this.JumpForce, out result.JumpForce);
      converter.Convert(this.PlayerRef, out result.PlayerRef);
      converter.Convert(this.CurrentlyCarrying, out result.CurrentlyCarrying);
      converter.Convert(this.CurrentStation, out result.CurrentStation);
      ConvertUser(converter, ref result);
      return result;
    }
  }
  [System.SerializableAttribute()]
  public unsafe partial class StationPrototype : Quantum.QuantumUnityPrototypeAdapter<Quantum.Prototypes.StationPrototype> {
    public FPVector3 PlayerPosition;
    public FPVector3 PlayerRotation;
    public Quantum.QuantumEntityPrototype Player;
    public Quantum.QuantumEntityPrototype Room;
    partial void ConvertUser(Quantum.QuantumEntityPrototypeConverter converter, ref Quantum.Prototypes.StationPrototype prototype);
    public override Quantum.Prototypes.StationPrototype Convert(Quantum.QuantumEntityPrototypeConverter converter) {
      var result = new Quantum.Prototypes.StationPrototype();
      converter.Convert(this.PlayerPosition, out result.PlayerPosition);
      converter.Convert(this.PlayerRotation, out result.PlayerRotation);
      converter.Convert(this.Player, out result.Player);
      converter.Convert(this.Room, out result.Room);
      ConvertUser(converter, ref result);
      return result;
    }
  }
}
#pragma warning restore 0109
#pragma warning restore 1591
