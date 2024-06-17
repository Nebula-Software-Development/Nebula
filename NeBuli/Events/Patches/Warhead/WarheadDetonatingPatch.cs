// -----------------------------------------------------------------------
// <copyright file=WarheadDetonatingPatch.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
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
    [HarmonyPatch(typeof(AlphaWarheadController), nameof(AlphaWarheadController.Detonate))]
    internal class WarheadDetonatingPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnDetonating(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<WarheadDetonatingPatch>(187, instructions);

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(AlphaWarheadController), nameof(AlphaWarheadController.Singleton))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(AlphaWarheadController), nameof(AlphaWarheadController.WarheadTriggeredby))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(WarheadDetonatingEvent))[0]),
                new(OpCodes.Call, Method(typeof(ServerHandlers), nameof(ServerHandlers.OnWarheadDetonated))),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(WarheadDetonatingEvent), nameof(WarheadDetonatingEvent.IsCancelled))),
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