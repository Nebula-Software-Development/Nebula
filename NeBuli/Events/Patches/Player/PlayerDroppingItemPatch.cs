using HarmonyLib;
using InventorySystem;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(Inventory), nameof(Inventory.UserCode_CmdDropItem__UInt16__Boolean))]
internal class PlayerDroppingItemPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnDrop(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PlayerDroppingItemPatch>(155, instructions);

        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ret) + 1;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
           new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
           new(OpCodes.Ldfld, Field(typeof(Inventory), nameof(Inventory._hub))),
           new(OpCodes.Ldloc_0),
           new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerDroppingItemEvent))[0]),
           new(OpCodes.Dup),
           new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnDroppingItem))),
           new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerDroppingItemEvent), nameof(PlayerDroppingItemEvent.IsCancelled))),
           new(OpCodes.Brtrue_S, retLabel),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
