// -----------------------------------------------------------------------
// <copyright file=Role.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using PlayerRoles;
using PlayerRoles.PlayableScps.Scp049;
using PlayerRoles.PlayableScps.Scp049.Zombies;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp096;
using PlayerRoles.PlayableScps.Scp106;
using PlayerRoles.PlayableScps.Scp173;
using PlayerRoles.PlayableScps.Scp3114;
using PlayerRoles.PlayableScps.Scp939;
using PlayerRoles.Spectating;
using UnityEngine;

namespace Nebuli.API.Features.Roles
{
    /// <summary>
    ///     Represents a base class for Nebuli game roles.
    /// </summary>
    public abstract class Role
    {
        protected Role(PlayerRoleBase roleBase)
        {
            if (roleBase.TryGetOwner(out ReferenceHub hub))
            {
                Owner = Player.TryGet(hub, out Player ply) ? ply : null;
            }

            Base = roleBase;
        }

        /// <summary>
        ///     Gets the owner of this <see cref="PlayerRoleBase" />.
        /// </summary>
        public Player Owner { get; }

        /// <summary>
        ///     Gets the roles position.
        /// </summary>
        public Vector3 Position => Base.transform.position;

        /// <summary>
        ///     Gets the <see cref="PlayerRoleBase" />.
        /// </summary>
        public PlayerRoleBase Base { get; }

        /// <summary>
        ///     Gets the total time this role has been active.
        /// </summary>
        public float TotalTimeAlive => Base.ActiveTime;

        /// <summary>
        ///     Gets the <see cref="PlayerRoles.RoleTypeId" /> of this role.
        /// </summary>
        public RoleTypeId RoleTypeId => Base.RoleTypeId;

        /// <summary>
        ///     Gets the roles name.
        /// </summary>
        public string RoleName => Base.RoleName;

        /// <summary>
        ///     Gets or sets the roles <see cref="PlayerRoles.RoleSpawnFlags" />.
        /// </summary>
        public RoleSpawnFlags RoleSpawnFlags
        {
            get => Base.ServerSpawnFlags;
            set => Base.ServerSpawnFlags = value;
        }

        /// <summary>
        ///     Gets or sets the roles <see cref="PlayerRoles.RoleChangeReason" />.
        /// </summary>
        public RoleChangeReason RoleChangeReason
        {
            get => Base.ServerSpawnReason;
            set => Base.ServerSpawnReason = value;
        }

        /// <summary>
        ///     Gets the roles <see cref="PlayerRoles.Team" />.
        /// </summary>
        public Team Team => Base.Team;

        /// <summary>
        ///     Gets if the roles team is dead.
        /// </summary>
        public bool IsDead => Team is Team.Dead;

        /// <summary>
        ///     Gets if the roles team is alive.
        /// </summary>
        public bool IsAlive => !IsDead;

        /// <summary>
        ///     Gets the roles <see cref="UnityEngine.Color" />.
        /// </summary>
        public Color Color => Base.RoleColor;

        /// <summary>
        ///     Gets the roles GameObject.
        /// </summary>
        public GameObject GameObject => Base.gameObject;

        /// <summary>
        ///     Sets the owner of this role to a new role.
        /// </summary>
        /// <param name="newRole">The new role to set.</param>
        /// <param name="reason">The reason for the role change.</param>
        /// <param name="roleSpawnFlags">The roles spawn flags.</param>
        public void SetNewRole(RoleTypeId newRole, RoleChangeReason reason = RoleChangeReason.RemoteAdmin,
            RoleSpawnFlags roleSpawnFlags = RoleSpawnFlags.All)
        {
            Owner.SetRole(newRole, reason, roleSpawnFlags);
        }

        /// <summary>
        ///     Creates a new <see cref="Role" /> given the <see cref="PlayerRoleBase" />.
        /// </summary>
        public static Role CreateNew(PlayerRoleBase role)
        {
            return role switch
            {
                NoneRole noneRole => new NonePlayerRole(noneRole),
                HumanRole human => new HumanPlayerRole(human),
                Scp939Role scp939 => new Scp939PlayerRole(scp939),
                Scp079Role scp079 => new Scp079PlayerRole(scp079),
                Scp096Role scp096 => new Scp096PlayerRole(scp096),
                Scp173Role scp173 => new Scp173PlayerRole(scp173),
                Scp106Role scp106 => new Scp106PlayerRole(scp106),
                Scp049Role scp049 => new Scp049PlayerRole(scp049),
                ZombieRole zombie => new Scp0492PlayerRole(zombie),
                Scp3114Role scp3114 => new Scp3114PlayerRole(scp3114),
                OverwatchRole overwatch => new OverwatchPlayerRole(overwatch),
                SpectatorRole spectator => new SpectatorPlayerRole(spectator),
                PlayerRoles.Filmmaker.FilmmakerRole filmmaker => new FilmmakerRole(filmmaker),
                _ => null
            };
        }
    }
}