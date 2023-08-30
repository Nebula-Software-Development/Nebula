using Nebuli.Events.EventArguments.SCPs.Scp096;

namespace Nebuli.Events.Handlers;

public static class Scp096Handlers
{
    public static EventManager.CustomEventHandler<Scp096AddingTargetEvent> AddingTarget;

    internal static void OnAddingTarget(Scp096AddingTargetEvent ev) => AddingTarget.CallEvent(ev);
}
