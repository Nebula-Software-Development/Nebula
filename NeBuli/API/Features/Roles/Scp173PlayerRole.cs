// -----------------------------------------------------------------------
// <copyright file=Scp173PlayerRole.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Mirror;
using Nebuli.API.Features.Player;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerRoles.PlayableScps.Scp173;
using PlayerRoles.PlayableScps.Subroutines;
using PlayerRoles.Subroutines;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nebuli.API.Features.Roles;

/// <summary>
/// Represents the <see cref="RoleTypeId.Scp173"/> role in-game.
/// </summary>
public class Scp173PlayerRole : FpcRoleBase
{
    /// <summary>
    /// Gets the <see cref="Scp173Role"/> base.
    /// </summary>
    public new Scp173Role Base { get; }

    internal Scp173PlayerRole(Scp173Role role) : base(role)
    {
        Base = role;
        SetupSubroutines();
    }

    /// <summary>
    /// Makes SCP-173 blink to a specified position.
    /// </summary>
    /// <param name="position">The position to blink to.</param>
    public void Blink(Vector3 position) => BlinkTimer.ServerBlink(position);

    /// <summary>
    /// Gets or sets the cooldown for SCP-173's Breakneck Speeds ability.
    /// </summary>
    public float BreakneckSpeedsAbilityCooldown
    {
        get => BreakneckSpeedsAbility.Cooldown.Remaining;
        set => BreakneckSpeedsAbility.Cooldown.Remaining = value;
    }

    /// <summary>
    /// Gets or sets the cooldown for SCP-173's Tantrum ability.
    /// </summary>
    public float TantrumCooldown
    {
        get => TantrumAbility.Cooldown.Remaining;
        set => TantrumAbility.Cooldown.Remaining = value;
    }

    /// <summary>
    /// Gets or sets the cooldown for SCP-173's Blink ability.
    /// </summary>
    public float BlinkCooldown
    {
        get => BlinkTimer.RemainingBlinkCooldown;
        set
        {
            BlinkTimer._initialStopTime = NetworkTime.time;
            BlinkTimer._totalCooldown = value;
            BlinkTimer.ServerSendRpc(true);
        }
    }

    /// <summary>
    /// Gets a list of <see cref="NebuliPlayer"/>'s who are watching SCP-173.
    /// </summary>
    public List<NebuliPlayer> PlayersWatching => ObserversTracker.Observers.Select(p => NebuliPlayer.Get(p)).ToList();

    /// <summary>
    /// Checks if SCP-173 is currently being watched.
    /// </summary>
    public bool IsBeingWatched => ObserversTracker.IsObserved;

    /// <summary>
    /// Checks if SCP-173 is being watched by a specific <see cref="NebuliPlayer"/>.
    /// </summary>
    /// <param name="player">The <see cref="NebuliPlayer"/> to check against.</param>
    /// <param name="widthMultiplier">The optional width multiplier.</param>
    /// <returns>Returns true if SCP-173 is being watched by the specified <see cref="NebuliPlayer"/>; otherwise, false.</returns>
    public bool IsBeingWatchedBy(NebuliPlayer player, float widthMultiplier = 1) => ObserversTracker.IsObservedBy(player.ReferenceHub, widthMultiplier);

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
    /// Gets the roles max blink distance.
    /// </summary>
    public float BlinkDistance => TeleportAbility.EffectiveBlinkDistance;

    /// <summary>
    /// Gets the roles <see cref="SubroutineManagerModule"/>.
    /// </summary>
    public SubroutineManagerModule ManagerModule { get; internal set; }

    /// <summary>
    /// Gets the roles <see cref="HumeShieldModuleBase"/>.
    /// </summary>
    public HumeShieldModuleBase HumeShieldModule { get; internal set; }

    /// <summary>
    /// Gets SCP-173's tantrum ability.
    /// </summary>
    public Scp173TantrumAbility TantrumAbility { get; internal set; }

    /// <summary>
    /// Gets SCP-173's breakneck speed ability.
    /// </summary>
    public Scp173BreakneckSpeedsAbility BreakneckSpeedsAbility { get; internal set; }

    /// <summary>
    /// Gets SCP-173's teleport ability.
    /// </summary>
    public Scp173TeleportAbility TeleportAbility { get; internal set; }

    /// <summary>
    /// Gets the observers tracker for SCP-173.
    /// </summary>
    public Scp173ObserversTracker ObserversTracker { get; internal set; }

    /// <summary>
    /// Gets the blink timer for SCP-173.
    /// </summary>
    public Scp173BlinkTimer BlinkTimer { get; internal set; }

    /// <summary>
    /// Gets the blink timer for SCP-173.
    /// </summary>
    public Scp173AudioPlayer AudioPlayer { get; internal set; }

    internal void SetupSubroutines()
    {
        try
        {
            ManagerModule = Base.SubroutineModule;
            HumeShieldModule = Base.HumeShieldModule;

            if (ManagerModule.TryGetSubroutine(out Scp173TantrumAbility tantrumAbility))
                TantrumAbility = tantrumAbility;

            if (ManagerModule.TryGetSubroutine(out Scp173BreakneckSpeedsAbility breakNeck))
                BreakneckSpeedsAbility = breakNeck;

            if (ManagerModule.TryGetSubroutine(out Scp173TeleportAbility teleportAbility))
                TeleportAbility = teleportAbility;

            if(ManagerModule.TryGetSubroutine(out Scp173ObserversTracker observersTracker))
                ObserversTracker = observersTracker;

            if(ManagerModule.TryGetSubroutine(out Scp173BlinkTimer blinkTimer))
                BlinkTimer = blinkTimer;

            if(ManagerModule.TryGetSubroutine(out Scp173AudioPlayer audioPlayer))
                AudioPlayer = audioPlayer;
        }
        catch (Exception e)
        {
            Log.Error("An error occurred setting up SCP-173 subroutines! Full error --> \n" + e);
        }
    }
}