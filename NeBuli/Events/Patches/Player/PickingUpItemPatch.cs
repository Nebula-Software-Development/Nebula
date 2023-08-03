using HarmonyLib;
using InventorySystem.Items.Firearms.Modules;
using InventorySystem.Searching;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player
{
    [HarmonyPatch(typeof(ItemSearchCompletor), nameof(ItemSearchCompletor.Complete))]
    public class PickingUpItemPatch
    {
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> OnPickingupItem(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PickingUpItemPatch>(29, instructions);

            Label retLabel = generator.DefineLabel();

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, Field(typeof(ItemSearchCompletor), nameof(ItemSearchCompletor.Hub))),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Ldfld, Field(typeof(ItemSearchCompletor), nameof(ItemSearchCompletor.TargetPickup))),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerPickingUpItem))[0]),
                new(OpCodes.Dup),
                new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnPickingupItem))),
                new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerPickingUpItem), nameof(PlayerPickingUpItem.IsCancelled))),
                new(OpCodes.Brtrue_S, retLabel),
            });

             newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

            foreach (CodeInstruction instruction in newInstructions)
                yield return instruction;

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}
