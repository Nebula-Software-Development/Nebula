using Interactables.Interobjects.DoorUtils;
using MapGeneration;
using Nebuli.API.Features.Enum;
using Nebuli.API.Features.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Room = Nebuli.API.Features.Map.Room;

namespace Nebuli.API.Features.Doors;

/// <summary>
/// Allows easier use of doors by wrapping the <see cref="DoorVariant"/> class.
/// </summary>
public class Door
{
    private static readonly Dictionary<string, DoorType> nameToDoorType = new()
    {
    { "LCZ_CAFE", DoorType.LczCafe },
    { "173_BOTTOM", DoorType.Scp173Bottom },
    { "330", DoorType.Scp330 },
    { "CHECKPOINT_LCZ_B", DoorType.CheckpointLczB },
    { "HID_RIGHT", DoorType.HIDRight },
    { "SERVERS_BOTTOM", DoorType.ServersBottom },
    { "HID_LEFT", DoorType.HIDLeft },
    { "106_SECONDARY", DoorType.Scp106Secondary },
    { "INTERCOM", DoorType.Intercom },
    { "GR18", DoorType.GR18Gate },
    { "LCZ_WC", DoorType.LczWc },
    { "914", DoorType.Scp914Gate },
    { "SURFACE_GATE", DoorType.SurfaceGate },
    { "LCZ_ARMORY", DoorType.LczArmory },
    { "CHECKPOINT_EZ_HCZ_A", DoorType.CheckpointEzHczA },
    { "NUKE_ARMORY", DoorType.NukeArmory },
    { "ESCAPE_SECONDARY", DoorType.EscapeSecondary },
    { "GATE_B", DoorType.GateB },
    { "939_CRYO", DoorType.Scp939Cryo },
    { "330_CHAMBER", DoorType.Scp330Chamber },
    { "049_ARMORY", DoorType.Scp049Armory },
    { "079_FIRST", DoorType.Scp079First },
    { "079_SECOND", DoorType.Scp079Second },
    { "HID", DoorType.HID },
    { "173_CONNECTOR", DoorType.Scp173Connector },
    { "173_GATE", DoorType.Scp173Gate },
    { "GATE_A", DoorType.GateA },
    { "CHECKPOINT_EZ_HCZ_B", DoorType.CheckpointEzHczB },
    { "GR18_INNER", DoorType.GR18Inner },
    { "106_PRIMARY", DoorType.Scp106Primary },
    { "096", DoorType.Scp096 },
    { "079_ARMORY", DoorType.Scp079Armory },
    { "CHECKPOINT_LCZ_A", DoorType.CheckpointLczA },
    { "ESCAPE_PRIMARY", DoorType.EscapePrimary },
    { "173_ARMORY", DoorType.Scp173Armory },
    { "SURFACE_NUKE", DoorType.NukeSurface },   
    };

    /// <summary>
    /// Gets the dictionary of all the <see cref="DoorVariant"/>, and their wrapper, <see cref="Door"/>.
    /// </summary>

    public static readonly Dictionary<DoorVariant, Door> Dictionary = new();

    internal Door(DoorVariant door)
    {
        Base = door;
        Dictionary.Add(door, this);
    }

    public static IEnumerable<Door> Collection => Dictionary.Values;

    /// <summary>
    /// Gets a list of all the current doors on the server.
    /// </summary>
    public static List<Door> List => Collection.ToList();

    /// <summary>
    /// Gets the doors ID.
    /// </summary>
    public byte DoorID => Base.DoorId;

    /// <summary>
    /// Gets or sets the doors required permissions.
    /// </summary>
    public DoorPermissions RequiredPermissions
    {
        get => Base.RequiredPermissions;
        set => Base.RequiredPermissions = value;
    }

    /// <summary>
    /// Gets the doors base.
    /// </summary>
    public DoorVariant Base { get; }

    /// <summary>
    /// Gets the doors <see cref="DoorType"/>.
    /// </summary>
    public DoorType Type { get; private set; } = DoorType.UnknownDoor;

    /// <summary>
    /// Gets the doors GameObject.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets the door's <see cref="DoorNametagExtension"/>.
    /// </summary>
    public DoorNametagExtension NameTag => Base.GetComponent<DoorNametagExtension>();

    /// <summary>
    /// Gets the doors Transform.
    /// </summary>
    public Transform Transform => Base.transform;

    /// <summary>
    /// Gets the doors current position.
    /// </summary>
    public Vector3 Position => Base.transform.position;

    /// <summary>
    /// Determines if the door can be seen through.
    /// </summary>
    public bool CanSeeThrough
    {
        get => Base.CanSeeThrough;
        set => Base.CanSeeThrough = value;
    }

    /// <summary>
    /// Gets or sets whether the door is opened or not.
    /// </summary>
    public bool IsOpened
    {
        get => Base.IsConsideredOpen();
        set => Base.NetworkTargetState = value;
    }

    /// <summary>
    /// Gets the doors current room.
    /// </summary>
    public Room CurrentRoom
    {
        get => Room.Get(Position);
    }

    /// <summary>
    /// Changes the doors lock to the specified <see cref="DoorLockingType"/>.
    /// </summary>
    /// <param name="type"></param>
    public void ChangeDoorLock(DoorLockingType type)
    {
        if (type is DoorLockingType.None)
            Base.NetworkActiveLocks = 0;
        DoorLockingType activeLocks = (DoorLockingType)Base.NetworkActiveLocks;
        activeLocks ^= type;
        Base.NetworkActiveLocks = (ushort)activeLocks;
        DoorEvents.TriggerAction(Base, IsLocked ? DoorAction.Locked : DoorAction.Unlocked, null);
    }

    /// <summary>
    /// Gets the doors current zone.
    /// </summary>
    public FacilityZone CurrentZone
    {
        get => Room.Get(Position).Zone;
    }

    /// <summary>
    /// Gets the doors active locks.
    /// </summary>
    public ushort ActiveLocks
    {
        get => Base.ActiveLocks;
        set => Base.ActiveLocks = value;
    }

    /// <summary>
    /// Gets if the door is locked.
    /// </summary>
    public bool IsLocked => (DoorLockingType)Base.NetworkActiveLocks is not DoorLockingType.None;

    /// <summary>
    /// Simulates a permission denied action on the door.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="colliderId"></param>
    public void PermissionDenied(NebuliPlayer player, byte colliderId) => Base.PermissionsDenied(player.ReferenceHub, colliderId);

    /// <summary>
    /// Triggers a door action.
    /// </summary>
    public void TriggerDoorAction(DoorVariant door, DoorAction action, NebuliPlayer player) => TriggerDoorAction(door, action, player.ReferenceHub);

    /// <summary>
    /// Triggers a door action.
    /// </summary>   
    public void TriggerDoorAction(DoorVariant door, DoorAction action, ReferenceHub player) => DoorEvents.TriggerAction(door, action, player);

    /// <summary>
    /// Gets a door given the <see cref="DoorVariant"/>.
    /// </summary>
    /// <param name="doorVariant">The <see cref="DoorVariant"/> to use to find the door.</param>
    /// <returns></returns>
    public static Door Get(DoorVariant doorVariant)
    {
        return Dictionary.TryGetValue(doorVariant, out Door door) ? door : new Door(doorVariant);
    }

    /// <summary>
    /// Locks the door.
    /// </summary>
    /// <param name="type"></param>
    public void LockDoor(DoorLockingType type = DoorLockingType.RegularSCP079)
    {
        ChangeDoorLock(type); 
    }

    /// <summary>
    /// Gets if the door can change its current state.
    /// </summary>
    /// <param name="variant">The door variant.</param>
    /// <returns>True if the door can change state, otherwise false.</returns>
    public static bool CanChangeState(DoorVariant variant)
    {
        float exactState = variant.GetExactState();
        bool canChangeState = exactState <= 0f || exactState >= 1f;
        return canChangeState;
    }

    /// <summary>
    /// Unlocks the door.
    /// </summary>
    public void UnLockDoor() => ChangeDoorLock(DoorLockingType.None);

    /// <summary>
    /// Locks the door and unlocks it after a specific time period has passed.
    /// </summary>
    /// <param name="timeToWait">The time to wait before unlocking.</param>
    /// <param name="typeToUnlock">The type of lock to unlock.</param>
    public void UnLockLater(float timeToWait, DoorLockingType typeToUnlock) => Base.UnlockLater(timeToWait, (DoorLockReason)typeToUnlock);

    internal static Door GetDoor(DoorVariant doorVariant) 
    {
        if (Dictionary.ContainsKey(doorVariant)) return Dictionary[doorVariant];

        return doorVariant switch
        {
            Interactables.Interobjects.ElevatorDoor elevatorDoor => new ElevatorDoor(elevatorDoor) { Type = GetDoorType(elevatorDoor) },
            Interactables.Interobjects.PryableDoor pryableDoor => new PryableDoor(pryableDoor) { Type = GetDoorType(pryableDoor) },
            Interactables.Interobjects.BasicDoor basicDoor => new BasicDoor(basicDoor) { Type = GetDoorType(basicDoor) },
            Interactables.Interobjects.CheckpointDoor checkpointDoor => new CheckpointDoor(checkpointDoor) { Type = GetDoorType(checkpointDoor) },           
            _ => new Door(doorVariant) { Type = GetDoorType(doorVariant)},
        };
    }

    internal static DoorType GetDoorType(DoorVariant door)
    {
        if (door.GetComponent<DoorNametagExtension>() is null)
        {
            string doorName = GetSubstringBeforeCharacter(door.name, ' ');

            return doorName switch
            {
                "LCZ" => DoorType.LightContainmentDoor,
                "HCZ" => DoorType.HeavyContainmentDoor,
                "EZ" => DoorType.LightContainmentDoor,
                "Prison" => DoorType.PrisonDoor,
                "914" => DoorType.Scp914Door,
                "Intercom" => DoorType.Intercom,
                "Unsecured" => DoorType.UnsecuredPryableGate,
                "Elevator" => DoorType.UnknownElevator,
                _ => DoorType.UnknownDoor,
            };
         
        }
        if (nameToDoorType.TryGetValue(door.GetComponent<DoorNametagExtension>().GetName, out DoorType doorType))
        {
            return doorType;
        }

        return DoorType.UnknownDoor;

    }
    private static string GetSubstringBeforeCharacter(string input, char character)
    {
        int index = input.IndexOf(character);
        if (index != -1)
        {
            return input.Substring(0, index);
        }
        return input;
    }
}