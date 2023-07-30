using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939PlaceCloud : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939PlaceCloud(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
    }
    
    public NebuliPlayer Player { get; }

    public bool IsCancelled { get; set; }
}