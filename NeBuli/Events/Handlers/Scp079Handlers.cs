using Nebuli.Events.EventArguments.SCPs.Scp079;

namespace Nebuli.Events.Handlers;

public static class Scp079Handlers
{
    public static event EventManager.CustomEventHandler<Scp079PingingEvent> PingingEvent;

    public static event EventManager.CustomEventHandler<Scp079LosingSignalEvent> LosingSignalEvent;

    internal static void OnScp079Pinging(Scp079PingingEvent ev) => PingingEvent.CallEvent(ev);

    internal static void OnScp079LosingSignal(Scp079LosingSignalEvent ev) => LosingSignalEvent.CallEvent(ev);
}
