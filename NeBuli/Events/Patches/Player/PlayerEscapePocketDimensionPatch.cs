// -----------------------------------------------------------------------
// <copyright file=PlayerEscapePocketDimensionPatch.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.Player;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Player
{
    [HarmonyPatch(typeof(PocketDimensionTeleport), nameof(PocketDimensionTeleport.OnTriggerEnter))]
    internal class PlayerEscapePocketDimensionPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnEscaping(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<PlayerEscapePocketDimensionPatch>(102, instructions);

            Label retLabel = generator.DefineLabel();

            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldloc_2);

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Ldloc_1),
                new(OpCodes.Ldc_I4_1),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerEscapingPocketEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnEscapingPocket))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerEscapingPocketEvent), nameof(PlayerEscapingPocketEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel)
            });

            index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldc_I4_0) - 1;

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Ldloc_1),
                new(OpCodes.Ldc_I4_0),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerEscapingPocketEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnEscapingPocket))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(PlayerEscapingPocketEvent), nameof(PlayerEscapingPocketEvent.IsCancelled))),
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