using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.API.Features.Player;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(TeslaGateController), nameof(TeslaGateController.FixedUpdate))]
internal class TriggeringTesla
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnTriggerTesla(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<TriggeringTesla>(101, instructions);

        int index = newInstructions.FindIndex(instruction => instruction.Calls(PropertyGetter(typeof(ReferenceHub), nameof(ReferenceHub.AllHubs))));
        
        newInstructions.RemoveRange(index, newInstructions.FindIndex(i => i.opcode == OpCodes.Endfinally) + 1 - index);

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new(OpCodes.Ldloc_1),
            new(OpCodes.Ldloca_S, 2),
            new(OpCodes.Ldloca_S, 3),
            new(OpCodes.Call, Method(typeof(TriggeringTesla), nameof(ProcessEvent), new[] { typeof(TeslaGate), typeof(bool).MakeByRefType(), typeof(bool).MakeByRefType() })),
        });
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static void ProcessEvent(TeslaGate teslaGate, ref bool inIdleRange, ref bool isTriggerable)
    {
        foreach (NebuliPlayer player in NebuliPlayer.List.Where(ply => teslaGate.IsInIdleRange(ply.ReferenceHub)))
        {
            PlayerTriggeringTeslaEvent args = new(player.ReferenceHub, teslaGate, inIdleRange, isTriggerable);
            PlayerHandlers.OnTriggerTesla(args);

            if (args.IsCancelled)
            {
                isTriggerable = false;
                inIdleRange = false;
                break;
            }

            isTriggerable = args.IsTriggerable;
            inIdleRange = args.IsInIdleRange;
        }
    }

}