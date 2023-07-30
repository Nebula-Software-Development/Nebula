using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939ToggleFocus : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939ToggleFocus(ReferenceHub player, bool state)
    {
        Player = NebuliPlayer.Get(player);
        State = state;
        IsCancelled = false;
    }
    
    public NebuliPlayer Player { get; }
    
    public bool IsCancelled { get; set; }
    
    public bool State { get; set; }
}