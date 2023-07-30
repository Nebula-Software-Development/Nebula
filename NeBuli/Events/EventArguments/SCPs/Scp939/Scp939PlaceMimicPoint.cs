using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939PlaceMimicPoint : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939PlaceMimicPoint(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
    }
    
    public NebuliPlayer Player { get; }

    public bool IsCancelled { get; set; }
}