using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(CustomNetworkManager), nameof(CustomNetworkManager.OnServerDisconnect))]
internal class LeavingServer
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnDisconnecting(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<LeavingServer>(45, instructions);

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_1),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerLeaveEvent))[0]),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnLeave))),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}