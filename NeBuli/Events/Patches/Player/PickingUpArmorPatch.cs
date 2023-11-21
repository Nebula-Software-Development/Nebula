// -----------------------------------------------------------------------
// <copyright file=PickingUpArmorPatch.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using InventorySystem.Searching;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player
{
    [HarmonyPatch(typeof(ArmorSearchCompletor), nameof(ArmorSearchCompletor.Complete))]
    internal class PickingUpArmorPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnPickingupArmor(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<PickingUpArmorPatch>(51, instructions);

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, Field(typeof(ArmorSearchCompletor), nameof(ArmorSearchCompletor.Hub))),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, Field(typeof(ArmorSearchCompletor), nameof(ArmorSearchCompletor.TargetPickup))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerPickingUpArmorEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnPlayerPickingUpArmor))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerPickingUpArmorEvent), nameof(PlayerPickingUpArmorEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel)
            });

            newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}