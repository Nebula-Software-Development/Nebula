using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp939;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp939;

[HarmonyPatch(typeof(Scp939FocusAbility), nameof(Scp939FocusAbility.TargetState), MethodType.Setter)]
internal class ToggleFocus
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnTogglingFocus(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<ToggleFocus>(37, instructions);
        
        Label retLabel = generator.DefineLabel();
        LocalBuilder @event = generator.DeclareLocal(typeof(Scp939ToggleFocusEvent));
        
        int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Ret) + 1;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp939FocusAbility), nameof(Scp939FocusAbility.Owner))),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp939ToggleFocusEvent))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp939Handlers), nameof(Scp939Handlers.OnToggleFocus))),
            new(OpCodes.Stloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp939ToggleFocusEvent), nameof(Scp939ToggleFocusEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp939ToggleFocusEvent), nameof(Scp939ToggleFocusEvent.State))),
            new (OpCodes.Ldarg_1),
            new (OpCodes.Ceq),
            new (OpCodes.Brfalse_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}