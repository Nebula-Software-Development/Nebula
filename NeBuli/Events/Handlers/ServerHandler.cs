using Nebuli.Events.EventArguments.Server;

namespace Nebuli.Events.Handlers
{
    public static class ServerHandler
    {
        public static EventManager.CustomEventHandler<RoundStartedEvent> RoundStart;

        internal static void OnRoundStart() => RoundStart.CallEvent(null);
    }
}
