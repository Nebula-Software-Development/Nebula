// -----------------------------------------------------------------------
// <copyright file=MapGeneratedPatch.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using MapGeneration;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Game
{
    [HarmonyPatch(typeof(SeedSynchronizer), nameof(SeedSynchronizer.Update))]
    internal class MapGeneratedPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnDetonating(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<MapGeneratedPatch>(121, instructions);

            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Brfalse_S) + 4;

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Call, Method(typeof(ServerHandlers), nameof(ServerHandlers.OnMapGenerated)))
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