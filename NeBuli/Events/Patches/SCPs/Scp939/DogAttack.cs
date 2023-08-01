using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp939;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs;

[HarmonyPatch(typeof(Scp939ClawAbility), nameof(Scp939ClawAbility.OnDestructibleDamaged))]
public class DogAttack
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnClawing(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<DogAttack>(11, instructions);

        Label retLabel = generator.DefineLabel();
        
        newInstructions.InsertRange(0, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp939ClawAbility), nameof(Scp939ClawAbility.Owner))),
            new(OpCodes.Ldarg_1),
            new(OpCodes.Callvirt, PropertyGetter(typeof(IDestructible), nameof(IDestructible.NetworkId))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp939Attack))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp939Handlers), nameof(Scp939Handlers.OnAttack))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp939Attack), nameof(Scp939Attack.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    } 
}
