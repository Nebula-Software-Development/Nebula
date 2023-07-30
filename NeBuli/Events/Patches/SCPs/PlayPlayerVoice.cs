using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using Nebuli.Events.EventArguments.SCPs.Scp939;
using Nebuli.Events.Handlers;
using NorthwoodLib.Pools;
using PlayerRoles.PlayableScps.Scp939.Mimicry;
using Utils.Networking;
using static HarmonyLib.AccessTools;

namespace Nebuli.Events.Patches.SCPs;

[HarmonyPatch(typeof(MimicryRecorder), nameof(MimicryRecorder.ServerProcessCmd))]
public class PlayPlayerVoice
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnPlayVoice(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = EventManager.CheckPatchInstructions<PlayPlayerVoice>(29, instructions);
        
        LocalBuilder playerVoice = generator.DeclareLocal(typeof(ReferenceHub));
        Label returnLabel = generator.DefineLabel();
        
        int index = newInstructions.FindIndex(i => i.Calls(Method(typeof(ReferenceHubReaderWriter), nameof(ReferenceHubReaderWriter.ReadReferenceHub)))) + 1;
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new(OpCodes.Dup),
            new(OpCodes.Stloc_S, playerVoice.LocalIndex),
        });
        
        index = newInstructions.FindLastIndex(x => x.IsLdarg(0));
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new CodeInstruction(OpCodes.Ldarg_0).MoveLabelsFrom(newInstructions[index]),
            new(OpCodes.Callvirt, PropertyGetter(typeof(MimicryRecorder), nameof(MimicryRecorder.Owner))),
            new(OpCodes.Ldloc_S, playerVoice.LocalIndex),
            new(OpCodes.Newobj, GetDeclaredConstructors(typeof(Scp939PlayVoice))[0]),
            new(OpCodes.Dup),
            new(OpCodes.Call, Method(typeof(Scp939Handlers), nameof(Scp939Handlers.OnPlayVoice))),
            new(OpCodes.Callvirt, PropertyGetter(typeof(Scp939PlayVoice), nameof(Scp939PlayVoice.IsCancelled))),
            new(OpCodes.Brtrue_S, returnLabel),
        });
        
        newInstructions[newInstructions.Count - 1].labels.Add(returnLabel);
        
        foreach (CodeInstruction instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }
}