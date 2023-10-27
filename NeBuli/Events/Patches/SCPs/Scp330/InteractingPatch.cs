using HarmonyLib;
using Interactables.Interobjects;
using InventorySystem;
using InventorySystem.Items.Usables.Scp330;
using Nebuli.Events.EventArguments.SCPs.Scp330;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp330;

[HarmonyPatch(typeof(Scp330Interobject), nameof(Scp330Interobject.ServerInteract))]
internal class InteractingPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnInteracting(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<InteractingPatch>(97, instructions);

        int index = newInstructions.FindLastIndex(i => i.Calls(typeof(Scp330Bag).GetMethod(nameof(Scp330Bag.ServerProcessPickup)))) - 2;

        newInstructions.RemoveRange(index, 3);

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
           new(OpCodes.Ldloc_2),
           new(OpCodes.Ldloca_S, 3),
           new(OpCodes.Ldloca_S, 2),
           new(OpCodes.Call, Method(typeof(InteractingPatch), nameof(ServerProcessPickup)))
        });

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static bool ServerProcessPickup(ReferenceHub player, int uses, out Scp330Bag bag, out int totaluses)
    {
        Scp330Pickup pickup = null;
        bag = null;
        totaluses = uses;

        Scp330InteractEvent @event = new(player, uses, Scp330Candies.GetRandom());
        Scp330Handlers.OnInteracting(@event);

        if (@event.IsCancelled)
            return false;

        totaluses = @event.Uses;

        if (!Scp330Bag.TryGetBag(player, out bag))
        {
            int num = pickup?.Info.Serial ?? 0;
            Scp330Bag scp330 = (Scp330Bag)player.inventory.ServerAddItem(ItemType.SCP330, (ushort)num, pickup);
            scp330.Candies.Clear();
            scp330.TryAddSpecific(@event.Candy);
            bag = scp330;
            return scp330 != null;
        }

        bool result;

        if (pickup == null)
            result = bag.TryAddSpecific(@event.Candy);
        else
        {
            Scp330Bag localBag = bag;
            result = pickup.StoredCandies.RemoveAll(candy => localBag.TryAddSpecific(candy)) > 0;
        }

        if (bag.AcquisitionAlreadyReceived)
            bag.ServerRefreshBag();

        return result;
    }
}