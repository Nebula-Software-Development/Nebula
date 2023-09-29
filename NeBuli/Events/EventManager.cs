using HarmonyLib;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Firearms.Attachments.Components;
using InventorySystem.Items.Pickups;
using MapGeneration;
using Nebuli.API.Extensions;
using Nebuli.API.Features;
using Nebuli.API.Features.Doors;
using Nebuli.API.Features.Enum;
using Nebuli.API.Features.Items;
using Nebuli.API.Features.Items.Pickups;
using Nebuli.API.Features.Map;
using Nebuli.API.Features.Player;
using Nebuli.API.Features.Pools;
using Nebuli.API.Features.Structs;
using Nebuli.Events.Handlers;
using Nebuli.Events.Patches.Game;
using Nebuli.Loader;
using PlayerRoles;
using PlayerRoles.Ragdolls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Nebuli.Events;

/// <summary>
/// Nebulis custom event management class.
/// </summary>
public static class EventManager
{
    /// <summary>
    /// Represents a custom event handler delegate with event argument of type T.
    /// </summary>
    /// <typeparam name="T">The type of event arguments.</typeparam>
    /// <param name="ev">The event argument.</param>
    public delegate void CustomEventHandler<in T>(T ev)
            where T : EventArgs;

    /// <summary>
    /// Invokes a custom event handler with event arguments of type T.
    /// </summary>
    /// <typeparam name="T">The type of event arguments.</typeparam>
    /// <param name="eventHandler">The custom event handler delegate.</param>
    /// <param name="args">The event arguments.</param>
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
                Log.Error("An error occurred while handling the event " + eventHandler.Method.Name + $"\n{e}", "Event Error");
            }
        }
    }

    /// <summary>
    /// Represents a custom event handler delegate without event arguments.
    /// </summary>
    public delegate void CustomEventHandler();

    /// <summary>
    /// Invokes a custom event handler without event arguments.
    /// </summary>
    /// <param name="eventHandler">The custom event handler delegate.</param>
    public static void CallEmptyEvent(this CustomEventHandler eventHandler)
    {
        if (eventHandler is null)
            return;

        foreach (Delegate sub in eventHandler.GetInvocationList())
        {
            try
            {
                sub.DynamicInvoke();
            }
            catch (Exception e)
            {
                Log.Error("An error occurred while handling the event " + eventHandler.Method.Name + $"\n{e}", "Event Error");
            }
        }
    }

    internal static void RegisterBaseEvents()
    {
        SceneManager.sceneUnloaded += OnSceneUnLoaded;
        CharacterClassManager.OnInstanceModeChanged += NpcInstanceModeFix.HandleInstanceModeChange;
        RagdollManager.OnRagdollSpawned += OnRagdollSpawned;
        RagdollManager.OnRagdollRemoved += OnRagdollDeSpawned;
        SeedSynchronizer.OnMapGenerated += OnMapGenerated;
        ItemPickupBase.OnPickupAdded += OnPickupAdded;
        ItemPickupBase.OnPickupDestroyed += OnPickupRemoved;
        InventoryExtensions.OnItemAdded += OnItemAdded;
        InventoryExtensions.OnItemRemoved += OnItemRemoved;
        CharacterClassManager.OnRoundStarted += RoundHandlers.OnRoundStart;
        PlayerRoleManager.OnRoleChanged += RoleChange;
        SeedSynchronizer.OnMapGenerated += Internal.Handler;
    }

    internal static void UnRegisterBaseEvents()
    {
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;
        RagdollManager.OnRagdollSpawned -= OnRagdollSpawned;
        CharacterClassManager.OnInstanceModeChanged -= NpcInstanceModeFix.HandleInstanceModeChange;
        RagdollManager.OnRagdollRemoved -= OnRagdollDeSpawned;
        SeedSynchronizer.OnMapGenerated -= OnMapGenerated;
        ItemPickupBase.OnPickupAdded -= OnPickupAdded;
        ItemPickupBase.OnPickupDestroyed -= OnPickupRemoved;
        InventoryExtensions.OnItemAdded -= OnItemAdded;
        InventoryExtensions.OnItemRemoved -= OnItemRemoved;
        CharacterClassManager.OnRoundStarted -= RoundHandlers.OnRoundStart;
        PlayerRoleManager.OnRoleChanged -= RoleChange;
        SeedSynchronizer.OnMapGenerated -= Internal.Handler;
    }

    private static void RoleChange(ReferenceHub userHub, PlayerRoleBase prevRole, PlayerRoleBase newRole)
    {

        API.Features.Roles.Role.CreateNew(newRole);
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
        NebuliPlayer.Dictionary.Clear();
        Ragdoll.Dictionary.Clear();
        Generator.Dictionary.Clear();
        Room.Dictionary.Clear();
        Door.Dictionary.Clear();
        Pickup.Dictionary.Clear();
        Item.Dictionary.Clear();
        if (Updater.PendingUpdate is not null) Update();
    }

    private static void OnMapGenerated()
    {
        foreach (RoomIdentifier room in RoomIdentifier.AllRoomIdentifiers)
            Room.Get(room);
        foreach (DoorVariant door in Object.FindObjectsOfType<DoorVariant>())
            Door.GetDoor(door);
        foreach (global::TeslaGate teslaGate in Object.FindObjectsOfType<TeslaGate>())
            API.Features.Map.TeslaGate.Get(teslaGate);
        foreach (ElevatorChamber elevatorChamber in Object.FindObjectsOfType<ElevatorChamber>())
            Elevator.Get(elevatorChamber);
        foreach (BreakableWindow breakableWindow in Object.FindObjectsOfType<BreakableWindow>())
            Window.Get(breakableWindow);
        foreach (PlayerRoles.PlayableScps.Scp079.Cameras.Scp079Camera camera in Object.FindObjectsOfType<PlayerRoles.PlayableScps.Scp079.Cameras.Scp079Camera>())
            Camera.Get(camera);
        if(ReferenceHub.TryGetHostHub(out ReferenceHub hub)) Server.NebuliHost = new(hub);
        GenerateAttachments();
    }

    private static void OnPickupAdded(ItemPickupBase itemPickupBase) => Pickup.Get(itemPickupBase);

    private static void OnPickupRemoved(ItemPickupBase itemPickupBase)
    {
        if (Pickup.Dictionary.ContainsKey(itemPickupBase)) Pickup.Dictionary.Remove(itemPickupBase);
    }

    private static void OnItemAdded(ReferenceHub hub, ItemBase ibase, ItemPickupBase ipbase) => Item.Get(ibase);

    private static void OnItemRemoved(ReferenceHub hub, ItemBase ibase, ItemPickupBase ipbase)
    {
        if (Item.Dictionary.ContainsKey(ibase)) Item.Dictionary.Remove(ibase);
    }

    private static void Update()
    {
        string destinationFilePath = Updater.NeubliPath;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            SetLinuxFilePermissions(destinationFilePath, Environment.UserName, "755");
        }

        Updater.Stream.CopyTo(Updater.PendingUpdate);
    }

    private static void SetLinuxFilePermissions(string filePath, string usernameOrGroup, string permissions)
    {
        ProcessStartInfo psi = new()
        {
            FileName = "chmod",
            Arguments = $"{permissions} {filePath}",
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

        using (Process process = Process.Start(psi))
        {
            process.WaitForExit();
        }

        ProcessStartInfo chownPsi = new()
        {
            FileName = "chown",
            Arguments = $"{usernameOrGroup}:{usernameOrGroup} {filePath}",
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

        using Process chownProcess = Process.Start(chownPsi);
        chownProcess.WaitForExit();
    }

    // Method from CursedMod: Allow us to check if the instructions of X Transpiler has changed or not
    public static List<CodeInstruction> CheckPatchInstructions<T>(int originalCodes, IEnumerable<CodeInstruction> instructions)
    {
        List<CodeInstruction> newInstructions = NorthwoodLib.Pools.ListPool<CodeInstruction>.Shared.Rent(instructions);
        if (originalCodes == newInstructions.Count)
            return newInstructions;

        Log.Error(typeof(T).FullDescription() + $" has an incorrect number of OpCodes ({originalCodes} != {newInstructions.Count}). The patch may be broken or bugged.");
        return newInstructions;
    }

    private static void GenerateAttachments()
    {
        Firearm.BaseCodes.Clear();
        Firearm.AvailableAttachments.Clear();
        Firearm.TypeToFirearm.Clear();

        foreach (FirearmType firearmType in Enum.GetValues(typeof(FirearmType)))
        {
            try
            {
                if (firearmType == FirearmType.None || firearmType == FirearmType.MircoHID || Item.Get(firearmType.ConvertToItemType().GetItemBase()) is not Firearm firearm)
                    continue;

                Firearm.TypeToFirearm.Add(firearmType, firearm);

                List<AttachmentIdentity> attachmentIdentifiers = ListPool<AttachmentIdentity>.Instance.Get();
                HashSet<AttachmentSlot> attachmentsSlots = HashSetPool<AttachmentSlot>.Pool.Get();

                uint code = 1;

                foreach (Attachment attachment in firearm.Attachments)
                {
                    attachmentsSlots.Add(attachment.Slot);
                    attachmentIdentifiers.Add(new AttachmentIdentity(code, attachment.Name, attachment.Slot));
                    code *= 2U;
                }

                uint baseCode = CalculateBaseCode(attachmentIdentifiers);

                if (!Firearm.BaseCodes.ContainsKey(firearmType)) Firearm.BaseCodes.Add(firearmType, baseCode);
                if (!Firearm.AvailableAttachments.ContainsKey(firearmType)) Firearm.AvailableAttachmentsValue.Add(firearmType, attachmentIdentifiers.ToArray());

                ListPool<AttachmentIdentity>.Instance.Return(attachmentIdentifiers);
                HashSetPool<AttachmentSlot>.Pool.Return(attachmentsSlots);
            }
            catch (Exception e)
            {
                Log.Error($"Error occurred while generating attachments! Full error --> \n{e}");
            }
        }
    }

    private static uint CalculateBaseCode(List<AttachmentIdentity> attachmentIdentifiers)
    {
        Dictionary<AttachmentSlot, uint> slotToMinCode = attachmentIdentifiers.GroupBy(attachment => attachment.Slot).ToDictionary(group => group.Key, group => group.Aggregate((minAttachment, nextAttachment) => nextAttachment.Code < minAttachment.Code ? nextAttachment : minAttachment).Code);
        uint baseCode = slotToMinCode.Values.Aggregate(0U, (acc, code) => acc + code);
        return baseCode;
    }
}