using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp173;

public class Scp173PlaceTantrum : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp173PlaceTantrum(ReferenceHub player)
    {
        Player = NebuliPlayer.Get(player);
        IsCancelled = false;
    }
    
    public NebuliPlayer Player { get; }

    public bool IsCancelled { get; set; }
}