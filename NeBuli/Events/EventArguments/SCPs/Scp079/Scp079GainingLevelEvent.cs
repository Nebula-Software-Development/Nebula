// -----------------------------------------------------------------------
// <copyright file=Scp079GainingLevelEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using System;
using Nebuli.Events.EventArguments.Interfaces;

namespace Nebuli.Events.EventArguments.SCPs.Scp079
{
    /// <summary>
    ///     Triggered when SCP-079 is gaining a new level.
    /// </summary>
    public class Scp079GainingLevelEvent : EventArgs, IPlayerEvent, ICancellableEvent
    {
        public Scp079GainingLevelEvent(ReferenceHub player)
        {
            Player = API.Features.Player.Get(player);
            IsCancelled = false;
        }

        public bool IsCancelled { get; set; }

        public API.Features.Player Player { get; }
    }
}