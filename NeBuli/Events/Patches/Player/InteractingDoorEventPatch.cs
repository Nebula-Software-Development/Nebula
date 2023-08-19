using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using Nebuli.API.Features.Doors;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(DoorVariant), nameof(DoorVariant.ServerInteract), typeof(ReferenceHub), typeof(byte))]
internal class InteractingDoorEventPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnDoorInteract(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<InteractingDoorEventPatch>(111, instructions);

        Label retLabel = generator.DefineLabel();

        LocalBuilder interactingDoor = generator.DeclareLocal(typeof(PlayerInteractingDoorEvent));

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Call, Method(typeof(Door), nameof(Door.CanChangeState))),
            new(OpCodes.Brfalse_S, retLabel),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerInteractingDoorEvent))[0]),
            new(OpCodes.Stloc_S, interactingDoor.LocalIndex),
            new(OpCodes.Ldloc_S, interactingDoor.LocalIndex),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnPlayerInteractingDoor))),
            new(OpCodes.Ldloc_S, interactingDoor.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerInteractingDoorEvent), nameof(PlayerInteractingDoorEvent.IsCancelled))),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Ldarg_2),
            new(OpCodes.Ldloc_S, interactingDoor.LocalIndex),
            new(OpCodes.Call, Method(typeof(InteractingDoorEventPatch), nameof(TriggerDeniedAction))),
            new(OpCodes.Brtrue, retLabel),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);

    }
    private static void TriggerDeniedAction(DoorVariant door, ReferenceHub player, byte id, PlayerInteractingDoorEvent ev)
    {
        if (!ev.IsCancelled)
            return;
        door.PermissionsDenied(player, id);
        DoorEvents.TriggerAction(door, DoorAction.AccessDenied, player);
    }
}
