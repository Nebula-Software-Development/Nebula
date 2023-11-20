// -----------------------------------------------------------------------
// <copyright file=Medkit.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using MedkitBase = InventorySystem.Items.Usables.Medkit;

namespace Nebuli.API.Features.Items.Usables;

public class Medkit : Usable
{
    /// <summary>
    /// Gets the <see cref="MedkitBase"/> base.
    /// </summary>
    public new MedkitBase Base { get; }

    internal Medkit(MedkitBase itemBase) : base(itemBase)
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