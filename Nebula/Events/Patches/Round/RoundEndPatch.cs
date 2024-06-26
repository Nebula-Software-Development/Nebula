﻿// -----------------------------------------------------------------------
// <copyright file=RoundEndPatch.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.Round;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Round
{
    [HarmonyPatch(typeof(RoundSummary), nameof(RoundSummary.RpcShowRoundSummary))]
    internal class RoundEndPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnRoundEnd(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<RoundEndPatch>(36, instructions);

            int index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Callvirt) + 1;

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_3),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(RoundEndEvent))[0]),
                new(OpCodes.Call, Method(typeof(RoundHandlers), nameof(RoundHandlers.OnRoundEnd)))
            });

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}