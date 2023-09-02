using HarmonyLib;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Nebuli.Events.Patches.Game;

[HarmonyPatch(typeof(Escape), nameof(Escape.ServerGetScenario))]
internal class GetScenario
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<GetScenario>(60, instructions);

        int ldcI4_0Count = 0;

        foreach (CodeInstruction instruction in newInstructions)
        {
            if (instruction.opcode == OpCodes.Ldc_I4_0)
            {
                ldcI4_0Count++;
                if (ldcI4_0Count > 3)
                {
                    instruction.opcode = OpCodes.Ldc_I4_5;
                }
            }
        }

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
