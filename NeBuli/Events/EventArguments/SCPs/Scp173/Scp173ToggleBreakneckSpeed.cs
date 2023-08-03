using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp173;

public class Scp173ToggleBreakneckSpeed : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp173ToggleBreakneckSpeed(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
    }
    
    public NebuliPlayer Player { get; }

    public bool IsCancelled { get; set; }
}