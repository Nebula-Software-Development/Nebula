// -----------------------------------------------------------------------
// <copyright file=Scp2176Projectile.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Scp2176ProjectileBase = InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile;

namespace Nebula.API.Features.Items.Projectiles
{
    public class Scp2176Projectile : GrenadeEffectProjectile
    {
        internal Scp2176Projectile(Scp2176ProjectileBase itemBase) : base(itemBase)
        {
            Base = itemBase;
        }

        /// <summary>
        ///     Gets the <see cref="Scp2176ProjectileBase" /> base.
        /// </summary>
        public new Scp2176ProjectileBase Base { get; }

        /// <summary>
        ///     Gets or sets if the next collision will trigger the drop sound.
        /// </summary>
        public bool PlayDropSound
        {
            get => Base.Network_playedDropSound;
            set => Base.Network_playedDropSound = value;
        }

        /// <summary>
        ///     Gets if SCP-2176 has triggered.
        /// </summary>
        public bool HasTriggered => Base._hasTriggered;
    }
}