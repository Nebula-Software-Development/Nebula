using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp096;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp096;
using PlayerRoles.PlayableScps.Subroutines;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp096;

[HarmonyPatch(typeof(Scp096TargetsTracker), nameof(Scp096TargetsTracker.AddTarget))]
internal class AddingTargetPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnAddTarget(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<AddingTargetPatch>(58, instructions);

        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + 1;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
           new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
           new(OpCodes.Call, PropertyGetter(typeof(ScpStandardSubroutine<Scp096Role>), nameof(ScpStandardSubroutine<Scp096Role>.Owner))),
           new(OpCodes.Ldarg_1),
           new(OpCodes.Ldarg_2),
           new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp096AddingTargetEvent))[0]),
           new(OpCodes.Dup),
           new(OpCodes.Call, Method(typeof(Scp096Handlers), nameof(Scp096Handlers.OnAddingTarget))),
           new(OpCodes.Callvirt, PropertyGetter(typeof(Scp096AddingTargetEvent), nameof(Scp096AddingTargetEvent.IsCancelled))),
           new(OpCodes.Brtrue_S, retLabel)
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
