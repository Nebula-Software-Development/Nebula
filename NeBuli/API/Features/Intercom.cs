using Mirror;
using Nebuli.API.Features.Player;
using PlayerRoles.Voice;
using UnityEngine;
using BaseIntercom = PlayerRoles.Voice.Intercom;

namespace Nebuli.API.Features;

/// <summary>
/// Wrapper class for making managing the intercom in-game easier.
/// </summary>
public static class Intercom
{
    /// <summary>
    /// Gets the singleton instance of the IntercomDisplay.
    /// </summary>
    public static IntercomDisplay IntercomDisplay => IntercomDisplay._singleton;

    /// <summary>
    /// Gets the GameObject associated with the IntercomDisplay.
    /// </summary>
    public static GameObject GameObject => IntercomDisplay._singleton.gameObject;

    /// <summary>
    /// Gets the Transform of the IntercomDisplay.
    /// </summary>
    public static Transform Transform => IntercomDisplay._singleton.transform;

    /// <summary>
    /// Initiates the playback of the intercom sound.
    /// </summary>
    /// <param name="isStartingSound">True if the sound is the intercom's start speaking sound; otherwise, false.</param>
    public static void InitiateIntercomSound(bool isStartingSound) => BaseIntercom._singleton.RpcPlayClip(isStartingSound);

    /// <summary>
    /// Resets the cooldown time for the intercom.
    /// </summary>
    public static void ResetIntercomCooldown() => CurrentState = IntercomState.Ready;

    /// <summary>
    /// Times out the intercom operation.
    /// </summary>
    public static void TimeoutIntercom() => CurrentState = IntercomState.Cooldown;

    /// <summary>
    /// Gets or sets the text displayed on the IntercomDisplay.
    /// </summary>
    public static string IntercomDisplayText
    {
        get => IntercomDisplay.Network_overrideText;
        set => IntercomDisplay.Network_overrideText = value;
    }

    /// <summary>
    /// Gets or sets the current state of the intercom.
    /// </summary>
    public static IntercomState CurrentState
    {
        get => BaseIntercom.State;
        set => BaseIntercom.State = value;
    }

    /// <summary>
    /// Determines if the intercom is currently in use.
    /// </summary>
    public static bool IsIntercomInUse => CurrentState is IntercomState.InUse or IntercomState.Starting;

    /// <summary>
    /// Gets the current speaker using the intercom, or returns null if no speaker is active.
    /// </summary>
    public static NebuliPlayer CurrentIntercomSpeaker => !IsIntercomInUse ? null : NebuliPlayer.Get(BaseIntercom._singleton._curSpeaker);

    /// <summary>
    /// Gets or sets the remaining cooldown time for the intercom.
    /// </summary>
    public static double RemainingCooldownTime
    {
        get => BaseIntercom._singleton.Network_nextTime - NetworkTime.time;
        set => BaseIntercom._singleton.Network_nextTime = NetworkTime.time + value;
    }

    /// <summary>
    /// Gets or sets the remaining time for speech transmission via the intercom.
    /// </summary>
    public static float SpeechRemainingTransmissionTime
    {
        get => !IsIntercomInUse ? 0f : BaseIntercom._singleton.RemainingTime;
        set => BaseIntercom._singleton._nextTime = NetworkTime.time + value;
    }
}