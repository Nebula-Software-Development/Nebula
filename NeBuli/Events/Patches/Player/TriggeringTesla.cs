using HarmonyLib;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

//[HarmonyPatch(typeof(TeslaGateController), nameof(TeslaGateController.FixedUpdate))]
public class TriggeringTesla
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnTrigger(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<TriggeringTesla>(101, instructions);

        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Ret);

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldloc_1),
            new CodeInstruction(OpCodes.Ldloca_S, 2),
            new CodeInstruction(OpCodes.Ldloca_S, 3),
            new CodeInstruction(OpCodes.Ldloca_S, 5),
            new CodeInstruction(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnTriggerTesla))),
            new CodeInstruction(OpCodes.Pop),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}