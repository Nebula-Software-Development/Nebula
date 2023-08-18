using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(PocketDimensionTeleport), nameof(PocketDimensionTeleport.OnTriggerEnter))]
internal class PlayerEscapePocketDimensionPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnEscaping(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PlayerEscapePocketDimensionPatch>(102, instructions);

        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldloc_2);

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
           new(OpCodes.Ldloc_1),
           new(OpCodes.Ldc_I4_1),
           new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerEscapingPocketEvent))[0]),
           new(OpCodes.Dup),
           new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnEscapingPocket))),
           new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerEscapingPocketEvent), nameof(PlayerEscapingPocketEvent.IsCancelled))),
           new(OpCodes.Brtrue_S, retLabel),
        });

        index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldc_I4_0) - 1;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
           new(OpCodes.Ldloc_1),
           new(OpCodes.Ldc_I4_0),
           new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerEscapingPocketEvent))[0]),
           new(OpCodes.Dup),
           new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnEscapingPocket))),
           new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerEscapingPocketEvent), nameof(PlayerEscapingPocketEvent.IsCancelled))),
           new(OpCodes.Brtrue_S, retLabel),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
