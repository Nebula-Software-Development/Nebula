using System.Linq;
using HarmonyLib;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using UnityEngine;

namespace Nebuli.Events.Patches.GameFixes;

[HarmonyPatch(typeof(AttachmentsServerHandler), nameof(AttachmentsServerHandler.SetupProvidedWeapon))]
internal static class WeaponAttachmentDesyncFix
{
    private static bool Prefix(ReferenceHub ply, InventorySystem.Items.ItemBase item)
    {
        if (item is Firearm firearm && !ply.inventory.UserInventory.ReserveAmmo.TryGetValue(firearm.AmmoType, out ushort num2))
        {
            bool isFlashlightEnabled = firearm.Attachments.Any(attachment => attachment.DescriptivePros.HasFlagFast(AttachmentDescriptiveAdvantages.Flashlight));
            int attachmentsCount = firearm.Attachments.Count(attachment => attachment.IsEnabled);
            firearm.Status = new FirearmStatus((byte)Mathf.Min(firearm.Status.Ammo, firearm.AmmoManagerModule.MaxAmmo), isFlashlightEnabled ? (FirearmStatusFlags.MagazineInserted | FirearmStatusFlags.FlashlightEnabled) : FirearmStatusFlags.MagazineInserted, (uint)attachmentsCount);
        }
        return true;
    }
}


