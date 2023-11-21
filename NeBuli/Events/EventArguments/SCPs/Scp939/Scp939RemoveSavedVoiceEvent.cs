// -----------------------------------------------------------------------
// <copyright file=Scp939RemoveSavedVoiceEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.Events.EventArguments.Interfaces;
using System;
using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments.SCPs.Scp939;

/// <summary>
/// Triggered when SCP-939 removes a saved player voice.
/// </summary>
public class Scp939RemoveSavedVoiceEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public Scp939RemoveSavedVoiceEvent(ReferenceHub player, ReferenceHub target)
    {
        Player = API.Features.Player.Get(player);
        Target = API.Features.Player.Get(target);
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player removing the saved voice.
    /// </summary>
    public API.Features.Player Player { get; }

    /// <summary>
    /// Gets the voices owner.
    /// </summary>
    public API.Features.Player Target { get; }

    /// <summary>
    /// Gets or sets if the event is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }
}