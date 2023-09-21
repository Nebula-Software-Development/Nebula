using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp079;

public class Scp079InteractingTeslaEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp079InteractingTeslaEvent(ReferenceHub player, global::TeslaGate teslaGate)
    {
        Player = NebuliPlayer.Get(player);
        TeslaGate = API.Features.Map.TeslaGate.Get(teslaGate);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player triggering the event.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets the Tesla Gate being triggered.
    /// </summary>
    public API.Features.Map.TeslaGate TeslaGate { get; }
}