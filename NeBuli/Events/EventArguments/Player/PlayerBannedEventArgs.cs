using CommandSystem;
using Footprinting;
using Nebuli.API.Features.Player;
using System;

namespace Nebuli.Events.EventArguments.Player;

public class PlayerBannedEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerBannedEventArgs(Footprint target, ICommandSender issuer, string reason, long duration)
    {
        Player = NebuliPlayer.Get(target.Hub);
        Issuer = NebuliPlayer.Get(issuer);
        Reason = reason;
        Duration = duration;
        IsCancelled = false;
    }

    /// <summary>
    /// If the event is cancelled or not.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// The player being banned.
    /// </summary>
    public NebuliPlayer Player { get; }

    /// <summary>
    /// The player issuing the ban.
    /// </summary>
    public NebuliPlayer Issuer { get; }

    /// <summary>
    /// The reason for the ban.
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// The duration of the ban.
    /// </summary>
    public long Duration { get; set; }
}