using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939RemoveSavedVoice : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939RemoveSavedVoice(ReferenceHub player, ReferenceHub target)
    {
        Player = NebuliPlayer.Get(player);
        Target = NebuliPlayer.Get(target);
        IsCancelled = false;
    }
    
    public NebuliPlayer Player { get; }
    
    public NebuliPlayer Target { get; }

    public bool IsCancelled { get; set; }
}