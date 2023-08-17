using Nebuli.API.Features.Player;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using System;
using UnityEngine;

namespace Nebuli.Events.EventArguments.Player
{
    public class PlayerSpawningEvent : EventArgs, IPlayerEvent
    {
        public PlayerSpawningEvent(ReferenceHub player, PlayerRoleBase prevRole, PlayerRoleBase newRole, Vector3 pos, float oldRotation)
        {
            Player = NebuliPlayer.Get(player);
            NewRole = newRole.RoleTypeId;
            OldRole = prevRole.RoleTypeId;
            HorizontalRotation = oldRotation;
            Position = pos;
        }

        /// <summary>
        /// Gets the player triggering the event.
        /// </summary>
        public NebuliPlayer Player { get; }

        /// <summary>
        /// Gets the position the player will be spawned at.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets the new <see cref="RoleTypeId"/>.
        /// </summary>
        public RoleTypeId NewRole { get; }

        /// <summary>
        /// Gets the old <see cref="RoleTypeId"/>.
        /// </summary>
        public RoleTypeId OldRole { get; }

        /// <summary>
        /// Gets or sets the players horizontal rotation when spawning.
        /// </summary>
        public float HorizontalRotation { get; set; }
    }
}
