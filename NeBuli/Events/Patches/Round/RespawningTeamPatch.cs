using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.EventArguments.Round;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using Respawning;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Round;

[HarmonyPatch(typeof(RespawnManager), nameof(RespawnManager.Spawn))]
internal class RespawningTeamPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnRespawning(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<RespawningTeamPatch>(299, instructions);

        Label retLabel = generator.DefineLabel();

        LocalBuilder respawnArgs = generator.DeclareLocal(typeof(RespawningTeamEvent));

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldloc_3);

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldloc_1).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Ldfld, Field(typeof(RespawnManager), nameof(RespawnManager.NextKnownTeam))),
            new(OpCodes.Ldloc_2),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(RespawningTeamEvent))[0]),
            new(OpCodes.Stloc_S, respawnArgs.LocalIndex),
            new(OpCodes.Ldloc_S, respawnArgs.LocalIndex),
            new(OpCodes.Call, Method(typeof(RoundHandlers), nameof(RoundHandlers.OnRespawning))),
            new(OpCodes.Ldloc_S, respawnArgs.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(RespawningTeamEvent), nameof(RespawningTeamEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
            new(OpCodes.Ldloc_S, respawnArgs.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(RespawningTeamEvent), nameof(RespawningTeamEvent.PlayersRespawning))),
            new(OpCodes.Stloc_1),          
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
