using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(ServerRoles), nameof(ServerRoles.SetGroup))]
internal class ChangingUserGroupPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnChanging(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<ChangingUserGroupPatch>(448, instructions);

        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Ret) + 1;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Ldfld, Field(typeof(ServerRoles), nameof(ServerRoles._hub))),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerChangingUserGroupEvent))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnChangingUserGroup))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerChangingUserGroupEvent), nameof(PlayerChangingUserGroupEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}