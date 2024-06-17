// -----------------------------------------------------------------------
// <copyright file=Scp1576.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Scp1576Base = InventorySystem.Items.Usables.Scp1576.Scp1576Item;

namespace Nebula.API.Features.Items.SCPs
{
    public class Scp1576 : Usable
    {
        internal Scp1576(Scp1576Base itemBase) : base(itemBase)
        {
            Base = itemBase;
        }

        /// <summary>
        ///     Gets the <see cref="Scp1576Base" /> base.
        /// </summary>
        public new Scp1576Base Base { get; }
    }
}