using Nebuli.API.Features.Doors;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp096;

/// <summary>
/// Triggered when SCP-096 is trying to pry open a gate.
/// </summary>
public class Scp096PryingGateEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp096PryingGateEvent(ReferenceHub player, Interactables.Interobjects.PryableDoor door)
    {
        Player = NebuliPlayer.Get(player);
        PryableDoor = (PryableDoor)Door.Get(door);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player prying the door.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets the <see cref="API.Features.Doors.PryableDoor"/> being pried.
    /// </summary>
    public PryableDoor PryableDoor { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}