// -----------------------------------------------------------------------
// <copyright file=Adrenaline.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using AdrenalineBase = InventorySystem.Items.Usables.Adrenaline;

namespace Nebuli.API.Features.Items.Usables;

public class Adrenaline : Usable
{
    /// <summary>
    /// Gets the <see cref="AdrenalineBase"/> base.
    /// </summary>
    public new AdrenalineBase Base { get; }

    internal Adrenaline(AdrenalineBase itemBase) : base(itemBase)
    {
        Base = itemBase;
    }

    /// <summary>
    /// Activates the usable's effects.
    /// </summary>
    public void ActivateEffect() => Base.ActivateEffects();

    /// <summary>
    /// Gets if the usable is ready to be activated.
    /// </summary>
    public bool ActivationReady => Base.ActivationReady;
}