﻿// -----------------------------------------------------------------------
// <copyright file=PryingGatePatch.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.SCPs.Scp096;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp096;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.SCPs.Scp096
{
    [HarmonyPatch(typeof(Scp096PrygateAbility), nameof(Scp096PrygateAbility.ServerProcessCmd))]
    internal class PryingGatePatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnAddTarget(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<PryingGatePatch>(70, instructions);

            Label retLabel = generator.DefineLabel();

            int index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Pop) + 1;

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Scp096PrygateAbility), nameof(Scp096PrygateAbility.Owner))),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, Field(typeof(Scp096PrygateAbility), nameof(Scp096PrygateAbility._syncDoor))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp096PryingGateEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(Scp096Handlers), nameof(Scp096Handlers.OnPryingGate))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp096PryingGateEvent), nameof(Scp096PryingGateEvent.IsCancelled))),
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