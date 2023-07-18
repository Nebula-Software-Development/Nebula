using CommandSystem;
using Footprinting;
using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(BanPlayer), nameof(BanPlayer.BanUser), typeof(Footprint), typeof(ICommandSender), typeof(string), typeof(long))]
public class BannedPlayer
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnBanning(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<BannedPlayer>(133, instructions);

        Label retLabel = generator.DefineLabel();

        LocalBuilder bannedArgs = generator.DeclareLocal(typeof(PlayerBannedEventArgs));

        int index = newInstructions.FindIndex(x => x.opcode == OpCodes.Ldc_I4_0) + 2;

        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Ldarg_2),
            new(OpCodes.Ldarg_3),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerBannedEventArgs))[0]),
            new(OpCodes.Stloc_S, bannedArgs.LocalIndex),
            new(OpCodes.Ldloc_S, bannedArgs.LocalIndex),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnBanned))),
            new(OpCodes.Ldloc_S, bannedArgs.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerBannedEventArgs), nameof(PlayerBannedEventArgs.IsCancelled))),
            new(OpCodes.Brfalse_S, retLabel),
            new(OpCodes.Ldloc_S, bannedArgs.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerBannedEventArgs), nameof(PlayerBannedEventArgs.Reason))),
            new(OpCodes.Starg_S, 2),
            new(OpCodes.Ldloc_S, bannedArgs.LocalIndex),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerBannedEventArgs), nameof(PlayerBannedEventArgs.Duration))),
            new(OpCodes.Starg_S, 3),
        });

        newInstructions[newInstructions.Count - 1].WithLabels(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}