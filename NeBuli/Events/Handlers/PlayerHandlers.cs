using Nebuli.Events.EventArguments.Player;

namespace Nebuli.Events.Handlers;

public static class PlayerHandlers
{
    public static event EventManager.CustomEventHandler<PlayerJoinEventArgs> Join;

    internal static void OnJoin(PlayerJoinEventArgs ev) => Join.CallEvent(ev);
}