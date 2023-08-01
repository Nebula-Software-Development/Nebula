using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp173;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp173;
using PlayerRoles.PlayableScps.Subroutines;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs;

[HarmonyPatch(typeof(Scp173TantrumAbility), nameof(Scp173TantrumAbility.ServerProcessCmd))]
public class PlaceTantrum
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnPlacingTantrum(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PlaceTantrum>(80, instructions);

        Label retLabel = generator.DefineLabel();
        
        int index = newInstructions.FindIndex(i => i.Calls(Method(typeof(AbilityCooldown), nameof(AbilityCooldown.Trigger), new[] {typeof(float)}))) - 3;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp173TantrumAbility), nameof(Scp173TantrumAbility.Owner))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp173PlaceTantrum))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp173Handlers), nameof(Scp173Handlers.OnPlaceTantrum))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp173PlaceTantrum), nameof(Scp173PlaceTantrum.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}