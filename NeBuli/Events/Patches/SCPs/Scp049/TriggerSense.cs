using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp049;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp049;

[HarmonyPatch(typeof(Scp049SenseAbility), nameof(Scp049SenseAbility.ServerProcessCmd))]
internal class TriggerSense
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnUsingSense(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<TriggerSense>(76, instructions);
        
        Label retLabel = generator.DefineLabel();
        LocalBuilder @event = generator.DeclareLocal(typeof(Scp049UseSenseEvent));
        
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldc_I4_0) - 1;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp049SenseAbility), nameof(Scp049SenseAbility.Owner))),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp049SenseAbility), nameof(Scp049SenseAbility.Target))),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Ldfld, Field(typeof(Scp049SenseAbility), nameof(Scp049SenseAbility._distanceThreshold))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp049UseSenseEvent))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp049Handlers), nameof(Scp049Handlers.OnSense))),
            new(OpCodes.Stloc_S, @event.LocalIndex),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp049UseSenseEvent), nameof(Scp049UseSenseEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp049UseSenseEvent), nameof(Scp049UseSenseEvent.Distance))),
            new(OpCodes.Stfld, Field(typeof(Scp049SenseAbility), nameof(Scp049SenseAbility._distanceThreshold))),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}