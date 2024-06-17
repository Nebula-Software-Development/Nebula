// -----------------------------------------------------------------------
// <copyright file=Throwable.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items.ThrowableProjectiles;
using Nebula.API.Features.Items.Pickups;
using Nebula.API.Features.Items.Projectiles;
using PlayerRoles.FirstPersonControl;
using UnityEngine;

namespace Nebula.API.Features.Items.Throwables
{
    public class Throwable : Item
    {
        internal Throwable(ThrowableItem itemBase) : base(itemBase)
        {
            Base = itemBase;

            //Credit to EXILED for method of setting the projectile properly
            Base.Projectile.gameObject.SetActive(false);
            Projectile = Pickup.Get(Object.Instantiate(Base.Projectile)) as Projectile;
            Base.Projectile.gameObject.SetActive(true);
            Projectile.Serial = Serial;
        }

        /// <summary>
        ///     Gets the <see cref="ThrowableItem" /> base.
        /// </summary>
        public new ThrowableItem Base { get; }

        /// <summary>
        ///     Gets if the throwable is ready to cancel.
        /// </summary>
        public bool ReadyToCancel => Base.ReadyToCancel;

        /// <summary>
        ///     Gets if the throwable is ready to be thrown.
        /// </summary>
        public bool ReadyToThrow => Base.ReadyToThrow;

        /// <summary>
        ///     Gets the throwables <see cref="ThrownProjectile" />.
        /// </summary>
        public Projectile Projectile { get; }

        /// <summary>
        ///     Gets or sets the time for the throwables pin pull.
        /// </summary>
        public float TimeForPinPull
        {
            get => Base._pinPullTime;
            set => Base._pinPullTime = value;
        }

        /// <summary>
        ///     Gets or sets if the throwable is re-pickupable after being thrown.
        /// </summary>
        public bool IsRepickable
        {
            get => Base._repickupable;
            set => Base._repickupable = value;
        }

        /// <summary>
        ///     Gets the throwables time tolerance.
        /// </summary>
        public float TimeTolerance => Base.CurrentTimeTolerance;

        /// <summary>
        ///     Throws the projectile.
        /// </summary>
        /// <param name="forceAmount">The amount of force to throw it with.</param>
        public void ThrowProjectile(float forceAmount)
        {
            ThrowableItem.ProjectileSettings projectileSettings = new();
            Base.ServerThrow(forceAmount, projectileSettings.UpwardsFactor, projectileSettings.StartTorque,
                ThrowableNetworkHandler.GetLimitedVelocity(Owner?.ReferenceHub.GetVelocity() ?? Vector3.one));
        }
    }
}