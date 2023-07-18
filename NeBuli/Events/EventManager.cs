﻿using System;
using System.Collections.Generic;
using HarmonyLib;
using Nebuli.API.Features;
using Nebuli.API.Features.Player;
using NorthwoodLib.Pools;
using PlayerRoles.Ragdolls;
using UnityEngine.SceneManagement;

namespace Nebuli.Events;

#pragma warning disable CS1591
public static class EventManager
{
    public delegate void CustomEventHandler<in T>(T ev)
        where T : EventArgs;

    public static void CallEvent<T>(this CustomEventHandler<T> eventHandler, T args)
        where T : EventArgs
    {
        if (eventHandler is null)
            return;
        
        foreach (Delegate sub in eventHandler.GetInvocationList())
        {
            try
            {
                sub.DynamicInvoke(args);
            }
            catch (Exception e)
            {
                Log.Error("An error occurred while handling the event " + eventHandler.Method.Name + $"\n{e}");
                throw;
            }
        }
    }

    internal static void RegisterBaseEvents()
    {
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
        RagdollManager.OnRagdollSpawned += OnRagdollSpawned;
    }

    internal static void UnRegisterBaseEvents()
    {
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;
        RagdollManager.OnRagdollSpawned -= OnRagdollSpawned;
    }

    private static void OnRagdollSpawned(BasicRagdoll basicRagdoll)
    {
        if (Ragdoll.Dictionary.ContainsKey(basicRagdoll))
            return;
        
        Ragdoll.Dictionary.Add(basicRagdoll, new Ragdoll(basicRagdoll));
    }

    private static void OnSceneUnLoaded(Scene scene)
    {
        if (scene.name != "Facility")
            return;
        
        NebuliPlayer.Dictionary.Clear();
        Ragdoll.Dictionary.Clear();
    }

    // Method from CursedMod: Allow us to check if the instructions of X Transpiler has changed or not
    public static List<CodeInstruction> CheckPatchInstructions<T>(int originalCodes, IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
        
        if (originalCodes == newInstructions.Count)
            return newInstructions;
        
        Log.Error(typeof(T).FullDescription() + $" has an incorrect number of OpCodes ({originalCodes} != {newInstructions.Count}). The patch may be broken or bugged.");
        return newInstructions;
    }
}
