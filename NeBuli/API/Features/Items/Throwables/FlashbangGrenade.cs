// -----------------------------------------------------------------------
// <copyright file=FlashbangGrenade.cs company="NebuliTeam">
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

public class FlashbangGrenade : Throwable
{
    /// <summary>
    /// Gets the <see cref="ThrowableItem"/> base.
    /// </summary>
    public new FlashbangProjectile Base { get; }

    internal FlashbangGrenade(ThrowableItem grenadeBase) : base(grenadeBase)
    {
        Base = (FlashbangProjectile)this.Projectile;
    }

    /// <summary>
    /// Spawns and activates a grenade.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public FlashbangProjectile SpawnAndActivate(Vector3 position, NebuliPlayer owner = null)
    {
        ItemPickupBase newGrenade = Object.Instantiate(Projectile.Base, position, Quaternion.identity);
        newGrenade.Info = new PickupSyncInfo(Base.ItemType, Base.ItemWeightKg, ItemSerialGenerator.GenerateNext());
        FlashbangProjectile grenade = (FlashbangProjectile)Pickup.Get(newGrenade);
        grenade.BlindTime = Base.BlindTime;
        grenade.AdditionalBlurDuration = Base.AdditionalBlurDuration;
        grenade.SurfaceZoneDistanceIntensifier = Base.SurfaceZoneDistanceIntensifier;
        grenade.FuzeTime = Base.FuzeTime;
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
    public FlashbangProjectile Spawn(Vector3 position, NebuliPlayer owner = null)
    {
        ItemPickupBase newGrenade = Object.Instantiate(Projectile.Base, position, Quaternion.identity);
        newGrenade.Info = new PickupSyncInfo(Base.ItemType, Base.ItemWeightKg, ItemSerialGenerator.GenerateNext());
        FlashbangProjectile grenade = (FlashbangProjectile)Pickup.Get(newGrenade);
        grenade.BlindTime = Base.BlindTime;
        grenade.AdditionalBlurDuration = Base.AdditionalBlurDuration;
        grenade.SurfaceZoneDistanceIntensifier = Base.SurfaceZoneDistanceIntensifier;
        grenade.FuzeTime = Base.FuzeTime;
        grenade.PreviousOwner = owner ?? Server.NebuliHost;
        grenade.Spawn();
        grenade.Base.gameObject.SetActive(true);
        return grenade;
    }
}