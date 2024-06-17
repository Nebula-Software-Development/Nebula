// -----------------------------------------------------------------------
// <copyright file=Scp173Handlers.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebula.Events.EventArguments.SCPs.Scp173;

namespace Nebula.Events.Handlers
{
    public static class Scp173Handlers
    {
        /// <summary>
        ///     Triggered when SCP-173 performs a blink.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp173BlinkEvent> Blink;

        /// <summary>
        ///     Triggered when SCP-173 attempts to place a tantrum.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp173PlaceTantrumEvent> PlaceTantrum;

        /// <summary>
        ///     Triggered when SCP-173 toggles its breakneck speed mode.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp173ToggleBreakneckSpeedEvent> ToggleBreakneckSpeed;

        /// <summary>
        ///     Triggered when SCP-173 attacks a player.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp173AttackEvent> Attack;

        internal static void OnBlink(Scp173BlinkEvent ev)
        {
            Blink.CallEvent(ev);
        }

        internal static void OnPlaceTantrum(Scp173PlaceTantrumEvent ev)
        {
            PlaceTantrum.CallEvent(ev);
        }

        internal static void OnToggleBreakneckSpeed(Scp173ToggleBreakneckSpeedEvent ev)
        {
            ToggleBreakneckSpeed.CallEvent(ev);
        }

        internal static void OnAttack(Scp173AttackEvent ev)
        {
            Attack.CallEvent(ev);
        }
    }
}