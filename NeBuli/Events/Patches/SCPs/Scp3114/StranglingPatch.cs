// -----------------------------------------------------------------------
// <copyright file=StranglingPatch.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.SCPs.Scp3114;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp3114;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.SCPs.Scp3114
{
    [HarmonyPatch(typeof(Scp3114Strangle), nameof(Scp3114Strangle.ProcessAttackRequest))]
    internal class StranglingPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnStrangling(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<StranglingPatch>(102, instructions);

            Label retLabel = generator.DefineLabel();
            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldloc_3);
            LocalBuilder @event = generator.DeclareLocal(typeof(Scp3114StranglingEvent));

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Scp3114Strangle), nameof(Scp3114Strangle.Owner))),
                new(OpCodes.Ldloc_3),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp3114StranglingEvent))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(Scp3114Handlers), nameof(Scp3114Handlers.OnStrangle))),
                new(OpCodes.Stloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp3114StranglingEvent), nameof(Scp3114StranglingEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(Scp3114StranglingEvent), nameof(Scp3114StranglingEvent.StrangleTarget))),
                new(OpCodes.Stloc_3)
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