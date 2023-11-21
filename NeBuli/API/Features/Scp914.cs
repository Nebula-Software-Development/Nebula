// -----------------------------------------------------------------------
// <copyright file=Scp914.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Scp914;
using UnityEngine;

namespace Nebuli.API.Features;

/// <summary>
/// Utility class for interacting with SCP-914.
/// </summary>
public static class Scp914
{
    /// <summary>
    /// Gets the cached <see cref="global::Scp914.Scp914Controller"/>.
    /// </summary>
    public static Scp914Controller Scp914Controller => Scp914Controller.Singleton;

    /// <summary>
    /// Gets or sets SCP-914's <see cref="Scp914KnobSetting"/>.
    /// </summary>
    public static Scp914KnobSetting KnobSetting
    {
        get => Scp914Controller.Network_knobSetting;
        set => Scp914Controller.Network_knobSetting = value;
    }

    /// <summary>
    /// Gets or sets SCP-914's config mode.
    /// </summary>
    public static Scp914Mode ConfigMode
    {
        get => Scp914Controller._configMode.Value;
        set => Scp914Controller._configMode.Value = value;
    }

    /// <summary>
    /// Gets SCP-914's <see cref="GameObject"/>.
    /// </summary>
    public static GameObject Scp914GameObject => Scp914Controller.gameObject;

    /// <summary>
    /// Gets SCP-914's <see cref="Transform"/>.
    /// </summary>
    public static Transform Scp914Transform => Scp914Controller.transform;

    /// <summary>
    /// Gets or sets the position of SCP-914's intake chamber.
    /// </summary>
    public static Vector3 IntakePosition
    {
        get => Scp914Controller.IntakeChamber.localPosition;
        set => Scp914Controller.IntakeChamber.localPosition = value;
    }

    /// <summary>
    /// Gets or sets the position of SCP-914's output chamber.
    /// </summary>
    public static Vector3 OutputPosition
    {
        get => Scp914Controller.OutputChamber.localPosition;
        set => Scp914Controller.OutputChamber.localPosition = value;
    }

    /// <summary>
    /// Gets a value indicating whether SCP-914 is active and currently processing items.
    /// </summary>
    public static bool IsUpgrading => Scp914Controller._isUpgrading;

    /// <summary>
    /// Gets or sets the intake booth <see cref="Transform"/>.
    /// </summary>
    public static Transform IntakeBooth
    {
        get => Scp914Controller.IntakeChamber;
        set => Scp914Controller.IntakeChamber = value;
    }

    /// <summary>
    /// Gets or sets the output booth <see cref="Transform"/>.
    /// </summary>
    public static Transform OutputBooth
    {
        get => Scp914Controller.OutputChamber;
        set => Scp914Controller.OutputChamber = value;
    }

    /// <summary>
    /// Plays the SCP-914's sound.
    /// </summary>
    public static void PlaySound(Scp914InteractCode scp914InteractCode) => Scp914Controller.RpcPlaySound((byte)scp914InteractCode);

    /// <summary>
    /// Starts SCP-914.
    /// </summary>
    public static void Start(Player player, Scp914InteractCode scp914InteractCode = Scp914InteractCode.Activate) => Scp914Controller.ServerInteract(player?.ReferenceHub, (byte)scp914InteractCode);
}