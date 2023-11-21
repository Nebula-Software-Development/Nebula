// -----------------------------------------------------------------------
// <copyright file=CustomHealthManager.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using PlayerStatsSystem;

namespace Nebuli.API.Internal
{
    public class CustomHealthManager : HealthStat
    {
        public override float MaxValue => MaxHealth == default ? base.MaxValue : MaxHealth;
        public override float MinValue => MinHealth == default ? base.MinValue : MinHealth;

        /// <summary>
        ///     Gets or sets the max health of the player.
        /// </summary>
        public float MaxHealth { get; set; }

        /// <summary>
        ///     Gets or sets the minimum health of the player.
        /// </summary>
        public float MinHealth { get; set; }
    }
}