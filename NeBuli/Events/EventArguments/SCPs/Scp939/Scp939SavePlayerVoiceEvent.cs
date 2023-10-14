using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

/// <summary>
/// Triggered when SCP-939 saves a player's voice.
/// </summary>
public class Scp939SavePlayerVoiceEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939SavePlayerVoiceEvent(ReferenceHub player, ReferenceHub target)
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