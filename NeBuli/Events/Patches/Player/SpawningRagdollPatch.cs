using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.Ragdolls;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(RagdollManager), nameof(RagdollManager.ServerSpawnRagdoll))]
internal class SpawningRagdollPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnSpawningRagdoll(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<SpawningRagdollPatch>(45, instructions);

        int index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Ldnull);

        Label retLabel = generator.DefineLabel();

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Ldloc_1),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerSpawningRagdollEvent))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnSpawningRagdoll))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerSpawningRagdollEvent), nameof(PlayerSpawningRagdollEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
