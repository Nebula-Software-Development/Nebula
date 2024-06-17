// -----------------------------------------------------------------------
// <copyright file=Radio.cs company="Nebula-Software-Development">
// Copyright (c) Nebula-Software-Development. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using static InventorySystem.Items.Radio.RadioMessages;
using RadioItemBase = InventorySystem.Items.Radio.RadioItem;

namespace Nebula.API.Features.Items
{
    /// <summary>
    ///     Wrapper class for Radios.
    /// </summary>
    public class Radio : Item
    {
        internal Radio(RadioItemBase itemBase) : base(itemBase)
        {
            Base = itemBase;
        }

        /// <summary>
        ///     Gets the <see cref="RadioItemBase" /> base.
        /// </summary>
        public new RadioItemBase Base { get; }

        /// <summary>
        ///     Gets or sets the radios current battery percentage.
        /// </summary>
        public byte BatteryPercent
        {
            get => Base.BatteryPercent;
            set => Base.BatteryPercent = value;
        }

        /// <summary>
        ///     Gets if the radio is usable.
        /// </summary>
        public bool IsUsable => Base.IsUsable;

        /// <summary>
        ///     Gets the radios current <see cref="RadioRangeLevel" />.
        /// </summary>
        public RadioRangeLevel CurrentRange => Base.RangeLevel;
    }
}