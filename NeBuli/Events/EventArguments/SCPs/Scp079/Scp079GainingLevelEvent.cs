using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp079;

public class Scp079GainingLevelEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp079GainingLevelEvent(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
    }

    public NebuliPlayer Player { get; }

    public bool IsCancelled { get; set; }
}
