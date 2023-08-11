using Nebuli.API.Features.Player;
using PryableDoorBase = Interactables.Interobjects.PryableDoor;

namespace Nebuli.API.Features.Doors;

public class PryableDoor : Door
{
    /// <summary>
    /// Gets the <see cref="PryableDoorBase"/> base.
    /// </summary>
    public new PryableDoorBase Base { get; }
    internal PryableDoor(PryableDoorBase door) : base(door)
    {
        Base = door;
    }

    /// <summary>
    /// Gets or sets if SCP-106 can passthrough.
    /// </summary>
    public bool Can106Passthrough
    {
        get => Base.IsScp106Passable;
        set => Base.IsScp106Passable = true;
    }

    /// <summary>
    /// Gets or sets if SCP-106 can passthrough the door while its locked.
    /// </summary>
    public bool Restrict106PassthroughWhenLocked
    {
        get => Base.Network_restrict106WhileLocked;
        set => Base.Network_restrict106WhileLocked = value;
    }

    /// <summary>
    /// Tries to pry a gate using the provided player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool TryPryGate(NebuliPlayer player) => Base.TryPryGate(player.ReferenceHub);
}
