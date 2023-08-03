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
    
    /// <summary>
    /// Gets the player removing the saved voice.
    /// </summary>
    public NebuliPlayer Player { get; }
    
    /// <summary>
    /// Gets the voices owner.
    /// </summary>
    public NebuliPlayer Target { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}