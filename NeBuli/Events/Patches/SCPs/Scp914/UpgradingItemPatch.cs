using HarmonyLib;
using InventorySystem.Items.Pickups;
using Nebuli.Events.EventArguments.SCPs.Scp914;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using Scp914;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp914;

[HarmonyPatch(typeof(Scp914Upgrader), nameof(Scp914Upgrader.ProcessPickup))]
public class UpgradingItemPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnItemUpgrade(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<UpgradingItemPatch>(60, instructions);

        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Starg_S) - 2;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Ldarg_3),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(UpgradingItem))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp914Handlers), nameof(Scp914Handlers.OnUpgradingItem))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(UpgradingItem), nameof(UpgradingItem.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });

        newInstructions.Add(new CodeInstruction(OpCodes.Ret));

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}


