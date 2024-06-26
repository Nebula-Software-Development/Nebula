﻿// -----------------------------------------------------------------------
// <copyright file=TantrumHazard.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using RelativePositioning;
using UnityEngine;
using TantrumHazardBase = Hazards.TantrumEnvironmentalHazard;

namespace Nebula.API.Features.Hazards
{
    public class TantrumHazard : TemporaryHazard
    {
        internal TantrumHazard(TantrumHazardBase tantrumHazardBase) : base(tantrumHazardBase)
        {
            Base = tantrumHazardBase;
        }

        /// <summary>
        ///     Gets the <see cref="TantrumHazardBase" /> base.
        /// </summary>
        public new TantrumHazardBase Base { get; }

        /// <summary>
        ///     Gets or sets the SynchronizedPosition of the tantrum.
        /// </summary>
        public RelativePosition SynchronizedPosition
        {
            get => Base.SynchronizedPosition;
            set => Base.SynchronizedPosition = value;
        }

        /// <summary>
        ///     Gets or sets if the tantrum will play a sizzle sound effect.
        /// </summary>
        public bool PlaySizzle
        {
            get => Base.PlaySizzle;
            set => Base.PlaySizzle = value;
        }

        /// <summary>
        ///     Gets or sets the correct position of the tantrum.
        /// </summary>
        public Transform CorrectPosition
        {
            get => Base._correctPosition;
            set => Base._correctPosition = value;
        }
    }
}