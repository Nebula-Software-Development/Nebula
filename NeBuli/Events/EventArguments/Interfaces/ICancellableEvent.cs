// -----------------------------------------------------------------------
// <copyright file=ICancellableEvent.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

namespace Nebula.Events.EventArguments.Interfaces
{
    public interface ICancellableEvent
    {
        /// <summary>
        ///     Gets or sets if the event is cancelled.
        /// </summary>
        public bool IsCancelled { get; set; }
    }
}