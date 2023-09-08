using InventorySystem.Items.Usables.Scp330;
using System.Collections.Generic;
using Scp330Base = InventorySystem.Items.Usables.Scp330.Scp330Pickup;

namespace Nebuli.API.Features.Items.Pickups.SCPs;

public class Scp330Pickup : Pickup
{
    /// <summary>
    /// Gets the <see cref="Scp330Base"/> base.
    /// </summary>
    public new Scp330Base Base { get; }
    internal Scp330Pickup(Scp330Base scp330) : base(scp330)
    {
        Base = scp330;
    }

    /// <summary>
    /// Gets or sets the ExposedCandy.
    /// </summary>
    public CandyKindID ExposedCandy
    {
        get => Base.ExposedCandy;
        set => Base.NetworkExposedCandy = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="CandyKindID"/> types inside the bag.
    /// </summary>
    public List<CandyKindID> Candies
    {
        get => Base.StoredCandies;
        set => Base.StoredCandies = value;
    }
}
