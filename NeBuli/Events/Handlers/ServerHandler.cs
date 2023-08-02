using Nebuli.Events.EventArguments.Server;

namespace Nebuli.Events.Handlers;

public static class ServerHandler
{
    public static event EventManager.CustomEventHandler<RoundStartedEvent> RoundStart;

    public static event EventManager.CustomEventHandler<WarheadDetonating> WarheadDetonated;

    internal static void OnRoundStart() => RoundStart.CallEvent(null);

    internal static void OnWarheadDetonated(WarheadDetonating ev) => WarheadDetonated.CallEvent(ev);
}