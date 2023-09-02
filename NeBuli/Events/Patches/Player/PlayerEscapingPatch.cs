using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;
using Label = System.Reflection.Emit.Label;
using OpCodes = System.Reflection.Emit.OpCodes;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(Escape), nameof(Escape.ServerHandlePlayer))]
internal class PlayerEscapingPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnEscape(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PlayerEscapingPatch>(61, instructions);

        Label retLabel = generator.DefineLabel();
        LocalBuilder @event = generator.DeclareLocal(typeof(PlayerEscapingEvent));

        int index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Conv_U1) - 6;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Ldloc_0),
            new(OpCodes.Ldloc_1),
            new(OpCodes.Ldloc_2),
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
            new(OpCodes.Ldloc, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerEscapingEvent), nameof(PlayerEscapingEvent.EscapeMessage))),
            new(OpCodes.Stloc_2),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}