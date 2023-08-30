using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp096;
public class Scp096CalmingEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp096CalmingEvent(ReferenceHub player, bool clearingTime)
    {
        Player = NebuliPlayer.Get(player);
        ClearingTime = clearingTime;
        IsCancelled = false;
    }

    public NebuliPlayer Player { get; }

    public bool ClearingTime { get; set; }

    public bool IsCancelled { get; set; }
}
