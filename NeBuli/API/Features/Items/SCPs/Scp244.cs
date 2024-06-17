// -----------------------------------------------------------------------
// <copyright file=Scp244.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Scp244Base = InventorySystem.Items.Usables.Scp244.Scp244Item;

namespace Nebula.API.Features.Items.SCPs
{
    public class Scp244 : Usable
    {
        internal Scp244(Scp244Base itemBase) : base(itemBase)
        {
            Base = itemBase;
        }

        /// <summary>
        ///     Gets the <see cref="Scp244Base" /> base.
        /// </summary>
        public new Scp244Base Base { get; }
    }
}