﻿// -----------------------------------------------------------------------
// <copyright file=WarheadStoppingPatch.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.Server;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Warhead
{
    [HarmonyPatch(typeof(AlphaWarheadController), nameof(AlphaWarheadController.CancelDetonation),
        typeof(ReferenceHub))]
    internal class WarheadStoppingPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnStopping(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<WarheadStoppingPatch>(113, instructions);

            Label retLabel = generator.DefineLabel();

            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + 1;

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_1).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(WarheadStoppingEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(ServerHandlers), nameof(ServerHandlers.OnWarheadStopping))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(WarheadStoppingEvent), nameof(WarheadStoppingEvent.IsCancelled))),
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