// -----------------------------------------------------------------------
// <copyright file=GrenadeEffectProjectile.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items.ThrowableProjectiles;

namespace Nebula.API.Features.Items.Projectiles
{
    public class GrenadeEffectProjectile : TimedExplosiveProjectile
    {
        internal GrenadeEffectProjectile(EffectGrenade timeGrenade) : base(timeGrenade)
        {
            Base = timeGrenade;
        }

        /// <summary>
        ///     Gets the <see cref="EffectGrenade" /> base.
        /// </summary>
        public new EffectGrenade Base { get; }
    }
}