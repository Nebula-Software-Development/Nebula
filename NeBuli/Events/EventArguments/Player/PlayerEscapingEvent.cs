// -----------------------------------------------------------------------
// <copyright file=PlayerEscapingEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebuli.API.Features.Enum;
using Nebuli.Events.EventArguments.Interfaces;
using PlayerRoles;
using Respawning;
using static Escape;

namespace Nebuli.Events.EventArguments.Player
{
    /// <summary>
    ///     Triggered when a player escapes from the facility.
    /// </summary>
    public class PlayerEscapingEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public PlayerEscapingEvent(ReferenceHub player, RoleTypeId newRole, EscapeScenarioType escapeType,
            EscapeMessage escapeMessage, SpawnableTeamType team)
        {
            Player = API.Features.Player.Get(player);
            NewRole = newRole;
            EscapeMessage = escapeMessage;
            OldRole = player.GetRoleId();
            EscapeScenario = (EscapeType)escapeType;
            TeamGettingTickets = team;
            IsCancelled = false;
            if (EscapeScenario == EscapeType.PluginEscape)
            {
                IsCancelled = true;
            }
        }

        /// <summary>
        ///     Gets the <see cref="Escape.EscapeMessage" /> for the event.
        /// </summary>
        public EscapeMessage EscapeMessage { get; set; }

        /// <summary>
        ///     The new role the escaping event will use.
        /// </summary>
        public RoleTypeId NewRole { get; set; }

        /// <summary>
        ///     The previous role of the player before the event.
        /// </summary>
        public RoleTypeId OldRole { get; }

        /// <summary>
        ///     The <see cref="EscapeType" /> of the event.
        /// </summary>
        public EscapeType EscapeScenario { get; }

        /// <summary>
        ///     Gets or sets the team that will recieve tickets for escaping. Can be <see cref="SpawnableTeamType.None" /> for no
        ///     tickets for either side.
        /// </summary>
        public SpawnableTeamType TeamGettingTickets { get; set; }

        /// <summary>
        ///     If the event is cancelled or not.
        /// </summary>
        public bool IsCancelled { get; set; }

        /// <summary>
        ///     The player that is escaping.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}