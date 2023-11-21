// -----------------------------------------------------------------------
// <copyright file=UpgradingItemEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using InventorySystem.Items.Pickups;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.Events.EventArguments.Interfaces;
using Scp914;

namespace Nebuli.Events.EventArguments.SCPs.Scp914
{
    /// <summary>
    ///     Triggered when a player is upgrading an item in SCP-914.
    /// </summary>
    public class UpgradingItemEvent : EventArgs, ICancellableEvent
    {
        public UpgradingItemEvent(ItemPickupBase pickup, bool upgradeDropped, Scp914KnobSetting knobSetting)
        {
            Pickup = Pickup.Get(pickup);
            IsCancelled = false;
            KnobSetting = knobSetting;
            UpgradeDropped = upgradeDropped;
        }

        /// <summary>
        ///     Gets the <see cref="API.Features.Items.Pickups.Pickup" /> being upgraded.
        /// </summary>
        public Pickup Pickup { get; }

        /// <summary>
        ///     Gets or sets if 914 should upgrade dropped.
        /// </summary>
        public bool UpgradeDropped { get; set; }

        /// <summary>
        ///     Gets or sets SCP-914s <see cref="Scp914KnobSetting" />.
        /// </summary>
        public Scp914KnobSetting KnobSetting { get; set; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}