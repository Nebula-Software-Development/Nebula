using Nebuli.Events.EventArguments.SCPs.Scp939;

namespace Nebuli.Events.Handlers;

public static class Scp939Handlers
{
    public static event EventManager.CustomEventHandler<Scp939PlaceCloudEvent> PlaceCloud;

    public static event EventManager.CustomEventHandler<Scp939CancelCloudPlacementEvent> CancelCloudPlacement;

    public static event EventManager.CustomEventHandler<Scp939PlaceMimicPointEvent> PlaceMimicPoint;

    public static event EventManager.CustomEventHandler<Scp939RemoveMimicPoint> RemoveMimicPoint;

    public static event EventManager.CustomEventHandler<Scp939PlaySound> PlaySound;

    public static event EventManager.CustomEventHandler<Scp939PlayVoiceEvent> PlayVoice;

    public static event EventManager.CustomEventHandler<Scp939SavePlayerVoiceEvent> SaveVoice;

    public static event EventManager.CustomEventHandler<Scp939RemoveSavedVoiceEvent> RemoveVoice;

    public static event EventManager.CustomEventHandler<Scp939UseLungeEvent> UseLunge;

    public static event EventManager.CustomEventHandler<Scp939ToggleFocusEvent> ToggleFocus;

    public static event EventManager.CustomEventHandler<Scp939AttackEvent> Attack;

    internal static void OnPlaceCloud(Scp939PlaceCloudEvent ev) => PlaceCloud.CallEvent(ev);

    internal static void OnCancelCloudPlacement(Scp939CancelCloudPlacementEvent ev) => CancelCloudPlacement.CallEvent(ev);

    internal static void OnPlaceMimicPoint(Scp939PlaceMimicPointEvent ev) => PlaceMimicPoint.CallEvent(ev);

    internal static void OnRemoveMimicPoint(Scp939RemoveMimicPoint ev) => RemoveMimicPoint.CallEvent(ev);

    internal static void OnPlaySound(Scp939PlaySound ev) => PlaySound.CallEvent(ev);

    internal static void OnPlayVoice(Scp939PlayVoiceEvent ev) => PlayVoice.CallEvent(ev);

    internal static void OnSaveVoice(Scp939SavePlayerVoiceEvent ev) => SaveVoice.CallEvent(ev);

    internal static void OnRemoveVoice(Scp939RemoveSavedVoiceEvent ev) => RemoveVoice.CallEvent(ev);

    internal static void OnUseLunge(Scp939UseLungeEvent ev) => UseLunge.CallEvent(ev);

    internal static void OnToggleFocus(Scp939ToggleFocusEvent ev) => ToggleFocus.CallEvent(ev);

    internal static void OnAttack(Scp939AttackEvent ev) => Attack.CallEvent(ev);
}