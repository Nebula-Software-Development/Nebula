using Nebuli.Events.EventArguments.Round;
using Nebuli.Events.EventArguments.Server;

namespace Nebuli.Events.Handlers;

public static class ServerHandler
{
    public static event EventManager.CustomEventHandler<RoundStartedEvent> RoundStart;

    public static event EventManager.CustomEventHandler<WarheadDetonatingEvent> WarheadDetonated;

    internal static void OnRoundStart() => RoundStart.CallEvent(null);

    internal static void OnWarheadDetonated(WarheadDetonatingEvent ev) => WarheadDetonated.CallEvent(ev);
}