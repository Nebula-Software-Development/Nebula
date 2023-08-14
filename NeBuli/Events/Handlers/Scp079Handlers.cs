using Nebuli.Events.EventArguments.SCPs.Scp079;

namespace Nebuli.Events.Handlers;

public static class Scp079Handlers
{
    public static event EventManager.CustomEventHandler<Scp079PingingEvent> PingingEvent;

    public static event EventManager.CustomEventHandler<Scp079LosingSignalEvent> LosingSignalEvent;

    public static event EventManager.CustomEventHandler<Scp079GainingExperienceEvent> GainingExperienceEvent;

    public static event EventManager.CustomEventHandler<Scp079GainingLevelEvent > GainingLevelEvent;

    internal static void OnScp079Pinging(Scp079PingingEvent ev) => PingingEvent.CallEvent(ev);

    internal static void OnScp079LosingSignal(Scp079LosingSignalEvent ev) => LosingSignalEvent.CallEvent(ev);

    internal static void OnScp079GainingExpereince(Scp079GainingExperienceEvent ev) => GainingExperienceEvent.CallEvent(ev);

    internal static void OnScp079GainingLevel(Scp079GainingLevelEvent ev) => GainingLevelEvent.CallEvent(ev);
}
