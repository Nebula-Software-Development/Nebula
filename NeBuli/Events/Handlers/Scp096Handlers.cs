using Nebuli.Events.EventArguments.SCPs.Scp096;

namespace Nebuli.Events.Handlers;

public static class Scp096Handlers
{
    public static event EventManager.CustomEventHandler<Scp096AddingTargetEvent> AddingTarget;

    public static event EventManager.CustomEventHandler<Scp096PryingGateEvent> PryingGate;

    public static event EventManager.CustomEventHandler<Scp096EnragingEvent> Enraging;

    public static event EventManager.CustomEventHandler<Scp096CalmingEvent> Calming;

    internal static void OnAddingTarget(Scp096AddingTargetEvent ev) => AddingTarget.CallEvent(ev);
    internal static void OnPryingGate(Scp096PryingGateEvent ev) => PryingGate.CallEvent(ev);
    internal static void OnEnraging(Scp096EnragingEvent ev) => Enraging.CallEvent(ev);
    internal static void OnCalming(Scp096CalmingEvent ev) => Calming.CallEvent(ev);
}
