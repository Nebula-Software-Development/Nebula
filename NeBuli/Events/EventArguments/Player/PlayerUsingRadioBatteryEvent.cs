// -----------------------------------------------------------------------
// <copyright file=PlayerUsingRadioBatteryEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items.Radio;
using Nebuli.API.Features.Items;
using Nebuli.Events.EventArguments.Interfaces;
using System;
using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments.Player;

/// <summary>
/// Triggered when a player uses a radio battery.
/// </summary>
public class PlayerUsingRadioBatteryEvent : EventArgs, IPlayerEvent, ICancellableEvent
{
    public PlayerUsingRadioBatteryEvent(ReferenceHub player, RadioItem item, float amt)
    {
        Player = API.Features.Player.Get(player);
        Radio = (Radio)Item.Get(item);
        DrainAmount = amt;
        IsCancelled = false;
    }

    /// <summary>
    /// Gets the player triggering the radio.
    /// </summary>
    public API.Features.Player Player { get; }

    /// <summary>
    /// Gets the <see cref="API.Features.Items.Radio"/> being drained.
    /// </summary>
    public Radio Radio { get; }

    /// <summary>
    /// Gets or sets the amount to be drained from the radio.
    /// </summary>
    public float DrainAmount { get; set; }

    /// <summary>
    /// Gets or sets if the event is cancelled or not.
    /// </summary>
    public bool IsCancelled { get; set; }
}