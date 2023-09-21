using Nebuli.Events.EventArguments.SCPs.Scp049;

namespace Nebuli.Events.Handlers;

public static class Scp049Handlers
{
    public static event EventManager.CustomEventHandler<Scp049UseSenseEvent> Sense;

    public static event EventManager.CustomEventHandler<Scp049UseCallEvent> Call;

    public static event EventManager.CustomEventHandler<Scp049StartResurrectEvent> StartResurrect;

    public static event EventManager.CustomEventHandler<Scp049FinishResurrectEvent> FinishResurrect;

    public static event EventManager.CustomEventHandler<Scp049CancelResurrectEvent> CancelResurrect;

    public static event EventManager.CustomEventHandler<Scp049LoseSenseTargetEvent> LoseSenseTarget;

    internal static void OnSense(Scp049UseSenseEvent ev) => Sense.CallEvent(ev);

    internal static void OnCall(Scp049UseCallEvent ev) => Call.CallEvent(ev);

    internal static void OnStartResurrect(Scp049StartResurrectEvent ev) => StartResurrect.CallEvent(ev);

    internal static void OnFinishResurrect(Scp049FinishResurrectEvent ev) => FinishResurrect.CallEvent(ev);

    internal static void OnCancelResurrect(Scp049CancelResurrectEvent ev) => CancelResurrect.CallEvent(ev);

    internal static void OnLoseSenseTarget(Scp049LoseSenseTargetEvent ev) => LoseSenseTarget.CallEvent(ev);
}