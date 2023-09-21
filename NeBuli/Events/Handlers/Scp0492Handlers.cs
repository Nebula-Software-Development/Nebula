using Nebuli.Events.EventArguments.SCPs.Scp0492;

namespace Nebuli.Events.Handlers;

public static class Scp0492Handlers
{
    public static event EventManager.CustomEventHandler<Scp0492ConsumeCorpseEvent> ConsumeCorpse;

    public static event EventManager.CustomEventHandler<Scp0492CorpseConsumedEvent> CorpseConsumed;

    public static event EventManager.CustomEventHandler<Scp0492AttackEvent> Attack;

    public static event EventManager.CustomEventHandler<Scp0492BloodlustEvent> BloodLust;

    internal static void OnConsumeCorpse(Scp0492ConsumeCorpseEvent ev) => ConsumeCorpse.CallEvent(ev);

    internal static void OnCorpseConsumed(Scp0492CorpseConsumedEvent ev) => CorpseConsumed.CallEvent(ev);

    internal static void OnAttack(Scp0492AttackEvent ev) => Attack.CallEvent(ev);

    internal static void OnBloodLust(Scp0492BloodlustEvent ev) => BloodLust.CallEvent(ev);
}