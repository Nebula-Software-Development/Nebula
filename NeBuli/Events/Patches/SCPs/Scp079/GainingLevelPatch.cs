using HarmonyLib;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp079;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp079;

//[HarmonyPatch(typeof(Scp079TierManager), nameof(Scp079TierManager.AccessTierIndex), MethodType.Setter)]
internal class GainingLevelPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnGainingLevel(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<GainingLevelPatch>(1, instructions);

        int index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Ldarg_0) - 4;

        Label retLabel = generator.DefineLabel();

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
           new(OpCodes.Ldarg_0),
           new(OpCodes.Callvirt, PropertyGetter(typeof(Scp079TierManager), nameof(Scp079TierManager.Owner))),

        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
