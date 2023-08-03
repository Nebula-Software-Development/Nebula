using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp173;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp173;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp173;

[HarmonyPatch(typeof(Scp173BreakneckSpeedsAbility), nameof(Scp173BreakneckSpeedsAbility.ServerProcessCmd))]
public class UseBreakneck
{
    [HarmonyTranspiler] 
    private static IEnumerable<CodeInstruction> OnTogglingBreakneck(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<UseBreakneck>(21, instructions);

        Label retLabel = generator.DefineLabel();
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + 1;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp173BreakneckSpeedsAbility), nameof(Scp173BreakneckSpeedsAbility.Owner))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp173ToggleBreakneckSpeed))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp173Handlers), nameof(Scp173Handlers.OnToggleBreakneckSpeed))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp173ToggleBreakneckSpeed), nameof(Scp173ToggleBreakneckSpeed.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });

        index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldc_I4_1) - 1;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp173BreakneckSpeedsAbility), nameof(Scp173BreakneckSpeedsAbility.Owner))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp173ToggleBreakneckSpeed))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp173Handlers), nameof(Scp173Handlers.OnToggleBreakneckSpeed))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp173ToggleBreakneckSpeed), nameof(Scp173ToggleBreakneckSpeed.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}