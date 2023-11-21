// -----------------------------------------------------------------------
// <copyright file=Scp0492PlayerRole.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using PlayerRoles;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp049.Zombies;
using PlayerRoles.Subroutines;

namespace Nebuli.API.Features.Roles
{
    /// <summary>
    ///     Represents the <see cref="RoleTypeId.Scp0492" /> role in-game.
    /// </summary>
    public class Scp0492PlayerRole : FpcRoleBase
    {
        public Scp0492PlayerRole(ZombieRole role) : base(role)
        {
            Base = role;
            SetupSubroutines();
        }

        /// <summary>
        ///     Gets the <see cref="ZombieRole" /> base.
        /// </summary>
        public new ZombieRole Base { get; }

        /// <summary>
        ///     Gets the ragdoll being consumed by SCP-049-2.
        /// </summary>
        public Ragdoll ConsumingRagdoll => Ragdoll.Get(ConsumeAbility.CurRagdoll);

        /// <summary>
        ///     Gets the progress of SCP-049-2's consume ability.
        /// </summary>
        public float Progress => ConsumeAbility.ProgressStatus;

        /// <summary>
        ///     Gets a value indicating whether SCP-049-2 is currently consuming a target.
        /// </summary>
        public bool IsConsuming => ConsumeAbility.IsInProgress;

        /// <summary>
        ///     Gets the attack damage of SCP-049-2.
        /// </summary>
        public float AttackDamage => AttackAbility.DamageAmount;

        /// <summary>
        ///     Gets or sets a value indicating whether SCP-049-2's attack has been triggered.
        /// </summary>
        public bool AttackTriggered
        {
            get => AttackAbility.AttackTriggered;
            set => AttackAbility.AttackTriggered = value;
        }

        /// <summary>
        ///     Gets a list of detected players by SCP-049-2.
        /// </summary>
        public List<Player> DetectedPlayers => AttackAbility.DetectedPlayers.Select(x => Player.Get(x)).ToList();

        /// <summary>
        ///     Gets a value indicating whether SCP-049-2 can perform an attack.
        /// </summary>
        public bool CanAttack => AttackAbility.CanTriggerAbility;

        /// <summary>
        ///     Gets or sets the simulated stare value for SCP-049-2.
        /// </summary>
        public float SimulatedStare
        {
            get => BloodlustAbility.SimulatedStare;
            set => BloodlustAbility.SimulatedStare = value;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether SCP-049-2 is using its bloodlust ability.
        /// </summary>
        public bool LookingAtSomeone
        {
            get => BloodlustAbility.LookingAtTarget;
            set => BloodlustAbility.LookingAtTarget = value;
        }

        /// <summary>
        ///     Gets if anyone is looking at SCP-049-2.
        /// </summary>
        public bool HasAnyLookers => Base.TryGetOwner(out ReferenceHub hub) &&
                                     BloodlustAbility.AnyTargets(hub, hub.PlayerCameraReference);

        /// <summary>
        ///     Gets the SubroutineManagerModule for SCP-049-2.
        /// </summary>
        public SubroutineManagerModule ManagerModule { get; internal set; }

        /// <summary>
        ///     Gets the HumeShieldModuleBase for SCP-049-2.
        /// </summary>
        public HumeShieldModuleBase HumeShieldModule { get; internal set; }

        /// <summary>
        ///     Gets the BloodLustAbility for SCP-049-2.
        /// </summary>
        public ZombieBloodlustAbility BloodlustAbility { get; internal set; }

        /// <summary>
        ///     Gets the AttackAbility for SCP-049-2.
        /// </summary>
        public ZombieAttackAbility AttackAbility { get; internal set; }

        /// <summary>
        ///     Gets the ConsumeAbility for SCP-049-2.
        /// </summary>
        public ZombieConsumeAbility ConsumeAbility { get; internal set; }

        /// <summary>
        ///     Forces SCP-049-2 to attack.
        /// </summary>
        public void Attack()
        {
            AttackAbility.ServerPerformAttack();
        }

        internal void SetupSubroutines()
        {
            ManagerModule = Base.SubroutineModule;
            HumeShieldModule = Base.HumeShieldModule;

            try
            {
                if (ManagerModule.TryGetSubroutine(out ZombieBloodlustAbility bloodAbility))
                {
                    BloodlustAbility = bloodAbility;
                }

                if (ManagerModule.TryGetSubroutine(out ZombieAttackAbility attackAbility))
                {
                    AttackAbility = attackAbility;
                }

                if (ManagerModule.TryGetSubroutine(out ZombieConsumeAbility consumeAbility))
                {
                    ConsumeAbility = consumeAbility;
                }
            }
            catch (Exception e)
            {
                Log.Error("An error occurred setting up SCP-049-2 subroutines! Full error --> \n" + e);
            }
        }
    }
}