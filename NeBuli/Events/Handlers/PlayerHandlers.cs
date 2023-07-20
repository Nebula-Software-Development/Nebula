using MonoMod.Utils;
using Nebuli.Events.EventArguments.Player;

namespace Nebuli.Events.Handlers;

public static class PlayerHandlers
{
    public static event EventManager.CustomEventHandler<PlayerJoinEventArgs> Join;

    public static event EventManager.CustomEventHandler<PlayerLeaveEventArgs> Leave;

    public static event EventManager.CustomEventHandler<PlayerHurtEventArgs> Hurt;

    public static event EventManager.CustomEventHandler<PlayerDyingEventArgs> Dying;

    public static event EventManager.CustomEventHandler<PlayerBannedEventArgs> Banned;

    public static event EventManager.CustomEventHandler<PlayerShotEventArgs> Shot;

    internal static void OnJoin(PlayerJoinEventArgs ev) => Join.CallEvent(ev);

    internal static void OnLeave(PlayerLeaveEventArgs ev) => Leave.CallEvent(ev);

    internal static void OnHurt(PlayerHurtEventArgs ev) => Hurt.CallEvent(ev);

    internal static void OnDying(PlayerDyingEventArgs ev) => Dying.CallEvent(ev);

    internal static void OnBanned(PlayerBannedEventArgs ev) => Banned.CallEvent(ev);

    internal static void OnShot(PlayerShotEventArgs ev) => Shot.CallEvent(ev);
}