using Nebuli.Events.EventArguments.SCPs.Scp049;

namespace Nebuli.Events.Handlers;

public static class Scp049Handlers
{
    public static event EventManager.CustomEventHandler<Scp049UseSense> Sense;
    
    public static event EventManager.CustomEventHandler<Scp049UseCall> Call;
    
    public static event EventManager.CustomEventHandler<Scp049StartResurrect> StartResurrect; 
    
    public static event EventManager.CustomEventHandler<Scp049FinishResurrect> FinishResurrect; 
    
    public static event EventManager.CustomEventHandler<Scp049CancelResurrect> CancelResurrect; 
    
    public static event EventManager.CustomEventHandler<Scp049LoseSenseTarget> LoseSenseTarget; 

    internal static void OnSense(Scp049UseSense ev) => Sense.CallEvent(ev);
    
    internal static void OnCall(Scp049UseCall ev) => Call.CallEvent(ev);
    
    internal static void OnStartResurrect(Scp049StartResurrect ev) => StartResurrect.CallEvent(ev);
    
    internal static void OnFinishResurrect(Scp049FinishResurrect ev) => FinishResurrect.CallEvent(ev);
    
    internal static void OnCancelResurrect(Scp049CancelResurrect ev) => CancelResurrect.CallEvent(ev);
    
    internal static void OnLoseSenseTarget(Scp049LoseSenseTarget ev) => LoseSenseTarget.CallEvent(ev);
}