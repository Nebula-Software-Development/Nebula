// -----------------------------------------------------------------------
// <copyright file=Scp096Handlers.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebula.Events.EventArguments.SCPs.Scp096;

namespace Nebula.Events.Handlers
{
    public static class Scp096Handlers
    {
        /// <summary>
        ///     Triggered when SCP-096 is adding a target to its enraged list.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp096AddingTargetEvent> AddingTarget;

        /// <summary>
        ///     Triggered when SCP-096 is trying to pry open a gate.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp096PryingGateEvent> PryingGate;

        /// <summary>
        ///     Triggered when SCP-096 is entering an enraged state.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp096EnragingEvent> Enraging;

        /// <summary>
        ///     Triggered when SCP-096 is calming down from an enraged state.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp096CalmingEvent> Calming;

        internal static void OnAddingTarget(Scp096AddingTargetEvent ev)
        {
            AddingTarget.CallEvent(ev);
        }

        internal static void OnPryingGate(Scp096PryingGateEvent ev)
        {
            PryingGate.CallEvent(ev);
        }

        internal static void OnEnraging(Scp096EnragingEvent ev)
        {
            Enraging.CallEvent(ev);
        }

        internal static void OnCalming(Scp096CalmingEvent ev)
        {
            Calming.CallEvent(ev);
        }
    }
}