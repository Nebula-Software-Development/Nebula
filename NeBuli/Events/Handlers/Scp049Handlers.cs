using Nebuli.Events.EventArguments.SCPs.Scp049;

namespace Nebuli.Events.Handlers;

public static class Scp049Handlers
{
    /// <summary>
    /// Triggered when SCP-049 uses its sense ability.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp049UseSenseEvent> Sense;

    /// <summary>
    /// Triggered when SCP-049 uses its call ability.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp049UseCallEvent> Call;

    /// <summary>
    /// Triggered when SCP-049 starts the resurrection process.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp049StartResurrectEvent> StartResurrect;

    /// <summary>
    /// Triggered when SCP-049 successfully finishes resurrecting a player.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp049FinishResurrectEvent> FinishResurrect;

    /// <summary>
    /// Triggered when SCP-049 cancels the resurrection process.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp049CancelResurrectEvent> CancelResurrect;

    /// <summary>
    /// Triggered when SCP-049 loses its sense target.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp049LoseSenseTargetEvent> LoseSenseTarget;

    internal static void OnSense(Scp049UseSenseEvent ev) => Sense.CallEvent(ev);

    internal static void OnCall(Scp049UseCallEvent ev) => Call.CallEvent(ev);

    internal static void OnStartResurrect(Scp049StartResurrectEvent ev) => StartResurrect.CallEvent(ev);

    internal static void OnFinishResurrect(Scp049FinishResurrectEvent ev) => FinishResurrect.CallEvent(ev);

    internal static void OnCancelResurrect(Scp049CancelResurrectEvent ev) => CancelResurrect.CallEvent(ev);

    internal static void OnLoseSenseTarget(Scp049LoseSenseTargetEvent ev) => LoseSenseTarget.CallEvent(ev);
}