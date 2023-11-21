// -----------------------------------------------------------------------
// <copyright file=Scp079GainingExperienceEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebuli.Events.EventArguments.Interfaces;
using PlayerRoles.PlayableScps.Scp079;

namespace Nebuli.Events.EventArguments.SCPs.Scp079
{
    /// <summary>
    ///     Triggered when SCP-079 is gaining experience points.
    /// </summary>
    public class Scp079GainingExperienceEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp079GainingExperienceEvent(ReferenceHub player, int amount, Scp079HudTranslation reason)
        {
            Player = API.Features.Player.Get(player);
            Amount = amount;
            Reason = reason;
            IsCancelled = false;
        }

        /// <summary>
        ///     Gets or sets the amount of XP that will be granted.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="Scp079HudTranslation" /> reason.
        /// </summary>
        public Scp079HudTranslation Reason { get; set; }

        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     Gets the player triggering the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}