﻿// -----------------------------------------------------------------------
// <copyright file=Adrenaline.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using AdrenalineBase = InventorySystem.Items.Usables.Adrenaline;

namespace Nebula.API.Features.Items.Usables
{
    public class Adrenaline : Usable
    {
        internal Adrenaline(AdrenalineBase itemBase) : base(itemBase)
        {
            Base = itemBase;
        }

        /// <summary>
        ///     Gets the <see cref="AdrenalineBase" /> base.
        /// </summary>
        public new AdrenalineBase Base { get; }

        /// <summary>
        ///     Gets if the usable is ready to be activated.
        /// </summary>
        public bool ActivationReady => Base.ActivationReady;

        /// <summary>
        ///     Activates the usable's effects.
        /// </summary>
        public void ActivateEffect()
        {
            Base.ActivateEffects();
        }
    }
}