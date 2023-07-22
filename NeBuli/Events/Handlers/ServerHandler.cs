using Nebuli.Events.EventArguments.Server;

namespace Nebuli.Events.Handlers
{
    public static class ServerHandler
    {
        public static EventManager.CustomEventHandler<RoundStartedEvent> RoundStart;

        public static EventManager.CustomEventHandler<WarheadDetonatedEventArgs> WarheadDetonated;

        internal static void OnRoundStart() => RoundStart.CallEvent(null);

        internal static void OnWarheadDetonated(WarheadDetonatedEventArgs ev) => WarheadDetonated.CallEvent(ev);
    }
}