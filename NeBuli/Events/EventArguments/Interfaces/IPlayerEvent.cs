// -----------------------------------------------------------------------
// <copyright file=IPlayerEvent.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Nebula.Events.EventArguments.Interfaces
{
    public interface IPlayerEvent
    {
        /// <summary>
        ///     The player triggering the event.
        /// </summary>
        public API.Features.Player Player { get; }
    }
}