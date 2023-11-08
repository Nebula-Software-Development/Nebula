// -----------------------------------------------------------------------
// <copyright file=MapGeneratedPatch.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using HarmonyLib;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Game;

[HarmonyPatch(typeof(MapGeneration.SeedSynchronizer), nameof(MapGeneration.SeedSynchronizer.Update))]
internal class MapGeneratedPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnDetonating(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<MapGeneratedPatch>(121, instructions);

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Brfalse_S) + 4;

        Label retLabel = generator.DefineLabel();

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
           new(OpCodes.Call, Method(typeof(ServerHandlers), nameof(ServerHandlers.OnMapGenerated))),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}