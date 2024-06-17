// -----------------------------------------------------------------------
// <copyright file=ExplosiveGrenadeProjectile.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items.ThrowableProjectiles;
using UnityEngine;

namespace Nebula.API.Features.Items.Projectiles
{
    public class ExplosiveGrenadeProjectile : GrenadeEffectProjectile
    {
        internal ExplosiveGrenadeProjectile(ExplosionGrenade itemBase) : base(itemBase)
        {
            Base = itemBase;
        }

        /// <summary>
        ///     Gets the <see cref="ExplosionGrenade" /> base.
        /// </summary>
        public new ExplosionGrenade Base { get; }

        /// <summary>
        ///     Gets or sets the grenades max radius.
        /// </summary>
        public float MaxRadius
        {
            get => Base._maxRadius;
            set => Base._maxRadius = value;
        }

        /// <summary>
        ///     Gets or sets the burned duration of the grenade.
        /// </summary>
        public float BurnedDuration
        {
            get => Base._burnedDuration;
            set => Base._burnedDuration = value;
        }

        /// <summary>
        ///     Gets or sets the concussed duration of the grenade.
        /// </summary>
        public float ConcussedDuration
        {
            get => Base._concussedDuration;
            set => Base._concussedDuration = value;
        }

        /// <summary>
        ///     Gets or sets the deafened duration of the grenade.
        /// </summary>
        public float DeafenedDuration
        {
            get => Base._deafenedDuration;
            set => Base._deafenedDuration = value;
        }

        /// <summary>
        ///     Gets or sets the detection mask of the grenade.
        /// </summary>
        public LayerMask DetectionMask
        {
            get => Base._detectionMask;
            set => Base._detectionMask = value;
        }

        /// <summary>
        ///     Gets or sets the hume shield multiplier of the grenade.
        /// </summary>
        public float HumeShieldMultiplier
        {
            get => Base._humeShieldMultipler;
            set => Base._humeShieldMultipler = value;
        }

        /// <summary>
        ///     Gets or sets the SCP damange multiplier.
        /// </summary>
        public float SCPDamageMultiplier
        {
            get => Base._scpDamageMultiplier;
            set => Base._scpDamageMultiplier = value;
        }
    }
}