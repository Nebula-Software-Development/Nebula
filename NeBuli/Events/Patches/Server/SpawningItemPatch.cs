// -----------------------------------------------------------------------
// <copyright file=SpawningItemPatch.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using MapGeneration.Distributors;
using Nebula.Events.EventArguments.Server;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Server
{
    [HarmonyPatch(typeof(ItemDistributor), nameof(ItemDistributor.CreatePickup))]
    internal class SpawningItemPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnPickingupItem(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<SpawningItemPatch>(52, instructions);

            Label retLabel = generator.DefineLabel();

            int index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Stfld) + 1;

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Ldloc_1),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(SpawningItemEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(ServerHandlers), nameof(ServerHandlers.OnSpawningItem))),
                new(OpCodes.Callvirt, PropertyGetter(typeof(SpawningItemEvent), nameof(SpawningItemEvent.IsCancelled))),
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