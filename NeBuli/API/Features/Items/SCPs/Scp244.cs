// -----------------------------------------------------------------------
// <copyright file=Scp244.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using Scp244Base = InventorySystem.Items.Usables.Scp244.Scp244Item;

namespace Nebuli.API.Features.Items.SCPs;

public class Scp244 : Usable
{
    /// <summary>
    /// Gets the <see cref="Scp244Base"/> base.
    /// </summary>
    public new Scp244Base Base { get; }

    internal Scp244(Scp244Base itemBase) : base(itemBase)
    {
        Base = itemBase;
    }
}