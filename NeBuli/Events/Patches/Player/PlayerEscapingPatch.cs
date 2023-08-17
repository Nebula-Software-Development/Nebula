using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(Escape), nameof(Escape.ServerHandlePlayer))]
internal class PlayerEscapingPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnEscape(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PlayerEscapingPatch>(71, instructions);

        Label retLabel = generator.DefineLabel();
        LocalBuilder @event = generator.DeclareLocal(typeof(PlayerEscapingEvent));

        int index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Newobj) - 2;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Ldloc_0),
            new(OpCodes.Ldloc_1),
            new(OpCodes.Ldloc_3),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerEscapingEvent))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnEscaping))),
            new(OpCodes.Stloc, @event.LocalIndex),
            new(OpCodes.Ldloc, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerEscapingEvent), nameof(PlayerEscapingEvent.IsCancelled))),
            new(OpCodes.Brtrue, retLabel),
            new(OpCodes.Ldloc, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerEscapingEvent), nameof(PlayerEscapingEvent.NewRole))),
            new(OpCodes.Stloc_0),             
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}

[HarmonyPatch(typeof(Escape), nameof(Escape.ServerGetScenario))]
internal static class GetScenario
{
    private static IEnumerable<CodeInstruction> ChangeScenario(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PlayerEscapingPatch>(60, instructions);

        int indexToReplace = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Ldloc_0) - 2;
        if (indexToReplace >= 0 && indexToReplace < newInstructions.Count)
        {
            newInstructions[indexToReplace].opcode = OpCodes.Ldc_I4_5;
        }

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
