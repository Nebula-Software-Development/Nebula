using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using MapGeneration;
using MapGeneration.Distributors;
using Nebuli.API.Features;
using Nebuli.API.Features.Item;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Map;
using Nebuli.API.Features.Player;
using Nebuli.API.Interfaces;
using NorthwoodLib.Pools;
using PlayerRoles;
using PlayerRoles.Ragdolls;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Nebuli.Events;

#pragma warning disable CS1591

public static class EventManager
{
    public delegate void CustomEventHandler<in T>(T ev)
            where T : EventArgs;

    public static void CallEvent<T>(this CustomEventHandler<T> eventHandler, T args) where T : EventArgs
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
                Log.Error("An error occurred while handling the event " + eventHandler.Method.Name + $"\n{e}", "EVENT ERROR");
            }
        }
    }

    internal static void RegisterBaseEvents()
    {
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
        RagdollManager.OnRagdollSpawned += OnRagdollSpawned;
        RagdollManager.OnRagdollRemoved += OnRagdollDeSpawned;
        SeedSynchronizer.OnMapGenerated += OnMapGenerated;
        ItemPickupBase.OnPickupAdded += OnPickupAdded;
        ItemPickupBase.OnPickupDestroyed += OnPickupRemoved;
        InventorySystem.InventoryExtensions.OnItemAdded += OnItemAdded;
        InventorySystem.InventoryExtensions.OnItemRemoved += OnItemRemoved;
        CharacterClassManager.OnRoundStarted += Handlers.ServerHandler.OnRoundStart;
        PlayerRoleManager.OnRoleChanged += RoleChange;
    }

    internal static void UnRegisterBaseEvents()
    {
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;
        RagdollManager.OnRagdollSpawned -= OnRagdollSpawned;
        RagdollManager.OnRagdollRemoved -= OnRagdollDeSpawned;
        SeedSynchronizer.OnMapGenerated -= OnMapGenerated;
        ItemPickupBase.OnPickupAdded -= OnPickupAdded;
        ItemPickupBase.OnPickupDestroyed -= OnPickupRemoved;
        InventorySystem.InventoryExtensions.OnItemAdded -= OnItemAdded;
        InventorySystem.InventoryExtensions.OnItemRemoved -= OnItemRemoved;
        PlayerRoleManager.OnRoleChanged -= RoleChange;
    }

    private static void RoleChange(ReferenceHub userHub, PlayerRoleBase prevRole, PlayerRoleBase newRole)
    {
        API.Features.Roles.Role newrole = API.Features.Roles.Role.CreateNew(newRole);
    }

    private static void OnRagdollSpawned(BasicRagdoll basicRagdoll)
    {
        if (Ragdoll.Dictionary.ContainsKey(basicRagdoll))
            return;

        Ragdoll.Dictionary.Add(basicRagdoll, new Ragdoll(basicRagdoll));
    }

    private static void OnRagdollDeSpawned(BasicRagdoll basicRagdoll)
    {
        if (Ragdoll.Dictionary.ContainsKey(basicRagdoll))
            Ragdoll.Dictionary.Remove(basicRagdoll);
    }

    private static void OnSceneUnLoaded(Scene scene)
    {
        if (scene.name != "Facility")
            return;
        Loader.Loader.EDisablePlugins();
        NebuliPlayer.Dictionary.Clear();
        Ragdoll.Dictionary.Clear();
        Generator.Dictionary.Clear();
        Room.Dictionary.Clear();
        Door.Dictionary.Clear();
        Pickup.Dictionary.Clear();
        Item.Dictionary.Clear();
    }

    private static void OnMapGenerated()
    {
        foreach (RoomIdentifier room in RoomIdentifier.AllRoomIdentifiers)
            Room.Get(room);
        foreach (Scp079Generator gen in Object.FindObjectsOfType<Scp079Generator>())
            Generator.Get(gen);
        foreach (DoorVariant door in Object.FindObjectsOfType<DoorVariant>())
            Door.Get(door);
        NebuliPlayer nebuliHost = new(ReferenceHub.HostHub);
        Server.NebuliHost = nebuliHost;
    }

    private static void OnPickupAdded(ItemPickupBase itemPickupBase)
    {
        Pickup.PickupGet(itemPickupBase);
    }

    private static void OnPickupRemoved(ItemPickupBase itemPickupBase)
    {
        if (Pickup.Dictionary.ContainsKey(itemPickupBase)) Pickup.Dictionary.Remove(itemPickupBase);
    }

    private static void OnItemAdded(ReferenceHub hub, ItemBase ibase, ItemPickupBase ipbase)
    {
        Item.ItemGet(ibase);
    }

    private static void OnItemRemoved(ReferenceHub hub, ItemBase ibase, ItemPickupBase ipbase)
    {
        if (Item.Dictionary.ContainsKey(ibase)) Item.Dictionary.Remove(ibase);
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