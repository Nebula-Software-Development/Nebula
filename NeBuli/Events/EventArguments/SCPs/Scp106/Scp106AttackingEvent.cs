using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp106;

public class Scp106AttackingEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp106AttackingEvent(ReferenceHub scp, ReferenceHub target)
    {
        Player = NebuliPlayer.Get(scp);
        Target = NebuliPlayer.Get(target);
        IsCancelled = false;
        Log.Info(scp);
    }

    public NebuliPlayer Player { get; }

    public NebuliPlayer Target { get; }

    public bool IsCancelled { get; set; }
}
