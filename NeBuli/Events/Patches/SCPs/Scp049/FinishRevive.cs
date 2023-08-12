using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp049;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp049;

[HarmonyPatch(typeof(Scp049ResurrectAbility), nameof(Scp049ResurrectAbility.ServerComplete))]
internal class FinishRevive
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnCompleteRevive(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<FinishRevive>(51, instructions);
        
        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Stloc_0) + 1;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp049ResurrectAbility), nameof(Scp049ResurrectAbility.Owner))),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp049ResurrectAbility), nameof(Scp049ResurrectAbility.CurRagdoll))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp049FinishResurrectEvent))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp049Handlers), nameof(Scp049Handlers.OnFinishResurrect))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp049FinishResurrectEvent), nameof(Scp049FinishResurrectEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}