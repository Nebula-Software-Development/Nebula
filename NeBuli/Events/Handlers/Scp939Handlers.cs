using Nebuli.Events.EventArguments.SCPs.Scp939;

namespace Nebuli.Events.Handlers;

public static class Scp939Handlers
{
    /// <summary>
    /// Triggered when SCP-939 places a cloud of gas.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp939PlaceCloudEvent> PlaceCloud;

    /// <summary>
    /// Triggered when SCP-939 cancels cloud placement.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp939CancelCloudPlacementEvent> CancelCloudPlacement;

    /// <summary>
    /// Triggered when SCP-939 places a mimic point.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp939PlaceMimicPointEvent> PlaceMimicPoint;

    /// <summary>
    /// Triggered when SCP-939 removes a mimic point.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp939RemoveMimicPoint> RemoveMimicPoint;

    /// <summary>
    /// Triggered when SCP-939 plays a sound.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp939PlaySound> PlaySound;

    /// <summary>
    /// Triggered when SCP-939 plays a voice.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp939PlayVoiceEvent> PlayVoice;

    /// <summary>
    /// Triggered when SCP-939 saves a player's voice.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp939SavePlayerVoiceEvent> SaveVoice;

    /// <summary>
    /// Triggered when SCP-939 removes saved player voice.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp939RemoveSavedVoiceEvent> RemoveVoice;

    /// <summary>
    /// Triggered when SCP-939 uses the lunge ability.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp939UseLungeEvent> UseLunge;

    /// <summary>
    /// Triggered when SCP-939 toggles focus.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp939ToggleFocusEvent> ToggleFocus;

    /// <summary>
    /// Triggered when SCP-939 attacks.
    /// </summary>
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