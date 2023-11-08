// -----------------------------------------------------------------------
// <copyright file=FlashbangProjectile.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items.ThrowableProjectiles;

namespace Nebuli.API.Features.Items.Projectiles;

public class FlashbangProjectile : GrenadeEffectProjectile
{
    /// <summary>
    /// Gets the <see cref="FlashbangGrenade"/> base.
    /// </summary>
    public new FlashbangGrenade Base { get; }

    internal FlashbangProjectile(FlashbangGrenade flashbang) : base(flashbang)
    {
        Base = flashbang;
    }

    /// <summary>
    /// Gets or sets the additional blur duration.
    /// </summary>
    public float AdditionalBlurDuration
    {
        get => Base._additionalBlurDuration;
        set => Base._additionalBlurDuration = value;
    }

    /// <summary>
    /// Gets or sets the total blind time.
    /// </summary>
    public float BlindTime
    {
        get => Base._blindTime;
        set => Base._blindTime = value;
    }

    /// <summary>
    /// idfk test this yourself.
    /// </summary>
    public float SurfaceZoneDistanceIntensifier
    {
        get => Base._surfaceZoneDistanceIntensifier;
        set => Base._surfaceZoneDistanceIntensifier = value;
    }
}