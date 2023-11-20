// -----------------------------------------------------------------------
// <copyright file=Scp079Handlers.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.Events.EventArguments.SCPs.Scp079;

namespace Nebuli.Events.Handlers;

public static class Scp079Handlers
{
    /// <summary>
    /// Triggered when SCP-079 is pinging a device.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp079PingingEvent> Pinging;

    /// <summary>
    /// Triggered when SCP-079 is losing its signal.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp079LosingSignalEvent> LosingSignal;

    /// <summary>
    /// Triggered when SCP-079 is gaining experience points.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp079GainingExperienceEvent> GainingExperience;

    /// <summary>
    /// Triggered when SCP-079 is gaining a new level.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp079GainingLevelEvent> GainingLevel;

    /// <summary>
    /// Triggered when SCP-079 is changing its camera view.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp079ChangingCameraEvent> ChangingCamera;

    /// <summary>
    /// Triggered when SCP-079 is interacting with a Tesla gate.
    /// </summary>
    public static event EventManager.CustomEventHandler<Scp079InteractingTeslaEvent> InteractingTesla;

    internal static void OnScp079Pinging(Scp079PingingEvent ev) => Pinging.CallEvent(ev);

    internal static void OnScp079LosingSignal(Scp079LosingSignalEvent ev) => LosingSignal.CallEvent(ev);

    internal static void OnScp079GainingExpereince(Scp079GainingExperienceEvent ev) => GainingExperience.CallEvent(ev);

    internal static void OnScp079GainingLevel(Scp079GainingLevelEvent ev) => GainingLevel.CallEvent(ev);

    internal static void OnScp079ChangingCamera(Scp079ChangingCameraEvent ev) => ChangingCamera.CallEvent(ev);

    internal static void OnScp079InteractingTesla(Scp079InteractingTeslaEvent ev) => InteractingTesla.CallEvent(ev);
}