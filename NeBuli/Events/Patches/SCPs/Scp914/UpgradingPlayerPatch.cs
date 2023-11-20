// -----------------------------------------------------------------------
// <copyright file=UpgradingPlayerPatch.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp914;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using Scp914;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp914;

[HarmonyPatch(typeof(Scp914Upgrader), nameof(Scp914Upgrader.ProcessPlayer))]
internal class UpgradingPlayerPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnPlayerUpgrade(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<UpgradingPlayerPatch>(155, instructions);

        Label retLabel = generator.DefineLabel();

        LocalBuilder @event = generator.DeclareLocal(typeof(UpgradingPlayerEvent));

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Stloc_0) + 1;

        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Ldarg_2),
            new(OpCodes.Ldarg_S, 4),
            new(OpCodes.Ldarg_3),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(UpgradingPlayerEvent))[0]),
            new(OpCodes.Stloc_S, @event.LocalIndex),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Call, Method(typeof(Scp914Handlers), nameof(Scp914Handlers.OnUpgradingPlayer))),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(UpgradingPlayerEvent), nameof(UpgradingPlayerEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(UpgradingPlayerEvent), nameof(UpgradingPlayerEvent.KnobSetting))),
            new(OpCodes.Starg_S, 4),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(UpgradingPlayerEvent), nameof(UpgradingPlayerEvent.UpgradeInventory))),
            new(OpCodes.Starg_S, 1),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(UpgradingPlayerEvent), nameof(UpgradingPlayerEvent.HeldOnly))),
            new(OpCodes.Starg_S, 2),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(UpgradingPlayerEvent), nameof(UpgradingPlayerEvent.OutputPosition))),
            new(OpCodes.Stloc_0)
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}