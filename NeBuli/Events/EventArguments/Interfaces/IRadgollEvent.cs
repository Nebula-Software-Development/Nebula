// -----------------------------------------------------------------------
// <copyright file=IRadgollEvent.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebuli.API.Features;

namespace Nebuli.Events.EventArguments.Interfaces
{
    public interface IRadgollEvent
    {
        /// <summary>
        ///     The ragdoll of the event.
        /// </summary>
        public Ragdoll Ragdoll { get; }
    }
}