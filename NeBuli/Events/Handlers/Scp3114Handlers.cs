﻿using Nebuli.Events.EventArguments.SCPs.Scp3114;

namespace Nebuli.Events.Handlers;

public static class Scp3114Handlers
{
    /// <summary>
    /// Triggered when SCP-3114 starts to complete its disguise.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp3114DisguisingEvent> Disguising;

    /// <summary>
    /// Triggered when SCP-3114 completes its disguise.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp3114DisguisedEvent> Disguised;

    /// <summary>
    /// Triggered when SCP-3114 starts to strangle.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp3114StranglingEvent> Strangling;

    /// <summary>
    /// Triggered before SCP-3114 reveals.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp3114RevealEvent> Reveal;

    internal static void OnDisguising(Scp3114DisguisingEvent ev) => Disguising.CallEvent(ev);

    internal static void OnDisguised(Scp3114DisguisedEvent ev) => Disguised.CallEvent(ev);

    internal static void OnStrangle(Scp3114StranglingEvent ev) => Strangling.CallEvent(ev);

    internal static void OnReveal(Scp3114RevealEvent ev) => Reveal.CallEvent(ev);
}
