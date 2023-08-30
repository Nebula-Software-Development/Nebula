using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp096;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp096;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp096;

[HarmonyPatch(typeof(Scp096RageManager), nameof(Scp096RageManager.ServerEnrage))]
internal class EnragingPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnEnraging(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<EnragingPatch>(37, instructions);

        Label retLabel = generator.DefineLabel();

        LocalBuilder enraging = generator.DeclareLocal(typeof(Scp096EnragingEvent));

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + 1;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp096RageManager), nameof(Scp096RageManager.Owner))),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp096EnragingEvent))[0]),
            new(OpCodes.Stloc_S, enraging.LocalIndex),
            new(OpCodes.Ldloc_S, enraging.LocalIndex),
            new(OpCodes.Call, Method(typeof(Scp096Handlers), nameof(Scp096Handlers.OnEnraging))),
            new(OpCodes.Ldloc_S, enraging.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp096EnragingEvent), nameof(Scp096EnragingEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
            new(OpCodes.Ldloc_S, enraging.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp096EnragingEvent), nameof(Scp096EnragingEvent.Duration))),
            new(OpCodes.Starg_S, 1),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
