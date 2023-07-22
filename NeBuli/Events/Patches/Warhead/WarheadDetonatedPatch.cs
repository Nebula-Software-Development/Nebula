using HarmonyLib;
using Nebuli.Events.EventArguments.Server;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Warhead;

[HarmonyPatch(typeof(AlphaWarheadController), nameof(AlphaWarheadController.Detonate))]
public class WarheadDetonatedPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnDetonated(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<WarheadDetonatedPatch>(184, instructions);

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(WarheadDetonatedEventArgs))[0]),
            new(OpCodes.Call, Method(typeof(ServerHandler), nameof(ServerHandler.OnWarheadDetonated))),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}