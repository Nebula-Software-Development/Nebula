using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp0492;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp049;
using PlayerRoles.PlayableScps.Scp049.Zombies;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs.Scp0492;

[HarmonyPatch(typeof(RagdollAbilityBase<ZombieRole>), nameof(RagdollAbilityBase<ZombieRole>.ServerProcessCmd))]
public class ConsumeCorpse
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnConsumingCorpse(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<ConsumeCorpse>(94, instructions);
        
        Label retLabel = generator.DefineLabel();
        int index = newInstructions.FindLastIndex(i => i.opcode == OpCodes.Ldarg_0);
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Callvirt, PropertyGetter(typeof(RagdollAbilityBase<ZombieRole>), nameof(RagdollAbilityBase<ZombieRole>.Owner))),
            new(OpCodes.Ldarg_0),
            new(OpCodes.Callvirt, PropertyGetter(typeof(RagdollAbilityBase<ZombieRole>), nameof(RagdollAbilityBase<ZombieRole>.CurRagdoll))),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp0492ConsumeCorpse))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp0492Handlers), nameof(Scp0492Handlers.OnConsumeCorpse))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp0492ConsumeCorpse), nameof(Scp0492ConsumeCorpse.IsCancelled))),
            new(OpCodes.Brtrue_S, retLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(retLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}