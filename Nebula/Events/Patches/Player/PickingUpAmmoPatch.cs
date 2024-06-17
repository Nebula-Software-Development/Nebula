// -----------------------------------------------------------------------
// <copyright file=PickingUpAmmoPatch.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using InventorySystem.Searching;
using Nebula.Events.EventArguments.Player;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Player
{
    [HarmonyPatch(typeof(AmmoSearchCompletor), nameof(AmmoSearchCompletor.Complete))]
    internal class PickingUpAmmoPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnPickingupAmmo(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<PickingUpAmmoPatch>(92, instructions);

            Label retLabel = generator.DefineLabel();

            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Brfalse) + 1;

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, Field(typeof(AmmoSearchCompletor), nameof(AmmoSearchCompletor.Hub))),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, Field(typeof(AmmoSearchCompletor), nameof(AmmoSearchCompletor.TargetPickup))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerPickingUpAmmoEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnPickingUpAmmo))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerPickingUpAmmoEvent), nameof(PlayerPickingUpAmmoEvent.IsCancelled))),
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