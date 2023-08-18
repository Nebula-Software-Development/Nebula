using UnityEngine;
using static Interactables.Interobjects.ElevatorManager;
using ElevatorDoorBase = Interactables.Interobjects.ElevatorDoor;

namespace Nebuli.API.Features.Doors;

public class ElevatorDoor : Door
{
    /// <summary>
    /// Gets the <see cref="ElevatorDoorBase"/> base.
    /// </summary>
    public new ElevatorDoorBase Base { get; }
    internal ElevatorDoor(ElevatorDoorBase door) : base(door)
    {
        Base = door;
    }

    /// <summary>
    /// Gets the elevators bottom position.
    /// </summary>
    public Vector3 BottomPosition => Base.BottomPosition;

    /// <summary>
    /// Gets the elevators top position.
    /// </summary>
    public Vector3 TopPosition => Base.TopPosition;

    /// <summary>
    /// Gets the elevators group.
    /// </summary>
    public ElevatorGroup Group => Base.Group;   
}
