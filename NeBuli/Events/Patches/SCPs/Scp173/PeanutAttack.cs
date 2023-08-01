using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp173;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp173;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp173;

[HarmonyPatch(typeof(Scp173SnapAbility), nameof(Scp173SnapAbility.ServerProcessCmd))]
public class PeanutAttack
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnSnap(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PeanutAttack>(137, instructions);

        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Stloc_1) - 3;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp173SnapAbility), nameof(Scp173SnapAbility.Owner))),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Ldfld, Field(typeof(Scp173SnapAbility), nameof(Scp173SnapAbility._targetHub))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp173Attack))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp173Handlers), nameof(Scp173Handlers.OnAttack))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp173Attack), nameof(Scp173Attack.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
