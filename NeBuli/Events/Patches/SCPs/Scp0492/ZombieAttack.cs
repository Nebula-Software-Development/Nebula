using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp0492;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps;
using PlayerRoles.PlayableScps.Scp049.Zombies;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp0492;

[HarmonyPatch(typeof(ScpAttackAbilityBase<ZombieRole>), nameof(ScpAttackAbilityBase<ZombieRole>.ServerProcessCmd))]
public class ZombieAttack
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnZombieAttack(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<ZombieAttack>(107, instructions);

        Label retLabel = generator.DefineLabel();

        int index = newInstructions.FindIndex(i => i.Calls(Method(typeof(ScpAttackAbilityBase<ZombieRole>), nameof(ScpAttackAbilityBase<ZombieRole>.ServerPerformAttack)))) - 1;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Callvirt, PropertyGetter(typeof(ScpAttackAbilityBase<ZombieRole>), nameof(ScpAttackAbilityBase<ZombieRole>.Owner))),
            new(OpCodes.Ldloc_S, 4),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp0492Attack))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp0492Handlers), nameof(Scp0492Handlers.OnAttack))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp0492Attack), nameof(Scp0492Attack.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel)
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}