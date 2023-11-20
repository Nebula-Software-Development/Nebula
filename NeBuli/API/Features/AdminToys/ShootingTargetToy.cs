// -----------------------------------------------------------------------
// <copyright file=ShootingTargetToy.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features.Enum;
using Nebuli.API.Features.Map;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using SLTarget = AdminToys.ShootingTarget;

namespace Nebuli.API.Features.AdminToys;

/// <summary>
/// Wrapper class for <see cref="global::AdminToys.ShootingTarget"/>.
/// </summary>
public class ShootingTargetToy : AdminToy
{
    private static readonly Dictionary<string, ShootingTargetToyType> nametotype = new()
    {
    { "sportTargetPrefab", ShootingTargetToyType.Sport },
    { "dboyTargetPrefab", ShootingTargetToyType.DClass },
    { "binaryTargetPrefab", ShootingTargetToyType.Binary },
    };

    /// <summary>
    /// Gets the <see cref="SLTarget"/> (base) of the <see cref="ShootingTargetToy"/>.
    /// </summary>
    public new SLTarget Base { get; }

    internal ShootingTargetToy(SLTarget target) : base(target, ToyType.ShootingTarget)
    {
        Base = target;
        ShootingTargetToyType = GetTypeFromName(GameObject.name);
    }

    /// <summary>
    /// Gets the <see cref="UnityEngine.GameObject"/> of the the <see cref="ShootingTargetToy"/>.
    /// </summary>
    public GameObject Bullseye => Base._bullsEye.gameObject;

    /// <summary>
    /// Gets the bullseye location of the <see cref="ShootingTargetToy"/>.
    /// </summary>
    public Vector3 BullseyePosition => Base._bullsEye.position;

    /// <summary>
    /// Gets or sets the bullseye radius of the <see cref="ShootingTargetToy"/>.
    /// </summary>
    public float BullseyeRadius
    {
        get => Base._bullsEyeRadius;
        set => Base._bullsEyeRadius = value;
    }

    /// <summary>
    /// Gets or sets the max health of the <see cref="ShootingTargetToy"/>.
    /// </summary>
    public int MaxHealth
    {
        get => Base._maxHp;
        set
        {
            Base._maxHp = value;
            Base.RpcSendInfo(MaxHealth, AutoResetTime);
        }
    }

    /// <summary>
    /// Gets or sets the current health of the <see cref="ShootingTargetToy"/>.
    /// </summary>
    public float Health
    {
        get => Base._hp;
        set => Base._hp = value;
    }

    /// <summary>
    /// Gets or sets the remaining health of the <see cref="ShootingTargetToy"/>.
    /// </summary>
    public int AutoResetTime
    {
        get => Base._autoDestroyTime;
        set
        {
            Base._autoDestroyTime = Mathf.Max(0, value);
            Base.RpcSendInfo(MaxHealth, AutoResetTime);
        }
    }

    /// <summary>
    /// Gets or sets if the <see cref="ShootingTargetToy"/> is in sync mode.
    /// </summary>
    public bool IsSynced
    {
        get => Base.Network_syncMode;
        set => Base.Network_syncMode = value;
    }

    /// <summary>
    /// Clears the <see cref="ShootingTargetToy"/>.
    /// </summary>
    public void ClearTarget() => Base.ClearTarget();

    /// <summary>
    /// Gets a <see cref="ShootingTargetToy"/> with the matching <see cref="SLTarget"/> base.
    /// </summary>
    public static ShootingTargetToy Get(SLTarget primitiveObjectToy) => Utilites.AdminToys
        .FirstOrDefault(x => x.Base == primitiveObjectToy) as ShootingTargetToy ?? new ShootingTargetToy(primitiveObjectToy);

    /// <summary>
    /// Gets the type of the target.
    /// </summary>
    public ShootingTargetToyType ShootingTargetToyType { get; }

    /// <summary>
    /// Creates a new <see cref="ShootingTargetToy"/>.
    /// </summary>
    public static ShootingTargetToy Create(ShootingTargetToyType type, Vector3 position = default, Quaternion rotation = default, Vector3 scale = default, bool spawn = true)
    {
        ShootingTargetToy shootingTargetToy = type switch
        {
            ShootingTargetToyType.DClass => new ShootingTargetToy(Object.Instantiate(ToyUtilities.DClassShootingTarget)),
            ShootingTargetToyType.Binary => new ShootingTargetToy(Object.Instantiate(ToyUtilities.BinaryShootingTarget)),
            _ => new ShootingTargetToy(Object.Instantiate(ToyUtilities.SportShootingTarget)),
        };
        shootingTargetToy.Position = position;
        shootingTargetToy.Rotation = rotation;
        shootingTargetToy.Scale = scale;
        if (spawn) shootingTargetToy.SpawnToy();
        return shootingTargetToy;
    }

    private ShootingTargetToyType GetTypeFromName(string name)
    {
        if (nametotype.TryGetValue(name.Substring(0, name.Length - 7), out ShootingTargetToyType type))
            return type;
        return ShootingTargetToyType.Undefined;
    }
}