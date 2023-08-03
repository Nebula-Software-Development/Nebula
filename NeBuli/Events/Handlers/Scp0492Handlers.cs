using Nebuli.Events.EventArguments.SCPs.Scp0492;

namespace Nebuli.Events.Handlers;

public static class Scp0492Handlers
{
    public static event EventManager.CustomEventHandler<Scp0492ConsumeCorpse> ConsumeCorpse;

    public static event EventManager.CustomEventHandler<Scp0492CorpseConsumed> CorpseConsumed;

    public static event EventManager.CustomEventHandler<Scp0492Attack> Attack;

    public static event EventManager.CustomEventHandler<Scp0492Bloodlust> BloodLust; 

    internal static void OnConsumeCorpse(Scp0492ConsumeCorpse ev) => ConsumeCorpse.CallEvent(ev);

    internal static void OnCorpseConsumed(Scp0492CorpseConsumed ev) => CorpseConsumed.CallEvent(ev);

    internal static void OnAttack(Scp0492Attack ev) => Attack.CallEvent(ev);

    internal static void OnBloodLust(Scp0492Bloodlust ev) => BloodLust.CallEvent(ev);
}