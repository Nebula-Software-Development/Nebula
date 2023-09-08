using InventorySystem.Items.Usables.Scp330;
using System.Collections.Generic;
using Scp330Base = InventorySystem.Items.Usables.Scp330.Scp330Bag;

namespace Nebuli.API.Features.Items.SCPs;

public class Scp330 : Item
{
    /// <summary>
    /// Gets the <see cref="Scp330Base"/> base.
    /// </summary>
    public new Scp330Base Base { get; }
    internal Scp330(Scp330Base itemBase) : base(itemBase)
    {
        Base = itemBase;
    }

    /// <summary>
    /// Gets the current selected CandyId.
    /// </summary>
    public int SelectedCandyId => Base.SelectedCandyId;

    /// <summary>
    /// Drops candy at the specificed index.
    /// </summary>
    public void DropCandy(int index) => Base.DropCandy(index);

    /// <summary>
    /// Refreshes the candy bag.
    /// </summary>
    public void RefreshBag() => Base.ServerRefreshBag();

    /// <summary>
    /// Gets if candy is currently selected from the bag.
    /// </summary>
    public bool IsCandySelected => Base.IsCandySelected;

    /// <summary>
    /// Selects a piece of candy at the specified index.
    /// </summary>
    /// <param name="index"></param>
    public void SelectCandy(int index) => Base.SelectCandy(index);

    /// <summary>
    /// Tries to add the specified <see cref="CandyKindID"/> to the bag.
    /// </summary>
    /// <returns>True if succesful, otherwise false.</returns>
    public bool TryAddCandy(CandyKindID candy) => Base.TryAddSpecific(candy);

    /// <summary>
    /// Gets or sets the <see cref="CandyKindID"/> types inside the bag.
    /// </summary>
    public List<CandyKindID> Candies
    {
        get => Base.Candies;
        set => Base.Candies = value;
    }
}
