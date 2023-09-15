using HarmonyLib;
using InventorySystem;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(Inventory), nameof(Inventory.CurInstance), MethodType.Setter)]
internal class ChangedItemPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnChanged(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<ChangedItemPatch>(39, instructions);

        newInstructions.InsertRange(newInstructions.Count - 1, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[newInstructions.Count - 1]),
            new(OpCodes.Ldfld, Field(typeof(Inventory), nameof(Inventory._hub))),
            new(OpCodes.Ldloc_0),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerChangedItemEvent))[0]),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnChangedItem))),
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
