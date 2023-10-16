using HarmonyLib;
using Nebuli.Events.EventArguments.Server;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Warhead;

[HarmonyPatch(typeof(AlphaWarheadController), nameof(AlphaWarheadController.StartDetonation))]
internal class WarheadStartingPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnStarting(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<WarheadStartingPatch>(111, instructions);

        Label retLabel = generator.DefineLabel();
        LocalBuilder @event = generator.DeclareLocal(typeof(WarheadStartingEvent));

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + 1;

        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_1).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Ldarg_2),
            new(OpCodes.Ldarg_3),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(WarheadStartingEvent))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(ServerHandlers), nameof(ServerHandlers.OnWarheadStarting))),
            new(OpCodes.Stloc_S, @event.LocalIndex),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(WarheadStartingEvent), nameof(WarheadStartingEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(WarheadStartingEvent), nameof(WarheadStartingEvent.IsAutomatic))),
            new(OpCodes.Starg_S, 1),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(WarheadStartingEvent), nameof(WarheadStartingEvent.SuppressSubtitles))),
            new(OpCodes.Starg_S, 2),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
