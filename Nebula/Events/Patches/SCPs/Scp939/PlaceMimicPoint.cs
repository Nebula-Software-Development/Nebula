﻿// -----------------------------------------------------------------------
// <copyright file=PlaceMimicPoint.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.SCPs.Scp939;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939.Mimicry;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.SCPs.Scp939
{
    [HarmonyPatch(typeof(MimicPointController), nameof(MimicPointController.ServerProcessCmd))]
    internal class PlaceMimicPoint
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnPlaceMimicPoint(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<PlaceMimicPoint>(30, instructions);

            Label retLabel = generator.DefineLabel();

            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldc_I4_S) - 1;

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Callvirt, PropertyGetter(typeof(MimicPointController), nameof(MimicPointController.Owner))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp939RemoveMimicPoint))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(Scp939Handlers), nameof(Scp939Handlers.OnRemoveMimicPoint))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp939RemoveMimicPoint), nameof(Scp939RemoveMimicPoint.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel)
            });

            index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Ldc_I4_S) - 1;

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Callvirt, PropertyGetter(typeof(MimicPointController), nameof(MimicPointController.Owner))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp939PlaceMimicPointEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(Scp939Handlers), nameof(Scp939Handlers.OnPlaceMimicPoint))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp939PlaceMimicPointEvent), nameof(Scp939PlaceMimicPointEvent.IsCancelled))),
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