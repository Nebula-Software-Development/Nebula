// -----------------------------------------------------------------------
// <copyright file=UpgradingItemPatch.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebula.Events.EventArguments.SCPs.Scp914;
using Nebula.Events.Handlers;
using NorthwoodLib.Pools;
using Scp914;
using static HarmonyLib.AccessTools;

namespace Nebula.Events.Patches.SCPs.Scp914
{
    [HarmonyPatch(typeof(Scp914Upgrader), nameof(Scp914Upgrader.ProcessPickup))]
    internal class UpgradingItemPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnItemUpgrade(IEnumerable<CodeInstruction> instructions,
            ILGenerator generator)
        {
            List<CodeInstruction> newInstructions =
                EventManager.CheckPatchInstructions<UpgradingItemPatch>(60, instructions);

            Label retLabel = generator.DefineLabel();

            int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Stloc_1) + 1;

            LocalBuilder @event = generator.DeclareLocal(typeof(UpgradingItemEvent));

            newInstructions.InsertRange(index, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldarg_1),
                new(OpCodes.Ldarg_3),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(UpgradingItemEvent))[0]),
                new(OpCodes.Stloc_S, @event.LocalIndex),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Call, Method(typeof(Scp914Handlers), nameof(Scp914Handlers.OnUpgradingItem))),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(UpgradingItemEvent), nameof(UpgradingItemEvent.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(UpgradingItemEvent), nameof(UpgradingItemEvent.KnobSetting))),
                new(OpCodes.Starg_S, 3),
                new(OpCodes.Ldloc_S, @event.LocalIndex),
                new(OpCodes.Callvirt,
                    PropertyGetter(typeof(UpgradingItemEvent), nameof(UpgradingItemEvent.UpgradeDropped))),
                new(OpCodes.Starg_S, 1)
            });

            newInstructions.Add(new CodeInstruction(OpCodes.Ret));

            newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

            foreach (CodeInstruction instruction in newInstructions)
            {
                yield return instruction;
            }

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}