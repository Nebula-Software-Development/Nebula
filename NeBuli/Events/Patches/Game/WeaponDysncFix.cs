// -----------------------------------------------------------------------
// <copyright file=WeaponDysncFix.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Linq;
using HarmonyLib;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using UnityEngine;

namespace Nebula.Events.Patches.Game
{
    [HarmonyPatch(typeof(AttachmentsServerHandler), nameof(AttachmentsServerHandler.SetupProvidedWeapon))]
    internal static class WeaponAttachmentDesyncFix
    {
        [HarmonyPrefix]
        private static bool Prefix(ReferenceHub ply, ItemBase item)
        {
            if (item is Firearm firearm &&
                !ply.inventory.UserInventory.ReserveAmmo.TryGetValue(firearm.AmmoType, out ushort num2))
            {
                bool isFlashlightEnabled = firearm.Attachments.Any(attachment =>
                    attachment.DescriptivePros.HasFlagFast(AttachmentDescriptiveAdvantages.Flashlight));
                int attachmentsCount = firearm.Attachments.Count(attachment => attachment.IsEnabled);
                firearm.Status = new FirearmStatus(
                    (byte)Mathf.Min(firearm.Status.Ammo, firearm.AmmoManagerModule.MaxAmmo),
                    isFlashlightEnabled
                        ? FirearmStatusFlags.MagazineInserted | FirearmStatusFlags.FlashlightEnabled
                        : FirearmStatusFlags.MagazineInserted, (uint)attachmentsCount);
            }

            return true;
        }
    }
}