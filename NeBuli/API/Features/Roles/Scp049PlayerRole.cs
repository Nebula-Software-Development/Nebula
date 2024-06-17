// -----------------------------------------------------------------------
// <copyright file=Scp049PlayerRole.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using PlayerRoles;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp049;
using PlayerRoles.Subroutines;
using UnityEngine;

namespace Nebula.API.Features.Roles
{
    /// <summary>
    ///     Represents the <see cref="RoleTypeId.Scp049" /> role in-game.
    /// </summary>
    public class Scp049PlayerRole : FpcRoleBase
    {
        internal Scp049PlayerRole(Scp049Role role) : base(role)
        {
            Base = role;
            SetupSubroutines();
        }

        /// <summary>
        ///     Gets the <see cref="Scp049Role" /> base.
        /// </summary>
        public new Scp049Role Base { get; }

        /// <summary>
        ///     Gets the total amount of resurrections SCP-049 has completed.
        /// </summary>
        public int GetTotalRessurections => Base.TryGetOwner(out ReferenceHub hub)
            ? Scp049ResurrectAbility.GetResurrectionsNumber(hub)
            : 0;

        /// <summary>
        ///     Gets or sets the distance from the target.
        /// </summary>
        public float DistanceFromTarget
        {
            get => SenseAbility.DistanceFromTarget;
            set => SenseAbility.DistanceFromTarget = value;
        }

        /// <summary>
        ///     Gets a list of dead targets.
        /// </summary>
        public List<Player> DeadTargets => SenseAbility.DeadTargets.Select(x => Player.Get(x)).ToList();

        /// <summary>
        ///     Gets a list of the players alive zombies.
        /// </summary>
        public List<Player> Zombies =>
            Scp049ResurrectAbility.ResurrectedPlayers.Keys.Select(x => Player.Get(x)).ToList();

        /// <summary>
        ///     Gets a list of the players dead zombies.
        /// </summary>
        public List<Player> DeadZombies => Scp049ResurrectAbility.DeadZombies.Select(x => Player.Get(x)).ToList();

        /// <summary>
        ///     Gets or sets the cooldown of the sense ability.
        /// </summary>
        public float SenseAbilityCooldown
        {
            get => SenseAbility.Cooldown.Remaining;
            set
            {
                SenseAbility.Cooldown.Remaining = value;
                SenseAbility.ServerSendRpc(true);
            }
        }

        /// <summary>
        ///     Gets or sets the cooldown of the call ability.
        /// </summary>
        public float CallAbilityCooldown
        {
            get => CallAbility.Cooldown.Remaining;
            set
            {
                CallAbility.Cooldown.Remaining = value;
                CallAbility.ServerSendRpc(true);
            }
        }

        /// <summary>
        ///     Gets or sets if SCP-049 is currently in progress of resurrecting a ragdoll.
        /// </summary>
        public bool IsInProgress
        {
            get => ResurrectAbility.IsInProgress;
            set => ResurrectAbility.IsInProgress = value;
        }

        /// <summary>
        ///     Gets or sets the current ragdoll SCP-049 is resurrecting.
        /// </summary>
        public Ragdoll CurrentRagdoll
        {
            get => Ragdoll.Get(ResurrectAbility.CurRagdoll);
            set => ResurrectAbility.CurRagdoll = value?.Base;
        }

        /// <summary>
        ///     Gets the SubroutineManagerModule for SCP-049.
        /// </summary>
        public SubroutineManagerModule ManagerModule { get; internal set; }

        /// <summary>
        ///     Gets the HumeShieldModuleBase for SCP-049.
        /// </summary>
        public HumeShieldModuleBase HumeShield { get; internal set; }

        /// <summary>
        ///     Gets the ResurrectAbility for SCP-049.
        /// </summary>
        public Scp049ResurrectAbility ResurrectAbility { get; internal set; }

        /// <summary>
        ///     Gets the CallAbility for SCP-049.
        /// </summary>
        public Scp049CallAbility CallAbility { get; internal set; }

        /// <summary>
        ///     Gets the AttackAbility for SCP-049.
        /// </summary>
        public Scp049AttackAbility AttackAbility { get; internal set; }

        /// <summary>
        ///     Gets the SenseAbility for SCP-049.
        /// </summary>
        public Scp049SenseAbility SenseAbility { get; internal set; }

        /// <summary>
        ///     Forces SCP-049 to lose its current sense target.
        /// </summary>
        public void LoseSenseTarget()
        {
            SenseAbility.ServerLoseTarget();
        }

        /// <summary>
        ///     Gets a bool if SCP-049 can find a target, and if true, gives the target found.
        /// </summary>
        public bool CanFindTarget(out Player player)
        {
            bool flag = SenseAbility.CanFindTarget(out ReferenceHub foundplayer);
            player = Player.Get(foundplayer);
            return flag;
        }

        /// <summary>
        ///     Gets if SCP-049 can reach/hit the specified <see cref="Player" />.
        /// </summary>
        public bool CanHitTarget(Player player)
        {
            return AttackAbility.IsTargetValid(player.ReferenceHub);
        }

        /// <summary>
        ///     Gets if SCP-049 is close enough to the <see cref="Ragdoll" /> to being resurrecting.
        /// </summary>
        /// <param name="position">The <see cref="Vector3" /> to check.</param>
        /// <returns>True if SCP-049 can reach, otherwise false.</returns>
        public bool CanReachRagdoll(Vector3 position)
        {
            return ResurrectAbility.IsCloseEnough(Position, position);
        }

        /// <summary>
        ///     Gets if SCP-049 can ressurect the <see cref="Ragdoll" />.
        /// </summary>
        /// <param name="ragdoll">The <see cref="Ragdoll" />.</param>
        /// <returns>True if SCP-049 can ressurect, otherwise false.</returns>
        public bool CanRessurect(Ragdoll ragdoll)
        {
            return ResurrectAbility.CheckRagdoll(ragdoll.Base);
        }

        /// <summary>
        ///     Checks if theres any conflicts with the <see cref="Ragdoll" />. For example, if the <see cref="Ragdoll" /> has a
        ///     current action with SCP-3114.
        /// </summary>
        /// <param name="ragdoll">The <see cref="Ragdoll" /></param>
        /// <returns>True if theres any conflicts, otherwise false.</returns>
        public bool HasConflicts(Ragdoll ragdoll)
        {
            return ResurrectAbility.AnyConflicts(ragdoll.Base);
        }


        internal void SetupSubroutines()
        {
            ManagerModule = Base.SubroutineModule;
            HumeShield = Base.HumeShieldModule;

            try
            {
                if (ManagerModule.TryGetSubroutine(out Scp049ResurrectAbility resurrectAbility))
                {
                    ResurrectAbility = resurrectAbility;
                }

                if (ManagerModule.TryGetSubroutine(out Scp049CallAbility callAbility))
                {
                    CallAbility = callAbility;
                }

                if (ManagerModule.TryGetSubroutine(out Scp049AttackAbility attackAbility))
                {
                    AttackAbility = attackAbility;
                }

                if (ManagerModule.TryGetSubroutine(out Scp049SenseAbility senseAbility))
                {
                    SenseAbility = senseAbility;
                }
            }
            catch (Exception e)
            {
                Log.Error("An error occurred setting up SCP-049 subroutines! Full error --> \n" + e);
            }
        }
    }
}