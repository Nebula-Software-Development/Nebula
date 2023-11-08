// -----------------------------------------------------------------------
// <copyright file=Scp079PlayerRole.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using PlayerRoles;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using PlayerRoles.PlayableScps.Scp079.Pinging;
using PlayerRoles.PlayableScps.Scp079.Rewards;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerRoles.Voice;
using System;
using Camera = Nebuli.API.Features.Map.Camera;

namespace Nebuli.API.Features.Roles;

/// <summary>
/// Represents the <see cref="RoleTypeId.Scp079"/> role in-game.
/// </summary>
public class Scp079PlayerRole : Role
{
    /// <summary>
    /// Gets the <see cref="Scp079Role"/> base.
    /// </summary>
    public new Scp079Role Base { get; }

    internal Scp079PlayerRole(Scp079Role role) : base(role)
    {
        Base = role;
        SetupSubroutines();
    }

    /// <summary>
    /// Gets if the blackout room ability is ready or not.
    /// </summary>
    public bool RoomBlackoutReady => BlackoutRoomAbility.IsReady;

    /// <summary>
    /// Gets if the blackout zone ability is ready or not.
    /// </summary>
    public bool ZoneBlackoutReady => BlackoutZoneAbility.IsReady;

    /// <summary>
    /// Gets if SCP-079 can transmit to a speaker.
    /// </summary>
    public bool CanTransmit => SpeakerAbility.CanTransmit;

    /// <summary>
    /// Grants the role experience.
    /// </summary>
    /// <param name="amount">The amount of experience to give.</param>
    public void GrantExperience(int amount) => TierManager.ServerGrantExperience(amount, Scp079HudTranslation.Experience);

    /// <summary>
    /// Gets or sets the roles tier level.
    /// </summary>
    public int TierLevel
    {
        get => TierManager.AccessTierLevel;
        set => Experience = value <= 1 ? 0 : TierManager.AbsoluteThresholds[Math.Max(value - 2, 0)];
    }

    /// <summary>
    /// Gets or sets the roles total experience.
    /// </summary>
    public int Experience
    {
        get => TierManager.TotalExp;
        set => TierManager.TotalExp = value;
    }

    /// <summary>
    /// Forces SCP-079 to lose singal for the specified duration.
    /// </summary>
    /// <param name="duration">The duration of the signal loss.</param>
    public void LoseSignal(float duration)
    {
        LostSignalHandler.ServerLoseSignal(duration);
        LostSignalHandler.ServerSendRpc(true);
    }

    /// <summary>
    /// Gets if the role is AFK.
    /// </summary>
    public bool IsAFK => Base.IsAFK;

    /// <summary>
    /// Gets if the role is currently being spectated.
    /// </summary>
    public bool IsSpectated => Base.IsSpectated;

    /// <summary>
    /// Gets if the current role is within the idle range of a Tesla Gate.
    /// </summary>
    /// <param name="teslaGate">The <see cref="TeslaGate"/> to check.</param>
    /// <returns></returns>
    public bool IsInIdleRange(TeslaGate teslaGate) => Base.IsInIdleRange(teslaGate);

    /// <summary>
    /// Gets the roles current camera horizontal rotation.
    /// </summary>
    public float HorizontalRotation => Base.HorizontalRotation;

    /// <summary>
    /// Gets if SCP-079 is currently using a speaker.
    /// </summary>
    public bool IsUsingSpeaker => SpeakerAbility.IsUsingSpeaker;

    /// <summary>
    /// Gets or sets SCP-079's current energy amount.
    /// </summary>
    public float CurrentEnergy
    {
        get => AuxManager.CurrentAux;
        set => AuxManager.CurrentAux = value;
    }

    /// <summary>
    /// Gets or sets the max energy SCP-079 can have for its <c>current</c> level.
    /// </summary>
    public float MaxEnergy
    {
        get => AuxManager.MaxAux;
        set => AuxManager._maxPerTier[TierManager.AccessTierIndex] = value;
    }

    /// <summary>
    /// Gets the current speaker used by SCP-079, can be null if none.
    /// </summary>
    public Scp079Speaker CurrentSpeaker => Scp079Speaker.TryGetSpeaker(Camera.Base, out Scp079Speaker speaker) ? speaker : null;

    /// <summary>
    /// Gets the roles current vertical rotation.
    /// </summary>
    public float VerticalRotation => Base.VerticalRotation;

    /// <summary>
    /// Gets if the role can activate a Tesla Gate shock.
    /// </summary>
    public bool CanActivateTeslaShock => Base.CanActivateShock;

    /// <summary>
    /// Gets or sets the current camera the role is on.
    /// </summary>
    public Camera Camera
    {
        get
        {
            Base._curCamSync.TryGetCurrentCamera(out Scp079Camera cam);
            return Camera.Get(cam);
        }
        set => Base._curCamSync.CurrentCamera = value?.Base;
    }

    /// <summary>
    /// Gets the roles <see cref="VoiceModuleBase"/>.
    /// </summary>
    public VoiceModuleBase VoiceModule => Base.VoiceModule;

    /// <summary>
    /// Gets the SCP-079 SubroutineManagerModule.
    /// </summary>
    public SubroutineManagerModule ManagerModule { get; internal set; }

    /// <summary>
    /// Gets the SCP-079 AbilityBase.
    /// </summary>
    public Scp079AbilityBase AbilityBase { get; internal set; }

    /// <summary>
    /// Gets the SCP-079 AuxManager.
    /// </summary>
    public Scp079AuxManager AuxManager { get; internal set; }

    /// <summary>
    /// Gets the SCP-079 BlackoutRoomAbility.
    /// </summary>
    public Scp079BlackoutRoomAbility BlackoutRoomAbility { get; internal set; }

    /// <summary>
    /// Gets the SCP-079 BlackoutZoneAbility.
    /// </summary>
    public Scp079BlackoutZoneAbility BlackoutZoneAbility { get; internal set; }

    /// <summary>
    /// Gets the SCP-079 LostSignalHandler.
    /// </summary>
    public Scp079LostSignalHandler LostSignalHandler { get; internal set; }

    /// <summary>
    /// Gets the SCP-079 TierManager.
    /// </summary>
    public Scp079TierManager TierManager { get; internal set; }

    /// <summary>
    /// Gets the SCP-079 RewardManager.
    /// </summary>
    public Scp079RewardManager RewardManager { get; internal set; }

    /// <summary>
    /// Gets the SCP-079 LockdownRoomAbility.
    /// </summary>
    public Scp079LockdownRoomAbility LockdownRoomAbility { get; internal set; }

    /// <summary>
    /// Gets the SCP-079 CurrentCameraSync.
    /// </summary>
    public Scp079CurrentCameraSync CurrentCameraSync { get; internal set; }

    /// <summary>
    /// Gets the SCP-079 PingAbility.
    /// </summary>
    public Scp079PingAbility PingAbility { get; internal set; }

    /// <summary>
    /// Gets the SCP-079 TeslaAbility.
    /// </summary>
    public Scp079TeslaAbility TeslaAbility { get; internal set; }

    /// <summary>
    /// Gets the SCP-070 SpeakerAbility.
    /// </summary>
    public Scp079SpeakerAbility SpeakerAbility { get; internal set; }

    internal void SetupSubroutines()
    {
        try
        {
            ManagerModule = Base.SubroutineModule;

            if (ManagerModule.TryGetSubroutine(out Scp079AuxManager scp079AuxManager))
                AuxManager = scp079AuxManager;

            if (ManagerModule.TryGetSubroutine(out Scp079TierManager scp079TierManager))
                TierManager = scp079TierManager;

            if (ManagerModule.TryGetSubroutine(out Scp079RewardManager scp079RewardManager))
                RewardManager = scp079RewardManager;

            if (ManagerModule.TryGetSubroutine(out Scp079LockdownRoomAbility scp079LockdownRoomAbility))
                LockdownRoomAbility = scp079LockdownRoomAbility;

            if (ManagerModule.TryGetSubroutine(out Scp079BlackoutRoomAbility scp079BlackoutRoomAbility))
                BlackoutRoomAbility = scp079BlackoutRoomAbility;

            if (ManagerModule.TryGetSubroutine(out Scp079BlackoutZoneAbility scp079BlackoutZoneAbility))
                BlackoutZoneAbility = scp079BlackoutZoneAbility;

            if (ManagerModule.TryGetSubroutine(out Scp079LostSignalHandler scp079LostSignalHandler))
                LostSignalHandler = scp079LostSignalHandler;

            if (ManagerModule.TryGetSubroutine(out Scp079CurrentCameraSync cameraSync))
                CurrentCameraSync = cameraSync;

            if (ManagerModule.TryGetSubroutine(out Scp079PingAbility scp079PingAbility))
                PingAbility = scp079PingAbility;

            if (ManagerModule.TryGetSubroutine(out Scp079TeslaAbility scp079TeslaAbility))
                TeslaAbility = scp079TeslaAbility;

            if(ManagerModule.TryGetSubroutine(out Scp079SpeakerAbility scp079SpeakerAbility))
                SpeakerAbility = scp079SpeakerAbility;
        }
        catch (Exception e)
        {
            Log.Error("An error occurred setting up SCP-079 subroutines! Full error --> \n" + e);
        }
    }
}