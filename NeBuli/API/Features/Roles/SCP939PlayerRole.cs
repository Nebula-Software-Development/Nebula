using Nebuli.API.Features.Player;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.Spawnpoints;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp939;
using PlayerRoles.PlayableScps.Scp939.Mimicry;
using PlayerRoles.PlayableScps.Scp939.Ripples;
using PlayerRoles.Voice;
using UnityEngine;

namespace Nebuli.API.Features.Roles
{
    public class SCP939PlayerRole : Role
    {
        internal SCP939PlayerRole(Scp939Role role) : base(role)
        {
            Base = role;
            Ragdoll = Ragdoll.Get(Base.Ragdoll);
        }

        /// <summary>
        /// Gets the <see cref="Scp939Role"/> base.
        /// </summary>
        public new Scp939Role Base { get; }

        /// <summary>
        /// Gets the roles ragdoll.
        /// </summary>
        public Ragdoll Ragdoll { get; }

        /// <summary>
        /// Gets the roles <see cref="PlayerRoles.RoleTypeId"/>.
        /// </summary>
        public override RoleTypeId RoleTypeId => Base.RoleTypeId;

        /// <summary>
        /// Gets if the role is in the darkness.
        /// </summary>
        public bool InDarkness => Base.InDarkness;

        /// <summary>
        /// Gets the role's <see cref="HumeShieldModuleBase"/>.
        /// </summary>
        public HumeShieldModuleBase HumeShield => Base.HumeShieldModule;

        /// <summary>
        /// Gets the roles <see cref="FirstPersonMovementModule"/>.
        /// </summary>
        public FirstPersonMovementModule FpcModule => Base.FpcModule;

        /// <summary>
        /// Gets the roles camera position.
        /// </summary>
        public Vector3 CameraPosition => Base.CameraPosition;

        /// <summary>
        /// Gets the roles max health.
        /// </summary>
        public float MaxHealth => Base.MaxHealth;

        /// <summary>
        /// Gets if the role is AFK.
        /// </summary>
        public bool IsAFK => Base.IsAFK;

        /// <summary>
        /// Re-shows the players start screen.
        /// </summary>
        public void ShowStartScreen() => Base.ShowStartScreen();

        /// <summary>
        /// Gets the roles <see cref="ISpawnpointHandler"/>.
        /// </summary>
        public ISpawnpointHandler SpawnpointHandler => Base.SpawnpointHandler;

        /// <summary>
        /// Gets the roles Spectator module.
        /// </summary>
        public PlayerRoles.Spectating.SpectatableModuleBase SpectatableModuleBase => Base.SpectatorModule;

        /// <summary>
        /// Gets the roles <see cref="VoiceModuleBase"/>.
        /// </summary>
        public VoiceModuleBase VoiceModule => Base.VoiceModule;

        /// <summary>
        /// Gets SCP-939's claw ability.
        /// </summary>
        public Scp939ClawAbility ClawAbility { get; }

        /// <summary>
        /// Gets SCP-939's focus ability.
        /// </summary>
        public Scp939FocusAbility FocusAbility { get; }

        /// <summary>
        /// Gets SCP-939's lunge ability.
        /// </summary>
        public Scp939LungeAbility LungeAbility { get; }

        /// <summary>
        /// Gets SCP-939's mimic point controller.
        /// </summary>
        public MimicPointController MimicPointController { get; }

        /// <summary>
        /// Gets SCP-939's amnestic cloud ability.
        /// </summary>
        public Scp939AmnesticCloudAbility AmnesticCloudAbility { get; }

        /// <summary>
        /// Gets SCP-939's environmental mimicry ability.
        /// </summary>
        public EnvironmentalMimicry EnvironmentalMimicry { get; }

        /// <summary>
        /// Gets SCP-939's footstep ripple trigger.
        /// </summary>
        public FootstepRippleTrigger FootstepRippleTrigger { get; }

        /// <summary>
        /// Gets SCP-939's firearm ripple trigger.
        /// </summary>
        public FirearmRippleTrigger FirearmRippleTrigger { get; }

        /// <summary>
        /// Gets SCP-939's mimicry recorder.
        /// </summary>
        public MimicryRecorder MimicryRecorder { get; }

        /// <summary>
        /// Gets if SCP-939 is focused.
        /// </summary>
        public bool IsFocused => FocusAbility.TargetState;

        /// <summary>
        /// Gets SCP-939's <see cref="Scp939LungeState"/>.
        /// </summary>
        public Scp939LungeState LungeState => LungeAbility.State;

        /// <summary>
        /// Gets a value indicating whether or not SCP-939 is currently lunging.
        /// </summary>
        public bool IsLunging => LungeState is not Scp939LungeState.None;

        /// <summary>
        /// Gets the total amount of saved voiced SCP-939 has saved.
        /// </summary>
        public int AmountOfSavedVoices => MimicryRecorder.SavedVoices.Count;

        /// <summary>
        /// Gets or sets the total amount of recordings SCP-939 can have.
        /// </summary>
        public int MaxRecordings
        {
            get => MimicryRecorder.MaxRecordings;
            set => MimicryRecorder.MaxRecordings = value;
        }

        /// <summary>
        /// Saves a recording of the specified referencehub.
        /// </summary>
        /// <param name="hub">The referencehub to save the recording of.</param>
        public void SaveRecording(ReferenceHub hub) => MimicryRecorder.SaveRecording(hub);

        /// <summary>
        /// Saves a recording of the specified player.
        /// </summary>
        /// <param name="player">The player to save the recording of.</param>
        public void SaveRecording(NebuliPlayer player) => MimicryRecorder.SaveRecording(player.ReferenceHub);

        /// <summary>
        /// Removes all recordings of the specified referencehub.
        /// </summary>
        /// <param name="hub">The referencehub to remove all recordings for.</param>
        public void RemoveRecordingOfPlayer(ReferenceHub hub) => MimicryRecorder.RemoveRecordingsOfPlayer(hub);

        /// <summary>
        /// Removes all recordings of the specified player.
        /// </summary>
        /// <param name="player">The player to remove all recordings for.</param>
        public void RemoveRecordingOfPlayer(NebuliPlayer player) => MimicryRecorder.RemoveRecordingsOfPlayer(player.ReferenceHub);
    }
}