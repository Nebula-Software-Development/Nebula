// -----------------------------------------------------------------------
// <copyright file=Painkillers.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using PainkillerBase = InventorySystem.Items.Usables.Painkillers;

namespace Nebuli.API.Features.Items.Usables;

public class Painkillers : Usable
{
    /// <summary>
    /// Gets the <see cref="PainkillerBase"/> base.
    /// </summary>
    public new PainkillerBase Base { get; }

    internal Painkillers(PainkillerBase itemBase) : base(itemBase)
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