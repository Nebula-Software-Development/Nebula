using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

//[HarmonyPatch(typeof(PlayerRoles.PlayerRoleManager), nameof(PlayerRoles.PlayerRoleManager.ServerSetRole))]
public class RoleChange
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> RoleChangeMethod(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<VerificationCompleted>(34, instructions);

        int index = newInstructions.FindIndex(instruction =>
        instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand
        == Method(typeof(PlayerRoles.PlayerRoleManager),
        nameof(PlayerRoles.PlayerRoleManager.InitializeNewRole))) + 4;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0),
            new CodeInstruction(OpCodes.Ldarg_1),
            new CodeInstruction(OpCodes.Ldarg_2),
            new CodeInstruction(OpCodes.Ldarg_3),
            new CodeInstruction(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerRoleChangeEventArgs))[0]),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}