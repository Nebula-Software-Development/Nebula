// -----------------------------------------------------------------------
// <copyright file=Scp0492Handlers.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebula.Events.EventArguments.SCPs.Scp0492;

namespace Nebula.Events.Handlers
{
    public static class Scp0492Handlers
    {
        /// <summary>
        ///     Triggered when SCP-049-2 consumes a corpse.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp0492ConsumeCorpseEvent> ConsumeCorpse;

        /// <summary>
        ///     Triggered when SCP-049-2 finishes consuming a corpse.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp0492CorpseConsumedEvent> CorpseConsumed;

        /// <summary>
        ///     Triggered when SCP-049-2 attacks a player.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp0492AttackEvent> Attack;

        /// <summary>
        ///     Triggered when SCP-049-2 goes into a bloodlust state.
        /// </summary>
        public static event EventManager.CustomEventHandler<Scp0492BloodlustEvent> BloodLust;

        internal static void OnConsumeCorpse(Scp0492ConsumeCorpseEvent ev)
        {
            ConsumeCorpse.CallEvent(ev);
        }

        internal static void OnCorpseConsumed(Scp0492CorpseConsumedEvent ev)
        {
            CorpseConsumed.CallEvent(ev);
        }

        internal static void OnAttack(Scp0492AttackEvent ev)
        {
            Attack.CallEvent(ev);
        }

        internal static void OnBloodLust(Scp0492BloodlustEvent ev)
        {
            BloodLust.CallEvent(ev);
        }
    }
}