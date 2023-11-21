// -----------------------------------------------------------------------
// <copyright file=PlayerHandlers.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.Events.EventArguments.Player;

namespace Nebuli.Events.Handlers
{
    public static class PlayerHandlers
    {
        /// <summary>
        ///     Triggered when a player joins the server.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerJoinEvent> Join;

        /// <summary>
        ///     Triggered when a player leaves the server.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerLeaveEvent> Leave;

        /// <summary>
        ///     Triggered when a player is hurt.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerHurtEvent> Hurt;

        /// <summary>
        ///     Triggered when a player is dying.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerDyingEvent> Dying;

        /// <summary>
        ///     Triggered when a player is banned from the server.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerBannedEvent> Banned;

        /// <summary>
        ///     Triggered when a player shoots a weapon.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerShotEventArgs> Shot;

        /// <summary>
        ///     Triggered when a player's role changes.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerRoleChangeEvent> RoleChange;

        /// <summary>
        ///     Triggered when a player triggers a Tesla gate.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerTriggeringTeslaEvent> TriggeringTesla;

        /// <summary>
        ///     Triggered when a player escapes from the facility.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerEscapingEvent> Escaping;

        /// <summary>
        ///     Triggered when a player picks up an item.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerPickingUpItemEvent> PickingUpItem;

        /// <summary>
        ///     Triggered when a player drops an item.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerDroppingItemEvent> DroppingItem;

        /// <summary>
        ///     Triggered when a player picks up ammo.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerPickingUpAmmoEvent> PickingUpAmmo;

        /// <summary>
        ///     Triggered when a player escapes from the pocket dimension.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerEscapingPocketEvent> EscapingPocket;

        /// <summary>
        ///     Triggered when a player changes their user group.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerChangingUserGroupEvent> UserChangingUserGroup;

        /// <summary>
        ///     Triggered when a player spawns a ragdoll.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerSpawningRagdollEvent> SpawningRagdoll;

        /// <summary>
        ///     Triggered when a player spawns in the game.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerSpawningEvent> Spawning;

        /// <summary>
        ///     Triggered when a player uses a radio battery.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerUsingRadioBatteryEvent> UsingRadioBattery;

        /// <summary>
        ///     Triggered when a player interacts with a door.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerInteractingDoorEvent> InteractingDoor;

        /// <summary>
        ///     Triggered when a player picks up armor.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerPickingUpArmorEvent> PickingUpArmor;

        /// <summary>
        ///     Triggered when a player is destroyed.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerDestroyingEvent> Destroying;

        /// <summary>
        ///     Triggered when a player flips a coin.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerFlippingCoinEvent> FlippingCoin;

        /// <summary>
        ///     Triggered when a player enters the pocket dimension.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerEnteringPocketDimensionEvent> EnteringPocketDimension;

        /// <summary>
        ///     Triggered when a player toggles NoClip mode.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerTogglingNoClipEvent> TogglingNoClip;

        /// <summary>
        ///     Triggered when a player changes their current item.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerChangedItemEvent> ChangedItem;

        /// <summary>
        ///     Triggered when a player changes their nickname.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerChangingNicknameEvent> ChangingNickname;

        /// <summary>
        ///     Triggered after the player has died.
        /// </summary>
        public static event EventManager.CustomEventHandler<PlayerDiedEvent> Died;

        internal static void OnJoin(PlayerJoinEvent ev)
        {
            Join.CallEvent(ev);
        }

        internal static void OnLeave(PlayerLeaveEvent ev)
        {
            Leave.CallEvent(ev);
        }

        internal static void OnHurt(PlayerHurtEvent ev)
        {
            Hurt.CallEvent(ev);
        }

        internal static void OnDying(PlayerDyingEvent ev)
        {
            Dying.CallEvent(ev);
        }

        internal static void OnBanned(PlayerBannedEvent ev)
        {
            Banned.CallEvent(ev);
        }

        internal static void OnShot(PlayerShotEventArgs ev)
        {
            Shot.CallEvent(ev);
        }

        internal static void OnRoleChange(PlayerRoleChangeEvent ev)
        {
            RoleChange.CallEvent(ev);
        }

        internal static void OnTriggerTesla(PlayerTriggeringTeslaEvent ev)
        {
            TriggeringTesla.CallEvent(ev);
        }

        internal static void OnEscaping(PlayerEscapingEvent ev)
        {
            Escaping.CallEvent(ev);
        }

        internal static void OnPickingupItem(PlayerPickingUpItemEvent ev)
        {
            PickingUpItem.CallEvent(ev);
        }

        internal static void OnDroppingItem(PlayerDroppingItemEvent ev)
        {
            DroppingItem.CallEvent(ev);
        }

        internal static void OnPickingUpAmmo(PlayerPickingUpAmmoEvent ev)
        {
            PickingUpAmmo.CallEvent(ev);
        }

        internal static void OnEscapingPocket(PlayerEscapingPocketEvent ev)
        {
            EscapingPocket.CallEvent(ev);
        }

        internal static void OnChangingUserGroup(PlayerChangingUserGroupEvent ev)
        {
            UserChangingUserGroup.CallEvent(ev);
        }

        internal static void OnSpawningRagdoll(PlayerSpawningRagdollEvent ev)
        {
            SpawningRagdoll.CallEvent(ev);
        }

        internal static void OnSpawning(PlayerSpawningEvent ev)
        {
            Spawning.CallEvent(ev);
        }

        internal static void OnUsingRadioBattery(PlayerUsingRadioBatteryEvent ev)
        {
            UsingRadioBattery.CallEvent(ev);
        }

        internal static void OnPlayerInteractingDoor(PlayerInteractingDoorEvent ev)
        {
            InteractingDoor.CallEvent(ev);
        }

        internal static void OnPlayerPickingUpArmor(PlayerPickingUpArmorEvent ev)
        {
            PickingUpArmor.CallEvent(ev);
        }

        internal static void OnDestroying(PlayerDestroyingEvent ev)
        {
            Destroying.CallEvent(ev);
        }

        internal static void OnFlippingCoin(PlayerFlippingCoinEvent ev)
        {
            FlippingCoin.CallEvent(ev);
        }

        internal static void OnPlayerEnteringPocket(PlayerEnteringPocketDimensionEvent ev)
        {
            EnteringPocketDimension.CallEvent(ev);
        }

        internal static void OnPlayerTogglingNoClip(PlayerTogglingNoClipEvent ev)
        {
            TogglingNoClip.CallEvent(ev);
        }

        internal static void OnChangedItem(PlayerChangedItemEvent ev)
        {
            ChangedItem.CallEvent(ev);
        }

        internal static void OnChangingNickname(PlayerChangingNicknameEvent ev)
        {
            ChangingNickname.CallEvent(ev);
        }

        internal static void OnDied(PlayerDiedEvent ev)
        {
            Died.CallEvent(ev);
        }
    }
}