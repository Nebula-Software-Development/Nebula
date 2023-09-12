﻿using static Broadcast;

namespace Nebuli.API.Features;

/// <summary>
/// Makes showing players broadcasts easier.
/// </summary>
public class Broadcast
{
    /// <summary>
    /// Creates a new <see cref="Broadcast"/> class.
    /// </summary>
    public Broadcast() { }

    /// <summary>
    /// Creates a new <see cref="Broadcast"/> class.
    /// </summary>
    /// <param name="message">The message of the broadcast.</param>
    /// <param name="duration">The duration of the broadcast.</param>
    /// <param name="broadcastFlags">The <see cref="global::Broadcast.BroadcastFlags"/> of the broadcast.</param>
    /// <param name="clearCurrent">If the broadcast will clear the players current broadcast before showing.</param>
    public Broadcast(string message, ushort duration = 5, BroadcastFlags broadcastFlags = BroadcastFlags.Normal, bool clearCurrent = true)
    {
        Message = message;
        Duration = duration;
        BroadcastFlags = broadcastFlags;
        ClearCurrent = clearCurrent;
    }

    /// <summary>
    /// Gets or sets the duration of the broadcast.
    /// </summary>
    public ushort Duration { get; set; }

    /// <summary>
    /// Gets or sets the message of the broadcast.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="global::Broadcast.BroadcastFlags"/> of the broadcast.
    /// </summary>
    public BroadcastFlags BroadcastFlags { get; set; }

    /// <summary>
    /// Gets or sets if the broadcast will clear the players current broadcasts.
    /// </summary>
    public bool ClearCurrent { get; set; }
}