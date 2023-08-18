﻿using Nebuli.API.Features.Enum;
using System.Collections.Generic;
using System;
using Nebuli.API.Features.Structs;
using Nebuli.API.Features.Items;
using System.Linq;
using InventorySystem.Items.Firearms.Attachments;

namespace Nebuli.API.Extensions;

/// <summary>
/// Extension methods for <see cref="ItemType"/> enumeration.
/// </summary>
public static class ItemTypeExtension
{
    /// <summary>
    /// Checks if the specified <see cref="ItemType"/> corresponds to an ammunition type.
    /// </summary>
    public static bool IsAmmoType(this ItemType item) => item.ToAmmoType() is not AmmoType.None;

    /// <summary>
    /// Checks if the specified <see cref="ItemType"/> corresponds to a firearm type.
    /// </summary>
    public static bool IsFirearmType(this ItemType type) => type.ToFirearmType() is not FirearmType.None;

    /// <summary>
    /// Retrieves a collection of <see cref="ItemType"/> from a collection of <see cref="Item"/>.
    /// </summary>
    public static IEnumerable<ItemType> GetItemTypesFromItems(this IEnumerable<Item> items) =>
        items.Select(item => item.ItemType);

    /// <summary>
    /// Converts a <see cref="FirearmType"/> to its corresponding <see cref="ItemType"/>.
    /// </summary>
    public static ItemType ConvertToItemType(this FirearmType type) => type switch
    {
        FirearmType.COM15 => ItemType.GunCOM15,
        FirearmType.COM18 => ItemType.GunCOM18,
        FirearmType.E11SR => ItemType.GunE11SR,
        FirearmType.Crossvec => ItemType.GunCrossvec,
        FirearmType.FSP9 => ItemType.GunFSP9,
        FirearmType.Logicer => ItemType.GunLogicer,
        FirearmType.Revolver => ItemType.GunRevolver,
        FirearmType.AK => ItemType.GunAK,
        FirearmType.Shotgun => ItemType.GunShotgun,
        FirearmType.COM45 => ItemType.GunCom45,
        FirearmType.ParticleDisruptor => ItemType.ParticleDisruptor,
        _ => ItemType.None,
    };

    /// <summary>
    /// Converts an <see cref="AmmoType"/> to its corresponding <see cref="ItemType"/>.
    /// </summary>
    public static ItemType ConvertToItemType(this AmmoType type) => type switch
    {
        AmmoType.NATO556 => ItemType.Ammo556x45,
        AmmoType.NATO762 => ItemType.Ammo762x39,
        AmmoType.NATO9 => ItemType.Ammo9x19,
        AmmoType.Ammo12Gauge => ItemType.Ammo12gauge,
        AmmoType.Ammo44Caliber => ItemType.Ammo44cal,
        _ => ItemType.None,
    };

    /// <summary>
    /// Converts a <see cref="ItemType"/> to its corresponding <see cref="AmmoType"/>.
    /// </summary>
    public static AmmoType ToAmmoType(this ItemType type) => type switch
    {
        ItemType.Ammo9x19 => AmmoType.NATO9,
        ItemType.Ammo556x45 => AmmoType.NATO556,
        ItemType.Ammo762x39 => AmmoType.NATO762,
        ItemType.Ammo12gauge => AmmoType.Ammo12Gauge,
        ItemType.Ammo44cal => AmmoType.Ammo44Caliber,
        _ => AmmoType.None,
    };


    /// <summary>
    /// Converts a <see cref="ItemType"/> to its corresponding <see cref="FirearmType"/>.
    /// </summary>
    public static FirearmType ToFirearmType(this ItemType type) => type switch
    {
        ItemType.GunCOM15 => FirearmType.COM15,
        ItemType.GunCOM18 => FirearmType.COM18,
        ItemType.GunE11SR => FirearmType.E11SR,
        ItemType.GunCrossvec => FirearmType.Crossvec,
        ItemType.GunFSP9 => FirearmType.FSP9,
        ItemType.GunLogicer => FirearmType.Logicer,
        ItemType.GunRevolver => FirearmType.Revolver,
        ItemType.GunAK => FirearmType.AK,
        ItemType.GunShotgun => FirearmType.Shotgun,
        ItemType.GunCom45 => FirearmType.COM45,
        ItemType.ParticleDisruptor => FirearmType.ParticleDisruptor,
        ItemType.MicroHID => FirearmType.MircoHID,
        _ => FirearmType.None,
    };

    /// <summary>
    /// Retrieves a collection of <see cref="AttachmentIdentity"/> for a specific <see cref="FirearmType"/> and attachment code.
    /// </summary>
    public static IEnumerable<AttachmentIdentity> GetAttachmentIdentifiers(this FirearmType type, uint code)
    {
        if (type.GetBaseCode() > code)
            code = type.GetBaseCode();

        if (!Firearm.TypeToFirearm.TryGetValue(type, out Firearm firearm))
            throw new ArgumentException($"Couldn't find a Firearm instance matching the ItemType value: {type}.");

        firearm.Base.ApplyAttachmentsCode(code, true);

        return firearm.AttachmentIdentities;
    }

    /// <summary>
    /// Retrieves the base code for a specific <see cref="FirearmType"/>.
    /// </summary>
    public static uint GetBaseCode(this FirearmType type)
    {
       if (type == FirearmType.None)
          return 0;

       if (Firearm.BaseCodes.TryGetValue(type, out uint baseCode))
          return baseCode;

       throw new KeyNotFoundException($"Basecode for weapon {type} not found!" + $" Stored BaseCodes:\nKeys: [{string.Join(", ", Firearm.BaseCodes.Keys)}]" + $" Values: [{string.Join(", ", Firearm.BaseCodes.Values)}]");
    }

}