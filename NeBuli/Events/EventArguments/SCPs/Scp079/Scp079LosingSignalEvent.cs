using Nebuli.API.Features.Player;
using PlayerRoles;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp079;

public class Scp079LosingSignalEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp079LosingSignalEvent(PlayerRoleBase player, float timeToLoseSignal)
    {
        if (player.TryGetOwner(out ReferenceHub hub))
        {
            Player = NebuliPlayer.Get(hub);
        }
        DurationOfSignalLoss = timeToLoseSignal;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player losing signal.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Gets or sets the duration of the signal loss.
    /// </summary>
    public float DurationOfSignalLoss { get; set; }
}
