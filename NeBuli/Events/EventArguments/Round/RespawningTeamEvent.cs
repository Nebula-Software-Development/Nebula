// -----------------------------------------------------------------------
// <copyright file=RespawningTeamEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Nebula.Events.EventArguments.Interfaces;
using Respawning;

namespace Nebula.Events.EventArguments.Round
{
    /// <summary>
    ///     Triggered when a team is respawning.
    /// </summary>
    public class RespawningTeamEvent : EventArgs, ICancellableEvent
    {
        public RespawningTeamEvent(List<ReferenceHub> respawningPlayers, SpawnableTeamType respawningTeam,
            int maxWaveSize)
        {
            PlayersRespawning = respawningPlayers;
            IsCancelled = false;
            MaxWaveSize = maxWaveSize;
            RespawningTeamType = respawningTeam;
        }

        /// <summary>
        ///     Gets or sets the max wave size for this respawn.
        /// </summary>
        public int MaxWaveSize { get; set; }

        /// <summary>
        ///     Gets a <see cref="List{ReferenceHub}" /> of all the players respawning.
        /// </summary>
        public List<ReferenceHub> PlayersRespawning { get; set; }

        /// <summary>
        ///     Gets the respawns <see cref="SpawnableTeamType" />.
        /// </summary>
        public SpawnableTeamType RespawningTeamType { get; }

        /// <summary>
        ///     Gets if the event is cancelled or not.
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}