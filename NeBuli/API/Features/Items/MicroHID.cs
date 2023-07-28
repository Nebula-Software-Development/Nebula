using MicroHIDBase = InventorySystem.Items.MicroHID.MicroHIDItem;

namespace Nebuli.API.Features.Items
{
    public class MicroHID : Item
    {
        public new MicroHIDBase Base { get; }
        internal MicroHID(MicroHIDBase itemBase) : base(itemBase)
        {
            Base = itemBase;
        }
    }
}
