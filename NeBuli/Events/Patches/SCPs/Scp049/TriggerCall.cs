using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp049;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp049;

[HarmonyPatch(typeof(Scp049CallAbility), nameof(Scp049CallAbility.ServerProcessCmd))]
internal class TriggerCall
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnUsingCall(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<TriggerCall>(19, instructions);
        
        Label retLabel = generator.DefineLabel();
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + 1;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp049CallAbility), nameof(Scp049CallAbility.Owner))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp049UseCall))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp049Handlers), nameof(Scp049Handlers.OnCall))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp049UseCall), nameof(Scp049UseCall.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}