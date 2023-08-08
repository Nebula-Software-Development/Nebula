using HarmonyLib;
using InventorySystem.Searching;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

//[HarmonyPatch(typeof(ItemSearchCompletor), nameof(AmmoSearchCompletor.Complete))]
internal class PickingUpAmmoPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnPickingupAmmo(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PickingUpAmmoPatch>(37, instructions);

        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldloc_1) + 1;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Ldfld, Field(typeof(ItemSearchCompletor), nameof(ItemSearchCompletor.Hub))),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerPickingUpAmmo))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnPickingUpAmmo))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerPickingUpAmmo), nameof(PlayerPickingUpAmmo.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
