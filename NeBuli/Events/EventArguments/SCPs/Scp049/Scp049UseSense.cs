using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp049;

public class Scp049UseSense : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp049UseSense(ReferenceHub player, ReferenceHub target, float distance)
    {
        Player = NebuliPlayer.Get(player);
        Target = NebuliPlayer.Get(target);
        Distance = distance;
        IsCancelled = false;
    }
    
    public NebuliPlayer Player { get; }
    
    public NebuliPlayer Target { get; }
    
    public float Distance { get; set; }

    public bool IsCancelled { get; set; }
}