using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp049;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049;
using System.Collections.Generic;
using System.Reflection.Emit;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp049;

[HarmonyPatch(typeof(Scp049ResurrectAbility), nameof(Scp049ResurrectAbility.ServerValidateBegin))]
internal class StartRevive
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnStartRevive(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<StartRevive>(16, instructions);

        Label retLabel = generator.DefineLabel();

        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp049ResurrectAbility), nameof(Scp049ResurrectAbility.Owner))),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp049StartResurrectEvent))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp049Handlers), nameof(Scp049Handlers.OnStartResurrect))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp049StartResurrectEvent), nameof(Scp049StartResurrectEvent.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
        });

        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);

        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;

        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}