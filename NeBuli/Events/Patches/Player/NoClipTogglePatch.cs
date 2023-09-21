using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.NetworkMessages;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(FpcNoclipToggleMessage), nameof(FpcNoclipToggleMessage.ProcessMessage))]
internal class NoClipTogglePatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnTogglingNoClip(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<NoClipTogglePatch>(28, instructions);

        Label retLabel = generator.DefineLabel();
        LocalBuilder @event = generator.DeclareLocal(typeof(PlayerTogglingNoClipEvent));
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Ldloc_0);
        Label newLabel = newInstructions[index].ExtractLabels()[0];

        newInstructions.RemoveRange(index, 4);

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldloc_0).WithLabels(newLabel),
            new(OpCodes.Ldloc_0),
            new(OpCodes.Call, Method(typeof(NoClipTogglePatch), nameof(GetReverseStatus))),
            new(OpCodes.Ldloc_0),
            new(OpCodes.Call, Method(typeof(FpcNoclip), nameof(FpcNoclip.IsPermitted))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerTogglingNoClipEvent))[0]),
            new(OpCodes.Stloc_S, @event.LocalIndex),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnPlayerTogglingNoClip))),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerTogglingNoClipEvent), nameof(PlayerTogglingNoClipEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static bool GetReverseStatus(ReferenceHub player) => player.roleManager.CurrentRole is IFpcRole fpc && !fpc.FpcModule.Noclip.IsActive;
}