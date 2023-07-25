using Nebuli.Events.EventArguments.Player;

namespace Nebuli.Events.Handlers;

public static class PlayerHandlers
{
    public static event EventManager.CustomEventHandler<PlayerJoin> Join;

    public static event EventManager.CustomEventHandler<PlayerLeave> Leave;

    public static event EventManager.CustomEventHandler<PlayerHurt> Hurt;

    public static event EventManager.CustomEventHandler<PlayerDying> Dying;

    public static event EventManager.CustomEventHandler<PlayerBanned> Banned;

    public static event EventManager.CustomEventHandler<PlayerShotEventArgs> Shot;

    public static event EventManager.CustomEventHandler<PlayerRoleChange> RoleChange;

    public static event EventManager.CustomEventHandler<PlayerTriggeringTesla> TriggeringTesla;

    internal static void OnJoin(PlayerJoin ev) => Join.CallEvent(ev);

    internal static void OnLeave(PlayerLeave ev) => Leave.CallEvent(ev);

    internal static void OnHurt(PlayerHurt ev) => Hurt.CallEvent(ev);

    internal static void OnDying(PlayerDying ev) => Dying.CallEvent(ev);

    internal static void OnBanned(PlayerBanned ev) => Banned.CallEvent(ev);

    internal static void OnShot(PlayerShotEventArgs ev) => Shot.CallEvent(ev);

    internal static void OnRoleChange(PlayerRoleChange ev) => RoleChange.CallEvent(ev);

    internal static void OnTriggerTesla(PlayerTriggeringTesla ev) => TriggeringTesla.CallEvent(ev);
}