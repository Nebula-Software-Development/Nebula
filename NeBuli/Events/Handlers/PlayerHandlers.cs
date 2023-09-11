using Nebuli.Events.EventArguments.Player;

namespace Nebuli.Events.Handlers;

public static class PlayerHandlers
{
    public static event EventManager.CustomEventHandler<PlayerJoinEvent> Join;

    public static event EventManager.CustomEventHandler<PlayerLeaveEvent> Leave;

    public static event EventManager.CustomEventHandler<PlayerHurtEvent> Hurt;

    public static event EventManager.CustomEventHandler<PlayerDyingEvent> Dying;

    public static event EventManager.CustomEventHandler<PlayerBannedEvent> Banned;

    public static event EventManager.CustomEventHandler<PlayerShotEventArgs> Shot;

    public static event EventManager.CustomEventHandler<PlayerRoleChangeEvent> RoleChange;

    public static event EventManager.CustomEventHandler<PlayerTriggeringTeslaEvent> TriggeringTesla;

    public static event EventManager.CustomEventHandler<PlayerEscapingEvent> Escaping;

    public static event EventManager.CustomEventHandler<PlayerPickingUpItemEvent> PickingUpItem;
     
    public static event EventManager.CustomEventHandler<PlayerDroppingItemEvent> DroppingItem;

    public static event EventManager.CustomEventHandler<PlayerPickingUpAmmoEvent> PickingUpAmmo;

    public static event EventManager.CustomEventHandler<PlayerEscapingPocketEvent> EscapingPocket;

    public static event EventManager.CustomEventHandler<PlayerChangingUserGroupEvent> UserChangingUserGroup;

    public static event EventManager.CustomEventHandler<PlayerSpawningRagdollEvent> SpawningRagdoll;

    public static event EventManager.CustomEventHandler<PlayerSpawningEvent> Spawning;

    public static event EventManager.CustomEventHandler<PlayerUsingRadioBatteryEvent> UsingRadioBattery;

    public static event EventManager.CustomEventHandler<PlayerInteractingDoorEvent> InteractingDoor;

    public static event EventManager.CustomEventHandler<PlayerPickingUpArmorEvent> PickingUpArmor;

    public static event EventManager.CustomEventHandler<PlayerDestroyingEvent> Destroying;

    public static event EventManager.CustomEventHandler<PlayerFlippingCoinEvent> FlippingCoin;

    public static event EventManager.CustomEventHandler<PlayerEnteringPocketDimensionEvent> EnteringPocketDimension;

    public static event EventManager.CustomEventHandler<PlayerTogglingNoClipEvent> TogglingNoClip;

    internal static void OnJoin(PlayerJoinEvent ev) => Join.CallEvent(ev);

    internal static void OnLeave(PlayerLeaveEvent ev) => Leave.CallEvent(ev);

    internal static void OnHurt(PlayerHurtEvent ev) => Hurt.CallEvent(ev);

    internal static void OnDying(PlayerDyingEvent ev) => Dying.CallEvent(ev);

    internal static void OnBanned(PlayerBannedEvent ev) => Banned.CallEvent(ev);

    internal static void OnShot(PlayerShotEventArgs ev) => Shot.CallEvent(ev);

    internal static void OnRoleChange(PlayerRoleChangeEvent ev) => RoleChange.CallEvent(ev);

    internal static void OnTriggerTesla(PlayerTriggeringTeslaEvent ev) => TriggeringTesla.CallEvent(ev);

    internal static void OnEscaping(PlayerEscapingEvent ev) => Escaping.CallEvent(ev);

    internal static void OnPickingupItem(PlayerPickingUpItemEvent ev) => PickingUpItem.CallEvent(ev);

    internal static void OnDroppingItem(PlayerDroppingItemEvent ev) => DroppingItem.CallEvent(ev);

    internal static void OnPickingUpAmmo(PlayerPickingUpAmmoEvent ev) => PickingUpAmmo.CallEvent(ev);

    internal static void OnEscapingPocket(PlayerEscapingPocketEvent ev) => EscapingPocket.CallEvent(ev);

    internal static void OnChangingUserGroup(PlayerChangingUserGroupEvent ev) => UserChangingUserGroup.CallEvent(ev); 

    internal static void OnSpawningRagdoll(PlayerSpawningRagdollEvent ev) => SpawningRagdoll.CallEvent(ev);

    internal static void OnSpawning(PlayerSpawningEvent ev) => Spawning.CallEvent(ev);

    internal static void OnUsingRadioBattery(PlayerUsingRadioBatteryEvent ev) => UsingRadioBattery.CallEvent(ev);

    internal static void OnPlayerInteractingDoor(PlayerInteractingDoorEvent ev) => InteractingDoor.CallEvent(ev);

    internal static void OnPlayerPickingUpArmor(PlayerPickingUpArmorEvent ev) => PickingUpArmor.CallEvent(ev);

    internal static void OnDestroying(PlayerDestroyingEvent ev) => Destroying.CallEvent(ev);

    internal static void OnFlippingCoin(PlayerFlippingCoinEvent ev) => FlippingCoin.CallEvent(ev);

    internal static void OnPlayerEnteringPocket(PlayerEnteringPocketDimensionEvent ev) => EnteringPocketDimension.CallEvent(ev);

    internal static void OnPlayerTogglingNoClip(PlayerTogglingNoClipEvent ev) => TogglingNoClip.CallEvent(ev);
}