using HarmonyLib;
using InventorySystem.Items.Firearms.Modules;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.EventArguments.SCPs.Scp173;
using Nebuli.Events.EventArguments.SCPs.Scp914;
using Nebuli.Events.EventArguments.SCPs.Scp939;
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

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Starg_S) + 1;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Ldarg_2), 
            new(OpCodes.Ldarg_S, 4), 
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(UpgradingPlayerEvent))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp914Handlers), nameof(Scp914Handlers.OnUpgradingPlayer))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(UpgradingPlayerEvent), nameof(UpgradingPlayerEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
