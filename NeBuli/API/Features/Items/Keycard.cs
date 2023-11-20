// -----------------------------------------------------------------------
// <copyright file=Keycard.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Interactables.Interobjects.DoorUtils;

namespace Nebuli.API.Features.Items;

public class Keycard : Item
{
    /// <summary>
    /// Gets the keycards base.
    /// </summary>
    public new InventorySystem.Items.Keycards.KeycardItem Base { get; }

    internal Keycard(InventorySystem.Items.Keycards.KeycardItem itemBase) : base(itemBase)
    {
        Base = itemBase;
    }

    /// <summary>
    /// Gets or sets the keycards <see cref="KeycardPermissions"/>.
    /// </summary>
    public KeycardPermissions Permissions
    {
        get => Base.Permissions;
        set => Base.Permissions = value;
    }
}