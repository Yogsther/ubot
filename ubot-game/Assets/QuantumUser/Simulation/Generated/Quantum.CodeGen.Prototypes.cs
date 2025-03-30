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


namespace Quantum.Prototypes {
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
  
  [ExcludeFromPrototype()]
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.BasePlayerInput))]
  public unsafe partial class BasePlayerInputPrototype : StructPrototype {
    public FPVector2 MoveDirection;
    public FPVector2 LookRotationDelta;
    public Button Jump;
    public Button Interact;
    public Button SecondInteract;
    public Int32 TextInput;
    partial void MaterializeUser(Frame frame, ref Quantum.BasePlayerInput result, in PrototypeMaterializationContext context);
    public void Materialize(Frame frame, ref Quantum.BasePlayerInput result, in PrototypeMaterializationContext context = default) {
        result.MoveDirection = this.MoveDirection;
        result.LookRotationDelta = this.LookRotationDelta;
        result.Jump = this.Jump;
        result.Interact = this.Interact;
        result.SecondInteract = this.SecondInteract;
        result.TextInput = this.TextInput;
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.Carryable))]
  public unsafe class CarryablePrototype : ComponentPrototype<Quantum.Carryable> {
    public MapEntityId Player;
    public FPVector3 PositionOffset;
    public FPVector3 RotationOffset;
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.Carryable component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.Carryable result, in PrototypeMaterializationContext context = default) {
        PrototypeValidator.FindMapEntity(this.Player, in context, out result.Player);
        result.PositionOffset = this.PositionOffset;
        result.RotationOffset = this.RotationOffset;
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.Input))]
  public unsafe partial class InputPrototype : StructPrototype {
    public Button Interact;
    public Button SecondInteract;
    public Button Jump;
    public Int32 TextInput;
    public Quantum.Prototypes.QuantumThumbSticksPrototype ThumbSticks;
    partial void MaterializeUser(Frame frame, ref Quantum.Input result, in PrototypeMaterializationContext context);
    public void Materialize(Frame frame, ref Quantum.Input result, in PrototypeMaterializationContext context = default) {
        result.Interact = this.Interact;
        result.SecondInteract = this.SecondInteract;
        result.Jump = this.Jump;
        result.TextInput = this.TextInput;
        this.ThumbSticks.Materialize(frame, ref result.ThumbSticks, in context);
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.Interactable))]
  public unsafe partial class InteractablePrototype : ComponentPrototype<Quantum.Interactable> {
    [HideInInspector()]
    public Int32 _empty_prototype_dummy_field_;
    partial void MaterializeUser(Frame frame, ref Quantum.Interactable result, in PrototypeMaterializationContext context);
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.Interactable component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.Interactable result, in PrototypeMaterializationContext context = default) {
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.KCC))]
  public unsafe class KCCPrototype : ComponentPrototype<Quantum.KCC> {
    public AssetRef<KCCSettings> Settings;
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.KCC component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.KCC result, in PrototypeMaterializationContext context = default) {
        result.Settings = this.Settings;
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.KCCCollision))]
  public unsafe class KCCCollisionPrototype : StructPrototype {
    public Quantum.QEnum8<EKCCCollisionSource> Source;
    public MapEntityId Reference;
    public AssetRef Processor;
    public void Materialize(Frame frame, ref Quantum.KCCCollision result, in PrototypeMaterializationContext context = default) {
        result.Source = this.Source;
        PrototypeValidator.FindMapEntity(this.Reference, in context, out result.Reference);
        result.Processor = this.Processor;
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.KCCData))]
  public unsafe partial class KCCDataPrototype : StructPrototype {
    public QBoolean IsActive;
    public FP LookPitch;
    public FP LookYaw;
    public FPVector3 BasePosition;
    public FPVector3 DesiredPosition;
    public FPVector3 TargetPosition;
    public FP DeltaTime;
    public FPVector3 InputDirection;
    public FPVector3 JumpImpulse;
    public FPVector3 Gravity;
    public FP MaxGroundAngle;
    public FP MaxWallAngle;
    public FP MaxHangAngle;
    public FPVector3 ExternalImpulse;
    public FPVector3 ExternalForce;
    public FPVector3 ExternalDelta;
    public FP KinematicSpeed;
    public FPVector3 KinematicTangent;
    public FPVector3 KinematicDirection;
    public FPVector3 KinematicVelocity;
    public FPVector3 DynamicVelocity;
    public FP RealSpeed;
    public FPVector3 RealVelocity;
    public QBoolean HasJumped;
    public QBoolean HasTeleported;
    public QBoolean IsGrounded;
    public QBoolean WasGrounded;
    public QBoolean IsSteppingUp;
    public QBoolean WasSteppingUp;
    public QBoolean IsSnappingToGround;
    public QBoolean WasSnappingToGround;
    public FPVector3 GroundNormal;
    public FPVector3 GroundTangent;
    public FPVector3 GroundPosition;
    public FP GroundDistance;
    public FP GroundAngle;
    partial void MaterializeUser(Frame frame, ref Quantum.KCCData result, in PrototypeMaterializationContext context);
    public void Materialize(Frame frame, ref Quantum.KCCData result, in PrototypeMaterializationContext context = default) {
        result.IsActive = this.IsActive;
        result.LookPitch = this.LookPitch;
        result.LookYaw = this.LookYaw;
        result.BasePosition = this.BasePosition;
        result.DesiredPosition = this.DesiredPosition;
        result.TargetPosition = this.TargetPosition;
        result.DeltaTime = this.DeltaTime;
        result.InputDirection = this.InputDirection;
        result.JumpImpulse = this.JumpImpulse;
        result.Gravity = this.Gravity;
        result.MaxGroundAngle = this.MaxGroundAngle;
        result.MaxWallAngle = this.MaxWallAngle;
        result.MaxHangAngle = this.MaxHangAngle;
        result.ExternalImpulse = this.ExternalImpulse;
        result.ExternalForce = this.ExternalForce;
        result.ExternalDelta = this.ExternalDelta;
        result.KinematicSpeed = this.KinematicSpeed;
        result.KinematicTangent = this.KinematicTangent;
        result.KinematicDirection = this.KinematicDirection;
        result.KinematicVelocity = this.KinematicVelocity;
        result.DynamicVelocity = this.DynamicVelocity;
        result.RealSpeed = this.RealSpeed;
        result.RealVelocity = this.RealVelocity;
        result.HasJumped = this.HasJumped;
        result.HasTeleported = this.HasTeleported;
        result.IsGrounded = this.IsGrounded;
        result.WasGrounded = this.WasGrounded;
        result.IsSteppingUp = this.IsSteppingUp;
        result.WasSteppingUp = this.WasSteppingUp;
        result.IsSnappingToGround = this.IsSnappingToGround;
        result.WasSnappingToGround = this.WasSnappingToGround;
        result.GroundNormal = this.GroundNormal;
        result.GroundTangent = this.GroundTangent;
        result.GroundPosition = this.GroundPosition;
        result.GroundDistance = this.GroundDistance;
        result.GroundAngle = this.GroundAngle;
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.KCCIgnore))]
  public unsafe class KCCIgnorePrototype : StructPrototype {
    public Quantum.QEnum8<EKCCIgnoreSource> Source;
    public MapEntityId Reference;
    public void Materialize(Frame frame, ref Quantum.KCCIgnore result, in PrototypeMaterializationContext context = default) {
        result.Source = this.Source;
        PrototypeValidator.FindMapEntity(this.Reference, in context, out result.Reference);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.KCCModifier))]
  public unsafe class KCCModifierPrototype : StructPrototype {
    public AssetRef Processor;
    public MapEntityId Entity;
    public void Materialize(Frame frame, ref Quantum.KCCModifier result, in PrototypeMaterializationContext context = default) {
        result.Processor = this.Processor;
        PrototypeValidator.FindMapEntity(this.Entity, in context, out result.Entity);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.KCCProcessorLink))]
  public unsafe partial class KCCProcessorLinkPrototype : ComponentPrototype<Quantum.KCCProcessorLink> {
    public AssetRef<KCCProcessor> Processor;
    partial void MaterializeUser(Frame frame, ref Quantum.KCCProcessorLink result, in PrototypeMaterializationContext context);
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.KCCProcessorLink component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.KCCProcessorLink result, in PrototypeMaterializationContext context = default) {
        result.Processor = this.Processor;
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.Player))]
  public unsafe class PlayerPrototype : ComponentPrototype<Quantum.Player> {
    public FP JumpForce;
    [HideInInspector()]
    public PlayerRef PlayerRef;
    public MapEntityId CurrentlyCarrying;
    public MapEntityId CurrentStation;
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.Player component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.Player result, in PrototypeMaterializationContext context = default) {
        result.JumpForce = this.JumpForce;
        result.PlayerRef = this.PlayerRef;
        PrototypeValidator.FindMapEntity(this.CurrentlyCarrying, in context, out result.CurrentlyCarrying);
        PrototypeValidator.FindMapEntity(this.CurrentStation, in context, out result.CurrentStation);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.PlayerFields))]
  public unsafe partial class PlayerFieldsPrototype : ComponentPrototype<Quantum.PlayerFields> {
    public PlayerRef Owner;
    partial void MaterializeUser(Frame frame, ref Quantum.PlayerFields result, in PrototypeMaterializationContext context);
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.PlayerFields component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.PlayerFields result, in PrototypeMaterializationContext context = default) {
        result.Owner = this.Owner;
        MaterializeUser(frame, ref result, in context);
    }
  }
  [ExcludeFromPrototype()]
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.QuantumHighresThumbSticks))]
  public unsafe partial class QuantumHighresThumbSticksPrototype : StructPrototype {
    public InputDirectionMagnitude _leftThumb;
    public InputPitchYaw _rightThumb;
    partial void MaterializeUser(Frame frame, ref Quantum.QuantumHighresThumbSticks result, in PrototypeMaterializationContext context);
    public void Materialize(Frame frame, ref Quantum.QuantumHighresThumbSticks result, in PrototypeMaterializationContext context = default) {
        result._leftThumb = this._leftThumb;
        result._rightThumb = this._rightThumb;
        MaterializeUser(frame, ref result, in context);
    }
  }
  [ExcludeFromPrototype()]
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.QuantumRegularThumbSticks))]
  public unsafe partial class QuantumRegularThumbSticksPrototype : StructPrototype {
    public InputDirectionMagnitude _leftThumb;
    public InputDirectionMagnitude _rightThumb;
    partial void MaterializeUser(Frame frame, ref Quantum.QuantumRegularThumbSticks result, in PrototypeMaterializationContext context);
    public void Materialize(Frame frame, ref Quantum.QuantumRegularThumbSticks result, in PrototypeMaterializationContext context = default) {
        result._leftThumb = this._leftThumb;
        result._rightThumb = this._rightThumb;
        MaterializeUser(frame, ref result, in context);
    }
  }
  [ExcludeFromPrototype()]
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.QuantumThumbSticks))]
  public unsafe partial class QuantumThumbSticksPrototype : UnionPrototype {
    public string _field_used_;
    public Quantum.Prototypes.QuantumRegularThumbSticksPrototype Regular;
    public Quantum.Prototypes.QuantumHighresThumbSticksPrototype HighRes;
    partial void MaterializeUser(Frame frame, ref Quantum.QuantumThumbSticks result, in PrototypeMaterializationContext context);
    public void Materialize(Frame frame, ref Quantum.QuantumThumbSticks result, in PrototypeMaterializationContext context = default) {
        switch (_field_used_) {
          case "REGULAR": this.Regular.Materialize(frame, ref *result.Regular, in context); break;
          case "HIGHRES": this.HighRes.Materialize(frame, ref *result.HighRes, in context); break;
          case "": case null: break;
          default: PrototypeValidator.UnknownUnionField(_field_used_, in context); break;
        }
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.Station))]
  public unsafe class StationPrototype : ComponentPrototype<Quantum.Station> {
    public FPVector3 PlayerPosition;
    public FPVector3 PlayerRotation;
    public MapEntityId Player;
    public MapEntityId Room;
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.Station component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.Station result, in PrototypeMaterializationContext context = default) {
        result.PlayerPosition = this.PlayerPosition;
        result.PlayerRotation = this.PlayerRotation;
        PrototypeValidator.FindMapEntity(this.Player, in context, out result.Player);
        PrototypeValidator.FindMapEntity(this.Room, in context, out result.Room);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.SteerStation))]
  public unsafe partial class SteerStationPrototype : ComponentPrototype<Quantum.SteerStation> {
    public FP Steering;
    public FP SteeringSpeed;
    partial void MaterializeUser(Frame frame, ref Quantum.SteerStation result, in PrototypeMaterializationContext context);
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.SteerStation component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.SteerStation result, in PrototypeMaterializationContext context = default) {
        result.Steering = this.Steering;
        result.SteeringSpeed = this.SteeringSpeed;
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.Submarine))]
  public unsafe partial class SubmarinePrototype : ComponentPrototype<Quantum.Submarine> {
    [Header("Stats")]
    public FP Acceleration;
    public FP TurnSpeed;
    partial void MaterializeUser(Frame frame, ref Quantum.Submarine result, in PrototypeMaterializationContext context);
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.Submarine component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.Submarine result, in PrototypeMaterializationContext context = default) {
        result.Acceleration = this.Acceleration;
        result.TurnSpeed = this.TurnSpeed;
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.SubmarineInterior))]
  public unsafe partial class SubmarineInteriorPrototype : ComponentPrototype<Quantum.SubmarineInterior> {
    [HideInInspector()]
    public Int32 _empty_prototype_dummy_field_;
    partial void MaterializeUser(Frame frame, ref Quantum.SubmarineInterior result, in PrototypeMaterializationContext context);
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.SubmarineInterior component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.SubmarineInterior result, in PrototypeMaterializationContext context = default) {
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.TeamLink))]
  public unsafe partial class TeamLinkPrototype : ComponentPrototype<Quantum.TeamLink> {
    public Quantum.QEnum32<TeamRef> Team;
    partial void MaterializeUser(Frame frame, ref Quantum.TeamLink result, in PrototypeMaterializationContext context);
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.TeamLink component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.TeamLink result, in PrototypeMaterializationContext context = default) {
        result.Team = this.Team;
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.TelescopeStation))]
  public unsafe partial class TelescopeStationPrototype : ComponentPrototype<Quantum.TelescopeStation> {
    [HideInInspector()]
    public Int32 _empty_prototype_dummy_field_;
    partial void MaterializeUser(Frame frame, ref Quantum.TelescopeStation result, in PrototypeMaterializationContext context);
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.TelescopeStation component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.TelescopeStation result, in PrototypeMaterializationContext context = default) {
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.TerminalStation))]
  public unsafe partial class TerminalStationPrototype : ComponentPrototype<Quantum.TerminalStation> {
    [HideInInspector()]
    public Int32 _empty_prototype_dummy_field_;
    partial void MaterializeUser(Frame frame, ref Quantum.TerminalStation result, in PrototypeMaterializationContext context);
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.TerminalStation component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.TerminalStation result, in PrototypeMaterializationContext context = default) {
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.ThrustStation))]
  public unsafe partial class ThrustStationPrototype : ComponentPrototype<Quantum.ThrustStation> {
    public FP Throttle;
    public FP ThrottleSpeed;
    partial void MaterializeUser(Frame frame, ref Quantum.ThrustStation result, in PrototypeMaterializationContext context);
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.ThrustStation component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.ThrustStation result, in PrototypeMaterializationContext context = default) {
        result.Throttle = this.Throttle;
        result.ThrottleSpeed = this.ThrottleSpeed;
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.Torpedo))]
  public unsafe class TorpedoPrototype : ComponentPrototype<Quantum.Torpedo> {
    public FP Acceleration;
    public MapEntityId LoadedIn;
    public QBoolean IsFired;
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.Torpedo component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.Torpedo result, in PrototypeMaterializationContext context = default) {
        result.Acceleration = this.Acceleration;
        PrototypeValidator.FindMapEntity(this.LoadedIn, in context, out result.LoadedIn);
        result.IsFired = this.IsFired;
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.WeaponFireStation))]
  public unsafe partial class WeaponFireStationPrototype : ComponentPrototype<Quantum.WeaponFireStation> {
    public QBoolean CanFire;
    public QBoolean IsOpen;
    partial void MaterializeUser(Frame frame, ref Quantum.WeaponFireStation result, in PrototypeMaterializationContext context);
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.WeaponFireStation component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.WeaponFireStation result, in PrototypeMaterializationContext context = default) {
        result.CanFire = this.CanFire;
        result.IsOpen = this.IsOpen;
        MaterializeUser(frame, ref result, in context);
    }
  }
  [System.SerializableAttribute()]
  [Quantum.Prototypes.Prototype(typeof(Quantum.WeaponLoaderStation))]
  public unsafe class WeaponLoaderStationPrototype : ComponentPrototype<Quantum.WeaponLoaderStation> {
    public FP LoadingSpeed;
    public FPVector3 WeaponRotation;
    public FPVector3 WeaponPositionFrom;
    public FPVector3 WeaponPositionTo;
    public MapEntityId CurrentTorpedo;
    public FP LoadingProgress;
    public override Boolean AddToEntity(FrameBase f, EntityRef entity, in PrototypeMaterializationContext context) {
        Quantum.WeaponLoaderStation component = default;
        Materialize((Frame)f, ref component, in context);
        return f.Set(entity, component) == SetResult.ComponentAdded;
    }
    public void Materialize(Frame frame, ref Quantum.WeaponLoaderStation result, in PrototypeMaterializationContext context = default) {
        result.LoadingSpeed = this.LoadingSpeed;
        result.WeaponRotation = this.WeaponRotation;
        result.WeaponPositionFrom = this.WeaponPositionFrom;
        result.WeaponPositionTo = this.WeaponPositionTo;
        PrototypeValidator.FindMapEntity(this.CurrentTorpedo, in context, out result.CurrentTorpedo);
        result.LoadingProgress = this.LoadingProgress;
    }
  }
}
#pragma warning restore 0109
#pragma warning restore 1591
