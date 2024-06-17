// -----------------------------------------------------------------------
// <copyright file=Keycard.cs company="NebulaTeam">
// Copyright (c) NebulaTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Keycards;

namespace Nebula.API.Features.Items
{
    public class Keycard : Item
    {
        internal Keycard(KeycardItem itemBase) : base(itemBase)
        {
            Base = itemBase;
        }

        /// <summary>
        ///     Gets the keycards base.
        /// </summary>
        public new KeycardItem Base { get; }

        /// <summary>
        ///     Gets or sets the keycards <see cref="KeycardPermissions" />.
        /// </summary>
        public KeycardPermissions Permissions
        {
            get => Base.Permissions;
            set => Base.Permissions = value;
        }
    }
}