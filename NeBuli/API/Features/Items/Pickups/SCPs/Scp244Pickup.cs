using Scp244Base = InventorySystem.Items.Usables.Scp244.Scp244DeployablePickup;

namespace Nebuli.API.Features.Items.Pickups.SCPs
{
    public class Scp244Pickup : Pickup
    {
        /// <summary>
        /// Gets the <see cref="Scp244Base"/> base.
        /// </summary>
        public new Scp244Base Base { get; }

        internal Scp244Pickup(Scp244Base pickupBase) : base(pickupBase)
        {
            Base = pickupBase;
        }
    }
}