// -----------------------------------------------------------------------
// <copyright file=ExplosiveGrenade.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Items.Projectiles;
using Nebuli.API.Features.Player;
using UnityEngine;

namespace Nebuli.API.Features.Items.Throwables;

public class ExplosiveGrenade : Throwable
{
    /// <summary>
    /// Gets the <see cref="ExplosiveGrenadeProjectile"/> base.
    /// </summary>
    public new ExplosiveGrenadeProjectile Projectile { get; }

    internal ExplosiveGrenade(ThrowableItem itemBase) : base(itemBase)
    {
        Projectile = (ExplosiveGrenadeProjectile)((Throwable)this).Projectile;
    }

    /// <summary>
    /// Spawns and activates a grenade.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public ExplosiveGrenadeProjectile SpawnAndActivate(Vector3 position, NebuliPlayer owner = null)
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
        grenade.PreviousOwner = owner ?? Server.NebuliHost;
        grenade.Spawn();
        grenade.Base.gameObject.SetActive(true);
        grenade.Base.ServerActivate();
        return grenade;
    }

    /// <summary>
    /// Spawns a grenade.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public ExplosiveGrenadeProjectile Spawn(Vector3 position, NebuliPlayer owner = null)
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
        grenade.PreviousOwner = owner ?? Server.NebuliHost;
        grenade.Spawn();
        grenade.Base.gameObject.SetActive(true);
        return grenade;
    }
}