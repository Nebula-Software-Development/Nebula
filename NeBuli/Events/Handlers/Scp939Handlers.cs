using Nebuli.Events.EventArguments.SCPs.Scp939;

namespace Nebuli.Events.Handlers;

public static class Scp939Handlers
{
    public static event EventManager.CustomEventHandler<Scp939PlaceCloud> PlaceCloud;
    
    public static event EventManager.CustomEventHandler<Scp939CancelCloudPlacement> CancelCloudPlacement;
    
    public static event EventManager.CustomEventHandler<Scp939PlaceMimicPoint> PlaceMimicPoint;
    
    public static event EventManager.CustomEventHandler<Scp939RemoveMimicPoint> RemoveMimicPoint;
    
    public static event EventManager.CustomEventHandler<Scp939PlaySound> PlaySound;
    
    public static event EventManager.CustomEventHandler<Scp939PlayVoice> PlayVoice;
    
    public static event EventManager.CustomEventHandler<Scp939SavePlayerVoice> SaveVoice;
    
    public static event EventManager.CustomEventHandler<Scp939RemoveSavedVoice> RemoveVoice;
    
    public static event EventManager.CustomEventHandler<Scp939UseLunge> UseLunge;
    
    public static event EventManager.CustomEventHandler<Scp939ToggleFocus> ToggleFocus;
    
    public static event EventManager.CustomEventHandler<Scp939Attack> Attack;

    internal static void OnPlaceCloud(Scp939PlaceCloud ev) => PlaceCloud.CallEvent(ev);
    
    internal static void OnCancelCloudPlacement(Scp939CancelCloudPlacement ev) => CancelCloudPlacement.CallEvent(ev);
    
    internal static void OnPlaceMimicPoint(Scp939PlaceMimicPoint ev) => PlaceMimicPoint.CallEvent(ev);
    
    internal static void OnRemoveMimicPoint(Scp939RemoveMimicPoint ev) => RemoveMimicPoint.CallEvent(ev);
    
    internal static void OnPlaySound(Scp939PlaySound ev) => PlaySound.CallEvent(ev);
    
    internal static void OnPlayVoice(Scp939PlayVoice ev) => PlayVoice.CallEvent(ev);
    
    internal static void OnSaveVoice(Scp939SavePlayerVoice ev) => SaveVoice.CallEvent(ev);
    
    internal static void OnRemoveVoice(Scp939RemoveSavedVoice ev) => RemoveVoice.CallEvent(ev);
    
    internal static void OnUseLunge(Scp939UseLunge ev) => UseLunge.CallEvent(ev);
    
    internal static void OnToggleFocus(Scp939ToggleFocus ev) => ToggleFocus.CallEvent(ev);
    
    internal static void OnAttack(Scp939Attack ev) => Attack.CallEvent(ev);
}
