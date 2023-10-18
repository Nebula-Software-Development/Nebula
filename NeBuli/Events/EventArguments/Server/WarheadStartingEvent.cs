using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.Server;

/// <summary>
/// Triggered before the warhead starts its detonation sequence.
/// </summary>
public class WarheadStartingEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public WarheadStartingEvent(bool automatic, bool supressSubtitles, ReferenceHub trigger)
    {
        Player = trigger == null ? API.Features.Server.NebuliHost : NebuliPlayer.Get(trigger);
        SuppressSubtitles = supressSubtitles;
        IsAutomatic = automatic;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player triggering the Alpha Warhead.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// Gets or sets if C.A.S.S.I.E should supress subtitles or not.
    /// </summary>
    public bool SuppressSubtitles { get; set; }

    /// <summary>
    /// Gets or sets if the detonation is a automatic detonation.
    /// </summary>
    public bool IsAutomatic { get; set; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}
