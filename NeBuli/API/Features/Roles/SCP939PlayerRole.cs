// -----------------------------------------------------------------------
// <copyright file=Scp939PlayerRole.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features.Player;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp939;
using PlayerRoles.PlayableScps.Scp939.Mimicry;
using PlayerRoles.PlayableScps.Scp939.Ripples;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerRoles.Subroutines;
using System;
using UnityEngine;

namespace Nebuli.API.Features.Roles;

/// <summary>
/// Represents the <see cref="RoleTypeId.Scp939"/> role in-game.
/// </summary>
public class Scp939PlayerRole : FpcRoleBase
{
    internal Scp939PlayerRole(Scp939Role role) : base(role)
    {
        Base = role;
        SetupSubroutines();
    }

    /// <summary>
    /// Gets the <see cref="Scp939Role"/> base.
    /// </summary>
    public new Scp939Role Base { get; }

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
    /// Gets the roles <see cref="SubroutineManagerModule"/>.
    /// </summary>
    public SubroutineManagerModule ManagerModule { get; internal set; }

    /// <summary>
    /// Gets the roles <see cref="HumeShieldModuleBase"/>.
    /// </summary>
    public HumeShieldModuleBase HumeShieldModule { get; internal set; }

    /// <summary>
    /// Gets SCP-939's claw ability.
    /// </summary>
    public Scp939ClawAbility ClawAbility { get; internal set; }

    /// <summary>
    /// Gets SCP-939's focus ability.
    /// </summary>
    public Scp939FocusAbility FocusAbility { get; internal set; }

    /// <summary>
    /// Gets SCP-939's lunge ability.
    /// </summary>
    public Scp939LungeAbility LungeAbility { get; internal set; }

    /// <summary>
    /// Gets SCP-939's mimic point controller.
    /// </summary>
    public MimicPointController MimicPointController { get; internal set; }

    /// <summary>
    /// Gets SCP-939's amnestic cloud ability.
    /// </summary>
    public Scp939AmnesticCloudAbility AmnesticCloudAbility { get; internal set; }

    /// <summary>
    /// Gets SCP-939's environmental mimicry ability.
    /// </summary>
    public EnvironmentalMimicry EnvironmentalMimicry { get; internal set; }

    /// <summary>
    /// Gets SCP-939's footstep ripple trigger.
    /// </summary>
    public FootstepRippleTrigger FootstepRippleTrigger { get; internal set; }

    /// <summary>
    /// Gets SCP-939's firearm ripple trigger.
    /// </summary>
    public FirearmRippleTrigger FirearmRippleTrigger { get; internal set; }

    /// <summary>
    /// Gets SCP-939's mimicry recorder.
    /// </summary>
    public MimicryRecorder MimicryRecorder { get; internal set; }

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

    internal void SetupSubroutines()
    {
        try
        {
            ManagerModule = Base.SubroutineModule;
            HumeShieldModule = Base.HumeShieldModule;

            if (ManagerModule.TryGetSubroutine(out Scp939ClawAbility sp939ClawAbility))
                ClawAbility = sp939ClawAbility;

            if (ManagerModule.TryGetSubroutine(out Scp939FocusAbility scp939FocusAbility))
                FocusAbility = scp939FocusAbility;

            if (ManagerModule.TryGetSubroutine(out Scp939LungeAbility scp939LungeAbility))
                LungeAbility = scp939LungeAbility;

            if (ManagerModule.TryGetSubroutine(out Scp939AmnesticCloudAbility scp939AmnesticCloudAbility))
                AmnesticCloudAbility = scp939AmnesticCloudAbility;

            if (ManagerModule.TryGetSubroutine(out EnvironmentalMimicry environmentalMimicry))
                EnvironmentalMimicry = environmentalMimicry;

            if (ManagerModule.TryGetSubroutine(out MimicryRecorder mimicryRecorder))
                MimicryRecorder = mimicryRecorder;

            if (ManagerModule.TryGetSubroutine(out FootstepRippleTrigger footstepRippleTrigger))
                FootstepRippleTrigger = footstepRippleTrigger;

            if (ManagerModule.TryGetSubroutine(out FirearmRippleTrigger firearmRippleTrigger))
                FirearmRippleTrigger = firearmRippleTrigger;
        }
        catch (Exception e)
        {
            Log.Error("An error occurred setting up SCP-939 subroutines! Full error --> \n" + e);
        }
    }
}