// -----------------------------------------------------------------------
// <copyright file=TriggeringTesla.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.Player;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.Player
{
    [HarmonyPatch(typeof(TeslaGateController), nameof(TeslaGateController.FixedUpdate))]
    internal class TriggeringTesla
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnTriggerTesla(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<TriggeringTesla>(101, instructions);

            int index = newInstructions.FindIndex(instruction =>
                instruction.Calls(PropertyGetter(typeof(ReferenceHub), nameof(ReferenceHub.AllHubs))));

            newInstructions.RemoveRange(index,
                newInstructions.FindIndex(i => i.opcode == OpCodes.Endfinally) + 1 - index);

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Ldloc_1),
                new(OpCodes.Ldloca_S, 2),
                new(OpCodes.Ldloca_S, 3),
                new(OpCodes.Call,
                    Method(typeof(TriggeringTesla), nameof(ProcessEvent),
                        new[] { typeof(TeslaGate), typeof(bool).MakeByRefType(), typeof(bool).MakeByRefType() }))
            });

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }

        private static void ProcessEvent(TeslaGate teslaGate, ref bool inIdleRange, ref bool isTriggerable)
        {
            foreach (API.Features.Player player in API.Features.Player.List.Where(ply =>
                         teslaGate.IsInIdleRange(ply.ReferenceHub)))
            {
                PlayerTriggeringTeslaEvent args = new(player, teslaGate);
                PlayerHandlers.OnTriggerTesla(args);

                if (args.IsCancelled)
                {
                    isTriggerable = false;
                    inIdleRange = false;
                    continue;
                }

                isTriggerable = args.IsTriggerable;
                inIdleRange = args.IsInIdleRange;
            }
        }
    }
}