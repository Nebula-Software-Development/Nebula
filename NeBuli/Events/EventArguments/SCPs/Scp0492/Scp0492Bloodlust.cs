using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp0492;

public class Scp0492Bloodlust : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp0492Bloodlust(ReferenceHub player, ReferenceHub observer)
    {
        Player = NebuliPlayer.Get(player);
        Observer = NebuliPlayer.Get(observer);
        IsCancelled = false;
    }
    
    public NebuliPlayer Player { get; }
    
    public NebuliPlayer Observer { get; }

    public bool IsCancelled { get; set; }
}