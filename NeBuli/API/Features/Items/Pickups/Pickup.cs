// -----------------------------------------------------------------------
// <copyright file=Pickup.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Footprinting;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using Mirror;
using Nebuli.API.Extensions;
using Nebuli.API.Features.Items.Pickups.SCPs;
using Nebuli.API.Features.Items.Projectiles;
using Nebuli.API.Features.Map;
using Nebuli.API.Features.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nebuli.API.Features.Items.Pickups;

/// <summary>
/// Wrapper class for handling <see cref="ItemPickupBase"/>.
/// </summary>
public class Pickup
{
    /// <summary>
    /// A Dictionary of <see cref="ItemPickupBase"/>, and their wrapper class <see cref="Pickup"/>.
    /// </summary>
    public static Dictionary<ItemPickupBase, Pickup> Dictionary = new();

    /// <summary>
    /// Gets if the pickup is spawned or not.
    /// </summary>
    public bool Spawned => NetworkServer.spawned.ContainsValue(Base.netIdentity);

    /// <summary>
    /// Gets the <see cref="ItemPickupBase"/>.
    /// </summary>
    public ItemPickupBase Base { get; }

    internal Pickup(ItemPickupBase pickupBase)
    {
        Base = pickupBase;
        Dictionary.AddIfMissing(pickupBase, this);
    }

    internal Pickup(ItemType type)
    {
        if (InventoryItemLoader.AvailableItems.TryGetValue(type, out ItemBase itemBase))
        {
            PickupSyncInfo info = new()
            {
                ItemId = type,
                Serial = ItemSerialGenerator.GenerateNext(),
                WeightKg = itemBase.Weight,
            };
            Base = Object.Instantiate(itemBase.PickupDropModel);
            Info = info;

            Dictionary.AddIfMissing(Base, this);
        }   
    }

    /// <summary>
    /// Gets a collection of all the current Pickups.
    /// </summary>
    public static IEnumerable<Pickup> Collection => Dictionary.Values;

    /// <summary>
    /// Gets a list of all the current Pickups.
    /// </summary>
    public static List<Pickup> List => Collection.ToList();

    /// <summary>
    /// Gets or sets the Pickup's <see cref="PickupSyncInfo"/>.
    /// </summary>
    public PickupSyncInfo Info
    {
        get => Base.NetworkInfo;
        set => Base.Info = value;
    }

    /// <summary>
    /// Gets the <see cref="Pickup"/> gameobject.
    /// </summary>
    public GameObject GameObject => Base.gameObject;

    /// <summary>
    /// Gets the Pickup's <see cref="UnityEngine.Rigidbody"/>.
    /// </summary>
    public Rigidbody Rigidbody => StandardPhysicsModule.Rb;

    /// <summary>
    /// Gets the Pickup's <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public Transform Transform => Base.transform;

    /// <summary>
    /// Gets the pickups serial.
    /// </summary>
    public ushort Serial
    {
        get => Base.Info.Serial;
        set => Base.Info.Serial = value;
    }

    /// <summary>
    /// Gets or sets the pickups scale.
    /// </summary>
    public Vector3 Scale
    {
        get => Transform.localScale;
        set
        {
            if (!Spawned)
            {
                Transform.localScale = value;
                return;
            }
            Despawn();
            Transform.localScale = value;
            Spawn();
        }
    }

    /// <summary>
    /// Gets or sets if the pickup is locked.
    /// </summary>
    public bool Locked
    {
        get => Base.Info.Locked;
        set => Base.Info.Locked = value;
    }

    /// <summary>
    /// Sets the search time needed to pick this item up by the specified player.
    /// </summary>
    /// <param name="player">The player to set it to.</param>
    public void SearchTime(NebuliPlayer player) => Base.SearchTimeForPlayer(player.ReferenceHub);

    /// <summary>
    /// Sets the search time needed to pick this item up by the specified ReferenceHub.
    /// </summary>
    /// <param name="hub">The ReferenceHub to set it to.</param>
    public void SearchTime(ReferenceHub hub) => Base.SearchTimeForPlayer(hub);

    /// <summary>
    /// Gets the pickups ItemType.
    /// </summary>
    public ItemType ItemType => Base.Info.ItemId;

    /// <summary>
    /// Gets or sets the Items WeightKg.
    /// </summary>
    public float ItemWeightKg
    {
        get => Base.Info.WeightKg;
        set => Base.Info.WeightKg = value;
    }

    /// <summary>
    /// Gets or sets if the pickup is in use.
    /// </summary>
    public bool InUse
    {
        get => Base.Info.InUse;
        set => Base.Info.InUse = value;
    }

    /// <summary>
    /// Gets the Pickup's NetId.
    /// </summary>
    public uint NetId => Base.netId;

    /// <summary>
    /// Gets the <see cref="Pickup"/> current <see cref="Map.Room"/>.
    /// </summary>
    public Room Room => Room.Get(Position);

    /// <summary>
    /// Gets or sets the Pickup's <see cref="PickupPhysicsModule"/>.
    /// </summary>
    public PickupPhysicsModule PhysicsModule
    {
        get => Base.PhysicsModule;
        set => Base.PhysicsModule = value;
    }

    /// <summary>
    /// Gets the Pickup's <see cref="PickupStandardPhysics"/> module.
    /// </summary>
    public PickupStandardPhysics StandardPhysicsModule => Base.PhysicsModule as PickupStandardPhysics;

    /// <summary>
    /// Gets the previous owner's <see cref="Footprint"/>
    /// </summary>
    public Footprint PreviousOwnerFootPrint
    {
        get => Base.PreviousOwner;
        set => Base.PreviousOwner = value;
    }

    /// <summary>
    /// Gets the previous owner's ReferenceHub.
    /// </summary>
    public ReferenceHub PreviousOwnerRefHub
    {
        get => ReferenceHub.GetHub(Base.PreviousOwner.PlayerId);
        set => Base.PreviousOwner = new Footprint(value);
    }

    /// <summary>
    /// Gets the previous owner as a <see cref="NebuliPlayer"/>.
    /// </summary>
    public NebuliPlayer PreviousOwner
    {
        get => NebuliPlayer.Get(Base.PreviousOwner);
        set => Base.PreviousOwner = value.Footprint;
    }

    /// <summary>
    /// Gets or sets the position of the pickup.
    /// </summary>
    public Vector3 Position
    {
        get => Base.Position;
        set => Base.Position = value;
    }

    /// <summary>
    /// Gets or sets the rotation of the pickup.
    /// </summary>
    public Quaternion Rotation
    {
        get => Base.Rotation;
        set => Base.Rotation = value;
    }

    /// <summary>
    /// Spawns a pickup at a given position and rotation with an optional previous owner.
    /// </summary>
    /// <param name="pickup">The <see cref="Pickup"/> to spawn.</param>
    /// <param name="position">The position where the pickup should spawn.</param>
    /// <param name="rotation">The rotation of the pickup when spawned.</param>
    /// <param name="oldOwner">The previous owner of the pickup. If null, the server's host player will be the previous owner.</param>
    public static void SpawnPickup(Pickup pickup, Vector3 position = default, Quaternion rotation = default, NebuliPlayer oldOwner = null)
    {
        oldOwner ??= Server.NebuliHost;
        pickup.Position = position;
        pickup.Rotation = rotation;
        pickup.PreviousOwner = oldOwner;
        pickup.Spawn();
    }

    /// <summary>
    /// Destroys the pickup.
    /// </summary>
    public void Destroy()
    {
        Dictionary.RemoveIfContains(Base);
        Base.DestroySelf();
    }

    /// <summary>
    /// Gets a <see cref="Pickup"/> that's Serial matches the given serial.
    /// </summary>
    public static Pickup Get(ushort pickupSerial) => List.FirstOrDefault(x => x.Serial == pickupSerial);

    /// <summary>
    /// Gets a <see cref="Pickup"/> that's <see cref="UnityEngine.GameObject"/> matches the given <see cref="UnityEngine.GameObject"/>.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static Pickup Get(GameObject gameObject) => List.FirstOrDefault(x => x.GameObject == gameObject);

    /// <summary>
    /// Creates a pickup given the ItemType.
    /// </summary>
    public static Pickup Create(ItemType type) => new(type);

    /// <summary>
    /// Creates a pickup given the <see cref="Item"/>.
    /// </summary>
    public static Pickup Create(Item item) => new(item.Base.PickupDropModel);

    /// <summary>
    /// Creates and spawns a pickup given the ItemType.
    /// </summary>
    public static Pickup CreateAndSpawn(ItemType type, Vector3 position)
    {
        Pickup pickup = Create(type);
        pickup.Position = position;
        pickup.Spawn();
        return pickup;
    }

    /// <summary>
    /// Spawns the pickup.
    /// </summary>
    public void Spawn()
    {
        NetworkServer.Spawn(GameObject);
        return;
    }

    /// <summary>
    /// Despawns the pickup.
    /// </summary>
    public void Despawn()
    {
        NetworkServer.UnSpawn(GameObject);
        return;
    }

    /// <summary>
    /// Tries to get a <see cref="Pickup"/> with a <see cref="ItemPickupBase"/>. If one cannot be found, it is created.
    /// </summary>
    /// <param name="ItemBase">The <see cref="ItemPickupBase"/> to find the <see cref="Pickup"/> with.</param>

    public static Pickup Get(ItemPickupBase ItemBase)
    {
        if (ItemBase == null)
            return null;

        if (Dictionary.ContainsKey(ItemBase)) return Dictionary[ItemBase];

        return ItemBase switch
        {
            InventorySystem.Items.Firearms.FirearmPickup firearm => new FirearmPickup(firearm),
            InventorySystem.Items.Keycards.KeycardPickup keycard => new KeycardPickup(keycard),
            InventorySystem.Items.Armor.BodyArmorPickup armorPickup => new ArmorPickup(armorPickup),
            InventorySystem.Items.Firearms.Ammo.AmmoPickup ammoPickup => new AmmoPickup(ammoPickup),
            InventorySystem.Items.Jailbird.JailbirdPickup jailbirdPickup => new JailbirdPickup(jailbirdPickup),
            InventorySystem.Items.MicroHID.MicroHIDPickup microHID => new MicroHIDPickup(microHID),
            InventorySystem.Items.Radio.RadioPickup radio => new RadioPickup(radio),
            InventorySystem.Items.ThrowableProjectiles.Scp018Projectile scp018 => new Scp018Projectile(scp018),
            InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile scp2174 => new Scp2176Projectile(scp2174),
            InventorySystem.Items.ThrowableProjectiles.ExplosionGrenade explosionGrenade => new ExplosiveGrenadeProjectile(explosionGrenade),
            InventorySystem.Items.ThrowableProjectiles.FlashbangGrenade flashbangGrenade => new FlashbangProjectile(flashbangGrenade),
            InventorySystem.Items.ThrowableProjectiles.EffectGrenade effectGrenade => new GrenadeEffectProjectile(effectGrenade),
            InventorySystem.Items.Usables.Scp330.Scp330Pickup scp330 => new Scp330Pickup(scp330),
            InventorySystem.Items.Usables.Scp244.Scp244DeployablePickup scp244 => new Scp244Pickup(scp244),
            InventorySystem.Items.Usables.Scp1576.Scp1576Pickup scp1576 => new Scp1576Pickup(scp1576),
            _ => new Pickup(ItemBase),
        };
    }
}