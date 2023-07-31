using System;
using Nebuli.API.Features.Player;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

public class Scp939PlayVoice : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939PlayVoice(ReferenceHub player, ReferenceHub target)
    {
        Player = NebuliPlayer.Get(player);
        VoicePlayer = NebuliPlayer.Get(target);
        IsCancelled = false;
    }
    
    /// <summary>
    /// Gets the player playing the voice.
    /// </summary>
    public NebuliPlayer Player { get; }
    
    /// <summary>
    /// Gets the voices player owner.
    /// </summary>
    public NebuliPlayer VoicePlayer { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}