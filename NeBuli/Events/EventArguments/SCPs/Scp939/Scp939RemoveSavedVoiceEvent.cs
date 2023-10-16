using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

/// <summary>
/// Triggered when SCP-939 removes a saved player voice.
/// </summary>
public class Scp939RemoveSavedVoiceEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939RemoveSavedVoiceEvent(ReferenceHub player, ReferenceHub target)
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