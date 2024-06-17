// -----------------------------------------------------------------------
// <copyright file=ChangedItemPatch.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using InventorySystem;
using Nebula.Events.EventArguments.Player;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Player
{
    [HarmonyPatch(typeof(Inventory), nameof(Inventory.CurInstance), MethodType.Setter)]
    internal class ChangedItemPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnChanged(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<ChangedItemPatch>(39, instructions);

            newInstructions.InsertRange(newInstructions.Count - 1, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[newInstructions.Count - 1]),
                new(OpCodes.Ldfld, Field(typeof(Inventory), nameof(Inventory._hub))),
                new(OpCodes.Ldloc_0),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerChangedItemEvent))[0]),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnChangedItem)))
            });

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}