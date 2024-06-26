// -----------------------------------------------------------------------
// <copyright file=IRadgollEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Nebula.API.Features;

namespace Nebula.Events.EventArguments.Interfaces
{
    public interface IRadgollEvent
    {
        /// <summary>
        ///     The ragdoll of the event.
        /// </summary>
        public Ragdoll Ragdoll { get; }
    }
}