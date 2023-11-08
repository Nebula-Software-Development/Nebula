// -----------------------------------------------------------------------
// <copyright file=Usable.cs company="NebuliTeam">
// Copyright (c) NebuliTeam. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// -----------------------------------------------------------------------

using UsableBase = InventorySystem.Items.Usables.UsableItem;

namespace Nebuli.API.Features.Items;

/// <summary>
/// Wrapper class for <see cref="UsableBase"/>.
/// </summary>
public class Usable : Item
{
    /// <summary>
    /// Gets the <see cref="UsableBase"/> base.
    /// </summary>
    public new UsableBase Base { get; }

    public Usable(UsableBase usableBase) : base(usableBase)
    {
        Base = usableBase;
    }

    /// <summary>
    /// Gets if the <see cref="Usable"/> can be used.
    /// </summary>
    public bool CanStartUsing => Base.CanStartUsing;

    /// <summary>
    /// Gets or sets if the <see cref="Usable"/> is being used.
    /// </summary>
    public bool IsBeingUsed
    {
        get => Base.IsUsing;
        set => Base.IsUsing = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="Usable"/> max cancellable time.
    /// </summary>
    public float MaxCancellableTime
    {
        get => Base.MaxCancellableTime;
        set => Base.MaxCancellableTime = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="Usable"/> use time.
    /// </summary>
    public float UseTime
    {
        get => Base.UseTime;
        set => Base.UseTime = value;
    }
}