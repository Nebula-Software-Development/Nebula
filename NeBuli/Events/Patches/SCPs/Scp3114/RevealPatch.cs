using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp3114;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp3114;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp3114;

[HarmonyPatch(typeof(Scp3114Reveal), nameof(Scp3114Reveal.ServerProcessCmd))]
internal class RevealPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnReveal(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<RevealPatch>(8, instructions);

        Label retLabel = generator.DefineLabel();

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp3114Reveal), nameof(Scp3114Reveal.Owner))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp3114RevealEvent))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp3114Handlers), nameof(Scp3114Handlers.OnReveal))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp3114RevealEvent), nameof(Scp3114RevealEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });

        newInstructions.InsertRange(newInstructions.Count - 1, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp3114Reveal), nameof(Scp3114Reveal.Owner))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp3114RevealedEvent))[0]),
            new(OpCodes.Call, Method(typeof(Scp3114Handlers), nameof(Scp3114Handlers.OnRevealed))),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}

[HarmonyPatch(typeof(Scp3114Identity), nameof(Scp3114Identity.Update))]
internal class RevealUpdatePatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnUpdateReveal(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<RevealUpdatePatch>(21, instructions);

        Label retLabel = generator.DefineLabel();
        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Brfalse_S) + 1;

        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp3114Identity), nameof(Scp3114Identity.Owner))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp3114RevealEvent))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp3114Handlers), nameof(Scp3114Handlers.OnReveal))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp3114RevealEvent), nameof(Scp3114RevealEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });

        newInstructions.InsertRange(newInstructions.Count - 1, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp3114Identity), nameof(Scp3114Identity.Owner))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp3114RevealedEvent))[0]),
            new(OpCodes.Call, Method(typeof(Scp3114Handlers), nameof(Scp3114Handlers.OnRevealed))),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}