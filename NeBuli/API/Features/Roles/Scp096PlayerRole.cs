using Nebuli.API.Features.Player;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.Spawnpoints;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp096;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerRoles.Voice;
using System;
using UnityEngine;

namespace Nebuli.API.Features.Roles
{
    public class Scp096PlayerRole : Role
    {
        /// <summary>
        /// Gets the roles <see cref="Scp096Role"/> base.
        /// </summary>
        public new Scp096Role Base { get; }

        public Scp096PlayerRole(Scp096Role role) : base(role)
        {
            Base = role;
            Ragdoll = Ragdoll.Get(Base.Ragdoll);
            SetupSubroutines();
        }

        /// <summary>
        /// Gets or sets the current Hume shield value for SCP-096.
        /// </summary>
        public float CurrentHumeShield
        {
            get => HumeShieldModule.HsCurrent;
            set => HumeShieldModule.HsCurrent = value;
        }

        /// <summary>
        /// Gets a value indicating whether SCP-096 has a Hume shield.
        /// </summary>
        public bool HasHumeShield => HumeShieldModule.HsCurrent is not 0;

        /// <summary>
        /// Gets or sets the ability state of SCP-096.
        /// </summary>
        public Scp096AbilityState AbilityState
        {
            get => StateController.AbilityState;
            set => StateController.SetAbilityState(value);
        }

        /// <summary>
        /// Gets or sets the rage state of SCP-096.
        /// </summary>
        public Scp096RageState RageState
        {
            get => StateController.RageState;
            set => StateController.SetRageState(value);
        }

        /// <summary>
        /// Gets a value indicating whether SCP-096 is in a calm state.
        /// </summary>
        public bool IsCalm => !EnragedOrDistressed;

        /// <summary>
        /// Gets a value indicating whether SCP-096 is in a distressed state.
        /// </summary>
        public bool IsDistressed => RageManager.IsDistressed;

        /// <summary>
        /// Gets a value indicating whether SCP-096 is in an enraged state.
        /// </summary>
        public bool IsEnraged => RageManager.IsEnraged;

        /// <summary>
        /// Gets a value indicating whether SCP-096 is either enraged or distressed.
        /// </summary>
        public bool EnragedOrDistressed => RageManager.IsEnragedOrDistressed;

        /// <summary>
        /// Gets or sets the total rage time of SCP-096.
        /// </summary>
        public float TotalRageTime
        {
            get => RageManager.TotalRageTime;
            set => RageManager.TotalRageTime = value;
        }

        /// <summary>
        /// Increases the duration of SCP-096's rage.
        /// </summary>
        /// <param name="increaseAmount">The amount to increase the rage duration.</param>
        public void IncreaseRageDuration(float increaseAmount) => RageManager.ServerIncreaseDuration(increaseAmount);

        /// <summary>
        /// Enrages SCP-096 for a specified duration.
        /// </summary>
        /// <param name="time">The duration of the enrage.</param>
        public void Enrage(float time = 20) => RageManager.ServerEnrage(time);

        /// <summary>
        /// Forces the end of SCP-096's rage.
        /// </summary>
        /// <param name="clearTime">Whether to clear the rage time or not.</param>
        public void ForceEndRage(bool clearTime) => RageManager.ServerEndEnrage(clearTime);

        /// <summary>
        /// Gets a value indicating whether the role is AFK.
        /// </summary>
        public bool IsAFK => Base.IsAFK;

        /// <summary>
        /// Returns a boolean value indicating if the current role's <see cref="Scp096AbilityState"/> matches the provided <see cref="Scp096AbilityState"/>.
        /// </summary>
        /// <param name="abilityState">The ability state to check against.</param>
        /// <returns>Returns true if the role's ability state matches the provided state; otherwise, false.</returns>
        public bool AbilityStateIs(Scp096AbilityState abilityState) => Base.IsAbilityState(abilityState);

        /// <summary>
        /// Returns a boolean value indicating if the current role's <see cref="Scp096RageState"/> matches the provided <see cref="Scp096RageState"/>.
        /// </summary>
        /// <param name="rageState">The rage state to check against.</param>
        /// <returns>Returns true if the role's rage state matches the provided state; otherwise, false.</returns>
        public bool RageStateIs(Scp096RageState rageState) => Base.IsRageState(rageState);

        /// <summary>
        /// Shows the start screen for SCP-096.
        /// </summary>
        public void ShowStartScreen() => Base.ShowStartScreen();

        /// <summary>
        /// Gets the role's <see cref="VoiceModuleBase"/>.
        /// </summary>
        public VoiceModuleBase VoiceModule => Base.VoiceModule;

        /// <summary>
        /// Gets the role's SpectatableModuleBase.
        /// </summary>
        public PlayerRoles.Spectating.SpectatableModuleBase SpectatableModuleBase => Base.SpectatorModule;

        /// <summary>
        /// Gets the role's <see cref="ISpawnpointHandler"/>.
        /// </summary>
        public ISpawnpointHandler SpawnpointHandler => Base.SpawnpointHandler;

        /// <summary>
        /// Gets the role's <see cref="FirstPersonMovementModule"/>.
        /// </summary>
        public FirstPersonMovementModule FpcModule => Base.FpcModule;

        /// <summary>
        /// Gets the role's camera position.
        /// </summary>
        public Vector3 CameraPosition => Base.CameraPosition;

        /// <summary>
        /// Gets a value indicating whether the role is in darkness.
        /// </summary>
        public bool InDarkness => Base.InDarkness;

        /// <summary>
        /// Gets the role's ragdoll.
        /// </summary>
        public Ragdoll Ragdoll { get; }

        /// <summary>
        /// Resets SCP-096's ability state.
        /// </summary>
        public void ResetAbilityState() => Base.ResetAbilityState();

        /// <summary>
        /// Gets SCP-096's SubroutineManagerModule.
        /// </summary>
        public SubroutineManagerModule ManagerModule { get; internal set; }

        /// <summary>
        /// Gets SCP-096's HumeShieldModuleBase.
        /// </summary>
        public HumeShieldModuleBase HumeShieldModule { get; internal set; }

        /// <summary>
        /// Gets SCP-096's RageManager.
        /// </summary>
        public Scp096RageManager RageManager { get; internal set; }

        /// <summary>
        /// Gets SCP-096's StateController.
        /// </summary>
        public Scp096StateController StateController { get; internal set; }

        /// <summary>
        /// Gets SCP-096's PrygateAbility.
        /// </summary>
        public Scp096PrygateAbility PrygateAbility { get; internal set; }

        /// <summary>
        /// Gets the RoleTypeId of the role.
        /// </summary>
        public override RoleTypeId RoleTypeId => Base.RoleTypeId;

        internal void SetupSubroutines()
        {
            try
            {
                ManagerModule = Base.SubroutineModule;
                HumeShieldModule = Base.HumeShieldModule;

                Scp096RageManager rageManager;
                if (ManagerModule.TryGetSubroutine(out rageManager))
                    RageManager = rageManager;
                Scp096StateController controller;
                if (ManagerModule.TryGetSubroutine(out controller))
                    StateController = controller;
                Scp096PrygateAbility prygateAbility;
                if (ManagerModule.TryGetSubroutine(out prygateAbility))
                    PrygateAbility = prygateAbility;
            }
            catch (Exception e)
            {
                Log.Error("An error occurred setting up SCP-096 subroutines! Full error --> \n" + e);
            }
        }
    }
}