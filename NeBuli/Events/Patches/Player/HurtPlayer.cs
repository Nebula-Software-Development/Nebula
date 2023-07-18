using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.Player;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerStatsSystem;
using PluginAPI.Events;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.Player;

[HarmonyPatch(typeof(PlayerStats), nameof(PlayerStats.DealDamage))]
public class HurtPlayer
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnHurting(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<HurtPlayer>(142, instructions);
        
        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i =>
            i.opcode == OpCodes.Newobj && i.OperandIs(GetDeclaredConstructors(typeof(PlayerDamageEvent))[0])) - 6;
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldloc_2).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Ldfld, Field(typeof(PlayerStats), nameof(PlayerStats._hub))),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerHurtEventArgs))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnHurt))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerHurtEventArgs), nameof(PlayerHurtEventArgs.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });

        index = newInstructions.FindIndex(i =>
            i.opcode == OpCodes.Newobj && i.OperandIs(GetDeclaredConstructors(typeof(PlayerDyingEvent))[0])) - 6;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldloc_3).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Ldfld, Field(typeof(PlayerStats), nameof(PlayerStats._hub))),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(PlayerDyingEventArgs))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(PlayerHandlers), nameof(PlayerHandlers.OnDying))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(PlayerDyingEventArgs), nameof(PlayerDyingEventArgs.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}
