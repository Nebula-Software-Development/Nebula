// -----------------------------------------------------------------------
// <copyright file=Scp096PlayerRole.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp096;
using PlayerRoles.Subroutines;
using UnityEngine;

namespace Nebuli.API.Features.Roles
{
    /// <summary>
    ///     Represents the <see cref="RoleTypeId.Scp096" /> role in-game.
    /// </summary>
    public class Scp096PlayerRole : FpcRoleBase
    {
        internal Scp096PlayerRole(Scp096Role role) : base(role)
        {
            Base = role;
            SetupSubroutines();
        }

        /// <summary>
        ///     Gets the roles <see cref="Scp096Role" /> base.
        /// </summary>
        public new Scp096Role Base { get; }

        /// <summary>
        ///     Gets if SCP-096 can attack.
        /// </summary>
        public bool CanAttack => AttackAbility.AttackPossible;

        /// <summary>
        ///     Gets a <see cref="HashSet{T}" /> of current targets.
        /// </summary>
        public HashSet<ReferenceHub> Targets => TargetsTracker.Targets;

        /// <summary>
        ///     Gets if SCP-096 can charge.
        /// </summary>
        public bool CanCharge => ChargeAbility.CanCharge;

        /// <summary>
        ///     Gets or sets the current Hume shield value for SCP-096.
        /// </summary>
        public float CurrentHumeShield
        {
            get => HumeShieldModule.HsCurrent;
            set => HumeShieldModule.HsCurrent = value;
        }

        /// <summary>
        ///     Gets a value indicating whether SCP-096 has a Hume shield.
        /// </summary>
        public bool HasHumeShield => HumeShieldModule.HsCurrent is not 0;

        /// <summary>
        ///     Gets or sets the ability state of SCP-096.
        /// </summary>
        public Scp096AbilityState AbilityState
        {
            get => StateController.AbilityState;
            set => StateController.SetAbilityState(value);
        }

        /// <summary>
        ///     Gets or sets the rage state of SCP-096.
        /// </summary>
        public Scp096RageState RageState
        {
            get => StateController.RageState;
            set => StateController.SetRageState(value);
        }

        /// <summary>
        ///     Gets a value indicating whether SCP-096 is in a calm state.
        /// </summary>
        public bool IsCalm => !EnragedOrDistressed;

        /// <summary>
        ///     Gets a value indicating whether SCP-096 is in a distressed state.
        /// </summary>
        public bool IsDistressed => RageManager.IsDistressed;

        /// <summary>
        ///     Gets a value indicating whether SCP-096 is in an enraged state.
        /// </summary>
        public bool IsEnraged => RageManager.IsEnraged;

        /// <summary>
        ///     Gets a value indicating whether SCP-096 is either enraged or distressed.
        /// </summary>
        public bool EnragedOrDistressed => RageManager.IsEnragedOrDistressed;

        /// <summary>
        ///     Gets or sets the total rage time of SCP-096.
        /// </summary>
        public float TotalRageTime
        {
            get => RageManager.TotalRageTime;
            set => RageManager.TotalRageTime = value;
        }

        /// <summary>
        ///     Gets the role's <see cref="FirstPersonMovementModule" />.
        /// </summary>
        public FirstPersonMovementModule FpcModule => Base.FpcModule;

        /// <summary>
        ///     Gets the role's camera position.
        /// </summary>
        public Vector3 CameraPosition => Base.CameraPosition;

        /// <summary>
        ///     Gets SCP-096's SubroutineManagerModule.
        /// </summary>
        public SubroutineManagerModule ManagerModule { get; internal set; }

        /// <summary>
        ///     Gets SCP-096's HumeShieldModuleBase.
        /// </summary>
        public HumeShieldModuleBase HumeShieldModule { get; internal set; }

        /// <summary>
        ///     Gets SCP-096's RageManager.
        /// </summary>
        public Scp096RageManager RageManager { get; internal set; }

        /// <summary>
        ///     Gets SCP-096's StateController.
        /// </summary>
        public Scp096StateController StateController { get; internal set; }

        /// <summary>
        ///     Gets SCP-096's PrygateAbility.
        /// </summary>
        public Scp096PrygateAbility PrygateAbility { get; internal set; }

        /// <summary>
        ///     Gets SCP-096's ChargeAbility.
        /// </summary>
        public Scp096ChargeAbility ChargeAbility { get; internal set; }

        /// <summary>
        ///     Gets SCP-096's TryNotToCryAbility.
        /// </summary>
        public Scp096TryNotToCryAbility TryNotToCryAbility { get; internal set; }

        /// <summary>
        ///     Gets SCP-096's TargetsTracker.
        /// </summary>
        public Scp096TargetsTracker TargetsTracker { get; internal set; }

        /// <summary>
        ///     Gets SCP-096's AttackAbility.
        /// </summary>
        public Scp096AttackAbility AttackAbility { get; internal set; }

        /// <summary>
        ///     Gets SCP-096's RageCycleAbility.
        /// </summary>
        public Scp096RageCycleAbility RageCycleAbility { get; internal set; }

        /// <summary>
        ///     Forces SCP-096 to attack.
        /// </summary>
        public void Attack()
        {
            AttackAbility.ServerAttack();
        }

        /// <summary>
        ///     Removes a <see cref="Player" /> from the target list.
        /// </summary>
        /// <param name="player">The <see cref="Player" /> to remove.</param>
        public bool RemoveTarget(Player player)
        {
            return TargetsTracker.RemoveTarget(player.ReferenceHub);
        }

        /// <summary>
        ///     Gets if the <see cref="Player" /> is looking at SCP-096.
        /// </summary>
        /// <param name="player">The <see cref="Player" /> to check.</param>
        /// <returns>True if the player is looking, otherwise false.</returns>
        public bool IsPlayerLooking(Player player)
        {
            return TargetsTracker.IsObservedBy(player.ReferenceHub);
        }

        /// <summary>
        ///     Gets if the <see cref="Player" /> is a current target.
        /// </summary>
        /// <param name="player">The <see cref="Player" /> to check.</param>
        /// <returns>True if the player is a target, otherwise false.</returns>
        public bool IsATarget(Player player)
        {
            return TargetsTracker.HasTarget(player.ReferenceHub);
        }

        /// <summary>
        ///     Clears all of SCP-096's targets.
        /// </summary>
        public void ClearAllTargets()
        {
            TargetsTracker.ClearAllTargets();
        }

        /// <summary>
        ///     Adds a target to SCP-096's current target tracker.
        /// </summary>
        /// <param name="player">The <see cref="Player" /> to add.</param>
        /// <param name="isForLooking">If the target looked at SCP-096 to get added.</param>
        /// <returns></returns>
        public bool AddTarget(Player player, bool isForLooking)
        {
            return TargetsTracker.AddTarget(player.ReferenceHub, isForLooking);
        }

        /// <summary>
        ///     Increases the duration of SCP-096's rage.
        /// </summary>
        /// <param name="increaseAmount">The amount to increase the rage duration.</param>
        public void IncreaseRageDuration(float increaseAmount = 3)
        {
            RageManager.ServerIncreaseDuration(increaseAmount);
        }

        /// <summary>
        ///     Enrages SCP-096 for a specified duration.
        /// </summary>
        /// <param name="time">The duration of the enrage.</param>
        public void Enrage(float time = 20)
        {
            RageManager.ServerEnrage(time);
        }

        /// <summary>
        ///     Forces the end of SCP-096's rage.
        /// </summary>
        /// <param name="clearTime">Whether to clear the rage time or not.</param>
        public void ForceEndRage(bool clearTime = true)
        {
            RageManager.ServerEndEnrage(clearTime);
        }

        /// <summary>
        ///     Returns a boolean value indicating if the current role's <see cref="Scp096AbilityState" /> matches the provided
        ///     <see cref="Scp096AbilityState" />.
        /// </summary>
        /// <param name="abilityState">The ability state to check against.</param>
        /// <returns>Returns true if the role's ability state matches the provided state; otherwise, false.</returns>
        public bool AbilityStateIs(Scp096AbilityState abilityState)
        {
            return Base.IsAbilityState(abilityState);
        }

        /// <summary>
        ///     Returns a boolean value indicating if the current role's <see cref="Scp096RageState" /> matches the provided
        ///     <see cref="Scp096RageState" />.
        /// </summary>
        /// <param name="rageState">The rage state to check against.</param>
        /// <returns>Returns true if the role's rage state matches the provided state; otherwise, false.</returns>
        public bool RageStateIs(Scp096RageState rageState)
        {
            return Base.IsRageState(rageState);
        }

        /// <summary>
        ///     Resets SCP-096's ability state.
        /// </summary>
        public void ResetAbilityState()
        {
            Base.ResetAbilityState();
        }

        internal void SetupSubroutines()
        {
            try
            {
                ManagerModule = Base.SubroutineModule;
                HumeShieldModule = Base.HumeShieldModule;

                if (ManagerModule.TryGetSubroutine(out Scp096RageManager rageManager))
                {
                    RageManager = rageManager;
                }

                if (ManagerModule.TryGetSubroutine(out Scp096StateController controller))
                {
                    StateController = controller;
                }

                if (ManagerModule.TryGetSubroutine(out Scp096PrygateAbility prygateAbility))
                {
                    PrygateAbility = prygateAbility;
                }

                if (ManagerModule.TryGetSubroutine(out Scp096ChargeAbility chargeAbility))
                {
                    ChargeAbility = chargeAbility;
                }

                if (ManagerModule.TryGetSubroutine(out Scp096TryNotToCryAbility tryNotToCryAbility))
                {
                    TryNotToCryAbility = tryNotToCryAbility;
                }

                if (ManagerModule.TryGetSubroutine(out Scp096TargetsTracker targetsTracker))
                {
                    TargetsTracker = targetsTracker;
                }

                if (ManagerModule.TryGetSubroutine(out Scp096AttackAbility attackAbility))
                {
                    AttackAbility = attackAbility;
                }

                if (ManagerModule.TryGetSubroutine(out Scp096RageCycleAbility rageCycleAbility))
                {
                    RageCycleAbility = rageCycleAbility;
                }
            }
            catch (Exception e)
            {
                Log.Error("An error occurred setting up SCP-096 subroutines! Full error --> \n" + e);
            }
        }
    }
}