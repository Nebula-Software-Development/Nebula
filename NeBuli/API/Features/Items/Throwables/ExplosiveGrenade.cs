// -----------------------------------------------------------------------
// <copyright file=ExplosiveGrenade.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using Nebula.API.Features.Items.Pickups;
using Nebula.API.Features.Items.Projectiles;
using UnityEngine;

namespace Nebula.API.Features.Items.Throwables
{
    public class ExplosiveGrenade : Throwable
    {
        internal ExplosiveGrenade(ThrowableItem itemBase) : base(itemBase)
        {
            Projectile = (ExplosiveGrenadeProjectile)((Throwable)this).Projectile;
        }

        /// <summary>
        ///     Gets the <see cref="ExplosiveGrenadeProjectile" /> base.
        /// </summary>
        public new ExplosiveGrenadeProjectile Projectile { get; }

        /// <summary>
        ///     Spawns and activates a grenade.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public ExplosiveGrenadeProjectile SpawnAndActivate(Vector3 position, Player owner = null)
        {
            ItemPickupBase newGrenade = Object.Instantiate(Projectile.Base, position, Quaternion.identity);
            newGrenade.Info = new PickupSyncInfo(Base.ItemTypeId, Base.Weight, ItemSerialGenerator.GenerateNext());
            ExplosiveGrenadeProjectile grenade = (ExplosiveGrenadeProjectile)Pickup.Get(newGrenade);
            grenade.MaxRadius = Projectile.MaxRadius;
            grenade.BurnedDuration = Projectile.BurnedDuration;
            grenade.DeafenedDuration = Projectile.DeafenedDuration;
            grenade.ConcussedDuration = Projectile.ConcussedDuration;
            grenade.DetectionMask = Projectile.DetectionMask;
            grenade.SCPDamageMultiplier = Projectile.SCPDamageMultiplier;
            grenade.FuzeTime = Projectile.FuzeTime;
            grenade.PreviousOwner = owner ?? Server.Host;
            grenade.Spawn();
            grenade.Base.gameObject.SetActive(true);
            grenade.Base.ServerActivate();
            return grenade;
        }

        /// <summary>
        ///     Spawns a grenade.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public ExplosiveGrenadeProjectile Spawn(Vector3 position, Player owner = null)
        {
            ItemPickupBase newGrenade = Object.Instantiate(Projectile.Base, position, Quaternion.identity);
            newGrenade.Info = new PickupSyncInfo(Base.ItemTypeId, Base.Weight, ItemSerialGenerator.GenerateNext());
            ExplosiveGrenadeProjectile grenade = (ExplosiveGrenadeProjectile)Pickup.Get(newGrenade);
            grenade.MaxRadius = Projectile.MaxRadius;
            grenade.BurnedDuration = Projectile.BurnedDuration;
            grenade.DeafenedDuration = Projectile.DeafenedDuration;
            grenade.ConcussedDuration = Projectile.ConcussedDuration;
            grenade.DetectionMask = Projectile.DetectionMask;
            grenade.SCPDamageMultiplier = Projectile.SCPDamageMultiplier;
            grenade.FuzeTime = Projectile.FuzeTime;
            grenade.PreviousOwner = owner ?? Server.Host;
            grenade.Spawn();
            grenade.Base.gameObject.SetActive(true);
            return grenade;
        }
    }
}