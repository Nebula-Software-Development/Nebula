using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(NicknameSync), nameof(NicknameSync.Network_displayName), MethodType.Setter)]
internal class ChangingNicknamePatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnChangingNickname(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<ChangingNicknamePatch>(11, instructions);

        Label retLabel = generator.DefineLabel();

        LocalBuilder @event = generator.DeclareLocal(typeof(PlayerChangingNicknameEvent));

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Ldfld, Field(typeof(NicknameSync), nameof(NicknameSync._hub))),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerChangingNicknameEvent))[0]),
            new(OpCodes.Stloc_S, @event.LocalIndex),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnChangingNickname))),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerChangingNicknameEvent), nameof(PlayerChangingNicknameEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
            new(OpCodes.Ldloc_S, @event.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerChangingNicknameEvent), nameof(PlayerChangingNicknameEvent.NewName))),
            new(OpCodes.Starg_S, 1),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
