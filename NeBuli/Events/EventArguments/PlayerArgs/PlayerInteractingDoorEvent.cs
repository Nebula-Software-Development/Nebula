using Interactables.Interobjects.DoorUtils;
using Nebuli.API.Features.Doors;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerInteractingDoorEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerInteractingDoorEvent(ReferenceHub player, DoorVariant door)
    {
        Player = API.Features.Player.Get(player);
        Door = Door.Get(door);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player interacting with the door.
    /// </summary>
    public API.Features.Player Player { get; }

    /// <summary>
    /// Gets the <see cref="API.Features.Doors.Door"/> being interacted with.
    /// </summary>
    public Door Door { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
