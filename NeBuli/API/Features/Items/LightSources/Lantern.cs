// -----------------------------------------------------------------------
// <copyright file=Lantern.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using LanternBase = InventorySystem.Items.ToggleableLights.Lantern.LanternItem;

namespace Nebuli.API.Features.Items.LightSources
{
    public class Lantern : Item
    {
        internal Lantern(LanternBase itemBase) : base(itemBase)
        {
            Base = itemBase;
        }

        /// <summary>
        ///     Gets the <see cref="LanternBase" /> base.
        /// </summary>
        public new LanternBase Base { get; }

        /// <summary>
        ///     Gets or sets if the lantern is emitting light.
        /// </summary>
        public bool IsEmittingLight
        {
            get => Base.IsEmittingLight;
            set => Base.IsEmittingLight = value;
        }
    }
}