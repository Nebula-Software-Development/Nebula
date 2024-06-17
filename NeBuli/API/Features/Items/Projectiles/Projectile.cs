// -----------------------------------------------------------------------
// <copyright file=Projectile.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items.ThrowableProjectiles;
using Nebula.API.Features.Items.Pickups;

namespace Nebula.API.Features.Items.Projectiles
{
    public class Projectile : Pickup
    {
        internal Projectile(ThrownProjectile thrownProjectile) : base(thrownProjectile)
        {
            Base = thrownProjectile;
        }

        /// <summary>
        ///     Gets the <see cref="ThrownProjectile" /> base.
        /// </summary>
        public new ThrownProjectile Base { get; }

        /// <summary>
        ///     Activates the projectile.
        /// </summary>
        public void Activate()
        {
            Base.ServerActivate();
        }
    }
}