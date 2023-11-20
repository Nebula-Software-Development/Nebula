// -----------------------------------------------------------------------
// <copyright file=GrenadeEffectProjectile.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items.ThrowableProjectiles;

namespace Nebuli.API.Features.Items.Projectiles;

public class GrenadeEffectProjectile : TimedExplosiveProjectile
{
    /// <summary>
    /// Gets the <see cref="EffectGrenade"/> base.
    /// </summary>
    public new EffectGrenade Base { get; }

    internal GrenadeEffectProjectile(EffectGrenade timeGrenade) : base(timeGrenade)
    {
        Base = timeGrenade;
    }
}