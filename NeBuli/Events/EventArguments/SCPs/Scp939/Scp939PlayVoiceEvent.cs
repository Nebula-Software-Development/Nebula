// -----------------------------------------------------------------------
// <copyright file=Scp939PlayVoiceEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Interfaces;
using System;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

/// <summary>
/// Triggered when SCP-939 plays a voice.
/// </summary>
public class Scp939PlayVoiceEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939PlayVoiceEvent(ReferenceHub player, ReferenceHub target)
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