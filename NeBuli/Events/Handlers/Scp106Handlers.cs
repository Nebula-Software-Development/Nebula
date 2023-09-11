using Nebuli.Events.EventArguments.SCPs.Scp106;

namespace Nebuli.Events.Handlers;

public static class Scp106Handlers
{
    public static event EventManager.CustomEventHandler<Scp106AttackingEvent> Attacking;

    internal static void OnAttacking(Scp106AttackingEvent ev) => Attacking.CallEvent(ev);
}
