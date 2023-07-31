using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939SavePlayerVoice : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939SavePlayerVoice(ReferenceHub player, ReferenceHub target)
    {
        Player = NebuliPlayer.Get(player);
        Target = NebuliPlayer.Get(target);
        IsCancelled = false;
    }
    
    /// <summary>
    /// Gets the player saving the voice.
    /// </summary>
    public NebuliPlayer Player { get; }
    
    /// <summary>
    /// Gets the owner of the voice being saved.
    /// </summary>
    public NebuliPlayer Target { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}