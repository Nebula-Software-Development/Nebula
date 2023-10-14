using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

/// <summary>
/// Triggered when SCP-939 plays a sound.
/// </summary>
public class Scp939PlaySound : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939PlaySound(ReferenceHub player, byte option)
    {
        Player = NebuliPlayer.Get(player);
        SoundOption = option;
        IsCancelled = false;
    }

    /// <summary>
    /// The player playing the sound.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// The sound to be played.
    /// </summary>
    public byte SoundOption { get; }
}