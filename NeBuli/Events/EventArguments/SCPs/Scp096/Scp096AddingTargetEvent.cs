using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp096;

public class Scp096AddingTargetEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp096AddingTargetEvent(ReferenceHub player, ReferenceHub looker, bool isForLooking)
    {
        Player = NebuliPlayer.Get(player);
        Target = NebuliPlayer.Get(looker);
        LookedAt096 = isForLooking;
        IsCancelled = false;
    }

    public NebuliPlayer Player { get; }

    public NebuliPlayer Target { get; }

    public bool LookedAt096 { get; }

    public bool IsCancelled { get; set; }
}
